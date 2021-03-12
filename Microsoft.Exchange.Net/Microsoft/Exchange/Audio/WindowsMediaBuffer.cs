using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x02000621 RID: 1569
	internal class WindowsMediaBuffer : IDisposable
	{
		// Token: 0x06001C49 RID: 7241 RVA: 0x00033742 File Offset: 0x00031942
		internal WindowsMediaBuffer(INSSBuffer inssBuffer)
		{
			this.inssBuffer = inssBuffer;
			this.inssBuffer.GetBufferAndLength(out this.bufferPtr, out this.length);
			this.inssBuffer.GetMaxLength(out this.maxLength);
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06001C4A RID: 7242 RVA: 0x00033779 File Offset: 0x00031979
		internal uint Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06001C4B RID: 7243 RVA: 0x00033781 File Offset: 0x00031981
		internal uint Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x00033789 File Offset: 0x00031989
		public void Dispose()
		{
			Marshal.ReleaseComObject(this.inssBuffer);
			this.inssBuffer = null;
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x000337A0 File Offset: 0x000319A0
		internal int Read(byte[] buffer, int offset, int numBytes)
		{
			int num = 0;
			if (this.position < this.length)
			{
				long num2 = (long)((ulong)(this.length - this.position));
				IntPtr source = (IntPtr)(this.bufferPtr.ToInt64() + (long)((ulong)this.position));
				num = Math.Min(numBytes, (int)num2);
				num = Math.Min(num, buffer.Length);
				if (num > 0)
				{
					Marshal.Copy(source, buffer, offset, num);
					this.position += (uint)num;
				}
				else
				{
					num = 0;
				}
			}
			return num;
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x00033818 File Offset: 0x00031A18
		internal void Write(byte[] buffer, int count)
		{
			IntPtr destination = (IntPtr)(this.bufferPtr.ToInt64() + (long)((ulong)this.position));
			Marshal.Copy(buffer, 0, destination, count);
			this.position += (uint)count;
		}

		// Token: 0x04001CF3 RID: 7411
		private INSSBuffer inssBuffer;

		// Token: 0x04001CF4 RID: 7412
		private uint length;

		// Token: 0x04001CF5 RID: 7413
		private uint maxLength;

		// Token: 0x04001CF6 RID: 7414
		private IntPtr bufferPtr;

		// Token: 0x04001CF7 RID: 7415
		private uint position;
	}
}
