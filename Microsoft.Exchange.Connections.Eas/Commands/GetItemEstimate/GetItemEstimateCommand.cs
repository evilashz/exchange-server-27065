using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.GetItemEstimate
{
	// Token: 0x02000049 RID: 73
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class GetItemEstimateCommand : EasServerCommand<GetItemEstimateRequest, GetItemEstimateResponse, GetItemEstimateStatus>
	{
		// Token: 0x06000170 RID: 368 RVA: 0x00004BF1 File Offset: 0x00002DF1
		internal GetItemEstimateCommand(EasConnectionSettings easConnectionSettings) : base(Command.GetItemEstimate, easConnectionSettings)
		{
		}
	}
}
