using AccessControl.Domain.Models;

namespace AccessControl.Domain.Interfaces.v1.UseCases
{
    public interface IDoorsUseCase
    {
        public abstract Task<Door> CreateDoor(int doorNumber, int doorType, string doorName);

        public abstract Task<string> RemoveDoor(int doorNumber);
    }
}
