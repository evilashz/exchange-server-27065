using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.HA;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Data.Storage.Cluster;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000142 RID: 322
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ReplayConfigurationHelper
	{
		// Token: 0x06000C4B RID: 3147 RVA: 0x000368A0 File Offset: 0x00034AA0
		public static Dictionary<Guid, ReplayConfiguration> GetAllLocalConfigurations(IADConfig adConfig, ActiveManager activeManager, out List<KeyValuePair<IADDatabase, Exception>> failedConfigurations)
		{
			Dictionary<Guid, ReplayConfiguration> dictionary = new Dictionary<Guid, ReplayConfiguration>(48);
			ExTraceGlobals.ReplayManagerTracer.TraceDebug(0L, "GetConfigurations: Looking for possible Configurations.");
			List<ReplayConfiguration> configList;
			List<ReplayConfiguration> configList2;
			ReplayConfiguration.ConstructAllLocalConfigurations(adConfig, activeManager, out configList, out configList2, out failedConfigurations);
			ReplayConfigurationHelper.AddConfigurationsToDictionary(configList, dictionary);
			ReplayConfigurationHelper.AddConfigurationsToDictionary(configList2, dictionary);
			ExTraceGlobals.ReplayManagerTracer.TraceDebug<int>(0L, "Found {0} configurations in total.", dictionary.Count);
			return dictionary;
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x000368F8 File Offset: 0x00034AF8
		public static ReplayConfiguration GetLocalReplayConfiguration(Guid dbGuid, IADConfig adConfig, ActiveManager activeManager, out Exception ex)
		{
			ex = null;
			if (activeManager == null)
			{
				activeManager = ActiveManager.GetNoncachingActiveManagerInstance();
			}
			IADServer localServer = adConfig.GetLocalServer();
			if (localServer == null)
			{
				ex = new FailedToFindServerException("localmachine");
				ExTraceGlobals.ReplayManagerTracer.TraceError<Guid, Exception>(0L, "GetReplayConfiguration ({0}): didn't find any server object for the local machine: {1}", dbGuid, ex);
				return null;
			}
			IADDatabase database = adConfig.GetDatabase(dbGuid);
			if (database == null)
			{
				ex = new FailedToFindDatabaseException(dbGuid.ToString());
				ExTraceGlobals.ReplayManagerTracer.TraceError<Guid>(0L, "GetReplayConfiguration ({0}): Didn't find any mailbox database object from AD.", dbGuid);
				return null;
			}
			bool flag;
			ReplayConfiguration replayConfiguration = ReplayConfiguration.GetReplayConfiguration(adConfig.GetLocalDag(), database, localServer, activeManager, out flag, out ex);
			if (ex != null)
			{
				ExTraceGlobals.ReplayManagerTracer.TraceError<Guid, Exception>(0L, "GetReplayConfiguration ({0}): Error occurred constructing the ReplayConfiguration. Error: {1}", dbGuid, ex);
				return null;
			}
			return replayConfiguration;
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x000369A4 File Offset: 0x00034BA4
		public static void TaskConstructAllDatabaseConfigurations(IADDatabaseAvailabilityGroup dag, IADServer server, out List<ReplayConfiguration> activeConfigurations, out List<ReplayConfiguration> passiveConfigurations)
		{
			activeConfigurations = new List<ReplayConfiguration>(20);
			passiveConfigurations = new List<ReplayConfiguration>(48);
			ActiveManager noncachingActiveManagerInstance = ActiveManager.GetNoncachingActiveManagerInstance();
			IADToplogyConfigurationSession iadtoplogyConfigurationSession = ADSessionFactory.CreateIgnoreInvalidRootOrgSession(true);
			IEnumerable<IADDatabase> allDatabases = iadtoplogyConfigurationSession.GetAllDatabases(server);
			if (allDatabases != null)
			{
				foreach (IADDatabase mdb in allDatabases)
				{
					bool flag;
					Exception ex;
					ReplayConfiguration replayConfiguration = ReplayConfiguration.GetReplayConfiguration(dag, mdb, server, noncachingActiveManagerInstance, out flag, out ex);
					if (replayConfiguration != null)
					{
						if (flag)
						{
							activeConfigurations.Add(replayConfiguration);
						}
						else
						{
							passiveConfigurations.Add(replayConfiguration);
						}
					}
				}
			}
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x00036A40 File Offset: 0x00034C40
		public static List<ReplayConfiguration> GetAllLocalCopyConfigurationsForVss()
		{
			Dependencies.ADConfig.Refresh("GetAllLocalCopyConfigurationsForVss");
			List<ReplayConfiguration> list;
			List<ReplayConfiguration> collection;
			List<KeyValuePair<IADDatabase, Exception>> list2;
			ReplayConfiguration.ConstructAllLocalConfigurations(Dependencies.ADConfig, null, out list, out collection, out list2);
			list.AddRange(collection);
			return list;
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x00036A78 File Offset: 0x00034C78
		public static Exception HandleKnownExceptions(EventHandler ev)
		{
			Exception result = null;
			try
			{
				ev(null, null);
			}
			catch (ClusterException ex)
			{
				result = ex;
			}
			catch (OperationAbortedException ex2)
			{
				result = ex2;
			}
			catch (OperationAbortedException ex3)
			{
				result = ex3;
			}
			catch (TransientException ex4)
			{
				result = ex4;
			}
			catch (AmServerException ex5)
			{
				result = ex5;
			}
			catch (SeederServerException ex6)
			{
				result = ex6;
			}
			catch (TaskServerException ex7)
			{
				result = ex7;
			}
			catch (DataSourceOperationException ex8)
			{
				result = ex8;
			}
			catch (MapiPermanentException ex9)
			{
				result = ex9;
			}
			catch (Win32Exception ex10)
			{
				result = ex10;
			}
			catch (IOException ex11)
			{
				result = ex11;
			}
			catch (UnauthorizedAccessException ex12)
			{
				result = ex12;
			}
			return result;
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x00036B74 File Offset: 0x00034D74
		public static void ThrowDbOperationWrapperExceptionIfNecessary(Exception exception)
		{
			if (exception == null)
			{
				return;
			}
			string operationError = string.Empty;
			IHaRpcServerBaseException ex = exception as IHaRpcServerBaseException;
			if (ex != null)
			{
				operationError = ex.ErrorMessage;
			}
			else
			{
				operationError = exception.Message;
			}
			if (exception is TransientException)
			{
				throw new ReplayDbOperationWrapperTransientException(operationError, exception);
			}
			throw new ReplayDbOperationWrapperException(operationError, exception);
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x00036BBC File Offset: 0x00034DBC
		private static void AddConfigurationsToDictionary(List<ReplayConfiguration> configList, Dictionary<Guid, ReplayConfiguration> configDictionary)
		{
			foreach (ReplayConfiguration replayConfiguration in configList)
			{
				if (!configDictionary.ContainsKey(replayConfiguration.IdentityGuid))
				{
					configDictionary.Add(replayConfiguration.IdentityGuid, replayConfiguration);
				}
			}
		}
	}
}
