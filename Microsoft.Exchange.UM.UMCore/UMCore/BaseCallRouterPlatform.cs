using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.TroubleshootingTool.Shared;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore.Exceptions;
using Microsoft.Exchange.UM.UMCore.OCS;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000246 RID: 582
	internal abstract class BaseCallRouterPlatform : DisposableBase
	{
		// Token: 0x0600110D RID: 4365 RVA: 0x0004C590 File Offset: 0x0004A790
		public BaseCallRouterPlatform(LocalizedString serviceName, LocalizedString serverName, UMADSettings config)
		{
			ValidateArgument.NotNull(serviceName, "ServiceName");
			ValidateArgument.NotNull(serverName, "ServerName");
			ValidateArgument.NotNull(config, "UMADSettings");
			this.serviceName = serviceName;
			this.serverName = serverName;
			this.config = config;
			switch (this.config.UMStartupMode)
			{
			case UMStartupMode.TCP:
				this.eventLogStringForMode = Strings.TCPOnly;
				this.eventLogStringForPorts = this.config.SipTcpListeningPort.ToString();
				break;
			case UMStartupMode.TLS:
				this.eventLogStringForMode = Strings.TLSOnly;
				this.eventLogStringForPorts = this.config.SipTlsListeningPort.ToString();
				break;
			case UMStartupMode.Dual:
				this.eventLogStringForMode = Strings.TCPnTLS;
				this.eventLogStringForPorts = Strings.Ports(this.config.SipTcpListeningPort, this.config.SipTlsListeningPort).ToString();
				break;
			default:
				throw new InvalidOperationException();
			}
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_CallRouterStartingMode, null, new object[]
			{
				this.eventLogStringForMode
			});
		}

		// Token: 0x0600110E RID: 4366
		public abstract void StartListening();

		// Token: 0x0600110F RID: 4367
		public abstract void StopListening();

		// Token: 0x06001110 RID: 4368
		public abstract void ChangeCertificate();

		// Token: 0x06001111 RID: 4369
		public abstract void SendPingAsync(PingInfo pingInfo, PingCompletedDelegate callBack);

		// Token: 0x06001112 RID: 4370 RVA: 0x0004C6BE File Offset: 0x0004A8BE
		protected static void SetCallRejectionCounters(bool successRedirect)
		{
			if (!successRedirect)
			{
				Util.IncrementCounter(CallRouterAvailabilityCounters.UMCallRouterCallsRejected);
			}
			Util.SetCounter(CallRouterAvailabilityCounters.RecentUMCallRouterCallsRejected, (long)BaseCallRouterPlatform.recentPercentageRejectedCalls.Update(successRedirect));
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x0004C6E3 File Offset: 0x0004A8E3
		protected void HandleMessageReceived(InfoMessage.PlatformMessageReceivedEventArgs e)
		{
			if (e.IsOptions)
			{
				this.HandleOptionsMessage(e);
				return;
			}
			this.HandleServiceRequest(e);
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x0004C6FC File Offset: 0x0004A8FC
		protected void HandleOptionsMessage(InfoMessage.PlatformMessageReceivedEventArgs e)
		{
			e.ResponseCode = 200;
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x0004C70C File Offset: 0x0004A90C
		protected void HandleServiceRequest(InfoMessage.PlatformMessageReceivedEventArgs e)
		{
			if (!CommonConstants.UseDataCenterCallRouting || string.IsNullOrEmpty(e.CallInfo.RemoteMatchedFQDN))
			{
				throw CallRejectedException.Create(Strings.InvalidRequest, CallEndingReason.InvalidRequest, null, new object[0]);
			}
			using (CafeRoutingContext cafeRoutingContext = new CafeRoutingContext(e.CallInfo, this.config))
			{
				RouterCallHandler.HandleServiceRequest(cafeRoutingContext);
				ExAssert.RetailAssert(cafeRoutingContext.RedirectUri != null, "Redirection Uri has not been set");
				e.ResponseCode = cafeRoutingContext.RedirectCode;
				e.ResponseContactUri = cafeRoutingContext.RedirectUri;
			}
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x0004C7AC File Offset: 0x0004A9AC
		protected void HandleLegacyLyncNotification(PlatformCallInfo callInfo, byte[] messageBody, UserNotificationEventContext notificationContext)
		{
			if (CommonConstants.UseDataCenterCallRouting || string.IsNullOrEmpty(callInfo.RemoteMatchedFQDN))
			{
				throw CallRejectedException.Create(Strings.InvalidRequest, CallEndingReason.InvalidRequest, null, new object[0]);
			}
			using (CafeRoutingContext cafeRoutingContext = new CafeRoutingContext(callInfo, this.config))
			{
				string text = UserNotificationEvent.ExtractEumProxyAddressFromXml(messageBody);
				notificationContext.User = text;
				EumAddress eumAddress = new EumAddress(ProxyAddress.Parse(text).AddressString);
				if (!eumAddress.IsSipExtension)
				{
					throw CallRejectedException.Create(Strings.InvalidRequest, CallEndingReason.InvalidSIPUris, "EumProxyAddress: {0}", new object[]
					{
						text
					});
				}
				IADRecipientLookup iadrecipientLookup = ADRecipientLookupFactory.CreateFromOrganizationId(OrganizationId.ForestWideOrgId, null);
				using (UMRecipient umrecipient = UMRecipient.Factory.FromADRecipient<UMRecipient>(iadrecipientLookup.LookupByUmAddress(text)))
				{
					if (umrecipient == null || umrecipient.ADRecipient.UMRecipientDialPlanId == null)
					{
						throw CallRejectedException.Create(Strings.InvalidRequest, CallEndingReason.MailboxIsNotUMEnabled, "User: {0}", new object[]
						{
							text
						});
					}
					if (umrecipient.RequiresLegacyRedirectForCallAnswering)
					{
						throw CallRejectedException.Create(Strings.InvalidRequest, CallEndingReason.NotificationNotSupportedForLegacyUser, "User: {0}", new object[]
						{
							text
						});
					}
					IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromADRecipient(umrecipient.ADRecipient);
					cafeRoutingContext.DialPlan = iadsystemConfigurationLookup.GetDialPlanFromId(umrecipient.ADRecipient.UMRecipientDialPlanId);
					if (cafeRoutingContext.DialPlan == null)
					{
						throw CallRejectedException.Create(Strings.InvalidRequest, CallEndingReason.MailboxIsNotUMEnabled, "Dial Plan not found for User: {0}", new object[]
						{
							text
						});
					}
					RedirectionTarget.ResultSet forCallAnsweringCall = RedirectionTarget.Instance.GetForCallAnsweringCall(umrecipient, cafeRoutingContext);
					notificationContext.Backend = forCallAnsweringCall;
					string fromUri = string.Format(CultureInfo.InvariantCulture, "sip:{0}", new object[]
					{
						eumAddress.Extension
					});
					this.SendServiceRequest(fromUri, forCallAnsweringCall, messageBody);
				}
			}
		}

		// Token: 0x06001117 RID: 4375
		protected abstract void SendServiceRequest(string fromUri, RedirectionTarget.ResultSet backendTarget, byte[] messageBody);

		// Token: 0x04000BAD RID: 2989
		protected LocalizedString serviceName;

		// Token: 0x04000BAE RID: 2990
		protected LocalizedString serverName;

		// Token: 0x04000BAF RID: 2991
		protected UMADSettings config;

		// Token: 0x04000BB0 RID: 2992
		protected volatile bool isPlatformEnabled;

		// Token: 0x04000BB1 RID: 2993
		protected string eventLogStringForPorts;

		// Token: 0x04000BB2 RID: 2994
		protected LocalizedString eventLogStringForMode;

		// Token: 0x04000BB3 RID: 2995
		private static PercentageBooleanSlidingCounter recentPercentageRejectedCalls = PercentageBooleanSlidingCounter.CreateFailureCounter(1000, TimeSpan.FromHours(1.0));
	}
}
