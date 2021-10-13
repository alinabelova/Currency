using System.Threading.Tasks;

namespace GPN.CR.TEST.Cron.Interfaces
{
    public interface ILoadCurrencyService
    {
        Task SaveToDb();
    }
}
