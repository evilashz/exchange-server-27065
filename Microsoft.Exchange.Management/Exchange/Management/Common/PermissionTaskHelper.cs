using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Management.Automation;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mapi;
using Microsoft.Exchange.Data.Mapi.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Management.Common
{
	// Token: 0x02000128 RID: 296
	internal static class PermissionTaskHelper
	{
		// Token: 0x06000AA0 RID: 2720 RVA: 0x00031188 File Offset: 0x0002F388
		public static ActiveDirectorySecurity ReadAdSecurityDescriptor(ADRawEntry entry, IDirectorySession session, Task.TaskErrorLoggingDelegate logError)
		{
			RawSecurityDescriptor rawSecurityDescriptor = null;
			return PermissionTaskHelper.ReadAdSecurityDescriptor(entry, session, logError, out rawSecurityDescriptor);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x000311A4 File Offset: 0x0002F3A4
		public static ActiveDirectorySecurity ReadAdSecurityDescriptor(ADRawEntry entry, IDirectorySession session, Task.TaskErrorLoggingDelegate logError, out RawSecurityDescriptor rawSd)
		{
			TaskLogger.LogEnter();
			rawSd = session.ReadSecurityDescriptor(entry.Id);
			if (rawSd == null)
			{
				if (logError != null)
				{
					logError(new SecurityDescriptorAccessDeniedException(entry.Id.DistinguishedName), ErrorCategory.ReadError, null);
				}
				return null;
			}
			ActiveDirectorySecurity result = SecurityDescriptorConverter.ConvertToActiveDirectorySecurity(rawSd);
			TaskLogger.LogExit();
			return result;
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x000311F4 File Offset: 0x0002F3F4
		public static ActiveDirectorySecurity ReadMailboxSecurityDescriptor(ADUser mailbox, IConfigurationSession adSession, Task.TaskVerboseLoggingDelegate logVerbose, Task.ErrorLoggerDelegate logError)
		{
			TaskLogger.LogEnter();
			RawSecurityDescriptor exchangeSecurityDescriptor = mailbox.ExchangeSecurityDescriptor;
			RawSecurityDescriptor rawSecurityDescriptor;
			if (mailbox.RecipientType == RecipientType.SystemAttendantMailbox)
			{
				rawSecurityDescriptor = exchangeSecurityDescriptor;
			}
			else
			{
				RawSecurityDescriptor rawSecurityDescriptor2 = adSession.ReadSecurityDescriptor(mailbox.Database);
				if (rawSecurityDescriptor2 == null)
				{
					logError(new TaskInvalidOperationException(Strings.ErrorReadDatabaseSecurityDescriptor(mailbox.Database.ToString())), ExchangeErrorCategory.ServerOperation, null);
					return null;
				}
				rawSecurityDescriptor = MailboxSecurity.CreateMailboxSecurityDescriptor(SecurityDescriptor.FromRawSecurityDescriptor(rawSecurityDescriptor2), SecurityDescriptor.FromRawSecurityDescriptor(exchangeSecurityDescriptor)).ToRawSecurityDescriptor();
				if (rawSecurityDescriptor == null)
				{
					logError(new TaskInvalidOperationException(Strings.ErrorReadMailboxSecurityDescriptor(mailbox.DistinguishedName)), ExchangeErrorCategory.ServerOperation, mailbox.Identity);
					return null;
				}
			}
			ActiveDirectorySecurity result = SecurityDescriptorConverter.ConvertToActiveDirectorySecurity(rawSecurityDescriptor);
			TaskLogger.LogExit();
			return result;
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x00031298 File Offset: 0x0002F498
		public static void SaveMailboxSecurityDescriptor(ADUser mailbox, ActiveDirectorySecurity adSecurity, IConfigDataProvider writableAdSession, ref MapiMessageStoreSession storeSession, Task.TaskVerboseLoggingDelegate logVerbose, Task.ErrorLoggerDelegate logError)
		{
			if (writableAdSession == null)
			{
				throw new ArgumentException("writableAdSession");
			}
			RawSecurityDescriptor rawSd = new RawSecurityDescriptor(adSecurity.GetSecurityDescriptorBinaryForm(), 0);
			PermissionTaskHelper.SaveAdSecurityDescriptor(mailbox, writableAdSession, rawSd, logVerbose, logError);
			string text = null;
			try
			{
				ActiveManager activeManagerInstance = ActiveManager.GetActiveManagerInstance();
				DatabaseLocationInfo serverForDatabase = activeManagerInstance.GetServerForDatabase(mailbox.Database.ObjectGuid);
				text = serverForDatabase.ServerFqdn;
				if (storeSession == null)
				{
					storeSession = new MapiMessageStoreSession(serverForDatabase.ServerLegacyDN, PermissionTaskHelper.CalcuteSystemAttendantMailboxLegacyDistingushName(serverForDatabase.ServerLegacyDN), Fqdn.Parse(serverForDatabase.ServerFqdn));
				}
				else
				{
					storeSession.RedirectServer(serverForDatabase.ServerLegacyDN, Fqdn.Parse(serverForDatabase.ServerFqdn));
				}
				MailboxId mailboxId = new MailboxId(MapiTaskHelper.ConvertDatabaseADObjectIdToDatabaseId(mailbox.Database), mailbox.ExchangeGuid);
				logVerbose(Strings.VerboseSaveStoreMailboxSecurityDescriptor(mailboxId.ToString(), storeSession.ServerName));
				storeSession.Administration.PurgeCachedMailboxObject(mailboxId.MailboxGuid);
			}
			catch (DatabaseNotFoundException)
			{
				logVerbose(Strings.ErrorMailboxDatabaseNotFound(mailbox.Database.ToString()));
			}
			catch (MapiExceptionNetworkError)
			{
				logVerbose(Strings.ErrorFailedToConnectToStore((text != null) ? text : string.Empty));
			}
			catch (FormatException)
			{
				logVerbose(Strings.ErrorInvalidServerLegacyDistinguishName(mailbox.DistinguishedName.ToString()));
			}
			catch (Microsoft.Exchange.Data.Mapi.Common.MailboxNotFoundException)
			{
				logVerbose(Strings.VerboseMailboxNotExistInStore(mailbox.DistinguishedName));
			}
			if (mailbox.HasLocalArchive)
			{
				PermissionTaskHelper.SaveArchiveSecurityDescriptor(mailbox, writableAdSession, rawSd, logVerbose, logError);
			}
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00031420 File Offset: 0x0002F620
		private static void SaveArchiveSecurityDescriptor(ADUser mailbox, IConfigDataProvider writableAdSession, RawSecurityDescriptor rawSd, Task.TaskVerboseLoggingDelegate logVerbose, Task.ErrorLoggerDelegate logError)
		{
			ADObjectId adobjectId = mailbox.ArchiveDatabase ?? mailbox.Database;
			MailboxId mailboxId = new MailboxId(MapiTaskHelper.ConvertDatabaseADObjectIdToDatabaseId(adobjectId), mailbox.ArchiveGuid);
			try
			{
				ActiveManager activeManagerInstance = ActiveManager.GetActiveManagerInstance();
				DatabaseLocationInfo serverForDatabase = activeManagerInstance.GetServerForDatabase(adobjectId.ObjectGuid);
				using (MapiMessageStoreSession mapiMessageStoreSession = new MapiMessageStoreSession(serverForDatabase.ServerLegacyDN, PermissionTaskHelper.CalcuteSystemAttendantMailboxLegacyDistingushName(serverForDatabase.ServerLegacyDN), Fqdn.Parse(serverForDatabase.ServerFqdn)))
				{
					logVerbose(Strings.VerboseSaveStoreMailboxSecurityDescriptor(mailboxId.ToString(), mapiMessageStoreSession.ServerName));
					mapiMessageStoreSession.ForceStoreToRefreshMailbox(mailboxId);
				}
			}
			catch (FormatException)
			{
				logError(new TaskInvalidOperationException(Strings.ErrorInvalidServerLegacyDistinguishName(mailbox.DistinguishedName.ToString())), ExchangeErrorCategory.ServerOperation, null);
			}
			catch (Microsoft.Exchange.Data.Mapi.Common.MailboxNotFoundException)
			{
				logVerbose(Strings.VerboseArchiveNotExistInStore(mailbox.Name));
				PermissionTaskHelper.SaveAdSecurityDescriptor(mailbox, writableAdSession, rawSd, logVerbose, logError);
			}
			catch (LocalizedException exception)
			{
				logError(new SetArchivePermissionException(mailbox.Name, exception), ExchangeErrorCategory.ServerOperation, null);
			}
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0003154C File Offset: 0x0002F74C
		private static void SaveAdSecurityDescriptor(ADUser mailbox, IConfigDataProvider writableAdSession, RawSecurityDescriptor rawSd, Task.TaskVerboseLoggingDelegate logVerbose, Task.ErrorLoggerDelegate logError)
		{
			if (writableAdSession != null)
			{
				ADUser aduser = mailbox;
				if (mailbox.IsReadOnly)
				{
					aduser = (ADUser)writableAdSession.Read<ADUser>(mailbox.Id);
				}
				if (aduser != null)
				{
					aduser.ExchangeSecurityDescriptor = rawSd;
					logVerbose(Strings.VerboseSaveADSecurityDescriptor(aduser.Id.ToString()));
					writableAdSession.Save(aduser);
					return;
				}
				logError(new DirectoryObjectNotFoundException(mailbox.Id.DistinguishedName), ExchangeErrorCategory.ServerOperation, null);
			}
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x000315C0 File Offset: 0x0002F7C0
		public static void SetMailboxAces(ADUser mailbox, IConfigDataProvider writableAdSession, Task.TaskVerboseLoggingDelegate logVerbose, Task.TaskWarningLoggingDelegate logWarning, Task.ErrorLoggerDelegate logError, IConfigurationSession adSession, ref MapiMessageStoreSession storeSession, bool remove, params ActiveDirectoryAccessRule[] aces)
		{
			ActiveDirectorySecurity activeDirectorySecurity = PermissionTaskHelper.ReadMailboxSecurityDescriptor(mailbox, adSession, logVerbose, logError);
			if (activeDirectorySecurity != null)
			{
				DirectoryCommon.ApplyAcesOnAcl(logVerbose, logWarning, null, mailbox.DistinguishedName, activeDirectorySecurity, remove, aces);
				PermissionTaskHelper.SaveMailboxSecurityDescriptor(mailbox, activeDirectorySecurity, writableAdSession, ref storeSession, logVerbose, logError);
			}
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00031600 File Offset: 0x0002F800
		public static void SetDelegation(ADUser principalUser, ADRecipient delegateRecipient, IConfigDataProvider writableAdSession, Task.TaskVerboseLoggingDelegate verboseLogger, bool remove)
		{
			if (!principalUser.ExchangeVersion.IsOlderThan(ADMailboxRecipientSchema.DelegateListLink.VersionAdded))
			{
				LocalizedString message;
				if (remove)
				{
					if (principalUser.DelegateListLink.Contains(delegateRecipient.Id))
					{
						principalUser.DelegateListLink.Remove(delegateRecipient.Id);
						writableAdSession.Save(principalUser);
						message = Strings.VerboseMailboxDelegateRemoved(delegateRecipient.ToString(), principalUser.ToString());
					}
					else
					{
						message = Strings.VerboseMailboxDelegateNotExits(delegateRecipient.ToString(), principalUser.ToString());
					}
				}
				else if (!principalUser.DelegateListLink.Contains(delegateRecipient.Id))
				{
					principalUser.DelegateListLink.Add(delegateRecipient.Id);
					writableAdSession.Save(principalUser);
					message = Strings.VerboseMailboxDelegateAdded(delegateRecipient.ToString(), principalUser.ToString());
				}
				else
				{
					message = Strings.VerboseMailboxDelegateAlreadyExists(delegateRecipient.ToString(), principalUser.ToString());
				}
				verboseLogger(message);
			}
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x000316D7 File Offset: 0x0002F8D7
		private static string CalcuteSystemAttendantMailboxLegacyDistingushName(string serverLegacyDistingushName)
		{
			return string.Format("{0}/cn=Microsoft System Attendant", serverLegacyDistingushName);
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x000316E4 File Offset: 0x0002F8E4
		internal static ActiveDirectoryAccessRule[] GetAcesToServerAdmin(IConfigurationSession configSession, SecurityIdentifier sid)
		{
			return new ActiveDirectoryAccessRule[]
			{
				new ActiveDirectoryAccessRule(sid, ActiveDirectoryRights.GenericAll, AccessControlType.Allow, ActiveDirectorySecurityInheritance.All),
				new ActiveDirectoryAccessRule(sid, ActiveDirectoryRights.ExtendedRight, AccessControlType.Deny, WellKnownGuid.SendAsExtendedRightGuid, ActiveDirectorySecurityInheritance.All),
				new ActiveDirectoryAccessRule(sid, ActiveDirectoryRights.ExtendedRight, AccessControlType.Deny, WellKnownGuid.ReceiveAsExtendedRightGuid, ActiveDirectorySecurityInheritance.All),
				new ActiveDirectoryAccessRule(sid, ActiveDirectoryRights.CreateChild | ActiveDirectoryRights.DeleteChild, AccessControlType.Deny, DirectoryCommon.GetSchemaClassGuid(configSession, "msExchPublicMDB"), ActiveDirectorySecurityInheritance.All)
			};
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0003174C File Offset: 0x0002F94C
		internal static SecurityIdentifier[] GetServerAdmins(Server server, IDirectorySession session, Task.TaskErrorLoggingDelegate logError)
		{
			List<SecurityIdentifier> list = new List<SecurityIdentifier>();
			ActiveDirectorySecurity activeDirectorySecurity = PermissionTaskHelper.ReadAdSecurityDescriptor(server, session, logError);
			AuthorizationRuleCollection accessRules = activeDirectorySecurity.GetAccessRules(true, false, typeof(SecurityIdentifier));
			foreach (object obj in accessRules)
			{
				ActiveDirectoryAccessRule activeDirectoryAccessRule = (ActiveDirectoryAccessRule)obj;
				if (activeDirectoryAccessRule.ActiveDirectoryRights == ActiveDirectoryRights.GenericAll)
				{
					SecurityIdentifier item = (SecurityIdentifier)activeDirectoryAccessRule.IdentityReference;
					list.Add(item);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x000317EC File Offset: 0x0002F9EC
		internal static IConfigurationSession GetReadOnlySession(Fqdn domainController)
		{
			IConfigurationSession configurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(domainController, true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 453, "GetReadOnlySession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Common\\recipient\\PermissionTaskHelper.cs");
			configurationSession.UseConfigNC = false;
			configurationSession.EnforceDefaultScope = false;
			configurationSession.UseGlobalCatalog = true;
			configurationSession.UseGlobalCatalog = configurationSession.IsReadConnectionAvailable();
			return configurationSession;
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00031844 File Offset: 0x0002FA44
		internal static IRecipientSession GetReadOnlyRecipientSession(Fqdn domainController)
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(domainController, true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 474, "GetReadOnlyRecipientSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Common\\recipient\\PermissionTaskHelper.cs");
			tenantOrRootOrgRecipientSession.EnforceDefaultScope = false;
			tenantOrRootOrgRecipientSession.UseGlobalCatalog = true;
			tenantOrRootOrgRecipientSession.UseGlobalCatalog = tenantOrRootOrgRecipientSession.IsReadConnectionAvailable();
			return tenantOrRootOrgRecipientSession;
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00031894 File Offset: 0x0002FA94
		internal static IConfigurationSession GetWritableSession(Fqdn domainController)
		{
			IConfigurationSession configurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(domainController, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 494, "GetWritableSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Common\\recipient\\PermissionTaskHelper.cs");
			configurationSession.UseConfigNC = false;
			configurationSession.EnforceDefaultScope = false;
			configurationSession.LinkResolutionServer = ADSession.GetCurrentConfigDC(configurationSession.SessionSettings.GetAccountOrResourceForestFqdn());
			return configurationSession;
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x000318F0 File Offset: 0x0002FAF0
		internal static IRecipientSession GetWritableRecipientSession(Fqdn domainController)
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(domainController, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 514, "GetWritableRecipientSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Common\\recipient\\PermissionTaskHelper.cs");
			tenantOrRootOrgRecipientSession.EnforceDefaultScope = false;
			tenantOrRootOrgRecipientSession.LinkResolutionServer = ADSession.GetCurrentConfigDC(tenantOrRootOrgRecipientSession.SessionSettings.GetAccountOrResourceForestFqdn());
			return tenantOrRootOrgRecipientSession;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00031944 File Offset: 0x0002FB44
		internal static IConfigurable ResolveDataObject(IConfigDataProvider readOnlySession, IConfigDataProvider readOnlyConfigurationSession, IConfigDataProvider globalCatalogSession, IIdentityParameter identity, DataAccessHelper.GetDataObjectDelegate getDataObjectHandler, Task.TaskVerboseLoggingDelegate logHandler)
		{
			IConfigurable configurable = null;
			ADObjectId adobjectId = null;
			ADObjectId rootID = RecipientTaskHelper.IsValidDistinguishedName(identity, out adobjectId) ? adobjectId.Parent : null;
			Exception innerException = null;
			if (readOnlySession != null)
			{
				try
				{
					configurable = getDataObjectHandler(identity, readOnlySession, rootID, null, null, new LocalizedString?(Strings.ErrorObjectNotUnique(identity.ToString())));
				}
				catch (ADTransientException ex)
				{
					innerException = ex;
					logHandler(Strings.VerboseCannotReadObject(identity.ToString(), readOnlySession.Source, ex.Message));
				}
				catch (ManagementObjectNotFoundException ex2)
				{
					innerException = ex2;
					logHandler(Strings.VerboseCannotReadObject(identity.ToString(), readOnlySession.Source, ex2.Message));
				}
			}
			if (configurable == null && readOnlyConfigurationSession != null)
			{
				try
				{
					configurable = getDataObjectHandler(identity, readOnlyConfigurationSession, rootID, null, null, new LocalizedString?(Strings.ErrorObjectNotUnique(identity.ToString())));
				}
				catch (ADTransientException ex3)
				{
					innerException = ex3;
					logHandler(Strings.VerboseCannotReadObject(identity.ToString(), readOnlyConfigurationSession.Source, ex3.Message));
				}
				catch (ManagementObjectNotFoundException ex4)
				{
					innerException = ex4;
					logHandler(Strings.VerboseCannotReadObject(identity.ToString(), readOnlyConfigurationSession.Source, ex4.Message));
				}
			}
			if (configurable == null && globalCatalogSession != null)
			{
				try
				{
					configurable = getDataObjectHandler(identity, globalCatalogSession, rootID, null, null, new LocalizedString?(Strings.ErrorObjectNotUnique(identity.ToString())));
				}
				catch (ADTransientException ex5)
				{
					innerException = ex5;
					logHandler(Strings.VerboseCannotReadObject(identity.ToString(), globalCatalogSession.Source, ex5.Message));
				}
				catch (ManagementObjectNotFoundException ex6)
				{
					innerException = ex6;
					logHandler(Strings.VerboseCannotReadObject(identity.ToString(), globalCatalogSession.Source, ex6.Message));
				}
			}
			if (configurable == null)
			{
				throw new ManagementObjectNotFoundException(Strings.ErrorObjectNotFound(identity.ToString()), innerException);
			}
			return configurable;
		}
	}
}
