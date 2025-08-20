using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LitobzorImport.Contracts
{
    public interface IImportService
    {
        void CleanDatabase();

        void ImportReferences();

        void ImportRelations();

        void ImportFullHierarchy();

        void ShowStatistics();
    }
}
