using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MessagingPolicies.AddressRewrite
{
	// Token: 0x0200001E RID: 30
	internal class Index
	{
		// Token: 0x06000083 RID: 131 RVA: 0x000050EA File Offset: 0x000032EA
		internal Index(BlockArray<IntBlock> pointersArray, BlockArray<StringBlock> dataArray)
		{
			this.pointersArray = pointersArray;
			this.dataArray = dataArray;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00005100 File Offset: 0x00003300
		internal void Add(string data, int extraSpaceNeeded)
		{
			int blockIndex = this.dataArray.FindBlockToAppendData(data.Length + 1 + extraSpaceNeeded);
			int offset = this.dataArray[blockIndex].AppendUnsafe(data);
			uint data2 = Macros.Address(blockIndex, offset);
			blockIndex = this.pointersArray.FindBlockToAppendData(1);
			this.pointersArray[blockIndex].Add(data2);
			this.count++;
		}

		// Token: 0x17000017 RID: 23
		internal uint this[int index]
		{
			get
			{
				int blockIndex = index / IntBlock.BlockSize;
				int index2 = index % IntBlock.BlockSize;
				return this.pointersArray[blockIndex][index2];
			}
			set
			{
				int blockIndex = index / IntBlock.BlockSize;
				int index2 = index % IntBlock.BlockSize;
				this.pointersArray[blockIndex][index2] = value;
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000051CC File Offset: 0x000033CC
		internal DataReference Ptr(uint dataPointer)
		{
			int blockIndex = Macros.BlockIndex(dataPointer);
			int offset = Macros.Offset(dataPointer);
			byte[] buffer;
			int length;
			this.dataArray[blockIndex].GetDataReference(offset, out buffer, out length);
			return new DataReference(buffer, offset, length);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00005208 File Offset: 0x00003408
		internal int BinarySearch(string searchData)
		{
			if (this.count == 0)
			{
				return -1;
			}
			int num = -1;
			int num2 = this.count;
			DataReference dataReference = new DataReference(searchData);
			int num3;
			for (;;)
			{
				num3 = (num + num2) / 2;
				DataReference other = this.Ptr(this[num3]);
				int num4 = dataReference.CompareTo(other);
				if (num4 == 0)
				{
					break;
				}
				if (num4 < 0)
				{
					num2 = num3;
				}
				else if (num4 > 0)
				{
					num = num3;
				}
				if (num == num2 || num2 == num + 1)
				{
					return -1;
				}
			}
			return num3;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00005274 File Offset: 0x00003474
		internal void HeapSort()
		{
			this.BuildHeap();
			for (int i = this.count - 1; i >= 1; i--)
			{
				uint value = this[0];
				this[0] = this[i];
				this[i] = value;
				this.heapSize--;
				this.Heapify(0);
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000052D0 File Offset: 0x000034D0
		private void BuildHeap()
		{
			this.heapSize = this.count;
			for (int i = this.count / 2 - 1; i >= 0; i--)
			{
				this.Heapify(i);
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00005308 File Offset: 0x00003508
		private void Heapify(int i)
		{
			for (;;)
			{
				int num = Macros.Left(i);
				int num2 = Macros.Right(i);
				int num3 = i;
				if (num < this.heapSize)
				{
					DataReference dataReference = this.Ptr(this[num]);
					DataReference other = this.Ptr(this[i]);
					num3 = ((dataReference.CompareTo(other) > 0) ? num : i);
				}
				if (num2 < this.heapSize && this.Ptr(this[num2]).CompareTo(this.Ptr(this[num3])) > 0)
				{
					num3 = num2;
				}
				if (num3 == i)
				{
					break;
				}
				uint value = this[num3];
				this[num3] = this[i];
				this[i] = value;
				i = num3;
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000053BC File Offset: 0x000035BC
		internal void Dump(List<string> outputList)
		{
			for (int i = 0; i < this.count; i++)
			{
				uint dataPointer = this[i];
				outputList.Add(this.Ptr(dataPointer).ToString());
			}
		}

		// Token: 0x0400006C RID: 108
		private BlockArray<IntBlock> pointersArray;

		// Token: 0x0400006D RID: 109
		private BlockArray<StringBlock> dataArray;

		// Token: 0x0400006E RID: 110
		private int count;

		// Token: 0x0400006F RID: 111
		private int heapSize;
	}
}
