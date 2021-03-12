using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.PushNotifications;
using Microsoft.Exchange.PushNotifications.Client;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000384 RID: 900
	internal class SubscribeToPushNotification : BaseSubscribeToPushNotificationCommand<SubscribeToPushNotificationRequest>
	{
		// Token: 0x06001909 RID: 6409 RVA: 0x0008B088 File Offset: 0x00089288
		public SubscribeToPushNotification(CallContext callContext, SubscribeToPushNotificationRequest request) : base(callContext, request)
		{
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x0008B092 File Offset: 0x00089292
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new SubscribeToPushNotificationResponse(base.Result.Code, base.Result.Error);
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x0008B0B0 File Offset: 0x000892B0
		protected override void InternalExecute(string subscriptionId)
		{
			string hubName = null;
			string owaDeviceId = base.CallContext.OwaDeviceId;
			using (IPushNotificationStorage pushNotificationStorage = PushNotificationStorage.Create(base.MailboxIdentityMailboxSession))
			{
				using (pushNotificationStorage.CreateOrUpdateSubscriptionItem(base.MailboxIdentityMailboxSession, subscriptionId, new PushNotificationServerSubscription(base.PushNotificationSubscription, DateTime.UtcNow, owaDeviceId)))
				{
				}
				PushNotificationsCrimsonEvents.SubscriptionPosted.LogPeriodic<string, string, Guid, string>(string.Format("{0}-{1}", base.PushNotificationSubscription.AppId, base.PushNotificationSubscription.DeviceNotificationId), TimeSpan.FromHours(12.0), base.PushNotificationSubscription.DeviceNotificationId, base.PushNotificationSubscription.AppId, base.MailboxIdentityMailboxSession.MailboxGuid, pushNotificationStorage.TenantId);
				hubName = pushNotificationStorage.TenantId;
			}
			int num = (base.Request != null) ? base.Request.LastUnseenEmailCount : 0;
			string text = base.PushNotificationSubscription.AppId ?? string.Empty;
			if (num > 0)
			{
				string text2 = base.MailboxIdentityMailboxSession.MailboxGuid.ToString();
				PushNotificationsCrimsonEvents.LastUnseenEmailCount.Log<string, int, string>(text, num, text2);
				ExTraceGlobals.NotificationsCallTracer.TraceDebug<string, int, string>(0L, "SubscribeToPushNotification.InternalExecute: App with AppId '{0}' reports {1} unseen Emails in the most recent received notification for mailbox '{2}'.", text, num, text2);
			}
			try
			{
				using (AzureDeviceRegistrationServiceProxy azureDeviceRegistrationServiceProxy = new AzureDeviceRegistrationServiceProxy(null))
				{
					azureDeviceRegistrationServiceProxy.EndDeviceRegistration(azureDeviceRegistrationServiceProxy.BeginDeviceRegistration(new AzureDeviceRegistrationInfo(base.PushNotificationSubscription.DeviceNotificationId, owaDeviceId, text, base.PushNotificationSubscription.RegistrationChallenge, hubName), null, null));
				}
			}
			catch (Exception exception)
			{
				PushNotificationsCrimsonEvents.AzureDeviceRegistrationRequestFailed.LogPeriodic<string, string, string, string, string, string>(base.PushNotificationSubscription.DeviceNotificationId, CrimsonConstants.DefaultLogPeriodicSuppressionInMinutes, text, base.PushNotificationSubscription.DeviceNotificationId, base.PushNotificationSubscription.DeviceNotificationType, subscriptionId, base.MailboxIdentityMailboxSession.MailboxGuid.ToString(), exception.ToTraceString());
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x0600190C RID: 6412 RVA: 0x0008B2C0 File Offset: 0x000894C0
		protected override string ClassName
		{
			get
			{
				return "SubscribeToPushNotification";
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x0600190D RID: 6413 RVA: 0x0008B2C7 File Offset: 0x000894C7
		protected override PushNotificationsCrimsonEvent ArgumentNullEvent
		{
			get
			{
				return PushNotificationsCrimsonEvents.SubscriptionArgumentNull;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x0600190E RID: 6414 RVA: 0x0008B2CE File Offset: 0x000894CE
		protected override PushNotificationsCrimsonEvent InvalidNotificationTypeEvent
		{
			get
			{
				return PushNotificationsCrimsonEvents.SubscriptionInvalidNotificationType;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x0600190F RID: 6415 RVA: 0x0008B2D5 File Offset: 0x000894D5
		protected override PushNotificationsCrimsonEvent UnexpectedErrorEvent
		{
			get
			{
				return PushNotificationsCrimsonEvents.SubscriptionUnexpectedError;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06001910 RID: 6416 RVA: 0x0008B2DC File Offset: 0x000894DC
		protected override PushNotificationsCrimsonEvent RequestedEvent
		{
			get
			{
				return PushNotificationsCrimsonEvents.SubscriptionRequested;
			}
		}
	}
}
