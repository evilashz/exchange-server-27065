using System;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000215 RID: 533
	internal class DatabaseCheckPassiveConnected : PassiveDatabaseCopyValidationCheck
	{
		// Token: 0x06001475 RID: 5237 RVA: 0x00052431 File Offset: 0x00050631
		public DatabaseCheckPassiveConnected() : base(DatabaseValidationCheck.ID.DatabaseCheckPassiveConnected)
		{
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x0005243C File Offset: 0x0005063C
		protected override DatabaseValidationCheck.Result ValidateInternal(DatabaseValidationCheck.Arguments args, ref LocalizedString error)
		{
			error = LocalizedString.Empty;
			DatabaseValidationCheck.Result result = DatabaseValidationCheck.Result.Passed;
			if (args.Status.CopyStatus != null)
			{
				string text = args.Status.CopyStatus.DBName + "\\" + args.Status.CopyStatus.MailboxServer;
				if (args.PropertyUpdateTracker.LastTimeCopierTimeUpdateTracker.UpdateCurrentValueOrReturnStaleness(text, args.Status.CopyStatus.LastLogInfoFromCopierTime).TotalSeconds >= (double)RegistryParameters.DatabaseHealthCheckCopyConnectedErrorThresholdInSec)
				{
					error = ReplayStrings.PassiveCopyDisconnected;
					result = DatabaseValidationCheck.Result.Failed;
				}
			}
			return result;
		}
	}
}
