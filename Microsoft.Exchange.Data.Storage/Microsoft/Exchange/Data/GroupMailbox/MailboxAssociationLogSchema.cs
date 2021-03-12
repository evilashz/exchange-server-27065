using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x020007F5 RID: 2037
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class MailboxAssociationLogSchema
	{
		// Token: 0x020007F6 RID: 2038
		internal enum OperationStart
		{
			// Token: 0x04002975 RID: 10613
			OperationName
		}

		// Token: 0x020007F7 RID: 2039
		internal enum CommandExecution
		{
			// Token: 0x04002977 RID: 10615
			Command,
			// Token: 0x04002978 RID: 10616
			GroupMailbox,
			// Token: 0x04002979 RID: 10617
			UserMailboxes
		}

		// Token: 0x020007F8 RID: 2040
		internal enum PerformanceCounter
		{
			// Token: 0x0400297B RID: 10619
			CounterName,
			// Token: 0x0400297C RID: 10620
			Context,
			// Token: 0x0400297D RID: 10621
			Latency
		}

		// Token: 0x020007F9 RID: 2041
		internal enum PerformanceCounterName
		{
			// Token: 0x0400297F RID: 10623
			JoinGroupAssociationReplication
		}

		// Token: 0x020007FA RID: 2042
		internal enum Warning
		{
			// Token: 0x04002981 RID: 10625
			Message,
			// Token: 0x04002982 RID: 10626
			Context
		}

		// Token: 0x020007FB RID: 2043
		internal enum Error
		{
			// Token: 0x04002984 RID: 10628
			Exception,
			// Token: 0x04002985 RID: 10629
			Context
		}

		// Token: 0x020007FC RID: 2044
		internal enum OperationEnd
		{
			// Token: 0x04002987 RID: 10631
			OperationName,
			// Token: 0x04002988 RID: 10632
			Elapsed,
			// Token: 0x04002989 RID: 10633
			CPU,
			// Token: 0x0400298A RID: 10634
			RPCCount,
			// Token: 0x0400298B RID: 10635
			RPCLatency,
			// Token: 0x0400298C RID: 10636
			DirectoryCount,
			// Token: 0x0400298D RID: 10637
			DirectoryLatency,
			// Token: 0x0400298E RID: 10638
			StoreTimeInServer,
			// Token: 0x0400298F RID: 10639
			StoreTimeInCPU,
			// Token: 0x04002990 RID: 10640
			StorePagesRead,
			// Token: 0x04002991 RID: 10641
			StorePagesPreRead,
			// Token: 0x04002992 RID: 10642
			StoreLogRecords,
			// Token: 0x04002993 RID: 10643
			StoreLogBytes,
			// Token: 0x04002994 RID: 10644
			NewSessionRequired,
			// Token: 0x04002995 RID: 10645
			NewSessionWrong,
			// Token: 0x04002996 RID: 10646
			NewSessionLatency,
			// Token: 0x04002997 RID: 10647
			AssociationsRead,
			// Token: 0x04002998 RID: 10648
			AssociationsCreated,
			// Token: 0x04002999 RID: 10649
			AssociationsUpdated,
			// Token: 0x0400299A RID: 10650
			AssociationsDeleted,
			// Token: 0x0400299B RID: 10651
			FailedAssociationsSearch,
			// Token: 0x0400299C RID: 10652
			MissingLegacyDns,
			// Token: 0x0400299D RID: 10653
			NonUniqueAssociationsFound,
			// Token: 0x0400299E RID: 10654
			AutoSubscribedMembers,
			// Token: 0x0400299F RID: 10655
			AssociationReplicationAttempts,
			// Token: 0x040029A0 RID: 10656
			FailedAssociationReplications,
			// Token: 0x040029A1 RID: 10657
			AADQueryLatency
		}
	}
}
