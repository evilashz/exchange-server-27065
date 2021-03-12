using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200018B RID: 395
	internal class UndeleteContext
	{
		// Token: 0x06000CB9 RID: 3257 RVA: 0x0002E960 File Offset: 0x0002CB60
		internal UndeleteContext(StoreObjectId parentId, byte[] searchKey)
		{
			this.searchKey = searchKey;
			this.parentFolderId = parentId;
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x0002E976 File Offset: 0x0002CB76
		internal StoreObjectId ParentFolderId
		{
			get
			{
				return this.parentFolderId;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x0002E97E File Offset: 0x0002CB7E
		internal byte[] SearchKey
		{
			get
			{
				return this.searchKey;
			}
		}

		// Token: 0x040006C8 RID: 1736
		private StoreObjectId parentFolderId;

		// Token: 0x040006C9 RID: 1737
		private byte[] searchKey;
	}
}
