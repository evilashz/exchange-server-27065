using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001C6 RID: 454
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class ProtectionRuleType
	{
		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x0600137A RID: 4986 RVA: 0x00025439 File Offset: 0x00023639
		// (set) Token: 0x0600137B RID: 4987 RVA: 0x00025441 File Offset: 0x00023641
		public ProtectionRuleConditionType Condition
		{
			get
			{
				return this.conditionField;
			}
			set
			{
				this.conditionField = value;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x0600137C RID: 4988 RVA: 0x0002544A File Offset: 0x0002364A
		// (set) Token: 0x0600137D RID: 4989 RVA: 0x00025452 File Offset: 0x00023652
		public ProtectionRuleActionType Action
		{
			get
			{
				return this.actionField;
			}
			set
			{
				this.actionField = value;
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x0600137E RID: 4990 RVA: 0x0002545B File Offset: 0x0002365B
		// (set) Token: 0x0600137F RID: 4991 RVA: 0x00025463 File Offset: 0x00023663
		[XmlAttribute]
		public string Name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001380 RID: 4992 RVA: 0x0002546C File Offset: 0x0002366C
		// (set) Token: 0x06001381 RID: 4993 RVA: 0x00025474 File Offset: 0x00023674
		[XmlAttribute]
		public bool UserOverridable
		{
			get
			{
				return this.userOverridableField;
			}
			set
			{
				this.userOverridableField = value;
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001382 RID: 4994 RVA: 0x0002547D File Offset: 0x0002367D
		// (set) Token: 0x06001383 RID: 4995 RVA: 0x00025485 File Offset: 0x00023685
		[XmlAttribute]
		public int Priority
		{
			get
			{
				return this.priorityField;
			}
			set
			{
				this.priorityField = value;
			}
		}

		// Token: 0x04000D78 RID: 3448
		private ProtectionRuleConditionType conditionField;

		// Token: 0x04000D79 RID: 3449
		private ProtectionRuleActionType actionField;

		// Token: 0x04000D7A RID: 3450
		private string nameField;

		// Token: 0x04000D7B RID: 3451
		private bool userOverridableField;

		// Token: 0x04000D7C RID: 3452
		private int priorityField;
	}
}
