using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000045 RID: 69
	public static class FidMidListSerializer
	{
		// Token: 0x06000683 RID: 1667 RVA: 0x0003C8F4 File Offset: 0x0003AAF4
		public static List<FidMid> FromBytes(byte[] buffer, ref int offset, IReplidGuidMap replidGuidMap)
		{
			if (buffer == null)
			{
				return new List<FidMid>(0);
			}
			int dword = (int)ParseSerialize.GetDword(buffer, ref offset, buffer.Length);
			List<FidMid> list = new List<FidMid>(dword);
			for (int i = 0; i < dword; i++)
			{
				ExchangeId folderId = ExchangeId.CreateFrom26ByteArray(null, null, ParseSerialize.GetByteArray(buffer, ref offset, buffer.Length));
				ExchangeId messageId = ExchangeId.CreateFrom26ByteArray(null, null, ParseSerialize.GetByteArray(buffer, ref offset, buffer.Length));
				list.Add(new FidMid(folderId, messageId));
			}
			return list;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0003C960 File Offset: 0x0003AB60
		public static byte[] ToBytes(IList<FidMid> data)
		{
			if (data == null)
			{
				return new byte[4];
			}
			byte[] array = new byte[4 + data.Count * 28 * 2];
			int num = ParseSerialize.SerializeInt32(data.Count, array, 0);
			for (int i = 0; i < data.Count; i++)
			{
				FidMid fidMid = data[i];
				ParseSerialize.SetByteArray(array, ref num, fidMid.FolderId.To26ByteArray());
				ParseSerialize.SetByteArray(array, ref num, fidMid.MessageId.To26ByteArray());
			}
			return array;
		}

		// Token: 0x04000383 RID: 899
		private const int SizeOfSerializedExchangeId = 28;
	}
}
