using System;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002BA RID: 698
	internal class CDRPipelineStage : CreateUnProtectedMessageStage
	{
		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001526 RID: 5414 RVA: 0x0005ACD4 File Offset: 0x00058ED4
		internal override TimeSpan ExpectedRunTime
		{
			get
			{
				return TimeSpan.FromMinutes(10.0);
			}
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x0005ACE4 File Offset: 0x00058EE4
		protected override StageRetryDetails InternalGetRetrySchedule()
		{
			return new StageRetryDetails(StageRetryDetails.FinalAction.DropMessage, TimeSpan.FromMinutes(2.0), TimeSpan.FromDays(1.0));
		}
	}
}
