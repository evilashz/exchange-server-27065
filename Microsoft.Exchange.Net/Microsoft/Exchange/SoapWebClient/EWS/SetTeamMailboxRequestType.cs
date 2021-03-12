using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000433 RID: 1075
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class SetTeamMailboxRequestType : BaseRequestType
	{
		// Token: 0x040016A2 RID: 5794
		public EmailAddressType EmailAddress;

		// Token: 0x040016A3 RID: 5795
		public string SharePointSiteUrl;

		// Token: 0x040016A4 RID: 5796
		public TeamMailboxLifecycleStateType State;
	}
}
