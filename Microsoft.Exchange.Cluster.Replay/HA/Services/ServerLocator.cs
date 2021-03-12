﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.HA.Services
{
	// Token: 0x02000331 RID: 817
	public class ServerLocator : IServerLocator, IDisposable
	{
		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06002148 RID: 8520 RVA: 0x0009A0E4 File Offset: 0x000982E4
		private static Microsoft.Exchange.Diagnostics.Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ServerLocatorServiceTracer;
			}
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x0009A0EC File Offset: 0x000982EC
		private static void HandleException(DatabaseServerInformationFaultType code, Exception ex, Guid databaseGuid)
		{
			ServerLocatorManager.Instance.Counters.RecordOneWCFCallError();
			ReplayEventLogConstants.Tuple_ServerLocatorServiceServerForDatabaseNotFoundError.LogEvent(databaseGuid.ToString(), new object[]
			{
				databaseGuid.ToString(),
				ex.Message
			});
			throw new FaultException<DatabaseServerInformationFault>(new DatabaseServerInformationFault(code, ex));
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x0009A14C File Offset: 0x0009834C
		private static void HandleException(DatabaseServerInformationFaultType code, Exception ex)
		{
			ServerLocatorManager.Instance.Counters.RecordOneWCFCallError();
			ReplayEventLogConstants.Tuple_ServerLocatorServiceGetAllError.LogEvent(null, new object[]
			{
				ex.Message
			});
			throw new FaultException<DatabaseServerInformationFault>(new DatabaseServerInformationFault(code, ex));
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x0009A190 File Offset: 0x00098390
		private bool TryPopulateServerVersionFromAdCache(AmServerName activeServerName, ref DatabaseServerInformation information)
		{
			information.ServerVersion = 0;
			try
			{
				IMonitoringADConfig recentConfig = ServerLocatorManager.Instance.ADConfigProvider.GetRecentConfig(false);
				IADServer iadserver = recentConfig.LookupMiniServerByName(activeServerName);
				if (iadserver != null)
				{
					information.ServerVersion = iadserver.VersionNumber;
					return true;
				}
				ReplayEventLogConstants.Tuple_ServerLocatorServiceGetServerObjectError.LogEvent(information.DatabaseGuid.ToString(), new object[]
				{
					activeServerName.Fqdn,
					information.DatabaseGuid.ToString(),
					"No object in cache."
				});
			}
			catch (MonitoringADConfigException ex)
			{
				ReplayEventLogConstants.Tuple_ServerLocatorServiceGetServerObjectError.LogEvent(information.DatabaseGuid.ToString(), new object[]
				{
					activeServerName.Fqdn,
					information.DatabaseGuid.ToString(),
					ex.Message
				});
				ServerLocator.Tracer.TraceError<string>(0L, "ADConfigProvider.GetRecentConfig() call returned error {0}", ex.Message);
			}
			return false;
		}

		// Token: 0x0600214C RID: 8524 RVA: 0x0009A2B4 File Offset: 0x000984B4
		private bool TryGetDatabaseServerInformationFromStateInfo(AmDbStateInfo stateInfo, ref DatabaseServerInformation information)
		{
			if (stateInfo.IsEntryExist)
			{
				information.DatabaseGuid = stateInfo.DatabaseGuid;
				information.ServerFqdn = stateInfo.ActiveServer.Fqdn;
				information.MountedTimeUtc = stateInfo.LastMountedTime;
				if (!string.IsNullOrEmpty(stateInfo.LastMountedServer.Fqdn))
				{
					information.LastMountedServerFqdn = stateInfo.LastMountedServer.Fqdn;
				}
				else
				{
					information.LastMountedServerFqdn = null;
				}
				information.FailoverSequenceNumber = stateInfo.FailoverSequenceNumber;
				this.TryPopulateServerVersionFromAdCache(stateInfo.ActiveServer, ref information);
				return true;
			}
			return false;
		}

		// Token: 0x0600214D RID: 8525 RVA: 0x0009A344 File Offset: 0x00098544
		private List<DatabaseServerInformation> GetActiveCopiesForDatabaseAvailabilityGroupInternal(bool cachedData)
		{
			ServerLocator.Tracer.TraceDebug<bool, DateTime>(0L, "Receieved GetActiveCopiesForDatabaseAvailabilityGroup(CachedData = {0}) call at {1}.", cachedData, DateTime.UtcNow);
			ServerLocatorManager.Instance.Counters.RecordOneWCFGetAllCall();
			Stopwatch stopwatch = Stopwatch.StartNew();
			List<DatabaseServerInformation> list = new List<DatabaseServerInformation>();
			if (cachedData)
			{
				try
				{
					IMonitoringADConfig recentConfig = ServerLocatorManager.Instance.ADConfigProvider.GetRecentConfig(false);
					List<AmServerName> amServerNames = recentConfig.AmServerNames;
					Dictionary<AmServerName, IEnumerable<IADDatabase>> databaseMap = recentConfig.DatabaseMap;
					foreach (AmServerName amServerName in amServerNames)
					{
						IEnumerable<CopyStatusClientCachedEntry> copyStatusesByServer = ServerLocatorManager.Instance.CopyStatusLookup.GetCopyStatusesByServer(amServerName, databaseMap[amServerName], CopyStatusClientLookupFlags.None);
						foreach (CopyStatusClientCachedEntry copyStatusClientCachedEntry in copyStatusesByServer)
						{
							if (copyStatusClientCachedEntry.IsActive)
							{
								ServerLocator.Tracer.TraceDebug<Guid, string>(0L, "GetActiveCopiesForDatabaseAvailabilityGroup found database {0} active on server {1}.", copyStatusClientCachedEntry.DbGuid, copyStatusClientCachedEntry.ActiveServer.Fqdn);
								DatabaseServerInformation databaseServerInformation = new DatabaseServerInformation();
								databaseServerInformation.DatabaseGuid = copyStatusClientCachedEntry.DbGuid;
								databaseServerInformation.ServerFqdn = copyStatusClientCachedEntry.ActiveServer.Fqdn;
								if (this.TryPopulateServerVersionFromAdCache(copyStatusClientCachedEntry.ActiveServer, ref databaseServerInformation))
								{
									list.Add(databaseServerInformation);
								}
							}
						}
					}
					goto IL_328;
				}
				catch (MonitoringADConfigException ex)
				{
					ServerLocator.Tracer.TraceError<string>(0L, "ADConfigProvider.GetRecentConfig() call returned error {0}", ex.Message);
					ServerLocator.HandleException(DatabaseServerInformationFaultType.TransientError, ex);
					goto IL_328;
				}
			}
			try
			{
				AmConfig config = AmSystemManager.Instance.Config;
				if (config.IsUnknown)
				{
					AmInvalidConfiguration ex2 = new AmInvalidConfiguration(config.LastError);
					ServerLocator.Tracer.TraceError<string>(0L, "GetActiveCopiesForDatabaseAvailabilityGroup failed because of invalid AM configuration on the node. Erorr: {0}", config.LastError);
					ServerLocator.HandleException(DatabaseServerInformationFaultType.TransientError, ex2);
				}
				else
				{
					AmDbStateInfo[] array = config.DbState.ReadAll();
					HashSet<Guid> hashSet = null;
					try
					{
						IMonitoringADConfig recentConfig2 = ServerLocatorManager.Instance.ADConfigProvider.GetRecentConfig(false);
						hashSet = new HashSet<Guid>(recentConfig2.DatabaseByGuidMap.Keys);
					}
					catch (MonitoringADConfigException ex3)
					{
						ServerLocator.Tracer.TraceError<string>(0L, "ADConfigProvider.GetRecentConfig() call returned error {0}", ex3.Message);
					}
					if (array != null)
					{
						foreach (AmDbStateInfo amDbStateInfo in array)
						{
							DatabaseServerInformation item = new DatabaseServerInformation();
							if (this.TryGetDatabaseServerInformationFromStateInfo(amDbStateInfo, ref item))
							{
								if (hashSet != null && hashSet.Contains(amDbStateInfo.DatabaseGuid))
								{
									hashSet.Remove(amDbStateInfo.DatabaseGuid);
								}
								list.Add(item);
							}
						}
					}
					if (hashSet != null && hashSet.Count > 0)
					{
						foreach (Guid dbGuid in hashSet)
						{
							AmDbStateInfo stateInfo = config.DbState.Read(dbGuid);
							DatabaseServerInformation item2 = new DatabaseServerInformation();
							if (this.TryGetDatabaseServerInformationFromStateInfo(stateInfo, ref item2))
							{
								list.Add(item2);
							}
						}
					}
				}
			}
			catch (AmInvalidDbStateException ex4)
			{
				ServerLocator.Tracer.TraceError<string>(0L, "GetActiveCopiesForDatabaseAvailabilityGroup returned error {0}", ex4.Message);
				ServerLocator.HandleException(DatabaseServerInformationFaultType.TransientError, ex4);
			}
			catch (ClusterException ex5)
			{
				ServerLocator.Tracer.TraceError<string>(0L, "GetActiveCopiesForDatabaseAvailabilityGroup returned error {0}", ex5.Message);
				ServerLocator.HandleException(DatabaseServerInformationFaultType.TransientError, ex5);
			}
			IL_328:
			ServerLocator.Tracer.TraceDebug<int>(0L, "GetActiveCopiesForDatabaseAvailabilityGroup processed the call and found {0} active databases in the Dag.", list.Count);
			ServerLocatorManager.Instance.Counters.RecordWCFGetAllCallLatency(stopwatch.ElapsedTicks);
			return list;
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x0009A754 File Offset: 0x00098954
		public ServerLocator()
		{
			ServerLocator.Tracer.TraceDebug(0L, "Creating Server Locator instance.");
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x0009A770 File Offset: 0x00098970
		ServiceVersion IServerLocator.GetVersion()
		{
			return new ServiceVersion
			{
				Version = 2L
			};
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x0009A78C File Offset: 0x0009898C
		DatabaseServerInformation IServerLocator.GetServerForDatabase(DatabaseServerInformation database)
		{
			DateTime utcNow = DateTime.UtcNow;
			ServerLocator.Tracer.TraceDebug<Guid, DateTime>(0L, "Receieved GetServerForDatabase call for database {0} at {1}.", database.DatabaseGuid, utcNow);
			ServerLocatorManager.Instance.Counters.RecordOneWCFCall();
			Stopwatch stopwatch = Stopwatch.StartNew();
			Guid databaseGuid = database.DatabaseGuid;
			if (database.DatabaseGuid == Guid.Empty)
			{
				ServerLocator.Tracer.TraceError<Guid>(0L, "GetServerForDatabase active manager client call for database {0} cannot be performed because Guid is empty.", database.DatabaseGuid);
				ServerLocator.HandleException(DatabaseServerInformationFaultType.PermanentError, new ArgumentException("DatabaseGuid cannot be Empty."), database.DatabaseGuid);
			}
			DatabaseServerInformation databaseServerInformation = new DatabaseServerInformation();
			databaseServerInformation.RequestSentUtc = database.RequestSentUtc;
			databaseServerInformation.RequestReceivedUtc = utcNow;
			databaseServerInformation.ServerFqdn = database.ServerFqdn;
			databaseServerInformation.ServerVersion = database.ServerVersion;
			databaseServerInformation.MountedTimeUtc = database.MountedTimeUtc;
			databaseServerInformation.LastMountedServerFqdn = database.LastMountedServerFqdn;
			try
			{
				ServerLocator.Tracer.TraceDebug<Guid>(0L, "Looking up clusdb registry for database {0}.", database.DatabaseGuid);
				AmConfig config = AmSystemManager.Instance.Config;
				if (config.IsUnknown)
				{
					AmInvalidConfiguration ex = new AmInvalidConfiguration(config.LastError);
					ServerLocator.Tracer.TraceError<Guid, string>(0L, "GetServerForDatabase for database {0} cannot find database because of invalid AM configuration on the node. Erorr: {1}", database.DatabaseGuid, config.LastError);
					ServerLocator.HandleException(DatabaseServerInformationFaultType.TransientError, ex, database.DatabaseGuid);
				}
				else
				{
					AmDbStateInfo amDbStateInfo = config.DbState.Read(database.DatabaseGuid);
					if (this.TryGetDatabaseServerInformationFromStateInfo(amDbStateInfo, ref databaseServerInformation))
					{
						ServerLocator.Tracer.TraceDebug<Guid, string>(0L, "GetServerForDatabase call for database {0} returned server {1}.", database.DatabaseGuid, amDbStateInfo.ActiveServer.Fqdn);
					}
					else
					{
						DatabaseNotFoundException ex2 = new DatabaseNotFoundException(database.DatabaseGuid.ToString());
						ServerLocator.Tracer.TraceError<Guid>(0L, "GetServerForDatabase for database {0} cannot find database in the clussdb, it could be a brand new database.", database.DatabaseGuid);
						ServerLocator.HandleException(DatabaseServerInformationFaultType.TransientError, ex2, database.DatabaseGuid);
					}
				}
			}
			catch (AmInvalidDbStateException ex3)
			{
				ServerLocator.Tracer.TraceError<Guid, string>(0L, "GetServerForDatabase active manager client call for database {0} returned error {1}", database.DatabaseGuid, ex3.Message);
				ServerLocator.HandleException(DatabaseServerInformationFaultType.TransientError, ex3, database.DatabaseGuid);
			}
			catch (ClusterException ex4)
			{
				ServerLocator.Tracer.TraceError<Guid, string>(0L, "GetServerForDatabase active manager client call for database {0} returned error {1}", database.DatabaseGuid, ex4.Message);
				ServerLocator.HandleException(DatabaseServerInformationFaultType.TransientError, ex4, database.DatabaseGuid);
			}
			databaseServerInformation.ReplySentUtc = DateTime.UtcNow;
			ServerLocator.Tracer.TraceDebug<Guid, DateTime>(0L, "GetServerForDatabase call for database {0} finished server processing at {1}.", database.DatabaseGuid, databaseServerInformation.ReplySentUtc);
			ServerLocatorManager.Instance.Counters.RecordWCFCallLatency(stopwatch.ElapsedTicks);
			return databaseServerInformation;
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x0009AA18 File Offset: 0x00098C18
		List<DatabaseServerInformation> IServerLocator.GetActiveCopiesForDatabaseAvailabilityGroupExtended(GetActiveCopiesForDatabaseAvailabilityGroupParameters parameters)
		{
			return this.GetActiveCopiesForDatabaseAvailabilityGroupInternal(parameters.CachedData);
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x0009AA26 File Offset: 0x00098C26
		List<DatabaseServerInformation> IServerLocator.GetActiveCopiesForDatabaseAvailabilityGroup()
		{
			return this.GetActiveCopiesForDatabaseAvailabilityGroupInternal(RegistryParameters.GetActiveCopiesForDatabaseAvailabilityGroupUseCache);
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x0009AA33 File Offset: 0x00098C33
		public void Dispose()
		{
			if (!this.m_fDisposed)
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x0009AA4C File Offset: 0x00098C4C
		public void Dispose(bool disposing)
		{
			lock (this)
			{
				if (!this.m_fDisposed)
				{
					if (disposing)
					{
						ServerLocator.Tracer.TraceDebug(0L, "Disposing Server Locator instance.");
					}
					this.m_fDisposed = true;
				}
			}
		}

		// Token: 0x04000D9D RID: 3485
		private bool m_fDisposed;
	}
}
