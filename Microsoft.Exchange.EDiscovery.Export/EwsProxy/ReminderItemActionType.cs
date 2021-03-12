using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002CA RID: 714
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ReminderItemActionType
	{
		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06001837 RID: 6199 RVA: 0x00027C0E File Offset: 0x00025E0E
		// (set) Token: 0x06001838 RID: 6200 RVA: 0x00027C16 File Offset: 0x00025E16
		public ReminderActionType ActionType
		{
			get
			{
				return this.actionTypeField;
			}
			set
			{
				this.actionTypeField = value;
			}
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06001839 RID: 6201 RVA: 0x00027C1F File Offset: 0x00025E1F
		// (set) Token: 0x0600183A RID: 6202 RVA: 0x00027C27 File Offset: 0x00025E27
		public ItemIdType ItemId
		{
			get
			{
				return this.itemIdField;
			}
			set
			{
				this.itemIdField = value;
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x0600183B RID: 6203 RVA: 0x00027C30 File Offset: 0x00025E30
		// (set) Token: 0x0600183C RID: 6204 RVA: 0x00027C38 File Offset: 0x00025E38
		public string NewReminderTime
		{
			get
			{
				return this.newReminderTimeField;
			}
			set
			{
				this.newReminderTimeField = value;
			}
		}

		// Token: 0x0400106B RID: 4203
		private ReminderActionType actionTypeField;

		// Token: 0x0400106C RID: 4204
		private ItemIdType itemIdField;

		// Token: 0x0400106D RID: 4205
		private string newReminderTimeField;
	}
}
