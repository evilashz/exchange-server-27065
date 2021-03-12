using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000060 RID: 96
	public static class ExchangeIdListHelpers
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000378 RID: 888 RVA: 0x00018D49 File Offset: 0x00016F49
		public static IList<ExchangeId> EmptyList
		{
			get
			{
				return ExchangeIdListHelpers.emptyList;
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00018D50 File Offset: 0x00016F50
		public static IList<ExchangeId> ListFromBytes(Context context, IReplidGuidMap replidGuidMap, byte[] buff, ref int pos)
		{
			int num = 0;
			if (buff != null)
			{
				num = (int)ParseSerialize.GetDword(buff, ref pos, buff.Length);
				ParseSerialize.CheckCount((uint)num, 8, buff.Length - pos);
			}
			IList<ExchangeId> result = ExchangeIdListHelpers.EmptyList;
			if (num != 0)
			{
				ExchangeId[] array = new ExchangeId[num];
				for (int i = 0; i < num; i++)
				{
					long qword = (long)ParseSerialize.GetQword(buff, ref pos, buff.Length);
					array[i] = ExchangeId.CreateFromInt64(context, replidGuidMap, qword);
				}
				result = array;
			}
			return result;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00018DBC File Offset: 0x00016FBC
		public static byte[] BytesFromList(IList<ExchangeId> data, bool returnNullIfEmpty)
		{
			if (data != null && data.Count != 0)
			{
				byte[] array = new byte[4 + data.Count * 8];
				ParseSerialize.SerializeInt32(data.Count, array, 0);
				int num = 4;
				int i = 0;
				while (i < data.Count)
				{
					ParseSerialize.SerializeInt64(data[i].ToLong(), array, num);
					i++;
					num += 8;
				}
				return array;
			}
			if (!returnNullIfEmpty)
			{
				return ExchangeIdListHelpers.emptySerializedList;
			}
			return null;
		}

		// Token: 0x04000313 RID: 787
		private static readonly ExchangeId[] emptyList = new ExchangeId[0];

		// Token: 0x04000314 RID: 788
		private static readonly byte[] emptySerializedList = new byte[4];
	}
}
