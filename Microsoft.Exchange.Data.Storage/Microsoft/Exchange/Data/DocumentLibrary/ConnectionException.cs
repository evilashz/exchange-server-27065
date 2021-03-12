using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006C3 RID: 1731
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class ConnectionException : DocumentLibraryException
	{
		// Token: 0x0600457B RID: 17787 RVA: 0x00127BBF File Offset: 0x00125DBF
		internal ConnectionException(ObjectId objectId, string message) : this(objectId, message, null)
		{
		}

		// Token: 0x0600457C RID: 17788 RVA: 0x00127BCA File Offset: 0x00125DCA
		internal ConnectionException(ObjectId objectId, string message, Exception innerException) : base(message, innerException)
		{
			this.objectId = objectId;
		}

		// Token: 0x17001424 RID: 5156
		// (get) Token: 0x0600457D RID: 17789 RVA: 0x00127BDB File Offset: 0x00125DDB
		public ObjectId ObjectId
		{
			get
			{
				return this.objectId;
			}
		}

		// Token: 0x0400260F RID: 9743
		private readonly ObjectId objectId;
	}
}
