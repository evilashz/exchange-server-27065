using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200009B RID: 155
	internal abstract class LightJobBase : DisposeTrackableBase
	{
		// Token: 0x060007E2 RID: 2018 RVA: 0x00036CE4 File Offset: 0x00034EE4
		public LightJobBase(Guid requestGuid, Guid requestQueueGuid, MapiStore systemMailbox, byte[] messageId)
		{
			this.RequestGuid = requestGuid;
			this.RequestQueueGuid = requestQueueGuid;
			this.SystemMailbox = systemMailbox;
			this.MessageId = messageId;
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x00036D09 File Offset: 0x00034F09
		// (set) Token: 0x060007E4 RID: 2020 RVA: 0x00036D11 File Offset: 0x00034F11
		protected Guid RequestGuid { get; set; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x00036D1A File Offset: 0x00034F1A
		// (set) Token: 0x060007E6 RID: 2022 RVA: 0x00036D22 File Offset: 0x00034F22
		protected virtual Guid RequestQueueGuid { get; set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x00036D2B File Offset: 0x00034F2B
		// (set) Token: 0x060007E8 RID: 2024 RVA: 0x00036D33 File Offset: 0x00034F33
		protected virtual MapiStore SystemMailbox { get; set; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x00036D3C File Offset: 0x00034F3C
		// (set) Token: 0x060007EA RID: 2026 RVA: 0x00036D44 File Offset: 0x00034F44
		protected virtual byte[] MessageId { get; set; }

		// Token: 0x060007EB RID: 2027 RVA: 0x00036D4D File Offset: 0x00034F4D
		public virtual void Run()
		{
			this.RelinquishRequest();
		}

		// Token: 0x060007EC RID: 2028
		protected abstract RequestState RelinquishAction(TransactionalRequestJob requestJob, ReportData report);

		// Token: 0x060007ED RID: 2029 RVA: 0x00036D55 File Offset: 0x00034F55
		protected virtual void AfterRelinquishAction()
		{
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00036D57 File Offset: 0x00034F57
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00036D59 File Offset: 0x00034F59
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<LightJobBase>(this);
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00036E8C File Offset: 0x0003508C
		protected void RelinquishRequest()
		{
			using (RequestJobProvider rjProvider = new RequestJobProvider(this.RequestQueueGuid, this.SystemMailbox))
			{
				MapiUtils.RetryOnObjectChanged(delegate
				{
					using (TransactionalRequestJob transactionalRequestJob = (TransactionalRequestJob)rjProvider.Read<TransactionalRequestJob>(new RequestJobObjectId(this.RequestGuid, this.RequestQueueGuid, this.MessageId)))
					{
						if (transactionalRequestJob != null)
						{
							ReportData report = new ReportData(transactionalRequestJob.IdentifyingGuid, transactionalRequestJob.ReportVersion);
							transactionalRequestJob.RequestJobState = JobProcessingState.Ready;
							transactionalRequestJob.MRSServerName = null;
							transactionalRequestJob.TimeTracker.CurrentState = this.RelinquishAction(transactionalRequestJob, report);
							report.Append(MrsStrings.ReportRelinquishingJob);
							rjProvider.Save(transactionalRequestJob);
							CommonUtils.CatchKnownExceptions(delegate
							{
								report.Flush(rjProvider.SystemMailbox);
							}, null);
							transactionalRequestJob.UpdateAsyncNotification(report);
							this.AfterRelinquishAction();
						}
					}
				});
			}
		}
	}
}
