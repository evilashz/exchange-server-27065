using System;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000214 RID: 532
	internal class DatabaseCheckPassiveCopyRealCopyQueueLength : PassiveDatabaseCopyValidationCheck
	{
		// Token: 0x06001473 RID: 5235 RVA: 0x000523E4 File Offset: 0x000505E4
		public DatabaseCheckPassiveCopyRealCopyQueueLength() : base(DatabaseValidationCheck.ID.DatabaseCheckPassiveCopyRealCopyQueueLength)
		{
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x000523F0 File Offset: 0x000505F0
		protected override DatabaseValidationCheck.Result ValidateInternal(DatabaseValidationCheck.Arguments args, ref LocalizedString error)
		{
			error = LocalizedString.Empty;
			if (!AmBcsCopyValidation.IsRealCopyQueueLengthAcceptable(args.DatabaseName, args.Status.CopyStatus, RegistryParameters.MaxAutoDatabaseMountDial, args.TargetServer, ref error))
			{
				return DatabaseValidationCheck.Result.Failed;
			}
			return DatabaseValidationCheck.Result.Passed;
		}
	}
}
