using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.License
{
	// Token: 0x020009CE RID: 2510
	[XmlType(Namespace = "http://microsoft.com/DRM/LicensingService")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class AcquireLicenseResponse
	{
		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x060036CC RID: 14028 RVA: 0x0008C09B File Offset: 0x0008A29B
		// (set) Token: 0x060036CD RID: 14029 RVA: 0x0008C0A3 File Offset: 0x0008A2A3
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

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x060036CE RID: 14030 RVA: 0x0008C0AC File Offset: 0x0008A2AC
		// (set) Token: 0x060036CF RID: 14031 RVA: 0x0008C0B4 File Offset: 0x0008A2B4
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

		// Token: 0x04002ED7 RID: 11991
		private XmlNode[] certificateChainField;

		// Token: 0x04002ED8 RID: 11992
		private XmlNode[] referenceCertificatesField;
	}
}
