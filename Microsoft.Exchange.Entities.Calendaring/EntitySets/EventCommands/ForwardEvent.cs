using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.EventCommands
{
	// Token: 0x0200004E RID: 78
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ForwardEvent : ForwardEventBase
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001EF RID: 495 RVA: 0x000083E3 File Offset: 0x000065E3
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x000083EB File Offset: 0x000065EB
		public int? SeriesSequenceNumber { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x000083F4 File Offset: 0x000065F4
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x000083FC File Offset: 0x000065FC
		public string OccurrencesViewPropertiesBlob { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00008405 File Offset: 0x00006605
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x0000840D File Offset: 0x0000660D
		internal Event UpdateToEvent { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00008416 File Offset: 0x00006616
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.ForwardEventTracer;
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00008420 File Offset: 0x00006620
		protected override VoidResult OnExecute()
		{
			StoreId entityStoreId = this.GetEntityStoreId();
			this.Scope.EventDataProvider.ForwardEvent(entityStoreId, base.Parameters, this.UpdateToEvent, this.SeriesSequenceNumber, this.OccurrencesViewPropertiesBlob, this.Context);
			return VoidResult.Value;
		}
	}
}
