using System;
using System.IO;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200029A RID: 666
	internal struct ByteBuffer
	{
		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x0600199B RID: 6555 RVA: 0x00095930 File Offset: 0x00093B30
		public int Length
		{
			get
			{
				return this.buffer.Length;
			}
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x0009593A File Offset: 0x00093B3A
		public ByteBuffer(int size)
		{
			this.buffer = new byte[size];
			this.offset = 0;
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x0009594F File Offset: 0x00093B4F
		public void SkipBytes(int count)
		{
			this.offset += count;
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x00095960 File Offset: 0x00093B60
		public uint ReadUInt32()
		{
			return (uint)((int)this.buffer[this.offset++] | ((int)this.buffer[this.offset++] | ((int)this.buffer[this.offset++] | (int)this.buffer[this.offset++] << 8) << 8) << 8);
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x000959D8 File Offset: 0x00093BD8
		public ushort ReadUInt16()
		{
			return (ushort)((int)this.buffer[this.offset++] | (int)this.buffer[this.offset++] << 8);
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x00095A1C File Offset: 0x00093C1C
		public void WriteUInt32(uint value)
		{
			this.buffer[this.offset++] = (byte)value;
			this.buffer[this.offset++] = (byte)(value >> 8);
			this.buffer[this.offset++] = (byte)(value >> 16);
			this.buffer[this.offset++] = (byte)(value >> 24);
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x00095A9C File Offset: 0x00093C9C
		public void WriteUInt16(ushort value)
		{
			this.buffer[this.offset++] = (byte)value;
			this.buffer[this.offset++] = (byte)(value >> 8);
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x00095ADF File Offset: 0x00093CDF
		public void WriteContentsTo(Stream writer)
		{
			writer.Write(this.buffer, 0, this.buffer.Length);
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x00095AF6 File Offset: 0x00093CF6
		public int ReadContentsFrom(Stream reader)
		{
			return reader.Read(this.buffer, 0, this.buffer.Length);
		}

		// Token: 0x040012C0 RID: 4800
		private byte[] buffer;

		// Token: 0x040012C1 RID: 4801
		private int offset;
	}
}
