using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.ApplicationLogic.SyncCalendar
{
	// Token: 0x020001B9 RID: 441
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SyncCalendarItemType
	{
		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x060010F8 RID: 4344 RVA: 0x00046D71 File Offset: 0x00044F71
		// (set) Token: 0x060010F9 RID: 4345 RVA: 0x00046D79 File Offset: 0x00044F79
		public string UID { get; set; }

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x060010FA RID: 4346 RVA: 0x00046D82 File Offset: 0x00044F82
		// (set) Token: 0x060010FB RID: 4347 RVA: 0x00046D8A File Offset: 0x00044F8A
		public StoreId ItemId { get; set; }

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x00046D93 File Offset: 0x00044F93
		// (set) Token: 0x060010FD RID: 4349 RVA: 0x00046D9B File Offset: 0x00044F9B
		public CalendarItemType CalendarItemType { get; set; }

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x060010FE RID: 4350 RVA: 0x00046DA4 File Offset: 0x00044FA4
		// (set) Token: 0x060010FF RID: 4351 RVA: 0x00046DAC File Offset: 0x00044FAC
		public ExDateTime? Start { get; set; }

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001100 RID: 4352 RVA: 0x00046DB5 File Offset: 0x00044FB5
		// (set) Token: 0x06001101 RID: 4353 RVA: 0x00046DBD File Offset: 0x00044FBD
		public ExDateTime? End { get; set; }

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06001102 RID: 4354 RVA: 0x00046DC6 File Offset: 0x00044FC6
		// (set) Token: 0x06001103 RID: 4355 RVA: 0x00046DCE File Offset: 0x00044FCE
		public ExDateTime? StartWallClock { get; set; }

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001104 RID: 4356 RVA: 0x00046DD7 File Offset: 0x00044FD7
		// (set) Token: 0x06001105 RID: 4357 RVA: 0x00046DDF File Offset: 0x00044FDF
		public ExDateTime? EndWallClock { get; set; }

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001106 RID: 4358 RVA: 0x00046DE8 File Offset: 0x00044FE8
		// (set) Token: 0x06001107 RID: 4359 RVA: 0x00046DF0 File Offset: 0x00044FF0
		public Dictionary<PropertyDefinition, object> RowData { get; set; }
	}
}
