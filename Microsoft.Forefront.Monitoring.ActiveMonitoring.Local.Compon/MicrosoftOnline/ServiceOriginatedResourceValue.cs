using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000D9 RID: 217
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[Serializable]
	public class ServiceOriginatedResourceValue
	{
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x0001F624 File Offset: 0x0001D824
		// (set) Token: 0x060006BC RID: 1724 RVA: 0x0001F62C File Offset: 0x0001D82C
		[XmlAttribute]
		public bool LicenseReconciliationNeeded
		{
			get
			{
				return this.licenseReconciliationNeededField;
			}
			set
			{
				this.licenseReconciliationNeededField = value;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x0001F635 File Offset: 0x0001D835
		// (set) Token: 0x060006BE RID: 1726 RVA: 0x0001F63D File Offset: 0x0001D83D
		[XmlIgnore]
		public bool LicenseReconciliationNeededSpecified
		{
			get
			{
				return this.licenseReconciliationNeededFieldSpecified;
			}
			set
			{
				this.licenseReconciliationNeededFieldSpecified = value;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x0001F646 File Offset: 0x0001D846
		// (set) Token: 0x060006C0 RID: 1728 RVA: 0x0001F64E File Offset: 0x0001D84E
		[XmlAttribute]
		public string ServiceInstance
		{
			get
			{
				return this.serviceInstanceField;
			}
			set
			{
				this.serviceInstanceField = value;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x0001F657 File Offset: 0x0001D857
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x0001F65F File Offset: 0x0001D85F
		[XmlAttribute]
		public string ServicePlanId
		{
			get
			{
				return this.servicePlanIdField;
			}
			set
			{
				this.servicePlanIdField = value;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x0001F668 File Offset: 0x0001D868
		// (set) Token: 0x060006C4 RID: 1732 RVA: 0x0001F670 File Offset: 0x0001D870
		[XmlAttribute]
		public string Capability
		{
			get
			{
				return this.capabilityField;
			}
			set
			{
				this.capabilityField = value;
			}
		}

		// Token: 0x04000370 RID: 880
		private bool licenseReconciliationNeededField;

		// Token: 0x04000371 RID: 881
		private bool licenseReconciliationNeededFieldSpecified;

		// Token: 0x04000372 RID: 882
		private string serviceInstanceField;

		// Token: 0x04000373 RID: 883
		private string servicePlanIdField;

		// Token: 0x04000374 RID: 884
		private string capabilityField;
	}
}
