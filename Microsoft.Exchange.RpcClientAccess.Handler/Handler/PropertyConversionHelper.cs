using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000049 RID: 73
	internal static class PropertyConversionHelper
	{
		// Token: 0x060002C7 RID: 711 RVA: 0x00018150 File Offset: 0x00016350
		public static byte[] ConvertFidMidPairToEntryId(StoreSession session, byte[] fidmid)
		{
			byte[] providerLevelItemId;
			using (BufferReader bufferReader = Reader.CreateBufferReader(fidmid))
			{
				StoreIdPair storeIdPair = StoreIdPair.Parse(bufferReader);
				IdConverter idConverter = session.IdConverter;
				try
				{
					StoreObjectId storeObjectId;
					if (storeIdPair.Second != StoreId.Empty)
					{
						storeObjectId = idConverter.CreateMessageId(storeIdPair.First, storeIdPair.Second);
					}
					else
					{
						storeObjectId = idConverter.CreateFolderId(storeIdPair.First);
					}
					providerLevelItemId = storeObjectId.ProviderLevelItemId;
				}
				catch (ObjectNotFoundException innerException)
				{
					throw new RopExecutionException("Invalid fidmid", (ErrorCode)2147746063U, innerException);
				}
				catch (CorruptDataException innerException2)
				{
					throw new RopExecutionException("Corrupt fidmid", (ErrorCode)2147746075U, innerException2);
				}
			}
			return providerLevelItemId;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00018224 File Offset: 0x00016424
		public static byte[] ConvertEntryIdToFidMidPair(StoreSession session, byte[] entryId)
		{
			long nativeId = 0L;
			long nativeId2 = 0L;
			try
			{
				StoreObjectId storeObjectId = StoreObjectId.FromProviderSpecificId(entryId);
				if (IdConverter.IsMessageId(storeObjectId))
				{
					nativeId = session.IdConverter.GetFidFromId(storeObjectId);
					nativeId2 = session.IdConverter.GetMidFromMessageId(storeObjectId);
				}
				else
				{
					if (!IdConverter.IsFolderId(storeObjectId))
					{
						throw new RopExecutionException("Corrupt entry id.", (ErrorCode)2147746075U);
					}
					nativeId = session.IdConverter.GetFidFromId(storeObjectId);
				}
			}
			catch (CorruptDataException innerException)
			{
				throw new RopExecutionException("Corrupt entry id.", (ErrorCode)2147746075U, innerException);
			}
			byte[] array = new byte[16];
			using (BufferWriter bufferWriter = new BufferWriter(array))
			{
				StoreIdPair storeIdPair = new StoreIdPair(new StoreId(nativeId), new StoreId(nativeId2));
				storeIdPair.Serialize(bufferWriter);
			}
			return array;
		}
	}
}
