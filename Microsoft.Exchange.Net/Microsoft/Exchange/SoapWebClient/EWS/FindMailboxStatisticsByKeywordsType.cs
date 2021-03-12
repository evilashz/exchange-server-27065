using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000424 RID: 1060
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class FindMailboxStatisticsByKeywordsType : BaseRequestType
	{
		// Token: 0x0400164F RID: 5711
		[XmlArrayItem("UserMailbox", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public UserMailboxType[] Mailboxes;

		// Token: 0x04001650 RID: 5712
		[XmlArrayItem("String", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] Keywords;

		// Token: 0x04001651 RID: 5713
		public string Language;

		// Token: 0x04001652 RID: 5714
		[XmlArrayItem("SmtpAddress", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] Senders;

		// Token: 0x04001653 RID: 5715
		[XmlArrayItem("SmtpAddress", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] Recipients;

		// Token: 0x04001654 RID: 5716
		public DateTime FromDate;

		// Token: 0x04001655 RID: 5717
		[XmlIgnore]
		public bool FromDateSpecified;

		// Token: 0x04001656 RID: 5718
		public DateTime ToDate;

		// Token: 0x04001657 RID: 5719
		[XmlIgnore]
		public bool ToDateSpecified;

		// Token: 0x04001658 RID: 5720
		[XmlArrayItem("SearchItemKind", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public SearchItemKindType[] MessageTypes;

		// Token: 0x04001659 RID: 5721
		public bool SearchDumpster;

		// Token: 0x0400165A RID: 5722
		[XmlIgnore]
		public bool SearchDumpsterSpecified;

		// Token: 0x0400165B RID: 5723
		public bool IncludePersonalArchive;

		// Token: 0x0400165C RID: 5724
		[XmlIgnore]
		public bool IncludePersonalArchiveSpecified;

		// Token: 0x0400165D RID: 5725
		public bool IncludeUnsearchableItems;

		// Token: 0x0400165E RID: 5726
		[XmlIgnore]
		public bool IncludeUnsearchableItemsSpecified;
	}
}
