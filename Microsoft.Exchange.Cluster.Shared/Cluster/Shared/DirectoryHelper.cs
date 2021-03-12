using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.HA.DirectoryServices;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x0200006B RID: 107
	internal static class DirectoryHelper
	{
		// Token: 0x0600032E RID: 814 RVA: 0x0000DCC0 File Offset: 0x0000BEC0
		public static IADDatabaseAvailabilityGroup GetLocalServerDatabaseAvailabilityGroup(IADToplogyConfigurationSession adSession, out Exception exception)
		{
			IADDatabaseAvailabilityGroup dag = null;
			Exception objNotFoundEx = null;
			exception = null;
			exception = SharedHelper.RunADOperationEx(delegate(object param0, EventArgs param1)
			{
				if (adSession == null)
				{
					adSession = ADSessionFactory.CreateIgnoreInvalidRootOrgSession(true);
				}
				if (adSession != null)
				{
					IADServer iadserver = adSession.FindServerByName(SharedDependencies.ManagementClassHelper.LocalMachineName);
					if (iadserver != null)
					{
						ADObjectId databaseAvailabilityGroup = iadserver.DatabaseAvailabilityGroup;
						if (databaseAvailabilityGroup != null)
						{
							dag = adSession.ReadADObject<IADDatabaseAvailabilityGroup>(databaseAvailabilityGroup);
							return;
						}
					}
					else
					{
						objNotFoundEx = new CouldNotFindServerObject(Environment.MachineName);
					}
				}
			});
			if (objNotFoundEx != null)
			{
				exception = objNotFoundEx;
			}
			return dag;
		}
	}
}
