using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Rpc.NspiServer;
using msclr;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x02000315 RID: 789
	internal abstract class BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_GetTemplateInfo,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>
	{
		// Token: 0x06000EC5 RID: 3781 RVA: 0x00040184 File Offset: 0x0003F584
		private static void Callback(ICancelableAsyncResult asyncResult)
		{
			((BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_GetTemplateInfo,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>)asyncResult.AsyncState).InternalCallback(asyncResult);
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x00037ADC File Offset: 0x00036EDC
		protected BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_GetTemplateInfo,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>()
		{
			this.asyncState = null;
			this.asyncServer = null;
			this.asyncDispatch = null;
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x00037B30 File Offset: 0x00036F30
		protected SafeRpcAsyncStateHandle AsyncState
		{
			get
			{
				return this.asyncState;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x00037B44 File Offset: 0x00036F44
		protected NspiAsyncRpcServer AsyncServer
		{
			get
			{
				return this.asyncServer;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x00037B58 File Offset: 0x00036F58
		protected INspiAsyncDispatch AsyncDispatch
		{
			get
			{
				return this.asyncDispatch;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000ECA RID: 3786 RVA: 0x00037B6C File Offset: 0x00036F6C
		protected ArraySegment<byte> EmptyByteArraySegment
		{
			get
			{
				return BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_GetTemplateInfo,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.emptyByteArraySegment;
			}
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x00037B04 File Offset: 0x00036F04
		protected void InternalInitialize(SafeRpcAsyncStateHandle asyncState, NspiAsyncRpcServer asyncServer)
		{
			this.asyncState = asyncState;
			this.asyncServer = asyncServer;
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x0003EA20 File Offset: 0x0003DE20
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

		// Token: 0x06000ECD RID: 3789 RVA: 0x0003ADF8 File Offset: 0x0003A1F8
		protected void ReleaseToPool()
		{
			@lock @lock = null;
			this.InternalReset();
			this.asyncState = null;
			this.asyncServer = null;
			this.asyncDispatch = null;
			NspiAsyncRpcState_GetTemplateInfo item = (NspiAsyncRpcState_GetTemplateInfo)this;
			@lock lock2 = new @lock(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_GetTemplateInfo,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.freePoolLock);
			try
			{
				@lock = lock2;
				if (BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_GetTemplateInfo,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.freePool.Count < this.PoolSize())
				{
					BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_GetTemplateInfo,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.freePool.Push(item);
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x0003A274 File Offset: 0x00039674
		public static NspiAsyncRpcState_GetTemplateInfo CreateFromPool()
		{
			@lock @lock = null;
			NspiAsyncRpcState_GetTemplateInfo nspiAsyncRpcState_GetTemplateInfo = null;
			@lock lock2 = new @lock(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_GetTemplateInfo,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.freePoolLock);
			try
			{
				@lock = lock2;
				if (BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_GetTemplateInfo,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.freePool.Count > 0)
				{
					nspiAsyncRpcState_GetTemplateInfo = BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_GetTemplateInfo,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.freePool.Pop();
				}
			}
			catch
			{
				((IDisposable)@lock).Dispose();
				throw;
			}
			((IDisposable)@lock).Dispose();
			if (nspiAsyncRpcState_GetTemplateInfo == null)
			{
				nspiAsyncRpcState_GetTemplateInfo = new NspiAsyncRpcState_GetTemplateInfo();
			}
			return nspiAsyncRpcState_GetTemplateInfo;
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x00037B20 File Offset: 0x00036F20
		public virtual int PoolSize()
		{
			return 100;
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x0003CE20 File Offset: 0x0003C220
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
					this.InternalBegin(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_GetTemplateInfo,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.asyncCallback);
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

		// Token: 0x06000ED1 RID: 3793
		public abstract void InternalBegin(CancelableAsyncCallback asyncCallback);

		// Token: 0x06000ED2 RID: 3794
		public abstract int InternalEnd(ICancelableAsyncResult asyncResult);

		// Token: 0x06000ED3 RID: 3795
		public abstract void InternalReset();

		// Token: 0x06000ED4 RID: 3796 RVA: 0x00040694 File Offset: 0x0003FA94
		// Note: this type is marked as 'beforefieldinit'.
		static BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_GetTemplateInfo,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>()
		{
			ArraySegment<byte> arraySegment = new ArraySegment<byte>(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_GetTemplateInfo,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.emptyByteArray);
			BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_GetTemplateInfo,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.emptyByteArraySegment = arraySegment;
		}

		// Token: 0x04000EAE RID: 3758
		private static readonly object freePoolLock = new object();

		// Token: 0x04000EAF RID: 3759
		private static readonly Stack<NspiAsyncRpcState_GetTemplateInfo> freePool = new Stack<NspiAsyncRpcState_GetTemplateInfo>();

		// Token: 0x04000EB0 RID: 3760
		private static readonly CancelableAsyncCallback asyncCallback = new CancelableAsyncCallback(BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_GetTemplateInfo,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>.Callback);

		// Token: 0x04000EB1 RID: 3761
		private static readonly byte[] emptyByteArray = new byte[0];

		// Token: 0x04000EB2 RID: 3762
		private static readonly ArraySegment<byte> emptyByteArraySegment;

		// Token: 0x04000EB3 RID: 3763
		private SafeRpcAsyncStateHandle asyncState;

		// Token: 0x04000EB4 RID: 3764
		private NspiAsyncRpcServer asyncServer;

		// Token: 0x04000EB5 RID: 3765
		private INspiAsyncDispatch asyncDispatch;
	}
}
