using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Config;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxLoadBalance.QueueProcessing.Rubs
{
	// Token: 0x020000EB RID: 235
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LoadBalanceWorkload : SystemWorkloadBase, IRequestQueueManager
	{
		// Token: 0x0600072B RID: 1835 RVA: 0x00014539 File Offset: 0x00012739
		public LoadBalanceWorkload(ILoadBalanceSettings settings)
		{
			this.queue = new RubsQueue(settings);
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x0001454D File Offset: 0x0001274D
		public override int BlockedTaskCount
		{
			get
			{
				return this.queue.BlockedTaskCount;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x0001455A File Offset: 0x0001275A
		public override string Id
		{
			get
			{
				return "Mailbox Load Balancing";
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x00014561 File Offset: 0x00012761
		public IRequestQueue MainProcessingQueue
		{
			get
			{
				return this.queue;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x00014569 File Offset: 0x00012769
		public override int TaskCount
		{
			get
			{
				return this.queue.TaskCount;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x00014576 File Offset: 0x00012776
		public override WorkloadType WorkloadType
		{
			get
			{
				return WorkloadType.MailboxReplicationServiceInternalMaintenance;
			}
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0001457C File Offset: 0x0001277C
		public QueueManagerDiagnosticData GetDiagnosticData(bool includeRequestDetails, bool includeRequestVerboseData)
		{
			QueueDiagnosticData diagnosticData = this.queue.GetDiagnosticData(includeRequestDetails, includeRequestVerboseData);
			return new QueueManagerDiagnosticData
			{
				ProcessingQueues = new List<QueueDiagnosticData>
				{
					diagnosticData
				}
			};
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x000145B4 File Offset: 0x000127B4
		public void Clean()
		{
			this.queue.Clean();
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x000145C1 File Offset: 0x000127C1
		public IRequestQueue GetInjectionQueue(DirectoryDatabase database)
		{
			return this.queue;
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x000145C9 File Offset: 0x000127C9
		public IRequestQueue GetProcessingQueue(LoadEntity loadObject)
		{
			return this.queue;
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x000145D1 File Offset: 0x000127D1
		public IRequestQueue GetProcessingQueue(DirectoryObject directoryObject)
		{
			return this.queue;
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x000145D9 File Offset: 0x000127D9
		protected override SystemTaskBase GetTask(ResourceReservationContext context)
		{
			return this.queue.GetTask(this, context);
		}

		// Token: 0x040002C7 RID: 711
		private readonly RubsQueue queue;
	}
}
