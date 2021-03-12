using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000A2 RID: 162
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://www.ccs.com/TestServices/")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class ServicePlanProvisioningStatus
	{
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x0001ED8C File Offset: 0x0001CF8C
		// (set) Token: 0x060005B8 RID: 1464 RVA: 0x0001ED94 File Offset: 0x0001CF94
		public AssignedPlanValue AssignedPlan
		{
			get
			{
				return this.assignedPlanField;
			}
			set
			{
				this.assignedPlanField = value;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x0001ED9D File Offset: 0x0001CF9D
		// (set) Token: 0x060005BA RID: 1466 RVA: 0x0001EDA5 File Offset: 0x0001CFA5
		public ProvisioningStatus1 ProvisioningStatus
		{
			get
			{
				return this.provisioningStatusField;
			}
			set
			{
				this.provisioningStatusField = value;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x0001EDAE File Offset: 0x0001CFAE
		// (set) Token: 0x060005BC RID: 1468 RVA: 0x0001EDB6 File Offset: 0x0001CFB6
		public XmlElement ErrorDetail
		{
			get
			{
				return this.errorDetailField;
			}
			set
			{
				this.errorDetailField = value;
			}
		}

		// Token: 0x040002EB RID: 747
		private AssignedPlanValue assignedPlanField;

		// Token: 0x040002EC RID: 748
		private ProvisioningStatus1 provisioningStatusField;

		// Token: 0x040002ED RID: 749
		private XmlElement errorDetailField;
	}
}
