using System;

namespace System.Security.Policy
{
	// Token: 0x0200032A RID: 810
	internal interface IDelayEvaluatedEvidence
	{
		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x0600294E RID: 10574
		bool IsVerified { [SecurityCritical] get; }

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x0600294F RID: 10575
		bool WasUsed { get; }

		// Token: 0x06002950 RID: 10576
		void MarkUsed();
	}
}
