using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x0200005F RID: 95
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class DummyMapiHttpTask : DummyTask
	{
		// Token: 0x0600020A RID: 522 RVA: 0x00007648 File Offset: 0x00005848
		public DummyMapiHttpTask(IContext context) : base(context, RpcHelper.DependenciesOfBuildMapiHttpBindingInfo)
		{
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00007656 File Offset: 0x00005856
		protected override IEmsmdbClient CreateEmsmdbClient()
		{
			return base.Environment.CreateEmsmdbClient(RpcHelper.BuildCompleteMapiHttpBindingInfo(base.Properties));
		}
	}
}
