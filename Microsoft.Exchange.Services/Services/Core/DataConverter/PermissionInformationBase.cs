using System;
using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000FF RID: 255
	internal abstract class PermissionInformationBase<PermissionSerializationType> where PermissionSerializationType : BasePermissionType
	{
		// Token: 0x06000705 RID: 1797 RVA: 0x00022C59 File Offset: 0x00020E59
		internal PermissionInformationBase(PermissionSerializationType permissionSerializationObject)
		{
			this.PermissionElement = permissionSerializationObject;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00022C68 File Offset: 0x00020E68
		internal PermissionInformationBase()
		{
			this.PermissionElement = this.CreateDefaultBasePermissionType();
		}

		// Token: 0x06000707 RID: 1799
		protected abstract PermissionSerializationType CreateDefaultBasePermissionType();

		// Token: 0x06000708 RID: 1800
		protected abstract PermissionLevel GetPermissionLevelToSet();

		// Token: 0x06000709 RID: 1801
		internal abstract bool IsNonCustomPermissionLevelSet();

		// Token: 0x0600070A RID: 1802
		protected abstract void SetByTypePermissionFieldsOntoPermission(Permission permission);

		// Token: 0x0600070B RID: 1803 RVA: 0x00022C7C File Offset: 0x00020E7C
		internal virtual bool DoAnyNonPermissionLevelFieldsHaveValue()
		{
			return this.CanReadItems != null || this.CanCreateItems != null || this.CanCreateSubFolders != null || this.IsFolderOwner != null || this.IsFolderVisible != null || this.IsFolderContact != null || this.EditItems != null || this.DeleteItems != null;
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x00022D0D File Offset: 0x00020F0D
		// (set) Token: 0x0600070D RID: 1805 RVA: 0x00022D15 File Offset: 0x00020F15
		internal PermissionSerializationType PermissionElement { get; private set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x00022D1E File Offset: 0x00020F1E
		// (set) Token: 0x0600070F RID: 1807 RVA: 0x00022D26 File Offset: 0x00020F26
		internal Action StoredErrorTrace { get; set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x00022D30 File Offset: 0x00020F30
		// (set) Token: 0x06000711 RID: 1809 RVA: 0x00022D58 File Offset: 0x00020F58
		internal DistinguishedUserType DistinguishedUserType
		{
			get
			{
				PermissionSerializationType permissionElement = this.PermissionElement;
				return permissionElement.UserId.DistinguishedUser;
			}
			set
			{
				PermissionSerializationType permissionElement = this.PermissionElement;
				permissionElement.UserId.DistinguishedUser = value;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x00022D7F File Offset: 0x00020F7F
		// (set) Token: 0x06000713 RID: 1811 RVA: 0x00022D9B File Offset: 0x00020F9B
		internal ADRecipient Recipient
		{
			get
			{
				if (this.recipient == null)
				{
					this.recipient = this.CreateADRecipientFromSerializationClass();
				}
				return this.recipient;
			}
			set
			{
				this.recipient = value;
				this.UpdateSerializationClassFromADRecipient(this.recipient);
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x00022DB0 File Offset: 0x00020FB0
		// (set) Token: 0x06000715 RID: 1813 RVA: 0x00022DD8 File Offset: 0x00020FD8
		internal string ExternalUserIdentity
		{
			get
			{
				PermissionSerializationType permissionElement = this.PermissionElement;
				return permissionElement.UserId.ExternalUserIdentity;
			}
			set
			{
				PermissionSerializationType permissionElement = this.PermissionElement;
				permissionElement.UserId.ExternalUserIdentity = value;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000716 RID: 1814
		// (set) Token: 0x06000717 RID: 1815
		internal abstract bool? CanReadItems { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x00022E00 File Offset: 0x00021000
		// (set) Token: 0x06000719 RID: 1817 RVA: 0x00022E4C File Offset: 0x0002104C
		internal bool? CanCreateItems
		{
			get
			{
				this.EnsurePermissionElementIsNotNull();
				PermissionSerializationType permissionElement = this.PermissionElement;
				if (permissionElement.CanCreateItemsSpecified)
				{
					PermissionSerializationType permissionElement2 = this.PermissionElement;
					return new bool?(permissionElement2.CanCreateItems);
				}
				return null;
			}
			set
			{
				this.EnsurePermissionElementIsNotNull();
				if (value != null)
				{
					PermissionSerializationType permissionElement = this.PermissionElement;
					permissionElement.CanCreateItems = value.Value;
					PermissionSerializationType permissionElement2 = this.PermissionElement;
					permissionElement2.CanCreateItemsSpecified = true;
					return;
				}
				PermissionSerializationType permissionElement3 = this.PermissionElement;
				permissionElement3.CanCreateItemsSpecified = false;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x00022EB0 File Offset: 0x000210B0
		// (set) Token: 0x0600071B RID: 1819 RVA: 0x00022EFC File Offset: 0x000210FC
		internal bool? CanCreateSubFolders
		{
			get
			{
				this.EnsurePermissionElementIsNotNull();
				PermissionSerializationType permissionElement = this.PermissionElement;
				if (permissionElement.CanCreateSubFoldersSpecified)
				{
					PermissionSerializationType permissionElement2 = this.PermissionElement;
					return new bool?(permissionElement2.CanCreateSubFolders);
				}
				return null;
			}
			set
			{
				this.EnsurePermissionElementIsNotNull();
				if (value != null)
				{
					PermissionSerializationType permissionElement = this.PermissionElement;
					permissionElement.CanCreateSubFolders = value.Value;
					PermissionSerializationType permissionElement2 = this.PermissionElement;
					permissionElement2.CanCreateSubFoldersSpecified = true;
					return;
				}
				PermissionSerializationType permissionElement3 = this.PermissionElement;
				permissionElement3.CanCreateSubFoldersSpecified = false;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x00022F60 File Offset: 0x00021160
		// (set) Token: 0x0600071D RID: 1821 RVA: 0x00022FAC File Offset: 0x000211AC
		internal bool? IsFolderOwner
		{
			get
			{
				this.EnsurePermissionElementIsNotNull();
				PermissionSerializationType permissionElement = this.PermissionElement;
				if (permissionElement.IsFolderOwnerSpecified)
				{
					PermissionSerializationType permissionElement2 = this.PermissionElement;
					return new bool?(permissionElement2.IsFolderOwner);
				}
				return null;
			}
			set
			{
				this.EnsurePermissionElementIsNotNull();
				if (value != null)
				{
					PermissionSerializationType permissionElement = this.PermissionElement;
					permissionElement.IsFolderOwner = value.Value;
					PermissionSerializationType permissionElement2 = this.PermissionElement;
					permissionElement2.IsFolderOwnerSpecified = true;
					return;
				}
				PermissionSerializationType permissionElement3 = this.PermissionElement;
				permissionElement3.IsFolderOwnerSpecified = false;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x00023010 File Offset: 0x00021210
		// (set) Token: 0x0600071F RID: 1823 RVA: 0x0002305C File Offset: 0x0002125C
		internal bool? IsFolderVisible
		{
			get
			{
				this.EnsurePermissionElementIsNotNull();
				PermissionSerializationType permissionElement = this.PermissionElement;
				if (permissionElement.IsFolderVisibleSpecified)
				{
					PermissionSerializationType permissionElement2 = this.PermissionElement;
					return new bool?(permissionElement2.IsFolderVisible);
				}
				return null;
			}
			set
			{
				this.EnsurePermissionElementIsNotNull();
				if (value != null)
				{
					PermissionSerializationType permissionElement = this.PermissionElement;
					permissionElement.IsFolderVisible = value.Value;
					PermissionSerializationType permissionElement2 = this.PermissionElement;
					permissionElement2.IsFolderVisibleSpecified = true;
					return;
				}
				PermissionSerializationType permissionElement3 = this.PermissionElement;
				permissionElement3.IsFolderVisibleSpecified = false;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x000230C0 File Offset: 0x000212C0
		// (set) Token: 0x06000721 RID: 1825 RVA: 0x0002310C File Offset: 0x0002130C
		internal bool? IsFolderContact
		{
			get
			{
				this.EnsurePermissionElementIsNotNull();
				PermissionSerializationType permissionElement = this.PermissionElement;
				if (permissionElement.IsFolderContactSpecified)
				{
					PermissionSerializationType permissionElement2 = this.PermissionElement;
					return new bool?(permissionElement2.IsFolderContact);
				}
				return null;
			}
			set
			{
				this.EnsurePermissionElementIsNotNull();
				if (value != null)
				{
					PermissionSerializationType permissionElement = this.PermissionElement;
					permissionElement.IsFolderContact = value.Value;
					PermissionSerializationType permissionElement2 = this.PermissionElement;
					permissionElement2.IsFolderContactSpecified = true;
					return;
				}
				PermissionSerializationType permissionElement3 = this.PermissionElement;
				permissionElement3.IsFolderContactSpecified = false;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x00023170 File Offset: 0x00021370
		// (set) Token: 0x06000723 RID: 1827 RVA: 0x000231C0 File Offset: 0x000213C0
		internal ItemPermissionScope? EditItems
		{
			get
			{
				this.EnsurePermissionElementIsNotNull();
				PermissionSerializationType permissionElement = this.PermissionElement;
				if (permissionElement.EditItemsSpecified)
				{
					PermissionSerializationType permissionElement2 = this.PermissionElement;
					return this.ConvertToItemPermissionScope(permissionElement2.EditItems);
				}
				return null;
			}
			set
			{
				this.EnsurePermissionElementIsNotNull();
				if (value != null)
				{
					PermissionSerializationType permissionElement = this.PermissionElement;
					permissionElement.EditItems = this.ConvertToItemPermissionActionType(value.Value).Value;
					PermissionSerializationType permissionElement2 = this.PermissionElement;
					permissionElement2.EditItemsSpecified = true;
					return;
				}
				PermissionSerializationType permissionElement3 = this.PermissionElement;
				permissionElement3.EditItemsSpecified = false;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x00023230 File Offset: 0x00021430
		// (set) Token: 0x06000725 RID: 1829 RVA: 0x00023280 File Offset: 0x00021480
		internal ItemPermissionScope? DeleteItems
		{
			get
			{
				this.EnsurePermissionElementIsNotNull();
				PermissionSerializationType permissionElement = this.PermissionElement;
				if (permissionElement.DeleteItemsSpecified)
				{
					PermissionSerializationType permissionElement2 = this.PermissionElement;
					return this.ConvertToItemPermissionScope(permissionElement2.DeleteItems);
				}
				return null;
			}
			set
			{
				this.EnsurePermissionElementIsNotNull();
				if (value != null)
				{
					PermissionSerializationType permissionElement = this.PermissionElement;
					permissionElement.DeleteItems = this.ConvertToItemPermissionActionType(value.Value).Value;
					PermissionSerializationType permissionElement2 = this.PermissionElement;
					permissionElement2.DeleteItemsSpecified = true;
					return;
				}
				PermissionSerializationType permissionElement3 = this.PermissionElement;
				permissionElement3.DeleteItemsSpecified = false;
			}
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x000232F0 File Offset: 0x000214F0
		private void SetCommonPermissionFieldsOntoPermission(Permission permission)
		{
			if (this.CanReadItems != null)
			{
				permission.CanReadItems = this.CanReadItems.Value;
			}
			if (this.CanCreateItems != null)
			{
				permission.CanCreateItems = this.CanCreateItems.Value;
			}
			if (this.CanCreateSubFolders != null)
			{
				permission.CanCreateSubfolders = this.CanCreateSubFolders.Value;
			}
			if (this.IsFolderOwner != null)
			{
				permission.IsFolderOwner = this.IsFolderOwner.Value;
			}
			if (this.IsFolderVisible != null)
			{
				permission.IsFolderVisible = this.IsFolderVisible.Value;
			}
			if (this.IsFolderContact != null)
			{
				permission.IsFolderContact = this.IsFolderContact.Value;
			}
			if (this.EditItems != null)
			{
				permission.EditItems = this.EditItems.Value;
			}
			if (this.DeleteItems != null)
			{
				permission.DeleteItems = this.DeleteItems.Value;
			}
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0002342C File Offset: 0x0002162C
		internal void SetPermissionIntoPermissionSet(PermissionSet permissionSet, ExternalUserCollection externalUserCollection)
		{
			Permission permission = null;
			PermissionLevel permissionLevel = this.GetPermissionLevelToSet();
			MemberRights? memberRights = null;
			if (permissionLevel == PermissionLevel.Custom)
			{
				permissionLevel = PermissionLevel.None;
			}
			if (this.DistinguishedUserType != DistinguishedUserType.None)
			{
				if (this.DistinguishedUserType == DistinguishedUserType.Anonymous)
				{
					permission = permissionSet.AnonymousPermission;
				}
				else
				{
					permission = permissionSet.DefaultPermission;
				}
				if (permission == null)
				{
					string arg;
					if (this.DistinguishedUserType == DistinguishedUserType.Anonymous)
					{
						arg = "Anonymous";
					}
					else
					{
						arg = "Default";
					}
					ExTraceGlobals.FolderAlgorithmTracer.TraceDebug<string>(0L, "[PermissionInformationBase::SetPermissionIntoPermissionSet] Distinguished user permission {0} is null, throwing InvalidUserInfoException", arg);
					throw new InvalidUserInfoException(this.PermissionElement);
				}
				permission.PermissionLevel = permissionLevel;
			}
			else
			{
				if (this.Recipient != null)
				{
					try
					{
						PermissionSecurityPrincipal securityPrincipal = new PermissionSecurityPrincipal(this.Recipient);
						permission = permissionSet.GetEntry(securityPrincipal);
						if (permission != null)
						{
							memberRights = new MemberRights?(permission.MemberRights);
							permission.MemberRights = MemberRights.None;
							permission.PermissionLevel = permissionLevel;
						}
						else
						{
							if (this.StoredErrorTrace != null)
							{
								this.StoredErrorTrace();
								throw new InvalidUserInfoException(this.PermissionElement);
							}
							permission = permissionSet.AddEntry(new PermissionSecurityPrincipal(this.Recipient), permissionLevel);
						}
						goto IL_1B0;
					}
					catch (ArgumentException arg2)
					{
						ExTraceGlobals.FolderAlgorithmTracer.TraceDebug<ArgumentException>(0L, "[PermissionInformationBase::SetPermissionIntoPermissionSet] Our permission is not valid per XSO - exception {0}", arg2);
						throw new InvalidUserInfoException(this.PermissionElement);
					}
				}
				if (this.ExternalUserIdentity != null)
				{
					if (externalUserCollection == null)
					{
						ExTraceGlobals.FolderAlgorithmTracer.TraceDebug(0L, "[PermissionInformationBase::SetPermissionIntoPermissionSet] ExternalUserCollection is null, throwing InvalidUserInfoException");
						throw new InvalidUserInfoException(this.PermissionElement);
					}
					SmtpAddress smtpAddress = new SmtpAddress(this.ExternalUserIdentity);
					ExternalUser externalUser = externalUserCollection.FindExternalUser(smtpAddress);
					if (externalUser == null)
					{
						externalUser = externalUserCollection.AddFederatedUser(smtpAddress);
					}
					try
					{
						permission = permissionSet.AddEntry(new PermissionSecurityPrincipal(externalUser), permissionLevel);
					}
					catch (ArgumentException arg3)
					{
						ExTraceGlobals.FolderAlgorithmTracer.TraceDebug<ArgumentException>(0L, "[PermissionInformationBase::SetPermissionIntoPermissionSet] Our permission is not valid per XSO - exception {0}", arg3);
						throw new InvalidUserInfoException(this.PermissionElement);
					}
				}
			}
			IL_1B0:
			this.SetCommonPermissionFieldsOntoPermission(permission);
			this.SetByTypePermissionFieldsOntoPermission(permission);
			if (memberRights != null && permission.MemberRights != memberRights.Value && this.StoredErrorTrace != null)
			{
				this.StoredErrorTrace();
				throw new InvalidUserInfoException(this.PermissionElement);
			}
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00023708 File Offset: 0x00021908
		private ADRecipient CreateADRecipientFromSerializationClass()
		{
			ADRecipient recipient = null;
			IRecipientSession adRecipientSession = CallContext.Current.ADRecipientSessionContext.GetADRecipientSession();
			PermissionSerializationType permissionElement = this.PermissionElement;
			if (!string.IsNullOrEmpty(permissionElement.UserId.Sid))
			{
				try
				{
					RequestDetailsLogger.Current.TrackLatency(ServiceLatencyMetadata.RecipientLookupLatency, delegate()
					{
						PermissionSerializationType permissionElement7 = this.PermissionElement;
						if (!Directory.TryFindRecipient(new SecurityIdentifier(permissionElement7.UserId.Sid), adRecipientSession, out recipient))
						{
							throw new InvalidUserInfoException(this.PermissionElement);
						}
					});
				}
				catch (ArgumentException innerException)
				{
					throw new InvalidUserInfoException(this.PermissionElement, innerException);
				}
				PermissionSerializationType permissionElement2 = this.PermissionElement;
				if (!string.IsNullOrEmpty(permissionElement2.UserId.PrimarySmtpAddress))
				{
					string a = recipient.PrimarySmtpAddress.ToString();
					PermissionSerializationType permissionElement3 = this.PermissionElement;
					if (!string.Equals(a, permissionElement3.UserId.PrimarySmtpAddress, StringComparison.OrdinalIgnoreCase))
					{
						throw new InvalidUserInfoException(this.PermissionElement);
					}
				}
			}
			else
			{
				PermissionSerializationType permissionElement4 = this.PermissionElement;
				if (!string.IsNullOrEmpty(permissionElement4.UserId.PrimarySmtpAddress))
				{
					try
					{
						RequestDetailsLogger.Current.TrackLatency(ServiceLatencyMetadata.RecipientLookupLatency, delegate()
						{
							PermissionSerializationType permissionElement7 = this.PermissionElement;
							if (!Directory.TryFindRecipient(permissionElement7.UserId.PrimarySmtpAddress, adRecipientSession, out recipient))
							{
								throw new InvalidUserInfoException(this.PermissionElement);
							}
						});
					}
					catch (ArgumentException innerException2)
					{
						throw new InvalidUserInfoException(this.PermissionElement, innerException2);
					}
				}
			}
			PermissionSerializationType permissionElement5 = this.PermissionElement;
			if (!string.IsNullOrEmpty(permissionElement5.UserId.DisplayName))
			{
				string displayName = recipient.DisplayName;
				PermissionSerializationType permissionElement6 = this.PermissionElement;
				if (!string.Equals(displayName, permissionElement6.UserId.DisplayName, StringComparison.OrdinalIgnoreCase))
				{
					throw new InvalidUserInfoException(this.PermissionElement);
				}
			}
			return recipient;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x000238F0 File Offset: 0x00021AF0
		private void UpdateSerializationClassFromADRecipient(ADRecipient recipient)
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
				PermissionSerializationType permissionElement = this.PermissionElement;
				permissionElement.UserId.Sid = aduser.Sid.ToString();
			}
			else if (adgroup != null)
			{
				PermissionSerializationType permissionElement2 = this.PermissionElement;
				permissionElement2.UserId.Sid = adgroup.Sid.ToString();
			}
			PermissionSerializationType permissionElement3 = this.PermissionElement;
			permissionElement3.UserId.PrimarySmtpAddress = recipient.PrimarySmtpAddress.ToString();
			PermissionSerializationType permissionElement4 = this.PermissionElement;
			permissionElement4.UserId.DisplayName = recipient.DisplayName;
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x000239E3 File Offset: 0x00021BE3
		protected void EnsurePermissionElementIsNotNull()
		{
			if (this.PermissionElement == null)
			{
				this.PermissionElement = this.CreateDefaultBasePermissionType();
			}
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x000239FE File Offset: 0x00021BFE
		private PermissionActionType? ConvertToItemPermissionActionType(ItemPermissionScope itemPermissionScope)
		{
			return new PermissionActionType?(PermissionInformationBase<PermissionSerializationType>.permissionTypeDictionary[itemPermissionScope]);
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00023A10 File Offset: 0x00021C10
		private ItemPermissionScope? ConvertToItemPermissionScope(PermissionActionType permissionActionType)
		{
			foreach (KeyValuePair<ItemPermissionScope, PermissionActionType> keyValuePair in PermissionInformationBase<PermissionSerializationType>.permissionTypeDictionary)
			{
				if (keyValuePair.Value == permissionActionType)
				{
					return new ItemPermissionScope?(keyValuePair.Key);
				}
			}
			return null;
		}

		// Token: 0x040006E7 RID: 1767
		private static Dictionary<ItemPermissionScope, PermissionActionType> permissionTypeDictionary = new Dictionary<ItemPermissionScope, PermissionActionType>
		{
			{
				ItemPermissionScope.AllItems,
				PermissionActionType.All
			},
			{
				ItemPermissionScope.None,
				PermissionActionType.None
			},
			{
				ItemPermissionScope.OwnedItems,
				PermissionActionType.Owned
			}
		};

		// Token: 0x040006E8 RID: 1768
		private ADRecipient recipient;
	}
}
