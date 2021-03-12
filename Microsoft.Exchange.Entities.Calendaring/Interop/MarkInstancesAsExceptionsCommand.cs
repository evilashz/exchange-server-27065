using System;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.Entities.Calendaring.Interop
{
	// Token: 0x02000066 RID: 102
	internal class MarkInstancesAsExceptionsCommand : ErrorRecoverySeriesCommand
	{
		// Token: 0x060002A9 RID: 681 RVA: 0x00009FF5 File Offset: 0x000081F5
		public MarkInstancesAsExceptionsCommand(Guid origianlActionId) : base(origianlActionId)
		{
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000A000 File Offset: 0x00008200
		protected override void PrepareRecoveryData(Event instanceDelta)
		{
			base.PrepareRecoveryData(instanceDelta);
			((IEventInternal)instanceDelta).MarkAllPropagatedPropertiesAsException = true;
		}
	}
}
