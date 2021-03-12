using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200028D RID: 653
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DefaultFolderData
	{
		// Token: 0x06001B3B RID: 6971 RVA: 0x0007DDAA File Offset: 0x0007BFAA
		public DefaultFolderData(StoreObjectId folderId, bool idInitialized, bool hasInitialized)
		{
			this.folderId = folderId;
			this.idInitialized = idInitialized;
			this.hasInitialized = hasInitialized;
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x0007DDC7 File Offset: 0x0007BFC7
		public DefaultFolderData(StoreObjectId folderId) : this(folderId, folderId != null, folderId != null)
		{
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x0007DDDE File Offset: 0x0007BFDE
		public DefaultFolderData(bool isInitialized)
		{
			this.hasInitialized = isInitialized;
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06001B3E RID: 6974 RVA: 0x0007DDED File Offset: 0x0007BFED
		public bool HasInitialized
		{
			get
			{
				return this.hasInitialized;
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06001B3F RID: 6975 RVA: 0x0007DDF5 File Offset: 0x0007BFF5
		public bool IdInitialized
		{
			get
			{
				return this.idInitialized;
			}
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06001B40 RID: 6976 RVA: 0x0007DDFD File Offset: 0x0007BFFD
		public StoreObjectId FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x040012EF RID: 4847
		private readonly bool hasInitialized;

		// Token: 0x040012F0 RID: 4848
		private readonly bool idInitialized;

		// Token: 0x040012F1 RID: 4849
		private readonly StoreObjectId folderId;
	}
}
