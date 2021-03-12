using System;
using System.Collections.Generic;
using Microsoft.Exchange.PushNotifications.Utils;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000CE RID: 206
	internal class WebAppErrorTracker : ErrorTracker<WebAppErrorType>
	{
		// Token: 0x060006CA RID: 1738 RVA: 0x00015A46 File Offset: 0x00013C46
		public WebAppErrorTracker(int backOffTimeInSeconds) : base(WebAppErrorTracker.ErrorWeightTable, 10, backOffTimeInSeconds, 0)
		{
		}

		// Token: 0x0400036E RID: 878
		private const int MaxErrorWeight = 10;

		// Token: 0x0400036F RID: 879
		private static readonly Dictionary<WebAppErrorType, int> ErrorWeightTable = new Dictionary<WebAppErrorType, int>
		{
			{
				WebAppErrorType.Unknown,
				1
			}
		};
	}
}
