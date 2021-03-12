using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000173 RID: 371
	[SimpleConfiguration("OWA.OtherMailbox", "OtherMailbox")]
	internal class OtherMailboxConfigEntry
	{
		// Token: 0x06000D0D RID: 3341 RVA: 0x000586C5 File Offset: 0x000568C5
		public OtherMailboxConfigEntry()
		{
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x000586CD File Offset: 0x000568CD
		public OtherMailboxConfigEntry(string displayName, OwaStoreObjectId rootFolderId)
		{
			this.mailboxDisplayName = displayName;
			this.mailboxRootFolderId = rootFolderId.ToString();
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x000586E8 File Offset: 0x000568E8
		// (set) Token: 0x06000D10 RID: 3344 RVA: 0x000586F0 File Offset: 0x000568F0
		[SimpleConfigurationProperty("displayName")]
		public string DisplayName
		{
			get
			{
				return this.mailboxDisplayName;
			}
			set
			{
				this.mailboxDisplayName = value;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x000586F9 File Offset: 0x000568F9
		// (set) Token: 0x06000D12 RID: 3346 RVA: 0x00058701 File Offset: 0x00056901
		[SimpleConfigurationProperty("rootFolderId")]
		public string RootFolderId
		{
			get
			{
				return this.mailboxRootFolderId;
			}
			set
			{
				this.mailboxRootFolderId = value;
			}
		}

		// Token: 0x04000919 RID: 2329
		private string mailboxDisplayName;

		// Token: 0x0400091A RID: 2330
		private string mailboxRootFolderId;
	}
}
