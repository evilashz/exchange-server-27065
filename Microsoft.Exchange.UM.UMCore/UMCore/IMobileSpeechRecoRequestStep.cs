using System;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000275 RID: 629
	internal interface IMobileSpeechRecoRequestStep
	{
		// Token: 0x060012A4 RID: 4772
		void ExecuteAsync(MobileRecoRequestStepAsyncCompletedDelegate callback, object token);
	}
}
