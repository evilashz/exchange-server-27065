using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200028B RID: 651
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class MailboxStatisticsSearchResultType
	{
		// Token: 0x040010A2 RID: 4258
		public UserMailboxType UserMailbox;

		// Token: 0x040010A3 RID: 4259
		public KeywordStatisticsSearchResultType KeywordStatisticsSearchResult;
	}
}
