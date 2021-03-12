using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ClientAccess
{
	// Token: 0x0200003F RID: 63
	internal enum ProbeState
	{
		// Token: 0x04000156 RID: 342
		PreparingRequest,
		// Token: 0x04000157 RID: 343
		WaitingResponse,
		// Token: 0x04000158 RID: 344
		Passed,
		// Token: 0x04000159 RID: 345
		FailedRequest,
		// Token: 0x0400015A RID: 346
		FailedResponse,
		// Token: 0x0400015B RID: 347
		TimedOut
	}
}
