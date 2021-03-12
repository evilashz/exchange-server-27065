using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.WorkingSet.Publisher
{
	// Token: 0x02000EEB RID: 3819
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class WorkingSetPublisherConfiguration
	{
		// Token: 0x170022F9 RID: 8953
		// (get) Token: 0x060083CF RID: 33743 RVA: 0x0023CC09 File Offset: 0x0023AE09
		// (set) Token: 0x060083D0 RID: 33744 RVA: 0x0023CC10 File Offset: 0x0023AE10
		public static bool PublishModernGroupsSignals { get; private set; } = AppConfigLoader.GetConfigBoolValue("PublishModernGroupsSignals", true);

		// Token: 0x170022FA RID: 8954
		// (get) Token: 0x060083D1 RID: 33745 RVA: 0x0023CC18 File Offset: 0x0023AE18
		// (set) Token: 0x060083D2 RID: 33746 RVA: 0x0023CC1F File Offset: 0x0023AE1F
		public static TimeSpan ModernGroupsDataExpiryTime { get; private set; } = AppConfigLoader.GetConfigTimeSpanValue("ModernGroupsDataExpiryTime", WorkingSetPublisherConfiguration.ModernGroupsDataExpiryTimeMinValue, WorkingSetPublisherConfiguration.ModernGroupsDataExpiryTimeMaxValue, WorkingSetPublisherConfiguration.ModernGroupsDataExpiryTimeDefaultValue);

		// Token: 0x170022FB RID: 8955
		// (get) Token: 0x060083D3 RID: 33747 RVA: 0x0023CC27 File Offset: 0x0023AE27
		// (set) Token: 0x060083D4 RID: 33748 RVA: 0x0023CC2E File Offset: 0x0023AE2E
		public static int ModernGroupsItemAddWeight { get; private set; } = AppConfigLoader.GetConfigIntValue("ModernGroupsItemAddWeight", 1, int.MaxValue, 10);

		// Token: 0x170022FC RID: 8956
		// (get) Token: 0x060083D5 RID: 33749 RVA: 0x0023CC36 File Offset: 0x0023AE36
		// (set) Token: 0x060083D6 RID: 33750 RVA: 0x0023CC3D File Offset: 0x0023AE3D
		public static int MaxTargetUsersToCachePerModernGroup { get; private set; } = AppConfigLoader.GetConfigIntValue("MaxTargetUsersToCachePerModernGroup", 1, int.MaxValue, 1000);

		// Token: 0x0400580A RID: 22538
		public const string PublishModernGroupsSignalsSetting = "PublishModernGroupsSignals";

		// Token: 0x0400580B RID: 22539
		public const string ModernGroupsDataExpiryTimeSetting = "ModernGroupsDataExpiryTime";

		// Token: 0x0400580C RID: 22540
		public const string ModernGroupsItemAddWeightSetting = "ModernGroupsItemAddWeight";

		// Token: 0x0400580D RID: 22541
		public const string MaxTargetUsersToCachePerModernGroupSetting = "MaxTargetUsersToCachePerModernGroup";

		// Token: 0x0400580E RID: 22542
		public const bool PublishModernGroupsSignalsDefaultValue = true;

		// Token: 0x0400580F RID: 22543
		public const int ModernGroupsItemAddWeightDefaultValue = 10;

		// Token: 0x04005810 RID: 22544
		public const int ModernGroupsItemAddWeightMinValue = 1;

		// Token: 0x04005811 RID: 22545
		public const int ModernGroupsItemAddWeightMaxValue = 2147483647;

		// Token: 0x04005812 RID: 22546
		public const int MaxTargetUsersToCachePerModernGroupDefaultValue = 1000;

		// Token: 0x04005813 RID: 22547
		public const int MaxTargetUsersToCachePerModernGroupMinValue = 1;

		// Token: 0x04005814 RID: 22548
		public const int MaxTargetUsersToCachePerModernGroupMaxValue = 2147483647;

		// Token: 0x04005815 RID: 22549
		public static readonly TimeSpan ModernGroupsDataExpiryTimeDefaultValue = TimeSpan.FromMinutes(60.0);

		// Token: 0x04005816 RID: 22550
		public static readonly TimeSpan ModernGroupsDataExpiryTimeMinValue = TimeSpan.FromMinutes(0.0);

		// Token: 0x04005817 RID: 22551
		public static readonly TimeSpan ModernGroupsDataExpiryTimeMaxValue = TimeSpan.FromDays(365.0);
	}
}
