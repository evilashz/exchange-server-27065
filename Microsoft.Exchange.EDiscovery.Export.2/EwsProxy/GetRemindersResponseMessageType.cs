using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001D4 RID: 468
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[Serializable]
	public class GetRemindersResponseMessageType : ResponseMessageType
	{
		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x000255E5 File Offset: 0x000237E5
		// (set) Token: 0x060013AE RID: 5038 RVA: 0x000255ED File Offset: 0x000237ED
		[XmlArrayItem("Reminder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public ReminderType[] Reminders
		{
			get
			{
				return this.remindersField;
			}
			set
			{
				this.remindersField = value;
			}
		}

		// Token: 0x04000D9A RID: 3482
		private ReminderType[] remindersField;
	}
}
