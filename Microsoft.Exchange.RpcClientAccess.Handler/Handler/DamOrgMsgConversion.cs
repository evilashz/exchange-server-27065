using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.RpcClientAccess.Handler.StorageObjects;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000040 RID: 64
	internal sealed class DamOrgMsgConversion : PropertyConversion
	{
		// Token: 0x060002AF RID: 687 RVA: 0x00017E7A File Offset: 0x0001607A
		internal DamOrgMsgConversion() : base(PropertyTag.DamOrgMsgServerId, PropertyTag.DamOrgMsgEntryId)
		{
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00017E8C File Offset: 0x0001608C
		protected override object ConvertValueFromClient(StoreSession session, IStorageObjectProperties storageObjectProperties, object propertyValue)
		{
			return ServerIdConverter.MakeEntryIdFromServerId(session, (byte[])propertyValue);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00017E9A File Offset: 0x0001609A
		protected override object ConvertValueToClient(StoreSession session, IStorageObjectProperties storageObjectProperties, object propertyValue)
		{
			return ServerIdConverter.MakeServerIdFromEntryId(session, (byte[])propertyValue);
		}
	}
}
