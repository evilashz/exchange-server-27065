using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006C7 RID: 1735
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class DocumentStreamAccessDeniedException : AccessDeniedException
	{
		// Token: 0x06004584 RID: 17796 RVA: 0x00127C30 File Offset: 0x00125E30
		internal DocumentStreamAccessDeniedException(ObjectId objectId, string uri, Exception innerException) : base(objectId, Strings.ExDocumentStreamAccessDenied(uri), innerException)
		{
			this.uri = uri;
		}

		// Token: 0x17001427 RID: 5159
		// (get) Token: 0x06004585 RID: 17797 RVA: 0x00127C47 File Offset: 0x00125E47
		public string Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x04002612 RID: 9746
		private readonly string uri;
	}
}
