using System;
using Microsoft.Exchange.Entities.Calendaring.EntitySets.EventCommands;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.Entities.Calendaring.Interop
{
	// Token: 0x02000062 RID: 98
	internal abstract class ErrorRecoverySeriesCommand : UpdateSeries
	{
		// Token: 0x0600028B RID: 651 RVA: 0x00009CAA File Offset: 0x00007EAA
		protected ErrorRecoverySeriesCommand(Guid originalActionId)
		{
			this.originalId = originalActionId;
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00009CB9 File Offset: 0x00007EB9
		public sealed override Guid CommandId
		{
			get
			{
				return this.originalId;
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00009CC1 File Offset: 0x00007EC1
		protected sealed override void PrepareDataForInstance(Event instanceDelta)
		{
			this.PrepareRecoveryData(instanceDelta);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00009CCC File Offset: 0x00007ECC
		protected virtual void PrepareRecoveryData(Event instanceDelta)
		{
			((IEventInternal)instanceDelta).SeriesToInstancePropagation = true;
		}

		// Token: 0x040000B0 RID: 176
		private readonly Guid originalId;
	}
}
