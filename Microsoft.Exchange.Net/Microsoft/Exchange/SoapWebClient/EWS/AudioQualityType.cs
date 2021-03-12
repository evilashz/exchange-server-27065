using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001A9 RID: 425
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class AudioQualityType
	{
		// Token: 0x040009F7 RID: 2551
		public float NMOS;

		// Token: 0x040009F8 RID: 2552
		public float NMOSDegradation;

		// Token: 0x040009F9 RID: 2553
		public float NMOSDegradationPacketLoss;

		// Token: 0x040009FA RID: 2554
		public float NMOSDegradationJitter;

		// Token: 0x040009FB RID: 2555
		public float Jitter;

		// Token: 0x040009FC RID: 2556
		public float PacketLoss;

		// Token: 0x040009FD RID: 2557
		public float RoundTrip;

		// Token: 0x040009FE RID: 2558
		public float BurstDensity;

		// Token: 0x040009FF RID: 2559
		public float BurstDuration;

		// Token: 0x04000A00 RID: 2560
		public string AudioCodec;
	}
}
