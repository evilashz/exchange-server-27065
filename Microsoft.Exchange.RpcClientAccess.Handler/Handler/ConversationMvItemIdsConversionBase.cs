using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.RpcClientAccess.Handler.StorageObjects;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x0200003D RID: 61
	internal abstract class ConversationMvItemIdsConversionBase : PropertyConversion
	{
		// Token: 0x060002AA RID: 682 RVA: 0x00017D70 File Offset: 0x00015F70
		internal ConversationMvItemIdsConversionBase(PropertyTag clientPropertyTag, PropertyTag serverPropertyTag) : base(clientPropertyTag, serverPropertyTag)
		{
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00017D7C File Offset: 0x00015F7C
		protected override object ConvertValueFromClient(StoreSession session, IStorageObjectProperties storageObjectProperties, object propertyValue)
		{
			byte[][] array = propertyValue as byte[][];
			if (array == null)
			{
				throw new RopExecutionException(string.Format("ConversationItemIdsConversionBase.ConvertValueFromClient multi-value is not byte[][]: {0}.", propertyValue), (ErrorCode)2147746055U);
			}
			byte[][] array2 = new byte[array.Length][];
			for (int i = 0; i < array.Length; i++)
			{
				byte[] array3 = array[i];
				if (array3 == null || array3.Length != 16)
				{
					throw new RopExecutionException(string.Format("ConversationItemIdsConversionBase.ConvertValueFromClient value is invalid: {0}.", array3), (ErrorCode)2147746055U);
				}
				array2[i] = PropertyConversionHelper.ConvertFidMidPairToEntryId(session, array3);
			}
			return array2;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00017DF0 File Offset: 0x00015FF0
		protected override object ConvertValueToClient(StoreSession session, IStorageObjectProperties storageObjectProperties, object propertyValue)
		{
			byte[][] array = propertyValue as byte[][];
			if (array == null)
			{
				throw new RopExecutionException(string.Format("ConversationItemIdsConversionBase.ConvertValueToClient multi-value is not byte[][]: {0}.", propertyValue), (ErrorCode)2147746055U);
			}
			byte[][] array2 = new byte[array.Length][];
			for (int i = 0; i < array.Length; i++)
			{
				byte[] array3 = array[i];
				if (array3 == null)
				{
					throw new RopExecutionException("ConversationItemIdsConversionBase.ConvertValueToClient element value is null.", (ErrorCode)2147746055U);
				}
				array2[i] = PropertyConversionHelper.ConvertEntryIdToFidMidPair(session, array3);
			}
			return array2;
		}
	}
}
