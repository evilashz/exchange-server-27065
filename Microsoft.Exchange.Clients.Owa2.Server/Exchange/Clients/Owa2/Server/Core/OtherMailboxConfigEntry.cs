using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000E4 RID: 228
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[SimpleConfiguration("OWA.OtherMailbox", "OtherMailbox")]
	internal class OtherMailboxConfigEntry
	{
		// Token: 0x06000865 RID: 2149 RVA: 0x0001B9CD File Offset: 0x00019BCD
		public OtherMailboxConfigEntry()
		{
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0001B9EB File Offset: 0x00019BEB
		public OtherMailboxConfigEntry(string displayName, string inboxFolderOwaStoreObjectId)
		{
			this.mailboxDisplayName = displayName;
			this.inboxFolderOwaStoreObjectId = inboxFolderOwaStoreObjectId;
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x0001BA17 File Offset: 0x00019C17
		// (set) Token: 0x06000868 RID: 2152 RVA: 0x0001BA1F File Offset: 0x00019C1F
		[SimpleConfigurationProperty("displayName")]
		[DataMember]
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

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x0001BA28 File Offset: 0x00019C28
		// (set) Token: 0x0600086A RID: 2154 RVA: 0x0001BA30 File Offset: 0x00019C30
		[SimpleConfigurationProperty("rootFolderId")]
		public string InboxFolderOwaStoreObjectId
		{
			get
			{
				return this.inboxFolderOwaStoreObjectId;
			}
			set
			{
				this.inboxFolderOwaStoreObjectId = value;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x0001BA39 File Offset: 0x00019C39
		// (set) Token: 0x0600086C RID: 2156 RVA: 0x0001BA41 File Offset: 0x00019C41
		[DataMember]
		[SimpleConfigurationProperty("principalSMTPAddress")]
		public string PrincipalSMTPAddress
		{
			get
			{
				return this.principalSMTPAddress;
			}
			set
			{
				this.principalSMTPAddress = value;
			}
		}

		// Token: 0x04000520 RID: 1312
		private string mailboxDisplayName = string.Empty;

		// Token: 0x04000521 RID: 1313
		private string inboxFolderOwaStoreObjectId = string.Empty;

		// Token: 0x04000522 RID: 1314
		private string principalSMTPAddress;
	}
}
