using System;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x0200001B RID: 27
	internal interface IDocumentProcessor
	{
		// Token: 0x0600007A RID: 122
		void ProcessDocument(IDocument document, object context);

		// Token: 0x0600007B RID: 123
		IAsyncResult BeginProcess(IDocument document, AsyncCallback callback, object context);

		// Token: 0x0600007C RID: 124
		void EndProcess(IAsyncResult asyncResult);
	}
}
