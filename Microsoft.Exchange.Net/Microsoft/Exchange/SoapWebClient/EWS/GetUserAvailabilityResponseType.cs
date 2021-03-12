using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000363 RID: 867
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetUserAvailabilityResponseType
	{
		// Token: 0x04001451 RID: 5201
		[XmlArrayItem("FreeBusyResponse", IsNullable = false)]
		public FreeBusyResponseType[] FreeBusyResponseArray;

		// Token: 0x04001452 RID: 5202
		public SuggestionsResponseType SuggestionsResponse;
	}
}
