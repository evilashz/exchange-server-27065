using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x0200000C RID: 12
	internal class DocumentFactory
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00002BB7 File Offset: 0x00000DB7
		protected DocumentFactory()
		{
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002BBF File Offset: 0x00000DBF
		internal static Hookable<DocumentFactory> Instance
		{
			get
			{
				return DocumentFactory.instance;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002BC6 File Offset: 0x00000DC6
		internal static DocumentFactory Current
		{
			get
			{
				return DocumentFactory.instance.Value;
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002BD2 File Offset: 0x00000DD2
		internal IDocument CreateDocument(IIdentity documentId, DocumentOperation documentOperation)
		{
			return this.CreateDocument(documentId, documentOperation, null);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002BDD File Offset: 0x00000DDD
		internal IDocument CreateDocument(IIdentity documentId, DocumentOperation documentOperation, IDocumentAdapter documentAdapter)
		{
			return this.CreateDocument(documentId, documentOperation, documentAdapter, false);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002BE9 File Offset: 0x00000DE9
		internal IDocument CreateDocument(IIdentity documentId, DocumentOperation documentOperation, IDocumentAdapter documentAdapter, bool initializeNestedDocuments)
		{
			Util.ThrowOnNullArgument(documentId, "documentId");
			return new Document(documentId, documentOperation, documentAdapter, initializeNestedDocuments);
		}

		// Token: 0x04000024 RID: 36
		private static readonly Hookable<DocumentFactory> instance = Hookable<DocumentFactory>.Create(true, new DocumentFactory());
	}
}
