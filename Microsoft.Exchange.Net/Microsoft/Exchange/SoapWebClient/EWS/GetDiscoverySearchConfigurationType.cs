using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000421 RID: 1057
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class GetDiscoverySearchConfigurationType : BaseRequestType
	{
		// Token: 0x0400163B RID: 5691
		public string SearchId;

		// Token: 0x0400163C RID: 5692
		public bool ExpandGroupMembership;

		// Token: 0x0400163D RID: 5693
		[XmlIgnore]
		public bool ExpandGroupMembershipSpecified;

		// Token: 0x0400163E RID: 5694
		public bool InPlaceHoldConfigurationOnly;

		// Token: 0x0400163F RID: 5695
		[XmlIgnore]
		public bool InPlaceHoldConfigurationOnlySpecified;
	}
}
