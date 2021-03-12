using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.Search.TokenOperators
{
	// Token: 0x02000012 RID: 18
	internal interface IRetrieverAttachmentCollection : IEnumerable<IRetrieverAttachmentHandle>, IEnumerable
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000E0 RID: 224
		int Count { get; }

		// Token: 0x060000E1 RID: 225
		IRetrieverAttachment Open(IRetrieverAttachmentHandle handle);
	}
}
