using System;
using System.IO;

namespace Microsoft.Exchange.Search.TokenOperators
{
	// Token: 0x02000018 RID: 24
	internal interface IRetrieverStreamAttachment : IRetrieverAttachment, IDisposable
	{
		// Token: 0x060000E9 RID: 233
		Stream TryGetContentStream();
	}
}
