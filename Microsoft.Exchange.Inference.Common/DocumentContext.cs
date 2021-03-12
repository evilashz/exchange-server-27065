using System;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x0200000A RID: 10
	internal sealed class DocumentContext
	{
		// Token: 0x0600003D RID: 61 RVA: 0x000027EB File Offset: 0x000009EB
		internal DocumentContext(IDocument document, AsyncResult asyncResult)
		{
			Util.ThrowOnNullArgument(document, "document");
			Util.ThrowOnNullArgument(asyncResult, "asyncResult");
			this.document = document;
			this.asyncResult = asyncResult;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002817 File Offset: 0x00000A17
		internal IDocument Document
		{
			get
			{
				return this.document;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003F RID: 63 RVA: 0x0000281F File Offset: 0x00000A1F
		internal AsyncResult AsyncResult
		{
			get
			{
				return this.asyncResult;
			}
		}

		// Token: 0x0400001C RID: 28
		private readonly IDocument document;

		// Token: 0x0400001D RID: 29
		private readonly AsyncResult asyncResult;
	}
}
