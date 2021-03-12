using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000445 RID: 1093
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetUserOofSettingsRequest : BaseRequestType
	{
		// Token: 0x040016CE RID: 5838
		[XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public EmailAddress Mailbox;
	}
}
