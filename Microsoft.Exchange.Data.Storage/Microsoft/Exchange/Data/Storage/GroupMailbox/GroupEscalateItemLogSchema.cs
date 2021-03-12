using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.GroupMailbox
{
	// Token: 0x020007E5 RID: 2021
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class GroupEscalateItemLogSchema
	{
		// Token: 0x020007E6 RID: 2022
		internal enum OperationStart
		{
			// Token: 0x04002916 RID: 10518
			OperationName
		}

		// Token: 0x020007E7 RID: 2023
		internal enum Error
		{
			// Token: 0x04002918 RID: 10520
			Exception,
			// Token: 0x04002919 RID: 10521
			Context
		}

		// Token: 0x020007E8 RID: 2024
		internal enum OperationEnd
		{
			// Token: 0x0400291B RID: 10523
			OperationName,
			// Token: 0x0400291C RID: 10524
			Elapsed,
			// Token: 0x0400291D RID: 10525
			CPU,
			// Token: 0x0400291E RID: 10526
			RPCCount,
			// Token: 0x0400291F RID: 10527
			RPCLatency,
			// Token: 0x04002920 RID: 10528
			DirectoryCount,
			// Token: 0x04002921 RID: 10529
			DirectoryLatency,
			// Token: 0x04002922 RID: 10530
			StoreTimeInServer,
			// Token: 0x04002923 RID: 10531
			StoreTimeInCPU,
			// Token: 0x04002924 RID: 10532
			StorePagesRead,
			// Token: 0x04002925 RID: 10533
			StorePagesPreRead,
			// Token: 0x04002926 RID: 10534
			StoreLogRecords,
			// Token: 0x04002927 RID: 10535
			StoreLogBytes,
			// Token: 0x04002928 RID: 10536
			OrigMsgSender,
			// Token: 0x04002929 RID: 10537
			OrigMsgSndRcpType,
			// Token: 0x0400292A RID: 10538
			OrigMsgClass,
			// Token: 0x0400292B RID: 10539
			OrigMsgId,
			// Token: 0x0400292C RID: 10540
			OrigMsgInetId,
			// Token: 0x0400292D RID: 10541
			PartOrigMsg,
			// Token: 0x0400292E RID: 10542
			GroupReplyTo,
			// Token: 0x0400292F RID: 10543
			GroupPart,
			// Token: 0x04002930 RID: 10544
			EnsGroupPart,
			// Token: 0x04002931 RID: 10545
			DedupePart,
			// Token: 0x04002932 RID: 10546
			PartAddedEsc,
			// Token: 0x04002933 RID: 10547
			PartSkippedEsc,
			// Token: 0x04002934 RID: 10548
			HasEscalated,
			// Token: 0x04002935 RID: 10549
			GroupReplyToSkipped,
			// Token: 0x04002936 RID: 10550
			SendToYammer,
			// Token: 0x04002937 RID: 10551
			SendToYammerMs,
			// Token: 0x04002938 RID: 10552
			UnsubscribeUrl,
			// Token: 0x04002939 RID: 10553
			UnsubscribeUrlBuildMs,
			// Token: 0x0400293A RID: 10554
			UnsubscribeBodySize,
			// Token: 0x0400293B RID: 10555
			UnsubscribeUrlDetectionMs,
			// Token: 0x0400293C RID: 10556
			UnsubscribeUrlInsertMs
		}
	}
}
