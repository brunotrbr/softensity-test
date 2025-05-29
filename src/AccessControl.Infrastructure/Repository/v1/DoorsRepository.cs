using AccessControl.Domain.Interfaces.v1.Repository;
using AccessControl.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Infrastructure.Repository.v1
{
    public class DoorsRepository(DBContext context) : IDoorsRepository
    {
        private readonly DBContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public Task<Door> AddDoor(int doorNumber, int doorType, string doorName)
        {
            using (_context)
            {
                Door d = new Door() { Id = "D" + doorNumber + "T" + doorType, Number = doorNumber, Name = doorName, CreatedAt = DateTime.Now };
                // turn into enum
                if (doorType == 0)
                {
                    d.Description = "Regular door";
                }
                else if (doorType == 1)
                {
                    d.Description = "Tripod";
                }
                else if (doorType == 2)
                {
                    d.Description = "Elevator";
                }
                if (doorNumber < 0 || doorNumber > 100)
                {
                    throw new Exception("Incorrect door number");
                }
                _context.Doors.Add(d);
                _context.SaveChangesAsync();
                return Task.FromResult(d);
            }
        }

        public Task<string> RemoveDoor(int doorNumber)
        {
            throw new NotImplementedException();
        }
    }
}
