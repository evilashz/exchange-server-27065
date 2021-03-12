using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000A9 RID: 169
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class StrongAuthenticationRuleValue
	{
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x0001EECB File Offset: 0x0001D0CB
		// (set) Token: 0x060005DD RID: 1501 RVA: 0x0001EED3 File Offset: 0x0001D0D3
		[XmlArrayItem("SelectionCondition", IsNullable = false)]
		public SelectionConditionValue[] SelectionConditions
		{
			get
			{
				return this.selectionConditionsField;
			}
			set
			{
				this.selectionConditionsField = value;
			}
		}

		// Token: 0x04000304 RID: 772
		private SelectionConditionValue[] selectionConditionsField;
	}
}
