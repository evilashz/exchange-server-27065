using System;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000219 RID: 537
	internal class DatabaseCheckCopyStatusNotStale : ActiveOrPassiveDatabaseCopyValidationCheck
	{
		// Token: 0x0600147D RID: 5245 RVA: 0x00052686 File Offset: 0x00050886
		public DatabaseCheckCopyStatusNotStale() : base(DatabaseValidationCheck.ID.DatabaseCheckCopyStatusNotStale)
		{
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x00052690 File Offset: 0x00050890
		protected override DatabaseValidationCheck.Result ValidateInternal(DatabaseValidationCheck.Arguments args, ref LocalizedString error)
		{
			CopyStatusClientCachedEntry status = args.Status;
			TimeSpan timeSpan = DateTime.UtcNow.Subtract(status.CreateTimeUtc);
			if (timeSpan > DatabaseCheckCopyStatusNotStale.s_maximumStatusAgeError)
			{
				DatabaseValidationCheck.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Database Copy '{1}' has copy status cached entry that is older than maximum *error* age of {2}. Actual age: {3}.", new object[]
				{
					base.CheckName,
					args.DatabaseCopyName,
					DatabaseCheckCopyStatusNotStale.s_maximumStatusAgeError,
					timeSpan
				});
				error = ReplayStrings.DbValidationCopyStatusTooOld(args.DatabaseCopyName, timeSpan, DatabaseCheckCopyStatusNotStale.s_maximumStatusAgeError);
				return DatabaseValidationCheck.Result.Failed;
			}
			if (timeSpan > DatabaseCheckCopyStatusNotStale.s_maximumStatusAgeWarning)
			{
				DatabaseValidationCheck.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Database Copy '{1}' has copy status cached entry that is older than maximum *warning* age of {2}. Actual age: {3}.", new object[]
				{
					base.CheckName,
					args.DatabaseCopyName,
					DatabaseCheckCopyStatusNotStale.s_maximumStatusAgeWarning,
					timeSpan
				});
				error = ReplayStrings.DbValidationCopyStatusTooOld(args.DatabaseCopyName, timeSpan, DatabaseCheckCopyStatusNotStale.s_maximumStatusAgeWarning);
				return DatabaseValidationCheck.Result.Warning;
			}
			return DatabaseValidationCheck.Result.Passed;
		}

		// Token: 0x040007F1 RID: 2033
		private static TimeSpan s_maximumStatusAgeWarning = TimeSpan.FromMilliseconds((double)(RegistryParameters.CopyStatusPollerIntervalInMsec * 4));

		// Token: 0x040007F2 RID: 2034
		private static TimeSpan s_maximumStatusAgeError = TimeSpan.FromMilliseconds((double)(RegistryParameters.CopyStatusPollerIntervalInMsec * 10));
	}
}
