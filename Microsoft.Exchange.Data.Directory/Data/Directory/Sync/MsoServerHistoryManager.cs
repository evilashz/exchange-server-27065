using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020007E3 RID: 2019
	internal class MsoServerHistoryManager
	{
		// Token: 0x060063E7 RID: 25575 RVA: 0x0015ADD4 File Offset: 0x00158FD4
		internal MsoServerHistoryManager(string serviceInstanceName, int maxServerHistoryEntries, bool createRootContainer)
		{
			this.serverHistorySession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 63, ".ctor", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\Sync\\MsoServerHistoryManager.cs");
			this.serverHistorySession.UseConfigNC = false;
			this.MaxServerHistoryEntries = maxServerHistoryEntries;
			this.ServiceInstanceName = serviceInstanceName;
			ADObjectId serviceInstanceObjectId = SyncServiceInstance.GetServiceInstanceObjectId(this.ServiceInstanceName);
			Container container = this.serverHistorySession.Read<Container>(serviceInstanceObjectId.GetChildId("ServerHistory"));
			if (container == null && createRootContainer)
			{
				container = new Container();
				container.SetId(serviceInstanceObjectId.GetChildId("ServerHistory"));
				try
				{
					this.serverHistorySession.Save(container);
				}
				catch (ADObjectAlreadyExistsException)
				{
				}
			}
			if (container != null)
			{
				ServerHistoryEntry[] array = this.serverHistorySession.Find<ServerHistoryEntry>(container.Id, QueryScope.OneLevel, null, null, 0);
				Array.Sort<ServerHistoryEntry>(array, (ServerHistoryEntry x, ServerHistoryEntry y) => DateTime.Compare(DateTime.Parse(x.Name.Substring(0, x.Name.IndexOf("-"))), DateTime.Parse(y.Name.Substring(0, y.Name.IndexOf("-")))));
				this.serverHistoryEntriesList = array.ToList<ServerHistoryEntry>();
				this.serverHistoryRootContainerID = container.Id;
			}
		}

		// Token: 0x17002368 RID: 9064
		// (get) Token: 0x060063E8 RID: 25576 RVA: 0x0015AEDC File Offset: 0x001590DC
		// (set) Token: 0x060063E9 RID: 25577 RVA: 0x0015AEE4 File Offset: 0x001590E4
		public int MaxServerHistoryEntries { get; set; }

		// Token: 0x17002369 RID: 9065
		// (get) Token: 0x060063EA RID: 25578 RVA: 0x0015AEED File Offset: 0x001590ED
		// (set) Token: 0x060063EB RID: 25579 RVA: 0x0015AEF5 File Offset: 0x001590F5
		public string ServiceInstanceName { get; set; }

		// Token: 0x060063EC RID: 25580 RVA: 0x0015AF00 File Offset: 0x00159100
		internal void UpdateOrCreateServerHistoryEntry(DateTime activeTimestamp)
		{
			ServerHistoryEntryData serverHistoryEntryData = this.ReadLastSyncServerHistory();
			string machineName = Environment.MachineName;
			if (serverHistoryEntryData != null && !string.Equals(serverHistoryEntryData.ServerName, machineName, StringComparison.InvariantCultureIgnoreCase) && serverHistoryEntryData.ActiveTimestamp.Equals(serverHistoryEntryData.PassiveTimestamp))
			{
				serverHistoryEntryData.PassiveReason = "Server became passive for unknown reason. Passive timestamp is not accurate.";
				this.WriteSyncServerHistory(serverHistoryEntryData, false);
			}
			if (serverHistoryEntryData == null || !string.Equals(serverHistoryEntryData.ServerName, machineName, StringComparison.InvariantCultureIgnoreCase) || !serverHistoryEntryData.ActiveTimestamp.Equals(serverHistoryEntryData.PassiveTimestamp))
			{
				this.WriteSyncServerHistory(new ServerHistoryEntryData(machineName, activeTimestamp, activeTimestamp, string.Empty), true);
			}
		}

		// Token: 0x060063ED RID: 25581 RVA: 0x0015AF94 File Offset: 0x00159194
		internal void UpdateLastServerHistoryEntry(DateTime passiveTimestamp, string passiveReason)
		{
			if (this.serverHistoryRootContainerID != null)
			{
				ServerHistoryEntryData serverHistoryEntryData = this.ReadLastSyncServerHistory();
				if (serverHistoryEntryData != null && string.Equals(serverHistoryEntryData.ServerName, Environment.MachineName))
				{
					serverHistoryEntryData.PassiveTimestamp = passiveTimestamp;
					serverHistoryEntryData.PassiveReason = passiveReason;
					this.WriteSyncServerHistory(serverHistoryEntryData, false);
				}
			}
		}

		// Token: 0x060063EE RID: 25582 RVA: 0x0015AFDC File Offset: 0x001591DC
		private void WriteSyncServerHistory(ServerHistoryEntryData serverHistoryData, bool writeNewEntry)
		{
			if (serverHistoryData == null)
			{
				throw new ArgumentNullException("serverHistoryEntryData");
			}
			if (this.serverHistoryEntriesList.Count == 0 && !writeNewEntry)
			{
				throw new Exception("last server history entry is null");
			}
			ServerHistoryEntry serverHistoryEntry = writeNewEntry ? null : this.serverHistoryEntriesList.Last<ServerHistoryEntry>();
			DateTime dateTime = (DateTime)ExDateTime.UtcNow;
			if (writeNewEntry)
			{
				if (this.serverHistoryEntriesList.Count > this.MaxServerHistoryEntries - 1)
				{
					this.CleanupOldServerHistory();
				}
				serverHistoryEntry = new ServerHistoryEntry();
				string unescapedCommonName = string.Format("{0}-{1}", dateTime, Environment.MachineName);
				serverHistoryEntry.SetId(this.serverHistoryRootContainerID.GetChildId(unescapedCommonName));
				serverHistoryEntry.Name = serverHistoryEntry.Id.Name;
				serverHistoryEntry.m_Session = this.serverHistorySession;
				serverHistoryEntry.Version = 1;
				this.serverHistoryEntriesList.Add(serverHistoryEntry);
			}
			serverHistoryEntry.Timestamp = dateTime;
			serverHistoryEntry.Data = serverHistoryData.ToBinary();
			this.serverHistorySession.Save(serverHistoryEntry);
		}

		// Token: 0x060063EF RID: 25583 RVA: 0x0015B0CA File Offset: 0x001592CA
		private ServerHistoryEntryData ReadLastSyncServerHistory()
		{
			if (this.serverHistoryEntriesList.Count > 0)
			{
				return new ServerHistoryEntryData(this.serverHistoryEntriesList.Last<ServerHistoryEntry>());
			}
			return null;
		}

		// Token: 0x060063F0 RID: 25584 RVA: 0x0015B0EC File Offset: 0x001592EC
		private void CleanupOldServerHistory()
		{
			while (this.serverHistoryEntriesList.Count > this.MaxServerHistoryEntries - 1)
			{
				try
				{
					this.serverHistorySession.Delete(this.serverHistoryEntriesList.First<ServerHistoryEntry>());
					this.serverHistoryEntriesList.RemoveAt(0);
				}
				catch (Exception ex)
				{
					Globals.LogEvent(DirectoryEventLogConstants.Tuple_FailedToCleanupCookies, this.ServiceInstanceName, new object[]
					{
						ex
					});
					break;
				}
			}
		}

		// Token: 0x04004276 RID: 17014
		private const int ServerHistoryVersion = 1;

		// Token: 0x04004277 RID: 17015
		public const string ServerHistoryContainerName = "ServerHistory";

		// Token: 0x04004278 RID: 17016
		private readonly ITopologyConfigurationSession serverHistorySession;

		// Token: 0x04004279 RID: 17017
		private readonly List<ServerHistoryEntry> serverHistoryEntriesList;

		// Token: 0x0400427A RID: 17018
		private readonly ADObjectId serverHistoryRootContainerID;
	}
}
