using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200042D RID: 1069
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class PerformReminderActionType : BaseRequestType
	{
		// Token: 0x04001693 RID: 5779
		[XmlArrayItem("ReminderItemAction", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public ReminderItemActionType[] ReminderItemActions;
	}
}
