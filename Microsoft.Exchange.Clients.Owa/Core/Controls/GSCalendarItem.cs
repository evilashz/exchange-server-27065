using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x0200031C RID: 796
	internal class GSCalendarItem
	{
		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06001E46 RID: 7750 RVA: 0x000AF3DF File Offset: 0x000AD5DF
		// (set) Token: 0x06001E47 RID: 7751 RVA: 0x000AF3E7 File Offset: 0x000AD5E7
		public ExDateTime StartTime { get; set; }

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06001E48 RID: 7752 RVA: 0x000AF3F0 File Offset: 0x000AD5F0
		// (set) Token: 0x06001E49 RID: 7753 RVA: 0x000AF3F8 File Offset: 0x000AD5F8
		public ExDateTime EndTime { get; set; }

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06001E4A RID: 7754 RVA: 0x000AF401 File Offset: 0x000AD601
		// (set) Token: 0x06001E4B RID: 7755 RVA: 0x000AF409 File Offset: 0x000AD609
		public BusyTypeWrapper BusyType { get; set; }

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06001E4C RID: 7756 RVA: 0x000AF412 File Offset: 0x000AD612
		// (set) Token: 0x06001E4D RID: 7757 RVA: 0x000AF41A File Offset: 0x000AD61A
		public string Subject { get; set; }

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06001E4E RID: 7758 RVA: 0x000AF423 File Offset: 0x000AD623
		// (set) Token: 0x06001E4F RID: 7759 RVA: 0x000AF42B File Offset: 0x000AD62B
		public string Location { get; set; }

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06001E50 RID: 7760 RVA: 0x000AF434 File Offset: 0x000AD634
		// (set) Token: 0x06001E51 RID: 7761 RVA: 0x000AF43C File Offset: 0x000AD63C
		public bool IsMeeting { get; set; }

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06001E52 RID: 7762 RVA: 0x000AF445 File Offset: 0x000AD645
		// (set) Token: 0x06001E53 RID: 7763 RVA: 0x000AF44D File Offset: 0x000AD64D
		public CalendarItemTypeWrapper CalendarItemType { get; set; }

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06001E54 RID: 7764 RVA: 0x000AF456 File Offset: 0x000AD656
		// (set) Token: 0x06001E55 RID: 7765 RVA: 0x000AF45E File Offset: 0x000AD65E
		public bool IsPrivate { get; set; }
	}
}
