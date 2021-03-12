using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.RpcHttpConnectionRegistration
{
	// Token: 0x020003E9 RID: 1001
	internal class ClientCallWrapper_Clear : ClientCallWrapper, IDisposable
	{
		// Token: 0x0600111E RID: 4382 RVA: 0x000558A0 File Offset: 0x00054CA0
		protected override string Name()
		{
			return "RpcHttpConnectionRegistration::Clear";
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x000558B4 File Offset: 0x00054CB4
		protected override int InternalExecute()
		{
			return <Module>.cli_RpcHttpConnectionRegistration_Clear(base.HBinding);
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x000558CC File Offset: 0x00054CCC
		protected override void InternalCleanup()
		{
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x000558DC File Offset: 0x00054CDC
		public unsafe ClientCallWrapper_Clear(void* hBinding) : base(hBinding)
		{
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x000558F0 File Offset: 0x00054CF0
		private void ~ClientCallWrapper_Clear()
		{
			this.InternalCleanup();
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00055904 File Offset: 0x00054D04
		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.InternalCleanup();
			}
			else
			{
				base.Finalize();
			}
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x000559A0 File Offset: 0x00054DA0
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
