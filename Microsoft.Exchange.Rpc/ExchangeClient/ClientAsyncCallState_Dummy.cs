using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Rpc.ExchangeClient
{
	// Token: 0x020001EC RID: 492
	internal class ClientAsyncCallState_Dummy : ClientAsyncCallState
	{
		// Token: 0x06000AA1 RID: 2721 RVA: 0x0001D8E8 File Offset: 0x0001CCE8
		public ClientAsyncCallState_Dummy(CancelableAsyncCallback asyncCallback, object asyncState, IntPtr pRpcBindingHandle) : base("Dummy", asyncCallback, asyncState)
		{
			try
			{
				this.m_pRpcBindingHandle = pRpcBindingHandle;
			}
			catch
			{
				base.Dispose(true);
				throw;
			}
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0001C518 File Offset: 0x0001B918
		private void ~ClientAsyncCallState_Dummy()
		{
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0001C528 File Offset: 0x0001B928
		public unsafe override void InternalBegin()
		{
			<Module>.cli_Async_EcDummyRpc((_RPC_ASYNC_STATE*)base.RpcAsyncState().ToPointer(), this.m_pRpcBindingHandle.ToPointer());
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0001D930 File Offset: 0x0001CD30
		public int End()
		{
			return base.CheckCompletion();
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0001D944 File Offset: 0x0001CD44
		[HandleProcessCorruptedStateExceptions]
		protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				try
				{
					return;
				}
				finally
				{
					base.Dispose(true);
				}
			}
			base.Dispose(false);
		}

		// Token: 0x04000BDD RID: 3037
		private IntPtr m_pRpcBindingHandle;
	}
}
