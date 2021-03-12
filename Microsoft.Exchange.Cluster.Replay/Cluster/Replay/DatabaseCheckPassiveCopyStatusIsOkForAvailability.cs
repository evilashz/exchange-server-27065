using System;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000211 RID: 529
	internal class DatabaseCheckPassiveCopyStatusIsOkForAvailability : PassiveDatabaseCopyValidationCheck
	{
		// Token: 0x0600146D RID: 5229 RVA: 0x00052219 File Offset: 0x00050419
		public DatabaseCheckPassiveCopyStatusIsOkForAvailability() : base(DatabaseValidationCheck.ID.DatabaseCheckPassiveCopyStatusIsOkForAvailability)
		{
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x00052224 File Offset: 0x00050424
		protected override DatabaseValidationCheck.Result ValidateInternal(DatabaseValidationCheck.Arguments args, ref LocalizedString error)
		{
			error = LocalizedString.Empty;
			if (!AmBcsCopyValidation.IsHealthyOrDisconnected(args.DatabaseName, args.Status.CopyStatus, args.TargetServer, ref error))
			{
				return DatabaseValidationCheck.Result.Failed;
			}
			return DatabaseValidationCheck.Result.Passed;
		}
	}
}
