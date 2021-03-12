using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Scheduler.Contracts;
using Microsoft.Exchange.Transport.Storage.Messaging;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200003C RID: 60
	internal class ProcessingSchedulerAdminWrapper : IProcessingSchedulerAdmin
	{
		// Token: 0x0600014C RID: 332 RVA: 0x00006580 File Offset: 0x00004780
		public ProcessingSchedulerAdminWrapper(IProcessingSchedulerAdmin processingSchedulerAdmin, IMessagingDatabaseComponent messagingDatabaseComponent)
		{
			ArgumentValidator.ThrowIfNull("processingSchedulerAdmin", processingSchedulerAdmin);
			ArgumentValidator.ThrowIfNull("messagingDatabaseComponent", messagingDatabaseComponent);
			this.processingSchedulerAdmin = processingSchedulerAdmin;
			this.queueStorage = messagingDatabaseComponent.GetOrAddQueue(NextHopSolutionKey.Submission);
			if (this.queueStorage.Suspended)
			{
				this.processingSchedulerAdmin.Pause();
				return;
			}
			this.processingSchedulerAdmin.Resume();
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600014D RID: 333 RVA: 0x000065E5 File Offset: 0x000047E5
		public bool IsRunning
		{
			get
			{
				return this.processingSchedulerAdmin.IsRunning;
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000065F2 File Offset: 0x000047F2
		public void Pause()
		{
			this.processingSchedulerAdmin.Pause();
			this.queueStorage.Suspended = true;
			this.queueStorage.Commit();
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00006616 File Offset: 0x00004816
		public void Resume()
		{
			this.processingSchedulerAdmin.Resume();
			this.queueStorage.Suspended = false;
			this.queueStorage.Commit();
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000663A File Offset: 0x0000483A
		public SchedulerDiagnosticsInfo GetDiagnosticsInfo()
		{
			return this.processingSchedulerAdmin.GetDiagnosticsInfo();
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00006647 File Offset: 0x00004847
		public bool Shutdown(int timeoutMilliseconds = -1)
		{
			return this.processingSchedulerAdmin.Shutdown(timeoutMilliseconds);
		}

		// Token: 0x040000B2 RID: 178
		private readonly IProcessingSchedulerAdmin processingSchedulerAdmin;

		// Token: 0x040000B3 RID: 179
		private readonly RoutedQueueBase queueStorage;
	}
}
