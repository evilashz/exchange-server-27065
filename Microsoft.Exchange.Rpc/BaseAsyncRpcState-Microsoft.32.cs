using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Rpc.RfriServer;
using msclr;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x020003AB RID: 939
	internal abstract class BaseAsyncRpcState<Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcState_GetFQDNFromLegacyDN,Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcServer,Microsoft::Exchange::Rpc::IRfriAsyncDispatch>
	{
		// Token: 0x06001098 RID: 4248 RVA: 0x0004DDD8 File Offset: 0x0004D1D8
		private static void Callback(ICancelableAsyncResult asyncResult)
		{
			((BaseAsyncRpcState<Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcState_GetFQDNFromLegacyDN,Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcServer,Microsoft::Exchange::Rpc::IRfriAsyncDispatch>)asyncResult.AsyncState).InternalCallback(asyncResult);
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0004CE7C File Offset: 0x0004C27C
		protected BaseAsyncRpcState<Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcState_GetFQDNFromLegacyDN,Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcServer,Microsoft::Exchange::Rpc::IRfriAsyncDispatch>()
		{
			this.asyncState = null;
			this.asyncServer = null;
			this.asyncDispatch = null;
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x0600109A RID: 4250 RVA: 0x0004CED0 File Offset: 0x0004C2D0
		protected SafeRpcAsyncStateHandle AsyncState
		{
			get
			{
				return this.asyncState;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600109B RID: 4251 RVA: 0x0004CEE4 File Offset: 0x0004C2E4
		protected RfriAsyncRpcServer AsyncServer
		{
			get
			{
				return this.asyncServer;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600109C RID: 4252 RVA: 0x0004CEF8 File Offset: 0x0004C2F8
		protected IRfriAsyncDispatch AsyncDispatch
		{
			get
			{
				return this.asyncDispatch;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600109D RID: 4253 RVA: 0x0004CF0C File Offset: 0x0004C30C
		protected ArraySegment<byte> EmptyByteArraySegment
		{
			get
			{
				return BaseAsyncRpcState<Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcState_GetFQDNFromLegacyDN,Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcServer,Microsoft::Exchange::Rpc::IRfriAsyncDispatch>.emptyByteArraySegment;
			}
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x0004CEA4 File Offset: 0x0004C2A4
		protected void InternalInitialize(SafeRpcAsyncStateHandle asyncState, RfriAsyncRpcServer asyncServer)
		{
			this.asyncState = asyncState;
			this.asyncServer = asyncServer;
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x0004DB38 File Offset: 0x0004CF38
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

		// Token: 0x060010A0 RID: 4256 RVA: 0x0004D620 File Offset: 0x0004CA20
		protected void ReleaseToPool()
		{
			@lock @lock = null;
			this.InternalReset();
			this.asyncState = null;
			this.asyncServer = null;
			this.asyncDispatch = null;
			RfriAsyncRpcState_GetFQDNFromLegacyDN item = (RfriAsyncRpcState_GetFQDNFromLegacyDN)this;
			@lock lock2 = new @lock(BaseAsyncRpcState<Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcState_GetFQDNFromLegacyDN,Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcServer,Microsoft::Exchange::Rpc::IRfriAsyncDispatch>.freePoolLock);
			try
			{
				@lock = lock2;
				if (BaseAsyncRpcState<Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcState_GetFQDNFromLegacyDN,Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcServer,Microsoft::Exchange::Rpc::IRfriAsyncDispatch>.freePool.Count < this.PoolSize())
				{
					BaseAsyncRpcState<Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcState_GetFQDNFromLegacyDN,Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcServer,Microsoft::Exchange::Rpc::IRfriAsyncDispatch>.freePool.Push(item);
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x0004D40C File Offset: 0x0004C80C
		public static RfriAsyncRpcState_GetFQDNFromLegacyDN CreateFromPool()
		{
			@lock @lock = null;
			RfriAsyncRpcState_GetFQDNFromLegacyDN rfriAsyncRpcState_GetFQDNFromLegacyDN = null;
			@lock lock2 = new @lock(BaseAsyncRpcState<Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcState_GetFQDNFromLegacyDN,Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcServer,Microsoft::Exchange::Rpc::IRfriAsyncDispatch>.freePoolLock);
			try
			{
				@lock = lock2;
				if (BaseAsyncRpcState<Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcState_GetFQDNFromLegacyDN,Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcServer,Microsoft::Exchange::Rpc::IRfriAsyncDispatch>.freePool.Count > 0)
				{
					rfriAsyncRpcState_GetFQDNFromLegacyDN = BaseAsyncRpcState<Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcState_GetFQDNFromLegacyDN,Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcServer,Microsoft::Exchange::Rpc::IRfriAsyncDispatch>.freePool.Pop();
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
			if (rfriAsyncRpcState_GetFQDNFromLegacyDN == null)
			{
				rfriAsyncRpcState_GetFQDNFromLegacyDN = new RfriAsyncRpcState_GetFQDNFromLegacyDN();
			}
			return rfriAsyncRpcState_GetFQDNFromLegacyDN;
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x0004CEC0 File Offset: 0x0004C2C0
		public virtual int PoolSize()
		{
			return 100;
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x0004D880 File Offset: 0x0004CC80
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
					this.InternalBegin(BaseAsyncRpcState<Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcState_GetFQDNFromLegacyDN,Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcServer,Microsoft::Exchange::Rpc::IRfriAsyncDispatch>.asyncCallback);
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

		// Token: 0x060010A4 RID: 4260
		public abstract void InternalBegin(CancelableAsyncCallback asyncCallback);

		// Token: 0x060010A5 RID: 4261
		public abstract int InternalEnd(ICancelableAsyncResult asyncResult);

		// Token: 0x060010A6 RID: 4262
		public abstract void InternalReset();

		// Token: 0x060010A7 RID: 4263 RVA: 0x0004DE48 File Offset: 0x0004D248
		// Note: this type is marked as 'beforefieldinit'.
		static BaseAsyncRpcState<Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcState_GetFQDNFromLegacyDN,Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcServer,Microsoft::Exchange::Rpc::IRfriAsyncDispatch>()
		{
			ArraySegment<byte> arraySegment = new ArraySegment<byte>(BaseAsyncRpcState<Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcState_GetFQDNFromLegacyDN,Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcServer,Microsoft::Exchange::Rpc::IRfriAsyncDispatch>.emptyByteArray);
			BaseAsyncRpcState<Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcState_GetFQDNFromLegacyDN,Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcServer,Microsoft::Exchange::Rpc::IRfriAsyncDispatch>.emptyByteArraySegment = arraySegment;
		}

		// Token: 0x04000FB6 RID: 4022
		private static readonly object freePoolLock = new object();

		// Token: 0x04000FB7 RID: 4023
		private static readonly Stack<RfriAsyncRpcState_GetFQDNFromLegacyDN> freePool = new Stack<RfriAsyncRpcState_GetFQDNFromLegacyDN>();

		// Token: 0x04000FB8 RID: 4024
		private static readonly CancelableAsyncCallback asyncCallback = new CancelableAsyncCallback(BaseAsyncRpcState<Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcState_GetFQDNFromLegacyDN,Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcServer,Microsoft::Exchange::Rpc::IRfriAsyncDispatch>.Callback);

		// Token: 0x04000FB9 RID: 4025
		private static readonly byte[] emptyByteArray = new byte[0];

		// Token: 0x04000FBA RID: 4026
		private static readonly ArraySegment<byte> emptyByteArraySegment;

		// Token: 0x04000FBB RID: 4027
		private SafeRpcAsyncStateHandle asyncState;

		// Token: 0x04000FBC RID: 4028
		private RfriAsyncRpcServer asyncServer;

		// Token: 0x04000FBD RID: 4029
		private IRfriAsyncDispatch asyncDispatch;
	}
}
