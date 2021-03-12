using System;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.HA.DirectoryServices;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200020E RID: 526
	internal class DatabaseCheckActivationDisfavored : ActiveOrPassiveDatabaseCopyValidationCheck
	{
		// Token: 0x06001467 RID: 5223 RVA: 0x00051FC2 File Offset: 0x000501C2
		public DatabaseCheckActivationDisfavored() : base(DatabaseValidationCheck.ID.DatabaseCheckActivationDisfavored)
		{
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x00051FCC File Offset: 0x000501CC
		protected override DatabaseValidationCheck.Result ValidateInternal(DatabaseValidationCheck.Arguments args, ref LocalizedString error)
		{
			if (!args.IgnoreActivationDisfavored)
			{
				AmServerName targetServer = args.TargetServer;
				IADServer iadserver = args.ADConfig.LookupMiniServerByName(targetServer);
				if (iadserver == null)
				{
					error = ReplayStrings.AmBcsTargetServerADError(args.TargetServer.Fqdn, ReplayStrings.AmBcsNoneSpecified);
					return DatabaseValidationCheck.Result.Failed;
				}
				if (iadserver.DatabaseCopyActivationDisabledAndMoveNow)
				{
					error = ReplayStrings.AmBcsTargetServerActivationDisabled(args.ActiveServer.Fqdn);
					return DatabaseValidationCheck.Result.Failed;
				}
			}
			return DatabaseValidationCheck.Result.Passed;
		}
	}
}
