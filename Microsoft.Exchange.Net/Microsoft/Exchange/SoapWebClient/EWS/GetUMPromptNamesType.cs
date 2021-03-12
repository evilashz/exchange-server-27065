using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000452 RID: 1106
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class GetUMPromptNamesType : BaseRequestType
	{
		// Token: 0x040016EE RID: 5870
		public string ConfigurationObject;

		// Token: 0x040016EF RID: 5871
		public int HoursElapsedSinceLastModified;
	}
}
