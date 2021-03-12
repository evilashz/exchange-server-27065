using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000284 RID: 644
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class KeywordStatisticsSearchResultType
	{
		// Token: 0x04001076 RID: 4214
		public string Keyword;

		// Token: 0x04001077 RID: 4215
		public int ItemHits;

		// Token: 0x04001078 RID: 4216
		public long Size;
	}
}
