using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.TopologyDiscovery;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Directory.TopologyService;
using Microsoft.Exchange.Directory.TopologyService.Configuration;
using Microsoft.Exchange.Directory.TopologyService.EventLog;

namespace Microsoft.Exchange.Directory.TopologyService.Data
{
	// Token: 0x0200002D RID: 45
	[DebuggerDisplay("{ForestFqdn}-{LocalSiteName}. ConfigDC = {ConfigDC.DnsName} Version = {Version} NeedsRediscover = {NeedsRediscover()}")]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ADTopology
	{
		// Token: 0x060001E6 RID: 486 RVA: 0x0000BF50 File Offset: 0x0000A150
		public ADTopology(string forestFqdn, string localSite, List<DirectoryServer> primaryServers, DirectoryServer configDC, DateTime cdcElectionTime, List<DirectoryServer> secondaryServers, List<DirectoryServer> allServers)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("forestFqdn", forestFqdn);
			ArgumentValidator.ThrowIfNullOrEmpty("localSite", localSite);
			ArgumentValidator.ThrowIfNull("primaryServers", primaryServers);
			ArgumentValidator.ThrowIfNull("secondaryServers", secondaryServers);
			ArgumentValidator.ThrowIfNull("allServers", allServers);
			ArgumentValidator.ThrowIfNull("configDC", configDC);
			if (!configDC.IsSuitableForRole(ADServerRole.ConfigurationDomainController))
			{
				throw new ArgumentException("configDC is not suitable for CDC role");
			}
			if (DateTime.UtcNow - (TimeSpan.FromMinutes((double)ConfigurationData.Instance.ConfigDCAffinityInMinutes) + ConfigurationData.Instance.DiscoveryFrequency) > cdcElectionTime)
			{
				throw new ArgumentException(string.Format("Invalid CDC Election Time. Too old. Current time '{0}' Election Time '{1}'. CDC Affinity in minutes {2} DiscoveryFrequency {3}.", new object[]
				{
					DateTime.UtcNow,
					cdcElectionTime,
					ConfigurationData.Instance.ConfigDCAffinityInMinutes,
					ConfigurationData.Instance.DiscoveryFrequency
				}));
			}
			if (cdcElectionTime > DateTime.UtcNow)
			{
				cdcElectionTime = DateTime.UtcNow;
			}
			this.versionMediator = new TopologyDiscoveryInfo.TopologyVersionMediator(null, 0, null);
			this.configDCElectionTime = cdcElectionTime;
			this.primaryServers = primaryServers;
			this.secondaryServers = secondaryServers;
			this.allServers = allServers;
			this.ForestFqdn = forestFqdn;
			this.LocalSiteName = localSite;
			this.configDC = configDC;
			this.cdcStatus = 0;
			this.isGCInPrimaryList = false;
			this.isDCInPrimaryList = false;
			foreach (DirectoryServer directoryServer in this.primaryServers)
			{
				this.isDCInPrimaryList = (this.isDCInPrimaryList || directoryServer.IsSuitableForRole(ADServerRole.DomainController));
				this.isGCInPrimaryList = (this.isGCInPrimaryList || directoryServer.IsSuitableForRole(ADServerRole.GlobalCatalog));
				if (this.isGCInPrimaryList && this.isDCInPrimaryList)
				{
					break;
				}
			}
			ExTraceGlobals.TopologyTracer.TraceDebug<string, bool, bool>((long)this.GetHashCode(), "{0} - IsGCInPrimaryList {1} IsDCInPrimaryList {2}", this.ForestFqdn, this.isGCInPrimaryList, this.isDCInPrimaryList);
			this.forestPerfCounter = ForestDiscoveryPerfCounters.GetInstance(this.ForestFqdn);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000C168 File Offset: 0x0000A368
		public ADTopology(string forestFqdn, string localSite, List<DirectoryServer> primaryServers, DirectoryServer configDC, DateTime cdcElectionTime) : this(forestFqdn, localSite, primaryServers, configDC, cdcElectionTime, ADTopology.EmptyDirectoryServerList, ADTopology.EmptyDirectoryServerList)
		{
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000C181 File Offset: 0x0000A381
		public ADTopology(string forestFqdn, string localSite, List<DirectoryServer> primaryServers, DirectoryServer configDC, DateTime cdcElectionTime, List<DirectoryServer> secondaryServers) : this(forestFqdn, localSite, primaryServers, configDC, cdcElectionTime, secondaryServers, ADTopology.EmptyDirectoryServerList)
		{
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000C197 File Offset: 0x0000A397
		// (set) Token: 0x060001EA RID: 490 RVA: 0x0000C19F File Offset: 0x0000A39F
		public string ForestFqdn { get; private set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000C1A8 File Offset: 0x0000A3A8
		// (set) Token: 0x060001EC RID: 492 RVA: 0x0000C1B0 File Offset: 0x0000A3B0
		public string LocalSiteName { get; private set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000C1B9 File Offset: 0x0000A3B9
		public DirectoryServer ConfigDC
		{
			get
			{
				return this.configDC;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000C1C1 File Offset: 0x0000A3C1
		public DateTime ConfigDCElectionTime
		{
			get
			{
				return this.configDCElectionTime;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000C1C9 File Offset: 0x0000A3C9
		public int Version
		{
			get
			{
				ExTraceGlobals.TopologyTracer.TraceDebug<string, int>((long)this.GetHashCode(), "{0} - Get Topology Version. Version {1}", this.ForestFqdn, this.versionMediator.Version);
				return this.versionMediator.Version;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000C1FD File Offset: 0x0000A3FD
		public IList<DirectoryServer> PrimaryServers
		{
			get
			{
				return new ReadOnlyCollection<DirectoryServer>(this.primaryServers);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000C20A File Offset: 0x0000A40A
		public IList<DirectoryServer> SecondaryServers
		{
			get
			{
				return new ReadOnlyCollection<DirectoryServer>(this.secondaryServers);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000C217 File Offset: 0x0000A417
		public IList<DirectoryServer> AllServers
		{
			get
			{
				return new List<DirectoryServer>(this.allServers);
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000C224 File Offset: 0x0000A424
		public ServerInfo GetServerForRole(ADServerRole role)
		{
			return this.GetServersForRole(null, role, 1, false).FirstOrDefault<ServerInfo>();
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000C240 File Offset: 0x0000A440
		public List<ServerInfo> GetServersForRole(List<string> currentlyUsedServers, ADServerRole role, int serversRequested, bool forestWideAffinityRequested = false)
		{
			ArgumentValidator.ThrowIfZeroOrNegative("serversRequested", serversRequested);
			if (ADServerRole.ConfigurationDomainController != role && ADServerRole.DomainController != role && ADServerRole.GlobalCatalog != role)
			{
				throw new ArgumentException("role");
			}
			List<ServerInfo> list = null;
			ServerInfo item = null;
			if (ADServerRole.ConfigurationDomainController == role)
			{
				if (!this.configDC.IsSuitableForRole(ADServerRole.ConfigurationDomainController))
				{
					this.CheckAndUpdateCDCIfRequired();
				}
				list = new List<ServerInfo>(1);
				if (this.ConfigDC.TryGetServerInfoForRole(ADServerRole.ConfigurationDomainController, out item, false))
				{
					list.Add(item);
				}
			}
			else
			{
				if (currentlyUsedServers == null)
				{
					currentlyUsedServers = new List<string>();
				}
				List<DirectoryServer> list2 = this.FindServersForRole(currentlyUsedServers, role, serversRequested, forestWideAffinityRequested);
				list = new List<ServerInfo>(list2.Count);
				foreach (DirectoryServer directoryServer in list2)
				{
					if (directoryServer.TryGetServerInfoForRole(role, out item, forestWideAffinityRequested))
					{
						list.Add(item);
					}
				}
			}
			if (list.Count == 0)
			{
				ExTraceGlobals.TopologyTracer.TraceError<string, ADServerRole>((long)this.GetHashCode(), "{0} - No servers found for role {1}", this.ForestFqdn, role);
				IEnumerable<DirectoryServer> enumerable = this.primaryServers.Concat(this.secondaryServers);
				if (ADServerRole.DomainController == role)
				{
					List<string> list3 = new List<string>(enumerable.Count<DirectoryServer>());
					foreach (DirectoryServer directoryServer2 in enumerable)
					{
						list3.Add(directoryServer2.DnsName);
					}
					string text = string.Join("\r\n", list3);
					ConfigurationData.LogEvent(MSExchangeADTopologyEventLogConstants.Tuple_DSC_EVENT_ALL_DC_DOWN, this.ForestFqdn, new object[]
					{
						this.ForestFqdn,
						text
					});
				}
				else
				{
					enumerable = from x in enumerable
					where x.IsGC
					select x;
					List<string> list4 = new List<string>(enumerable.Count<DirectoryServer>());
					foreach (DirectoryServer directoryServer3 in enumerable)
					{
						list4.Add(directoryServer3.DnsName);
					}
					string text2 = string.Join("\r\n", list4);
					Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_ALL_GC_DOWN, this.ForestFqdn, new object[]
					{
						this.ForestFqdn,
						text2
					});
				}
			}
			return list;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000C4A4 File Offset: 0x0000A6A4
		public void InitializeTopologyVersion(TopologyDiscoveryInfo.TopologyVersionMediator mediator)
		{
			ArgumentValidator.ThrowIfNull("mediator", mediator);
			ArgumentValidator.ThrowIfNegative("mediator", mediator.Version);
			ExTraceGlobals.TopologyTracer.TraceDebug<string, int>((long)this.GetHashCode(), "{0} - Initializing Topology Version. Version {1}.", this.ForestFqdn, mediator.Version);
			if (this.Version != 0)
			{
				throw new InvalidOperationException("Topology version can only be initialized once. Current version: " + this.Version);
			}
			this.versionMediator = mediator;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000C518 File Offset: 0x0000A718
		public bool TrySetConfigDC(string configDCFqdn)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("configDCFqdn", configDCFqdn);
			ExTraceGlobals.TopologyTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0} - Try SetConfigDC Server FQDN {1}", this.ForestFqdn, configDCFqdn);
			if (this.ConfigDC.DnsName.Equals(configDCFqdn, StringComparison.OrdinalIgnoreCase))
			{
				ExTraceGlobals.TopologyTracer.TraceDebug<string>((long)this.GetHashCode(), "{0} - Try SetConfigDC No change in config DC. Current config DC is the same as the proposed one.", this.ForestFqdn);
				return true;
			}
			foreach (DirectoryServer directoryServer in this.primaryServers.Concat(this.secondaryServers))
			{
				if (directoryServer.DnsName.Equals(configDCFqdn, StringComparison.OrdinalIgnoreCase) && directoryServer.IsSuitableForRole(ADServerRole.ConfigurationDomainController))
				{
					bool flag = false;
					try
					{
						if (Interlocked.CompareExchange(ref this.cdcStatus, 1, 0) == 0)
						{
							flag = true;
							return this.InternalTrySetConfigDC(directoryServer);
						}
					}
					finally
					{
						if (flag)
						{
							Interlocked.Exchange(ref this.cdcStatus, 0);
						}
					}
				}
			}
			ExTraceGlobals.TopologyTracer.TraceDebug<string>((long)this.GetHashCode(), "{0} - Try SetConfigDC  The server proposed as configDC is unknown in the current topology a rediscover will be required.", this.ForestFqdn);
			return false;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000C654 File Offset: 0x0000A854
		public void ReportServerDown(string serverFqdn, ADServerRole role)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("serverFqdn", serverFqdn);
			if (role == ADServerRole.None)
			{
				throw new ArgumentException("role");
			}
			ExTraceGlobals.TopologyTracer.Information<string, string, ADServerRole>((long)this.GetHashCode(), "{0} - Attempting to mark server {1} as down for Role {2}. ", this.ForestFqdn, serverFqdn, role);
			bool flag = false;
			bool flag2 = false;
			bool[] array = new bool[2];
			array[0] = true;
			foreach (bool flag3 in array)
			{
				IList<DirectoryServer> source = flag3 ? this.PrimaryServers : this.SecondaryServers;
				DirectoryServer directoryServer = (from x in source
				where x.DnsName.Equals(serverFqdn, StringComparison.OrdinalIgnoreCase)
				select x).FirstOrDefault<DirectoryServer>();
				if (directoryServer != null)
				{
					flag = directoryServer.IsSuitableForRole(role);
					directoryServer.SetSuitabilityForRole(role, false);
					ADProviderPerf.UpdateDCCounter(directoryServer.DnsName, Counter.DCStateReachability, UpdateType.Update, (uint)directoryServer.SuitabilityResult.IsReachableByTCPConnection);
					if (flag)
					{
						ExPerformanceCounter exPerformanceCounter = (ADServerRole.GlobalCatalog == role) ? (flag3 ? this.forestPerfCounter.GCInSite : this.forestPerfCounter.GCOutOfSite) : (flag3 ? this.forestPerfCounter.DCInSite : this.forestPerfCounter.DCOutOfSite);
						ExTraceGlobals.TopologyTracer.TracePerformance<string, string>((long)this.GetHashCode(), "{0} - Decrementing # counter {1}", this.ForestFqdn, exPerformanceCounter.CounterName);
						exPerformanceCounter.Decrement();
					}
					flag2 = true;
					break;
				}
			}
			if (!flag2)
			{
				return;
			}
			int version = this.Version;
			this.CheckAndUpdateCDCIfRequired();
			if (version == this.Version && flag)
			{
				this.versionMediator.IncrementVersion();
			}
			this.RefreshStatsAndPerfCounters();
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000C830 File Offset: 0x0000AA30
		public void UpdateServersMaintenanceModeState(List<Tuple<string, bool>> serversFqdnWithMM)
		{
			ArgumentValidator.ThrowIfNull("serversFqdnWithMM", serversFqdnWithMM);
			bool flag = false;
			using (List<Tuple<string, bool>>.Enumerator enumerator = serversFqdnWithMM.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Tuple<string, bool> serverWithMM = enumerator.Current;
					ExTraceGlobals.TopologyTracer.Information<string, string, bool>((long)this.GetHashCode(), "{0} - Attempting to update server {1} Maintenance Mode state to {2}.", this.ForestFqdn, serverWithMM.Item1, serverWithMM.Item2);
					bool[] array = new bool[2];
					array[0] = true;
					bool[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						IList<DirectoryServer> source = array2[i] ? this.PrimaryServers : this.SecondaryServers;
						DirectoryServer directoryServer = (from x in source
						where x.DnsName.Equals(serverWithMM.Item1, StringComparison.OrdinalIgnoreCase)
						select x).FirstOrDefault<DirectoryServer>();
						if (directoryServer != null)
						{
							flag = (flag || directoryServer.SuitabilityResult.IsInMM != serverWithMM.Item2);
							directoryServer.SuitabilityResult.IsInMM = serverWithMM.Item2;
							break;
						}
					}
					IList<DirectoryServer> source2 = this.AllServers;
					DirectoryServer directoryServer2 = (from x in source2
					where x.DnsName.Equals(serverWithMM.Item1, StringComparison.OrdinalIgnoreCase)
					select x).FirstOrDefault<DirectoryServer>();
					if (directoryServer2 != null)
					{
						flag = (flag || directoryServer2.SuitabilityResult.IsInMM != serverWithMM.Item2);
						directoryServer2.SuitabilityResult.IsInMM = serverWithMM.Item2;
					}
				}
			}
			if (!flag)
			{
				return;
			}
			int version = this.Version;
			this.CheckAndUpdateCDCIfRequired();
			if (version == this.Version)
			{
				this.versionMediator.IncrementVersion();
			}
			this.RefreshStatsAndPerfCounters();
			string text = string.Join<DirectoryServer>(Environment.NewLine, this.primaryServers);
			ConfigurationData.LogEvent(MSExchangeADTopologyEventLogConstants.Tuple_DSC_EVENT_DCMMON_PRIMARY_SERVERS_STATE, null, new object[]
			{
				this.ForestFqdn,
				text
			});
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000CA4C File Offset: 0x0000AC4C
		public bool HasMinimalRequiredPrimaryServers()
		{
			ExTraceGlobals.TopologyTracer.Information((long)this.GetHashCode(), "{0} - HasMinimalRequiredPrimaryServers. primaryHealthyDCs {1} primaryHealthyGCs {2} MinSuitableServer {3}, total primary DCs {4}", new object[]
			{
				this.ForestFqdn,
				this.primaryHealthyDCs,
				this.primaryHealthyGCs,
				ConfigurationData.Instance.MinSuitableServer,
				this.primaryServers.Count
			});
			if (this.primaryServers.Count <= ConfigurationData.Instance.MinSuitableServer && this.primaryHealthyDCs >= 1 && this.primaryHealthyGCs >= 1)
			{
				return true;
			}
			bool flag = false;
			bool flag2 = false;
			if (string.Equals(this.ForestFqdn, TopologyProvider.LocalForestFqdn, StringComparison.OrdinalIgnoreCase))
			{
				if (this.primaryHealthyDCs >= 1 && ConfigurationData.Instance.DomainControllers.Count > 0 && ConfigurationData.Instance.DomainControllers.Count <= ConfigurationData.Instance.MinSuitableServer)
				{
					flag = true;
				}
				if (this.primaryHealthyGCs >= 1 && ConfigurationData.Instance.GlobalCatalogs.Count > 0 && ConfigurationData.Instance.GlobalCatalogs.Count <= ConfigurationData.Instance.MinSuitableServer)
				{
					flag2 = true;
				}
			}
			return (!this.isDCInPrimaryList || this.primaryHealthyDCs >= ConfigurationData.Instance.MinSuitableServer || flag) && (!this.isGCInPrimaryList || this.primaryHealthyGCs >= ConfigurationData.Instance.MinSuitableServer || flag2);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000CBB4 File Offset: 0x0000ADB4
		public bool HasMinimalRequiredServers()
		{
			int num = this.primaryServers.Count + this.secondaryServers.Count;
			ExTraceGlobals.TopologyTracer.Information((long)this.GetHashCode(), "{0} - HasMinimalRequiredServers. totalHealthyDCs {1} totalHealthyGCs {2} MinSuitableServer {3}, totalDCs {4}", new object[]
			{
				this.ForestFqdn,
				this.totalHealthyDCs,
				this.totalHealthyGCs,
				ConfigurationData.Instance.MinSuitableServer,
				num
			});
			return (num <= ConfigurationData.Instance.MinSuitableServer && this.totalHealthyDCs >= 1 && this.totalHealthyGCs >= 1) || (this.totalHealthyDCs >= ConfigurationData.Instance.MinSuitableServer && this.totalHealthyGCs >= ConfigurationData.Instance.MinSuitableServer);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000CC81 File Offset: 0x0000AE81
		public bool NeedsRediscover()
		{
			return this.AllServers.Count == 0 || !this.HasMinimalRequiredPrimaryServers() || !this.HasMinimalRequiredServers() || !this.IsMinPercentageOfDCsHealthy();
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000CCFC File Offset: 0x0000AEFC
		public List<ServerInfo> FindServersForWritableNC(ADObjectId domain, int serversRequested)
		{
			ArgumentValidator.ThrowIfNull("domain", domain);
			ArgumentValidator.ThrowIfZeroOrNegative("serversRequested", serversRequested);
			ExTraceGlobals.TopologyTracer.TraceDebug<string, ADObjectId>((long)this.GetHashCode(), "{0} FindServersForWritableNC {1}", this.ForestFqdn, domain);
			List<DirectoryServer> list = new List<DirectoryServer>();
			List<DirectoryServer> list2 = new List<DirectoryServer>();
			bool[] array = new bool[2];
			array[0] = true;
			foreach (bool flag in array)
			{
				List<DirectoryServer> source = flag ? this.primaryServers : this.secondaryServers;
				foreach (DirectoryServer directoryServer in from x in source
				where x.IsSuitableForRole(ADServerRole.DomainController) && x.WritableDomainNC.DistinguishedName.Equals(domain.DistinguishedName, StringComparison.OrdinalIgnoreCase)
				select x)
				{
					ExTraceGlobals.TopologyTracer.TraceDebug<string, string, bool>((long)this.GetHashCode(), "{0} - Including server {1} in suitability set for FindServersForWritableNC. (Is In Primary List {2})", this.ForestFqdn, directoryServer.DnsName, flag);
					if (ConfigurationData.Instance.IsPDCCheckEnabled && directoryServer.SuitabilityResult.IsPDC)
					{
						list2.Add(directoryServer);
					}
					else
					{
						list.Add(directoryServer);
					}
				}
				if (flag && list.Count > Math.Min(ConfigurationData.Instance.MinSuitableServer, serversRequested))
				{
					break;
				}
			}
			Random rand = new Random();
			List<DirectoryServer> list3 = new List<DirectoryServer>((from x in list
			orderby rand.Next()
			select x).Take(Math.Min(list.Count, serversRequested)));
			if (list2.Count > 0 && ConfigurationData.Instance.MinUserDC > list3.Count)
			{
				foreach (DirectoryServer item in from x in list2
				orderby rand.Next()
				select x)
				{
					if (list3.Count == serversRequested)
					{
						break;
					}
					list3.Add(item);
				}
			}
			ServerInfo item2 = null;
			List<ServerInfo> list4 = new List<ServerInfo>(list3.Count);
			foreach (DirectoryServer directoryServer2 in list3)
			{
				if (directoryServer2.TryGetServerInfoForRole(ADServerRole.DomainController, out item2, false))
				{
					list4.Add(item2);
				}
			}
			return list4;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000CF84 File Offset: 0x0000B184
		public void RefreshStatsAndPerfCounters()
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			foreach (DirectoryServer directoryServer in this.PrimaryServers)
			{
				directoryServer.RefreshCounters(true);
				if (directoryServer.IsSuitableForRole(ADServerRole.GlobalCatalog))
				{
					num2++;
				}
				if (directoryServer.IsSuitableForRole(ADServerRole.DomainController))
				{
					num++;
				}
			}
			foreach (DirectoryServer directoryServer2 in this.SecondaryServers)
			{
				directoryServer2.RefreshCounters(false);
				if (directoryServer2.IsSuitableForRole(ADServerRole.GlobalCatalog))
				{
					num4++;
				}
				if (directoryServer2.IsSuitableForRole(ADServerRole.DomainController))
				{
					num3++;
				}
			}
			this.primaryHealthyDCs = num;
			this.primaryHealthyGCs = num2;
			this.totalHealthyDCs = num + num3;
			this.totalHealthyGCs = num2 + num4;
			this.forestPerfCounter.DCInSite.RawValue = (long)num;
			this.forestPerfCounter.GCInSite.RawValue = (long)num2;
			this.forestPerfCounter.DCOutOfSite.RawValue = (long)num3;
			this.forestPerfCounter.GCOutOfSite.RawValue = (long)num4;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000D0C4 File Offset: 0x0000B2C4
		private bool IsMinPercentageOfDCsHealthy()
		{
			if (string.Equals(this.ForestFqdn, TopologyProvider.LocalForestFqdn, StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
			int num = this.primaryServers.Count + this.secondaryServers.Count;
			double num2 = (double)ConfigurationData.Instance.MinPercentageOfHealthyDC / 100.0;
			ExTraceGlobals.TopologyTracer.Information((long)this.GetHashCode(), "{0} - Total DC\\GC {1}. Total Healthy DCs '{2}' Total Healthy GCs '{3}'. Threshold {4}", new object[]
			{
				this.ForestFqdn,
				num,
				this.totalHealthyDCs,
				this.totalHealthyGCs,
				num2
			});
			return (double)this.totalHealthyDCs >= num2 * (double)num && (double)this.totalHealthyGCs >= num2 * (double)num;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000D188 File Offset: 0x0000B388
		private void CheckAndUpdateCDCIfRequired()
		{
			bool flag = this.ConfigDC.IsSuitableForRole(ADServerRole.ConfigurationDomainController);
			bool flag2 = this.ConfigDC.Site.Equals(this.LocalSiteName);
			ExTraceGlobals.WCFServiceEndpointTracer.Information((long)this.GetHashCode(), "{0} - CDC is {1}suitable. CDC is local site {2}. Topology has local site DCs {3}", new object[]
			{
				this.ForestFqdn,
				flag ? string.Empty : "NOT ",
				flag2,
				this.isDCInPrimaryList
			});
			if (!flag || (!flag2 && this.isDCInPrimaryList))
			{
				List<DirectoryServer> list = this.FindServersForRole(ADTopology.EmptyStringList, ADServerRole.ConfigurationDomainController, 1, false);
				if (list.Count != 0 && (!flag || list[0].Site.Equals(this.LocalSiteName)))
				{
					bool flag3 = false;
					try
					{
						if (Interlocked.CompareExchange(ref this.cdcStatus, 1, 0) == 0)
						{
							flag3 = true;
							this.InternalTrySetConfigDC(list[0]);
						}
					}
					finally
					{
						if (flag3)
						{
							Interlocked.Exchange(ref this.cdcStatus, 0);
						}
					}
				}
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000D2C4 File Offset: 0x0000B4C4
		private List<DirectoryServer> FindServersForRole(List<string> currentlyUsedServers, ADServerRole role, int serversRequested, bool forestWideAffinityRequested = false)
		{
			ArgumentValidator.ThrowIfZeroOrNegative("serversRequested", serversRequested);
			ArgumentValidator.ThrowIfNull("currentlyUsedServers", currentlyUsedServers);
			if (role == ADServerRole.None)
			{
				throw new ArgumentException("role");
			}
			if (ExTraceGlobals.TopologyTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.TopologyTracer.TraceDebug((long)this.GetHashCode(), "{0} - {1} servers requested for role {2}. Currently used servers |{3}|", new object[]
				{
					this.ForestFqdn,
					serversRequested,
					role.ToString(),
					string.Join(",", currentlyUsedServers)
				});
			}
			if (forestWideAffinityRequested && ConfigurationData.Instance.ForestWideAffinityRequested)
			{
				if (this.allServers.Count == 0)
				{
					ExTraceGlobals.TopologyTracer.TraceDebug<string>((long)this.GetHashCode(), "{0} - It is abnormal that the all servers list does not contain any DC", this.ForestFqdn);
				}
				else
				{
					ExTraceGlobals.TopologyTracer.TraceDebug<int, string>((long)this.GetHashCode(), "FindServersForRole(): allServers: {0}: {1}", this.allServers.Count, string.Join<DirectoryServer>(",", this.allServers));
				}
				return this.allServers;
			}
			List<DirectoryServer> list = new List<DirectoryServer>();
			List<DirectoryServer> list2 = new List<DirectoryServer>();
			List<DirectoryServer> list3 = new List<DirectoryServer>();
			List<DirectoryServer> list4 = new List<DirectoryServer>();
			List<DirectoryServer> list5 = new List<DirectoryServer>();
			string localComputerFqdn = NativeHelpers.GetLocalComputerFqdn(true);
			bool[] array = new bool[2];
			array[0] = true;
			foreach (bool flag in array)
			{
				List<DirectoryServer> source = flag ? this.primaryServers : this.secondaryServers;
				foreach (DirectoryServer directoryServer in from x in source
				where x.IsSuitableForRole(role)
				select x)
				{
					ExTraceGlobals.TopologyTracer.TraceDebug<string, string, bool>((long)this.GetHashCode(), "{0} - Including server {1} in suitability set. (Is In Primary List {2})", this.ForestFqdn, directoryServer.DnsName, flag);
					if (directoryServer.DnsName.Equals(localComputerFqdn, StringComparison.OrdinalIgnoreCase))
					{
						ExTraceGlobals.TopologyTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0} - Found ourself, {1}, in suitable set, using ourself only.", this.ForestFqdn, directoryServer.DnsName);
						list.Clear();
						list.Add(directoryServer);
						return list;
					}
					if (ConfigurationData.Instance.IsPDCCheckEnabled && directoryServer.SuitabilityResult.IsPDC)
					{
						list5.Add(directoryServer);
					}
					else if (currentlyUsedServers.Contains(directoryServer.DnsName, StringComparer.OrdinalIgnoreCase))
					{
						if (flag)
						{
							list.Add(directoryServer);
						}
						else
						{
							list2.Add(directoryServer);
						}
					}
					else if (flag)
					{
						list3.Add(directoryServer);
					}
					else
					{
						list4.Add(directoryServer);
					}
				}
				if (flag)
				{
					if (list.Count + list3.Count >= ((ADServerRole.ConfigurationDomainController == role) ? 1 : Math.Min(ConfigurationData.Instance.MinSuitableServer, serversRequested)))
					{
						break;
					}
					ExTraceGlobals.TopologyTracer.TraceWarning<string>((long)this.GetHashCode(), "{0} - No enough suitable servers found in primary list, switching to secondary", this.ForestFqdn);
					this.LogGoingOutOfSiteIfNecessary(role);
				}
			}
			List<DirectoryServer> list6 = new List<DirectoryServer>();
			Random rand = new Random();
			foreach (List<DirectoryServer> source2 in new List<DirectoryServer>[]
			{
				list,
				list3,
				list2,
				list4
			})
			{
				if (list6.Count < serversRequested)
				{
					foreach (DirectoryServer item in from x in source2
					orderby rand.Next()
					select x)
					{
						if (list6.Count == serversRequested)
						{
							break;
						}
						list6.Add(item);
					}
				}
			}
			if (list5.Count > 0 && ConfigurationData.Instance.MinUserDC > list6.Count)
			{
				foreach (DirectoryServer item2 in from x in list5
				orderby rand.Next()
				select x)
				{
					if (list6.Count == serversRequested)
					{
						break;
					}
					list6.Add(item2);
				}
			}
			if (ExTraceGlobals.TopologyTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.TopologyTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0} - FindServersForRole Returned Servers: | {1} |", this.ForestFqdn, string.Join<DirectoryServer>(",", list6));
			}
			return list6;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000D758 File Offset: 0x0000B958
		private bool InternalTrySetConfigDC(DirectoryServer newCDC)
		{
			ArgumentValidator.ThrowIfNull("newCDC", newCDC);
			ExTraceGlobals.TopologyTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0} - Internal Try SetConfigDC Server FQDN {1}", this.ForestFqdn, newCDC.DnsName);
			if (this.ConfigDC.DnsName.Equals(newCDC.DnsName, StringComparison.OrdinalIgnoreCase))
			{
				ExTraceGlobals.TopologyTracer.TraceDebug<string>((long)this.GetHashCode(), "{0} - Internal Try SetConfigDC No change in config DC. Current config DC is the same as the proposed one.", this.ForestFqdn);
				return true;
			}
			ExTraceGlobals.TopologyTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "{0} - Internal Try SetConfigDC Changing config DC from {1} to {2}", this.ForestFqdn, this.ConfigDC.DnsName, newCDC.DnsName);
			DirectoryServer directoryServer = this.ConfigDC;
			Interlocked.Exchange<DirectoryServer>(ref this.configDC, newCDC);
			this.configDCElectionTime = DateTime.UtcNow;
			ConfigurationData.LogEvent(MSExchangeADTopologyEventLogConstants.Tuple_DSC_EVENT_CDC_CHANGED, null, new object[]
			{
				this.ForestFqdn,
				directoryServer.DnsName,
				this.ConfigDC.DnsName
			});
			this.versionMediator.IncrementVersion();
			return true;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000D878 File Offset: 0x0000BA78
		private void LogGoingOutOfSiteIfNecessary(ADServerRole serverRole)
		{
			if (ADServerRole.DomainController == serverRole && this.isDCInPrimaryList)
			{
				IEnumerable<DirectoryServer> enumerable = from x in this.secondaryServers
				where x.IsSuitableForRole(serverRole)
				select x;
				List<string> list = new List<string>(enumerable.Count<DirectoryServer>());
				foreach (DirectoryServer directoryServer in enumerable)
				{
					list.Add(directoryServer.DnsName);
				}
				string text = string.Join(Environment.NewLine, list);
				ConfigurationData.LogEvent(MSExchangeADTopologyEventLogConstants.Tuple_DSC_EVENT_GOING_OUT_OF_SITE_DC, null, new object[]
				{
					this.ForestFqdn,
					this.LocalSiteName,
					text
				});
			}
			if (ADServerRole.GlobalCatalog == serverRole && this.isGCInPrimaryList)
			{
				IEnumerable<DirectoryServer> enumerable2 = from x in this.secondaryServers
				where x.IsSuitableForRole(serverRole)
				select x;
				List<string> list2 = new List<string>(enumerable2.Count<DirectoryServer>());
				foreach (DirectoryServer directoryServer2 in enumerable2)
				{
					list2.Add(directoryServer2.DnsName);
				}
				string text2 = string.Join(Environment.NewLine, list2);
				ConfigurationData.LogEvent(MSExchangeADTopologyEventLogConstants.Tuple_DSC_EVENT_GOING_OUT_OF_SITE_GC, null, new object[]
				{
					this.ForestFqdn,
					this.LocalSiteName,
					text2
				});
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000DA28 File Offset: 0x0000BC28
		[Conditional("DEBUG")]
		private void DbgCheckServers()
		{
			this.primaryServers.Concat(this.secondaryServers);
			IList<string> domainControllers = ConfigurationData.Instance.DomainControllers;
			IList<string> globalCatalogs = ConfigurationData.Instance.GlobalCatalogs;
			foreach (DirectoryServer directoryServer in this.primaryServers)
			{
				if (!directoryServer.DnsName.Equals(this.ConfigDC.DnsName) && (domainControllers == null || !domainControllers.Contains(directoryServer.DnsName)) && globalCatalogs != null)
				{
					globalCatalogs.Contains(directoryServer.DnsName);
				}
			}
		}

		// Token: 0x040000F8 RID: 248
		public static readonly List<DirectoryServer> EmptyDirectoryServerList = new List<DirectoryServer>(0);

		// Token: 0x040000F9 RID: 249
		private static readonly List<string> EmptyStringList = new List<string>(0);

		// Token: 0x040000FA RID: 250
		private readonly bool isDCInPrimaryList;

		// Token: 0x040000FB RID: 251
		private readonly bool isGCInPrimaryList;

		// Token: 0x040000FC RID: 252
		private List<DirectoryServer> primaryServers;

		// Token: 0x040000FD RID: 253
		private List<DirectoryServer> secondaryServers;

		// Token: 0x040000FE RID: 254
		private List<DirectoryServer> allServers;

		// Token: 0x040000FF RID: 255
		private TopologyDiscoveryInfo.TopologyVersionMediator versionMediator;

		// Token: 0x04000100 RID: 256
		private DirectoryServer configDC;

		// Token: 0x04000101 RID: 257
		private DateTime configDCElectionTime;

		// Token: 0x04000102 RID: 258
		private ForestDiscoveryPerfCountersInstance forestPerfCounter;

		// Token: 0x04000103 RID: 259
		private int cdcStatus;

		// Token: 0x04000104 RID: 260
		private int primaryHealthyDCs;

		// Token: 0x04000105 RID: 261
		private int primaryHealthyGCs;

		// Token: 0x04000106 RID: 262
		private int totalHealthyDCs;

		// Token: 0x04000107 RID: 263
		private int totalHealthyGCs;

		// Token: 0x0200002E RID: 46
		private enum CDCStatus
		{
			// Token: 0x0400010C RID: 268
			None,
			// Token: 0x0400010D RID: 269
			Updating
		}
	}
}
