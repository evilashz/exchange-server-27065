using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200034B RID: 843
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Flags]
	[Serializable]
	public enum ServiceConfigurationType
	{
		// Token: 0x0400123D RID: 4669
		MailTips = 1,
		// Token: 0x0400123E RID: 4670
		UnifiedMessagingConfiguration = 2,
		// Token: 0x0400123F RID: 4671
		ProtectionRules = 4,
		// Token: 0x04001240 RID: 4672
		PolicyNudges = 8
	}
}
