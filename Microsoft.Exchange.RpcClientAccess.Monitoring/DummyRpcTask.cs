using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000060 RID: 96
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class DummyRpcTask : DummyTask
	{
		// Token: 0x0600020C RID: 524 RVA: 0x0000766E File Offset: 0x0000586E
		public DummyRpcTask(IContext context) : base(context, RpcHelper.DependenciesOfBuildCompleteBindingInfo)
		{
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000767C File Offset: 0x0000587C
		protected override IEmsmdbClient CreateEmsmdbClient()
		{
			return base.Environment.CreateEmsmdbClient(RpcHelper.BuildCompleteBindingInfo(base.Properties, 6001));
		}
	}
}
