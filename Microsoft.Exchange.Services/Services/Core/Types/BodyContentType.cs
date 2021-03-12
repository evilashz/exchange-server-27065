using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005A4 RID: 1444
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class BodyContentType
	{
		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06002923 RID: 10531 RVA: 0x000ACF45 File Offset: 0x000AB145
		// (set) Token: 0x06002924 RID: 10532 RVA: 0x000ACF4D File Offset: 0x000AB14D
		[IgnoreDataMember]
		[XmlAttribute("BodyType")]
		public BodyType BodyType { get; set; }

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06002925 RID: 10533 RVA: 0x000ACF56 File Offset: 0x000AB156
		// (set) Token: 0x06002926 RID: 10534 RVA: 0x000ACF63 File Offset: 0x000AB163
		[XmlIgnore]
		[DataMember(Name = "BodyType", IsRequired = true)]
		public string BodyTypeString
		{
			get
			{
				return EnumUtilities.ToString<BodyType>(this.BodyType);
			}
			set
			{
				this.BodyType = EnumUtilities.Parse<BodyType>(value);
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06002927 RID: 10535 RVA: 0x000ACF71 File Offset: 0x000AB171
		// (set) Token: 0x06002928 RID: 10536 RVA: 0x000ACF79 File Offset: 0x000AB179
		[DataMember(IsRequired = true, EmitDefaultValue = false)]
		[XmlText]
		public string Value { get; set; }

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06002929 RID: 10537 RVA: 0x000ACF82 File Offset: 0x000AB182
		// (set) Token: 0x0600292A RID: 10538 RVA: 0x000ACF8A File Offset: 0x000AB18A
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		[XmlIgnore]
		public string QuotedText { get; set; }

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x0600292B RID: 10539 RVA: 0x000ACF93 File Offset: 0x000AB193
		// (set) Token: 0x0600292C RID: 10540 RVA: 0x000ACF9B File Offset: 0x000AB19B
		[IgnoreDataMember]
		[XmlIgnore]
		public bool IsTruncatedSpecified { get; set; }

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x0600292D RID: 10541 RVA: 0x000ACFA4 File Offset: 0x000AB1A4
		// (set) Token: 0x0600292E RID: 10542 RVA: 0x000ACFAC File Offset: 0x000AB1AC
		[DataMember(IsRequired = false, EmitDefaultValue = true)]
		[XmlAttribute("IsTruncated")]
		public bool IsTruncated { get; set; }
	}
}
