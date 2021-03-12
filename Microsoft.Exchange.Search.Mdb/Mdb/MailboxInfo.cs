using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x02000018 RID: 24
	internal class MailboxInfo
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000064E5 File Offset: 0x000046E5
		// (set) Token: 0x06000073 RID: 115 RVA: 0x000064ED File Offset: 0x000046ED
		public Guid MailboxGuid { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000064F6 File Offset: 0x000046F6
		// (set) Token: 0x06000075 RID: 117 RVA: 0x000064FE File Offset: 0x000046FE
		public bool IsArchive { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00006507 File Offset: 0x00004707
		// (set) Token: 0x06000077 RID: 119 RVA: 0x0000650F File Offset: 0x0000470F
		public string DisplayName { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00006518 File Offset: 0x00004718
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00006520 File Offset: 0x00004720
		public int MailboxNumber { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00006529 File Offset: 0x00004729
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00006531 File Offset: 0x00004731
		public bool IsPublicFolderMailbox { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600007C RID: 124 RVA: 0x0000653A File Offset: 0x0000473A
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00006542 File Offset: 0x00004742
		public OrganizationId OrganizationId { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600007E RID: 126 RVA: 0x0000654B File Offset: 0x0000474B
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00006553 File Offset: 0x00004753
		public bool IsSharedMailbox { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000080 RID: 128 RVA: 0x0000655C File Offset: 0x0000475C
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00006564 File Offset: 0x00004764
		public bool IsTeamSiteMailbox { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000082 RID: 130 RVA: 0x0000656D File Offset: 0x0000476D
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00006575 File Offset: 0x00004775
		public bool IsModernGroupMailbox { get; set; }

		// Token: 0x06000084 RID: 132 RVA: 0x0000657E File Offset: 0x0000477E
		public override string ToString()
		{
			return string.Format("{0} ({1}, MailboxNumber={2})", this.DisplayName, this.MailboxGuid, this.MailboxNumber);
		}
	}
}
