using System;
using Microsoft.Exchange.Common.HA;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x0200018A RID: 394
	internal class FailureItemPublisherImpl : IFailureItemPublisher
	{
		// Token: 0x06000FC7 RID: 4039 RVA: 0x00044731 File Offset: 0x00042931
		public void PublishAction(FailureTag failureTag, Guid databaseGuid, string dbInstanceName, IoErrorInfo ioErrorInfo)
		{
			FailureItemPublisherHelper.PublishAction(failureTag, databaseGuid, dbInstanceName, ioErrorInfo);
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x0004473D File Offset: 0x0004293D
		public void PublishAction(FailureTag failureTag, Guid databaseGuid, string dbInstanceName)
		{
			FailureItemPublisherHelper.PublishAction(failureTag, databaseGuid, dbInstanceName);
		}
	}
}
