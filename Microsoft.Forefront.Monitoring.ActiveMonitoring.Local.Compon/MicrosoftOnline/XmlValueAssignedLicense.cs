using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x0200010B RID: 267
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class XmlValueAssignedLicense
	{
		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x000200EE File Offset: 0x0001E2EE
		// (set) Token: 0x060007F2 RID: 2034 RVA: 0x000200F6 File Offset: 0x0001E2F6
		public AssignedLicenseValue License
		{
			get
			{
				return this.licenseField;
			}
			set
			{
				this.licenseField = value;
			}
		}

		// Token: 0x04000415 RID: 1045
		private AssignedLicenseValue licenseField;
	}
}
