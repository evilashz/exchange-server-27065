using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200015F RID: 351
	public sealed class MissingMessageRec : XMLSerializableBase
	{
		// Token: 0x06000C5A RID: 3162 RVA: 0x0001D683 File Offset: 0x0001B883
		public MissingMessageRec()
		{
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x0001D68B File Offset: 0x0001B88B
		internal MissingMessageRec(MessageRec mr)
		{
			this.entryId = mr.EntryId;
			this.folderId = mr.FolderId;
			this.flags = (int)mr.Flags;
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000C5C RID: 3164 RVA: 0x0001D6B7 File Offset: 0x0001B8B7
		// (set) Token: 0x06000C5D RID: 3165 RVA: 0x0001D6BF File Offset: 0x0001B8BF
		[XmlElement]
		public byte[] EntryId
		{
			get
			{
				return this.entryId;
			}
			set
			{
				this.entryId = value;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x0001D6C8 File Offset: 0x0001B8C8
		// (set) Token: 0x06000C5F RID: 3167 RVA: 0x0001D6D0 File Offset: 0x0001B8D0
		[XmlElement]
		public byte[] FolderId
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

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x0001D6D9 File Offset: 0x0001B8D9
		// (set) Token: 0x06000C61 RID: 3169 RVA: 0x0001D6E1 File Offset: 0x0001B8E1
		[XmlElement]
		public int Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
			}
		}

		// Token: 0x04000711 RID: 1809
		private byte[] entryId;

		// Token: 0x04000712 RID: 1810
		private byte[] folderId;

		// Token: 0x04000713 RID: 1811
		private int flags;
	}
}
