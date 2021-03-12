using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.DxStore.Common;
using Microsoft.Win32;

namespace Microsoft.Exchange.DxStore.HA
{
	// Token: 0x0200016F RID: 367
	public class DistributedStoreTopologyProvider : DistributedStore.DxStoreRegistryProviderWithVariantConfig
	{
		// Token: 0x06000ED6 RID: 3798 RVA: 0x0003F49C File Offset: 0x0003D69C
		public DistributedStoreTopologyProvider(IDxStoreEventLogger eventLogger, string self = null, bool isZeroboxMode = false)
		{
			this.eventLogger = eventLogger;
			this.versionChecker = new DistributedStoreDagVersionChecker(this, "DxStoreStartCheck", RegistryParameters.DistributedStoreStartupMinimumRequiredVersionAcrossDag);
			base.DefaultInstanceProcessFullPath = Path.Combine(isZeroboxMode ? "C:\\EXCHANGE" : ExchangeSetupContext.InstallPath, "bin\\Microsoft.Exchange.DxStore.HA.Instance.exe");
			base.Initialize("ExchangeHA", self, null, eventLogger, isZeroboxMode);
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0003F508 File Offset: 0x0003D708
		public override string ResolveName(string shortServerName)
		{
			string text;
			if (this.fixedMemberServersMap.TryGetValue(shortServerName, out text) && !string.IsNullOrWhiteSpace(text))
			{
				return text;
			}
			IADServer server = Dependencies.ADConfig.GetServer(shortServerName);
			if (server != null)
			{
				return server.Fqdn;
			}
			return null;
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0003F548 File Offset: 0x0003D748
		public SortedDictionary<string, string> GetFixedDagMembers()
		{
			string keyName = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters";
			string text = (string)Registry.GetValue(keyName, "FixedDagMembersFqdn", string.Empty);
			if (!string.IsNullOrWhiteSpace(text))
			{
				SortedDictionary<string, string> sortedDictionary = new SortedDictionary<string, string>();
				string[] array = text.Split(new char[]
				{
					';'
				});
				foreach (string text2 in array)
				{
					string key = text2;
					string value = string.Empty;
					int num = text2.IndexOf('.');
					if (num != -1)
					{
						key = text2.Substring(0, num);
						value = text2;
					}
					sortedDictionary[key] = value;
				}
				return sortedDictionary;
			}
			return null;
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0003F5F4 File Offset: 0x0003D7F4
		public override TopologyInfo GetLocalServerTopology(bool isForceRefresh = false)
		{
			TopologyInfo topologyInfo = null;
			IActiveManagerSettings settings = DxStoreSetting.Instance.GetSettings();
			bool flag = settings.DxStoreRunMode != DxStoreMode.Disabled;
			if (isForceRefresh)
			{
				Dependencies.ADConfig.Refresh("Distributed store requesting for AD config refresh since members seem to have changed");
			}
			IADServer localServer = Dependencies.ADConfig.GetLocalServer();
			if (localServer != null)
			{
				topologyInfo = new TopologyInfo();
				if (flag && localServer.DatabaseAvailabilityGroup != null)
				{
					IADDatabaseAvailabilityGroup localDag = Dependencies.ADConfig.GetLocalDag();
					if (localDag != null)
					{
						topologyInfo.IsConfigured = true;
						topologyInfo.Name = localDag.Name;
						if (this.fixedMemberServersMap.Count == 0)
						{
							SortedDictionary<string, string> fixedDagMembers = this.GetFixedDagMembers();
							if (fixedDagMembers != null && fixedDagMembers.Count > 0)
							{
								this.fixedMemberServersMap = fixedDagMembers;
								this.eventLogger.Log(DxEventSeverity.Info, 0, "Found fixed dag members {0}", new object[]
								{
									string.Join(";", fixedDagMembers.Values)
								});
							}
						}
						if (this.fixedMemberServersMap.Count > 0)
						{
							topologyInfo.Members = this.fixedMemberServersMap.Keys.ToArray<string>();
						}
						else if (localDag.Servers != null)
						{
							topologyInfo.Members = (from s in localDag.Servers
							select s.Name).ToArray<string>();
						}
						topologyInfo.IsAllMembersVersionCompatible = this.versionChecker.CheckVersionCompatibility();
					}
				}
			}
			return topologyInfo;
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0003F768 File Offset: 0x0003D968
		public Tuple<string, int>[] GetDagServerVersionsSortedBestEffort(out Exception exception)
		{
			Tuple<string, int>[] serverVersions = null;
			exception = Utils.RunBestEffort(delegate
			{
				serverVersions = this.GetDagServerVersionsSorted();
			});
			return serverVersions;
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0003F7AC File Offset: 0x0003D9AC
		public Tuple<string, int>[] GetDagServerVersionsSorted()
		{
			List<Tuple<string, int>> list = new List<Tuple<string, int>>();
			IADDatabaseAvailabilityGroup localDag = Dependencies.ADConfig.GetLocalDag();
			if (localDag != null)
			{
				foreach (ADObjectId adobjectId in localDag.Servers)
				{
					IADServer server = Dependencies.ADConfig.GetServer(adobjectId.Name);
					if (server != null)
					{
						list.Add(new Tuple<string, int>(server.Name, server.AdminDisplayVersion.ToInt()));
					}
				}
			}
			return (from t in list
			orderby t.Item2
			select t).ToArray<Tuple<string, int>>();
		}

		// Token: 0x04000617 RID: 1559
		private DistributedStoreDagVersionChecker versionChecker;

		// Token: 0x04000618 RID: 1560
		private SortedDictionary<string, string> fixedMemberServersMap = new SortedDictionary<string, string>();

		// Token: 0x04000619 RID: 1561
		private IDxStoreEventLogger eventLogger;
	}
}
