using System;
using System.Collections.Generic;
using Microsoft.Exchange.PushNotifications.Utils;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000C4 RID: 196
	internal class ProxyErrorTracker : ErrorTracker<ProxyErrorType>
	{
		// Token: 0x0600068F RID: 1679 RVA: 0x000151F0 File Offset: 0x000133F0
		public ProxyErrorTracker(int maxErrorWeight, int backOffTimeInSeconds, int baseDelayInMilliseconds) : base(ProxyErrorTracker.ProxyErrorWeightTable, maxErrorWeight * 20, backOffTimeInSeconds, baseDelayInMilliseconds)
		{
		}

		// Token: 0x0400034D RID: 845
		private const int UnknownErrorWeight = 30;

		// Token: 0x0400034E RID: 846
		private const int TransientErrorWeight = 20;

		// Token: 0x0400034F RID: 847
		private const int PermanentErrorWeight = 60;

		// Token: 0x04000350 RID: 848
		private static readonly Dictionary<ProxyErrorType, int> ProxyErrorWeightTable = new Dictionary<ProxyErrorType, int>
		{
			{
				ProxyErrorType.Unknown,
				30
			},
			{
				ProxyErrorType.Transient,
				20
			},
			{
				ProxyErrorType.Permanent,
				60
			}
		};
	}
}
