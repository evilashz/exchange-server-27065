using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.HA.DirectoryServices
{
	// Token: 0x02000024 RID: 36
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class AdObjectLookupHelper
	{
		// Token: 0x060001B6 RID: 438 RVA: 0x00004E4C File Offset: 0x0000304C
		public static IADServer FindLocalServer(IFindAdObject<IADServer> serverLookup)
		{
			string localComputerFqdn = NativeHelpers.GetLocalComputerFqdn(true);
			return serverLookup.FindServerByFqdn(localComputerFqdn);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00004E68 File Offset: 0x00003068
		public static IADDatabase[] GetAllDatabases(IFindAdObject<IADDatabase> databaseLookup, IADServer server)
		{
			List<IADDatabase> list = new List<IADDatabase>(20);
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, server.Name);
			IADDatabaseCopy[] array = databaseLookup.AdSession.Find<IADDatabaseCopy>(null, QueryScope.SubTree, filter, null, 0);
			foreach (IADDatabaseCopy iaddatabaseCopy in array)
			{
				ADObjectId parent = iaddatabaseCopy.Id.Parent;
				IADDatabase iaddatabase = databaseLookup.ReadAdObjectByObjectId(parent);
				if (iaddatabase != null)
				{
					list.Add(iaddatabase);
				}
			}
			return (list.Count<IADDatabase>() > 0) ? list.ToArray() : null;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00004F1C File Offset: 0x0000311C
		public static IADDatabase[] GetAllDatabases(IADToplogyConfigurationSession adSession, IFindAdObject<IADDatabase> databaseLookup, MiniServer miniServer, out Exception exception)
		{
			IADDatabaseCopy[] copies = null;
			QueryFilter serverFilter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, miniServer.Name);
			exception = ADUtils.RunADOperation(delegate()
			{
				copies = adSession.Find<IADDatabaseCopy>(null, QueryScope.SubTree, serverFilter, null, 0);
			}, 2);
			if (exception != null)
			{
				return null;
			}
			List<IADDatabase> list = new List<IADDatabase>(20);
			foreach (IADDatabaseCopy iaddatabaseCopy in copies)
			{
				ADObjectId parent = iaddatabaseCopy.Id.Parent;
				IADDatabase iaddatabase = databaseLookup.ReadAdObjectByObjectIdEx(parent, out exception);
				if (exception != null)
				{
					return null;
				}
				if (iaddatabase != null)
				{
					list.Add(iaddatabase);
				}
			}
			return (list.Count > 0) ? list.ToArray() : null;
		}
	}
}
