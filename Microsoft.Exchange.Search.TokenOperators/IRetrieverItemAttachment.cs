using System;

namespace Microsoft.Exchange.Search.TokenOperators
{
	// Token: 0x02000015 RID: 21
	internal interface IRetrieverItemAttachment : IRetrieverAttachment, IDisposable
	{
		// Token: 0x060000E6 RID: 230
		IRetrieverItem GetItem();
	}
}
