using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.DAL
{
    public interface IDataAccess
    {
        List<T> GetData<T>(FileName fileName);
        Task SaveData<T>(List<T> list, FileName fileName);
    }
}