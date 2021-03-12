using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Optics
{
	// Token: 0x020007E3 RID: 2019
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IDiagnosticsFrameFactory<TLogger, TTracker> where TLogger : IExtensibleLogger where TTracker : IMailboxPerformanceTracker
	{
		// Token: 0x06004BA0 RID: 19360
		IDiagnosticsFrame CreateDiagnosticsFrame(string operationContext, string operationName, IExtensibleLogger logger, IMailboxPerformanceTracker performanceTracker);

		// Token: 0x06004BA1 RID: 19361
		TLogger CreateLogger(Guid mailboxGuid, OrganizationId organizationId);

		// Token: 0x06004BA2 RID: 19362
		TTracker CreatePerformanceTracker(IMailboxSession mailboxSession);
	}
}
