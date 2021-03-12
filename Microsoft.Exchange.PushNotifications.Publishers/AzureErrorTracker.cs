using System;
using System.Collections.Generic;
using Microsoft.Exchange.PushNotifications.Utils;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000046 RID: 70
	internal class AzureErrorTracker : ErrorTracker<AzureErrorType>
	{
		// Token: 0x060002AA RID: 682 RVA: 0x00009F48 File Offset: 0x00008148
		public AzureErrorTracker(int backOffTimeInSeconds) : base(AzureErrorTracker.AzureErrorWeightTable, 60, backOffTimeInSeconds, 0)
		{
		}

		// Token: 0x04000126 RID: 294
		private const int MaxErrorWeight = 60;

		// Token: 0x04000127 RID: 295
		private static readonly Dictionary<AzureErrorType, int> AzureErrorWeightTable = new Dictionary<AzureErrorType, int>
		{
			{
				AzureErrorType.Unknown,
				30
			},
			{
				AzureErrorType.Transient,
				20
			},
			{
				AzureErrorType.Permanent,
				60
			}
		};
	}
}
