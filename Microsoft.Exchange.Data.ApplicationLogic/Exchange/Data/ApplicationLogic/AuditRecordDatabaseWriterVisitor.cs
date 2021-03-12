using System;
using System.Text;
using Microsoft.Exchange.Data.ApplicationLogic.AuditLog;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.Compliance.Audit.Schema;
using Microsoft.Office.Compliance.Audit.Schema.Admin;
using Microsoft.Office.Compliance.Audit.Schema.Mailbox;
using Microsoft.Office.Compliance.Audit.Schema.Monitoring;
using Microsoft.Office.Compliance.Audit.Schema.SharePoint;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x020000AB RID: 171
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class AuditRecordDatabaseWriterVisitor : IAuditRecordVisitor
	{
		// Token: 0x0600074D RID: 1869 RVA: 0x0001C84F File Offset: 0x0001AA4F
		public AuditRecordDatabaseWriterVisitor(Trace tracer)
		{
			this.adminAuditWriter = new AdminAuditWriter(tracer);
			this.mailboxAuditWriter = new MailboxAuditWriter();
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0001C86E File Offset: 0x0001AA6E
		public void Visit(ExchangeAdminAuditRecord record)
		{
			this.adminAuditWriter.Write(record);
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0001C87C File Offset: 0x0001AA7C
		public void Visit(ExchangeMailboxAuditRecord record)
		{
			this.mailboxAuditWriter.Write(record);
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0001C88A File Offset: 0x0001AA8A
		public void Visit(ExchangeMailboxAuditGroupRecord record)
		{
			this.mailboxAuditWriter.Write(record);
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0001C898 File Offset: 0x0001AA98
		public void Visit(SharePointAuditRecord record)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0001C89F File Offset: 0x0001AA9F
		public void Visit(SyntheticProbeAuditRecord record)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0001C8A8 File Offset: 0x0001AAA8
		internal static OrganizationId GetOrganizationId(string organizationIdEncoded)
		{
			if (string.IsNullOrWhiteSpace(organizationIdEncoded))
			{
				return OrganizationId.ForestWideOrgId;
			}
			byte[] bytes;
			try
			{
				bytes = Convert.FromBase64String(organizationIdEncoded);
			}
			catch (FormatException)
			{
				throw new InvalidOrganizationException(organizationIdEncoded);
			}
			OrganizationId organizationId;
			OrganizationId.TryCreateFromBytes(bytes, Encoding.UTF8, out organizationId);
			if (organizationId == null)
			{
				throw new InvalidOrganizationException(organizationIdEncoded);
			}
			return organizationId;
		}

		// Token: 0x04000350 RID: 848
		private readonly AdminAuditWriter adminAuditWriter;

		// Token: 0x04000351 RID: 849
		private readonly MailboxAuditWriter mailboxAuditWriter;
	}
}
