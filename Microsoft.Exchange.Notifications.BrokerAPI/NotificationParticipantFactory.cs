using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x0200000B RID: 11
	internal static class NotificationParticipantFactory
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002F80 File Offset: 0x00001180
		public static NotificationParticipant FromExchangePrincipal(ExchangePrincipal exchangePrincipal)
		{
			NotificationParticipant notificationParticipant = new NotificationParticipant();
			IMailboxInfo mailboxInfo = exchangePrincipal.MailboxInfo;
			notificationParticipant.OrganizationId = mailboxInfo.OrganizationId;
			notificationParticipant.DatabaseGuid = mailboxInfo.MailboxDatabase.ObjectGuid;
			notificationParticipant.MailboxGuid = mailboxInfo.MailboxGuid;
			notificationParticipant.MailboxSmtp = mailboxInfo.PrimarySmtpAddress.ToString();
			notificationParticipant.LocationKind = NotificationParticipantLocationKind.LocalResourceForest;
			return notificationParticipant;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002FE8 File Offset: 0x000011E8
		public static NotificationParticipant FromADUser(ADUser user)
		{
			NotificationParticipant notificationParticipant = new NotificationParticipant();
			notificationParticipant.OrganizationId = user.OrganizationId;
			notificationParticipant.MailboxSmtp = user.PrimarySmtpAddress.ToString();
			if (user.Database != null)
			{
				if (PartitionId.IsLocalForestPartition(user.Database.PartitionFQDN))
				{
					notificationParticipant.LocationKind = NotificationParticipantLocationKind.LocalResourceForest;
				}
				else
				{
					notificationParticipant.LocationKind = NotificationParticipantLocationKind.RemoteResourceForest;
				}
				notificationParticipant.DatabaseGuid = user.Database.ObjectGuid;
				notificationParticipant.MailboxGuid = user.ExchangeGuid;
			}
			else
			{
				notificationParticipant.LocationKind = NotificationParticipantLocationKind.CrossPremise;
			}
			return notificationParticipant;
		}
	}
}
