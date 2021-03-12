using System;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x0200011A RID: 282
	[Serializable]
	public class SyncAgentConfiguration
	{
		// Token: 0x060007CE RID: 1998 RVA: 0x00017B4C File Offset: 0x00015D4C
		public SyncAgentConfiguration(int maxOperationalRetryTimes, Workload workload, bool asyncCallSyncSvc, int ioBatchSize, int maxSyncWorkItemsPerJob, int maxPublishWorkItemsPerJob, TimeSpan jobDispatcherWaitIntervalWhenStarve, int parallelJobDispatcherMaxPendingJobNumber, int maxQueueLength, bool reEnqueueNonSuccessWorkItem, TimeSpan? dispatcherTriggerInterval, string certificateSubject, string partnerName, bool enablePolicyApplication, TimeSpan workItemExecuteDelayTime, TimeSpan policySyncSLA, bool enableMonitor, string workItemRetryStrategy = null)
		{
			ArgumentValidator.ThrowIfNegative("maxOperationalRetryTimes", maxOperationalRetryTimes);
			ArgumentValidator.ThrowIfZeroOrNegative("ioBatchSize", ioBatchSize);
			ArgumentValidator.ThrowIfZeroOrNegative("maxSyncWorkItemsPerJob", maxSyncWorkItemsPerJob);
			ArgumentValidator.ThrowIfZeroOrNegative("maxPublishWorkItemsPerJob", maxPublishWorkItemsPerJob);
			ArgumentValidator.ThrowIfNegativeTimeSpan("jobDispatcherWaitIntervalWhenStarve", jobDispatcherWaitIntervalWhenStarve);
			ArgumentValidator.ThrowIfZeroOrNegative("parallelJobDispatcherMaxPendingJobNumber", parallelJobDispatcherMaxPendingJobNumber);
			ArgumentValidator.ThrowIfZeroOrNegative("maxQueueLength", maxQueueLength);
			ArgumentValidator.ThrowIfNegativeTimeSpan("workItemExecuteDelayTime", workItemExecuteDelayTime);
			ArgumentValidator.ThrowIfNegativeTimeSpan("policySyncSLA", policySyncSLA);
			if (dispatcherTriggerInterval != null)
			{
				ArgumentValidator.ThrowIfNegativeTimeSpan("dispatcherTriggerInterval", dispatcherTriggerInterval.Value);
			}
			this.MaxOperationalRetryTimes = maxOperationalRetryTimes;
			this.WorkLoad = workload;
			this.AsyncCallSyncSvc = asyncCallSyncSvc;
			this.IoBatchSize = ioBatchSize;
			this.MaxSyncWorkItemsPerJob = maxSyncWorkItemsPerJob;
			this.MaxPublishWorkItemsPerJob = maxPublishWorkItemsPerJob;
			this.JobDispatcherWaitIntervalWhenStarve = jobDispatcherWaitIntervalWhenStarve;
			this.ParallelJobDispatcherMaxPendingJobNumber = parallelJobDispatcherMaxPendingJobNumber;
			this.MaxQueueLength = maxQueueLength;
			this.ReEnqueueNonSuccessWorkItem = reEnqueueNonSuccessWorkItem;
			this.DispatcherTriggerInterval = dispatcherTriggerInterval;
			this.CertificateSubject = certificateSubject;
			this.PartnerName = partnerName;
			this.EnablePolicyApplication = enablePolicyApplication;
			this.strWorkItemRetryStrategy = workItemRetryStrategy;
			this.WorkItemExecuteDelayTime = workItemExecuteDelayTime;
			this.PolicySyncSLA = policySyncSLA;
			this.EnableMonitor = enableMonitor;
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00017C71 File Offset: 0x00015E71
		protected SyncAgentConfiguration()
		{
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x00017C79 File Offset: 0x00015E79
		// (set) Token: 0x060007D1 RID: 2001 RVA: 0x00017C81 File Offset: 0x00015E81
		public int MaxOperationalRetryTimes { get; protected set; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x00017C8A File Offset: 0x00015E8A
		// (set) Token: 0x060007D3 RID: 2003 RVA: 0x00017C92 File Offset: 0x00015E92
		public Workload WorkLoad { get; protected set; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x00017C9B File Offset: 0x00015E9B
		// (set) Token: 0x060007D5 RID: 2005 RVA: 0x00017CA3 File Offset: 0x00015EA3
		public bool AsyncCallSyncSvc { get; protected set; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x00017CAC File Offset: 0x00015EAC
		// (set) Token: 0x060007D7 RID: 2007 RVA: 0x00017CB4 File Offset: 0x00015EB4
		public int IoBatchSize { get; protected set; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x00017CBD File Offset: 0x00015EBD
		// (set) Token: 0x060007D9 RID: 2009 RVA: 0x00017CC5 File Offset: 0x00015EC5
		public int MaxSyncWorkItemsPerJob { get; protected set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x00017CCE File Offset: 0x00015ECE
		// (set) Token: 0x060007DB RID: 2011 RVA: 0x00017CD6 File Offset: 0x00015ED6
		public int MaxPublishWorkItemsPerJob { get; protected set; }

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x00017CDF File Offset: 0x00015EDF
		// (set) Token: 0x060007DD RID: 2013 RVA: 0x00017CE7 File Offset: 0x00015EE7
		public string CertificateSubject { get; protected set; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x00017CF0 File Offset: 0x00015EF0
		// (set) Token: 0x060007DF RID: 2015 RVA: 0x00017CF8 File Offset: 0x00015EF8
		public string PartnerName { get; protected set; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x00017D01 File Offset: 0x00015F01
		// (set) Token: 0x060007E1 RID: 2017 RVA: 0x00017D09 File Offset: 0x00015F09
		public TimeSpan JobDispatcherWaitIntervalWhenStarve { get; protected set; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x00017D12 File Offset: 0x00015F12
		// (set) Token: 0x060007E3 RID: 2019 RVA: 0x00017D1A File Offset: 0x00015F1A
		public int ParallelJobDispatcherMaxPendingJobNumber { get; protected set; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x00017D23 File Offset: 0x00015F23
		// (set) Token: 0x060007E5 RID: 2021 RVA: 0x00017D2B File Offset: 0x00015F2B
		public int MaxQueueLength { get; protected set; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x00017D34 File Offset: 0x00015F34
		// (set) Token: 0x060007E7 RID: 2023 RVA: 0x00017D3C File Offset: 0x00015F3C
		public bool ReEnqueueNonSuccessWorkItem { get; protected set; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x00017D45 File Offset: 0x00015F45
		// (set) Token: 0x060007E9 RID: 2025 RVA: 0x00017D4D File Offset: 0x00015F4D
		public TimeSpan? DispatcherTriggerInterval { get; protected set; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x00017D56 File Offset: 0x00015F56
		// (set) Token: 0x060007EB RID: 2027 RVA: 0x00017D5E File Offset: 0x00015F5E
		public TimeSpan WorkItemExecuteDelayTime { get; protected set; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x00017D67 File Offset: 0x00015F67
		// (set) Token: 0x060007ED RID: 2029 RVA: 0x00017D6F File Offset: 0x00015F6F
		public bool EnablePolicyApplication { get; protected set; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x00017D78 File Offset: 0x00015F78
		// (set) Token: 0x060007EF RID: 2031 RVA: 0x00017D80 File Offset: 0x00015F80
		public TimeSpan PolicySyncSLA { get; protected set; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x00017D89 File Offset: 0x00015F89
		// (set) Token: 0x060007F1 RID: 2033 RVA: 0x00017D91 File Offset: 0x00015F91
		public bool EnableMonitor { get; protected set; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x00017D9A File Offset: 0x00015F9A
		public RetryStrategy RetryStrategy
		{
			get
			{
				if (this.omWorkItemRetryStrategy == null)
				{
					this.omWorkItemRetryStrategy = new RetryStrategy(this.strWorkItemRetryStrategy);
				}
				return this.omWorkItemRetryStrategy;
			}
		}

		// Token: 0x17000238 RID: 568
		// (set) Token: 0x060007F3 RID: 2035 RVA: 0x00017DBB File Offset: 0x00015FBB
		protected string WorkItemRetryStrategy
		{
			set
			{
				this.strWorkItemRetryStrategy = value;
			}
		}

		// Token: 0x04000445 RID: 1093
		private RetryStrategy omWorkItemRetryStrategy;

		// Token: 0x04000446 RID: 1094
		private string strWorkItemRetryStrategy;
	}
}
