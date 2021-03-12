using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006C8 RID: 1736
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class GetViewAccessDeniedException : AccessDeniedException
	{
		// Token: 0x06004586 RID: 17798 RVA: 0x00127C4F File Offset: 0x00125E4F
		internal GetViewAccessDeniedException(ObjectId objectId, string uri, Exception innerException) : base(objectId, Strings.ExAccessDeniedForGetViewUnder(uri), innerException)
		{
			this.uri = uri;
		}

		// Token: 0x17001428 RID: 5160
		// (get) Token: 0x06004587 RID: 17799 RVA: 0x00127C66 File Offset: 0x00125E66
		public string Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x04002613 RID: 9747
		private readonly string uri;
	}
}
