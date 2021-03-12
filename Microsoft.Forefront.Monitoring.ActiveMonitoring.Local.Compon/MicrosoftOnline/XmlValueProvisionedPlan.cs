using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000E9 RID: 233
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class XmlValueProvisionedPlan
	{
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x0001FB75 File Offset: 0x0001DD75
		// (set) Token: 0x06000751 RID: 1873 RVA: 0x0001FB7D File Offset: 0x0001DD7D
		public ProvisionedPlanValue Plan
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

		// Token: 0x040003BF RID: 959
		private ProvisionedPlanValue planField;
	}
}
