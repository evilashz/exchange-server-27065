using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;
using Microsoft.Exchange.PushNotifications.Extensions;
using Microsoft.Exchange.PushNotifications.Publishers;
using Microsoft.Exchange.PushNotifications.Server.Core;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.PushNotifications.Server.Commands
{
	// Token: 0x0200000F RID: 15
	internal class GetAppConfigData : ServiceCommand<AzureAppConfigRequestInfo, AzureAppConfigResponseInfo>
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00002AE0 File Offset: 0x00000CE0
		public GetAppConfigData(AzureAppConfigRequestInfo requestConfig, PushNotificationPublisherManager publisherManager, PushNotificationPublisherConfiguration configuration, AsyncCallback asyncCallback, object asyncState) : base(requestConfig, asyncCallback, asyncState)
		{
			this.PublisherManager = publisherManager;
			this.Configuration = configuration;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002AFB File Offset: 0x00000CFB
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00002B03 File Offset: 0x00000D03
		public OrganizationId AuthenticatedTenantId { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002B0C File Offset: 0x00000D0C
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00002B14 File Offset: 0x00000D14
		private PushNotificationPublisherManager PublisherManager { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002B1D File Offset: 0x00000D1D
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00002B25 File Offset: 0x00000D25
		private PushNotificationPublisherConfiguration Configuration { get; set; }

		// Token: 0x0600006A RID: 106 RVA: 0x00002B30 File Offset: 0x00000D30
		protected override void InternalInitialize(IBudget budget)
		{
			base.InternalInitialize(budget);
			this.AuthenticatedTenantId = OrganizationId.ForestWideOrgId;
			TenantBudgetKey tenantBudgetKey = budget.Owner as TenantBudgetKey;
			if (tenantBudgetKey != null && tenantBudgetKey.OrganizationId != null)
			{
				this.AuthenticatedTenantId = tenantBudgetKey.OrganizationId;
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002B80 File Offset: 0x00000D80
		protected sealed override AzureAppConfigResponseInfo InternalExecute(TimeSpan queueAndDelay, TimeSpan totalTime)
		{
			List<AzureAppConfigData> list = new List<AzureAppConfigData>();
			string text = null;
			if (this.AuthenticatedTenantId != null && !OrganizationId.ForestWideOrgId.Equals(this.AuthenticatedTenantId))
			{
				text = this.AuthenticatedTenantId.ToExternalDirectoryOrganizationId();
			}
			if (string.IsNullOrWhiteSpace(text))
			{
				string text2 = base.Budget.Owner.ToNullableString(null);
				PushNotificationsCrimsonEvents.CannotResolveOrganizationId.LogPeriodic<string, string>(text2, CrimsonConstants.DefaultLogPeriodicSuppressionInMinutes, text2, (base.RequestInstance != null) ? base.RequestInstance.ToFullString() : "null");
				return new AzureAppConfigResponseInfo(list.ToArray(), text);
			}
			PushNotificationPublishingContext context = new PushNotificationPublishingContext(base.GetType().Name, this.AuthenticatedTenantId, false, text);
			foreach (string text3 in base.RequestInstance.AppIds)
			{
				if (!this.Configuration.AzureSendPublisherSettings.ContainsKey(text3))
				{
					string text4 = base.Budget.Owner.ToNullableString(null);
					PushNotificationsCrimsonEvents.CannotResolvePublisherSettings.LogPeriodic<string, string, string>(text4, CrimsonConstants.DefaultLogPeriodicSuppressionInMinutes, text4, text3, (base.RequestInstance != null) ? base.RequestInstance.ToFullString() : "null");
				}
				else if (!this.Configuration.AzureSendPublisherSettings[text3].IsMultifactorRegistrationEnabled)
				{
					string text5 = (base.Budget == null) ? base.Budget.ToNullableString(null) : base.Budget.Owner.ToNullableString(null);
					PushNotificationsCrimsonEvents.MultifactorRegistrationDisabled.LogPeriodic<string, string, string>(text5, CrimsonConstants.DefaultLogPeriodicSuppressionInMinutes, text5, text3, (base.RequestInstance != null) ? base.RequestInstance.ToFullString() : "null");
				}
				else
				{
					AzureChannelSettings channelSettings = this.Configuration.AzureSendPublisherSettings[text3].ChannelSettings;
					this.PublisherManager.Publish(new AzureHubDefinition(text, text3), context);
					string resourceUri = channelSettings.UriTemplate.CreateOnPremResourceStringUri(text3, text);
					AzureSasToken azureSasToken = channelSettings.AzureSasTokenProvider.CreateSasToken(resourceUri, 93600);
					list.Add(new AzureAppConfigData(text3, azureSasToken.ToJson(), channelSettings.PartitionName));
				}
			}
			return new AzureAppConfigResponseInfo(list.ToArray(), text);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002DA4 File Offset: 0x00000FA4
		protected override ResourceKey[] InternalGetResources()
		{
			return new ResourceKey[]
			{
				ProcessorResourceKey.Local
			};
		}
	}
}
