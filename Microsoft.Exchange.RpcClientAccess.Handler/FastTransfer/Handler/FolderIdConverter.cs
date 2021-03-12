using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler
{
	// Token: 0x02000044 RID: 68
	internal sealed class FolderIdConverter : IdConverter
	{
		// Token: 0x060002BA RID: 698 RVA: 0x00018023 File Offset: 0x00016223
		internal FolderIdConverter() : base(PropertyTag.Fid, CoreFolderSchema.Id)
		{
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00018035 File Offset: 0x00016235
		protected override long CreateClientId(StoreSession session, StoreId id)
		{
			return session.IdConverter.GetFidFromId(StoreId.GetStoreObjectId(id));
		}
	}
}
