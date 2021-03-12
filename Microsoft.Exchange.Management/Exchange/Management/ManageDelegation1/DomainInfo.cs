using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Management.ManageDelegation1
{
	// Token: 0x02000DAE RID: 3502
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://domains.live.com/Service/ManageDelegation/V1.0")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class DomainInfo
	{
		// Token: 0x170029BF RID: 10687
		// (get) Token: 0x06008620 RID: 34336 RVA: 0x00224F4D File Offset: 0x0022314D
		// (set) Token: 0x06008621 RID: 34337 RVA: 0x00224F55 File Offset: 0x00223155
		public string DomainName
		{
			get
			{
				return this.domainNameField;
			}
			set
			{
				this.domainNameField = value;
			}
		}

		// Token: 0x170029C0 RID: 10688
		// (get) Token: 0x06008622 RID: 34338 RVA: 0x00224F5E File Offset: 0x0022315E
		// (set) Token: 0x06008623 RID: 34339 RVA: 0x00224F66 File Offset: 0x00223166
		public string AppId
		{
			get
			{
				return this.appIdField;
			}
			set
			{
				this.appIdField = value;
			}
		}

		// Token: 0x170029C1 RID: 10689
		// (get) Token: 0x06008624 RID: 34340 RVA: 0x00224F6F File Offset: 0x0022316F
		// (set) Token: 0x06008625 RID: 34341 RVA: 0x00224F77 File Offset: 0x00223177
		public DomainState DomainState
		{
			get
			{
				return this.domainStateField;
			}
			set
			{
				this.domainStateField = value;
			}
		}

		// Token: 0x0400413E RID: 16702
		private string domainNameField;

		// Token: 0x0400413F RID: 16703
		private string appIdField;

		// Token: 0x04004140 RID: 16704
		private DomainState domainStateField;
	}
}
