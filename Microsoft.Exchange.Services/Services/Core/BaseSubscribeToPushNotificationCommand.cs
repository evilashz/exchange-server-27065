using System;
using Microsoft.Exchange.Data.PushNotifications;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;
using Microsoft.Exchange.PushNotifications.Extensions;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002A6 RID: 678
	internal abstract class BaseSubscribeToPushNotificationCommand<RequestType> : SingleStepServiceCommand<RequestType, ServiceResultNone> where RequestType : BaseSubscribeToPushNotificationRequest
	{
		// Token: 0x0600121F RID: 4639 RVA: 0x00058E4C File Offset: 0x0005704C
		protected BaseSubscribeToPushNotificationCommand(CallContext callContext, RequestType request) : base(callContext, request)
		{
			this.PushNotificationSubscription = ((request != null) ? request.SubscriptionRequest : null);
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x00058E74 File Offset: 0x00057074
		internal override ServiceResult<ServiceResultNone> Execute()
		{
			string subscriptionId = this.GetSubscriptionId();
			MailboxSession session = null;
			ServiceResult<ServiceResultNone> result;
			try
			{
				session = base.MailboxIdentityMailboxSession;
				BaseSubscribeToPushNotificationCommand<SubscribeToPushNotificationRequest>.ValidateSubscription(this.ClassName, subscriptionId, this.PushNotificationSubscription, session, this.ArgumentNullEvent, this.InvalidNotificationTypeEvent);
				BaseSubscribeToPushNotificationCommand<SubscribeToPushNotificationRequest>.LogRequest(this.ClassName, this.RequestedEvent, this.PushNotificationSubscription, subscriptionId, session);
				this.InternalExecute(subscriptionId);
				result = new ServiceResult<ServiceResultNone>(new ServiceResultNone());
			}
			catch (Exception ex)
			{
				if (ex is SaveConflictException)
				{
					BaseSubscribeToPushNotificationCommand<SubscribeToPushNotificationRequest>.LogException(this.ClassName, PushNotificationsCrimsonEvents.SubscriptionKnownError, this.PushNotificationSubscription, subscriptionId, session, ex);
				}
				else if (!(ex is InvalidRequestException))
				{
					BaseSubscribeToPushNotificationCommand<SubscribeToPushNotificationRequest>.LogException(this.ClassName, this.UnexpectedErrorEvent, this.PushNotificationSubscription, subscriptionId, session, ex);
				}
				throw;
			}
			return result;
		}

		// Token: 0x06001221 RID: 4641
		protected abstract void InternalExecute(string subscriptionId);

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06001222 RID: 4642 RVA: 0x00058F38 File Offset: 0x00057138
		// (set) Token: 0x06001223 RID: 4643 RVA: 0x00058F40 File Offset: 0x00057140
		private protected PushNotificationSubscription PushNotificationSubscription { protected get; private set; }

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06001224 RID: 4644
		protected abstract string ClassName { get; }

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06001225 RID: 4645
		protected abstract PushNotificationsCrimsonEvent ArgumentNullEvent { get; }

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06001226 RID: 4646
		protected abstract PushNotificationsCrimsonEvent InvalidNotificationTypeEvent { get; }

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06001227 RID: 4647
		protected abstract PushNotificationsCrimsonEvent UnexpectedErrorEvent { get; }

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06001228 RID: 4648
		protected abstract PushNotificationsCrimsonEvent RequestedEvent { get; }

		// Token: 0x06001229 RID: 4649 RVA: 0x00058F4C File Offset: 0x0005714C
		private static void ValidateSubscription(string className, string subscriptionId, PushNotificationSubscription subscription, MailboxSession session, PushNotificationsCrimsonEvent argumentNullEvent, PushNotificationsCrimsonEvent invalidNotificationTypeEvent)
		{
			if (subscription == null || string.IsNullOrEmpty(subscription.AppId) || string.IsNullOrEmpty(subscription.DeviceNotificationId) || string.IsNullOrEmpty(subscription.DeviceNotificationType))
			{
				BaseSubscribeToPushNotificationCommand<RequestType>.LogException(className, argumentNullEvent, subscription, subscriptionId, session, null);
				throw new InvalidRequestException();
			}
			if (!subscription.Platform.SupportsSubscriptions())
			{
				BaseSubscribeToPushNotificationCommand<RequestType>.LogException(className, invalidNotificationTypeEvent, subscription, subscriptionId, session, null);
				throw new InvalidRequestException();
			}
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x00058FB4 File Offset: 0x000571B4
		private static void LogException(string className, PushNotificationsCrimsonEvent crimsonEvent, PushNotificationSubscription subscription, string subscriptionId, MailboxSession session, Exception ex)
		{
			string text = (subscription == null) ? null : subscription.AppId;
			string text2 = (subscription == null) ? null : subscription.DeviceNotificationId;
			string text3 = (subscription == null) ? null : subscription.DeviceNotificationType;
			string text4 = (session == null) ? string.Empty : session.MailboxGuid.ToString();
			string text5 = (ex != null) ? ex.ToTraceString() : string.Empty;
			crimsonEvent.LogPeriodicGeneric(subscriptionId, CrimsonConstants.DefaultLogPeriodicSuppressionInMinutes, new object[]
			{
				text,
				text2,
				text3,
				subscriptionId,
				text4,
				text5
			});
			ExTraceGlobals.NotificationsCallTracer.TraceError(0L, "{0}.Execute: Error: SubscriptionId={1}, AppId = {2}, DeviceNotificationId = {3}, DeviceNotificationType = {4}, MailboxGuid = {5}, Exception = {6}.", new object[]
			{
				className,
				subscriptionId,
				text,
				text2,
				text3,
				text4,
				text5
			});
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00059094 File Offset: 0x00057294
		private static void LogRequest(string className, PushNotificationsCrimsonEvent crimsonEvent, PushNotificationSubscription subscription, string subscriptionId, MailboxSession session)
		{
			crimsonEvent.LogGeneric(new object[]
			{
				subscription.AppId,
				subscription.DeviceNotificationId,
				subscription.DeviceNotificationType,
				subscriptionId,
				subscription.SubscriptionOption,
				session.MailboxGuid
			});
			ExTraceGlobals.NotificationsCallTracer.TraceDebug(0L, "{0}.Execute: Error: SubscriptionId={1} request processed for AppId = {2}, NotificationId = {3}, NotificationType = {4}, SubscriptionOption = {5}.", new object[]
			{
				className,
				subscriptionId,
				subscription.AppId,
				subscription.DeviceNotificationId,
				subscription.DeviceNotificationType,
				subscription.SubscriptionOption
			});
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x00059135 File Offset: 0x00057335
		private string GetSubscriptionId()
		{
			return PushNotificationSubscriptionItem.GenerateSubscriptionId(base.CallContext.OwaProtocol, base.CallContext.OwaDeviceId, base.CallContext.OwaDeviceType);
		}
	}
}
