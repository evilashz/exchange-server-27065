using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000422 RID: 1058
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class SearchMailboxesType : BaseRequestType
	{
		// Token: 0x04001640 RID: 5696
		[XmlArrayItem("MailboxQuery", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public MailboxQueryType[] SearchQueries;

		// Token: 0x04001641 RID: 5697
		public SearchResultType ResultType;

		// Token: 0x04001642 RID: 5698
		public PreviewItemResponseShapeType PreviewItemResponseShape;

		// Token: 0x04001643 RID: 5699
		public FieldOrderType SortBy;

		// Token: 0x04001644 RID: 5700
		public string Language;

		// Token: 0x04001645 RID: 5701
		public bool Deduplication;

		// Token: 0x04001646 RID: 5702
		[XmlIgnore]
		public bool DeduplicationSpecified;

		// Token: 0x04001647 RID: 5703
		public int PageSize;

		// Token: 0x04001648 RID: 5704
		[XmlIgnore]
		public bool PageSizeSpecified;

		// Token: 0x04001649 RID: 5705
		public string PageItemReference;

		// Token: 0x0400164A RID: 5706
		public SearchPageDirectionType PageDirection;

		// Token: 0x0400164B RID: 5707
		[XmlIgnore]
		public bool PageDirectionSpecified;
	}
}
