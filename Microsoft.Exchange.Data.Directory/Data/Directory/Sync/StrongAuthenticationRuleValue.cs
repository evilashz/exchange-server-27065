using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000916 RID: 2326
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class StrongAuthenticationRuleValue
	{
		// Token: 0x17002787 RID: 10119
		// (get) Token: 0x06006F41 RID: 28481 RVA: 0x00176A53 File Offset: 0x00174C53
		// (set) Token: 0x06006F42 RID: 28482 RVA: 0x00176A5B File Offset: 0x00174C5B
		[XmlArray(Order = 0)]
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

		// Token: 0x04004833 RID: 18483
		private SelectionConditionValue[] selectionConditionsField;
	}
}
