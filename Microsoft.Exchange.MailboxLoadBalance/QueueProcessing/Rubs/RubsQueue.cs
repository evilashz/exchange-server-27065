using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Config;
using Microsoft.Exchange.MailboxLoadBalance.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Logging;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxLoadBalance.QueueProcessing.Rubs
{
	// Token: 0x020000EC RID: 236
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RubsQueue : IRequestQueue
	{
		// Token: 0x06000737 RID: 1847 RVA: 0x000145E8 File Offset: 0x000127E8
		public RubsQueue(ILoadBalanceSettings settings)
		{
			this.settings = settings;
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x0001460D File Offset: 0x0001280D
		// (set) Token: 0x06000739 RID: 1849 RVA: 0x00014615 File Offset: 0x00012815
		public int BlockedTaskCount { get; private set; }

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x0001461E File Offset: 0x0001281E
		public Guid Id
		{
			get
			{
				return Guid.Empty;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x00014625 File Offset: 0x00012825
		public int TaskCount
		{
			get
			{
				return this.requests.Count;
			}
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00014634 File Offset: 0x00012834
		public void Clean()
		{
			lock (this.requestQueueLock)
			{
				this.requests.Clear();
			}
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0001467C File Offset: 0x0001287C
		public void EnqueueRequest(IRequest request)
		{
			lock (this.requestQueueLock)
			{
				request.AssignQueue(this);
				this.requests.Add(request);
			}
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x000146E4 File Offset: 0x000128E4
		public QueueDiagnosticData GetDiagnosticData(bool includeRequestDetails, bool includeRequestVerboseDiagnostics)
		{
			QueueDiagnosticData queueDiagnosticData = new QueueDiagnosticData
			{
				QueueGuid = Guid.Empty,
				QueueLength = this.requests.Count,
				IsActive = true
			};
			queueDiagnosticData.CurrentRequest = null;
			if (includeRequestDetails)
			{
				lock (this.requestQueueLock)
				{
					queueDiagnosticData.Requests = (from request in this.requests
					select request.GetDiagnosticData(includeRequestVerboseDiagnostics)).ToList<RequestDiagnosticData>();
				}
			}
			return queueDiagnosticData;
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00014790 File Offset: 0x00012990
		public SystemTaskBase GetTask(LoadBalanceWorkload workload, ResourceReservationContext context)
		{
			lock (this.requestQueueLock)
			{
				this.BlockedTaskCount = 0;
				if (this.requests.Count == 0)
				{
					return null;
				}
				for (int i = 0; i < this.requests.Count; i++)
				{
					IRequest request = this.requests[i];
					if (request.IsBlocked)
					{
						this.BlockedTaskCount++;
					}
					else if (request.ShouldCancel(this.settings.IdleRunDelay))
					{
						request.Abort();
						DatabaseRequestLog.Write(request);
						this.requests.RemoveAt(i);
						i--;
					}
					else
					{
						ResourceKey obj2;
						ResourceReservation reservation = context.GetReservation(workload, request.Resources, out obj2);
						if (reservation != null)
						{
							this.requests.RemoveAt(i);
							return new LoadBalanceTask(workload, reservation, request);
						}
						if (ProcessorResourceKey.Local.Equals(obj2))
						{
							this.BlockedTaskCount = this.requests.Count;
							break;
						}
						this.BlockedTaskCount++;
					}
				}
			}
			return null;
		}

		// Token: 0x040002C8 RID: 712
		private readonly object requestQueueLock = new object();

		// Token: 0x040002C9 RID: 713
		private readonly List<IRequest> requests = new List<IRequest>();

		// Token: 0x040002CA RID: 714
		private readonly ILoadBalanceSettings settings;
	}
}
