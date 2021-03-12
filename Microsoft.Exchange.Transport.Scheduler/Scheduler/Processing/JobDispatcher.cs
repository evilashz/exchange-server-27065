using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Scheduler.Contracts;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x02000018 RID: 24
	internal class JobDispatcher
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00002ED0 File Offset: 0x000010D0
		public JobDispatcher(int concurrencyLevel, IMessageProcessor processor, ISchedulerMetering metering, ISchedulerDiagnostics schedulerDiagnostics, Func<DateTime> timeProvider)
		{
			ArgumentValidator.ThrowIfInvalidValue<int>("concurrencyLevel", concurrencyLevel, (int count) => count > 0);
			ArgumentValidator.ThrowIfNull("processor", processor);
			ArgumentValidator.ThrowIfNull("metering", metering);
			ArgumentValidator.ThrowIfNull("timeProvider", timeProvider);
			ArgumentValidator.ThrowIfNull("schedulerDiagnostics", schedulerDiagnostics);
			this.processor = processor;
			this.schedulerDiagnostics = schedulerDiagnostics;
			this.concurrencySemaphore = new SemaphoreSlim(concurrencyLevel);
			this.jobManager = new JobManager(metering, timeProvider);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002F8A File Offset: 0x0000118A
		public JobDispatcher(int concurrencyLevel, IMessageProcessor processor, ISchedulerMetering metering, ISchedulerDiagnostics schedulerDiagnostics) : this(concurrencyLevel, processor, metering, schedulerDiagnostics, () => DateTime.UtcNow)
		{
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002FBC File Offset: 0x000011BC
		public void Start(IProcessingScheduler processingScheduler)
		{
			ArgumentValidator.ThrowIfNull("processingScheduler", processingScheduler);
			this.scheduler = processingScheduler;
			Task.Run(delegate()
			{
				this.RunAsync();
			});
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002FE2 File Offset: 0x000011E2
		public void WorkAvailable()
		{
			this.newWorkResetEvent.Set();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002FEF File Offset: 0x000011EF
		public void EnqueueCommand(ISchedulerCommand command)
		{
			ArgumentValidator.ThrowIfNull("command", command);
			this.commands.Enqueue(command);
			this.WorkAvailable();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003444 File Offset: 0x00001644
		public async void RunAsync()
		{
			while (await this.newWorkResetEvent.WaitAsync())
			{
				for (;;)
				{
					JobDispatcher.<>c__DisplayClass6 CS$<>8__locals1 = new JobDispatcher.<>c__DisplayClass6();
					ISchedulerCommand command;
					while (this.commands.TryDequeue(out command))
					{
						command.Execute();
					}
					await this.concurrencySemaphore.WaitAsync();
					this.concurrencySemaphore.Release();
					JobInfo completedJob;
					while (this.jobsCompletedQueue.TryDequeue(out completedJob))
					{
						await completedJob.ExecutingTask;
						this.schedulerDiagnostics.Processed();
						this.jobManager.End(completedJob);
					}
					if (!this.scheduler.TryGetNext(out CS$<>8__locals1.message))
					{
						break;
					}
					JobInfo jobInfo = new JobInfo(DateTime.UtcNow, CS$<>8__locals1.message.Scopes);
					await this.concurrencySemaphore.WaitAsync();
					this.schedulerDiagnostics.Dispatched();
					this.jobManager.Start(jobInfo);
					Task task = new Task(delegate()
					{
						try
						{
							this.processor.Process(CS$<>8__locals1.message);
						}
						finally
						{
							this.concurrencySemaphore.Release();
							this.jobsCompletedQueue.Enqueue(jobInfo);
							this.WorkAvailable();
						}
					});
					jobInfo.ExecutingTask = task;
					task.Start();
				}
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000347E File Offset: 0x0000167E
		internal void StartShutdown()
		{
			this.jobManager.StartShutdown();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000348B File Offset: 0x0000168B
		internal bool WaitForShutdown(int timeoutMilliseconds = -1)
		{
			return this.jobManager.WaitForShutdown(timeoutMilliseconds);
		}

		// Token: 0x04000033 RID: 51
		private readonly JobManager jobManager;

		// Token: 0x04000034 RID: 52
		private readonly ConcurrentQueue<JobInfo> jobsCompletedQueue = new ConcurrentQueue<JobInfo>();

		// Token: 0x04000035 RID: 53
		private readonly ConcurrentQueue<ISchedulerCommand> commands = new ConcurrentQueue<ISchedulerCommand>();

		// Token: 0x04000036 RID: 54
		private readonly AsyncAutoResetEvent newWorkResetEvent = new AsyncAutoResetEvent();

		// Token: 0x04000037 RID: 55
		private readonly SemaphoreSlim concurrencySemaphore;

		// Token: 0x04000038 RID: 56
		private readonly IMessageProcessor processor;

		// Token: 0x04000039 RID: 57
		private readonly ISchedulerDiagnostics schedulerDiagnostics;

		// Token: 0x0400003A RID: 58
		private IProcessingScheduler scheduler;
	}
}
