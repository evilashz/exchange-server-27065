using System;
using System.Security.Principal;
using System.Web.Caching;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000098 RID: 152
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMailboxContext : IDisposable
	{
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000597 RID: 1431
		// (set) Token: 0x06000598 RID: 1432
		UserContextState State { get; set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000599 RID: 1433
		UserContextKey Key { get; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600059A RID: 1434
		// (set) Token: 0x0600059B RID: 1435
		ExchangePrincipal ExchangePrincipal { get; set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600059C RID: 1436
		MailboxSession MailboxSession { get; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600059D RID: 1437
		string UserPrincipalName { get; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600059E RID: 1438
		SmtpAddress PrimarySmtpAddress { get; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600059F RID: 1439
		OwaIdentity LogonIdentity { get; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060005A0 RID: 1440
		OwaIdentity MailboxIdentity { get; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060005A1 RID: 1441
		INotificationManager NotificationManager { get; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060005A2 RID: 1442
		PendingRequestManager PendingRequestManager { get; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060005A3 RID: 1443
		// (set) Token: 0x060005A4 RID: 1444
		CacheItemRemovedReason AbandonedReason { get; set; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060005A5 RID: 1445
		// (set) Token: 0x060005A6 RID: 1446
		UserContextTerminationStatus TerminationStatus { get; set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060005A7 RID: 1447
		bool IsExplicitLogon { get; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060005A8 RID: 1448
		SessionDataCache SessionDataCache { get; }

		// Token: 0x060005A9 RID: 1449
		void Load(OwaIdentity logonIdentity, OwaIdentity mailboxIdentity, UserContextStatistics stats);

		// Token: 0x060005AA RID: 1450
		void ValidateLogonPermissionIfNecessary();

		// Token: 0x060005AB RID: 1451
		void LogBreadcrumb(string message);

		// Token: 0x060005AC RID: 1452
		string DumpBreadcrumbs();

		// Token: 0x060005AD RID: 1453
		bool LockAndReconnectMailboxSession(int timeout);

		// Token: 0x060005AE RID: 1454
		void UnlockAndDisconnectMailboxSession();

		// Token: 0x060005AF RID: 1455
		bool MailboxSessionLockedByCurrentThread();

		// Token: 0x060005B0 RID: 1456
		void DisconnectMailboxSession();

		// Token: 0x060005B1 RID: 1457
		MailboxSession CloneMailboxSession(string mailboxKey, ExchangePrincipal exchangePrincipal, IADOrgPerson person, ClientSecurityContext clientSecurityContext, GenericIdentity genericIdentity, bool unifiedLogon);
	}
}
