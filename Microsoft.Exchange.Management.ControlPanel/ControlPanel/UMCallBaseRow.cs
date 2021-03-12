using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004AE RID: 1198
	[DataContract]
	public abstract class UMCallBaseRow : BaseRow
	{
		// Token: 0x06003B46 RID: 15174 RVA: 0x000B333F File Offset: 0x000B153F
		public UMCallBaseRow(UMCallReportBase report) : base(UMUtils.CreateUniqueUMReportingRowIdentity(), report)
		{
			this.UMCallReportBase = report;
			this.audioQualityIcon = UMUtils.GetAudioQualityIconAndAlternateText(this.UMCallReportBase.NMOS, out this.audioQualityAltText);
		}

		// Token: 0x1700236A RID: 9066
		// (get) Token: 0x06003B47 RID: 15175 RVA: 0x000B3370 File Offset: 0x000B1570
		// (set) Token: 0x06003B48 RID: 15176 RVA: 0x000B3378 File Offset: 0x000B1578
		private UMCallReportBase UMCallReportBase { get; set; }

		// Token: 0x1700236B RID: 9067
		// (get) Token: 0x06003B49 RID: 15177 RVA: 0x000B3381 File Offset: 0x000B1581
		// (set) Token: 0x06003B4A RID: 15178 RVA: 0x000B3398 File Offset: 0x000B1598
		[DataMember]
		public string NMOS
		{
			get
			{
				return UMUtils.FormatAudioQualityMetricDisplay(UMUtils.FormatFloat(this.UMCallReportBase.NMOS));
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700236C RID: 9068
		// (get) Token: 0x06003B4B RID: 15179 RVA: 0x000B339F File Offset: 0x000B159F
		// (set) Token: 0x06003B4C RID: 15180 RVA: 0x000B33B6 File Offset: 0x000B15B6
		[DataMember]
		public string NMOSDegradation
		{
			get
			{
				return UMUtils.FormatAudioQualityMetricDisplay(UMUtils.FormatFloat(this.UMCallReportBase.NMOSDegradation));
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700236D RID: 9069
		// (get) Token: 0x06003B4D RID: 15181 RVA: 0x000B33C0 File Offset: 0x000B15C0
		// (set) Token: 0x06003B4E RID: 15182 RVA: 0x000B3416 File Offset: 0x000B1616
		[DataMember]
		public string PacketLoss
		{
			get
			{
				string metric = string.Empty;
				if (this.UMCallReportBase.PercentPacketLoss != null)
				{
					metric = (this.UMCallReportBase.PercentPacketLoss.Value / 100f).ToString("#0.0%");
				}
				return UMUtils.FormatAudioQualityMetricDisplay(metric);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700236E RID: 9070
		// (get) Token: 0x06003B4F RID: 15183 RVA: 0x000B341D File Offset: 0x000B161D
		// (set) Token: 0x06003B50 RID: 15184 RVA: 0x000B3439 File Offset: 0x000B1639
		[DataMember]
		public string Jitter
		{
			get
			{
				return UMUtils.FormatAudioQualityMetricDisplay(UMUtils.AppendMillisecondSuffix(UMUtils.FormatFloat(this.UMCallReportBase.Jitter)));
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700236F RID: 9071
		// (get) Token: 0x06003B51 RID: 15185 RVA: 0x000B3440 File Offset: 0x000B1640
		// (set) Token: 0x06003B52 RID: 15186 RVA: 0x000B345C File Offset: 0x000B165C
		[DataMember]
		public string RoundTrip
		{
			get
			{
				return UMUtils.FormatAudioQualityMetricDisplay(UMUtils.AppendMillisecondSuffix(UMUtils.FormatFloat(this.UMCallReportBase.RoundTripMilliseconds)));
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002370 RID: 9072
		// (get) Token: 0x06003B53 RID: 15187 RVA: 0x000B3463 File Offset: 0x000B1663
		// (set) Token: 0x06003B54 RID: 15188 RVA: 0x000B347F File Offset: 0x000B167F
		[DataMember]
		public string BurstLossDuration
		{
			get
			{
				return UMUtils.FormatAudioQualityMetricDisplay(UMUtils.AppendMillisecondSuffix(UMUtils.FormatFloat(this.UMCallReportBase.BurstLossDurationMilliseconds)));
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002371 RID: 9073
		// (get) Token: 0x06003B55 RID: 15189 RVA: 0x000B3486 File Offset: 0x000B1686
		// (set) Token: 0x06003B56 RID: 15190 RVA: 0x000B348E File Offset: 0x000B168E
		[DataMember]
		public string AudioQuality
		{
			get
			{
				return this.audioQualityIcon;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002372 RID: 9074
		// (get) Token: 0x06003B57 RID: 15191 RVA: 0x000B3495 File Offset: 0x000B1695
		// (set) Token: 0x06003B58 RID: 15192 RVA: 0x000B349D File Offset: 0x000B169D
		[DataMember]
		public string AudioQualityAlternateText
		{
			get
			{
				return this.audioQualityAltText;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x04002763 RID: 10083
		private string audioQualityIcon;

		// Token: 0x04002764 RID: 10084
		private string audioQualityAltText;
	}
}
