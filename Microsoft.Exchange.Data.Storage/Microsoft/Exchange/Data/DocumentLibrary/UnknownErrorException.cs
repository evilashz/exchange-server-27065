using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006D0 RID: 1744
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class UnknownErrorException : DocumentLibraryException
	{
		// Token: 0x06004598 RID: 17816 RVA: 0x00127D74 File Offset: 0x00125F74
		internal UnknownErrorException(ObjectId objectId, string message, Exception innerException) : base(message, innerException)
		{
			this.objectId = objectId;
		}

		// Token: 0x1700142E RID: 5166
		// (get) Token: 0x06004599 RID: 17817 RVA: 0x00127D85 File Offset: 0x00125F85
		public ObjectId ObjectId
		{
			get
			{
				return this.objectId;
			}
		}

		// Token: 0x04002618 RID: 9752
		private readonly ObjectId objectId;
	}
}
