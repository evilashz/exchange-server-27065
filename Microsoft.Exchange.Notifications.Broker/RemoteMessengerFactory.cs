using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000034 RID: 52
	internal static class RemoteMessengerFactory
	{
		// Token: 0x0600020A RID: 522 RVA: 0x0000BDE1 File Offset: 0x00009FE1
		public static RemoteMessenger CreateForSubscribeRequest(BrokerSubscription subscription)
		{
			return RemoteMessengerFactory.CreateRemoteMessengerBasedOnSender(subscription, "Subscribe");
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000BDEE File Offset: 0x00009FEE
		public static RemoteMessenger CreateForUnsubscribeRequest(BrokerSubscription subscription)
		{
			return RemoteMessengerFactory.CreateRemoteMessengerBasedOnSender(subscription, "Unsubscribe");
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000BDFC File Offset: 0x00009FFC
		public static RemoteMessenger CreateForNotification(BrokerSubscription subscription)
		{
			if (!string.IsNullOrEmpty(subscription.Receiver.FrontEndUrl))
			{
				return new RemoteMessenger.CrossDeploymentRemoteMessenger(new Uri(subscription.Receiver.FrontEndUrl), subscription.Sender.OrganizationId, subscription.Receiver.MailboxSmtp, "DeliverNotificationBatch");
			}
			if (Guid.Empty != subscription.Receiver.MailboxGuid)
			{
				return new RemoteMessenger.BackendToBackendRemoteMessenger(RemoteConduit.FindBackEndServer(subscription.Receiver.MailboxGuid, subscription.Sender.OrganizationId), "DeliverNotificationBatch");
			}
			throw new InvalidBrokerSubscriptionException(string.Empty);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000BE94 File Offset: 0x0000A094
		public static void FixupReceiver(BrokerSubscription brokerSubscription, IGenerator generator)
		{
			NotificationParticipant sender = brokerSubscription.Sender;
			NotificationParticipant receiver = brokerSubscription.Receiver;
			if (receiver.LocationKind == NotificationParticipantLocationKind.LocalResourceForest && sender.LocationKind == NotificationParticipantLocationKind.LocalResourceForest)
			{
				return;
			}
			if (receiver.LocationKind == NotificationParticipantLocationKind.CrossPremise)
			{
				throw new NotSupportedException("receiver should be not in the other premise.");
			}
			receiver.FrontEndUrl = RemoteMessengerFactory.GetCurrentNotificationBrokerExternalUrl().ToString();
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000BEE8 File Offset: 0x0000A0E8
		public static void FixupSender(BrokerSubscription brokerSubscription, OrganizationId resolvedOrganizationId)
		{
			NotificationParticipant sender = brokerSubscription.Sender;
			if (resolvedOrganizationId != null && sender.MailboxGuid == Guid.Empty)
			{
				ADSessionSettings adSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(resolvedOrganizationId);
				ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromProxyAddress(adSettings, sender.MailboxSmtp);
				sender.OrganizationId = resolvedOrganizationId;
				sender.MailboxGuid = exchangePrincipal.MailboxInfo.MailboxGuid;
				sender.DatabaseGuid = exchangePrincipal.MailboxInfo.MailboxDatabase.ObjectGuid;
				if (resolvedOrganizationId != OrganizationId.ForestWideOrgId)
				{
					sender.ExternalDirectoryOrganizationId = new Guid(resolvedOrganizationId.ToExternalDirectoryOrganizationId());
				}
				return;
			}
			if (sender.OrganizationId == null)
			{
				sender.OrganizationId = ((Guid.Empty != sender.ExternalDirectoryOrganizationId) ? OrganizationId.FromExternalDirectoryOrganizationId(sender.ExternalDirectoryOrganizationId) : OrganizationId.ForestWideOrgId);
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000BFB0 File Offset: 0x0000A1B0
		private static RemoteMessenger CreateRemoteMessengerBasedOnSender(BrokerSubscription subscription, string methodName)
		{
			NotificationParticipant sender = subscription.Sender;
			switch (sender.LocationKind)
			{
			case NotificationParticipantLocationKind.LocalResourceForest:
				return new RemoteMessenger.BackendToBackendRemoteMessenger(RemoteConduit.FindBackEndServer(sender.MailboxGuid, sender.OrganizationId), methodName);
			case NotificationParticipantLocationKind.RemoteResourceForest:
				return new RemoteMessenger.CrossDeploymentRemoteMessenger(RemoteMessengerFactory.GetCurrentNotificationBrokerExternalUrl(), sender.OrganizationId, sender.MailboxSmtp, methodName);
			case NotificationParticipantLocationKind.CrossPremise:
			{
				OrganizationIdCacheValue organizationIdCacheValue = OrganizationIdCache.Singleton.Get(sender.OrganizationId);
				string domain = SmtpAddress.Parse(sender.MailboxSmtp).Domain;
				IntraOrganizationConnector intraOrganizationConnector = organizationIdCacheValue.GetIntraOrganizationConnector(domain);
				if (intraOrganizationConnector != null)
				{
					Uri discoveryEndpoint = intraOrganizationConnector.DiscoveryEndpoint;
					if (discoveryEndpoint != null)
					{
						return new RemoteMessenger.CrossDeploymentRemoteMessenger(discoveryEndpoint, sender.OrganizationId, sender.MailboxSmtp, methodName);
					}
				}
				break;
			}
			}
			throw new InvalidBrokerSubscriptionException(string.Empty);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000C078 File Offset: 0x0000A278
		private static Uri GetCurrentNotificationBrokerExternalUrl()
		{
			Uri datacenterFrontEndWebServicesUrl = FrontEndLocator.GetDatacenterFrontEndWebServicesUrl();
			return new UriBuilder(datacenterFrontEndWebServicesUrl)
			{
				Path = BrokerConfiguration.VdirName.Value
			}.Uri;
		}
	}
}
