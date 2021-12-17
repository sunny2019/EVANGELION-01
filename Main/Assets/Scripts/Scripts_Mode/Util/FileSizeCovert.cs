namespace Mode
{
    public static class FileSizeCovert
    {
        public static float BytesToMb(long bytes)
        {
            return bytes / 1024f / 1024f;
        }
    }
}