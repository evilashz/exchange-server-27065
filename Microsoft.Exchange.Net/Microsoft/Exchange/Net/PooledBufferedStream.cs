using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000012 RID: 18
	internal sealed class PooledBufferedStream : Stream, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00003106 File Offset: 0x00001306
		public PooledBufferedStream(Stream stream, BufferPool bufferPool, bool closeStream) : this(stream, bufferPool, bufferPool.BufferSize, closeStream)
		{
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003117 File Offset: 0x00001317
		public PooledBufferedStream(Stream stream, BufferPool bufferPool, int bufferSizeToUse, bool closeStream) : this(stream, bufferPool, bufferSizeToUse)
		{
			this.closeStream = closeStream;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000312A File Offset: 0x0000132A
		public PooledBufferedStream(Stream stream, BufferPoolCollection.BufferSize bufferSize) : this(stream, BufferPoolCollection.AutoCleanupCollection.Acquire(bufferSize))
		{
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000313E File Offset: 0x0000133E
		public PooledBufferedStream(Stream stream, BufferPool bufferPool) : this(stream, bufferPool, bufferPool.BufferSize)
		{
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003150 File Offset: 0x00001350
		public PooledBufferedStream(Stream stream, BufferPool bufferPool, int bufferSizeToUse)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (bufferPool == null)
			{
				throw new ArgumentNullException("bufferPool");
			}
			if (bufferPool.BufferSize < bufferSizeToUse)
			{
				throw new ArgumentException("Buffer size mismatch");
			}
			if (!stream.CanRead)
			{
				if (!stream.CanWrite)
				{
					throw new ArgumentException(NetException.ImmutableStream, "stream");
				}
				this.isWriteMode = true;
			}
			this.pool = bufferPool;
			this.bufferSize = bufferSizeToUse;
			this.internalStream = stream;
			if (this.internalStream.CanSeek)
			{
				this.position = this.internalStream.Position;
			}
			this.bufferOffset = this.position;
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003211 File Offset: 0x00001411
		public override bool CanRead
		{
			get
			{
				return this.internalStream.CanRead;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600005F RID: 95 RVA: 0x0000321E File Offset: 0x0000141E
		public override bool CanWrite
		{
			get
			{
				return this.internalStream.CanWrite;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000060 RID: 96 RVA: 0x0000322B File Offset: 0x0000142B
		public override bool CanSeek
		{
			get
			{
				return this.internalStream.CanSeek;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003238 File Offset: 0x00001438
		public override long Length
		{
			get
			{
				long length = this.internalStream.Length;
				long val = (this.bufferTopBorder != 0) ? (this.bufferOffset + (long)this.bufferTopBorder) : 0L;
				return Math.Max(length, val);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003273 File Offset: 0x00001473
		// (set) Token: 0x06000063 RID: 99 RVA: 0x0000328E File Offset: 0x0000148E
		public override long Position
		{
			get
			{
				if (!this.internalStream.CanSeek)
				{
					throw new NotSupportedException();
				}
				return this.position;
			}
			set
			{
				if (!this.internalStream.CanSeek)
				{
					throw new NotSupportedException();
				}
				this.position = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000032AA File Offset: 0x000014AA
		private bool HasDataBufferedForReading
		{
			get
			{
				return this.position < this.bufferOffset + (long)this.bufferTopBorder && this.position >= this.bufferOffset;
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000032D5 File Offset: 0x000014D5
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<PooledBufferedStream>(this);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000032DD File Offset: 0x000014DD
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000032F4 File Offset: 0x000014F4
		public Stream FlushAndTakeWrappedStream()
		{
			this.Flush();
			Stream result = this.internalStream;
			this.internalStream = null;
			return result;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003318 File Offset: 0x00001518
		public override void Write(byte[] source, int offset, int count)
		{
			this.EnsureWriting();
			int num = this.FlushIfPositionOutOfBuffer();
			if (num == 0 && count > this.bufferSize)
			{
				this.UpdateInternalStreamPositionForWriting();
				this.internalStream.Write(source, offset, count);
				this.position += (long)count;
				this.bufferOffset = this.position;
				return;
			}
			int num2 = Math.Min(this.bufferSize - num, count);
			this.EnsureBufferAcquired();
			Array.Copy(source, offset, this.buffer, num, num2);
			this.bufferTopBorder = Math.Max(num + num2, this.bufferTopBorder);
			this.position += (long)num2;
			this.FlushIfBufferFull();
			if (num2 < count)
			{
				this.Write(source, offset + num2, count - num2);
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000033CC File Offset: 0x000015CC
		public override int Read(byte[] destination, int offset, int count)
		{
			this.EnsureReading();
			if (!this.HasDataBufferedForReading)
			{
				this.UpdateInternalStreamPositionForReading();
				if (count >= this.bufferSize)
				{
					int num = this.internalStream.Read(destination, offset, count);
					this.position += (long)num;
					return num;
				}
				if (!this.FillBuffer())
				{
					return 0;
				}
			}
			int num2 = (int)(this.position - this.bufferOffset);
			int num3 = Math.Min(count, this.bufferTopBorder - num2);
			Array.Copy(this.buffer, num2, destination, offset, num3);
			this.position += (long)num3;
			return num3;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003460 File Offset: 0x00001660
		public override void SetLength(long value)
		{
			if (!this.internalStream.CanSeek)
			{
				throw new NotSupportedException();
			}
			this.EnsureWriting();
			this.FlushBuffer();
			if (this.internalStream.Position != this.position)
			{
				this.internalStream.Position = this.position;
			}
			this.internalStream.SetLength(value);
			this.position = this.internalStream.Position;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000034D0 File Offset: 0x000016D0
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (!this.internalStream.CanSeek)
			{
				throw new NotSupportedException();
			}
			switch (origin)
			{
			case SeekOrigin.Begin:
				this.position = offset;
				break;
			case SeekOrigin.Current:
				this.position += offset;
				break;
			case SeekOrigin.End:
				if (this.isWriteMode)
				{
					this.FlushBuffer();
				}
				this.internalStream.Seek(offset, SeekOrigin.End);
				this.position = this.internalStream.Position;
				break;
			}
			return this.position;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003552 File Offset: 0x00001752
		public override void Flush()
		{
			if (this.internalStream.CanWrite)
			{
				if (this.isWriteMode)
				{
					this.FlushBuffer();
				}
				this.internalStream.Flush();
				return;
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003580 File Offset: 0x00001780
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.isClosed = true;
					if (this.internalStream != null)
					{
						try
						{
							if (this.isWriteMode)
							{
								this.FlushBuffer();
							}
						}
						finally
						{
							if (this.closeStream)
							{
								this.internalStream.Dispose();
								this.internalStream = null;
							}
						}
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
				if (disposing && this.buffer != null && this.pool != null)
				{
					this.pool.Release(this.buffer);
					this.buffer = null;
					this.pool = null;
				}
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003638 File Offset: 0x00001838
		[Conditional("DEBUG")]
		private void CheckDisposed(string methodName)
		{
			if (this.isClosed)
			{
				throw new ObjectDisposedException(base.GetType().ToString(), methodName);
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003654 File Offset: 0x00001854
		private void EnsureBufferAcquired()
		{
			if (this.buffer == null)
			{
				this.buffer = this.pool.Acquire();
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003670 File Offset: 0x00001870
		private void EnsureReading()
		{
			if (!this.isWriteMode)
			{
				return;
			}
			if (!this.internalStream.CanRead)
			{
				throw new NotSupportedException();
			}
			if (this.bufferTopBorder != 0)
			{
				this.UpdateInternalStreamPositionForWriting();
				this.internalStream.Write(this.buffer, 0, this.bufferTopBorder);
			}
			this.isWriteMode = false;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000036C8 File Offset: 0x000018C8
		private void EnsureWriting()
		{
			if (this.isWriteMode)
			{
				return;
			}
			if (!this.internalStream.CanWrite || !this.internalStream.CanSeek)
			{
				throw new NotSupportedException();
			}
			this.bufferTopBorder = 0;
			this.bufferOffset = this.position;
			this.isWriteMode = true;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003718 File Offset: 0x00001918
		private void UpdateInternalStreamPositionForReading()
		{
			if (this.internalStream.CanSeek && this.internalStream.Position != this.position)
			{
				this.internalStream.Position = this.position;
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000374B File Offset: 0x0000194B
		private bool FillBuffer()
		{
			this.bufferOffset = this.position;
			this.EnsureBufferAcquired();
			this.bufferTopBorder = this.internalStream.Read(this.buffer, 0, this.bufferSize);
			return this.bufferTopBorder != 0;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003789 File Offset: 0x00001989
		private void UpdateInternalStreamPositionForWriting()
		{
			if (this.internalStream.CanSeek && this.internalStream.Position != this.bufferOffset)
			{
				this.internalStream.Position = this.bufferOffset;
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000037BC File Offset: 0x000019BC
		private void FlushBuffer()
		{
			if (this.bufferTopBorder != 0)
			{
				this.UpdateInternalStreamPositionForWriting();
				this.internalStream.Write(this.buffer, 0, this.bufferTopBorder);
				this.bufferTopBorder = 0;
			}
			this.bufferOffset = this.position;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000037F7 File Offset: 0x000019F7
		private int FlushIfPositionOutOfBuffer()
		{
			if (this.position < this.bufferOffset || this.position > this.bufferOffset + (long)this.bufferTopBorder)
			{
				this.FlushBuffer();
			}
			return (int)(this.position - this.bufferOffset);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003831 File Offset: 0x00001A31
		private void FlushIfBufferFull()
		{
			if (this.bufferTopBorder == this.bufferSize)
			{
				this.FlushBuffer();
			}
		}

		// Token: 0x04000045 RID: 69
		private readonly int bufferSize;

		// Token: 0x04000046 RID: 70
		private readonly DisposeTracker disposeTracker;

		// Token: 0x04000047 RID: 71
		private readonly bool closeStream = true;

		// Token: 0x04000048 RID: 72
		private BufferPool pool;

		// Token: 0x04000049 RID: 73
		private byte[] buffer;

		// Token: 0x0400004A RID: 74
		private Stream internalStream;

		// Token: 0x0400004B RID: 75
		private bool isClosed;

		// Token: 0x0400004C RID: 76
		private long position;

		// Token: 0x0400004D RID: 77
		private long bufferOffset;

		// Token: 0x0400004E RID: 78
		private int bufferTopBorder;

		// Token: 0x0400004F RID: 79
		private bool isWriteMode;
	}
}
