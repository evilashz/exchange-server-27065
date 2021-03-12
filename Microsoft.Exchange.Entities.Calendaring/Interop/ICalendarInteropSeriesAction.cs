using System;
using Microsoft.Exchange.Entities.Calendaring.EntitySets;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.Entities.Calendaring.Interop
{
	// Token: 0x0200003A RID: 58
	internal interface ICalendarInteropSeriesAction
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600015D RID: 349
		Guid CommandId { get; }

		// Token: 0x0600015E RID: 350
		void ExecuteOnInstance(Event seriesInstance);

		// Token: 0x0600015F RID: 351
		void RestoreExecutionContext(Events entitySet, SeriesInteropCommand seriesInteropCommand);

		// Token: 0x06000160 RID: 352
		Event CleanUp(Event master);

		// Token: 0x06000161 RID: 353
		Event GetInitialMasterValue();

		// Token: 0x06000162 RID: 354
		Event InitialMasterOperation(Event updateToMaster);
	}
}
