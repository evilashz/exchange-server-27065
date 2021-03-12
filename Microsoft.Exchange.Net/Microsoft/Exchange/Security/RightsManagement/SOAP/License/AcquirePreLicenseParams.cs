using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.License
{
	// Token: 0x020009CD RID: 2509
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://microsoft.com/DRM/LicensingService")]
	[Serializable]
	public class AcquirePreLicenseParams
	{
		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x060036C5 RID: 14021 RVA: 0x0008C060 File Offset: 0x0008A260
		// (set) Token: 0x060036C6 RID: 14022 RVA: 0x0008C068 File Offset: 0x0008A268
		public string[] LicenseeIdentities
		{
			get
			{
				return this.licenseeIdentitiesField;
			}
			set
			{
				this.licenseeIdentitiesField = value;
			}
		}

		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x060036C7 RID: 14023 RVA: 0x0008C071 File Offset: 0x0008A271
		// (set) Token: 0x060036C8 RID: 14024 RVA: 0x0008C079 File Offset: 0x0008A279
		[XmlArrayItem("Certificate")]
		public XmlNode[] IssuanceLicense
		{
			get
			{
				return this.issuanceLicenseField;
			}
			set
			{
				this.issuanceLicenseField = value;
			}
		}

		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x060036C9 RID: 14025 RVA: 0x0008C082 File Offset: 0x0008A282
		// (set) Token: 0x060036CA RID: 14026 RVA: 0x0008C08A File Offset: 0x0008A28A
		public XmlNode ApplicationData
		{
			get
			{
				return this.applicationDataField;
			}
			set
			{
				this.applicationDataField = value;
			}
		}

		// Token: 0x04002ED4 RID: 11988
		private string[] licenseeIdentitiesField;

		// Token: 0x04002ED5 RID: 11989
		private XmlNode[] issuanceLicenseField;

		// Token: 0x04002ED6 RID: 11990
		private XmlNode applicationDataField;
	}
}
