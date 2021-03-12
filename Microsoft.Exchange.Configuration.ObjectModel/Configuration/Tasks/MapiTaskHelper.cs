using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mapi;
using Microsoft.Exchange.Data.Mapi.Common;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200006D RID: 109
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class MapiTaskHelper
	{
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0000F3FB File Offset: 0x0000D5FB
		public static bool IsDatacenter
		{
			get
			{
				if (!MapiTaskHelper.checkedForDatacenter)
				{
					MapiTaskHelper.isDatacenter = (Datacenter.GetExchangeSku() == Datacenter.ExchangeSku.ExchangeDatacenter);
					MapiTaskHelper.checkedForDatacenter = true;
				}
				return MapiTaskHelper.isDatacenter;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x0000F41C File Offset: 0x0000D61C
		public static bool IsDatacenterDedicated
		{
			get
			{
				if (!MapiTaskHelper.checkedForDatacenterDedicated)
				{
					MapiTaskHelper.isDatacenterDedicated = (Datacenter.GetExchangeSku() == Datacenter.ExchangeSku.DatacenterDedicated);
					MapiTaskHelper.checkedForDatacenterDedicated = true;
				}
				return MapiTaskHelper.isDatacenterDedicated;
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000F440 File Offset: 0x0000D640
		public static Server GetMailboxServer(ServerIdParameter serverIdParameter, ITopologyConfigurationSession configurationSession, Task.ErrorLoggerDelegate errorHandler)
		{
			if (serverIdParameter == null)
			{
				throw new ArgumentNullException("serverIdParameter");
			}
			if (configurationSession == null)
			{
				throw new ArgumentNullException("serverIdParameter");
			}
			if (errorHandler == null)
			{
				throw new ArgumentNullException("errorHandler");
			}
			IEnumerable<Server> objects = serverIdParameter.GetObjects<Server>(null, configurationSession);
			Server server = null;
			using (IEnumerator<Server> enumerator = objects.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					errorHandler(new ManagementObjectNotFoundException(Strings.ErrorServerNotFound(serverIdParameter.ToString())), ExchangeErrorCategory.Client, null);
					return null;
				}
				server = enumerator.Current;
				if (enumerator.MoveNext())
				{
					errorHandler(new ManagementObjectAmbiguousException(Strings.ErrorServerNotUnique(serverIdParameter.ToString())), ExchangeErrorCategory.Client, null);
					return null;
				}
			}
			if (!server.IsExchange2007OrLater)
			{
				errorHandler(new TaskInvalidOperationException(Strings.ExceptionLegacyObjects(serverIdParameter.ToString())), ExchangeErrorCategory.Context, null);
				return null;
			}
			if (!server.IsMailboxServer)
			{
				errorHandler(new TaskInvalidOperationException(Strings.ErrorNotMailboxServer(serverIdParameter.ToString())), ExchangeErrorCategory.Client, null);
				return null;
			}
			return server;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000F550 File Offset: 0x0000D750
		internal static OrganizationId ResolveTargetOrganization(Fqdn domainController, OrganizationIdParameter organization, ADObjectId rootOrgContainerId, OrganizationId currentOrganizationId, OrganizationId executingUserOrganizationId)
		{
			if (organization != null)
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(rootOrgContainerId, currentOrganizationId, executingUserOrganizationId, false);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(domainController, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, ConfigScopes.TenantSubTree, 183, "ResolveTargetOrganization", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\MapiTaskHelper.cs");
				tenantOrTopologyConfigurationSession.UseConfigNC = false;
				ADOrganizationalUnit adorganizationalUnit = null;
				LocalizedString? localizedString = null;
				IEnumerable<ADOrganizationalUnit> objects = organization.GetObjects<ADOrganizationalUnit>(null, tenantOrTopologyConfigurationSession, null, out localizedString);
				using (IEnumerator<ADOrganizationalUnit> enumerator = objects.GetEnumerator())
				{
					if (!enumerator.MoveNext())
					{
						throw new ManagementObjectNotFoundException(Strings.ErrorOrganizationNotFound(organization.ToString()));
					}
					adorganizationalUnit = enumerator.Current;
					if (enumerator.MoveNext())
					{
						throw new ManagementObjectAmbiguousException(Strings.ErrorOrganizationNotUnique(organization.ToString()));
					}
				}
				return adorganizationalUnit.OrganizationId;
			}
			return currentOrganizationId ?? executingUserOrganizationId;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000F624 File Offset: 0x0000D824
		internal static OrganizationIdParameter ResolveTargetOrganizationIdParameter(OrganizationIdParameter organizationParameter, IIdentityParameter identity, OrganizationId currentOrganizationId, Task.ErrorLoggerDelegate errorHandler, Task.TaskWarningLoggingDelegate warningHandler)
		{
			OrganizationIdParameter organizationIdParameter = null;
			if (identity != null)
			{
				if (identity is MailPublicFolderIdParameter)
				{
					organizationIdParameter = (identity as MailPublicFolderIdParameter).Organization;
				}
				else if (identity is PublicFolderIdParameter)
				{
					organizationIdParameter = (identity as PublicFolderIdParameter).Organization;
				}
			}
			if (!currentOrganizationId.Equals(OrganizationId.ForestWideOrgId))
			{
				if (organizationIdParameter != null)
				{
					errorHandler(new ManagementObjectNotFoundException(Strings.ErrorManagementObjectNotFound(identity.ToString())), ExchangeErrorCategory.Client, identity);
				}
			}
			else
			{
				if (organizationParameter != null)
				{
					if (organizationIdParameter != null)
					{
						warningHandler(Strings.WarningDuplicateOrganizationSpecified(organizationParameter.ToString(), organizationIdParameter.ToString()));
					}
					organizationIdParameter = organizationParameter;
				}
				if (organizationIdParameter == null && !(identity is MailPublicFolderIdParameter))
				{
					errorHandler(new ErrorMissOrganizationException(), ExchangeErrorCategory.Client, null);
				}
			}
			return organizationIdParameter;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		private static void GetServerForDatabase(Guid publicFolderDatabaseGuid, out string serverLegacyDn, out Fqdn serverFqdn)
		{
			ActiveManager activeManagerInstance = ActiveManager.GetActiveManagerInstance();
			DatabaseLocationInfo serverForDatabase = activeManagerInstance.GetServerForDatabase(publicFolderDatabaseGuid, GetServerForDatabaseFlags.ThrowServerForDatabaseNotFoundException);
			serverFqdn = Fqdn.Parse(serverForDatabase.ServerFqdn);
			serverLegacyDn = serverForDatabase.ServerLegacyDN;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000F700 File Offset: 0x0000D900
		internal static DatabaseId ConvertDatabaseADObjectToDatabaseId(Database adObject)
		{
			if (adObject == null)
			{
				throw new ArgumentNullException("adObject");
			}
			string serverName = adObject.ServerName;
			string text = adObject.Name;
			Guid guid = adObject.Guid;
			if (adObject.Identity != null)
			{
				DatabaseId databaseId = MapiTaskHelper.ConvertDatabaseADObjectIdToDatabaseId((ADObjectId)adObject.Identity);
				if (string.IsNullOrEmpty(serverName))
				{
					serverName = databaseId.ServerName;
				}
				if (string.IsNullOrEmpty(text))
				{
					text = databaseId.DatabaseName;
				}
				if (Guid.Empty == guid)
				{
					guid = databaseId.Guid;
				}
			}
			return new DatabaseId(null, serverName, text, guid);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000F788 File Offset: 0x0000D988
		internal static DatabaseId ConvertDatabaseADObjectIdToDatabaseId(ADObjectId adObjectId)
		{
			if (adObjectId == null)
			{
				throw new ArgumentNullException("adObjectId");
			}
			if (string.IsNullOrEmpty(adObjectId.DistinguishedName) || 3 > adObjectId.Depth)
			{
				return new DatabaseId(adObjectId.ObjectGuid);
			}
			return new DatabaseId(null, null, adObjectId.Name, adObjectId.ObjectGuid);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000F7D8 File Offset: 0x0000D9D8
		internal static MailboxStatistics[] GetStoreMailboxesFromId(IConfigDataProvider dataSession, StoreMailboxIdParameter identity, ObjectId rootId)
		{
			List<MailboxStatistics> list = new List<MailboxStatistics>();
			IEnumerable<MailboxStatistics> objects = identity.GetObjects<MailboxStatistics>(rootId, dataSession);
			if (objects == null)
			{
				return null;
			}
			foreach (MailboxStatistics item in objects)
			{
				list.Add(item);
			}
			return list.ToArray();
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000F83C File Offset: 0x0000DA3C
		internal static MapiAdministrationSession GetAdminSession(ActiveManager activeManager, Guid databaseGuid)
		{
			DatabaseLocationInfo serverForDatabase = activeManager.GetServerForDatabase(databaseGuid);
			return new MapiAdministrationSession(serverForDatabase.ServerLegacyDN, Fqdn.Parse(serverForDatabase.ServerFqdn));
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0000F868 File Offset: 0x0000DA68
		internal static string GetMailboxLegacyDN(MapiAdministrationSession mapiAdminSession, ADObjectId databaseId, Guid mailboxGuid)
		{
			string result = null;
			DatabaseId root = MapiTaskHelper.ConvertDatabaseADObjectIdToDatabaseId(databaseId);
			MailboxContextFilter filter = new MailboxContextFilter(mailboxGuid);
			MailboxStatistics[] array = null;
			try
			{
				array = mapiAdminSession.Find<MailboxStatistics>(filter, root, QueryScope.SubTree, null, 1);
			}
			catch (MapiObjectNotFoundException)
			{
			}
			if (array != null)
			{
				result = array[0].LegacyDN;
				array[0].Dispose();
			}
			return result;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0000F8C0 File Offset: 0x0000DAC0
		internal static void VerifyIsWithinConfigWriteScope(ADSessionSettings sessionSettings, ADObject obj, Task.ErrorLoggerDelegate errorHandler)
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(null, false, ConsistencyMode.PartiallyConsistent, sessionSettings, 481, "VerifyIsWithinConfigWriteScope", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\MapiTaskHelper.cs");
			ADScopeException ex;
			if (!tenantOrTopologyConfigurationSession.TryVerifyIsWithinScopes(obj, true, out ex))
			{
				errorHandler(new IsOutofConfigWriteScopeException(obj.GetType().ToString(), obj.Name), ExchangeErrorCategory.Client, null);
			}
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000F919 File Offset: 0x0000DB19
		internal static void VerifyDatabaseAndItsOwningServerInScope(ADSessionSettings sessionSettings, Database database, Task.ErrorLoggerDelegate errorHandler)
		{
			MapiTaskHelper.VerifyDatabaseIsWithinScope(sessionSettings, database, errorHandler, true);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000F924 File Offset: 0x0000DB24
		internal static void VerifyDatabaseIsWithinScope(ADSessionSettings sessionSettings, Database database, Task.ErrorLoggerDelegate errorHandler)
		{
			MapiTaskHelper.VerifyDatabaseIsWithinScope(sessionSettings, database, errorHandler, false);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000F930 File Offset: 0x0000DB30
		internal static void VerifyServerIsWithinScope(Database database, Task.ErrorLoggerDelegate errorHandler, ITopologyConfigurationSession adConfigSession)
		{
			ADObjectId[] array = database.IsExchange2009OrLater ? database.Servers : new ADObjectId[]
			{
				database.Server
			};
			if (array == null || array.Length == 0)
			{
				errorHandler(new NoServersForDatabaseException(database.Name), ExchangeErrorCategory.Client, null);
			}
			bool flag = false;
			ADScopeException ex = null;
			foreach (ADObjectId adObjectId in array)
			{
				Server mailboxServer = MapiTaskHelper.GetMailboxServer(new ServerIdParameter(adObjectId), adConfigSession, errorHandler);
				if (adConfigSession.TryVerifyIsWithinScopes(mailboxServer, true, out ex))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				errorHandler(new IsOutofDatabaseScopeException(database.Name, ex.Message), ExchangeErrorCategory.Authorization, null);
			}
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000F9E4 File Offset: 0x0000DBE4
		private static void VerifyDatabaseIsWithinScope(ADSessionSettings sessionSettings, Database database, Task.ErrorLoggerDelegate errorHandler, bool includeCheckForServer)
		{
			if (sessionSettings == null)
			{
				throw new ArgumentNullException("sessionSettings");
			}
			if (database == null)
			{
				throw new ArgumentNullException("database");
			}
			if (errorHandler == null)
			{
				throw new ArgumentNullException("errorHandler");
			}
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, sessionSettings, 613, "VerifyDatabaseIsWithinScope", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\MapiTaskHelper.cs");
			ADScopeException ex;
			if (!topologyConfigurationSession.TryVerifyIsWithinScopes(database, true, out ex))
			{
				errorHandler(new TaskInvalidOperationException(Strings.ErrorIsOutOfDatabaseScopeNoServerCheck(database.Name, ex.Message)), ExchangeErrorCategory.Authorization, null);
			}
			if (includeCheckForServer)
			{
				MapiTaskHelper.VerifyServerIsWithinScope(database, errorHandler, topologyConfigurationSession);
			}
		}

		// Token: 0x0400010A RID: 266
		private const int ServerRdnGenerationInDatabaseDN = 3;

		// Token: 0x0400010B RID: 267
		private const int StorageGroupRdnGenerationInDatabaseDN = 1;

		// Token: 0x0400010C RID: 268
		private static bool checkedForDatacenter;

		// Token: 0x0400010D RID: 269
		private static bool isDatacenter;

		// Token: 0x0400010E RID: 270
		private static bool checkedForDatacenterDedicated;

		// Token: 0x0400010F RID: 271
		private static bool isDatacenterDedicated;
	}
}
