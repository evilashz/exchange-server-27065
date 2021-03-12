using System;
using System.Globalization;
using System.Security.Principal;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ProvisioningAgent;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.EventMessages;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200004D RID: 77
	internal static class AdminAuditLogHelper
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00007538 File Offset: 0x00005738
		public static bool RunningOnDataCenter
		{
			get
			{
				if (AdminAuditLogHelper.isDataCenter == null)
				{
					AdminAuditLogHelper.isDataCenter = new bool?(Datacenter.ExchangeSku.ExchangeDatacenter == Datacenter.GetExchangeSku());
				}
				return AdminAuditLogHelper.isDataCenter.Value;
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00007564 File Offset: 0x00005764
		public static IConfigurationSession CreateSession(OrganizationId organizationId, string configurationDomainController)
		{
			ADObjectId rootOrgContainerIdForLocalForest = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest();
			ADSessionSettings adsessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(rootOrgContainerIdForLocalForest, organizationId, null, false);
			if (adsessionSettings == null)
			{
				AdminAuditLogHelper.Tracer.TraceError(0L, "AdminAuditLogHelper: adSessionSettings is null. Cannot get config objects from AD.");
				throw new ArgumentNullException("adSessionSettings is null");
			}
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(configurationDomainController, true, ConsistencyMode.FullyConsistent, adsessionSettings, 133, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\AdminAuditLog\\AdminAuditLogHelper.cs");
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000075C0 File Offset: 0x000057C0
		public static AdminAuditLogConfig GetAdminAuditLogConfig(IConfigurationSession configSession)
		{
			OrganizationId currentOrganizationId = configSession.SessionSettings.CurrentOrganizationId;
			AdminAuditLogHelper.Tracer.TraceDebug<OrganizationId>(0L, "AdminAuditLogHelper: Will retrieve config objects from AD for OrganizationId: {0}", currentOrganizationId);
			SharedConfiguration sharedConfiguration = SharedConfiguration.GetSharedConfiguration(currentOrganizationId);
			IConfigurationSession configurationSession = configSession;
			if (sharedConfiguration != null)
			{
				configurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(configSession.ConsistencyMode, sharedConfiguration.GetSharedConfigurationSessionSettings(), 160, "GetAdminAuditLogConfig", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\AdminAuditLog\\AdminAuditLogHelper.cs");
			}
			configurationSession.SessionSettings.IsSharedConfigChecked = true;
			bool includeCNFObject = configurationSession.SessionSettings.IncludeCNFObject;
			AdminAuditLogConfig[] array;
			try
			{
				configurationSession.SessionSettings.IncludeCNFObject = false;
				array = configurationSession.Find<AdminAuditLogConfig>(null, QueryScope.SubTree, null, null, 2);
			}
			finally
			{
				configurationSession.SessionSettings.IncludeCNFObject = includeCNFObject;
			}
			if (array.Length == 0)
			{
				AdminAuditLogHelper.Tracer.TraceDebug<OrganizationId>(0L, "AdminAuditLogHelper: No AdminAuditLogConfig was found in org {0}.", currentOrganizationId);
				AdminAuditLogHelper.WriteNoAdminAuditLogWarningAsNeed(configSession);
				return null;
			}
			if (array.Length > 1)
			{
				AdminAuditLogHelper.Tracer.TraceDebug<OrganizationId>(0L, "AdminAuditLogHelper: More than one AdminAuditLogConfig was found in org {0}.", currentOrganizationId);
				ExManagementApplicationLogger.LogEvent(currentOrganizationId, ManagementEventLogConstants.Tuple_MultipleAdminAuditLogConfig, new string[]
				{
					currentOrganizationId.ToString()
				});
				throw new MultipleAdminAuditLogConfigException(currentOrganizationId.ToString());
			}
			AdminAuditLogHelper.Tracer.TraceDebug<OrganizationId, bool>(0L, "AdminAuditLogHelper: One AdminAuditLogConfig was found in org {0}. Logging enabled is {1}.", currentOrganizationId, array[0].AdminAuditLogEnabled);
			return array[0];
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000076F0 File Offset: 0x000058F0
		private static void WriteNoAdminAuditLogWarningAsNeed(IConfigurationSession configSession)
		{
			bool flag = true;
			OrganizationId currentOrganizationId = configSession.SessionSettings.CurrentOrganizationId;
			if (currentOrganizationId.ConfigurationUnit != null)
			{
				ExchangeConfigurationUnit exchangeConfigurationUnit = configSession.Read<ExchangeConfigurationUnit>(currentOrganizationId.ConfigurationUnit);
				if (exchangeConfigurationUnit == null)
				{
					AdminAuditLogHelper.Tracer.TraceDebug<OrganizationId>(0L, "AdminAuditLogHelper: ExchangeConfigurationUnit for org {0} is null", currentOrganizationId);
				}
				else
				{
					AdminAuditLogHelper.Tracer.TraceDebug<OrganizationId, OrganizationStatus>(0L, "AdminAuditLogHelper: org {0}'s status is {1}", currentOrganizationId, exchangeConfigurationUnit.OrganizationStatus);
					if (exchangeConfigurationUnit.OrganizationStatus == OrganizationStatus.PendingCompletion)
					{
						flag = false;
					}
				}
			}
			else
			{
				AdminAuditLogHelper.Tracer.TraceDebug<OrganizationId>(0L, "AdminAuditLogHelper: ConfigurationUnit for org {0} is null", currentOrganizationId);
			}
			if (flag)
			{
				ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_NoAdminAuditLogConfig, new string[]
				{
					currentOrganizationId.ToString()
				});
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000778C File Offset: 0x0000598C
		public static ADUser GetTenantArbitrationMailbox(OrganizationId organizationId)
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), organizationId, null, false);
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 287, "GetTenantArbitrationMailbox", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\AdminAuditLog\\AdminAuditLogHelper.cs");
			return MailboxDataProvider.GetDiscoveryMailbox(tenantOrRootOrgRecipientSession);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000077CC File Offset: 0x000059CC
		public static StoreObjectId GetOrCreateAdminAuditLogsFolderId(OrganizationId orgId)
		{
			ADUser tenantArbitrationMailbox = AdminAuditLogHelper.GetTenantArbitrationMailbox(orgId);
			return AdminAuditLogHelper.GetOrCreateAdminAuditLogsFolderId(tenantArbitrationMailbox);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000077E8 File Offset: 0x000059E8
		public static StoreObjectId GetOrCreateAdminAuditLogsFolderId(ADUser adUser)
		{
			ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromADUser(adUser.OrganizationId.ToADSessionSettings(), adUser, RemotingOptions.AllowCrossSite);
			StoreObjectId orCreateAdminAuditLogsFolderId;
			using (MailboxSession mailboxSession = AdminAuditLogHelper.GetMailboxSession(exchangePrincipal, "Client=Management;Action=GetOrCreateAdminAuditLogsFolderId"))
			{
				orCreateAdminAuditLogsFolderId = AdminAuditLogHelper.GetOrCreateAdminAuditLogsFolderId(mailboxSession);
			}
			return orCreateAdminAuditLogsFolderId;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00007838 File Offset: 0x00005A38
		public static StoreObjectId GetOrCreateAdminAuditLogsFolderId(MailboxSession mailboxSession)
		{
			StoreObjectId storeObjectId = mailboxSession.GetAdminAuditLogsFolderId();
			if (storeObjectId == null)
			{
				if (mailboxSession.GetDefaultFolderId(DefaultFolderType.RecoverableItemsRoot) == null)
				{
					AdminAuditLogHelper.Tracer.TraceDebug<string>(0L, "AdminAuditLogHelper: Create the folder 'RecoverableItemsRoot' in the mailbox '{0}'", mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
					mailboxSession.CreateDefaultFolder(DefaultFolderType.RecoverableItemsRoot);
				}
				AdminAuditLogHelper.Tracer.TraceDebug<string>(0L, "AdminAuditLogHelper: Create the folder 'AdminAuditLogs' in the mailbox '{0}'", mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
				storeObjectId = mailboxSession.CreateDefaultFolder(DefaultFolderType.AdminAuditLogs);
			}
			return storeObjectId;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x000078CC File Offset: 0x00005ACC
		public static bool ShouldIssueWarning(OrganizationId organizationId)
		{
			ADUser aduser;
			ExchangePrincipal exchangePrincipal;
			Exception ex;
			ArbitrationMailboxStatus arbitrationMailboxStatus = AdminAuditLogHelper.CheckArbitrationMailboxStatus(organizationId, out aduser, out exchangePrincipal, out ex);
			return arbitrationMailboxStatus != ArbitrationMailboxStatus.E15 && arbitrationMailboxStatus != ArbitrationMailboxStatus.FFO;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x000078F4 File Offset: 0x00005AF4
		public static ArbitrationMailboxStatus CheckArbitrationMailboxStatus(OrganizationId organizationId, out ADUser user, out ExchangePrincipal principal, out Exception exception)
		{
			user = null;
			principal = null;
			exception = null;
			try
			{
				if (DatacenterRegistry.IsForefrontForOffice())
				{
					return ArbitrationMailboxStatus.FFO;
				}
				user = AdminAuditLogHelper.GetTenantArbitrationMailbox(organizationId);
				principal = ExchangePrincipal.FromADUser(ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId), user, RemotingOptions.AllowCrossSite);
				AdminAuditLogHelper.Tracer.TraceDebug<int>(0L, "AdminAuditLogHelper: Tenant arbitration mailbox server version is {0}", principal.MailboxInfo.Location.ServerVersion);
				if (principal.MailboxInfo.Location.ServerVersion < Server.E14SP1MinVersion)
				{
					return ArbitrationMailboxStatus.R4;
				}
				if (principal.MailboxInfo.Location.ServerVersion < Server.E15MinVersion)
				{
					return ArbitrationMailboxStatus.R5;
				}
				return ArbitrationMailboxStatus.E15;
			}
			catch (ObjectNotFoundException ex)
			{
				exception = ex;
				AdminAuditLogHelper.Tracer.TraceDebug<OrganizationId, ObjectNotFoundException>(0L, "AdminAuditLogHelper: unable to determine the arbitration mailbox version for org {0}, exception {1}", organizationId, ex);
			}
			catch (MailboxInfoStaleException ex2)
			{
				exception = ex2;
				AdminAuditLogHelper.Tracer.TraceDebug<OrganizationId, MailboxInfoStaleException>(0L, "AdminAuditLogHelper: unable to determine the arbitration mailbox version for org {0}, exception {1}", organizationId, ex2);
			}
			catch (SuitabilityDirectoryException ex3)
			{
				exception = ex3;
				AdminAuditLogHelper.Tracer.TraceDebug<OrganizationId, SuitabilityDirectoryException>(0L, "AdminAuditLogHelper: unable to determine the arbitration mailbox version for org {0}, exception {1}", organizationId, ex3);
			}
			catch (DatabaseLocationUnavailableException ex4)
			{
				exception = ex4;
				AdminAuditLogHelper.Tracer.TraceDebug<OrganizationId, DatabaseLocationUnavailableException>(0L, "AdminAuditLogHelper: unable to determine the arbitration mailbox version for org {0}, exception {1}", organizationId, ex4);
			}
			return ArbitrationMailboxStatus.UnableToKnow;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00007A54 File Offset: 0x00005C54
		public static MailboxSession GetMailboxSession(ExchangePrincipal exchangePrincipal, string clientInfo)
		{
			return AdminAuditLogHelper.RetryOnStorageTransientException<MailboxSession>(() => MailboxSession.OpenAsSystemService(exchangePrincipal, CultureInfo.InvariantCulture, clientInfo));
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00007A88 File Offset: 0x00005C88
		public static void SetResolveUsers(AdminAuditLogSearch searchObject, DataAccessHelper.GetDataObjectDelegate getDataObject, Task.TaskVerboseLoggingDelegate writeVerbose, Task.TaskWarningLoggingDelegate writeWarning)
		{
			if (searchObject.UserIdsUserInput != null && searchObject.UserIdsUserInput.Count > 0)
			{
				writeVerbose(Strings.VerboseStartResolvingUsers);
				ADObjectId rootOrgContainerIdForLocalForest = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest();
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(rootOrgContainerIdForLocalForest, searchObject.OrganizationId, null, false);
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.PartiallyConsistent, sessionSettings, 515, "SetResolveUsers", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\AdminAuditLog\\AdminAuditLogHelper.cs");
				tenantOrRootOrgRecipientSession.UseConfigNC = false;
				searchObject.UserIds = new MultiValuedProperty<string>();
				searchObject.ResolvedUsers = new MultiValuedProperty<string>();
				foreach (SecurityPrincipalIdParameter securityPrincipalIdParameter in searchObject.UserIdsUserInput)
				{
					searchObject.UserIds.Add(securityPrincipalIdParameter.RawIdentity);
					bool flag = false;
					try
					{
						ADRecipient adrecipient = (ADRecipient)getDataObject(securityPrincipalIdParameter, tenantOrRootOrgRecipientSession, null, null, new LocalizedString?(Strings.WarningSearchUserNotFound(securityPrincipalIdParameter.ToString())), new LocalizedString?(Strings.ErrorSearchUserNotUnique(securityPrincipalIdParameter.ToString())));
						if (adrecipient.Id != null && adrecipient.Id.DomainId != null && !string.IsNullOrEmpty(adrecipient.Id.DomainId.Name))
						{
							string text = (string)adrecipient.propertyBag[IADSecurityPrincipalSchema.SamAccountName];
							if (!string.IsNullOrEmpty(text))
							{
								searchObject.ResolvedUsers.Add(adrecipient.Id.DomainId.Name + "\\" + text);
								flag = true;
								writeVerbose(Strings.DebugResolvingDomainAccount(securityPrincipalIdParameter.ToString(), adrecipient.Id.DomainId.Name, text));
							}
						}
						if (adrecipient.propertyBag[IADSecurityPrincipalSchema.Sid] != null)
						{
							string value = ((SecurityIdentifier)adrecipient.propertyBag[IADSecurityPrincipalSchema.Sid]).Value;
							if (!string.IsNullOrEmpty(value))
							{
								searchObject.ResolvedUsers.Add(value);
								flag = true;
								writeVerbose(Strings.DebugResolvingUserSid(securityPrincipalIdParameter.ToString(), value));
							}
						}
						if (adrecipient.Id != null && !string.IsNullOrEmpty(adrecipient.Id.ToString()))
						{
							searchObject.ResolvedUsers.Add(adrecipient.Id.ToString());
							flag = true;
							writeVerbose(Strings.DebugResolvingUserCN(securityPrincipalIdParameter.ToString(), adrecipient.Id.ToString()));
						}
						if (!flag)
						{
							writeWarning(Strings.WarningCannotResolveUser(securityPrincipalIdParameter.ToString()));
							searchObject.ResolvedUsers.Add(securityPrincipalIdParameter.ToString());
						}
					}
					catch (ManagementObjectNotFoundException)
					{
						writeWarning(Strings.WarningSearchUserNotFound(securityPrincipalIdParameter.ToString()));
						searchObject.ResolvedUsers.Add(securityPrincipalIdParameter.ToString());
					}
				}
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00007D5C File Offset: 0x00005F5C
		private static T RetryOnStorageTransientException<T>(Func<T> func)
		{
			for (int i = 0; i < 3; i++)
			{
				try
				{
					return func();
				}
				catch (StorageTransientException arg)
				{
					AdminAuditLogHelper.Tracer.TraceDebug<StorageTransientException>(0L, "AdminAuditLogHelper: failed due transient exception, retrying. Exception: {0}.", arg);
				}
			}
			return func();
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00007DAC File Offset: 0x00005FAC
		public static bool IsDiscoverySearchModifierCmdlet(string cmdletName)
		{
			string text = cmdletName.ToLower();
			return text.Equals("set-mailboxsearch") || text.Equals("start-mailboxsearch") || text.Equals("stop-mailboxsearch") || text.Equals("remove-mailboxsearch");
		}

		// Token: 0x0400011C RID: 284
		public const int TransientRetries = 3;

		// Token: 0x0400011D RID: 285
		public static readonly Trace Tracer = ExTraceGlobals.AdminAuditLogTracer;

		// Token: 0x0400011E RID: 286
		private static bool? isDataCenter;
	}
}
