using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MailboxLoadBalance.MailboxProcessors;

namespace Microsoft.Exchange.MailboxLoadBalance.Logging
{
	// Token: 0x020000A9 RID: 169
	internal class ProvisioningConstraintFixStateLogEntry
	{
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x0000F8F9 File Offset: 0x0000DAF9
		// (set) Token: 0x060005E5 RID: 1509 RVA: 0x0000F901 File Offset: 0x0000DB01
		public Guid MailboxGuid { get; set; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x0000F90A File Offset: 0x0000DB0A
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x0000F912 File Offset: 0x0000DB12
		public Guid ExistingMoveRequestGuid { get; set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x0000F91B File Offset: 0x0000DB1B
		// (set) Token: 0x060005E9 RID: 1513 RVA: 0x0000F923 File Offset: 0x0000DB23
		public MoveStatus ExistingMoveStatus { get; set; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x0000F92C File Offset: 0x0000DB2C
		// (set) Token: 0x060005EB RID: 1515 RVA: 0x0000F934 File Offset: 0x0000DB34
		public Guid SourceDatabaseGuid { get; set; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x0000F93D File Offset: 0x0000DB3D
		// (set) Token: 0x060005ED RID: 1517 RVA: 0x0000F945 File Offset: 0x0000DB45
		public MailboxProvisioningAttributes SourceDatabaseProvisioningAttributes { get; set; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x0000F94E File Offset: 0x0000DB4E
		// (set) Token: 0x060005EF RID: 1519 RVA: 0x0000F956 File Offset: 0x0000DB56
		public IMailboxProvisioningConstraint MailboxProvisioningHardConstraint { get; set; }
	}
}
