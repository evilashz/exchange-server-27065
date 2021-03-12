using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200034C RID: 844
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class PerformReminderActionType : BaseRequestType
	{
		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06001B5C RID: 7004 RVA: 0x00029694 File Offset: 0x00027894
		// (set) Token: 0x06001B5D RID: 7005 RVA: 0x0002969C File Offset: 0x0002789C
		[XmlArrayItem("ReminderItemAction", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public ReminderItemActionType[] ReminderItemActions
		{
			get
			{
				return this.reminderItemActionsField;
			}
			set
			{
				this.reminderItemActionsField = value;
			}
		}

		// Token: 0x04001241 RID: 4673
		private ReminderItemActionType[] reminderItemActionsField;
	}
}
