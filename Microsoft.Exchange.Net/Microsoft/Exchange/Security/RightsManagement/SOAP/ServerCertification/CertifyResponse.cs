using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.ServerCertification
{
	// Token: 0x020009E1 RID: 2529
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://microsoft.com/DRM/CertificationService")]
	[Serializable]
	public class CertifyResponse
	{
		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x06003731 RID: 14129 RVA: 0x0008C78C File Offset: 0x0008A98C
		// (set) Token: 0x06003732 RID: 14130 RVA: 0x0008C794 File Offset: 0x0008A994
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

		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x06003733 RID: 14131 RVA: 0x0008C79D File Offset: 0x0008A99D
		// (set) Token: 0x06003734 RID: 14132 RVA: 0x0008C7A5 File Offset: 0x0008A9A5
		public QuotaResponse Quota
		{
			get
			{
				return this.quotaField;
			}
			set
			{
				this.quotaField = value;
			}
		}

		// Token: 0x04002EF5 RID: 12021
		private XmlNode[] certificateChainField;

		// Token: 0x04002EF6 RID: 12022
		private QuotaResponse quotaField;
	}
}
