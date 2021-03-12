using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x02000034 RID: 52
	internal interface IPipeline : IDocumentProcessor, IStartStop, IDisposable, IDiagnosable
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600011F RID: 287
		int MaxConcurrency { get; }
	}
}
