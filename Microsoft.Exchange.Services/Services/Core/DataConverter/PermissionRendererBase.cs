using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000101 RID: 257
	internal abstract class PermissionRendererBase<PermissionType, PermissionSetSerializationType, PermissionSerializationType> where PermissionType : Permission where PermissionSetSerializationType : BasePermissionSetType where PermissionSerializationType : BasePermissionType
	{
		// Token: 0x0600073D RID: 1853
		protected abstract string GetPermissionsArrayElementName();

		// Token: 0x0600073E RID: 1854
		protected abstract string GetPermissionElementName();

		// Token: 0x0600073F RID: 1855
		protected abstract PermissionType GetDefaultPermission();

		// Token: 0x06000740 RID: 1856
		protected abstract PermissionType GetAnonymousPermission();

		// Token: 0x06000741 RID: 1857
		protected abstract IEnumerator<PermissionType> GetPermissionEnumerator();

		// Token: 0x06000742 RID: 1858
		protected abstract void RenderByTypePermissionDetails(PermissionSerializationType permissionElement, PermissionType permission);

		// Token: 0x06000743 RID: 1859 RVA: 0x00023D97 File Offset: 0x00021F97
		protected PermissionRendererBase()
		{
			this.serviceProperty = this.CreatePermissionSetElement();
		}

		// Token: 0x06000744 RID: 1860
		protected abstract PermissionSetSerializationType CreatePermissionSetElement();

		// Token: 0x06000745 RID: 1861 RVA: 0x00023DAC File Offset: 0x00021FAC
		private void RenderDefaultPermissionIfSet(List<PermissionSerializationType> permissions)
		{
			PermissionType defaultPermission = this.GetDefaultPermission();
			if (defaultPermission == null)
			{
				return;
			}
			permissions.Add(this.RenderDistinguishedUserPermission(DistinguishedUserType.Default, defaultPermission));
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00023DD8 File Offset: 0x00021FD8
		private void RenderAnonymousPermissionIfSet(List<PermissionSerializationType> permissions)
		{
			PermissionType anonymousPermission = this.GetAnonymousPermission();
			if (anonymousPermission == null)
			{
				return;
			}
			permissions.Add(this.RenderDistinguishedUserPermission(DistinguishedUserType.Anonymous, anonymousPermission));
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00023E04 File Offset: 0x00022004
		private void RenderIteratorPermissions(List<PermissionSerializationType> permissions)
		{
			IEnumerator<PermissionType> permissionEnumerator = this.GetPermissionEnumerator();
			while (permissionEnumerator.MoveNext())
			{
				PermissionType permissionType = permissionEnumerator.Current;
				if (permissionType.Principal.Type == PermissionSecurityPrincipal.SecurityPrincipalType.ADRecipientPrincipal)
				{
					permissions.Add(this.RenderUserPermission(permissionEnumerator.Current));
				}
			}
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00023E50 File Offset: 0x00022050
		private string[] GetUnknownEntries()
		{
			List<string> list = new List<string>(1);
			IEnumerator<PermissionType> permissionEnumerator = this.GetPermissionEnumerator();
			while (permissionEnumerator.MoveNext())
			{
				PermissionType permissionType = permissionEnumerator.Current;
				if (permissionType.Principal.Type == PermissionSecurityPrincipal.SecurityPrincipalType.UnknownPrincipal)
				{
					list.Add(permissionType.Principal.UnknownPrincipalMemberName);
				}
				else if (!ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010) && permissionType.Principal.Type == PermissionSecurityPrincipal.SecurityPrincipalType.ExternalUserPrincipal)
				{
					list.Add(permissionType.Principal.ExternalUser.Name);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00023EF8 File Offset: 0x000220F8
		private void RenderUnknownEntries()
		{
			string[] unknownEntries = this.GetUnknownEntries();
			if (unknownEntries != null && unknownEntries.Length != 0)
			{
				this.SetUnknownEntriesOnSerializationObject(this.serviceProperty, unknownEntries);
			}
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00023F24 File Offset: 0x00022124
		internal PermissionSetSerializationType Render()
		{
			List<PermissionSerializationType> list = new List<PermissionSerializationType>();
			this.RenderDefaultPermissionIfSet(list);
			this.RenderAnonymousPermissionIfSet(list);
			this.RenderIteratorPermissions(list);
			this.RenderExternalPermissions(list);
			this.RenderUnknownEntries();
			this.SetPermissionsOnSerializationObject(this.serviceProperty, list);
			return this.serviceProperty;
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00023F6C File Offset: 0x0002216C
		private void RenderDistinguishedUserId(PermissionSerializationType permissionElement, DistinguishedUserType distinguishedUserType)
		{
			if (distinguishedUserType == DistinguishedUserType.Default)
			{
				permissionElement.UserId.DistinguishedUser = DistinguishedUserType.Default;
				return;
			}
			permissionElement.UserId.DistinguishedUser = DistinguishedUserType.Anonymous;
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x00023F9C File Offset: 0x0002219C
		private void RenderUserId(PermissionSerializationType permissionElement, ADRecipient recipient)
		{
			ADUser aduser = recipient as ADUser;
			ADGroup adgroup = recipient as ADGroup;
			ADContact adcontact = recipient as ADContact;
			if (aduser == null && adgroup == null && adcontact == null)
			{
				ExTraceGlobals.FolderAlgorithmTracer.TraceDebug<SmtpAddress, string, string>(0L, "[PermissionSetProperty::RenderUserId] User with primary SMTP address {0}/display name {1} was not an ADUser, ADGroup, or ADContact - was of type {2}", recipient.PrimarySmtpAddress, recipient.DisplayName, recipient.GetType().FullName);
			}
			if (aduser != null)
			{
				permissionElement.UserId.Sid = aduser.Sid.ToString();
			}
			else if (adgroup != null)
			{
				permissionElement.UserId.Sid = adgroup.Sid.ToString();
			}
			permissionElement.UserId.PrimarySmtpAddress = recipient.PrimarySmtpAddress.ToString();
			permissionElement.UserId.DisplayName = recipient.DisplayName;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00024070 File Offset: 0x00022270
		private PermissionSerializationType RenderDistinguishedUserPermission(DistinguishedUserType distinguishedUserType, PermissionType permission)
		{
			PermissionSerializationType permissionSerializationType = this.CreatePermissionElement();
			this.RenderDistinguishedUserId(permissionSerializationType, distinguishedUserType);
			this.RenderBasePermissionDetails(permissionSerializationType, permission);
			this.RenderByTypePermissionDetails(permissionSerializationType, permission);
			return permissionSerializationType;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x000240A0 File Offset: 0x000222A0
		private PermissionSerializationType RenderUserPermission(PermissionType permission)
		{
			PermissionSerializationType permissionSerializationType = this.CreatePermissionElement();
			this.RenderUserId(permissionSerializationType, permission.Principal.ADRecipient);
			this.RenderBasePermissionDetails(permissionSerializationType, permission);
			this.RenderByTypePermissionDetails(permissionSerializationType, permission);
			return permissionSerializationType;
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x000240DE File Offset: 0x000222DE
		private PermissionActionType CreatePermissionAction(ItemPermissionScope permissionScope)
		{
			if (ItemPermissionScope.AllItems == permissionScope)
			{
				return PermissionActionType.All;
			}
			if (ItemPermissionScope.OwnedItems == permissionScope)
			{
				return PermissionActionType.Owned;
			}
			return PermissionActionType.None;
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x000240F0 File Offset: 0x000222F0
		private void RenderBasePermissionDetails(PermissionSerializationType permissionElement, PermissionType permission)
		{
			permissionElement.CanCreateItems = permission.CanCreateItems;
			permissionElement.CanCreateItemsSpecified = true;
			permissionElement.CanCreateSubFolders = permission.CanCreateSubfolders;
			permissionElement.CanCreateSubFoldersSpecified = true;
			permissionElement.IsFolderOwner = permission.IsFolderOwner;
			permissionElement.IsFolderOwnerSpecified = true;
			permissionElement.IsFolderVisible = permission.IsFolderVisible;
			permissionElement.IsFolderVisibleSpecified = true;
			permissionElement.IsFolderContact = permission.IsFolderContact;
			permissionElement.IsFolderContactSpecified = true;
			permissionElement.EditItems = this.CreatePermissionAction(permission.EditItems);
			permissionElement.EditItemsSpecified = true;
			permissionElement.DeleteItems = this.CreatePermissionAction(permission.DeleteItems);
			permissionElement.DeleteItemsSpecified = true;
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00024224 File Offset: 0x00022424
		private void RenderExternalPermissions(List<PermissionSerializationType> permissions)
		{
			if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010))
			{
				IEnumerator<PermissionType> permissionEnumerator = this.GetPermissionEnumerator();
				while (permissionEnumerator.MoveNext())
				{
					PermissionType permissionType = permissionEnumerator.Current;
					if (permissionType.Principal.Type == PermissionSecurityPrincipal.SecurityPrincipalType.ExternalUserPrincipal)
					{
						permissions.Add(this.RenderExternalUserPermission(permissionEnumerator.Current));
					}
				}
			}
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00024284 File Offset: 0x00022484
		protected PermissionSerializationType RenderExternalUserPermission(PermissionType permission)
		{
			PermissionSerializationType permissionSerializationType = this.CreatePermissionElement();
			this.RenderExternalUserId(permissionSerializationType, permission.Principal.ExternalUser.SmtpAddress.ToString());
			this.RenderBasePermissionDetails(permissionSerializationType, permission);
			this.RenderByTypePermissionDetails(permissionSerializationType, permission);
			return permissionSerializationType;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x000242D5 File Offset: 0x000224D5
		private void RenderExternalUserId(PermissionSerializationType permissionElement, string externalUserIdentity)
		{
			permissionElement.UserId.ExternalUserIdentity = externalUserIdentity;
		}

		// Token: 0x06000754 RID: 1876
		protected abstract PermissionSerializationType CreatePermissionElement();

		// Token: 0x06000755 RID: 1877
		protected abstract void SetPermissionsOnSerializationObject(PermissionSetSerializationType serviceProperty, List<PermissionSerializationType> renderedPermissions);

		// Token: 0x06000756 RID: 1878
		protected abstract void SetUnknownEntriesOnSerializationObject(PermissionSetSerializationType serviceProperty, string[] entries);

		// Token: 0x040006ED RID: 1773
		private PermissionSetSerializationType serviceProperty;
	}
}
