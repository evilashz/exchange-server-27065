using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.PoolRpc;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.RpcProxy
{
	// Token: 0x0200001F RID: 31
	internal class RpcInstancePool : IDisposable
	{
		// Token: 0x060000EB RID: 235 RVA: 0x0000BD78 File Offset: 0x00009F78
		public RpcInstancePool(Guid instance, int generation)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.Generation = generation;
				this.handleToUse = 0L;
				this.pool = new PoolRpcClient("localhost", instance);
				this.contextHandles = new IntPtr[32];
				Guid empty = Guid.Empty;
				for (int i = 0; i < this.contextHandles.Length; i++)
				{
					IRpcAsyncResult asyncResult = this.pool.BeginEcPoolConnect(0U, empty, RpcServerBase.EmptyArraySegment, null, null);
					ArraySegment<byte> emptyArraySegment = RpcServerBase.EmptyArraySegment;
					IntPtr intPtr;
					try
					{
						uint num;
						uint num2;
						ErrorCode errorCode = ErrorCode.CreateWithLid((LID)56184U, (ErrorCodeValue)this.pool.EndEcPoolConnect(asyncResult, out intPtr, out num, out num2, out empty, out emptyArraySegment));
						if (ErrorCode.NoError != errorCode)
						{
							throw new FailRpcException("Failed to connect to instance pool", (int)errorCode);
						}
					}
					finally
					{
						RpcBufferPool.ReleaseBuffer(emptyArraySegment.Array);
					}
					this.contextHandles[i] = intPtr;
				}
				disposeGuard.Success();
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000EC RID: 236 RVA: 0x0000BE9C File Offset: 0x0000A09C
		// (set) Token: 0x060000ED RID: 237 RVA: 0x0000BEA4 File Offset: 0x0000A0A4
		public int Generation { get; private set; }

		// Token: 0x060000EE RID: 238 RVA: 0x0000BEB0 File Offset: 0x0000A0B0
		public IRpcAsyncResult BeginEcPoolCreateSession(byte[] sessionSecurityContext, uint flags, string userDn, uint connectionMode, uint codePageId, uint localeIdString, uint localeIdSort, short[] clientVersion, ArraySegment<byte> auxiliaryIn, AsyncCallback callback, object state)
		{
			return this.pool.BeginEcPoolCreateSession(this.GetContextHandle(), sessionSecurityContext, flags, userDn, connectionMode, codePageId, localeIdString, localeIdSort, clientVersion, auxiliaryIn, callback, state);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000BEE4 File Offset: 0x0000A0E4
		public ErrorCode EndEcPoolCreateSession(IRpcAsyncResult asyncResult, out uint sessionHandle, out string displayName, out uint maximumPolls, out uint retryCount, out uint retryDelay, out uint timeStamp, out short[] serverVersion, out short[] bestVersion, out ArraySegment<byte> auxOut)
		{
			return ErrorCode.CreateWithLid((LID)43896U, (ErrorCodeValue)this.pool.EndEcPoolCreateSession(asyncResult, out sessionHandle, out displayName, out maximumPolls, out retryCount, out retryDelay, out timeStamp, out serverVersion, out bestVersion, out auxOut));
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000BF90 File Offset: 0x0000A190
		public void BeginEcPoolCloseSession(uint sessionHandle, Action<object> callback, object state)
		{
			this.pool.BeginEcPoolCloseSession(this.GetContextHandle(), sessionHandle, delegate(IAsyncResult asyncResult)
			{
				try
				{
					this.pool.EndEcPoolCloseSession((IRpcAsyncResult)asyncResult);
				}
				catch (RpcException exception)
				{
					NullExecutionContext.Instance.Diagnostics.OnExceptionCatch(exception);
				}
				finally
				{
					callback(asyncResult.AsyncState);
				}
			}, state);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000BFD4 File Offset: 0x0000A1D4
		public IRpcAsyncResult BeginEcPoolSessionDoRpc(uint sessionHandle, uint flags, uint maximumResponseSize, ArraySegment<byte> request, ArraySegment<byte> auxiliaryIn, AsyncCallback callback, object state)
		{
			return this.pool.BeginEcPoolSessionDoRpc(this.GetContextHandle(), sessionHandle, flags, maximumResponseSize, request, auxiliaryIn, callback, state);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000BFFD File Offset: 0x0000A1FD
		public ErrorCode EndEcPoolSessionDoRpc(IRpcAsyncResult asyncResult, out uint flags, out ArraySegment<byte> response, out ArraySegment<byte> auxOut)
		{
			return ErrorCode.CreateWithLid((LID)45628U, (ErrorCodeValue)this.pool.EndEcPoolSessionDoRpc(asyncResult, out flags, out response, out auxOut));
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000C01E File Offset: 0x0000A21E
		public IRpcAsyncResult BeginEcPoolWaitForNotificationsAsync(AsyncCallback callback, object state)
		{
			return this.pool.BeginEcPoolWaitForNotificationsAsync(this.GetContextHandle(), callback, state);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000C033 File Offset: 0x0000A233
		public ErrorCode EndEcPoolWaitForNotificationsAsync(IRpcAsyncResult asyncResult, out uint[] sessionHandles)
		{
			return ErrorCode.CreateWithLid((LID)60280U, (ErrorCodeValue)this.pool.EndEcPoolWaitForNotificationsAsync(asyncResult, out sessionHandles));
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000C054 File Offset: 0x0000A254
		public void Dispose()
		{
			if (this.pool != null)
			{
				foreach (IntPtr intPtr in this.contextHandles)
				{
					if (IntPtr.Zero != intPtr)
					{
						this.pool.EcPoolDisconnect(intPtr);
					}
				}
				this.pool.Dispose();
				this.pool = null;
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000C0B8 File Offset: 0x0000A2B8
		private IntPtr GetContextHandle()
		{
			long num;
			long value;
			do
			{
				num = this.handleToUse;
				value = (num + 1L) % (long)this.contextHandles.Length;
			}
			while (num != Interlocked.CompareExchange(ref this.handleToUse, value, num));
			return this.contextHandles[(int)(checked((IntPtr)num))];
		}

		// Token: 0x04000082 RID: 130
		private PoolRpcClient pool;

		// Token: 0x04000083 RID: 131
		private IntPtr[] contextHandles;

		// Token: 0x04000084 RID: 132
		private long handleToUse;
	}
}
