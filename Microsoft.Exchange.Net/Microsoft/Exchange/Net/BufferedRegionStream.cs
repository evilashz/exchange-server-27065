using System;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000BD2 RID: 3026
	internal class BufferedRegionStream : Stream, IDisposeTrackable, IDisposable
	{
		// Token: 0x06004167 RID: 16743 RVA: 0x000AD8DD File Offset: 0x000ABADD
		public BufferedRegionStream(Stream stream) : this(stream, false)
		{
		}

		// Token: 0x06004168 RID: 16744 RVA: 0x000AD8E8 File Offset: 0x000ABAE8
		public BufferedRegionStream(Stream stream, bool takeStreamOwnership)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException("Stream is not readable", "stream");
			}
			this.stream = stream;
			this.ownWrappedStream = takeStreamOwnership;
			this.disposeTracker = ((IDisposeTrackable)this).GetDisposeTracker();
		}

		// Token: 0x17001033 RID: 4147
		// (get) Token: 0x06004169 RID: 16745 RVA: 0x000AD93B File Offset: 0x000ABB3B
		public override bool CanRead
		{
			get
			{
				return !this.isDisposed;
			}
		}

		// Token: 0x17001034 RID: 4148
		// (get) Token: 0x0600416A RID: 16746 RVA: 0x000AD946 File Offset: 0x000ABB46
		public override bool CanSeek
		{
			get
			{
				return !this.isDisposed;
			}
		}

		// Token: 0x17001035 RID: 4149
		// (get) Token: 0x0600416B RID: 16747 RVA: 0x000AD951 File Offset: 0x000ABB51
		public override bool CanTimeout
		{
			get
			{
				return this.stream.CanTimeout;
			}
		}

		// Token: 0x17001036 RID: 4150
		// (get) Token: 0x0600416C RID: 16748 RVA: 0x000AD95E File Offset: 0x000ABB5E
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001037 RID: 4151
		// (get) Token: 0x0600416D RID: 16749 RVA: 0x000AD961 File Offset: 0x000ABB61
		public override long Length
		{
			get
			{
				this.CheckDisposed();
				return this.stream.Length;
			}
		}

		// Token: 0x0600416E RID: 16750 RVA: 0x000AD9F4 File Offset: 0x000ABBF4
		public static BufferedRegionStream CreateWithBufferPoolCollection(Stream stream, int maxBufferSize, bool takeStreamOwnership)
		{
			BufferedRegionStream bufferedRegionStream = null;
			bool flag = false;
			BufferedRegionStream result;
			try
			{
				BufferPool bufferPool = null;
				bufferedRegionStream = new BufferedRegionStream(stream, takeStreamOwnership);
				bufferedRegionStream.SetBufferedRegion(maxBufferSize, delegate(int size)
				{
					BufferPoolCollection.BufferSize bufferSize;
					if (!BufferPoolCollection.AutoCleanupCollection.TryMatchBufferSize(maxBufferSize, out bufferSize))
					{
						throw new InvalidOperationException(string.Format("Could not get buffer size of {0} for BufferedRegionStream buffer", maxBufferSize));
					}
					bufferPool = BufferPoolCollection.AutoCleanupCollection.Acquire(bufferSize);
					return bufferPool.Acquire();
				}, delegate(byte[] memory)
				{
					bufferPool.Release(memory);
				});
				flag = true;
				result = bufferedRegionStream;
			}
			finally
			{
				if (!flag && bufferedRegionStream != null)
				{
					bufferedRegionStream.Dispose();
				}
			}
			return result;
		}

		// Token: 0x17001038 RID: 4152
		// (get) Token: 0x0600416F RID: 16751 RVA: 0x000ADA78 File Offset: 0x000ABC78
		// (set) Token: 0x06004170 RID: 16752 RVA: 0x000ADA8C File Offset: 0x000ABC8C
		public override long Position
		{
			get
			{
				this.CheckDisposed();
				return this.stream.Position;
			}
			set
			{
				this.CheckDisposed();
				if (this.regionBuffer == null)
				{
					throw new InvalidOperationException("Not currently buffering");
				}
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("Position cannot be set to a negative value");
				}
				if (value > (long)this.regionBuffer.Length)
				{
					throw new ArgumentOutOfRangeException("Position set beyond the end of the buffer");
				}
				if (value > this.regionBufferPosition)
				{
					throw new ArgumentOutOfRangeException("Position set beyond furthest read");
				}
				this.regionBufferPosition = value;
			}
		}

		// Token: 0x17001039 RID: 4153
		// (get) Token: 0x06004171 RID: 16753 RVA: 0x000ADAF4 File Offset: 0x000ABCF4
		// (set) Token: 0x06004172 RID: 16754 RVA: 0x000ADB01 File Offset: 0x000ABD01
		public override int ReadTimeout
		{
			get
			{
				return this.stream.ReadTimeout;
			}
			set
			{
				this.stream.ReadTimeout = value;
			}
		}

		// Token: 0x1700103A RID: 4154
		// (get) Token: 0x06004173 RID: 16755 RVA: 0x000ADB0F File Offset: 0x000ABD0F
		// (set) Token: 0x06004174 RID: 16756 RVA: 0x000ADB1C File Offset: 0x000ABD1C
		public override int WriteTimeout
		{
			get
			{
				return this.stream.WriteTimeout;
			}
			set
			{
				this.stream.WriteTimeout = value;
			}
		}

		// Token: 0x06004175 RID: 16757 RVA: 0x000ADB60 File Offset: 0x000ABD60
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			this.CheckDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("offset and count run past end of buffer");
			}
			if (this.regionBuffer == null)
			{
				return this.stream.BeginRead(buffer, offset, count, callback, state);
			}
			if (count == 0)
			{
				IAsyncResult asyncResult3 = new BufferedRegionStream.BufferedAsyncResult(state, 0);
				if (callback != null)
				{
					callback(asyncResult3);
				}
				return asyncResult3;
			}
			if (this.regionBufferPosition < this.stream.Position)
			{
				long num = this.stream.Position - this.regionBufferPosition;
				if ((long)count < num)
				{
					num = (long)count;
				}
				Array.Copy(this.regionBuffer, this.regionBufferPosition, buffer, (long)offset, num);
				this.regionBufferPosition += num;
				IAsyncResult asyncResult2 = new BufferedRegionStream.BufferedAsyncResult(state, (int)num);
				if (callback != null)
				{
					callback(asyncResult2);
				}
				return asyncResult2;
			}
			AsyncCallback callback2 = null;
			if (callback != null)
			{
				callback2 = delegate(IAsyncResult asyncResult)
				{
					BufferedRegionStream.WrappedAsyncResult ar = new BufferedRegionStream.WrappedAsyncResult(asyncResult, buffer, offset);
					callback(ar);
				};
			}
			IAsyncResult wrappedResult = this.stream.BeginRead(buffer, offset, count, callback2, state);
			return new BufferedRegionStream.WrappedAsyncResult(wrappedResult, buffer, offset);
		}

		// Token: 0x06004176 RID: 16758 RVA: 0x000ADD14 File Offset: 0x000ABF14
		public override int EndRead(IAsyncResult asyncResult)
		{
			this.CheckDisposed();
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			BufferedRegionStream.BufferedAsyncResult bufferedAsyncResult = asyncResult as BufferedRegionStream.BufferedAsyncResult;
			if (bufferedAsyncResult != null)
			{
				return bufferedAsyncResult.BytesRead;
			}
			BufferedRegionStream.WrappedAsyncResult wrappedAsyncResult = asyncResult as BufferedRegionStream.WrappedAsyncResult;
			if (wrappedAsyncResult != null)
			{
				int num = this.stream.EndRead(wrappedAsyncResult.WrappedResult);
				if ((long)(this.regionBuffer.Length - num) < this.regionBufferPosition)
				{
					this.releaseAction(this.regionBuffer);
					this.regionBuffer = null;
				}
				else
				{
					Array.Copy(wrappedAsyncResult.Buffer, (long)wrappedAsyncResult.Offset, this.regionBuffer, this.regionBufferPosition, (long)num);
					this.regionBufferPosition += (long)num;
				}
				return num;
			}
			return this.stream.EndRead(asyncResult);
		}

		// Token: 0x06004177 RID: 16759 RVA: 0x000ADDCC File Offset: 0x000ABFCC
		public override void Flush()
		{
			this.CheckDisposed();
		}

		// Token: 0x06004178 RID: 16760 RVA: 0x000ADDD4 File Offset: 0x000ABFD4
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.CheckDisposed();
			if (count == 0)
			{
				return 0;
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("offset and count run past end of buffer");
			}
			if (this.regionBuffer == null)
			{
				return this.stream.Read(buffer, offset, count);
			}
			if (this.regionBufferPosition < this.stream.Position)
			{
				long num = this.stream.Position - this.regionBufferPosition;
				if ((long)count < num)
				{
					num = (long)count;
				}
				Array.Copy(this.regionBuffer, this.regionBufferPosition, buffer, (long)offset, num);
				this.regionBufferPosition += num;
				return (int)num;
			}
			int num2 = this.stream.Read(buffer, offset, count);
			if ((long)(this.regionBuffer.Length - num2) < this.regionBufferPosition)
			{
				this.releaseAction(this.regionBuffer);
				this.regionBuffer = null;
			}
			else
			{
				Array.Copy(buffer, (long)offset, this.regionBuffer, this.regionBufferPosition, (long)num2);
				this.regionBufferPosition += (long)num2;
			}
			return num2;
		}

		// Token: 0x06004179 RID: 16761 RVA: 0x000ADEF8 File Offset: 0x000AC0F8
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.CheckDisposed();
			throw new NotImplementedException();
		}

		// Token: 0x0600417A RID: 16762 RVA: 0x000ADF08 File Offset: 0x000AC108
		public void SetBufferedRegion(int regionSize, Func<int, byte[]> acquireFunc, Action<byte[]> releaseAction)
		{
			this.CheckDisposed();
			if (regionSize <= 0)
			{
				throw new ArgumentOutOfRangeException("regionSize");
			}
			if (acquireFunc == null)
			{
				throw new ArgumentNullException("acquireFunc");
			}
			if (releaseAction == null)
			{
				throw new ArgumentNullException("releaseAction");
			}
			if (this.stream.Position != 0L)
			{
				throw new InvalidOperationException("Can only set buffered region at the start of a stream");
			}
			this.regionBuffer = acquireFunc(regionSize);
			this.releaseAction = releaseAction;
		}

		// Token: 0x0600417B RID: 16763 RVA: 0x000ADF74 File Offset: 0x000AC174
		public override void SetLength(long value)
		{
			this.CheckDisposed();
			throw new NotSupportedException();
		}

		// Token: 0x0600417C RID: 16764 RVA: 0x000ADF81 File Offset: 0x000AC181
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.CheckDisposed();
			throw new NotSupportedException();
		}

		// Token: 0x0600417D RID: 16765 RVA: 0x000ADF8E File Offset: 0x000AC18E
		DisposeTracker IDisposeTrackable.GetDisposeTracker()
		{
			return DisposeTracker.Get<BufferedRegionStream>(this);
		}

		// Token: 0x0600417E RID: 16766 RVA: 0x000ADF96 File Offset: 0x000AC196
		void IDisposeTrackable.SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x0600417F RID: 16767 RVA: 0x000ADFAC File Offset: 0x000AC1AC
		protected override void Dispose(bool disposing)
		{
			if (this.regionBuffer != null)
			{
				this.releaseAction(this.regionBuffer);
				this.regionBuffer = null;
			}
			if (this.ownWrappedStream && this.stream != null)
			{
				this.stream.Dispose();
				this.stream = null;
			}
			base.Dispose(disposing);
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			this.isDisposed = true;
		}

		// Token: 0x06004180 RID: 16768 RVA: 0x000AE01C File Offset: 0x000AC21C
		protected void CheckDisposed()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x04003858 RID: 14424
		private readonly bool ownWrappedStream;

		// Token: 0x04003859 RID: 14425
		private Stream stream;

		// Token: 0x0400385A RID: 14426
		private DisposeTracker disposeTracker;

		// Token: 0x0400385B RID: 14427
		private bool isDisposed;

		// Token: 0x0400385C RID: 14428
		private byte[] regionBuffer;

		// Token: 0x0400385D RID: 14429
		private Action<byte[]> releaseAction;

		// Token: 0x0400385E RID: 14430
		private long regionBufferPosition;

		// Token: 0x02000BD3 RID: 3027
		private class BufferedAsyncResult : IAsyncResult
		{
			// Token: 0x06004181 RID: 16769 RVA: 0x000AE037 File Offset: 0x000AC237
			public BufferedAsyncResult(object asyncState, int bytesRead)
			{
				this.asyncState = asyncState;
				this.bytesRead = bytesRead;
			}

			// Token: 0x1700103B RID: 4155
			// (get) Token: 0x06004182 RID: 16770 RVA: 0x000AE04D File Offset: 0x000AC24D
			public object AsyncState
			{
				get
				{
					return this.asyncState;
				}
			}

			// Token: 0x1700103C RID: 4156
			// (get) Token: 0x06004183 RID: 16771 RVA: 0x000AE055 File Offset: 0x000AC255
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					if (this.asyncWaitHandle == null)
					{
						this.asyncWaitHandle = new ManualResetEvent(true);
					}
					return this.asyncWaitHandle;
				}
			}

			// Token: 0x1700103D RID: 4157
			// (get) Token: 0x06004184 RID: 16772 RVA: 0x000AE071 File Offset: 0x000AC271
			public bool CompletedSynchronously
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700103E RID: 4158
			// (get) Token: 0x06004185 RID: 16773 RVA: 0x000AE074 File Offset: 0x000AC274
			public bool IsCompleted
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700103F RID: 4159
			// (get) Token: 0x06004186 RID: 16774 RVA: 0x000AE077 File Offset: 0x000AC277
			public int BytesRead
			{
				get
				{
					return this.bytesRead;
				}
			}

			// Token: 0x0400385F RID: 14431
			private readonly object asyncState;

			// Token: 0x04003860 RID: 14432
			private readonly int bytesRead;

			// Token: 0x04003861 RID: 14433
			private WaitHandle asyncWaitHandle;
		}

		// Token: 0x02000BD4 RID: 3028
		private class WrappedAsyncResult : IAsyncResult
		{
			// Token: 0x06004187 RID: 16775 RVA: 0x000AE07F File Offset: 0x000AC27F
			public WrappedAsyncResult(IAsyncResult wrappedResult, byte[] buffer, int offset)
			{
				this.wrappedResult = wrappedResult;
				this.buffer = buffer;
				this.offset = offset;
			}

			// Token: 0x17001040 RID: 4160
			// (get) Token: 0x06004188 RID: 16776 RVA: 0x000AE09C File Offset: 0x000AC29C
			public object AsyncState
			{
				get
				{
					return this.wrappedResult.AsyncState;
				}
			}

			// Token: 0x17001041 RID: 4161
			// (get) Token: 0x06004189 RID: 16777 RVA: 0x000AE0A9 File Offset: 0x000AC2A9
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					return this.wrappedResult.AsyncWaitHandle;
				}
			}

			// Token: 0x17001042 RID: 4162
			// (get) Token: 0x0600418A RID: 16778 RVA: 0x000AE0B6 File Offset: 0x000AC2B6
			public bool CompletedSynchronously
			{
				get
				{
					return this.wrappedResult.CompletedSynchronously;
				}
			}

			// Token: 0x17001043 RID: 4163
			// (get) Token: 0x0600418B RID: 16779 RVA: 0x000AE0C3 File Offset: 0x000AC2C3
			public bool IsCompleted
			{
				get
				{
					return this.wrappedResult.IsCompleted;
				}
			}

			// Token: 0x17001044 RID: 4164
			// (get) Token: 0x0600418C RID: 16780 RVA: 0x000AE0D0 File Offset: 0x000AC2D0
			public byte[] Buffer
			{
				get
				{
					return this.buffer;
				}
			}

			// Token: 0x17001045 RID: 4165
			// (get) Token: 0x0600418D RID: 16781 RVA: 0x000AE0D8 File Offset: 0x000AC2D8
			public int Offset
			{
				get
				{
					return this.offset;
				}
			}

			// Token: 0x17001046 RID: 4166
			// (get) Token: 0x0600418E RID: 16782 RVA: 0x000AE0E0 File Offset: 0x000AC2E0
			public IAsyncResult WrappedResult
			{
				get
				{
					return this.wrappedResult;
				}
			}

			// Token: 0x04003862 RID: 14434
			private readonly IAsyncResult wrappedResult;

			// Token: 0x04003863 RID: 14435
			private readonly byte[] buffer;

			// Token: 0x04003864 RID: 14436
			private readonly int offset;
		}
	}
}
