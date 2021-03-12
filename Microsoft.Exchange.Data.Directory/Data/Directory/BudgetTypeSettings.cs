using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009A9 RID: 2473
	internal static class BudgetTypeSettings
	{
		// Token: 0x06007226 RID: 29222 RVA: 0x0017A10C File Offset: 0x0017830C
		static BudgetTypeSettings()
		{
			BudgetTypeSettings.Initialize();
		}

		// Token: 0x06007227 RID: 29223 RVA: 0x0017A119 File Offset: 0x00178319
		internal static void Initialize()
		{
			BudgetTypeSettings.CreateDefaultBudgetTypeSettingsMap();
			BudgetTypeSettings.LoadAppConfigSettings();
		}

		// Token: 0x06007228 RID: 29224 RVA: 0x0017A125 File Offset: 0x00178325
		public static BudgetTypeSetting Get(BudgetType budgetType)
		{
			if (BudgetTypeSettings.GetSettingsTestHook != null)
			{
				return BudgetTypeSettings.GetSettingsTestHook(budgetType);
			}
			return BudgetTypeSettings.budgetTypeSettingsMap[budgetType];
		}

		// Token: 0x06007229 RID: 29225 RVA: 0x0017A148 File Offset: 0x00178348
		internal static BudgetTypeSetting GetDefault(BudgetType budgetType)
		{
			if (BudgetTypeSettings.GetSettingsTestHook != null)
			{
				return BudgetTypeSettings.GetSettingsTestHook(budgetType);
			}
			BudgetTypeSetting result = null;
			if (BudgetTypeSettings.defaultBudgetTypeSettingsMap.TryGetValue(budgetType, out result))
			{
				return result;
			}
			return BudgetTypeSetting.OneMinuteSetting;
		}

		// Token: 0x0600722A RID: 29226 RVA: 0x0017A180 File Offset: 0x00178380
		private static void CreateDefaultBudgetTypeSettingsMap()
		{
			BudgetTypeSettings.defaultBudgetTypeSettingsMap = new Dictionary<BudgetType, BudgetTypeSetting>();
			BudgetTypeSettings.defaultBudgetTypeSettingsMap.Add(BudgetType.OwaVoice, new BudgetTypeSetting(TimeSpan.FromSeconds(5.0), 3, 5));
			BudgetTypeSettings.defaultBudgetTypeSettingsMap.Add(BudgetType.Rca, new BudgetTypeSetting(TimeSpan.FromSeconds(5.0), 3, 5));
		}

		// Token: 0x0600722B RID: 29227 RVA: 0x0017A1D8 File Offset: 0x001783D8
		internal static string BuildMicroDelayMultiplierKey(BudgetType budgetType)
		{
			return string.Format("{0}.{1}", budgetType, "MaxMicroDelayMultiplier");
		}

		// Token: 0x0600722C RID: 29228 RVA: 0x0017A1EF File Offset: 0x001783EF
		internal static string BuildMaxDelayKey(BudgetType budgetType)
		{
			return string.Format("{0}.{1}", budgetType, "MaxDelayInSeconds");
		}

		// Token: 0x0600722D RID: 29229 RVA: 0x0017A206 File Offset: 0x00178406
		internal static string BuildMaxDelayedThreadsKey(BudgetType budgetType)
		{
			return string.Format("{0}.{1}", budgetType, "MaxDelayedThreadsPerProcessor");
		}

		// Token: 0x0600722E RID: 29230 RVA: 0x0017A220 File Offset: 0x00178420
		private static void LoadAppConfigSettings()
		{
			BudgetTypeSettings.budgetTypeSettingsMap = new Dictionary<BudgetType, BudgetTypeSetting>();
			foreach (object obj in Enum.GetValues(typeof(BudgetType)))
			{
				BudgetType budgetType = (BudgetType)obj;
				string name = BudgetTypeSettings.BuildMicroDelayMultiplierKey(budgetType);
				string name2 = BudgetTypeSettings.BuildMaxDelayKey(budgetType);
				string name3 = BudgetTypeSettings.BuildMaxDelayedThreadsKey(budgetType);
				BudgetTypeSetting @default = BudgetTypeSettings.GetDefault(budgetType);
				IntAppSettingsEntry intAppSettingsEntry = new IntAppSettingsEntry(name, @default.MaxMicroDelayMultiplier, ExTraceGlobals.BudgetDelayTracer);
				TimeSpanAppSettingsEntry timeSpanAppSettingsEntry = new TimeSpanAppSettingsEntry(name2, TimeSpanUnit.Seconds, @default.MaxDelay, ExTraceGlobals.BudgetDelayTracer);
				IntAppSettingsEntry intAppSettingsEntry2 = new IntAppSettingsEntry(name3, @default.MaxDelayedThreadPerProcessor, ExTraceGlobals.BudgetDelayTracer);
				BudgetTypeSetting value = new BudgetTypeSetting(timeSpanAppSettingsEntry.Value, intAppSettingsEntry.Value, intAppSettingsEntry2.Value);
				BudgetTypeSettings.budgetTypeSettingsMap[budgetType] = value;
			}
		}

		// Token: 0x040049E8 RID: 18920
		public const string MaxMicroDelayMultiplierValueName = "MaxMicroDelayMultiplier";

		// Token: 0x040049E9 RID: 18921
		public const string MaxDelayValueName = "MaxDelayInSeconds";

		// Token: 0x040049EA RID: 18922
		public const string MaxDelayedThreadsValuePerProcessorName = "MaxDelayedThreadsPerProcessor";

		// Token: 0x040049EB RID: 18923
		public static Func<BudgetType, BudgetTypeSetting> GetSettingsTestHook = null;

		// Token: 0x040049EC RID: 18924
		private static Dictionary<BudgetType, BudgetTypeSetting> defaultBudgetTypeSettingsMap;

		// Token: 0x040049ED RID: 18925
		private static Dictionary<BudgetType, BudgetTypeSetting> budgetTypeSettingsMap;
	}
}
