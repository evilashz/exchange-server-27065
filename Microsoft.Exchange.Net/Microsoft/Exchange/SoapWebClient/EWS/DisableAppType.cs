using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200040D RID: 1037
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class DisableAppType : BaseRequestType
	{
		// Token: 0x040015F5 RID: 5621
		public string ID;

		// Token: 0x040015F6 RID: 5622
		public DisableReasonType DisableReason;
	}
}
