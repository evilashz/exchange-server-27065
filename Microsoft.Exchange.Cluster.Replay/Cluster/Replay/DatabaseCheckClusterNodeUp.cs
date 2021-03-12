using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200020A RID: 522
	internal class DatabaseCheckClusterNodeUp : ActiveOrPassiveDatabaseCopyValidationCheck
	{
		// Token: 0x0600145E RID: 5214 RVA: 0x00051D5B File Offset: 0x0004FF5B
		public DatabaseCheckClusterNodeUp() : base(DatabaseValidationCheck.ID.DatabaseCheckClusterNodeUp)
		{
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x00051D64 File Offset: 0x0004FF64
		protected override DatabaseValidationCheck.Result ValidateInternal(DatabaseValidationCheck.Arguments args, ref LocalizedString error)
		{
			RpcDatabaseCopyStatus2 copyStatus = args.Status.CopyStatus;
			if (copyStatus.NodeStatus == NodeUpStatusEnum.Down)
			{
				error = ReplayStrings.AmBcsTargetNodeDownError(args.TargetServer.NetbiosName);
				return DatabaseValidationCheck.Result.Failed;
			}
			return DatabaseValidationCheck.Result.Passed;
		}
	}
}
