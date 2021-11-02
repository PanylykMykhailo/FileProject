using System.Threading.Tasks;
using FileSortService.Dtos;

namespace FileSortService.SyncDataServices.Http
{
    public interface IFileDataClient
    {
        Task SendFileSortToFileS(InfoAboutFileDto infoAboutFileDto);
    }
}