using System;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000210 RID: 528
	internal class DatabaseCheckActiveCopyNotActivationSuspended : ActiveDatabaseCopyValidationCheck
	{
		// Token: 0x0600146B RID: 5227 RVA: 0x000521AC File Offset: 0x000503AC
		public DatabaseCheckActiveCopyNotActivationSuspended() : base(DatabaseValidationCheck.ID.DatabaseCheckActiveCopyNotActivationSuspended)
		{
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x000521B8 File Offset: 0x000503B8
		protected override DatabaseValidationCheck.Result ValidateInternal(DatabaseValidationCheck.Arguments args, ref LocalizedString error)
		{
			AmServerName targetServer = args.TargetServer;
			RpcDatabaseCopyStatus2 copyStatus = args.Status.CopyStatus;
			if (copyStatus.ActivationSuspended)
			{
				error = ReplayStrings.AmBcsDatabaseCopyActivationSuspended(args.DatabaseName, targetServer.NetbiosName, string.IsNullOrEmpty(copyStatus.SuspendComment) ? ReplayStrings.AmBcsNoneSpecified : copyStatus.SuspendComment);
				return DatabaseValidationCheck.Result.Failed;
			}
			return DatabaseValidationCheck.Result.Passed;
		}
	}
}
