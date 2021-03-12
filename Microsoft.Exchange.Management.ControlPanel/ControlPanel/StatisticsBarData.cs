using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000F4 RID: 244
	[DataContract]
	public class StatisticsBarData
	{
		// Token: 0x06001E97 RID: 7831 RVA: 0x0005C1A1 File Offset: 0x0005A3A1
		public StatisticsBarData(uint usagePercentage, StatisticsBarState usageState, string usageText) : this(usagePercentage, usageState, usageText, null)
		{
		}

		// Token: 0x06001E98 RID: 7832 RVA: 0x0005C1AD File Offset: 0x0005A3AD
		public StatisticsBarData(uint usagePercentage, StatisticsBarState usageState, string usageText, string additionalInfoText)
		{
			this.UsagePercentage = usagePercentage;
			this.UsageState = usageState;
			this.UsageText = usageText;
			this.AdditionalInfoText = additionalInfoText;
		}

		// Token: 0x170019CF RID: 6607
		// (get) Token: 0x06001E99 RID: 7833 RVA: 0x0005C1D2 File Offset: 0x0005A3D2
		// (set) Token: 0x06001E9A RID: 7834 RVA: 0x0005C1DA File Offset: 0x0005A3DA
		[DataMember]
		public uint UsagePercentage { get; private set; }

		// Token: 0x170019D0 RID: 6608
		// (get) Token: 0x06001E9B RID: 7835 RVA: 0x0005C1E3 File Offset: 0x0005A3E3
		// (set) Token: 0x06001E9C RID: 7836 RVA: 0x0005C1EB File Offset: 0x0005A3EB
		[DataMember]
		public StatisticsBarState UsageState { get; private set; }

		// Token: 0x170019D1 RID: 6609
		// (get) Token: 0x06001E9D RID: 7837 RVA: 0x0005C1F4 File Offset: 0x0005A3F4
		// (set) Token: 0x06001E9E RID: 7838 RVA: 0x0005C1FC File Offset: 0x0005A3FC
		[DataMember]
		public string UsageText { get; private set; }

		// Token: 0x170019D2 RID: 6610
		// (get) Token: 0x06001E9F RID: 7839 RVA: 0x0005C205 File Offset: 0x0005A405
		// (set) Token: 0x06001EA0 RID: 7840 RVA: 0x0005C20D File Offset: 0x0005A40D
		[DataMember]
		public string AdditionalInfoText { get; private set; }

		// Token: 0x06001EA1 RID: 7841 RVA: 0x0005C218 File Offset: 0x0005A418
		public bool Equals(StatisticsBarData statisticsBardata)
		{
			return statisticsBardata != null && (string.Compare(this.UsageText, statisticsBardata.UsageText) == 0 && this.UsageState == statisticsBardata.UsageState && this.UsagePercentage == statisticsBardata.UsagePercentage) && string.Compare(this.AdditionalInfoText, statisticsBardata.AdditionalInfoText) == 0;
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x0005C26F File Offset: 0x0005A46F
		public override bool Equals(object obj)
		{
			return this.Equals((StatisticsBarData)obj);
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x0005C280 File Offset: 0x0005A480
		public override int GetHashCode()
		{
			return (this.UsageText + this.UsageState.ToString() + this.UsagePercentage.ToString() + this.AdditionalInfoText).GetHashCode();
		}
	}
}
