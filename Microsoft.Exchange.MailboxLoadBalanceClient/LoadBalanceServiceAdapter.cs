using System;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.CapacityData;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.LoadBalance;
using Microsoft.Exchange.MailboxLoadBalance.Provisioning;
using Microsoft.Exchange.MailboxLoadBalance.ServiceSupport;

namespace Microsoft.Exchange.MailboxLoadBalanceClient
{
	// Token: 0x02000005 RID: 5
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LoadBalanceServiceAdapter : ILoadBalanceServicePort
	{
		// Token: 0x06000019 RID: 25 RVA: 0x000023DF File Offset: 0x000005DF
		protected LoadBalanceServiceAdapter(ILogger logger)
		{
			this.logger = logger;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000023EE File Offset: 0x000005EE
		protected virtual ByteQuantifiedSize AverageMailboxSize
		{
			get
			{
				return ByteQuantifiedSize.FromMB(100UL);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023F8 File Offset: 0x000005F8
		public static ILoadBalanceServicePort Create(ILogger logger)
		{
			VersionedClientBase<ILoadBalanceService>.UseUpdatedBinding = true;
			return new LoadBalanceServiceAdapter(logger);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002408 File Offset: 0x00000608
		public virtual BatchCapacityDatum GetBatchCapacityForForest(int numberOfMailboxes)
		{
			return this.GetBatchCapacityForForest(numberOfMailboxes, ByteQuantifiedSize.FromMB((ulong)((long)numberOfMailboxes * (long)this.AverageMailboxSize.ToMB())));
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002450 File Offset: 0x00000650
		public virtual BatchCapacityDatum GetBatchCapacityForForest(int numberOfMailboxes, ByteQuantifiedSize expectedBatchSize)
		{
			Func<ILoadBalanceService, BatchCapacityDatum> func = (ILoadBalanceService service) => service.GetConsumerBatchCapacity(numberOfMailboxes, expectedBatchSize);
			return this.CallClientFunction<BatchCapacityDatum>(func);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024A4 File Offset: 0x000006A4
		public CapacitySummary GetCapacitySummary(DirectoryIdentity objectIdentity, bool refreshData)
		{
			Func<ILoadBalanceService, HeatMapCapacityData> func = (ILoadBalanceService service) => service.GetCapacitySummary(objectIdentity, refreshData);
			HeatMapCapacityData capacityDatum = this.CallClientFunction<HeatMapCapacityData>(func);
			return CapacitySummary.FromDatum(capacityDatum);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002510 File Offset: 0x00000710
		public ADObjectId GetDatabaseForNewConsumerMailbox()
		{
			MailboxProvisioningResult provisioningResult = null;
			this.CallClientFunction<MailboxProvisioningResult>((ILoadBalanceService svc) => provisioningResult = svc.GetLocalDatabaseForProvisioning(new MailboxProvisioningData(ByteQuantifiedSize.Zero, null, null)));
			if (provisioningResult == null)
			{
				return null;
			}
			provisioningResult.ValidateSelection();
			return provisioningResult.Database.ADObjectId;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002564 File Offset: 0x00000764
		protected T CallClientFunction<T>(Func<ILoadBalanceService, T> func)
		{
			Server server = LocalServer.GetServer();
			T result;
			using (LoadBalancerClient loadBalancerClient = LoadBalancerClient.Create(server.Name, NullDirectory.Instance, this.logger))
			{
				this.logger.Log(MigrationEventType.Verbose, "Making WCF call to load balancer", new object[0]);
				result = func(loadBalancerClient);
			}
			return result;
		}

		// Token: 0x04000008 RID: 8
		private readonly ILogger logger;
	}
}
