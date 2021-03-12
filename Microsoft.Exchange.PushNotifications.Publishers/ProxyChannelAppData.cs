using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.PushNotifications.Client;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;
using Microsoft.Exchange.PushNotifications.Extensions;
using Microsoft.Exchange.Security.OAuth;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000BE RID: 190
	internal class ProxyChannelAppData : PushNotificationChannel<ProxyNotification>
	{
		// Token: 0x06000643 RID: 1603 RVA: 0x00013FE0 File Offset: 0x000121E0
		public ProxyChannelAppData(ProxyChannelSettings settings, ITracer tracer, AzureAppConfigDataServiceProxy onPremAppDataClient = null, ProxyErrorTracker errorTracker = null, ADConfigurationManager configManager = null) : base(settings.AppId, tracer)
		{
			ArgumentValidator.ThrowIfNull("settings", settings);
			this.State = ProxyChannelAppDataState.Init;
			this.Settings = settings;
			if (onPremAppDataClient == null)
			{
				OAuthCredentials oauthCredentialsForAppToken = OAuthCredentials.GetOAuthCredentialsForAppToken(OrganizationId.ForestWideOrgId, this.Settings.Organization);
				oauthCredentialsForAppToken.Tracer = new PushNotificationsOutboundTracer(string.Format("{0} ({1})", settings.AppId, Guid.NewGuid()));
				this.ProxyClient = new AzureAppConfigDataServiceProxy(settings.ServiceUri, oauthCredentialsForAppToken);
			}
			else
			{
				this.ProxyClient = onPremAppDataClient;
			}
			this.ErrorTracker = (errorTracker ?? new ProxyErrorTracker(this.Settings.PublishRetryMax, this.Settings.BackOffTimeInSeconds, this.Settings.PublishRetryDelay));
			this.ConfigManager = (configManager ?? new ADConfigurationManager(DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 123, ".ctor", "f:\\15.00.1497\\sources\\dev\\PushNotifications\\src\\publishers\\Proxy\\ProxyChannelAppData.cs")));
			this.IdleChannelUntil = ((this.Settings.LastUpdated != null) ? this.Settings.LastUpdated.Value.AddHours(24.0) : DateTime.UtcNow);
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x00014112 File Offset: 0x00012312
		// (set) Token: 0x06000645 RID: 1605 RVA: 0x0001411A File Offset: 0x0001231A
		public ProxyChannelAppDataState State { get; private set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x00014123 File Offset: 0x00012323
		// (set) Token: 0x06000647 RID: 1607 RVA: 0x0001412B File Offset: 0x0001232B
		private ProxyChannelSettings Settings { get; set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x00014134 File Offset: 0x00012334
		// (set) Token: 0x06000649 RID: 1609 RVA: 0x0001413C File Offset: 0x0001233C
		private AzureAppConfigDataServiceProxy ProxyClient { get; set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x00014145 File Offset: 0x00012345
		// (set) Token: 0x0600064B RID: 1611 RVA: 0x0001414D File Offset: 0x0001234D
		private ProxyErrorTracker ErrorTracker { get; set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x00014156 File Offset: 0x00012356
		// (set) Token: 0x0600064D RID: 1613 RVA: 0x0001415E File Offset: 0x0001235E
		private AzureAppConfigResponseInfo ResponseInfo { get; set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x00014167 File Offset: 0x00012367
		// (set) Token: 0x0600064F RID: 1615 RVA: 0x0001416F File Offset: 0x0001236F
		private ADConfigurationManager ConfigManager { get; set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x00014178 File Offset: 0x00012378
		// (set) Token: 0x06000651 RID: 1617 RVA: 0x00014180 File Offset: 0x00012380
		private DateTime IdleChannelUntil { get; set; }

		// Token: 0x06000652 RID: 1618 RVA: 0x0001418C File Offset: 0x0001238C
		public override void Send(ProxyNotification notification, CancellationToken cancelToken)
		{
			base.CheckDisposed();
			ArgumentValidator.ThrowIfNull("notification", notification);
			PushNotificationChannelContext<ProxyNotification> pushNotificationChannelContext = new PushNotificationChannelContext<ProxyNotification>(notification, cancelToken, base.Tracer);
			ProxyChannelAppDataState proxyChannelAppDataState = this.State;
			while (pushNotificationChannelContext.IsActive)
			{
				this.CheckCancellation(pushNotificationChannelContext);
				switch (this.State)
				{
				case ProxyChannelAppDataState.Init:
					proxyChannelAppDataState = this.ProcessInit(pushNotificationChannelContext);
					break;
				case ProxyChannelAppDataState.AppDataRequesting:
					proxyChannelAppDataState = this.ProcessAppDataRequest(pushNotificationChannelContext);
					break;
				case ProxyChannelAppDataState.AppDataUpdating:
					proxyChannelAppDataState = this.UpdateAppDataRequest(pushNotificationChannelContext);
					break;
				case ProxyChannelAppDataState.Discarding:
					proxyChannelAppDataState = this.ProcessDiscarding(pushNotificationChannelContext);
					break;
				case ProxyChannelAppDataState.Updated:
					proxyChannelAppDataState = this.ProcessUpdated(pushNotificationChannelContext);
					break;
				default:
					pushNotificationChannelContext.Drop(null);
					proxyChannelAppDataState = ProxyChannelAppDataState.Init;
					break;
				}
				base.Tracer.TraceDebug<ProxyChannelAppDataState, ProxyChannelAppDataState>((long)this.GetHashCode(), "[Send] Transitioning from {0} to {1}", this.State, proxyChannelAppDataState);
				this.State = proxyChannelAppDataState;
			}
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00014259 File Offset: 0x00012459
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				base.Tracer.TraceDebug<string>((long)this.GetHashCode(), "[InternalDispose] Disposing the channel for '{0}'", base.AppId);
				this.ProxyClient.Dispose();
			}
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00014286 File Offset: 0x00012486
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ProxyChannelAppData>(this);
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00014290 File Offset: 0x00012490
		private ProxyChannelAppDataState ProcessInit(PushNotificationChannelContext<ProxyNotification> currentNotification)
		{
			base.Tracer.TraceDebug((long)this.GetHashCode(), "[ProcessInit] PushNotificationChannelContext<ProxyNotification> in ProcessInit");
			this.ResponseInfo = null;
			if (DateTime.UtcNow < this.IdleChannelUntil)
			{
				currentNotification.Drop(Strings.NotificationDroppedDueToLastUpdateTime(this.IdleChannelUntil.ToString()));
				return ProxyChannelAppDataState.Init;
			}
			return ProxyChannelAppDataState.AppDataRequesting;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00014304 File Offset: 0x00012504
		private ProxyChannelAppDataState ProcessAppDataRequest(PushNotificationChannelContext<ProxyNotification> currentNotification)
		{
			base.Tracer.TraceDebug<PushNotificationChannelContext<ProxyNotification>>((long)this.GetHashCode(), "[ProcessAppDataRequest] Sending notification '{0}'", currentNotification);
			string text = null;
			try
			{
				AverageTimeCounterBase averageTimeCounterBase = new AverageTimeCounterBase(ProxyCounters.AveragePublishingTime, ProxyCounters.AveragePublishingTimeBase);
				averageTimeCounterBase.Start();
				AzureAppConfigRequestInfo requestConfig = new AzureAppConfigRequestInfo((from x in currentNotification.Notification.AzureSettings
				select x.AppId).ToArray<string>());
				IAsyncResult asyncResult = this.ProxyClient.BeginGetAppConfigData(requestConfig, null, null);
				ICancelableAsyncResult cancelableAsyncResult = asyncResult as ICancelableAsyncResult;
				bool flag = base.WaitUntilDoneOrCancelled(cancelableAsyncResult, currentNotification, 500);
				this.ResponseInfo = this.ProxyClient.EndGetAppConfigData(cancelableAsyncResult);
				if (flag)
				{
					if (PushNotificationsCrimsonEvents.ProxyAppDataResponse.IsEnabled(PushNotificationsCrimsonEvent.Provider))
					{
						PushNotificationsCrimsonEvents.ProxyAppDataResponse.Log<string, string>(base.AppId, (this.ResponseInfo != null) ? this.ResponseInfo.ToFullString() : "null");
					}
					return ProxyChannelAppDataState.AppDataUpdating;
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
					if (faultContract.CanRetry)
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
				base.Tracer.TraceError<string>((long)this.GetHashCode(), "[ProcessAppDataRequest] An Exception was reported back from the service: {0}", text);
				PushNotificationsCrimsonEvents.ProxyGetAppDataError.Log<string, string, PushNotificationChannelContext<ProxyNotification>, string, string>(base.AppId, currentNotification.Notification.Identifier, currentNotification, this.ProxyClient.ProxyRequest.ToNullableString((PushNotificationProxyRequest x) => x.ToTraceString()), text);
				currentNotification.Drop(text);
				if (this.ShouldBackOff(text))
				{
					base.Tracer.TraceDebug<ExDateTime>((long)this.GetHashCode(), "[ProcessAppDataRequest] Will back off publishing notification for: {0}", this.ErrorTracker.BackOffEndTime);
					return ProxyChannelAppDataState.Discarding;
				}
			}
			return ProxyChannelAppDataState.Init;
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0001458C File Offset: 0x0001278C
		private ProxyChannelAppDataState UpdateAppDataRequest(PushNotificationChannelContext<ProxyNotification> currentNotification)
		{
			base.Tracer.TraceDebug<PushNotificationChannelContext<ProxyNotification>>((long)this.GetHashCode(), "[UpdateAppDataRequest] Sending notification '{0}'", currentNotification);
			string text = null;
			try
			{
				Dictionary<string, AzureAppConfigData> dictionary = new Dictionary<string, AzureAppConfigData>();
				if (this.ResponseInfo != null && this.ResponseInfo.AppData != null)
				{
					foreach (AzureAppConfigData azureAppConfigData in this.ResponseInfo.AppData)
					{
						dictionary.Add(azureAppConfigData.AppId, azureAppConfigData);
					}
				}
				PushNotificationApp pushNotificationApp = null;
				foreach (PushNotificationApp pushNotificationApp2 in this.ConfigManager.GetAllPushNotficationApps())
				{
					if (!string.IsNullOrWhiteSpace(pushNotificationApp2.Name) && dictionary.ContainsKey(pushNotificationApp2.Name))
					{
						AzureAppConfigData azureAppConfigData2 = dictionary[pushNotificationApp2.Name];
						if (azureAppConfigData2.Partition != null && !azureAppConfigData2.Partition.Equals(pushNotificationApp2.PartitionName))
						{
							pushNotificationApp2.PartitionName = azureAppConfigData2.Partition;
						}
						if (!string.IsNullOrWhiteSpace(azureAppConfigData2.SerializedToken))
						{
							pushNotificationApp2.AuthenticationKey = azureAppConfigData2.SerializedToken;
						}
						this.ConfigManager.Save(pushNotificationApp2);
					}
					if (PushNotificationCannedApp.OnPremProxy.Name.Equals(pushNotificationApp2.Name))
					{
						pushNotificationApp = pushNotificationApp2;
					}
				}
				if (pushNotificationApp != null)
				{
					pushNotificationApp.LastUpdateTimeUtc = new DateTime?(DateTime.UtcNow);
					if (this.ResponseInfo != null && !string.IsNullOrEmpty(this.ResponseInfo.HubName) && !this.ResponseInfo.HubName.Equals(pushNotificationApp.AuthenticationId))
					{
						pushNotificationApp.AuthenticationId = this.ResponseInfo.HubName;
					}
					this.ConfigManager.Save(pushNotificationApp);
					PushNotificationTracker.ReportSent(currentNotification.Notification, PushNotificationPlatform.None);
					currentNotification.Done();
					this.ErrorTracker.ReportSuccess();
					return ProxyChannelAppDataState.Updated;
				}
				this.ErrorTracker.ReportError(ProxyErrorType.Transient);
				text = Strings.CannotResolveProxyAppFromAD;
			}
			catch (ADTransientException exception)
			{
				text = exception.ToTraceString();
				this.ErrorTracker.ReportError(ProxyErrorType.Transient);
			}
			catch (ADOperationException exception2)
			{
				text = exception2.ToTraceString();
				this.ErrorTracker.ReportError(ProxyErrorType.Permanent);
			}
			PushNotificationsCrimsonEvents.ProxyAppDataUpdateError.Log<string, string, string>(base.AppId, currentNotification.Notification.ToFullString(), text);
			currentNotification.Drop(text);
			if (this.ShouldBackOff(text))
			{
				return ProxyChannelAppDataState.Discarding;
			}
			return ProxyChannelAppDataState.Init;
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0001482C File Offset: 0x00012A2C
		private ProxyChannelAppDataState ProcessDiscarding(PushNotificationChannelContext<ProxyNotification> currentNotification)
		{
			if (this.ErrorTracker.ShouldBackOff)
			{
				currentNotification.Drop(null);
				return ProxyChannelAppDataState.Discarding;
			}
			this.ErrorTracker.Reset();
			return ProxyChannelAppDataState.AppDataRequesting;
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00014850 File Offset: 0x00012A50
		private ProxyChannelAppDataState ProcessUpdated(PushNotificationChannelContext<ProxyNotification> currentNotification)
		{
			base.Tracer.TraceDebug((long)this.GetHashCode(), "[ProcessUpdated] PushNotificationChannelContext<ProxyNotification> in ProcessInit");
			currentNotification.Drop(null);
			return ProxyChannelAppDataState.Updated;
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00014874 File Offset: 0x00012A74
		private bool ShouldBackOff(string traces)
		{
			if (this.ErrorTracker.ShouldBackOff)
			{
				base.Tracer.TraceError<ExDateTime>((long)this.GetHashCode(), "[ShouldBackOff] Backing off because of notification errors until {0}", this.ErrorTracker.BackOffEndTime);
				PushNotificationsCrimsonEvents.ProxyChannelTransitionToDiscarding.Log<string, ExDateTime>(base.AppId, this.ErrorTracker.BackOffEndTime);
				return true;
			}
			return false;
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x000148CE File Offset: 0x00012ACE
		private void CheckCancellation(PushNotificationChannelContext<ProxyNotification> currentNotification)
		{
			if (currentNotification.IsCancelled)
			{
				base.Tracer.TraceDebug<ProxyChannelAppDataState>((long)this.GetHashCode(), "[CheckCancellation] Cancellation requested. Current state is {0}", this.State);
				throw new OperationCanceledException();
			}
		}

		// Token: 0x04000325 RID: 805
		private const int BaseIdleNumberOfHours = 24;
	}
}
