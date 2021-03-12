using System;
using System.Globalization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Auditing;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.Compliance.Audit.Schema.Mailbox;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x020000AA RID: 170
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MailboxAuditWriter
	{
		// Token: 0x06000748 RID: 1864 RVA: 0x0001C748 File Offset: 0x0001A948
		public void Write(ExchangeMailboxAuditRecord record)
		{
			MailboxSession mailboxSession = this.GetMailboxSession(record.OrganizationId, record.MailboxGuid);
			AuditEventRecordAdapter auditEvent = new ItemOperationAuditEventRecordAdapter(record, mailboxSession.OrganizationId.ToString());
			mailboxSession.AuditMailboxAccess(auditEvent, true);
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0001C784 File Offset: 0x0001A984
		public void Write(ExchangeMailboxAuditGroupRecord record)
		{
			MailboxSession mailboxSession = this.GetMailboxSession(record.OrganizationId, record.MailboxGuid);
			AuditEventRecordAdapter auditEvent = new GroupOperationAuditEventRecordAdapter(record, mailboxSession.OrganizationId.ToString());
			mailboxSession.AuditMailboxAccess(auditEvent, true);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0001C7C0 File Offset: 0x0001A9C0
		private MailboxSession GetMailboxSession(string organizationIdEncoded, Guid mailboxGuid)
		{
			CacheEntry<MailboxSession> cacheEntry;
			MailboxSession result;
			if (this.sessions.TryGetValue(mailboxGuid, DateTime.UtcNow, out cacheEntry))
			{
				result = cacheEntry.Value;
			}
			else
			{
				OrganizationId organizationId = AuditRecordDatabaseWriterVisitor.GetOrganizationId(organizationIdEncoded);
				ADSessionSettings adSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), organizationId, null, false);
				ExchangePrincipal mailboxOwner = ExchangePrincipal.FromMailboxGuid(adSettings, mailboxGuid, RemotingOptions.AllowCrossSite, null);
				result = MailboxSession.OpenAsSystemService(mailboxOwner, CultureInfo.InvariantCulture, "Client=Management;Action=AuditLog");
			}
			return result;
		}

		// Token: 0x0400034C RID: 844
		private const string ClientInfoString = "Client=Management;Action=AuditLog";

		// Token: 0x0400034D RID: 845
		public const int SessionCacheSize = 32;

		// Token: 0x0400034E RID: 846
		public static TimeSpan SessionLifeTime = TimeSpan.FromMinutes(5.0);

		// Token: 0x0400034F RID: 847
		private readonly CacheWithExpiration<Guid, CacheEntry<MailboxSession>> sessions = new CacheWithExpiration<Guid, CacheEntry<MailboxSession>>(32, MailboxAuditWriter.SessionLifeTime, null);
	}
}
