using System;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.Entities.Calendaring.Interop
{
	// Token: 0x0200006A RID: 106
	internal class PropagateToInstance : SeriesPendingActionsInterop
	{
		// Token: 0x060002CD RID: 717 RVA: 0x0000A870 File Offset: 0x00008A70
		public PropagateToInstance(ICalendarInteropLog logger, ISeriesActionParser parser = null) : base(logger, parser)
		{
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000A87A File Offset: 0x00008A7A
		// (set) Token: 0x060002CF RID: 719 RVA: 0x0000A882 File Offset: 0x00008A82
		public Event Instance { get; set; }

		// Token: 0x060002D0 RID: 720 RVA: 0x0000A88B File Offset: 0x00008A8B
		protected override void PropagateToSeriesInstances()
		{
			this.ExecuteOnInstance(this.Instance);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000A899 File Offset: 0x00008A99
		protected override void TryCleanUp()
		{
		}
	}
}
