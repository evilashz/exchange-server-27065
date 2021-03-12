using System;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200005C RID: 92
	internal class AzureChallengeRequestChannel : AzureBaseChannel<AzureChallengeRequestNotification>
	{
		// Token: 0x0600036D RID: 877 RVA: 0x0000BB6C File Offset: 0x00009D6C
		public AzureChallengeRequestChannel(AzureChallengeRequestChannelSettings settings, ITracer tracer, EventHandler<MissingHubEventArgs> missingHubHandler, AzureClient azureClient = null, AzureErrorTracker errorTracker = null) : base(settings.AppId, tracer, azureClient, missingHubHandler)
		{
			ArgumentValidator.ThrowIfNull("settings", settings);
			settings.Validate();
			this.State = AzureChallengeRequestChannelState.Init;
			this.Settings = settings;
			this.ErrorTracker = (errorTracker ?? new AzureErrorTracker(this.Settings.BackOffTimeInSeconds));
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0000BBC4 File Offset: 0x00009DC4
		// (set) Token: 0x0600036F RID: 879 RVA: 0x0000BBCC File Offset: 0x00009DCC
		public AzureChallengeRequestChannelState State { get; private set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0000BBD5 File Offset: 0x00009DD5
		// (set) Token: 0x06000371 RID: 881 RVA: 0x0000BBDD File Offset: 0x00009DDD
		private AzureChallengeRequestChannelSettings Settings { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000BBE6 File Offset: 0x00009DE6
		// (set) Token: 0x06000373 RID: 883 RVA: 0x0000BBEE File Offset: 0x00009DEE
		private AzureErrorTracker ErrorTracker { get; set; }

		// Token: 0x06000374 RID: 884 RVA: 0x0000BBF8 File Offset: 0x00009DF8
		public override void Send(AzureChallengeRequestNotification notification, CancellationToken cancelToken)
		{
			base.CheckDisposed();
			ArgumentValidator.ThrowIfNull("notification", notification);
			if (!notification.IsValid)
			{
				this.OnInvalidNotificationFound(new InvalidNotificationEventArgs(notification, new InvalidPushNotificationException(notification.ValidationErrors[0])));
				return;
			}
			PushNotificationChannelContext<AzureChallengeRequestNotification> pushNotificationChannelContext = new PushNotificationChannelContext<AzureChallengeRequestNotification>(notification, cancelToken, base.Tracer);
			AzureChallengeRequestChannelState azureChallengeRequestChannelState = this.State;
			while (pushNotificationChannelContext.IsActive)
			{
				this.CheckCancellation(pushNotificationChannelContext);
				switch (this.State)
				{
				case AzureChallengeRequestChannelState.Init:
					azureChallengeRequestChannelState = this.ProcessInit(pushNotificationChannelContext);
					break;
				case AzureChallengeRequestChannelState.Sending:
					azureChallengeRequestChannelState = this.ProcessSending(pushNotificationChannelContext);
					break;
				case AzureChallengeRequestChannelState.Discarding:
					azureChallengeRequestChannelState = this.ProcessDiscarding(pushNotificationChannelContext);
					break;
				default:
					pushNotificationChannelContext.Drop(null);
					azureChallengeRequestChannelState = AzureChallengeRequestChannelState.Sending;
					break;
				}
				base.Tracer.TraceDebug<AzureChallengeRequestChannelState, AzureChallengeRequestChannelState>((long)this.GetHashCode(), "[Send] Transitioning from {0} to {1}", this.State, azureChallengeRequestChannelState);
				this.State = azureChallengeRequestChannelState;
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000BCC9 File Offset: 0x00009EC9
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AzureChallengeRequestChannel>(this);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000BCD1 File Offset: 0x00009ED1
		private void CheckCancellation(PushNotificationChannelContext<AzureChallengeRequestNotification> currentNotification)
		{
			if (currentNotification.IsCancelled)
			{
				base.Tracer.TraceDebug<AzureChallengeRequestChannelState>((long)this.GetHashCode(), "[CheckCancellation] Cancellation requested. Current state is {0}", this.State);
				throw new OperationCanceledException();
			}
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000BCFE File Offset: 0x00009EFE
		private AzureChallengeRequestChannelState ProcessInit(PushNotificationChannelContext<AzureChallengeRequestNotification> currentNotification)
		{
			return AzureChallengeRequestChannelState.Sending;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000BD04 File Offset: 0x00009F04
		private AzureChallengeRequestChannelState ProcessDelaying(PushNotificationChannelContext<AzureChallengeRequestNotification> currentNotification)
		{
			base.Tracer.TraceDebug<PushNotificationChannelContext<AzureChallengeRequestNotification>, ExDateTime>((long)this.GetHashCode(), "[ProcessDelaying] Delaying notification {0} until {1} (UTC)", currentNotification, this.ErrorTracker.DelayEndTime);
			while (this.ErrorTracker.ShouldDelay && !currentNotification.IsCancelled)
			{
				this.ErrorTracker.ConsumeDelay(this.Settings.RequestStepTimeout);
			}
			return AzureChallengeRequestChannelState.Init;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000BD62 File Offset: 0x00009F62
		private AzureChallengeRequestChannelState ProcessDiscarding(PushNotificationChannelContext<AzureChallengeRequestNotification> currentNotification)
		{
			if (this.ErrorTracker.ShouldBackOff)
			{
				currentNotification.Drop(null);
				return AzureChallengeRequestChannelState.Discarding;
			}
			this.ErrorTracker.Reset();
			return AzureChallengeRequestChannelState.Init;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000BD88 File Offset: 0x00009F88
		private AzureChallengeRequestChannelState ProcessSending(PushNotificationChannelContext<AzureChallengeRequestNotification> currentNotification)
		{
			base.Tracer.TraceDebug<PushNotificationChannelContext<AzureChallengeRequestNotification>>((long)this.GetHashCode(), "[ProcessSending] Sending notification '{0}'", currentNotification);
			AzureUriTemplate uriTemplate = currentNotification.Notification.UriTemplate;
			string text = uriTemplate.CreateIssueRegistrationSecretStringUri(currentNotification.Notification.TargetAppId, currentNotification.Notification.HubName);
			using (AzureRegistrationChallengeRequest azureRegistrationChallengeRequest = new AzureRegistrationChallengeRequest(currentNotification.Notification, this.CreateAzureSasToken(currentNotification.Notification.AzureSasTokenProvider, text), text))
			{
				base.LogAzureRequest(currentNotification, azureRegistrationChallengeRequest);
				azureRegistrationChallengeRequest.Timeout = this.Settings.RequestTimeout;
				ICancelableAsyncResult asyncResult = base.AzureClient.BeginRegistrationChallengeRequest(azureRegistrationChallengeRequest);
				bool flag = base.WaitUntilDoneOrCancelled(asyncResult, currentNotification, this.Settings.RequestStepTimeout);
				AzureResponse azureResponse = base.AzureClient.EndRegistrationChallengeRequest(asyncResult);
				if (flag)
				{
					if (azureResponse.HasSucceeded)
					{
						base.LogAzureResponse(currentNotification, azureResponse);
						PushNotificationTracker.ReportSent(currentNotification.Notification, PushNotificationPlatform.AzureChallengeRequest);
						if (currentNotification.Notification.IsMonitoring)
						{
							PushNotificationsMonitoring.PublishSuccessNotification("ChallengeRequestProcessed", currentNotification.Notification.TargetAppId);
						}
						currentNotification.Done();
						this.ErrorTracker.ReportSuccess();
					}
					else
					{
						if (!string.IsNullOrEmpty(currentNotification.Notification.HubName) && azureResponse.OriginalStatusCode != null && azureResponse.OriginalStatusCode.Value == HttpStatusCode.NotFound)
						{
							this.FireMissingHubEvent(currentNotification.Notification.TargetAppId, currentNotification.Notification.HubName);
						}
						else
						{
							this.AnalyzeErrorResponse(azureRegistrationChallengeRequest, azureResponse, currentNotification);
						}
						currentNotification.Drop(azureResponse.ToTraceString());
						if (this.ErrorTracker.ShouldBackOff)
						{
							base.Tracer.TraceError<ExDateTime>((long)this.GetHashCode(), "[ProcessSending] Backing off because of notification errors until {0}", this.ErrorTracker.BackOffEndTime);
							PushNotificationsCrimsonEvents.AzureIssueChallengeChannelTransitionToDiscarding.Log<string, ExDateTime>(base.AppId, this.ErrorTracker.BackOffEndTime);
							PushNotificationsMonitoring.PublishFailureNotification("AzureChallengeRequestChannelBackOff", base.AppId, azureResponse.ToTraceString());
							return AzureChallengeRequestChannelState.Discarding;
						}
					}
				}
			}
			return AzureChallengeRequestChannelState.Init;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000BFA0 File Offset: 0x0000A1A0
		private void AnalyzeErrorResponse(AzureRequestBase request, AzureResponse response, PushNotificationChannelContext<AzureChallengeRequestNotification> currentNotification)
		{
			if (response.OriginalStatusCode == null)
			{
				this.ErrorTracker.ReportError(AzureErrorType.Unknown);
				base.LogError("[AnalyzeErrorResponse] An unexpected error occurred sending a notification to Azure Notification Hub for Notification: '{0}'. Request: {1}; Response: {2}.", currentNotification.Notification, request, response, new Func<object, TimeSpan, string, string, string, string, string, bool>(PushNotificationsCrimsonEvents.AzureIssueChallengeChannelUnknownError.LogPeriodic<string, string, string, string, string>));
				return;
			}
			HttpStatusCode value = response.OriginalStatusCode.Value;
			if (value <= HttpStatusCode.RequestEntityTooLarge)
			{
				switch (value)
				{
				case HttpStatusCode.BadRequest:
				case HttpStatusCode.Unauthorized:
				case HttpStatusCode.NotFound:
					this.ErrorTracker.ReportError(AzureErrorType.Permanent);
					base.LogError("[AnalyzeErrorResponse] Permanent Error; Notification: '{0}'; Request: {1}; Response: {2}.", currentNotification.Notification, request, response, new Func<object, TimeSpan, string, string, string, string, string, bool>(PushNotificationsCrimsonEvents.AzureIssueChallengeChannelPermanentError.LogPeriodic<string, string, string, string, string>));
					return;
				case HttpStatusCode.PaymentRequired:
					goto IL_F3;
				case HttpStatusCode.Forbidden:
					break;
				default:
					if (value != HttpStatusCode.RequestEntityTooLarge)
					{
						goto IL_F3;
					}
					break;
				}
			}
			else if (value != HttpStatusCode.InternalServerError && value != HttpStatusCode.ServiceUnavailable)
			{
				goto IL_F3;
			}
			this.ErrorTracker.ReportError(AzureErrorType.Transient);
			base.LogError("[AnalyzeErrorResponse] Transient Error; Notification: '{0}'; Request: {1}; Response: {2}.", currentNotification.Notification, request, response, new Func<object, TimeSpan, string, string, string, string, string, bool>(PushNotificationsCrimsonEvents.AzureIssueChallengeChannelTransientError.LogPeriodic<string, string, string, string, string>));
			return;
			IL_F3:
			this.ErrorTracker.ReportError(AzureErrorType.Unknown);
			base.LogError("[AnalyzeErrorResponse] Unknown Error; Notification: '{0}'; Request: {1}; Response: {2}.", currentNotification.Notification, request, response, new Func<object, TimeSpan, string, string, string, string, string, bool>(PushNotificationsCrimsonEvents.AzureIssueChallengeChannelUnknownError.LogPeriodic<string, string, string, string, string>));
		}
	}
}
