using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000209 RID: 521
	internal class DatabaseCheckCopyStatusRpcSuccessful : ActiveOrPassiveDatabaseCopyValidationCheck
	{
		// Token: 0x0600145C RID: 5212 RVA: 0x00051CE5 File Offset: 0x0004FEE5
		public DatabaseCheckCopyStatusRpcSuccessful() : base(DatabaseValidationCheck.ID.DatabaseCheckCopyStatusRpcSuccessful)
		{
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x00051CF0 File Offset: 0x0004FEF0
		protected override DatabaseValidationCheck.Result ValidateInternal(DatabaseValidationCheck.Arguments args, ref LocalizedString error)
		{
			if (args.Status.Result != CopyStatusRpcResult.Success)
			{
				error = ReplayStrings.DbValidationCopyStatusRpcFailed(args.DatabaseName, args.TargetServer.NetbiosName, args.Status.LastException.Message);
				return DatabaseValidationCheck.Result.Failed;
			}
			DiagCore.RetailAssert(args.Status.CopyStatus != null, "CopyStatus cannot be null if Result is Success!", new object[0]);
			return DatabaseValidationCheck.Result.Passed;
		}
	}
}
