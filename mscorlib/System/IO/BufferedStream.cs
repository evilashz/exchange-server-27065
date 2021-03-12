using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x02000179 RID: 377
	[ComVisible(true)]
	public sealed class BufferedStream : Stream
	{
		// Token: 0x060016D6 RID: 5846 RVA: 0x0004898B File Offset: 0x00046B8B
		private BufferedStream()
		{
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x00048993 File Offset: 0x00046B93
		public BufferedStream(Stream stream) : this(stream, 4096)
		{
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x000489A4 File Offset: 0x00046BA4
		public BufferedStream(Stream stream, int bufferSize)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("ArgumentOutOfRange_MustBePositive", new object[]
				{
					"bufferSize"
				}));
			}
			this._stream = stream;
			this._bufferSize = bufferSize;
			if (!this._stream.CanRead && !this._stream.CanWrite)
			{
				__Error.StreamIsClosed();
			}
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x00048A19 File Offset: 0x00046C19
		private void EnsureNotClosed()
		{
			if (this._stream == null)
			{
				__Error.StreamIsClosed();
			}
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x00048A28 File Offset: 0x00046C28
		private void EnsureCanSeek()
		{
			if (!this._stream.CanSeek)
			{
				__Error.SeekNotSupported();
			}
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x00048A3C File Offset: 0x00046C3C
		private void EnsureCanRead()
		{
			if (!this._stream.CanRead)
			{
				__Error.ReadNotSupported();
			}
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x00048A50 File Offset: 0x00046C50
		private void EnsureCanWrite()
		{
			if (!this._stream.CanWrite)
			{
				__Error.WriteNotSupported();
			}
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x00048A64 File Offset: 0x00046C64
		private void EnsureBeginEndAwaitableAllocated()
		{
			if (this._beginEndAwaitable == null)
			{
				this._beginEndAwaitable = new BeginEndAwaitableAdapter();
			}
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x00048A7C File Offset: 0x00046C7C
		private void EnsureShadowBufferAllocated()
		{
			if (this._buffer.Length != this._bufferSize || this._bufferSize >= 81920)
			{
				return;
			}
			byte[] array = new byte[Math.Min(this._bufferSize + this._bufferSize, 81920)];
			Buffer.InternalBlockCopy(this._buffer, 0, array, 0, this._writePos);
			this._buffer = array;
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x00048ADF File Offset: 0x00046CDF
		private void EnsureBufferAllocated()
		{
			if (this._buffer == null)
			{
				this._buffer = new byte[this._bufferSize];
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060016E0 RID: 5856 RVA: 0x00048AFA File Offset: 0x00046CFA
		internal Stream UnderlyingStream
		{
			[FriendAccessAllowed]
			get
			{
				return this._stream;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060016E1 RID: 5857 RVA: 0x00048B02 File Offset: 0x00046D02
		internal int BufferSize
		{
			[FriendAccessAllowed]
			get
			{
				return this._bufferSize;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060016E2 RID: 5858 RVA: 0x00048B0A File Offset: 0x00046D0A
		public override bool CanRead
		{
			get
			{
				return this._stream != null && this._stream.CanRead;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060016E3 RID: 5859 RVA: 0x00048B21 File Offset: 0x00046D21
		public override bool CanWrite
		{
			get
			{
				return this._stream != null && this._stream.CanWrite;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060016E4 RID: 5860 RVA: 0x00048B38 File Offset: 0x00046D38
		public override bool CanSeek
		{
			get
			{
				return this._stream != null && this._stream.CanSeek;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060016E5 RID: 5861 RVA: 0x00048B4F File Offset: 0x00046D4F
		public override long Length
		{
			get
			{
				this.EnsureNotClosed();
				if (this._writePos > 0)
				{
					this.FlushWrite();
				}
				return this._stream.Length;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060016E6 RID: 5862 RVA: 0x00048B71 File Offset: 0x00046D71
		// (set) Token: 0x060016E7 RID: 5863 RVA: 0x00048BA0 File Offset: 0x00046DA0
		public override long Position
		{
			get
			{
				this.EnsureNotClosed();
				this.EnsureCanSeek();
				return this._stream.Position + (long)(this._readPos - this._readLen + this._writePos);
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				this.EnsureNotClosed();
				this.EnsureCanSeek();
				if (this._writePos > 0)
				{
					this.FlushWrite();
				}
				this._readPos = 0;
				this._readLen = 0;
				this._stream.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x00048C00 File Offset: 0x00046E00
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this._stream != null)
				{
					try
					{
						this.Flush();
					}
					finally
					{
						this._stream.Close();
					}
				}
			}
			finally
			{
				this._stream = null;
				this._buffer = null;
				this._lastSyncCompletedReadTask = null;
				base.Dispose(disposing);
			}
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x00048C68 File Offset: 0x00046E68
		public override void Flush()
		{
			this.EnsureNotClosed();
			if (this._writePos > 0)
			{
				this.FlushWrite();
				return;
			}
			if (this._readPos >= this._readLen)
			{
				if (this._stream.CanWrite || this._stream is BufferedStream)
				{
					this._stream.Flush();
				}
				this._writePos = (this._readPos = (this._readLen = 0));
				return;
			}
			if (!this._stream.CanSeek)
			{
				return;
			}
			this.FlushRead();
			if (this._stream.CanWrite || this._stream is BufferedStream)
			{
				this._stream.Flush();
			}
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x00048D11 File Offset: 0x00046F11
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCancellation<int>(cancellationToken);
			}
			this.EnsureNotClosed();
			return BufferedStream.FlushAsyncInternal(cancellationToken, this, this._stream, this._writePos, this._readPos, this._readLen);
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x00048D48 File Offset: 0x00046F48
		private static async Task FlushAsyncInternal(CancellationToken cancellationToken, BufferedStream _this, Stream stream, int writePos, int readPos, int readLen)
		{
			SemaphoreSlim sem = _this.EnsureAsyncActiveSemaphoreInitialized();
			await sem.WaitAsync().ConfigureAwait(false);
			try
			{
				if (writePos > 0)
				{
					await _this.FlushWriteAsync(cancellationToken).ConfigureAwait(false);
				}
				else if (readPos < readLen)
				{
					if (stream.CanSeek)
					{
						_this.FlushRead();
						if (stream.CanRead || stream is BufferedStream)
						{
							await stream.FlushAsync(cancellationToken).ConfigureAwait(false);
						}
					}
				}
				else if (stream.CanWrite || stream is BufferedStream)
				{
					await stream.FlushAsync(cancellationToken).ConfigureAwait(false);
				}
			}
			finally
			{
				sem.Release();
			}
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x00048DB7 File Offset: 0x00046FB7
		private void FlushRead()
		{
			if (this._readPos - this._readLen != 0)
			{
				this._stream.Seek((long)(this._readPos - this._readLen), SeekOrigin.Current);
			}
			this._readPos = 0;
			this._readLen = 0;
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x00048DF4 File Offset: 0x00046FF4
		private void ClearReadBufferBeforeWrite()
		{
			if (this._readPos == this._readLen)
			{
				this._readPos = (this._readLen = 0);
				return;
			}
			if (!this._stream.CanSeek)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_CannotWriteToBufferedStreamIfReadBufferCannotBeFlushed"));
			}
			this.FlushRead();
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x00048E43 File Offset: 0x00047043
		private void FlushWrite()
		{
			this._stream.Write(this._buffer, 0, this._writePos);
			this._writePos = 0;
			this._stream.Flush();
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x00048E70 File Offset: 0x00047070
		private async Task FlushWriteAsync(CancellationToken cancellationToken)
		{
			await this._stream.WriteAsync(this._buffer, 0, this._writePos, cancellationToken).ConfigureAwait(false);
			this._writePos = 0;
			await this._stream.FlushAsync(cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x00048EC0 File Offset: 0x000470C0
		private int ReadFromBuffer(byte[] array, int offset, int count)
		{
			int num = this._readLen - this._readPos;
			if (num == 0)
			{
				return 0;
			}
			if (num > count)
			{
				num = count;
			}
			Buffer.InternalBlockCopy(this._buffer, this._readPos, array, offset, num);
			this._readPos += num;
			return num;
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x00048F0C File Offset: 0x0004710C
		private int ReadFromBuffer(byte[] array, int offset, int count, out Exception error)
		{
			int result;
			try
			{
				error = null;
				result = this.ReadFromBuffer(array, offset, count);
			}
			catch (Exception ex)
			{
				error = ex;
				result = 0;
			}
			return result;
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x00048F44 File Offset: 0x00047144
		public override int Read([In] [Out] byte[] array, int offset, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - offset < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			this.EnsureNotClosed();
			this.EnsureCanRead();
			int num = this.ReadFromBuffer(array, offset, count);
			if (num == count)
			{
				return num;
			}
			int num2 = num;
			if (num > 0)
			{
				count -= num;
				offset += num;
			}
			this._readPos = (this._readLen = 0);
			if (this._writePos > 0)
			{
				this.FlushWrite();
			}
			if (count >= this._bufferSize)
			{
				return this._stream.Read(array, offset, count) + num2;
			}
			this.EnsureBufferAllocated();
			this._readLen = this._stream.Read(this._buffer, 0, this._bufferSize);
			num = this.ReadFromBuffer(array, offset, count);
			return num + num2;
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x0004904C File Offset: 0x0004724C
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (this._stream == null)
			{
				__Error.ReadNotSupported();
			}
			this.EnsureCanRead();
			int num = 0;
			SemaphoreSlim semaphoreSlim = base.EnsureAsyncActiveSemaphoreInitialized();
			Task task = semaphoreSlim.WaitAsync();
			if (task.Status == TaskStatus.RanToCompletion)
			{
				bool flag = true;
				try
				{
					Exception ex;
					num = this.ReadFromBuffer(buffer, offset, count, out ex);
					flag = (num == count || ex != null);
					if (flag)
					{
						Stream.SynchronousAsyncResult synchronousAsyncResult = (ex == null) ? new Stream.SynchronousAsyncResult(num, state) : new Stream.SynchronousAsyncResult(ex, state, false);
						if (callback != null)
						{
							callback(synchronousAsyncResult);
						}
						return synchronousAsyncResult;
					}
				}
				finally
				{
					if (flag)
					{
						semaphoreSlim.Release();
					}
				}
			}
			return this.BeginReadFromUnderlyingStream(buffer, offset + num, count - num, callback, state, num, task);
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x00049168 File Offset: 0x00047368
		private IAsyncResult BeginReadFromUnderlyingStream(byte[] buffer, int offset, int count, AsyncCallback callback, object state, int bytesAlreadySatisfied, Task semaphoreLockTask)
		{
			Task<int> task = this.ReadFromUnderlyingStreamAsync(buffer, offset, count, CancellationToken.None, bytesAlreadySatisfied, semaphoreLockTask, true);
			return TaskToApm.Begin(task, callback, state);
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x00049194 File Offset: 0x00047394
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			Stream.SynchronousAsyncResult synchronousAsyncResult = asyncResult as Stream.SynchronousAsyncResult;
			if (synchronousAsyncResult != null)
			{
				return Stream.SynchronousAsyncResult.EndRead(asyncResult);
			}
			return TaskToApm.End<int>(asyncResult);
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x000491C8 File Offset: 0x000473C8
		private Task<int> LastSyncCompletedReadTask(int val)
		{
			Task<int> task = this._lastSyncCompletedReadTask;
			if (task != null && task.Result == val)
			{
				return task;
			}
			task = Task.FromResult<int>(val);
			this._lastSyncCompletedReadTask = task;
			return task;
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x000491FC File Offset: 0x000473FC
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCancellation<int>(cancellationToken);
			}
			this.EnsureNotClosed();
			this.EnsureCanRead();
			int num = 0;
			SemaphoreSlim semaphoreSlim = base.EnsureAsyncActiveSemaphoreInitialized();
			Task task = semaphoreSlim.WaitAsync();
			if (task.Status == TaskStatus.RanToCompletion)
			{
				bool flag = true;
				try
				{
					Exception ex;
					num = this.ReadFromBuffer(buffer, offset, count, out ex);
					flag = (num == count || ex != null);
					if (flag)
					{
						return (ex == null) ? this.LastSyncCompletedReadTask(num) : Task.FromException<int>(ex);
					}
				}
				finally
				{
					if (flag)
					{
						semaphoreSlim.Release();
					}
				}
			}
			return this.ReadFromUnderlyingStreamAsync(buffer, offset + num, count - num, cancellationToken, num, task, false);
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x0004930C File Offset: 0x0004750C
		private async Task<int> ReadFromUnderlyingStreamAsync(byte[] array, int offset, int count, CancellationToken cancellationToken, int bytesAlreadySatisfied, Task semaphoreLockTask, bool useApmPattern)
		{
			await semaphoreLockTask.ConfigureAwait(false);
			int result;
			try
			{
				int num = this.ReadFromBuffer(array, offset, count);
				if (num == count)
				{
					result = bytesAlreadySatisfied + num;
				}
				else
				{
					if (num > 0)
					{
						count -= num;
						offset += num;
						bytesAlreadySatisfied += num;
					}
					int num2 = 0;
					this._readLen = num2;
					this._readPos = num2;
					if (this._writePos > 0)
					{
						await this.FlushWriteAsync(cancellationToken).ConfigureAwait(false);
					}
					if (count >= this._bufferSize)
					{
						if (useApmPattern)
						{
							this.EnsureBeginEndAwaitableAllocated();
							this._stream.BeginRead(array, offset, count, BeginEndAwaitableAdapter.Callback, this._beginEndAwaitable);
							int num3 = bytesAlreadySatisfied;
							Stream stream = this._stream;
							result = num3 + stream.EndRead(await this._beginEndAwaitable);
						}
						else
						{
							int num3 = bytesAlreadySatisfied;
							result = num3 + await this._stream.ReadAsync(array, offset, count, cancellationToken).ConfigureAwait(false);
						}
					}
					else
					{
						this.EnsureBufferAllocated();
						if (useApmPattern)
						{
							this.EnsureBeginEndAwaitableAllocated();
							this._stream.BeginRead(this._buffer, 0, this._bufferSize, BeginEndAwaitableAdapter.Callback, this._beginEndAwaitable);
							BufferedStream bufferedStream = this;
							int readLen = bufferedStream._readLen;
							Stream stream = this._stream;
							bufferedStream._readLen = stream.EndRead(await this._beginEndAwaitable);
							bufferedStream = null;
							stream = null;
						}
						else
						{
							BufferedStream bufferedStream = this;
							int readLen2 = bufferedStream._readLen;
							bufferedStream._readLen = await this._stream.ReadAsync(this._buffer, 0, this._bufferSize, cancellationToken).ConfigureAwait(false);
							bufferedStream = null;
						}
						result = bytesAlreadySatisfied + this.ReadFromBuffer(array, offset, count);
					}
				}
			}
			finally
			{
				base.EnsureAsyncActiveSemaphoreInitialized().Release();
			}
			return result;
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x00049390 File Offset: 0x00047590
		public override int ReadByte()
		{
			this.EnsureNotClosed();
			this.EnsureCanRead();
			if (this._readPos == this._readLen)
			{
				if (this._writePos > 0)
				{
					this.FlushWrite();
				}
				this.EnsureBufferAllocated();
				this._readLen = this._stream.Read(this._buffer, 0, this._bufferSize);
				this._readPos = 0;
			}
			if (this._readPos == this._readLen)
			{
				return -1;
			}
			byte[] buffer = this._buffer;
			int readPos = this._readPos;
			this._readPos = readPos + 1;
			return buffer[readPos];
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x0004941C File Offset: 0x0004761C
		private void WriteToBuffer(byte[] array, ref int offset, ref int count)
		{
			int num = Math.Min(this._bufferSize - this._writePos, count);
			if (num <= 0)
			{
				return;
			}
			this.EnsureBufferAllocated();
			Buffer.InternalBlockCopy(array, offset, this._buffer, this._writePos, num);
			this._writePos += num;
			count -= num;
			offset += num;
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x00049478 File Offset: 0x00047678
		private void WriteToBuffer(byte[] array, ref int offset, ref int count, out Exception error)
		{
			try
			{
				error = null;
				this.WriteToBuffer(array, ref offset, ref count);
			}
			catch (Exception ex)
			{
				error = ex;
			}
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x000494AC File Offset: 0x000476AC
		public override void Write(byte[] array, int offset, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - offset < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			this.EnsureNotClosed();
			this.EnsureCanWrite();
			if (this._writePos == 0)
			{
				this.ClearReadBufferBeforeWrite();
			}
			int num;
			bool flag;
			checked
			{
				num = this._writePos + count;
				flag = (num + count < this._bufferSize + this._bufferSize);
			}
			if (!flag)
			{
				if (this._writePos > 0)
				{
					if (num <= this._bufferSize + this._bufferSize && num <= 81920)
					{
						this.EnsureShadowBufferAllocated();
						Buffer.InternalBlockCopy(array, offset, this._buffer, this._writePos, count);
						this._stream.Write(this._buffer, 0, num);
						this._writePos = 0;
						return;
					}
					this._stream.Write(this._buffer, 0, this._writePos);
					this._writePos = 0;
				}
				this._stream.Write(array, offset, count);
				return;
			}
			this.WriteToBuffer(array, ref offset, ref count);
			if (this._writePos < this._bufferSize)
			{
				return;
			}
			this._stream.Write(this._buffer, 0, this._writePos);
			this._writePos = 0;
			this.WriteToBuffer(array, ref offset, ref count);
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x0004961C File Offset: 0x0004781C
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (this._stream == null)
			{
				__Error.ReadNotSupported();
			}
			this.EnsureCanWrite();
			SemaphoreSlim semaphoreSlim = base.EnsureAsyncActiveSemaphoreInitialized();
			Task task = semaphoreSlim.WaitAsync();
			if (task.Status == TaskStatus.RanToCompletion)
			{
				bool flag = true;
				try
				{
					if (this._writePos == 0)
					{
						this.ClearReadBufferBeforeWrite();
					}
					flag = (count < this._bufferSize - this._writePos);
					if (flag)
					{
						Exception ex;
						this.WriteToBuffer(buffer, ref offset, ref count, out ex);
						Stream.SynchronousAsyncResult synchronousAsyncResult = (ex == null) ? new Stream.SynchronousAsyncResult(state) : new Stream.SynchronousAsyncResult(ex, state, true);
						if (callback != null)
						{
							callback(synchronousAsyncResult);
						}
						return synchronousAsyncResult;
					}
				}
				finally
				{
					if (flag)
					{
						semaphoreSlim.Release();
					}
				}
			}
			return this.BeginWriteToUnderlyingStream(buffer, offset, count, callback, state, task);
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x00049740 File Offset: 0x00047940
		private IAsyncResult BeginWriteToUnderlyingStream(byte[] buffer, int offset, int count, AsyncCallback callback, object state, Task semaphoreLockTask)
		{
			Task task = this.WriteToUnderlyingStreamAsync(buffer, offset, count, CancellationToken.None, semaphoreLockTask, true);
			return TaskToApm.Begin(task, callback, state);
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x0004976C File Offset: 0x0004796C
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			Stream.SynchronousAsyncResult synchronousAsyncResult = asyncResult as Stream.SynchronousAsyncResult;
			if (synchronousAsyncResult != null)
			{
				Stream.SynchronousAsyncResult.EndWrite(asyncResult);
				return;
			}
			TaskToApm.End(asyncResult);
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x000497A0 File Offset: 0x000479A0
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCancellation<int>(cancellationToken);
			}
			this.EnsureNotClosed();
			this.EnsureCanWrite();
			SemaphoreSlim semaphoreSlim = base.EnsureAsyncActiveSemaphoreInitialized();
			Task task = semaphoreSlim.WaitAsync();
			if (task.Status == TaskStatus.RanToCompletion)
			{
				bool flag = true;
				try
				{
					if (this._writePos == 0)
					{
						this.ClearReadBufferBeforeWrite();
					}
					flag = (count < this._bufferSize - this._writePos);
					if (flag)
					{
						Exception ex;
						this.WriteToBuffer(buffer, ref offset, ref count, out ex);
						return (ex == null) ? Task.CompletedTask : Task.FromException(ex);
					}
				}
				finally
				{
					if (flag)
					{
						semaphoreSlim.Release();
					}
				}
			}
			return this.WriteToUnderlyingStreamAsync(buffer, offset, count, cancellationToken, task, false);
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x000498B8 File Offset: 0x00047AB8
		private async Task WriteToUnderlyingStreamAsync(byte[] array, int offset, int count, CancellationToken cancellationToken, Task semaphoreLockTask, bool useApmPattern)
		{
			await semaphoreLockTask.ConfigureAwait(false);
			try
			{
				if (this._writePos == 0)
				{
					this.ClearReadBufferBeforeWrite();
				}
				int totalUserBytes = checked(this._writePos + count);
				if (checked(totalUserBytes + count < this._bufferSize + this._bufferSize))
				{
					this.WriteToBuffer(array, ref offset, ref count);
					if (this._writePos >= this._bufferSize)
					{
						if (useApmPattern)
						{
							this.EnsureBeginEndAwaitableAllocated();
							this._stream.BeginWrite(this._buffer, 0, this._writePos, BeginEndAwaitableAdapter.Callback, this._beginEndAwaitable);
							Stream stream = this._stream;
							stream.EndWrite(await this._beginEndAwaitable);
							stream = null;
						}
						else
						{
							await this._stream.WriteAsync(this._buffer, 0, this._writePos, cancellationToken).ConfigureAwait(false);
						}
						this._writePos = 0;
						this.WriteToBuffer(array, ref offset, ref count);
					}
				}
				else
				{
					if (this._writePos > 0)
					{
						if (totalUserBytes <= this._bufferSize + this._bufferSize && totalUserBytes <= 81920)
						{
							this.EnsureShadowBufferAllocated();
							Buffer.InternalBlockCopy(array, offset, this._buffer, this._writePos, count);
							if (useApmPattern)
							{
								this.EnsureBeginEndAwaitableAllocated();
								this._stream.BeginWrite(this._buffer, 0, totalUserBytes, BeginEndAwaitableAdapter.Callback, this._beginEndAwaitable);
								Stream stream = this._stream;
								stream.EndWrite(await this._beginEndAwaitable);
								stream = null;
							}
							else
							{
								await this._stream.WriteAsync(this._buffer, 0, totalUserBytes, cancellationToken).ConfigureAwait(false);
							}
							this._writePos = 0;
							return;
						}
						if (useApmPattern)
						{
							this.EnsureBeginEndAwaitableAllocated();
							this._stream.BeginWrite(this._buffer, 0, this._writePos, BeginEndAwaitableAdapter.Callback, this._beginEndAwaitable);
							Stream stream = this._stream;
							stream.EndWrite(await this._beginEndAwaitable);
							stream = null;
						}
						else
						{
							await this._stream.WriteAsync(this._buffer, 0, this._writePos, cancellationToken).ConfigureAwait(false);
						}
						this._writePos = 0;
					}
					if (useApmPattern)
					{
						this.EnsureBeginEndAwaitableAllocated();
						this._stream.BeginWrite(array, offset, count, BeginEndAwaitableAdapter.Callback, this._beginEndAwaitable);
						Stream stream = this._stream;
						stream.EndWrite(await this._beginEndAwaitable);
						stream = null;
					}
					else
					{
						await this._stream.WriteAsync(array, offset, count, cancellationToken).ConfigureAwait(false);
					}
				}
			}
			finally
			{
				base.EnsureAsyncActiveSemaphoreInitialized().Release();
			}
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x00049930 File Offset: 0x00047B30
		public override void WriteByte(byte value)
		{
			this.EnsureNotClosed();
			if (this._writePos == 0)
			{
				this.EnsureCanWrite();
				this.ClearReadBufferBeforeWrite();
				this.EnsureBufferAllocated();
			}
			if (this._writePos >= this._bufferSize - 1)
			{
				this.FlushWrite();
			}
			byte[] buffer = this._buffer;
			int writePos = this._writePos;
			this._writePos = writePos + 1;
			buffer[writePos] = value;
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x0004998C File Offset: 0x00047B8C
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.EnsureNotClosed();
			this.EnsureCanSeek();
			if (this._writePos > 0)
			{
				this.FlushWrite();
				return this._stream.Seek(offset, origin);
			}
			if (this._readLen - this._readPos > 0 && origin == SeekOrigin.Current)
			{
				offset -= (long)(this._readLen - this._readPos);
			}
			long position = this.Position;
			long num = this._stream.Seek(offset, origin);
			this._readPos = (int)(num - (position - (long)this._readPos));
			if (0 <= this._readPos && this._readPos < this._readLen)
			{
				this._stream.Seek((long)(this._readLen - this._readPos), SeekOrigin.Current);
			}
			else
			{
				this._readPos = (this._readLen = 0);
			}
			return num;
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x00049A54 File Offset: 0x00047C54
		public override void SetLength(long value)
		{
			if (value < 0L)
			{
				throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NegFileSize"));
			}
			this.EnsureNotClosed();
			this.EnsureCanSeek();
			this.EnsureCanWrite();
			this.Flush();
			this._stream.SetLength(value);
		}

		// Token: 0x0400080F RID: 2063
		private const int _DefaultBufferSize = 4096;

		// Token: 0x04000810 RID: 2064
		private Stream _stream;

		// Token: 0x04000811 RID: 2065
		private byte[] _buffer;

		// Token: 0x04000812 RID: 2066
		private readonly int _bufferSize;

		// Token: 0x04000813 RID: 2067
		private int _readPos;

		// Token: 0x04000814 RID: 2068
		private int _readLen;

		// Token: 0x04000815 RID: 2069
		private int _writePos;

		// Token: 0x04000816 RID: 2070
		private BeginEndAwaitableAdapter _beginEndAwaitable;

		// Token: 0x04000817 RID: 2071
		private Task<int> _lastSyncCompletedReadTask;

		// Token: 0x04000818 RID: 2072
		private const int MaxShadowBufferSize = 81920;
	}
}
