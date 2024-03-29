namespace zfiFamilyRenameTool.Services
{
    using System.Collections.Generic;
    using Abstractions;
    using Autodesk.Revit.DB;
    using Exceptions;

    public class FamilyParametersProvider : IRenameableProvider
    {
        public string Name => "Параметры семейств";

        public IEnumerable<IRenameable> GetRenameables(Document doc)
        {
            if (!doc.IsFamilyDocument)
            {
                throw new PluginException("Документ не является семейством");
            }

            var fm = doc.FamilyManager;

            foreach (FamilyParameter p in fm.Parameters)
            {
                if (p.IsShared || p.IsReadOnly || p.Id.IntegerValue < 0)
                {
                    continue;
                }

                yield return new FamilyParameterWrapper(p, doc);
            }
        }
    }
}