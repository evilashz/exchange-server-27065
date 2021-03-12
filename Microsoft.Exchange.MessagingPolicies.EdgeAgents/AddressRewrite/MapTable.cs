using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.MessagingPolicies.AddressRewrite
{
	// Token: 0x02000012 RID: 18
	internal class MapTable
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00004004 File Offset: 0x00002204
		internal string[] Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000400C File Offset: 0x0000220C
		internal MapTable(string internalDomain, string externalDomain)
		{
			this.domain[0] = internalDomain;
			this.domain[1] = externalDomain;
			BlockArray<IntBlock> pointersArray = new BlockArray<IntBlock>();
			BlockArray<IntBlock> pointersArray2 = new BlockArray<IntBlock>();
			this.addressDataArray = new BlockArray<StringBlock>();
			this.internalIndex = new Index(pointersArray, this.addressDataArray);
			this.externalIndex = new Index(pointersArray2, this.addressDataArray);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00004078 File Offset: 0x00002278
		internal bool IsCorrectMapTable(string domain, MapTable.MapEntryType direction)
		{
			return string.Compare(domain, this.domain[(int)direction], StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000408C File Offset: 0x0000228C
		internal void AddEntry(string internalAddress, string externalAddress)
		{
			this.internalIndex.Add(internalAddress, externalAddress.Length + 1);
			this.externalIndex.Add(externalAddress, 0);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000040B0 File Offset: 0x000022B0
		internal string Remap(string address, MapTable.MapEntryType entryType)
		{
			Index index = (entryType == MapTable.MapEntryType.Internal) ? this.internalIndex : this.externalIndex;
			int num = index.BinarySearch(address);
			if (num == -1)
			{
				return null;
			}
			uint address2 = index[num];
			int offset = Macros.Offset(address2);
			StringBlock stringBlock = this.addressDataArray.Block(address2);
			int offset2;
			if (entryType == MapTable.MapEntryType.Internal)
			{
				offset2 = stringBlock.FindOffsetNextString(offset);
			}
			else
			{
				offset2 = stringBlock.FindOffsetPreviousString(offset);
			}
			byte[] bytes = new byte[256];
			int count;
			stringBlock.ReadStringUnsafe(offset2, ref bytes, out count);
			return Encoding.ASCII.GetString(bytes, 0, count);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000413E File Offset: 0x0000233E
		internal void Sort()
		{
			this.internalIndex.HeapSort();
			this.externalIndex.HeapSort();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00004156 File Offset: 0x00002356
		internal void Dump(List<string> internalIndex, List<string> externalIndex)
		{
			this.internalIndex.Dump(internalIndex);
			this.externalIndex.Dump(externalIndex);
		}

		// Token: 0x04000048 RID: 72
		private string[] domain = new string[2];

		// Token: 0x04000049 RID: 73
		private Index internalIndex;

		// Token: 0x0400004A RID: 74
		private Index externalIndex;

		// Token: 0x0400004B RID: 75
		private BlockArray<StringBlock> addressDataArray;

		// Token: 0x02000013 RID: 19
		internal enum MapEntryType
		{
			// Token: 0x0400004D RID: 77
			Internal,
			// Token: 0x0400004E RID: 78
			External
		}
	}
}
