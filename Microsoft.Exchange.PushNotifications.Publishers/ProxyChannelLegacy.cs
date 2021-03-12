using System;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.PushNotifications.Client;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;
using Microsoft.Exchange.Security.OAuth;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000C0 RID: 192
	internal class ProxyChannelLegacy : PushNotificationChannel<ProxyNotification>
	{
		// Token: 0x0600065E RID: 1630 RVA: 0x000148FC File Offset: 0x00012AFC
		public ProxyChannelLegacy(ProxyChannelSettings settings, ITracer tracer, OnPremPublisherServiceProxy onPremClient = null, ProxyErrorTracker errorTracker = null) : base(settings.AppId, tracer)
		{
			ArgumentValidator.ThrowIfNull("settings", settings);
			this.State = ProxyChannelLegacyState.Init;
			this.Settings = settings;
			if (onPremClient == null)
			{
				OAuthCredentials oauthCredentialsForAppToken = OAuthCredentials.GetOAuthCredentialsForAppToken(OrganizationId.ForestWideOrgId, this.Settings.Organization);
				oauthCredentialsForAppToken.Tracer = new PushNotificationsOutboundTracer(string.Format("{0} ({1})", settings.AppId, Guid.NewGuid()));
				this.ProxyClient = new OnPremPublisherServiceProxy(settings.ServiceUri, oauthCredentialsForAppToken);
			}
			else
			{
				this.ProxyClient = onPremClient;
			}
			this.ServerBackOffTime = ExDateTime.MinValue;
			this.ErrorTracker = (errorTracker ?? new ProxyErrorTracker(this.Settings.PublishRetryMax, this.Settings.BackOffTimeInSeconds, this.Settings.PublishRetryDelay));
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x000149C5 File Offset: 0x00012BC5
		// (set) Token: 0x06000660 RID: 1632 RVA: 0x000149CD File Offset: 0x00012BCD
		public ProxyChannelLegacyState State { get; private set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x000149D6 File Offset: 0x00012BD6
		// (set) Token: 0x06000662 RID: 1634 RVA: 0x000149DE File Offset: 0x00012BDE
		private ProxyChannelSettings Settings { get; set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x000149E7 File Offset: 0x00012BE7
		// (set) Token: 0x06000664 RID: 1636 RVA: 0x000149EF File Offset: 0x00012BEF
		private OnPremPublisherServiceProxy ProxyClient { get; set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x000149F8 File Offset: 0x00012BF8
		// (set) Token: 0x06000666 RID: 1638 RVA: 0x00014A00 File Offset: 0x00012C00
		private ProxyErrorTracker ErrorTracker { get; set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x00014A09 File Offset: 0x00012C09
		// (set) Token: 0x06000668 RID: 1640 RVA: 0x00014A11 File Offset: 0x00012C11
		private ExDateTime ServerBackOffTime { get; set; }

		// Token: 0x06000669 RID: 1641 RVA: 0x00014A1C File Offset: 0x00012C1C
		public override void Send(ProxyNotification notification, CancellationToken cancelToken)
		{
			base.CheckDisposed();
			ArgumentValidator.ThrowIfNull("notification", notification);
			ProxyChannelLegacy.NotificationContext notificationContext = new ProxyChannelLegacy.NotificationContext(notification, cancelToken, base.Tracer);
			ProxyChannelLegacyState proxyChannelLegacyState = this.State;
			while (notificationContext.IsActive)
			{
				this.CheckCancellation(notificationContext);
				switch (this.State)
				{
				case ProxyChannelLegacyState.Init:
					proxyChannelLegacyState = this.ProcessInit(notificationContext);
					break;
				case ProxyChannelLegacyState.Delaying:
					proxyChannelLegacyState = this.ProcessDelaying(notificationContext);
					break;
				case ProxyChannelLegacyState.Publishing:
					proxyChannelLegacyState = this.ProcessPublishing(notificationContext);
					break;
				case ProxyChannelLegacyState.Discarding:
					proxyChannelLegacyState = this.ProcessDiscarding(notificationContext);
					break;
				default:
					notificationContext.Drop();
					proxyChannelLegacyState = ProxyChannelLegacyState.Init;
					break;
				}
				base.Tracer.TraceDebug<ProxyChannelLegacyState, ProxyChannelLegacyState>((long)this.GetHashCode(), "[Send] Transitioning from {0} to {1}", this.State, proxyChannelLegacyState);
				this.State = proxyChannelLegacyState;
			}
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00014AD7 File Offset: 0x00012CD7
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				base.Tracer.TraceDebug<string>((long)this.GetHashCode(), "[InternalDispose] Disposing the channel for '{0}'", base.AppId);
				this.ProxyClient.Dispose();
			}
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00014B04 File Offset: 0x00012D04
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ProxyChannelLegacy>(this);
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00014B0C File Offset: 0x00012D0C
		protected virtual bool ShouldBackOff(ExDateTime serverBackOffTime)
		{
			return this.ErrorTracker.ShouldBackOff || serverBackOffTime > ExDateTime.UtcNow;
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00014B28 File Offset: 0x00012D28
		protected virtual bool ShouldDelay()
		{
			return this.ErrorTracker.ShouldDelay;
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00014B35 File Offset: 0x00012D35
		private void CheckCancellation(ProxyChannelLegacy.NotificationContext currentNotification)
		{
			if (currentNotification.IsCancelled)
			{
				if (this.State == ProxyChannelLegacyState.Delaying)
				{
					this.State = ProxyChannelLegacyState.Init;
				}
				base.Tracer.TraceDebug<ProxyChannelLegacyState>((long)this.GetHashCode(), "[CheckCancellation] Cancellation requested. Current state is {0}", this.State);
				throw new OperationCanceledException();
			}
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00014B72 File Offset: 0x00012D72
		private ProxyChannelLegacyState ProcessInit(ProxyChannelLegacy.NotificationContext currentNotification)
		{
			base.Tracer.TraceDebug((long)this.GetHashCode(), "[ProcessInit] NotificationContext in ProcessInit");
			this.ServerBackOffTime = ExDateTime.MinValue;
			return ProxyChannelLegacyState.Publishing;
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00014B98 File Offset: 0x00012D98
		private ProxyChannelLegacyState ProcessPublishing(ProxyChannelLegacy.NotificationContext currentNotification)
		{
			base.Tracer.TraceDebug<ProxyChannelLegacy.NotificationContext>((long)this.GetHashCode(), "[ProcessSending] Sending notification '{0}'", currentNotification);
			string text = null;
			try
			{
				AverageTimeCounterBase averageTimeCounterBase = new AverageTimeCounterBase(ProxyCounters.AveragePublishingTime, ProxyCounters.AveragePublishingTimeBase);
				averageTimeCounterBase.Start();
				IAsyncResult asyncResult = this.ProxyClient.BeginPublishOnPremNotifications(currentNotification.Notification.NotificationBatch, null, null);
				ICancelableAsyncResult cancelableAsyncResult = asyncResult as ICancelableAsyncResult;
				bool flag = this.WaitUntilDoneOrCancelled(cancelableAsyncResult, currentNotification);
				this.ProxyClient.EndPublishOnPremNotifications(cancelableAsyncResult);
				if (flag)
				{
					PushNotificationTracker.ReportSent(currentNotification.Notification, PushNotificationPlatform.None);
					averageTimeCounterBase.Stop();
					currentNotification.Done();
					this.ErrorTracker.ReportSuccess();
				}
			}
			catch (PushNotificationServerException<PushNotificationFault> pushNotificationServerException)
			{
				if (pushNotificationServerException.FaultContract == null)
				{
					text = pushNotificationServerException.ToTraceString();
					this.ErrorTracker.ReportError(ProxyErrorType.Unknown);
				}
				else
				{
					PushNotificationFault faultContract = pushNotificationServerException.FaultContract;
					text = string.Format("OriginatingServer:{0},ServerException:{1};FaultException:{2}", faultContract.OriginatingServer, pushNotificationServerException.ToTraceString(), faultContract.ToFullString());
					if (faultContract.BackOffTimeInMilliseconds > 0)
					{
						this.ServerBackOffTime = ExDateTime.UtcNow.AddMilliseconds((double)Math.Min(3600000, faultContract.BackOffTimeInMilliseconds));
						PushNotificationsCrimsonEvents.ProxyServerRequestedBackOff.Log<ExDateTime, string>(this.ServerBackOffTime, faultContract.OriginatingServer);
					}
					else if (faultContract.CanRetry)
					{
						this.ErrorTracker.ReportError(ProxyErrorType.Transient);
					}
					else
					{
						this.ErrorTracker.ReportError(ProxyErrorType.Permanent);
					}
				}
			}
			catch (PushNotificationTransientException exception)
			{
				text = exception.ToTraceString();
				this.ErrorTracker.ReportError(ProxyErrorType.Transient);
			}
			catch (PushNotificationPermanentException exception2)
			{
				text = exception2.ToTraceString();
				this.ErrorTracker.ReportError(ProxyErrorType.Permanent);
			}
			catch (Exception exception3)
			{
				text = exception3.ToTraceString();
				this.ErrorTracker.ReportError(ProxyErrorType.Unknown);
			}
			if (text != null)
			{
				base.Tracer.TraceError<string>((long)this.GetHashCode(), "[ProcessSending] An Exception was reported back from the service: {0}", text);
				PushNotificationsCrimsonEvents.ProxyPublishingError.Log<string, ProxyChannelLegacy.NotificationContext, string>(base.AppId, currentNotification, text);
			}
			if (this.ShouldBackOff(this.ServerBackOffTime))
			{
				base.Tracer.TraceDebug<ExDateTime>((long)this.GetHashCode(), "[ProcessSending] Will back off publishing notification for: {0}", this.ErrorTracker.BackOffEndTime);
				return ProxyChannelLegacyState.Discarding;
			}
			if (this.ShouldDelay())
			{
				base.Tracer.TraceDebug<ExDateTime>((long)this.GetHashCode(), "[ProcessSending] Will delay notification for: {0}", this.ErrorTracker.DelayEndTime);
				return ProxyChannelLegacyState.Delaying;
			}
			return ProxyChannelLegacyState.Publishing;
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00014E00 File Offset: 0x00013000
		private ProxyChannelLegacyState ProcessDelaying(ProxyChannelLegacy.NotificationContext currentNotification)
		{
			base.Tracer.TraceDebug<ProxyChannelLegacy.NotificationContext, ExDateTime>((long)this.GetHashCode(), "[ProcessDelaying] Delaying notification {0} until {1} (UTC)", currentNotification, this.ErrorTracker.DelayEndTime);
			while (this.ShouldDelay() && !currentNotification.IsCancelled)
			{
				this.ErrorTracker.ConsumeDelay(this.Settings.PublishStepTimeout);
			}
			return ProxyChannelLegacyState.Publishing;
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00014E59 File Offset: 0x00013059
		private ProxyChannelLegacyState ProcessDiscarding(ProxyChannelLegacy.NotificationContext currentNotification)
		{
			if (this.ShouldBackOff(this.ServerBackOffTime))
			{
				currentNotification.Drop();
				return ProxyChannelLegacyState.Discarding;
			}
			this.ErrorTracker.Reset();
			return ProxyChannelLegacyState.Publishing;
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00014E80 File Offset: 0x00013080
		private bool WaitUntilDoneOrCancelled(ICancelableAsyncResult asyncResult, ProxyChannelLegacy.NotificationContext currentNotification)
		{
			int num = 0;
			while (!currentNotification.IsCancelled)
			{
				if (asyncResult.AsyncWaitHandle.WaitOne(this.Settings.PublishStepTimeout))
				{
					return true;
				}
				num++;
				if (num % 3 == 0)
				{
					base.Tracer.TraceDebug<int>((long)this.GetHashCode(), "[WaitUntilDoneOrCancelled] Still waiting for the operation to finish: '{0}'", num);
				}
			}
			base.Tracer.TraceDebug<string>((long)this.GetHashCode(), "[WaitUntilDoneOrCancelled] Current notification was cancelled: '{0}'", currentNotification.ToString());
			asyncResult.Cancel();
			return false;
		}

		// Token: 0x04000334 RID: 820
		public const int MaxServerBackOffTimeInMilliseconds = 3600000;

		// Token: 0x020000C1 RID: 193
		private class NotificationContext
		{
			// Token: 0x06000674 RID: 1652 RVA: 0x00014EF8 File Offset: 0x000130F8
			public NotificationContext(ProxyNotification notification, CancellationToken cancellationToken, ITracer tracer)
			{
				this.Notification = notification;
				this.CancellationToken = cancellationToken;
				this.Tracer = tracer;
			}

			// Token: 0x170001AE RID: 430
			// (get) Token: 0x06000675 RID: 1653 RVA: 0x00014F15 File Offset: 0x00013115
			// (set) Token: 0x06000676 RID: 1654 RVA: 0x00014F1D File Offset: 0x0001311D
			public ProxyNotification Notification { get; private set; }

			// Token: 0x170001AF RID: 431
			// (get) Token: 0x06000677 RID: 1655 RVA: 0x00014F26 File Offset: 0x00013126
			public bool IsActive
			{
				get
				{
					return this.Notification != null;
				}
			}

			// Token: 0x170001B0 RID: 432
			// (get) Token: 0x06000678 RID: 1656 RVA: 0x00014F34 File Offset: 0x00013134
			public bool IsCancelled
			{
				get
				{
					return this.CancellationToken.IsCancellationRequested;
				}
			}

			// Token: 0x170001B1 RID: 433
			// (get) Token: 0x06000679 RID: 1657 RVA: 0x00014F4F File Offset: 0x0001314F
			// (set) Token: 0x0600067A RID: 1658 RVA: 0x00014F57 File Offset: 0x00013157
			private CancellationToken CancellationToken { get; set; }

			// Token: 0x170001B2 RID: 434
			// (get) Token: 0x0600067B RID: 1659 RVA: 0x00014F60 File Offset: 0x00013160
			// (set) Token: 0x0600067C RID: 1660 RVA: 0x00014F68 File Offset: 0x00013168
			private ITracer Tracer { get; set; }

			// Token: 0x0600067D RID: 1661 RVA: 0x00014F71 File Offset: 0x00013171
			public void Done()
			{
				this.Tracer.TraceDebug<ProxyNotification>((long)this.GetHashCode(), "[Done] Done with notification '{0}'", this.Notification);
				this.Notification = null;
			}

			// Token: 0x0600067E RID: 1662 RVA: 0x00014F98 File Offset: 0x00013198
			public void Drop()
			{
				if (PushNotificationsCrimsonEvents.ProxyNotificationDiscarded.IsEnabled(PushNotificationsCrimsonEvent.Provider))
				{
					PushNotificationsCrimsonEvents.ProxyNotificationDiscarded.Log<string, string>(this.Notification.AppId, this.Notification.ToFullString());
				}
				this.Tracer.TraceWarning<ProxyNotification>((long)this.GetHashCode(), "[Drop] Dropping notification '{0}'", this.Notification);
				this.Notification = null;
			}

			// Token: 0x0600067F RID: 1663 RVA: 0x00014FFA File Offset: 0x000131FA
			public override string ToString()
			{
				return this.Notification.ToString();
			}
		}
	}
}
