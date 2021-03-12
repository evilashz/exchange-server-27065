using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.RpcHttpConnectionRegistration
{
	// Token: 0x020003E6 RID: 998
	internal abstract class ClientCallWrapper
	{
		// Token: 0x06001107 RID: 4359 RVA: 0x000552A8 File Offset: 0x000546A8
		[HandleProcessCorruptedStateExceptions]
		private int WrappedExecute()
		{
			int num = 0;
			int result = 0;
			<Module>.RpcBindingReset(this.hBinding);
			try
			{
				result = this.InternalExecute();
			}
			catch when (endfilter(true))
			{
				num = Marshal.GetExceptionCode();
			}
			if (num != null)
			{
				<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(num, this.Name());
			}
			return result;
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x00055308 File Offset: 0x00054708
		protected unsafe ClientCallWrapper(void* hBinding)
		{
			this.hBinding = hBinding;
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06001109 RID: 4361 RVA: 0x00055324 File Offset: 0x00054724
		protected unsafe void* HBinding
		{
			get
			{
				return this.hBinding;
			}
		}

		// Token: 0x0600110A RID: 4362
		protected abstract string Name();

		// Token: 0x0600110B RID: 4363
		protected abstract int InternalExecute();

		// Token: 0x0600110C RID: 4364
		protected abstract void InternalCleanup();

		// Token: 0x0600110D RID: 4365 RVA: 0x00055338 File Offset: 0x00054738
		public int Execute()
		{
			int result;
			try
			{
				result = this.WrappedExecute();
			}
			finally
			{
				this.InternalCleanup();
			}
			return result;
		}

		// Token: 0x04001006 RID: 4102
		private unsafe void* hBinding;
	}
}
