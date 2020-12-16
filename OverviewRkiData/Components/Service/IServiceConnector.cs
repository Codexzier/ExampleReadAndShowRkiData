using System.Threading.Tasks;

namespace OverviewRkiData.Components.Service
{
    public interface IServiceConnector
    {
        void SetAddress(string serverAddres);

        Task<IResponse> AddDvdItem(IRequest request);

        Task<IResponse> GetDvdItem(long id);

        Task<IResponse> GetDvdItems();

        Task<IResponse> EditDvdItem(IRequest request);
    }
}
