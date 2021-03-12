using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x02000017 RID: 23
	internal interface IDocument : IPropertyBag, IReadOnlyPropertyBag
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600006C RID: 108
		IIdentity Identity { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600006D RID: 109
		DocumentOperation Operation { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600006E RID: 110
		ICollection<DocumentFailureDescription> Failures { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600006F RID: 111
		ReadOnlyCollection<IDocument> NestedDocuments { get; }

		// Token: 0x06000070 RID: 112
		void AddDocument(IDocument document);

		// Token: 0x06000071 RID: 113
		int RemoveDocuments(ICollection<IDocument> documentsToRemove);
	}
}
