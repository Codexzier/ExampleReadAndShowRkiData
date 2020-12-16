namespace OverviewRkiData.Components.Service
{
    internal class UploadResult
    {
        public string Content { get; internal set; } = string.Empty;
        public bool IsConnectionError { get; internal set; } = false;
        public bool Success { get; internal set; } = false;
    }
}