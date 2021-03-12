using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000DD RID: 221
	internal sealed class WnsChannel : PushNotificationChannel<WnsNotification>
	{
		// Token: 0x06000731 RID: 1841 RVA: 0x00016704 File Offset: 0x00014904
		public WnsChannel(WnsChannelSettings settings, ITracer tracer, WnsClient wnsClient = null, WnsErrorTracker errorTracker = null) : base(settings.AppId, tracer)
		{
			ArgumentValidator.ThrowIfNull("settings", settings);
			settings.Validate();
			this.State = WnsChannelState.Init;
			this.Settings = settings;
			this.WnsClient = (wnsClient ?? new WnsClient(new HttpClient()));
			this.ErrorTracker = (errorTracker ?? new WnsErrorTracker(this.Settings.AuthenticateRetryMax, this.Settings.AuthenticateRetryDelay, this.Settings.BackOffTimeInSeconds));
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x00016784 File Offset: 0x00014984
		// (set) Token: 0x06000733 RID: 1843 RVA: 0x0001678C File Offset: 0x0001498C
		public WnsChannelState State { get; private set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x00016795 File Offset: 0x00014995
		// (set) Token: 0x06000735 RID: 1845 RVA: 0x0001679D File Offset: 0x0001499D
		private WnsChannelSettings Settings { get; set; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x000167A6 File Offset: 0x000149A6
		// (set) Token: 0x06000737 RID: 1847 RVA: 0x000167AE File Offset: 0x000149AE
		private WnsClient WnsClient { get; set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x000167B7 File Offset: 0x000149B7
		// (set) Token: 0x06000739 RID: 1849 RVA: 0x000167BF File Offset: 0x000149BF
		private WnsErrorTracker ErrorTracker { get; set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x000167C8 File Offset: 0x000149C8
		// (set) Token: 0x0600073B RID: 1851 RVA: 0x000167D0 File Offset: 0x000149D0
		private WnsAccessToken AccessToken { get; set; }

		// Token: 0x0600073C RID: 1852 RVA: 0x000167DC File Offset: 0x000149DC
		public override void Send(WnsNotification notification, CancellationToken cancelToken)
		{
			base.CheckDisposed();
			ArgumentValidator.ThrowIfNull("notification", notification);
			if (!notification.IsValid)
			{
				this.OnInvalidNotificationFound(new InvalidNotificationEventArgs(notification, new InvalidPushNotificationException(notification.ValidationErrors[0])));
				return;
			}
			PushNotificationChannelContext<WnsNotification> pushNotificationChannelContext = new PushNotificationChannelContext<WnsNotification>(notification, cancelToken, base.Tracer);
			WnsChannelState wnsChannelState = this.State;
			while (pushNotificationChannelContext.IsActive)
			{
				this.CheckCancellation(pushNotificationChannelContext);
				switch (this.State)
				{
				case WnsChannelState.Init:
					wnsChannelState = this.ProcessInit(pushNotificationChannelContext);
					break;
				case WnsChannelState.Authenticating:
					wnsChannelState = this.ProcessAuthenticating(pushNotificationChannelContext);
					break;
				case WnsChannelState.Delaying:
					wnsChannelState = this.ProcessDelaying(pushNotificationChannelContext);
					break;
				case WnsChannelState.Sending:
					wnsChannelState = this.ProcessSending(pushNotificationChannelContext);
					break;
				case WnsChannelState.Discarding:
					wnsChannelState = this.ProcessDiscarding(pushNotificationChannelContext);
					break;
				default:
					pushNotificationChannelContext.Drop(this.State.ToString());
					wnsChannelState = WnsChannelState.Init;
					break;
				}
				base.Tracer.TraceDebug<WnsChannelState, WnsChannelState>((long)this.GetHashCode(), "[Send] Transitioning from {0} to {1}", this.State, wnsChannelState);
				this.State = wnsChannelState;
			}
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x000168DE File Offset: 0x00014ADE
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				base.Tracer.TraceDebug<string>((long)this.GetHashCode(), "[InternalDispose] Disposing the channel for '{0}'", base.AppId);
				this.WnsClient.Dispose();
			}
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0001690B File Offset: 0x00014B0B
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<WnsChannel>(this);
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00016913 File Offset: 0x00014B13
		private void CheckCancellation(PushNotificationChannelContext<WnsNotification> currentNotification)
		{
			if (currentNotification.IsCancelled)
			{
				if (this.State == WnsChannelState.Delaying)
				{
					this.State = WnsChannelState.Authenticating;
				}
				base.Tracer.TraceDebug<WnsChannelState>((long)this.GetHashCode(), "[CheckCancellation] Cancellation requested. Current state is {0}", this.State);
				throw new OperationCanceledException();
			}
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00016950 File Offset: 0x00014B50
		private WnsChannelState ProcessInit(PushNotificationChannelContext<WnsNotification> currentNotification)
		{
			if (this.AccessToken != null)
			{
				base.Tracer.TraceDebug((long)this.GetHashCode(), "[ProcessInit] Resetting the access token");
				this.AccessToken = null;
			}
			return WnsChannelState.Authenticating;
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0001697C File Offset: 0x00014B7C
		private WnsChannelState ProcessAuthenticating(PushNotificationChannelContext<WnsNotification> currentNotification)
		{
			base.Tracer.TraceDebug<string, Uri>((long)this.GetHashCode(), "[ProcessAuthenticating] Authenticating '{0}' with '{1}", this.Settings.AppSid, this.Settings.AuthenticationUri);
			WnsChannelState result;
			using (WnsAuthRequest wnsAuthRequest = new WnsAuthRequest(this.Settings.AuthenticationUri, this.Settings.AppSid, this.Settings.AppSecret))
			{
				wnsAuthRequest.Timeout = this.Settings.RequestTimeout;
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				ICancelableAsyncResult asyncResult = this.WnsClient.BeginSendAuthRequest(wnsAuthRequest);
				bool flag = base.WaitUntilDoneOrCancelled(asyncResult, currentNotification, this.Settings.RequestStepTimeout);
				WnsResult<WnsAccessToken> wnsResult = this.WnsClient.EndSendAuthRequest(asyncResult);
				stopwatch.Stop();
				if (flag)
				{
					if (wnsResult.Response != null)
					{
						this.AccessToken = wnsResult.Response;
						this.ErrorTracker.ReportAuthenticationSuccess();
						base.Tracer.TraceDebug((long)this.GetHashCode(), "[ProcessAuthenticating] Authentication succeeded");
						PushNotificationsCrimsonEvents.WnsChannelAuthenticationSucceeded.Log<string, long>(base.AppId, stopwatch.ElapsedMilliseconds);
						result = WnsChannelState.Sending;
					}
					else
					{
						string text = (wnsResult.Exception != null) ? wnsResult.Exception.ToTraceString() : "<null>";
						base.Tracer.TraceError<string>((long)this.GetHashCode(), "[ProcessAuthenticating] Authentication request failed: {0}", text);
						PushNotificationsCrimsonEvents.WnsChannelAuthenticationError.Log<string, long, string>(base.AppId, stopwatch.ElapsedMilliseconds, text);
						this.ErrorTracker.ReportAuthenticationFailure();
						if (this.ErrorTracker.ShouldBackOff)
						{
							base.Tracer.TraceError<ExDateTime>((long)this.GetHashCode(), "[ProcessAuthenticating] Backing off because of Authentication errors until {0}", this.ErrorTracker.BackOffEndTime);
							PushNotificationsCrimsonEvents.WnsChannelTransitionToDiscarding.Log<string, ExDateTime>(base.AppId, this.ErrorTracker.BackOffEndTime);
							this.RaiseBackOffMonitoringEvent(text);
							result = WnsChannelState.Discarding;
						}
						else
						{
							result = WnsChannelState.Delaying;
						}
					}
				}
				else
				{
					result = WnsChannelState.Authenticating;
				}
			}
			return result;
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00016B70 File Offset: 0x00014D70
		private WnsChannelState ProcessSending(PushNotificationChannelContext<WnsNotification> currentNotification)
		{
			base.Tracer.TraceDebug<PushNotificationChannelContext<WnsNotification>>((long)this.GetHashCode(), "[ProcessSending] Sending notification '{0}'", currentNotification);
			using (WnsRequest wnsRequest = currentNotification.Notification.CreateWnsRequest())
			{
				wnsRequest.Authorization = this.AccessToken.ToWnsAuthorizationString();
				wnsRequest.Timeout = this.Settings.RequestTimeout;
				if (!currentNotification.Notification.IsMonitoring)
				{
					ICancelableAsyncResult asyncResult = this.WnsClient.BeginSendNotificationRequest(wnsRequest);
					bool flag = base.WaitUntilDoneOrCancelled(asyncResult, currentNotification, this.Settings.RequestStepTimeout);
					WnsResult<WnsResponse> wnsResult = this.WnsClient.EndSendNotificationRequest(asyncResult);
					if (flag)
					{
						if (wnsResult.Response == WnsResponse.Succeeded)
						{
							base.Tracer.TraceDebug((long)this.GetHashCode(), string.Format("[ProcessSending] WnsResponse: Succeeded.", new object[0]));
							PushNotificationTracker.ReportSent(currentNotification.Notification, PushNotificationPlatform.None);
							currentNotification.Done();
							this.ErrorTracker.ReportWnsRequestSuccess();
						}
						else
						{
							base.Tracer.TraceDebug((long)this.GetHashCode(), string.Format("[ProcessSending] WnsResponse:{0}; Error:{1}", wnsResult.Response, wnsResult.Exception.ToTraceString()));
							string text;
							if (wnsResult.Response != null && wnsResult.Response.ResponseCode == HttpStatusCode.Unauthorized)
							{
								text = string.Format("[ProcessSending] Access token expired: {0}", wnsResult.Response);
								base.Tracer.TraceDebug((long)this.GetHashCode(), text);
								PushNotificationsCrimsonEvents.WnsChannelAccessTokenExpired.Log<string, int, WnsResponse>(base.AppId, this.AccessToken.GetUsageTimeInMinutes(), wnsResult.Response);
								this.ErrorTracker.ReportWnsRequestFailure(WnsResultErrorType.AuthTokenExpired);
								if (!this.ErrorTracker.ShouldBackOff)
								{
									return WnsChannelState.Init;
								}
								this.AccessToken = null;
							}
							else
							{
								text = this.AnalyzeWnsResultError(wnsResult, currentNotification);
							}
							currentNotification.Drop(text);
							if (this.ErrorTracker.ShouldBackOff)
							{
								base.Tracer.TraceError<ExDateTime>((long)this.GetHashCode(), "[ProcessSending] Backing off because of notification errors until {0}", this.ErrorTracker.BackOffEndTime);
								PushNotificationsCrimsonEvents.WnsChannelTransitionToDiscarding.Log<string, ExDateTime>(base.AppId, this.ErrorTracker.BackOffEndTime);
								this.RaiseBackOffMonitoringEvent(text);
								return WnsChannelState.Discarding;
							}
						}
					}
				}
				else
				{
					PushNotificationsMonitoring.PublishSuccessNotification("NotificationProcessed", base.AppId);
					currentNotification.Done();
				}
			}
			return WnsChannelState.Sending;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00016DC4 File Offset: 0x00014FC4
		private WnsChannelState ProcessDelaying(PushNotificationChannelContext<WnsNotification> currentNotification)
		{
			base.Tracer.TraceDebug<PushNotificationChannelContext<WnsNotification>, ExDateTime>((long)this.GetHashCode(), "[ProcessDelaying] Delaying notification {0} until {1} (UTC)", currentNotification, this.ErrorTracker.DelayEndTime);
			while (this.ErrorTracker.ShouldDelay && !currentNotification.IsCancelled)
			{
				this.ErrorTracker.ConsumeDelay(this.Settings.RequestStepTimeout);
			}
			return WnsChannelState.Authenticating;
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00016E22 File Offset: 0x00015022
		private WnsChannelState ProcessDiscarding(PushNotificationChannelContext<WnsNotification> currentNotification)
		{
			if (this.ErrorTracker.ShouldBackOff)
			{
				currentNotification.Drop(this.State.ToString());
				return WnsChannelState.Discarding;
			}
			this.ErrorTracker.Reset();
			if (this.AccessToken != null)
			{
				return WnsChannelState.Sending;
			}
			return WnsChannelState.Authenticating;
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x00016E60 File Offset: 0x00015060
		private string AnalyzeWnsResultError(WnsResult<WnsResponse> wnsResult, PushNotificationChannelContext<WnsNotification> currentNotification)
		{
			string text = string.Empty;
			if (wnsResult.Response == null)
			{
				string text2 = (wnsResult.Exception != null) ? wnsResult.Exception.ToTraceString() : string.Empty;
				text = string.Format("[AnalyzeWnsResultError] WNS request failed for notification {0}: {1}", currentNotification, text2);
				base.Tracer.TraceError((long)this.GetHashCode(), text);
				PushNotificationsCrimsonEvents.WnsChannelUnknownError.Log<string, PushNotificationChannelContext<WnsNotification>, string>(base.AppId, currentNotification, text2);
				this.ErrorTracker.ReportWnsRequestFailure(wnsResult.IsTimeout ? WnsResultErrorType.Timeout : WnsResultErrorType.Unknown);
				return text;
			}
			HttpStatusCode responseCode = wnsResult.Response.ResponseCode;
			if (responseCode <= HttpStatusCode.RequestEntityTooLarge)
			{
				switch (responseCode)
				{
				case HttpStatusCode.BadRequest:
				case HttpStatusCode.MethodNotAllowed:
					break;
				case HttpStatusCode.Unauthorized:
				case HttpStatusCode.PaymentRequired:
				case HttpStatusCode.ProxyAuthenticationRequired:
				case HttpStatusCode.RequestTimeout:
				case HttpStatusCode.Conflict:
					goto IL_229;
				case HttpStatusCode.Forbidden:
				case HttpStatusCode.NotFound:
				case HttpStatusCode.Gone:
				{
					InvalidPushNotificationException ex = new InvalidPushNotificationException(Strings.WnsChannelInvalidNotificationReported(wnsResult.Response.ToString()), wnsResult.Exception);
					text = ex.Message;
					this.OnInvalidNotificationFound(new InvalidNotificationEventArgs(currentNotification.Notification, ex));
					return text;
				}
				case HttpStatusCode.NotAcceptable:
					text = string.Format("[AnalyzeWnsResultError] WNS is throttling the channel: {0}", wnsResult.Response);
					base.Tracer.TraceError((long)this.GetHashCode(), text);
					PushNotificationsCrimsonEvents.WnsChannelThrottlingError.Log<string, WnsResponse>(base.AppId, wnsResult.Response);
					this.ErrorTracker.ReportWnsRequestFailure(WnsResultErrorType.Throttle);
					return text;
				default:
					if (responseCode != HttpStatusCode.RequestEntityTooLarge)
					{
						goto IL_229;
					}
					break;
				}
				text = string.Format("[AnalyzeWnsResultError] WNS reported the notification was built incorrectly: '{0}'. '{1}'", wnsResult.Response, currentNotification.Notification.ToFullString());
				base.Tracer.TraceError((long)this.GetHashCode(), text);
				PushNotificationsCrimsonEvents.WnsChannelMalformedNotification.Log<string, WnsResponse, string>(base.AppId, wnsResult.Response, currentNotification.Notification.ToFullString());
				return text;
			}
			if (responseCode == HttpStatusCode.InternalServerError || responseCode == HttpStatusCode.ServiceUnavailable)
			{
				text = string.Format("[AnalyzeWnsResultError] WNS reported a service error: {0}", wnsResult.Response);
				base.Tracer.TraceError((long)this.GetHashCode(), text);
				PushNotificationsCrimsonEvents.WnsChannelServiceError.LogPeriodic<string, WnsResponse>(wnsResult.Response.ResponseCode, CrimsonConstants.DefaultLogPeriodicSuppressionInMinutes, base.AppId, wnsResult.Response);
				this.ErrorTracker.ReportWnsRequestFailure(WnsResultErrorType.ServerUnavailable);
				return text;
			}
			IL_229:
			string text3 = wnsResult.Response.ToString();
			text = string.Format("[AnalyzeWnsResultError] WNS request failed for notification {0}: {1}", currentNotification, text3);
			base.Tracer.TraceError((long)this.GetHashCode(), text);
			PushNotificationsCrimsonEvents.WnsChannelUnknownError.Log<string, PushNotificationChannelContext<WnsNotification>, string>(base.AppId, currentNotification, text3);
			this.ErrorTracker.ReportWnsRequestFailure(WnsResultErrorType.Unknown);
			return text;
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x000170E1 File Offset: 0x000152E1
		private void RaiseBackOffMonitoringEvent(string traces = "")
		{
			PushNotificationsMonitoring.PublishFailureNotification("WnsChannelBackOff", base.AppId, traces);
		}
	}
}
