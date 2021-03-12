using System;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002B3 RID: 691
	internal interface IUMCAMessage
	{
		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060014FC RID: 5372
		UMRecipient CAMessageRecipient { get; }

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060014FD RID: 5373
		bool CollectMessageForAnalysis { get; }
	}
}
