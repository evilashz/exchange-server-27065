using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Common.Cache;

namespace Microsoft.Exchange.Common.Net
{
	// Token: 0x02000695 RID: 1685
	internal class MultiByteArrayMemoryStream : Stream
	{
		// Token: 0x06001EA2 RID: 7842 RVA: 0x0003955B File Offset: 0x0003775B
		public override void Flush()
		{
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x00039560 File Offset: 0x00037760
		public override long Seek(long offset, SeekOrigin origin)
		{
			long num;
			switch (origin)
			{
			case SeekOrigin.Begin:
				num = offset;
				break;
			case SeekOrigin.Current:
				num = this.position + offset;
				break;
			case SeekOrigin.End:
				num = this.length + offset;
				break;
			default:
				throw new InvalidOperationException("Unexpected SeekOrigin " + origin);
			}
			if (num < 0L)
			{
				throw new InvalidOperationException(string.Format("Unexpected offset {0} is specified with SeekOrigin {1}. Current position is {2}. Current length is {3}", new object[]
				{
					offset,
					origin,
					this.position,
					this.length
				}));
			}
			if (num > this.length)
			{
				if (num - this.length > 2000L)
				{
					throw new InvalidOperationException(string.Format("Cannot seek forward more than 2000 positions. Seek offset requested is {0}. Seek origin requested is {1}", offset, origin));
				}
				this.IncreaseCapacity(num);
			}
			this.position = num;
			return this.position;
		}

		// Token: 0x06001EA4 RID: 7844 RVA: 0x00039648 File Offset: 0x00037848
		private void IncreaseCapacity(long capacity)
		{
			while (capacity > (long)(this.inMemoryBackingStorage.Count * MultiByteArrayMemoryStream.BufferSize))
			{
				this.inMemoryBackingStorage.Add(MultiByteArrayMemoryStream.cache.GetBuffer(MultiByteArrayMemoryStream.BufferSize));
			}
			this.length = Math.Max(capacity, this.length);
		}

		// Token: 0x06001EA5 RID: 7845 RVA: 0x00039698 File Offset: 0x00037898
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001EA6 RID: 7846 RVA: 0x000396A0 File Offset: 0x000378A0
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.CheckArguments(buffer, offset, count);
			for (int i = offset; i < offset + count; i++)
			{
				if (this.position == this.length)
				{
					return i - offset;
				}
				buffer[i] = this.ReadByteAtCurrentPosition();
			}
			return count;
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x000396E0 File Offset: 0x000378E0
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.CheckArguments(buffer, offset, count);
			this.IncreaseCapacity(this.position + (long)count);
			int num2;
			for (int i = offset; i < offset + count; i += num2)
			{
				byte[] byteArrayForCurrentPosition = this.GetByteArrayForCurrentPosition();
				long num = this.position % (long)MultiByteArrayMemoryStream.BufferSize;
				long val = (long)MultiByteArrayMemoryStream.BufferSize - num;
				num2 = (int)Math.Min(val, (long)(count - (i - offset)));
				Buffer.BlockCopy(buffer, i, byteArrayForCurrentPosition, (int)num, num2);
				this.position += (long)num2;
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06001EA8 RID: 7848 RVA: 0x0003975D File Offset: 0x0003795D
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06001EA9 RID: 7849 RVA: 0x00039760 File Offset: 0x00037960
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06001EAA RID: 7850 RVA: 0x00039763 File Offset: 0x00037963
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06001EAB RID: 7851 RVA: 0x00039766 File Offset: 0x00037966
		public override long Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06001EAC RID: 7852 RVA: 0x0003976E File Offset: 0x0003796E
		// (set) Token: 0x06001EAD RID: 7853 RVA: 0x00039776 File Offset: 0x00037976
		public override long Position
		{
			get
			{
				return this.position;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x0003977D File Offset: 0x0003797D
		public override int ReadByte()
		{
			if (this.position == this.length)
			{
				return -1;
			}
			return (int)this.ReadByteAtCurrentPosition();
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x00039795 File Offset: 0x00037995
		public override void WriteByte(byte value)
		{
			this.IncreaseCapacity(this.position + 1L);
			this.WriteByteAtCurrentPosition(value);
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x000397B0 File Offset: 0x000379B0
		public override void Close()
		{
			lock (this)
			{
				if (this.inMemoryBackingStorage != null)
				{
					foreach (BufferCacheEntry bufferCacheEntry in this.inMemoryBackingStorage)
					{
						MultiByteArrayMemoryStream.cache.ReturnBuffer(bufferCacheEntry);
					}
					this.inMemoryBackingStorage.Clear();
					this.inMemoryBackingStorage = null;
				}
			}
			base.Close();
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x0003984C File Offset: 0x00037A4C
		private void WriteByteAtCurrentPosition(byte value)
		{
			byte[] byteArrayForCurrentPosition = this.GetByteArrayForCurrentPosition();
			byteArrayForCurrentPosition[(int)(checked((IntPtr)(this.position % unchecked((long)MultiByteArrayMemoryStream.BufferSize))))] = value;
			this.position += 1L;
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x00039880 File Offset: 0x00037A80
		private void CheckArguments(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (buffer.Length < offset + count)
			{
				throw new InvalidOperationException(string.Format("Unexpected buffer size {0}. Expected a buffer with size {1} or more", buffer.Length, offset + count));
			}
			if (this.position > this.length)
			{
				throw new InvalidOperationException(string.Format("Position {0} cannot be greater than Length {1}", this.position, this.length));
			}
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x000398F8 File Offset: 0x00037AF8
		private byte[] GetByteArrayForCurrentPosition()
		{
			if (this.position >= this.length)
			{
				throw new InvalidOperationException(string.Format("Position {0} cannot be greater than or equal to length {1}", this.position, this.length));
			}
			return this.inMemoryBackingStorage[(int)this.position / MultiByteArrayMemoryStream.BufferSize].Buffer;
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x00039958 File Offset: 0x00037B58
		private byte ReadByteAtCurrentPosition()
		{
			byte[] byteArrayForCurrentPosition = this.GetByteArrayForCurrentPosition();
			byte result = byteArrayForCurrentPosition[(int)(checked((IntPtr)(this.position % unchecked((long)MultiByteArrayMemoryStream.BufferSize))))];
			this.position += 1L;
			return result;
		}

		// Token: 0x04001E6A RID: 7786
		private static readonly BufferCache cache = new BufferCache(500);

		// Token: 0x04001E6B RID: 7787
		private static readonly int BufferSize = BufferCache.OneKiloByteBufferSize;

		// Token: 0x04001E6C RID: 7788
		private List<BufferCacheEntry> inMemoryBackingStorage = new List<BufferCacheEntry>();

		// Token: 0x04001E6D RID: 7789
		private long length;

		// Token: 0x04001E6E RID: 7790
		private long position;
	}
}
