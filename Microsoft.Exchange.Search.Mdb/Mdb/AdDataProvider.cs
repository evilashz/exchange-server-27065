using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Diagnostics;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x02000002 RID: 2
	internal class AdDataProvider
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		private AdDataProvider(IDiagnosticsSession diagnosticsSession)
		{
			this.adSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 41, ".ctor", "f:\\15.00.1497\\sources\\dev\\Search\\src\\Mdb\\AdDataProvider.cs");
			this.diagnosticsSession = diagnosticsSession;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002101 File Offset: 0x00000301
		public static AdDataProvider Create(IDiagnosticsSession diagnosticsSession)
		{
			return new AdDataProvider(diagnosticsSession);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000212C File Offset: 0x0000032C
		public ADNotificationRequestCookie RegisterChangeNotification(ADNotificationCallback callback)
		{
			this.diagnosticsSession.TraceDebug("Registrering for a change notification.", new object[0]);
			ADObjectId databaseRootId = this.GetDatabasesContainerId();
			ADNotificationRequestCookie cookie = null;
			this.RunAdOperation(delegate
			{
				cookie = ADNotificationAdapter.RegisterChangeNotification<DatabaseCopy>(databaseRootId, callback);
			}, false, 0, Strings.FailedToRegisterDatabaseChangeNotification);
			return cookie;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000021A4 File Offset: 0x000003A4
		public void UnRegisterChangeNotification(ADNotificationRequestCookie cookie)
		{
			this.diagnosticsSession.TraceDebug<ADNotificationRequestCookie>("Unregistering from a database change notification with a cookie {0}.", cookie);
			this.RunAdOperation(delegate
			{
				ADNotificationAdapter.UnregisterChangeNotification(cookie);
			}, true, 0, Strings.FailedToUnRegisterDatabaseChangeNotification);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002208 File Offset: 0x00000408
		public MailboxDatabase[] GetLocalMailboxDatabases(Server server)
		{
			this.diagnosticsSession.TraceDebug<string>("Retrieving all mailbox databases on server {0}", server.Fqdn);
			MailboxDatabase[] databases = null;
			this.RunAdOperation(delegate
			{
				databases = server.GetMailboxDatabases();
			}, true, 0, Strings.FailedToGetMailboxDatabases);
			return databases;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002294 File Offset: 0x00000494
		public MiniServer[] GetServers(ICollection<Guid> serverIds, int maxResults)
		{
			List<QueryFilter> list = new List<QueryFilter>(serverIds.Count);
			foreach (Guid guid in serverIds)
			{
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Guid, guid));
			}
			QueryFilter queryFilter;
			switch (list.Count)
			{
			case 0:
				return new MiniServer[0];
			case 1:
				queryFilter = list[0];
				break;
			default:
				queryFilter = new OrFilter(list.ToArray());
				break;
			}
			this.diagnosticsSession.TraceDebug<QueryFilter>("Getting servers in the dag using query filter:{0}", queryFilter);
			MiniServer[] servers = null;
			this.RunAdOperation(delegate
			{
				servers = this.adSession.FindMiniServer(null, QueryScope.SubTree, queryFilter, null, maxResults, null);
			}, true, 3, Strings.AdOperationFailed);
			return servers ?? new MiniServer[0];
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000023B4 File Offset: 0x000005B4
		public ADObjectId GetDatabasesContainerId()
		{
			this.diagnosticsSession.TraceDebug("Getting databases container id.", new object[0]);
			ADObjectId databaseRootId = null;
			this.RunAdOperation(delegate
			{
				databaseRootId = this.adSession.GetDatabasesContainerId();
			}, true, 0, Strings.FailedToGetDatabasesContainerId);
			return databaseRootId;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002430 File Offset: 0x00000630
		public Database FindDatabase(Guid databaseGuid)
		{
			this.diagnosticsSession.TraceDebug<Guid>("Looking for a database with mdbGuid {0}", databaseGuid);
			Database database = null;
			this.RunAdOperation(delegate
			{
				database = this.adSession.FindDatabaseByGuid<Database>(databaseGuid);
			}, true, 0, Strings.AdOperationFailed);
			return database;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000024A4 File Offset: 0x000006A4
		public Server GetLocalServer()
		{
			this.diagnosticsSession.TraceDebug("Getting a local server.", new object[0]);
			Server server = null;
			this.RunAdOperation(delegate
			{
				server = LocalServer.GetServer();
			}, true, 0, Strings.FailedToGetLocalServer);
			if (server == null)
			{
				throw new AdDataProvider.AdTransientException(Strings.FailedToGetLocalServer);
			}
			return server;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000251C File Offset: 0x0000071C
		private void RunAdOperation(Action adAction, bool wrapAdOperation, int retryCount, LocalizedString exceptionString)
		{
			if (!wrapAdOperation)
			{
				adAction();
				return;
			}
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				adAction();
			}, retryCount);
			this.diagnosticsSession.TraceDebug<ADOperationResult, Exception>("Finished Ad operation. Result:{0}. Exception:{1}", adoperationResult, adoperationResult.Exception);
			switch (adoperationResult.ErrorCode)
			{
			case ADOperationErrorCode.Success:
				return;
			case ADOperationErrorCode.RetryableError:
				throw new AdDataProvider.AdTransientException(exceptionString, adoperationResult.Exception);
			case ADOperationErrorCode.PermanentError:
				throw new AdDataProvider.AdPermanentException(exceptionString, adoperationResult.Exception);
			default:
				throw new ArgumentException(string.Format("Unknown result error code {0}", adoperationResult.ErrorCode));
			}
		}

		// Token: 0x04000001 RID: 1
		private readonly ITopologyConfigurationSession adSession;

		// Token: 0x04000002 RID: 2
		private readonly IDiagnosticsSession diagnosticsSession;

		// Token: 0x02000003 RID: 3
		public class AdTransientException : ComponentFailedTransientException
		{
			// Token: 0x0600000B RID: 11 RVA: 0x000025C1 File Offset: 0x000007C1
			public AdTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
			{
			}

			// Token: 0x0600000C RID: 12 RVA: 0x000025CB File Offset: 0x000007CB
			public AdTransientException(LocalizedString message) : base(message)
			{
			}
		}

		// Token: 0x02000004 RID: 4
		public class AdPermanentException : ComponentFailedPermanentException
		{
			// Token: 0x0600000D RID: 13 RVA: 0x000025D4 File Offset: 0x000007D4
			public AdPermanentException(LocalizedString message, Exception innerException) : base(message, innerException)
			{
			}
		}
	}
}
