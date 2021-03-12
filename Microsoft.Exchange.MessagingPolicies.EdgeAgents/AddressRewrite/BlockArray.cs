using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MessagingPolicies.AddressRewrite
{
	// Token: 0x0200001C RID: 28
	internal class BlockArray<BlockType> where BlockType : IBlock, new()
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00004F16 File Offset: 0x00003116
		internal int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000016 RID: 22
		internal BlockType this[int blockIndex]
		{
			get
			{
				return this.blocks[blockIndex];
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004F2C File Offset: 0x0000312C
		internal BlockType Block(uint address)
		{
			return this.blocks[Macros.BlockIndex(address)];
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004F40 File Offset: 0x00003140
		internal int FindBlockToAppendData(int toWrite)
		{
			BlockType blockType = (this.count == 0) ? default(BlockType) : this.blocks[this.count - 1];
			if (blockType == null || toWrite > blockType.Free)
			{
				blockType = Activator.CreateInstance<BlockType>();
				if (this.count >= this.blocks.Count)
				{
					this.blocks.Add(blockType);
				}
				if (toWrite > blockType.Free)
				{
					throw new OutOfMemoryException();
				}
				this.blocks[this.count++] = blockType;
			}
			return this.count - 1;
		}

		// Token: 0x04000066 RID: 102
		private int count;

		// Token: 0x04000067 RID: 103
		private List<BlockType> blocks = new List<BlockType>(BlockArray<BlockType>.NumBlocks);

		// Token: 0x04000068 RID: 104
		internal static int NumBlocks = 256;
	}
}
