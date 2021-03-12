using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001E0 RID: 480
	[Serializable]
	public sealed class MoveFolderInfo : XMLSerializableBase, IEquatable<MoveFolderInfo>
	{
		// Token: 0x060013FA RID: 5114 RVA: 0x0002DDF0 File Offset: 0x0002BFF0
		private MoveFolderInfo()
		{
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x0002DDF8 File Offset: 0x0002BFF8
		public MoveFolderInfo(string entryId, bool isFinalized)
		{
			this.EntryId = entryId;
			this.IsFinalized = isFinalized;
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x060013FC RID: 5116 RVA: 0x0002DE0E File Offset: 0x0002C00E
		// (set) Token: 0x060013FD RID: 5117 RVA: 0x0002DE16 File Offset: 0x0002C016
		[XmlElement(ElementName = "EntryId")]
		public string EntryId { get; set; }

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x060013FE RID: 5118 RVA: 0x0002DE1F File Offset: 0x0002C01F
		// (set) Token: 0x060013FF RID: 5119 RVA: 0x0002DE27 File Offset: 0x0002C027
		[XmlElement(ElementName = "IsFinalized")]
		public bool IsFinalized { get; set; }

		// Token: 0x06001400 RID: 5120 RVA: 0x0002DE30 File Offset: 0x0002C030
		public bool Equals(MoveFolderInfo folder)
		{
			return this.EntryId == folder.EntryId;
		}
	}
}
