using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020008C6 RID: 2246
	internal static class StampGroupTaskHelper
	{
		// Token: 0x06004FBB RID: 20411 RVA: 0x0014CF60 File Offset: 0x0014B160
		internal static void CheckServerDoesNotBelongToDifferentStampGroup(Task.TaskErrorLoggingDelegate writeError, IConfigDataProvider dataSession, Server server, string stampGroupName)
		{
			ADObjectId databaseAvailabilityGroup = server.DatabaseAvailabilityGroup;
			if (databaseAvailabilityGroup != null)
			{
				StampGroup stampGroup = (StampGroup)dataSession.Read<StampGroup>(databaseAvailabilityGroup);
				if (stampGroup != null && stampGroup.Name != stampGroupName)
				{
					writeError(new DagTaskServerMailboxServerIsInDifferentDagException(server.Name, stampGroup.Name), ErrorCategory.InvalidArgument, null);
				}
			}
		}

		// Token: 0x06004FBC RID: 20412 RVA: 0x0014CFB0 File Offset: 0x0014B1B0
		internal static StampGroup StampGroupIdParameterToStampGroup(StampGroupIdParameter stampGroupIdParam, IConfigDataProvider configSession)
		{
			IEnumerable<StampGroup> objects = stampGroupIdParam.GetObjects<StampGroup>(null, configSession);
			StampGroup result;
			using (IEnumerator<StampGroup> enumerator = objects.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw new ManagementObjectNotFoundException(Strings.ErrorDagNotFound(stampGroupIdParam.ToString()));
				}
				StampGroup stampGroup = enumerator.Current;
				if (enumerator.MoveNext())
				{
					throw new ManagementObjectAmbiguousException(Strings.ErrorDagNotUnique(stampGroupIdParam.ToString()));
				}
				result = stampGroup;
			}
			return result;
		}

		// Token: 0x06004FBD RID: 20413 RVA: 0x0014D028 File Offset: 0x0014B228
		internal static StampGroup ReadStampGroup(ADObjectId stampGroupId, IConfigDataProvider configSession)
		{
			StampGroup result = null;
			if (!ADObjectId.IsNullOrEmpty(stampGroupId))
			{
				result = (StampGroup)configSession.Read<StampGroup>(stampGroupId);
			}
			return result;
		}

		// Token: 0x06004FBE RID: 20414 RVA: 0x0014D050 File Offset: 0x0014B250
		internal static ADObjectId FindServerAdObjectIdInStampGroup(StampGroup stampGroup, AmServerName serverName)
		{
			foreach (ADObjectId adobjectId in stampGroup.Servers)
			{
				if (SharedHelper.StringIEquals(adobjectId.Name, serverName.NetbiosName))
				{
					return adobjectId;
				}
			}
			return null;
		}
	}
}
