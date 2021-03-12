using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000360 RID: 864
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class FreeBusyView
	{
		// Token: 0x04001444 RID: 5188
		public FreeBusyViewType FreeBusyViewType;

		// Token: 0x04001445 RID: 5189
		public string MergedFreeBusy;

		// Token: 0x04001446 RID: 5190
		[XmlArrayItem(IsNullable = false)]
		public CalendarEvent[] CalendarEventArray;

		// Token: 0x04001447 RID: 5191
		public WorkingHours WorkingHours;
	}
}
