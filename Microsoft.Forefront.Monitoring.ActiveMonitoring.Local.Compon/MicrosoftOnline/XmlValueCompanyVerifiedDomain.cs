using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000101 RID: 257
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[Serializable]
	public class XmlValueCompanyVerifiedDomain
	{
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x0001FF23 File Offset: 0x0001E123
		// (set) Token: 0x060007C1 RID: 1985 RVA: 0x0001FF2B File Offset: 0x0001E12B
		public CompanyVerifiedDomainValue Domain
		{
			get
			{
				return this.domainField;
			}
			set
			{
				this.domainField = value;
			}
		}

		// Token: 0x040003FD RID: 1021
		private CompanyVerifiedDomainValue domainField;
	}
}
