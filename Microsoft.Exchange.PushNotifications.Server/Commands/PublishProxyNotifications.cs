using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;
using Microsoft.Exchange.PushNotifications.Publishers;
using Microsoft.Exchange.PushNotifications.Server.Services;

namespace Microsoft.Exchange.PushNotifications.Server.Commands
{
	// Token: 0x02000015 RID: 21
	internal class PublishProxyNotifications : PublishNotificationsBase<MailboxNotificationBatch>
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00003064 File Offset: 0x00001264
		public PublishProxyNotifications(MailboxNotificationBatch notifications, PushNotificationPublisherManager publisherManager, AsyncCallback asyncCallback, object asyncState) : base(notifications, publisherManager, asyncCallback, asyncState)
		{
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003071 File Offset: 0x00001271
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00003079 File Offset: 0x00001279
		public OrganizationId AuthenticatedTenantId { get; private set; }

		// Token: 0x06000080 RID: 128 RVA: 0x00003084 File Offset: 0x00001284
		protected override void InternalInitialize(IBudget budget)
		{
			base.InternalInitialize(budget);
			TenantBudgetKey tenantBudgetKey = budget.Owner as TenantBudgetKey;
			if (tenantBudgetKey != null && tenantBudgetKey.OrganizationId != null)
			{
				this.AuthenticatedTenantId = tenantBudgetKey.OrganizationId;
			}
			if (this.AuthenticatedTenantId == null)
			{
				this.AuthenticatedTenantId = OrganizationId.ForestWideOrgId;
				string text = budget.Owner.ToString();
				if (base.RequestInstance == null || !base.RequestInstance.IsMonitoring() || !object.ReferenceEquals(budget.Owner, PushNotificationService.ServiceBudgetKey))
				{
					PushNotificationsCrimsonEvents.CannotResolveAuthenticatedTenantId.LogPeriodic<string>(text, CrimsonConstants.DefaultLogPeriodicSuppressionInMinutes, text);
					ExTraceGlobals.PushNotificationServiceTracer.TraceError((long)this.GetHashCode(), string.Format("Failed to resolve the authenticated Organization for the current request: '{0}'.", text));
				}
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003144 File Offset: 0x00001344
		protected override void Publish()
		{
			foreach (MailboxNotification notification in base.RequestInstance.Notifications)
			{
				PushNotificationPublishingContext context = new PushNotificationPublishingContext(base.NotificationSource, this.AuthenticatedTenantId, true, null);
				base.PublisherManager.Publish(notification, context);
			}
		}
	}
}
