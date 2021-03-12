using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Auditing
{
	// Token: 0x02000F1A RID: 3866
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UnifiedAuditLoggerSettings
	{
		// Token: 0x1700233C RID: 9020
		// (get) Token: 0x0600850B RID: 34059 RVA: 0x002465C5 File Offset: 0x002447C5
		// (set) Token: 0x0600850C RID: 34060 RVA: 0x002465CD File Offset: 0x002447CD
		internal string DirectoryPath { get; private set; }

		// Token: 0x1700233D RID: 9021
		// (get) Token: 0x0600850D RID: 34061 RVA: 0x002465D6 File Offset: 0x002447D6
		// (set) Token: 0x0600850E RID: 34062 RVA: 0x002465DE File Offset: 0x002447DE
		internal TimeSpan MaxAge { get; private set; }

		// Token: 0x1700233E RID: 9022
		// (get) Token: 0x0600850F RID: 34063 RVA: 0x002465E7 File Offset: 0x002447E7
		// (set) Token: 0x06008510 RID: 34064 RVA: 0x002465EF File Offset: 0x002447EF
		internal ByteQuantifiedSize MaxDirectorySize { get; private set; }

		// Token: 0x1700233F RID: 9023
		// (get) Token: 0x06008511 RID: 34065 RVA: 0x002465F8 File Offset: 0x002447F8
		// (set) Token: 0x06008512 RID: 34066 RVA: 0x00246600 File Offset: 0x00244800
		internal ByteQuantifiedSize MaxFileSize { get; private set; }

		// Token: 0x17002340 RID: 9024
		// (get) Token: 0x06008513 RID: 34067 RVA: 0x00246609 File Offset: 0x00244809
		// (set) Token: 0x06008514 RID: 34068 RVA: 0x00246611 File Offset: 0x00244811
		internal ByteQuantifiedSize CacheSize { get; private set; }

		// Token: 0x17002341 RID: 9025
		// (get) Token: 0x06008515 RID: 34069 RVA: 0x0024661A File Offset: 0x0024481A
		// (set) Token: 0x06008516 RID: 34070 RVA: 0x00246622 File Offset: 0x00244822
		internal TimeSpan FlushInterval { get; private set; }

		// Token: 0x17002342 RID: 9026
		// (get) Token: 0x06008517 RID: 34071 RVA: 0x0024662B File Offset: 0x0024482B
		// (set) Token: 0x06008518 RID: 34072 RVA: 0x00246633 File Offset: 0x00244833
		internal bool FlushToDisk { get; private set; }

		// Token: 0x06008519 RID: 34073 RVA: 0x0024663C File Offset: 0x0024483C
		internal static UnifiedAuditLoggerSettings Load()
		{
			return new UnifiedAuditLoggerSettings
			{
				CacheSize = ByteQuantifiedSize.FromKB(128UL),
				DirectoryPath = "D:\\ComplianceAudit\\LocalQueue",
				FlushInterval = TimeSpan.FromMilliseconds(1000.0),
				FlushToDisk = true,
				MaxAge = TimeSpan.FromDays(3.0),
				MaxDirectorySize = ByteQuantifiedSize.FromMB(5000UL),
				MaxFileSize = ByteQuantifiedSize.FromMB(4UL)
			};
		}
	}
}
