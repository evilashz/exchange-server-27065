using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000947 RID: 2375
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class XmlValueCompanyVerifiedDomain
	{
		// Token: 0x170027DD RID: 10205
		// (get) Token: 0x06007015 RID: 28693 RVA: 0x00177157 File Offset: 0x00175357
		// (set) Token: 0x06007016 RID: 28694 RVA: 0x0017715F File Offset: 0x0017535F
		[XmlElement(Order = 0)]
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

		// Token: 0x040048BC RID: 18620
		private CompanyVerifiedDomainValue domainField;
	}
}
