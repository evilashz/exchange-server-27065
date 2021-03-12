using System;
using Microsoft.Exchange.Data.Metering;
using Microsoft.Exchange.Data.Metering.Throttling;
using Microsoft.Exchange.Transport.LoggingCommon;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000354 RID: 852
	internal class QueueQuotaLoggingContext
	{
		// Token: 0x060024FE RID: 9470 RVA: 0x0008EB48 File Offset: 0x0008CD48
		public QueueQuotaLoggingContext(ICountedEntity<MeteredEntity> entity, QueueQuotaResources resource, IQueueQuotaThresholdFetcher thresholdFetcher)
		{
			this.Resource = QueueQuotaHelper.GetThrottlingResource(resource);
			if (entity.Name.Type == MeteredEntity.Tenant)
			{
				this.Scope = ThrottlingScope.Tenant;
				this.ScopeValue = entity.Name.Value;
				Guid guid;
				if (Guid.TryParse(entity.Name.Value, out guid))
				{
					this.OrgId = guid;
				}
				this.HighThreshold = thresholdFetcher.GetOrganizationQuotaHighMark(guid, resource);
				this.WarningThreshold = thresholdFetcher.GetOrganizationWarningMark(guid, resource);
				return;
			}
			if (entity.Name.Type == MeteredEntity.Sender)
			{
				this.Scope = ThrottlingScope.Sender;
				this.Sender = QueueQuotaHelper.GetRedactedSender(QueueQuotaEntity.Sender, entity.Name.Value);
				this.ScopeValue = this.Sender;
				Guid guid2;
				if (Guid.TryParse(entity.GroupName.Value, out guid2))
				{
					this.OrgId = guid2;
				}
				this.HighThreshold = thresholdFetcher.GetSenderQuotaHighMark(guid2, entity.Name.Value, resource);
				this.WarningThreshold = thresholdFetcher.GetSenderWarningMark(guid2, entity.Name.Value, resource);
				return;
			}
			if (entity.Name.Type == MeteredEntity.AccountForest)
			{
				this.Scope = ThrottlingScope.AccountForest;
				this.OrgId = Guid.Empty;
				this.ScopeValue = entity.Name.Value;
				this.HighThreshold = thresholdFetcher.GetAccountForestQuotaHighMark(resource);
				this.WarningThreshold = thresholdFetcher.GetAccountForestWarningMark(resource);
			}
		}

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x060024FF RID: 9471 RVA: 0x0008EC99 File Offset: 0x0008CE99
		// (set) Token: 0x06002500 RID: 9472 RVA: 0x0008ECA1 File Offset: 0x0008CEA1
		internal Guid OrgId { get; private set; }

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x06002501 RID: 9473 RVA: 0x0008ECAA File Offset: 0x0008CEAA
		// (set) Token: 0x06002502 RID: 9474 RVA: 0x0008ECB2 File Offset: 0x0008CEB2
		internal string Sender { get; private set; }

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06002503 RID: 9475 RVA: 0x0008ECBB File Offset: 0x0008CEBB
		// (set) Token: 0x06002504 RID: 9476 RVA: 0x0008ECC3 File Offset: 0x0008CEC3
		internal int HighThreshold { get; private set; }

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06002505 RID: 9477 RVA: 0x0008ECCC File Offset: 0x0008CECC
		// (set) Token: 0x06002506 RID: 9478 RVA: 0x0008ECD4 File Offset: 0x0008CED4
		internal int WarningThreshold { get; private set; }

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06002507 RID: 9479 RVA: 0x0008ECDD File Offset: 0x0008CEDD
		// (set) Token: 0x06002508 RID: 9480 RVA: 0x0008ECE5 File Offset: 0x0008CEE5
		internal ThrottlingScope Scope { get; private set; }

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x06002509 RID: 9481 RVA: 0x0008ECEE File Offset: 0x0008CEEE
		// (set) Token: 0x0600250A RID: 9482 RVA: 0x0008ECF6 File Offset: 0x0008CEF6
		internal string ScopeValue { get; private set; }

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x0600250B RID: 9483 RVA: 0x0008ECFF File Offset: 0x0008CEFF
		// (set) Token: 0x0600250C RID: 9484 RVA: 0x0008ED07 File Offset: 0x0008CF07
		internal ThrottlingResource Resource { get; private set; }
	}
}
