using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Reporting;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Hygiene.Data.DataProvider;

namespace Microsoft.Exchange.Hygiene.Data.Reporting
{
	// Token: 0x020001C9 RID: 457
	internal sealed class ReportingSession : HygieneSession, ITenantThrottlingSession
	{
		// Token: 0x0600132F RID: 4911 RVA: 0x0003A197 File Offset: 0x00038397
		public ReportingSession()
		{
			this.DataProvider = ConfigDataProviderFactory.Default.Create(DatabaseType.Mtrt);
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x0003A1B0 File Offset: 0x000383B0
		public static Guid GenerateNewId()
		{
			return CombGuidGenerator.NewGuid();
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x0003A1B8 File Offset: 0x000383B8
		public IPagedReader<AggOutboundIpHistory> FindPagedOutboundTenantHistory(int lastNMinutes, int perTenantMinimumEmailThreshold, int pageSize = 1000)
		{
			return this.GetPagedReader<AggOutboundIpHistory>(QueryFilter.AndTogether(new QueryFilter[]
			{
				ReportingSession.NewPropertyFilter(AggOutboundIPSchema.LastNMinutesQueryProperty, lastNMinutes),
				ReportingSession.NewPropertyFilter(AggOutboundIPSchema.MinimumEmailThresholdQueryProperty, perTenantMinimumEmailThreshold),
				ReportingSession.NewPropertyFilter(AggOutboundIPSchema.PageSizeQueryProperty, pageSize)
			}), pageSize);
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x0003A214 File Offset: 0x00038414
		public IPagedReader<AggOutboundEmailAddressIpHistory> FindPagedOutboundStatsGroupByIPEmailAddress(int lastNMinutes, int perTenantPerAddressMinimumEmailThreshold, int pageSize = 1000, bool summaryOnly = true)
		{
			return this.GetPagedReader<AggOutboundEmailAddressIpHistory>(QueryFilter.AndTogether(new QueryFilter[]
			{
				ReportingSession.NewPropertyFilter(AggOutboundIPSchema.LastNMinutesQueryProperty, lastNMinutes),
				ReportingSession.NewPropertyFilter(AggOutboundIPSchema.MinimumEmailThresholdQueryProperty, perTenantPerAddressMinimumEmailThreshold),
				ReportingSession.NewPropertyFilter(AggOutboundIPSchema.PageSizeQueryProperty, pageSize),
				ReportingSession.NewPropertyFilter(AggOutboundIPSchema.SummaryOnlyQueryProperty, summaryOnly)
			}), pageSize);
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x0003A284 File Offset: 0x00038484
		public IPagedReader<AggOutboundEmailAddressIpHistory> FindPagedOutboundHistoricalStatsEmailAddress(int lastNMinutes, Guid tenantId, string emailAddress, int pageSize = 1000)
		{
			return new ConfigDataProviderPagedReader<AggOutboundEmailAddressIpHistory>(this.DataProvider, null, QueryFilter.AndTogether(new QueryFilter[]
			{
				ReportingSession.NewPropertyFilter(AggOutboundIPSchema.LastNMinutesQueryProperty, lastNMinutes),
				ReportingSession.NewPropertyFilter(AggOutboundIPSchema.TenantIdProperty, tenantId),
				ReportingSession.NewPropertyFilter(AggOutboundIPSchema.FromEmailAddressProperty, emailAddress),
				ReportingSession.NewPropertyFilter(AggOutboundIPSchema.PageSizeQueryProperty, pageSize)
			}), null, pageSize);
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x0003A2F8 File Offset: 0x000384F8
		public IPagedReader<AggInboundSpamDataHistory> FindPagedInboundSpamIPData(int lastNMinutes, double spamPercentageThreshold, int spamCountThreshold, int pageSize = 1000)
		{
			return this.GetPagedReader<AggInboundSpamDataHistory>(QueryFilter.AndTogether(new QueryFilter[]
			{
				ReportingSession.NewPropertyFilter(AggInboundIPSchema.LastNMinutesQueryProperty, lastNMinutes),
				ReportingSession.NewPropertyFilter(AggInboundIPSchema.MinimumSpamPercentageQueryProperty, spamPercentageThreshold),
				ReportingSession.NewPropertyFilter(AggInboundIPSchema.MinimumSpamCountQueryProperty, spamCountThreshold),
				ReportingSession.NewPropertyFilter(AggInboundIPSchema.PageSizeQueryProperty, pageSize)
			}), pageSize);
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x0003A368 File Offset: 0x00038568
		public IPagedReader<AggInboundSpamDataHistory> GetPagedInboundHistoricalIPData(int lastNMinutes, IPAddress startingIpAddress, IPAddress endIpAddress, int pageSize = 1000)
		{
			return this.GetPagedReader<AggInboundSpamDataHistory>(QueryFilter.AndTogether(new QueryFilter[]
			{
				ReportingSession.NewPropertyFilter(AggInboundIPSchema.LastNMinutesQueryProperty, lastNMinutes),
				ReportingSession.NewPropertyFilter(AggInboundIPSchema.StartingIPAddressQueryProperty, startingIpAddress),
				ReportingSession.NewPropertyFilter(AggInboundIPSchema.EndIPAddressQueryProperty, endIpAddress),
				ReportingSession.NewPropertyFilter(AggInboundIPSchema.PageSizeQueryProperty, pageSize)
			}), pageSize);
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x0003A3D8 File Offset: 0x000385D8
		public bool CheckForGoodMailFromRange(int lastNMinutes, int goodMessageThreshold, IPAddress startingIpAddress, IPAddress endIpAddress)
		{
			List<GoodMessageData> source = this.DataProvider.Find<GoodMessageData>(QueryFilter.AndTogether(new QueryFilter[]
			{
				ReportingSession.NewPropertyFilter(GoodMessageSchema.LastNMinutesQueryProperty, lastNMinutes),
				ReportingSession.NewPropertyFilter(GoodMessageSchema.MinimumGoodMessageCountQueryProperty, goodMessageThreshold),
				ReportingSession.NewPropertyFilter(GoodMessageSchema.StartingIPAddressQueryProperty, startingIpAddress),
				ReportingSession.NewPropertyFilter(GoodMessageSchema.EndIPAddressQueryProperty, endIpAddress)
			}), null, true, null).Cast<GoodMessageData>().ToList<GoodMessageData>();
			return source.Any((GoodMessageData goodMsgData) => goodMsgData.GoodMessageExists);
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x0003A474 File Offset: 0x00038674
		public TransportProcessingQuotaConfig GetTransportThrottlingConfig()
		{
			string name = typeof(TransportProcessingQuotaConfig).Name;
			new ComparisonFilter(ComparisonOperator.Equal, TransportProcessingQuotaConfigSchema.SettingName, name);
			return this.DataProvider.Find<TransportProcessingQuotaConfig>(null, null, false, null).Cast<TransportProcessingQuotaConfig>().FirstOrDefault<TransportProcessingQuotaConfig>();
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x0003A4B7 File Offset: 0x000386B7
		public void SetTransportThrottlingConfig(TransportProcessingQuotaConfig config)
		{
			this.DataProvider.Save(config);
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x0003A4C8 File Offset: 0x000386C8
		public void SetThrottleState(TenantThrottleInfo throttleInfo)
		{
			if (throttleInfo == null)
			{
				throw new ArgumentNullException("throttleInfo");
			}
			this.SaveTenantThrottleInfo(new List<TenantThrottleInfo>
			{
				throttleInfo
			}, 0);
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x0003A4F8 File Offset: 0x000386F8
		public TenantThrottleInfo GetThrottleState(Guid tenantId)
		{
			int partitionId = 0;
			bool overriddenOnly = true;
			List<TenantThrottleInfo> tenantThrottlingDigest = this.GetTenantThrottlingDigest(partitionId, new Guid?(tenantId), overriddenOnly, 50, false);
			if (tenantThrottlingDigest == null || tenantThrottlingDigest.Count == 0)
			{
				return null;
			}
			return tenantThrottlingDigest[0];
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x0003A548 File Offset: 0x00038748
		public void SaveTenantThrottleInfo(List<TenantThrottleInfo> throttleInfoList, int partitionId = 0)
		{
			if (throttleInfoList == null || throttleInfoList.Count == 0)
			{
				throw new ArgumentException("throttleInfo");
			}
			int physicalPartitionCopyCount = this.GetPhysicalPartitionCopyCount(partitionId);
			List<TransientDALException> list = null;
			for (int j = 0; j < physicalPartitionCopyCount; j++)
			{
				try
				{
					TenantThrottleInfoBatch batch = new TenantThrottleInfoBatch
					{
						PartitionId = partitionId,
						FssCopyId = j
					};
					throttleInfoList.ForEach(delegate(TenantThrottleInfo i)
					{
						batch.TenantThrottleInfoList.Add(i);
					});
					this.DataProvider.Save(batch);
				}
				catch (TransientDALException item)
				{
					if (list == null)
					{
						list = new List<TransientDALException>();
					}
					list.Add(item);
				}
			}
			if (list != null && list.Count == physicalPartitionCopyCount)
			{
				throw new AggregateException(list.ToArray());
			}
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x0003A608 File Offset: 0x00038808
		public List<TenantThrottleInfo> GetTenantThrottlingDigest(int partitionId = 0, Guid? tenantId = null, bool overriddenOnly = false, int tenantCount = 50, bool throttledOnly = true)
		{
			return this.DataProvider.Find<TenantThrottleInfo>(QueryFilter.AndTogether(new QueryFilter[]
			{
				ReportingSession.NewPropertyFilter(DalHelper.PhysicalInstanceKeyProp, partitionId),
				ReportingSession.NewPropertyFilter(ReportingCommonSchema.OrganizationalUnitRootProperty, tenantId),
				ReportingSession.NewPropertyFilter(ReportingCommonSchema.OverriddenOnlyProperty, overriddenOnly),
				ReportingSession.NewPropertyFilter(ReportingCommonSchema.DataCountProperty, tenantCount),
				ReportingSession.NewPropertyFilter(ReportingCommonSchema.ThrottledOnlyProperty, throttledOnly)
			}), null, true, null).Cast<TenantThrottleInfo>().ToList<TenantThrottleInfo>();
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x0003A69C File Offset: 0x0003889C
		public int GetPhysicalPartitionCount()
		{
			IPartitionedDataProvider partitionedDataProvider = this.DataProvider as IPartitionedDataProvider;
			if (partitionedDataProvider == null)
			{
				throw new NotSupportedException("GetPhysicalPartitionCount may not be called from an environment that does not use a partitioned data provider.");
			}
			return partitionedDataProvider.GetNumberOfPhysicalPartitions();
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x0003A6C9 File Offset: 0x000388C9
		private static ComparisonFilter NewPropertyFilter(PropertyDefinition property, object propertyValue)
		{
			return new ComparisonFilter(ComparisonOperator.Equal, property, propertyValue);
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x0003A6D4 File Offset: 0x000388D4
		private IPagedReader<T> GetPagedReader<T>(QueryFilter queryFilter, int pageSize) where T : IConfigurable, new()
		{
			List<IPagedReader<T>> list = new List<IPagedReader<T>>();
			foreach (object propertyValue in ((IPartitionedDataProvider)this.DataProvider).GetAllPhysicalPartitions())
			{
				list.Add(new ConfigDataProviderPagedReader<T>(this.DataProvider, null, QueryFilter.AndTogether(new QueryFilter[]
				{
					ReportingSession.NewPropertyFilter(DalHelper.PhysicalInstanceKeyProp, propertyValue),
					queryFilter
				}), null, pageSize));
			}
			return new CompositePagedReader<T>(list.ToArray());
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x0003A74C File Offset: 0x0003894C
		private int GetPhysicalPartitionCopyCount(int physicalInstanceId)
		{
			IPartitionedDataProvider partitionedDataProvider = this.DataProvider as IPartitionedDataProvider;
			if (partitionedDataProvider == null)
			{
				throw new NotSupportedException("GetPhysicalPartitionCopyCount may not be called from an environment that does not use a partitioned data provider.");
			}
			return partitionedDataProvider.GetNumberOfPersistentCopiesPerPartition(physicalInstanceId);
		}

		// Token: 0x04000946 RID: 2374
		internal readonly IConfigDataProvider DataProvider;
	}
}
