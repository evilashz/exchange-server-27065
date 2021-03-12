using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x0200000C RID: 12
	internal sealed class UnifiedGroupADAccessLayer
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00004A08 File Offset: 0x00002C08
		internal UnifiedGroupADAccessLayer(ADUser groupMailbox, string preferredDC)
		{
			if (groupMailbox.RecipientDisplayType == null || groupMailbox.RecipientDisplayType != RecipientDisplayType.GroupMailboxUser)
			{
				throw new InvalidOperationException("Update membership is only allowed on GroupMailbox. Recipient type:" + groupMailbox.RecipientDisplayType);
			}
			this.groupMailbox = groupMailbox;
			this.preferredDC = preferredDC;
			this.sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.groupMailbox.OrganizationId);
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00004A8C File Offset: 0x00002C8C
		private IRecipientSession ReadWriteADSession
		{
			get
			{
				if (this.readWriteSession == null)
				{
					this.readWriteSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(this.preferredDC, false, ConsistencyMode.IgnoreInvalid, this.sessionSettings, 90, "ReadWriteADSession", "f:\\15.00.1497\\sources\\dev\\UnifiedGroups\\src\\UnifiedGroups\\GroupMailboxAccessLayer\\UnifiedGroupADAccessLayer.cs");
				}
				return this.readWriteSession;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00004AD4 File Offset: 0x00002CD4
		private IRecipientSession ReadOnlyAdSession
		{
			get
			{
				if (this.readOnlySession == null)
				{
					this.readOnlySession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(this.preferredDC, true, ConsistencyMode.IgnoreInvalid, this.sessionSettings, 111, "ReadOnlyAdSession", "f:\\15.00.1497\\sources\\dev\\UnifiedGroups\\src\\UnifiedGroups\\GroupMailboxAccessLayer\\UnifiedGroupADAccessLayer.cs");
				}
				return this.readOnlySession;
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00004B1C File Offset: 0x00002D1C
		public static ADPagedReader<ADRawEntry> GetAllGroupMembers(IRecipientSession recipientSession, ADObjectId groupId, IEnumerable<PropertyDefinition> properties, SortBy sortBy, QueryFilter searchFilter = null, int pageSize = 0)
		{
			QueryFilter queryFilter = QueryFilter.AndTogether(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, IUnifiedGroupMailboxSchema.UnifiedGroupMembersBL, groupId),
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, ADUser.MostDerivedClass)
			});
			if (searchFilter != null)
			{
				queryFilter = QueryFilter.AndTogether(new QueryFilter[]
				{
					queryFilter,
					searchFilter
				});
			}
			return recipientSession.FindPagedADRawEntry(null, QueryScope.SubTree, queryFilter, sortBy, pageSize, properties);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00004B84 File Offset: 0x00002D84
		internal void UpdateMembership(ADUser[] addedMembers, ADUser[] removedMembers)
		{
			if (addedMembers.IsNullOrEmpty<ADUser>() && removedMembers.IsNullOrEmpty<ADUser>())
			{
				return;
			}
			ModifyRequest modifyRequest = new ModifyRequest
			{
				DistinguishedName = this.groupMailbox.Id.DistinguishedName
			};
			modifyRequest.Controls.Add(new PermissiveModifyControl());
			this.PopulateModifyRequest(modifyRequest, IUnifiedGroupMailboxSchema.UnifiedGroupMembersLink.LdapDisplayName, DirectoryAttributeOperation.Delete, removedMembers);
			this.PopulateModifyRequest(modifyRequest, IUnifiedGroupMailboxSchema.UnifiedGroupMembersLink.LdapDisplayName, DirectoryAttributeOperation.Add, addedMembers);
			this.ReadWriteADSession.UnsafeExecuteModificationRequest(modifyRequest, this.groupMailbox.Id);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00004C0E File Offset: 0x00002E0E
		internal IEnumerable<UnifiedGroupParticipant> GetMembers(bool sortByDisplayName = true, int sizeLimit = 100)
		{
			return this.GetMembersInternal(sortByDisplayName, sizeLimit, null);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00004C19 File Offset: 0x00002E19
		internal IEnumerable<UnifiedGroupParticipant> GetMembersByAnrMatch(string anrToMatch, bool sortByDisplayName = true, int sizeLimit = 100)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("anrToMatch", anrToMatch);
			if (anrToMatch.Length > 255)
			{
				anrToMatch = anrToMatch.Substring(0, 255);
			}
			return this.GetMembersInternal(sortByDisplayName, sizeLimit, anrToMatch);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00004F78 File Offset: 0x00003178
		private IEnumerable<UnifiedGroupParticipant> GetMembersInternal(bool sortByDisplayName, int sizeLimit, string anrToMatch = null)
		{
			QueryFilter anrFilter = null;
			if (!string.IsNullOrEmpty(anrToMatch))
			{
				anrToMatch = anrToMatch.Trim();
				if (anrToMatch.Length == 1)
				{
					anrFilter = QueryFilter.OrTogether(new QueryFilter[]
					{
						new TextFilter(ADUserSchema.FirstName, anrToMatch, MatchOptions.Prefix, MatchFlags.IgnoreCase),
						new TextFilter(ADUserSchema.LastName, anrToMatch, MatchOptions.Prefix, MatchFlags.IgnoreCase)
					});
				}
				else
				{
					anrFilter = new AmbiguousNameResolutionFilter(anrToMatch);
				}
			}
			ADPagedReader<ADRawEntry> pagedReader = UnifiedGroupADAccessLayer.GetAllGroupMembers(this.ReadOnlyAdSession, this.groupMailbox.Id, UnifiedGroupParticipant.DefaultMemberProperties, sortByDisplayName ? new SortBy(ADRecipientSchema.DisplayName, SortOrder.Ascending) : null, anrFilter, 0);
			HashSet<Guid> ownerSet = new HashSet<Guid>();
			foreach (ADObjectId adobjectId in this.groupMailbox.Owners)
			{
				ownerSet.Add(adobjectId.ObjectGuid);
			}
			int counter = 0;
			foreach (ADRawEntry item in pagedReader)
			{
				if (sizeLimit > 0)
				{
					counter++;
					if (counter > sizeLimit)
					{
						break;
					}
				}
				yield return this.SetOwnership(UnifiedGroupParticipant.CreateFromADRawEntry(item), ownerSet);
			}
			yield break;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00004FAA File Offset: 0x000031AA
		private UnifiedGroupParticipant SetOwnership(UnifiedGroupParticipant participant, HashSet<Guid> ownerSet)
		{
			if (ownerSet.Contains(participant.Id.ObjectGuid))
			{
				participant.IsOwner = true;
			}
			return participant;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00004FC8 File Offset: 0x000031C8
		private void PopulateModifyRequest(ModifyRequest request, string name, DirectoryAttributeOperation operation, IEnumerable<ADUser> members)
		{
			if (members != null)
			{
				DirectoryAttributeModification directoryAttributeModification = new DirectoryAttributeModification
				{
					Name = name,
					Operation = operation
				};
				foreach (ADUser aduser in members)
				{
					directoryAttributeModification.Add(aduser.Id.ToGuidOrDNString());
				}
				if (directoryAttributeModification.Count > 0)
				{
					request.Modifications.Add(directoryAttributeModification);
				}
			}
		}

		// Token: 0x04000025 RID: 37
		private const int DefaultSizeLimit = 100;

		// Token: 0x04000026 RID: 38
		private const int MaxQueryStringLength = 255;

		// Token: 0x04000027 RID: 39
		private readonly ADUser groupMailbox;

		// Token: 0x04000028 RID: 40
		private readonly string preferredDC;

		// Token: 0x04000029 RID: 41
		private readonly ADSessionSettings sessionSettings;

		// Token: 0x0400002A RID: 42
		private IRecipientSession readWriteSession;

		// Token: 0x0400002B RID: 43
		private IRecipientSession readOnlySession;
	}
}
