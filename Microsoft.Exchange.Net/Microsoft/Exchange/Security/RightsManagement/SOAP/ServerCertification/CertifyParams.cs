using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.ServerCertification
{
	// Token: 0x020009E2 RID: 2530
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://microsoft.com/DRM/CertificationService")]
	[Serializable]
	public class CertifyParams
	{
		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x06003736 RID: 14134 RVA: 0x0008C7B6 File Offset: 0x0008A9B6
		// (set) Token: 0x06003737 RID: 14135 RVA: 0x0008C7BE File Offset: 0x0008A9BE
		[XmlArrayItem("Certificate")]
		public XmlNode[] MachineCertificateChain
		{
			get
			{
				return this.machineCertificateChainField;
			}
			set
			{
				this.machineCertificateChainField = value;
			}
		}

		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x06003738 RID: 14136 RVA: 0x0008C7C7 File Offset: 0x0008A9C7
		// (set) Token: 0x06003739 RID: 14137 RVA: 0x0008C7CF File Offset: 0x0008A9CF
		public bool Persistent
		{
			get
			{
				return this.persistentField;
			}
			set
			{
				this.persistentField = value;
			}
		}

		// Token: 0x04002EF7 RID: 12023
		private XmlNode[] machineCertificateChainField;

		// Token: 0x04002EF8 RID: 12024
		private bool persistentField;
	}
}
