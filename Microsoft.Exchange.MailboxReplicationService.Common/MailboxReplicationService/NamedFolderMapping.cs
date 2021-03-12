using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000119 RID: 281
	internal class NamedFolderMapping : WellKnownFolderMapping
	{
		// Token: 0x060009B2 RID: 2482 RVA: 0x00013DFE File Offset: 0x00011FFE
		public NamedFolderMapping(WellKnownFolderType wkft, WellKnownFolderType parentType, string folderName)
		{
			base.WKFType = wkft;
			this.ParentType = parentType;
			this.FolderName = folderName;
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x00013E1B File Offset: 0x0001201B
		// (set) Token: 0x060009B4 RID: 2484 RVA: 0x00013E23 File Offset: 0x00012023
		public WellKnownFolderType ParentType { get; protected set; }

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x00013E2C File Offset: 0x0001202C
		// (set) Token: 0x060009B6 RID: 2486 RVA: 0x00013E34 File Offset: 0x00012034
		public string FolderName { get; protected set; }
	}
}
