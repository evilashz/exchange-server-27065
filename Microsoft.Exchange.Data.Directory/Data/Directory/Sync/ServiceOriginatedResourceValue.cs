using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000939 RID: 2361
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[Serializable]
	public class ServiceOriginatedResourceValue
	{
		// Token: 0x170027C0 RID: 10176
		// (get) Token: 0x06006FD0 RID: 28624 RVA: 0x00176F04 File Offset: 0x00175104
		// (set) Token: 0x06006FD1 RID: 28625 RVA: 0x00176F0C File Offset: 0x0017510C
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

		// Token: 0x170027C1 RID: 10177
		// (get) Token: 0x06006FD2 RID: 28626 RVA: 0x00176F15 File Offset: 0x00175115
		// (set) Token: 0x06006FD3 RID: 28627 RVA: 0x00176F1D File Offset: 0x0017511D
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

		// Token: 0x170027C2 RID: 10178
		// (get) Token: 0x06006FD4 RID: 28628 RVA: 0x00176F26 File Offset: 0x00175126
		// (set) Token: 0x06006FD5 RID: 28629 RVA: 0x00176F2E File Offset: 0x0017512E
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

		// Token: 0x170027C3 RID: 10179
		// (get) Token: 0x06006FD6 RID: 28630 RVA: 0x00176F37 File Offset: 0x00175137
		// (set) Token: 0x06006FD7 RID: 28631 RVA: 0x00176F3F File Offset: 0x0017513F
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

		// Token: 0x170027C4 RID: 10180
		// (get) Token: 0x06006FD8 RID: 28632 RVA: 0x00176F48 File Offset: 0x00175148
		// (set) Token: 0x06006FD9 RID: 28633 RVA: 0x00176F50 File Offset: 0x00175150
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

		// Token: 0x04004892 RID: 18578
		private bool licenseReconciliationNeededField;

		// Token: 0x04004893 RID: 18579
		private bool licenseReconciliationNeededFieldSpecified;

		// Token: 0x04004894 RID: 18580
		private string serviceInstanceField;

		// Token: 0x04004895 RID: 18581
		private string servicePlanIdField;

		// Token: 0x04004896 RID: 18582
		private string capabilityField;
	}
}
