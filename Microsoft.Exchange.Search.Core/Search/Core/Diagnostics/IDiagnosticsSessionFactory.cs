using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Search.Core.Diagnostics
{
	// Token: 0x02000099 RID: 153
	internal interface IDiagnosticsSessionFactory
	{
		// Token: 0x06000486 RID: 1158
		IDiagnosticsSession CreateComponentDiagnosticsSession(string componentName, Trace tracer, long traceContext);

		// Token: 0x06000487 RID: 1159
		IDiagnosticsSession CreateComponentDiagnosticsSession(string componentName, string eventLogSourceName, Trace tracer, long traceContext);

		// Token: 0x06000488 RID: 1160
		IDiagnosticsSession CreateDocumentDiagnosticsSession(IIdentity documentId, Trace tracer);
	}
}
