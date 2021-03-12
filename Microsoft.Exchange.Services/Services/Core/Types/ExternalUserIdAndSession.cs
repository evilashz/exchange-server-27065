using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000780 RID: 1920
	internal class ExternalUserIdAndSession : IdAndSession
	{
		// Token: 0x0600395B RID: 14683 RVA: 0x000CB26E File Offset: 0x000C946E
		public ExternalUserIdAndSession(StoreId storeId, StoreSession session, Permission permission) : base(storeId, session)
		{
			this.permission = permission;
		}

		// Token: 0x0600395C RID: 14684 RVA: 0x000CB27F File Offset: 0x000C947F
		public ExternalUserIdAndSession(StoreId storeId, StoreSession session, IList<AttachmentId> attachmentIds, Permission permission) : base(storeId, session, attachmentIds)
		{
			this.permission = permission;
		}

		// Token: 0x0600395D RID: 14685 RVA: 0x000CB292 File Offset: 0x000C9492
		public ExternalUserIdAndSession(StoreId storeId, StoreId parentFolderId, StoreSession session, Permission permission) : base(storeId, parentFolderId, session)
		{
			this.permission = permission;
		}

		// Token: 0x0600395E RID: 14686 RVA: 0x000CB2A5 File Offset: 0x000C94A5
		public ExternalUserIdAndSession(StoreId storeId, StoreId parentFolderId, StoreSession session, IList<AttachmentId> attachmentIds, Permission permission) : base(storeId, parentFolderId, session, attachmentIds)
		{
			this.permission = permission;
		}

		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x0600395F RID: 14687 RVA: 0x000CB2BA File Offset: 0x000C94BA
		public Permission PermissionGranted
		{
			get
			{
				return this.permission;
			}
		}

		// Token: 0x04001FF5 RID: 8181
		private Permission permission;
	}
}
