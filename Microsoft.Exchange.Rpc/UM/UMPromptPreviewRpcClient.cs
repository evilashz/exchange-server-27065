using System;

namespace Microsoft.Exchange.Rpc.UM
{
	// Token: 0x02000403 RID: 1027
	internal class UMPromptPreviewRpcClient : UMVersionedRpcClientBase
	{
		// Token: 0x06001186 RID: 4486 RVA: 0x00057F90 File Offset: 0x00057390
		public UMPromptPreviewRpcClient(string serverFqdn) : base(serverFqdn)
		{
			try
			{
				this.operationName = string.Format("{0}(IUMPromptPreview.ExecutePromptPreviewRequest)", serverFqdn);
				this.executeRequestDelegate = <Module>.__unep@?cli_ExecutePromptPreviewRequest@@$$J0YAJPEAXHPEAEPEAHPEAPEAE@Z;
			}
			catch
			{
				base.Dispose(true);
				throw;
			}
		}
	}
}
