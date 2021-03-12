using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000014 RID: 20
	internal abstract class CiResourceHealthMonitorBase : CacheableResourceHealthMonitor, IResourceHealthPoller
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x000041B8 File Offset: 0x000023B8
		protected CiResourceHealthMonitorBase(CiResourceKey key) : base(key)
		{
			this.mdbGuid = key.DatabaseGuid;
			this.average = new FixedTimeAverage((ushort)this.Interval.TotalMilliseconds, 6, Environment.TickCount);
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000CA RID: 202 RVA: 0x0000420B File Offset: 0x0000240B
		public bool IsActive
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000CB RID: 203 RVA: 0x0000420E File Offset: 0x0000240E
		public virtual TimeSpan Interval
		{
			get
			{
				return CiHealthMonitorConfiguration.RefreshInterval;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00004215 File Offset: 0x00002415
		protected override int InternalMetricValue
		{
			get
			{
				return (int)Interlocked.Read(ref this.metricValue);
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004224 File Offset: 0x00002424
		public virtual void Execute()
		{
			this.metricCalculationStage = "Execute";
			int num = this.CalculateNewMetric();
			if (num >= 0 && num != 2147483647)
			{
				this.lastMetricUpdateUnsuccessfulCount = 0;
				this.average.Add(Environment.TickCount, (uint)num);
				Interlocked.Exchange(ref this.metricValue, (long)((int)this.average.GetValue()));
			}
			else if (this.lastMetricUpdateUnsuccessfulCount < CiHealthMonitorConfiguration.FailedCatalogStatusThreshold)
			{
				this.lastMetricUpdateUnsuccessfulCount++;
			}
			else
			{
				Interlocked.Exchange(ref this.metricValue, (long)num);
			}
			this.LastUpdateUtc = DateTime.UtcNow;
			ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug((long)this.GetHashCode(), "[CiResourceHealthMonitorBase::Execute] (MetricType: {0}, MDB: {1}) Got new metric value of {2}, will report {3}", new object[]
			{
				base.Key.MetricType,
				this.mdbGuid,
				num,
				this.InternalMetricValue
			});
			this.metricCalculationStage = "Completed";
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004319 File Offset: 0x00002519
		protected override object GetResourceLoadlInfo(ResourceLoad load)
		{
			return this.info ?? this.metricCalculationStage;
		}

		// Token: 0x060000CF RID: 207
		protected abstract int GetMetricFromStatusInternal(RpcDatabaseCopyStatus2 status);

		// Token: 0x060000D0 RID: 208 RVA: 0x00004378 File Offset: 0x00002578
		private int CalculateNewMetric()
		{
			this.metricCalculationStage = "Calculate";
			if (!CiHealthMonitorConfiguration.Enabled)
			{
				ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<ResourceMetricType, Guid>((long)this.GetHashCode(), "[CiResourceHealthMonitorBase::CalculateNewMetric] (MetricType: {0}, MDB: {1}) Disabled, will return metric as unknown.", base.Key.MetricType, this.mdbGuid);
				this.metricCalculationStage = "Disabled";
				return -1;
			}
			if (CiHealthMonitorConfiguration.OverrideMetricValue != null)
			{
				ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<ResourceMetricType, Guid, int>((long)this.GetHashCode(), "[CiResourceHealthMonitorBase::CalculateNewMetric] (MetricType: {0}, MDB: {1}) Metric override set to {2}", base.Key.MetricType, this.mdbGuid, CiHealthMonitorConfiguration.OverrideMetricValue.Value);
				this.metricCalculationStage = "Override";
				return CiHealthMonitorConfiguration.OverrideMetricValue.Value;
			}
			List<string> serversForMdb = MdbCopyMonitor.Instance.Value.GetServersForMdb(this.mdbGuid);
			if (serversForMdb == null || serversForMdb.Count == 0)
			{
				ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<ResourceMetricType, Guid>((long)this.GetHashCode(), "[CiResourceHealthMonitorBase::CalculateNewMetric] (MetricType: {0}, MDB: {1}) Unable to get topology data from AD, will return metric as unknown.", base.Key.MetricType, this.mdbGuid);
				this.metricCalculationStage = "NoServers";
				return -1;
			}
			List<CiMdbCopyInfo> list = new List<CiMdbCopyInfo>(serversForMdb.Count);
			int num = -1;
			foreach (string text in serversForMdb)
			{
				RpcDatabaseCopyStatus2 rpcDatabaseCopyStatus = MailboxDatabaseCopyStatusCache.Instance.Value.TryGetCopyStatus(text, this.mdbGuid);
				if (rpcDatabaseCopyStatus == null)
				{
					ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<ResourceMetricType, Guid, string>((long)this.GetHashCode(), "[CiResourceHealthMonitorBase::CalculateNewMetric] (MetricType: {0}, MDB: {1}) Server {2} failed to report CI status.", base.Key.MetricType, this.mdbGuid, text);
					list.Add(new CiMdbCopyInfo(text));
				}
				else
				{
					int metricFromStatus = this.GetMetricFromStatus(rpcDatabaseCopyStatus.MailboxServer, rpcDatabaseCopyStatus);
					ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug((long)this.GetHashCode(), "[CiResourceHealthMonitorBase::CalculateNewMetric] (MetricType: {0}, MDB: {1}) Server {2} reporting metric as {3}.", new object[]
					{
						base.Key.MetricType,
						this.mdbGuid,
						rpcDatabaseCopyStatus.MailboxServer,
						metricFromStatus
					});
					CiMdbCopyInfo ciMdbCopyInfo = new CiMdbCopyInfo(text, rpcDatabaseCopyStatus.CopyStatus == CopyStatusEnum.Mounted, metricFromStatus);
					if (ciMdbCopyInfo.Mounted)
					{
						num = ciMdbCopyInfo.Metric;
						ciMdbCopyInfo.Used = true;
					}
					list.Add(ciMdbCopyInfo);
				}
			}
			int num2 = Math.Min(CiHealthMonitorConfiguration.NumberOfHealthyCopiesRequired, list.Count);
			list.Sort(delegate(CiMdbCopyInfo copy1, CiMdbCopyInfo copy2)
			{
				if (copy1.Mounted)
				{
					if (copy2.Mounted)
					{
						return 0;
					}
					return -1;
				}
				else
				{
					if (copy2.Mounted)
					{
						return 1;
					}
					if (copy1.Metric < copy2.Metric)
					{
						return -1;
					}
					if (copy1.Metric > copy2.Metric)
					{
						return 1;
					}
					return 0;
				}
			});
			int num3 = -1;
			for (int i = 0; i < num2; i++)
			{
				CiMdbCopyInfo ciMdbCopyInfo2 = list[i];
				ciMdbCopyInfo2.Used = true;
				if (!ciMdbCopyInfo2.Mounted && ciMdbCopyInfo2.Metric > num3)
				{
					num3 = ciMdbCopyInfo2.Metric;
				}
			}
			this.info = new CiMdbInfo(list);
			if (num == -1)
			{
				return -1;
			}
			return Math.Max(num, num3);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004668 File Offset: 0x00002868
		private int GetMetricFromStatus(string serverFqdn, RpcDatabaseCopyStatus2 status)
		{
			int result;
			switch (status.ContentIndexStatus)
			{
			case ContentIndexStatusType.Unknown:
				result = -1;
				break;
			case ContentIndexStatusType.Healthy:
				result = this.GetMetricFromStatusInternal(status);
				break;
			case ContentIndexStatusType.Crawling:
				result = 0;
				break;
			case ContentIndexStatusType.Failed:
			case ContentIndexStatusType.Seeding:
			case ContentIndexStatusType.FailedAndSuspended:
			case ContentIndexStatusType.Suspended:
			case ContentIndexStatusType.AutoSuspended:
				result = int.MaxValue;
				break;
			case ContentIndexStatusType.Disabled:
				result = 0;
				break;
			case ContentIndexStatusType.HealthyAndUpgrading:
				result = 0;
				break;
			default:
				result = -1;
				break;
			}
			ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug((long)this.GetHashCode(), "[CiResourceHealthMonitorBase::GetMetricFromStatus] (MetricType: {0}, MDB: {1}) Server {2} reporting Search status as {3}.", new object[]
			{
				base.Key.MetricType,
				this.mdbGuid,
				serverFqdn,
				status.ContentIndexStatus
			});
			return result;
		}

		// Token: 0x04000057 RID: 87
		private const int Unknown = -1;

		// Token: 0x04000058 RID: 88
		private const int Failed = 2147483647;

		// Token: 0x04000059 RID: 89
		private const int DontThrottle = 0;

		// Token: 0x0400005A RID: 90
		private readonly Guid mdbGuid;

		// Token: 0x0400005B RID: 91
		private readonly FixedTimeAverage average;

		// Token: 0x0400005C RID: 92
		private int lastMetricUpdateUnsuccessfulCount;

		// Token: 0x0400005D RID: 93
		private long metricValue = -1L;

		// Token: 0x0400005E RID: 94
		private CiMdbInfo info;

		// Token: 0x0400005F RID: 95
		private string metricCalculationStage = "NotStarted";
	}
}
