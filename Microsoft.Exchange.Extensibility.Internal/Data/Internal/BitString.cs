using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Internal
{
	// Token: 0x02000035 RID: 53
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct BitString
	{
		// Token: 0x06000208 RID: 520 RVA: 0x0000B7E5 File Offset: 0x000099E5
		internal BitString(byte[] buffer, int offset, int length)
		{
			this.bytes = new byte[length];
			Buffer.BlockCopy(buffer, offset, this.bytes, 0, length);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000B802 File Offset: 0x00009A02
		internal BitString(byte[] buffer, int offset, int length, bool last)
		{
			this.bytes = new byte[last ? length : (length + 1)];
			Buffer.BlockCopy(buffer, offset, this.bytes, last ? 0 : 1, length);
			if (!last)
			{
				this.bytes[0] = byte.MaxValue;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000B840 File Offset: 0x00009A40
		public int Length
		{
			get
			{
				if (this.bytes != null)
				{
					return this.bytes.Length;
				}
				return 0;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000B854 File Offset: 0x00009A54
		public int LengthBits
		{
			get
			{
				if (this.bytes == null)
				{
					return 0;
				}
				if (this.bytes.Length != 0)
				{
					return this.bytes.Length * 8 - (int)this.bytes[0];
				}
				return 0;
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000B880 File Offset: 0x00009A80
		internal void AddSegment(byte[] buffer, int offset, int length, bool last)
		{
			byte[] dst = new byte[this.bytes.Length + (last ? (length - 1) : length)];
			Buffer.BlockCopy(this.bytes, 0, dst, 0, this.bytes.Length);
			Buffer.BlockCopy(buffer, last ? (offset + 1) : offset, dst, this.bytes.Length, last ? (length - 1) : length);
			this.bytes = dst;
			if (last)
			{
				this.bytes[0] = buffer[0];
			}
		}

		// Token: 0x04000280 RID: 640
		private byte[] bytes;
	}
}
