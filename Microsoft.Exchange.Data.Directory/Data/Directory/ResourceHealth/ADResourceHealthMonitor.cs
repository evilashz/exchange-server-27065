using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory.Diagnostics;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Data.Directory.ResourceHealth
{
	// Token: 0x020009F0 RID: 2544
	internal class ADResourceHealthMonitor : CacheableResourceHealthMonitor, IResourceHealthPoller
	{
		// Token: 0x06007609 RID: 30217 RVA: 0x0018462B File Offset: 0x0018282B
		internal ADResourceHealthMonitor(ADResourceKey key) : base(key)
		{
		}

		// Token: 0x17002A4A RID: 10826
		// (get) Token: 0x0600760A RID: 30218 RVA: 0x00184654 File Offset: 0x00182854
		public TimeSpan Interval
		{
			get
			{
				if (this.IsActive)
				{
					return ADHealthMonitorConfiguration.RefreshInterval;
				}
				return TimeSpan.MaxValue;
			}
		}

		// Token: 0x17002A4B RID: 10827
		// (get) Token: 0x0600760B RID: 30219 RVA: 0x00184669 File Offset: 0x00182869
		public bool IsActive
		{
			get
			{
				return ADHealthMonitorConfiguration.Enabled;
			}
		}

		// Token: 0x0600760C RID: 30220 RVA: 0x00184670 File Offset: 0x00182870
		public void Execute()
		{
			foreach (string text in this.GetForests())
			{
				ADMetrics previousMetrics;
				this.metricsDictionary.TryGetValue(text, out previousMetrics);
				ADMetrics metricsForForest = ADMetrics.GetMetricsForForest(previousMetrics, text);
				this.SetMetrics(metricsForForest, text);
			}
		}

		// Token: 0x0600760D RID: 30221 RVA: 0x001846D8 File Offset: 0x001828D8
		protected void SetMetrics(ADMetrics newMetrics, string forestFqdn)
		{
			ADMetrics oldMetrics;
			if (newMetrics == null)
			{
				this.metricValuesDictionary[forestFqdn] = -1;
			}
			else if (newMetrics.AllServerMetrics.Count == 1)
			{
				this.metricValuesDictionary[forestFqdn] = 0;
			}
			else if (this.metricsDictionary.TryGetValue(forestFqdn, out oldMetrics))
			{
				this.SetInjectionRates(oldMetrics, newMetrics);
				this.metricValuesDictionary[forestFqdn] = this.CalculateMetricValue(newMetrics);
				if (DirectoryThrottlingLog.LoggingEnabled)
				{
					int countOfDCsToLog = DirectoryThrottlingLog.CountOfDCsToLog;
					Dictionary<string, ADServerMetrics> topNDomainControllers = newMetrics.GetTopNDomainControllers(countOfDCsToLog);
					IResourceLoadMonitor resourceLoadMonitor = ResourceHealthMonitorManager.Singleton.Get(ADResourceKey.Key);
					ResourceLoad resourceLoad = resourceLoadMonitor.GetResourceLoad(WorkloadClassification.Discretionary, false, forestFqdn);
					DirectoryThrottlingLog.Instance.Log(forestFqdn, resourceLoad.State, this.metricValuesDictionary[forestFqdn], topNDomainControllers);
				}
				ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<string, int>((long)this.GetHashCode(), "[ADResourceHealthMonitor::SetMetrics] Current Metrics for forest {0} is={1}.", forestFqdn, this.metricValuesDictionary[forestFqdn]);
			}
			else
			{
				this.metricValuesDictionary[forestFqdn] = -1;
			}
			if (newMetrics != null)
			{
				this.metricsDictionary[forestFqdn] = newMetrics;
				this.LastUpdateUtc = (DateTime)newMetrics.UpdateTime;
			}
		}

		// Token: 0x0600760E RID: 30222 RVA: 0x001847EC File Offset: 0x001829EC
		private int CalculateHealth(ADMetrics newMetrics)
		{
			int result = Math.Min(ADResourceHealthMonitor.CalculateIncomingReplicationHealth(newMetrics, this.GetHashCode()), ADResourceHealthMonitor.CalculateOutgoingReplicationHealth(newMetrics, this.GetHashCode()));
			string text = newMetrics.GetReport();
			if (text.Length > 31766)
			{
				text = text.Substring(0, 31762) + "...";
			}
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_ADHealthReport, newMetrics.ForestFqdn, new object[]
			{
				text
			});
			return result;
		}

		// Token: 0x0600760F RID: 30223 RVA: 0x00184860 File Offset: 0x00182A60
		private int CalculateMetricValue(ADMetrics newMetrics)
		{
			return Math.Max((int)ADResourceHealthMonitor.CalculateIncomingReplicationLatency(newMetrics, this.GetHashCode()).TotalMinutes, (int)ADResourceHealthMonitor.CalculateOutgoingReplicationLatency(newMetrics, this.GetHashCode()).TotalMinutes);
		}

		// Token: 0x06007610 RID: 30224 RVA: 0x0018489C File Offset: 0x00182A9C
		private void SetInjectionRates(ADMetrics oldMetrics, ADMetrics newMetrics)
		{
			if (oldMetrics == null)
			{
				throw new ArgumentNullException("oldMetrics");
			}
			foreach (ADServerMetrics adserverMetrics in newMetrics.AllServerMetrics)
			{
				if (adserverMetrics.IsSuitable)
				{
					adserverMetrics.SetInjectionRate(oldMetrics[adserverMetrics.DnsHostName]);
				}
			}
		}

		// Token: 0x06007611 RID: 30225 RVA: 0x0018490C File Offset: 0x00182B0C
		private static int CalculateIncomingReplicationHealth(ADMetrics newMetrics, int traceKey)
		{
			double num = 0.0;
			int num2 = 100;
			ADServerMetrics adserverMetrics = null;
			int num3 = 0;
			foreach (ADServerMetrics adserverMetrics2 in newMetrics.AllServerMetrics)
			{
				if (adserverMetrics2.IsSuitable && adserverMetrics2.InjectionRate >= 0.0)
				{
					adserverMetrics2.IncomingDebt = 0L;
					foreach (ADReplicationLinkMetrics adreplicationLinkMetrics in adserverMetrics2.ConfigReplicationMetrics)
					{
						if (newMetrics[adreplicationLinkMetrics.NeighborDnsHostName].IsSuitable)
						{
							adreplicationLinkMetrics.Debt = newMetrics[adreplicationLinkMetrics.NeighborDnsHostName].HighestUsn - adreplicationLinkMetrics.UpToDatenessUsn;
							if (adreplicationLinkMetrics.Debt > adserverMetrics2.IncomingDebt)
							{
								adserverMetrics2.IncomingDebt = adreplicationLinkMetrics.Debt;
							}
						}
					}
					foreach (ADReplicationLinkMetrics adreplicationLinkMetrics2 in adserverMetrics2.DomainReplicationMetrics)
					{
						if (newMetrics[adreplicationLinkMetrics2.NeighborDnsHostName].IsSuitable)
						{
							adreplicationLinkMetrics2.Debt = newMetrics[adreplicationLinkMetrics2.NeighborDnsHostName].HighestUsn - adreplicationLinkMetrics2.UpToDatenessUsn;
							if (adreplicationLinkMetrics2.Debt > adserverMetrics2.IncomingDebt)
							{
								adserverMetrics2.IncomingDebt = adreplicationLinkMetrics2.Debt;
							}
						}
					}
					ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<string, long>((long)traceKey, "[ADResourceHealthMonitor::CalculateIncomingReplicationHealth] {0}: IncomingDebt={1}.", adserverMetrics2.DnsHostName, adserverMetrics2.IncomingDebt);
					adserverMetrics2.IncomingHealth = ADResourceHealthMonitor.CalculateReplicationHealthMeasure((double)adserverMetrics2.IncomingDebt, adserverMetrics2.InjectionRate, traceKey);
					ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<string, int>((long)traceKey, "[ADResourceHealthMonitor::CalculateIncomingReplicationHealth] {0}: IncomingHealthMeasure={1}.", adserverMetrics2.DnsHostName, adserverMetrics2.IncomingHealth);
					if (adserverMetrics2.IncomingHealth < num2 || adserverMetrics == null)
					{
						num2 = adserverMetrics2.IncomingHealth;
						adserverMetrics = adserverMetrics2;
					}
					num += (double)adserverMetrics2.IncomingHealth;
					num3++;
				}
			}
			int num4;
			if (num3 > 2)
			{
				ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<int, string>((long)traceKey, "[ADResourceHealthMonitor::CalculateIncomingReplicationHealth] Ignoring worst incoming health measure {0} on DC {1}.", num2, (adserverMetrics != null) ? adserverMetrics.DnsHostName : "<none>");
				num4 = (int)((num - (double)num2) / (double)(num3 - 1));
			}
			else if (num3 > 1)
			{
				num4 = (int)(num / (double)num3);
			}
			else
			{
				ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug((long)traceKey, "[ADResourceHealthMonitor::CalculateIncomingReplicationHealth] Single suitable DCs. Considering fully healthy.");
				num4 = 100;
			}
			ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<int>((long)traceKey, "[ADResourceHealthMonitor::CalculateIncomingReplicationHealth] Total IncomingHealthMeasure={0}.", num4);
			newMetrics.IncomingHealth = num4;
			if (num2 < 100)
			{
				newMetrics.MinIncomingHealthServer = adserverMetrics.DnsHostName;
			}
			return num4;
		}

		// Token: 0x06007612 RID: 30226 RVA: 0x00184C08 File Offset: 0x00182E08
		private static int CalculateOutgoingReplicationHealth(ADMetrics newMetrics, int traceKey)
		{
			int num = 100;
			ADServerMetrics adserverMetrics = null;
			using (IEnumerator<ADServerMetrics> enumerator = newMetrics.AllServerMetrics.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ADServerMetrics dcMetrics = enumerator.Current;
					if (dcMetrics.IsSuitable && dcMetrics.InjectionRate >= 0.0)
					{
						foreach (ADServerMetrics adserverMetrics2 in newMetrics.AllServerMetrics)
						{
							if (adserverMetrics2.IsSuitable && adserverMetrics2.InjectionRate >= 0.0)
							{
								long? num2 = null;
								foreach (ICollection<ADReplicationLinkMetrics> source in new ICollection<ADReplicationLinkMetrics>[]
								{
									adserverMetrics2.ConfigReplicationMetrics,
									adserverMetrics2.DomainReplicationMetrics
								})
								{
									ADReplicationLinkMetrics adreplicationLinkMetrics = source.FirstOrDefault((ADReplicationLinkMetrics linkMetrics) => linkMetrics.NeighborDnsHostName.Equals(dcMetrics.DnsHostName, StringComparison.OrdinalIgnoreCase));
									if (adreplicationLinkMetrics != null && (num2 == null || num2.Value > dcMetrics.HighestUsn - adreplicationLinkMetrics.UpToDatenessUsn))
									{
										num2 = new long?(dcMetrics.HighestUsn - adreplicationLinkMetrics.UpToDatenessUsn);
									}
								}
								dcMetrics.OutgoingDebt = (num2 ?? 0L);
								ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<string, long>((long)traceKey, "[ADResourceHealthMonitor::CalculateOutgoingReplicationHealth] {0}: OutgoingDebt={1}.", dcMetrics.DnsHostName, dcMetrics.OutgoingDebt);
								dcMetrics.OutgoingHealth = ADResourceHealthMonitor.CalculateReplicationHealthMeasure((double)dcMetrics.OutgoingDebt, adserverMetrics2.InjectionRate, traceKey);
								ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<string, int>((long)traceKey, "[ADResourceHealthMonitor::CalculateOutgoingReplicationHealth] {0}: OutgoingHealthMeasure={1}.", dcMetrics.DnsHostName, dcMetrics.OutgoingHealth);
								if (dcMetrics.OutgoingHealth < num || adserverMetrics == null)
								{
									num = dcMetrics.OutgoingHealth;
									adserverMetrics = dcMetrics;
								}
							}
						}
					}
				}
			}
			ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<int, string>((long)traceKey, "[ADResourceHealthMonitor::CalculateOutgoingReplicationHealth] Min OutgoingHealthMeasure={0} on DC {1}.", num, (adserverMetrics != null) ? adserverMetrics.DnsHostName : "<none>");
			newMetrics.OutgoingHealth = num;
			if (num < 100)
			{
				newMetrics.MinOutgoingHealthServer = adserverMetrics.DnsHostName;
			}
			return num;
		}

		// Token: 0x06007613 RID: 30227 RVA: 0x00184EB0 File Offset: 0x001830B0
		private static int CalculateReplicationHealthMeasure(double debt, double injectionRate, int traceKey)
		{
			if (injectionRate < 1.0)
			{
				ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug((long)traceKey, "[ADResourceHealthMonitor::CalculateReplicationHealthMeasure] Injection rate is less than 1. Assuming at least 1.");
				injectionRate = 1.0;
			}
			TimeSpan t = TimeSpan.FromSeconds(debt / injectionRate);
			int result;
			if (t > ADHealthMonitorConfiguration.FairCutoff)
			{
				result = 0;
			}
			else if (t > ADHealthMonitorConfiguration.HealthyCutoff)
			{
				result = (int)(100.0 * (t - ADHealthMonitorConfiguration.HealthyCutoff).TotalSeconds / (ADHealthMonitorConfiguration.FairCutoff - ADHealthMonitorConfiguration.HealthyCutoff).TotalSeconds);
			}
			else
			{
				result = 100;
			}
			return result;
		}

		// Token: 0x06007614 RID: 30228 RVA: 0x00184F4C File Offset: 0x0018314C
		private static TimeSpan CalculateIncomingReplicationLatency(ADMetrics newMetrics, int traceKey)
		{
			TimeSpan t = TimeSpan.Zero;
			TimeSpan timeSpan = TimeSpan.Zero;
			ADServerMetrics adserverMetrics = null;
			int num = 0;
			foreach (ADServerMetrics adserverMetrics2 in newMetrics.AllServerMetrics)
			{
				if (adserverMetrics2.IsSuitable && adserverMetrics2.InjectionRate >= 0.0)
				{
					long num2 = 0L;
					foreach (ADReplicationLinkMetrics adreplicationLinkMetrics in adserverMetrics2.ConfigReplicationMetrics)
					{
						if (newMetrics[adreplicationLinkMetrics.NeighborDnsHostName].IsSuitable)
						{
							ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<string, long, long>((long)traceKey, "[ADResourceHealthMonitor::CalculateIncomingReplicationLatency] Config replicaiton partner={0}, HighestUsn={1}, UpToDatenessUsn={2}.", newMetrics[adreplicationLinkMetrics.NeighborDnsHostName].DnsHostName, newMetrics[adreplicationLinkMetrics.NeighborDnsHostName].HighestUsn, adreplicationLinkMetrics.UpToDatenessUsn);
							num2 += newMetrics[adreplicationLinkMetrics.NeighborDnsHostName].HighestUsn - adreplicationLinkMetrics.UpToDatenessUsn;
						}
					}
					foreach (ADReplicationLinkMetrics adreplicationLinkMetrics2 in adserverMetrics2.DomainReplicationMetrics)
					{
						if (newMetrics[adreplicationLinkMetrics2.NeighborDnsHostName].IsSuitable)
						{
							ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<string, long, long>((long)traceKey, "[ADResourceHealthMonitor::CalculateIncomingReplicationLatency] Domain replicaiton partner={0}, HighestUsn={1}, UpToDatenessUsn={2}.", newMetrics[adreplicationLinkMetrics2.NeighborDnsHostName].DnsHostName, newMetrics[adreplicationLinkMetrics2.NeighborDnsHostName].HighestUsn, adreplicationLinkMetrics2.UpToDatenessUsn);
							num2 += newMetrics[adreplicationLinkMetrics2.NeighborDnsHostName].HighestUsn - adreplicationLinkMetrics2.UpToDatenessUsn;
						}
					}
					adserverMetrics2.IncomingDebt = num2;
					ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<string, long>((long)traceKey, "[ADResourceHealthMonitor::CalculateIncomingReplicationLatency] {0}: IncomingDebt={1}.", adserverMetrics2.DnsHostName, num2);
					TimeSpan timeSpan2 = ADResourceHealthMonitor.CalculateReplicationLatency((double)num2, adserverMetrics2.InjectionRate, traceKey);
					ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<string, TimeSpan, double>((long)traceKey, "[ADResourceHealthMonitor::CalculateIncomingReplicationLatency] {0}: replicationLatency={1}, injectionRate={2}.", adserverMetrics2.DnsHostName, timeSpan2, adserverMetrics2.InjectionRate);
					if (timeSpan2 > timeSpan || adserverMetrics == null)
					{
						timeSpan = timeSpan2;
						adserverMetrics = adserverMetrics2;
					}
					t += timeSpan2;
					num++;
				}
			}
			TimeSpan timeSpan3 = TimeSpan.Zero;
			if (num > 2)
			{
				ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<TimeSpan, string>((long)traceKey, "[ADResourceHealthMonitor::CalculateIncomingReplicationLatency] Ignoring worst incoming health measure {0} on DC {1}.", timeSpan, (adserverMetrics != null) ? adserverMetrics.DnsHostName : "<none>");
				timeSpan3 = TimeSpan.FromTicks((t - timeSpan).Ticks / (long)(num - 1));
			}
			else if (num > 1)
			{
				timeSpan3 = TimeSpan.FromTicks(t.Ticks / (long)num);
			}
			else
			{
				ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug((long)traceKey, "[ADResourceHealthMonitor::CalculateIncomingReplicationLatency] Single suitable DCs. Considering fully healthy.");
				timeSpan3 = TimeSpan.Zero;
			}
			ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<TimeSpan>((long)traceKey, "[ADResourceHealthMonitor::CalculateIncomingReplicationLatency] Total IncomingHealthMeasure={0}.", timeSpan3);
			return timeSpan3;
		}

		// Token: 0x06007615 RID: 30229 RVA: 0x0018527C File Offset: 0x0018347C
		private static TimeSpan CalculateOutgoingReplicationLatency(ADMetrics newMetrics, int traceKey)
		{
			TimeSpan timeSpan = TimeSpan.Zero;
			ADServerMetrics adserverMetrics = null;
			using (IEnumerator<ADServerMetrics> enumerator = newMetrics.AllServerMetrics.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ADServerMetrics dcMetrics = enumerator.Current;
					if (dcMetrics.IsSuitable && dcMetrics.InjectionRate >= 0.0)
					{
						foreach (ADServerMetrics adserverMetrics2 in newMetrics.AllServerMetrics)
						{
							if (adserverMetrics2.IsSuitable && adserverMetrics2.InjectionRate >= 0.0)
							{
								long num = 0L;
								foreach (ICollection<ADReplicationLinkMetrics> source in new ICollection<ADReplicationLinkMetrics>[]
								{
									adserverMetrics2.ConfigReplicationMetrics,
									adserverMetrics2.DomainReplicationMetrics
								})
								{
									ADReplicationLinkMetrics adreplicationLinkMetrics = source.FirstOrDefault((ADReplicationLinkMetrics linkMetrics) => linkMetrics.NeighborDnsHostName.Equals(dcMetrics.DnsHostName, StringComparison.OrdinalIgnoreCase));
									if (adreplicationLinkMetrics != null)
									{
										num += dcMetrics.HighestUsn - adreplicationLinkMetrics.UpToDatenessUsn;
									}
								}
								ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<string, long>((long)traceKey, "[ADResourceHealthMonitor::CalculateOutgoingReplicationLatency] {0}: OutgoingDebt={1}.", dcMetrics.DnsHostName, num);
								dcMetrics.OutgoingDebt = num;
								TimeSpan timeSpan2 = ADResourceHealthMonitor.CalculateReplicationLatency((double)num, adserverMetrics2.InjectionRate, traceKey);
								ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<string, TimeSpan>((long)traceKey, "[ADResourceHealthMonitor::CalculateOutgoingReplicationLatency] {0}: replicationLatency={1}.", dcMetrics.DnsHostName, timeSpan2);
								if (timeSpan2 > timeSpan || adserverMetrics == null)
								{
									timeSpan = timeSpan2;
									adserverMetrics = dcMetrics;
								}
							}
						}
					}
				}
			}
			ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<TimeSpan, string>((long)traceKey, "[ADResourceHealthMonitor::CalculateOutgoingReplicationLatency] Min OutgoingHealthMeasure={0} on DC {1}.", timeSpan, (adserverMetrics != null) ? adserverMetrics.DnsHostName : "<none>");
			return timeSpan;
		}

		// Token: 0x06007616 RID: 30230 RVA: 0x00185494 File Offset: 0x00183694
		private static TimeSpan CalculateReplicationLatency(double debt, double injectionRate, int traceKey)
		{
			if (injectionRate < 1.0)
			{
				ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug((long)traceKey, "[ADResourceHealthMonitor::CalculateReplicationLatency] Injection rate is less than 1. Assuming at least 1.");
				injectionRate = 1.0;
			}
			return TimeSpan.FromSeconds(debt / injectionRate);
		}

		// Token: 0x06007617 RID: 30231 RVA: 0x001854C6 File Offset: 0x001836C6
		public override bool ShouldRemoveResourceFromCache()
		{
			return false;
		}

		// Token: 0x17002A4C RID: 10828
		// (get) Token: 0x06007618 RID: 30232 RVA: 0x001854C9 File Offset: 0x001836C9
		protected override int InternalMetricValue
		{
			get
			{
				if (this.metricValuesDictionary.Count > 0)
				{
					return this.metricValuesDictionary.Values.Min();
				}
				return 0;
			}
		}

		// Token: 0x06007619 RID: 30233 RVA: 0x001854EC File Offset: 0x001836EC
		protected override int InternalGetMetricValue(object optionalData)
		{
			string key = (optionalData != null) ? ((string)optionalData) : TopologyProvider.LocalForestFqdn;
			int result;
			if (this.metricValuesDictionary.TryGetValue(key, out result))
			{
				return result;
			}
			return 0;
		}

		// Token: 0x0600761A RID: 30234 RVA: 0x00185520 File Offset: 0x00183720
		private IEnumerable<string> GetForests()
		{
			Dictionary<string, bool> dictionary = new Dictionary<string, bool>(StringComparer.InvariantCultureIgnoreCase);
			if (ADResourceHealthMonitor.isMultiTenancyEnabled)
			{
				foreach (PartitionId partitionId in ADAccountPartitionLocator.GetAllAccountPartitionIds())
				{
					dictionary[partitionId.ForestFQDN] = true;
				}
			}
			dictionary[TopologyProvider.LocalForestFqdn] = true;
			return dictionary.Keys;
		}

		// Token: 0x04004BA8 RID: 19368
		private static readonly bool isMultiTenancyEnabled = VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled;

		// Token: 0x04004BA9 RID: 19369
		private Dictionary<string, ADMetrics> metricsDictionary = new Dictionary<string, ADMetrics>(StringComparer.InvariantCultureIgnoreCase);

		// Token: 0x04004BAA RID: 19370
		private Dictionary<string, int> metricValuesDictionary = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);
	}
}
