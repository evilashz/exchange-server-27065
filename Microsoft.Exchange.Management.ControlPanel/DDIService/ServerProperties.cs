using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200003C RID: 60
	public static class ServerProperties
	{
		// Token: 0x0600196B RID: 6507 RVA: 0x00050108 File Offset: 0x0004E308
		public static void GetListPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			dataTable.BeginLoadData();
			ServerVersion b = (inputRow["MaxMajorVersion"] is DBNull || string.IsNullOrEmpty((string)inputRow["MaxMajorVersion"])) ? new ServerVersion(int.MaxValue, 0, 0, 0) : new ServerVersion(int.Parse((string)inputRow["MaxMajorVersion"]), int.MaxValue, 0, 0);
			ServerVersion b2 = (inputRow["MinMajorVersion"] is DBNull || string.IsNullOrEmpty((string)inputRow["MinMajorVersion"])) ? new ServerVersion(0, 0, 0, 0) : new ServerVersion(int.Parse((string)inputRow["MinMajorVersion"]), 0, 0, 0);
			ArrayList arrayList = new ArrayList();
			if (!DDIHelper.IsEmptyValue(inputRow["ServerRole"]))
			{
				string text = (string)inputRow["ServerRole"];
				string[] array = text.Split(new char[]
				{
					','
				}, StringSplitOptions.RemoveEmptyEntries);
				if (array != null && array.Length > 0)
				{
					foreach (string value in array)
					{
						ServerRole serverRole;
						if (Enum.TryParse<ServerRole>(value, out serverRole))
						{
							arrayList.Add(serverRole);
						}
					}
				}
			}
			List<DataRow> list = new List<DataRow>(dataTable.Rows.Count);
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (!ServerProperties.ServerHasRole(arrayList, (ServerRole)dataRow["ServerRole"]))
				{
					list.Add(dataRow);
				}
				else
				{
					ServerVersion a = (ServerVersion)dataRow["AdminDisplayVersion"];
					if (ServerVersion.Compare(a, b) > 0 || ServerVersion.Compare(a, b2) < 0)
					{
						list.Add(dataRow);
					}
				}
			}
			foreach (DataRow row in list)
			{
				dataTable.Rows.Remove(row);
			}
			dataTable.EndLoadData();
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x0005034C File Offset: 0x0004E54C
		public static void GetEdgeServerPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store, PowerShellResults[] results)
		{
			inputRow["ServerRole"] = "Edge";
			ServerProperties.GetListPostAction(inputRow, dataTable, store);
			if (dataTable.Rows.Count == 0 && results != null && results.Length == 1 && results[0].Succeeded)
			{
				DDIUtil.InsertError(results[0], Strings.NoEdgeServerFound);
			}
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x000503A4 File Offset: 0x0004E5A4
		private static bool ServerHasRole(ArrayList roles, ServerRole role)
		{
			if (roles != null)
			{
				foreach (object obj in roles)
				{
					ServerRole serverRole = (ServerRole)obj;
					if (role.HasFlag(serverRole))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}
	}
}
