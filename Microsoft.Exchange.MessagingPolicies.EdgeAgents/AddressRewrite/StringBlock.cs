using System;

namespace Microsoft.Exchange.MessagingPolicies.AddressRewrite
{
	// Token: 0x02000021 RID: 33
	internal class StringBlock : Block<byte>
	{
		// Token: 0x06000096 RID: 150 RVA: 0x0000547E File Offset: 0x0000367E
		public StringBlock() : base(StringBlock.BlockSize)
		{
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000548C File Offset: 0x0000368C
		internal int AppendUnsafe(string data)
		{
			int written = this.written;
			for (int i = 0; i < data.Length; i++)
			{
				this.buffer[this.written + i] = (byte)data[i];
			}
			this.buffer[this.written + data.Length] = 0;
			this.free -= data.Length + 1;
			this.written += data.Length + 1;
			return written;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00005508 File Offset: 0x00003708
		internal void ReadStringUnsafe(int offset, ref byte[] outBuffer, out int bytesRead)
		{
			int num = 0;
			while (this.buffer[offset + num] != 0)
			{
				outBuffer[num] = this.buffer[offset + num];
				num++;
			}
			bytesRead = num;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000553C File Offset: 0x0000373C
		internal int FindOffsetPreviousString(int offset)
		{
			offset -= 2;
			while (offset >= 0 && base[offset] != 0)
			{
				offset--;
			}
			return offset + 1;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000555A File Offset: 0x0000375A
		internal int FindOffsetNextString(int offset)
		{
			while (base[offset] != 0)
			{
				offset++;
			}
			return offset + 1;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000556F File Offset: 0x0000376F
		internal void GetDataReference(int offset, out byte[] buffer, out int length)
		{
			buffer = this.buffer;
			length = 0;
			while (this.buffer[offset + length] != 0)
			{
				length++;
			}
		}

		// Token: 0x04000071 RID: 113
		internal static int BlockSize = 131072;
	}
}
