using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003AB RID: 939
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ReminderItemActionType
	{
		// Token: 0x040014BD RID: 5309
		public ReminderActionType ActionType;

		// Token: 0x040014BE RID: 5310
		public ItemIdType ItemId;

		// Token: 0x040014BF RID: 5311
		public string NewReminderTime;
	}
}
