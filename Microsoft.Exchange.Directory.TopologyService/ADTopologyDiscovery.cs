using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.TopologyDiscovery;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Directory.TopologyService;
using Microsoft.Exchange.Directory.TopologyService.Common;
using Microsoft.Exchange.Directory.TopologyService.Configuration;
using Microsoft.Exchange.Directory.TopologyService.Data;
using Microsoft.Exchange.Directory.TopologyService.EventLog;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.Directory.TopologyService
{
	// Token: 0x02000007 RID: 7
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ADTopologyDiscovery : WorkItem<ADTopologyDiscoveryResult>
	{
		// Token: 0x06000021 RID: 33 RVA: 0x000024E8 File Offset: 0x000006E8
		protected ADTopologyDiscovery(TopologyDiscoveryInfo topologyDiscoveryInfo, DiscoveryFlags discoveryFlags, string cdcFqdn)
		{
			ArgumentValidator.ThrowIfNull("topologyDiscoveryInfo", topologyDiscoveryInfo);
			this.TopologyDiscoveryInfo = topologyDiscoveryInfo;
			this.PreferredCDCFdqn = cdcFqdn;
			this.DiscoveryFlags = discoveryFlags;
			base.Data = new ADTopologyDiscoveryResult();
			base.ResultType = ResultType.Failed;
			base.Data.DiscoveryFlags = this.DiscoveryFlags;
			base.Data.TopologyDiscoveryInfo = topologyDiscoveryInfo;
			this.id = string.Format("{0}-{1}-{2}-{3}", new object[]
			{
				base.GetType().Name,
				this.ForestFqdn,
				DateTime.UtcNow,
				this.GetHashCode()
			});
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002594 File Offset: 0x00000794
		// (set) Token: 0x06000023 RID: 35 RVA: 0x0000259C File Offset: 0x0000079C
		public DiscoveryFlags DiscoveryFlags { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000025A5 File Offset: 0x000007A5
		public override string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000025AD File Offset: 0x000007AD
		public override TimeSpan TimeoutInterval
		{
			get
			{
				if ((this.DiscoveryFlags & DiscoveryFlags.FullDiscovery) != DiscoveryFlags.None)
				{
					return ConfigurationData.Instance.FullTopologyDiscoveryTimeout;
				}
				return ConfigurationData.Instance.UrgentOrInitialTopologyTimeout;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000025CE File Offset: 0x000007CE
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000025D6 File Offset: 0x000007D6
		private protected string PreferredCDCFdqn { protected get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000025DF File Offset: 0x000007DF
		// (set) Token: 0x06000029 RID: 41 RVA: 0x000025E7 File Offset: 0x000007E7
		private protected TopologyDiscoveryInfo TopologyDiscoveryInfo { protected get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000025F0 File Offset: 0x000007F0
		// (set) Token: 0x0600002B RID: 43 RVA: 0x000025F8 File Offset: 0x000007F8
		private protected CancellationToken CancellationToken { protected get; private set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002601 File Offset: 0x00000801
		protected string ForestFqdn
		{
			get
			{
				return this.TopologyDiscoveryInfo.ForestFqdn;
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000260E File Offset: 0x0000080E
		public static ADTopologyDiscovery CreateTopologyDiscoveryWorkItem(TopologyDiscoveryInfo topologyDiscoveryInfo, DiscoveryFlags flags, string cdcFqdn = null)
		{
			if (topologyDiscoveryInfo.ForestFqdn.Equals(TopologyProvider.LocalForestFqdn, StringComparison.OrdinalIgnoreCase))
			{
				return new LocalForestTopologyDiscovery(topologyDiscoveryInfo, flags, cdcFqdn);
			}
			return new RemoteForestTopologyDiscovery(topologyDiscoveryInfo, flags, cdcFqdn);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002644 File Offset: 0x00000844
		public void Discover()
		{
			ExTraceGlobals.DiscoveryTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Discovering forest {0}. Discovery Flags {1}.", this.ForestFqdn, this.DiscoveryFlags.ToString());
			DateTime utcNow = DateTime.UtcNow;
			List<DirectoryServer> list = this.FindPrimaryDS();
			bool flag = (from x in list
			select x.HasAnySuitableRole()).Count<bool>() != 0;
			if (ExTraceGlobals.DiscoveryTracer.IsTraceEnabled(TraceType.WarningTrace) && !flag)
			{
				ExTraceGlobals.DiscoveryTracer.TraceWarning<string, string>((long)this.GetHashCode(), "{0} - No suitable domain controllers found in site {1}", this.ForestFqdn, this.GetPreferredSiteName());
			}
			bool flag2 = this.CheckForMinimalRequiredServers(list, true);
			ExTraceGlobals.DiscoveryTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0} - Minimal required number of suitable servers {1}available in local site", this.ForestFqdn, flag2 ? string.Empty : "NOT ");
			DirectoryServer directoryServer;
			if (flag2 && (this.DiscoveryFlags & (DiscoveryFlags.InitialDiscovery | DiscoveryFlags.UrgentDiscovery)) != DiscoveryFlags.None)
			{
				directoryServer = this.ChooseConfigDC(list, out utcNow);
				if (this.TopologyDiscoveryInfo.Topology != null && this.TopologyDiscoveryInfo.Topology.AllServers.Count != 0)
				{
					base.Data.Topology = new ADTopology(this.ForestFqdn, this.GetPreferredSiteName(), list, directoryServer, utcNow, ADTopology.EmptyDirectoryServerList, (List<DirectoryServer>)this.TopologyDiscoveryInfo.Topology.AllServers);
				}
				else
				{
					base.Data.Topology = new ADTopology(this.ForestFqdn, this.GetPreferredSiteName(), list, directoryServer, utcNow);
				}
				ExTraceGlobals.DiscoveryTracer.TraceDebug<string>((long)this.GetHashCode(), "{0} - Minitopology creation completed", this.ForestFqdn);
				return;
			}
			List<string> list2 = null;
			List<DirectoryServer> list3 = this.FindSecondaryDS(out list2);
			this.LogNoServersFoundIfNecessary(this.GetPreferredSiteName(), list, list3, list2);
			bool flag3 = (from x in list3
			select x.HasAnySuitableRole()).Count<bool>() != 0;
			if (!flag && !flag3)
			{
				if (ExTraceGlobals.DiscoveryTracer.IsTraceEnabled(TraceType.WarningTrace))
				{
					ExTraceGlobals.DiscoveryTracer.TraceWarning<string, string, string>((long)this.GetHashCode(), "{0} - No suitable secondary servers found in connected sites from {1} sites tested.", this.ForestFqdn, this.GetPreferredSiteName(), (list2 == null) ? "<NONE>" : string.Join(",", list2));
				}
				ExTraceGlobals.DiscoveryTracer.TraceError<string>((long)this.GetHashCode(), "{0} - No suitable servers found.", this.ForestFqdn);
				ExTraceGlobals.DiscoveryTracer.TraceList(this.GetHashCode(), list, "Servers found in local site");
				ExTraceGlobals.DiscoveryTracer.TraceList(this.GetHashCode(), list3, "Servers found in connected sites");
				throw new TopologyDiscoveryException(Strings.NoSuitableDirectoryServersInSiteAndConnectedSites(this.ForestFqdn, this.GetPreferredSiteName()))
				{
					ErrorCode = 3
				};
			}
			bool flag4 = this.CheckForMinimalRequiredServers(list3, false);
			if (!flag2 && !flag4)
			{
				List<DirectoryServer> list4 = new List<DirectoryServer>(list.Count + list3.Count);
				list4.AddRange(list);
				list4.AddRange(list3);
				if (!this.CheckForMinimalRequiredServers(list4, true))
				{
					ExTraceGlobals.DiscoveryTracer.TraceError<string>((long)this.GetHashCode(), "{0} - No minimal required suitable servers found in local and secondary site(s).", this.ForestFqdn);
					ExTraceGlobals.DiscoveryTracer.TraceList(this.GetHashCode(), list4, "Servers found in primary and seconday site(s)");
					throw new TopologyDiscoveryException(Strings.NoRequiredSuitableDirectoryServersInSiteAndConnectedSites(this.ForestFqdn, this.GetPreferredSiteName()))
					{
						ErrorCode = 4
					};
				}
			}
			directoryServer = this.ChooseConfigDC(list, out utcNow);
			if (directoryServer == null)
			{
				directoryServer = this.ChooseConfigDC(list3, out utcNow);
			}
			if (ConfigurationData.Instance.ForestWideAffinityRequested)
			{
				List<DirectoryServer> allServers;
				if (this is RemoteForestTopologyDiscovery && (this.DiscoveryFlags & DiscoveryFlags.FullDiscovery) != DiscoveryFlags.None && ConfigurationData.Instance.EnableWholeForestDiscovery)
				{
					allServers = this.SortByDnsName(list);
				}
				else
				{
					allServers = this.FindAllDS();
				}
				base.Data.Topology = new ADTopology(this.ForestFqdn, this.GetPreferredSiteName(), list, directoryServer, utcNow, list3, allServers);
			}
			else
			{
				base.Data.Topology = new ADTopology(this.ForestFqdn, this.GetPreferredSiteName(), list, directoryServer, utcNow, list3);
			}
			ExTraceGlobals.DiscoveryTracer.TraceInformation<string>(this.GetHashCode(), (long)this.GetHashCode(), "{0}-Topology service did a full discovery.", this.ForestFqdn);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002A5C File Offset: 0x00000C5C
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				base.ResultType = ResultType.Abandoned;
				return;
			}
			ExTraceGlobals.DiscoveryTracer.TracePerformance<string, DiscoveryFlags>((long)this.GetHashCode(), "{0} - Create topology - begin. Discovery Flags {1}", this.ForestFqdn, this.DiscoveryFlags);
			try
			{
				this.Discover();
				if (Globals.IsDatacenter)
				{
					ConfigurationData.LogSecureChannelDCForDomain(this.ForestFqdn);
				}
			}
			catch (ADTransientException ex)
			{
				ExTraceGlobals.DiscoveryTracer.TraceError<string, DiscoveryFlags, string>((long)this.GetHashCode(), "{0} - Error creating topology - begin. Discovery Flags {1}  Error {2}", this.ForestFqdn, this.DiscoveryFlags, ex.ToString());
				if (ex.Data.Contains(TopologyMode.Ldap.ToString()))
				{
					ADErrorRecord aderrorRecord = ex.Data[TopologyMode.Ldap.ToString()] as ADErrorRecord;
					if (aderrorRecord != null && LdapError.ServerDown == aderrorRecord.LdapError)
					{
						ConfigurationData.LogEvent(MSExchangeADTopologyEventLogConstants.Tuple_DSC_EVENT_ALL_DOMAIN_DS_DOWN, this.ForestFqdn, new object[]
						{
							this.ForestFqdn,
							ex.Message
						});
						Task.Factory.StartNew<Task>(() => DnsTroubleshooter.DiagnoseDnsProblemForDomain(this.ForestFqdn), cancellationToken, TaskCreationOptions.AttachedToParent, TaskScheduler.Current).Unwrap();
					}
				}
				throw;
			}
			TopologyDiscoveryInfo.TopologyVersionMediator topologyVersionMediator = this.TopologyDiscoveryInfo.GetTopologyVersionMediator();
			topologyVersionMediator.IncrementVersion();
			base.Data.Topology.InitializeTopologyVersion(topologyVersionMediator);
			this.LogConfigDCChangeIfNecesary(base.Data.Topology);
			this.LogNewTopologyDiscovered(base.Data.Topology);
			base.ResultType = ResultType.Succeeded;
			ExTraceGlobals.DiscoveryTracer.TracePerformance<string, DiscoveryFlags>((long)this.GetHashCode(), "{0} - Create topology - End. Discovery Flags {1}", this.ForestFqdn, this.DiscoveryFlags);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002C04 File Offset: 0x00000E04
		protected virtual void UpdateSuitabilities(ICollection<DirectoryServer> servers)
		{
			List<Task> list = new List<Task>(servers.Count);
			foreach (DirectoryServer directoryServer in servers)
			{
				if (directoryServer.SuitabilityResult.IsEnabled)
				{
					list.Add(SuitabilityVerifier.CheckAndUpdateServerSuitabilities(directoryServer, this.CancellationToken, (this.DiscoveryFlags & (DiscoveryFlags.InitialDiscovery | DiscoveryFlags.UrgentDiscovery)) != DiscoveryFlags.None, ConfigurationData.Instance.AllowPreW2KSP3DC, ConfigurationData.Instance.IsPDCCheckEnabled, ConfigurationData.Instance.DisableNetLogonCheck));
				}
			}
			ExTraceGlobals.DiscoveryTracer.TraceDebug<string, int>((long)this.GetHashCode(), "{0} - Waiting for suitabilities check for {1} server(s).", this.ForestFqdn, list.Count);
			Task.WaitAll(list.ToArray());
			ExTraceGlobals.DiscoveryTracer.TraceDebug<string>((long)this.GetHashCode(), "{0} - Suitabilities checks completed.", this.ForestFqdn);
			list.ForEach(delegate(Task x)
			{
				x.Dispose();
			});
			ExTraceGlobals.DiscoveryTracer.TracePerformance<string, int>(this.GetHashCode(), (long)this.GetHashCode(), "{0} - Exiting UpdateSuitabilities for {1} DC(s).", this.ForestFqdn, servers.Count);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002D30 File Offset: 0x00000F30
		protected virtual bool CheckForMinimalRequiredServersForRole(ICollection<DirectoryServer> servers, ADServerRole role, bool isPrimary)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			foreach (DirectoryServer directoryServer in servers)
			{
				if (directoryServer.IsSuitableForRole(role))
				{
					num++;
				}
				if (num >= ((ADServerRole.ConfigurationDomainController == role) ? 1 : ConfigurationData.Instance.MinSuitableServer))
				{
					return true;
				}
				if (isPrimary && string.Equals(TopologyProvider.LocalForestFqdn, this.ForestFqdn, StringComparison.OrdinalIgnoreCase) && ((ADServerRole.DomainController == role && ConfigurationData.Instance.DomainControllers.Contains(directoryServer.DnsName)) || (ADServerRole.GlobalCatalog == role && ConfigurationData.Instance.GlobalCatalogs.Contains(directoryServer.DnsName))))
				{
					num2++;
				}
				num3++;
			}
			return (num >= 1 && num2 > 0 && num2 <= ConfigurationData.Instance.MinSuitableServer) || (num >= 1 && num3 <= ConfigurationData.Instance.MinSuitableServer);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002E30 File Offset: 0x00001030
		protected virtual bool CheckForMinimalRequiredServers(ICollection<DirectoryServer> servers, bool isPrimary = false)
		{
			bool flag = this.CheckForMinimalRequiredServersForRole(servers, ADServerRole.DomainController, isPrimary);
			bool flag2 = this.CheckForMinimalRequiredServersForRole(servers, ADServerRole.GlobalCatalog, isPrimary);
			bool flag3 = this.CheckForMinimalRequiredServersForRole(servers, ADServerRole.ConfigurationDomainController, isPrimary);
			ExTraceGlobals.DiscoveryTracer.TraceDebug((long)this.GetHashCode(), "{0} - CheckForMinimalRequiredServers returns: has minimal required suitable DC: {1}, has minimal required suitable GC: {2}, has suitable CDC: {3}", new object[]
			{
				this.ForestFqdn,
				flag,
				flag2,
				flag3
			});
			return flag && flag2 && flag3;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002EC8 File Offset: 0x000010C8
		protected virtual DirectoryServer ChooseConfigDC(ICollection<DirectoryServer> servers, out DateTime electionTime)
		{
			DirectoryServer directoryServer = null;
			electionTime = DateTime.UtcNow;
			if (!string.IsNullOrEmpty(this.PreferredCDCFdqn))
			{
				directoryServer = servers.FirstOrDefault((DirectoryServer x) => x.IsSuitableForRole(ADServerRole.ConfigurationDomainController) && x.DnsName.Equals(this.PreferredCDCFdqn, StringComparison.OrdinalIgnoreCase));
				if (directoryServer == null)
				{
					ExTraceGlobals.DiscoveryTracer.TraceWarning<string, string>((long)this.GetHashCode(), "{0} - CDC set using setConfigDC api is not suitable or not found. Ignoring it and using random selection. CDC FQND {1}", this.ForestFqdn, this.PreferredCDCFdqn);
					ConfigurationData.LogEvent(MSExchangeADTopologyEventLogConstants.Tuple_DSC_EVENT_SET_CDC_DOWN, null, new object[]
					{
						this.ForestFqdn,
						this.PreferredCDCFdqn
					});
				}
			}
			if (directoryServer == null)
			{
				directoryServer = this.ChooseRandomConfigDC(servers, out electionTime);
			}
			ExTraceGlobals.DiscoveryTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0} - ChooseConfigDC config DC returned {1}.", this.ForestFqdn, (directoryServer == null) ? "<NULL>" : directoryServer.DnsName);
			return directoryServer;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002FC8 File Offset: 0x000011C8
		protected virtual DirectoryServer ChooseRandomConfigDC(ICollection<DirectoryServer> servers, out DateTime electionTime)
		{
			electionTime = DateTime.UtcNow;
			IEnumerable<DirectoryServer> source = from x in servers
			where x.IsSuitableForRole(ADServerRole.ConfigurationDomainController)
			select x;
			DirectoryServer directoryServer = null;
			if (this.TopologyDiscoveryInfo.Topology != null && this.TopologyDiscoveryInfo.Topology.ConfigDCElectionTime.AddMinutes((double)ConfigurationData.Instance.ConfigDCAffinityInMinutes) > DateTime.UtcNow)
			{
				DirectoryServer previousCDC = this.TopologyDiscoveryInfo.Topology.ConfigDC;
				directoryServer = source.FirstOrDefault((DirectoryServer x) => x.DnsName.Equals(previousCDC.DnsName));
				ExTraceGlobals.DiscoveryTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "{0} - Previous CDC {1} was {2}found in new topology.", this.ForestFqdn, previousCDC.DnsName, (directoryServer == null) ? "NOT " : string.Empty);
				if (directoryServer != null)
				{
					electionTime = this.TopologyDiscoveryInfo.Topology.ConfigDCElectionTime;
				}
			}
			if (directoryServer == null && source.Count<DirectoryServer>() > 0)
			{
				Random random = new Random();
				if (!string.Equals(TopologyProvider.LocalForestFqdn, this.ForestFqdn, StringComparison.OrdinalIgnoreCase))
				{
					IEnumerable<DirectoryServer> enumerable = from x in source
					where string.Equals(x.Site, this.GetPreferredSiteName())
					select x;
					if (enumerable.Count<DirectoryServer>() > 0)
					{
						source = enumerable;
					}
				}
				int index = random.Next(Math.Max(source.Count<DirectoryServer>() - 1, 1));
				directoryServer = source.ElementAt(index);
				ExTraceGlobals.DiscoveryTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "{0} - Randomly chose DC {1} for configDC. Site {2}.", this.ForestFqdn, directoryServer.DnsName, this.GetPreferredSiteName());
			}
			ExTraceGlobals.DiscoveryTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0} - Choose Choose Random Config DC return {1}.", this.ForestFqdn, (directoryServer == null) ? "<NULL>" : directoryServer.DnsName);
			return directoryServer;
		}

		// Token: 0x06000035 RID: 53
		protected abstract List<DirectoryServer> FindPrimaryDS();

		// Token: 0x06000036 RID: 54
		protected abstract List<DirectoryServer> FindSecondaryDS(out List<string> connectedSitesTested);

		// Token: 0x06000037 RID: 55 RVA: 0x00003190 File Offset: 0x00001390
		protected List<DirectoryServer> FindAllDS()
		{
			ExTraceGlobals.DiscoveryTracer.TraceDebug<string>((long)this.GetHashCode(), "{0} - Find all servers from the current server.", this.ForestFqdn);
			List<DirectoryServer> list = this.GetTopologyDiscoverySession().FindDirectoryServers(null);
			this.UpdateSuitabilities(list);
			if (list.Count == 0)
			{
				return list;
			}
			return this.SortByDnsName(list);
		}

		// Token: 0x06000038 RID: 56
		protected abstract TopologyDiscoverySession GetTopologyDiscoverySession();

		// Token: 0x06000039 RID: 57
		protected abstract string GetPreferredSiteName();

		// Token: 0x0600003A RID: 58 RVA: 0x000031F4 File Offset: 0x000013F4
		protected void ResolveAndAddPreferredCDC(List<DirectoryServer> primaryServers)
		{
			if (string.IsNullOrEmpty(this.PreferredCDCFdqn))
			{
				return;
			}
			if (primaryServers.FirstOrDefault((DirectoryServer x) => x.DnsName.Equals(this.PreferredCDCFdqn, StringComparison.OrdinalIgnoreCase)) != null)
			{
				return;
			}
			ExTraceGlobals.DiscoveryTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0} - Resolving CDC set using setConfigDC api. FQDN {1}", this.ForestFqdn, this.PreferredCDCFdqn);
			List<DirectoryServer> source = this.GetTopologyDiscoverySession().FindDirectoryServers(new List<string>(1)
			{
				this.PreferredCDCFdqn
			});
			DirectoryServer directoryServer = source.FirstOrDefault<DirectoryServer>();
			if (directoryServer == null)
			{
				ConfigurationData.LogEvent(MSExchangeADTopologyEventLogConstants.Tuple_DSC_EVENT_SET_CDC_BAD, null, new object[]
				{
					this.ForestFqdn,
					this.PreferredCDCFdqn
				});
				ExTraceGlobals.DiscoveryTracer.TraceWarning<string>((long)this.GetHashCode(), "{0} - CDC from setConfigDC api not found in AD", this.ForestFqdn);
				return;
			}
			primaryServers.Add(directoryServer);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000032C1 File Offset: 0x000014C1
		private List<DirectoryServer> SortByDnsName(List<DirectoryServer> directoryServers)
		{
			if (directoryServers == null || directoryServers.Count == 0)
			{
				return new List<DirectoryServer>(0);
			}
			directoryServers = (from ds in directoryServers
			orderby ds.DnsName
			select ds).ToList<DirectoryServer>();
			return directoryServers;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003300 File Offset: 0x00001500
		private void LogNoServersFoundIfNecessary(string preferredSite, List<DirectoryServer> primaryServers, List<DirectoryServer> secondaryServers = null, List<string> connectedSites = null)
		{
			bool flag = !primaryServers.IsNullOrEmpty() && this.CheckForMinimalRequiredServersForRole(primaryServers, ADServerRole.DomainController, true);
			bool flag2 = !primaryServers.IsNullOrEmpty() && this.CheckForMinimalRequiredServersForRole(primaryServers, ADServerRole.GlobalCatalog, true);
			if (!flag)
			{
				ConfigurationData.LogEvent(MSExchangeADTopologyEventLogConstants.Tuple_DSC_EVENT_NO_LOCAL_DC_FOUND, "ID_EVENT_NO_LOCAL_DC_FOUND" + this.ForestFqdn, new object[]
				{
					this.ForestFqdn,
					preferredSite
				});
			}
			if (!flag2)
			{
				ConfigurationData.LogEvent(MSExchangeADTopologyEventLogConstants.Tuple_DSC_EVENT_NO_LOCAL_GC_FOUND, "ID_EVENT_NO_LOCAL_GC_FOUND" + this.ForestFqdn, new object[]
				{
					this.ForestFqdn,
					preferredSite
				});
			}
			if (!flag && (secondaryServers.IsNullOrEmpty() || !this.CheckForMinimalRequiredServersForRole(secondaryServers, ADServerRole.DomainController, false)))
			{
				string text = (connectedSites != null) ? string.Join(Environment.NewLine, connectedSites) : string.Empty;
				ConfigurationData.LogEvent(MSExchangeADTopologyEventLogConstants.Tuple_DSC_EVENT_NO_DC_FOUND, null, new object[]
				{
					this.ForestFqdn,
					preferredSite,
					text
				});
			}
			if (!flag2 && (secondaryServers.IsNullOrEmpty() || !this.CheckForMinimalRequiredServersForRole(secondaryServers, ADServerRole.GlobalCatalog, false)))
			{
				string text2 = (connectedSites != null) ? string.Join(Environment.NewLine, connectedSites) : string.Empty;
				ConfigurationData.LogEvent(MSExchangeADTopologyEventLogConstants.Tuple_DSC_EVENT_NO_GC_FOUND, null, new object[]
				{
					this.ForestFqdn,
					preferredSite,
					text2
				});
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003460 File Offset: 0x00001660
		private void LogConfigDCChangeIfNecesary(ADTopology newAdTopology)
		{
			if (this.TopologyDiscoveryInfo.Topology != null && !this.TopologyDiscoveryInfo.Topology.ConfigDC.DnsName.Equals(newAdTopology.ConfigDC.DnsName))
			{
				ConfigurationData.LogEvent(MSExchangeADTopologyEventLogConstants.Tuple_DSC_EVENT_CDC_CHANGED, null, new object[]
				{
					newAdTopology.ForestFqdn,
					this.TopologyDiscoveryInfo.Topology.ConfigDC.DnsName,
					newAdTopology.ConfigDC.DnsName
				});
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000034E4 File Offset: 0x000016E4
		private void LogNewTopologyDiscovered(ADTopology topology)
		{
			ArgumentValidator.ThrowIfNull("topology", topology);
			if ((this.DiscoveryFlags & DiscoveryFlags.InitialDiscovery) != DiscoveryFlags.None)
			{
				StringBuilder stringBuilder = new StringBuilder();
				StringBuilder stringBuilder2 = new StringBuilder();
				foreach (DirectoryServer directoryServer in topology.PrimaryServers)
				{
					stringBuilder.AppendLine(directoryServer.DnsName);
					if (directoryServer.IsGC)
					{
						stringBuilder2.AppendLine(directoryServer.DnsName);
					}
				}
				ConfigurationData.LogEvent(MSExchangeADTopologyEventLogConstants.Tuple_DSC_EVENT_SUITABLE_SERVERS, null, new object[]
				{
					topology.ForestFqdn,
					stringBuilder.ToString(),
					stringBuilder2.ToString(),
					topology.ConfigDC.DnsName
				});
			}
			string text = string.Join<DirectoryServer>(Environment.NewLine, topology.PrimaryServers);
			string text2 = string.Join<DirectoryServer>(Environment.NewLine, topology.SecondaryServers);
			if (Globals.IsDatacenter)
			{
				ConfigurationData.LogEvent(MSExchangeADTopologyEventLogConstants.Tuple_DSC_EVENT_DISCOVERED_SERVERS_DATACENTER, null, new object[]
				{
					topology.ForestFqdn,
					text,
					text2
				});
				return;
			}
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_DISCOVERED_SERVERS, null, new object[]
			{
				text,
				text2
			});
		}

		// Token: 0x04000015 RID: 21
		private readonly string id;
	}
}
