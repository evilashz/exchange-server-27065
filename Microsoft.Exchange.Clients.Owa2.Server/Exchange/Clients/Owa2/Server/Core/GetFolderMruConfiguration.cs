using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000314 RID: 788
	internal class GetFolderMruConfiguration : ServiceCommand<TargetFolderMruConfiguration>
	{
		// Token: 0x06001A2C RID: 6700 RVA: 0x0005F68B File Offset: 0x0005D88B
		public GetFolderMruConfiguration(CallContext callContext) : base(callContext)
		{
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x0005F694 File Offset: 0x0005D894
		protected override TargetFolderMruConfiguration InternalExecute()
		{
			TargetFolderMruConfiguration targetFolderMruConfiguration = new TargetFolderMruConfiguration();
			targetFolderMruConfiguration.LoadAll(base.CallContext);
			return targetFolderMruConfiguration;
		}
	}
}
