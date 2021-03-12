using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.TopologyDiscovery;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Directory.TopologyService;
using Microsoft.Exchange.Directory.TopologyService.Common;
using Microsoft.Exchange.Directory.TopologyService.Configuration;
using Microsoft.Exchange.Directory.TopologyService.Data;
using Microsoft.Exchange.Directory.TopologyService.EventLog;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Directory.TopologyService
{
	// Token: 0x0200000C RID: 12
	internal class ForestMMMonitor
	{
		// Token: 0x06000062 RID: 98 RVA: 0x00003ABB File Offset: 0x00001CBB
		internal ForestMMMonitor(TopologyDiscoveryWorkProvider workQueue, TopologyCache topologyCache)
		{
			ArgumentValidator.ThrowIfNull("workQueue", workQueue);
			ArgumentValidator.ThrowIfNull("topologyCache", topologyCache);
			this.workQueue = workQueue;
			this.cache = topologyCache;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003AE7 File Offset: 0x00001CE7
		internal ForestMMMonitor(TopologyDiscoveryWorkProvider workQueue) : this(workQueue, TopologyCache.Default)
		{
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003AF8 File Offset: 0x00001CF8
		public void Start()
		{
			this.shouldScheduleWork = true;
			this.workQueue.ScheduleWork(new ForestMMMonitor.ForestMMMonitorWorkItem(this.cache), ExEnvironment.IsTest ? DateTime.UtcNow.Add(TimeSpan.FromMinutes(3.0)) : DateTime.UtcNow.Add(TimeSpan.FromMinutes(5.0)), new Action<IWorkItemResult>(this.WorkItemCompletedCallback));
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003B6D File Offset: 0x00001D6D
		public void Stop()
		{
			this.shouldScheduleWork = false;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003B78 File Offset: 0x00001D78
		protected void WorkItemCompletedCallback(IWorkItemResult result)
		{
			ArgumentValidator.ThrowIfNull("result", result);
			if (this.shouldScheduleWork)
			{
				ExTraceGlobals.TopologyTracer.TraceInformation<DateTime>(this.GetHashCode(), (long)this.GetHashCode(), "Account Forest Scan - Perform MM scan on {0}", DateTime.UtcNow.Add(ConfigurationData.Instance.MaintenanceModeDiscoveryFrequency));
				this.workQueue.ScheduleWork(new ForestMMMonitor.ForestMMMonitorWorkItem(this.cache), DateTime.UtcNow.Add(ConfigurationData.Instance.MaintenanceModeDiscoveryFrequency), new Action<IWorkItemResult>(this.WorkItemCompletedCallback));
			}
		}

		// Token: 0x0400002C RID: 44
		private bool shouldScheduleWork;

		// Token: 0x0400002D RID: 45
		private TopologyDiscoveryWorkProvider workQueue;

		// Token: 0x0400002E RID: 46
		private TopologyCache cache;

		// Token: 0x0200000D RID: 13
		internal class ForestMMMonitorWorkItem : WorkItem<object>
		{
			// Token: 0x06000067 RID: 103 RVA: 0x00003C04 File Offset: 0x00001E04
			public ForestMMMonitorWorkItem(TopologyCache cache)
			{
				ArgumentValidator.ThrowIfNull("cache", cache);
				this.id = string.Format("{0}-{1}-{2}", base.GetType().Name, DateTime.UtcNow, this.GetHashCode());
				this.cache = cache;
				base.Data = new object();
			}

			// Token: 0x17000021 RID: 33
			// (get) Token: 0x06000068 RID: 104 RVA: 0x00003C64 File Offset: 0x00001E64
			public override string Id
			{
				get
				{
					return this.id;
				}
			}

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x06000069 RID: 105 RVA: 0x00003C6C File Offset: 0x00001E6C
			public override TimeSpan TimeoutInterval
			{
				get
				{
					return ConfigurationData.Instance.MaintenanceModeDiscoveryTimeout;
				}
			}

			// Token: 0x0600006A RID: 106 RVA: 0x00003C78 File Offset: 0x00001E78
			protected override void DoWork(CancellationToken cancellationToken)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					base.ResultType = ResultType.Abandoned;
					return;
				}
				ExTraceGlobals.DiscoveryTracer.TraceDebug((long)this.GetHashCode(), "Starting MM Discovery for forests");
				foreach (TopologyDiscoveryInfo topologyDiscoveryInfo in this.cache.Values)
				{
					if (cancellationToken.IsCancellationRequested)
					{
						base.ResultType = ResultType.TimedOut;
						return;
					}
					if (topologyDiscoveryInfo.Topology == null || (topologyDiscoveryInfo.State == DiscoveryState.InProgress && !ExEnvironment.IsTest))
					{
						ExTraceGlobals.DiscoveryTracer.TraceInformation<string>(this.GetHashCode(), (long)this.GetHashCode(), "{0} - Skipping MM state check", topologyDiscoveryInfo.ForestFqdn);
					}
					else
					{
						ExTraceGlobals.DiscoveryTracer.TraceInformation<string>(this.GetHashCode(), (long)this.GetHashCode(), "{0} - Reading MM state for DCs", topologyDiscoveryInfo.ForestFqdn);
						List<Tuple<string, bool>> dcmmstate = this.GetDCMMState(topologyDiscoveryInfo.ForestFqdn);
						if (dcmmstate.Count == 0)
						{
							ExTraceGlobals.DiscoveryTracer.TraceWarning<TopologyDiscoveryInfo>((long)this.GetHashCode(), "{0} - Forest Check Skipped", topologyDiscoveryInfo);
						}
						else
						{
							ADTopology topology = topologyDiscoveryInfo.Topology;
							if (topology == null)
							{
								ExTraceGlobals.DiscoveryTracer.TraceError<string>((long)this.GetHashCode(), "{0} - Forest topology went from NOT NULL to NULL", topologyDiscoveryInfo.ForestFqdn);
							}
							else
							{
								topology.UpdateServersMaintenanceModeState(dcmmstate);
								LdapTopologyProvider ldapTopologyProvider = TopologyProvider.GetInstance() as LdapTopologyProvider;
								if (ldapTopologyProvider != null)
								{
									foreach (Tuple<string, bool> tuple in dcmmstate)
									{
										if (tuple.Item2)
										{
											ldapTopologyProvider.ReportServerDown(topologyDiscoveryInfo.ForestFqdn, tuple.Item1, ADServerRole.DomainController);
											ldapTopologyProvider.ReportServerDown(topologyDiscoveryInfo.ForestFqdn, tuple.Item1, ADServerRole.GlobalCatalog);
										}
									}
								}
								this.ResetSecureChannelDSIfNecessary(topologyDiscoveryInfo.ForestFqdn, dcmmstate);
								this.ResetDCLocatorDSIfNecessary(topologyDiscoveryInfo.ForestFqdn, dcmmstate);
							}
						}
					}
				}
				base.ResultType = ResultType.Succeeded;
			}

			// Token: 0x0600006B RID: 107 RVA: 0x00003E80 File Offset: 0x00002080
			private void ResetSecureChannelDSIfNecessary(string forestFqdn, List<Tuple<string, bool>> dcsWithMMState)
			{
				ArgumentValidator.ThrowIfNullOrEmpty("forestFqdn", forestFqdn);
				ArgumentValidator.ThrowIfNull("dcsWithMMState", dcsWithMMState);
				TopologyDiscoveryInfo topologyDiscoveryInfo = null;
				ADTopology topology = null;
				if (this.cache.TryGetValue(forestFqdn, out topologyDiscoveryInfo))
				{
					topology = topologyDiscoveryInfo.Topology;
				}
				List<string> list = new List<string>();
				List<string> list2 = new List<string>();
				List<string> list3 = new List<string>();
				foreach (Tuple<string, bool> tuple in dcsWithMMState)
				{
					if (!tuple.Item2)
					{
						if (this.IsDcPrimary(tuple.Item1, topology))
						{
							list.Add(tuple.Item1);
						}
						else if (this.IsDcKnown(tuple.Item1, topology))
						{
							list2.Add(tuple.Item1);
						}
						else
						{
							list3.Add(tuple.Item1);
						}
					}
				}
				int num = 0;
				Random random = new Random();
				bool flag;
				do
				{
					flag = false;
					string secureChannelDCForDomain = NativeHelpers.GetSecureChannelDCForDomain(forestFqdn, false);
					if (!string.IsNullOrEmpty(secureChannelDCForDomain))
					{
						ExTraceGlobals.DiscoveryTracer.TraceInformation<int, string, string>(this.GetHashCode(), (long)this.GetHashCode(), "Attempt #{0} - Current secure channel DC for forest {1} is {2}", num, forestFqdn, secureChannelDCForDomain);
					}
					else
					{
						ExTraceGlobals.DiscoveryTracer.TraceWarning<int, string>((long)this.GetHashCode(), "Attempt #{0} - Unable to get Secure Channel DC for forest {1}", num, forestFqdn);
					}
					if (!string.IsNullOrEmpty(secureChannelDCForDomain) && this.IsDcInMM(secureChannelDCForDomain, dcsWithMMState))
					{
						string text = null;
						if (list.Count > 0)
						{
							text = list[random.Next(list.Count)];
						}
						else if (list2.Count > 0)
						{
							text = list2[random.Next(list2.Count)];
						}
						else if (list3.Count > 0)
						{
							text = list3[random.Next(list3.Count)];
						}
						string text3;
						if (text != null)
						{
							string text2 = forestFqdn + "\\" + text;
							ExTraceGlobals.DiscoveryTracer.TraceInformation<int, string, string>(this.GetHashCode(), (long)this.GetHashCode(), "Attempt #{0} - Trying to reset secure channel DC for forest {1} to {2}", num, forestFqdn, text2);
							text3 = NativeHelpers.ResetSecureChannelDCForDomain(text2, false);
						}
						else
						{
							ExTraceGlobals.DiscoveryTracer.TraceInformation<int, string>(this.GetHashCode(), (long)this.GetHashCode(), "Attempt #{0} - Trying to reset secure channel DC for forest {1} by auto rediscovery", num, forestFqdn);
							text3 = NativeHelpers.ResetSecureChannelDCForDomain(forestFqdn, false);
						}
						ConfigurationData.LogEvent(MSExchangeADTopologyEventLogConstants.Tuple_DSC_EVENT_DCMMMON_RESET_SC_CHANNEL, null, new object[]
						{
							forestFqdn,
							secureChannelDCForDomain ?? string.Empty,
							text3 ?? string.Empty
						});
						if (!string.IsNullOrEmpty(text3))
						{
							if (!this.IsDcKnown(text3, topology))
							{
								ConfigurationData.LogEvent(MSExchangeADTopologyEventLogConstants.Tuple_SecureChannelDCIsUnknown, forestFqdn, new object[]
								{
									forestFqdn,
									text3
								});
							}
							if (this.IsDcInMM(text3, dcsWithMMState))
							{
								num++;
								if (num < ConfigurationData.Instance.MaxAttemptsResetDCCache)
								{
									flag = true;
								}
								ExTraceGlobals.DiscoveryTracer.TraceInformation(this.GetHashCode(), (long)this.GetHashCode(), "Attempt #{0} - Secure channel was reset to an MM DC {1} for forest {2}.{3}", new object[]
								{
									num,
									text3,
									forestFqdn,
									flag ? " Will retry" : string.Empty
								});
								if (flag)
								{
									ConfigurationData.LogEvent(MSExchangeADTopologyEventLogConstants.Tuple_DSC_EVENT_DCMMMON_RESET_SC_CHANNEL_TO_MM_DC, null, new object[]
									{
										forestFqdn,
										text3,
										num
									});
								}
								else
								{
									ConfigurationData.LogEvent(MSExchangeADTopologyEventLogConstants.Tuple_DSC_EVENT_DCMMMON_RESET_SC_CHANNEL_TO_MM_DC_FINALLY, null, new object[]
									{
										forestFqdn,
										text3,
										num
									});
								}
							}
						}
					}
				}
				while (flag);
			}

			// Token: 0x0600006C RID: 108 RVA: 0x000041F4 File Offset: 0x000023F4
			private void ResetDCLocatorDSIfNecessary(string forestFqdn, List<Tuple<string, bool>> dcsWithMMState)
			{
				ArgumentValidator.ThrowIfNullOrEmpty("forestFqdn", forestFqdn);
				ArgumentValidator.ThrowIfNull("dcsWithMMState", dcsWithMMState);
				TopologyDiscoveryInfo topologyDiscoveryInfo = null;
				if (this.cache.TryGetValue(forestFqdn, out topologyDiscoveryInfo))
				{
					ADTopology topology = topologyDiscoveryInfo.Topology;
				}
				int num = 0;
				bool flag;
				do
				{
					flag = false;
					NativeMethods.DsGetDCNameFlags dsGetDCNameFlags = NativeMethods.DsGetDCNameFlags.DirectoryServiceRequired;
					SafeDomainControllerInfoHandle safeDomainControllerInfoHandle;
					uint num2 = NativeMethods.DsGetDcName(null, forestFqdn, null, dsGetDCNameFlags, out safeDomainControllerInfoHandle);
					string text;
					if (safeDomainControllerInfoHandle != null && num2 == 0U)
					{
						text = (safeDomainControllerInfoHandle.GetDomainControllerInfo().DomainControllerName ?? string.Empty);
						text = text.Replace("\\", string.Empty);
						ExTraceGlobals.DiscoveryTracer.TraceInformation<int, string, string>(this.GetHashCode(), (long)this.GetHashCode(), "Attempt #{0} - Current DC Locator cache DC for forest {1} is {2}", num, forestFqdn, string.IsNullOrEmpty(text) ? "Unset" : text);
					}
					else
					{
						ExTraceGlobals.DiscoveryTracer.TraceWarning<int, string>((long)this.GetHashCode(), "Attempt #{0} - Unable to get DC Locator cache DC for forest {1}", num, forestFqdn);
						text = null;
					}
					if (!string.IsNullOrEmpty(text) && this.IsDcInMM(text, dcsWithMMState))
					{
						dsGetDCNameFlags |= NativeMethods.DsGetDCNameFlags.ForceRediscovery;
						uint num3 = NativeMethods.DsGetDcName(null, forestFqdn, null, dsGetDCNameFlags, out safeDomainControllerInfoHandle);
						string text2;
						if (safeDomainControllerInfoHandle != null && num3 == 0U)
						{
							text2 = (safeDomainControllerInfoHandle.GetDomainControllerInfo().DomainControllerName ?? string.Empty);
							text2 = text2.Replace("\\", string.Empty);
						}
						else
						{
							ExTraceGlobals.DiscoveryTracer.TraceWarning<int, string>((long)this.GetHashCode(), "Attempt #{0} - DC Locator unable to rediscover forest {1}", num, forestFqdn);
							text2 = null;
						}
						if (!string.IsNullOrEmpty(text2) && this.IsDcInMM(text2, dcsWithMMState))
						{
							num++;
							if (num < ConfigurationData.Instance.MaxAttemptsResetDCCache)
							{
								flag = true;
							}
							ExTraceGlobals.DiscoveryTracer.TraceInformation(this.GetHashCode(), (long)this.GetHashCode(), "Attempt #{0} - DC Locator cache DC was reset to an MM DC {1} for forest {2}.{3}", new object[]
							{
								num,
								text2,
								forestFqdn,
								flag ? " Will retry" : string.Empty
							});
							if (flag)
							{
								ConfigurationData.LogEvent(MSExchangeADTopologyEventLogConstants.Tuple_DSC_EVENT_DCMMMON_RESET_DC_LOCATOR_TO_MM_DC, null, new object[]
								{
									forestFqdn,
									text2,
									num
								});
							}
							else
							{
								ConfigurationData.LogEvent(MSExchangeADTopologyEventLogConstants.Tuple_DSC_EVENT_DCMMMON_RESET_DC_LOCATOR_TO_MM_DC_FINALLY, null, new object[]
								{
									forestFqdn,
									text2,
									num
								});
							}
						}
					}
				}
				while (flag);
			}

			// Token: 0x0600006D RID: 109 RVA: 0x0000442C File Offset: 0x0000262C
			private List<Tuple<string, bool>> GetDCMMState(string forestFqdn)
			{
				ArgumentValidator.ThrowIfNullOrEmpty("forestFqdn", forestFqdn);
				Dictionary<string, Tuple<string, bool>> dictionary = new Dictionary<string, Tuple<string, bool>>();
				int num = 3;
				string text = string.Empty;
				try
				{
					ADSessionSettings adsessionSettings = ADSessionSettings.SessionSettingsFactory.Default.FromAccountPartitionRootOrgScopeSet(new PartitionId(forestFqdn));
					adsessionSettings.IncludeCNFObject = false;
					TopologyDiscoverySession topologyDiscoverySession = new TopologyDiscoverySession(true, adsessionSettings);
					topologyDiscoverySession.UseConfigNC = false;
					ADObjectId childId = topologyDiscoverySession.GetDomainNamingContext().GetChildId("OU", "Domain Controllers");
					List<string> list = new List<string>();
					foreach (ADComputer adcomputer in topologyDiscoverySession.Find<ADComputer>(childId, QueryScope.OneLevel, null, null, 0))
					{
						dictionary.Add(adcomputer.DnsHostName, new Tuple<string, bool>(adcomputer.DnsHostName, adcomputer.IsOutOfService));
						if (!adcomputer.IsOutOfService)
						{
							list.Add(adcomputer.DnsHostName);
						}
					}
					if (list.Count > 1)
					{
						HashSet<string> hashSet = new HashSet<string>();
						hashSet.Add(topologyDiscoverySession.LastUsedDc.Substring(0, 3));
						Random rand = new Random();
						List<string> list2 = (from x in list
						orderby rand.Next()
						select x).ToList<string>();
						bool flag = false;
						do
						{
							text = topologyDiscoverySession.LastUsedDc;
							string text2 = null;
							foreach (string text3 in list2)
							{
								string item = text3.Substring(0, 3);
								if (!hashSet.Contains(item))
								{
									text2 = text3;
									hashSet.Add(item);
									break;
								}
							}
							if (text2 == null)
							{
								break;
							}
							ADObjectId childId2 = childId.GetChildId("CN", text.Split(new char[]
							{
								'.'
							})[0]);
							try
							{
								topologyDiscoverySession.DomainController = text2;
								ADComputer adcomputer2 = topologyDiscoverySession.Read<ADComputer>(childId2);
								if (adcomputer2 != null)
								{
									if (!adcomputer2.IsOutOfService)
									{
										ExTraceGlobals.DiscoveryTracer.TraceInformation<string, string>(this.GetHashCode(), (long)this.GetHashCode(), "The DC {0} used for discovering forest {1} is in good state and thus trustable.", adcomputer2.DnsHostName, forestFqdn);
										flag = true;
										break;
									}
									dictionary = new Dictionary<string, Tuple<string, bool>>();
									foreach (ADComputer adcomputer3 in topologyDiscoverySession.Find<ADComputer>(childId, QueryScope.OneLevel, null, null, 0))
									{
										dictionary.Add(adcomputer3.DnsHostName, new Tuple<string, bool>(adcomputer3.DnsHostName, adcomputer3.IsOutOfService));
									}
								}
							}
							catch (TransientException arg)
							{
								ExTraceGlobals.DiscoveryTracer.TraceError<string, Exception>((long)this.GetHashCode(), "{0} - Error {1}", forestFqdn, arg);
							}
						}
						while (--num > 0);
						if (!flag)
						{
							ExTraceGlobals.DiscoveryTracer.TraceWarning<string>(this.GetHashCode(), (long)this.GetHashCode(), "No good DCs have been found for discovering forest {0}. The MM state information returned may not be accurate.", forestFqdn);
						}
					}
					if (ExTraceGlobals.DiscoveryTracer.IsTraceEnabled(TraceType.InfoTrace))
					{
						ExTraceGlobals.DiscoveryTracer.TraceInformation<string, string>(this.GetHashCode(), (long)this.GetHashCode(), "{0} - DCs MM state | {1} |", forestFqdn, string.Join<Tuple<string, bool>>(",", dictionary.Values.ToList<Tuple<string, bool>>()));
					}
				}
				catch (TransientException arg2)
				{
					ExTraceGlobals.DiscoveryTracer.TraceError<string, Exception>((long)this.GetHashCode(), "{0} - Error {1}", forestFqdn, arg2);
				}
				return dictionary.Values.ToList<Tuple<string, bool>>();
			}

			// Token: 0x0600006E RID: 110 RVA: 0x0000477C File Offset: 0x0000297C
			private bool IsDcInMM(string dcFqdn, List<Tuple<string, bool>> dcsWithMMState)
			{
				foreach (Tuple<string, bool> tuple in dcsWithMMState)
				{
					if (tuple.Item1.Equals(dcFqdn, StringComparison.OrdinalIgnoreCase))
					{
						return tuple.Item2;
					}
				}
				return false;
			}

			// Token: 0x0600006F RID: 111 RVA: 0x000047E0 File Offset: 0x000029E0
			private bool IsDcPrimary(string dcFqdn, ADTopology topology)
			{
				if (!string.IsNullOrEmpty(dcFqdn) && topology != null)
				{
					foreach (DirectoryServer directoryServer in topology.PrimaryServers)
					{
						if (directoryServer.DnsName.Equals(dcFqdn, StringComparison.OrdinalIgnoreCase))
						{
							return true;
						}
					}
					return false;
				}
				return false;
			}

			// Token: 0x06000070 RID: 112 RVA: 0x00004848 File Offset: 0x00002A48
			private bool IsDcKnown(string dcFqdn, ADTopology topology)
			{
				if (!string.IsNullOrEmpty(dcFqdn) && topology != null)
				{
					foreach (DirectoryServer directoryServer in topology.PrimaryServers.Concat(topology.SecondaryServers))
					{
						if (directoryServer.DnsName.Equals(dcFqdn, StringComparison.OrdinalIgnoreCase))
						{
							return true;
						}
					}
					return false;
				}
				return false;
			}

			// Token: 0x0400002F RID: 47
			private readonly string id;

			// Token: 0x04000030 RID: 48
			private TopologyCache cache;
		}
	}
}
