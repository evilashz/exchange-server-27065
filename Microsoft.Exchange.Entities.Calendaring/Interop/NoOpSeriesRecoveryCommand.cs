using System;

namespace Microsoft.Exchange.Entities.Calendaring.Interop
{
	// Token: 0x02000067 RID: 103
	internal class NoOpSeriesRecoveryCommand : ErrorRecoverySeriesCommand
	{
		// Token: 0x060002AB RID: 683 RVA: 0x0000A01D File Offset: 0x0000821D
		public NoOpSeriesRecoveryCommand(Guid originalActionId) : base(originalActionId)
		{
		}
	}
}
