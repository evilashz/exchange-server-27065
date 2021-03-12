using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Transport.Sync.Common;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000189 RID: 393
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MrsConnectedAccountsNotificationManager : ConnectedAccountsNotificationManagerBase
	{
		// Token: 0x06000E1A RID: 3610 RVA: 0x00035835 File Offset: 0x00033A35
		private MrsConnectedAccountsNotificationManager(Guid userMailboxGuid, Guid userMdbGuid, string userMailboxServerFQDN, ISyncNowNotificationClient notificationClient) : base(userMailboxGuid, userMdbGuid, userMailboxServerFQDN, ConnectedAccountsConfiguration.Instance, notificationClient)
		{
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x00035848 File Offset: 0x00033A48
		public static MrsConnectedAccountsNotificationManager Create(MailboxSession mailboxSession, UserContext userContext)
		{
			if (ConnectedAccountsNotificationManagerBase.ShouldSetupNotificationManagerForUser(mailboxSession, userContext) && MrsConnectedAccountsNotificationManager.ShouldSetupNotificationManagerForUser(mailboxSession))
			{
				IExchangePrincipal mailboxOwner = mailboxSession.MailboxOwner;
				ExTraceGlobals.ConnectedAccountsTracer.TraceDebug<Guid, Guid, string>((long)userContext.GetHashCode(), "MrsConnectedAccountsNotificationManager.Create::Setting up ConnectedAccountsNotificationManager for User (MailboxGuid:{0}, MdbGuid:{1}, ServerFullyQualifiedDomainName:{2}).", mailboxOwner.MailboxInfo.MailboxGuid, mailboxOwner.MailboxInfo.GetDatabaseGuid(), mailboxOwner.MailboxInfo.Location.ServerFqdn);
				ISyncNowNotificationClient notificationClient = new MrsSyncNowNotificationClient();
				return new MrsConnectedAccountsNotificationManager(mailboxOwner.MailboxInfo.MailboxGuid, mailboxOwner.MailboxInfo.GetDatabaseGuid(), mailboxOwner.MailboxInfo.Location.ServerFqdn, notificationClient);
			}
			return null;
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x000358E0 File Offset: 0x00033AE0
		internal static MrsConnectedAccountsNotificationManager CreateFromTest(Guid userMailboxGuid, Guid userMdbGuid, string userMailboxServerFQDN)
		{
			ISyncNowNotificationClient notificationClient = new MrsSyncNowNotificationClient();
			return new MrsConnectedAccountsNotificationManager(userMailboxGuid, userMdbGuid, userMailboxServerFQDN, notificationClient);
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x000358FC File Offset: 0x00033AFC
		private static bool ShouldSetupNotificationManagerForUser(MailboxSession userMailboxSession)
		{
			SyncUtilities.ThrowIfArgumentNull("userMailboxSession", userMailboxSession);
			try
			{
				IExchangePrincipal mailboxOwner = userMailboxSession.MailboxOwner;
				ADUser aduser = DirectoryHelper.ReadADRecipient(mailboxOwner.MailboxInfo.MailboxGuid, mailboxOwner.MailboxInfo.IsArchive, userMailboxSession.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid)) as ADUser;
				if (aduser != null)
				{
					AggregatedAccountHelper aggregatedAccountHelper = new AggregatedAccountHelper(userMailboxSession, aduser);
					List<AggregatedAccountInfo> listOfAccounts = aggregatedAccountHelper.GetListOfAccounts();
					if (listOfAccounts != null)
					{
						return listOfAccounts.Count > 0;
					}
				}
			}
			catch (LocalizedException arg)
			{
				ExTraceGlobals.ConnectedAccountsTracer.TraceError<Guid, SmtpAddress, LocalizedException>((long)userMailboxSession.GetHashCode(), "MrsConnectedAccountsNotificationManager.ShouldSetupNotificationManagerForUser failed for User (MailboxGuid:{0}, PrimarySmtpAddress:{1}), with error:{2}. We will assume that user has active connected accounts.", userMailboxSession.MailboxGuid, userMailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress, arg);
			}
			return false;
		}
	}
}
