using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AnchorService
{
	// Token: 0x02000021 RID: 33
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAnchorService : IDisposable
	{
		// Token: 0x0600018B RID: 395
		bool Initialize(AnchorContext context);

		// Token: 0x0600018C RID: 396
		void Start();

		// Token: 0x0600018D RID: 397
		IEnumerable<IDiagnosable> GetDiagnosableComponents();
	}
}
