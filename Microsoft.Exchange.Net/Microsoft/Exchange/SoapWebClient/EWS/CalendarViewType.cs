using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003E6 RID: 998
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class CalendarViewType : BasePagingType
	{
		// Token: 0x04001597 RID: 5527
		[XmlAttribute]
		public DateTime StartDate;

		// Token: 0x04001598 RID: 5528
		[XmlAttribute]
		public DateTime EndDate;
	}
}
