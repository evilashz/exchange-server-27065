using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x020001A1 RID: 417
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class Stream : MarshalByRefObject, IDisposable
	{
		// Token: 0x0600196D RID: 6509 RVA: 0x000549C3 File Offset: 0x00052BC3
		internal SemaphoreSlim EnsureAsyncActiveSemaphoreInitialized()
		{
			return LazyInitializer.EnsureInitialized<SemaphoreSlim>(ref this._asyncActiveSemaphore, () => new SemaphoreSlim(1, 1));
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x0600196E RID: 6510
		[__DynamicallyInvokable]
		public abstract bool CanRead { [__DynamicallyInvokable] get; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x0600196F RID: 6511
		[__DynamicallyInvokable]
		public abstract bool CanSeek { [__DynamicallyInvokable] get; }

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06001970 RID: 6512 RVA: 0x000549EF File Offset: 0x00052BEF
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public virtual bool CanTimeout
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06001971 RID: 6513
		[__DynamicallyInvokable]
		public abstract bool CanWrite { [__DynamicallyInvokable] get; }

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06001972 RID: 6514
		[__DynamicallyInvokable]
		public abstract long Length { [__DynamicallyInvokable] get; }

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06001973 RID: 6515
		// (set) Token: 0x06001974 RID: 6516
		[__DynamicallyInvokable]
		public abstract long Position { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06001975 RID: 6517 RVA: 0x000549F2 File Offset: 0x00052BF2
		// (set) Token: 0x06001976 RID: 6518 RVA: 0x00054A03 File Offset: 0x00052C03
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public virtual int ReadTimeout
		{
			[__DynamicallyInvokable]
			get
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TimeoutsNotSupported"));
			}
			[__DynamicallyInvokable]
			set
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TimeoutsNotSupported"));
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06001977 RID: 6519 RVA: 0x00054A14 File Offset: 0x00052C14
		// (set) Token: 0x06001978 RID: 6520 RVA: 0x00054A25 File Offset: 0x00052C25
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public virtual int WriteTimeout
		{
			[__DynamicallyInvokable]
			get
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TimeoutsNotSupported"));
			}
			[__DynamicallyInvokable]
			set
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TimeoutsNotSupported"));
			}
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x00054A36 File Offset: 0x00052C36
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task CopyToAsync(Stream destination)
		{
			return this.CopyToAsync(destination, 81920);
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x00054A44 File Offset: 0x00052C44
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task CopyToAsync(Stream destination, int bufferSize)
		{
			return this.CopyToAsync(destination, bufferSize, CancellationToken.None);
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x00054A54 File Offset: 0x00052C54
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
			}
			if (!this.CanRead && !this.CanWrite)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_StreamClosed"));
			}
			if (!destination.CanRead && !destination.CanWrite)
			{
				throw new ObjectDisposedException("destination", Environment.GetResourceString("ObjectDisposed_StreamClosed"));
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnreadableStream"));
			}
			if (!destination.CanWrite)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnwritableStream"));
			}
			return this.CopyToAsyncInternal(destination, bufferSize, cancellationToken);
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x00054B08 File Offset: 0x00052D08
		private async Task CopyToAsyncInternal(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			byte[] buffer = new byte[bufferSize];
			int bytesRead;
			while ((bytesRead = await this.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false)) != 0)
			{
				await destination.WriteAsync(buffer, 0, bytesRead, cancellationToken).ConfigureAwait(false);
			}
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x00054B68 File Offset: 0x00052D68
		[__DynamicallyInvokable]
		public void CopyTo(Stream destination)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (!this.CanRead && !this.CanWrite)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_StreamClosed"));
			}
			if (!destination.CanRead && !destination.CanWrite)
			{
				throw new ObjectDisposedException("destination", Environment.GetResourceString("ObjectDisposed_StreamClosed"));
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnreadableStream"));
			}
			if (!destination.CanWrite)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnwritableStream"));
			}
			this.InternalCopyTo(destination, 81920);
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x00054C08 File Offset: 0x00052E08
		[__DynamicallyInvokable]
		public void CopyTo(Stream destination, int bufferSize)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
			}
			if (!this.CanRead && !this.CanWrite)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_StreamClosed"));
			}
			if (!destination.CanRead && !destination.CanWrite)
			{
				throw new ObjectDisposedException("destination", Environment.GetResourceString("ObjectDisposed_StreamClosed"));
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnreadableStream"));
			}
			if (!destination.CanWrite)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnwritableStream"));
			}
			this.InternalCopyTo(destination, bufferSize);
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x00054CBC File Offset: 0x00052EBC
		private void InternalCopyTo(Stream destination, int bufferSize)
		{
			byte[] array = new byte[bufferSize];
			int count;
			while ((count = this.Read(array, 0, array.Length)) != 0)
			{
				destination.Write(array, 0, count);
			}
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x00054CEA File Offset: 0x00052EEA
		public virtual void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x00054CF9 File Offset: 0x00052EF9
		[__DynamicallyInvokable]
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x00054D01 File Offset: 0x00052F01
		[__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06001983 RID: 6531
		[__DynamicallyInvokable]
		public abstract void Flush();

		// Token: 0x06001984 RID: 6532 RVA: 0x00054D03 File Offset: 0x00052F03
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task FlushAsync()
		{
			return this.FlushAsync(CancellationToken.None);
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x00054D10 File Offset: 0x00052F10
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task FlushAsync(CancellationToken cancellationToken)
		{
			return Task.Factory.StartNew(delegate(object state)
			{
				((Stream)state).Flush();
			}, this, cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x00054D43 File Offset: 0x00052F43
		[Obsolete("CreateWaitHandle will be removed eventually.  Please use \"new ManualResetEvent(false)\" instead.")]
		protected virtual WaitHandle CreateWaitHandle()
		{
			return new ManualResetEvent(false);
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x00054D4B File Offset: 0x00052F4B
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.BeginReadInternal(buffer, offset, count, callback, state, false);
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x00054D5C File Offset: 0x00052F5C
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		internal IAsyncResult BeginReadInternal(byte[] buffer, int offset, int count, AsyncCallback callback, object state, bool serializeAsynchronously)
		{
			if (!this.CanRead)
			{
				__Error.ReadNotSupported();
			}
			if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
			{
				return this.BlockingBeginRead(buffer, offset, count, callback, state);
			}
			SemaphoreSlim semaphoreSlim = this.EnsureAsyncActiveSemaphoreInitialized();
			Task task = null;
			if (serializeAsynchronously)
			{
				task = semaphoreSlim.WaitAsync();
			}
			else
			{
				semaphoreSlim.Wait();
			}
			Stream.ReadWriteTask readWriteTask = new Stream.ReadWriteTask(true, delegate(object <p0>)
			{
				Stream.ReadWriteTask readWriteTask2 = Task.InternalCurrent as Stream.ReadWriteTask;
				int result = readWriteTask2._stream.Read(readWriteTask2._buffer, readWriteTask2._offset, readWriteTask2._count);
				readWriteTask2.ClearBeginState();
				return result;
			}, state, this, buffer, offset, count, callback);
			if (task != null)
			{
				this.RunReadWriteTaskWhenReady(task, readWriteTask);
			}
			else
			{
				this.RunReadWriteTask(readWriteTask);
			}
			return readWriteTask;
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x00054DEC File Offset: 0x00052FEC
		[__DynamicallyInvokable]
		public virtual int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
			{
				return Stream.BlockingEndRead(asyncResult);
			}
			Stream.ReadWriteTask activeReadWriteTask = this._activeReadWriteTask;
			if (activeReadWriteTask == null)
			{
				throw new ArgumentException(Environment.GetResourceString("InvalidOperation_WrongAsyncResultOrEndReadCalledMultiple"));
			}
			if (activeReadWriteTask != asyncResult)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_WrongAsyncResultOrEndReadCalledMultiple"));
			}
			if (!activeReadWriteTask._isRead)
			{
				throw new ArgumentException(Environment.GetResourceString("InvalidOperation_WrongAsyncResultOrEndReadCalledMultiple"));
			}
			int result;
			try
			{
				result = activeReadWriteTask.GetAwaiter().GetResult();
			}
			finally
			{
				this._activeReadWriteTask = null;
				this._asyncActiveSemaphore.Release();
			}
			return result;
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x00054E94 File Offset: 0x00053094
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<int> ReadAsync(byte[] buffer, int offset, int count)
		{
			return this.ReadAsync(buffer, offset, count, CancellationToken.None);
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x00054EA4 File Offset: 0x000530A4
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return this.BeginEndReadAsync(buffer, offset, count);
			}
			return Task.FromCancellation<int>(cancellationToken);
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x00054EC0 File Offset: 0x000530C0
		private Task<int> BeginEndReadAsync(byte[] buffer, int offset, int count)
		{
			return TaskFactory<int>.FromAsyncTrim<Stream, Stream.ReadWriteParameters>(this, new Stream.ReadWriteParameters
			{
				Buffer = buffer,
				Offset = offset,
				Count = count
			}, (Stream stream, Stream.ReadWriteParameters args, AsyncCallback callback, object state) => stream.BeginRead(args.Buffer, args.Offset, args.Count, callback, state), (Stream stream, IAsyncResult asyncResult) => stream.EndRead(asyncResult));
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x00054F32 File Offset: 0x00053132
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.BeginWriteInternal(buffer, offset, count, callback, state, false);
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x00054F44 File Offset: 0x00053144
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		internal IAsyncResult BeginWriteInternal(byte[] buffer, int offset, int count, AsyncCallback callback, object state, bool serializeAsynchronously)
		{
			if (!this.CanWrite)
			{
				__Error.WriteNotSupported();
			}
			if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
			{
				return this.BlockingBeginWrite(buffer, offset, count, callback, state);
			}
			SemaphoreSlim semaphoreSlim = this.EnsureAsyncActiveSemaphoreInitialized();
			Task task = null;
			if (serializeAsynchronously)
			{
				task = semaphoreSlim.WaitAsync();
			}
			else
			{
				semaphoreSlim.Wait();
			}
			Stream.ReadWriteTask readWriteTask = new Stream.ReadWriteTask(false, delegate(object <p0>)
			{
				Stream.ReadWriteTask readWriteTask2 = Task.InternalCurrent as Stream.ReadWriteTask;
				readWriteTask2._stream.Write(readWriteTask2._buffer, readWriteTask2._offset, readWriteTask2._count);
				readWriteTask2.ClearBeginState();
				return 0;
			}, state, this, buffer, offset, count, callback);
			if (task != null)
			{
				this.RunReadWriteTaskWhenReady(task, readWriteTask);
			}
			else
			{
				this.RunReadWriteTask(readWriteTask);
			}
			return readWriteTask;
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x00054FD4 File Offset: 0x000531D4
		private void RunReadWriteTaskWhenReady(Task asyncWaiter, Stream.ReadWriteTask readWriteTask)
		{
			if (asyncWaiter.IsCompleted)
			{
				this.RunReadWriteTask(readWriteTask);
				return;
			}
			asyncWaiter.ContinueWith(delegate(Task t, object state)
			{
				Tuple<Stream, Stream.ReadWriteTask> tuple = (Tuple<Stream, Stream.ReadWriteTask>)state;
				tuple.Item1.RunReadWriteTask(tuple.Item2);
			}, Tuple.Create<Stream, Stream.ReadWriteTask>(this, readWriteTask), default(CancellationToken), TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x00055031 File Offset: 0x00053231
		private void RunReadWriteTask(Stream.ReadWriteTask readWriteTask)
		{
			this._activeReadWriteTask = readWriteTask;
			readWriteTask.m_taskScheduler = TaskScheduler.Default;
			readWriteTask.ScheduleAndStart(false);
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x0005504C File Offset: 0x0005324C
		[__DynamicallyInvokable]
		public virtual void EndWrite(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
			{
				Stream.BlockingEndWrite(asyncResult);
				return;
			}
			Stream.ReadWriteTask activeReadWriteTask = this._activeReadWriteTask;
			if (activeReadWriteTask == null)
			{
				throw new ArgumentException(Environment.GetResourceString("InvalidOperation_WrongAsyncResultOrEndWriteCalledMultiple"));
			}
			if (activeReadWriteTask != asyncResult)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_WrongAsyncResultOrEndWriteCalledMultiple"));
			}
			if (activeReadWriteTask._isRead)
			{
				throw new ArgumentException(Environment.GetResourceString("InvalidOperation_WrongAsyncResultOrEndWriteCalledMultiple"));
			}
			try
			{
				activeReadWriteTask.GetAwaiter().GetResult();
			}
			finally
			{
				this._activeReadWriteTask = null;
				this._asyncActiveSemaphore.Release();
			}
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x000550F4 File Offset: 0x000532F4
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task WriteAsync(byte[] buffer, int offset, int count)
		{
			return this.WriteAsync(buffer, offset, count, CancellationToken.None);
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x00055104 File Offset: 0x00053304
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return this.BeginEndWriteAsync(buffer, offset, count);
			}
			return Task.FromCancellation(cancellationToken);
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x00055120 File Offset: 0x00053320
		private Task BeginEndWriteAsync(byte[] buffer, int offset, int count)
		{
			return TaskFactory<VoidTaskResult>.FromAsyncTrim<Stream, Stream.ReadWriteParameters>(this, new Stream.ReadWriteParameters
			{
				Buffer = buffer,
				Offset = offset,
				Count = count
			}, (Stream stream, Stream.ReadWriteParameters args, AsyncCallback callback, object state) => stream.BeginWrite(args.Buffer, args.Offset, args.Count, callback, state), delegate(Stream stream, IAsyncResult asyncResult)
			{
				stream.EndWrite(asyncResult);
				return default(VoidTaskResult);
			});
		}

		// Token: 0x06001995 RID: 6549
		[__DynamicallyInvokable]
		public abstract long Seek(long offset, SeekOrigin origin);

		// Token: 0x06001996 RID: 6550
		[__DynamicallyInvokable]
		public abstract void SetLength(long value);

		// Token: 0x06001997 RID: 6551
		[__DynamicallyInvokable]
		public abstract int Read([In] [Out] byte[] buffer, int offset, int count);

		// Token: 0x06001998 RID: 6552 RVA: 0x00055194 File Offset: 0x00053394
		[__DynamicallyInvokable]
		public virtual int ReadByte()
		{
			byte[] array = new byte[1];
			if (this.Read(array, 0, 1) == 0)
			{
				return -1;
			}
			return (int)array[0];
		}

		// Token: 0x06001999 RID: 6553
		[__DynamicallyInvokable]
		public abstract void Write(byte[] buffer, int offset, int count);

		// Token: 0x0600199A RID: 6554 RVA: 0x000551BC File Offset: 0x000533BC
		[__DynamicallyInvokable]
		public virtual void WriteByte(byte value)
		{
			this.Write(new byte[]
			{
				value
			}, 0, 1);
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x000551DD File Offset: 0x000533DD
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		public static Stream Synchronized(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (stream is Stream.SyncStream)
			{
				return stream;
			}
			return new Stream.SyncStream(stream);
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x000551FD File Offset: 0x000533FD
		[Obsolete("Do not call or override this method.")]
		protected virtual void ObjectInvariant()
		{
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x00055200 File Offset: 0x00053400
		internal IAsyncResult BlockingBeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			Stream.SynchronousAsyncResult synchronousAsyncResult;
			try
			{
				int bytesRead = this.Read(buffer, offset, count);
				synchronousAsyncResult = new Stream.SynchronousAsyncResult(bytesRead, state);
			}
			catch (IOException ex)
			{
				synchronousAsyncResult = new Stream.SynchronousAsyncResult(ex, state, false);
			}
			if (callback != null)
			{
				callback(synchronousAsyncResult);
			}
			return synchronousAsyncResult;
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x0005524C File Offset: 0x0005344C
		internal static int BlockingEndRead(IAsyncResult asyncResult)
		{
			return Stream.SynchronousAsyncResult.EndRead(asyncResult);
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x00055254 File Offset: 0x00053454
		internal IAsyncResult BlockingBeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			Stream.SynchronousAsyncResult synchronousAsyncResult;
			try
			{
				this.Write(buffer, offset, count);
				synchronousAsyncResult = new Stream.SynchronousAsyncResult(state);
			}
			catch (IOException ex)
			{
				synchronousAsyncResult = new Stream.SynchronousAsyncResult(ex, state, true);
			}
			if (callback != null)
			{
				callback(synchronousAsyncResult);
			}
			return synchronousAsyncResult;
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x000552A0 File Offset: 0x000534A0
		internal static void BlockingEndWrite(IAsyncResult asyncResult)
		{
			Stream.SynchronousAsyncResult.EndWrite(asyncResult);
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x000552A8 File Offset: 0x000534A8
		[__DynamicallyInvokable]
		protected Stream()
		{
		}

		// Token: 0x040008F3 RID: 2291
		[__DynamicallyInvokable]
		public static readonly Stream Null = new Stream.NullStream();

		// Token: 0x040008F4 RID: 2292
		private const int _DefaultCopyBufferSize = 81920;

		// Token: 0x040008F5 RID: 2293
		[NonSerialized]
		private Stream.ReadWriteTask _activeReadWriteTask;

		// Token: 0x040008F6 RID: 2294
		[NonSerialized]
		private SemaphoreSlim _asyncActiveSemaphore;

		// Token: 0x02000AE4 RID: 2788
		private struct ReadWriteParameters
		{
			// Token: 0x040031DA RID: 12762
			internal byte[] Buffer;

			// Token: 0x040031DB RID: 12763
			internal int Offset;

			// Token: 0x040031DC RID: 12764
			internal int Count;
		}

		// Token: 0x02000AE5 RID: 2789
		private sealed class ReadWriteTask : Task<int>, ITaskCompletionAction
		{
			// Token: 0x0600697E RID: 27006 RVA: 0x0016BE58 File Offset: 0x0016A058
			internal void ClearBeginState()
			{
				this._stream = null;
				this._buffer = null;
			}

			// Token: 0x0600697F RID: 27007 RVA: 0x0016BE68 File Offset: 0x0016A068
			[SecuritySafeCritical]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public ReadWriteTask(bool isRead, Func<object, int> function, object state, Stream stream, byte[] buffer, int offset, int count, AsyncCallback callback) : base(function, state, CancellationToken.None, TaskCreationOptions.DenyChildAttach)
			{
				StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
				this._isRead = isRead;
				this._stream = stream;
				this._buffer = buffer;
				this._offset = offset;
				this._count = count;
				if (callback != null)
				{
					this._callback = callback;
					this._context = ExecutionContext.Capture(ref stackCrawlMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
					base.AddCompletionAction(this);
				}
			}

			// Token: 0x06006980 RID: 27008 RVA: 0x0016BED0 File Offset: 0x0016A0D0
			[SecurityCritical]
			private static void InvokeAsyncCallback(object completedTask)
			{
				Stream.ReadWriteTask readWriteTask = (Stream.ReadWriteTask)completedTask;
				AsyncCallback callback = readWriteTask._callback;
				readWriteTask._callback = null;
				callback(readWriteTask);
			}

			// Token: 0x06006981 RID: 27009 RVA: 0x0016BEFC File Offset: 0x0016A0FC
			[SecuritySafeCritical]
			void ITaskCompletionAction.Invoke(Task completingTask)
			{
				ExecutionContext context = this._context;
				if (context == null)
				{
					AsyncCallback callback = this._callback;
					this._callback = null;
					callback(completingTask);
					return;
				}
				this._context = null;
				ContextCallback contextCallback = Stream.ReadWriteTask.s_invokeAsyncCallback;
				if (contextCallback == null)
				{
					contextCallback = (Stream.ReadWriteTask.s_invokeAsyncCallback = new ContextCallback(Stream.ReadWriteTask.InvokeAsyncCallback));
				}
				using (context)
				{
					ExecutionContext.Run(context, contextCallback, this, true);
				}
			}

			// Token: 0x040031DD RID: 12765
			internal readonly bool _isRead;

			// Token: 0x040031DE RID: 12766
			internal Stream _stream;

			// Token: 0x040031DF RID: 12767
			internal byte[] _buffer;

			// Token: 0x040031E0 RID: 12768
			internal int _offset;

			// Token: 0x040031E1 RID: 12769
			internal int _count;

			// Token: 0x040031E2 RID: 12770
			private AsyncCallback _callback;

			// Token: 0x040031E3 RID: 12771
			private ExecutionContext _context;

			// Token: 0x040031E4 RID: 12772
			[SecurityCritical]
			private static ContextCallback s_invokeAsyncCallback;
		}

		// Token: 0x02000AE6 RID: 2790
		[Serializable]
		private sealed class NullStream : Stream
		{
			// Token: 0x06006982 RID: 27010 RVA: 0x0016BF74 File Offset: 0x0016A174
			internal NullStream()
			{
			}

			// Token: 0x170011F0 RID: 4592
			// (get) Token: 0x06006983 RID: 27011 RVA: 0x0016BF7C File Offset: 0x0016A17C
			public override bool CanRead
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170011F1 RID: 4593
			// (get) Token: 0x06006984 RID: 27012 RVA: 0x0016BF7F File Offset: 0x0016A17F
			public override bool CanWrite
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170011F2 RID: 4594
			// (get) Token: 0x06006985 RID: 27013 RVA: 0x0016BF82 File Offset: 0x0016A182
			public override bool CanSeek
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170011F3 RID: 4595
			// (get) Token: 0x06006986 RID: 27014 RVA: 0x0016BF85 File Offset: 0x0016A185
			public override long Length
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x170011F4 RID: 4596
			// (get) Token: 0x06006987 RID: 27015 RVA: 0x0016BF89 File Offset: 0x0016A189
			// (set) Token: 0x06006988 RID: 27016 RVA: 0x0016BF8D File Offset: 0x0016A18D
			public override long Position
			{
				get
				{
					return 0L;
				}
				set
				{
				}
			}

			// Token: 0x06006989 RID: 27017 RVA: 0x0016BF8F File Offset: 0x0016A18F
			protected override void Dispose(bool disposing)
			{
			}

			// Token: 0x0600698A RID: 27018 RVA: 0x0016BF91 File Offset: 0x0016A191
			public override void Flush()
			{
			}

			// Token: 0x0600698B RID: 27019 RVA: 0x0016BF93 File Offset: 0x0016A193
			[ComVisible(false)]
			public override Task FlushAsync(CancellationToken cancellationToken)
			{
				if (!cancellationToken.IsCancellationRequested)
				{
					return Task.CompletedTask;
				}
				return Task.FromCancellation(cancellationToken);
			}

			// Token: 0x0600698C RID: 27020 RVA: 0x0016BFAA File Offset: 0x0016A1AA
			[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				if (!this.CanRead)
				{
					__Error.ReadNotSupported();
				}
				return base.BlockingBeginRead(buffer, offset, count, callback, state);
			}

			// Token: 0x0600698D RID: 27021 RVA: 0x0016BFC6 File Offset: 0x0016A1C6
			public override int EndRead(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				return Stream.BlockingEndRead(asyncResult);
			}

			// Token: 0x0600698E RID: 27022 RVA: 0x0016BFDC File Offset: 0x0016A1DC
			[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				if (!this.CanWrite)
				{
					__Error.WriteNotSupported();
				}
				return base.BlockingBeginWrite(buffer, offset, count, callback, state);
			}

			// Token: 0x0600698F RID: 27023 RVA: 0x0016BFF8 File Offset: 0x0016A1F8
			public override void EndWrite(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				Stream.BlockingEndWrite(asyncResult);
			}

			// Token: 0x06006990 RID: 27024 RVA: 0x0016C00E File Offset: 0x0016A20E
			public override int Read([In] [Out] byte[] buffer, int offset, int count)
			{
				return 0;
			}

			// Token: 0x06006991 RID: 27025 RVA: 0x0016C014 File Offset: 0x0016A214
			[ComVisible(false)]
			public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
			{
				Task<int> task = Stream.NullStream.s_nullReadTask;
				if (task == null)
				{
					task = (Stream.NullStream.s_nullReadTask = new Task<int>(false, 0, (TaskCreationOptions)16384, CancellationToken.None));
				}
				return task;
			}

			// Token: 0x06006992 RID: 27026 RVA: 0x0016C043 File Offset: 0x0016A243
			public override int ReadByte()
			{
				return -1;
			}

			// Token: 0x06006993 RID: 27027 RVA: 0x0016C046 File Offset: 0x0016A246
			public override void Write(byte[] buffer, int offset, int count)
			{
			}

			// Token: 0x06006994 RID: 27028 RVA: 0x0016C048 File Offset: 0x0016A248
			[ComVisible(false)]
			public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
			{
				if (!cancellationToken.IsCancellationRequested)
				{
					return Task.CompletedTask;
				}
				return Task.FromCancellation(cancellationToken);
			}

			// Token: 0x06006995 RID: 27029 RVA: 0x0016C060 File Offset: 0x0016A260
			public override void WriteByte(byte value)
			{
			}

			// Token: 0x06006996 RID: 27030 RVA: 0x0016C062 File Offset: 0x0016A262
			public override long Seek(long offset, SeekOrigin origin)
			{
				return 0L;
			}

			// Token: 0x06006997 RID: 27031 RVA: 0x0016C066 File Offset: 0x0016A266
			public override void SetLength(long length)
			{
			}

			// Token: 0x040031E5 RID: 12773
			private static Task<int> s_nullReadTask;
		}

		// Token: 0x02000AE7 RID: 2791
		internal sealed class SynchronousAsyncResult : IAsyncResult
		{
			// Token: 0x06006998 RID: 27032 RVA: 0x0016C068 File Offset: 0x0016A268
			internal SynchronousAsyncResult(int bytesRead, object asyncStateObject)
			{
				this._bytesRead = bytesRead;
				this._stateObject = asyncStateObject;
			}

			// Token: 0x06006999 RID: 27033 RVA: 0x0016C07E File Offset: 0x0016A27E
			internal SynchronousAsyncResult(object asyncStateObject)
			{
				this._stateObject = asyncStateObject;
				this._isWrite = true;
			}

			// Token: 0x0600699A RID: 27034 RVA: 0x0016C094 File Offset: 0x0016A294
			internal SynchronousAsyncResult(Exception ex, object asyncStateObject, bool isWrite)
			{
				this._exceptionInfo = ExceptionDispatchInfo.Capture(ex);
				this._stateObject = asyncStateObject;
				this._isWrite = isWrite;
			}

			// Token: 0x170011F5 RID: 4597
			// (get) Token: 0x0600699B RID: 27035 RVA: 0x0016C0B6 File Offset: 0x0016A2B6
			public bool IsCompleted
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170011F6 RID: 4598
			// (get) Token: 0x0600699C RID: 27036 RVA: 0x0016C0B9 File Offset: 0x0016A2B9
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					return LazyInitializer.EnsureInitialized<ManualResetEvent>(ref this._waitHandle, () => new ManualResetEvent(true));
				}
			}

			// Token: 0x170011F7 RID: 4599
			// (get) Token: 0x0600699D RID: 27037 RVA: 0x0016C0E5 File Offset: 0x0016A2E5
			public object AsyncState
			{
				get
				{
					return this._stateObject;
				}
			}

			// Token: 0x170011F8 RID: 4600
			// (get) Token: 0x0600699E RID: 27038 RVA: 0x0016C0ED File Offset: 0x0016A2ED
			public bool CompletedSynchronously
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600699F RID: 27039 RVA: 0x0016C0F0 File Offset: 0x0016A2F0
			internal void ThrowIfError()
			{
				if (this._exceptionInfo != null)
				{
					this._exceptionInfo.Throw();
				}
			}

			// Token: 0x060069A0 RID: 27040 RVA: 0x0016C108 File Offset: 0x0016A308
			internal static int EndRead(IAsyncResult asyncResult)
			{
				Stream.SynchronousAsyncResult synchronousAsyncResult = asyncResult as Stream.SynchronousAsyncResult;
				if (synchronousAsyncResult == null || synchronousAsyncResult._isWrite)
				{
					__Error.WrongAsyncResult();
				}
				if (synchronousAsyncResult._endXxxCalled)
				{
					__Error.EndReadCalledTwice();
				}
				synchronousAsyncResult._endXxxCalled = true;
				synchronousAsyncResult.ThrowIfError();
				return synchronousAsyncResult._bytesRead;
			}

			// Token: 0x060069A1 RID: 27041 RVA: 0x0016C14C File Offset: 0x0016A34C
			internal static void EndWrite(IAsyncResult asyncResult)
			{
				Stream.SynchronousAsyncResult synchronousAsyncResult = asyncResult as Stream.SynchronousAsyncResult;
				if (synchronousAsyncResult == null || !synchronousAsyncResult._isWrite)
				{
					__Error.WrongAsyncResult();
				}
				if (synchronousAsyncResult._endXxxCalled)
				{
					__Error.EndWriteCalledTwice();
				}
				synchronousAsyncResult._endXxxCalled = true;
				synchronousAsyncResult.ThrowIfError();
			}

			// Token: 0x040031E6 RID: 12774
			private readonly object _stateObject;

			// Token: 0x040031E7 RID: 12775
			private readonly bool _isWrite;

			// Token: 0x040031E8 RID: 12776
			private ManualResetEvent _waitHandle;

			// Token: 0x040031E9 RID: 12777
			private ExceptionDispatchInfo _exceptionInfo;

			// Token: 0x040031EA RID: 12778
			private bool _endXxxCalled;

			// Token: 0x040031EB RID: 12779
			private int _bytesRead;
		}

		// Token: 0x02000AE8 RID: 2792
		[Serializable]
		internal sealed class SyncStream : Stream, IDisposable
		{
			// Token: 0x060069A2 RID: 27042 RVA: 0x0016C18A File Offset: 0x0016A38A
			internal SyncStream(Stream stream)
			{
				if (stream == null)
				{
					throw new ArgumentNullException("stream");
				}
				this._stream = stream;
			}

			// Token: 0x170011F9 RID: 4601
			// (get) Token: 0x060069A3 RID: 27043 RVA: 0x0016C1A7 File Offset: 0x0016A3A7
			public override bool CanRead
			{
				get
				{
					return this._stream.CanRead;
				}
			}

			// Token: 0x170011FA RID: 4602
			// (get) Token: 0x060069A4 RID: 27044 RVA: 0x0016C1B4 File Offset: 0x0016A3B4
			public override bool CanWrite
			{
				get
				{
					return this._stream.CanWrite;
				}
			}

			// Token: 0x170011FB RID: 4603
			// (get) Token: 0x060069A5 RID: 27045 RVA: 0x0016C1C1 File Offset: 0x0016A3C1
			public override bool CanSeek
			{
				get
				{
					return this._stream.CanSeek;
				}
			}

			// Token: 0x170011FC RID: 4604
			// (get) Token: 0x060069A6 RID: 27046 RVA: 0x0016C1CE File Offset: 0x0016A3CE
			[ComVisible(false)]
			public override bool CanTimeout
			{
				get
				{
					return this._stream.CanTimeout;
				}
			}

			// Token: 0x170011FD RID: 4605
			// (get) Token: 0x060069A7 RID: 27047 RVA: 0x0016C1DC File Offset: 0x0016A3DC
			public override long Length
			{
				get
				{
					Stream stream = this._stream;
					long length;
					lock (stream)
					{
						length = this._stream.Length;
					}
					return length;
				}
			}

			// Token: 0x170011FE RID: 4606
			// (get) Token: 0x060069A8 RID: 27048 RVA: 0x0016C224 File Offset: 0x0016A424
			// (set) Token: 0x060069A9 RID: 27049 RVA: 0x0016C26C File Offset: 0x0016A46C
			public override long Position
			{
				get
				{
					Stream stream = this._stream;
					long position;
					lock (stream)
					{
						position = this._stream.Position;
					}
					return position;
				}
				set
				{
					Stream stream = this._stream;
					lock (stream)
					{
						this._stream.Position = value;
					}
				}
			}

			// Token: 0x170011FF RID: 4607
			// (get) Token: 0x060069AA RID: 27050 RVA: 0x0016C2B4 File Offset: 0x0016A4B4
			// (set) Token: 0x060069AB RID: 27051 RVA: 0x0016C2C1 File Offset: 0x0016A4C1
			[ComVisible(false)]
			public override int ReadTimeout
			{
				get
				{
					return this._stream.ReadTimeout;
				}
				set
				{
					this._stream.ReadTimeout = value;
				}
			}

			// Token: 0x17001200 RID: 4608
			// (get) Token: 0x060069AC RID: 27052 RVA: 0x0016C2CF File Offset: 0x0016A4CF
			// (set) Token: 0x060069AD RID: 27053 RVA: 0x0016C2DC File Offset: 0x0016A4DC
			[ComVisible(false)]
			public override int WriteTimeout
			{
				get
				{
					return this._stream.WriteTimeout;
				}
				set
				{
					this._stream.WriteTimeout = value;
				}
			}

			// Token: 0x060069AE RID: 27054 RVA: 0x0016C2EC File Offset: 0x0016A4EC
			public override void Close()
			{
				Stream stream = this._stream;
				lock (stream)
				{
					try
					{
						this._stream.Close();
					}
					finally
					{
						base.Dispose(true);
					}
				}
			}

			// Token: 0x060069AF RID: 27055 RVA: 0x0016C348 File Offset: 0x0016A548
			protected override void Dispose(bool disposing)
			{
				Stream stream = this._stream;
				lock (stream)
				{
					try
					{
						if (disposing)
						{
							((IDisposable)this._stream).Dispose();
						}
					}
					finally
					{
						base.Dispose(disposing);
					}
				}
			}

			// Token: 0x060069B0 RID: 27056 RVA: 0x0016C3A4 File Offset: 0x0016A5A4
			public override void Flush()
			{
				Stream stream = this._stream;
				lock (stream)
				{
					this._stream.Flush();
				}
			}

			// Token: 0x060069B1 RID: 27057 RVA: 0x0016C3EC File Offset: 0x0016A5EC
			public override int Read([In] [Out] byte[] bytes, int offset, int count)
			{
				Stream stream = this._stream;
				int result;
				lock (stream)
				{
					result = this._stream.Read(bytes, offset, count);
				}
				return result;
			}

			// Token: 0x060069B2 RID: 27058 RVA: 0x0016C438 File Offset: 0x0016A638
			public override int ReadByte()
			{
				Stream stream = this._stream;
				int result;
				lock (stream)
				{
					result = this._stream.ReadByte();
				}
				return result;
			}

			// Token: 0x060069B3 RID: 27059 RVA: 0x0016C480 File Offset: 0x0016A680
			private static bool OverridesBeginMethod(Stream stream, string methodName)
			{
				MethodInfo[] methods = stream.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public);
				foreach (MethodInfo methodInfo in methods)
				{
					if (methodInfo.DeclaringType == typeof(Stream) && methodInfo.Name == methodName)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x060069B4 RID: 27060 RVA: 0x0016C4D8 File Offset: 0x0016A6D8
			[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				if (this._overridesBeginRead == null)
				{
					this._overridesBeginRead = new bool?(Stream.SyncStream.OverridesBeginMethod(this._stream, "BeginRead"));
				}
				Stream stream = this._stream;
				IAsyncResult result;
				lock (stream)
				{
					result = (this._overridesBeginRead.Value ? this._stream.BeginRead(buffer, offset, count, callback, state) : this._stream.BeginReadInternal(buffer, offset, count, callback, state, true));
				}
				return result;
			}

			// Token: 0x060069B5 RID: 27061 RVA: 0x0016C570 File Offset: 0x0016A770
			public override int EndRead(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				Stream stream = this._stream;
				int result;
				lock (stream)
				{
					result = this._stream.EndRead(asyncResult);
				}
				return result;
			}

			// Token: 0x060069B6 RID: 27062 RVA: 0x0016C5C8 File Offset: 0x0016A7C8
			public override long Seek(long offset, SeekOrigin origin)
			{
				Stream stream = this._stream;
				long result;
				lock (stream)
				{
					result = this._stream.Seek(offset, origin);
				}
				return result;
			}

			// Token: 0x060069B7 RID: 27063 RVA: 0x0016C614 File Offset: 0x0016A814
			public override void SetLength(long length)
			{
				Stream stream = this._stream;
				lock (stream)
				{
					this._stream.SetLength(length);
				}
			}

			// Token: 0x060069B8 RID: 27064 RVA: 0x0016C65C File Offset: 0x0016A85C
			public override void Write(byte[] bytes, int offset, int count)
			{
				Stream stream = this._stream;
				lock (stream)
				{
					this._stream.Write(bytes, offset, count);
				}
			}

			// Token: 0x060069B9 RID: 27065 RVA: 0x0016C6A4 File Offset: 0x0016A8A4
			public override void WriteByte(byte b)
			{
				Stream stream = this._stream;
				lock (stream)
				{
					this._stream.WriteByte(b);
				}
			}

			// Token: 0x060069BA RID: 27066 RVA: 0x0016C6EC File Offset: 0x0016A8EC
			[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				if (this._overridesBeginWrite == null)
				{
					this._overridesBeginWrite = new bool?(Stream.SyncStream.OverridesBeginMethod(this._stream, "BeginWrite"));
				}
				Stream stream = this._stream;
				IAsyncResult result;
				lock (stream)
				{
					result = (this._overridesBeginWrite.Value ? this._stream.BeginWrite(buffer, offset, count, callback, state) : this._stream.BeginWriteInternal(buffer, offset, count, callback, state, true));
				}
				return result;
			}

			// Token: 0x060069BB RID: 27067 RVA: 0x0016C784 File Offset: 0x0016A984
			public override void EndWrite(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				Stream stream = this._stream;
				lock (stream)
				{
					this._stream.EndWrite(asyncResult);
				}
			}

			// Token: 0x040031EC RID: 12780
			private Stream _stream;

			// Token: 0x040031ED RID: 12781
			[NonSerialized]
			private bool? _overridesBeginRead;

			// Token: 0x040031EE RID: 12782
			[NonSerialized]
			private bool? _overridesBeginWrite;
		}
	}
}
