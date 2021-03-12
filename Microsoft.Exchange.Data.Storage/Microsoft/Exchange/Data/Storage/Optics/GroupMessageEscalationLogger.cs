using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Optics
{
	// Token: 0x020007EF RID: 2031
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class GroupMessageEscalationLogger : MailboxLoggerBase
	{
		// Token: 0x06004C13 RID: 19475 RVA: 0x0013C07B File Offset: 0x0013A27B
		public GroupMessageEscalationLogger(Guid mailboxGuid, OrganizationId organizationId) : base(mailboxGuid, organizationId, GroupMessageEscalationLogger.Instance.Value)
		{
		}

		// Token: 0x04002967 RID: 10599
		private static readonly Lazy<ExtensibleLogger> Instance = new Lazy<ExtensibleLogger>(() => new ExtensibleLogger(GroupMessageEscalationLogConfiguration.Default));
	}
}
