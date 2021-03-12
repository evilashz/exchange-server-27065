﻿using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001BA RID: 442
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TxSyncConnectedAccountsNotificationManager : ConnectedAccountsNotificationManagerBase
	{
		// Token: 0x06000FBD RID: 4029 RVA: 0x0003CBF6 File Offset: 0x0003ADF6
		private TxSyncConnectedAccountsNotificationManager(Guid userMailboxGuid, Guid userMdbGuid, string userMailboxServerFQDN) : base(userMailboxGuid, userMdbGuid, userMailboxServerFQDN, ConnectedAccountsConfiguration.Instance, SubscriptionNotificationClient.DefaultInstance)
		{
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x0003CC0C File Offset: 0x0003AE0C
		public static TxSyncConnectedAccountsNotificationManager Create(MailboxSession mailboxSession, UserContext userContext)
		{
			if (ConnectedAccountsNotificationManagerBase.ShouldSetupNotificationManagerForUser(mailboxSession, userContext) && TxSyncConnectedAccountsNotificationManager.ShouldSetupNotificationManagerForUser(mailboxSession))
			{
				IExchangePrincipal mailboxOwner = mailboxSession.MailboxOwner;
				ExTraceGlobals.ConnectedAccountsTracer.TraceDebug<Guid, Guid, string>((long)userContext.GetHashCode(), "TxSyncConnectedAccountsNotificationManager.Create::Setting up ConnectedAccountsNotificationManager for User (MailboxGuid:{0}, MdbGuid:{1}, ServerFullyQualifiedDomainName:{2}).", mailboxOwner.MailboxInfo.MailboxGuid, mailboxOwner.MailboxInfo.GetDatabaseGuid(), mailboxOwner.MailboxInfo.Location.ServerFqdn);
				return new TxSyncConnectedAccountsNotificationManager(mailboxOwner.MailboxInfo.MailboxGuid, mailboxOwner.MailboxInfo.GetDatabaseGuid(), mailboxOwner.MailboxInfo.Location.ServerFqdn);
			}
			return null;
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x0003CC9C File Offset: 0x0003AE9C
		private static bool ShouldSetupNotificationManagerForUser(MailboxSession userMailboxSession)
		{
			SyncUtilities.ThrowIfArgumentNull("userMailboxSession", userMailboxSession);
			bool result;
			try
			{
				bool flag = SubscriptionManager.DoesUserHasAnyActiveConnectedAccounts(userMailboxSession, AggregationSubscriptionType.AllEMail);
				result = flag;
			}
			catch (LocalizedException arg)
			{
				ExTraceGlobals.ConnectedAccountsTracer.TraceError<Guid, SmtpAddress, LocalizedException>((long)userMailboxSession.GetHashCode(), "DoesUserHasActiveConnectedAccounts failed for User (MailboxGuid:{0}, PrimarySmtpAddress:{1}), with error:{2}. We will assume that user has active connected accounts.", userMailboxSession.MailboxGuid, userMailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress, arg);
				result = true;
			}
			return result;
		}
	}
}
