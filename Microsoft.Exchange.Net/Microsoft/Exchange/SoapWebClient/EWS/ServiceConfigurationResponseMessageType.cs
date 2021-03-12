using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002A2 RID: 674
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class ServiceConfigurationResponseMessageType : ResponseMessageType
	{
		// Token: 0x040011B8 RID: 4536
		public MailTipsServiceConfiguration MailTipsConfiguration;

		// Token: 0x040011B9 RID: 4537
		public UnifiedMessageServiceConfiguration UnifiedMessagingConfiguration;

		// Token: 0x040011BA RID: 4538
		public ProtectionRulesServiceConfiguration ProtectionRulesConfiguration;

		// Token: 0x040011BB RID: 4539
		public PolicyNudgeRulesServiceConfiguration PolicyNudgeRulesConfiguration;
	}
}
