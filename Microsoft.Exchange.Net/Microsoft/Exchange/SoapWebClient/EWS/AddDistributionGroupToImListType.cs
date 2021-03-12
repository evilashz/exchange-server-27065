using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000407 RID: 1031
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class AddDistributionGroupToImListType : BaseRequestType
	{
		// Token: 0x040015E7 RID: 5607
		public string SmtpAddress;

		// Token: 0x040015E8 RID: 5608
		public string DisplayName;
	}
}
