using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000040 RID: 64
	internal abstract class AzureBaseChannel<TNotif> : PushNotificationChannel<TNotif> where TNotif : PushNotification
	{
		// Token: 0x06000264 RID: 612 RVA: 0x00008BC6 File Offset: 0x00006DC6
		public AzureBaseChannel(string appId, ITracer tracer, AzureClient azureClient = null, EventHandler<MissingHubEventArgs> missingHubHandler = null) : base(appId, tracer)
		{
			this.MissingHubDetected = missingHubHandler;
			this.AzureClient = (azureClient ?? new AzureClient(new HttpClient()));
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000265 RID: 613 RVA: 0x00008BF0 File Offset: 0x00006DF0
		// (remove) Token: 0x06000266 RID: 614 RVA: 0x00008C28 File Offset: 0x00006E28
		private event EventHandler<MissingHubEventArgs> MissingHubDetected;

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000267 RID: 615 RVA: 0x00008C5D File Offset: 0x00006E5D
		// (set) Token: 0x06000268 RID: 616 RVA: 0x00008C65 File Offset: 0x00006E65
		private protected AzureClient AzureClient { protected get; private set; }

		// Token: 0x06000269 RID: 617 RVA: 0x00008C70 File Offset: 0x00006E70
		protected virtual void FireMissingHubEvent(string targetAppId, string hubName)
		{
			if (this.MissingHubDetected != null)
			{
				this.MissingHubDetected(this, new MissingHubEventArgs(targetAppId, hubName));
			}
			PushNotificationsCrimsonEvents.MissingAzureHub.LogPeriodic<string, string, bool>(hubName, CrimsonConstants.DefaultLogPeriodicSuppressionInMinutes, base.AppId, hubName, this.MissingHubDetected != null);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00008CBC File Offset: 0x00006EBC
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				base.Tracer.TraceDebug<string>((long)this.GetHashCode(), "[InternalDispose] Disposing the channel for '{0}'", base.AppId);
				this.AzureClient.Dispose();
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00008CEC File Offset: 0x00006EEC
		protected virtual AzureSasToken CreateAzureSasToken(IAzureSasTokenProvider sasTokenProvider, string targetResourceUri)
		{
			ArgumentValidator.ThrowIfNull("sasTokenProvider", sasTokenProvider);
			AzureSasToken azureSasToken = sasTokenProvider.CreateSasToken(targetResourceUri);
			if (azureSasToken == null || !azureSasToken.IsValid())
			{
				PushNotificationsCrimsonEvents.InvalidAzureSasToken.LogPeriodic<string, string>(base.AppId, CrimsonConstants.DefaultLogPeriodicSuppressionInMinutes, base.AppId, azureSasToken.ToNullableString(null));
				throw new PushNotificationPermanentException(Strings.CannotCreateValidSasToken(base.AppId, azureSasToken.ToNullableString(null)));
			}
			return azureSasToken;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00008D54 File Offset: 0x00006F54
		protected void LogAzureResponse(PushNotificationChannelContext<TNotif> notification, AzureResponse response)
		{
			if (base.Tracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ITracer tracer = base.Tracer;
				long id = (long)this.GetHashCode();
				string formatString = "[LogAzureResponse] Azure Response {0} {1} '{2}'";
				string appId = base.AppId;
				TNotif notification2 = notification.Notification;
				tracer.TraceDebug<string, string, string>(id, formatString, appId, notification2.Identifier, response.ToTraceString());
			}
			if (PushNotificationsCrimsonEvents.AzureNotificationResponse.IsEnabled(PushNotificationsCrimsonEvent.Provider))
			{
				AzureNotificationResponseEvent azureNotificationResponse = PushNotificationsCrimsonEvents.AzureNotificationResponse;
				string appId2 = base.AppId;
				TNotif notification3 = notification.Notification;
				azureNotificationResponse.Log<string, string, string>(appId2, notification3.Identifier, response.ToTraceString());
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00008DE4 File Offset: 0x00006FE4
		protected void LogAzureRequest(PushNotificationChannelContext<TNotif> notification, AzureRequestBase request)
		{
			if (base.Tracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ITracer tracer = base.Tracer;
				long id = (long)this.GetHashCode();
				string formatString = "[LogAzureRequest] Azure Request {0} {1} '{2}'";
				string appId = base.AppId;
				TNotif notification2 = notification.Notification;
				tracer.TraceDebug<string, string, string>(id, formatString, appId, notification2.Identifier, request.ToTraceString());
			}
			if (PushNotificationsCrimsonEvents.AzureNotificationRequest.IsEnabled(PushNotificationsCrimsonEvent.Provider))
			{
				AzureNotificationRequestEvent azureNotificationRequest = PushNotificationsCrimsonEvents.AzureNotificationRequest;
				string appId2 = base.AppId;
				TNotif notification3 = notification.Notification;
				azureNotificationRequest.Log<string, string, string>(appId2, notification3.Identifier, request.ToTraceString());
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00008E74 File Offset: 0x00007074
		protected void LogError(string traceTemplate, TNotif notification, AzureRequestBase request, AzureResponse response, Func<object, TimeSpan, string, string, string, string, string, bool> log)
		{
			if (base.Tracer.IsTraceEnabled(TraceType.ErrorTrace))
			{
				string arg = string.Format(traceTemplate, notification.ToFullString(), request.ToTraceString(), response.ToTraceString());
				base.Tracer.TraceError((long)this.GetHashCode(), string.Format("AppId:{0}, Trace:{1}", base.AppId, arg));
			}
			log(notification.RecipientId, CrimsonConstants.DefaultLogPeriodicSuppressionInMinutes, base.AppId, notification.Identifier, notification.ToFullString(), request.ToTraceString(), response.ToTraceString());
		}
	}
}
