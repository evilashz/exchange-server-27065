using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x020002C4 RID: 708
	public class MockRequestQueueWrapper : IDisposable
	{
		// Token: 0x06000CD4 RID: 3284 RVA: 0x00030C5C File Offset: 0x0003005C
		public unsafe MockRequestQueueWrapper(int maxRequests, int maxThreads, RequestDelegate processRequest, RequestDelegate abortRequest)
		{
			this.localProcessRequestDelegate = processRequest;
			this.localAbortRequestDelegate = abortRequest;
			MockRequestQueue* ptr = <Module>.@new(152UL);
			MockRequestQueue* ptr2;
			try
			{
				if (ptr != null)
				{
					IntPtr functionPointerForDelegate = Marshal.GetFunctionPointerForDelegate(this.localAbortRequestDelegate);
					ptr2 = <Module>.MockRequestQueue.{ctor}(ptr, Marshal.GetFunctionPointerForDelegate(this.localProcessRequestDelegate).ToPointer(), functionPointerForDelegate.ToPointer());
				}
				else
				{
					ptr2 = 0L;
				}
			}
			catch
			{
				<Module>.delete((void*)ptr);
				throw;
			}
			this.requestQueue = ptr2;
			<Module>.CRequestQueue<int>.FInitialize(ptr2, maxRequests, maxThreads);
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x00030D40 File Offset: 0x00030140
		private void ~MockRequestQueueWrapper()
		{
			this.!MockRequestQueueWrapper();
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x00030CF4 File Offset: 0x000300F4
		private unsafe void !MockRequestQueueWrapper()
		{
			MockRequestQueue* ptr = this.requestQueue;
			if (ptr != null)
			{
				object obj = calli(System.Void* modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32), ptr, 1, *(*(long*)ptr + 72L));
			}
			this.requestQueue = null;
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x00030D24 File Offset: 0x00030124
		public void Process(int requestData)
		{
			<Module>.CRequestQueue<int>.Process(this.requestQueue, ref requestData);
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x00030D54 File Offset: 0x00030154
		[HandleProcessCorruptedStateExceptions]
		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.!MockRequestQueueWrapper();
			}
			else
			{
				try
				{
					this.!MockRequestQueueWrapper();
				}
				finally
				{
					base.Finalize();
				}
			}
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x00030DAC File Offset: 0x000301AC
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x00030D98 File Offset: 0x00030198
		protected override void Finalize()
		{
			this.Dispose(false);
		}

		// Token: 0x04000D76 RID: 3446
		private RequestDelegate localProcessRequestDelegate;

		// Token: 0x04000D77 RID: 3447
		private RequestDelegate localAbortRequestDelegate;

		// Token: 0x04000D78 RID: 3448
		private unsafe MockRequestQueue* requestQueue;
	}
}
