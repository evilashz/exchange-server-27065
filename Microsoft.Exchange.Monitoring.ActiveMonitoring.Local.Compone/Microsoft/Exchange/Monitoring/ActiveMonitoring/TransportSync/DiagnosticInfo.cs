using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TransportSync
{
	// Token: 0x02000114 RID: 276
	public class DiagnosticInfo
	{
		// Token: 0x06000849 RID: 2121 RVA: 0x0003149C File Offset: 0x0002F69C
		protected DiagnosticInfo(string serverName)
		{
			if (string.IsNullOrEmpty(serverName))
			{
				throw new ArgumentException("This class should be instatiated only via GetCachedInstance()");
			}
			this.serverName = serverName;
			this.Refresh();
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x000314C4 File Offset: 0x0002F6C4
		// (set) Token: 0x0600084B RID: 2123 RVA: 0x000314CC File Offset: 0x0002F6CC
		public ExDateTime? LastDatabaseDiscoveryStartTime { get; private set; }

		// Token: 0x0600084C RID: 2124 RVA: 0x000314D8 File Offset: 0x0002F6D8
		public static DiagnosticInfo GetCachedInstance(string serverName)
		{
			if (string.IsNullOrEmpty(serverName))
			{
				serverName = Environment.MachineName;
			}
			lock (DiagnosticInfo.diagInfoCache)
			{
				if (DiagnosticInfo.timeoutTimes.ContainsKey(serverName) && DiagnosticInfo.timeoutTimes[serverName] > ExDateTime.UtcNow)
				{
					return DiagnosticInfo.diagInfoCache[serverName];
				}
			}
			DiagnosticInfo diagnosticInfo = new DiagnosticInfo(serverName);
			lock (DiagnosticInfo.diagInfoCache)
			{
				DiagnosticInfo.timeoutTimes[serverName] = ExDateTime.UtcNow + DiagnosticInfoConstants.DiagnosticInfoCacheTimeout;
				DiagnosticInfo.diagInfoCache[serverName] = diagnosticInfo;
			}
			return diagnosticInfo;
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x000315B0 File Offset: 0x0002F7B0
		public static DiagnosticInfo GetCachedInstance()
		{
			return DiagnosticInfo.GetCachedInstance(null);
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x000315B8 File Offset: 0x0002F7B8
		public MdbHealthInfo GetHealthInfoPerMdb(Guid mdbGuid)
		{
			MdbHealthInfo result = null;
			this.healthInfoPerMdb.TryGetValue(mdbGuid, out result);
			return result;
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x000315D8 File Offset: 0x0002F7D8
		public SlaDiagnosticInfo GetSLAInfoPerMdb(Guid mdbGuid)
		{
			SlaDiagnosticInfo result = null;
			this.slaInfoPerMdb.TryGetValue(mdbGuid, out result);
			return result;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x000315F7 File Offset: 0x0002F7F7
		public void Refresh()
		{
			this.Refresh(DiagnosticMode.Basic);
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00031600 File Offset: 0x0002F800
		public void Refresh(DiagnosticMode mode)
		{
			string componentArgument = null;
			if (mode == DiagnosticMode.Basic)
			{
				componentArgument = string.Format("{0} {1}", "basic", "databasemanager dispatchmanager");
				this.preArgument = DiagnosticMode.Basic;
			}
			else if (mode == DiagnosticMode.Info)
			{
				if (this.preArgument == DiagnosticMode.Info)
				{
					return;
				}
				componentArgument = string.Format("{0} {1}", "info", "databasemanager dispatchmanager");
				this.preArgument = DiagnosticMode.Info;
			}
			string diagnosticInformation = ProcessAccessManager.ClientRunProcessCommand(this.serverName, Configurations.TransportSyncManagerProcessName, "syncmanager", componentArgument, false, true, null);
			this.Parse(diagnosticInformation);
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0003167C File Offset: 0x0002F87C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (MdbHealthInfo mdbHealthInfo in this.healthInfoPerMdb.Values)
			{
				stringBuilder.AppendLine(mdbHealthInfo.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x000316E8 File Offset: 0x0002F8E8
		private void Parse(string diagnosticInformation)
		{
			this.healthInfoPerMdb = new Dictionary<Guid, MdbHealthInfo>(DiagnosticInfoConstants.AverageNumberOfMdbsPerServer);
			this.slaInfoPerMdb = new Dictionary<Guid, SlaDiagnosticInfo>(DiagnosticInfoConstants.AverageNumberOfMdbsPerServer);
			bool flag = false;
			bool flag2 = false;
			try
			{
				using (MemoryStream memoryStream = new MemoryStream(Encoding.Default.GetBytes(diagnosticInformation)))
				{
					using (XmlReader xmlReader = XmlReader.Create(memoryStream, new XmlReaderSettings
					{
						IgnoreComments = true
					}))
					{
						while (xmlReader.Read())
						{
							string name;
							if ((name = xmlReader.Name) != null)
							{
								if (!(name == "GlobalDatabaseHandler"))
								{
									if (name == "DispatchManager")
									{
										this.ParseTransportSyncSLAInfo(xmlReader);
										flag2 = true;
									}
								}
								else
								{
									this.ParseTransportSyncInfo(xmlReader);
									flag = true;
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException(string.Format("Unable to parse diagnostic information string, error {0}: {1}", ex.Message, diagnosticInformation), ex);
			}
			if (!flag || !flag2)
			{
				throw new InvalidOperationException(string.Format("Unable to locate expected elements in the diagnostic information string: {0}", diagnosticInformation));
			}
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00031800 File Offset: 0x0002FA00
		private void ParseTransportSyncInfo(XmlReader xmlReader)
		{
			int depth = xmlReader.Depth;
			do
			{
				if (xmlReader.Name == "Database" && xmlReader.IsStartElement("Database"))
				{
					MdbHealthInfo mdbHealthInfo = new MdbHealthInfo(xmlReader);
					if (mdbHealthInfo.MdbGuid != Guid.Empty)
					{
						this.healthInfoPerMdb[mdbHealthInfo.MdbGuid] = mdbHealthInfo;
					}
				}
				else if (xmlReader.Name == "LastDatabaseDiscoveryStartTime" && xmlReader.IsStartElement("LastDatabaseDiscoveryStartTime"))
				{
					this.LastDatabaseDiscoveryStartTime = new ExDateTime?(ExDateTime.Parse(xmlReader.ReadString()).ToUtc());
				}
				else
				{
					xmlReader.Read();
				}
			}
			while (xmlReader.Depth >= depth && xmlReader.Name != "GlobalDatabaseHandler");
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x000318C4 File Offset: 0x0002FAC4
		private void ParseTransportSyncSLAInfo(XmlReader xmlReader)
		{
			int depth = xmlReader.Depth;
			do
			{
				if (xmlReader.Name == "DatabaseQueueManager" && xmlReader.IsStartElement("DatabaseQueueManager"))
				{
					SlaDiagnosticInfo slaDiagnosticInfo = new SlaDiagnosticInfo(xmlReader);
					if (slaDiagnosticInfo.MdbGuid != Guid.Empty)
					{
						this.slaInfoPerMdb[slaDiagnosticInfo.MdbGuid] = slaDiagnosticInfo;
					}
				}
				else
				{
					xmlReader.Read();
				}
			}
			while (xmlReader.Depth >= depth && xmlReader.Name != "DispatchManager");
		}

		// Token: 0x040005A3 RID: 1443
		private readonly string serverName;

		// Token: 0x040005A4 RID: 1444
		private static Dictionary<string, DiagnosticInfo> diagInfoCache = new Dictionary<string, DiagnosticInfo>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040005A5 RID: 1445
		private static Dictionary<string, ExDateTime> timeoutTimes = new Dictionary<string, ExDateTime>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040005A6 RID: 1446
		private DiagnosticMode preArgument;

		// Token: 0x040005A7 RID: 1447
		private Dictionary<Guid, MdbHealthInfo> healthInfoPerMdb;

		// Token: 0x040005A8 RID: 1448
		private Dictionary<Guid, SlaDiagnosticInfo> slaInfoPerMdb;
	}
}
