using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200035E RID: 862
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class CalendarEventDetails
	{
		// Token: 0x04001438 RID: 5176
		public string ID;

		// Token: 0x04001439 RID: 5177
		public string Subject;

		// Token: 0x0400143A RID: 5178
		public string Location;

		// Token: 0x0400143B RID: 5179
		public bool IsMeeting;

		// Token: 0x0400143C RID: 5180
		public bool IsRecurring;

		// Token: 0x0400143D RID: 5181
		public bool IsException;

		// Token: 0x0400143E RID: 5182
		public bool IsReminderSet;

		// Token: 0x0400143F RID: 5183
		public bool IsPrivate;
	}
}
