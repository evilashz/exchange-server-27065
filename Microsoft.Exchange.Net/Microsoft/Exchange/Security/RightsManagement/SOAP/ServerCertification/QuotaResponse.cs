using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.ServerCertification
{
	// Token: 0x020009E0 RID: 2528
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://microsoft.com/DRM/CertificationService")]
	[Serializable]
	public class QuotaResponse
	{
		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x0600372A RID: 14122 RVA: 0x0008C751 File Offset: 0x0008A951
		// (set) Token: 0x0600372B RID: 14123 RVA: 0x0008C759 File Offset: 0x0008A959
		public bool Verified
		{
			get
			{
				return this.verifiedField;
			}
			set
			{
				this.verifiedField = value;
			}
		}

		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x0600372C RID: 14124 RVA: 0x0008C762 File Offset: 0x0008A962
		// (set) Token: 0x0600372D RID: 14125 RVA: 0x0008C76A File Offset: 0x0008A96A
		public int CurrentConsumption
		{
			get
			{
				return this.currentConsumptionField;
			}
			set
			{
				this.currentConsumptionField = value;
			}
		}

		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x0600372E RID: 14126 RVA: 0x0008C773 File Offset: 0x0008A973
		// (set) Token: 0x0600372F RID: 14127 RVA: 0x0008C77B File Offset: 0x0008A97B
		public int Maximum
		{
			get
			{
				return this.maximumField;
			}
			set
			{
				this.maximumField = value;
			}
		}

		// Token: 0x04002EF2 RID: 12018
		private bool verifiedField;

		// Token: 0x04002EF3 RID: 12019
		private int currentConsumptionField;

		// Token: 0x04002EF4 RID: 12020
		private int maximumField;
	}
}
