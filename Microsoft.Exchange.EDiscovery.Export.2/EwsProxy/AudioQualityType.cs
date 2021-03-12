using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000C8 RID: 200
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class AudioQualityType
	{
		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x000202BF File Offset: 0x0001E4BF
		// (set) Token: 0x060009D9 RID: 2521 RVA: 0x000202C7 File Offset: 0x0001E4C7
		public float NMOS
		{
			get
			{
				return this.nMOSField;
			}
			set
			{
				this.nMOSField = value;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x000202D0 File Offset: 0x0001E4D0
		// (set) Token: 0x060009DB RID: 2523 RVA: 0x000202D8 File Offset: 0x0001E4D8
		public float NMOSDegradation
		{
			get
			{
				return this.nMOSDegradationField;
			}
			set
			{
				this.nMOSDegradationField = value;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x000202E1 File Offset: 0x0001E4E1
		// (set) Token: 0x060009DD RID: 2525 RVA: 0x000202E9 File Offset: 0x0001E4E9
		public float NMOSDegradationPacketLoss
		{
			get
			{
				return this.nMOSDegradationPacketLossField;
			}
			set
			{
				this.nMOSDegradationPacketLossField = value;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x000202F2 File Offset: 0x0001E4F2
		// (set) Token: 0x060009DF RID: 2527 RVA: 0x000202FA File Offset: 0x0001E4FA
		public float NMOSDegradationJitter
		{
			get
			{
				return this.nMOSDegradationJitterField;
			}
			set
			{
				this.nMOSDegradationJitterField = value;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x00020303 File Offset: 0x0001E503
		// (set) Token: 0x060009E1 RID: 2529 RVA: 0x0002030B File Offset: 0x0001E50B
		public float Jitter
		{
			get
			{
				return this.jitterField;
			}
			set
			{
				this.jitterField = value;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x00020314 File Offset: 0x0001E514
		// (set) Token: 0x060009E3 RID: 2531 RVA: 0x0002031C File Offset: 0x0001E51C
		public float PacketLoss
		{
			get
			{
				return this.packetLossField;
			}
			set
			{
				this.packetLossField = value;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x00020325 File Offset: 0x0001E525
		// (set) Token: 0x060009E5 RID: 2533 RVA: 0x0002032D File Offset: 0x0001E52D
		public float RoundTrip
		{
			get
			{
				return this.roundTripField;
			}
			set
			{
				this.roundTripField = value;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x00020336 File Offset: 0x0001E536
		// (set) Token: 0x060009E7 RID: 2535 RVA: 0x0002033E File Offset: 0x0001E53E
		public float BurstDensity
		{
			get
			{
				return this.burstDensityField;
			}
			set
			{
				this.burstDensityField = value;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x00020347 File Offset: 0x0001E547
		// (set) Token: 0x060009E9 RID: 2537 RVA: 0x0002034F File Offset: 0x0001E54F
		public float BurstDuration
		{
			get
			{
				return this.burstDurationField;
			}
			set
			{
				this.burstDurationField = value;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x00020358 File Offset: 0x0001E558
		// (set) Token: 0x060009EB RID: 2539 RVA: 0x00020360 File Offset: 0x0001E560
		public string AudioCodec
		{
			get
			{
				return this.audioCodecField;
			}
			set
			{
				this.audioCodecField = value;
			}
		}

		// Token: 0x040005A5 RID: 1445
		private float nMOSField;

		// Token: 0x040005A6 RID: 1446
		private float nMOSDegradationField;

		// Token: 0x040005A7 RID: 1447
		private float nMOSDegradationPacketLossField;

		// Token: 0x040005A8 RID: 1448
		private float nMOSDegradationJitterField;

		// Token: 0x040005A9 RID: 1449
		private float jitterField;

		// Token: 0x040005AA RID: 1450
		private float packetLossField;

		// Token: 0x040005AB RID: 1451
		private float roundTripField;

		// Token: 0x040005AC RID: 1452
		private float burstDensityField;

		// Token: 0x040005AD RID: 1453
		private float burstDurationField;

		// Token: 0x040005AE RID: 1454
		private string audioCodecField;
	}
}
