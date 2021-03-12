using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.RpcClientAccess.Handler.StorageObjects;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x0200004C RID: 76
	internal sealed class RuleFolderIdConversion : PropertyConversion
	{
		// Token: 0x060002F1 RID: 753 RVA: 0x00018BD9 File Offset: 0x00016DD9
		internal RuleFolderIdConversion() : base(PropertyTag.RuleFolderFid, PropertyTag.RuleFolderEntryId)
		{
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00018BEB File Offset: 0x00016DEB
		protected override object ConvertValueFromClient(StoreSession session, IStorageObjectProperties storageObjectProperties, object propertyValue)
		{
			return ServerIdConverter.MakeEntryIdFromFid(session, (long)propertyValue);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00018BFC File Offset: 0x00016DFC
		protected override object ConvertValueToClient(StoreSession session, IStorageObjectProperties storageObjectProperties, object propertyValue)
		{
			StoreObjectId storeObjectId = StoreObjectId.FromProviderSpecificId((byte[])propertyValue);
			return session.IdConverter.GetFidFromId(storeObjectId);
		}
	}
}
