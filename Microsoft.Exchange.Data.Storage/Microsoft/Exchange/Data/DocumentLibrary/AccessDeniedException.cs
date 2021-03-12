using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006C2 RID: 1730
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class AccessDeniedException : DocumentLibraryException
	{
		// Token: 0x06004578 RID: 17784 RVA: 0x00127B9B File Offset: 0x00125D9B
		internal AccessDeniedException(ObjectId objectId, string message) : this(objectId, message, null)
		{
		}

		// Token: 0x06004579 RID: 17785 RVA: 0x00127BA6 File Offset: 0x00125DA6
		internal AccessDeniedException(ObjectId objectId, string message, Exception innerException) : base(message, innerException)
		{
			this.objectId = objectId;
		}

		// Token: 0x17001423 RID: 5155
		// (get) Token: 0x0600457A RID: 17786 RVA: 0x00127BB7 File Offset: 0x00125DB7
		public ObjectId ObjectId
		{
			get
			{
				return this.objectId;
			}
		}

		// Token: 0x0400260E RID: 9742
		private readonly ObjectId objectId;
	}
}
