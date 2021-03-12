using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000874 RID: 2164
	[DataContract(Name = "SearchPreviewItem", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "SearchPreviewItemType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class SearchPreviewItem
	{
		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x06003E12 RID: 15890 RVA: 0x000D822F File Offset: 0x000D642F
		// (set) Token: 0x06003E13 RID: 15891 RVA: 0x000D8237 File Offset: 0x000D6437
		[XmlElement("Id")]
		[DataMember(Name = "Id", IsRequired = true)]
		public ItemId Id { get; set; }

		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x06003E14 RID: 15892 RVA: 0x000D8240 File Offset: 0x000D6440
		// (set) Token: 0x06003E15 RID: 15893 RVA: 0x000D8248 File Offset: 0x000D6448
		[XmlElement("Mailbox")]
		[DataMember(Name = "Mailbox", IsRequired = true)]
		public PreviewItemMailbox Mailbox { get; set; }

		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x06003E16 RID: 15894 RVA: 0x000D8251 File Offset: 0x000D6451
		// (set) Token: 0x06003E17 RID: 15895 RVA: 0x000D8259 File Offset: 0x000D6459
		[XmlElement("ParentId")]
		[DataMember(Name = "ParentId", IsRequired = true)]
		public ItemId ParentId { get; set; }

		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x06003E18 RID: 15896 RVA: 0x000D8262 File Offset: 0x000D6462
		// (set) Token: 0x06003E19 RID: 15897 RVA: 0x000D826A File Offset: 0x000D646A
		[DataMember(Name = "ItemClass", IsRequired = false)]
		[XmlElement("ItemClass")]
		public string ItemClass { get; set; }

		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x06003E1A RID: 15898 RVA: 0x000D8273 File Offset: 0x000D6473
		// (set) Token: 0x06003E1B RID: 15899 RVA: 0x000D827B File Offset: 0x000D647B
		[DataMember(Name = "UniqueHash", IsRequired = false)]
		[XmlElement("UniqueHash")]
		public string UniqueHash { get; set; }

		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x06003E1C RID: 15900 RVA: 0x000D8284 File Offset: 0x000D6484
		// (set) Token: 0x06003E1D RID: 15901 RVA: 0x000D828C File Offset: 0x000D648C
		[XmlElement("SortValue")]
		[DataMember(Name = "SortValue", IsRequired = false)]
		public string SortValue { get; set; }

		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x06003E1E RID: 15902 RVA: 0x000D8295 File Offset: 0x000D6495
		// (set) Token: 0x06003E1F RID: 15903 RVA: 0x000D829D File Offset: 0x000D649D
		[DataMember(Name = "OwaLink", IsRequired = false)]
		[XmlElement("OwaLink")]
		public string OwaLink { get; set; }

		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x06003E20 RID: 15904 RVA: 0x000D82A6 File Offset: 0x000D64A6
		// (set) Token: 0x06003E21 RID: 15905 RVA: 0x000D82AE File Offset: 0x000D64AE
		[DataMember(Name = "Sender", IsRequired = false)]
		[XmlElement("Sender")]
		public string Sender { get; set; }

		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x06003E22 RID: 15906 RVA: 0x000D82B7 File Offset: 0x000D64B7
		// (set) Token: 0x06003E23 RID: 15907 RVA: 0x000D82BF File Offset: 0x000D64BF
		[XmlArrayItem(ElementName = "SmtpAddress", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(string))]
		[DataMember(Name = "ToRecipients", IsRequired = false)]
		[XmlArray(ElementName = "ToRecipients", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public string[] ToRecipients { get; set; }

		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x06003E24 RID: 15908 RVA: 0x000D82C8 File Offset: 0x000D64C8
		// (set) Token: 0x06003E25 RID: 15909 RVA: 0x000D82D0 File Offset: 0x000D64D0
		[DataMember(Name = "CcRecipients", IsRequired = false)]
		[XmlArray(ElementName = "CcRecipients", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[XmlArrayItem(ElementName = "SmtpAddress", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(string))]
		public string[] CcRecipients { get; set; }

		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x06003E26 RID: 15910 RVA: 0x000D82D9 File Offset: 0x000D64D9
		// (set) Token: 0x06003E27 RID: 15911 RVA: 0x000D82E1 File Offset: 0x000D64E1
		[XmlArray(ElementName = "BccRecipients", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[XmlArrayItem(ElementName = "SmtpAddress", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(string))]
		[DataMember(Name = "BccRecipients", IsRequired = false)]
		public string[] BccRecipients { get; set; }

		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x06003E28 RID: 15912 RVA: 0x000D82EA File Offset: 0x000D64EA
		// (set) Token: 0x06003E29 RID: 15913 RVA: 0x000D82F2 File Offset: 0x000D64F2
		[DataMember(Name = "CreatedTime", EmitDefaultValue = false)]
		[XmlElement("CreatedTime")]
		public string CreatedTime { get; set; }

		// Token: 0x17000EFF RID: 3839
		// (get) Token: 0x06003E2A RID: 15914 RVA: 0x000D82FB File Offset: 0x000D64FB
		// (set) Token: 0x06003E2B RID: 15915 RVA: 0x000D8303 File Offset: 0x000D6503
		[DataMember(Name = "ReceivedTime", EmitDefaultValue = false)]
		[XmlElement("ReceivedTime")]
		public string ReceivedTime { get; set; }

		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x06003E2C RID: 15916 RVA: 0x000D830C File Offset: 0x000D650C
		// (set) Token: 0x06003E2D RID: 15917 RVA: 0x000D8314 File Offset: 0x000D6514
		[DataMember(Name = "SentTime", EmitDefaultValue = false)]
		[XmlElement("SentTime")]
		public string SentTime { get; set; }

		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x06003E2E RID: 15918 RVA: 0x000D831D File Offset: 0x000D651D
		// (set) Token: 0x06003E2F RID: 15919 RVA: 0x000D8325 File Offset: 0x000D6525
		[DataMember(Name = "Subject", IsRequired = false)]
		[XmlElement("Subject")]
		public string Subject { get; set; }

		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x06003E30 RID: 15920 RVA: 0x000D832E File Offset: 0x000D652E
		// (set) Token: 0x06003E31 RID: 15921 RVA: 0x000D8336 File Offset: 0x000D6536
		[XmlElement("Size")]
		[DataMember(Name = "Size", EmitDefaultValue = true, IsRequired = false)]
		public ulong? Size { get; set; }

		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x06003E32 RID: 15922 RVA: 0x000D8340 File Offset: 0x000D6540
		// (set) Token: 0x06003E33 RID: 15923 RVA: 0x000D836D File Offset: 0x000D656D
		[XmlIgnore]
		[IgnoreDataMember]
		public bool SizeSpecified
		{
			get
			{
				return this.Size != null && this.Size != null;
			}
			set
			{
			}
		}

		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x06003E34 RID: 15924 RVA: 0x000D836F File Offset: 0x000D656F
		// (set) Token: 0x06003E35 RID: 15925 RVA: 0x000D8377 File Offset: 0x000D6577
		[DataMember(Name = "Preview", IsRequired = false)]
		[XmlElement("Preview")]
		public string Preview { get; set; }

		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x06003E36 RID: 15926 RVA: 0x000D8380 File Offset: 0x000D6580
		// (set) Token: 0x06003E37 RID: 15927 RVA: 0x000D8388 File Offset: 0x000D6588
		[XmlElement("Importance")]
		[DataMember(Name = "Importance", IsRequired = false)]
		public string Importance { get; set; }

		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x06003E38 RID: 15928 RVA: 0x000D8391 File Offset: 0x000D6591
		// (set) Token: 0x06003E39 RID: 15929 RVA: 0x000D8399 File Offset: 0x000D6599
		[XmlElement("Read")]
		[DataMember(Name = "Read", EmitDefaultValue = false, IsRequired = false)]
		public bool? Read { get; set; }

		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x06003E3A RID: 15930 RVA: 0x000D83A4 File Offset: 0x000D65A4
		// (set) Token: 0x06003E3B RID: 15931 RVA: 0x000D83D1 File Offset: 0x000D65D1
		[XmlIgnore]
		[IgnoreDataMember]
		public bool ReadSpecified
		{
			get
			{
				return this.Read != null && this.Read != null;
			}
			set
			{
			}
		}

		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x06003E3C RID: 15932 RVA: 0x000D83D3 File Offset: 0x000D65D3
		// (set) Token: 0x06003E3D RID: 15933 RVA: 0x000D83DB File Offset: 0x000D65DB
		[DataMember(Name = "HasAttachment", EmitDefaultValue = false, IsRequired = false)]
		[XmlElement("HasAttachment")]
		public bool? HasAttachment { get; set; }

		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x06003E3E RID: 15934 RVA: 0x000D83E4 File Offset: 0x000D65E4
		// (set) Token: 0x06003E3F RID: 15935 RVA: 0x000D8411 File Offset: 0x000D6611
		[IgnoreDataMember]
		[XmlIgnore]
		public bool HasAttachmentSpecified
		{
			get
			{
				return this.HasAttachment != null && this.HasAttachment != null;
			}
			set
			{
			}
		}

		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x06003E40 RID: 15936 RVA: 0x000D8413 File Offset: 0x000D6613
		// (set) Token: 0x06003E41 RID: 15937 RVA: 0x000D841B File Offset: 0x000D661B
		[XmlArrayItem(ElementName = "ExtendedProperty", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(ExtendedPropertyType))]
		[XmlArray(ElementName = "ExtendedProperties", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[DataMember(Name = "ExtendedProperties", EmitDefaultValue = false)]
		public ExtendedPropertyType[] ExtendedProperties { get; set; }
	}
}
