using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000BD1 RID: 3025
	internal class BufferBuilder
	{
		// Token: 0x06004155 RID: 16725 RVA: 0x000AD5B8 File Offset: 0x000AB7B8
		internal BufferBuilder() : this(256)
		{
		}

		// Token: 0x06004156 RID: 16726 RVA: 0x000AD5C5 File Offset: 0x000AB7C5
		internal BufferBuilder(int capacity)
		{
			this.buffer = new byte[capacity];
		}

		// Token: 0x17001030 RID: 4144
		// (get) Token: 0x06004157 RID: 16727 RVA: 0x000AD5D9 File Offset: 0x000AB7D9
		internal int Capacity
		{
			get
			{
				return this.buffer.Length;
			}
		}

		// Token: 0x17001031 RID: 4145
		// (get) Token: 0x06004158 RID: 16728 RVA: 0x000AD5E3 File Offset: 0x000AB7E3
		internal int Length
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x17001032 RID: 4146
		// (get) Token: 0x06004159 RID: 16729 RVA: 0x000AD5EB File Offset: 0x000AB7EB
		// (set) Token: 0x0600415A RID: 16730 RVA: 0x000AD5F3 File Offset: 0x000AB7F3
		internal bool Secure
		{
			get
			{
				return this.secure;
			}
			set
			{
				if (!value)
				{
					throw new InvalidOperationException();
				}
				this.secure = value;
			}
		}

		// Token: 0x0600415B RID: 16731 RVA: 0x000AD605 File Offset: 0x000AB805
		public override string ToString()
		{
			if (this.secure)
			{
				throw new InvalidOperationException();
			}
			return Encoding.ASCII.GetString(this.buffer, 0, this.offset);
		}

		// Token: 0x0600415C RID: 16732 RVA: 0x000AD62C File Offset: 0x000AB82C
		internal void Append(byte value)
		{
			this.EnsureBuffer(1);
			this.buffer[this.offset++] = value;
		}

		// Token: 0x0600415D RID: 16733 RVA: 0x000AD659 File Offset: 0x000AB859
		internal void Append(byte[] value)
		{
			this.Append(value, 0, value.Length);
		}

		// Token: 0x0600415E RID: 16734 RVA: 0x000AD666 File Offset: 0x000AB866
		internal void Append(byte[] value, int offset, int count)
		{
			this.EnsureBuffer(count);
			Buffer.BlockCopy(value, offset, this.buffer, this.offset, count);
			this.offset += count;
		}

		// Token: 0x0600415F RID: 16735 RVA: 0x000AD691 File Offset: 0x000AB891
		internal void Append(string value)
		{
			this.Append(value, 0, value.Length);
		}

		// Token: 0x06004160 RID: 16736 RVA: 0x000AD6A4 File Offset: 0x000AB8A4
		internal void Append(string value, int offset, int count)
		{
			this.EnsureBuffer(count);
			for (int i = 0; i < count; i++)
			{
				char c = value[offset + i];
				if (c > 'ÿ')
				{
					throw new ArgumentException(NetException.StringContainsInvalidCharacters, "value");
				}
				this.buffer[this.offset + i] = (byte)c;
			}
			this.offset += count;
		}

		// Token: 0x06004161 RID: 16737 RVA: 0x000AD70C File Offset: 0x000AB90C
		internal void Append(SecureString value)
		{
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				this.secure = true;
				this.EnsureBuffer(value.Length);
				intPtr = Marshal.SecureStringToCoTaskMemUnicode(value);
				for (int i = 0; i < value.Length; i++)
				{
					char c = (char)Marshal.ReadInt16(intPtr, i * Marshal.SizeOf(typeof(short)));
					if (c > 'ÿ')
					{
						throw new ArgumentException(NetException.StringContainsInvalidCharacters, "value");
					}
					this.Append((byte)c);
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.ZeroFreeCoTaskMemUnicode(intPtr);
				}
			}
		}

		// Token: 0x06004162 RID: 16738 RVA: 0x000AD7B0 File Offset: 0x000AB9B0
		internal byte[] GetBuffer()
		{
			return this.buffer;
		}

		// Token: 0x06004163 RID: 16739 RVA: 0x000AD7B8 File Offset: 0x000AB9B8
		internal void RemoveUnusedBufferSpace()
		{
			if (this.offset != this.buffer.Length)
			{
				byte[] dst = new byte[this.offset];
				Buffer.BlockCopy(this.buffer, 0, dst, 0, this.offset);
				if (this.secure)
				{
					Array.Clear(this.buffer, 0, this.buffer.Length);
				}
				this.buffer = dst;
			}
		}

		// Token: 0x06004164 RID: 16740 RVA: 0x000AD818 File Offset: 0x000ABA18
		internal void Reset()
		{
			this.offset = 0;
			if (this.secure)
			{
				Array.Clear(this.buffer, 0, this.buffer.Length);
				this.secure = false;
			}
		}

		// Token: 0x06004165 RID: 16741 RVA: 0x000AD844 File Offset: 0x000ABA44
		internal void SetLength(int newLength)
		{
			if (newLength > this.buffer.Length)
			{
				this.EnsureBuffer(newLength - this.buffer.Length);
			}
			this.offset = newLength;
		}

		// Token: 0x06004166 RID: 16742 RVA: 0x000AD868 File Offset: 0x000ABA68
		protected void EnsureBuffer(int count)
		{
			if (count > this.buffer.Length - this.offset)
			{
				byte[] dst = new byte[Math.Max(this.buffer.Length * 2, this.buffer.Length + count)];
				Buffer.BlockCopy(this.buffer, 0, dst, 0, this.offset);
				if (this.secure)
				{
					Array.Clear(this.buffer, 0, this.buffer.Length);
				}
				this.buffer = dst;
			}
		}

		// Token: 0x04003855 RID: 14421
		private byte[] buffer;

		// Token: 0x04003856 RID: 14422
		private int offset;

		// Token: 0x04003857 RID: 14423
		private bool secure;
	}
}
