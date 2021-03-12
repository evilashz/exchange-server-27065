using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006C6 RID: 1734
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class DocumentModifiedException : AccessDeniedException
	{
		// Token: 0x06004582 RID: 17794 RVA: 0x00127C12 File Offset: 0x00125E12
		internal DocumentModifiedException(ObjectId objectId, string uri) : base(objectId, Strings.ExDocumentModified(uri))
		{
			this.uri = uri;
		}

		// Token: 0x17001426 RID: 5158
		// (get) Token: 0x06004583 RID: 17795 RVA: 0x00127C28 File Offset: 0x00125E28
		public string Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x04002611 RID: 9745
		private readonly string uri;
	}
}
