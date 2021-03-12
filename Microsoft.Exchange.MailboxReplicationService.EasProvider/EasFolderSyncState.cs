using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000006 RID: 6
	[XmlType(TypeName = "EasFolderSyncState")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	public sealed class EasFolderSyncState : XMLSerializableBase
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00003CF3 File Offset: 0x00001EF3
		public EasFolderSyncState()
		{
			this.CrawlerDeletions = new List<string>(512);
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003D0B File Offset: 0x00001F0B
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00003D13 File Offset: 0x00001F13
		[XmlElement(ElementName = "SyncKey")]
		public string SyncKey { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003D1C File Offset: 0x00001F1C
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00003D24 File Offset: 0x00001F24
		[XmlElement(ElementName = "ChangesSynced")]
		public DateTime? ChangesSynced { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003D2D File Offset: 0x00001F2D
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00003D35 File Offset: 0x00001F35
		[XmlElement(ElementName = "CrawlerSyncKey")]
		public string CrawlerSyncKey { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00003D3E File Offset: 0x00001F3E
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00003D46 File Offset: 0x00001F46
		[XmlArray(ElementName = "CrawlerDeletions")]
		public List<string> CrawlerDeletions { get; set; }

		// Token: 0x0600008C RID: 140 RVA: 0x00003D50 File Offset: 0x00001F50
		internal static EasFolderSyncState Deserialize(byte[] data)
		{
			string @string = Encoding.UTF7.GetString(data);
			return XMLSerializableBase.Deserialize<EasFolderSyncState>(@string, true);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003D70 File Offset: 0x00001F70
		internal byte[] Serialize()
		{
			string s = base.Serialize(false);
			return Encoding.UTF7.GetBytes(s);
		}
	}
}
