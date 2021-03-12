using System;

namespace Microsoft.Exchange.Rpc.NspiServer
{
	// Token: 0x020002FA RID: 762
	internal abstract class NspiAsyncRpcServer : BaseAsyncRpcServer<Microsoft::Exchange::Rpc::INspiAsyncDispatch>
	{
		// Token: 0x06000DAE RID: 3502 RVA: 0x00035884 File Offset: 0x00034C84
		public override void DroppedConnection(IntPtr contextHandle)
		{
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x00035894 File Offset: 0x00034C94
		public override void RundownContext(IntPtr contextHandle)
		{
			INspiAsyncDispatch asyncDispatch = this.GetAsyncDispatch();
			if (asyncDispatch != null)
			{
				asyncDispatch.ContextHandleRundown(contextHandle);
			}
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x0003809C File Offset: 0x0003749C
		public unsafe override void StartRundownQueue()
		{
			RundownQueue_NSPI_HANDLE* ptr = null;
			if (<Module>.g_pRundownQueue_NSPI_HANDLE != null)
			{
				throw new InvalidOperationException("Rundown queue was already started!");
			}
			try
			{
				RundownQueue_NSPI_HANDLE* ptr2 = <Module>.@new(96UL);
				RundownQueue_NSPI_HANDLE* ptr3;
				try
				{
					if (ptr2 != null)
					{
						ptr3 = <Module>.RundownQueue_NSPI_HANDLE.{ctor}(ptr2);
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
					throw new OutOfMemoryException("Unable to allocate RundownQueue_NSPI_HANDLE");
				}
				if (<Module>.BaseRundownQueue<void\u0020*>.FInitialize(ptr3) == null)
				{
					throw new OutOfMemoryException("Unable to initialize RundownQueue_NSPI_HANDLE");
				}
				<Module>.g_pRundownQueue_NSPI_HANDLE = ptr3;
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

		// Token: 0x06000DB1 RID: 3505 RVA: 0x000358B4 File Offset: 0x00034CB4
		public unsafe override void StopRundownQueue()
		{
			if (<Module>.g_pRundownQueue_NSPI_HANDLE != null)
			{
				BaseRundownQueue<void\u0020*>* g_pRundownQueue_NSPI_HANDLE = <Module>.g_pRundownQueue_NSPI_HANDLE;
				<Module>.g_pRundownQueue_NSPI_HANDLE = null;
				<Module>.BaseRundownQueue<void\u0020*>.Uninitialize(g_pRundownQueue_NSPI_HANDLE);
			}
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0003F420 File Offset: 0x0003E820
		public NspiAsyncRpcServer()
		{
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x000358F0 File Offset: 0x00034CF0
		// Note: this type is marked as 'beforefieldinit'.
		static NspiAsyncRpcServer()
		{
			IntPtr rpcIntfHandle = new IntPtr(<Module>.nspi_v56_0_s_ifspec);
			NspiAsyncRpcServer.RpcIntfHandle = rpcIntfHandle;
		}

		// Token: 0x04000DFB RID: 3579
		public static readonly IntPtr RpcIntfHandle;
	}
}
