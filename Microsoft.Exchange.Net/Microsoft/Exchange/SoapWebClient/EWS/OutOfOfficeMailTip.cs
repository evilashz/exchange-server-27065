using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200032B RID: 811
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class OutOfOfficeMailTip
	{
		// Token: 0x04001376 RID: 4982
		public ReplyBody ReplyBody;

		// Token: 0x04001377 RID: 4983
		public Duration Duration;
	}
}
