using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000070 RID: 112
	public class ActiveSyncRequestData
	{
		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x00024C18 File Offset: 0x00022E18
		// (set) Token: 0x0600061A RID: 1562 RVA: 0x00024C20 File Offset: 0x00022E20
		public Guid Id { get; private set; }

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x00024C29 File Offset: 0x00022E29
		// (set) Token: 0x0600061C RID: 1564 RVA: 0x00024C31 File Offset: 0x00022E31
		public string ServerName { get; set; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x00024C3A File Offset: 0x00022E3A
		// (set) Token: 0x0600061E RID: 1566 RVA: 0x00024C42 File Offset: 0x00022E42
		public string DeviceID { get; set; }

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x00024C4B File Offset: 0x00022E4B
		// (set) Token: 0x06000620 RID: 1568 RVA: 0x00024C53 File Offset: 0x00022E53
		public string DeviceType { get; set; }

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x00024C5C File Offset: 0x00022E5C
		// (set) Token: 0x06000622 RID: 1570 RVA: 0x00024C64 File Offset: 0x00022E64
		public string UserAgent { get; set; }

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x00024C6D File Offset: 0x00022E6D
		// (set) Token: 0x06000624 RID: 1572 RVA: 0x00024C75 File Offset: 0x00022E75
		public string UserEmail { get; set; }

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x00024C7E File Offset: 0x00022E7E
		// (set) Token: 0x06000626 RID: 1574 RVA: 0x00024C86 File Offset: 0x00022E86
		public HttpStatusCode HttpStatus { get; set; }

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x00024C8F File Offset: 0x00022E8F
		// (set) Token: 0x06000628 RID: 1576 RVA: 0x00024C97 File Offset: 0x00022E97
		public string AirSyncStatus { get; set; }

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x00024CA0 File Offset: 0x00022EA0
		// (set) Token: 0x0600062A RID: 1578 RVA: 0x00024CA8 File Offset: 0x00022EA8
		public bool HasErrors { get; set; }

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x00024CB1 File Offset: 0x00022EB1
		// (set) Token: 0x0600062C RID: 1580 RVA: 0x00024CB9 File Offset: 0x00022EB9
		public List<ErrorDetail> ErrorDetails { get; set; }

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x00024CC2 File Offset: 0x00022EC2
		// (set) Token: 0x0600062E RID: 1582 RVA: 0x00024CCA File Offset: 0x00022ECA
		public ExDateTime StartTime { get; set; }

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x00024CD3 File Offset: 0x00022ED3
		// (set) Token: 0x06000630 RID: 1584 RVA: 0x00024CDB File Offset: 0x00022EDB
		public double RequestTime { get; set; }

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x00024CE4 File Offset: 0x00022EE4
		// (set) Token: 0x06000632 RID: 1586 RVA: 0x00024CEC File Offset: 0x00022EEC
		public bool IsHanging { get; set; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x00024CF5 File Offset: 0x00022EF5
		// (set) Token: 0x06000634 RID: 1588 RVA: 0x00024CFD File Offset: 0x00022EFD
		public string CommandName { get; set; }

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x00024D06 File Offset: 0x00022F06
		// (set) Token: 0x06000636 RID: 1590 RVA: 0x00024D0E File Offset: 0x00022F0E
		public bool NewDeviceCreated { get; set; }

		// Token: 0x06000637 RID: 1591 RVA: 0x00024D17 File Offset: 0x00022F17
		internal ActiveSyncRequestData(Guid id)
		{
			this.Id = id;
		}
	}
}
