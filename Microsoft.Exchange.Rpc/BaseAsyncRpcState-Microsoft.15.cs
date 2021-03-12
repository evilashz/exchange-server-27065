using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Rpc.NspiServer;
using msclr;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x02000305 RID: 773
	internal abstract class BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_GetMatches,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>
	{
		// Token: 0x06000E1D RID: 3613 RVA: 0x00040084 File Offset: 0x0003F484
		private static void Callback(ICancelableAsyncResult asyncResult)
		{
			((BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_GetMatches,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>)asyncResult.AsyncState).InternalCallback(asyncResult);
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x000375BC File Offset: 0x000369BC
		protected BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_GetMatches,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>()
		{
			this.asyncState = null;
			this.asyncServer = null;
			this.asyncDispatch = null;
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x00037610 File Offset: 0x00036A10
		protected SafeRpcAsyncStateHandle AsyncState
		{
			get
			{
				return this.asyncState;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000E20 RID: 3616 RVA: 0x00037624 File Offset: 0x00036A24
		protected NspiAsyncRpcServer AsyncServer
		{
			get
			{
				return this.asyncServer;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000E21 RID: 3617 RVA: 0x00037638 File Offset: 0x00036A38
		protected INspiAsyncDispatch AsyncDispatch
		{
			get
			{
				return this.asyncDispatch;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x0003764C File Offset: 0x00036A4C
		protected ArraySegment<byte> EmptyByteArraySegment
		{
			get
			{
				return BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_GetMatches,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.emptyByteArraySegment;
			}
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x000375E4 File Offset: 0x000369E4
		protected void InternalInitialize(SafeRpcAsyncStateHandle asyncState, NspiAsyncRpcServer asyncServer)
		{
			this.asyncState = asyncState;
			this.asyncServer = asyncServer;
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x0003E020 File Offset: 0x0003D420
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

		// Token: 0x06000E25 RID: 3621 RVA: 0x0003A9B8 File Offset: 0x00039DB8
		protected void ReleaseToPool()
		{
			@lock @lock = null;
			this.InternalReset();
			this.asyncState = null;
			this.asyncServer = null;
			this.asyncDispatch = null;
			NspiAsyncRpcState_GetMatches item = (NspiAsyncRpcState_GetMatches)this;
			@lock lock2 = new @lock(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_GetMatches,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.freePoolLock);
			try
			{
				@lock = lock2;
				if (BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_GetMatches,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.freePool.Count < this.PoolSize())
				{
					BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_GetMatches,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.freePool.Push(item);
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x00039EF4 File Offset: 0x000392F4
		public static NspiAsyncRpcState_GetMatches CreateFromPool()
		{
			@lock @lock = null;
			NspiAsyncRpcState_GetMatches nspiAsyncRpcState_GetMatches = null;
			@lock lock2 = new @lock(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_GetMatches,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.freePoolLock);
			try
			{
				@lock = lock2;
				if (BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_GetMatches,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.freePool.Count > 0)
				{
					nspiAsyncRpcState_GetMatches = BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_GetMatches,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.freePool.Pop();
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
			if (nspiAsyncRpcState_GetMatches == null)
			{
				nspiAsyncRpcState_GetMatches = new NspiAsyncRpcState_GetMatches();
			}
			return nspiAsyncRpcState_GetMatches;
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x00037600 File Offset: 0x00036A00
		public virtual int PoolSize()
		{
			return 100;
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x0003C260 File Offset: 0x0003B660
		public void Begin()
		{
			bool flag = false;
			try
			{
				INspiAsyncDispatch nspiAsyncDispatch = this.asyncServer.GetAsyncDispatch();
				this.asyncDispatch = nspiAsyncDispatch;
				if (nspiAsyncDispatch == null)
				{
					this.asyncState.AbortCall(1722U);
				}
				else
				{
					this.InternalBegin(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_GetMatches,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.asyncCallback);
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

		// Token: 0x06000E29 RID: 3625
		public abstract void InternalBegin(CancelableAsyncCallback asyncCallback);

		// Token: 0x06000E2A RID: 3626
		public abstract int InternalEnd(ICancelableAsyncResult asyncResult);

		// Token: 0x06000E2B RID: 3627
		public abstract void InternalReset();

		// Token: 0x06000E2C RID: 3628 RVA: 0x00040414 File Offset: 0x0003F814
		// Note: this type is marked as 'beforefieldinit'.
		static BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_GetMatches,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>()
		{
			ArraySegment<byte> arraySegment = new ArraySegment<byte>(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_GetMatches,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.emptyByteArray);
			BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_GetMatches,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.emptyByteArraySegment = arraySegment;
		}

		// Token: 0x04000E3E RID: 3646
		private static readonly object freePoolLock = new object();

		// Token: 0x04000E3F RID: 3647
		private static readonly Stack<NspiAsyncRpcState_GetMatches> freePool = new Stack<NspiAsyncRpcState_GetMatches>();

		// Token: 0x04000E40 RID: 3648
		private static readonly CancelableAsyncCallback asyncCallback = new CancelableAsyncCallback(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_GetMatches,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.Callback);

		// Token: 0x04000E41 RID: 3649
		private static readonly byte[] emptyByteArray = new byte[0];

		// Token: 0x04000E42 RID: 3650
		private static readonly ArraySegment<byte> emptyByteArraySegment;

		// Token: 0x04000E43 RID: 3651
		private SafeRpcAsyncStateHandle asyncState;

		// Token: 0x04000E44 RID: 3652
		private NspiAsyncRpcServer asyncServer;

		// Token: 0x04000E45 RID: 3653
		private INspiAsyncDispatch asyncDispatch;
	}
}
