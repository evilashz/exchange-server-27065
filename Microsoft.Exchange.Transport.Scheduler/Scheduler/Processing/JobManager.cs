using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Common;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x0200001A RID: 26
	internal sealed class JobManager
	{
		// Token: 0x0600006C RID: 108 RVA: 0x000034F0 File Offset: 0x000016F0
		public JobManager(ISchedulerMetering metering, Func<DateTime> timeProvider)
		{
			ArgumentValidator.ThrowIfNull("metering", metering);
			ArgumentValidator.ThrowIfNull("timeProvider", timeProvider);
			this.metering = metering;
			this.timeProvider = timeProvider;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003545 File Offset: 0x00001745
		public JobManager(ISchedulerMetering metering) : this(metering, () => DateTime.UtcNow)
		{
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600006E RID: 110 RVA: 0x0000356B File Offset: 0x0000176B
		public IEnumerable<JobInfo> CurrentJobs
		{
			get
			{
				return this.currentJobs.Values;
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003578 File Offset: 0x00001778
		public void Start(JobInfo jobInfo)
		{
			ArgumentValidator.ThrowIfNull("jobInfo", jobInfo);
			if (this.IsShutdown())
			{
				throw new InvalidProgramException("Job Manager is already shutdown, no new job allowed to be started");
			}
			TransportHelpers.AttemptAddToDictionary<Guid, JobInfo>(this.currentJobs, jobInfo.Id, jobInfo, null);
			this.metering.RecordStart(jobInfo.Scopes, 0L);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000035CC File Offset: 0x000017CC
		public void End(JobInfo jobInfo)
		{
			ArgumentValidator.ThrowIfNull("jobInfo", jobInfo);
			this.currentJobs.Remove(jobInfo.Id);
			this.metering.RecordEnd(jobInfo.Scopes, this.timeProvider() - jobInfo.StartTime);
			if (this.IsShutdown() && this.currentJobs.Count == 0)
			{
				this.shutdownEventSlim.Set();
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000363D File Offset: 0x0000183D
		internal void StartShutdown()
		{
			if (Interlocked.CompareExchange(ref this.shutdown, 1, 0) == 0 && this.currentJobs.Count == 0)
			{
				this.shutdownEventSlim.Set();
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003666 File Offset: 0x00001866
		internal bool WaitForShutdown(int timeoutMilliseconds = -1)
		{
			return this.shutdownEventSlim.Wait(timeoutMilliseconds);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003674 File Offset: 0x00001874
		private bool IsShutdown()
		{
			return this.shutdown == 1;
		}

		// Token: 0x04000041 RID: 65
		private readonly Func<DateTime> timeProvider;

		// Token: 0x04000042 RID: 66
		private readonly IDictionary<Guid, JobInfo> currentJobs = new Dictionary<Guid, JobInfo>();

		// Token: 0x04000043 RID: 67
		private readonly ISchedulerMetering metering;

		// Token: 0x04000044 RID: 68
		private readonly ManualResetEventSlim shutdownEventSlim = new ManualResetEventSlim(false);

		// Token: 0x04000045 RID: 69
		private int shutdown;
	}
}
