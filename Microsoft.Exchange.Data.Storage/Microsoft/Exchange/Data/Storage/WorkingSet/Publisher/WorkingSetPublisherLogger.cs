using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Optics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.WorkingSet.Publisher
{
	// Token: 0x02000EEE RID: 3822
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class WorkingSetPublisherLogger : MailboxLoggerBase
	{
		// Token: 0x060083E8 RID: 33768 RVA: 0x0023CE0F File Offset: 0x0023B00F
		public WorkingSetPublisherLogger(Guid mailboxGuid, OrganizationId organizationId) : base(mailboxGuid, organizationId, WorkingSetPublisherLogger.Instance.Value)
		{
		}

		// Token: 0x04005829 RID: 22569
		private static readonly Lazy<ExtensibleLogger> Instance = new Lazy<ExtensibleLogger>(() => new ExtensibleLogger(WorkingSetPublisherLogConfiguration.Default));
	}
}
