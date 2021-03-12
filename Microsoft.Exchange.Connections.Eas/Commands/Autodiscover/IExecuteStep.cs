using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.Autodiscover
{
	// Token: 0x0200001F RID: 31
	[ClassAccessLevel(AccessLevel.Implementation)]
	public interface IExecuteStep
	{
		// Token: 0x060000BA RID: 186
		Step PrepareAndExecuteStep(StepContext stepContext);

		// Token: 0x060000BB RID: 187
		Step ExecuteStep(StepContext stepContext);
	}
}
