using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002A6 RID: 678
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ProtectionRulesServiceConfiguration : ServiceConfiguration
	{
		// Token: 0x040011C7 RID: 4551
		[XmlArrayItem("Rule", IsNullable = false)]
		public ProtectionRuleType[] Rules;

		// Token: 0x040011C8 RID: 4552
		[XmlArrayItem("Domain", IsNullable = false)]
		public SmtpDomain[] InternalDomains;

		// Token: 0x040011C9 RID: 4553
		[XmlAttribute]
		public int RefreshInterval;
	}
}
