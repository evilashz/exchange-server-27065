using System;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000053 RID: 83
	internal interface IPromptCounter
	{
		// Token: 0x0600030E RID: 782
		void SetPromptCount(string promptId, int newCount);

		// Token: 0x0600030F RID: 783
		int GetPromptCount(string promptId);

		// Token: 0x06000310 RID: 784
		void SavePromptCount();
	}
}
