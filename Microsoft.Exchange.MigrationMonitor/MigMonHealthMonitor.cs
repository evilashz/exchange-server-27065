using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000019 RID: 25
	internal static class MigMonHealthMonitor
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x000049DC File Offset: 0x00002BDC
		public static void PublishServerHealthStatus()
		{
			List<MigMonHealthMonitor.ServerInfo> list = MigMonHealthMonitor.FindMailboxServers();
			if (list == null || !list.Any<MigMonHealthMonitor.ServerInfo>())
			{
				MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Error, "No mailbox servers found in the site. Site is probably decomissioned.", new object[0]);
				return;
			}
			MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Information, "Uploading server health info.", new object[0]);
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("ServerFQDN", typeof(string));
			dataTable.Columns.Add("AdminDisplayVersion", typeof(string));
			dataTable.Columns.Add("IsOnline", typeof(bool));
			foreach (MigMonHealthMonitor.ServerInfo serverInfo in list)
			{
				dataTable.Rows.Add(new object[]
				{
					serverInfo.ServerFQDN,
					serverInfo.AdminDisplayVersion,
					serverInfo.IsOnline
				});
			}
			List<SqlParameter> list2 = new List<SqlParameter>();
			list2.Add(new SqlParameter("ServerList", dataTable)
			{
				SqlDbType = SqlDbType.Structured,
				TypeName = "dbo.ServerHealthStatus"
			});
			try
			{
				MigrationMonitor.SqlHelper.ExecuteSprocNonQuery("MIGMON_UpdateServerHealthStatus", list2, 30);
			}
			catch (SqlQueryFailedException ex)
			{
				MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Error, ex, "Error updating server health info to the database. Will attempt again next cycle.", new object[0]);
				throw new HealthStatusPublishFailureException(ex.InnerException);
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004B7C File Offset: 0x00002D7C
		private static List<MigMonHealthMonitor.ServerInfo> FindMailboxServers()
		{
			List<MigMonHealthMonitor.ServerInfo> list = new List<MigMonHealthMonitor.ServerInfo>();
			MiniServer[] array;
			try
			{
				AnchorADProvider rootOrgProvider = AnchorADProvider.GetRootOrgProvider(MigrationMonitor.MigrationMonitorContext);
				array = CommonUtils.FindServers(rootOrgProvider.ConfigurationSession, Server.E15MinVersion, ServerRole.Mailbox, CommonUtils.LocalSiteId, new ADPropertyDefinition[]
				{
					ServerSchema.ComponentStates
				});
			}
			catch (LocalizedException ex)
			{
				MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Error, "Exception encountered looking for servers. Will try again next cycle. Error: {0}", new object[]
				{
					ex
				});
				throw new HealthStatusPublishFailureException(ex);
			}
			if (array == null)
			{
				MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Information, "Found no e15 mbx servers on the local site", new object[0]);
				return list;
			}
			foreach (MiniServer miniServer in array)
			{
				list.Add(new MigMonHealthMonitor.ServerInfo
				{
					ServerFQDN = miniServer.Fqdn,
					AdminDisplayVersion = miniServer.AdminDisplayVersion.ToString(),
					IsOnline = ServerComponentStates.IsServerOnline((MultiValuedProperty<string>)miniServer[ServerSchema.ComponentStates])
				});
			}
			return list;
		}

		// Token: 0x0400008E RID: 142
		private const string SProcNameHealthUpdate = "MIGMON_UpdateServerHealthStatus";

		// Token: 0x0200001A RID: 26
		private struct ServerInfo
		{
			// Token: 0x1700002F RID: 47
			// (get) Token: 0x060000AB RID: 171 RVA: 0x00004C90 File Offset: 0x00002E90
			// (set) Token: 0x060000AC RID: 172 RVA: 0x00004C98 File Offset: 0x00002E98
			public string ServerFQDN { get; set; }

			// Token: 0x17000030 RID: 48
			// (get) Token: 0x060000AD RID: 173 RVA: 0x00004CA1 File Offset: 0x00002EA1
			// (set) Token: 0x060000AE RID: 174 RVA: 0x00004CA9 File Offset: 0x00002EA9
			public string AdminDisplayVersion { get; set; }

			// Token: 0x17000031 RID: 49
			// (get) Token: 0x060000AF RID: 175 RVA: 0x00004CB2 File Offset: 0x00002EB2
			// (set) Token: 0x060000B0 RID: 176 RVA: 0x00004CBA File Offset: 0x00002EBA
			public bool IsOnline { get; set; }
		}
	}
}
