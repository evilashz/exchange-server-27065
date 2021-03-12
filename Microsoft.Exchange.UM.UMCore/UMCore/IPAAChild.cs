using System;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000291 RID: 657
	internal interface IPAAChild : IPAACommonInterface
	{
		// Token: 0x06001365 RID: 4965
		void TerminateCall();

		// Token: 0x06001366 RID: 4966
		void TerminateCallToTryNextNumberTransfer();
	}
}
