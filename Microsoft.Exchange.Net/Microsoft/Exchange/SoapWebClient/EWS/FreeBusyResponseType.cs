using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000362 RID: 866
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class FreeBusyResponseType
	{
		// Token: 0x0400144F RID: 5199
		public ResponseMessageType ResponseMessage;

		// Token: 0x04001450 RID: 5200
		public FreeBusyView FreeBusyView;
	}
}
