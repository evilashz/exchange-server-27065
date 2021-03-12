using System;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000216 RID: 534
	internal class DatabaseCheckPassiveCopyInspectorQueueLength : PassiveDatabaseCopyValidationCheck
	{
		// Token: 0x06001477 RID: 5239 RVA: 0x000524D3 File Offset: 0x000506D3
		public DatabaseCheckPassiveCopyInspectorQueueLength() : base(DatabaseValidationCheck.ID.DatabaseCheckPassiveCopyInspectorQueueLength)
		{
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x000524E0 File Offset: 0x000506E0
		protected override DatabaseValidationCheck.Result ValidateInternal(DatabaseValidationCheck.Arguments args, ref LocalizedString error)
		{
			RpcDatabaseCopyStatus2 copyStatus = args.Status.CopyStatus;
			long num = Math.Max(0L, copyStatus.LastLogCopied - copyStatus.LastLogInspected);
			if (num > (long)RegistryParameters.DatabaseCheckInspectorQueueLengthFailedThreshold)
			{
				error = ReplayStrings.DbValidationInspectorQueueLengthTooHigh(args.DatabaseCopyName, num, (long)RegistryParameters.DatabaseCheckInspectorQueueLengthFailedThreshold);
				return DatabaseValidationCheck.Result.Failed;
			}
			if (num > (long)RegistryParameters.DatabaseCheckInspectorQueueLengthWarningThreshold)
			{
				error = ReplayStrings.DbValidationInspectorQueueLengthTooHigh(args.DatabaseCopyName, num, (long)RegistryParameters.DatabaseCheckInspectorQueueLengthWarningThreshold);
				return DatabaseValidationCheck.Result.Warning;
			}
			return DatabaseValidationCheck.Result.Passed;
		}
	}
}
