using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x02000034 RID: 52
	internal class SocketStream : Stream
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00006E8F File Offset: 0x0000508F
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.TcpChannelTracer;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00006E96 File Offset: 0x00005096
		public Socket Socket
		{
			get
			{
				return this.m_StreamSocket;
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00006E9E File Offset: 0x0000509E
		public SocketStream(Socket socket, ISimpleBufferPool bufPool, IPool<SocketStreamAsyncArgs> asyncPool, SocketStream.ISocketStreamPerfCounters perfCtrs)
		{
			this.m_StreamSocket = socket;
			this.m_bufPool = bufPool;
			this.m_asyncArgsPool = asyncPool;
			this.m_perfCounters = perfCtrs;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00006ED4 File Offset: 0x000050D4
		protected override void Dispose(bool disposing)
		{
			bool cleanedUp = this.m_CleanedUp;
			this.m_CleanedUp = true;
			if (!cleanedUp && disposing && this.m_StreamSocket != null)
			{
				Socket streamSocket = this.m_StreamSocket;
				if (streamSocket != null)
				{
					Exception ex = null;
					try
					{
						streamSocket.Shutdown(SocketShutdown.Both);
					}
					catch (SocketException ex2)
					{
						ex = ex2;
					}
					catch (ObjectDisposedException ex3)
					{
						ex = ex3;
					}
					if (ex != null)
					{
						SocketStream.Tracer.TraceError<Exception>((long)this.GetHashCode(), "Dispose.Shutdown got exception: {0}", ex);
					}
					streamSocket.Close(0);
				}
			}
			base.Dispose(disposing);
			this.ReleaseInternalIoResources();
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00006F68 File Offset: 0x00005168
		private void ReleaseInternalIoResources()
		{
			int num = Interlocked.Exchange(ref this.m_readIsBusy, 1);
			if (num != 0)
			{
				int num2 = 10;
				int num3 = num2 * 10;
				do
				{
					Thread.Sleep(100);
					num = Interlocked.Exchange(ref this.m_readIsBusy, 1);
				}
				while (num != 0 && --num3 > 0);
				if (num != 0)
				{
					this.m_buffersWereLeaked = true;
					SocketStream.Tracer.TraceError<bool>((long)this.GetHashCode(), "buffer leak: {0}", this.m_buffersWereLeaked);
					ReplayCrimsonEvents.SocketStreamLeakOnClose.LogPeriodic<int>(Environment.MachineName, Parameters.CurrentValues.DefaultEventSuppressionInterval, num2);
					return;
				}
			}
			if (this.m_readArgs != null)
			{
				SocketStreamAsyncArgs readArgs = this.m_readArgs;
				this.m_readArgs = null;
				this.ReleaseArgsToPools(readArgs);
			}
			this.m_bufIsAllocated = false;
			Interlocked.Exchange(ref this.m_readIsBusy, 0);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000701F File Offset: 0x0000521F
		public override void Flush()
		{
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00007021 File Offset: 0x00005221
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00007028 File Offset: 0x00005228
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00007030 File Offset: 0x00005230
		// (set) Token: 0x060001AB RID: 427 RVA: 0x0000705E File Offset: 0x0000525E
		public override int ReadTimeout
		{
			get
			{
				int num = (int)this.m_StreamSocket.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout);
				if (num == 0)
				{
					return -1;
				}
				return num;
			}
			set
			{
				if (value <= 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.SetSocketTimeoutOption(SocketShutdown.Receive, value);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000707C File Offset: 0x0000527C
		// (set) Token: 0x060001AD RID: 429 RVA: 0x000070AA File Offset: 0x000052AA
		public override int WriteTimeout
		{
			get
			{
				int num = (int)this.m_StreamSocket.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout);
				if (num == 0)
				{
					return -1;
				}
				return num;
			}
			set
			{
				if (value <= 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.SetSocketTimeoutOption(SocketShutdown.Send, value);
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000070C8 File Offset: 0x000052C8
		internal void SetSocketTimeoutOption(SocketShutdown mode, int timeout)
		{
			if (timeout < 0)
			{
				timeout = 0;
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				return;
			}
			if ((mode == SocketShutdown.Send || mode == SocketShutdown.Both) && timeout != this.m_CurrentWriteTimeout)
			{
				streamSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, timeout);
				this.m_CurrentWriteTimeout = timeout;
			}
			if ((mode == SocketShutdown.Receive || mode == SocketShutdown.Both) && timeout != this.m_CurrentReadTimeout)
			{
				streamSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, timeout);
				this.m_CurrentReadTimeout = timeout;
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00007138 File Offset: 0x00005338
		public override int Read(byte[] buffer, int offset, int size)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			Socket socket = this.Socket;
			if (socket == null)
			{
				throw new IOException("no socket");
			}
			int result;
			try
			{
				int num = socket.Receive(buffer, offset, size, SocketFlags.None);
				result = num;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new IOException(string.Format("net_io_readfailure: {0}", ex.Message), ex);
			}
			return result;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000071E8 File Offset: 0x000053E8
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			if (!this.CanRead)
			{
				throw new InvalidOperationException("net_writeonlystream");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException("net_io_readfailure");
			}
			SocketStreamAsyncArgs socketStreamAsyncArgs = this.ObtainArgsForStartRead(size);
			if (socketStreamAsyncArgs != null)
			{
				return this.StartInternalRead(streamSocket, socketStreamAsyncArgs, buffer, offset, size, callback, state);
			}
			return streamSocket.BeginReceive(buffer, offset, size, SocketFlags.None, callback, state);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00007284 File Offset: 0x00005484
		private SocketStreamAsyncArgs ObtainArgsForStartRead(int readSize)
		{
			SocketStreamAsyncArgs socketStreamAsyncArgs = null;
			if (this.m_asyncArgsPool == null)
			{
				return null;
			}
			int num = Interlocked.Exchange(ref this.m_readIsBusy, 1);
			if (num == 1)
			{
				return null;
			}
			try
			{
				if (!this.m_bufIsAllocated)
				{
					socketStreamAsyncArgs = this.ObtainInternalBuffer(readSize);
					if (socketStreamAsyncArgs == null)
					{
						return null;
					}
				}
				socketStreamAsyncArgs = this.m_readArgs;
				if (readSize > socketStreamAsyncArgs.InternalBuffer.Buffer.Length)
				{
					socketStreamAsyncArgs = null;
				}
			}
			finally
			{
				if (socketStreamAsyncArgs == null)
				{
					Interlocked.Exchange(ref this.m_readIsBusy, 0);
				}
			}
			return socketStreamAsyncArgs;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00007308 File Offset: 0x00005508
		private SocketStreamAsyncArgs ObtainInternalBuffer(int readSize)
		{
			SocketStreamAsyncArgs socketStreamAsyncArgs = null;
			SimpleBuffer simpleBuffer = null;
			bool flag = false;
			try
			{
				simpleBuffer = this.m_bufPool.TryGetObject(readSize);
				if (simpleBuffer == null)
				{
					if (readSize <= this.m_bufPool.BufferSize)
					{
						ReplayCrimsonEvents.SocketStreamBufferExhaustion.LogPeriodic(Environment.MachineName, Parameters.CurrentValues.DefaultEventSuppressionInterval);
					}
					return null;
				}
				socketStreamAsyncArgs = this.m_asyncArgsPool.TryGetObject();
				if (socketStreamAsyncArgs == null)
				{
					this.m_extraArgCount++;
					socketStreamAsyncArgs = new SocketStreamAsyncArgs(false);
				}
				this.BindArgsForUse(simpleBuffer, socketStreamAsyncArgs);
				this.m_readArgs = socketStreamAsyncArgs;
				this.m_bufIsAllocated = true;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					if (socketStreamAsyncArgs != null)
					{
						this.ReleaseArgsToPools(socketStreamAsyncArgs);
						socketStreamAsyncArgs = null;
					}
					else if (simpleBuffer != null)
					{
						this.m_bufPool.TryReturnObject(simpleBuffer);
						simpleBuffer = null;
					}
				}
			}
			return socketStreamAsyncArgs;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000073CC File Offset: 0x000055CC
		private void BindArgsForUse(SimpleBuffer buf, SocketStreamAsyncArgs args)
		{
			args.InternalBuffer = buf;
			args.CompletionRtn = new EventHandler<SocketAsyncEventArgs>(this.IO_Completed);
			args.Completed += args.CompletionRtn;
			args.SetBuffer(buf.Buffer, 0, buf.Buffer.Length);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00007408 File Offset: 0x00005608
		private void ReleaseArgsToPools(SocketStreamAsyncArgs args)
		{
			SimpleBuffer internalBuffer = args.InternalBuffer;
			args.Completed -= args.CompletionRtn;
			args.CompletionRtn = null;
			args.InternalBuffer = null;
			if (internalBuffer != null)
			{
				this.m_bufPool.TryReturnObject(internalBuffer);
			}
			if (!this.m_asyncArgsPool.TryReturnObject(args))
			{
				args.Dispose();
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000745C File Offset: 0x0000565C
		private IAsyncResult StartInternalRead(Socket streamSocket, SocketStreamAsyncArgs readArgs, byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			IAsyncResult asyncResult;
			try
			{
				SocketStream.IoReq ioReq = new SocketStream.IoReq(state, callback);
				ioReq.RecordUserBuffer(buffer, offset, size);
				ioReq.InternalBuffer = readArgs.InternalBuffer;
				readArgs.SetBuffer(ioReq.InternalBuffer.Buffer, 0, size);
				readArgs.UserToken = ioReq;
				SocketStream.Tracer.TraceDebug((long)this.GetHashCode(), "ReceiveAsync called");
				if (!streamSocket.ReceiveAsync(readArgs))
				{
					SocketStream.Tracer.TraceDebug((long)this.GetHashCode(), "ReceiveAsync sync completion");
					this.IO_Completed(null, readArgs);
				}
				asyncResult = ioReq.AsyncResult;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new IOException(ex.Message, ex);
			}
			return asyncResult;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00007524 File Offset: 0x00005724
		private void IO_Completed(object sender, SocketAsyncEventArgs e)
		{
			SocketAsyncOperation lastOperation = e.LastOperation;
			if (lastOperation == SocketAsyncOperation.Receive)
			{
				this.ProcessReceive(e);
				return;
			}
			if (lastOperation != SocketAsyncOperation.Send)
			{
				throw new ArgumentException("The last operation completed on the socket was not a receive or send");
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00007558 File Offset: 0x00005758
		private void ProcessReceive(SocketAsyncEventArgs e)
		{
			SocketStream.IoReq ioReq = e.UserToken as SocketStream.IoReq;
			if (ioReq == null)
			{
				throw new ArgumentException("UserToken must be ReadReq");
			}
			try
			{
				ioReq.SocketError = e.SocketError;
				ioReq.BytesTransferred = e.BytesTransferred;
				Array.Copy(ioReq.InternalBuffer.Buffer, 0, ioReq.UserBuffer, ioReq.UserOffset, ioReq.BytesTransferred);
				ioReq.InternalBuffer = null;
				e.SetBuffer(null, 0, 0);
			}
			finally
			{
				SocketStream.Tracer.TraceDebug((long)this.GetHashCode(), "ProcessReceive marks read idle");
				Interlocked.Exchange(ref this.m_readIsBusy, 0);
				ioReq.Completed = true;
			}
			ioReq.AsyncResult.InvokeCallback();
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00007614 File Offset: 0x00005814
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			LazyAsyncResult lazyAsyncResult = asyncResult as LazyAsyncResult;
			if (lazyAsyncResult != null)
			{
				return this.EndReadFromInternalBuf(lazyAsyncResult);
			}
			Socket streamSocket = this.m_StreamSocket;
			if (streamSocket == null)
			{
				throw new IOException("EndRead with no socket");
			}
			return streamSocket.EndReceive(asyncResult);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00007660 File Offset: 0x00005860
		private int EndReadFromInternalBuf(LazyAsyncResult result)
		{
			SocketStream.IoReq ioReq = result.AsyncObject as SocketStream.IoReq;
			if (ioReq == null)
			{
				throw new ArgumentException("AsyncObject corrupt");
			}
			result.InternalWaitForCompletion();
			if (ioReq.SocketError != SocketError.Success)
			{
				throw new SocketException((int)ioReq.SocketError);
			}
			return ioReq.BytesTransferred;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000076A8 File Offset: 0x000058A8
		public override void Write(byte[] buffer, int offset, int size)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("size");
			}
			Socket socket = this.Socket;
			if (socket == null)
			{
				throw new IOException("net_io_writefailure:nosocket");
			}
			try
			{
				StopwatchStamp stamp = StopwatchStamp.GetStamp();
				socket.Send(buffer, offset, size, SocketFlags.None);
				if (this.m_perfCounters != null)
				{
					this.m_perfCounters.RecordWriteLatency(stamp.ElapsedTicks);
				}
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new IOException(string.Format("net_io_writefailure: {0}", ex.Message), ex);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00007774 File Offset: 0x00005974
		public override bool CanRead
		{
			get
			{
				return this.Socket.Connected;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00007781 File Offset: 0x00005981
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00007784 File Offset: 0x00005984
		public override bool CanWrite
		{
			get
			{
				return this.Socket.Connected;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00007791 File Offset: 0x00005991
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00007798 File Offset: 0x00005998
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x0000779F File Offset: 0x0000599F
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x04000111 RID: 273
		private Socket m_StreamSocket;

		// Token: 0x04000112 RID: 274
		private int m_CurrentReadTimeout = -1;

		// Token: 0x04000113 RID: 275
		private int m_CurrentWriteTimeout = -1;

		// Token: 0x04000114 RID: 276
		private bool m_CleanedUp;

		// Token: 0x04000115 RID: 277
		private bool m_buffersWereLeaked;

		// Token: 0x04000116 RID: 278
		private ISimpleBufferPool m_bufPool;

		// Token: 0x04000117 RID: 279
		private IPool<SocketStreamAsyncArgs> m_asyncArgsPool;

		// Token: 0x04000118 RID: 280
		private SocketStream.ISocketStreamPerfCounters m_perfCounters;

		// Token: 0x04000119 RID: 281
		private int m_readIsBusy;

		// Token: 0x0400011A RID: 282
		private bool m_bufIsAllocated;

		// Token: 0x0400011B RID: 283
		private int m_extraArgCount;

		// Token: 0x0400011C RID: 284
		private SocketStreamAsyncArgs m_readArgs;

		// Token: 0x02000035 RID: 53
		public interface ISocketStreamPerfCounters
		{
			// Token: 0x060001C1 RID: 449
			void RecordWriteLatency(long tics);
		}

		// Token: 0x02000036 RID: 54
		private class IoReq
		{
			// Token: 0x060001C2 RID: 450 RVA: 0x000077A6 File Offset: 0x000059A6
			public IoReq(object callerState, AsyncCallback callerCallback)
			{
				this.AsyncResult = new LazyAsyncResult(this, callerState, callerCallback);
			}

			// Token: 0x1700007B RID: 123
			// (get) Token: 0x060001C3 RID: 451 RVA: 0x000077BC File Offset: 0x000059BC
			// (set) Token: 0x060001C4 RID: 452 RVA: 0x000077C4 File Offset: 0x000059C4
			public LazyAsyncResult AsyncResult { get; set; }

			// Token: 0x1700007C RID: 124
			// (get) Token: 0x060001C5 RID: 453 RVA: 0x000077CD File Offset: 0x000059CD
			// (set) Token: 0x060001C6 RID: 454 RVA: 0x000077D5 File Offset: 0x000059D5
			public SocketError SocketError { get; set; }

			// Token: 0x1700007D RID: 125
			// (get) Token: 0x060001C7 RID: 455 RVA: 0x000077DE File Offset: 0x000059DE
			// (set) Token: 0x060001C8 RID: 456 RVA: 0x000077E6 File Offset: 0x000059E6
			public int BytesTransferred { get; set; }

			// Token: 0x1700007E RID: 126
			// (get) Token: 0x060001C9 RID: 457 RVA: 0x000077EF File Offset: 0x000059EF
			// (set) Token: 0x060001CA RID: 458 RVA: 0x000077F7 File Offset: 0x000059F7
			public SimpleBuffer InternalBuffer { get; set; }

			// Token: 0x1700007F RID: 127
			// (get) Token: 0x060001CB RID: 459 RVA: 0x00007800 File Offset: 0x00005A00
			// (set) Token: 0x060001CC RID: 460 RVA: 0x00007808 File Offset: 0x00005A08
			public byte[] UserBuffer { get; set; }

			// Token: 0x17000080 RID: 128
			// (get) Token: 0x060001CD RID: 461 RVA: 0x00007811 File Offset: 0x00005A11
			// (set) Token: 0x060001CE RID: 462 RVA: 0x00007819 File Offset: 0x00005A19
			public int UserOffset { get; set; }

			// Token: 0x17000081 RID: 129
			// (get) Token: 0x060001CF RID: 463 RVA: 0x00007822 File Offset: 0x00005A22
			// (set) Token: 0x060001D0 RID: 464 RVA: 0x0000782A File Offset: 0x00005A2A
			public int UserSize { get; set; }

			// Token: 0x17000082 RID: 130
			// (get) Token: 0x060001D1 RID: 465 RVA: 0x00007833 File Offset: 0x00005A33
			// (set) Token: 0x060001D2 RID: 466 RVA: 0x0000783B File Offset: 0x00005A3B
			public bool Completed { get; set; }

			// Token: 0x060001D3 RID: 467 RVA: 0x00007844 File Offset: 0x00005A44
			public void RecordUserBuffer(byte[] buffer, int offset, int size)
			{
				this.UserBuffer = buffer;
				this.UserOffset = offset;
				this.UserSize = size;
			}
		}
	}
}
