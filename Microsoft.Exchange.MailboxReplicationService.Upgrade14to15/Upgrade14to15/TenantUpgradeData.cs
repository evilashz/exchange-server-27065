using System;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000C4 RID: 196
	internal struct TenantUpgradeData
	{
		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x0000D297 File Offset: 0x0000B497
		// (set) Token: 0x060005EE RID: 1518 RVA: 0x0000D29F File Offset: 0x0000B49F
		public TenantOrganizationPresentationObjectWrapper Tenant { get; set; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x0000D2A8 File Offset: 0x0000B4A8
		// (set) Token: 0x060005F0 RID: 1520 RVA: 0x0000D2B0 File Offset: 0x0000B4B0
		public RecipientWrapper PilotUser { get; set; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x0000D2B9 File Offset: 0x0000B4B9
		// (set) Token: 0x060005F2 RID: 1522 RVA: 0x0000D2C1 File Offset: 0x0000B4C1
		public string ErrorType { get; set; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x0000D2CA File Offset: 0x0000B4CA
		// (set) Token: 0x060005F4 RID: 1524 RVA: 0x0000D2D2 File Offset: 0x0000B4D2
		public string ErrorDetails { get; set; }
	}
}
