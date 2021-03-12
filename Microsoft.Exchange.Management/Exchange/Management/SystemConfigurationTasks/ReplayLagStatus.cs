using System;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000895 RID: 2197
	[Serializable]
	public sealed class ReplayLagStatus
	{
		// Token: 0x17001703 RID: 5891
		// (get) Token: 0x06004D19 RID: 19737 RVA: 0x0013FC3E File Offset: 0x0013DE3E
		// (set) Token: 0x06004D1A RID: 19738 RVA: 0x0013FC46 File Offset: 0x0013DE46
		public EnhancedTimeSpan ConfiguredLagTime { get; private set; }

		// Token: 0x17001704 RID: 5892
		// (get) Token: 0x06004D1B RID: 19739 RVA: 0x0013FC4F File Offset: 0x0013DE4F
		// (set) Token: 0x06004D1C RID: 19740 RVA: 0x0013FC57 File Offset: 0x0013DE57
		public EnhancedTimeSpan ActualLagTime { get; private set; }

		// Token: 0x17001705 RID: 5893
		// (get) Token: 0x06004D1D RID: 19741 RVA: 0x0013FC60 File Offset: 0x0013DE60
		// (set) Token: 0x06004D1E RID: 19742 RVA: 0x0013FC68 File Offset: 0x0013DE68
		public int Percentage { get; private set; }

		// Token: 0x17001706 RID: 5894
		// (get) Token: 0x06004D1F RID: 19743 RVA: 0x0013FC71 File Offset: 0x0013DE71
		// (set) Token: 0x06004D20 RID: 19744 RVA: 0x0013FC79 File Offset: 0x0013DE79
		public string DisabledReason { get; private set; }

		// Token: 0x17001707 RID: 5895
		// (get) Token: 0x06004D21 RID: 19745 RVA: 0x0013FC82 File Offset: 0x0013DE82
		// (set) Token: 0x06004D22 RID: 19746 RVA: 0x0013FC8A File Offset: 0x0013DE8A
		public bool Enabled { get; private set; }

		// Token: 0x17001708 RID: 5896
		// (get) Token: 0x06004D23 RID: 19747 RVA: 0x0013FC93 File Offset: 0x0013DE93
		// (set) Token: 0x06004D24 RID: 19748 RVA: 0x0013FC9B File Offset: 0x0013DE9B
		public ReplayLagPlayDownReason PlayDownReason { get; private set; }

		// Token: 0x06004D25 RID: 19749 RVA: 0x0013FCA4 File Offset: 0x0013DEA4
		internal ReplayLagStatus(bool isLagInEffect, EnhancedTimeSpan configuredLagTime, EnhancedTimeSpan actualLagTime, int lagPercentage, ReplayLagPlayDownReason playDownReason, string disabledReason)
		{
			this.Enabled = isLagInEffect;
			this.ConfiguredLagTime = configuredLagTime;
			this.ActualLagTime = actualLagTime;
			this.Percentage = lagPercentage;
			this.PlayDownReason = playDownReason;
			this.DisabledReason = disabledReason;
		}

		// Token: 0x06004D26 RID: 19750 RVA: 0x0013FCDC File Offset: 0x0013DEDC
		public override string ToString()
		{
			return string.Format("{3}:{0}; {7}:{6}; {9}:{8}; {4}:{1}; {5}:{2}", new object[]
			{
				this.Enabled,
				this.ConvertTimeSpanToString(this.ConfiguredLagTime),
				this.ConvertTimeSpanToString(this.ActualLagTime),
				Strings.ReplayLagStatusLabelEnabled,
				Strings.ReplayLagStatusLabelConfigured,
				Strings.ReplayLagStatusLabelActual,
				this.PlayDownReason,
				Strings.ReplayLagStatusLabelPlayDownReason,
				this.Percentage,
				Strings.ReplayLagStatusLabelPercentage
			});
		}

		// Token: 0x06004D27 RID: 19751 RVA: 0x0013FD86 File Offset: 0x0013DF86
		private string ConvertTimeSpanToString(EnhancedTimeSpan timeSpan)
		{
			return DateTimeHelper.ConvertTimeSpanToShortString(timeSpan);
		}

		// Token: 0x04002E11 RID: 11793
		private const string ToStringFormatStr = "{3}:{0}; {7}:{6}; {9}:{8}; {4}:{1}; {5}:{2}";
	}
}
