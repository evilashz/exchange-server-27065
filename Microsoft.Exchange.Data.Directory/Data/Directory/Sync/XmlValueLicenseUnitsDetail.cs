using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000942 RID: 2370
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class XmlValueLicenseUnitsDetail
	{
		// Token: 0x170027D4 RID: 10196
		// (get) Token: 0x06006FFF RID: 28671 RVA: 0x00177097 File Offset: 0x00175297
		// (set) Token: 0x06007000 RID: 28672 RVA: 0x0017709F File Offset: 0x0017529F
		[XmlElement(Order = 0)]
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

		// Token: 0x040048B0 RID: 18608
		private LicenseUnitsDetailValue licenseUnitsDetailField;
	}
}
