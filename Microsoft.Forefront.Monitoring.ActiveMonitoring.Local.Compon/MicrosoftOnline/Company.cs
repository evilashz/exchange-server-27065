using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x0200009E RID: 158
	[XmlType(Namespace = "http://www.ccs.com/TestServices/")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class Company
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x0001EC8E File Offset: 0x0001CE8E
		// (set) Token: 0x0600059A RID: 1434 RVA: 0x0001EC96 File Offset: 0x0001CE96
		public string DomainPrefix
		{
			get
			{
				return this.domainPrefixField;
			}
			set
			{
				this.domainPrefixField = value;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x0001EC9F File Offset: 0x0001CE9F
		// (set) Token: 0x0600059C RID: 1436 RVA: 0x0001ECA7 File Offset: 0x0001CEA7
		public string DomainSuffix
		{
			get
			{
				return this.domainSuffixField;
			}
			set
			{
				this.domainSuffixField = value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x0001ECB0 File Offset: 0x0001CEB0
		// (set) Token: 0x0600059E RID: 1438 RVA: 0x0001ECB8 File Offset: 0x0001CEB8
		public CompanyProfile CompanyProfile
		{
			get
			{
				return this.companyProfileField;
			}
			set
			{
				this.companyProfileField = value;
			}
		}

		// Token: 0x040002D3 RID: 723
		private string domainPrefixField;

		// Token: 0x040002D4 RID: 724
		private string domainSuffixField;

		// Token: 0x040002D5 RID: 725
		private CompanyProfile companyProfileField;
	}
}
