using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000191 RID: 401
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[Serializable]
	public class GetClientIntentResponseMessageType : ResponseMessageType
	{
		// Token: 0x0400099B RID: 2459
		public ClientIntentType ClientIntent;
	}
}
