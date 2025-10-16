namespace ToDoApp.BLL.Extentions
{
    public static class ResourcesExtentions
    {
        public static string FormatWith(this string resource, params object[] args)
        {
            return string.Format(resource, args);
        }
    }
}
