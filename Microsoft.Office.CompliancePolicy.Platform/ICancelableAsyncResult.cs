using System;

namespace Microsoft.Office.CompliancePolicy
{
	// Token: 0x02000037 RID: 55
	public interface ICancelableAsyncResult : IAsyncResult
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000C1 RID: 193
		bool IsCanceled { get; }

		// Token: 0x060000C2 RID: 194
		void Cancel();
	}
}
