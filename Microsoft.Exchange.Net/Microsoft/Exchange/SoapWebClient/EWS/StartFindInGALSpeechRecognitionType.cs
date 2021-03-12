using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003FC RID: 1020
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class StartFindInGALSpeechRecognitionType : BaseRequestType
	{
		// Token: 0x040015CD RID: 5581
		public string Culture;

		// Token: 0x040015CE RID: 5582
		public string TimeZone;

		// Token: 0x040015CF RID: 5583
		public string UserObjectGuid;

		// Token: 0x040015D0 RID: 5584
		public string TenantGuid;
	}
}
