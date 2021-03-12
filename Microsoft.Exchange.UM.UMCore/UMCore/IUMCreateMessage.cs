using System;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002B1 RID: 689
	internal interface IUMCreateMessage
	{
		// Token: 0x060014D0 RID: 5328
		void PrepareUnProtectedMessage();

		// Token: 0x060014D1 RID: 5329
		void PrepareProtectedMessage();

		// Token: 0x060014D2 RID: 5330
		void PrepareNDRForFailureToGenerateProtectedMessage();
	}
}
