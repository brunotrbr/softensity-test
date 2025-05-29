using AccessControl.Domain.Enums;
using AccessControl.Domain.Models;

namespace AccessControl.Domain.Interfaces.v1.Repository
{
    public interface IDoorsRepository
    {
        public abstract Task<Door> AddDoor(int doorNumber, DoorTypeEnum doorType, string doorName);

        public abstract Task<string> RemoveDoor(int doorNumber);

        public abstract Task<IQueryable<Door>> ListDoors();
    }
}
