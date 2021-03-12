using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x02000004 RID: 4
	[XmlType(Namespace = "http://domains.live.com/Service/DomainServices/V1.0")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class CertData
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00006866 File Offset: 0x00004A66
		// (set) Token: 0x0600017F RID: 383 RVA: 0x0000686E File Offset: 0x00004A6E
		public string CertIssuer
		{
			get
			{
				return this.certIssuerField;
			}
			set
			{
				this.certIssuerField = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00006877 File Offset: 0x00004A77
		// (set) Token: 0x06000181 RID: 385 RVA: 0x0000687F File Offset: 0x00004A7F
		public string CertSubject
		{
			get
			{
				return this.certSubjectField;
			}
			set
			{
				this.certSubjectField = value;
			}
		}

		// Token: 0x04000062 RID: 98
		private string certIssuerField;

		// Token: 0x04000063 RID: 99
		private string certSubjectField;
	}
}
