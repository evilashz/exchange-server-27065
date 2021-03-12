using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000218 RID: 536
	internal class DatabaseCheckDatabaseIsReplicated : ActiveOrPassiveDatabaseCopyValidationCheck
	{
		// Token: 0x0600147B RID: 5243 RVA: 0x0005261A File Offset: 0x0005081A
		public DatabaseCheckDatabaseIsReplicated() : base(DatabaseValidationCheck.ID.DatabaseCheckDatabaseIsReplicated)
		{
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x00052624 File Offset: 0x00050824
		protected override DatabaseValidationCheck.Result ValidateInternal(DatabaseValidationCheck.Arguments args, ref LocalizedString error)
		{
			if (args.Database.ReplicationType != ReplicationType.Remote)
			{
				DatabaseValidationCheck.Tracer.TraceDebug<string, string, int>((long)this.GetHashCode(), "{0}: Database '{1}' is *NOT* replicated! It has only {2} copy(ies) configured in the AD.", base.CheckName, args.DatabaseName, args.Database.DatabaseCopies.Length);
				error = ReplayStrings.DbValidationDbNotReplicated(args.DatabaseName);
				return DatabaseValidationCheck.Result.Warning;
			}
			return DatabaseValidationCheck.Result.Passed;
		}
	}
}
