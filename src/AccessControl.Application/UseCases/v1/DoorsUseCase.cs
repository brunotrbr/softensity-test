using AccessControl.Domain.Enums;
using AccessControl.Domain.Interfaces.v1.Repository;
using AccessControl.Domain.Interfaces.v1.UseCases;
using AccessControl.Domain.Models;

namespace AccessControl.Application.UseCases.v1
{
    public class DoorsUseCase(IDoorsRepository doorsRepository) : IDoorsUseCase
    {
        private readonly IDoorsRepository _doorsRepository = doorsRepository ?? throw new ArgumentNullException(nameof(doorsRepository));
        
        public async Task<Door> CreateDoor(int doorNumber, DoorTypeEnum doorType, string doorName)
        {
            return await _doorsRepository.AddDoor(doorNumber, doorType, doorName);
        }

        public async Task<string> RemoveDoor(int doorNumber)
        {
            return await _doorsRepository.RemoveDoor(doorNumber);
        }

        public async Task<IQueryable<Door>> ListDoors()
        {
            return await _doorsRepository.ListDoors();
        }
    }
}
