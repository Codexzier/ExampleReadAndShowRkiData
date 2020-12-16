using OverviewRkiData.Components.Data;

namespace OverviewRkiData.Components.Service
{
    public class Response : IResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public CommonData[] DataItems { get; set; }
        public bool IsConnectionError { get; set; }
    }
}
