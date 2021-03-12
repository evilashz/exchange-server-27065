using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessRequest
{
	// Token: 0x02000167 RID: 359
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[XmlType(AnonymousType = true, Namespace = "DeltaSyncV2:")]
	[XmlRoot(Namespace = "DeltaSyncV2:", IsNullable = false)]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[Serializable]
	public class Clause
	{
		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x0001D68E File Offset: 0x0001B88E
		// (set) Token: 0x06000A88 RID: 2696 RVA: 0x0001D696 File Offset: 0x0001B896
		public string Property
		{
			get
			{
				return this.propertyField;
			}
			set
			{
				this.propertyField = value;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x0001D69F File Offset: 0x0001B89F
		// (set) Token: 0x06000A8A RID: 2698 RVA: 0x0001D6A7 File Offset: 0x0001B8A7
		public ActionType Action
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

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x0001D6B0 File Offset: 0x0001B8B0
		// (set) Token: 0x06000A8C RID: 2700 RVA: 0x0001D6B8 File Offset: 0x0001B8B8
		public string Value
		{
			get
			{
				return this.valueField;
			}
			set
			{
				this.valueField = value;
			}
		}

		// Token: 0x040005D6 RID: 1494
		private string propertyField;

		// Token: 0x040005D7 RID: 1495
		private ActionType actionField;

		// Token: 0x040005D8 RID: 1496
		private string valueField;
	}
}
