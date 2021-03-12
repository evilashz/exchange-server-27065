using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000296 RID: 662
	public sealed class SyncHierarchyManifestState : XMLSerializableBase
	{
		// Token: 0x0600202C RID: 8236 RVA: 0x00044B10 File Offset: 0x00042D10
		public SyncHierarchyManifestState()
		{
			this.IdsetGiven = Array<byte>.Empty;
			this.CnsetSeen = Array<byte>.Empty;
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x0600202D RID: 8237 RVA: 0x00044B2E File Offset: 0x00042D2E
		// (set) Token: 0x0600202E RID: 8238 RVA: 0x00044B36 File Offset: 0x00042D36
		[XmlElement(ElementName = "IdsetGiven")]
		public byte[] IdsetGiven { get; set; }

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x0600202F RID: 8239 RVA: 0x00044B3F File Offset: 0x00042D3F
		// (set) Token: 0x06002030 RID: 8240 RVA: 0x00044B47 File Offset: 0x00042D47
		[XmlElement(ElementName = "CnsetSeen")]
		public byte[] CnsetSeen { get; set; }

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x06002031 RID: 8241 RVA: 0x00044B50 File Offset: 0x00042D50
		// (set) Token: 0x06002032 RID: 8242 RVA: 0x00044B58 File Offset: 0x00042D58
		[XmlElement(ElementName = "ProviderSyncState")]
		public string ProviderSyncState { get; set; }

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x06002033 RID: 8243 RVA: 0x00044B61 File Offset: 0x00042D61
		// (set) Token: 0x06002034 RID: 8244 RVA: 0x00044B69 File Offset: 0x00042D69
		[XmlElement(ElementName = "ManualSyncData")]
		public SyncHierarchyManifestState.FolderData[] ManualSyncData { get; set; }

		// Token: 0x02000297 RID: 663
		public sealed class FolderData : XMLSerializableBase
		{
			// Token: 0x06002035 RID: 8245 RVA: 0x00044B72 File Offset: 0x00042D72
			public FolderData()
			{
			}

			// Token: 0x06002036 RID: 8246 RVA: 0x00044B7A File Offset: 0x00042D7A
			internal FolderData(FolderRec fRec)
			{
				this.EntryId = fRec.EntryId;
				this.ParentId = fRec.ParentId;
				this.LastModifyTimestamp = fRec.LastModifyTimestamp;
			}

			// Token: 0x17000C38 RID: 3128
			// (get) Token: 0x06002037 RID: 8247 RVA: 0x00044BA6 File Offset: 0x00042DA6
			// (set) Token: 0x06002038 RID: 8248 RVA: 0x00044BAE File Offset: 0x00042DAE
			[XmlElement(ElementName = "EntryId")]
			public byte[] EntryId { get; set; }

			// Token: 0x17000C39 RID: 3129
			// (get) Token: 0x06002039 RID: 8249 RVA: 0x00044BB7 File Offset: 0x00042DB7
			// (set) Token: 0x0600203A RID: 8250 RVA: 0x00044BBF File Offset: 0x00042DBF
			[XmlElement(ElementName = "ParentId")]
			public byte[] ParentId { get; set; }

			// Token: 0x17000C3A RID: 3130
			// (get) Token: 0x0600203B RID: 8251 RVA: 0x00044BC8 File Offset: 0x00042DC8
			// (set) Token: 0x0600203C RID: 8252 RVA: 0x00044BD0 File Offset: 0x00042DD0
			[XmlElement(ElementName = "LastModifyTimestamp")]
			public DateTime LastModifyTimestamp { get; set; }
		}
	}
}
