using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007E3 RID: 2019
	[DataContract(Name = "KeywordStatisticsSearchResult", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "KeywordStatisticsSearchResultType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class KeywordStatisticsSearchResult
	{
		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x06003B51 RID: 15185 RVA: 0x000CFF90 File Offset: 0x000CE190
		// (set) Token: 0x06003B52 RID: 15186 RVA: 0x000CFF98 File Offset: 0x000CE198
		[DataMember(Name = "Keyword", IsRequired = true)]
		[XmlElement("Keyword")]
		public string Keyword
		{
			get
			{
				return this.keywordField;
			}
			set
			{
				this.keywordField = value;
			}
		}

		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x06003B53 RID: 15187 RVA: 0x000CFFA1 File Offset: 0x000CE1A1
		// (set) Token: 0x06003B54 RID: 15188 RVA: 0x000CFFA9 File Offset: 0x000CE1A9
		[DataMember(Name = "ItemHits", IsRequired = true)]
		[XmlElement("ItemHits")]
		public int ItemHits
		{
			get
			{
				return this.itemHitsField;
			}
			set
			{
				this.itemHitsField = value;
			}
		}

		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x06003B55 RID: 15189 RVA: 0x000CFFB2 File Offset: 0x000CE1B2
		// (set) Token: 0x06003B56 RID: 15190 RVA: 0x000CFFBA File Offset: 0x000CE1BA
		[DataMember(Name = "Size", IsRequired = true)]
		[XmlElement("Size")]
		public ulong Size
		{
			get
			{
				return this.sizeField;
			}
			set
			{
				this.sizeField = value;
			}
		}

		// Token: 0x040020B5 RID: 8373
		private string keywordField = string.Empty;

		// Token: 0x040020B6 RID: 8374
		private int itemHitsField;

		// Token: 0x040020B7 RID: 8375
		private ulong sizeField;
	}
}
