using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200041B RID: 1051
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetNonIndexableItemDetailsType : BaseRequestType
	{
		// Token: 0x0400161D RID: 5661
		[XmlArrayItem("LegacyDN", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] Mailboxes;

		// Token: 0x0400161E RID: 5662
		public int PageSize;

		// Token: 0x0400161F RID: 5663
		[XmlIgnore]
		public bool PageSizeSpecified;

		// Token: 0x04001620 RID: 5664
		public string PageItemReference;

		// Token: 0x04001621 RID: 5665
		public SearchPageDirectionType PageDirection;

		// Token: 0x04001622 RID: 5666
		[XmlIgnore]
		public bool PageDirectionSpecified;

		// Token: 0x04001623 RID: 5667
		public bool SearchArchiveOnly;

		// Token: 0x04001624 RID: 5668
		[XmlIgnore]
		public bool SearchArchiveOnlySpecified;
	}
}
