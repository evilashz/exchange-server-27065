using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Performance;

namespace Microsoft.Exchange.Data.HA.DirectoryServices
{
	// Token: 0x02000022 RID: 34
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ADUtils
	{
		// Token: 0x060001B4 RID: 436 RVA: 0x00004DC4 File Offset: 0x00002FC4
		public static Exception RunADOperation(Action adOperation, int retryCount = 2)
		{
			Exception result = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				adOperation();
			}, retryCount);
			if (!adoperationResult.Succeeded)
			{
				result = adoperationResult.Exception;
			}
			return result;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00004E04 File Offset: 0x00003004
		public static Exception RunADOperation(Action adOperation, IPerformanceDataLogger perfLogger, int retryCount = 2)
		{
			Exception result = null;
			string marker = "ADQuery";
			using (new StopwatchPerformanceTracker(marker, perfLogger))
			{
				result = ADUtils.RunADOperation(adOperation, retryCount);
			}
			return result;
		}
	}
}
