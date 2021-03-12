using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Diagnostics;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000013 RID: 19
	public sealed class PooledMemoryStream : Stream, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000078 RID: 120 RVA: 0x00003848 File Offset: 0x00001A48
		public PooledMemoryStream(int size)
		{
			if (size < 0)
			{
				throw new ArgumentOutOfRangeException("size", size, NetException.NegativeParameter);
			}
			this.isOpen = true;
			this.startingSize = PooledMemoryStream.GetCapacityToUse(size);
			this.currentBuffer = PooledMemoryStream.empty;
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000038A4 File Offset: 0x00001AA4
		public override bool CanRead
		{
			get
			{
				return this.isOpen;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000038AC File Offset: 0x00001AAC
		public override bool CanSeek
		{
			get
			{
				return this.isOpen;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000038B4 File Offset: 0x00001AB4
		public override bool CanWrite
		{
			get
			{
				return this.isOpen;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600007C RID: 124 RVA: 0x000038BC File Offset: 0x00001ABC
		// (set) Token: 0x0600007D RID: 125 RVA: 0x000038C4 File Offset: 0x00001AC4
		public int Capacity
		{
			get
			{
				return this.capacity;
			}
			set
			{
				this.TryIncreaseCapacity(value);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000038CE File Offset: 0x00001ACE
		public override long Length
		{
			get
			{
				return (long)this.length;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000038D7 File Offset: 0x00001AD7
		// (set) Token: 0x06000080 RID: 128 RVA: 0x000038E0 File Offset: 0x00001AE0
		public override long Position
		{
			get
			{
				return (long)this.position;
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", value, NetException.NegativeParameter);
				}
				if (value > 2147483647L)
				{
					throw new ArgumentOutOfRangeException("value", value, NetException.LargeParameter);
				}
				this.position = (int)value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003939 File Offset: 0x00001B39
		internal BufferPool CurrentBufferPool
		{
			get
			{
				return this.currentPool;
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003941 File Offset: 0x00001B41
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<PooledMemoryStream>(this);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003949 File Offset: 0x00001B49
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000395E File Offset: 0x00001B5E
		public override void Flush()
		{
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003960 File Offset: 0x00001B60
		public byte[] GetBuffer()
		{
			return this.currentBuffer;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003968 File Offset: 0x00001B68
		public override int Read(byte[] buffer, int offset, int count)
		{
			PooledMemoryStream.CheckBufferArguments(buffer, offset, count);
			int num = this.length - this.position;
			if (num > count)
			{
				num = count;
			}
			if (num > 0)
			{
				Buffer.BlockCopy(this.currentBuffer, this.position, buffer, offset, num);
				this.position += num;
			}
			else
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000039BC File Offset: 0x00001BBC
		public override int ReadByte()
		{
			if (this.position >= this.length)
			{
				return -1;
			}
			return (int)this.currentBuffer[this.position++];
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000039F4 File Offset: 0x00001BF4
		public override long Seek(long offset, SeekOrigin origin)
		{
			PooledMemoryStream.CheckForLargeBufferIndex(offset);
			switch (origin)
			{
			case SeekOrigin.Begin:
				PooledMemoryStream.CheckForNegativeBufferIndex(offset);
				this.position = (int)offset;
				break;
			case SeekOrigin.Current:
			{
				long num = offset + (long)this.position;
				PooledMemoryStream.CheckForLargeBufferIndex(num);
				PooledMemoryStream.CheckForNegativeBufferIndex(num);
				this.position = (int)num;
				break;
			}
			case SeekOrigin.End:
			{
				long num = (long)this.length + offset;
				PooledMemoryStream.CheckForLargeBufferIndex(num);
				PooledMemoryStream.CheckForNegativeBufferIndex(num);
				this.position = (int)num;
				break;
			}
			default:
				throw new ArgumentException(NetException.SeekOrigin, "origin");
			}
			return (long)this.position;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003A8C File Offset: 0x00001C8C
		public override void SetLength(long value)
		{
			if (value > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("value", value, NetException.LargeParameter);
			}
			if (value < 0L)
			{
				throw new ArgumentOutOfRangeException("value", value, NetException.NegativeParameter);
			}
			int num = (int)value;
			if (!this.TryIncreaseCapacity(num) && num > this.length)
			{
				Array.Clear(this.currentBuffer, this.length, num - this.length);
			}
			this.length = num;
			if (this.position > num)
			{
				this.position = num;
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003B24 File Offset: 0x00001D24
		public byte[] ToArray()
		{
			byte[] array = new byte[this.length];
			Buffer.BlockCopy(this.currentBuffer, 0, array, 0, this.length);
			return array;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003B54 File Offset: 0x00001D54
		public override void Write(byte[] buffer, int offset, int count)
		{
			PooledMemoryStream.CheckBufferArguments(buffer, offset, count);
			long num = (long)(this.position + count);
			PooledMemoryStream.CheckForLargeBufferIndex(num);
			PooledMemoryStream.CheckForNegativeBufferIndex(num);
			int num2 = (int)num;
			if (num > (long)this.length)
			{
				bool flag = (num <= (long)this.capacity || !this.TryIncreaseCapacity(num2)) && this.position > this.length;
				if (flag)
				{
					Array.Clear(this.currentBuffer, this.length, num2 - this.length);
				}
				this.length = num2;
			}
			Buffer.BlockCopy(buffer, offset, this.currentBuffer, this.position, count);
			this.position = num2;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003BF0 File Offset: 0x00001DF0
		public override void WriteByte(byte value)
		{
			if (this.position >= this.length)
			{
				int num = this.position + 1;
				bool flag = this.position > this.length;
				if (num >= this.capacity && this.TryIncreaseCapacity(num))
				{
					flag = false;
				}
				if (flag)
				{
					Array.Clear(this.currentBuffer, this.length, this.position - this.length);
				}
				this.length = num;
			}
			this.currentBuffer[this.position++] = value;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003C78 File Offset: 0x00001E78
		public void WriteTo(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			stream.Write(this.currentBuffer, 0, this.length);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003C9C File Offset: 0x00001E9C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.isOpen = false;
					if (this.currentBuffer != null && this.currentPool != null)
					{
						this.currentPool.Release(this.currentBuffer);
					}
					if (this.disposeTracker != null)
					{
						this.disposeTracker.Dispose();
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003D04 File Offset: 0x00001F04
		private static void CheckForNegativeBufferIndex(long index)
		{
			if (index < 0L)
			{
				throw new IOException(NetException.NegativeIndex);
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003D1B File Offset: 0x00001F1B
		private static void CheckForLargeBufferIndex(long index)
		{
			if (index > 2147483647L)
			{
				throw new IOException(NetException.LargeIndex);
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003D38 File Offset: 0x00001F38
		private static void CheckBufferArguments(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", offset, NetException.NegativeParameter);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", count, NetException.NegativeParameter);
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(NetException.BufferOverflow);
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003DAC File Offset: 0x00001FAC
		private static int GetCapacityToUse(int minimumCapacity)
		{
			if (minimumCapacity < 0)
			{
				throw new ArgumentOutOfRangeException("minimumCapacity", minimumCapacity, NetException.NegativeCapacity);
			}
			int num = 256;
			while (num < 2147483647 && num < minimumCapacity)
			{
				if (num < 0)
				{
					num = int.MaxValue;
					break;
				}
				num *= 2;
			}
			return num;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003E00 File Offset: 0x00002000
		private bool TryIncreaseCapacity(int value)
		{
			if (value < 0)
			{
				throw new IOException(NetException.NegativeCapacity);
			}
			int num = Math.Max(value, this.startingSize);
			bool flag = num > this.capacity;
			if (flag)
			{
				this.SetCapacity(PooledMemoryStream.GetCapacityToUse(num));
			}
			return flag;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003E48 File Offset: 0x00002048
		private void SetCapacity(int value)
		{
			if (value != this.capacity)
			{
				if (value < this.length)
				{
					throw new ArgumentOutOfRangeException("value", value, NetException.SmallCapacity);
				}
				ExTraceGlobals.CommonTracer.TraceWarning<int, int>((long)this.GetHashCode(), "PooledMemoryStream.SetCapacity({0}) called for a stream that had a buffer with capacity = {1}.", value, this.capacity);
				if (value > 0)
				{
					BufferPoolCollection.BufferSize bufferSize;
					BufferPool bufferPool;
					byte[] array;
					if (PooledMemoryStream.pool.TryMatchBufferSize(value, out bufferSize))
					{
						bufferPool = PooledMemoryStream.pool.Acquire(bufferSize);
						array = bufferPool.Acquire();
					}
					else
					{
						bufferPool = null;
						array = new byte[value];
					}
					if (this.length > 0)
					{
						Buffer.BlockCopy(this.currentBuffer, 0, array, 0, this.length);
					}
					this.ReplaceCurrentBuffer(array);
					this.currentPool = bufferPool;
				}
				else
				{
					this.ReplaceCurrentBuffer(null);
				}
				this.capacity = value;
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003F0E File Offset: 0x0000210E
		private void ReplaceCurrentBuffer(byte[] newBuffer)
		{
			if (this.currentBuffer != null && this.currentPool != null)
			{
				this.currentPool.Release(this.currentBuffer);
			}
			this.currentBuffer = newBuffer;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003F38 File Offset: 0x00002138
		[Conditional("DEBUG")]
		private void CheckDisposed()
		{
			if (!this.isOpen)
			{
				throw new ObjectDisposedException(NetException.ClosedStream);
			}
		}

		// Token: 0x04000050 RID: 80
		public const int MaximumBufferSize = 2147483647;

		// Token: 0x04000051 RID: 81
		private static readonly byte[] empty = new byte[0];

		// Token: 0x04000052 RID: 82
		private static readonly BufferPoolCollection pool = BufferPoolCollection.AutoCleanupCollection;

		// Token: 0x04000053 RID: 83
		private readonly int startingSize;

		// Token: 0x04000054 RID: 84
		private readonly DisposeTracker disposeTracker;

		// Token: 0x04000055 RID: 85
		private byte[] currentBuffer;

		// Token: 0x04000056 RID: 86
		private BufferPool currentPool;

		// Token: 0x04000057 RID: 87
		private bool isOpen;

		// Token: 0x04000058 RID: 88
		private int capacity;

		// Token: 0x04000059 RID: 89
		private int length;

		// Token: 0x0400005A RID: 90
		private int position;
	}
}
