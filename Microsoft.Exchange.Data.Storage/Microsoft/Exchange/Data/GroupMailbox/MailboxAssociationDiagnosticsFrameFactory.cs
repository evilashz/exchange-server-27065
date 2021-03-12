using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Optics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x020007F1 RID: 2033
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxAssociationDiagnosticsFrameFactory : IDiagnosticsFrameFactory<IExtensibleLogger, IMailboxAssociationPerformanceTracker>
	{
		// Token: 0x06004C24 RID: 19492 RVA: 0x0013C0C4 File Offset: 0x0013A2C4
		private MailboxAssociationDiagnosticsFrameFactory()
		{
		}

		// Token: 0x06004C25 RID: 19493 RVA: 0x0013C0CC File Offset: 0x0013A2CC
		public IDiagnosticsFrame CreateDiagnosticsFrame(string operationContext, string operationName, IExtensibleLogger logger, IMailboxPerformanceTracker performanceTracker)
		{
			return new DiagnosticsFrame(operationContext, operationName, MailboxAssociationDiagnosticsFrameFactory.Tracer, logger, performanceTracker);
		}

		// Token: 0x06004C26 RID: 19494 RVA: 0x0013C0DD File Offset: 0x0013A2DD
		public IExtensibleLogger CreateLogger(Guid mailboxGuid, OrganizationId organizationId)
		{
			return new MailboxAssociationLogger(mailboxGuid, organizationId);
		}

		// Token: 0x06004C27 RID: 19495 RVA: 0x0013C0E6 File Offset: 0x0013A2E6
		public IMailboxAssociationPerformanceTracker CreatePerformanceTracker(IMailboxSession mailboxSession)
		{
			return new MailboxAssociationPerformanceTracker();
		}

		// Token: 0x04002969 RID: 10601
		private static readonly Trace Tracer = ExTraceGlobals.GroupMailboxAccessLayerTracer;

		// Token: 0x0400296A RID: 10602
		public static readonly MailboxAssociationDiagnosticsFrameFactory Default = new MailboxAssociationDiagnosticsFrameFactory();
	}
}
