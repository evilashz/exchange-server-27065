using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Rpc.UM;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D61 RID: 3425
	internal class UMPromptPreviewRpcTarget : UMVersionedRpcTargetBase
	{
		// Token: 0x06008365 RID: 33637 RVA: 0x00218A25 File Offset: 0x00216C25
		internal UMPromptPreviewRpcTarget(Server server) : base(server)
		{
		}

		// Token: 0x06008366 RID: 33638 RVA: 0x00218A2E File Offset: 0x00216C2E
		protected override UMVersionedRpcClientBase CreateRpcClient()
		{
			return new UMPromptPreviewRpcClient(base.Server.Fqdn);
		}
	}
}
