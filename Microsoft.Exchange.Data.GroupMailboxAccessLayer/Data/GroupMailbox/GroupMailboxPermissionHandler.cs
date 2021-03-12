using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x0200001E RID: 30
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class GroupMailboxPermissionHandler
	{
		// Token: 0x0600010C RID: 268 RVA: 0x00008516 File Offset: 0x00006716
		public static bool IsFolderToBeIgnored(DefaultFolderType defaultFolderType)
		{
			return defaultFolderType == DefaultFolderType.None || defaultFolderType == DefaultFolderType.LegacySpoolerQueue || defaultFolderType == DefaultFolderType.Audits || defaultFolderType == DefaultFolderType.System || defaultFolderType == DefaultFolderType.AdminAuditLogs || defaultFolderType == DefaultFolderType.RssSubscription || defaultFolderType == DefaultFolderType.Conflicts || defaultFolderType == DefaultFolderType.SyncIssues || defaultFolderType == DefaultFolderType.LocalFailures || defaultFolderType == DefaultFolderType.ServerFailures;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000854C File Offset: 0x0000674C
		public static bool ModifyPermission(PermissionSet permissionSet, PermissionSecurityPrincipal permissionSecurityPrincipal, MemberRights memberRights)
		{
			bool result = false;
			if (permissionSecurityPrincipal.Type == PermissionSecurityPrincipal.SecurityPrincipalType.SpecialPrincipal && permissionSecurityPrincipal.SpecialType == PermissionSecurityPrincipal.SpecialPrincipalType.Default)
			{
				GroupMailboxPermissionHandler.Tracer.TraceDebug<PermissionSecurityPrincipal.SpecialPrincipalType, MemberRights>(0L, "Modifying permissions for permissionSecurityPrincipal {0} with rights {1}", PermissionSecurityPrincipal.SpecialPrincipalType.Default, memberRights);
				Permission permission = permissionSet.DefaultPermission;
				if (permission != null && permission.MemberRights != memberRights)
				{
					permissionSet.SetDefaultPermission(memberRights);
					result = true;
				}
			}
			else
			{
				GroupMailboxPermissionHandler.Tracer.TraceDebug<string, MemberRights>(0L, "Modifying permissions for permissionSecurityPrincipal {0} with rights {1}", (permissionSecurityPrincipal.Type == PermissionSecurityPrincipal.SecurityPrincipalType.ExternalUserPrincipal) ? permissionSecurityPrincipal.ExternalUser.ExternalId : string.Empty, memberRights);
				Permission permission = permissionSet.GetEntry(permissionSecurityPrincipal);
				if (permission == null && memberRights != MemberRights.None)
				{
					permission = permissionSet.AddEntry(permissionSecurityPrincipal, PermissionLevel.None);
					permission.MemberRights = memberRights;
					result = true;
				}
				else if (permission != null && memberRights == MemberRights.None)
				{
					permissionSet.RemoveEntry(permissionSecurityPrincipal);
					result = true;
				}
				else if (permission != null && permission.MemberRights != memberRights)
				{
					permission.MemberRights = memberRights;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000861C File Offset: 0x0000681C
		public static bool AssignMemberRight(MailboxSession mailboxSession, List<PermissionEntry> permissionEntries, DefaultFolderType defaultFolderType, out int foldersModified)
		{
			foldersModified = 0;
			try
			{
				using (Folder folder = Folder.Bind(mailboxSession, defaultFolderType))
				{
					bool flag = false;
					PermissionSet permissionSet = folder.GetPermissionSet();
					foreach (PermissionEntry permissionEntry in permissionEntries)
					{
						if (permissionEntry.UserSecurityPrincipal != null && GroupMailboxPermissionHandler.ModifyPermission(permissionSet, permissionEntry.UserSecurityPrincipal, permissionEntry.UserRights))
						{
							flag = true;
							foldersModified++;
						}
					}
					if (flag)
					{
						folder.Save();
					}
					return true;
				}
			}
			catch (CorruptDataException arg)
			{
				GroupMailboxPermissionHandler.Tracer.TraceDebug<DefaultFolderType, CorruptDataException>(1L, "Member rights already configured for folder {0}. Exception: {1}", defaultFolderType, arg);
			}
			catch (ObjectNotFoundException ex)
			{
				GroupMailboxPermissionHandler.Tracer.TraceError(1L, (ex.InnerException != null) ? ex.InnerException.ToString() : ex.ToString());
			}
			return false;
		}

		// Token: 0x04000068 RID: 104
		public static readonly MemberRights DefaultFolderPermission = Permission.GetMemberRights(PermissionLevel.Author);

		// Token: 0x04000069 RID: 105
		public static readonly MemberRights OwnerSpecificPermission = MemberRights.DeleteAny;

		// Token: 0x0400006A RID: 106
		public static readonly MemberRights CalendarFolderPermissionForDefaultUsers = Permission.GetMemberRights(PermissionLevel.Reviewer);

		// Token: 0x0400006B RID: 107
		public static readonly MemberRights CalendarFolderPermission = Permission.GetMemberRights(PermissionLevel.Editor) | MemberRights.FreeBusySimple | MemberRights.FreeBusyDetailed;

		// Token: 0x0400006C RID: 108
		public static readonly MemberRights SearchFolderPermission = Permission.GetMemberRights(PermissionLevel.PublishingAuthor);

		// Token: 0x0400006D RID: 109
		public static readonly MemberRights MailboxAssociationPermission = MemberRights.None;

		// Token: 0x0400006E RID: 110
		public static readonly MemberRights ConfigurationFolderPermission = GroupMailboxPermissionHandler.DefaultFolderPermission | GroupMailboxPermissionHandler.OwnerSpecificPermission | MemberRights.EditAny;

		// Token: 0x0400006F RID: 111
		public static readonly ActiveDirectoryRights MailboxRightsFullAccess = ActiveDirectoryRights.CreateChild;

		// Token: 0x04000070 RID: 112
		internal static readonly int GroupMailboxPermissionVersion = 2;

		// Token: 0x04000071 RID: 113
		internal static readonly ActiveDirectoryAccessRule MailboxRightsFullAccessRule = new ActiveDirectoryAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), GroupMailboxPermissionHandler.MailboxRightsFullAccess, AccessControlType.Allow, Guid.Empty, ActiveDirectorySecurityInheritance.All, Guid.Empty);

		// Token: 0x04000072 RID: 114
		private static readonly Trace Tracer = ExTraceGlobals.GroupMailboxAccessLayerTracer;
	}
}
