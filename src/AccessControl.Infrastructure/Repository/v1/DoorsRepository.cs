using AccessControl.Domain.Enums;
using AccessControl.Domain.Interfaces.v1.Repository;
using AccessControl.Domain.Models;
using Microsoft.Extensions.Logging;

namespace AccessControl.Infrastructure.Repository.v1
{
    public class DoorsRepository(DBContext context, ILogger<DoorsRepository> logger) : IDoorsRepository
    {
        private readonly DBContext _context = context ?? throw new ArgumentNullException(nameof(context));
        private readonly ILogger<DoorsRepository> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        private static string GenerateDoorId(int doorNumber, DoorTypeEnum doorType)
        {
            return "D" + doorNumber + "T" + (int)doorType;
        }
        public Task<Door> AddDoor(int doorNumber, DoorTypeEnum doorType, string doorName)
        {
            _logger.LogInformation(nameof(AddDoor));
            using (_context)
            {
                if (_context.Doors.Any(d => d.Number == doorNumber))
                {
                    throw new InvalidOperationException($"Door with number {doorNumber} already exists.");
                }

                var doorId = GenerateDoorId(doorNumber, doorType);
                var doorDescription = doorType.GetDescription();

                Door d = new Door() { 
                    Id = doorId, 
                    Number = doorNumber,
                    Description = doorDescription,
                    Name = doorName, 
                    CreatedAt = DateTime.Now };

                _context.Doors.Add(d);
                _context.SaveChanges();
                return Task.FromResult(d);
            }
        }

        public Task<string> RemoveDoor(int doorNumber)
        {
            _logger.LogInformation(nameof(RemoveDoor));
            return Task.Run(() =>
            {
                if (!_context.Doors.Any(d => d.Number == doorNumber))
                {
                    throw new InvalidOperationException($"Door with number {doorNumber} does not exists.");
                }

                if(_context.Cards.Any(c => c.DoorsNumbersWithAccess.Contains(doorNumber)))
                {
                    throw new InvalidOperationException($"Door with number {doorNumber} cannot be removed because it is associated with one or more cards.");
                }

                var entity = _context.Doors.First(d => d.Number.Equals(doorNumber));

                _context.Remove(entity);
                _context.SaveChanges();

                return doorNumber.ToString();
            });
        }

        public Task<IQueryable<Door>> ListDoors()
        {
            _logger.LogInformation(nameof(ListDoors));
            return Task.FromResult(_context.Doors.AsQueryable());
        }
    }
}
