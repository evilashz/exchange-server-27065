using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002B5 RID: 693
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetRemindersResponseMessageType : ResponseMessageType
	{
		// Token: 0x040011EC RID: 4588
		[XmlArrayItem("Reminder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public ReminderType[] Reminders;
	}
}
