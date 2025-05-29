using System.ComponentModel;

namespace AccessControl.Domain.Enums
{
    public enum DoorTypeEnum
    {
        [Description("Regular Door")]
        Regular = 0,
        [Description("Tripod")]
        Tripod = 1,
        [Description("Elevator")]
        Elevator = 2
    }

    public static class Extensions
    {
        static public string GetDescription(this Enum enumValue)
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());
            if (field == null)
                return enumValue.ToString();

            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                return attribute.Description;
            }

            return enumValue.ToString();
        }
    }
}
