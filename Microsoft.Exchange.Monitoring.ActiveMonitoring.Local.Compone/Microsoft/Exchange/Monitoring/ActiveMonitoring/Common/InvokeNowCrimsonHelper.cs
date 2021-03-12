using System;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x020001E2 RID: 482
	internal class InvokeNowCrimsonHelper
	{
		// Token: 0x06000D74 RID: 3444 RVA: 0x0005B374 File Offset: 0x00059574
		internal static CrimsonReader<InvokeNowEntry> PrepareReader(RecoveryActionId actionId, string resourceName, string instanceId, RecoveryActionState state, RecoveryActionResult result, DateTime startTime, DateTime endTime, string xpathQueryString = null)
		{
			CrimsonReader<InvokeNowEntry> crimsonReader = new CrimsonReader<InvokeNowEntry>(null, null, "Microsoft-Exchange-ManagedAvailability/InvokeNowRequest");
			crimsonReader.IsReverseDirection = true;
			if (string.IsNullOrEmpty(xpathQueryString))
			{
				crimsonReader.QueryStartTime = new DateTime?(startTime);
				crimsonReader.QueryEndTime = new DateTime?(endTime);
			}
			else
			{
				crimsonReader.ExplicitXPathQuery = xpathQueryString;
			}
			return crimsonReader;
		}

		// Token: 0x04000A09 RID: 2569
		internal const string InvokeNowRequestChannelName = "Microsoft-Exchange-ManagedAvailability/InvokeNowRequest";

		// Token: 0x04000A0A RID: 2570
		internal const string InvokeNowResultChannelName = "Microsoft-Exchange-ManagedAvailability/InvokeNowResult";

		// Token: 0x04000A0B RID: 2571
		internal const int InvokeNowRequestUploadStarted = 2002;

		// Token: 0x04000A0C RID: 2572
		internal const int InvokeNowRequestUploadSucceeded = 2003;

		// Token: 0x04000A0D RID: 2573
		internal const int InvokeNowRequestUploadFailed = 2004;

		// Token: 0x020001E3 RID: 483
		internal enum InvokeNowChannelType
		{
			// Token: 0x04000A0F RID: 2575
			Request,
			// Token: 0x04000A10 RID: 2576
			Result
		}
	}
}
