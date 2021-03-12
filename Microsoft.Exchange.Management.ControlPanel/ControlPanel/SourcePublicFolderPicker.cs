using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Exchange.Management.DDIService;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000360 RID: 864
	public static class SourcePublicFolderPicker
	{
		// Token: 0x17001F16 RID: 7958
		// (get) Token: 0x06002FCF RID: 12239 RVA: 0x00091AB1 File Offset: 0x0008FCB1
		// (set) Token: 0x06002FD0 RID: 12240 RVA: 0x00091AB8 File Offset: 0x0008FCB8
		public static string CurrentPath
		{
			get
			{
				return SourcePublicFolderPicker.currentPath;
			}
			set
			{
				SourcePublicFolderPicker.currentPath = value;
			}
		}

		// Token: 0x06002FD1 RID: 12241 RVA: 0x00091AC0 File Offset: 0x0008FCC0
		public static void GetListPreAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			List<string> list = new List<string>();
			string value = "\\";
			if (inputRow["SearchText"] != DBNull.Value && !string.IsNullOrWhiteSpace((string)inputRow["SearchText"]))
			{
				value = (((string)inputRow["SearchText"]).StartsWith("\\") ? ((string)inputRow["SearchText"]) : ((string)inputRow["SearchText"]).Insert(0, "\\"));
				inputRow["SearchText"] = value;
				list.Add("SearchText");
			}
			SourcePublicFolderPicker.CurrentPath = value;
			if (list.Count > 0)
			{
				store.SetModifiedColumns(list);
			}
		}

		// Token: 0x06002FD2 RID: 12242 RVA: 0x00091B7C File Offset: 0x0008FD7C
		public static void GetListPostAction(DataRow inputrow, DataTable dataTable, DataObjectStore store)
		{
			List<DataRow> list = new List<DataRow>();
			dataTable.BeginLoadData();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (string.Compare((string)dataRow["FolderPath"], "\\", StringComparison.Ordinal) == 0)
				{
					list.Add(dataRow);
				}
			}
			foreach (DataRow row in list)
			{
				dataTable.Rows.Remove(row);
			}
			dataTable.EndLoadData();
		}

		// Token: 0x0400231D RID: 8989
		internal const string SearchTextColumnName = "SearchText";

		// Token: 0x0400231E RID: 8990
		private static string currentPath = "\\";
	}
}
