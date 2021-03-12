using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Data.Storage.Optics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;

namespace Microsoft.Exchange.Data.Storage.GroupMailbox
{
	// Token: 0x020007EC RID: 2028
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class GroupEscalationGetterDiagnosticsFrameFactory : IDiagnosticsFrameFactory<IExtensibleLogger, IMailboxAssociationPerformanceTracker>
	{
		// Token: 0x06004BFB RID: 19451 RVA: 0x0013BDC5 File Offset: 0x00139FC5
		private GroupEscalationGetterDiagnosticsFrameFactory()
		{
		}

		// Token: 0x06004BFC RID: 19452 RVA: 0x0013BDCD File Offset: 0x00139FCD
		public IDiagnosticsFrame CreateDiagnosticsFrame(string operationContext, string operationName, IExtensibleLogger logger, IMailboxPerformanceTracker performanceTracker)
		{
			return new DiagnosticsFrame(operationContext, operationName, GroupEscalationGetterDiagnosticsFrameFactory.Tracer, logger, performanceTracker);
		}

		// Token: 0x06004BFD RID: 19453 RVA: 0x0013BDDE File Offset: 0x00139FDE
		public IExtensibleLogger CreateLogger(Guid mailboxGuid, OrganizationId organizationId)
		{
			return new GroupMessageEscalationLogger(mailboxGuid, organizationId);
		}

		// Token: 0x06004BFE RID: 19454 RVA: 0x0013BDE7 File Offset: 0x00139FE7
		public IMailboxAssociationPerformanceTracker CreatePerformanceTracker(IMailboxSession mailboxSession)
		{
			return new MailboxAssociationPerformanceTracker();
		}

		// Token: 0x04002952 RID: 10578
		private static readonly Trace Tracer = ExTraceGlobals.GroupMailboxAccessLayerTracer;

		// Token: 0x04002953 RID: 10579
		public static readonly GroupEscalationGetterDiagnosticsFrameFactory Default = new GroupEscalationGetterDiagnosticsFrameFactory();
	}
}
