using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA.DirectoryServices;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200020B RID: 523
	internal class DatabaseCheckServerInMaintenanceMode : ActiveOrPassiveDatabaseCopyValidationCheck
	{
		// Token: 0x06001460 RID: 5216 RVA: 0x00051D9F File Offset: 0x0004FF9F
		public DatabaseCheckServerInMaintenanceMode() : base(DatabaseValidationCheck.ID.DatabaseCheckServerInMaintenanceMode)
		{
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x00051DA8 File Offset: 0x0004FFA8
		protected override DatabaseValidationCheck.Result ValidateInternal(DatabaseValidationCheck.Arguments args, ref LocalizedString error)
		{
			if (!args.IgnoreMaintenanceChecks)
			{
				IADServer iadserver = args.ADConfig.LookupMiniServerByName(args.TargetServer);
				if (iadserver == null)
				{
					error = ReplayStrings.AmBcsTargetServerADError(args.TargetServer.Fqdn, ReplayStrings.AmBcsNoneSpecified);
					return DatabaseValidationCheck.Result.Failed;
				}
				if (ServerComponentStates.ReadEffectiveComponentState(iadserver.Fqdn, iadserver.ComponentStates, ServerComponentStateSources.AD, ServerComponentStates.GetComponentId(ServerComponentEnum.HighAvailability), ServiceState.Active) == ServiceState.Inactive)
				{
					error = ReplayStrings.AmBcsTargetServerIsHAComponentOffline(iadserver.Fqdn);
					return DatabaseValidationCheck.Result.Failed;
				}
				if (iadserver.DatabaseCopyAutoActivationPolicy == DatabaseCopyAutoActivationPolicyType.Blocked)
				{
					error = ReplayStrings.AmBcsTargetServerActivationBlocked(args.TargetServer.Fqdn);
					return DatabaseValidationCheck.Result.Failed;
				}
			}
			return DatabaseValidationCheck.Result.Passed;
		}
	}
}
