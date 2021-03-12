using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000082 RID: 130
	public static class IdSetUtilities
	{
		// Token: 0x060004CD RID: 1229 RVA: 0x0001D61C File Offset: 0x0001B81C
		internal static IdSet IdSetFromBytes(Context context, byte[] idsetBytes)
		{
			IdSet result;
			try
			{
				result = IdSetUtilities.ThrowableIdSetFromBytes(context, idsetBytes);
			}
			catch (StoreException exception)
			{
				context.OnExceptionCatch(exception);
				result = new IdSet();
			}
			return result;
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0001D654 File Offset: 0x0001B854
		internal static IdSet ThrowableIdSetFromBytes(Context context, byte[] idsetBytes)
		{
			IdSet result;
			using (Reader reader = Reader.CreateBufferReader(idsetBytes))
			{
				result = IdSetUtilities.ThrowableIdSetFromReader(context, reader);
			}
			return result;
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0001D690 File Offset: 0x0001B890
		internal static IdSet ThrowableIdSetFromStream(Context context, Stream readStream)
		{
			readStream.Position = 0L;
			IdSet result;
			using (Reader reader = Reader.CreateStreamReader(readStream))
			{
				result = IdSetUtilities.ThrowableIdSetFromReader(context, reader);
			}
			return result;
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0001D6D4 File Offset: 0x0001B8D4
		internal static IdSet ThrowableIdSetFromReader(Context context, Reader rcaReader)
		{
			IdSet result;
			try
			{
				IdSet idSet = IdSet.ParseWithReplGuids(rcaReader);
				if (rcaReader.Length != rcaReader.Position)
				{
					throw new StoreException((LID)30272U, ErrorCodeValue.CorruptData, "Serialized IDSet is corrupted");
				}
				result = idSet;
			}
			catch (BufferParseException ex)
			{
				context.OnExceptionCatch(ex);
				throw new StoreException((LID)30100U, ErrorCodeValue.CorruptData, "Serialized IDSet is corrupted", ex);
			}
			return result;
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0001D748 File Offset: 0x0001B948
		internal static byte[] BytesFromIdSet(IdSet idSet)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(100))
			{
				using (Writer writer = new StreamWriter(memoryStream))
				{
					idSet.SerializeWithReplGuids(writer);
				}
				result = memoryStream.ToArray();
			}
			return result;
		}
	}
}
