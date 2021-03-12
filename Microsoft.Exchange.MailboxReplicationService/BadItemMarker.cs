using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000003 RID: 3
	public sealed class BadItemMarker : XMLSerializableBase
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002FB9 File Offset: 0x000011B9
		public BadItemMarker()
		{
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002FC1 File Offset: 0x000011C1
		internal BadItemMarker(BadMessageRec badItem)
		{
			this.Kind = badItem.Kind;
			this.EntryId = badItem.EntryId;
			this.MessageSize = badItem.MessageSize;
			this.Category = badItem.Category;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002FF9 File Offset: 0x000011F9
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00003001 File Offset: 0x00001201
		[XmlIgnore]
		public BadItemKind Kind { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000300A File Offset: 0x0000120A
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00003012 File Offset: 0x00001212
		[XmlElement(ElementName = "Kind")]
		public int KindInt
		{
			get
			{
				return (int)this.Kind;
			}
			set
			{
				this.Kind = (BadItemKind)value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000301B File Offset: 0x0000121B
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00003023 File Offset: 0x00001223
		[XmlElement(ElementName = "EntryId")]
		public byte[] EntryId { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000302C File Offset: 0x0000122C
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00003034 File Offset: 0x00001234
		[XmlElement(ElementName = "MessageSize")]
		public int? MessageSize { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000303D File Offset: 0x0000123D
		// (set) Token: 0x06000011 RID: 17 RVA: 0x00003045 File Offset: 0x00001245
		[XmlElement(ElementName = "Category")]
		public string Category { get; set; }
	}
}
