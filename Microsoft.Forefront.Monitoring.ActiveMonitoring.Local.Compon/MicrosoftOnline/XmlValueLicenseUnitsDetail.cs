using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000F0 RID: 240
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class XmlValueLicenseUnitsDetail
	{
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x0001FCC6 File Offset: 0x0001DEC6
		// (set) Token: 0x06000779 RID: 1913 RVA: 0x0001FCCE File Offset: 0x0001DECE
		public LicenseUnitsDetailValue LicenseUnitsDetail
		{
			get
			{
				return this.licenseUnitsDetailField;
			}
			set
			{
				this.licenseUnitsDetailField = value;
			}
		}

		// Token: 0x040003D3 RID: 979
		private LicenseUnitsDetailValue licenseUnitsDetailField;
	}
}
