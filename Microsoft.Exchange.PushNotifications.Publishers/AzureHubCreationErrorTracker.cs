using System;
using System.Collections.Generic;
using Microsoft.Exchange.PushNotifications.Utils;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000081 RID: 129
	internal class AzureHubCreationErrorTracker : ErrorTracker<AzureHubCreationErrorType>
	{
		// Token: 0x06000487 RID: 1159 RVA: 0x0000F31C File Offset: 0x0000D51C
		public AzureHubCreationErrorTracker(int baseDelayInMilliseconds, int backOffTimeInSeconds) : base(AzureHubCreationErrorTracker.AzureErrorWeightTable, 60, backOffTimeInSeconds, baseDelayInMilliseconds)
		{
		}

		// Token: 0x0400023A RID: 570
		private const int MaxErrorWeight = 60;

		// Token: 0x0400023B RID: 571
		private static readonly Dictionary<AzureHubCreationErrorType, int> AzureErrorWeightTable = new Dictionary<AzureHubCreationErrorType, int>
		{
			{
				AzureHubCreationErrorType.Unknown,
				30
			},
			{
				AzureHubCreationErrorType.Unauthorized,
				20
			},
			{
				AzureHubCreationErrorType.Permanent,
				60
			}
		};
	}
}
