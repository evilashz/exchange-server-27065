using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007AF RID: 1967
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class MapiAclTableRestriction : IModifyTableRestriction
	{
		// Token: 0x06004A06 RID: 18950 RVA: 0x0013573E File Offset: 0x0013393E
		internal MapiAclTableRestriction(CoreFolder coreFolder)
		{
			Util.ThrowOnNullArgument(coreFolder, "coreFolder");
			this.coreFolder = coreFolder;
			this.session = coreFolder.Session;
		}

		// Token: 0x06004A07 RID: 18951 RVA: 0x001357B4 File Offset: 0x001339B4
		void IModifyTableRestriction.Enforce(IModifyTable modifyTable, IEnumerable<ModifyTableOperation> changingEntries)
		{
			if (changingEntries == null)
			{
				return;
			}
			this.Enforce(() => this.GetExternalUserPermissions(new MapiAclTableAdapter(modifyTable), from entry in changingEntries
			select AclTableEntry.ModifyOperation.FromModifyTableOperation(entry)));
		}

		// Token: 0x06004A08 RID: 18952 RVA: 0x001357F8 File Offset: 0x001339F8
		internal void Enforce(Func<ICollection<MapiAclTableRestriction.ExternalUserPermission>> getExternalUserPermissions)
		{
			if ((this.session == null || !this.session.IsMoveUser) && this.coreFolder.IsPermissionChangeBlocked())
			{
				ExTraceGlobals.StorageTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: Cannot change permissions on permission-change-blocked folder.", this.session.DisplayAddress);
				throw new CannotChangePermissionsOnFolderException();
			}
			MailboxSession mailboxSession = this.session as MailboxSession;
			if (mailboxSession == null || mailboxSession.IsGroupMailbox())
			{
				return;
			}
			this.EnforceSharingPolicy(mailboxSession, getExternalUserPermissions());
		}

		// Token: 0x06004A09 RID: 18953 RVA: 0x00135874 File Offset: 0x00133A74
		private void EnforceSharingPolicy(MailboxSession mailboxSession, ICollection<MapiAclTableRestriction.ExternalUserPermission> externalUserPermissions)
		{
			Util.ThrowOnNullArgument(mailboxSession, "mailboxSession");
			if (externalUserPermissions == null || externalUserPermissions.Count == 0)
			{
				return;
			}
			List<RightsNotAllowedRecipient> list = new List<RightsNotAllowedRecipient>(externalUserPermissions.Count);
			SharingPolicy sharingPolicy = null;
			foreach (MapiAclTableRestriction.ExternalUserPermission externalUserPermission in externalUserPermissions)
			{
				if (sharingPolicy == null)
				{
					IMailboxInfo mailboxInfo = mailboxSession.MailboxOwner.MailboxInfo;
					sharingPolicy = DirectoryHelper.ReadSharingPolicy(mailboxInfo.MailboxGuid, mailboxInfo.IsArchive, mailboxSession.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid));
					if (sharingPolicy == null)
					{
						ExTraceGlobals.StorageTracer.TraceDebug<IExchangePrincipal>((long)this.GetHashCode(), "{0}: No policy assigned means no external sharing is allowed for this user.", mailboxSession.MailboxOwner);
						throw new NotAllowedExternalSharingByPolicyException();
					}
				}
				if (!sharingPolicy.Enabled)
				{
					ExTraceGlobals.StorageTracer.TraceDebug<IExchangePrincipal>((long)this.GetHashCode(), "{0}: A disabled policy means no external sharing is allowed for this user.", mailboxSession.MailboxOwner);
					throw new NotAllowedExternalSharingByPolicyException();
				}
				SharingPolicyAction sharingPolicyAction = externalUserPermission.Principal.ExternalUser.IsReachUser ? sharingPolicy.GetAllowedForAnonymousCalendarSharing() : sharingPolicy.GetAllowed(externalUserPermission.Principal.ExternalUser.SmtpAddress.Domain);
				if (sharingPolicyAction == (SharingPolicyAction)0)
				{
					ExTraceGlobals.StorageTracer.TraceDebug<IExchangePrincipal, PermissionSecurityPrincipal>((long)this.GetHashCode(), "{0}: Policy does not allow granting permissions to {1}.", mailboxSession.MailboxOwner, externalUserPermission.Principal);
					throw new PrincipalNotAllowedByPolicyException(externalUserPermission.Principal);
				}
				MemberRights allowed = PolicyAllowedMemberRights.GetAllowed(sharingPolicyAction, this.FolderInfo.StoreObjectType);
				MemberRights memberRights = ~allowed & externalUserPermission.MemberRights;
				if (memberRights != MemberRights.None)
				{
					ExTraceGlobals.StorageTracer.TraceDebug((long)this.GetHashCode(), "{0}: Policy does not allow granting permission {1} to {2} on {3} folder '{4}'.", new object[]
					{
						mailboxSession.MailboxOwner,
						memberRights,
						externalUserPermission.Principal,
						this.FolderInfo.StoreObjectType,
						this.FolderInfo.DisplayName
					});
					list.Add(new RightsNotAllowedRecipient(externalUserPermission.Principal, memberRights));
				}
			}
			if (list.Count > 0)
			{
				throw new RightsNotAllowedByPolicyException(list.ToArray(), this.FolderInfo.StoreObjectType, this.FolderInfo.DisplayName);
			}
		}

		// Token: 0x06004A0A RID: 18954 RVA: 0x00135A9C File Offset: 0x00133C9C
		private ICollection<MapiAclTableRestriction.ExternalUserPermission> GetExternalUserPermissions(MapiAclTableAdapter mapiAclTableAdapter, IEnumerable<AclTableEntry.ModifyOperation> changingEntries)
		{
			List<MapiAclTableRestriction.ExternalUserPermission> list = null;
			ExternalUserCollection disposable = null;
			try
			{
				foreach (AclTableEntry.ModifyOperation modifyOperation in changingEntries)
				{
					if (modifyOperation.Operation == ModifyTableOperationType.Add || modifyOperation.Operation == ModifyTableOperationType.Modify)
					{
						MapiAclTableRestriction.ExternalUserPermission externalUserPermission = this.TryGetExternalUserPermission(modifyOperation.Entry, mapiAclTableAdapter, ref disposable);
						if (externalUserPermission != null)
						{
							if (list == null)
							{
								list = new List<MapiAclTableRestriction.ExternalUserPermission>();
							}
							list.Add(externalUserPermission);
						}
					}
				}
			}
			finally
			{
				Util.DisposeIfPresent(disposable);
			}
			return list;
		}

		// Token: 0x06004A0B RID: 18955 RVA: 0x00135B30 File Offset: 0x00133D30
		private MapiAclTableRestriction.ExternalUserPermission TryGetExternalUserPermission(AclTableEntry aclTableEntry, MapiAclTableAdapter mapiAclTableAdapter, ref ExternalUserCollection externalUsers)
		{
			Util.ThrowOnNullArgument(aclTableEntry, "aclTableEntry");
			Util.ThrowOnNullArgument(mapiAclTableAdapter, "mapiAclTableAdapter");
			MailboxSession mailboxSession = this.session as MailboxSession;
			if (mailboxSession == null)
			{
				return null;
			}
			byte[] memberEntryId = aclTableEntry.MemberEntryId;
			MemberRights memberRights = aclTableEntry.MemberRights;
			long memberId = aclTableEntry.MemberId;
			if (memberEntryId == null || memberEntryId.Length == 0)
			{
				if (memberId <= 0L)
				{
					return null;
				}
				ExTraceGlobals.StorageTracer.TraceDebug<IExchangePrincipal, long>((long)this.GetHashCode(), "{0}: Getting memberEntryId from current ACL table for MemberId {1}.", mailboxSession.MailboxOwner, memberId);
				AclTableEntry byMemberId = mapiAclTableAdapter.GetByMemberId(memberId);
				if (byMemberId == null || byMemberId.MemberEntryId == null)
				{
					ExTraceGlobals.StorageTracer.TraceDebug<IExchangePrincipal, long>((long)this.GetHashCode(), "{0}: Not found memberEntryId from current ACL table for MemberId {1}. Skipped.", mailboxSession.MailboxOwner, memberId);
					return null;
				}
				memberEntryId = byMemberId.MemberEntryId;
			}
			if (mapiAclTableAdapter.TryGetParticipantEntryId(memberEntryId) != null)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<IExchangePrincipal>((long)this.GetHashCode(), "{0}: MemberEntryId indicates internal user. Skipped.", mailboxSession.MailboxOwner);
				return null;
			}
			ExternalUser externalUser = mapiAclTableAdapter.TryGetExternalUser(memberEntryId, ref externalUsers);
			if (externalUser == null)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<IExchangePrincipal>((long)this.GetHashCode(), "{0}: MemberEntryId is not external user. Skipped.", mailboxSession.MailboxOwner);
				return null;
			}
			return new MapiAclTableRestriction.ExternalUserPermission(externalUser, memberRights);
		}

		// Token: 0x17001525 RID: 5413
		// (get) Token: 0x06004A0C RID: 18956 RVA: 0x00135C3D File Offset: 0x00133E3D
		private MapiAclTableRestriction.FolderInformation FolderInfo
		{
			get
			{
				if (this.folderInfo == null)
				{
					this.folderInfo = new MapiAclTableRestriction.FolderInformation(this.coreFolder);
				}
				return this.folderInfo;
			}
		}

		// Token: 0x040027F4 RID: 10228
		private readonly CoreFolder coreFolder;

		// Token: 0x040027F5 RID: 10229
		private readonly StoreSession session;

		// Token: 0x040027F6 RID: 10230
		private MapiAclTableRestriction.FolderInformation folderInfo;

		// Token: 0x020007B0 RID: 1968
		internal class ExternalUserPermission
		{
			// Token: 0x06004A0D RID: 18957 RVA: 0x00135C5E File Offset: 0x00133E5E
			internal ExternalUserPermission(ExternalUser externalUser, MemberRights memberRights)
			{
				this.Principal = new PermissionSecurityPrincipal(externalUser);
				this.MemberRights = memberRights;
			}

			// Token: 0x06004A0E RID: 18958 RVA: 0x00135C79 File Offset: 0x00133E79
			internal ExternalUserPermission(PermissionSecurityPrincipal principal, MemberRights memberRights)
			{
				this.Principal = principal;
				this.MemberRights = memberRights;
			}

			// Token: 0x040027F7 RID: 10231
			internal readonly MemberRights MemberRights;

			// Token: 0x040027F8 RID: 10232
			internal readonly PermissionSecurityPrincipal Principal;
		}

		// Token: 0x020007B1 RID: 1969
		private class FolderInformation
		{
			// Token: 0x06004A0F RID: 18959 RVA: 0x00135C90 File Offset: 0x00133E90
			internal FolderInformation(CoreFolder coreFolder)
			{
				this.propertyBag = CoreObject.GetPersistablePropertyBag(coreFolder);
				this.propertyBag.Load(MapiAclTableRestriction.FolderInformation.PropertyDefinitions);
				string valueOrDefault = this.propertyBag.GetValueOrDefault<string>(StoreObjectSchema.ContainerClass);
				this.storeObjectType = ObjectClass.GetObjectType(valueOrDefault);
			}

			// Token: 0x17001526 RID: 5414
			// (get) Token: 0x06004A10 RID: 18960 RVA: 0x00135CDC File Offset: 0x00133EDC
			internal StoreObjectType StoreObjectType
			{
				get
				{
					return this.storeObjectType;
				}
			}

			// Token: 0x17001527 RID: 5415
			// (get) Token: 0x06004A11 RID: 18961 RVA: 0x00135CE4 File Offset: 0x00133EE4
			internal string DisplayName
			{
				get
				{
					if (string.IsNullOrEmpty(this.displayName))
					{
						this.displayName = this.propertyBag.GetValueOrDefault<string>(FolderSchema.DisplayName);
					}
					return this.displayName ?? string.Empty;
				}
			}

			// Token: 0x040027F9 RID: 10233
			private static readonly PropertyDefinition[] PropertyDefinitions = new PropertyDefinition[]
			{
				StoreObjectSchema.ContainerClass,
				FolderSchema.DisplayName
			};

			// Token: 0x040027FA RID: 10234
			private readonly PersistablePropertyBag propertyBag;

			// Token: 0x040027FB RID: 10235
			private StoreObjectType storeObjectType;

			// Token: 0x040027FC RID: 10236
			private string displayName;
		}

		// Token: 0x020007B2 RID: 1970
		private enum RowFlags
		{
			// Token: 0x040027FE RID: 10238
			Add = 1,
			// Token: 0x040027FF RID: 10239
			Modify
		}
	}
}
