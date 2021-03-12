using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.RpcClientAccess.Handler.StorageObjects;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000048 RID: 72
	internal sealed class ParentIdConversion : PropertyConversion
	{
		// Token: 0x060002C4 RID: 708 RVA: 0x00018104 File Offset: 0x00016304
		internal ParentIdConversion() : base(PropertyTag.ParentFid, PropertyTag.ParentEntryId)
		{
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00018116 File Offset: 0x00016316
		protected override object ConvertValueFromClient(StoreSession session, IStorageObjectProperties storageObjectProperties, object propertyValue)
		{
			return ServerIdConverter.MakeEntryIdFromFid(session, (long)propertyValue);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00018124 File Offset: 0x00016324
		protected override object ConvertValueToClient(StoreSession session, IStorageObjectProperties storageObjectProperties, object propertyValue)
		{
			StoreObjectId storeObjectId = StoreObjectId.FromProviderSpecificId((byte[])propertyValue);
			return session.IdConverter.GetFidFromId(storeObjectId);
		}
	}
}
