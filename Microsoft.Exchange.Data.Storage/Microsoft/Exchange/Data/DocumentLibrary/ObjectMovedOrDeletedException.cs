using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006CA RID: 1738
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class ObjectMovedOrDeletedException : ObjectNotFoundException
	{
		// Token: 0x0600458B RID: 17803 RVA: 0x00127C92 File Offset: 0x00125E92
		internal ObjectMovedOrDeletedException(ObjectId objectId, string uri) : this(objectId, uri, null)
		{
		}

		// Token: 0x0600458C RID: 17804 RVA: 0x00127C9D File Offset: 0x00125E9D
		internal ObjectMovedOrDeletedException(ObjectId objectId, string uri, Exception innerException) : base(objectId, Strings.ExObjectMovedOrDeleted(uri), innerException)
		{
			this.uri = uri;
		}

		// Token: 0x1700142A RID: 5162
		// (get) Token: 0x0600458D RID: 17805 RVA: 0x00127CB4 File Offset: 0x00125EB4
		public string Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x04002615 RID: 9749
		private readonly string uri;
	}
}
