using System;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000CB RID: 203
	internal sealed class WebAppChannel : PushNotificationChannel<WebAppNotification>
	{
		// Token: 0x060006B5 RID: 1717 RVA: 0x000155F0 File Offset: 0x000137F0
		public WebAppChannel(WebAppChannelSettings settings, ITracer tracer, WebAppErrorTracker errorTracker = null) : base((settings != null) ? settings.AppId : string.Empty, tracer)
		{
			ArgumentValidator.ThrowIfNull("settings", settings);
			settings.Validate();
			this.State = WebAppChannelState.Sending;
			this.Settings = settings;
			this.ErrorTracker = (errorTracker ?? new WebAppErrorTracker(this.Settings.BackOffTimeInSeconds));
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x0001564E File Offset: 0x0001384E
		// (set) Token: 0x060006B7 RID: 1719 RVA: 0x00015656 File Offset: 0x00013856
		public WebAppChannelState State { get; private set; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x0001565F File Offset: 0x0001385F
		// (set) Token: 0x060006B9 RID: 1721 RVA: 0x00015667 File Offset: 0x00013867
		private WebAppChannelSettings Settings { get; set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x00015670 File Offset: 0x00013870
		// (set) Token: 0x060006BB RID: 1723 RVA: 0x00015678 File Offset: 0x00013878
		private WebAppErrorTracker ErrorTracker { get; set; }

		// Token: 0x060006BC RID: 1724 RVA: 0x00015684 File Offset: 0x00013884
		public override void Send(WebAppNotification notification, CancellationToken cancelToken)
		{
			base.CheckDisposed();
			ArgumentValidator.ThrowIfNull("notification", notification);
			if (!notification.IsValid)
			{
				this.OnInvalidNotificationFound(new InvalidNotificationEventArgs(notification, new InvalidPushNotificationException(notification.ValidationErrors[0])));
				return;
			}
			PushNotificationChannelContext<WebAppNotification> pushNotificationChannelContext = new PushNotificationChannelContext<WebAppNotification>(notification, cancelToken, base.Tracer);
			WebAppChannelState webAppChannelState = this.State;
			while (pushNotificationChannelContext.IsActive)
			{
				this.CheckCancellation(pushNotificationChannelContext);
				switch (this.State)
				{
				case WebAppChannelState.Sending:
					webAppChannelState = this.ProcessSending(pushNotificationChannelContext);
					break;
				case WebAppChannelState.Discarding:
					webAppChannelState = this.ProcessDiscarding(pushNotificationChannelContext);
					break;
				default:
					pushNotificationChannelContext.Drop(null);
					webAppChannelState = WebAppChannelState.Sending;
					break;
				}
				base.Tracer.TraceDebug<WebAppChannelState, WebAppChannelState>((long)this.GetHashCode(), "[Send] Transitioning from {0} to {1}", this.State, webAppChannelState);
				this.State = webAppChannelState;
			}
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00015747 File Offset: 0x00013947
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				base.Tracer.TraceDebug<string>((long)this.GetHashCode(), "[InternalDispose] Disposing the channel for '{0}'", base.AppId);
			}
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00015769 File Offset: 0x00013969
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<WebAppChannel>(this);
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x00015771 File Offset: 0x00013971
		private void CheckCancellation(PushNotificationChannelContext<WebAppNotification> currentNotification)
		{
			if (currentNotification.IsCancelled)
			{
				base.Tracer.TraceDebug<WebAppChannelState>((long)this.GetHashCode(), "[CheckCancellation] Cancellation requested. Current state is {0}", this.State);
				throw new OperationCanceledException();
			}
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x000157A0 File Offset: 0x000139A0
		private WebAppChannelState ProcessSending(PushNotificationChannelContext<WebAppNotification> currentNotification)
		{
			base.Tracer.TraceDebug<PushNotificationChannelContext<WebAppNotification>>((long)this.GetHashCode(), "[ProcessSending] Sending notification '{0}'", currentNotification);
			using (EsoRequest esoRequest = new EsoRequest(currentNotification.Notification.Action, "WebAppChannel", currentNotification.Notification.Payload))
			{
				esoRequest.Timeout = this.Settings.RequestTimeout;
				if (!currentNotification.Notification.IsMonitoring)
				{
					ICancelableAsyncResult asyncResult = esoRequest.BeginSend();
					bool flag = base.WaitUntilDoneOrCancelled(asyncResult, currentNotification, this.Settings.RequestStepTimeout);
					DownloadResult downloadResult = esoRequest.EndSend(asyncResult);
					if (flag)
					{
						if (downloadResult.IsSucceeded)
						{
							PushNotificationTracker.ReportSent(currentNotification.Notification, PushNotificationPlatform.None);
							currentNotification.Done();
							this.ErrorTracker.ReportSuccess();
						}
						else
						{
							string text = (downloadResult.Exception != null) ? downloadResult.Exception.ToTraceString() : string.Empty;
							PushNotificationsCrimsonEvents.WebAppChannelUnknownError.Log<string, string, string>(base.AppId, currentNotification.ToString(), text);
							this.ErrorTracker.ReportError(WebAppErrorType.Unknown);
							currentNotification.Drop(null);
							if (this.ErrorTracker.ShouldBackOff)
							{
								base.Tracer.TraceError<ExDateTime>((long)this.GetHashCode(), "[ProcessSending] Backing off because of notification errors until {0}", this.ErrorTracker.BackOffEndTime);
								PushNotificationsCrimsonEvents.WebAppChannelTransitionToDiscarding.Log<string, ExDateTime>(base.AppId, this.ErrorTracker.BackOffEndTime);
								PushNotificationsMonitoring.PublishFailureNotification("WebAppChannelBackOff", base.AppId, text);
								return WebAppChannelState.Discarding;
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
			return WebAppChannelState.Sending;
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00015948 File Offset: 0x00013B48
		private WebAppChannelState ProcessDiscarding(PushNotificationChannelContext<WebAppNotification> currentNotification)
		{
			if (this.ErrorTracker.ShouldBackOff)
			{
				currentNotification.Drop(null);
				return WebAppChannelState.Discarding;
			}
			this.ErrorTracker.Reset();
			return WebAppChannelState.Sending;
		}
	}
}
