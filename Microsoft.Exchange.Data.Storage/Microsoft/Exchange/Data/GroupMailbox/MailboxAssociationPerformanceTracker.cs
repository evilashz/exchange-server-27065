using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Optics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x020007FD RID: 2045
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MailboxAssociationPerformanceTracker : PerformanceTrackerBase, IMailboxAssociationPerformanceTracker, IMailboxPerformanceTracker, IPerformanceTracker
	{
		// Token: 0x06004C3E RID: 19518 RVA: 0x0013C32B File Offset: 0x0013A52B
		public void IncrementAssociationsRead()
		{
			this.associationsRead++;
		}

		// Token: 0x06004C3F RID: 19519 RVA: 0x0013C33B File Offset: 0x0013A53B
		public void IncrementAssociationsCreated()
		{
			this.associationsCreated++;
		}

		// Token: 0x06004C40 RID: 19520 RVA: 0x0013C34B File Offset: 0x0013A54B
		public void IncrementAssociationsUpdated()
		{
			this.associationsUpdated++;
		}

		// Token: 0x06004C41 RID: 19521 RVA: 0x0013C35B File Offset: 0x0013A55B
		public void IncrementAssociationsDeleted()
		{
			this.associationsDeleted++;
		}

		// Token: 0x06004C42 RID: 19522 RVA: 0x0013C36B File Offset: 0x0013A56B
		public void IncrementAssociationReplicationAttempts()
		{
			this.associationReplicationAttempts++;
		}

		// Token: 0x06004C43 RID: 19523 RVA: 0x0013C37B File Offset: 0x0013A57B
		public void IncrementFailedAssociationReplications()
		{
			this.failedAssociationReplications++;
		}

		// Token: 0x06004C44 RID: 19524 RVA: 0x0013C38B File Offset: 0x0013A58B
		public void IncrementFailedAssociationsSearch()
		{
			this.failedAssociationsSearch++;
		}

		// Token: 0x06004C45 RID: 19525 RVA: 0x0013C39B File Offset: 0x0013A59B
		public void IncrementNonUniqueAssociationsFound()
		{
			this.nonUniqueAssociationsFound++;
		}

		// Token: 0x06004C46 RID: 19526 RVA: 0x0013C3AB File Offset: 0x0013A5AB
		public void IncrementAutoSubscribedMembers()
		{
			this.autoSubscribedMembers++;
		}

		// Token: 0x06004C47 RID: 19527 RVA: 0x0013C3BB File Offset: 0x0013A5BB
		public void IncrementMissingLegacyDns()
		{
			this.missingLegacyDns++;
		}

		// Token: 0x06004C48 RID: 19528 RVA: 0x0013C3CB File Offset: 0x0013A5CB
		public void SetNewSessionRequired(bool isRequired)
		{
			this.isNewSessionRequired = isRequired;
		}

		// Token: 0x06004C49 RID: 19529 RVA: 0x0013C3D4 File Offset: 0x0013A5D4
		public void SetNewSessionWrongServer(bool isWrongServer)
		{
			this.isNewSessionWrongServer = isWrongServer;
		}

		// Token: 0x06004C4A RID: 19530 RVA: 0x0013C3DD File Offset: 0x0013A5DD
		public void SetNewSessionLatency(long milliseconds)
		{
			this.newSessionLatency = milliseconds;
		}

		// Token: 0x06004C4B RID: 19531 RVA: 0x0013C3E6 File Offset: 0x0013A5E6
		public void SetAADQueryLatency(long milliseconds)
		{
			this.aadQueryLatency = milliseconds;
		}

		// Token: 0x06004C4C RID: 19532 RVA: 0x0013C3F0 File Offset: 0x0013A5F0
		public ILogEvent GetLogEvent(string operationName)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("operationName", operationName);
			base.EnforceInternalState(PerformanceTrackerBase.InternalState.Stopped, "GetLogEvent");
			return new SchemaBasedLogEvent<MailboxAssociationLogSchema.OperationEnd>
			{
				{
					MailboxAssociationLogSchema.OperationEnd.OperationName,
					operationName
				},
				{
					MailboxAssociationLogSchema.OperationEnd.Elapsed,
					base.ElapsedTime.TotalMilliseconds
				},
				{
					MailboxAssociationLogSchema.OperationEnd.CPU,
					base.CpuTime.TotalMilliseconds
				},
				{
					MailboxAssociationLogSchema.OperationEnd.RPCCount,
					base.StoreRpcCount
				},
				{
					MailboxAssociationLogSchema.OperationEnd.RPCLatency,
					base.StoreRpcLatency.TotalMilliseconds
				},
				{
					MailboxAssociationLogSchema.OperationEnd.DirectoryCount,
					base.DirectoryCount
				},
				{
					MailboxAssociationLogSchema.OperationEnd.DirectoryLatency,
					base.DirectoryLatency.TotalMilliseconds
				},
				{
					MailboxAssociationLogSchema.OperationEnd.StoreTimeInServer,
					base.StoreTimeInServer.TotalMilliseconds
				},
				{
					MailboxAssociationLogSchema.OperationEnd.StoreTimeInCPU,
					base.StoreTimeInCPU.TotalMilliseconds
				},
				{
					MailboxAssociationLogSchema.OperationEnd.StorePagesRead,
					base.StorePagesRead
				},
				{
					MailboxAssociationLogSchema.OperationEnd.StorePagesPreRead,
					base.StorePagesPreread
				},
				{
					MailboxAssociationLogSchema.OperationEnd.StoreLogRecords,
					base.StoreLogRecords
				},
				{
					MailboxAssociationLogSchema.OperationEnd.StoreLogBytes,
					base.StoreLogBytes
				},
				{
					MailboxAssociationLogSchema.OperationEnd.NewSessionRequired,
					this.isNewSessionRequired
				},
				{
					MailboxAssociationLogSchema.OperationEnd.NewSessionWrong,
					this.isNewSessionWrongServer
				},
				{
					MailboxAssociationLogSchema.OperationEnd.NewSessionLatency,
					this.newSessionLatency
				},
				{
					MailboxAssociationLogSchema.OperationEnd.AssociationsRead,
					this.associationsRead
				},
				{
					MailboxAssociationLogSchema.OperationEnd.AssociationsCreated,
					this.associationsCreated
				},
				{
					MailboxAssociationLogSchema.OperationEnd.AssociationsUpdated,
					this.associationsUpdated
				},
				{
					MailboxAssociationLogSchema.OperationEnd.AssociationsDeleted,
					this.associationsDeleted
				},
				{
					MailboxAssociationLogSchema.OperationEnd.FailedAssociationsSearch,
					this.failedAssociationsSearch
				},
				{
					MailboxAssociationLogSchema.OperationEnd.MissingLegacyDns,
					this.missingLegacyDns
				},
				{
					MailboxAssociationLogSchema.OperationEnd.NonUniqueAssociationsFound,
					this.nonUniqueAssociationsFound
				},
				{
					MailboxAssociationLogSchema.OperationEnd.AutoSubscribedMembers,
					this.autoSubscribedMembers
				},
				{
					MailboxAssociationLogSchema.OperationEnd.AssociationReplicationAttempts,
					this.associationReplicationAttempts
				},
				{
					MailboxAssociationLogSchema.OperationEnd.FailedAssociationReplications,
					this.failedAssociationReplications
				},
				{
					MailboxAssociationLogSchema.OperationEnd.AADQueryLatency,
					this.aadQueryLatency
				}
			};
		}

		// Token: 0x040029A2 RID: 10658
		private int associationsRead;

		// Token: 0x040029A3 RID: 10659
		private int associationsCreated;

		// Token: 0x040029A4 RID: 10660
		private int associationsUpdated;

		// Token: 0x040029A5 RID: 10661
		private int associationsDeleted;

		// Token: 0x040029A6 RID: 10662
		private int associationReplicationAttempts;

		// Token: 0x040029A7 RID: 10663
		private int failedAssociationReplications;

		// Token: 0x040029A8 RID: 10664
		private int failedAssociationsSearch;

		// Token: 0x040029A9 RID: 10665
		private int nonUniqueAssociationsFound;

		// Token: 0x040029AA RID: 10666
		private int autoSubscribedMembers;

		// Token: 0x040029AB RID: 10667
		private int missingLegacyDns;

		// Token: 0x040029AC RID: 10668
		private bool isNewSessionRequired;

		// Token: 0x040029AD RID: 10669
		private bool isNewSessionWrongServer;

		// Token: 0x040029AE RID: 10670
		private long newSessionLatency;

		// Token: 0x040029AF RID: 10671
		private long aadQueryLatency;
	}
}
