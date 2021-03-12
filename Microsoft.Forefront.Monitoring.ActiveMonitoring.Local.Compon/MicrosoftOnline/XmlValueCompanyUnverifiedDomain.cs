using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000108 RID: 264
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[Serializable]
	public class XmlValueCompanyUnverifiedDomain
	{
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x00020075 File Offset: 0x0001E275
		// (set) Token: 0x060007E5 RID: 2021 RVA: 0x0002007D File Offset: 0x0001E27D
		public CompanyUnverifiedDomainValue Domain
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

		// Token: 0x04000410 RID: 1040
		private CompanyUnverifiedDomainValue domainField;
	}
}
