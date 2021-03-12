using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006C9 RID: 1737
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class ObjectNotFoundException : DocumentLibraryException
	{
		// Token: 0x06004588 RID: 17800 RVA: 0x00127C6E File Offset: 0x00125E6E
		internal ObjectNotFoundException(ObjectId objectId, string message) : this(objectId, message, null)
		{
		}

		// Token: 0x06004589 RID: 17801 RVA: 0x00127C79 File Offset: 0x00125E79
		internal ObjectNotFoundException(ObjectId objectId, string message, Exception innerException) : base(message, innerException)
		{
			this.objectId = objectId;
		}

		// Token: 0x17001429 RID: 5161
		// (get) Token: 0x0600458A RID: 17802 RVA: 0x00127C8A File Offset: 0x00125E8A
		public ObjectId ObjectId
		{
			get
			{
				return this.objectId;
			}
		}

		// Token: 0x04002614 RID: 9748
		private readonly ObjectId objectId;
	}
}
