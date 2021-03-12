using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007C5 RID: 1989
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class PermissionTable : IEnumerable<Permission>, IEnumerable
	{
		// Token: 0x06004AA3 RID: 19107 RVA: 0x00138408 File Offset: 0x00136608
		internal static PermissionTable Load(Func<PermissionTable, PermissionSet> permissionSetFactory, CoreFolder coreFolder)
		{
			PermissionTable permissionTable = new PermissionTable(permissionSetFactory);
			permissionTable.LoadFrom(coreFolder);
			object obj = coreFolder.Session.Mailbox.TryGetProperty(MailboxSchema.MailboxType);
			if (obj is int && StoreSession.IsPublicFolderMailbox((int)obj))
			{
				permissionTable.isPublicFolder = true;
			}
			PermissionTable.isCrossPremiseDelegateAllowedForMailboxOwner = (coreFolder.Session.MailboxOwner != null && DelegateUserCollection.IsCrossPremiseDelegateEnabled(coreFolder.Session.MailboxOwner));
			return permissionTable;
		}

		// Token: 0x06004AA4 RID: 19108 RVA: 0x0013847C File Offset: 0x0013667C
		internal static PermissionTable Create(Func<PermissionTable, PermissionSet> permissionSetFactory)
		{
			PermissionTable permissionTable = new PermissionTable(permissionSetFactory);
			permissionTable.defaultMemberPermission = permissionTable.PermissionSet.CreatePermission(new PermissionSecurityPrincipal(PermissionSecurityPrincipal.SpecialPrincipalType.Default), MemberRights.None, 0L);
			permissionTable.anonymousMemberPermission = permissionTable.PermissionSet.CreatePermission(new PermissionSecurityPrincipal(PermissionSecurityPrincipal.SpecialPrincipalType.Anonymous), MemberRights.None, -1L);
			return permissionTable;
		}

		// Token: 0x06004AA5 RID: 19109 RVA: 0x001384C5 File Offset: 0x001366C5
		private PermissionTable(Func<PermissionTable, PermissionSet> permissionSetFactory)
		{
			Util.ThrowOnNullArgument(permissionSetFactory, "permissionSetFactory");
			this.permissionSet = permissionSetFactory(this);
		}

		// Token: 0x06004AA6 RID: 19110 RVA: 0x001384FC File Offset: 0x001366FC
		internal Permission AddEntry(PermissionSecurityPrincipal securityPrincipal, MemberRights memberRights)
		{
			this.CheckValid();
			EnumValidator.ThrowIfInvalid<MemberRights>(memberRights, "memberRights");
			if (securityPrincipal == null)
			{
				throw new ArgumentNullException("securityPrincipal");
			}
			if (string.IsNullOrEmpty(securityPrincipal.IndexString))
			{
				throw new ArgumentException("SecurityPrincipal must at least have a valid index string.", "securityPrincipal");
			}
			if (securityPrincipal.Type == PermissionSecurityPrincipal.SecurityPrincipalType.ADRecipientPrincipal && !PermissionTable.isCrossPremiseDelegateAllowedForMailboxOwner)
			{
				ADRecipient adrecipient = securityPrincipal.ADRecipient;
				if (!ADRecipient.IsValidRecipient(adrecipient, !this.isPublicFolder))
				{
					throw new ArgumentException("Cannot use " + adrecipient.DisplayName + " as security principal", "securityPrincipal");
				}
			}
			Permission permission = null;
			if (!this.permissions.ContainsKey(securityPrincipal.IndexString))
			{
				if (this.removedPermissions.TryGetValue(securityPrincipal.IndexString, out permission))
				{
					this.removedPermissions.Remove(securityPrincipal.IndexString);
					permission.MemberRights = memberRights;
				}
				else
				{
					permission = this.PermissionSet.CreatePermission(securityPrincipal, memberRights);
				}
			}
			this.AddPermissionEntry(securityPrincipal, permission);
			return permission;
		}

		// Token: 0x06004AA7 RID: 19111 RVA: 0x001385E8 File Offset: 0x001367E8
		internal void RemoveEntry(PermissionSecurityPrincipal securityPrincipal)
		{
			this.CheckValid();
			Util.ThrowOnNullArgument(securityPrincipal, "securityPrincipal");
			Permission permission;
			if (this.permissions.TryGetValue(securityPrincipal.IndexString, out permission))
			{
				if (permission.Origin == PermissionOrigin.Read)
				{
					this.removedPermissions.Add(securityPrincipal.IndexString, permission);
				}
				this.permissions.Remove(securityPrincipal.IndexString);
			}
		}

		// Token: 0x06004AA8 RID: 19112 RVA: 0x00138648 File Offset: 0x00136848
		internal void Clear()
		{
			this.CheckValid();
			if (this.DefaultPermission != null)
			{
				this.DefaultPermission.MemberRights = MemberRights.None;
			}
			if (this.AnonymousPermission != null)
			{
				this.AnonymousPermission.MemberRights = MemberRights.None;
			}
			List<PermissionSecurityPrincipal> list = new List<PermissionSecurityPrincipal>();
			foreach (Permission permission in this)
			{
				list.Add(permission.Principal);
			}
			foreach (PermissionSecurityPrincipal securityPrincipal in list)
			{
				this.RemoveEntry(securityPrincipal);
			}
		}

		// Token: 0x06004AA9 RID: 19113 RVA: 0x00138708 File Offset: 0x00136908
		internal Permission GetEntry(PermissionSecurityPrincipal securityPrincipal)
		{
			this.CheckValid();
			Util.ThrowOnNullArgument(securityPrincipal, "securityPrincipal");
			Permission result = null;
			if (this.permissions.TryGetValue(securityPrincipal.IndexString, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x1700154B RID: 5451
		// (get) Token: 0x06004AAA RID: 19114 RVA: 0x00138740 File Offset: 0x00136940
		// (set) Token: 0x06004AAB RID: 19115 RVA: 0x0013874E File Offset: 0x0013694E
		internal Permission DefaultPermission
		{
			get
			{
				this.CheckValid();
				return this.defaultMemberPermission;
			}
			set
			{
				this.CheckValid();
				this.defaultMemberPermission = value;
			}
		}

		// Token: 0x1700154C RID: 5452
		// (get) Token: 0x06004AAC RID: 19116 RVA: 0x0013875D File Offset: 0x0013695D
		// (set) Token: 0x06004AAD RID: 19117 RVA: 0x0013876B File Offset: 0x0013696B
		internal Permission AnonymousPermission
		{
			get
			{
				this.CheckValid();
				return this.anonymousMemberPermission;
			}
			set
			{
				this.CheckValid();
				this.anonymousMemberPermission = value;
			}
		}

		// Token: 0x06004AAE RID: 19118 RVA: 0x0013877C File Offset: 0x0013697C
		internal void Save(CoreFolder coreFolder)
		{
			this.CheckValid();
			Util.ThrowOnNullArgument(coreFolder, "coreFolder");
			if (this.IsDirty)
			{
				this.EnforceRestriction(coreFolder);
				this.SaveSharingPartnership(coreFolder.Session as MailboxSession);
				using (IModifyTable permissionTable = coreFolder.GetPermissionTable(this.PermissionSet.ModifyTableOptions))
				{
					MapiAclTableAdapter mapiAclTableAdapter = new MapiAclTableAdapter(permissionTable);
					this.AddPermissionEntriesForRemove(mapiAclTableAdapter);
					this.AddPermissionEntriesForAddOrModify(mapiAclTableAdapter);
					mapiAclTableAdapter.ApplyPendingChanges(true);
				}
			}
			this.isInvalid = true;
		}

		// Token: 0x1700154D RID: 5453
		// (get) Token: 0x06004AAF RID: 19119 RVA: 0x0013880C File Offset: 0x00136A0C
		internal bool IsDirty
		{
			get
			{
				this.CheckValid();
				if (this.removedPermissions.Count > 0)
				{
					return true;
				}
				if (this.defaultMemberPermission != null && this.defaultMemberPermission.IsDirty)
				{
					return true;
				}
				if (this.anonymousMemberPermission != null && this.anonymousMemberPermission.IsDirty)
				{
					return true;
				}
				foreach (Permission permission in this.permissions.Values)
				{
					if (permission.IsDirty)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x1700154E RID: 5454
		// (get) Token: 0x06004AB0 RID: 19120 RVA: 0x001388B0 File Offset: 0x00136AB0
		internal PermissionSet PermissionSet
		{
			get
			{
				this.CheckValid();
				return this.permissionSet;
			}
		}

		// Token: 0x06004AB1 RID: 19121 RVA: 0x001388BE File Offset: 0x00136ABE
		public IEnumerator<Permission> GetEnumerator()
		{
			this.CheckValid();
			return this.permissions.Values.GetEnumerator();
		}

		// Token: 0x06004AB2 RID: 19122 RVA: 0x001388DB File Offset: 0x00136ADB
		IEnumerator IEnumerable.GetEnumerator()
		{
			this.CheckValid();
			return this.GetEnumerator();
		}

		// Token: 0x06004AB3 RID: 19123 RVA: 0x001388E9 File Offset: 0x00136AE9
		private void CheckValid()
		{
			if (this.isInvalid)
			{
				throw new InvalidOperationException("PermissionTable should not be used after a call to Save()");
			}
		}

		// Token: 0x06004AB4 RID: 19124 RVA: 0x00138900 File Offset: 0x00136B00
		private void LoadFrom(CoreFolder coreFolder)
		{
			using (IModifyTable permissionTable = coreFolder.GetPermissionTable(this.PermissionSet.ModifyTableOptions))
			{
				try
				{
					this.LoadFrom(new MapiAclTableAdapter(permissionTable));
				}
				catch (DataSourceOperationException ex)
				{
					throw StorageGlobals.TranslateDirectoryException(ServerStrings.ADException, ex, null, "PermissionsTable.LoadFrom. Failed due to directory exception {0}.", new object[]
					{
						ex
					});
				}
				catch (DataSourceTransientException ex2)
				{
					throw StorageGlobals.TranslateDirectoryException(ServerStrings.ADException, ex2, null, "PermissionsTable.LoadFrom. Failed due to directory exception {0}.", new object[]
					{
						ex2
					});
				}
			}
		}

		// Token: 0x06004AB5 RID: 19125 RVA: 0x001389A4 File Offset: 0x00136BA4
		private void LoadFrom(MapiAclTableAdapter mapiAclTableAdapter)
		{
			IRecipientSession recipientSession = null;
			ExternalUserCollection disposable = null;
			AclTableEntry[] all = mapiAclTableAdapter.GetAll();
			try
			{
				foreach (AclTableEntry aclTableEntry in all)
				{
					long memberId = aclTableEntry.MemberId;
					byte[] memberEntryId = aclTableEntry.MemberEntryId;
					string memberName = aclTableEntry.MemberName;
					MemberRights memberRights = aclTableEntry.MemberRights;
					if (memberId == 0L)
					{
						this.defaultMemberPermission = this.permissionSet.CreatePermission(new PermissionSecurityPrincipal(PermissionSecurityPrincipal.SpecialPrincipalType.Default), memberRights, memberId);
					}
					else if (memberId == -1L)
					{
						this.anonymousMemberPermission = this.permissionSet.CreatePermission(new PermissionSecurityPrincipal(PermissionSecurityPrincipal.SpecialPrincipalType.Anonymous), memberRights, memberId);
					}
					else if (memberEntryId != null)
					{
						ADParticipantEntryId adparticipantEntryId = mapiAclTableAdapter.TryGetParticipantEntryId(memberEntryId);
						if (adparticipantEntryId != null)
						{
							if (recipientSession == null)
							{
								recipientSession = mapiAclTableAdapter.Session.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid);
							}
							ADRecipient adrecipient = null;
							try
							{
								adrecipient = recipientSession.FindByLegacyExchangeDN(adparticipantEntryId.LegacyDN);
							}
							catch (DataValidationException)
							{
								ExTraceGlobals.StorageTracer.TraceDebug<string, string>(0L, "PermissionTable::PermissionTable. Caught exception from ADSesssion.FindByLegacyExchangeDN when trying to find a recipient from the ACL Table. Recipient name = {0}, LegDN = {1}.", memberName, adparticipantEntryId.LegacyDN);
								this.AddUnknownEntry(memberName, memberId, memberEntryId, memberRights);
								goto IL_17E;
							}
							if (adrecipient != null)
							{
								Permission permission = this.permissionSet.CreatePermission(new PermissionSecurityPrincipal(adrecipient), memberRights, memberId);
								this.AddPermissionEntry(permission.Principal, permission);
							}
							else
							{
								ExTraceGlobals.StorageTracer.TraceDebug<string, string>(0L, "PermissionTable::PermissionTable. Did not find the recipient from the ACL table in the AD. Recipient name = {0}, LegDN = {1}.", memberName, adparticipantEntryId.LegacyDN);
								this.AddUnknownEntry(memberName, memberId, memberEntryId, memberRights);
							}
						}
						else
						{
							this.AddNonADEntry(mapiAclTableAdapter, ref disposable, memberName, memberId, memberEntryId, memberRights);
						}
					}
					else
					{
						ExTraceGlobals.StorageTracer.TraceDebug<string>(0L, "PermissionTable::PermissionTable. Found a member in the ACL table (other than anonymous and default) without a member entry id. Recipient Name = {0}.", memberName);
						this.AddUnknownEntry(memberName, memberId, memberEntryId, memberRights);
					}
					IL_17E:;
				}
			}
			finally
			{
				Util.DisposeIfPresent(disposable);
			}
		}

		// Token: 0x06004AB6 RID: 19126 RVA: 0x00138B80 File Offset: 0x00136D80
		private void AddNonADEntry(MapiAclTableAdapter mapiAclTableAdapter, ref ExternalUserCollection externalUsers, string memberName, long memberId, byte[] memberEntryId, MemberRights rights)
		{
			ExternalUser externalUser = mapiAclTableAdapter.TryGetExternalUser(memberEntryId, ref externalUsers);
			if (externalUser != null)
			{
				PermissionSecurityPrincipal securityPrincipal = new PermissionSecurityPrincipal(externalUser);
				Permission permission = this.permissionSet.CreatePermission(securityPrincipal, rights, memberId);
				this.AddPermissionEntry(securityPrincipal, permission);
				return;
			}
			ExTraceGlobals.StorageTracer.TraceDebug<string>(0L, "PermissionTable::PermissionTable. Member has invalid entry id, member name = {0}.", memberName);
			this.AddUnknownEntry(memberName, memberId, memberEntryId, rights);
		}

		// Token: 0x06004AB7 RID: 19127 RVA: 0x00138BDC File Offset: 0x00136DDC
		private void AddUnknownEntry(string memberName, long memberId, byte[] memberEntryId, MemberRights rights)
		{
			PermissionSecurityPrincipal securityPrincipal = new PermissionSecurityPrincipal(memberName, memberEntryId, memberId);
			Permission permission = this.permissionSet.CreatePermission(securityPrincipal, rights, memberId);
			this.AddPermissionEntry(securityPrincipal, permission);
		}

		// Token: 0x06004AB8 RID: 19128 RVA: 0x00138C0C File Offset: 0x00136E0C
		private void AddPermissionEntriesForRemove(MapiAclTableAdapter mapiAclTableAdapter)
		{
			foreach (Permission permission in this.removedPermissions.Values)
			{
				mapiAclTableAdapter.RemovePermissionEntry(permission.MemberId);
			}
		}

		// Token: 0x06004AB9 RID: 19129 RVA: 0x00138C6C File Offset: 0x00136E6C
		private void AddPermissionEntriesForAddOrModify(MapiAclTableAdapter mapiAclTableAdapter)
		{
			foreach (Permission permission in this.permissions.Values)
			{
				byte[] array = null;
				if (permission.Origin == PermissionOrigin.New)
				{
					if (permission.Principal.Type == PermissionSecurityPrincipal.SecurityPrincipalType.ADRecipientPrincipal)
					{
						ParticipantEntryId participantEntryId = ParticipantEntryId.FromParticipant(new Participant(permission.Principal.ADRecipient), ParticipantEntryIdConsumer.SupportsADParticipantEntryId);
						array = participantEntryId.ToByteArray();
					}
					else if (permission.Principal.Type == PermissionSecurityPrincipal.SecurityPrincipalType.ExternalUserPrincipal)
					{
						ExternalUser externalUser = permission.Principal.ExternalUser;
						byte[] array2 = new byte[externalUser.Sid.BinaryLength];
						externalUser.Sid.GetBinaryForm(array2, 0);
						array = MapiStore.GetAddressBookEntryIdFromLocalDirectorySID(array2);
					}
					if (array != null)
					{
						mapiAclTableAdapter.AddPermissionEntry(array, permission.MemberRights);
					}
				}
				else if (permission.IsDirty)
				{
					mapiAclTableAdapter.ModifyPermissionEntry(permission.MemberId, permission.MemberRights);
				}
			}
			if (this.anonymousMemberPermission != null && this.anonymousMemberPermission.IsDirty)
			{
				mapiAclTableAdapter.ModifyPermissionEntry(this.anonymousMemberPermission.MemberId, this.anonymousMemberPermission.MemberRights);
			}
			if (this.defaultMemberPermission != null && this.defaultMemberPermission.IsDirty)
			{
				mapiAclTableAdapter.ModifyPermissionEntry(this.defaultMemberPermission.MemberId, this.defaultMemberPermission.MemberRights);
			}
		}

		// Token: 0x06004ABA RID: 19130 RVA: 0x00138DD0 File Offset: 0x00136FD0
		private void EnforceRestriction(CoreFolder coreFolder)
		{
			new MapiAclTableRestriction(coreFolder).Enforce(new Func<ICollection<MapiAclTableRestriction.ExternalUserPermission>>(this.GetExternalUserPermissions));
		}

		// Token: 0x06004ABB RID: 19131 RVA: 0x00138DEC File Offset: 0x00136FEC
		private ICollection<MapiAclTableRestriction.ExternalUserPermission> GetExternalUserPermissions()
		{
			List<MapiAclTableRestriction.ExternalUserPermission> list = null;
			foreach (Permission permission in this.permissions.Values)
			{
				if (permission.Principal.Type == PermissionSecurityPrincipal.SecurityPrincipalType.ExternalUserPrincipal)
				{
					if (permission.Origin == PermissionOrigin.New || permission.IsDirty)
					{
						if (list == null)
						{
							list = new List<MapiAclTableRestriction.ExternalUserPermission>(this.permissions.Count);
						}
						list.Add(new MapiAclTableRestriction.ExternalUserPermission(permission.Principal, permission.MemberRights));
					}
					else
					{
						ExTraceGlobals.StorageTracer.TraceDebug<PermissionSecurityPrincipal, MemberRights>((long)this.GetHashCode(), "PermissionTable::GetExternalUserPermissions. Skipped policy checking on unchanged external permission: Principal={0}, MemberRights={1}.", permission.Principal, permission.MemberRights);
					}
				}
			}
			return list;
		}

		// Token: 0x06004ABC RID: 19132 RVA: 0x00138EB0 File Offset: 0x001370B0
		private void SaveSharingPartnership(MailboxSession mailboxSession)
		{
			if (mailboxSession == null)
			{
				return;
			}
			IRecipientSession recipientSession = null;
			ADUser aduser = null;
			foreach (Permission permission in this.permissions.Values)
			{
				if (permission.Principal.Type == PermissionSecurityPrincipal.SecurityPrincipalType.ExternalUserPrincipal && !permission.Principal.ExternalUser.IsReachUser)
				{
					if (aduser == null)
					{
						recipientSession = mailboxSession.GetADRecipientSession(false, ConsistencyMode.FullyConsistent);
						ADRecipient adrecipient = recipientSession.Read(mailboxSession.MailboxOwner.ObjectId);
						if (adrecipient == null)
						{
							throw new ObjectNotFoundException(ServerStrings.ADUserNotFound);
						}
						aduser = (adrecipient as ADUser);
						if (aduser == null)
						{
							ExTraceGlobals.StorageTracer.TraceDebug<ADRecipient>((long)this.GetHashCode(), "PermissionTable::SaveSharingParterner. This is not an ADUser so SharingPartnerIdentities doesn't apply. Recipient = {0}.", adrecipient);
							return;
						}
					}
					string externalId = permission.Principal.ExternalUser.ExternalId;
					if (!aduser.SharingPartnerIdentities.Contains(externalId))
					{
						try
						{
							aduser.SharingPartnerIdentities.Add(externalId);
						}
						catch (InvalidOperationException ex)
						{
							ExTraceGlobals.StorageTracer.TraceError<ADUser, InvalidOperationException>((long)this.GetHashCode(), "PermissionTable::SaveSharingParterner. Failed to add SharingPartnerIdentities on user {0} due to exception {1}.", aduser, ex);
							throw new InvalidObjectOperationException(new LocalizedString(ex.Message), ex);
						}
					}
				}
			}
			if (aduser != null && aduser.SharingPartnerIdentities.Changed)
			{
				try
				{
					recipientSession.Save(aduser);
				}
				catch (DataValidationException innerException)
				{
					throw new CorruptDataException(ServerStrings.ExCannotSaveInvalidObject(aduser), innerException);
				}
				catch (DataSourceOperationException ex2)
				{
					throw StorageGlobals.TranslateDirectoryException(ServerStrings.ADException, ex2, null, "PermissionTable::SaveSharingParterner(): Failed due to directory exception {0}.", new object[]
					{
						ex2
					});
				}
				catch (DataSourceTransientException ex3)
				{
					throw StorageGlobals.TranslateDirectoryException(ServerStrings.ADException, ex3, null, "PermissionTable::SaveSharingParterner(): Failed due to directory exception {0}.", new object[]
					{
						ex3
					});
				}
			}
		}

		// Token: 0x06004ABD RID: 19133 RVA: 0x0013908C File Offset: 0x0013728C
		private void AddPermissionEntry(PermissionSecurityPrincipal securityPrincipal, Permission permission)
		{
			if (this.permissions.ContainsKey(securityPrincipal.IndexString))
			{
				throw new CorruptDataException(ServerStrings.SecurityPrincipalAlreadyDefined);
			}
			this.permissions.Add(securityPrincipal.IndexString, permission);
		}

		// Token: 0x04002883 RID: 10371
		internal const long AnonymousMemberId = -1L;

		// Token: 0x04002884 RID: 10372
		internal const long DefaultMemberId = 0L;

		// Token: 0x04002885 RID: 10373
		private bool isPublicFolder;

		// Token: 0x04002886 RID: 10374
		private static bool isCrossPremiseDelegateAllowedForMailboxOwner;

		// Token: 0x04002887 RID: 10375
		private readonly Dictionary<string, Permission> permissions = new Dictionary<string, Permission>();

		// Token: 0x04002888 RID: 10376
		private readonly Dictionary<string, Permission> removedPermissions = new Dictionary<string, Permission>();

		// Token: 0x04002889 RID: 10377
		private readonly PermissionSet permissionSet;

		// Token: 0x0400288A RID: 10378
		private Permission defaultMemberPermission;

		// Token: 0x0400288B RID: 10379
		private Permission anonymousMemberPermission;

		// Token: 0x0400288C RID: 10380
		private bool isInvalid;

		// Token: 0x0400288D RID: 10381
		private static readonly PropertyDefinition[] PropertiesToRead = new PropertyDefinition[]
		{
			InternalSchema.MemberId,
			InternalSchema.MemberEntryId,
			InternalSchema.MemberName,
			InternalSchema.MemberRights
		};
	}
}
