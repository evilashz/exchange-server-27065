using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001A3 RID: 419
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class KeywordStatisticsSearchResultType
	{
		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x060011B2 RID: 4530 RVA: 0x00024525 File Offset: 0x00022725
		// (set) Token: 0x060011B3 RID: 4531 RVA: 0x0002452D File Offset: 0x0002272D
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

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060011B4 RID: 4532 RVA: 0x00024536 File Offset: 0x00022736
		// (set) Token: 0x060011B5 RID: 4533 RVA: 0x0002453E File Offset: 0x0002273E
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

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x00024547 File Offset: 0x00022747
		// (set) Token: 0x060011B7 RID: 4535 RVA: 0x0002454F File Offset: 0x0002274F
		public long Size
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

		// Token: 0x04000C24 RID: 3108
		private string keywordField;

		// Token: 0x04000C25 RID: 3109
		private int itemHitsField;

		// Token: 0x04000C26 RID: 3110
		private long sizeField;
	}
}
