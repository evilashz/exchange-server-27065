using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Rpc.RfriServer;
using msclr;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x020003A9 RID: 937
	internal abstract class BaseAsyncRpcState<Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcState_GetNewDSA,Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcServer,Microsoft::Exchange::Rpc::IRfriAsyncDispatch>
	{
		// Token: 0x06001083 RID: 4227 RVA: 0x0004DDB8 File Offset: 0x0004D1B8
		private static void Callback(ICancelableAsyncResult asyncResult)
		{
			((BaseAsyncRpcState<Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcState_GetNewDSA,Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcServer,Microsoft::Exchange::Rpc::IRfriAsyncDispatch>)asyncResult.AsyncState).InternalCallback(asyncResult);
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x0004CDD8 File Offset: 0x0004C1D8
		protected BaseAsyncRpcState<Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcState_GetNewDSA,Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcServer,Microsoft::Exchange::Rpc::IRfriAsyncDispatch>()
		{
			this.asyncState = null;
			this.asyncServer = null;
			this.asyncDispatch = null;
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06001085 RID: 4229 RVA: 0x0004CE2C File Offset: 0x0004C22C
		protected SafeRpcAsyncStateHandle AsyncState
		{
			get
			{
				return this.asyncState;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06001086 RID: 4230 RVA: 0x0004CE40 File Offset: 0x0004C240
		protected RfriAsyncRpcServer AsyncServer
		{
			get
			{
				return this.asyncServer;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06001087 RID: 4231 RVA: 0x0004CE54 File Offset: 0x0004C254
		protected IRfriAsyncDispatch AsyncDispatch
		{
			get
			{
				return this.asyncDispatch;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06001088 RID: 4232 RVA: 0x0004CE68 File Offset: 0x0004C268
		protected ArraySegment<byte> EmptyByteArraySegment
		{
			get
			{
				return BaseAsyncRpcState<Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcState_GetNewDSA,Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcServer,Microsoft::Exchange::Rpc::IRfriAsyncDispatch>.emptyByteArraySegment;
			}
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x0004CE00 File Offset: 0x0004C200
		protected void InternalInitialize(SafeRpcAsyncStateHandle asyncState, RfriAsyncRpcServer asyncServer)
		{
			this.asyncState = asyncState;
			this.asyncServer = asyncServer;
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x0004D9F8 File Offset: 0x0004CDF8
		protected void InternalCallback(ICancelableAsyncResult asyncResult)
		{
			try
			{
				int result = this.InternalEnd(asyncResult);
				this.asyncState.CompleteCall(result);
			}
			catch (RpcException ex)
			{
				this.asyncState.AbortCall((uint)ex.ErrorCode);
			}
			catch (FailRpcException ex2)
			{
				this.asyncState.CompleteCall(ex2.ErrorCode);
			}
			catch (ThreadAbortException)
			{
				this.asyncState.AbortCall(1726U);
			}
			catch (OutOfMemoryException)
			{
				this.asyncState.AbortCall(1130U);
			}
			catch (Exception e)
			{
				<Module>.Microsoft.Exchange.Rpc.ManagedExceptionCrashWrapper.CrashMeNow(e);
			}
			catch (object o)
			{
				<Module>.Microsoft.Exchange.Rpc.ManagedExceptionCrashWrapper.CrashMeNow(o);
			}
			finally
			{
				this.ReleaseToPool();
			}
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x0004D598 File Offset: 0x0004C998
		protected void ReleaseToPool()
		{
			@lock @lock = null;
			this.InternalReset();
			this.asyncState = null;
			this.asyncServer = null;
			this.asyncDispatch = null;
			RfriAsyncRpcState_GetNewDSA item = (RfriAsyncRpcState_GetNewDSA)this;
			@lock lock2 = new @lock(BaseAsyncRpcState<Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcState_GetNewDSA,Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcServer,Microsoft::Exchange::Rpc::IRfriAsyncDispatch>.freePoolLock);
			try
			{
				@lock = lock2;
				if (BaseAsyncRpcState<Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcState_GetNewDSA,Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcServer,Microsoft::Exchange::Rpc::IRfriAsyncDispatch>.freePool.Count < this.PoolSize())
				{
					BaseAsyncRpcState<Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcState_GetNewDSA,Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcServer,Microsoft::Exchange::Rpc::IRfriAsyncDispatch>.freePool.Push(item);
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x0004D39C File Offset: 0x0004C79C
		public static RfriAsyncRpcState_GetNewDSA CreateFromPool()
		{
			@lock @lock = null;
			RfriAsyncRpcState_GetNewDSA rfriAsyncRpcState_GetNewDSA = null;
			@lock lock2 = new @lock(BaseAsyncRpcState<Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcState_GetNewDSA,Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcServer,Microsoft::Exchange::Rpc::IRfriAsyncDispatch>.freePoolLock);
			try
			{
				@lock = lock2;
				if (BaseAsyncRpcState<Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcState_GetNewDSA,Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcServer,Microsoft::Exchange::Rpc::IRfriAsyncDispatch>.freePool.Count > 0)
				{
					rfriAsyncRpcState_GetNewDSA = BaseAsyncRpcState<Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcState_GetNewDSA,Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcServer,Microsoft::Exchange::Rpc::IRfriAsyncDispatch>.freePool.Pop();
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
			if (rfriAsyncRpcState_GetNewDSA == null)
			{
				rfriAsyncRpcState_GetNewDSA = new RfriAsyncRpcState_GetNewDSA();
			}
			return rfriAsyncRpcState_GetNewDSA;
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x0004CE1C File Offset: 0x0004C21C
		public virtual int PoolSize()
		{
			return 100;
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x0004D708 File Offset: 0x0004CB08
		public void Begin()
		{
			bool flag = false;
			try
			{
				IRfriAsyncDispatch rfriAsyncDispatch = this.asyncServer.GetAsyncDispatch();
				this.asyncDispatch = rfriAsyncDispatch;
				if (rfriAsyncDispatch == null)
				{
					this.asyncState.AbortCall(1722U);
				}
				else
				{
					this.InternalBegin(BaseAsyncRpcState<Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcState_GetNewDSA,Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcServer,Microsoft::Exchange::Rpc::IRfriAsyncDispatch>.asyncCallback);
				}
				flag = true;
			}
			catch (RpcException ex)
			{
				this.asyncState.AbortCall((uint)ex.ErrorCode);
			}
			catch (FailRpcException ex2)
			{
				this.asyncState.CompleteCall(ex2.ErrorCode);
			}
			catch (ThreadAbortException)
			{
				this.asyncState.AbortCall(1726U);
			}
			catch (OutOfMemoryException)
			{
				this.asyncState.AbortCall(1130U);
			}
			catch (Exception e)
			{
				<Module>.Microsoft.Exchange.Rpc.ManagedExceptionCrashWrapper.CrashMeNow(e);
			}
			catch (object o)
			{
				<Module>.Microsoft.Exchange.Rpc.ManagedExceptionCrashWrapper.CrashMeNow(o);
			}
			finally
			{
				if (!flag)
				{
					this.ReleaseToPool();
				}
			}
		}

		// Token: 0x0600108F RID: 4239
		public abstract void InternalBegin(CancelableAsyncCallback asyncCallback);

		// Token: 0x06001090 RID: 4240
		public abstract int InternalEnd(ICancelableAsyncResult asyncResult);

		// Token: 0x06001091 RID: 4241
		public abstract void InternalReset();

		// Token: 0x06001092 RID: 4242 RVA: 0x0004DDF8 File Offset: 0x0004D1F8
		// Note: this type is marked as 'beforefieldinit'.
		static BaseAsyncRpcState<Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcState_GetNewDSA,Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcServer,Microsoft::Exchange::Rpc::IRfriAsyncDispatch>()
		{
			ArraySegment<byte> arraySegment = new ArraySegment<byte>(BaseAsyncRpcState<Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcState_GetNewDSA,Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcServer,Microsoft::Exchange::Rpc::IRfriAsyncDispatch>.emptyByteArray);
			BaseAsyncRpcState<Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcState_GetNewDSA,Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcServer,Microsoft::Exchange::Rpc::IRfriAsyncDispatch>.emptyByteArraySegment = arraySegment;
		}

		// Token: 0x04000FA8 RID: 4008
		private static readonly object freePoolLock = new object();

		// Token: 0x04000FA9 RID: 4009
		private static readonly Stack<RfriAsyncRpcState_GetNewDSA> freePool = new Stack<RfriAsyncRpcState_GetNewDSA>();

		// Token: 0x04000FAA RID: 4010
		private static readonly CancelableAsyncCallback asyncCallback = new CancelableAsyncCallback(BaseAsyncRpcState<Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcState_GetNewDSA,Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcServer,Microsoft::Exchange::Rpc::IRfriAsyncDispatch>.Callback);

		// Token: 0x04000FAB RID: 4011
		private static readonly byte[] emptyByteArray = new byte[0];

		// Token: 0x04000FAC RID: 4012
		private static readonly ArraySegment<byte> emptyByteArraySegment;

		// Token: 0x04000FAD RID: 4013
		private SafeRpcAsyncStateHandle asyncState;

		// Token: 0x04000FAE RID: 4014
		private RfriAsyncRpcServer asyncServer;

		// Token: 0x04000FAF RID: 4015
		private IRfriAsyncDispatch asyncDispatch;
	}
}
