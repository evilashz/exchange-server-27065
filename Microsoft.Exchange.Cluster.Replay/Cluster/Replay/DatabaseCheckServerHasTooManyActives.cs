using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.HA.DirectoryServices;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200020C RID: 524
	internal class DatabaseCheckServerHasTooManyActives : ActiveOrPassiveDatabaseCopyValidationCheck
	{
		// Token: 0x06001462 RID: 5218 RVA: 0x00051E48 File Offset: 0x00050048
		public DatabaseCheckServerHasTooManyActives() : base(DatabaseValidationCheck.ID.DatabaseCheckServerHasTooManyActives)
		{
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x00051E5C File Offset: 0x0005005C
		protected override DatabaseValidationCheck.Result ValidateInternal(DatabaseValidationCheck.Arguments args, ref LocalizedString error)
		{
			if (!args.IgnoreTooManyActivesCheck)
			{
				IADServer iadserver = args.ADConfig.LookupMiniServerByName(args.TargetServer);
				if (iadserver == null)
				{
					error = ReplayStrings.AmBcsTargetServerADError(args.TargetServer.Fqdn, ReplayStrings.AmBcsNoneSpecified);
					return DatabaseValidationCheck.Result.Failed;
				}
				int? maximumPreferredActiveDatabases = iadserver.MaximumPreferredActiveDatabases;
				if (maximumPreferredActiveDatabases != null)
				{
					IEnumerable<IADDatabase> expectedDatabases = args.ADConfig.DatabaseMap[args.TargetServer];
					IEnumerable<CopyStatusClientCachedEntry> copyStatusesByServer = args.StatusLookup.GetCopyStatusesByServer(args.TargetServer, expectedDatabases, CopyStatusClientLookupFlags.None);
					IEnumerable<CopyStatusClientCachedEntry> enumerable = from copy in copyStatusesByServer
					where copy.IsActive
					select copy;
					if (enumerable != null && enumerable.Count<CopyStatusClientCachedEntry>() > maximumPreferredActiveDatabases)
					{
						error = ReplayStrings.AmBcsTargetServerPreferredMaxActivesExceeded(args.TargetServer.Fqdn, (maximumPreferredActiveDatabases != null) ? maximumPreferredActiveDatabases.Value.ToString() : "<null>");
						return DatabaseValidationCheck.Result.Failed;
					}
				}
			}
			return DatabaseValidationCheck.Result.Passed;
		}
	}
}
