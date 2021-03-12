using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.RpcClientAccess.Handler.StorageObjects;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000046 RID: 70
	internal sealed class LocalDirectoryEntryIdConversion : PropertyConversion
	{
		// Token: 0x060002BE RID: 702 RVA: 0x0001806D File Offset: 0x0001626D
		internal LocalDirectoryEntryIdConversion() : base(PropertyTag.LocalDirectoryEntryId, PropertyTag.LocalDirectoryEntryId)
		{
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00018080 File Offset: 0x00016280
		protected override object ConvertValueFromClient(StoreSession session, IStorageObjectProperties storageObjectProperties, object propertyValue)
		{
			byte[] array = propertyValue as byte[];
			if (array == null || array.Length != 16)
			{
				throw new RopExecutionException("Invalid size of fidmid", (ErrorCode)2147746075U);
			}
			return PropertyConversionHelper.ConvertFidMidPairToEntryId(session, array);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x000180B8 File Offset: 0x000162B8
		protected override object ConvertValueToClient(StoreSession session, IStorageObjectProperties storageObjectProperties, object propertyValue)
		{
			byte[] array = propertyValue as byte[];
			if (array == null)
			{
				throw new RopExecutionException(string.Format("entryId is not byte[]: {0}.", propertyValue), (ErrorCode)2147746055U);
			}
			return PropertyConversionHelper.ConvertEntryIdToFidMidPair(session, array);
		}
	}
}
