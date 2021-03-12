using System;

namespace Microsoft.Exchange.Rpc.UM
{
	// Token: 0x02000401 RID: 1025
	internal class UMPlayOnPhoneRpcClient : UMVersionedRpcClientBase
	{
		// Token: 0x06001184 RID: 4484 RVA: 0x00057EE0 File Offset: 0x000572E0
		public UMPlayOnPhoneRpcClient(string serverFqdn) : base(serverFqdn)
		{
			try
			{
				this.operationName = string.Format("{0}(IUMPlayOnPhone.ExecutePoPRequest)", serverFqdn);
				this.executeRequestDelegate = <Module>.__unep@?cli_ExecutePoPRequest@@$$J0YAJPEAXHPEAEPEAHPEAPEAE@Z;
			}
			catch
			{
				base.Dispose(true);
				throw;
			}
		}
	}
}
