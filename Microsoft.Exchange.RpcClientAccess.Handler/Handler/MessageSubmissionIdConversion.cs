using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.RpcClientAccess.Handler.StorageObjects;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000047 RID: 71
	internal sealed class MessageSubmissionIdConversion : PropertyConversion
	{
		// Token: 0x060002C1 RID: 705 RVA: 0x000180EC File Offset: 0x000162EC
		internal MessageSubmissionIdConversion() : base(PropertyTag.MessageSubmissionIdFromClient, PropertyTag.MessageSubmissionId)
		{
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x000180FE File Offset: 0x000162FE
		protected override object ConvertValueFromClient(StoreSession session, IStorageObjectProperties storageObjectProperties, object propertyValue)
		{
			return propertyValue;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00018101 File Offset: 0x00016301
		protected override object ConvertValueToClient(StoreSession session, IStorageObjectProperties storageObjectProperties, object propertyValue)
		{
			return propertyValue;
		}
	}
}
