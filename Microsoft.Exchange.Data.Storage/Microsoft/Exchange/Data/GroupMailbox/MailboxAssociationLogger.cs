using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Optics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x020007F4 RID: 2036
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class MailboxAssociationLogger : MailboxLoggerBase
	{
		// Token: 0x06004C3B RID: 19515 RVA: 0x0013C2E2 File Offset: 0x0013A4E2
		public MailboxAssociationLogger(Guid mailboxGuid, OrganizationId organizationId) : base(mailboxGuid, organizationId, MailboxAssociationLogger.Instance.Value)
		{
		}

		// Token: 0x04002972 RID: 10610
		private static readonly Lazy<ExtensibleLogger> Instance = new Lazy<ExtensibleLogger>(() => new ExtensibleLogger(MailboxAssociationLogConfiguration.Default));
	}
}
