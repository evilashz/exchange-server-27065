using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200035F RID: 863
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class CalendarEvent
	{
		// Token: 0x04001440 RID: 5184
		public DateTime StartTime;

		// Token: 0x04001441 RID: 5185
		public DateTime EndTime;

		// Token: 0x04001442 RID: 5186
		public LegacyFreeBusyType BusyType;

		// Token: 0x04001443 RID: 5187
		public CalendarEventDetails CalendarEventDetails;
	}
}
