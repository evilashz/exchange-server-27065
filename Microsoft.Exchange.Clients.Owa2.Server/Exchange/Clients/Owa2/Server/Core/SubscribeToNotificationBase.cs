using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200035C RID: 860
	internal abstract class SubscribeToNotificationBase : ServiceCommand<SubscriptionResponseData[]>
	{
		// Token: 0x06001BD3 RID: 7123 RVA: 0x0006B7D0 File Offset: 0x000699D0
		public SubscribeToNotificationBase(NotificationSubscribeJsonRequest request, CallContext callContext, SubscriptionData[] subscriptionData) : base(callContext)
		{
			if (subscriptionData == null)
			{
				throw new ArgumentNullException("Subscription data cannot be null");
			}
			this.request = request;
			this.subscriptionData = subscriptionData;
			OwsLogRegistry.Register(base.GetType().Name, typeof(SubscribeToNotificationMetadata), new Type[0]);
		}

		// Token: 0x06001BD4 RID: 7124 RVA: 0x0006B82C File Offset: 0x00069A2C
		protected override SubscriptionResponseData[] InternalExecute()
		{
			ExTraceGlobals.NotificationsCallTracer.TraceDebug(0L, "[SubscribeToNotificationBase.InternalExecute] Acquiring the UserContext.");
			Stopwatch stopwatch = Stopwatch.StartNew();
			IMailboxContext mailboxContext = UserContextManager.GetMailboxContext(CallContext.Current.HttpContext, CallContext.Current.EffectiveCaller, false);
			if (mailboxContext.NotificationManager == null)
			{
				throw new OwaInvalidOperationException("UserContext.MapiNotificationManager is null");
			}
			base.CallContext.ProtocolLog.Set(SubscribeToNotificationMetadata.UserContextLatency, stopwatch.ElapsedMilliseconds);
			base.CallContext.ProtocolLog.Set(SubscribeToNotificationMetadata.SubscriptionCount, this.subscriptionData.Length);
			if (this.request.Header.TimeZoneContext != null)
			{
				base.CallContext.ProtocolLog.Set(SubscribeToNotificationMetadata.RequestTimeZone, this.request.Header.TimeZoneContext.TimeZoneDefinition.ExTimeZone.DisplayName);
			}
			return this.InternalSubscribe(mailboxContext).ToArray();
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x0006B918 File Offset: 0x00069B18
		protected virtual void InternalCreateASubscription(IMailboxContext userContext, SubscriptionData subscription, bool remoteSubscription)
		{
			switch (subscription.Parameters.NotificationType)
			{
			case NotificationType.RowNotification:
				if (userContext.NotificationManager == null)
				{
					throw new OwaInvalidOperationException("User context does not have a valid NotificationManager. Can't subsribe to RowNotification.");
				}
				this.metricType = SubscribeToNotificationMetadata.RowNotificationLatency;
				userContext.NotificationManager.SubscribeToRowNotification(subscription.SubscriptionId, subscription.Parameters, this.request.Header.TimeZoneContext.TimeZoneDefinition.ExTimeZone, base.CallContext, remoteSubscription);
				return;
			case NotificationType.CalendarItemNotification:
				if (userContext.NotificationManager == null)
				{
					throw new OwaInvalidOperationException("User context does not have a valid NotificationManager. Can't subsribe to CalendarItenNotification.");
				}
				this.metricType = SubscribeToNotificationMetadata.CalendarItemNotificationLatency;
				userContext.NotificationManager.SubscribeToRowNotification(subscription.SubscriptionId, subscription.Parameters, this.request.Header.TimeZoneContext.TimeZoneDefinition.ExTimeZone, base.CallContext, remoteSubscription);
				return;
			default:
				ExTraceGlobals.NotificationsCallTracer.TraceWarning(0L, "[SubscribeToNotificationBase.InternalCreateASubscription] Unsupported subscription type provided.");
				return;
			}
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x0006BC30 File Offset: 0x00069E30
		private List<SubscriptionResponseData> InternalSubscribe(IMailboxContext userContext)
		{
			List<SubscriptionResponseData> list = new List<SubscriptionResponseData>();
			SubscriptionData[] array = this.subscriptionData;
			for (int i = 0; i < array.Length; i++)
			{
				SubscriptionData subscription = array[i];
				string subscriptionId = subscription.SubscriptionId;
				try
				{
					Stopwatch notificationLatency = Stopwatch.StartNew();
					SubscriptionResponseData response = new SubscriptionResponseData(subscriptionId, true);
					OwaDiagnostics.SendWatsonReportsForGrayExceptions(delegate()
					{
						if (subscription.Parameters == null)
						{
							throw new ArgumentNullException("Subscription data parameters cannot be null");
						}
						ExTraceGlobals.NotificationsCallTracer.TraceDebug(0L, string.Format("[SubscribeToNotificationBase.InternalCreateASubscription] Creating subscription of type {0}.", subscription.Parameters.NotificationType));
						this.metricType = SubscribeToNotificationMetadata.Other;
						NameValueCollection headers = this.CallContext.HttpContext.Request.Headers;
						bool flag = RemoteRequestProcessor.IsRemoteRequest(headers);
						if (flag && string.IsNullOrWhiteSpace(subscription.Parameters.ChannelId))
						{
							throw new OwaInvalidRequestException("ChannelId is null or empty. ChannelId is required for remote notification subscribe requests.");
						}
						this.InternalCreateASubscription(userContext, subscription, flag);
						if (flag)
						{
							bool subscriptionExists;
							RemoteNotificationManager.Instance.Subscribe(userContext.Key.ToString(), userContext.ExchangePrincipal.MailboxInfo.MailboxGuid.ToString(), subscription.SubscriptionId, subscription.Parameters.ChannelId, RemoteRequestProcessor.GetRequesterUserId(headers), subscription.Parameters.NotificationType, headers["X-OWA-Test-RemoteNotificationEndPoint"], out subscriptionExists);
							response.SubscriptionExists = subscriptionExists;
						}
						if (!this.latenciesPerNotificationType.ContainsKey(this.metricType))
						{
							this.latenciesPerNotificationType.Add(this.metricType, 0);
						}
						Dictionary<SubscribeToNotificationMetadata, int> dictionary;
						SubscribeToNotificationMetadata key;
						(dictionary = this.latenciesPerNotificationType)[key = this.metricType] = dictionary[key] + (int)notificationLatency.ElapsedMilliseconds;
					});
					list.Add(response);
				}
				catch (GrayException ex)
				{
					ExTraceGlobals.NotificationsCallTracer.TraceError<NotificationType, string, string>((long)this.GetHashCode(), "[SubscribeToNotificationBase.InternalSubscribe]: Exception thrown while creating subscription of type [{0}] with id {1}. Error: {2}", subscription.Parameters.NotificationType, subscriptionId, ex.ToString());
					OwaServerTraceLogger.AppendToLog(new TraceLogEvent("SubErr", userContext, "SubscribeToNotificationBase.InternalSubscribe", ex.ToString()));
					list.Add(new SubscriptionResponseData(subscriptionId, false)
					{
						ErrorInfo = ((ex.InnerException != null) ? ex.InnerException.Message : ex.Message)
					});
				}
			}
			foreach (KeyValuePair<SubscribeToNotificationMetadata, int> keyValuePair in this.latenciesPerNotificationType)
			{
				base.CallContext.ProtocolLog.Set(keyValuePair.Key, keyValuePair.Value);
			}
			return list;
		}

		// Token: 0x04000FCA RID: 4042
		private readonly SubscriptionData[] subscriptionData;

		// Token: 0x04000FCB RID: 4043
		protected readonly NotificationSubscribeJsonRequest request;

		// Token: 0x04000FCC RID: 4044
		protected Dictionary<SubscribeToNotificationMetadata, int> latenciesPerNotificationType = new Dictionary<SubscribeToNotificationMetadata, int>();

		// Token: 0x04000FCD RID: 4045
		protected SubscribeToNotificationMetadata metricType;
	}
}
