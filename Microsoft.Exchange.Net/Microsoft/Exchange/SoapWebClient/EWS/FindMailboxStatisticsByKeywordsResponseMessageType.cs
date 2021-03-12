using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200028A RID: 650
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class FindMailboxStatisticsByKeywordsResponseMessageType : ResponseMessageType
	{
		// Token: 0x040010A1 RID: 4257
		public MailboxStatisticsSearchResultType MailboxStatisticsSearchResult;
	}
}
