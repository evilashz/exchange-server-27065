using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.ApplicationLogic.SyncCalendar
{
	// Token: 0x020001BA RID: 442
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SyncCalendarResponse
	{
		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001109 RID: 4361 RVA: 0x00046E01 File Offset: 0x00045001
		// (set) Token: 0x0600110A RID: 4362 RVA: 0x00046E09 File Offset: 0x00045009
		public CalendarViewQueryResumptionPoint QueryResumptionPoint { get; set; }

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x0600110B RID: 4363 RVA: 0x00046E12 File Offset: 0x00045012
		// (set) Token: 0x0600110C RID: 4364 RVA: 0x00046E1A File Offset: 0x0004501A
		public ExDateTime? OldWindowEnd { get; set; }

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x0600110D RID: 4365 RVA: 0x00046E23 File Offset: 0x00045023
		// (set) Token: 0x0600110E RID: 4366 RVA: 0x00046E2B File Offset: 0x0004502B
		public bool IncludesLastItemInRange { get; set; }

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x0600110F RID: 4367 RVA: 0x00046E34 File Offset: 0x00045034
		// (set) Token: 0x06001110 RID: 4368 RVA: 0x00046E3C File Offset: 0x0004503C
		public IList<StoreId> DeletedItems { get; set; }

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06001111 RID: 4369 RVA: 0x00046E45 File Offset: 0x00045045
		// (set) Token: 0x06001112 RID: 4370 RVA: 0x00046E4D File Offset: 0x0004504D
		public IList<SyncCalendarItemType> UpdatedItems { get; set; }

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001113 RID: 4371 RVA: 0x00046E56 File Offset: 0x00045056
		// (set) Token: 0x06001114 RID: 4372 RVA: 0x00046E5E File Offset: 0x0004505E
		public IList<SyncCalendarItemType> RecurrenceMastersWithInstances { get; set; }

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06001115 RID: 4373 RVA: 0x00046E67 File Offset: 0x00045067
		// (set) Token: 0x06001116 RID: 4374 RVA: 0x00046E6F File Offset: 0x0004506F
		public IList<SyncCalendarItemType> RecurrenceMastersWithoutInstances { get; set; }

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001117 RID: 4375 RVA: 0x00046E78 File Offset: 0x00045078
		// (set) Token: 0x06001118 RID: 4376 RVA: 0x00046E80 File Offset: 0x00045080
		public IList<SyncCalendarItemType> UnchangedRecurrenceMastersWithInstances { get; set; }
	}
}
