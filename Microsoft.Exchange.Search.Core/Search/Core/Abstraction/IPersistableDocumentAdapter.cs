using System;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x0200003C RID: 60
	internal interface IPersistableDocumentAdapter : IDocumentAdapter
	{
		// Token: 0x06000126 RID: 294
		void Save();

		// Token: 0x06000127 RID: 295
		void Save(bool reaload);
	}
}
