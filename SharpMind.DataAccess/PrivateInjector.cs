using System.Reflection;

namespace SharpMind.DataAccess
{
    public static class PrivateInjector
    {
        public static void SetField(this object entity, string fieldName, object value)
        {
            var field = entity.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);

            field.SetValue(entity, value);
        }

        public static object GetField(this object entity, string fieldName)
        {
            var field = entity.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);

            return field.GetValue(entity);
        }
    }
}
