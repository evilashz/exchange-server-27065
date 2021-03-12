using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Globalization;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Security;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Common;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mapi;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Management.EventMessages;
using Microsoft.Exchange.Management.MapiTasks;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.OAB;
using Microsoft.Exchange.Provisioning.LoadBalancing;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Mapi;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Common
{
	// Token: 0x0200011C RID: 284
	internal static class MailboxTaskHelper
	{
		// Token: 0x060009A8 RID: 2472 RVA: 0x0002AC58 File Offset: 0x00028E58
		internal static SecurityIdentifier GetAccountSidFromAnotherForest(IIdentityParameter id, string userForestDomainController, NetworkCredential userForestCredential, ITopologyConfigurationSession resourceForestSession, MailboxTaskHelper.GetUniqueObject getUniqueObject, Task.ErrorLoggerDelegate errorHandler)
		{
			return MailboxTaskHelper.GetSidFromAnotherForest<ADUser>(id, userForestDomainController, userForestCredential, resourceForestSession, getUniqueObject, errorHandler, new MailboxTaskHelper.OneStringErrorDelegate(Strings.ErrorLinkedAccountInTheCurrentForest), new MailboxTaskHelper.TwoStringErrorDelegate(Strings.ErrorUserNotFoundOnGlobalCatalog), new MailboxTaskHelper.TwoStringErrorDelegate(Strings.ErrorUserNotFoundOnDomainController), new MailboxTaskHelper.TwoStringErrorDelegate(Strings.ErrorUserNotUniqueOnGlobalCatalog), new MailboxTaskHelper.TwoStringErrorDelegate(Strings.ErrorUserNotUniqueOnDomainController), new MailboxTaskHelper.OneStringErrorDelegate(Strings.ErrorVerifyLinkedForest));
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0002ACBC File Offset: 0x00028EBC
		internal static SecurityIdentifier GetSidFromAnotherForest<TObject>(IIdentityParameter id, string userForestDomainController, NetworkCredential userForestCredential, ITopologyConfigurationSession resourceForestSession, MailboxTaskHelper.GetUniqueObject getUniqueObject, Task.ErrorLoggerDelegate errorHandler, MailboxTaskHelper.OneStringErrorDelegate linkedObjectInCurrentForest, MailboxTaskHelper.TwoStringErrorDelegate linkedObjectNotFoundOnGC, MailboxTaskHelper.TwoStringErrorDelegate linkedObjectNotFoundOnDC, MailboxTaskHelper.TwoStringErrorDelegate linkedObjectNotUniqueOnGC, MailboxTaskHelper.TwoStringErrorDelegate linkedObjectNotUniqueOnDC, MailboxTaskHelper.OneStringErrorDelegate linkedObjectVerifyForest) where TObject : IADSecurityPrincipal
		{
			try
			{
				SecurityIdentifier securityIdentifier = new SecurityIdentifier(id.RawIdentity);
				if (securityIdentifier != null)
				{
					if (!MailboxTaskHelper.IsMasterAccountAlreadyExist(resourceForestSession, securityIdentifier, id, errorHandler))
					{
						return securityIdentifier;
					}
					return null;
				}
			}
			catch (ArgumentException)
			{
			}
			try
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(userForestDomainController, true, ConsistencyMode.PartiallyConsistent, userForestCredential, ADSessionSettings.FromRootOrgScopeSet(), 448, "GetSidFromAnotherForest", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Common\\recipient\\MailboxTaskHelper.cs");
				string rootDomainNamingContextFromCurrentReadConnection = resourceForestSession.GetRootDomainNamingContextFromCurrentReadConnection();
				string rootDomainNamingContextFromCurrentReadConnection2 = topologyConfigurationSession.GetRootDomainNamingContextFromCurrentReadConnection();
				if (string.Equals(rootDomainNamingContextFromCurrentReadConnection, rootDomainNamingContextFromCurrentReadConnection2, StringComparison.OrdinalIgnoreCase))
				{
					errorHandler(new TaskInvalidOperationException(linkedObjectInCurrentForest(NativeHelpers.CanonicalNameFromDistinguishedName(rootDomainNamingContextFromCurrentReadConnection))), ExchangeErrorCategory.Client, null);
				}
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(userForestDomainController, true, ConsistencyMode.PartiallyConsistent, userForestCredential, ADSessionSettings.FromRootOrgScopeSet(), 466, "GetSidFromAnotherForest", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Common\\recipient\\MailboxTaskHelper.cs");
				tenantOrRootOrgRecipientSession.UseGlobalCatalog = true;
				if (!tenantOrRootOrgRecipientSession.IsReadConnectionAvailable())
				{
					tenantOrRootOrgRecipientSession.UseGlobalCatalog = false;
				}
				TObject tobject = (TObject)((object)getUniqueObject(id, tenantOrRootOrgRecipientSession, null, new LocalizedString?(tenantOrRootOrgRecipientSession.UseGlobalCatalog ? linkedObjectNotFoundOnGC(id.ToString(), userForestDomainController) : linkedObjectNotFoundOnDC(id.ToString(), userForestDomainController)), new LocalizedString?(tenantOrRootOrgRecipientSession.UseGlobalCatalog ? linkedObjectNotUniqueOnGC(id.ToString(), userForestDomainController) : linkedObjectNotUniqueOnDC(id.ToString(), userForestDomainController)), ExchangeErrorCategory.Client));
				if (tobject != null)
				{
					SecurityIdentifier sid = tobject.Sid;
					if (!(tobject is ADUser))
					{
						return sid;
					}
					if (!MailboxTaskHelper.IsMasterAccountAlreadyExist(resourceForestSession, sid, id, errorHandler))
					{
						return sid;
					}
				}
			}
			catch (DataSourceTransientException ex)
			{
				errorHandler(new TaskInvalidOperationException(linkedObjectVerifyForest(ex.Message), ex), ExchangeErrorCategory.ServerTransient, null);
			}
			catch (DataSourceOperationException ex2)
			{
				errorHandler(new TaskInvalidOperationException(linkedObjectVerifyForest(ex2.Message), ex2), ExchangeErrorCategory.ServerOperation, null);
			}
			return null;
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0002AEE4 File Offset: 0x000290E4
		internal static bool IsMasterAccountAlreadyExist(ITopologyConfigurationSession resourceForestSession, SecurityIdentifier sid, IIdentityParameter id, Task.ErrorLoggerDelegate errorHandler)
		{
			IRecipientSession recipientSession = DirectorySessionFactory.Default.CreateRootOrgRecipientSession(resourceForestSession.DomainController, null, CultureInfo.CurrentCulture.LCID, true, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.FromRootOrgScopeSet(), 526, "IsMasterAccountAlreadyExist", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Common\\recipient\\MailboxTaskHelper.cs");
			if (!recipientSession.IsReadConnectionAvailable())
			{
				recipientSession = DirectorySessionFactory.Default.CreateRootOrgRecipientSession(true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 538, "IsMasterAccountAlreadyExist", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Common\\recipient\\MailboxTaskHelper.cs");
			}
			recipientSession.UseGlobalCatalog = true;
			IConfigurable[] array = recipientSession.Find(null, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.MasterAccountSid, sid), null, 1);
			if (array.Length == 0)
			{
				return false;
			}
			errorHandler(new TaskInvalidOperationException(Strings.ErrorLinkedUserAccountIsAlreadyUsed(id.ToString(), array[0].Identity.ToString())), ExchangeErrorCategory.Client, null);
			return true;
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0002AF9C File Offset: 0x0002919C
		internal static void GrantPermissionToLinkedUserAccount(ADUser user, IConfigurationSession adConfigSession, Task.ErrorLoggerDelegate errorHandler, Task.TaskVerboseLoggingDelegate logHandler)
		{
			RawSecurityDescriptor exchangeSecurityDescriptor = null;
			if (Guid.Empty != user.ExchangeGuid && user.Database != null)
			{
				ActiveDirectorySecurity activeDirectorySecurity = PermissionTaskHelper.ReadMailboxSecurityDescriptor(user, adConfigSession, logHandler, errorHandler);
				exchangeSecurityDescriptor = new RawSecurityDescriptor(activeDirectorySecurity.GetSecurityDescriptorBinaryForm(), 0);
			}
			logHandler(Strings.VerboseReadADSecurityDescriptor(user.Id.ToString()));
			RawSecurityDescriptor rawSecurityDescriptor = user.ReadSecurityDescriptor();
			MailboxTaskHelper.GrantPermissionToLinkedUserAccount(user.MasterAccountSid, ref exchangeSecurityDescriptor, ref rawSecurityDescriptor);
			user.ExchangeSecurityDescriptor = exchangeSecurityDescriptor;
			user.propertyBag.SetField(ADObjectSchema.NTSecurityDescriptor, SecurityDescriptor.FromRawSecurityDescriptor(rawSecurityDescriptor));
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0002B026 File Offset: 0x00029226
		internal static void GrantPermissionToLinkedUserAccount(ADUser user, Task.TaskVerboseLoggingDelegate logHandler)
		{
			MailboxTaskHelper.GrantPermissionToLinkedUserAccounts(user, null, logHandler);
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0002B030 File Offset: 0x00029230
		internal static void GrantPermissionToLinkedUserAccounts(ADUser user, SecurityIdentifier[] altUserSids, Task.TaskVerboseLoggingDelegate logHandler)
		{
			RawSecurityDescriptor exchangeSecurityDescriptor = user.ExchangeSecurityDescriptor;
			logHandler(Strings.VerboseReadADSecurityDescriptor(user.Id.ToString()));
			RawSecurityDescriptor rawSecurityDescriptor = user.ReadSecurityDescriptor();
			MailboxTaskHelper.GrantPermissionToLinkedUserAccounts(user.MasterAccountSid, altUserSids, ref exchangeSecurityDescriptor, ref rawSecurityDescriptor);
			user.ExchangeSecurityDescriptor = exchangeSecurityDescriptor;
			user.propertyBag.SetField(ADObjectSchema.NTSecurityDescriptor, SecurityDescriptor.FromRawSecurityDescriptor(rawSecurityDescriptor));
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0002B090 File Offset: 0x00029290
		internal static void ClearExternalAssociatedAccountPermission(ADUser user, IConfigurationSession adConfigSession, Task.ErrorLoggerDelegate errorHandler, Task.TaskVerboseLoggingDelegate logHandler)
		{
			ActiveDirectorySecurity activeDirectorySecurity = PermissionTaskHelper.ReadMailboxSecurityDescriptor(user, adConfigSession, logHandler, errorHandler);
			RawSecurityDescriptor rawSecurityDescriptor = new RawSecurityDescriptor(activeDirectorySecurity.GetSecurityDescriptorBinaryForm(), 0);
			RawAcl discretionaryAcl = rawSecurityDescriptor.DiscretionaryAcl;
			MailboxTaskHelper.ClearExternalAssociatedAccountPermission(discretionaryAcl);
			rawSecurityDescriptor.DiscretionaryAcl = discretionaryAcl;
			user.ExchangeSecurityDescriptor = rawSecurityDescriptor;
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0002B0D0 File Offset: 0x000292D0
		private static void ClearExternalAssociatedAccountPermission(RawAcl dacl)
		{
			if (dacl != null)
			{
				for (int i = 0; i < dacl.Count; i++)
				{
					if (dacl[i] is CommonAce)
					{
						CommonAce commonAce = (CommonAce)dacl[i];
						if ((4 & commonAce.AccessMask) != 0)
						{
							commonAce.AccessMask &= -5;
							dacl[i] = commonAce;
						}
					}
				}
			}
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0002B12D File Offset: 0x0002932D
		internal static void GrantPermissionToLinkedUserAccount(SecurityIdentifier linkedUserSid, ref RawSecurityDescriptor exchangeSecurityDescriptor, ref RawSecurityDescriptor ntSecurityDescriptor)
		{
			MailboxTaskHelper.GrantPermissionToLinkedUserAccounts(linkedUserSid, null, ref exchangeSecurityDescriptor, ref ntSecurityDescriptor);
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0002B138 File Offset: 0x00029338
		internal static void GrantPermissionToLinkedUserAccounts(SecurityIdentifier linkedUserSid, SecurityIdentifier[] altUserSids, ref RawSecurityDescriptor exchangeSecurityDescriptor, ref RawSecurityDescriptor ntSecurityDescriptor)
		{
			DiscretionaryAcl discretionaryAcl2;
			if (exchangeSecurityDescriptor != null)
			{
				byte[] binaryForm = new byte[exchangeSecurityDescriptor.BinaryLength];
				exchangeSecurityDescriptor.GetBinaryForm(binaryForm, 0);
				exchangeSecurityDescriptor = new RawSecurityDescriptor(binaryForm, 0);
				RawAcl discretionaryAcl = exchangeSecurityDescriptor.DiscretionaryAcl;
				MailboxTaskHelper.ClearExternalAssociatedAccountPermission(discretionaryAcl);
				discretionaryAcl2 = new DiscretionaryAcl(true, true, discretionaryAcl);
			}
			else
			{
				using (WindowsIdentity current = WindowsIdentity.GetCurrent())
				{
					SecurityIdentifier user = current.User;
					exchangeSecurityDescriptor = new RawSecurityDescriptor(ControlFlags.DiscretionaryAclDefaulted | ControlFlags.SystemAclDefaulted | ControlFlags.SelfRelative, user, user, null, null);
				}
				discretionaryAcl2 = new DiscretionaryAcl(true, true, 1);
			}
			DiscretionaryAcl discretionaryAcl3;
			if (ntSecurityDescriptor != null)
			{
				discretionaryAcl3 = new DiscretionaryAcl(true, true, ntSecurityDescriptor.DiscretionaryAcl);
			}
			else
			{
				using (WindowsIdentity current2 = WindowsIdentity.GetCurrent())
				{
					SecurityIdentifier user2 = current2.User;
					ntSecurityDescriptor = new RawSecurityDescriptor(ControlFlags.DiscretionaryAclDefaulted | ControlFlags.SystemAclDefaulted | ControlFlags.SelfRelative, user2, user2, null, null);
					discretionaryAcl3 = new DiscretionaryAcl(true, true, 2);
				}
			}
			discretionaryAcl2.AddAccess(AccessControlType.Allow, linkedUserSid, 5, InheritanceFlags.ContainerInherit, PropagationFlags.None);
			discretionaryAcl3.AddAccess(AccessControlType.Allow, linkedUserSid, 256, InheritanceFlags.ContainerInherit, PropagationFlags.None, ObjectAceFlags.ObjectAceTypePresent, WellKnownGuid.SendAsExtendedRightGuid, Guid.Empty);
			if (altUserSids != null)
			{
				foreach (SecurityIdentifier sid in altUserSids)
				{
					discretionaryAcl3.AddAccess(AccessControlType.Allow, sid, 256, InheritanceFlags.ContainerInherit, PropagationFlags.None, ObjectAceFlags.ObjectAceTypePresent, WellKnownGuid.SendAsExtendedRightGuid, Guid.Empty);
				}
			}
			discretionaryAcl3.AddAccess(AccessControlType.Allow, linkedUserSid, 48, InheritanceFlags.ContainerInherit, PropagationFlags.None, ObjectAceFlags.ObjectAceTypePresent, WellKnownGuid.PersonalInfoPropSetGuid, Guid.Empty);
			byte[] binaryForm2 = new byte[discretionaryAcl2.BinaryLength];
			byte[] binaryForm3 = new byte[discretionaryAcl3.BinaryLength];
			discretionaryAcl2.GetBinaryForm(binaryForm2, 0);
			discretionaryAcl3.GetBinaryForm(binaryForm3, 0);
			exchangeSecurityDescriptor.DiscretionaryAcl = new RawAcl(binaryForm2, 0);
			ntSecurityDescriptor.DiscretionaryAcl = new RawAcl(binaryForm3, 0);
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0002B2E8 File Offset: 0x000294E8
		internal static ADRecipient FindConnectedMailbox(IRecipientSession globalCatalogSession, Guid mailboxGuid, Task.TaskVerboseLoggingDelegate logHandler)
		{
			OrFilter filter = new OrFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADMailboxRecipientSchema.ExchangeGuid, mailboxGuid),
				new ComparisonFilter(ComparisonOperator.Equal, ADUserSchema.ArchiveGuid, mailboxGuid)
			});
			logHandler(TaskVerboseStringHelper.GetFindDataObjectsVerboseString(globalCatalogSession, typeof(ADRecipient), filter, null, true));
			ADUser[] array = null;
			try
			{
				array = globalCatalogSession.FindADUser(null, QueryScope.SubTree, filter, null, 0);
			}
			finally
			{
				logHandler(TaskVerboseStringHelper.GetSourceVerboseString(globalCatalogSession));
			}
			if (array != null && array.Length > 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if ((array[i].ExchangeGuid == mailboxGuid && array[i].Database != null) || (array[i].ArchiveGuid == mailboxGuid && array[i].ArchiveState == ArchiveState.Local))
					{
						return array[i];
					}
					TaskLogger.Trace("User {0} has ExchangeGuid/ArchiveGuid pointing to mailbox {1} but is not really connected to it.", new object[]
					{
						array[i].Identity,
						mailboxGuid
					});
				}
			}
			return null;
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0002B3EC File Offset: 0x000295EC
		internal static void ValidateMailboxIsDisconnected(IRecipientSession globalCatalogSession, Guid mailboxGuid, Task.TaskVerboseLoggingDelegate logHandler, Task.ErrorLoggerDelegate errorHandler)
		{
			ADRecipient adrecipient = MailboxTaskHelper.FindConnectedMailbox(globalCatalogSession, mailboxGuid, logHandler);
			if (adrecipient != null)
			{
				errorHandler(new TaskInvalidOperationException(Strings.ErrorMailboxIsConnected(adrecipient.DisplayName, mailboxGuid.ToString())), ExchangeErrorCategory.Client, null);
			}
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0002B430 File Offset: 0x00029630
		internal static void StampMailboxRecipientTypes(ADRecipient recipient, string parameterSetName)
		{
			if (parameterSetName == "Linked" || parameterSetName == "LinkedWithSyncMailbox")
			{
				recipient.RecipientTypeDetails = RecipientTypeDetails.LinkedMailbox;
			}
			else if (parameterSetName == "Shared")
			{
				recipient.RecipientTypeDetails = RecipientTypeDetails.SharedMailbox;
			}
			else if (parameterSetName == "TeamMailboxIW" || parameterSetName == "TeamMailboxITPro")
			{
				recipient.RecipientTypeDetails = RecipientTypeDetails.TeamMailbox;
			}
			else if (parameterSetName == "Room" || parameterSetName == "EnableRoomMailboxAccount")
			{
				recipient.RecipientTypeDetails = RecipientTypeDetails.RoomMailbox;
			}
			else if (parameterSetName == "LinkedRoomMailbox")
			{
				recipient.RecipientTypeDetails = RecipientTypeDetails.LinkedRoomMailbox;
			}
			else if (parameterSetName == "Equipment")
			{
				recipient.RecipientTypeDetails = RecipientTypeDetails.EquipmentMailbox;
			}
			else if (parameterSetName == "Arbitration")
			{
				recipient.RecipientTypeDetails = RecipientTypeDetails.ArbitrationMailbox;
			}
			else if (parameterSetName == "Discovery")
			{
				recipient.RecipientTypeDetails = RecipientTypeDetails.DiscoveryMailbox;
			}
			else if (parameterSetName == "MailboxPlan")
			{
				recipient.RecipientTypeDetails = RecipientTypeDetails.MailboxPlan;
			}
			else if (parameterSetName == "PublicFolder")
			{
				recipient.RecipientTypeDetails = RecipientTypeDetails.PublicFolderMailbox;
			}
			else if (parameterSetName == "GroupMailbox")
			{
				recipient.RecipientTypeDetails = RecipientTypeDetails.GroupMailbox;
			}
			else if (parameterSetName == "AuditLog")
			{
				recipient.RecipientTypeDetails = RecipientTypeDetails.AuditLogMailbox;
			}
			else if (parameterSetName == "Monitoring")
			{
				recipient.RecipientTypeDetails = RecipientTypeDetails.MonitoringMailbox;
			}
			else
			{
				recipient.RecipientTypeDetails = RecipientTypeDetails.UserMailbox;
			}
			MailboxTaskHelper.StampMailboxRecipientDisplayType(recipient);
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0002B5F0 File Offset: 0x000297F0
		internal static void StampMailboxRecipientDisplayType(ADRecipient recipient)
		{
			if (RecipientTypeDetails.LinkedMailbox == recipient.RecipientTypeDetails)
			{
				recipient.RecipientDisplayType = new RecipientDisplayType?(RecipientDisplayType.ACLableMailboxUser);
				return;
			}
			if (RecipientTypeDetails.SharedMailbox == recipient.RecipientTypeDetails)
			{
				recipient.RecipientDisplayType = new RecipientDisplayType?(RecipientDisplayType.MailboxUser);
				return;
			}
			if (RecipientTypeDetails.TeamMailbox == recipient.RecipientTypeDetails)
			{
				recipient.RecipientDisplayType = new RecipientDisplayType?(RecipientDisplayType.TeamMailboxUser);
				return;
			}
			if (RecipientTypeDetails.RoomMailbox == recipient.RecipientTypeDetails || RecipientTypeDetails.LinkedRoomMailbox == recipient.RecipientTypeDetails)
			{
				recipient.RecipientDisplayType = new RecipientDisplayType?(RecipientDisplayType.ConferenceRoomMailbox);
				return;
			}
			if (RecipientTypeDetails.EquipmentMailbox == recipient.RecipientTypeDetails)
			{
				recipient.RecipientDisplayType = new RecipientDisplayType?(RecipientDisplayType.EquipmentMailbox);
				return;
			}
			if (RecipientTypeDetails.UserMailbox == recipient.RecipientTypeDetails)
			{
				recipient.RecipientDisplayType = new RecipientDisplayType?(RecipientDisplayType.ACLableMailboxUser);
				return;
			}
			if (RecipientTypeDetails.LegacyMailbox == recipient.RecipientTypeDetails)
			{
				recipient.RecipientDisplayType = new RecipientDisplayType?(RecipientDisplayType.ACLableMailboxUser);
				return;
			}
			if (RecipientTypeDetails.ArbitrationMailbox == recipient.RecipientTypeDetails)
			{
				recipient.RecipientDisplayType = new RecipientDisplayType?(RecipientDisplayType.ArbitrationMailbox);
				return;
			}
			if (RecipientTypeDetails.MailboxPlan == recipient.RecipientTypeDetails)
			{
				recipient.RecipientDisplayType = new RecipientDisplayType?(RecipientDisplayType.MailboxPlan);
				return;
			}
			if (RecipientTypeDetails.LinkedUser == recipient.RecipientTypeDetails)
			{
				recipient.RecipientDisplayType = new RecipientDisplayType?(RecipientDisplayType.LinkedUser);
				return;
			}
			if (RecipientTypeDetails.RoomList == recipient.RecipientTypeDetails)
			{
				recipient.RecipientDisplayType = new RecipientDisplayType?(RecipientDisplayType.DistributionGroup);
				return;
			}
			if (RecipientTypeDetails.GroupMailbox == recipient.RecipientTypeDetails)
			{
				recipient.RecipientDisplayType = new RecipientDisplayType?(RecipientDisplayType.GroupMailboxUser);
				return;
			}
			if (RecipientTypeDetails.DiscoveryMailbox == recipient.RecipientTypeDetails)
			{
				recipient.RecipientDisplayType = null;
				return;
			}
			if (RecipientTypeDetails.AuditLogMailbox == recipient.RecipientTypeDetails)
			{
				recipient.RecipientDisplayType = null;
			}
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0002B794 File Offset: 0x00029994
		internal static void IsLiveIdExists(IRecipientSession recipientSession, SmtpAddress windowsLiveID, NetID netId, Task.ErrorLoggerDelegate errorLogger)
		{
			ADUser aduser = MailboxTaskHelper.FindADUserByNetId(netId, recipientSession);
			if (aduser == null)
			{
				ADUser aduser2 = MailboxTaskHelper.FindADUserByWindowsLiveId(windowsLiveID, recipientSession);
				if (aduser2 != null)
				{
					errorLogger(new UserWithMatchingWindowsLiveIdAndDifferentNetIdExistsException(Strings.ErrorUserWithMatchingWindowsLiveIdAndDifferentNetIdExists(windowsLiveID.ToString(), aduser2.Identity.ToString())), ExchangeErrorCategory.Client, null);
				}
				return;
			}
			if (aduser.WindowsLiveID.Equals(windowsLiveID))
			{
				errorLogger(new WindowsLiveIdAlreadyUsedException(Strings.ErrorWindowsLiveIdAssociatedWithAnotherRecipient(windowsLiveID.ToString(), aduser.Identity.ToString())), ExchangeErrorCategory.WindowsLiveIdAlreadyUsed, null);
				return;
			}
			errorLogger(new UserWithMatchingNetIdAndDifferentWindowsLiveIdExistsException(Strings.ErrorUserWithMatchingNetIdAndDifferentWindowsLiveIdExists(windowsLiveID.ToString(), aduser.Identity.ToString())), ExchangeErrorCategory.Client, null);
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0002B854 File Offset: 0x00029A54
		internal static void IsMemberExists(IRecipientSession recipientSession, SmtpAddress windowsLiveID, Task.ErrorLoggerDelegate errorLogger)
		{
			ADUser aduser = MailboxTaskHelper.FindADUserByWindowsLiveId(windowsLiveID, recipientSession);
			if (aduser != null)
			{
				errorLogger(new UserWithMatchingWindowsLiveIdExistsException(Strings.ErrorUserWithMatchingWindowsLiveIdExists(windowsLiveID.ToString(), aduser.Identity.ToString())), ExchangeErrorCategory.Client, null);
			}
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0002B89C File Offset: 0x00029A9C
		private static ADUser FindADUserByNetId(NetID netId, IRecipientSession recipientSession)
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, IADSecurityPrincipalSchema.NetID, netId);
			ADUser[] array = recipientSession.FindADUser(null, QueryScope.SubTree, filter, null, 1);
			if (array == null || array.Length == 0)
			{
				return null;
			}
			return array[0];
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0002B8D0 File Offset: 0x00029AD0
		private static ADUser FindADUserByWindowsLiveId(SmtpAddress windowsLiveId, IRecipientSession recipientSession)
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.WindowsLiveID, windowsLiveId);
			ADUser[] array = recipientSession.FindADUser(null, QueryScope.SubTree, filter, null, 1);
			if (array == null || array.Length == 0)
			{
				return null;
			}
			return array[0];
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0002B908 File Offset: 0x00029B08
		public static void CheckNameAvailability(IRecipientSession recipientSession, string name, ADObjectId parentContainer, Task.ErrorLoggerDelegate errorLogger)
		{
			ADRecipient adrecipient = recipientSession.Read(parentContainer.GetChildId(name));
			if (adrecipient != null)
			{
				errorLogger(new NameNotAvailableException(Strings.ErrorNameNotAvailable(name)), ExchangeErrorCategory.Client, null);
			}
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0002B940 File Offset: 0x00029B40
		internal static string AppendRandomNameSuffix(string name)
		{
			return name.Substring(0, Math.Min(name.Length, 53)) + "_" + Guid.NewGuid().ToString("N").Substring(0, 10);
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0002B988 File Offset: 0x00029B88
		internal static void SetMailboxPassword(IRecipientSession recipientSession, ADUser user, string staticPassword, Task.ErrorLoggerDelegate errorLogger)
		{
			int num = 0;
			for (;;)
			{
				string password;
				if (staticPassword == null)
				{
					password = PasswordHelper.GetRandomPassword(user.DisplayName, user.SamAccountName, 128);
				}
				else
				{
					password = staticPassword;
				}
				try
				{
					using (SecureString secureString = password.ConvertToSecureString())
					{
						recipientSession.SetPassword(user, secureString);
					}
				}
				catch (ADInvalidPasswordException ex)
				{
					if (num != 3 && staticPassword == null)
					{
						num++;
						continue;
					}
					errorLogger(new InvalidADObjectOperationException(Strings.ErrorFailedToGenerateRandomPassword(password, user.DisplayName, user.SamAccountName, ex.Message)), ExchangeErrorCategory.ServerOperation, user.Identity);
				}
				break;
			}
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0002BA2C File Offset: 0x00029C2C
		internal static bool ExcludeArbitrationMailbox(ADRecipient mbx, bool showArbitration)
		{
			return mbx != null && ((!showArbitration && RecipientTypeDetails.ArbitrationMailbox == mbx.RecipientTypeDetails) || (showArbitration && RecipientTypeDetails.ArbitrationMailbox != mbx.RecipientTypeDetails));
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0002BA56 File Offset: 0x00029C56
		internal static bool ExcludePublicFolderMailbox(ADRecipient mbx, bool showPublicFolder)
		{
			return mbx != null && ((!showPublicFolder && RecipientTypeDetails.PublicFolderMailbox == mbx.RecipientTypeDetails) || (showPublicFolder && RecipientTypeDetails.PublicFolderMailbox != mbx.RecipientTypeDetails));
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0002BA86 File Offset: 0x00029C86
		internal static bool ExcludeMailboxPlan(ADRecipient mbx, bool showMailboxPlan)
		{
			return mbx != null && ((!showMailboxPlan && RecipientTypeDetails.MailboxPlan == mbx.RecipientTypeDetails) || (showMailboxPlan && RecipientTypeDetails.MailboxPlan != mbx.RecipientTypeDetails));
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0002BAB0 File Offset: 0x00029CB0
		internal static bool ExcludeGroupMailbox(ADRecipient mbx, bool showGroupMailbox)
		{
			return mbx != null && ((!showGroupMailbox && RecipientTypeDetails.GroupMailbox == mbx.RecipientTypeDetails) || (showGroupMailbox && RecipientTypeDetails.GroupMailbox != mbx.RecipientTypeDetails));
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0002BAE0 File Offset: 0x00029CE0
		internal static bool ExcludeTeamMailbox(ADRecipient mbx, bool showTeamMailbox)
		{
			return mbx != null && ((!showTeamMailbox && RecipientTypeDetails.TeamMailbox == mbx.RecipientTypeDetails) || (showTeamMailbox && RecipientTypeDetails.TeamMailbox != mbx.RecipientTypeDetails));
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0002BB10 File Offset: 0x00029D10
		internal static bool ExcludeAuditLogMailbox(ADRecipient mbx, bool showAuditLog)
		{
			return mbx != null && showAuditLog != (RecipientTypeDetails.AuditLogMailbox == mbx.RecipientTypeDetails);
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0002BB30 File Offset: 0x00029D30
		internal static void UpdateHostedExchangeSecurityGroupForMailbox(IRecipientSession recipientSession, OrganizationId orgId, Guid wellKnownGroup, ADUser mailbox, bool addMember, Task.TaskVerboseLoggingDelegate writeVerbose, Task.TaskWarningLoggingDelegate writeWarning)
		{
			if (null == orgId)
			{
				throw new ArgumentNullException("orgId");
			}
			if (OrganizationId.ForestWideOrgId.Equals(orgId))
			{
				return;
			}
			if (recipientSession == null)
			{
				throw new ArgumentNullException("recipientSession");
			}
			if (recipientSession.UseGlobalCatalog)
			{
				throw new ArgumentOutOfRangeException("recipientSession.UseGlobalCatalog");
			}
			ADGroup adgroup = MailboxTaskHelper.ResolveWellknownExchangeSecurityGroup(recipientSession, orgId, wellKnownGroup, writeVerbose, writeWarning);
			if (adgroup != null)
			{
				MailboxTaskHelper.UpdateExchangeSecurityGroupForMailbox(recipientSession, adgroup, mailbox, addMember, writeVerbose, writeWarning);
			}
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0002BBA0 File Offset: 0x00029DA0
		internal static void UpdateExchangeSecurityGroupForMailbox(IRecipientSession recipientSession, ADGroup group, ADUser mailbox, bool addMember, Task.TaskVerboseLoggingDelegate writeVerbose, Task.TaskWarningLoggingDelegate writeWarning)
		{
			if (recipientSession == null)
			{
				throw new ArgumentNullException("recipientSession");
			}
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			if (recipientSession.UseGlobalCatalog)
			{
				throw new ArgumentOutOfRangeException("recipientSession.UseGlobalCatalog");
			}
			LocalizedException ex = null;
			if ((addMember && group.Members.Contains(mailbox.Id)) || (group.Members.IsCompletelyRead && !addMember && !group.Members.Contains(mailbox.Id)))
			{
				return;
			}
			try
			{
				group.Members.Clear();
				group.ResetChangeTracking();
				group.Members.Add(mailbox.Id);
				if (!addMember)
				{
					group.ResetChangeTracking();
					group.Members.Remove(mailbox.Id);
				}
				if (group.m_Session != recipientSession)
				{
					recipientSession = (IRecipientSession)group.m_Session;
				}
				recipientSession.Save(group);
			}
			catch (ADTransientException ex2)
			{
				ex = ex2;
			}
			catch (ADOperationException ex3)
			{
				if (addMember && !(ex3 is ADObjectEntryAlreadyExistsException))
				{
					ex = ex3;
				}
			}
			catch (DataValidationException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				if (addMember)
				{
					writeWarning(Strings.ErrorCanNotAddMailboxToWellKnownHostedMailboxSG(mailbox.Id.ToString(), group.Name, (group.OrganizationId != null) ? group.OrganizationId.OrganizationalUnit.Name : "/", ex.Message));
					ExEventLog.EventTuple tuple_FailedToAddToUSG = ManagementEventLogConstants.Tuple_FailedToAddToUSG;
					ExManagementApplicationLogger.LogEvent(tuple_FailedToAddToUSG, new string[]
					{
						mailbox.Id.ToString(),
						group.Name,
						(group.OrganizationId != null) ? group.OrganizationId.OrganizationalUnit.Name : "/",
						ex.Message
					});
					return;
				}
				writeWarning(Strings.ErrorCanNotRemoveMailboxToWellKnownHostedMailboxSG(mailbox.Id.ToString(), group.Name, (group.OrganizationId != null) ? group.OrganizationId.OrganizationalUnit.Name : "/", ex.Message));
				ExEventLog.EventTuple tuple_FailedToRemoveFromUSG = ManagementEventLogConstants.Tuple_FailedToRemoveFromUSG;
				ExManagementApplicationLogger.LogEvent(tuple_FailedToRemoveFromUSG, new string[]
				{
					mailbox.Id.ToString(),
					group.Name,
					(group.OrganizationId != null) ? group.OrganizationId.OrganizationalUnit.Name : "/",
					ex.Message
				});
			}
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0002BE1C File Offset: 0x0002A01C
		public static ADGroup ResolveWellknownExchangeSecurityGroup(IRecipientSession recipientSession, OrganizationId orgId, Guid wellKnownGroup, Task.TaskVerboseLoggingDelegate writeVerbose, Task.TaskWarningLoggingDelegate writeWarning)
		{
			if (recipientSession == null)
			{
				throw new ArgumentNullException("recipientSession");
			}
			if (recipientSession.UseGlobalCatalog)
			{
				throw new ArgumentOutOfRangeException("recipientSession.UseGlobalCatalog");
			}
			if (wellKnownGroup.Equals(WellKnownGuid.EopsWkGuid))
			{
				string exchangePasswordSettingsSG = InitializeTenantUniversalGroups.ExchangePasswordSettingsSG;
				string domainController = recipientSession.DomainController;
				bool useConfigNC = recipientSession.UseConfigNC;
				bool useGlobalCatalog = recipientSession.UseGlobalCatalog;
				ADGroup adgroup = null;
				try
				{
					recipientSession.UseConfigNC = false;
					recipientSession.UseGlobalCatalog = true;
					bool skipRangedAttributes = recipientSession.SkipRangedAttributes;
					try
					{
						recipientSession.SkipRangedAttributes = true;
						adgroup = recipientSession.ResolveWellKnownGuid<ADGroup>(wellKnownGroup, orgId.ConfigurationUnit);
					}
					finally
					{
						recipientSession.SkipRangedAttributes = skipRangedAttributes;
					}
				}
				finally
				{
					recipientSession.UseConfigNC = useConfigNC;
					recipientSession.UseGlobalCatalog = useGlobalCatalog;
				}
				if (adgroup == null)
				{
					writeWarning(Strings.ErrorWellKnownHostedMailboxSGNotFound(exchangePasswordSettingsSG, (orgId != null) ? orgId.OrganizationalUnit.Name : "/"));
					ExEventLog.EventTuple tuple_FailedToFindUSG = ManagementEventLogConstants.Tuple_FailedToFindUSG;
					ExManagementApplicationLogger.LogEvent(tuple_FailedToFindUSG, new string[]
					{
						exchangePasswordSettingsSG,
						(orgId != null) ? orgId.OrganizationalUnit.Name : "/"
					});
				}
				return adgroup;
			}
			throw new ArgumentOutOfRangeException("wellKnownGroup");
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0002BF50 File Offset: 0x0002A150
		private static ADGroup ResolveForestWideUSGGuid(IRecipientSession recipientSession, Guid wkg)
		{
			ADObjectId configurationNamingContext = ADSession.GetConfigurationNamingContext(recipientSession.SessionSettings.GetAccountOrResourceForestFqdn());
			ADGroup adgroup = null;
			bool useConfigNC = recipientSession.UseConfigNC;
			bool useGlobalCatalog = recipientSession.UseGlobalCatalog;
			recipientSession.UseConfigNC = false;
			recipientSession.UseGlobalCatalog = true;
			ADGroup result;
			try
			{
				try
				{
					adgroup = recipientSession.ResolveWellKnownGuid<ADGroup>(wkg, configurationNamingContext);
				}
				catch (ADReferralException)
				{
				}
				result = adgroup;
			}
			finally
			{
				recipientSession.UseConfigNC = useConfigNC;
				recipientSession.UseGlobalCatalog = useGlobalCatalog;
			}
			return result;
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0002BFCC File Offset: 0x0002A1CC
		internal static string GetNameOfAcceptableLengthForMultiTenantMode(string name, out LocalizedString warning)
		{
			string text = name;
			warning = LocalizedString.Empty;
			if (text.Length > 64 && VariantConfiguration.InvariantNoFlightingSnapshot.CmdletInfra.LimitNameMaxlength.Enabled)
			{
				string text2 = Guid.NewGuid().ToString("N");
				text = text.Substring(0, 64 - text2.Length - 1) + text2;
				warning = Strings.WarningChangingUserSuppliedName(name, text);
			}
			return text;
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0002C044 File Offset: 0x0002A244
		internal static ADObject LookupManager(UserContactIdParameter managerId, DataAccessHelper.CategorizedGetDataObjectDelegate getDataObjectDelegate, ExchangeErrorCategory errorCategory, IRecipientSession tenantGlobalCatalogSession)
		{
			if (managerId != null)
			{
				return (ADRecipient)getDataObjectDelegate(managerId, tenantGlobalCatalogSession, null, null, new LocalizedString?(Strings.ErrorUserOrContactNotFound(managerId.ToString())), new LocalizedString?(Strings.ErrorUserOrContactNotUnique(managerId.ToString())), errorCategory);
			}
			return null;
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0002C088 File Offset: 0x0002A288
		internal static ADObjectId GetArbitrationMailbox(IRecipientSession adRecipientSession, ADObjectId orgAdObjectId)
		{
			return MailboxTaskHelper.GetArbitrationMailbox(adRecipientSession, orgAdObjectId, null);
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0002C094 File Offset: 0x0002A294
		internal static ADObjectId GetArbitrationMailbox(IRecipientSession adRecipientSession, ADObjectId orgAdObjectId, ADObjectId excludedArbitrationMailboxId)
		{
			if (adRecipientSession == null)
			{
				throw new ArgumentNullException("adRecipientSession");
			}
			if (orgAdObjectId == null)
			{
				throw new ArgumentNullException("orgAdObjectId");
			}
			ADObjectId descendantId = orgAdObjectId.GetDescendantId(ApprovalApplication.ParentPathInternal);
			ADObjectId childId = descendantId.GetChildId("ModeratedRecipients");
			QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetailsValue, RecipientTypeDetails.ArbitrationMailbox);
			QueryFilter queryFilter2 = new ComparisonFilter(ComparisonOperator.Equal, ADUserSchema.ApprovalApplications, childId);
			QueryFilter filter;
			if (excludedArbitrationMailboxId == null)
			{
				filter = new AndFilter(new QueryFilter[]
				{
					queryFilter,
					queryFilter2
				});
			}
			else
			{
				filter = new AndFilter(new QueryFilter[]
				{
					queryFilter,
					queryFilter2,
					new ComparisonFilter(ComparisonOperator.NotEqual, ADObjectSchema.Id, excludedArbitrationMailboxId)
				});
			}
			ADPagedReader<ADRecipient> adpagedReader = adRecipientSession.FindPaged(null, QueryScope.SubTree, filter, null, 1);
			using (IEnumerator<ADRecipient> enumerator = adpagedReader.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					ADRecipient adrecipient = enumerator.Current;
					return adrecipient.Id;
				}
			}
			return null;
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0002C19C File Offset: 0x0002A39C
		internal static void ApplyMailboxPlansDelta(ADUser oldPlan, ADUser newPlan, ADUser target, ApplyMailboxPlanFlags flags)
		{
			ADPresentationObject[] array;
			if (oldPlan != null)
			{
				array = new ADPresentationObject[]
				{
					new Mailbox(oldPlan),
					new User(oldPlan),
					new CASMailbox(oldPlan),
					new UMMailbox(oldPlan)
				};
			}
			else
			{
				ADPresentationObject[] array2 = new ADPresentationObject[4];
				array = array2;
			}
			ADPresentationObject[] array3 = new ADPresentationObject[]
			{
				new Mailbox(newPlan),
				new User(newPlan),
				new CASMailbox(newPlan),
				new UMMailbox(newPlan)
			};
			ADPresentationObject[] array4 = new ADPresentationObject[]
			{
				new Mailbox(target),
				new User(target),
				new CASMailbox(target),
				new UMMailbox(target)
			};
			for (int i = 0; i < array4.Length; i++)
			{
				ADPresentationObject.ApplyPresentationObjectDelta(array[i], array3[i], array4[i], flags);
			}
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0002C274 File Offset: 0x0002A474
		internal static void ApplyDefaultArchivePolicy(ADUser user, IConfigurationSession configSession)
		{
			if (user.RetentionPolicy != null)
			{
				return;
			}
			ADObjectId childId;
			if (user.OrganizationId.ConfigurationUnit == null)
			{
				childId = configSession.GetOrgContainerId().GetChildId("Retention Policies Container").GetChildId(RecipientConstants.DefaultArchiveAndRetentionPolicyName);
			}
			else
			{
				childId = user.OrganizationId.ConfigurationUnit.GetChildId("Retention Policies Container").GetChildId(RecipientConstants.DefaultArchiveAndRetentionPolicyName);
			}
			if (configSession.Read<RetentionPolicy>(childId) != null)
			{
				user.RetentionPolicy = childId;
			}
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0002C2E8 File Offset: 0x0002A4E8
		internal static IRecipientSession GetSessionForDeletedObjects(Fqdn domainController, OrganizationId currentOrganizationId, OrganizationId executingUserOrganizationId)
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), currentOrganizationId, executingUserOrganizationId, true);
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(domainController, true, ConsistencyMode.IgnoreInvalid, sessionSettings, 1884, "GetSessionForDeletedObjects", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Common\\recipient\\MailboxTaskHelper.cs");
			tenantOrRootOrgRecipientSession.UseGlobalCatalog = true;
			return tenantOrRootOrgRecipientSession;
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0002C330 File Offset: 0x0002A530
		internal static RemovedMailbox GetRemovedMailbox(Fqdn domainController, OrganizationId organizationId, OrganizationId executingUserOrganizationId, RemovedMailboxIdParameter identity, Task.ErrorLoggerDelegate errorLogger)
		{
			RemovedMailbox result = null;
			IRecipientSession sessionForDeletedObjects = MailboxTaskHelper.GetSessionForDeletedObjects(domainController, organizationId, executingUserOrganizationId);
			IEnumerable<RemovedMailbox> enumerable = identity.GetObjects<RemovedMailbox>(organizationId.OrganizationalUnit, sessionForDeletedObjects) ?? new List<RemovedMailbox>();
			using (IEnumerator<RemovedMailbox> enumerator = enumerable.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					errorLogger(new RecipientTaskException(Strings.ErrorRemovedMailboxNotFound(identity.ToString())), ExchangeErrorCategory.Client, null);
				}
				result = enumerator.Current;
				if (enumerator.MoveNext())
				{
					errorLogger(new RecipientTaskException(Strings.ErrorRemovedMailboxNotUnique(identity.ToString())), ExchangeErrorCategory.Client, null);
				}
			}
			return result;
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0002C3D4 File Offset: 0x0002A5D4
		internal static MailboxStatistics GetDeletedStoreMailbox(IConfigDataProvider dataSession, StoreMailboxIdParameter identity, ObjectId rootId, DatabaseIdParameter databaseId, Task.ErrorLoggerDelegate errorHandler)
		{
			MailboxStatistics[] storeMailboxesFromId = MapiTaskHelper.GetStoreMailboxesFromId(dataSession, identity, rootId);
			if (storeMailboxesFromId == null || storeMailboxesFromId.Length == 0)
			{
				errorHandler(new MdbAdminTaskException(Strings.ErrorStoreMailboxNotFound(identity.ToString(), databaseId.ToString())), ExchangeErrorCategory.Client, identity);
			}
			if (storeMailboxesFromId.Length > 1)
			{
				errorHandler(new MdbAdminTaskException(Strings.ErrorStoreMailboxNotUnique(identity.ToString(), databaseId.ToString())), ExchangeErrorCategory.Client, identity);
			}
			ObjectClass objectClass = storeMailboxesFromId[0].ObjectClass;
			if ((ObjectClass.ExOleDbSystemMailbox & objectClass) != ObjectClass.Unknown)
			{
				errorHandler(new MdbAdminTaskException(Strings.ErrorConnectSystemMailbox(identity.ToString(), databaseId.ToString())), ExchangeErrorCategory.Client, identity);
			}
			if ((ObjectClass.SystemAttendantMailbox & objectClass) != ObjectClass.Unknown)
			{
				errorHandler(new MdbAdminTaskException(Strings.ErrorConnectSystemAttendantMailbox(identity.ToString(), databaseId.ToString())), ExchangeErrorCategory.Client, identity);
			}
			return storeMailboxesFromId[0];
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0002C49C File Offset: 0x0002A69C
		internal static bool HasPublicFolderDatabases(DataAccessHelper.CategorizedGetDataObjectDelegate getDataObjectDelegate, ITopologyConfigurationSession globalConfigSession)
		{
			ServerIdParameter serverIdParameter = ServerIdParameter.Parse(Environment.MachineName);
			Server server = (Server)getDataObjectDelegate(serverIdParameter, globalConfigSession, null, null, new LocalizedString?(Strings.ErrorServerNotFound(serverIdParameter.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(serverIdParameter.ToString())), ExchangeErrorCategory.Client);
			PublicFolderDatabase[] publicFolderDatabases = server.GetPublicFolderDatabases();
			PublicFolderDatabase publicFolderDatabase = null;
			if (publicFolderDatabases.Length == 0)
			{
				ADObjectId adobjectId = PublicFolderDatabase.FindClosestPublicFolderDatabase(globalConfigSession, server.Id);
				if (adobjectId != null)
				{
					publicFolderDatabase = globalConfigSession.Read<PublicFolderDatabase>(adobjectId);
				}
			}
			else
			{
				publicFolderDatabase = publicFolderDatabases[0];
			}
			return publicFolderDatabase != null;
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0002C51F File Offset: 0x0002A71F
		internal static bool IsReservedLiveId(SmtpAddress windowsLiveId)
		{
			return windowsLiveId.Local.Equals("c4e67852e761400490f0750a898dc64e") || windowsLiveId.Local.StartsWith("ExRemoved-");
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0002C548 File Offset: 0x0002A748
		internal static void BlockRemoveOrDisableIfLitigationHoldEnabled(ADUser dataObject, Task.ErrorLoggerDelegate writeError, bool isDisableOperation, bool ignoreLitigationHold)
		{
			if (!ignoreLitigationHold && dataObject.LitigationHoldEnabled)
			{
				string mbxId = dataObject.Identity.ToString();
				LocalizedString message;
				if (isDisableOperation)
				{
					if (dataObject.RecipientType == RecipientType.MailUser)
					{
						message = Strings.ErrorDisableMailuserWithLitigationHold(mbxId);
					}
					else
					{
						message = Strings.ErrorDisableMailboxWithLitigationHold(mbxId);
					}
				}
				else if (dataObject.RecipientType == RecipientType.MailUser)
				{
					message = Strings.ErrorRemoveMailuserWithLitigationHold(mbxId);
				}
				else
				{
					message = Strings.ErrorRemoveMailboxWithLitigationHold(mbxId);
				}
				writeError(new RecipientTaskException(message), ExchangeErrorCategory.Client, dataObject.Identity);
			}
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0002C5BC File Offset: 0x0002A7BC
		internal static void BlockRemoveOrDisableIfDiscoveryHoldEnabled(ADUser dataObject, Task.ErrorLoggerDelegate writeError, bool isDisableOperation, bool ignoreLitigationHold)
		{
			if (!ignoreLitigationHold && dataObject.InPlaceHolds != null && dataObject.InPlaceHolds.Count > 0 && dataObject.RecipientType != RecipientType.MailUser)
			{
				string mbxId = dataObject.Identity.ToString();
				LocalizedString message;
				if (isDisableOperation)
				{
					message = Strings.ErrorDisableMailboxWithDiscoveryHold(mbxId);
				}
				else
				{
					message = Strings.ErrorRemoveMailboxWithDiscoveryHold(mbxId);
				}
				writeError(new RecipientTaskException(message), ExchangeErrorCategory.Client, dataObject.Identity);
			}
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0002C624 File Offset: 0x0002A824
		internal static void BlockRemoveOrDisableIfJournalNDRMailbox(ADUser dataObject, IConfigurationSession configSession, Task.ErrorLoggerDelegate writeError, bool isDisableOperation = false)
		{
			if (dataObject.OrganizationId != OrganizationId.ForestWideOrgId)
			{
				TransportConfigContainer[] array = configSession.Find<TransportConfigContainer>(dataObject.OrganizationId.ConfigurationUnit, QueryScope.SubTree, null, null, 1);
				if (array != null && array.Length == 1)
				{
					dataObject.Identity.ToString();
					SmtpAddress journalingReportNdrTo = array[0].JournalingReportNdrTo;
					if (journalingReportNdrTo != SmtpAddress.NullReversePath && journalingReportNdrTo == dataObject.PrimarySmtpAddress)
					{
						writeError(new RecipientTaskException(isDisableOperation ? Strings.ErrorDisableMailboxIsJournalReportNdrTo(dataObject.Identity.ToString()) : Strings.ErrorRemoveMailboxIsJournalReportNdrTo(dataObject.Identity.ToString())), ExchangeErrorCategory.Client, dataObject.Identity);
					}
				}
			}
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0002C6D0 File Offset: 0x0002A8D0
		internal static void BlockRemoveOrDisableMailUserIfJournalArchiveEnabled(IRecipientSession recipientSession, IConfigurationSession configurationSession, ADUser dataObject, Task.ErrorLoggerDelegate writeError, bool isDisableOperation, bool isSyncOperation)
		{
			if (dataObject.IsSoftDeleted)
			{
				TaskLogger.Trace("Mail user is soft deleted, skip blocking.", new object[0]);
				return;
			}
			if (dataObject.JournalArchiveAddress == SmtpAddress.Empty)
			{
				TaskLogger.Trace("Mail user has no journal archive address, skip blocking.", new object[0]);
				return;
			}
			bool flag = false;
			SmtpProxyAddress proxyAddress = new SmtpProxyAddress(dataObject.JournalArchiveAddress.ToString(), true);
			try
			{
				ADRecipient adrecipient = recipientSession.FindByProxyAddress(proxyAddress);
				if (adrecipient != null && adrecipient.RecipientTypeDetails == RecipientTypeDetails.UserMailbox)
				{
					flag = true;
				}
			}
			catch (NonUniqueRecipientException)
			{
				writeError(new RecipientTaskException(Strings.ErrorMailuserWithMultipleJournalArchive(dataObject.JournalArchiveAddress.ToString())), ExchangeErrorCategory.Client, dataObject.Identity);
				return;
			}
			bool flag2 = false;
			if (isSyncOperation)
			{
				flag2 = (dataObject.IsDirSyncEnabled && MailboxTaskHelper.IsOrgDirSyncEnabled(configurationSession, dataObject.OrganizationId));
			}
			if (flag && !flag2)
			{
				LocalizedString message;
				if (isDisableOperation)
				{
					message = Strings.ErrorDisableMailuserWithJournalArchive;
				}
				else
				{
					message = Strings.ErrorRemoveMailuserWithJournalArchive;
				}
				writeError(new RecipientTaskException(message), ExchangeErrorCategory.Client, dataObject.Identity);
				return;
			}
			TaskLogger.Trace("Block was skipped because journalMailboxExists = {0} and ignoreBlock = {1}.", new object[]
			{
				flag,
				flag2
			});
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0002C80C File Offset: 0x0002AA0C
		internal static void BlockRemoveOrDisableMailboxIfJournalArchiveEnabled(IRecipientSession recipientSession, IConfigurationSession configurationSession, ADUser dataObject, Task.ErrorLoggerDelegate writeError, bool isDisableOperation)
		{
			if (dataObject.IsSoftDeleted)
			{
				TaskLogger.Trace("Mailbox user is soft deleted, skip blocking.", new object[0]);
				return;
			}
			if (dataObject.PrimarySmtpAddress == SmtpAddress.Empty)
			{
				TaskLogger.Trace("Mailbox user has no primary smtp address, skip blocking.", new object[0]);
				return;
			}
			ADRecipient journalArchiveMailUser = MailboxTaskHelper.GetJournalArchiveMailUser(recipientSession, dataObject);
			if (journalArchiveMailUser != null)
			{
				bool flag = journalArchiveMailUser.IsDirSyncEnabled && MailboxTaskHelper.IsOrgDirSyncEnabled(configurationSession, journalArchiveMailUser.OrganizationId);
				LocalizedString message;
				if (isDisableOperation)
				{
					if (flag)
					{
						message = Strings.ErrorDisableMailboxWithJournalArchiveWithDirSync;
					}
					else
					{
						message = Strings.ErrorDisableMailboxWithJournalArchive;
					}
				}
				else if (flag)
				{
					message = Strings.ErrorRemoveMailboxWithJournalArchiveWithDirSync;
				}
				else
				{
					message = Strings.ErrorRemoveMailboxWithJournalArchive;
				}
				writeError(new RecipientTaskException(message), ExchangeErrorCategory.Client, dataObject.Identity);
				return;
			}
			TaskLogger.Trace("No user is using this mailbox for journal archiving, skip blocking.", new object[0]);
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0002C8C8 File Offset: 0x0002AAC8
		internal static ADRecipient GetJournalArchiveMailUser(IRecipientSession recipientSession, ADUser dataObject)
		{
			ADRecipient result = null;
			CustomProxyAddress customProxyAddress = new CustomProxyAddress((CustomProxyAddressPrefix)ProxyAddressPrefix.JRNL, dataObject.PrimarySmtpAddress.ToString(), false);
			ComparisonFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.EmailAddresses, customProxyAddress.ToString());
			ADRecipient[] array = recipientSession.Find<ADRecipient>(null, QueryScope.SubTree, filter, null, 2);
			foreach (ADRecipient adrecipient in array)
			{
				if (!adrecipient.Identity.Equals(dataObject.Identity))
				{
					result = adrecipient;
					break;
				}
			}
			return result;
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0002C954 File Offset: 0x0002AB54
		internal static bool IsOrgDirSyncEnabled(IConfigurationSession configurationSession, OrganizationId organizationId)
		{
			if (organizationId == null)
			{
				TaskLogger.Trace("Unable to determine organization dirSync status because organizationId is null.", new object[0]);
				return false;
			}
			ExchangeConfigurationUnit exchangeConfigUnit = RecipientTaskHelper.GetExchangeConfigUnit(configurationSession, organizationId);
			if (exchangeConfigUnit != null)
			{
				return exchangeConfigUnit.IsDirSyncEnabled;
			}
			TaskLogger.Trace("Unable to determine organization dirSync status because Exchange configuration unit was not found for {0}.", new object[]
			{
				organizationId.ToString()
			});
			return false;
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0002C9AC File Offset: 0x0002ABAC
		internal static void ValidateNotBuiltInArbitrationMailbox(ADUser dataObject, Task.ErrorLoggerDelegate writeError, LocalizedString cantRemoveError)
		{
			string name = dataObject.Name;
			if (name.Equals("FederatedEmail.4c1f4d8b-8179-4148-93bf-00a95fa1e042", StringComparison.OrdinalIgnoreCase) || name.Equals("SystemMailbox{e0dc1c29-89c3-4034-b678-e6c29d823ed9}", StringComparison.OrdinalIgnoreCase))
			{
				writeError(new RecipientTaskException(cantRemoveError), ExchangeErrorCategory.Client, dataObject.Identity);
			}
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0002C9F4 File Offset: 0x0002ABF4
		internal static void ValidateMaximumDiscoveryMailboxQuota(ADUser dataObject, IConfigurationSession configurationSession, OrganizationId currentOrganizationId, Task.ErrorLoggerDelegate errorLogger)
		{
			if (configurationSession == null || currentOrganizationId == null || currentOrganizationId.ConfigurationUnit == null)
			{
				MailboxTaskHelper.ValidateMaximumDiscoveryMailboxQuota(dataObject, errorLogger);
				return;
			}
			ExchangeConfigurationUnit exchangeConfigUnit = RecipientTaskHelper.GetExchangeConfigUnit(configurationSession, currentOrganizationId);
			MailboxTaskHelper.ValidateMaximumDiscoveryMailboxQuota(dataObject, exchangeConfigUnit, errorLogger);
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0002CA2D File Offset: 0x0002AC2D
		private static void ValidateMaximumDiscoveryMailboxQuota(ADUser dataObject, Task.ErrorLoggerDelegate errorLogger)
		{
			MailboxTaskHelper.ValidateMaximumDiscoveryMailboxQuota(dataObject, VariantConfiguration.InvariantNoFlightingSnapshot, errorLogger);
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0002CA3C File Offset: 0x0002AC3C
		private static void ValidateMaximumDiscoveryMailboxQuota(ADUser dataObject, ExchangeConfigurationUnit configurationUnit, Task.ErrorLoggerDelegate errorLogger)
		{
			VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(dataObject.GetContext(configurationUnit), null, null);
			MailboxTaskHelper.ValidateMaximumDiscoveryMailboxQuota(dataObject, snapshot, errorLogger);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0002CA60 File Offset: 0x0002AC60
		private static void ValidateMaximumDiscoveryMailboxQuota(ADUser dataObject, VariantConfigurationSnapshot configurationSnapshot, Task.ErrorLoggerDelegate errorLogger)
		{
			if (dataObject.IsModified(MailboxSchema.ProhibitSendReceiveQuota))
			{
				Unlimited<ByteQuantifiedSize> other = Unlimited<ByteQuantifiedSize>.Parse(configurationSnapshot.Discovery.DiscoveryMailboxMaxProhibitSendReceiveQuota.Value);
				if (((Unlimited<ByteQuantifiedSize>)dataObject[MailboxSchema.ProhibitSendReceiveQuota]).CompareTo(other) > 0)
				{
					errorLogger(new RecipientTaskException(Strings.DiscoveryMailboxQuotaLimitExceeded("ProhibitSendReceiveQuota", other.ToString())), ExchangeErrorCategory.Client, dataObject);
				}
			}
			if (dataObject.IsModified(MailboxSchema.ProhibitSendQuota))
			{
				Unlimited<ByteQuantifiedSize> other2 = Unlimited<ByteQuantifiedSize>.Parse(configurationSnapshot.Discovery.DiscoveryMailboxMaxProhibitSendQuota.Value);
				if (((Unlimited<ByteQuantifiedSize>)dataObject[MailboxSchema.ProhibitSendQuota]).CompareTo(other2) > 0)
				{
					errorLogger(new RecipientTaskException(Strings.DiscoveryMailboxQuotaLimitExceeded("ProhibitSendQuota", other2.ToString())), ExchangeErrorCategory.Client, dataObject);
				}
			}
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0002CB44 File Offset: 0x0002AD44
		internal static void RemoveOrDisablePublicFolderMailbox(ADUser dataObject, Guid mailboxGuid, IConfigurationSession tenantConfigurationSession, Task.ErrorLoggerDelegate writeError, bool isDisableOperation, bool ignoreContentExistenceCheck)
		{
			if (dataObject.ExchangeGuid != Guid.Empty)
			{
				mailboxGuid = dataObject.ExchangeGuid;
			}
			Organization orgContainer = tenantConfigurationSession.GetOrgContainer();
			TenantPublicFolderConfiguration value = TenantPublicFolderConfigurationCache.Instance.GetValue(orgContainer.OrganizationId);
			PublicFolderInformation hierarchyMailboxInformation = value.GetHierarchyMailboxInformation();
			bool flag = hierarchyMailboxInformation.Type == PublicFolderInformation.HierarchyType.InTransitMailboxGuid;
			if (hierarchyMailboxInformation.HierarchyMailboxGuid == mailboxGuid)
			{
				if (value.GetContentMailboxGuids().Length > 0)
				{
					LocalizedString message;
					if (isDisableOperation)
					{
						message = Strings.ErrorCannotDisablePrimaryPublicFolderMailbox(dataObject.Identity.ToString());
					}
					else
					{
						message = Strings.ErrorCannotRemovePrimaryPublicFolderMailbox(dataObject.Identity.ToString());
					}
					writeError(new TaskInvalidOperationException(message), ExchangeErrorCategory.Client, dataObject);
					return;
				}
				if (!flag && !ignoreContentExistenceCheck && MailboxTaskHelper.DoesPublicFolderMailboxContainFoldersWithContents(orgContainer.OrganizationId, Guid.Empty))
				{
					LocalizedString message2;
					if (isDisableOperation)
					{
						message2 = Strings.ErrorCannotDisablePublicFolderMailboxWithFolders;
					}
					else
					{
						message2 = Strings.ErrorCannotRemovePublicFolderMailboxWithFolders;
					}
					writeError(new TaskInvalidOperationException(message2), ExchangeErrorCategory.Client, dataObject);
				}
				if (!hierarchyMailboxInformation.CanUpdate)
				{
					writeError(new RecipientTaskException(Strings.ErrorCannotUpdatePublicFolderHierarchyInformation), ExchangeErrorCategory.Client, dataObject);
				}
				orgContainer.DefaultPublicFolderMailbox = orgContainer.DefaultPublicFolderMailbox.Clone();
				orgContainer.DefaultPublicFolderMailbox.SetHierarchyMailbox(Guid.Empty, PublicFolderInformation.HierarchyType.MailboxGuid);
				tenantConfigurationSession.Save(orgContainer);
				return;
			}
			else
			{
				if (flag)
				{
					return;
				}
				if (!ignoreContentExistenceCheck && MailboxTaskHelper.DoesPublicFolderMailboxContainFoldersWithContents(orgContainer.OrganizationId, mailboxGuid))
				{
					LocalizedString message3;
					if (isDisableOperation)
					{
						message3 = Strings.ErrorCannotDisablePublicFolderMailboxWithFolders;
					}
					else
					{
						message3 = Strings.ErrorCannotRemovePublicFolderMailboxWithFolders;
					}
					writeError(new TaskInvalidOperationException(message3), ExchangeErrorCategory.Client, dataObject);
				}
				return;
			}
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0002CCB4 File Offset: 0x0002AEB4
		internal static void PrepopulateCacheForMailbox(Database database, string owningServerFqdn, OrganizationId organizationId, string mailboxLegacyDN, Guid mailboxGuid, string domainController, Task.TaskWarningLoggingDelegate warningLogger, Task.TaskVerboseLoggingDelegate verboseLogger)
		{
			MailboxTaskHelper.PrepopulateCacheForMailbox(database.Guid, database.Name, owningServerFqdn, organizationId, mailboxLegacyDN, mailboxGuid, domainController, warningLogger, verboseLogger);
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0002CCE0 File Offset: 0x0002AEE0
		internal static void PrepopulateCacheForMailbox(Guid databaseGuid, string databaseName, string owningServerFqdn, OrganizationId organizationId, string mailboxLegacyDN, Guid mailboxGuid, string domainController, Task.TaskWarningLoggingDelegate warningLogger, Task.TaskVerboseLoggingDelegate verboseLogger)
		{
			ExRpcAdmin exRpcAdmin = null;
			try
			{
				exRpcAdmin = ExRpcAdmin.Create("Client=Management", owningServerFqdn, null, null, null);
				verboseLogger(Strings.VerboseRPCConnectionCreated(owningServerFqdn));
				exRpcAdmin.PrePopulateCache(databaseGuid, mailboxLegacyDN, mailboxGuid, TenantPartitionHint.Serialize(TenantPartitionHint.FromOrganizationId(organizationId ?? OrganizationId.ForestWideOrgId)), domainController);
				verboseLogger(Strings.VerboseSucceedToPrepopulateCache);
			}
			catch (MapiPermanentException ex)
			{
				warningLogger(Strings.ErrorFailedToPrepopulateCache(databaseName, owningServerFqdn, mailboxLegacyDN, ex.LowLevelError.ToString("X")));
				verboseLogger(Strings.VerboseFailedToPrepopulateCache(ex.Message));
			}
			catch (MapiRetryableException ex2)
			{
				warningLogger(Strings.ErrorFailedToPrepopulateCache(databaseName, owningServerFqdn, mailboxLegacyDN, ex2.LowLevelError.ToString("X")));
				verboseLogger(Strings.VerboseFailedToPrepopulateCache(ex2.Message));
			}
			finally
			{
				if (exRpcAdmin != null)
				{
					exRpcAdmin.Dispose();
				}
			}
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0002CDE0 File Offset: 0x0002AFE0
		internal static string GetMonitoringTenantName(string nameSuffix = "E15")
		{
			string text = NativeHelpers.GetForestName();
			if (text.Equals("prod.exchangelabs.com", StringComparison.OrdinalIgnoreCase))
			{
				text = "namprd01";
			}
			else
			{
				int num = text.IndexOf('.');
				if (num != -1)
				{
					text = text.Substring(0, num);
				}
			}
			return string.Format("{0}{1}.O365.ExchangeMon.net", text, nameSuffix);
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0002CE2C File Offset: 0x0002B02C
		internal static ADUser CreateMonitoringMailbox(Guid guid, MailboxDatabase database, ADObjectId[] forcedReplicationSites, Task.ErrorLoggerDelegate errorLogger, Task.TaskWarningLoggingDelegate warningLogger, Task.TaskVerboseLoggingDelegate verboseLogger, Action<ADUser, string> liveIdFiller = null, string displayName = null, string monitoringTenantName = null, string password = null)
		{
			string monitoringMailboxName = ADUser.GetMonitoringMailboxName(guid);
			verboseLogger(Strings.VerboseCreatingMonitoringMailbox(monitoringMailboxName));
			ADUser aduser = new ADUser();
			aduser.StampPersistableDefaultValues();
			aduser.Name = monitoringMailboxName;
			aduser.DisplayName = ((displayName == null) ? monitoringMailboxName : displayName);
			aduser.Alias = monitoringMailboxName;
			aduser.SamAccountName = "SM_" + Guid.NewGuid().ToString("N").Substring(0, 17);
			aduser.HiddenFromAddressListsEnabled = true;
			aduser.RecipientTypeDetails = RecipientTypeDetails.MonitoringMailbox;
			aduser.Database = database.Id;
			aduser.ExchangeGuid = Guid.NewGuid();
			aduser.ArchiveDatabase = aduser.Database;
			aduser.ArchiveGuid = Guid.NewGuid();
			aduser.ArchiveName = new MultiValuedProperty<string>(Strings.ArchiveNamePrefix + monitoringMailboxName);
			aduser.ArchiveStatus = ArchiveStatusFlags.Active;
			Server server = database.GetServer();
			aduser.ServerLegacyDN = server.ExchangeLegacyDN;
			if (VariantConfiguration.InvariantNoFlightingSnapshot.ActiveMonitoring.PinMonitoringMailboxesToDatabases.Enabled)
			{
				aduser.MailboxProvisioningConstraint = new MailboxProvisioningConstraint(string.Format("{{DatabaseName -eq '{0}'}}", database.Name));
			}
			aduser.UserAccountControl = (UserAccountControlFlags.PasswordNotRequired | UserAccountControlFlags.NormalAccount | UserAccountControlFlags.DoNotExpirePassword);
			ADSessionSettings sessionSettings;
			IConfigurationSession configurationSession;
			IRecipientSession recipientSession;
			if (VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled)
			{
				if (string.IsNullOrWhiteSpace(monitoringTenantName))
				{
					monitoringTenantName = MailboxTaskHelper.GetMonitoringTenantName("E15");
				}
				try
				{
					sessionSettings = ADSessionSettings.FromTenantCUName(monitoringTenantName);
					configurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 2805, "CreateMonitoringMailbox", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Common\\recipient\\MailboxTaskHelper.cs");
					recipientSession = DirectorySessionFactory.Default.CreateTenantRecipientSession(false, ConsistencyMode.IgnoreInvalid, sessionSettings, 2806, "CreateMonitoringMailbox", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Common\\recipient\\MailboxTaskHelper.cs");
				}
				catch (CannotResolveTenantNameException)
				{
					warningLogger(Strings.WarningNoMonitoringTenant(monitoringTenantName));
					return null;
				}
				ExchangeConfigurationUnit[] array = configurationSession.Find<ExchangeConfigurationUnit>(null, QueryScope.SubTree, null, null, 0);
				if (array == null || array.Length == 0)
				{
					warningLogger(Strings.WarningNoMonitoringTenant(monitoringTenantName));
					return null;
				}
				ExchangeConfigurationUnit exchangeConfigurationUnit = array[0];
				if (exchangeConfigurationUnit.OrganizationStatus != OrganizationStatus.Active)
				{
					warningLogger(Strings.WarningMonitoringTenantNotActive(monitoringTenantName, exchangeConfigurationUnit.OrganizationStatus.ToString()));
					return null;
				}
				goto IL_258;
			}
			sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
			configurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 2836, "CreateMonitoringMailbox", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Common\\recipient\\MailboxTaskHelper.cs");
			recipientSession = DirectorySessionFactory.Default.CreateRootOrgRecipientSession(false, ConsistencyMode.IgnoreInvalid, sessionSettings, 2837, "CreateMonitoringMailbox", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Common\\recipient\\MailboxTaskHelper.cs");
			IL_258:
			AcceptedDomain defaultAcceptedDomain = configurationSession.GetDefaultAcceptedDomain();
			if (defaultAcceptedDomain == null || defaultAcceptedDomain.DomainName == null || defaultAcceptedDomain.DomainName.Domain == null)
			{
				throw new ManagementObjectNotFoundException(Strings.ErrorNoDefaultAcceptedDomainFound(database.Identity.ToString()));
			}
			aduser.EmailAddresses.Add(ProxyAddress.Parse("SMTP:" + aduser.Alias + "@" + defaultAcceptedDomain.DomainName.Domain.ToString()));
			aduser.EmailAddresses.Add(ProxyAddress.Parse("SIP:" + aduser.Alias + "@" + defaultAcceptedDomain.DomainName.Domain.ToString()));
			aduser.WindowsEmailAddress = aduser.PrimarySmtpAddress;
			aduser.UserPrincipalName = aduser.Alias + "@" + defaultAcceptedDomain.DomainName.Domain.ToString();
			aduser.ResetPasswordOnNextLogon = false;
			aduser.SendModerationNotifications = TransportModerationNotificationFlags.Never;
			ADObjectId orgContainerId = configurationSession.GetOrgContainerId();
			ADOrganizationConfig adorganizationConfig = configurationSession.Read<ADOrganizationConfig>(orgContainerId);
			aduser.OrganizationId = adorganizationConfig.OrganizationId;
			ADObjectId adobjectId;
			if (adorganizationConfig.OrganizationId != OrganizationId.ForestWideOrgId)
			{
				adobjectId = adorganizationConfig.OrganizationId.OrganizationalUnit;
			}
			else
			{
				bool useConfigNC = configurationSession.UseConfigNC;
				bool useGlobalCatalog = configurationSession.UseGlobalCatalog;
				ADComputer adcomputer;
				try
				{
					configurationSession.UseConfigNC = false;
					configurationSession.UseGlobalCatalog = true;
					adcomputer = ((ITopologyConfigurationSession)configurationSession).FindComputerByHostName(server.Name);
				}
				finally
				{
					configurationSession.UseConfigNC = useConfigNC;
					configurationSession.UseGlobalCatalog = useGlobalCatalog;
				}
				if (adcomputer == null)
				{
					throw new ManagementObjectNotFoundException(Strings.ErrorDBOwningServerNotFound(database.Identity.ToString()));
				}
				ADObjectId adobjectId2 = adcomputer.Id.DomainId;
				adobjectId2 = adobjectId2.GetChildId("Microsoft Exchange System Objects");
				adobjectId = adobjectId2.GetChildId("Monitoring Mailboxes");
			}
			aduser.SetId(adobjectId.GetChildId(aduser.Name));
			string parentLegacyDN = string.Format(CultureInfo.InvariantCulture, "{0}/ou={1}/cn=Recipients", new object[]
			{
				adorganizationConfig.LegacyExchangeDN,
				adobjectId.Name
			});
			aduser.LegacyExchangeDN = LegacyDN.GenerateLegacyDN(parentLegacyDN, aduser);
			RoleAssignmentPolicy roleAssignmentPolicy = RecipientTaskHelper.FindDefaultRoleAssignmentPolicy(configurationSession, errorLogger, Strings.ErrorDefaultRoleAssignmentPolicyNotUnique, Strings.ErrorDefaultRoleAssignmentPolicyNotFound);
			if (roleAssignmentPolicy != null)
			{
				aduser.RoleAssignmentPolicy = roleAssignmentPolicy.Id;
			}
			recipientSession.LinkResolutionServer = database.OriginatingServer;
			if (liveIdFiller != null)
			{
				liveIdFiller(aduser, password);
			}
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, monitoringMailboxName);
			ADUser[] array2 = recipientSession.FindADUser(adobjectId, QueryScope.SubTree, filter, null, 1);
			if (array2 != null && array2.Length > 0)
			{
				return array2[0];
			}
			recipientSession.Save(aduser);
			MailboxTaskHelper.SetMailboxPassword(recipientSession, aduser, password, errorLogger);
			if (database.Mounted != null && database.Mounted.Value)
			{
				MailboxTaskHelper.PrepopulateCacheForMailbox(database, database.GetServer().Fqdn, aduser.OrganizationId, aduser.LegacyExchangeDN, aduser.ExchangeGuid, aduser.OriginatingServer, warningLogger, verboseLogger);
			}
			if (forcedReplicationSites != null)
			{
				DagTaskHelper.ForceReplication(recipientSession, aduser, forcedReplicationSites, database.Name, warningLogger, verboseLogger);
			}
			array2 = recipientSession.FindADUser(adobjectId, QueryScope.SubTree, filter, null, 1);
			if (array2 != null && array2.Length > 0)
			{
				aduser = array2[0];
				aduser.UserAccountControl &= ~UserAccountControlFlags.PasswordNotRequired;
				recipientSession.Save(aduser);
				return aduser;
			}
			return null;
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0002D3F0 File Offset: 0x0002B5F0
		internal static void RemoveMonitoringMailboxes(MailboxDatabase database, Task.TaskWarningLoggingDelegate warningLogger, Task.TaskVerboseLoggingDelegate verboseLogger)
		{
			verboseLogger(Strings.VerboseDeleteMonitoringMailbox(database.Id.ToString()));
			IRecipientSession recipientSession;
			IRecipientSession recipientSession2;
			if (Datacenter.IsMicrosoftHostedOnly(true))
			{
				string monitoringTenantName = MailboxTaskHelper.GetMonitoringTenantName("E15");
				try
				{
					ADSessionSettings sessionSettings = ADSessionSettings.FromTenantCUName(monitoringTenantName);
					recipientSession = DirectorySessionFactory.Default.CreateTenantRecipientSession(false, ConsistencyMode.IgnoreInvalid, sessionSettings, 3032, "RemoveMonitoringMailboxes", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Common\\recipient\\MailboxTaskHelper.cs");
					recipientSession2 = DirectorySessionFactory.Default.CreateTenantRecipientSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 3033, "RemoveMonitoringMailboxes", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Common\\recipient\\MailboxTaskHelper.cs");
					goto IL_C1;
				}
				catch (CannotResolveTenantNameException)
				{
					warningLogger(Strings.WarningNoMonitoringTenant(monitoringTenantName));
					return;
				}
			}
			ADSessionSettings sessionSettings2 = ADSessionSettings.FromRootOrgScopeSet();
			recipientSession = DirectorySessionFactory.Default.CreateRootOrgRecipientSession(false, ConsistencyMode.IgnoreInvalid, sessionSettings2, 3046, "RemoveMonitoringMailboxes", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Common\\recipient\\MailboxTaskHelper.cs");
			recipientSession2 = DirectorySessionFactory.Default.CreateRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings2, 3047, "RemoveMonitoringMailboxes", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Common\\recipient\\MailboxTaskHelper.cs");
			IL_C1:
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADMailboxRecipientSchema.Database, database.Id),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.MonitoringMailbox)
			});
			ADRecipient[] array = recipientSession2.Find(null, QueryScope.SubTree, filter, null, 0);
			if (array != null && array.Length != 0)
			{
				foreach (ADRecipient instanceToDelete in array)
				{
					recipientSession.Delete(instanceToDelete);
				}
				return;
			}
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0002D550 File Offset: 0x0002B750
		private static bool DoesPublicFolderMailboxContainFoldersWithContents(OrganizationId organizationId, Guid contentMailboxGuid)
		{
			new List<StoreId>();
			bool flag = contentMailboxGuid == Guid.Empty;
			using (PublicFolderSession publicFolderSession = PublicFolderSession.OpenAsAdmin(organizationId, null, Guid.Empty, null, CultureInfo.InvariantCulture, "Client=Management;Action=RemoveOrDisablePublicFolderMailbox", null))
			{
				using (Folder folder = Folder.Bind(publicFolderSession, publicFolderSession.GetIpmSubtreeFolderId()))
				{
					using (QueryResult queryResult = folder.FolderQuery(flag ? FolderQueryFlags.None : FolderQueryFlags.DeepTraversal, null, null, new PropertyDefinition[]
					{
						FolderSchema.Id,
						FolderSchema.ReplicaList,
						FolderSchema.ReplicaListBinary
					}))
					{
						for (;;)
						{
							object[][] rows = queryResult.GetRows(flag ? 1 : 10000);
							if (rows.Length <= 0)
							{
								goto IL_F3;
							}
							if (flag)
							{
								break;
							}
							foreach (object[] array2 in rows)
							{
								string[] array3 = array2[1] as string[];
								if (array3 != null && array3.Length > 0)
								{
									PublicFolderContentMailboxInfo publicFolderContentMailboxInfo = new PublicFolderContentMailboxInfo(array3[0]);
									if (publicFolderContentMailboxInfo.IsValid && publicFolderContentMailboxInfo.MailboxGuid == contentMailboxGuid)
									{
										goto Block_13;
									}
								}
							}
						}
						return true;
						Block_13:
						return true;
						IL_F3:;
					}
				}
			}
			return false;
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0002D6A0 File Offset: 0x0002B8A0
		internal static void ValidatePublicFolderInformationWritable(IConfigurationSession tenantLocalConfigSession, bool holdForMigration, Task.ErrorLoggerDelegate writeError, bool force)
		{
			Organization orgContainer = tenantLocalConfigSession.GetOrgContainer();
			PublicFolderInformation defaultPublicFolderMailbox = orgContainer.DefaultPublicFolderMailbox;
			if (!defaultPublicFolderMailbox.CanUpdate)
			{
				writeError(new RecipientTaskException(Strings.ErrorCannotUpdatePublicFolderHierarchyInformation), ExchangeErrorCategory.Client, null);
			}
			if (holdForMigration && defaultPublicFolderMailbox.Type != PublicFolderInformation.HierarchyType.InTransitMailboxGuid && defaultPublicFolderMailbox.HierarchyMailboxGuid != Guid.Empty)
			{
				writeError(new RecipientTaskException(Strings.ErrorPublicFolderHierarchyAlreadyProvisioned), ExchangeErrorCategory.Client, null);
			}
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0002D710 File Offset: 0x0002B910
		internal static void ValidateArbitrationMailboxHasNoGroups(ADUser dataObject, IRecipientSession tenantGCSession, Task.ErrorLoggerDelegate writeError, LocalizedString validationFailedError)
		{
			QueryFilter queryFilter = (OrganizationId.ForestWideOrgId == dataObject.OrganizationId) ? new NotFilter(new ExistsFilter(ADObjectSchema.ConfigurationUnit)) : new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ConfigurationUnit, dataObject.ConfigurationUnit);
			ADRecipient[] array = tenantGCSession.Find(null, QueryScope.SubTree, new AndFilter(new QueryFilter[]
			{
				queryFilter,
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.ArbitrationMailbox, dataObject.Id),
				new OrFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADGroupSchema.MemberJoinRestriction, MemberUpdateType.ApprovalRequired),
					new ComparisonFilter(ComparisonOperator.Equal, ADGroupSchema.MemberDepartRestriction, MemberUpdateType.ApprovalRequired),
					new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.ModerationEnabled, true)
				})
			}), null, 1);
			if (array != null && array.Length != 0)
			{
				writeError(new RecipientTaskException(validationFailedError), ExchangeErrorCategory.Client, dataObject.Identity);
			}
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0002D7F0 File Offset: 0x0002B9F0
		internal static void ValidateNotLastArbitrationMailbox(ADUser dataObject, IRecipientSession tenantGCSession, ADObjectId rootOrgContainerId, bool allowRemoveOrDisableLast, Task.ErrorLoggerDelegate writeError, LocalizedString validationFailedError)
		{
			bool flag = null == MailboxTaskHelper.GetArbitrationMailbox(tenantGCSession, dataObject.ConfigurationUnit ?? rootOrgContainerId, dataObject.Id);
			if (flag && !allowRemoveOrDisableLast)
			{
				writeError(new RecipientTaskException(validationFailedError), ExchangeErrorCategory.Client, dataObject.Identity);
			}
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0002D838 File Offset: 0x0002BA38
		internal static void ValidateNoOABsAssignedToArbitrationMailbox(ADUser dataObject, bool overrideCheck, Task.ErrorLoggerDelegate writeError, LocalizedString validationFailedError)
		{
			if (overrideCheck)
			{
				return;
			}
			if (OABVariantConfigurationSettings.IsLinkedOABGenMailboxesEnabled && dataObject.RecipientTypeDetails == RecipientTypeDetails.ArbitrationMailbox && dataObject.PersistedCapabilities != null && dataObject.PersistedCapabilities.Contains(Capability.OrganizationCapabilityOABGen) && dataObject.GeneratedOfflineAddressBooks != null && dataObject.GeneratedOfflineAddressBooks.Count > 0)
			{
				writeError(new RecipientTaskException(validationFailedError), ExchangeErrorCategory.Client, dataObject.Identity);
			}
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0002D8A2 File Offset: 0x0002BAA2
		internal static void ValidateMailboxPlanRelease(ADUser mailboxPlan, Task.ErrorLoggerDelegate writeError)
		{
			if (MailboxPlanRelease.NonCurrentRelease == (MailboxPlanRelease)mailboxPlan[ADRecipientSchema.MailboxPlanRelease])
			{
				writeError(new RecipientTaskException(Strings.ErrorMailboxPlanInvalidInThisRelease), ExchangeErrorCategory.Client, mailboxPlan);
			}
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0002D8CE File Offset: 0x0002BACE
		internal static void ValidateRoomMailboxPasswordParameterCanOnlyBeUsedWithEnableRoomMailboxPassword(bool userHasSpecifiedRoomMailboxPasswordInCommandLine, bool userHasSpecifiedEnableRoomMailboxAccountInCommandLine, Task.ErrorLoggerDelegate errorLogger)
		{
			if (userHasSpecifiedRoomMailboxPasswordInCommandLine && !userHasSpecifiedEnableRoomMailboxAccountInCommandLine)
			{
				errorLogger(new TaskArgumentException(Strings.ErrorRoomMailboxPasswordCanOnlyBeUsedWithEnableRoomMailboxAccount), ExchangeErrorCategory.Client, null);
			}
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0002D8EC File Offset: 0x0002BAEC
		internal static void EnsureUserSpecifiedDatabaseMatchesMailboxProvisioningConstraint(Database mailboxDatabase, Database archiveDatabase, PropertyBag fields, MailboxProvisioningConstraint mailboxProvisioningConstraint, Task.ErrorLoggerDelegate errorLogger, object databaseObject)
		{
			if (mailboxProvisioningConstraint != null)
			{
				if (fields.IsModified(databaseObject) && mailboxDatabase.MailboxProvisioningAttributes != null && !mailboxProvisioningConstraint.IsMatch(mailboxDatabase.MailboxProvisioningAttributes))
				{
					errorLogger(new RecipientTaskException(Strings.Error_DatabaseAttributesMismatch(mailboxDatabase.Name, mailboxProvisioningConstraint.Value)), ExchangeErrorCategory.Client, null);
				}
				if (fields.IsModified(ADUserSchema.ArchiveDatabase) && archiveDatabase.MailboxProvisioningAttributes != null && !mailboxProvisioningConstraint.IsMatch(archiveDatabase.MailboxProvisioningAttributes))
				{
					errorLogger(new RecipientTaskException(Strings.Error_DatabaseAttributesMismatch(archiveDatabase.Name, mailboxProvisioningConstraint.Value)), ExchangeErrorCategory.Client, null);
				}
			}
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0002D988 File Offset: 0x0002BB88
		internal static void ValidateMailboxProvisioningConstraintEntries(IEnumerable<MailboxProvisioningConstraint> mailboxProvisioningConstraints, string domainController, LogMessageDelegate verboseLogger, Task.ErrorLoggerDelegate errorLogger)
		{
			List<MailboxDatabase> allCachedDatabasesForProvisioning = PhysicalResourceLoadBalancing.GetAllCachedDatabasesForProvisioning(domainController, verboseLogger);
			foreach (MailboxProvisioningConstraint mailboxProvisioningConstraint in mailboxProvisioningConstraints)
			{
				bool flag = false;
				foreach (MailboxDatabase mailboxDatabase in allCachedDatabasesForProvisioning)
				{
					if (mailboxDatabase.MailboxProvisioningAttributes != null && mailboxProvisioningConstraint.IsMatch(mailboxDatabase.MailboxProvisioningAttributes))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					errorLogger(new RecipientTaskException(Strings.Error_NoDatabaseAttributesMatchingMailboxProvisioningConstraint(mailboxProvisioningConstraint.Value)), ExchangeErrorCategory.Client, null);
				}
			}
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0002DA80 File Offset: 0x0002BC80
		internal static AuthenticationType? GetNamespaceAuthenticationType(OrganizationId organizationId, string domain)
		{
			return ProvisioningCache.Instance.TryAddAndGetOrganizationDictionaryValue<AuthenticationType?, string>(CannedProvisioningCacheKeys.NamespaceAuthenticationTypeCacheKey, organizationId, domain, delegate()
			{
				OrganizationIdCacheValue organizationIdCacheValue = OrganizationIdCache.Singleton.Get(organizationId);
				return organizationIdCacheValue.GetNamespaceAuthenticationType(domain);
			});
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0002DAC8 File Offset: 0x0002BCC8
		internal static ADUser FindMailboxPlanWithSKUCapability(Capability skuCapability, IRecipientSession session, out LocalizedString errorString, bool checkCurrentReleasePlanFirst)
		{
			errorString = LocalizedString.Empty;
			QueryFilter queryFilter = checkCurrentReleasePlanFirst ? MailboxTaskHelper.currentReleaseMailboxPlanFilter : MailboxTaskHelper.mailboxPlanFilter;
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				queryFilter,
				new ComparisonFilter(ComparisonOperator.Equal, ADUserSchema.PersistedCapabilities, skuCapability)
			});
			bool includeSoftDeletedObjects = session.SessionSettings.IncludeSoftDeletedObjects;
			ADUser[] array = null;
			try
			{
				session.SessionSettings.IncludeSoftDeletedObjects = false;
				array = session.FindADUser(null, QueryScope.OneLevel, filter, null, 2);
				if (checkCurrentReleasePlanFirst && array.Length != 1)
				{
					filter = new AndFilter(new QueryFilter[]
					{
						MailboxTaskHelper.mailboxPlanFilter,
						new ComparisonFilter(ComparisonOperator.Equal, ADUserSchema.PersistedCapabilities, skuCapability)
					});
					array = session.FindADUser(null, QueryScope.OneLevel, filter, null, 2);
				}
			}
			finally
			{
				session.SessionSettings.IncludeSoftDeletedObjects = includeSoftDeletedObjects;
			}
			if (array.Length == 1)
			{
				return array[0];
			}
			if (array.Length < 1)
			{
				errorString = Strings.ErrorNoMailboxPlanWithSKUCapability(skuCapability.ToString());
			}
			else
			{
				errorString = Strings.ErrorMoreThanOneMailboxPlanWithSKUCapability(skuCapability.ToString());
			}
			return null;
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0002DBE4 File Offset: 0x0002BDE4
		internal static void VerifyDatabaseIsWithinScopeForRecipientCmdlets(ADSessionSettings sessionSettings, Database database, Task.ErrorLoggerDelegate errorHandler)
		{
			if (!VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled)
			{
				MapiTaskHelper.VerifyDatabaseIsWithinScope(sessionSettings, database, errorHandler);
			}
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0002DC48 File Offset: 0x0002BE48
		internal static void UpdateAuditSettings(ADUser user)
		{
			string[] array = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Exchange_Test", "AuditConfig", null) as string[];
			if (array != null && array.Length > 0 && bool.TrueString.Equals(array[0], StringComparison.OrdinalIgnoreCase))
			{
				user.MailboxAuditEnabled = true;
				bool flag = Array.Exists<string>(array, (string str) => string.Compare(str, "admin", true) == 0);
				bool flag2 = Array.Exists<string>(array, (string str) => string.Compare(str, "delegate", true) == 0);
				bool flag3 = Array.Exists<string>(array, (string str) => string.Compare(str, "owner", true) == 0);
				if (!flag)
				{
					user.AuditAdminOperations = MailboxAuditOperations.None;
					user.AuditDelegateAdminOperations = MailboxAuditOperations.None;
				}
				if (!flag2)
				{
					user.AuditDelegateOperations = MailboxAuditOperations.None;
				}
				if (!flag3)
				{
					user.AuditOwnerOperations = MailboxAuditOperations.None;
				}
			}
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0002DD28 File Offset: 0x0002BF28
		internal static void WriteWarningWhenMailboxIsUnlicensed(ADUser user, Task.TaskWarningLoggingDelegate writeWarning)
		{
			if (user.RecipientTypeDetails == RecipientTypeDetails.UserMailbox && (user.SKUAssigned == null || !user.SKUAssigned.Value) && CapabilityHelper.GetIsLicensingEnforcedInOrg(user.OrganizationId))
			{
				writeWarning(Strings.WarningUnlicensedMailbox);
			}
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0002DD78 File Offset: 0x0002BF78
		internal static bool IsArchiveRecoverable(ADUser user, IConfigurationSession configurationSession, IRecipientSession globalCatalogSession)
		{
			if (configurationSession == null)
			{
				throw new ArgumentNullException("configurationSession");
			}
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			ADObjectId deletedObjectsContainer = configurationSession.DeletedObjectsContainer;
			if (user.DisabledArchiveDatabase == null)
			{
				return false;
			}
			if (user.DisabledArchiveDatabase.Parent != null && !user.DisabledArchiveDatabase.Parent.Equals(deletedObjectsContainer))
			{
				using (MapiAdministrationSession adminSession = MapiTaskHelper.GetAdminSession(RecipientTaskHelper.GetActiveManagerInstance(), user.DisabledArchiveDatabase.ObjectGuid))
				{
					string mailboxLegacyDN = MapiTaskHelper.GetMailboxLegacyDN(adminSession, user.DisabledArchiveDatabase, user.DisabledArchiveGuid);
					if (mailboxLegacyDN != null)
					{
						ADRecipient adrecipient = ConnectMailbox.FindMailboxByLegacyDN(mailboxLegacyDN, globalCatalogSession);
						if (adrecipient == null || adrecipient.LegacyExchangeDN == user.LegacyExchangeDN)
						{
							return true;
						}
						TaskLogger.Trace("The previous archive '{1}' of user '{0}' is in use by the following user in Active Directory: '{2}'. The recovery of archive failed.", new object[]
						{
							user.DisplayName,
							user.DisabledArchiveGuid,
							adrecipient.DisplayName
						});
					}
					else
					{
						TaskLogger.Trace("The previous archive '{1}' of user '{0}' cannot be found in store. The recovery of archive failed. This can occur if the archive was disabled and the mailbox retention period has passed. It can also occur if the archive was enabled but later disabled without the user ever logging on to it.", new object[]
						{
							user.DisplayName,
							user.DisabledArchiveGuid
						});
					}
				}
				return false;
			}
			TaskLogger.Trace("The previous archive database '{1}' of user '{0}' is no longer available. The recovery of archive failed.", new object[]
			{
				user.DisplayName,
				user.DisabledArchiveDatabase
			});
			return false;
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0002DED4 File Offset: 0x0002C0D4
		internal static void BlockLowerMajorVersionArchive(int archiveServerVersion, string primaryDatabaseDN, string archiveDatabaseDN, string archiveDatabaseName, ADObjectId primaryDatabaseId, DataAccessHelper.CategorizedGetDataObjectDelegate getDataObjectDelegate, ITopologyConfigurationSession globalConfigSession, ActiveManager activeManager, Task.ErrorLoggerDelegate errorLogger)
		{
			if (archiveDatabaseDN != primaryDatabaseDN)
			{
				Database database = (MailboxDatabase)getDataObjectDelegate(new DatabaseIdParameter(primaryDatabaseId), globalConfigSession, null, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(primaryDatabaseId.ToString())), new LocalizedString?(Strings.ErrorDatabaseNotUnique(primaryDatabaseId.ToString())), ExchangeErrorCategory.Client);
				DatabaseLocationInfo databaseLocationInfo = MailboxTaskHelper.GetDatabaseLocationInfo(database, activeManager, errorLogger);
				if (databaseLocationInfo == null)
				{
					errorLogger(new RecipientTaskException(Strings.ErrorPrimaryDatabaseLocationNotFound(database.ToString())), ExchangeErrorCategory.Client, null);
					return;
				}
				if (archiveServerVersion < databaseLocationInfo.ServerVersion)
				{
					ServerVersion serverVersion = new ServerVersion(databaseLocationInfo.ServerVersion);
					ServerVersion serverVersion2 = new ServerVersion(archiveServerVersion);
					if (serverVersion2.Major < serverVersion.Major)
					{
						errorLogger(new RecipientTaskException(Strings.ErrorArchiveCanNotBeDownVersion(archiveDatabaseName, database.ToString())), ExchangeErrorCategory.Client, null);
					}
				}
			}
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0002DFA0 File Offset: 0x0002C1A0
		internal static DatabaseLocationInfo GetDatabaseLocationInfo(Database database, ActiveManager activeManager, Task.ErrorLoggerDelegate errorLogger)
		{
			try
			{
				return activeManager.GetServerForDatabase(database.Guid);
			}
			catch (ObjectNotFoundException exception)
			{
				errorLogger(exception, ExchangeErrorCategory.Client, null);
			}
			catch (ServerForDatabaseNotFoundException exception2)
			{
				errorLogger(exception2, ExchangeErrorCategory.Client, null);
			}
			return null;
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0002DFFC File Offset: 0x0002C1FC
		internal static bool SupportsMailboxReleaseVersioning(ADUser adUser)
		{
			return adUser.IsFromDatacenter;
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0002E004 File Offset: 0x0002C204
		internal static MailboxRelease ComputeRequiredMailboxRelease(IConfigurationSession configSession, ADUser adUser, ExchangeConfigurationUnit configurationUnit, Task.ErrorLoggerDelegate errorLogger)
		{
			if (configurationUnit == null)
			{
				configurationUnit = configSession.Read<ExchangeConfigurationUnit>(adUser.OrganizationId.ConfigurationUnit);
				if (configurationUnit == null)
				{
					errorLogger(new ManagementObjectNotFoundException(Strings.ErrorOrganizationNotFound(adUser.OrganizationId.ToString())), ExchangeErrorCategory.Client, null);
				}
			}
			if (adUser.UpgradeRequest != UpgradeRequestTypes.PilotUpgrade)
			{
				return configurationUnit.MailboxRelease;
			}
			return configurationUnit.PilotMailboxRelease;
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0002E061 File Offset: 0x0002C261
		internal static void ValidateMailboxRelease(MailboxRelease targetServerMailboxRelease, MailboxRelease requiredMailboxRelease, string userIdentity, string databaseIdentity, Task.ErrorLoggerDelegate errorLogger)
		{
			if (requiredMailboxRelease != targetServerMailboxRelease)
			{
				errorLogger(new MismatchedMailboxReleaseException(userIdentity, databaseIdentity, targetServerMailboxRelease.ToString(), requiredMailboxRelease.ToString()), ExchangeErrorCategory.Client, null);
			}
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0002E094 File Offset: 0x0002C294
		internal static void ApplyMbxPlanSettingsInTargetForest(ADUser user, Func<ADObjectId, ADUser> getMbxPlanObject, ApplyMailboxPlanFlags flags)
		{
			ADObjectId adobjectId = null;
			if (user.IntendedMailboxPlan != null)
			{
				adobjectId = user.IntendedMailboxPlan;
			}
			else if (user.MailboxPlan != null)
			{
				adobjectId = user.MailboxPlan;
			}
			if (adobjectId != null)
			{
				ADUser newPlan = getMbxPlanObject(adobjectId);
				MailboxTaskHelper.ApplyMailboxPlansDelta(null, newPlan, user, flags);
				user.MailboxPlan = adobjectId;
				user.IntendedMailboxPlan = null;
			}
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0002E0E8 File Offset: 0x0002C2E8
		internal static void ApplyMbxPlanDeltaOnMbxMove(ADUser user, Func<ADObjectId, ADUser> getMbxPlanObject, IConfigurationSession configurationSession, IRecipientSession recipientSession, Task.ErrorLoggerDelegate writeError)
		{
			if (user.MailboxPlan != null && user.SKUCapability != null)
			{
				bool checkCurrentReleasePlanFirst = RecipientTaskHelper.IsOrganizationInPilot(configurationSession, user.OrganizationId);
				LocalizedString message;
				ADUser aduser = MailboxTaskHelper.FindMailboxPlanWithSKUCapability(user.SKUCapability.Value, recipientSession, out message, checkCurrentReleasePlanFirst);
				if (aduser == null)
				{
					writeError(new RecipientTaskException(message), ExchangeErrorCategory.ServerOperation, user.Id);
				}
				MailboxTaskHelper.UpdateMailboxPlan(user, aduser, getMbxPlanObject);
			}
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0002E158 File Offset: 0x0002C358
		internal static void UpdateMailboxPlan(ADUser userObject, ADUser newMbxPlan, Func<ADObjectId, ADUser> getMbxPlanObject)
		{
			ADObjectId mailboxPlan = userObject.MailboxPlan;
			if (!newMbxPlan.Id.Equals(mailboxPlan))
			{
				ADUser oldPlan = null;
				if (mailboxPlan != null)
				{
					oldPlan = getMbxPlanObject(mailboxPlan);
				}
				userObject.MailboxPlan = newMbxPlan.Id;
				MailboxTaskHelper.ApplyMailboxPlansDelta(oldPlan, newMbxPlan, userObject, ApplyMailboxPlanFlags.None);
				RecipientTaskHelper.UpgradeArchiveQuotaOnArchiveAddOnSKU(userObject, userObject.PersistedCapabilities);
			}
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0002E1A8 File Offset: 0x0002C3A8
		internal static MailboxIdParameter ResolveMailboxIdentity(ADObjectId executingUserId, Task.ErrorLoggerDelegate errorLogger)
		{
			if (executingUserId != null)
			{
				return new MailboxIdParameter(executingUserId);
			}
			errorLogger(new RecipientTaskException(Strings.ErrorParameterRequired("Mailbox")), ExchangeErrorCategory.Client, null);
			return null;
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0002E1FF File Offset: 0x0002C3FF
		internal static void RemovePersistentProperties(List<PropertyDefinition> propertiesToClear)
		{
			propertiesToClear.Remove(ADRecipientSchema.EmailAddresses);
			propertiesToClear.Remove(ADRecipientSchema.WindowsEmailAddress);
			propertiesToClear.RemoveAll(delegate(PropertyDefinition definition)
			{
				string ldapDisplayName = ((ADPropertyDefinition)definition).LdapDisplayName;
				return !string.IsNullOrEmpty(ldapDisplayName) && ldapDisplayName.StartsWith("extensionAttribute", StringComparison.InvariantCultureIgnoreCase);
			});
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0002E240 File Offset: 0x0002C440
		internal static void ClearExchangeProperties(ADRecipient recipient, IEnumerable<PropertyDefinition> propertiesToReset)
		{
			if (recipient.MaximumSupportedExchangeObjectVersion.IsOlderThan(recipient.ExchangeVersion))
			{
				throw new DataValidationException(new PropertyValidationError(Strings.ErrorCannotSaveBecauseTooNew(recipient.ExchangeVersion.ToString(), recipient.MaximumSupportedExchangeObjectVersion.ToString()), ADObjectSchema.ExchangeVersion, recipient.ExchangeVersion));
			}
			foreach (PropertyDefinition propertyDefinition in propertiesToReset)
			{
				ADPropertyDefinition adpropertyDefinition = (ADPropertyDefinition)propertyDefinition;
				if (!recipient.ExchangeVersion.IsOlderThan(adpropertyDefinition.VersionAdded))
				{
					recipient[adpropertyDefinition] = null;
				}
			}
			if (recipient.RecipientTypeDetails == RecipientTypeDetails.PublicFolderMailbox && !recipient.ExchangeVersion.IsOlderThan(ADRecipientSchema.RecipientTypeDetails.VersionAdded))
			{
				recipient[ADRecipientSchema.RecipientTypeDetails] = null;
			}
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0002E31C File Offset: 0x0002C51C
		public static void ValidateExternalEmailAddress(ADRecipient recipient, IConfigurationSession configurationSession, Task.ErrorLoggerDelegate writeError, ProvisioningCache provisioningCache)
		{
			if (VariantConfiguration.InvariantNoFlightingSnapshot.CmdletInfra.ValidateExternalEmailAddressInAcceptedDomain.Enabled)
			{
				SmtpProxyAddress smtpProxyAddress = recipient.ExternalEmailAddress as SmtpProxyAddress;
				if (smtpProxyAddress == null)
				{
					writeError(new RecipientTaskException(Strings.ErrorExternalEmailAddressNotSmtpAddress((recipient.ExternalEmailAddress == null) ? "$null" : recipient.ExternalEmailAddress.ToString())), ExchangeErrorCategory.Client, recipient.Identity);
					return;
				}
				if (RecipientTaskHelper.SMTPAddressCheckWithAcceptedDomain(configurationSession, recipient.OrganizationId, writeError, provisioningCache))
				{
					RecipientTaskHelper.ValidateInAcceptedDomain(configurationSession, recipient.OrganizationId, new SmtpAddress(smtpProxyAddress.SmtpAddress).Domain, writeError, provisioningCache);
				}
				recipient.EmailAddressPolicyEnabled = false;
				if (recipient.PrimarySmtpAddress == SmtpAddress.Empty)
				{
					recipient.PrimarySmtpAddress = new SmtpAddress(smtpProxyAddress.SmtpAddress);
				}
			}
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0002E3F4 File Offset: 0x0002C5F4
		internal static void CheckAndResolveManagedBy<TObject>(NewGeneralRecipientObjectTask<TObject> task, DataAccessHelper.CategorizedGetDataObjectDelegate getDataObject, ExchangeErrorCategory errorCategory, RecipientIdParameter[] managedByParameter, out MultiValuedProperty<ADRecipient> managedByRecipients) where TObject : ADRecipient, new()
		{
			managedByRecipients = null;
			if (task.Fields.IsModified(ADGroupSchema.ManagedBy))
			{
				if (managedByParameter == null || managedByParameter.Length == 0)
				{
					task.WriteError(new RecipientTaskException(Strings.AutoGroupManagedByCannotBeEmpty), ErrorCategory.InvalidArgument, null);
				}
				managedByRecipients = new MultiValuedProperty<ADRecipient>();
				foreach (RecipientIdParameter recipientIdParameter in managedByParameter)
				{
					ADRecipient item = (ADRecipient)getDataObject(recipientIdParameter, task.TenantGlobalCatalogSession, null, null, new LocalizedString?(Strings.ErrorRecipientNotFound(recipientIdParameter.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(recipientIdParameter.ToString())), errorCategory);
					managedByRecipients.Add(item);
				}
			}
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0002E48C File Offset: 0x0002C68C
		internal static void StampOnManagedBy(ADGroup group, MultiValuedProperty<ADRecipient> managedByRecipients, Task.ErrorLoggerDelegate writeError)
		{
			if (managedByRecipients == null || managedByRecipients.Count == 0)
			{
				group.ManagedBy = null;
				return;
			}
			MultiValuedProperty<ADObjectId> multiValuedProperty = new MultiValuedProperty<ADObjectId>();
			foreach (ADRecipient adrecipient in managedByRecipients)
			{
				if (multiValuedProperty.Contains(adrecipient.Id))
				{
					writeError(new TaskInvalidOperationException(Strings.ErrorManagedByAlreadyExisted(group.Identity.ToString(), adrecipient.Id.ToString())), ExchangeErrorCategory.Client, group.Identity);
				}
				else
				{
					multiValuedProperty.Add(adrecipient.Id);
				}
			}
			group.ManagedBy = multiValuedProperty;
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0002E540 File Offset: 0x0002C740
		internal static void ValidateAndAddMember(IConfigDataProvider session, ADGroup group, RecipientIdParameter member, bool isSelfValidation, Task.ErrorLoggerDelegate writeError, DataAccessHelper.CategorizedGetDataObjectDelegate getDataObject)
		{
			ADRecipient adrecipient = (ADRecipient)getDataObject(member, session, group.OrganizationId.OrganizationalUnit, null, new LocalizedString?(Strings.ErrorRecipientNotFound((string)member)), new LocalizedString?(Strings.ErrorRecipientNotUnique((string)member)), ExchangeErrorCategory.Client);
			if (MailboxTaskHelper.GroupContainsMember(group, adrecipient.Id, session))
			{
				MailboxTaskHelper.WriteMemberAlreadyExistsError(group, member, isSelfValidation, writeError);
			}
			MailboxTaskHelper.ValidateAndAddMember(group, member, adrecipient, writeError);
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0002E5B0 File Offset: 0x0002C7B0
		internal static void ValidateAndAddMember(ADGroup group, RecipientIdParameter memberId, ADRecipient memberRecipient, Task.ErrorLoggerDelegate writeError)
		{
			MailboxTaskHelper.ValidateGroupMember(group, memberRecipient, memberId, writeError);
			group.Members.Add(memberRecipient.Id);
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0002E5CC File Offset: 0x0002C7CC
		internal static void ValidateGroupMember(ADGroup group, ADRecipient memberRecipient, Task.ErrorLoggerDelegate writeError)
		{
			MailboxTaskHelper.ValidateGroupMember(group, memberRecipient, null, writeError);
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0002E5D8 File Offset: 0x0002C7D8
		internal static void ValidateGroupMember(ADGroup group, ADRecipient memberRecipient, RecipientIdParameter memberId, Task.ErrorLoggerDelegate writeError)
		{
			if (group.RecipientTypeDetails == RecipientTypeDetails.RoomList && memberRecipient.RecipientTypeDetails != RecipientTypeDetails.RoomMailbox && memberRecipient.RecipientTypeDetails != RecipientTypeDetails.RoomList && memberRecipient.RecipientDisplayType != RecipientDisplayType.SyncedConferenceRoomMailbox)
			{
				writeError(new NonRoomMailboxAddToRoomListException(group.Id.ToString()), ExchangeErrorCategory.Client, memberId);
			}
			MailboxTaskHelper.ValidateMemberInGroup(memberRecipient, group, writeError);
			MailboxTaskHelper.CheckGroupVersion(group);
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0002E65C File Offset: 0x0002C85C
		internal static void ValidateAddedMembers(IConfigDataProvider session, ADGroup dataObject, Task.ErrorLoggerDelegate writeError, DataAccessHelper.CategorizedGetDataObjectDelegate getDataObject)
		{
			if ((dataObject.GroupType & GroupTypeFlags.Universal) != GroupTypeFlags.Universal)
			{
				writeError(new RecipientTaskException(Strings.ErrorOnlyAllowChangeMembersOnUniversalGroup(dataObject.Name)), ExchangeErrorCategory.Client, dataObject.Identity);
			}
			if (dataObject.Members != null && dataObject.Members.Added.Length > 0)
			{
				foreach (ADObjectId adobjectId in dataObject.Members.Added)
				{
					ADRecipient adrecipient = (ADRecipient)getDataObject(new GeneralRecipientIdParameter(adobjectId), session, null, null, new LocalizedString?(Strings.ErrorRecipientNotFound(adobjectId.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(adobjectId.ToString())), ExchangeErrorCategory.Client);
					MailboxTaskHelper.ValidateMemberInGroup(adrecipient, dataObject, writeError);
					if (adrecipient.Id.Parent.DistinguishedName.StartsWith("CN=ForeignSecurityPrincipals,DC=", StringComparison.InvariantCultureIgnoreCase))
					{
						writeError(new RecipientTaskException(Strings.ErrorUniversalGroupCannotHaveForeignSP(dataObject.Name, adobjectId.ToString())), ExchangeErrorCategory.Client, dataObject.Identity);
					}
				}
			}
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0002E764 File Offset: 0x0002C964
		internal static bool ValidateMemberInGroup(ADRecipient memberRecipient, ADGroup containerGroup, Task.ErrorLoggerDelegate writeError)
		{
			if (containerGroup.Guid == memberRecipient.Guid)
			{
				writeError(new RecipientTaskException(Strings.ErrorGroupMembersCannotContainItself(memberRecipient.Id.Name)), ExchangeErrorCategory.Client, memberRecipient.Identity);
				return false;
			}
			OrganizationId organizationId = containerGroup.OrganizationId;
			OrganizationId organizationId2 = memberRecipient.OrganizationId;
			if (!organizationId.Equals(organizationId2))
			{
				writeError(new RecipientTaskException(Strings.ErrorAddGroupMemberCrossTenant), ExchangeErrorCategory.Client, memberRecipient.Identity);
				return false;
			}
			ADGroup adgroup = memberRecipient as ADGroup;
			if (adgroup != null && (containerGroup.GroupType & GroupTypeFlags.Universal) == GroupTypeFlags.Universal && (adgroup.GroupType & GroupTypeFlags.DomainLocal) == GroupTypeFlags.DomainLocal)
			{
				writeError(new RecipientTaskException(Strings.ErrorUniversalGroupCannotHaveLocalGroup(containerGroup.Id.Name, adgroup.Id.Name)), ExchangeErrorCategory.Client, containerGroup.Identity);
				return false;
			}
			return true;
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0002E834 File Offset: 0x0002CA34
		internal static void CheckGroupVersion(ADGroup group)
		{
			if (group.MaximumSupportedExchangeObjectVersion.IsOlderThan(group.ExchangeVersion))
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.ErrorCannotSaveBecauseTooNew(group.ExchangeVersion, group.MaximumSupportedExchangeObjectVersion), ADObjectSchema.ExchangeVersion, group.ExchangeVersion));
			}
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0002E870 File Offset: 0x0002CA70
		internal static void WriteMemberAlreadyExistsError(ADGroup group, RecipientIdParameter member, bool isSelfValidation, Task.ErrorLoggerDelegate writeError)
		{
			if (isSelfValidation)
			{
				writeError(new SelfMemberAlreadyExistsException(group.Id.ToString()), ExchangeErrorCategory.Client, member);
				return;
			}
			writeError(new MemberAlreadyExistsException(member.ToString(), group.Id.ToString()), ExchangeErrorCategory.Client, member);
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0002E8BF File Offset: 0x0002CABF
		internal static void ValidateGroupManagedBy(IRecipientSession recipientSession, ADGroup group, MultiValuedProperty<ADRecipient> recipients, DataAccessHelper.CategorizedGetDataObjectDelegate getDataObject, Task.ErrorLoggerDelegate writeError)
		{
			MailboxTaskHelper.ValidateGroupManagedBy(recipientSession, group, recipients, RecipientConstants.DistributionGroup_OwnerRecipientTypeDetails, false, getDataObject, writeError);
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0002E8D4 File Offset: 0x0002CAD4
		internal static void ValidateGroupManagedBy(IRecipientSession recipientSession, ADGroup group, MultiValuedProperty<ADRecipient> recipients, RecipientTypeDetails[] allowedRecipientTypeDetails, bool useSecurityPrincipalIdParameter, DataAccessHelper.CategorizedGetDataObjectDelegate getDataObject, Task.ErrorLoggerDelegate writeError)
		{
			recipients = MailboxTaskHelper.GetGroupManagedbyRecipients(recipientSession, group, useSecurityPrincipalIdParameter, recipients, getDataObject);
			foreach (ADRecipient adrecipient in recipients)
			{
				if (!group.OrganizationId.Equals(adrecipient.OrganizationId))
				{
					writeError(new RecipientTaskException(Strings.ErrorManagedByCrossTenant(adrecipient.Id.ToString())), ExchangeErrorCategory.Client, group.Identity);
				}
				MailboxTaskHelper.ValidateGroupManagedBy(group, adrecipient, allowedRecipientTypeDetails, writeError);
			}
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x0002E970 File Offset: 0x0002CB70
		internal static void ValidateGroupManagedBy(ADGroup group, ADRecipient recipient, Task.ErrorLoggerDelegate writeError)
		{
			MailboxTaskHelper.ValidateGroupManagedBy(group, recipient, RecipientConstants.DistributionGroup_OwnerRecipientTypeDetails, writeError);
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0002E9A4 File Offset: 0x0002CBA4
		private static void ValidateGroupManagedBy(ADGroup group, ADRecipient recipient, RecipientTypeDetails[] allowedRecipientTypeDetails, Task.ErrorLoggerDelegate writeError)
		{
			if (!group.ExchangeVersion.IsOlderThan(ExchangeObjectVersion.Exchange2010))
			{
				if (!Array.Exists<RecipientTypeDetails>(allowedRecipientTypeDetails, (RecipientTypeDetails item) => item == recipient.RecipientTypeDetails))
				{
					writeError(new RecipientTaskException(Strings.ErrorManagedByWrongRecipientTypeDetails(group.Id.ToString(), recipient.Id.ToString(), string.Join(",", (from detail in allowedRecipientTypeDetails
					select detail.ToString()).ToArray<string>()))), ExchangeErrorCategory.Client, group.Identity);
				}
			}
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x0002EA58 File Offset: 0x0002CC58
		internal static void ValidateGroupManagedByRecipientRestriction(IRecipientSession recipientSession, ADGroup group, MultiValuedProperty<ADRecipient> recipients, DataAccessHelper.CategorizedGetDataObjectDelegate getDataObject, Task.ErrorLoggerDelegate writeError, Task.TaskWarningLoggingDelegate writeWarning)
		{
			recipients = MailboxTaskHelper.GetGroupManagedbyRecipients(recipientSession, group, false, recipients, getDataObject);
			bool flag = false;
			bool flag2 = true;
			foreach (ADRecipient adrecipient in recipients)
			{
				if (adrecipient.RecipientTypeDetails != RecipientTypeDetails.MailUniversalSecurityGroup && adrecipient.RecipientTypeDetails != RecipientTypeDetails.UniversalSecurityGroup)
				{
					flag2 = false;
				}
				else if (adrecipient.RecipientTypeDetails == RecipientTypeDetails.MailUniversalSecurityGroup || adrecipient.RecipientTypeDetails == RecipientTypeDetails.UniversalSecurityGroup)
				{
					flag = true;
				}
			}
			if (recipients.Count > 0 && writeError != null && writeWarning != null)
			{
				if (group.MemberJoinRestriction == MemberUpdateType.ApprovalRequired || group.MemberDepartRestriction == MemberUpdateType.ApprovalRequired)
				{
					if (flag2)
					{
						writeError(new RecipientTaskException(Strings.ErrorRestrictionWithWrongGroupType), ExchangeErrorCategory.Client, group.Identity);
					}
					else if (flag)
					{
						writeWarning(Strings.WarningRestrictionWithWrongGroupType);
					}
				}
				if (group.ModerationEnabled && group.ModeratedBy.Count == 0)
				{
					if (flag2)
					{
						writeError(new RecipientTaskException(Strings.ErrorModerationWithWrongGroupType), ExchangeErrorCategory.Client, group.Identity);
						return;
					}
					if (flag)
					{
						writeWarning(Strings.WarningModerationWithWrongGroupType);
					}
				}
			}
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0002EB90 File Offset: 0x0002CD90
		private static MultiValuedProperty<ADRecipient> GetGroupManagedbyRecipients(IRecipientSession recipientSession, ADGroup group, bool useSecurityPrincipalIdParameter, MultiValuedProperty<ADRecipient> recipients, DataAccessHelper.CategorizedGetDataObjectDelegate getDataObject)
		{
			if (recipients == null)
			{
				recipients = new MultiValuedProperty<ADRecipient>();
				foreach (ADObjectId adobjectId in group.ManagedBy)
				{
					ADRecipient item = null;
					try
					{
						item = (ADRecipient)getDataObject(useSecurityPrincipalIdParameter ? new SecurityPrincipalIdParameter(adobjectId) : new RecipientIdParameter(adobjectId), recipientSession, null, null, new LocalizedString?(Strings.ErrorRecipientNotFound(adobjectId.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(adobjectId.ToString())), ExchangeErrorCategory.Client);
					}
					catch (ManagementObjectNotFoundException)
					{
						RoleGroupIdParameter roleGroupIdParameter = new RoleGroupIdParameter(adobjectId);
						if (roleGroupIdParameter.InternalADObjectId == null || !(roleGroupIdParameter.InternalADObjectId.Name == "Organization Management"))
						{
							throw;
						}
						item = (ADRecipient)getDataObject(useSecurityPrincipalIdParameter ? new SecurityPrincipalIdParameter(adobjectId) : roleGroupIdParameter, recipientSession, null, null, new LocalizedString?(Strings.ErrorRecipientNotFound(adobjectId.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(adobjectId.ToString())), ExchangeErrorCategory.Client);
					}
					recipients.Add(item);
				}
			}
			return recipients;
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0002ECBC File Offset: 0x0002CEBC
		internal static void ValidateAndRemoveMember(IConfigDataProvider session, ADGroup group, RecipientIdParameter member, string groupRawIdentity, bool isSelfValidation, Task.TaskErrorLoggingDelegate writeError, DataAccessHelper.GetDataObjectDelegate getDataObject)
		{
			ADRecipient adrecipient = (ADRecipient)getDataObject(member, session, group.OrganizationId.OrganizationalUnit, null, new LocalizedString?(Strings.ErrorRecipientNotFound((string)member)), new LocalizedString?(Strings.ErrorRecipientNotUnique((string)member)));
			if (!MailboxTaskHelper.GroupContainsMember(group, adrecipient.Id, session))
			{
				MailboxTaskHelper.WriteMemberNotFoundError(group, member, groupRawIdentity, isSelfValidation, writeError);
			}
			MailboxTaskHelper.ValidateAndRemoveMember(group, adrecipient);
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0002ED26 File Offset: 0x0002CF26
		internal static void ValidateAndRemoveMember(ADGroup group, ADRecipient memberRecipient)
		{
			MailboxTaskHelper.CheckGroupVersion(group);
			MailboxTaskHelper.RemoveItem(group.Members, memberRecipient.Id);
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0002ED40 File Offset: 0x0002CF40
		internal static bool GroupContainsMember(ADGroup group, ADObjectId memberid, IConfigDataProvider session)
		{
			bool result = false;
			if (group.Members.IsCompletelyRead)
			{
				result = group.Members.Contains(memberid);
			}
			else
			{
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADGroupSchema.Members, memberid);
				IDirectorySession directorySession = (IDirectorySession)session;
				bool skipRangedAttributes = directorySession.SkipRangedAttributes;
				ADRawEntry[] array;
				try
				{
					directorySession.SkipRangedAttributes = true;
					array = directorySession.Find(group.Id, QueryScope.Base, filter, null, 1, new ADPropertyDefinition[]
					{
						ADObjectSchema.Id
					});
				}
				finally
				{
					directorySession.SkipRangedAttributes = skipRangedAttributes;
				}
				if (array != null && array.Length > 0)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0002EDDC File Offset: 0x0002CFDC
		internal static void RemoveItem(MultiValuedProperty<ADObjectId> collection, ADObjectId itemToRemove)
		{
			if (!collection.Contains(itemToRemove))
			{
				object[] added = collection.Added;
				object[] removed = collection.Removed;
				foreach (object item in added)
				{
					collection.Remove(item);
				}
				foreach (object item2 in removed)
				{
					collection.Add(item2);
				}
				collection.Add(itemToRemove);
				collection.ResetChangeTracking();
				foreach (object item3 in added)
				{
					collection.Add(item3);
				}
				foreach (object item4 in removed)
				{
					collection.Remove(item4);
				}
			}
			collection.Remove(itemToRemove);
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0002EEAC File Offset: 0x0002D0AC
		internal static void WriteMemberNotFoundError(ADGroup group, RecipientIdParameter member, string groupRawIdentity, bool isSelfValidation, Task.TaskErrorLoggingDelegate writeError)
		{
			if (isSelfValidation)
			{
				writeError(new SelfMemberNotFoundException(string.IsNullOrEmpty(groupRawIdentity) ? group.Id.ToString() : groupRawIdentity), ErrorCategory.InvalidData, member);
				return;
			}
			writeError(new MemberNotFoundException(member.ToString(), string.IsNullOrEmpty(groupRawIdentity) ? group.Id.ToString() : groupRawIdentity), ErrorCategory.InvalidData, member);
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0002EF0C File Offset: 0x0002D10C
		internal static MultiValuedProperty<string> ValidateAndSanitizeTranslations(IList<string> mailTipsTranslations, HashSet<string> mailTipTranslationCultures, bool isDefaultMailTipChanged, bool isDeletingDefaultMailTip, Task.ErrorLoggerDelegate writeError)
		{
			List<string> list = new List<string>(mailTipsTranslations);
			for (int i = 0; i < mailTipsTranslations.Count; i++)
			{
				string text;
				string unsafeHtml;
				if (!ADRecipient.TryGetMailTipParts(mailTipsTranslations[i], out text, out unsafeHtml))
				{
					writeError(new RecipientTaskException(DirectoryStrings.ErrorMailTipTranslationFormatIncorrect), ExchangeErrorCategory.Client, null);
				}
				bool flag = "default".Equals(text, StringComparison.OrdinalIgnoreCase);
				if (flag && isDefaultMailTipChanged)
				{
					writeError(new RecipientTaskException(Strings.ErrorMoreThanOneDefaultMailTipTranslationSpecified), ExchangeErrorCategory.Client, null);
				}
				else if (mailTipTranslationCultures.Contains(text))
				{
					if (flag)
					{
						writeError(new RecipientTaskException(Strings.ErrorMoreThanOneDefaultMailTipTranslationSpecified), ExchangeErrorCategory.Client, null);
					}
					else
					{
						writeError(new RecipientTaskException(Strings.ErrorMoreThanOneMailTipTranslationForThisCulture(text)), ExchangeErrorCategory.Client, null);
					}
				}
				if (isDeletingDefaultMailTip && !flag)
				{
					writeError(new RecipientTaskException(Strings.ErrorMailTipSetTranslationsWithoutDefault), ExchangeErrorCategory.Client, null);
				}
				mailTipTranslationCultures.Add(text);
				string str = TextConverterHelper.SanitizeHtml(unsafeHtml);
				list[i] = text + ":" + str;
			}
			if (!isDefaultMailTipChanged && !mailTipTranslationCultures.Contains("default"))
			{
				writeError(new RecipientTaskException(Strings.ErrorMailTipSetTranslationsWithoutDefault), ExchangeErrorCategory.Client, null);
			}
			return new MultiValuedProperty<string>(list);
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0002F03C File Offset: 0x0002D23C
		internal static MultiValuedProperty<string> ValidateAndSanitizeTranslations(IList<string> translations, Task.ErrorLoggerDelegate writeError)
		{
			HashSet<string> mailTipTranslationCultures = new HashSet<string>(translations.Count<string>(), StringComparer.OrdinalIgnoreCase);
			return MailboxTaskHelper.ValidateAndSanitizeTranslations(translations, mailTipTranslationCultures, false, false, writeError);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0002F064 File Offset: 0x0002D264
		public static void ProcessRecord(Action action, MailboxTaskHelper.ThrowTerminatingErrorDelegate handleError, object identity)
		{
			try
			{
				action();
			}
			catch (InboxRuleOperationException ex)
			{
				handleError(new InvalidOperationException(ex.Message), ErrorCategory.InvalidOperation, identity);
			}
			catch (RulesTooBigException)
			{
				handleError(new InvalidOperationException(Strings.ErrorInboxRuleTooBig), ErrorCategory.InvalidOperation, identity);
			}
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0002F0C8 File Offset: 0x0002D2C8
		internal static ADMicrosoftExchangeRecipient FindMicrosoftExchangeRecipient(IRecipientSession recipientSession, IConfigurationSession configurationSession)
		{
			if (recipientSession == null)
			{
				throw new ArgumentNullException("recipientSession");
			}
			if (configurationSession == null)
			{
				throw new ArgumentNullException("configurationSession");
			}
			bool useConfigNC = recipientSession.UseConfigNC;
			bool useGlobalCatalog = recipientSession.UseGlobalCatalog;
			recipientSession.UseConfigNC = true;
			recipientSession.UseGlobalCatalog = false;
			ADMicrosoftExchangeRecipient result = (ADMicrosoftExchangeRecipient)recipientSession.Read(ADMicrosoftExchangeRecipient.GetDefaultId(configurationSession));
			recipientSession.UseConfigNC = useConfigNC;
			recipientSession.UseGlobalCatalog = useGlobalCatalog;
			return result;
		}

		// Token: 0x0400053F RID: 1343
		internal const string LiveIdForBrandInfoPrefix = "c4e67852e761400490f0750a898dc64e";

		// Token: 0x04000540 RID: 1344
		internal const string ExchangeRemovedMailboxId = "ExRemoved-";

		// Token: 0x04000541 RID: 1345
		public const string Password = "Password";

		// Token: 0x04000542 RID: 1346
		public const string RoomMailboxPassword = "RoomMailboxPassword";

		// Token: 0x04000543 RID: 1347
		public const string ParameterSkipMailboxProvisioningConstraintValidation = "SkipMailboxProvisioningConstraintValidation";

		// Token: 0x04000544 RID: 1348
		private const string ForeignSecurityPrincipalsDNPrefix = "CN=ForeignSecurityPrincipals,DC=";

		// Token: 0x04000545 RID: 1349
		internal const int MaxNameLength = 64;

		// Token: 0x04000546 RID: 1350
		internal static readonly QueryFilter mailboxPlanFilter = new AndFilter(new QueryFilter[]
		{
			new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetailsValue, RecipientTypeDetails.MailboxPlan),
			new OrFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.MailboxPlanRelease, MailboxPlanRelease.CurrentRelease),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.MailboxPlanRelease, MailboxPlanRelease.AllReleases)
			})
		});

		// Token: 0x04000547 RID: 1351
		internal static readonly QueryFilter currentReleaseMailboxPlanFilter = new AndFilter(new QueryFilter[]
		{
			new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetailsValue, RecipientTypeDetails.MailboxPlan),
			new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.MailboxPlanRelease, MailboxPlanRelease.CurrentRelease)
		});

		// Token: 0x04000548 RID: 1352
		internal static readonly QueryFilter defaultMailboxPlanFilter = QueryFilter.AndTogether(new QueryFilter[]
		{
			MailboxTaskHelper.mailboxPlanFilter,
			new ComparisonFilter(ComparisonOperator.Equal, MailboxPlanSchema.IsDefault, true)
		});

		// Token: 0x0200011D RID: 285
		// (Invoke) Token: 0x06000A1F RID: 2591
		internal delegate IConfigurable GetUniqueObject(IIdentityParameter id, IConfigDataProvider session, ObjectId rootID, LocalizedString? notFoundError, LocalizedString? multipleFoundError, ExchangeErrorCategory errorCategory);

		// Token: 0x0200011E RID: 286
		// (Invoke) Token: 0x06000A23 RID: 2595
		internal delegate LocalizedString TwoStringErrorDelegate(string str1, string str2);

		// Token: 0x0200011F RID: 287
		// (Invoke) Token: 0x06000A27 RID: 2599
		internal delegate LocalizedString OneStringErrorDelegate(string str1);

		// Token: 0x02000120 RID: 288
		// (Invoke) Token: 0x06000A2B RID: 2603
		public delegate void ThrowTerminatingErrorDelegate(Exception exception, ErrorCategory category, object target);
	}
}
