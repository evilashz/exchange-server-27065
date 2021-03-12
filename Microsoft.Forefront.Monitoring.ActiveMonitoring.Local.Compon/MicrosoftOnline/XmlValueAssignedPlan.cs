using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000109 RID: 265
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class XmlValueAssignedPlan
	{
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x0002008E File Offset: 0x0001E28E
		// (set) Token: 0x060007E8 RID: 2024 RVA: 0x00020096 File Offset: 0x0001E296
		public AssignedPlanValue Plan
		{
			get
			{
				return this.planField;
			}
			set
			{
				this.planField = value;
			}
		}

		// Token: 0x04000411 RID: 1041
		private AssignedPlanValue planField;
	}
}
