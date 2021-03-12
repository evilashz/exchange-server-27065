using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Search.Core.Diagnostics;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x02000085 RID: 133
	internal class StreamChannel : Stream
	{
		// Token: 0x06000359 RID: 857 RVA: 0x0000B038 File Offset: 0x00009238
		internal StreamChannel(Socket socket)
		{
			this.diagnosticsSession = DiagnosticsSession.CreateComponentDiagnosticsSession("StreamChannel", ComponentInstance.Globals.Search.ServiceName, ExTraceGlobals.StreamChannelTracer, (long)this.GetHashCode());
			this.networkStream = new NetworkConnectionStream(socket);
			this.stream = this.networkStream;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000B095 File Offset: 0x00009295
		protected StreamChannel()
		{
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000B0A9 File Offset: 0x000092A9
		public override bool CanRead
		{
			[DebuggerStepThrough]
			get
			{
				return true;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000B0AC File Offset: 0x000092AC
		public override bool CanSeek
		{
			[DebuggerStepThrough]
			get
			{
				return false;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000B0AF File Offset: 0x000092AF
		public override bool CanTimeout
		{
			[DebuggerStepThrough]
			get
			{
				return true;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0000B0B2 File Offset: 0x000092B2
		public override bool CanWrite
		{
			[DebuggerStepThrough]
			get
			{
				return true;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000B0B5 File Offset: 0x000092B5
		public override long Length
		{
			[DebuggerStepThrough]
			get
			{
				this.diagnosticsSession.TraceError("GetLength: Not supported", new object[0]);
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000B0D2 File Offset: 0x000092D2
		// (set) Token: 0x06000361 RID: 865 RVA: 0x0000B0EF File Offset: 0x000092EF
		public override long Position
		{
			[DebuggerStepThrough]
			get
			{
				this.diagnosticsSession.TraceError("GetPosition: Not supported", new object[0]);
				throw new NotSupportedException();
			}
			[DebuggerStepThrough]
			set
			{
				this.diagnosticsSession.TraceError("SetPosition: Not supported", new object[0]);
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000B10C File Offset: 0x0000930C
		// (set) Token: 0x06000363 RID: 867 RVA: 0x0000B119 File Offset: 0x00009319
		public override int WriteTimeout
		{
			[DebuggerStepThrough]
			get
			{
				return this.networkStream.WriteTimeout;
			}
			[DebuggerStepThrough]
			set
			{
				this.networkStream.WriteTimeout = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0000B127 File Offset: 0x00009327
		// (set) Token: 0x06000365 RID: 869 RVA: 0x0000B134 File Offset: 0x00009334
		public override int ReadTimeout
		{
			[DebuggerStepThrough]
			get
			{
				return this.networkStream.ReadTimeout;
			}
			[DebuggerStepThrough]
			set
			{
				this.networkStream.ReadTimeout = value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0000B142 File Offset: 0x00009342
		// (set) Token: 0x06000367 RID: 871 RVA: 0x0000B14A File Offset: 0x0000934A
		public Guid ContextId
		{
			[DebuggerStepThrough]
			get
			{
				return this.contextId;
			}
			[DebuggerStepThrough]
			set
			{
				this.contextId = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0000B153 File Offset: 0x00009353
		internal IPEndPoint LocalEndPoint
		{
			[DebuggerStepThrough]
			get
			{
				return this.networkStream.NetworkConnection.LocalEndPoint;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000B165 File Offset: 0x00009365
		internal IPEndPoint RemoteEndPoint
		{
			[DebuggerStepThrough]
			get
			{
				return this.networkStream.NetworkConnection.RemoteEndPoint;
			}
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000B177 File Offset: 0x00009377
		public override void Flush()
		{
			this.diagnosticsSession.TraceDebug("Flush", new object[0]);
			this.WritePacketHeader(0);
			this.stream.Flush();
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000B1A1 File Offset: 0x000093A1
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.diagnosticsSession.TraceError("Seek: Not supported", new object[0]);
			throw new NotSupportedException();
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000B1BE File Offset: 0x000093BE
		public override void SetLength(long value)
		{
			this.diagnosticsSession.TraceError("SetLength: Not supported", new object[0]);
			throw new NotSupportedException();
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000B1DC File Offset: 0x000093DC
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.diagnosticsSession.TraceDebug<int>("Write: {0} bytes", count);
			StreamChannel.AsyncResult asyncResult = (StreamChannel.AsyncResult)this.BeginWrite(buffer, offset, count, null, null);
			try
			{
				this.EndWrite(asyncResult);
			}
			finally
			{
				asyncResult.InternalCleanup();
			}
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000B22C File Offset: 0x0000942C
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.diagnosticsSession.TraceDebug<int>("Read: requested={0}", count);
			StreamChannel.AsyncResult asyncResult = (StreamChannel.AsyncResult)this.BeginRead(buffer, offset, count, null, null);
			int num = 0;
			try
			{
				num = this.EndRead(asyncResult);
			}
			finally
			{
				asyncResult.InternalCleanup();
			}
			this.diagnosticsSession.TraceDebug<int, int>("Read: {0} bytes, {1} remaining in packet", num, this.bytesRemainingInPacket);
			return num;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000B298 File Offset: 0x00009498
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			this.diagnosticsSession.TraceDebug<int>("BeginRead: requested={0}", count);
			StreamChannel.AsyncResult asyncResult = new StreamChannel.AsyncResult(buffer, offset, count, callback, state);
			if (this.bytesRemainingInPacket == 0)
			{
				this.bytesRemainingInPacket = 4;
				this.AsyncReadHeaderBytes(asyncResult);
			}
			else
			{
				this.AsyncReadData(asyncResult);
			}
			return asyncResult;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000B2E4 File Offset: 0x000094E4
		public override int EndRead(IAsyncResult asyncResult)
		{
			this.diagnosticsSession.TraceDebug("EndRead", new object[0]);
			StreamChannel.AsyncResult asyncResult2 = StreamChannel.AsyncResult.EndAsyncOperation(asyncResult);
			Exception ex = asyncResult2.Result as Exception;
			if (ex != null)
			{
				this.diagnosticsSession.TraceError<Exception>("EndRead exception: {0}", ex);
				throw ex;
			}
			int num = (int)asyncResult2.Result;
			this.diagnosticsSession.TraceDebug<int, int>("EndRead: {0} bytes, {1} remaining in packet", num, this.bytesRemainingInPacket);
			return num;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000B354 File Offset: 0x00009554
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			this.diagnosticsSession.TraceDebug<int>("BeginWrite: {0} bytes", count);
			if (count <= 0 || count > 1048576)
			{
				throw new ArgumentException("count");
			}
			StreamChannel.AsyncResult asyncResult = new StreamChannel.AsyncResult(buffer, offset, count, callback, state);
			ExBitConverter.Write(count, this.packetLengthBuffer, 0);
			this.stream.BeginWrite(this.packetLengthBuffer, 0, this.packetLengthBuffer.Length, new AsyncCallback(this.WritePacketHeaderComplete), asyncResult);
			return asyncResult;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000B3D0 File Offset: 0x000095D0
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.diagnosticsSession.TraceDebug("EndWrite", new object[0]);
			StreamChannel.AsyncResult asyncResult2 = StreamChannel.AsyncResult.EndAsyncOperation(asyncResult);
			Exception ex = asyncResult2.Result as Exception;
			if (ex != null)
			{
				this.diagnosticsSession.TraceError<Exception>("EndWrite exception: {0}", ex);
				throw ex;
			}
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000B41C File Offset: 0x0000961C
		public override string ToString()
		{
			return string.Format("StreamChannel {0} Local: {1} Remote: {2}", this.ContextId, this.LocalEndPoint, this.RemoteEndPoint);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000B43F File Offset: 0x0000963F
		protected override void Dispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.stream != null)
			{
				this.stream.Dispose();
			}
			base.Dispose(calledFromDispose);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000B45E File Offset: 0x0000965E
		private void WritePacketHeader(int count)
		{
			ExBitConverter.Write(count, this.packetLengthBuffer, 0);
			this.stream.Write(this.packetLengthBuffer, 0, this.packetLengthBuffer.Length);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000B488 File Offset: 0x00009688
		private void AsyncReadHeaderBytes(StreamChannel.AsyncResult readAsyncResult)
		{
			this.stream.BeginRead(this.packetLengthBuffer, 4 - this.bytesRemainingInPacket, this.bytesRemainingInPacket, new AsyncCallback(this.ReadHeaderBytesComplete), readAsyncResult);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000B4B8 File Offset: 0x000096B8
		private void ReadHeaderBytesComplete(IAsyncResult readHeaderAsyncResult)
		{
			StreamChannel.AsyncResult asyncResult = (StreamChannel.AsyncResult)readHeaderAsyncResult.AsyncState;
			try
			{
				int num = this.stream.EndRead(readHeaderAsyncResult);
				if (num == 0)
				{
					asyncResult.InvokeCallback(new EndOfStreamException());
				}
				else
				{
					this.bytesRemainingInPacket -= num;
					if (this.bytesRemainingInPacket > 0)
					{
						this.AsyncReadHeaderBytes(asyncResult);
					}
					else
					{
						this.bytesRemainingInPacket = BitConverter.ToInt32(this.packetLengthBuffer, 0);
						if (this.bytesRemainingInPacket < 0 || this.bytesRemainingInPacket > 1048576)
						{
							throw new InvalidDataException("bytesRemainingInPacket");
						}
						if (this.bytesRemainingInPacket == 0)
						{
							asyncResult.InvokeCallback(0);
						}
						else
						{
							this.AsyncReadData(asyncResult);
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.diagnosticsSession.TraceError<Exception>("ReadHeaderBytesComplete exception: {0}", ex);
				asyncResult.InvokeCallback(ex);
			}
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000B594 File Offset: 0x00009794
		private void AsyncReadData(StreamChannel.AsyncResult readAsyncResult)
		{
			this.stream.BeginRead(readAsyncResult.Buffer, readAsyncResult.Offset, Math.Min(this.bytesRemainingInPacket, readAsyncResult.Count), new AsyncCallback(this.ReadDataComplete), readAsyncResult);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000B5CC File Offset: 0x000097CC
		private void ReadDataComplete(IAsyncResult asyncResult)
		{
			StreamChannel.AsyncResult asyncResult2 = (StreamChannel.AsyncResult)asyncResult.AsyncState;
			try
			{
				int num = this.stream.EndRead(asyncResult);
				if (num == 0)
				{
					asyncResult2.InvokeCallback(new EndOfStreamException());
				}
				else
				{
					this.bytesRemainingInPacket -= num;
					asyncResult2.Offset += num;
					asyncResult2.Count -= num;
					if (this.bytesRemainingInPacket == 0 || asyncResult2.Count == 0)
					{
						asyncResult2.InvokeCallback(asyncResult2.BytesRequested - asyncResult2.Count);
					}
					else
					{
						this.AsyncReadData(asyncResult2);
					}
				}
			}
			catch (Exception ex)
			{
				this.diagnosticsSession.TraceError<Exception>("ReadDataComplete exception: {0}", ex);
				asyncResult2.InvokeCallback(ex);
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000B690 File Offset: 0x00009890
		private void WritePacketHeaderComplete(IAsyncResult asyncResult)
		{
			StreamChannel.AsyncResult asyncResult2 = (StreamChannel.AsyncResult)asyncResult.AsyncState;
			try
			{
				this.stream.EndWrite(asyncResult);
				this.stream.BeginWrite(asyncResult2.Buffer, asyncResult2.Offset, asyncResult2.Count, new AsyncCallback(this.WriteDataComplete), asyncResult2);
			}
			catch (Exception ex)
			{
				this.diagnosticsSession.TraceError<Exception>("WritePacketHeaderComplete exception: {0}", ex);
				asyncResult2.InvokeCallback(ex);
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000B710 File Offset: 0x00009910
		private void WriteDataComplete(IAsyncResult asyncResult)
		{
			StreamChannel.AsyncResult asyncResult2 = (StreamChannel.AsyncResult)asyncResult.AsyncState;
			try
			{
				this.stream.EndWrite(asyncResult);
				asyncResult2.InvokeCallback();
			}
			catch (Exception ex)
			{
				this.diagnosticsSession.TraceError<Exception>("WriteDataComplete exception: {0}", ex);
				asyncResult2.InvokeCallback(ex);
			}
		}

		// Token: 0x0400017E RID: 382
		private const int PacketLengthBufferLength = 4;

		// Token: 0x0400017F RID: 383
		private const int FlushPacket = 0;

		// Token: 0x04000180 RID: 384
		private const int MaxPacketSize = 1048576;

		// Token: 0x04000181 RID: 385
		private readonly IDiagnosticsSession diagnosticsSession;

		// Token: 0x04000182 RID: 386
		private byte[] packetLengthBuffer = new byte[4];

		// Token: 0x04000183 RID: 387
		private NetworkConnectionStream networkStream;

		// Token: 0x04000184 RID: 388
		private Stream stream;

		// Token: 0x04000185 RID: 389
		private Guid contextId;

		// Token: 0x04000186 RID: 390
		private int bytesRemainingInPacket;

		// Token: 0x02000086 RID: 134
		private class AsyncResult : LazyAsyncResult
		{
			// Token: 0x0600037C RID: 892 RVA: 0x0000B76C File Offset: 0x0000996C
			internal AsyncResult(byte[] buffer, int offset, int count, AsyncCallback callback, object callerState) : base(null, callerState, callback)
			{
				this.buffer = buffer;
				this.offset = offset;
				this.count = count;
				this.bytesRequested = count;
			}

			// Token: 0x170000BC RID: 188
			// (get) Token: 0x0600037D RID: 893 RVA: 0x0000B795 File Offset: 0x00009995
			internal byte[] Buffer
			{
				[DebuggerStepThrough]
				get
				{
					return this.buffer;
				}
			}

			// Token: 0x170000BD RID: 189
			// (get) Token: 0x0600037E RID: 894 RVA: 0x0000B79D File Offset: 0x0000999D
			// (set) Token: 0x0600037F RID: 895 RVA: 0x0000B7A5 File Offset: 0x000099A5
			internal int Offset
			{
				[DebuggerStepThrough]
				get
				{
					return this.offset;
				}
				[DebuggerStepThrough]
				set
				{
					this.offset = value;
				}
			}

			// Token: 0x170000BE RID: 190
			// (get) Token: 0x06000380 RID: 896 RVA: 0x0000B7AE File Offset: 0x000099AE
			// (set) Token: 0x06000381 RID: 897 RVA: 0x0000B7B6 File Offset: 0x000099B6
			internal int Count
			{
				[DebuggerStepThrough]
				get
				{
					return this.count;
				}
				[DebuggerStepThrough]
				set
				{
					this.count = value;
				}
			}

			// Token: 0x170000BF RID: 191
			// (get) Token: 0x06000382 RID: 898 RVA: 0x0000B7BF File Offset: 0x000099BF
			internal int BytesRequested
			{
				[DebuggerStepThrough]
				get
				{
					return this.bytesRequested;
				}
			}

			// Token: 0x06000383 RID: 899 RVA: 0x0000B7C7 File Offset: 0x000099C7
			internal static StreamChannel.AsyncResult EndAsyncOperation(IAsyncResult asyncResult)
			{
				return LazyAsyncResult.EndAsyncOperation<StreamChannel.AsyncResult>(asyncResult);
			}

			// Token: 0x04000187 RID: 391
			private readonly int bytesRequested;

			// Token: 0x04000188 RID: 392
			private byte[] buffer;

			// Token: 0x04000189 RID: 393
			private int offset;

			// Token: 0x0400018A RID: 394
			private int count;
		}
	}
}
