using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.ApplicationLogic.TextMessaging
{
	// Token: 0x02000082 RID: 130
	[XmlType(AnonymousType = true)]
	[GeneratedCode("xsd", "4.0.30319.17627")]
	[Serializable]
	public enum TextMessagingHostingDataServicesServiceType
	{
		// Token: 0x04000274 RID: 628
		VoiceCallForwarding,
		// Token: 0x04000275 RID: 629
		SmtpToSmsGateway
	}
}
