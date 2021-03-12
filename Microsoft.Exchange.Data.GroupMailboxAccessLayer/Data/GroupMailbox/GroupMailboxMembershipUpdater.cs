using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x0200001D RID: 29
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class GroupMailboxMembershipUpdater
	{
		// Token: 0x06000102 RID: 258 RVA: 0x00007CF0 File Offset: 0x00005EF0
		public GroupMailboxMembershipUpdater(MailboxSession mailboxSession)
		{
			this.mailboxSession = mailboxSession;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00007CFF File Offset: 0x00005EFF
		public void Update()
		{
			if (this.PreMembershipUpdate() && this.MembershipUpdate())
			{
				this.PostMembershipUpdate();
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00007D18 File Offset: 0x00005F18
		private bool PreMembershipUpdate()
		{
			this.recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(false, ConsistencyMode.IgnoreInvalid, this.mailboxSession.GetADSessionSettings(), 106, "PreMembershipUpdate", "f:\\15.00.1497\\sources\\dev\\UnifiedGroups\\src\\UnifiedGroups\\GroupMailboxAccessLayer\\Commands\\GroupMailboxMembershipUpdater.cs");
			this.groupObject = this.recipientSession.FindADUserByObjectId(this.mailboxSession.MailboxOwner.ObjectId);
			if (this.groupObject == null)
			{
				GroupMailboxMembershipUpdater.Tracer.TraceError<string>((long)this.GetHashCode(), "PreMembershipUpdate: Unable to locate the AD object for the group mailbox {0} successfully", this.mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
				return false;
			}
			if (this.groupObject.RecipientTypeDetails != RecipientTypeDetails.GroupMailbox)
			{
				GroupMailboxMembershipUpdater.Tracer.TraceError<string>((long)this.GetHashCode(), "PreMembershipUpdate: The mailbox {0} is not of type group mailbox", this.mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
				return false;
			}
			GroupMailboxMembershipUpdater.Tracer.TraceDebug<string>((long)this.GetHashCode(), "PreMembershipUpdate: Located the AD object of the group mailbox {0} successfully", this.mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
			this.previousExternalMemberUser = new ExternalUser(this.mailboxSession.DisplayName, this.mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(), SmtpAddress.Parse(this.mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString()), GroupMailboxMembershipUpdater.EarlierGroupMailboxMemberAccessSecurityIdentifier);
			this.currentExternalMemberUser = ExternalUser.CreateExternalUserForGroupMailbox(this.mailboxSession.DisplayName, "Member@local", this.mailboxSession.MailboxGuid, SecurityIdentity.GroupMailboxMemberType.Member);
			this.currentExternalOwnerUser = ExternalUser.CreateExternalUserForGroupMailbox(this.mailboxSession.DisplayName, "Owner@local", this.mailboxSession.MailboxGuid, SecurityIdentity.GroupMailboxMemberType.Owner);
			using (ExternalUserCollection externalUsers = this.mailboxSession.GetExternalUsers())
			{
				if (!this.AddToExternalUserCollection(externalUsers, this.previousExternalMemberUser) || !this.AddToExternalUserCollection(externalUsers, this.currentExternalMemberUser) || !this.AddToExternalUserCollection(externalUsers, this.currentExternalOwnerUser))
				{
					GroupMailboxMembershipUpdater.Tracer.TraceError<string>((long)this.GetHashCode(), "PreMembershipUpdate: Unable to update external user collection to the group mailbox {0} successfully", this.mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
					return false;
				}
				GroupMailboxMembershipUpdater.Tracer.TraceDebug<string>((long)this.GetHashCode(), "PreMembershipUpdate: Updated external user collection of the group mailbox {0} successfully", this.mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
			}
			return true;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00007FC4 File Offset: 0x000061C4
		private bool MembershipUpdate()
		{
			return this.FolderMembershipUpdate() && this.ActiveDirectoryMembershipUpdate();
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00007FD8 File Offset: 0x000061D8
		private bool FolderMembershipUpdate()
		{
			ArgumentValidator.ThrowIfNull("previousExternalMemberUser", this.previousExternalMemberUser);
			ArgumentValidator.ThrowIfNull("currentExternalMemberUser", this.currentExternalMemberUser);
			ArgumentValidator.ThrowIfNull("currentExternalOwnerUser", this.currentExternalOwnerUser);
			PermissionSecurityPrincipal userSecurityPrincipal = new PermissionSecurityPrincipal(this.previousExternalMemberUser);
			PermissionSecurityPrincipal userSecurityPrincipal2 = new PermissionSecurityPrincipal(this.currentExternalMemberUser);
			PermissionSecurityPrincipal userSecurityPrincipal3 = new PermissionSecurityPrincipal(this.currentExternalOwnerUser);
			List<PermissionEntry> list = new List<PermissionEntry>(3);
			int num = 0;
			int num2 = 0;
			bool result = true;
			foreach (DefaultFolderType defaultFolderType in this.mailboxSession.DefaultFolders)
			{
				list.Clear();
				if (!GroupMailboxPermissionHandler.IsFolderToBeIgnored(defaultFolderType) && defaultFolderType != DefaultFolderType.MailboxAssociation)
				{
					if (this.mailboxSession.GetDefaultFolderId(defaultFolderType) != null)
					{
						DefaultFolderType defaultFolderType2 = defaultFolderType;
						MemberRights memberRights;
						MemberRights userRights;
						if (defaultFolderType2 != DefaultFolderType.Calendar)
						{
							switch (defaultFolderType2)
							{
							case DefaultFolderType.Configuration:
								memberRights = GroupMailboxPermissionHandler.DefaultFolderPermission;
								userRights = GroupMailboxPermissionHandler.ConfigurationFolderPermission;
								break;
							case DefaultFolderType.SearchFolders:
								memberRights = GroupMailboxPermissionHandler.SearchFolderPermission;
								userRights = (memberRights | GroupMailboxPermissionHandler.OwnerSpecificPermission);
								break;
							default:
								memberRights = GroupMailboxPermissionHandler.DefaultFolderPermission;
								userRights = (memberRights | GroupMailboxPermissionHandler.OwnerSpecificPermission);
								break;
							}
						}
						else
						{
							memberRights = GroupMailboxPermissionHandler.CalendarFolderPermission;
							userRights = GroupMailboxPermissionHandler.CalendarFolderPermission;
						}
						list.Add(new PermissionEntry(userSecurityPrincipal, MemberRights.None));
						list.Add(new PermissionEntry(userSecurityPrincipal3, userRights));
						list.Add(new PermissionEntry(userSecurityPrincipal2, memberRights));
						if (defaultFolderType == DefaultFolderType.Calendar)
						{
							list.Add(new PermissionEntry(new PermissionSecurityPrincipal(PermissionSecurityPrincipal.SpecialPrincipalType.Default), MemberRights.FreeBusySimple));
						}
						else
						{
							list.Add(new PermissionEntry(new PermissionSecurityPrincipal(PermissionSecurityPrincipal.SpecialPrincipalType.Default), MemberRights.None));
						}
						if (!GroupMailboxPermissionHandler.AssignMemberRight(this.mailboxSession, list, defaultFolderType, out num2))
						{
							result = false;
							break;
						}
						num += num2;
					}
					else
					{
						GroupMailboxMembershipUpdater.Tracer.TraceError<DefaultFolderType, string>((long)this.GetHashCode(), "MembershipUpdate: Found a folder {0} that is not in group mailbox {1}", defaultFolderType, this.mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
					}
				}
			}
			GroupMailboxMembershipUpdater.Tracer.TraceDebug<int, string>((long)this.GetHashCode(), "MembershipUpdate: {0} folders had been modified for the group mailbox {1}", num, this.mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
			return result;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00008200 File Offset: 0x00006400
		private bool ActiveDirectoryMembershipUpdate()
		{
			ArgumentValidator.ThrowIfNull("groupObject", this.groupObject);
			Exception ex = null;
			try
			{
				ActiveDirectorySecurity activeDirectorySecurity = SecurityDescriptorConverter.ConvertToActiveDirectorySecurity(this.groupObject.ExchangeSecurityDescriptor);
				activeDirectorySecurity.RemoveAccessRule(GroupMailboxPermissionHandler.MailboxRightsFullAccessRule);
				this.groupObject.ExchangeSecurityDescriptor = SecurityDescriptorConverter.ConvertToRawSecurityDescriptor(activeDirectorySecurity);
			}
			catch (OverflowException ex2)
			{
				ex = ex2;
			}
			catch (COMException ex3)
			{
				ex = ex3;
			}
			catch (UnauthorizedAccessException ex4)
			{
				ex = ex4;
			}
			catch (TransientException ex5)
			{
				ex = ex5;
			}
			catch (DataSourceOperationException ex6)
			{
				ex = ex6;
			}
			if (ex != null)
			{
				GroupMailboxMembershipUpdater.Tracer.TraceError<string>((long)this.GetHashCode(), "ActiveDirectoryMembershipUpdate: Exception {0}", ex.Message);
				return false;
			}
			GroupMailboxMembershipUpdater.Tracer.TraceDebug<string>((long)this.GetHashCode(), "ActiveDirectoryMembershipUpdate: Updated active directory successfully for the group mailbox {0}", this.mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
			this.recipientSession.Save(this.groupObject);
			return true;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000831C File Offset: 0x0000651C
		private void PostMembershipUpdate()
		{
			ArgumentValidator.ThrowIfNull("previousExternalMemberUser", this.previousExternalMemberUser);
			using (ExternalUserCollection externalUsers = this.mailboxSession.GetExternalUsers())
			{
				if (!this.RemoveFromExternalUserCollection(externalUsers, this.previousExternalMemberUser))
				{
					GroupMailboxMembershipUpdater.Tracer.TraceError<string>((long)this.GetHashCode(), "PostMembershipUpdate: Unable to update external user collection of the group mailbox {0} successfully", this.mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
				}
				else
				{
					GroupMailboxMembershipUpdater.Tracer.TraceDebug<string>((long)this.GetHashCode(), "PostMembershipUpdate: Updated external user collection of the group mailbox {0} successfully", this.mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
				}
			}
			this.mailboxSession.Mailbox[MailboxSchema.GroupMailboxPermissionsVersion] = GroupMailboxPermissionHandler.GroupMailboxPermissionVersion;
			this.mailboxSession.Mailbox.Save();
			this.mailboxSession.Mailbox.Load();
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00008424 File Offset: 0x00006624
		private bool AddToExternalUserCollection(ExternalUserCollection externalUserCollection, ExternalUser externalUser)
		{
			if (!externalUserCollection.Contains(externalUser))
			{
				externalUserCollection.Add(externalUser);
			}
			externalUserCollection.Save();
			if (!externalUserCollection.Contains(externalUser))
			{
				GroupMailboxMembershipUpdater.Tracer.TraceError<ExternalUser, string>((long)this.GetHashCode(), "Unable to add external user {0} to the group mailbox {1}", externalUser, this.mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
				return false;
			}
			return true;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00008490 File Offset: 0x00006690
		private bool RemoveFromExternalUserCollection(ExternalUserCollection externalUserCollection, ExternalUser externalUser)
		{
			if (externalUserCollection.Contains(externalUser))
			{
				externalUserCollection.Remove(externalUser);
			}
			externalUserCollection.Save();
			if (externalUserCollection.Contains(externalUser))
			{
				GroupMailboxMembershipUpdater.Tracer.TraceError<ExternalUser, string>((long)this.GetHashCode(), "Unable to remove external user {0} from the group mailbox {1}", externalUser, this.mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
				return false;
			}
			return true;
		}

		// Token: 0x04000060 RID: 96
		public static readonly SecurityIdentifier EarlierGroupMailboxMemberAccessSecurityIdentifier = new SecurityIdentifier("S-1-8-0-0-0-1");

		// Token: 0x04000061 RID: 97
		private static readonly Trace Tracer = ExTraceGlobals.GroupMailboxAccessLayerTracer;

		// Token: 0x04000062 RID: 98
		private readonly MailboxSession mailboxSession;

		// Token: 0x04000063 RID: 99
		private ExternalUser previousExternalMemberUser;

		// Token: 0x04000064 RID: 100
		private ExternalUser currentExternalMemberUser;

		// Token: 0x04000065 RID: 101
		private ExternalUser currentExternalOwnerUser;

		// Token: 0x04000066 RID: 102
		private ADUser groupObject;

		// Token: 0x04000067 RID: 103
		private IRecipientSession recipientSession;
	}
}
