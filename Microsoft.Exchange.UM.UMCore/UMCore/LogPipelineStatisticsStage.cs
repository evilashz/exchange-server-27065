using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002C1 RID: 705
	internal class LogPipelineStatisticsStage : SynchronousPipelineStageBase
	{
		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x0600155B RID: 5467 RVA: 0x0005B6C5 File Offset: 0x000598C5
		internal override PipelineDispatcher.PipelineResourceType ResourceType
		{
			get
			{
				return PipelineDispatcher.PipelineResourceType.CpuBound;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x0600155C RID: 5468 RVA: 0x0005B6C8 File Offset: 0x000598C8
		internal override TimeSpan ExpectedRunTime
		{
			get
			{
				return TimeSpan.FromMinutes(1.0);
			}
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x0005B6D8 File Offset: 0x000598D8
		protected override void InternalDoSynchronousWork()
		{
			IUMResolveCaller iumresolveCaller = base.WorkItem.Message as IUMResolveCaller;
			if (iumresolveCaller != null)
			{
				base.WorkItem.PipelineStatisticsLogRow.CallerName = iumresolveCaller.ContactInfo.DisplayName;
			}
			IUMCAMessage iumcamessage = base.WorkItem.Message as IUMCAMessage;
			if (iumcamessage != null)
			{
				base.WorkItem.PipelineStatisticsLogRow.CalleeAlias = iumcamessage.CAMessageRecipient.ADRecipient.Alias;
				base.WorkItem.PipelineStatisticsLogRow.OrganizationId = Util.GetTenantName(iumcamessage.CAMessageRecipient.ADRecipient);
			}
			PipelineStatisticsLogger.Instance.Append(base.WorkItem.PipelineStatisticsLogRow);
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x0005B77D File Offset: 0x0005997D
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<LogPipelineStatisticsStage>(this);
		}
	}
}
