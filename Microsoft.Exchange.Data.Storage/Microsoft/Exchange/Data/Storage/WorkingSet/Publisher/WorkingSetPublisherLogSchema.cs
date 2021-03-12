using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.WorkingSet.Publisher
{
	// Token: 0x02000EEF RID: 3823
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class WorkingSetPublisherLogSchema
	{
		// Token: 0x02000EF0 RID: 3824
		internal enum OperationStart
		{
			// Token: 0x0400582C RID: 22572
			OperationName
		}

		// Token: 0x02000EF1 RID: 3825
		internal enum CommandExecution
		{
			// Token: 0x0400582E RID: 22574
			Command,
			// Token: 0x0400582F RID: 22575
			GroupMailbox,
			// Token: 0x04005830 RID: 22576
			UserMailboxes
		}

		// Token: 0x02000EF2 RID: 3826
		internal enum Error
		{
			// Token: 0x04005832 RID: 22578
			Exception,
			// Token: 0x04005833 RID: 22579
			Context
		}

		// Token: 0x02000EF3 RID: 3827
		internal enum OperationEnd
		{
			// Token: 0x04005835 RID: 22581
			OperationName,
			// Token: 0x04005836 RID: 22582
			Elapsed,
			// Token: 0x04005837 RID: 22583
			CPU,
			// Token: 0x04005838 RID: 22584
			RPCCount,
			// Token: 0x04005839 RID: 22585
			RPCLatency,
			// Token: 0x0400583A RID: 22586
			DirectoryCount,
			// Token: 0x0400583B RID: 22587
			DirectoryLatency,
			// Token: 0x0400583C RID: 22588
			StoreTimeInServer,
			// Token: 0x0400583D RID: 22589
			StoreTimeInCPU,
			// Token: 0x0400583E RID: 22590
			StorePagesRead,
			// Token: 0x0400583F RID: 22591
			StorePagesPreRead,
			// Token: 0x04005840 RID: 22592
			StoreLogRecords,
			// Token: 0x04005841 RID: 22593
			StoreLogBytes,
			// Token: 0x04005842 RID: 22594
			OrigMsgSender,
			// Token: 0x04005843 RID: 22595
			OrigMsgSndRcpType,
			// Token: 0x04005844 RID: 22596
			OrigMsgClass,
			// Token: 0x04005845 RID: 22597
			OrigMsgId,
			// Token: 0x04005846 RID: 22598
			OrigMsgInetId,
			// Token: 0x04005847 RID: 22599
			PartOrigMsg,
			// Token: 0x04005848 RID: 22600
			DedupePart,
			// Token: 0x04005849 RID: 22601
			GroupPart,
			// Token: 0x0400584A RID: 22602
			EnsGroupPart,
			// Token: 0x0400584B RID: 22603
			PartAddedPub,
			// Token: 0x0400584C RID: 22604
			PartSkippedPub,
			// Token: 0x0400584D RID: 22605
			PubMsgId,
			// Token: 0x0400584E RID: 22606
			PubMsgInetId,
			// Token: 0x0400584F RID: 22607
			HasWorkingSet,
			// Token: 0x04005850 RID: 22608
			Exception
		}
	}
}
