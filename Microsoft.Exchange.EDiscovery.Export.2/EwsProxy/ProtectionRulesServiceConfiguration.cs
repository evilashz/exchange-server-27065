using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001C5 RID: 453
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class ProtectionRulesServiceConfiguration : ServiceConfiguration
	{
		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001373 RID: 4979 RVA: 0x000253FE File Offset: 0x000235FE
		// (set) Token: 0x06001374 RID: 4980 RVA: 0x00025406 File Offset: 0x00023606
		[XmlArrayItem("Rule", IsNullable = false)]
		public ProtectionRuleType[] Rules
		{
			get
			{
				return this.rulesField;
			}
			set
			{
				this.rulesField = value;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001375 RID: 4981 RVA: 0x0002540F File Offset: 0x0002360F
		// (set) Token: 0x06001376 RID: 4982 RVA: 0x00025417 File Offset: 0x00023617
		[XmlArrayItem("Domain", IsNullable = false)]
		public SmtpDomain[] InternalDomains
		{
			get
			{
				return this.internalDomainsField;
			}
			set
			{
				this.internalDomainsField = value;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001377 RID: 4983 RVA: 0x00025420 File Offset: 0x00023620
		// (set) Token: 0x06001378 RID: 4984 RVA: 0x00025428 File Offset: 0x00023628
		[XmlAttribute]
		public int RefreshInterval
		{
			get
			{
				return this.refreshIntervalField;
			}
			set
			{
				this.refreshIntervalField = value;
			}
		}

		// Token: 0x04000D75 RID: 3445
		private ProtectionRuleType[] rulesField;

		// Token: 0x04000D76 RID: 3446
		private SmtpDomain[] internalDomainsField;

		// Token: 0x04000D77 RID: 3447
		private int refreshIntervalField;
	}
}
