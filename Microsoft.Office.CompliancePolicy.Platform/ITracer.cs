using System;

namespace Microsoft.Office.CompliancePolicy
{
	// Token: 0x02000050 RID: 80
	public interface ITracer
	{
		// Token: 0x060001B7 RID: 439
		void TraceDebug(string message);

		// Token: 0x060001B8 RID: 440
		void TraceDebug(string formatString, params object[] args);

		// Token: 0x060001B9 RID: 441
		void TraceWarning(string message);

		// Token: 0x060001BA RID: 442
		void TraceWarning(string formatString, params object[] args);

		// Token: 0x060001BB RID: 443
		void TraceError(string message);

		// Token: 0x060001BC RID: 444
		void TraceError(string formatString, params object[] args);
	}
}
