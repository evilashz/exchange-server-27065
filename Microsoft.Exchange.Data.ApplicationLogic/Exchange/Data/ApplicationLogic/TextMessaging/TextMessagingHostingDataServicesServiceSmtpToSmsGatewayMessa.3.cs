using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.ApplicationLogic.TextMessaging
{
	// Token: 0x02000089 RID: 137
	[XmlType(AnonymousType = true)]
	[GeneratedCode("xsd", "4.0.30319.17627")]
	[Serializable]
	public enum TextMessagingHostingDataServicesServiceSmtpToSmsGatewayMessageRenderingCapacityCodingScheme
	{
		// Token: 0x04000288 RID: 648
		GsmDefault,
		// Token: 0x04000289 RID: 649
		Unicode,
		// Token: 0x0400028A RID: 650
		UsAscii,
		// Token: 0x0400028B RID: 651
		Ia5,
		// Token: 0x0400028C RID: 652
		Iso_8859_1,
		// Token: 0x0400028D RID: 653
		Iso_8859_8,
		// Token: 0x0400028E RID: 654
		ShiftJis,
		// Token: 0x0400028F RID: 655
		EucKr
	}
}
