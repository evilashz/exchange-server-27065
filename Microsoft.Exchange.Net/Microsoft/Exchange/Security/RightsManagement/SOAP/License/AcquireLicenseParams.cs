using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.License
{
	// Token: 0x020009CF RID: 2511
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://microsoft.com/DRM/LicensingService")]
	[Serializable]
	public class AcquireLicenseParams
	{
		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x060036D1 RID: 14033 RVA: 0x0008C0C5 File Offset: 0x0008A2C5
		// (set) Token: 0x060036D2 RID: 14034 RVA: 0x0008C0CD File Offset: 0x0008A2CD
		[XmlArrayItem("Certificate")]
		public XmlNode[] LicenseeCerts
		{
			get
			{
				return this.licenseeCertsField;
			}
			set
			{
				this.licenseeCertsField = value;
			}
		}

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x060036D3 RID: 14035 RVA: 0x0008C0D6 File Offset: 0x0008A2D6
		// (set) Token: 0x060036D4 RID: 14036 RVA: 0x0008C0DE File Offset: 0x0008A2DE
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

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x060036D5 RID: 14037 RVA: 0x0008C0E7 File Offset: 0x0008A2E7
		// (set) Token: 0x060036D6 RID: 14038 RVA: 0x0008C0EF File Offset: 0x0008A2EF
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

		// Token: 0x04002ED9 RID: 11993
		private XmlNode[] licenseeCertsField;

		// Token: 0x04002EDA RID: 11994
		private XmlNode[] issuanceLicenseField;

		// Token: 0x04002EDB RID: 11995
		private XmlNode applicationDataField;
	}
}
