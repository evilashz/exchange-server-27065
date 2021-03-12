using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000154 RID: 340
	[Serializable]
	public sealed class MailboxSizeRec : XMLSerializableBase
	{
		// Token: 0x06000BCE RID: 3022 RVA: 0x0001AD1B File Offset: 0x00018F1B
		public MailboxSizeRec()
		{
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0001AD24 File Offset: 0x00018F24
		internal MailboxSizeRec(MailboxInformation mailboxInfo)
		{
			this.ItemCount = mailboxInfo.RegularItemCount;
			this.ItemSize = mailboxInfo.RegularItemsSize;
			this.FAIItemCount = mailboxInfo.AssociatedItemCount;
			this.FAIItemSize = mailboxInfo.AssociatedItemsSize;
			this.DeletedItemCount = mailboxInfo.RegularDeletedItemCount;
			this.DeletedItemSize = mailboxInfo.RegularDeletedItemsSize;
			this.DeletedFAIItemCount = mailboxInfo.AssociatedDeletedItemCount;
			this.DeletedFAIItemSize = mailboxInfo.AssociatedDeletedItemsSize;
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x0001AD97 File Offset: 0x00018F97
		// (set) Token: 0x06000BD1 RID: 3025 RVA: 0x0001AD9F File Offset: 0x00018F9F
		[XmlElement(ElementName = "ItemCount")]
		public ulong ItemCount { get; set; }

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x0001ADA8 File Offset: 0x00018FA8
		// (set) Token: 0x06000BD3 RID: 3027 RVA: 0x0001ADB0 File Offset: 0x00018FB0
		[XmlElement(ElementName = "ItemSize")]
		public ulong ItemSize { get; set; }

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x0001ADB9 File Offset: 0x00018FB9
		// (set) Token: 0x06000BD5 RID: 3029 RVA: 0x0001ADC1 File Offset: 0x00018FC1
		[XmlElement(ElementName = "FAIItemCount")]
		public ulong FAIItemCount { get; set; }

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x0001ADCA File Offset: 0x00018FCA
		// (set) Token: 0x06000BD7 RID: 3031 RVA: 0x0001ADD2 File Offset: 0x00018FD2
		[XmlElement(ElementName = "FAIItemSize")]
		public ulong FAIItemSize { get; set; }

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x0001ADDB File Offset: 0x00018FDB
		// (set) Token: 0x06000BD9 RID: 3033 RVA: 0x0001ADE3 File Offset: 0x00018FE3
		[XmlElement(ElementName = "DeletedItemCount")]
		public ulong DeletedItemCount { get; set; }

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x0001ADEC File Offset: 0x00018FEC
		// (set) Token: 0x06000BDB RID: 3035 RVA: 0x0001ADF4 File Offset: 0x00018FF4
		[XmlElement(ElementName = "DeletedItemSize")]
		public ulong DeletedItemSize { get; set; }

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000BDC RID: 3036 RVA: 0x0001ADFD File Offset: 0x00018FFD
		// (set) Token: 0x06000BDD RID: 3037 RVA: 0x0001AE05 File Offset: 0x00019005
		[XmlElement(ElementName = "DeletedFAIItemCount")]
		public ulong DeletedFAIItemCount { get; set; }

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x0001AE0E File Offset: 0x0001900E
		// (set) Token: 0x06000BDF RID: 3039 RVA: 0x0001AE16 File Offset: 0x00019016
		[XmlElement(ElementName = "DeletedFAIItemSize")]
		public ulong DeletedFAIItemSize { get; set; }

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0001AE20 File Offset: 0x00019020
		public override string ToString()
		{
			return MrsStrings.ItemCountsAndSizes(this.ItemCount, new ByteQuantifiedSize(this.ItemSize).ToString(), this.DeletedItemCount, new ByteQuantifiedSize(this.DeletedItemSize).ToString(), this.FAIItemCount, new ByteQuantifiedSize(this.FAIItemSize).ToString(), this.DeletedFAIItemCount, new ByteQuantifiedSize(this.DeletedFAIItemSize).ToString());
		}
	}
}
