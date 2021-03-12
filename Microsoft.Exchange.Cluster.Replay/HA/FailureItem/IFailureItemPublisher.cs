using System;
using Microsoft.Exchange.Common.HA;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x02000189 RID: 393
	internal interface IFailureItemPublisher
	{
		// Token: 0x06000FC5 RID: 4037
		void PublishAction(FailureTag failureTag, Guid databaseGuid, string dbInstanceName, IoErrorInfo ioErrorInfo);

		// Token: 0x06000FC6 RID: 4038
		void PublishAction(FailureTag failureTag, Guid databaseGuid, string dbInstanceName);
	}
}
