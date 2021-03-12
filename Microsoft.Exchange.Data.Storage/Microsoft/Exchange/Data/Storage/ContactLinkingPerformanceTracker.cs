using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004C5 RID: 1221
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ContactLinkingPerformanceTracker : PerformanceTrackerBase, IContactLinkingPerformanceTracker, IPerformanceTracker
	{
		// Token: 0x0600359E RID: 13726 RVA: 0x000D7D95 File Offset: 0x000D5F95
		public ContactLinkingPerformanceTracker(IMailboxSession mailboxSession) : base(mailboxSession)
		{
		}

		// Token: 0x0600359F RID: 13727 RVA: 0x000D7D9E File Offset: 0x000D5F9E
		public void IncrementContactsCreated()
		{
			this.contactsCreated++;
		}

		// Token: 0x060035A0 RID: 13728 RVA: 0x000D7DAE File Offset: 0x000D5FAE
		public void IncrementContactsUpdated()
		{
			this.contactsUpdated++;
		}

		// Token: 0x060035A1 RID: 13729 RVA: 0x000D7DBE File Offset: 0x000D5FBE
		public void IncrementContactsRead()
		{
			this.contactsRead++;
		}

		// Token: 0x060035A2 RID: 13730 RVA: 0x000D7DCE File Offset: 0x000D5FCE
		public void IncrementContactsProcessed()
		{
			this.contactsProcessed++;
		}

		// Token: 0x060035A3 RID: 13731 RVA: 0x000D7DE0 File Offset: 0x000D5FE0
		public ILogEvent GetLogEvent()
		{
			base.EnforceInternalState(PerformanceTrackerBase.InternalState.Stopped, "GetLogEvent");
			return new SchemaBasedLogEvent<ContactLinkingLogSchema.PerformanceData>
			{
				{
					ContactLinkingLogSchema.PerformanceData.Elapsed,
					base.ElapsedTime.TotalMilliseconds
				},
				{
					ContactLinkingLogSchema.PerformanceData.CPU,
					base.CpuTime.TotalMilliseconds
				},
				{
					ContactLinkingLogSchema.PerformanceData.RPCCount,
					base.StoreRpcCount
				},
				{
					ContactLinkingLogSchema.PerformanceData.RPCLatency,
					base.StoreRpcLatency.TotalMilliseconds
				},
				{
					ContactLinkingLogSchema.PerformanceData.DirectoryCount,
					base.DirectoryCount
				},
				{
					ContactLinkingLogSchema.PerformanceData.DirectoryLatency,
					base.DirectoryLatency.TotalMilliseconds
				},
				{
					ContactLinkingLogSchema.PerformanceData.StoreTimeInServer,
					base.StoreTimeInServer.TotalMilliseconds
				},
				{
					ContactLinkingLogSchema.PerformanceData.StoreTimeInCPU,
					base.StoreTimeInCPU.TotalMilliseconds
				},
				{
					ContactLinkingLogSchema.PerformanceData.StorePagesRead,
					base.StorePagesRead
				},
				{
					ContactLinkingLogSchema.PerformanceData.StorePagesPreRead,
					base.StorePagesPreread
				},
				{
					ContactLinkingLogSchema.PerformanceData.StoreLogRecords,
					base.StoreLogRecords
				},
				{
					ContactLinkingLogSchema.PerformanceData.StoreLogBytes,
					base.StoreLogBytes
				},
				{
					ContactLinkingLogSchema.PerformanceData.ContactsCreated,
					this.contactsCreated
				},
				{
					ContactLinkingLogSchema.PerformanceData.ContactsUpdated,
					this.contactsUpdated
				},
				{
					ContactLinkingLogSchema.PerformanceData.ContactsRead,
					this.contactsRead
				},
				{
					ContactLinkingLogSchema.PerformanceData.ContactsProcessed,
					this.contactsProcessed
				}
			};
		}

		// Token: 0x04001CC8 RID: 7368
		private int contactsCreated;

		// Token: 0x04001CC9 RID: 7369
		private int contactsUpdated;

		// Token: 0x04001CCA RID: 7370
		private int contactsRead;

		// Token: 0x04001CCB RID: 7371
		private int contactsProcessed;
	}
}
