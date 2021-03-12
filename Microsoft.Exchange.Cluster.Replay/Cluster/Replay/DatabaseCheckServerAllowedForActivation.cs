using System;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200020D RID: 525
	internal class DatabaseCheckServerAllowedForActivation : ActiveOrPassiveDatabaseCopyValidationCheck
	{
		// Token: 0x06001465 RID: 5221 RVA: 0x00051F6E File Offset: 0x0005016E
		public DatabaseCheckServerAllowedForActivation() : base(DatabaseValidationCheck.ID.DatabaseCheckServerAllowedForActivation)
		{
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x00051F78 File Offset: 0x00050178
		protected override DatabaseValidationCheck.Result ValidateInternal(DatabaseValidationCheck.Arguments args, ref LocalizedString error)
		{
			error = LocalizedString.Empty;
			AmBcsServerChecks checks = AmBcsServerChecks.DatacenterActivationModeStarted | AmBcsServerChecks.AutoActivationAllowed;
			AmBcsServerValidation amBcsServerValidation = new AmBcsServerValidation(args.TargetServer, args.ActiveServer, args.Database, null, null, args.ADConfig);
			if (!amBcsServerValidation.RunChecks(checks, ref error))
			{
				return DatabaseValidationCheck.Result.Failed;
			}
			return DatabaseValidationCheck.Result.Passed;
		}
	}
}
