using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000423 RID: 1059
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[Serializable]
	public class GetSearchableMailboxesType : BaseRequestType
	{
		// Token: 0x0400164C RID: 5708
		public string SearchFilter;

		// Token: 0x0400164D RID: 5709
		public bool ExpandGroupMembership;

		// Token: 0x0400164E RID: 5710
		[XmlIgnore]
		public bool ExpandGroupMembershipSpecified;
	}
}
