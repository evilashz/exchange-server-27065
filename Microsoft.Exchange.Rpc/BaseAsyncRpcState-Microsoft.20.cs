using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Rpc.NspiServer;
using msclr;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x0200030F RID: 783
	internal abstract class BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_CompareDNTs,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>
	{
		// Token: 0x06000E86 RID: 3718 RVA: 0x00040124 File Offset: 0x0003F524
		private static void Callback(ICancelableAsyncResult asyncResult)
		{
			((BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_CompareDNTs,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>)asyncResult.AsyncState).InternalCallback(asyncResult);
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x000378F0 File Offset: 0x00036CF0
		protected BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_CompareDNTs,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>()
		{
			this.asyncState = null;
			this.asyncServer = null;
			this.asyncDispatch = null;
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000E88 RID: 3720 RVA: 0x00037944 File Offset: 0x00036D44
		protected SafeRpcAsyncStateHandle AsyncState
		{
			get
			{
				return this.asyncState;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000E89 RID: 3721 RVA: 0x00037958 File Offset: 0x00036D58
		protected NspiAsyncRpcServer AsyncServer
		{
			get
			{
				return this.asyncServer;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000E8A RID: 3722 RVA: 0x0003796C File Offset: 0x00036D6C
		protected INspiAsyncDispatch AsyncDispatch
		{
			get
			{
				return this.asyncDispatch;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000E8B RID: 3723 RVA: 0x00037980 File Offset: 0x00036D80
		protected ArraySegment<byte> EmptyByteArraySegment
		{
			get
			{
				return BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_CompareDNTs,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.emptyByteArraySegment;
			}
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x00037918 File Offset: 0x00036D18
		protected void InternalInitialize(SafeRpcAsyncStateHandle asyncState, NspiAsyncRpcServer asyncServer)
		{
			this.asyncState = asyncState;
			this.asyncServer = asyncServer;
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x0003E660 File Offset: 0x0003DA60
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

		// Token: 0x06000E8E RID: 3726 RVA: 0x0003AC60 File Offset: 0x0003A060
		protected void ReleaseToPool()
		{
			@lock @lock = null;
			this.InternalReset();
			this.asyncState = null;
			this.asyncServer = null;
			this.asyncDispatch = null;
			NspiAsyncRpcState_CompareDNTs item = (NspiAsyncRpcState_CompareDNTs)this;
			@lock lock2 = new @lock(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_CompareDNTs,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.freePoolLock);
			try
			{
				@lock = lock2;
				if (BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_CompareDNTs,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.freePool.Count < this.PoolSize())
				{
					BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_CompareDNTs,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.freePool.Push(item);
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0003A124 File Offset: 0x00039524
		public static NspiAsyncRpcState_CompareDNTs CreateFromPool()
		{
			@lock @lock = null;
			NspiAsyncRpcState_CompareDNTs nspiAsyncRpcState_CompareDNTs = null;
			@lock lock2 = new @lock(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_CompareDNTs,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.freePoolLock);
			try
			{
				@lock = lock2;
				if (BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_CompareDNTs,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.freePool.Count > 0)
				{
					nspiAsyncRpcState_CompareDNTs = BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_CompareDNTs,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.freePool.Pop();
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
			if (nspiAsyncRpcState_CompareDNTs == null)
			{
				nspiAsyncRpcState_CompareDNTs = new NspiAsyncRpcState_CompareDNTs();
			}
			return nspiAsyncRpcState_CompareDNTs;
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x00037934 File Offset: 0x00036D34
		public virtual int PoolSize()
		{
			return 100;
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0003C9B8 File Offset: 0x0003BDB8
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
					this.InternalBegin(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_CompareDNTs,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.asyncCallback);
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

		// Token: 0x06000E92 RID: 3730
		public abstract void InternalBegin(CancelableAsyncCallback asyncCallback);

		// Token: 0x06000E93 RID: 3731
		public abstract int InternalEnd(ICancelableAsyncResult asyncResult);

		// Token: 0x06000E94 RID: 3732
		public abstract void InternalReset();

		// Token: 0x06000E95 RID: 3733 RVA: 0x000405A4 File Offset: 0x0003F9A4
		// Note: this type is marked as 'beforefieldinit'.
		static BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_CompareDNTs,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>()
		{
			ArraySegment<byte> arraySegment = new ArraySegment<byte>(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_CompareDNTs,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.emptyByteArray);
			BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_CompareDNTs,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.emptyByteArraySegment = arraySegment;
		}

		// Token: 0x04000E85 RID: 3717
		private static readonly object freePoolLock = new object();

		// Token: 0x04000E86 RID: 3718
		private static readonly Stack<NspiAsyncRpcState_CompareDNTs> freePool = new Stack<NspiAsyncRpcState_CompareDNTs>();

		// Token: 0x04000E87 RID: 3719
		private static readonly CancelableAsyncCallback asyncCallback = new CancelableAsyncCallback(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_CompareDNTs,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.Callback);

		// Token: 0x04000E88 RID: 3720
		private static readonly byte[] emptyByteArray = new byte[0];

		// Token: 0x04000E89 RID: 3721
		private static readonly ArraySegment<byte> emptyByteArraySegment;

		// Token: 0x04000E8A RID: 3722
		private SafeRpcAsyncStateHandle asyncState;

		// Token: 0x04000E8B RID: 3723
		private NspiAsyncRpcServer asyncServer;

		// Token: 0x04000E8C RID: 3724
		private INspiAsyncDispatch asyncDispatch;
	}
}
