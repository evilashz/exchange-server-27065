using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x0200010A RID: 266
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[Serializable]
	public class AssignedLicenseValue
	{
		// Token: 0x060007EA RID: 2026 RVA: 0x000200A7 File Offset: 0x0001E2A7
		public AssignedLicenseValue()
		{
			this.disabledField = new string[0];
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x000200BB File Offset: 0x0001E2BB
		// (set) Token: 0x060007EC RID: 2028 RVA: 0x000200C3 File Offset: 0x0001E2C3
		[XmlAttribute]
		public string AccountId
		{
			get
			{
				return this.accountIdField;
			}
			set
			{
				this.accountIdField = value;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x000200CC File Offset: 0x0001E2CC
		// (set) Token: 0x060007EE RID: 2030 RVA: 0x000200D4 File Offset: 0x0001E2D4
		[XmlAttribute]
		public string SkuId
		{
			get
			{
				return this.skuIdField;
			}
			set
			{
				this.skuIdField = value;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x000200DD File Offset: 0x0001E2DD
		// (set) Token: 0x060007F0 RID: 2032 RVA: 0x000200E5 File Offset: 0x0001E2E5
		[XmlAttribute]
		public string[] Disabled
		{
			get
			{
				return this.disabledField;
			}
			set
			{
				this.disabledField = value;
			}
		}

		// Token: 0x04000412 RID: 1042
		private string accountIdField;

		// Token: 0x04000413 RID: 1043
		private string skuIdField;

		// Token: 0x04000414 RID: 1044
		private string[] disabledField;
	}
}
