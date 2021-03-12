using System;
using System.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000255 RID: 597
	internal class ElcFoldeFilterBuilder : IExchangeCommandFilterBuilder
	{
		// Token: 0x06001A63 RID: 6755 RVA: 0x00074AD8 File Offset: 0x00072CD8
		public void BuildFilter(out string parameterList, out string filter, out string preArgs, DataRow row)
		{
			preArgs = null;
			ElcFolderFilter elcFolderFilter = ElcFolderFilter.All;
			if (!DBNull.Value.Equals(row["FolderFilter"]))
			{
				elcFolderFilter = (ElcFolderFilter)row["FolderFilter"];
			}
			switch (elcFolderFilter)
			{
			case ElcFolderFilter.Default:
				filter = " | Filter-PropertyNotEqualTo -Property 'FolderType' -Value 'ManagedCustomFolder'";
				break;
			case ElcFolderFilter.Organizational:
				filter = " | Filter-PropertyEqualTo -Property 'FolderType' -Value 'ManagedCustomFolder'";
				break;
			default:
				filter = null;
				break;
			}
			parameterList = null;
		}
	}
}
