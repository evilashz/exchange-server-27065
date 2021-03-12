using System;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x0200003E RID: 62
	internal interface ISubmitDocument : IDisposable
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600012D RID: 301
		// (set) Token: 0x0600012E RID: 302
		TimeSpan SubmissionTimeout { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600012F RID: 303
		IFastDocumentHelper DocumentHelper { get; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000130 RID: 304
		// (set) Token: 0x06000131 RID: 305
		IDocumentTracker Tracker { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000132 RID: 306
		// (set) Token: 0x06000133 RID: 307
		string IndexSystemName { get; set; }

		// Token: 0x06000134 RID: 308
		void Initialize();

		// Token: 0x06000135 RID: 309
		ICancelableAsyncResult BeginSubmitDocument(IFastDocument document, AsyncCallback callback, object state);

		// Token: 0x06000136 RID: 310
		bool EndSubmitDocument(IAsyncResult asyncResult);

		// Token: 0x06000137 RID: 311
		bool TryCompleteSubmitDocument(IAsyncResult asyncResult);

		// Token: 0x06000138 RID: 312
		IFastDocument CreateFastDocument(DocumentOperation operation);
	}
}
