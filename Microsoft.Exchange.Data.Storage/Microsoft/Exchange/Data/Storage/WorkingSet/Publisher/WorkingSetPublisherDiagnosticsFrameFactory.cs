using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Optics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.WorkingSet;

namespace Microsoft.Exchange.Data.Storage.WorkingSet.Publisher
{
	// Token: 0x02000EEC RID: 3820
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class WorkingSetPublisherDiagnosticsFrameFactory : IDiagnosticsFrameFactory<IExtensibleLogger, IWorkingSetPublisherPerformanceTracker>
	{
		// Token: 0x060083D7 RID: 33751 RVA: 0x0023CC45 File Offset: 0x0023AE45
		private WorkingSetPublisherDiagnosticsFrameFactory()
		{
		}

		// Token: 0x060083D8 RID: 33752 RVA: 0x0023CC4D File Offset: 0x0023AE4D
		public IDiagnosticsFrame CreateDiagnosticsFrame(string operationContext, string operationName, IExtensibleLogger logger, IMailboxPerformanceTracker performanceTracker)
		{
			return new DiagnosticsFrame(operationContext, operationName, WorkingSetPublisherDiagnosticsFrameFactory.Tracer, logger, performanceTracker);
		}

		// Token: 0x060083D9 RID: 33753 RVA: 0x0023CC5E File Offset: 0x0023AE5E
		public IExtensibleLogger CreateLogger(Guid mailboxGuid, OrganizationId organizationId)
		{
			return new WorkingSetPublisherLogger(mailboxGuid, organizationId);
		}

		// Token: 0x060083DA RID: 33754 RVA: 0x0023CC67 File Offset: 0x0023AE67
		public IWorkingSetPublisherPerformanceTracker CreatePerformanceTracker(IMailboxSession mailboxSession)
		{
			return new WorkingSetPublisherPerformanceTracker(mailboxSession);
		}

		// Token: 0x0400581C RID: 22556
		private static readonly Trace Tracer = ExTraceGlobals.WorkingSetPublisherTracer;

		// Token: 0x0400581D RID: 22557
		public static readonly WorkingSetPublisherDiagnosticsFrameFactory Default = new WorkingSetPublisherDiagnosticsFrameFactory();
	}
}
