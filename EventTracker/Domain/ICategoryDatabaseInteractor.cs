using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface ICategoryDatabaseInteractor
    {
        Category Add(Category categoryItem);
        Category Update(long categoryId, Category categoryItem);
        void Delete(long categoryId);
        Category Get(long categoryId);
    }
}
