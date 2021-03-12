using System;
using System.IO;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000060 RID: 96
	internal struct ByteBuffer
	{
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0000C81C File Offset: 0x0000AA1C
		public int Length
		{
			get
			{
				return this.buffer.Length;
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000C826 File Offset: 0x0000AA26
		public ByteBuffer(int size)
		{
			this.buffer = new byte[size];
			this.offset = 0;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000C83B File Offset: 0x0000AA3B
		public void SkipBytes(int count)
		{
			this.offset += count;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000C84C File Offset: 0x0000AA4C
		public uint ReadUInt32()
		{
			return (uint)((int)this.buffer[this.offset++] | ((int)this.buffer[this.offset++] | ((int)this.buffer[this.offset++] | (int)this.buffer[this.offset++] << 8) << 8) << 8);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000C8C4 File Offset: 0x0000AAC4
		public ushort ReadUInt16()
		{
			return (ushort)((int)this.buffer[this.offset++] | (int)this.buffer[this.offset++] << 8);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000C908 File Offset: 0x0000AB08
		public void WriteUInt32(uint value)
		{
			this.buffer[this.offset++] = (byte)value;
			this.buffer[this.offset++] = (byte)(value >> 8);
			this.buffer[this.offset++] = (byte)(value >> 16);
			this.buffer[this.offset++] = (byte)(value >> 24);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000C988 File Offset: 0x0000AB88
		public void WriteUInt16(ushort value)
		{
			this.buffer[this.offset++] = (byte)value;
			this.buffer[this.offset++] = (byte)(value >> 8);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000C9CB File Offset: 0x0000ABCB
		public void WriteContentsTo(Stream writer)
		{
			writer.Write(this.buffer, 0, this.buffer.Length);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000C9E2 File Offset: 0x0000ABE2
		public int ReadContentsFrom(Stream reader)
		{
			return reader.Read(this.buffer, 0, this.buffer.Length);
		}

		// Token: 0x04000185 RID: 389
		private byte[] buffer;

		// Token: 0x04000186 RID: 390
		private int offset;
	}
}
