using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002A0 RID: 672
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[Serializable]
	public class FindMessageTrackingReportResponseMessageType : ResponseMessageType
	{
		// Token: 0x040011AA RID: 4522
		[XmlArrayItem("String", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] Diagnostics;

		// Token: 0x040011AB RID: 4523
		[XmlArrayItem("MessageTrackingSearchResult", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public FindMessageTrackingSearchResultType[] MessageTrackingSearchResults;

		// Token: 0x040011AC RID: 4524
		public string ExecutedSearchScope;

		// Token: 0x040011AD RID: 4525
		[XmlArrayItem(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public ArrayOfTrackingPropertiesType[] Errors;

		// Token: 0x040011AE RID: 4526
		[XmlArrayItem(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public TrackingPropertyType[] Properties;
	}
}
