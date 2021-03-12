using System;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000213 RID: 531
	internal class DatabaseCheckPassiveCopyTotalQueueLength : PassiveDatabaseCopyValidationCheck
	{
		// Token: 0x06001471 RID: 5233 RVA: 0x0005239B File Offset: 0x0005059B
		public DatabaseCheckPassiveCopyTotalQueueLength() : base(DatabaseValidationCheck.ID.DatabaseCheckPassiveCopyTotalQueueLength)
		{
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x000523A8 File Offset: 0x000505A8
		protected override DatabaseValidationCheck.Result ValidateInternal(DatabaseValidationCheck.Arguments args, ref LocalizedString error)
		{
			error = LocalizedString.Empty;
			if (!AmBcsCopyValidation.IsTotalQueueLengthLessThanMaxThreshold(args.DatabaseName, args.Status.CopyStatus, args.TargetServer, ref error))
			{
				return DatabaseValidationCheck.Result.Failed;
			}
			return DatabaseValidationCheck.Result.Passed;
		}
	}
}
