using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage.ActiveManager
{
	// Token: 0x0200030A RID: 778
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ActiveManagerUtil
	{
		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x0600230A RID: 8970 RVA: 0x0008DD49 File Offset: 0x0008BF49
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ActiveManagerClientTracer;
			}
		}

		// Token: 0x0600230B RID: 8971 RVA: 0x0008DD50 File Offset: 0x0008BF50
		private static List<ADObjectId> GetMasterServerIdsForDatabase(IFindAdObject<IADDatabaseAvailabilityGroup> dagLookup, IADDatabase database, out Exception exception)
		{
			IADToplogyConfigurationSession adSession = dagLookup.AdSession;
			List<ADObjectId> list = new List<ADObjectId>(16);
			exception = null;
			try
			{
				ADObjectId masterServerOrAvailabilityGroup = database.MasterServerOrAvailabilityGroup;
				if (masterServerOrAvailabilityGroup == null)
				{
					return null;
				}
				if (masterServerOrAvailabilityGroup.IsDeleted)
				{
					ActiveManagerUtil.Tracer.TraceError<string, string>(0L, "GetMasterServerIdsForDatabase() for database '{0}' found the MasterServerOrAvailabilityGroup to be a link to a deleted object. Returning an empty collection. MasterServerOrAvailabilityGroup = [{1}]", database.Name, masterServerOrAvailabilityGroup.Name);
					return null;
				}
				IADDatabaseAvailabilityGroup iaddatabaseAvailabilityGroup = dagLookup.ReadAdObjectByObjectId(masterServerOrAvailabilityGroup);
				if (iaddatabaseAvailabilityGroup != null)
				{
					list.AddRange(iaddatabaseAvailabilityGroup.Servers);
				}
				else
				{
					IADDatabase iaddatabase = adSession.ReadADObject<IADDatabase>(database.Id);
					ADObjectId adobjectId = null;
					if (iaddatabase != null)
					{
						adobjectId = iaddatabase.MasterServerOrAvailabilityGroup;
						if (!masterServerOrAvailabilityGroup.Equals(adobjectId))
						{
							ActiveManagerUtil.Tracer.TraceDebug<ADObjectId, ADObjectId>(0L, "GetMasterServerIdsForDatabase() re-read the Database object and it made a difference. MasterServerOrDag was {0} and is now {1}", masterServerOrAvailabilityGroup, adobjectId);
							iaddatabaseAvailabilityGroup = adSession.ReadADObject<IADDatabaseAvailabilityGroup>(adobjectId);
							if (iaddatabaseAvailabilityGroup == null)
							{
								goto IL_165;
							}
							HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
							foreach (string fqdn in iaddatabaseAvailabilityGroup.StoppedMailboxServers)
							{
								hashSet.Add(MachineName.GetNodeNameFromFqdn(fqdn));
							}
							using (MultiValuedProperty<ADObjectId>.Enumerator enumerator2 = iaddatabaseAvailabilityGroup.Servers.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									ADObjectId adobjectId2 = enumerator2.Current;
									if (!hashSet.Contains(adobjectId2.Name))
									{
										list.Add(adobjectId2);
									}
								}
								goto IL_165;
							}
						}
						ActiveManagerUtil.Tracer.TraceDebug<ADObjectId>(0L, "GetMasterServerIdsForDatabase: re-reading the Database object made no difference. MasterServerOrDag is still {0}.", masterServerOrAvailabilityGroup);
					}
					IL_165:
					if (iaddatabaseAvailabilityGroup == null && adobjectId != null)
					{
						IADServer iadserver = adSession.ReadMiniServer(adobjectId);
						if (iadserver != null)
						{
							list.Add(adobjectId);
						}
					}
				}
			}
			catch (DataValidationException ex)
			{
				exception = ex;
			}
			catch (ADTransientException ex2)
			{
				exception = ex2;
			}
			catch (ADOperationException ex3)
			{
				exception = ex3;
			}
			catch (ADTopologyUnexpectedException ex4)
			{
				exception = ex4;
			}
			catch (ADTopologyPermanentException ex5)
			{
				exception = ex5;
			}
			if (exception != null)
			{
				ActiveManagerUtil.Tracer.TraceDebug<Exception>(0L, "GetMasterServerIdsForDatabase() got exception: {0}", exception);
				list = null;
			}
			return list;
		}

		// Token: 0x0600230C RID: 8972 RVA: 0x0008DFD4 File Offset: 0x0008C1D4
		public static List<ADObjectId> GetServersForDatabaseInDag(IADDatabase database, DatabaseAvailabilityGroup dag)
		{
			MultiValuedProperty<ADObjectId> servers = dag.Servers;
			ADObjectId[] servers2 = database.Servers;
			List<ADObjectId> list = new List<ADObjectId>(servers2.Length);
			foreach (ADObjectId id in servers2)
			{
				foreach (ADObjectId adobjectId in servers)
				{
					if (adobjectId.Equals(id))
					{
						list.Add(adobjectId);
						break;
					}
				}
			}
			return list;
		}

		// Token: 0x0600230D RID: 8973 RVA: 0x0008E068 File Offset: 0x0008C268
		public static bool IsNullEncoded(string serverName)
		{
			return !string.IsNullOrEmpty(serverName) && string.Equals(serverName, "*", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600230E RID: 8974 RVA: 0x0008E084 File Offset: 0x0008C284
		internal static List<ADObjectId> GetOrderedServerIdsForDatabase(IFindAdObject<IADDatabaseAvailabilityGroup> dagLookup, IADDatabase database, out Exception exception)
		{
			List<ADObjectId> masterServerIdsForDatabase = ActiveManagerUtil.GetMasterServerIdsForDatabase(dagLookup, database, out exception);
			if (exception != null)
			{
				return masterServerIdsForDatabase;
			}
			if (masterServerIdsForDatabase == null || masterServerIdsForDatabase.Count == 0)
			{
				ExTraceGlobals.ActiveManagerClientTracer.TraceError<string>(0L, "Database {0} master is pointing to a non-existent/deleted server or DAG, the database has been deleted, or the database object is corrupted in the AD.", database.Name);
				exception = new AmDatabaseMasterIsInvalid(database.Name);
				return masterServerIdsForDatabase;
			}
			int num = -1;
			string local = MachineName.Local;
			for (int i = 0; i < masterServerIdsForDatabase.Count; i++)
			{
				if (string.Equals(masterServerIdsForDatabase[i].Name, local, StringComparison.OrdinalIgnoreCase))
				{
					num = i;
					break;
				}
			}
			if (num > 0)
			{
				ADObjectId value = masterServerIdsForDatabase[num];
				masterServerIdsForDatabase[num] = masterServerIdsForDatabase[0];
				masterServerIdsForDatabase[0] = value;
			}
			return masterServerIdsForDatabase;
		}

		// Token: 0x0600230F RID: 8975 RVA: 0x0008E128 File Offset: 0x0008C328
		internal static ADObjectId GetServerSiteFromServer(Server server)
		{
			if (server.IsExchange2007OrLater)
			{
				return server.ServerSite;
			}
			return null;
		}

		// Token: 0x06002310 RID: 8976 RVA: 0x0008E13A File Offset: 0x0008C33A
		internal static ADObjectId GetServerSiteFromMiniServer(IADServer server)
		{
			if (server.IsExchange2007OrLater)
			{
				return server.ServerSite;
			}
			return null;
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x0008E14C File Offset: 0x0008C34C
		internal static string NullEncode(string serverName)
		{
			if (string.IsNullOrEmpty(serverName))
			{
				return "*";
			}
			return serverName;
		}

		// Token: 0x04001479 RID: 5241
		internal const string NullEncodeString = "*";
	}
}
