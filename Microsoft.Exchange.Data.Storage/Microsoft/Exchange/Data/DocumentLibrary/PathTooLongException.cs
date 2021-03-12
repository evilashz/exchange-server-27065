using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006CB RID: 1739
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class PathTooLongException : DocumentLibraryException
	{
		// Token: 0x0600458E RID: 17806 RVA: 0x00127CBC File Offset: 0x00125EBC
		internal PathTooLongException(ObjectId objectId, string message, Exception innerException) : base(message, innerException)
		{
			this.objectId = objectId;
		}

		// Token: 0x1700142B RID: 5163
		// (get) Token: 0x0600458F RID: 17807 RVA: 0x00127CCD File Offset: 0x00125ECD
		public ObjectId ObjectId
		{
			get
			{
				return this.objectId;
			}
		}

		// Token: 0x04002616 RID: 9750
		private readonly ObjectId objectId;
	}
}
