using AccessControl.Domain.Enums;
using AccessControl.Domain.Interfaces.v1.Repository;
using AccessControl.Domain.Interfaces.v1.UseCases;
using AccessControl.Domain.Models;
using Microsoft.Extensions.Logging;

namespace AccessControl.Application.UseCases.v1
{
    public class DoorsUseCase(IDoorsRepository doorsRepository, ILogger<DoorsUseCase> logger) : IDoorsUseCase
    {
        private readonly IDoorsRepository _doorsRepository = doorsRepository ?? throw new ArgumentNullException(nameof(doorsRepository));
        private readonly ILogger<DoorsUseCase> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        public async Task<Door> CreateDoor(int doorNumber, DoorTypeEnum doorType, string doorName)
        {
            _logger.LogInformation(nameof(CreateDoor));
            return await _doorsRepository.AddDoor(doorNumber, doorType, doorName);
        }

        public async Task<string> RemoveDoor(int doorNumber)
        {
            _logger.LogInformation(nameof(RemoveDoor));
            return await _doorsRepository.RemoveDoor(doorNumber);
        }

        public async Task<IQueryable<Door>> ListDoors()
        {
            _logger.LogInformation(nameof(ListDoors));
            return await _doorsRepository.ListDoors();
        }
    }
}
