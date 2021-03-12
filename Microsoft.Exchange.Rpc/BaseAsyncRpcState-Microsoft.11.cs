using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Rpc.NspiServer;
using msclr;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x020002FD RID: 765
	internal abstract class BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_Unbind,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>
	{
		// Token: 0x06000DC9 RID: 3529 RVA: 0x00040004 File Offset: 0x0003F404
		private static void Callback(ICancelableAsyncResult asyncResult)
		{
			((BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_Unbind,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>)asyncResult.AsyncState).InternalCallback(asyncResult);
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x0003732C File Offset: 0x0003672C
		protected BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_Unbind,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>()
		{
			this.asyncState = null;
			this.asyncServer = null;
			this.asyncDispatch = null;
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x00037380 File Offset: 0x00036780
		protected SafeRpcAsyncStateHandle AsyncState
		{
			get
			{
				return this.asyncState;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x00037394 File Offset: 0x00036794
		protected NspiAsyncRpcServer AsyncServer
		{
			get
			{
				return this.asyncServer;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000DCD RID: 3533 RVA: 0x000373A8 File Offset: 0x000367A8
		protected INspiAsyncDispatch AsyncDispatch
		{
			get
			{
				return this.asyncDispatch;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x000373BC File Offset: 0x000367BC
		protected ArraySegment<byte> EmptyByteArraySegment
		{
			get
			{
				return BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_Unbind,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.emptyByteArraySegment;
			}
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x00037354 File Offset: 0x00036754
		protected void InternalInitialize(SafeRpcAsyncStateHandle asyncState, NspiAsyncRpcServer asyncServer)
		{
			this.asyncState = asyncState;
			this.asyncServer = asyncServer;
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x0003DB20 File Offset: 0x0003CF20
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

		// Token: 0x06000DD1 RID: 3537 RVA: 0x0003A798 File Offset: 0x00039B98
		protected void ReleaseToPool()
		{
			@lock @lock = null;
			this.InternalReset();
			this.asyncState = null;
			this.asyncServer = null;
			this.asyncDispatch = null;
			NspiAsyncRpcState_Unbind item = (NspiAsyncRpcState_Unbind)this;
			@lock lock2 = new @lock(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_Unbind,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.freePoolLock);
			try
			{
				@lock = lock2;
				if (BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_Unbind,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.freePool.Count < this.PoolSize())
				{
					BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_Unbind,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.freePool.Push(item);
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x00039D34 File Offset: 0x00039134
		public static NspiAsyncRpcState_Unbind CreateFromPool()
		{
			@lock @lock = null;
			NspiAsyncRpcState_Unbind nspiAsyncRpcState_Unbind = null;
			@lock lock2 = new @lock(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_Unbind,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.freePoolLock);
			try
			{
				@lock = lock2;
				if (BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_Unbind,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.freePool.Count > 0)
				{
					nspiAsyncRpcState_Unbind = BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_Unbind,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.freePool.Pop();
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
			if (nspiAsyncRpcState_Unbind == null)
			{
				nspiAsyncRpcState_Unbind = new NspiAsyncRpcState_Unbind();
			}
			return nspiAsyncRpcState_Unbind;
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x00037370 File Offset: 0x00036770
		public virtual int PoolSize()
		{
			return 100;
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x0003BC80 File Offset: 0x0003B080
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
					this.InternalBegin(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_Unbind,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.asyncCallback);
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

		// Token: 0x06000DD5 RID: 3541
		public abstract void InternalBegin(CancelableAsyncCallback asyncCallback);

		// Token: 0x06000DD6 RID: 3542
		public abstract int InternalEnd(ICancelableAsyncResult asyncResult);

		// Token: 0x06000DD7 RID: 3543
		public abstract void InternalReset();

		// Token: 0x06000DD8 RID: 3544 RVA: 0x000402D4 File Offset: 0x0003F6D4
		// Note: this type is marked as 'beforefieldinit'.
		static BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_Unbind,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>()
		{
			ArraySegment<byte> arraySegment = new ArraySegment<byte>(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_Unbind,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.emptyByteArray);
			BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_Unbind,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.emptyByteArraySegment = arraySegment;
		}

		// Token: 0x04000E09 RID: 3593
		private static readonly object freePoolLock = new object();

		// Token: 0x04000E0A RID: 3594
		private static readonly Stack<NspiAsyncRpcState_Unbind> freePool = new Stack<NspiAsyncRpcState_Unbind>();

		// Token: 0x04000E0B RID: 3595
		private static readonly CancelableAsyncCallback asyncCallback = new CancelableAsyncCallback(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_Unbind,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.Callback);

		// Token: 0x04000E0C RID: 3596
		private static readonly byte[] emptyByteArray = new byte[0];

		// Token: 0x04000E0D RID: 3597
		private static readonly ArraySegment<byte> emptyByteArraySegment;

		// Token: 0x04000E0E RID: 3598
		private SafeRpcAsyncStateHandle asyncState;

		// Token: 0x04000E0F RID: 3599
		private NspiAsyncRpcServer asyncServer;

		// Token: 0x04000E10 RID: 3600
		private INspiAsyncDispatch asyncDispatch;
	}
}
