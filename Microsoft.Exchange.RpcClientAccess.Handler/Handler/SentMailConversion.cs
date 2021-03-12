using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.RpcClientAccess.Handler.StorageObjects;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x0200004D RID: 77
	internal sealed class SentMailConversion : PropertyConversion
	{
		// Token: 0x060002F4 RID: 756 RVA: 0x00018C26 File Offset: 0x00016E26
		internal SentMailConversion() : base(PropertyTag.SentMailServerId, PropertyTag.SentMailEntryId)
		{
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00018C38 File Offset: 0x00016E38
		protected override object ConvertValueFromClient(StoreSession session, IStorageObjectProperties storageObjectProperties, object propertyValue)
		{
			return ServerIdConverter.MakeEntryIdFromServerId(session, (byte[])propertyValue);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00018C46 File Offset: 0x00016E46
		protected override object ConvertValueToClient(StoreSession session, IStorageObjectProperties storageObjectProperties, object propertyValue)
		{
			return ServerIdConverter.MakeServerIdFromEntryId(session, (byte[])propertyValue);
		}
	}
}
