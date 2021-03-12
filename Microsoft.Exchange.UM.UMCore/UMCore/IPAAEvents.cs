using System;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200029E RID: 670
	internal interface IPAAEvents
	{
		// Token: 0x06001469 RID: 5225
		void OnBeginEvaluatingPAA();

		// Token: 0x0600146A RID: 5226
		void OnEndEvaluatingPAA(PAAEvaluationStatus status, bool subscriberHasConfiguredPAA);
	}
}
