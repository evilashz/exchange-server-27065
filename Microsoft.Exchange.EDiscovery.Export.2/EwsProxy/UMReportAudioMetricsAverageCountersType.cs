using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000C4 RID: 196
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class UMReportAudioMetricsAverageCountersType
	{
		// Token: 0x17000214 RID: 532
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x0002004C File Offset: 0x0001E24C
		// (set) Token: 0x0600098F RID: 2447 RVA: 0x00020054 File Offset: 0x0001E254
		public AudioMetricsAverageType NMOS
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

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x0002005D File Offset: 0x0001E25D
		// (set) Token: 0x06000991 RID: 2449 RVA: 0x00020065 File Offset: 0x0001E265
		public AudioMetricsAverageType NMOSDegradation
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

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x0002006E File Offset: 0x0001E26E
		// (set) Token: 0x06000993 RID: 2451 RVA: 0x00020076 File Offset: 0x0001E276
		public AudioMetricsAverageType Jitter
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

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x0002007F File Offset: 0x0001E27F
		// (set) Token: 0x06000995 RID: 2453 RVA: 0x00020087 File Offset: 0x0001E287
		public AudioMetricsAverageType PercentPacketLoss
		{
			get
			{
				return this.percentPacketLossField;
			}
			set
			{
				this.percentPacketLossField = value;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x00020090 File Offset: 0x0001E290
		// (set) Token: 0x06000997 RID: 2455 RVA: 0x00020098 File Offset: 0x0001E298
		public AudioMetricsAverageType RoundTrip
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

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x000200A1 File Offset: 0x0001E2A1
		// (set) Token: 0x06000999 RID: 2457 RVA: 0x000200A9 File Offset: 0x0001E2A9
		public AudioMetricsAverageType BurstLossDuration
		{
			get
			{
				return this.burstLossDurationField;
			}
			set
			{
				this.burstLossDurationField = value;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x000200B2 File Offset: 0x0001E2B2
		// (set) Token: 0x0600099B RID: 2459 RVA: 0x000200BA File Offset: 0x0001E2BA
		public long TotalAudioQualityCallsSampled
		{
			get
			{
				return this.totalAudioQualityCallsSampledField;
			}
			set
			{
				this.totalAudioQualityCallsSampledField = value;
			}
		}

		// Token: 0x04000582 RID: 1410
		private AudioMetricsAverageType nMOSField;

		// Token: 0x04000583 RID: 1411
		private AudioMetricsAverageType nMOSDegradationField;

		// Token: 0x04000584 RID: 1412
		private AudioMetricsAverageType jitterField;

		// Token: 0x04000585 RID: 1413
		private AudioMetricsAverageType percentPacketLossField;

		// Token: 0x04000586 RID: 1414
		private AudioMetricsAverageType roundTripField;

		// Token: 0x04000587 RID: 1415
		private AudioMetricsAverageType burstLossDurationField;

		// Token: 0x04000588 RID: 1416
		private long totalAudioQualityCallsSampledField;
	}
}
