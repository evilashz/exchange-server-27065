using System;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200041C RID: 1052
	[SimpleConfiguration("TargetFolderMRU", "TargetFolderMRU")]
	internal class TargetFolderMRUEntry
	{
		// Token: 0x060025BD RID: 9661 RVA: 0x000DA8BE File Offset: 0x000D8ABE
		public TargetFolderMRUEntry()
		{
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x000DA8C6 File Offset: 0x000D8AC6
		public TargetFolderMRUEntry(OwaStoreObjectId folderId)
		{
			this.folderId = folderId.ToString();
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x060025BF RID: 9663 RVA: 0x000DA8DA File Offset: 0x000D8ADA
		// (set) Token: 0x060025C0 RID: 9664 RVA: 0x000DA8E2 File Offset: 0x000D8AE2
		[SimpleConfigurationProperty("folderId")]
		public string FolderId
		{
			get
			{
				return this.folderId;
			}
			set
			{
				this.folderId = value;
			}
		}

		// Token: 0x04001A10 RID: 6672
		private string folderId;
	}
}
