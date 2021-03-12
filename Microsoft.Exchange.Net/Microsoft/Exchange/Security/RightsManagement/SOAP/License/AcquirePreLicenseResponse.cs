using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.License
{
	// Token: 0x020009CC RID: 2508
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://microsoft.com/DRM/LicensingService")]
	[Serializable]
	public class AcquirePreLicenseResponse
	{
		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x060036BE RID: 14014 RVA: 0x0008C025 File Offset: 0x0008A225
		// (set) Token: 0x060036BF RID: 14015 RVA: 0x0008C02D File Offset: 0x0008A22D
		[XmlArrayItem("Certificate")]
		public XmlNode[] Licenses
		{
			get
			{
				return this.licensesField;
			}
			set
			{
				this.licensesField = value;
			}
		}

		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x060036C0 RID: 14016 RVA: 0x0008C036 File Offset: 0x0008A236
		// (set) Token: 0x060036C1 RID: 14017 RVA: 0x0008C03E File Offset: 0x0008A23E
		[XmlArrayItem("Certificate")]
		public XmlNode[] CertificateChain
		{
			get
			{
				return this.certificateChainField;
			}
			set
			{
				this.certificateChainField = value;
			}
		}

		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x060036C2 RID: 14018 RVA: 0x0008C047 File Offset: 0x0008A247
		// (set) Token: 0x060036C3 RID: 14019 RVA: 0x0008C04F File Offset: 0x0008A24F
		[XmlArrayItem("Certificate")]
		public XmlNode[] ReferenceCertificates
		{
			get
			{
				return this.referenceCertificatesField;
			}
			set
			{
				this.referenceCertificatesField = value;
			}
		}

		// Token: 0x04002ED1 RID: 11985
		private XmlNode[] licensesField;

		// Token: 0x04002ED2 RID: 11986
		private XmlNode[] certificateChainField;

		// Token: 0x04002ED3 RID: 11987
		private XmlNode[] referenceCertificatesField;
	}
}
