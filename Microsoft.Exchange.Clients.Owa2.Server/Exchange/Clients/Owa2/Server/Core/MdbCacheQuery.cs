using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Services;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200004A RID: 74
	public class MdbCacheQuery : MdbCache.IQuery
	{
		// Token: 0x06000202 RID: 514 RVA: 0x00007DF3 File Offset: 0x00005FF3
		public static MdbCacheQuery GetInstance()
		{
			if (MdbCacheQuery.singleton == null)
			{
				MdbCacheQuery.singleton = new MdbCacheQuery();
			}
			return MdbCacheQuery.singleton;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00007E0C File Offset: 0x0000600C
		public bool TryGetDatabasePaths(out Dictionary<Guid, string> paths)
		{
			RequestDetailsLogger getRequestDetailsLogger = OwaApplication.GetRequestDetailsLogger;
			if (getRequestDetailsLogger != null)
			{
				return this.TryExecuteWithExistingLogger(getRequestDetailsLogger, out paths);
			}
			return this.TryExecuteWithNewLogger(out paths);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00007E54 File Offset: 0x00006054
		private bool TryExecuteWithExistingLogger(RequestDetailsLogger logger, out Dictionary<Guid, string> result)
		{
			Dictionary<Guid, string> paths = null;
			ADNotificationAdapter.RunADOperation(delegate()
			{
				paths = this.ExecuteQuery(logger);
			});
			result = paths;
			return result != null;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00007F14 File Offset: 0x00006114
		private bool TryExecuteWithNewLogger(out Dictionary<Guid, string> result)
		{
			Dictionary<Guid, string> paths = null;
			SimulatedWebRequestContext.ExecuteWithoutUserContext("WAC.MdbCacheUpdate", delegate(RequestDetailsLogger logger)
			{
				WacUtilities.SetEventId(logger, "WAC.MdbCacheUpdate");
				ADNotificationAdapter.RunADOperation(delegate()
				{
					paths = this.ExecuteQuery(logger);
				});
			});
			result = paths;
			return result != null;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00007F5C File Offset: 0x0000615C
		private Dictionary<Guid, string> ExecuteQuery(RequestDetailsLogger logger)
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
			ADTopologyConfigurationSession adtopologyConfigurationSession = new ADTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, sessionSettings);
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			Server server = adtopologyConfigurationSession.ReadLocalServer();
			Database[] databases = server.GetDatabases();
			stopwatch.Stop();
			logger.ActivityScope.SetProperty(WacRequestHandlerMetadata.MdbCacheReloadTime, stopwatch.ElapsedMilliseconds.ToString());
			logger.ActivityScope.SetProperty(WacRequestHandlerMetadata.MdbCacheSize, databases.Length.ToString());
			Dictionary<Guid, string> dictionary = new Dictionary<Guid, string>(databases.Length);
			foreach (Database database in databases)
			{
				string directoryName = Path.GetDirectoryName(database.EdbFilePath.PathName);
				string value = Path.Combine(directoryName, "OwaCobalt");
				dictionary.Add(database.Guid, value);
			}
			return dictionary;
		}

		// Token: 0x040000CE RID: 206
		private static MdbCacheQuery singleton;
	}
}
