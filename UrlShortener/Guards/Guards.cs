namespace UrlShortener.Guards
{
    public static class Guard
    {
        public static void AgainstNotFound(object? resource, string errorMessage = "Match not found")
        {
            if (resource is null)
            {
                Console.WriteLine(errorMessage);
            }
        }
    }
}
