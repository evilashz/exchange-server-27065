using System;

namespace Microsoft.Exchange.Rpc.ExchangeServer
{
	// Token: 0x020001FA RID: 506
	internal abstract class ExchangeAsyncRpcServer_EMSMDB : ExchangeAsyncRpcServer
	{
		// Token: 0x06000AB7 RID: 2743 RVA: 0x0001DF74 File Offset: 0x0001D374
		public override void RundownContext(IntPtr contextHandle)
		{
			IExchangeAsyncDispatch asyncDispatch = this.GetAsyncDispatch();
			if (asyncDispatch != null)
			{
				asyncDispatch.ContextHandleRundown(contextHandle);
			}
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0001F4E8 File Offset: 0x0001E8E8
		public unsafe override void StartRundownQueue()
		{
			RundownQueue_ExCXH* ptr = null;
			if (<Module>.g_pRundownQueue_ExCXH != null)
			{
				throw new InvalidOperationException("Rundown queue was already started!");
			}
			try
			{
				RundownQueue_ExCXH* ptr2 = <Module>.@new(96UL);
				RundownQueue_ExCXH* ptr3;
				try
				{
					if (ptr2 != null)
					{
						ptr3 = <Module>.RundownQueue_ExCXH.{ctor}(ptr2);
					}
					else
					{
						ptr3 = 0L;
					}
				}
				catch
				{
					<Module>.delete((void*)ptr2);
					throw;
				}
				ptr = ptr3;
				if (ptr3 == null)
				{
					throw new OutOfMemoryException("Unable to allocate RundownQueue_ExCXH");
				}
				if (<Module>.BaseRundownQueue<void\u0020*>.FInitialize(ptr3) == null)
				{
					throw new OutOfMemoryException("Unable to initialize RundownQueue_ExCXH");
				}
				<Module>.g_pRundownQueue_ExCXH = ptr3;
				ptr = null;
			}
			finally
			{
				if (ptr != null)
				{
					object obj = calli(System.Void* modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32), ptr, 1, *(*(long*)ptr + 24L));
				}
			}
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0001DF94 File Offset: 0x0001D394
		public unsafe override void StopRundownQueue()
		{
			if (<Module>.g_pRundownQueue_ExCXH != null)
			{
				BaseRundownQueue<void\u0020*>* g_pRundownQueue_ExCXH = <Module>.g_pRundownQueue_ExCXH;
				<Module>.g_pRundownQueue_ExCXH = null;
				<Module>.BaseRundownQueue<void\u0020*>.Uninitialize(g_pRundownQueue_ExCXH);
			}
		}

		// Token: 0x06000ABA RID: 2746
		public abstract ushort GetVersionDelta();

		// Token: 0x06000ABB RID: 2747 RVA: 0x00021A54 File Offset: 0x00020E54
		public ExchangeAsyncRpcServer_EMSMDB()
		{
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0001DFBC File Offset: 0x0001D3BC
		// Note: this type is marked as 'beforefieldinit'.
		static ExchangeAsyncRpcServer_EMSMDB()
		{
			IntPtr rpcIntfHandle = new IntPtr(<Module>.emsmdbAsync_v0_81_s_ifspec);
			ExchangeAsyncRpcServer_EMSMDB.RpcIntfHandle = rpcIntfHandle;
		}

		// Token: 0x04000BF0 RID: 3056
		public static readonly IntPtr RpcIntfHandle;
	}
}
