using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200042E RID: 1070
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetRemindersType : BaseRequestType
	{
		// Token: 0x04001694 RID: 5780
		public DateTime BeginTime;

		// Token: 0x04001695 RID: 5781
		[XmlIgnore]
		public bool BeginTimeSpecified;

		// Token: 0x04001696 RID: 5782
		public DateTime EndTime;

		// Token: 0x04001697 RID: 5783
		[XmlIgnore]
		public bool EndTimeSpecified;

		// Token: 0x04001698 RID: 5784
		public int MaxItems;

		// Token: 0x04001699 RID: 5785
		[XmlIgnore]
		public bool MaxItemsSpecified;

		// Token: 0x0400169A RID: 5786
		public GetRemindersTypeReminderType ReminderType;

		// Token: 0x0400169B RID: 5787
		[XmlIgnore]
		public bool ReminderTypeSpecified;
	}
}
