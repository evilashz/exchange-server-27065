using System;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000292 RID: 658
	internal interface IPAAParent : IPAACommonInterface
	{
		// Token: 0x06001367 RID: 4967
		void AcceptCall();

		// Token: 0x06001368 RID: 4968
		void TerminateFindMe();

		// Token: 0x06001369 RID: 4969
		void ContinueFindMe();

		// Token: 0x0600136A RID: 4970
		void SetPointerToChild(IPAAChild pointer);

		// Token: 0x0600136B RID: 4971
		void DisconnectChildCall();

		// Token: 0x0600136C RID: 4972
		object GetCallerRecordedName();

		// Token: 0x0600136D RID: 4973
		object GetCalleeRecordedName();
	}
}
