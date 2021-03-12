using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.InfoWorker.Common.ELC
{
	// Token: 0x0200018A RID: 394
	internal class AdFolderData
	{
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x0002CA64 File Offset: 0x0002AC64
		// (set) Token: 0x06000A8D RID: 2701 RVA: 0x0002CA6C File Offset: 0x0002AC6C
		public ELCFolder Folder
		{
			get
			{
				return this.folder;
			}
			set
			{
				this.folder = value;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x0002CA75 File Offset: 0x0002AC75
		// (set) Token: 0x06000A8F RID: 2703 RVA: 0x0002CA7D File Offset: 0x0002AC7D
		public ContentSetting[] FolderSettings
		{
			get
			{
				return this.folderSettings;
			}
			set
			{
				this.folderSettings = value;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x0002CA86 File Offset: 0x0002AC86
		// (set) Token: 0x06000A91 RID: 2705 RVA: 0x0002CA8E File Offset: 0x0002AC8E
		public bool LinkedToTemplate
		{
			get
			{
				return this.linkedToTemplate;
			}
			set
			{
				this.linkedToTemplate = value;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x0002CA97 File Offset: 0x0002AC97
		// (set) Token: 0x06000A93 RID: 2707 RVA: 0x0002CA9F File Offset: 0x0002AC9F
		public bool Synced
		{
			get
			{
				return this.synced;
			}
			set
			{
				this.synced = value;
			}
		}

		// Token: 0x04000807 RID: 2055
		private ELCFolder folder;

		// Token: 0x04000808 RID: 2056
		private ContentSetting[] folderSettings;

		// Token: 0x04000809 RID: 2057
		private bool linkedToTemplate;

		// Token: 0x0400080A RID: 2058
		private bool synced;
	}
}
