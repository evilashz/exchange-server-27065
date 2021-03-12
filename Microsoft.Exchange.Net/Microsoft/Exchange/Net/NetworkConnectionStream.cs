using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000C5B RID: 3163
	internal class NetworkConnectionStream : Stream
	{
		// Token: 0x06004613 RID: 17939 RVA: 0x000BB120 File Offset: 0x000B9320
		internal NetworkConnectionStream(Socket socket) : this(new NetworkConnection(socket, 4096))
		{
		}

		// Token: 0x06004614 RID: 17940 RVA: 0x000BB133 File Offset: 0x000B9333
		internal NetworkConnectionStream(NetworkConnection nc)
		{
			if (nc == null)
			{
				throw new ArgumentNullException("nc");
			}
			this.networkConnection = nc;
		}

		// Token: 0x170011A6 RID: 4518
		// (get) Token: 0x06004615 RID: 17941 RVA: 0x000BB150 File Offset: 0x000B9350
		public override bool CanRead
		{
			[DebuggerStepThrough]
			get
			{
				return true;
			}
		}

		// Token: 0x170011A7 RID: 4519
		// (get) Token: 0x06004616 RID: 17942 RVA: 0x000BB153 File Offset: 0x000B9353
		public override bool CanSeek
		{
			[DebuggerStepThrough]
			get
			{
				return false;
			}
		}

		// Token: 0x170011A8 RID: 4520
		// (get) Token: 0x06004617 RID: 17943 RVA: 0x000BB156 File Offset: 0x000B9356
		public override bool CanTimeout
		{
			[DebuggerStepThrough]
			get
			{
				return true;
			}
		}

		// Token: 0x170011A9 RID: 4521
		// (get) Token: 0x06004618 RID: 17944 RVA: 0x000BB159 File Offset: 0x000B9359
		public override bool CanWrite
		{
			[DebuggerStepThrough]
			get
			{
				return true;
			}
		}

		// Token: 0x170011AA RID: 4522
		// (get) Token: 0x06004619 RID: 17945 RVA: 0x000BB15C File Offset: 0x000B935C
		public override long Length
		{
			[DebuggerStepThrough]
			get
			{
				ExTraceGlobals.NetworkTracer.TraceError(this.networkConnection.ConnectionId, "GetLength: Not supported");
				throw new NotSupportedException();
			}
		}

		// Token: 0x170011AB RID: 4523
		// (get) Token: 0x0600461A RID: 17946 RVA: 0x000BB17D File Offset: 0x000B937D
		// (set) Token: 0x0600461B RID: 17947 RVA: 0x000BB19E File Offset: 0x000B939E
		public override long Position
		{
			[DebuggerStepThrough]
			get
			{
				ExTraceGlobals.NetworkTracer.TraceError(this.networkConnection.ConnectionId, "GetPosition: Not supported");
				throw new NotSupportedException();
			}
			[DebuggerStepThrough]
			set
			{
				ExTraceGlobals.NetworkTracer.TraceError(this.networkConnection.ConnectionId, "SetPosition: Not supported");
				throw new NotSupportedException();
			}
		}

		// Token: 0x170011AC RID: 4524
		// (get) Token: 0x0600461C RID: 17948 RVA: 0x000BB1BF File Offset: 0x000B93BF
		// (set) Token: 0x0600461D RID: 17949 RVA: 0x000BB1D2 File Offset: 0x000B93D2
		public override int ReadTimeout
		{
			[DebuggerStepThrough]
			get
			{
				return this.networkConnection.ReceiveTimeout * 1000;
			}
			[DebuggerStepThrough]
			set
			{
				this.networkConnection.ReceiveTimeout = value / 1000;
			}
		}

		// Token: 0x170011AD RID: 4525
		// (get) Token: 0x0600461E RID: 17950 RVA: 0x000BB1E6 File Offset: 0x000B93E6
		// (set) Token: 0x0600461F RID: 17951 RVA: 0x000BB1F9 File Offset: 0x000B93F9
		public override int WriteTimeout
		{
			[DebuggerStepThrough]
			get
			{
				return this.networkConnection.SendTimeout * 1000;
			}
			[DebuggerStepThrough]
			set
			{
				this.networkConnection.SendTimeout = value / 1000;
			}
		}

		// Token: 0x170011AE RID: 4526
		// (get) Token: 0x06004620 RID: 17952 RVA: 0x000BB20D File Offset: 0x000B940D
		public NetworkConnection NetworkConnection
		{
			[DebuggerStepThrough]
			get
			{
				return this.networkConnection;
			}
		}

		// Token: 0x06004621 RID: 17953 RVA: 0x000BB215 File Offset: 0x000B9415
		public override void Flush()
		{
		}

		// Token: 0x06004622 RID: 17954 RVA: 0x000BB217 File Offset: 0x000B9417
		public override long Seek(long offset, SeekOrigin origin)
		{
			ExTraceGlobals.NetworkTracer.TraceError(this.networkConnection.ConnectionId, "Seek: Not supported");
			throw new NotSupportedException();
		}

		// Token: 0x06004623 RID: 17955 RVA: 0x000BB238 File Offset: 0x000B9438
		public override void SetLength(long value)
		{
			ExTraceGlobals.NetworkTracer.TraceError(this.networkConnection.ConnectionId, "SetLength: Not supported");
			throw new NotSupportedException();
		}

		// Token: 0x06004624 RID: 17956 RVA: 0x000BB25C File Offset: 0x000B945C
		public override void Write(byte[] buffer, int offset, int count)
		{
			ExTraceGlobals.NetworkTracer.TraceDebug<int>(this.networkConnection.ConnectionId, "Write: {0} bytes", count);
			object errorCode;
			this.networkConnection.Write(buffer, offset, count, out errorCode);
			this.ThrowOnError(NetworkConnectionStream.Function.Write, errorCode);
		}

		// Token: 0x06004625 RID: 17957 RVA: 0x000BB29C File Offset: 0x000B949C
		public override int Read(byte[] buffer, int offset, int count)
		{
			ExTraceGlobals.NetworkTracer.TraceDebug<int>(this.networkConnection.ConnectionId, "Read: requested {0} bytes", count);
			byte[] src;
			int srcOffset;
			int num;
			object errorCode;
			this.networkConnection.Read(out src, out srcOffset, out num, out errorCode);
			this.ThrowOnError(NetworkConnectionStream.Function.Read, errorCode);
			int num2 = Math.Min(count, num);
			if (num2 != 0)
			{
				Buffer.BlockCopy(src, srcOffset, buffer, offset, num2);
			}
			this.networkConnection.PutBackReceivedBytes(num - num2);
			ExTraceGlobals.NetworkTracer.TraceDebug<int>(this.networkConnection.ConnectionId, "Read: {0} bytes returned", num2);
			return num2;
		}

		// Token: 0x06004626 RID: 17958 RVA: 0x000BB324 File Offset: 0x000B9524
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			ExTraceGlobals.NetworkTracer.TraceDebug<int>(this.networkConnection.ConnectionId, "BeginRead: requested {0} bytes", count);
			NetworkConnectionStream.AsyncResult asyncResult = new NetworkConnectionStream.AsyncResult(new ArraySegment<byte>(buffer, offset, count), callback, state);
			this.networkConnection.BeginRead(new AsyncCallback(this.ReadComplete), asyncResult);
			return asyncResult;
		}

		// Token: 0x06004627 RID: 17959 RVA: 0x000BB380 File Offset: 0x000B9580
		public override int EndRead(IAsyncResult asyncResult)
		{
			NetworkConnectionStream.AsyncResult asyncResult2 = NetworkConnectionStream.AsyncResult.EndAsyncOperation(asyncResult);
			if (asyncResult2.Result is ArraySegment<byte>)
			{
				ArraySegment<byte> arraySegment = (ArraySegment<byte>)asyncResult2.Result;
				ArraySegment<byte> arraySegment2 = (ArraySegment<byte>)asyncResult2.AsyncObject;
				int num = Math.Min(arraySegment2.Count, arraySegment.Count);
				if (num != 0)
				{
					Buffer.BlockCopy(arraySegment.Array, arraySegment.Offset, arraySegment2.Array, arraySegment2.Offset, num);
				}
				this.networkConnection.PutBackReceivedBytes(arraySegment.Count - num);
				ExTraceGlobals.NetworkTracer.TraceDebug<int>(this.networkConnection.ConnectionId, "EndRead: {0} bytes returned", num);
				return num;
			}
			this.ThrowOnError(NetworkConnectionStream.Function.EndRead, asyncResult2.Result);
			throw new InvalidOperationException();
		}

		// Token: 0x06004628 RID: 17960 RVA: 0x000BB438 File Offset: 0x000B9638
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			ExTraceGlobals.NetworkTracer.TraceDebug<int>(this.networkConnection.ConnectionId, "BeginWrite: {0} bytes", count);
			NetworkConnectionStream.AsyncResult asyncResult = new NetworkConnectionStream.AsyncResult(null, callback, state);
			this.networkConnection.BeginWrite(buffer, offset, count, new AsyncCallback(this.WriteComplete), asyncResult);
			return asyncResult;
		}

		// Token: 0x06004629 RID: 17961 RVA: 0x000BB488 File Offset: 0x000B9688
		public override void EndWrite(IAsyncResult asyncResult)
		{
			ExTraceGlobals.NetworkTracer.TraceDebug(this.networkConnection.ConnectionId, "EndWrite");
			NetworkConnectionStream.AsyncResult asyncResult2 = NetworkConnectionStream.AsyncResult.EndAsyncOperation(asyncResult);
			this.ThrowOnError(NetworkConnectionStream.Function.EndWrite, asyncResult2.Result);
		}

		// Token: 0x0600462A RID: 17962 RVA: 0x000BB4C3 File Offset: 0x000B96C3
		public override string ToString()
		{
			return string.Format("NetworkConnectionChannel: {0}", this.networkConnection.ConnectionId);
		}

		// Token: 0x0600462B RID: 17963 RVA: 0x000BB4DF File Offset: 0x000B96DF
		protected override void Dispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				this.networkConnection.Dispose();
			}
			base.Dispose(calledFromDispose);
		}

		// Token: 0x0600462C RID: 17964 RVA: 0x000BB4F8 File Offset: 0x000B96F8
		private void ReadComplete(IAsyncResult iasyncResult)
		{
			NetworkConnectionStream.AsyncResult asyncResult = (NetworkConnectionStream.AsyncResult)iasyncResult.AsyncState;
			byte[] array;
			int offset;
			int count;
			object obj;
			this.networkConnection.EndRead(iasyncResult, out array, out offset, out count, out obj);
			if (obj == null)
			{
				asyncResult.InvokeCallback(new ArraySegment<byte>(array, offset, count));
				return;
			}
			asyncResult.InvokeCallback(obj);
		}

		// Token: 0x0600462D RID: 17965 RVA: 0x000BB548 File Offset: 0x000B9748
		private void WriteComplete(IAsyncResult iasyncResult)
		{
			NetworkConnectionStream.AsyncResult asyncResult = (NetworkConnectionStream.AsyncResult)iasyncResult.AsyncState;
			object value;
			this.networkConnection.EndWrite(iasyncResult, out value);
			asyncResult.InvokeCallback(value);
		}

		// Token: 0x0600462E RID: 17966 RVA: 0x000BB578 File Offset: 0x000B9778
		private void ThrowOnError(NetworkConnectionStream.Function function, object errorCode)
		{
			if (errorCode == null)
			{
				return;
			}
			ExTraceGlobals.NetworkTracer.TraceDebug<NetworkConnectionStream.Function, object>(this.networkConnection.ConnectionId, "{0}: Error: {1}", function, errorCode);
			if (errorCode is SocketError)
			{
				throw new IOException(function.ToString(), new SocketException((int)errorCode));
			}
			throw new IOException(string.Format("Unknown error in function {0}: {1}", function, errorCode));
		}

		// Token: 0x04003A91 RID: 14993
		private const int MillisecondsPerSecond = 1000;

		// Token: 0x04003A92 RID: 14994
		private NetworkConnection networkConnection;

		// Token: 0x02000C5C RID: 3164
		private enum Function
		{
			// Token: 0x04003A94 RID: 14996
			Read,
			// Token: 0x04003A95 RID: 14997
			EndRead,
			// Token: 0x04003A96 RID: 14998
			Write,
			// Token: 0x04003A97 RID: 14999
			EndWrite
		}

		// Token: 0x02000C5D RID: 3165
		private class AsyncResult : LazyAsyncResult
		{
			// Token: 0x0600462F RID: 17967 RVA: 0x000BB5DF File Offset: 0x000B97DF
			internal AsyncResult(object worker, AsyncCallback callback, object state) : base(worker, state, callback)
			{
			}

			// Token: 0x06004630 RID: 17968 RVA: 0x000BB5EA File Offset: 0x000B97EA
			internal static NetworkConnectionStream.AsyncResult EndAsyncOperation(IAsyncResult asyncResult)
			{
				return LazyAsyncResult.EndAsyncOperation<NetworkConnectionStream.AsyncResult>(asyncResult);
			}
		}
	}
}
