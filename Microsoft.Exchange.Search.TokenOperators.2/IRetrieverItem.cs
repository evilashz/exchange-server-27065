using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Search.TokenOperators
{
	// Token: 0x02000014 RID: 20
	internal interface IRetrieverItem : IDisposable
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000E3 RID: 227
		IRetrieverAttachmentCollection AttachmentCollection { get; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000E4 RID: 228
		IEnumerable<IRetrieverPropertyDefinition> EmbeddedMessageProperties { get; }

		// Token: 0x060000E5 RID: 229
		object GetValue(IRetrieverPropertyDefinition propertyDefinition);
	}
}
