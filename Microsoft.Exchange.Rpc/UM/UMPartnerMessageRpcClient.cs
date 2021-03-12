using System;

namespace Microsoft.Exchange.Rpc.UM
{
	// Token: 0x02000402 RID: 1026
	internal class UMPartnerMessageRpcClient : UMVersionedRpcClientBase
	{
		// Token: 0x06001185 RID: 4485 RVA: 0x00057F38 File Offset: 0x00057338
		public UMPartnerMessageRpcClient(string serverFqdn) : base(serverFqdn)
		{
			try
			{
				this.operationName = string.Format("{0}(IUMPartnerMessage.ProcessPartnerMessage)", serverFqdn);
				this.executeRequestDelegate = <Module>.__unep@?cli_ProcessPartnerMessage@@$$J0YAJPEAXHPEAEPEAHPEAPEAE@Z;
			}
			catch
			{
				base.Dispose(true);
				throw;
			}
		}
	}
}
