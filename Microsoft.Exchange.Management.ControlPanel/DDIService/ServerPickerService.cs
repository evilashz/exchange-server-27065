using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200005A RID: 90
	public class ServerPickerService : DDICodeBehind
	{
		// Token: 0x06001A1F RID: 6687 RVA: 0x000539F0 File Offset: 0x00051BF0
		public static void GetReceiveConnectorServerListPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			dataTable.BeginLoadData();
			List<DataRow> list = new List<DataRow>();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				ServerRole serverRole = (ServerRole)dataRow["ServerRole"];
				ServerVersion serverVersion = (ServerVersion)dataRow["AdminDisplayVersion"];
				if (serverVersion.Major == 15)
				{
					if ((serverRole & ServerRole.ClientAccess) != ServerRole.ClientAccess && (serverRole & ServerRole.Mailbox) != ServerRole.Mailbox)
					{
						list.Add(dataRow);
					}
					else if ((serverRole & ServerRole.ClientAccess) == ServerRole.ClientAccess && (serverRole & ServerRole.Mailbox) == ServerRole.Mailbox)
					{
						dataRow["ServerData"] = string.Format("{0},{1}", (string)dataRow["Fqdn"], ServerPickerService.ServerRoleType.Mixed.ToString());
					}
					else if ((serverRole & ServerRole.ClientAccess) == ServerRole.ClientAccess)
					{
						dataRow["ServerData"] = string.Format("{0},{1}", (string)dataRow["Fqdn"], ServerPickerService.ServerRoleType.ClientAccess.ToString());
					}
					else if ((serverRole & ServerRole.Mailbox) == ServerRole.Mailbox)
					{
						dataRow["ServerData"] = string.Format("{0},{1}", (string)dataRow["Fqdn"], ServerPickerService.ServerRoleType.Mailbox.ToString());
					}
				}
				else if ((serverRole & ServerRole.HubTransport) != ServerRole.HubTransport)
				{
					list.Add(dataRow);
				}
				else
				{
					dataRow["ServerData"] = string.Format("{0},{1}", (string)dataRow["Fqdn"], ServerPickerService.ServerRoleType.HubTransport.ToString());
				}
			}
			foreach (DataRow row in list)
			{
				dataTable.Rows.Remove(row);
			}
			dataTable.EndLoadData();
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x00053BF8 File Offset: 0x00051DF8
		public static void GetSendConnectorSouceServerListPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			dataTable.BeginLoadData();
			List<DataRow> list = new List<DataRow>();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				bool flag = (bool)dataRow["IsEdgeServer"];
				bool flag2 = (bool)dataRow["IsHubTransportServer"];
				if (!flag && !flag2)
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

		// Token: 0x06001A21 RID: 6689 RVA: 0x00053CD8 File Offset: 0x00051ED8
		public static string ExtractServerId(string serverData)
		{
			return serverData.Split(new char[]
			{
				','
			})[0];
		}

		// Token: 0x0200005B RID: 91
		public enum ServerRoleType
		{
			// Token: 0x04001B04 RID: 6916
			Mixed = 1,
			// Token: 0x04001B05 RID: 6917
			ClientAccess,
			// Token: 0x04001B06 RID: 6918
			Mailbox,
			// Token: 0x04001B07 RID: 6919
			HubTransport
		}
	}
}
