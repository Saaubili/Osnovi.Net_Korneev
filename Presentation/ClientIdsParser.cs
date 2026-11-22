namespace Presentation
{
    public static class ClientIdsParser
    {
        public static bool TryParse(string? input, out long[] ids)
        {
            ids = [];

            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            var parts = input.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            var result = new List<long>();

            foreach (var part in parts)
            {
                if (!long.TryParse(part, out var id))
                {
                    return false;
                }

                result.Add(id);
            }

            if (result.Count == 0)
            {
                return false;
            }

            ids = result.ToArray();
            return true;
        }
    }
}