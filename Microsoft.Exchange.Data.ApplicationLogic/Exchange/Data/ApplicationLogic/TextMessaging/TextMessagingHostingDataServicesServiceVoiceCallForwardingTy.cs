using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.ApplicationLogic.TextMessaging
{
	// Token: 0x02000084 RID: 132
	[XmlType(AnonymousType = true)]
	[GeneratedCode("xsd", "4.0.30319.17627")]
	[Serializable]
	public enum TextMessagingHostingDataServicesServiceVoiceCallForwardingType
	{
		// Token: 0x0400027B RID: 635
		Conditional,
		// Token: 0x0400027C RID: 636
		Busy,
		// Token: 0x0400027D RID: 637
		NoAnswer,
		// Token: 0x0400027E RID: 638
		OutOfReach
	}
}
