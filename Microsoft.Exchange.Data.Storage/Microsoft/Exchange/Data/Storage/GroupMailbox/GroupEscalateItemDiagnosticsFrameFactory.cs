using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Optics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Groups;

namespace Microsoft.Exchange.Data.Storage.GroupMailbox
{
	// Token: 0x020007E4 RID: 2020
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class GroupEscalateItemDiagnosticsFrameFactory : IDiagnosticsFrameFactory<IExtensibleLogger, IGroupEscalateItemPerformanceTracker>
	{
		// Token: 0x06004BA3 RID: 19363 RVA: 0x0013B924 File Offset: 0x00139B24
		private GroupEscalateItemDiagnosticsFrameFactory()
		{
		}

		// Token: 0x06004BA4 RID: 19364 RVA: 0x0013B92C File Offset: 0x00139B2C
		public IDiagnosticsFrame CreateDiagnosticsFrame(string operationContext, string operationName, IExtensibleLogger logger, IMailboxPerformanceTracker performanceTracker)
		{
			return new DiagnosticsFrame(operationContext, operationName, GroupEscalateItemDiagnosticsFrameFactory.Tracer, logger, performanceTracker);
		}

		// Token: 0x06004BA5 RID: 19365 RVA: 0x0013B93D File Offset: 0x00139B3D
		public IExtensibleLogger CreateLogger(Guid mailboxGuid, OrganizationId organizationId)
		{
			return new GroupMessageEscalationLogger(mailboxGuid, organizationId);
		}

		// Token: 0x06004BA6 RID: 19366 RVA: 0x0013B946 File Offset: 0x00139B46
		public IGroupEscalateItemPerformanceTracker CreatePerformanceTracker(IMailboxSession mailboxSession)
		{
			return new GroupEscalateItemPerformanceTracker(mailboxSession);
		}

		// Token: 0x04002913 RID: 10515
		private static readonly Trace Tracer = ExTraceGlobals.COWGroupMessageEscalationTracer;

		// Token: 0x04002914 RID: 10516
		public static readonly GroupEscalateItemDiagnosticsFrameFactory Default = new GroupEscalateItemDiagnosticsFrameFactory();
	}
}
