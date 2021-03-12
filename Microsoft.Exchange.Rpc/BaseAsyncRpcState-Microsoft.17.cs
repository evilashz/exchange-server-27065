using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Rpc.NspiServer;
using msclr;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x02000309 RID: 777
	internal abstract class BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_DNToEph,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>
	{
		// Token: 0x06000E47 RID: 3655 RVA: 0x000400C4 File Offset: 0x0003F4C4
		private static void Callback(ICancelableAsyncResult asyncResult)
		{
			((BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_DNToEph,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>)asyncResult.AsyncState).InternalCallback(asyncResult);
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x00037704 File Offset: 0x00036B04
		protected BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_DNToEph,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>()
		{
			this.asyncState = null;
			this.asyncServer = null;
			this.asyncDispatch = null;
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x00037758 File Offset: 0x00036B58
		protected SafeRpcAsyncStateHandle AsyncState
		{
			get
			{
				return this.asyncState;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000E4A RID: 3658 RVA: 0x0003776C File Offset: 0x00036B6C
		protected NspiAsyncRpcServer AsyncServer
		{
			get
			{
				return this.asyncServer;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000E4B RID: 3659 RVA: 0x00037780 File Offset: 0x00036B80
		protected INspiAsyncDispatch AsyncDispatch
		{
			get
			{
				return this.asyncDispatch;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000E4C RID: 3660 RVA: 0x00037794 File Offset: 0x00036B94
		protected ArraySegment<byte> EmptyByteArraySegment
		{
			get
			{
				return BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_DNToEph,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.emptyByteArraySegment;
			}
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x0003772C File Offset: 0x00036B2C
		protected void InternalInitialize(SafeRpcAsyncStateHandle asyncState, NspiAsyncRpcServer asyncServer)
		{
			this.asyncState = asyncState;
			this.asyncServer = asyncServer;
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x0003E2A0 File Offset: 0x0003D6A0
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

		// Token: 0x06000E4F RID: 3663 RVA: 0x0003AAC8 File Offset: 0x00039EC8
		protected void ReleaseToPool()
		{
			@lock @lock = null;
			this.InternalReset();
			this.asyncState = null;
			this.asyncServer = null;
			this.asyncDispatch = null;
			NspiAsyncRpcState_DNToEph item = (NspiAsyncRpcState_DNToEph)this;
			@lock lock2 = new @lock(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_DNToEph,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.freePoolLock);
			try
			{
				@lock = lock2;
				if (BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_DNToEph,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.freePool.Count < this.PoolSize())
				{
					BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_DNToEph,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.freePool.Push(item);
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x00039FD4 File Offset: 0x000393D4
		public static NspiAsyncRpcState_DNToEph CreateFromPool()
		{
			@lock @lock = null;
			NspiAsyncRpcState_DNToEph nspiAsyncRpcState_DNToEph = null;
			@lock lock2 = new @lock(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_DNToEph,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.freePoolLock);
			try
			{
				@lock = lock2;
				if (BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_DNToEph,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.freePool.Count > 0)
				{
					nspiAsyncRpcState_DNToEph = BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_DNToEph,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.freePool.Pop();
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
			if (nspiAsyncRpcState_DNToEph == null)
			{
				nspiAsyncRpcState_DNToEph = new NspiAsyncRpcState_DNToEph();
			}
			return nspiAsyncRpcState_DNToEph;
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x00037748 File Offset: 0x00036B48
		public virtual int PoolSize()
		{
			return 100;
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x0003C550 File Offset: 0x0003B950
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
					this.InternalBegin(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_DNToEph,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.asyncCallback);
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

		// Token: 0x06000E53 RID: 3667
		public abstract void InternalBegin(CancelableAsyncCallback asyncCallback);

		// Token: 0x06000E54 RID: 3668
		public abstract int InternalEnd(ICancelableAsyncResult asyncResult);

		// Token: 0x06000E55 RID: 3669
		public abstract void InternalReset();

		// Token: 0x06000E56 RID: 3670 RVA: 0x000404B4 File Offset: 0x0003F8B4
		// Note: this type is marked as 'beforefieldinit'.
		static BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_DNToEph,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>()
		{
			ArraySegment<byte> arraySegment = new ArraySegment<byte>(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_DNToEph,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.emptyByteArray);
			BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_DNToEph,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.emptyByteArraySegment = arraySegment;
		}

		// Token: 0x04000E5E RID: 3678
		private static readonly object freePoolLock = new object();

		// Token: 0x04000E5F RID: 3679
		private static readonly Stack<NspiAsyncRpcState_DNToEph> freePool = new Stack<NspiAsyncRpcState_DNToEph>();

		// Token: 0x04000E60 RID: 3680
		private static readonly CancelableAsyncCallback asyncCallback = new CancelableAsyncCallback(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_DNToEph,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.Callback);

		// Token: 0x04000E61 RID: 3681
		private static readonly byte[] emptyByteArray = new byte[0];

		// Token: 0x04000E62 RID: 3682
		private static readonly ArraySegment<byte> emptyByteArraySegment;

		// Token: 0x04000E63 RID: 3683
		private SafeRpcAsyncStateHandle asyncState;

		// Token: 0x04000E64 RID: 3684
		private NspiAsyncRpcServer asyncServer;

		// Token: 0x04000E65 RID: 3685
		private INspiAsyncDispatch asyncDispatch;
	}
}
