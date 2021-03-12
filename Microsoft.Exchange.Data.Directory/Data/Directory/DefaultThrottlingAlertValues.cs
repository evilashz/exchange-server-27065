using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009B4 RID: 2484
	internal static class DefaultThrottlingAlertValues
	{
		// Token: 0x0600729C RID: 29340 RVA: 0x0017B765 File Offset: 0x00179965
		public static int MassUserOverBudgetPercent(BudgetType budgetType)
		{
			return DefaultThrottlingAlertValues.SafeGetValueFromMap(DefaultThrottlingAlertValues.massUserOverBudgetPercentMap, budgetType, 50);
		}

		// Token: 0x0600729D RID: 29341 RVA: 0x0017B774 File Offset: 0x00179974
		public static int DelayTimeThreshold(BudgetType budgetType)
		{
			return DefaultThrottlingAlertValues.SafeGetValueFromMap(DefaultThrottlingAlertValues.delayTimeThresholdMap, budgetType, 10000);
		}

		// Token: 0x0600729E RID: 29342 RVA: 0x0017B788 File Offset: 0x00179988
		private static int SafeGetValueFromMap(Dictionary<BudgetType, int> map, BudgetType budgetType, int defaultValue)
		{
			int result;
			if (!map.TryGetValue(budgetType, out result))
			{
				ExTraceGlobals.ClientThrottlingTracer.TraceDebug<int, BudgetType>(0L, "[DefaultThrottlingAlertValues::SafeGetValueFromMap] Using default alert value of {0} for budgetType {1}.", defaultValue, budgetType);
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x04004A3C RID: 19004
		private const int DefaultMassUserOverBudgetPercent = 50;

		// Token: 0x04004A3D RID: 19005
		private const int DefaultDelayTimeThreshold = 10000;

		// Token: 0x04004A3E RID: 19006
		private static Dictionary<BudgetType, int> massUserOverBudgetPercentMap = new Dictionary<BudgetType, int>();

		// Token: 0x04004A3F RID: 19007
		private static Dictionary<BudgetType, int> delayTimeThresholdMap = new Dictionary<BudgetType, int>();
	}
}
