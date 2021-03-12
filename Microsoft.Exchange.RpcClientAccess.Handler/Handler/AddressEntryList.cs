using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000006 RID: 6
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class AddressEntryList
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002434 File Offset: 0x00000634
		public static AddressEntryList Parse(byte[] entryIdBlob, Encoding string8Encoding)
		{
			AddressEntryList result;
			using (Reader reader = Reader.CreateBufferReader(entryIdBlob))
			{
				result = AddressEntryList.InternalParse(reader, string8Encoding);
			}
			return result;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000259B File Offset: 0x0000079B
		public byte[] Serialize()
		{
			return AddressEntryId.ToBytes(delegate(Writer writer)
			{
				writer.WriteUInt32((uint)this.internalList.Count);
				writer.WriteSizedBlock(delegate
				{
					int num = 0;
					using (IEnumerator<AddressEntryId> enumerator = this.internalList.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							AddressEntryId entryId = enumerator.Current;
							uint size = writer.WriteSizedBlock(delegate
							{
								entryId.Serialize(writer);
							});
							if (num < this.internalList.Count - 1)
							{
								writer.WriteBytes(new byte[AddressEntryList.PaddingSize(size)]);
							}
							num++;
						}
					}
				});
			});
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000025B0 File Offset: 0x000007B0
		public void SetUnicode()
		{
			foreach (AddressEntryId addressEntryId in this.internalList)
			{
				addressEntryId.SetUnicode();
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000025FC File Offset: 0x000007FC
		public void SetString8(Encoding string8Encoding)
		{
			foreach (AddressEntryId addressEntryId in this.internalList)
			{
				addressEntryId.SetString8(string8Encoding);
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000264C File Offset: 0x0000084C
		private static uint PaddingSize(uint size)
		{
			uint num = size & 3U;
			if (num != 0U)
			{
				return 4U - num;
			}
			return 0U;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002668 File Offset: 0x00000868
		private static AddressEntryList InternalParse(Reader reader, Encoding string8Encoding)
		{
			uint num = reader.ReadUInt32();
			uint num2 = reader.ReadUInt32();
			if (num * 4U > num2 || (ulong)num2 != (ulong)(reader.Length - 8L))
			{
				throw new BufferParseException(string.Format("The total size of the AddressEntryList is not correct. Entry count = {0}, Size = {1}, Length = {2}.", num, num2, reader.Length));
			}
			AddressEntryList addressEntryList = new AddressEntryList();
			for (uint num3 = 0U; num3 < num; num3 += 1U)
			{
				uint num4 = reader.ReadUInt32();
				if (AddressBookEntryId.IsAddressBookEntryId(reader, num4))
				{
					addressEntryList.Add(AddressEntryId.Parse(reader, string8Encoding, num4));
				}
				else
				{
					addressEntryList.Add(OneOffEntryId.Parse(reader, string8Encoding, num4));
				}
				if (num3 != num - 1U)
				{
					reader.ReadArraySegment(AddressEntryList.PaddingSize(num4));
				}
			}
			return addressEntryList;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002716 File Offset: 0x00000916
		public void Add(AddressEntryId item)
		{
			this.internalList.Add(item);
		}

		// Token: 0x0400000E RID: 14
		private readonly IList<AddressEntryId> internalList = new List<AddressEntryId>();
	}
}
