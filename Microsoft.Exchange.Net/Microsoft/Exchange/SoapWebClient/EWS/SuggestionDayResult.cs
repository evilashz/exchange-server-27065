using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000358 RID: 856
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class SuggestionDayResult
	{
		// Token: 0x04001425 RID: 5157
		public DateTime Date;

		// Token: 0x04001426 RID: 5158
		public SuggestionQuality DayQuality;

		// Token: 0x04001427 RID: 5159
		[XmlArrayItem(IsNullable = false)]
		public Suggestion[] SuggestionArray;
	}
}
