using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000A8 RID: 168
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[Serializable]
	public class SelectionConditionValue
	{
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x0001EE90 File Offset: 0x0001D090
		// (set) Token: 0x060005D6 RID: 1494 RVA: 0x0001EE98 File Offset: 0x0001D098
		[XmlArrayItem("Value", IsNullable = false)]
		public string[] Values
		{
			get
			{
				return this.valuesField;
			}
			set
			{
				this.valuesField = value;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x0001EEA1 File Offset: 0x0001D0A1
		// (set) Token: 0x060005D8 RID: 1496 RVA: 0x0001EEA9 File Offset: 0x0001D0A9
		[XmlAttribute]
		public int Claim
		{
			get
			{
				return this.claimField;
			}
			set
			{
				this.claimField = value;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0001EEB2 File Offset: 0x0001D0B2
		// (set) Token: 0x060005DA RID: 1498 RVA: 0x0001EEBA File Offset: 0x0001D0BA
		[XmlAttribute]
		public int Operator
		{
			get
			{
				return this.operatorField;
			}
			set
			{
				this.operatorField = value;
			}
		}

		// Token: 0x04000301 RID: 769
		private string[] valuesField;

		// Token: 0x04000302 RID: 770
		private int claimField;

		// Token: 0x04000303 RID: 771
		private int operatorField;
	}
}
