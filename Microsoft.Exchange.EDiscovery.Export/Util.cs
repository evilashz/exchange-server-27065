using System;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000031 RID: 49
	internal static class Util
	{
		// Token: 0x0600018F RID: 399 RVA: 0x00005D6C File Offset: 0x00003F6C
		internal static bool IncludeUnsearchableItems(IExportContext exportContext)
		{
			if (exportContext == null || exportContext.ExportMetadata == null)
			{
				throw new ArgumentException("exportContext.ExportMetadata should not be null.");
			}
			if (exportContext.ExportMetadata.IncludeUnsearchableItems)
			{
				if (exportContext.Sources == null || exportContext.Sources.Count == 0)
				{
					return exportContext.ExportMetadata.IncludeUnsearchableItems;
				}
				string pattern = string.Format("(^$|^{0}$|^received[><]=\\\".+\\\"$|^\\(received>=\\\".+\\\" AND received<=\\\".+\\\"\\)$)", Util.EmptyQueryReplacement);
				Regex regex = new Regex(pattern);
				string sourceFilter = exportContext.Sources[0].SourceFilter;
				if (regex.IsMatch(sourceFilter))
				{
					return false;
				}
			}
			return exportContext.ExportMetadata.IncludeUnsearchableItems;
		}

		// Token: 0x040000A5 RID: 165
		internal static readonly string EmptyQueryReplacement = "size>=0";
	}
}
