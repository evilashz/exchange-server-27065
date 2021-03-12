using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000337 RID: 823
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class AddressBookBase : ADLegacyVersionableObject, ISupportRecipientFilter, IProvisioningCacheInvalidation
	{
		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x0600261B RID: 9755 RVA: 0x000A1EDB File Offset: 0x000A00DB
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "addressBookContainer";
			}
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x0600261C RID: 9756 RVA: 0x000A1EE2 File Offset: 0x000A00E2
		internal override ADObjectSchema Schema
		{
			get
			{
				return AddressBookBase.schema;
			}
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x0600261D RID: 9757 RVA: 0x000A1EE9 File Offset: 0x000A00E9
		internal override bool ExchangeVersionUpgradeSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600261F RID: 9759 RVA: 0x000A1EF4 File Offset: 0x000A00F4
		internal static object Base64GuidGetter(IPropertyBag propertyBag)
		{
			ADObjectId adobjectId = (ADObjectId)propertyBag[ADObjectSchema.Id];
			if (adobjectId == null)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty(AddressBookBaseSchema.Base64Guid.Name, DirectoryStrings.IdIsNotSet), AddressBookBaseSchema.Base64Guid, propertyBag[ADObjectSchema.Id]));
			}
			return Convert.ToBase64String(adobjectId.ObjectGuid.ToByteArray());
		}

		// Token: 0x06002620 RID: 9760 RVA: 0x000A1F5C File Offset: 0x000A015C
		internal static object ContainerGetter(IPropertyBag propertyBag)
		{
			object result;
			try
			{
				ADObjectId adobjectId = (ADObjectId)propertyBag[ADObjectSchema.Id];
				if (adobjectId == null)
				{
					throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty(AddressBookBaseSchema.Container.Name, DirectoryStrings.IdIsNotSet), AddressBookBaseSchema.Container, propertyBag[ADObjectSchema.Id]));
				}
				ADObjectId adobjectId2 = (ADObjectId)propertyBag[ADObjectSchema.ConfigurationUnit];
				ADObjectId adobjectId3;
				if (adobjectId2 == null)
				{
					adobjectId3 = adobjectId.DescendantDN(6);
					if (adobjectId3.DistinguishedName.Length == adobjectId.DistinguishedName.Length)
					{
						return string.Empty;
					}
				}
				else
				{
					adobjectId3 = adobjectId.DescendantDN(3);
					if (adobjectId3.DistinguishedName.Length != adobjectId2.DistinguishedName.Length)
					{
						adobjectId3 = adobjectId.DescendantDN(6);
					}
				}
				StringBuilder stringBuilder = new StringBuilder("\\");
				for (ADObjectId parent = adobjectId.Parent; parent != adobjectId3; parent = parent.Parent)
				{
					stringBuilder.Insert(0, "\\");
					stringBuilder.Insert(1, parent.Rdn.UnescapedName.Replace("\\", "\\\\"));
				}
				if (stringBuilder.Length > 1)
				{
					stringBuilder.Remove(stringBuilder.Length - 1, 1);
				}
				result = stringBuilder.ToString();
			}
			catch (InvalidOperationException ex)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("Container", ex.Message), AddressBookBaseSchema.Container, propertyBag[ADObjectSchema.Id]), ex);
			}
			return result;
		}

		// Token: 0x06002621 RID: 9761 RVA: 0x000A20E0 File Offset: 0x000A02E0
		internal static object PathGetter(IPropertyBag propertyBag)
		{
			ADObjectId adobjectId = (ADObjectId)propertyBag[ADObjectSchema.Id];
			if (adobjectId == null)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty(AddressBookBaseSchema.Path.Name, DirectoryStrings.IdIsNotSet), AddressBookBaseSchema.Path, propertyBag[ADObjectSchema.Id]));
			}
			string text = (string)AddressBookBase.ContainerGetter(propertyBag);
			if (string.IsNullOrEmpty(text))
			{
				return "\\";
			}
			string text2 = adobjectId.Rdn.UnescapedName.Replace("\\", "\\\\");
			if (text.EndsWith("\\"))
			{
				return text + text2;
			}
			return text + "\\" + text2;
		}

		// Token: 0x06002622 RID: 9762 RVA: 0x000A218C File Offset: 0x000A038C
		internal static object IsTopContainerGetter(IPropertyBag propertyBag)
		{
			object result;
			try
			{
				ADObjectId adobjectId = (ADObjectId)propertyBag[ADObjectSchema.Id];
				if (adobjectId == null)
				{
					throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty(AddressBookBaseSchema.IsTopContainer.Name, DirectoryStrings.IdIsNotSet), AddressBookBaseSchema.IsTopContainer, propertyBag[ADObjectSchema.Id]));
				}
				ADObjectId adobjectId2 = (ADObjectId)propertyBag[ADObjectSchema.ConfigurationUnit];
				if (adobjectId2 == null)
				{
					result = (adobjectId.DistinguishedName.Length == adobjectId.DescendantDN(6).DistinguishedName.Length);
				}
				else
				{
					result = (adobjectId.DistinguishedName.Equals(adobjectId2.GetDescendantId(GlobalAddressList.RdnGalContainerToOrganization).DistinguishedName, StringComparison.OrdinalIgnoreCase) || adobjectId.DistinguishedName.Equals(adobjectId2.GetDescendantId(AddressList.RdnAlContainerToOrganization).DistinguishedName, StringComparison.OrdinalIgnoreCase) || adobjectId.DistinguishedName.Equals(adobjectId2.GetDescendantId(SystemAddressList.RdnSystemAddressListContainerToOrganization).DistinguishedName, StringComparison.OrdinalIgnoreCase));
				}
			}
			catch (InvalidOperationException ex)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("IsTopContainer", ex.Message), AddressBookBaseSchema.IsTopContainer, propertyBag[ADObjectSchema.Id]), ex);
			}
			return result;
		}

		// Token: 0x06002623 RID: 9763 RVA: 0x000A22C0 File Offset: 0x000A04C0
		private static bool IsStartWithRDN(IPropertyBag propertyBag, ADObjectId rDN, ADPropertyDefinition propertyDefinition)
		{
			bool result;
			try
			{
				ADObjectId adobjectId = (ADObjectId)propertyBag[ADObjectSchema.Id];
				if (adobjectId == null)
				{
					throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty(propertyDefinition.Name, DirectoryStrings.IdIsNotSet), propertyDefinition, propertyBag[ADObjectSchema.Id]));
				}
				ADObjectId adobjectId2 = (ADObjectId)propertyBag[ADObjectSchema.ConfigurationUnit];
				if (adobjectId2 == null)
				{
					result = adobjectId.DescendantDN(6).DistinguishedName.StartsWith(rDN.DistinguishedName, StringComparison.OrdinalIgnoreCase);
				}
				else
				{
					result = adobjectId.DistinguishedName.Substring(0, adobjectId.DistinguishedName.Length - adobjectId2.DistinguishedName.Length - 1).EndsWith(rDN.DistinguishedName, StringComparison.OrdinalIgnoreCase);
				}
			}
			catch (InvalidOperationException ex)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty(propertyDefinition.Name, ex.Message), propertyDefinition, propertyBag[ADObjectSchema.Id]), ex);
			}
			return result;
		}

		// Token: 0x06002624 RID: 9764 RVA: 0x000A23AC File Offset: 0x000A05AC
		internal static object IsGlobalAddressListGetter(IPropertyBag propertyBag)
		{
			return AddressBookBase.IsStartWithRDN(propertyBag, GlobalAddressList.RdnGalContainerToOrganization, AddressBookBaseSchema.IsGlobalAddressList);
		}

		// Token: 0x06002625 RID: 9765 RVA: 0x000A23C3 File Offset: 0x000A05C3
		internal static object IsInSystemAddressListContainerGetter(IPropertyBag propertyBag)
		{
			return AddressBookBase.IsStartWithRDN(propertyBag, SystemAddressList.RdnSystemAddressListContainerToOrganization, AddressBookBaseSchema.IsInSystemAddressListContainer);
		}

		// Token: 0x06002626 RID: 9766 RVA: 0x000A23DC File Offset: 0x000A05DC
		internal static QueryFilter IsDefaultGlobalAddressListFilterBuilder(SinglePropertyFilter filter)
		{
			bool flag = (bool)ADObject.PropertyValueFromEqualityFilter(filter);
			QueryFilter queryFilter = new OrFilter(new QueryFilter[]
			{
				new AndFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ADObjectSchema.ExchangeVersion, ExchangeObjectVersion.Exchange2007),
					new BitMaskAndFilter(AddressBookBaseSchema.RecipientFilterFlags, 2UL)
				}),
				new AndFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ExchangeVersion, ExchangeObjectVersion.Exchange2003),
					new ExistsFilter(AddressBookBaseSchema.LdapRecipientFilter),
					new NotFilter(new ExistsFilter(AddressBookBaseSchema.PurportedSearchUI))
				})
			});
			if (!flag)
			{
				return new NotFilter(queryFilter);
			}
			return queryFilter;
		}

		// Token: 0x06002627 RID: 9767 RVA: 0x000A2488 File Offset: 0x000A0688
		internal static object IsDefaultGlobalAddressListGetter(IPropertyBag propertyBag)
		{
			if (!(bool)AddressBookBase.IsGlobalAddressListGetter(propertyBag))
			{
				return false;
			}
			ExchangeObjectVersion exchangeObjectVersion = (ExchangeObjectVersion)propertyBag[ADObjectSchema.ExchangeVersion];
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[AddressBookBaseSchema.PurportedSearchUI];
			if (exchangeObjectVersion.IsOlderThan(ExchangeObjectVersion.Exchange2007))
			{
				string value = (string)propertyBag[AddressBookBaseSchema.LdapRecipientFilter];
				return !string.IsNullOrEmpty(value) && 0 == multiValuedProperty.Count;
			}
			return RecipientFilterableObjectFlags.IsDefault == (RecipientFilterableObjectFlags.IsDefault & (RecipientFilterableObjectFlags)propertyBag[AddressBookBaseSchema.RecipientFilterFlags]);
		}

		// Token: 0x06002628 RID: 9768 RVA: 0x000A2520 File Offset: 0x000A0720
		private static QueryFilter CreateFindFilter(string searchString, AddressBookBase addressBookBase, AddressBookBase.RecipientCategory recipientCategory)
		{
			QueryFilter queryFilter = new AmbiguousNameResolutionFilter(searchString);
			QueryFilter queryFilter2 = null;
			switch (recipientCategory)
			{
			case AddressBookBase.RecipientCategory.People:
				queryFilter2 = AddressBookBase.CategoryFilters.PersonFilter;
				break;
			case AddressBookBase.RecipientCategory.Groups:
				queryFilter2 = AddressBookBase.CategoryFilters.GroupFilter;
				break;
			}
			QueryFilter queryFilter3;
			if (addressBookBase != null)
			{
				queryFilter3 = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.AddressListMembership, addressBookBase.Id);
			}
			else
			{
				queryFilter3 = new ExistsFilter(ADRecipientSchema.AddressListMembership);
			}
			if (queryFilter2 != null && queryFilter3 != null)
			{
				queryFilter = new AndFilter(new QueryFilter[]
				{
					queryFilter3,
					queryFilter2,
					queryFilter
				});
			}
			else if (queryFilter2 != null && queryFilter3 == null)
			{
				queryFilter = new AndFilter(new QueryFilter[]
				{
					queryFilter2,
					queryFilter
				});
			}
			else if (queryFilter2 == null && queryFilter3 != null)
			{
				queryFilter = new AndFilter(new QueryFilter[]
				{
					queryFilter3,
					queryFilter
				});
			}
			return queryFilter;
		}

		// Token: 0x06002629 RID: 9769 RVA: 0x000A25E8 File Offset: 0x000A07E8
		private static IRecipientSession GetScopedRecipientSession(ADObjectId rootId, int lcid, string preferredServerName, OrganizationId organizationId)
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, rootId, lcid, true, ConsistencyMode.IgnoreInvalid, null, ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), organizationId, null, false), 1181, "GetScopedRecipientSession", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\AddressBookBase.cs");
			if (!string.IsNullOrEmpty(preferredServerName))
			{
				tenantOrRootOrgRecipientSession.DomainController = preferredServerName;
			}
			return tenantOrRootOrgRecipientSession;
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x000A2634 File Offset: 0x000A0834
		private static bool ListObjectModeEnabled()
		{
			bool result;
			lock (AddressBookBase.heuristicsLock)
			{
				if (AddressBookBase.heuristicsNextUpdateTime < DateTime.UtcNow)
				{
					IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 1214, "ListObjectModeEnabled", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\AddressBookBase.cs");
					tenantOrTopologyConfigurationSession.ServerTimeout = new TimeSpan?(TimeSpan.FromSeconds(10.0));
					ADObjectId descendantId = tenantOrTopologyConfigurationSession.ConfigurationNamingContext.GetDescendantId(NtdsService.ContainerId);
					NtdsService ntdsService = tenantOrTopologyConfigurationSession.Read<NtdsService>(descendantId);
					if (ntdsService != null)
					{
						AddressBookBase.listObjectMode = ntdsService.DoListObject;
					}
					else
					{
						AddressBookBase.listObjectMode = false;
					}
					AddressBookBase.heuristicsNextUpdateTime = DateTime.UtcNow + TimeSpan.FromMinutes(15.0);
				}
				result = AddressBookBase.listObjectMode;
			}
			return result;
		}

		// Token: 0x0600262B RID: 9771 RVA: 0x000A2714 File Offset: 0x000A0914
		internal IEnumerable<ADRecipient> FindUpdatingRecipientsPaged(IRecipientSession recipientSession, ADObjectId rootId)
		{
			if (recipientSession == null)
			{
				throw new ArgumentNullException("recipientSession");
			}
			OrganizationId organizationId = base.OrganizationId;
			ADObjectId propertyValue = (base.Guid == Guid.Empty) ? base.Id : new ADObjectId(base.Guid);
			QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.AddressListMembership, propertyValue);
			QueryFilter queryFilter2 = null;
			if (!string.IsNullOrEmpty(this.LdapRecipientFilter))
			{
				queryFilter2 = new CustomLdapFilter(this.LdapRecipientFilter);
			}
			if (queryFilter2 == null)
			{
				return recipientSession.FindPaged(rootId, QueryScope.SubTree, queryFilter, null, 0);
			}
			QueryFilter filter = new OrFilter(new QueryFilter[]
			{
				new AndFilter(new QueryFilter[]
				{
					queryFilter,
					new NotFilter(queryFilter2)
				}),
				new AndFilter(new QueryFilter[]
				{
					new NotFilter(queryFilter),
					queryFilter2
				})
			});
			if (this.RecipientContainer == null || (organizationId.OrganizationalUnit != null && organizationId.OrganizationalUnit.DistinguishedName.Equals(this.RecipientContainer.DistinguishedName, StringComparison.OrdinalIgnoreCase)))
			{
				return recipientSession.FindPaged(rootId, QueryScope.SubTree, filter, null, 0);
			}
			if (rootId == null)
			{
				return this.FindUpdatingRecipientsPaged(recipientSession);
			}
			if (!rootId.DistinguishedName.EndsWith(this.RecipientContainer.DistinguishedName, StringComparison.OrdinalIgnoreCase))
			{
				return recipientSession.FindPaged(rootId, QueryScope.SubTree, queryFilter, null, 0);
			}
			return recipientSession.FindPaged(rootId, QueryScope.SubTree, filter, null, 0);
		}

		// Token: 0x0600262C RID: 9772 RVA: 0x000A2B8C File Offset: 0x000A0D8C
		private IEnumerable<ADRecipient> FindUpdatingRecipientsPaged(IRecipientSession recipientSession)
		{
			OrganizationId organizationId = base.OrganizationId;
			ADObjectId searchId = (base.Guid == Guid.Empty) ? base.Id : new ADObjectId(base.Guid);
			QueryFilter includedFilter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.AddressListMembership, searchId);
			QueryFilter toBeIncludedFilter = new CustomLdapFilter(this.LdapRecipientFilter);
			ADPagedReader<ADRecipient> included = recipientSession.FindPaged(null, QueryScope.SubTree, includedFilter, new SortBy(ADObjectSchema.Guid, SortOrder.Ascending), 0);
			ADPagedReader<ADRecipient> toBeIncluded = recipientSession.FindPaged(this.RecipientContainer, QueryScope.SubTree, toBeIncludedFilter, new SortBy(ADObjectSchema.Guid, SortOrder.Ascending), 0);
			IEnumerator<ADRecipient> includedEnumerator = included.GetEnumerator();
			IEnumerator<ADRecipient> toBeIncludedEnumerator = toBeIncluded.GetEnumerator();
			bool includedAvailable = includedEnumerator.MoveNext();
			bool toBeIncludedAvailable = toBeIncludedEnumerator.MoveNext();
			while (includedAvailable)
			{
				if (!toBeIncludedAvailable)
				{
					break;
				}
				int temp = includedEnumerator.Current.Guid.CompareTo(toBeIncludedEnumerator.Current.Guid);
				if (temp < 0)
				{
					yield return includedEnumerator.Current;
					includedAvailable = includedEnumerator.MoveNext();
				}
				else if (temp > 0)
				{
					yield return toBeIncludedEnumerator.Current;
					toBeIncludedAvailable = toBeIncludedEnumerator.MoveNext();
				}
				else
				{
					includedAvailable = includedEnumerator.MoveNext();
					toBeIncludedAvailable = toBeIncludedEnumerator.MoveNext();
				}
			}
			while (includedAvailable)
			{
				yield return includedEnumerator.Current;
				includedAvailable = includedEnumerator.MoveNext();
			}
			while (toBeIncludedAvailable)
			{
				yield return toBeIncludedEnumerator.Current;
				toBeIncludedAvailable = toBeIncludedEnumerator.MoveNext();
			}
			yield break;
		}

		// Token: 0x0600262D RID: 9773 RVA: 0x000A2BB0 File Offset: 0x000A0DB0
		internal bool CheckForAssociatedAddressBookPolicies()
		{
			QueryFilter filter;
			if (this.IsGlobalAddressList)
			{
				filter = new AndFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.DistinguishedName, base.Id.DistinguishedName),
					new ExistsFilter(AddressBookBaseSchema.AssociatedAddressBookPoliciesForGAL)
				});
			}
			else
			{
				filter = new AndFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.DistinguishedName, base.Id.DistinguishedName),
					new OrFilter(new QueryFilter[]
					{
						new ExistsFilter(AddressBookBaseSchema.AssociatedAddressBookPoliciesForAddressLists),
						new ExistsFilter(AddressBookBaseSchema.AssociatedAddressBookPoliciesForAllRoomList)
					})
				});
			}
			if (base.Session != null)
			{
				AddressBookBase[] array = base.Session.Find<AddressBookBase>(null, QueryScope.SubTree, filter, null, 1);
				return array != null && array.Length > 0;
			}
			return true;
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x0600262E RID: 9774 RVA: 0x000A2C76 File Offset: 0x000A0E76
		// (set) Token: 0x0600262F RID: 9775 RVA: 0x000A2C88 File Offset: 0x000A0E88
		[Parameter(Mandatory = false)]
		public string SimpleDisplayName
		{
			get
			{
				return (string)this[AddressBookBaseSchema.SimpleDisplayName];
			}
			set
			{
				this[AddressBookBaseSchema.SimpleDisplayName] = value;
			}
		}

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06002630 RID: 9776 RVA: 0x000A2C96 File Offset: 0x000A0E96
		// (set) Token: 0x06002631 RID: 9777 RVA: 0x000A2CA8 File Offset: 0x000A0EA8
		[Parameter(Mandatory = false)]
		public string DisplayName
		{
			get
			{
				return (string)this[AddressBookBaseSchema.DisplayName];
			}
			set
			{
				this[AddressBookBaseSchema.DisplayName] = value;
			}
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06002632 RID: 9778 RVA: 0x000A2CB6 File Offset: 0x000A0EB6
		public string RecipientFilter
		{
			get
			{
				return (string)this[AddressBookBaseSchema.RecipientFilter];
			}
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06002633 RID: 9779 RVA: 0x000A2CC8 File Offset: 0x000A0EC8
		public string LastUpdatedRecipientFilter
		{
			get
			{
				return (string)this[AddressBookBaseSchema.LastUpdatedRecipientFilter];
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06002634 RID: 9780 RVA: 0x000A2CDA File Offset: 0x000A0EDA
		public bool RecipientFilterApplied
		{
			get
			{
				return (bool)this[AddressBookBaseSchema.RecipientFilterApplied];
			}
		}

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06002635 RID: 9781 RVA: 0x000A2CEC File Offset: 0x000A0EEC
		public string LdapRecipientFilter
		{
			get
			{
				return (string)this[AddressBookBaseSchema.LdapRecipientFilter];
			}
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06002636 RID: 9782 RVA: 0x000A2CFE File Offset: 0x000A0EFE
		// (set) Token: 0x06002637 RID: 9783 RVA: 0x000A2D10 File Offset: 0x000A0F10
		public WellKnownRecipientType? IncludedRecipients
		{
			get
			{
				return (WellKnownRecipientType?)this[AddressBookBaseSchema.IncludedRecipients];
			}
			internal set
			{
				this[AddressBookBaseSchema.IncludedRecipients] = value;
			}
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06002638 RID: 9784 RVA: 0x000A2D23 File Offset: 0x000A0F23
		// (set) Token: 0x06002639 RID: 9785 RVA: 0x000A2D2B File Offset: 0x000A0F2B
		WellKnownRecipientType? ISupportRecipientFilter.IncludedRecipients
		{
			get
			{
				return this.IncludedRecipients;
			}
			set
			{
				this.IncludedRecipients = value;
			}
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x0600263A RID: 9786 RVA: 0x000A2D34 File Offset: 0x000A0F34
		// (set) Token: 0x0600263B RID: 9787 RVA: 0x000A2D46 File Offset: 0x000A0F46
		public MultiValuedProperty<string> ConditionalDepartment
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressBookBaseSchema.ConditionalDepartment];
			}
			internal set
			{
				this[AddressBookBaseSchema.ConditionalDepartment] = value;
			}
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x0600263C RID: 9788 RVA: 0x000A2D54 File Offset: 0x000A0F54
		// (set) Token: 0x0600263D RID: 9789 RVA: 0x000A2D5C File Offset: 0x000A0F5C
		MultiValuedProperty<string> ISupportRecipientFilter.ConditionalDepartment
		{
			get
			{
				return this.ConditionalDepartment;
			}
			set
			{
				this.ConditionalDepartment = value;
			}
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x0600263E RID: 9790 RVA: 0x000A2D65 File Offset: 0x000A0F65
		// (set) Token: 0x0600263F RID: 9791 RVA: 0x000A2D77 File Offset: 0x000A0F77
		public MultiValuedProperty<string> ConditionalCompany
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressBookBaseSchema.ConditionalCompany];
			}
			internal set
			{
				this[AddressBookBaseSchema.ConditionalCompany] = value;
			}
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06002640 RID: 9792 RVA: 0x000A2D85 File Offset: 0x000A0F85
		// (set) Token: 0x06002641 RID: 9793 RVA: 0x000A2D8D File Offset: 0x000A0F8D
		MultiValuedProperty<string> ISupportRecipientFilter.ConditionalCompany
		{
			get
			{
				return this.ConditionalCompany;
			}
			set
			{
				this.ConditionalCompany = value;
			}
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06002642 RID: 9794 RVA: 0x000A2D96 File Offset: 0x000A0F96
		// (set) Token: 0x06002643 RID: 9795 RVA: 0x000A2DA8 File Offset: 0x000A0FA8
		public MultiValuedProperty<string> ConditionalStateOrProvince
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressBookBaseSchema.ConditionalStateOrProvince];
			}
			internal set
			{
				this[AddressBookBaseSchema.ConditionalStateOrProvince] = value;
			}
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06002644 RID: 9796 RVA: 0x000A2DB6 File Offset: 0x000A0FB6
		// (set) Token: 0x06002645 RID: 9797 RVA: 0x000A2DBE File Offset: 0x000A0FBE
		MultiValuedProperty<string> ISupportRecipientFilter.ConditionalStateOrProvince
		{
			get
			{
				return this.ConditionalStateOrProvince;
			}
			set
			{
				this.ConditionalStateOrProvince = value;
			}
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06002646 RID: 9798 RVA: 0x000A2DC7 File Offset: 0x000A0FC7
		public WellKnownRecipientFilterType RecipientFilterType
		{
			get
			{
				return (WellKnownRecipientFilterType)this[AddressBookBaseSchema.RecipientFilterType];
			}
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06002647 RID: 9799 RVA: 0x000A2DD9 File Offset: 0x000A0FD9
		// (set) Token: 0x06002648 RID: 9800 RVA: 0x000A2DEB File Offset: 0x000A0FEB
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute1
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressBookBaseSchema.ConditionalCustomAttribute1];
			}
			set
			{
				this[AddressBookBaseSchema.ConditionalCustomAttribute1] = value;
			}
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06002649 RID: 9801 RVA: 0x000A2DF9 File Offset: 0x000A0FF9
		// (set) Token: 0x0600264A RID: 9802 RVA: 0x000A2E0B File Offset: 0x000A100B
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute2
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressBookBaseSchema.ConditionalCustomAttribute2];
			}
			set
			{
				this[AddressBookBaseSchema.ConditionalCustomAttribute2] = value;
			}
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x0600264B RID: 9803 RVA: 0x000A2E19 File Offset: 0x000A1019
		// (set) Token: 0x0600264C RID: 9804 RVA: 0x000A2E2B File Offset: 0x000A102B
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute3
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressBookBaseSchema.ConditionalCustomAttribute3];
			}
			set
			{
				this[AddressBookBaseSchema.ConditionalCustomAttribute3] = value;
			}
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x0600264D RID: 9805 RVA: 0x000A2E39 File Offset: 0x000A1039
		// (set) Token: 0x0600264E RID: 9806 RVA: 0x000A2E4B File Offset: 0x000A104B
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute4
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressBookBaseSchema.ConditionalCustomAttribute4];
			}
			set
			{
				this[AddressBookBaseSchema.ConditionalCustomAttribute4] = value;
			}
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x0600264F RID: 9807 RVA: 0x000A2E59 File Offset: 0x000A1059
		// (set) Token: 0x06002650 RID: 9808 RVA: 0x000A2E6B File Offset: 0x000A106B
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute5
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressBookBaseSchema.ConditionalCustomAttribute5];
			}
			set
			{
				this[AddressBookBaseSchema.ConditionalCustomAttribute5] = value;
			}
		}

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06002651 RID: 9809 RVA: 0x000A2E79 File Offset: 0x000A1079
		// (set) Token: 0x06002652 RID: 9810 RVA: 0x000A2E8B File Offset: 0x000A108B
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute6
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressBookBaseSchema.ConditionalCustomAttribute6];
			}
			set
			{
				this[AddressBookBaseSchema.ConditionalCustomAttribute6] = value;
			}
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06002653 RID: 9811 RVA: 0x000A2E99 File Offset: 0x000A1099
		// (set) Token: 0x06002654 RID: 9812 RVA: 0x000A2EAB File Offset: 0x000A10AB
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute7
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressBookBaseSchema.ConditionalCustomAttribute7];
			}
			set
			{
				this[AddressBookBaseSchema.ConditionalCustomAttribute7] = value;
			}
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06002655 RID: 9813 RVA: 0x000A2EB9 File Offset: 0x000A10B9
		// (set) Token: 0x06002656 RID: 9814 RVA: 0x000A2ECB File Offset: 0x000A10CB
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute8
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressBookBaseSchema.ConditionalCustomAttribute8];
			}
			set
			{
				this[AddressBookBaseSchema.ConditionalCustomAttribute8] = value;
			}
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06002657 RID: 9815 RVA: 0x000A2ED9 File Offset: 0x000A10D9
		// (set) Token: 0x06002658 RID: 9816 RVA: 0x000A2EEB File Offset: 0x000A10EB
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute9
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressBookBaseSchema.ConditionalCustomAttribute9];
			}
			set
			{
				this[AddressBookBaseSchema.ConditionalCustomAttribute9] = value;
			}
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06002659 RID: 9817 RVA: 0x000A2EF9 File Offset: 0x000A10F9
		// (set) Token: 0x0600265A RID: 9818 RVA: 0x000A2F0B File Offset: 0x000A110B
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute10
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressBookBaseSchema.ConditionalCustomAttribute10];
			}
			set
			{
				this[AddressBookBaseSchema.ConditionalCustomAttribute10] = value;
			}
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x0600265B RID: 9819 RVA: 0x000A2F19 File Offset: 0x000A1119
		// (set) Token: 0x0600265C RID: 9820 RVA: 0x000A2F2B File Offset: 0x000A112B
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute11
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressBookBaseSchema.ConditionalCustomAttribute11];
			}
			set
			{
				this[AddressBookBaseSchema.ConditionalCustomAttribute11] = value;
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x0600265D RID: 9821 RVA: 0x000A2F39 File Offset: 0x000A1139
		// (set) Token: 0x0600265E RID: 9822 RVA: 0x000A2F4B File Offset: 0x000A114B
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute12
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressBookBaseSchema.ConditionalCustomAttribute12];
			}
			set
			{
				this[AddressBookBaseSchema.ConditionalCustomAttribute12] = value;
			}
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x0600265F RID: 9823 RVA: 0x000A2F59 File Offset: 0x000A1159
		// (set) Token: 0x06002660 RID: 9824 RVA: 0x000A2F6B File Offset: 0x000A116B
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute13
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressBookBaseSchema.ConditionalCustomAttribute13];
			}
			set
			{
				this[AddressBookBaseSchema.ConditionalCustomAttribute13] = value;
			}
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06002661 RID: 9825 RVA: 0x000A2F79 File Offset: 0x000A1179
		// (set) Token: 0x06002662 RID: 9826 RVA: 0x000A2F8B File Offset: 0x000A118B
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute14
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressBookBaseSchema.ConditionalCustomAttribute14];
			}
			set
			{
				this[AddressBookBaseSchema.ConditionalCustomAttribute14] = value;
			}
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06002663 RID: 9827 RVA: 0x000A2F99 File Offset: 0x000A1199
		// (set) Token: 0x06002664 RID: 9828 RVA: 0x000A2FAB File Offset: 0x000A11AB
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute15
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressBookBaseSchema.ConditionalCustomAttribute15];
			}
			set
			{
				this[AddressBookBaseSchema.ConditionalCustomAttribute15] = value;
			}
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06002665 RID: 9829 RVA: 0x000A2FB9 File Offset: 0x000A11B9
		// (set) Token: 0x06002666 RID: 9830 RVA: 0x000A2FCB File Offset: 0x000A11CB
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public new string Name
		{
			get
			{
				return (string)this[AddressBookBaseSchema.Name];
			}
			set
			{
				this[AddressBookBaseSchema.Name] = value;
			}
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06002667 RID: 9831 RVA: 0x000A2FD9 File Offset: 0x000A11D9
		// (set) Token: 0x06002668 RID: 9832 RVA: 0x000A2FEB File Offset: 0x000A11EB
		public ADObjectId RecipientContainer
		{
			get
			{
				return (ADObjectId)this[AddressBookBaseSchema.RecipientContainer];
			}
			set
			{
				this[AddressBookBaseSchema.RecipientContainer] = value;
			}
		}

		// Token: 0x06002669 RID: 9833 RVA: 0x000A2FFC File Offset: 0x000A11FC
		internal void SetRecipientFilter(QueryFilter filter)
		{
			if (filter == null)
			{
				this[AddressBookBaseSchema.RecipientFilter] = string.Empty;
				this[AddressBookBaseSchema.LdapRecipientFilter] = string.Empty;
			}
			else
			{
				this[AddressBookBaseSchema.RecipientFilter] = filter.GenerateInfixString(FilterLanguage.Monad);
				this[AddressBookBaseSchema.LdapRecipientFilter] = LdapFilterBuilder.LdapFilterFromQueryFilter(filter);
			}
			RecipientFilterHelper.SetRecipientFilterType(WellKnownRecipientFilterType.Custom, this.propertyBag, AddressBookBaseSchema.RecipientFilterMetadata);
		}

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x0600266A RID: 9834 RVA: 0x000A3062 File Offset: 0x000A1262
		public string Base64Guid
		{
			get
			{
				return (string)this[AddressBookBaseSchema.Base64Guid];
			}
		}

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x0600266B RID: 9835 RVA: 0x000A3074 File Offset: 0x000A1274
		public string Container
		{
			get
			{
				return (string)this[AddressBookBaseSchema.Container];
			}
		}

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x0600266C RID: 9836 RVA: 0x000A3086 File Offset: 0x000A1286
		public string Path
		{
			get
			{
				return (string)this[AddressBookBaseSchema.Path];
			}
		}

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x0600266D RID: 9837 RVA: 0x000A3098 File Offset: 0x000A1298
		public bool IsTopContainer
		{
			get
			{
				return (bool)this[AddressBookBaseSchema.IsTopContainer];
			}
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x0600266E RID: 9838 RVA: 0x000A30AA File Offset: 0x000A12AA
		public int Depth
		{
			get
			{
				return this.depth;
			}
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x0600266F RID: 9839 RVA: 0x000A30B2 File Offset: 0x000A12B2
		public bool IsGlobalAddressList
		{
			get
			{
				return (bool)this[AddressBookBaseSchema.IsGlobalAddressList];
			}
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06002670 RID: 9840 RVA: 0x000A30C4 File Offset: 0x000A12C4
		// (set) Token: 0x06002671 RID: 9841 RVA: 0x000A30D6 File Offset: 0x000A12D6
		public bool IsSystemAddressList
		{
			get
			{
				return (bool)this[AddressBookBaseSchema.IsSystemAddressList];
			}
			internal set
			{
				this[AddressBookBaseSchema.IsSystemAddressList] = value;
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06002672 RID: 9842 RVA: 0x000A30E9 File Offset: 0x000A12E9
		// (set) Token: 0x06002673 RID: 9843 RVA: 0x000A30FB File Offset: 0x000A12FB
		public bool IsModernGroupsAddressList
		{
			get
			{
				return (bool)this[AddressBookBaseSchema.IsModernGroupsAddressList];
			}
			internal set
			{
				this[AddressBookBaseSchema.IsModernGroupsAddressList] = value;
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06002674 RID: 9844 RVA: 0x000A310E File Offset: 0x000A130E
		public bool IsInSystemAddressListContainer
		{
			get
			{
				return (bool)this[AddressBookBaseSchema.IsInSystemAddressListContainer];
			}
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06002675 RID: 9845 RVA: 0x000A3120 File Offset: 0x000A1320
		public bool IsDefaultGlobalAddressList
		{
			get
			{
				return (bool)this[AddressBookBaseSchema.IsDefaultGlobalAddressList];
			}
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06002676 RID: 9846 RVA: 0x000A3132 File Offset: 0x000A1332
		internal override SystemFlagsEnum SystemFlags
		{
			get
			{
				return (SystemFlagsEnum)this[AddressBookBaseSchema.SystemFlags];
			}
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06002677 RID: 9847 RVA: 0x000A3144 File Offset: 0x000A1344
		ADPropertyDefinition ISupportRecipientFilter.RecipientFilterSchema
		{
			get
			{
				return AddressBookBaseSchema.RecipientFilter;
			}
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06002678 RID: 9848 RVA: 0x000A314B File Offset: 0x000A134B
		ADPropertyDefinition ISupportRecipientFilter.LdapRecipientFilterSchema
		{
			get
			{
				return AddressBookBaseSchema.LdapRecipientFilter;
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06002679 RID: 9849 RVA: 0x000A3152 File Offset: 0x000A1352
		ADPropertyDefinition ISupportRecipientFilter.IncludedRecipientsSchema
		{
			get
			{
				return AddressBookBaseSchema.IncludedRecipients;
			}
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x0600267A RID: 9850 RVA: 0x000A3159 File Offset: 0x000A1359
		ADPropertyDefinition ISupportRecipientFilter.ConditionalDepartmentSchema
		{
			get
			{
				return AddressBookBaseSchema.ConditionalDepartment;
			}
		}

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x0600267B RID: 9851 RVA: 0x000A3160 File Offset: 0x000A1360
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCompanySchema
		{
			get
			{
				return AddressBookBaseSchema.ConditionalCompany;
			}
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x0600267C RID: 9852 RVA: 0x000A3167 File Offset: 0x000A1367
		ADPropertyDefinition ISupportRecipientFilter.ConditionalStateOrProvinceSchema
		{
			get
			{
				return AddressBookBaseSchema.ConditionalStateOrProvince;
			}
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x0600267D RID: 9853 RVA: 0x000A316E File Offset: 0x000A136E
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute1Schema
		{
			get
			{
				return AddressBookBaseSchema.ConditionalCustomAttribute1;
			}
		}

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x0600267E RID: 9854 RVA: 0x000A3175 File Offset: 0x000A1375
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute2Schema
		{
			get
			{
				return AddressBookBaseSchema.ConditionalCustomAttribute2;
			}
		}

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x0600267F RID: 9855 RVA: 0x000A317C File Offset: 0x000A137C
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute3Schema
		{
			get
			{
				return AddressBookBaseSchema.ConditionalCustomAttribute3;
			}
		}

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x06002680 RID: 9856 RVA: 0x000A3183 File Offset: 0x000A1383
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute4Schema
		{
			get
			{
				return AddressBookBaseSchema.ConditionalCustomAttribute4;
			}
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06002681 RID: 9857 RVA: 0x000A318A File Offset: 0x000A138A
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute5Schema
		{
			get
			{
				return AddressBookBaseSchema.ConditionalCustomAttribute5;
			}
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06002682 RID: 9858 RVA: 0x000A3191 File Offset: 0x000A1391
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute6Schema
		{
			get
			{
				return AddressBookBaseSchema.ConditionalCustomAttribute6;
			}
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06002683 RID: 9859 RVA: 0x000A3198 File Offset: 0x000A1398
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute7Schema
		{
			get
			{
				return AddressBookBaseSchema.ConditionalCustomAttribute7;
			}
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06002684 RID: 9860 RVA: 0x000A319F File Offset: 0x000A139F
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute8Schema
		{
			get
			{
				return AddressBookBaseSchema.ConditionalCustomAttribute8;
			}
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06002685 RID: 9861 RVA: 0x000A31A6 File Offset: 0x000A13A6
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute9Schema
		{
			get
			{
				return AddressBookBaseSchema.ConditionalCustomAttribute9;
			}
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06002686 RID: 9862 RVA: 0x000A31AD File Offset: 0x000A13AD
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute10Schema
		{
			get
			{
				return AddressBookBaseSchema.ConditionalCustomAttribute10;
			}
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06002687 RID: 9863 RVA: 0x000A31B4 File Offset: 0x000A13B4
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute11Schema
		{
			get
			{
				return AddressBookBaseSchema.ConditionalCustomAttribute11;
			}
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06002688 RID: 9864 RVA: 0x000A31BB File Offset: 0x000A13BB
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute12Schema
		{
			get
			{
				return AddressBookBaseSchema.ConditionalCustomAttribute12;
			}
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06002689 RID: 9865 RVA: 0x000A31C2 File Offset: 0x000A13C2
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute13Schema
		{
			get
			{
				return AddressBookBaseSchema.ConditionalCustomAttribute13;
			}
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x0600268A RID: 9866 RVA: 0x000A31C9 File Offset: 0x000A13C9
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute14Schema
		{
			get
			{
				return AddressBookBaseSchema.ConditionalCustomAttribute14;
			}
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x0600268B RID: 9867 RVA: 0x000A31D0 File Offset: 0x000A13D0
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute15Schema
		{
			get
			{
				return AddressBookBaseSchema.ConditionalCustomAttribute15;
			}
		}

		// Token: 0x0600268C RID: 9868 RVA: 0x000A31D8 File Offset: 0x000A13D8
		internal bool ShouldInvalidProvisioningCache(out OrganizationId orgId, out Guid[] keys)
		{
			orgId = null;
			keys = null;
			bool flag = false;
			if (base.OrganizationId == null)
			{
				return flag;
			}
			if (base.ObjectState == ObjectState.New || base.ObjectState == ObjectState.Deleted)
			{
				flag = true;
			}
			if (!flag && base.ObjectState == ObjectState.Changed && (base.IsChanged(ADObjectSchema.ExchangeVersion) || base.IsChanged(AddressBookBaseSchema.LdapRecipientFilter) || base.IsChanged(AddressBookBaseSchema.RecipientContainer) || base.IsChanged(AddressBookBaseSchema.IsSystemAddressList)))
			{
				flag = true;
			}
			if (flag)
			{
				orgId = base.OrganizationId;
				keys = new Guid[1];
				keys[0] = CannedProvisioningCacheKeys.AddressBookPolicies;
			}
			return flag;
		}

		// Token: 0x0600268D RID: 9869 RVA: 0x000A3278 File Offset: 0x000A1478
		bool IProvisioningCacheInvalidation.ShouldInvalidProvisioningCache(out OrganizationId orgId, out Guid[] keys)
		{
			return this.ShouldInvalidProvisioningCache(out orgId, out keys);
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x000A3284 File Offset: 0x000A1484
		protected override void ValidateRead(List<ValidationError> errors)
		{
			base.ValidateRead(errors);
			if (!this.IsTopContainer && this.IsSystemAddressList && !this.IsInSystemAddressListContainer)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorSystemAddressListInWrongContainer, this.Identity, AddressBookBaseSchema.IsSystemAddressList.Name));
			}
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x000A32D0 File Offset: 0x000A14D0
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			ValidationError validationError = RecipientFilterHelper.ValidatePrecannedRecipientFilter(this.propertyBag, AddressBookBaseSchema.RecipientFilterMetadata, AddressBookBaseSchema.RecipientFilter, AddressBookBaseSchema.IncludedRecipients, this.Identity);
			if (validationError != null)
			{
				errors.Add(validationError);
			}
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x000A330F File Offset: 0x000A150F
		internal override void StampPersistableDefaultValues()
		{
			if (!base.IsModified(AddressBookBaseSchema.RecipientFilterMetadata))
			{
				this.IncludedRecipients = new WellKnownRecipientType?(WellKnownRecipientType.None);
			}
			base.StampPersistableDefaultValues();
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x000A3330 File Offset: 0x000A1530
		internal object[][] BrowseTo(ref string cookie, int startRange, int itemCount, out int currentRow, params PropertyDefinition[] properties)
		{
			int defaultLcid = LcidMapper.DefaultLcid;
			string empty = string.Empty;
			return this.BrowseTo(ref cookie, null, ref defaultLcid, ref empty, null, startRange, itemCount, out currentRow, false, false, properties);
		}

		// Token: 0x06002692 RID: 9874 RVA: 0x000A3360 File Offset: 0x000A1560
		internal object[][] BrowseTo(ref string cookie, string seekTo, int itemCount, out int currentRow, params PropertyDefinition[] properties)
		{
			int defaultLcid = LcidMapper.DefaultLcid;
			string empty = string.Empty;
			return this.BrowseTo(ref cookie, null, ref defaultLcid, ref empty, seekTo, 0, itemCount, out currentRow, true, false, properties);
		}

		// Token: 0x06002693 RID: 9875 RVA: 0x000A3390 File Offset: 0x000A1590
		internal object[][] BrowseTo(ref string cookie, ADObjectId rootId, ref int lcid, ref string preferredServerName, int startRange, int itemCount, out int currentRow, bool isVirtualListView, params PropertyDefinition[] properties)
		{
			return this.BrowseTo(ref cookie, rootId, ref lcid, ref preferredServerName, null, startRange, itemCount, out currentRow, false, isVirtualListView, properties);
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x000A33B4 File Offset: 0x000A15B4
		internal object[][] BrowseTo(ref string cookie, ADObjectId rootId, ref int lcid, ref string preferredServerName, string seekTo, int itemCount, out int currentRow, bool isVirtualListView, params PropertyDefinition[] properties)
		{
			return this.BrowseTo(ref cookie, rootId, ref lcid, ref preferredServerName, seekTo, 0, itemCount, out currentRow, true, isVirtualListView, properties);
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x000A33D8 File Offset: 0x000A15D8
		private object[][] BrowseTo(ref string cookie, ADObjectId rootId, ref int lcid, ref string preferredServerName, string seekToString, int seekToOffset, int itemCount, out int currentRow, bool seekToCondition, bool isVirtualListView, PropertyDefinition[] properties)
		{
			currentRow = 0;
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, rootId, lcid, true, ConsistencyMode.IgnoreInvalid, null, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(base.OrganizationId), 2293, "BrowseTo", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\AddressBookBase.cs");
			tenantOrRootOrgRecipientSession.DomainController = preferredServerName;
			ADVirtualListView advirtualListView = (ADVirtualListView)tenantOrRootOrgRecipientSession.Browse(isVirtualListView ? null : base.Id, itemCount, properties);
			if (!string.IsNullOrEmpty(cookie))
			{
				try
				{
					advirtualListView.Cookie = Convert.FromBase64String(cookie);
					cookie = string.Empty;
				}
				catch (FormatException)
				{
				}
			}
			if (seekToCondition)
			{
				TextFilter seekFilter = new TextFilter(ADRecipientSchema.DisplayName, seekToString, MatchOptions.Prefix, MatchFlags.Default);
				advirtualListView.SeekToCondition(SeekReference.OriginBeginning, seekFilter);
			}
			else
			{
				advirtualListView.SeekToOffset(SeekReference.OriginBeginning, seekToOffset);
			}
			object[][] rows = advirtualListView.GetRows(itemCount);
			currentRow = advirtualListView.CurrentRow;
			if (advirtualListView.Cookie != null)
			{
				cookie = Convert.ToBase64String(advirtualListView.Cookie);
			}
			lcid = advirtualListView.Lcid;
			preferredServerName = advirtualListView.PreferredServerName;
			return rows;
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x000A34D0 File Offset: 0x000A16D0
		internal static bool IsGlobalAddressListId(ADObjectId alObjectId)
		{
			return alObjectId.DescendantDN(6).DistinguishedName.StartsWith(GlobalAddressList.RdnGalContainerToOrganization.DistinguishedName, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x000A34F0 File Offset: 0x000A16F0
		internal static object[][] PagedSearch(ADObjectId rootId, AddressBookBase addressBookBase, OrganizationId organizationId, AddressBookBase.RecipientCategory recipientCategory, string searchString, ref string cookie, int pagesToSkip, int pageSize, out int itemsTouched, ref int lcid, ref string preferredServerName, PropertyDefinition[] properties)
		{
			if (pagesToSkip < 0)
			{
				throw new ArgumentException("pagesToSkip must be greater than 0");
			}
			int itemsToSkip = pagesToSkip * pageSize;
			return AddressBookBase.PagedSearch(rootId, addressBookBase, organizationId, recipientCategory, searchString, itemsToSkip, ref cookie, pageSize, out itemsTouched, ref lcid, ref preferredServerName, properties);
		}

		// Token: 0x06002698 RID: 9880 RVA: 0x000A352C File Offset: 0x000A172C
		internal static object[][] PagedSearch(ADObjectId rootId, AddressBookBase addressBookBase, OrganizationId organizationId, AddressBookBase.RecipientCategory recipientCategory, string searchString, int itemsToSkip, ref string cookie, int pageSize, out int itemsTouched, ref int lcid, ref string preferredServerName, PropertyDefinition[] properties)
		{
			itemsTouched = 0;
			if (string.IsNullOrEmpty(searchString))
			{
				throw new ArgumentException("searchString should not be null or empty");
			}
			if (itemsToSkip < 0)
			{
				throw new ArgumentOutOfRangeException("itemsToSkip must be >= 0");
			}
			if (pageSize <= 0 || pageSize > 10000)
			{
				throw new ArgumentOutOfRangeException("pageSize must be greater than 0 and less than " + 10000);
			}
			if (properties == null || properties.Length == 0)
			{
				throw new ArgumentException("properties should not be null or empty");
			}
			byte[] cookie2 = new byte[0];
			SortBy sortBy = new SortBy(ADRecipientSchema.DisplayName, SortOrder.Ascending);
			ADPagedReader<ADRawEntry> adpagedReader = null;
			List<object[]> list = new List<object[]>();
			QueryFilter filter = AddressBookBase.CreateFindFilter(searchString, addressBookBase, recipientCategory);
			if (!string.IsNullOrEmpty(cookie))
			{
				try
				{
					cookie2 = Convert.FromBase64String(cookie);
					cookie = string.Empty;
				}
				catch (FormatException)
				{
				}
			}
			IRecipientSession scopedRecipientSession = AddressBookBase.GetScopedRecipientSession(rootId, lcid, preferredServerName, organizationId);
			int num = ADGenericPagedReader<ADRawEntry>.DefaultPageSize;
			int i = itemsToSkip + pageSize;
			if (itemsToSkip > 0)
			{
				if (itemsToSkip < num)
				{
					num = itemsToSkip;
				}
				adpagedReader = scopedRecipientSession.FindPagedADRawEntry(rootId, QueryScope.SubTree, filter, sortBy, num, properties);
				adpagedReader.Cookie = cookie2;
				using (IEnumerator<ADRawEntry> enumerator = adpagedReader.GetEnumerator())
				{
					while (i > ADGenericPagedReader<ADRawEntry>.DefaultPageSize)
					{
						if (itemsToSkip < ADGenericPagedReader<ADRawEntry>.DefaultPageSize)
						{
							num = itemsToSkip;
						}
						for (int j = 0; j < num; j++)
						{
							if (!enumerator.MoveNext())
							{
								return new object[0][];
							}
							itemsToSkip--;
							i--;
							itemsTouched++;
						}
						if (itemsToSkip == 0)
						{
							while (enumerator.MoveNext())
							{
								itemsTouched++;
								list.Add(enumerator.Current.GetProperties(properties));
							}
							return list.ToArray();
						}
					}
				}
				cookie2 = adpagedReader.Cookie;
				if (!string.IsNullOrEmpty(adpagedReader.PreferredServerName))
				{
					scopedRecipientSession.DomainController = adpagedReader.PreferredServerName;
				}
			}
			adpagedReader = scopedRecipientSession.FindPagedADRawEntry(rootId, QueryScope.SubTree, filter, sortBy, pageSize + itemsToSkip, properties);
			adpagedReader.Cookie = cookie2;
			IEnumerator<ADRawEntry> enumerator3;
			IEnumerator<ADRawEntry> enumerator2 = enumerator3 = adpagedReader.GetEnumerator();
			try
			{
				for (int k = 0; k < itemsToSkip; k++)
				{
					if (!enumerator2.MoveNext())
					{
						return new object[0][];
					}
					itemsTouched++;
				}
				int num2 = 0;
				while (num2 < pageSize && enumerator2.MoveNext())
				{
					itemsTouched++;
					list.Add(enumerator2.Current.GetProperties(properties));
					num2++;
				}
			}
			finally
			{
				if (enumerator3 != null)
				{
					enumerator3.Dispose();
				}
			}
			if (adpagedReader.Cookie == null)
			{
				cookie = string.Empty;
			}
			else
			{
				cookie = Convert.ToBase64String(adpagedReader.Cookie);
			}
			lcid = adpagedReader.Lcid;
			preferredServerName = adpagedReader.PreferredServerName;
			return list.ToArray();
		}

		// Token: 0x06002699 RID: 9881 RVA: 0x000A37EC File Offset: 0x000A19EC
		internal bool CanOpenAddressList(ClientSecurityContext clientSecurityContext)
		{
			bool flag = clientSecurityContext.HasExtendedRightOnObject(this.GetSecurityDescriptor(), WellKnownGuid.OpenAddressBookRight);
			ExTraceGlobals.AddressListTracer.TraceDebug<string, bool>(0L, "CanOpenAddressList: {0} {1}", base.DistinguishedName, flag);
			return flag;
		}

		// Token: 0x0600269A RID: 9882 RVA: 0x000A3824 File Offset: 0x000A1A24
		private bool CanEnumerateChildren(ClientSecurityContext clientSecurityContext)
		{
			AccessMask grantedAccess = (AccessMask)clientSecurityContext.GetGrantedAccess(this.GetSecurityDescriptor(), AccessMask.List);
			ExTraceGlobals.AddressListTracer.TraceDebug<string, AccessMask>(0L, "CanEnumerateChildren: {0} perms {1}", base.DistinguishedName, grantedAccess);
			return (grantedAccess & AccessMask.List) != AccessMask.Open;
		}

		// Token: 0x0600269B RID: 9883 RVA: 0x000A3860 File Offset: 0x000A1A60
		private bool CanListObject(ClientSecurityContext clientSecurityContext)
		{
			if (!AddressBookBase.ListObjectModeEnabled())
			{
				return false;
			}
			AccessMask grantedAccess = (AccessMask)clientSecurityContext.GetGrantedAccess(this.GetSecurityDescriptor(), AccessMask.ListObject);
			ExTraceGlobals.AddressListTracer.TraceDebug<string, AccessMask>(0L, "CanListObject: {0} perms {1}", base.DistinguishedName, grantedAccess);
			return (grantedAccess & AccessMask.ListObject) != AccessMask.Open;
		}

		// Token: 0x0600269C RID: 9884 RVA: 0x000A38B0 File Offset: 0x000A1AB0
		internal static void ResetHeuristics()
		{
			lock (AddressBookBase.heuristicsLock)
			{
				AddressBookBase.listObjectMode = false;
				AddressBookBase.heuristicsNextUpdateTime = DateTime.MinValue;
			}
		}

		// Token: 0x0600269D RID: 9885 RVA: 0x000A38FC File Offset: 0x000A1AFC
		private SecurityDescriptor GetSecurityDescriptor()
		{
			SecurityDescriptor securityDescriptor;
			if (this.weakSecurityDescriptor == null)
			{
				securityDescriptor = base.ReadSecurityDescriptorBlob();
				this.weakSecurityDescriptor = new WeakReference(securityDescriptor);
			}
			else
			{
				securityDescriptor = (this.weakSecurityDescriptor.Target as SecurityDescriptor);
				if (securityDescriptor == null)
				{
					securityDescriptor = base.ReadSecurityDescriptorBlob();
					this.weakSecurityDescriptor.Target = securityDescriptor;
				}
			}
			if (securityDescriptor == null)
			{
				throw new ADOperationException(DirectoryStrings.AddressBookNoSecurityDescriptor(base.DistinguishedName));
			}
			return securityDescriptor;
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x000A3964 File Offset: 0x000A1B64
		private bool Contains(ADUser user)
		{
			MultiValuedProperty<ADObjectId> addressListMembership = user.AddressListMembership;
			if (addressListMembership == null || addressListMembership.Count <= 0)
			{
				return false;
			}
			foreach (ADObjectId id in addressListMembership)
			{
				if (base.Id.Equals(id))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600269F RID: 9887 RVA: 0x000A39D8 File Offset: 0x000A1BD8
		internal static AddressBookBase GetGlobalAddressList(ClientSecurityContext clientSecurityContext, IConfigurationSession configurationSession, IRecipientSession recipientSession)
		{
			return AddressBookBase.GetGlobalAddressList(clientSecurityContext, configurationSession, recipientSession, null);
		}

		// Token: 0x060026A0 RID: 9888 RVA: 0x000A39E4 File Offset: 0x000A1BE4
		internal static AddressBookBase GetGlobalAddressList(ClientSecurityContext clientSecurityContext, IConfigurationSession configurationSession, IRecipientSession recipientSession, bool preferCurrentUserGAL)
		{
			ADObjectId adobjectId = null;
			if (preferCurrentUserGAL && clientSecurityContext != null)
			{
				ADUser aduser = recipientSession.FindBySid(clientSecurityContext.UserSid) as ADUser;
				if (aduser == null)
				{
					ExTraceGlobals.AddressListTracer.TraceDebug<SecurityIdentifier>(0L, "Couldn't get a user object for sid '{0}'", clientSecurityContext.UserSid);
				}
				else
				{
					adobjectId = aduser.GlobalAddressListFromAddressBookPolicy;
					if (adobjectId == null)
					{
						ExTraceGlobals.AddressListTracer.TraceDebug<string>(0L, "Couldn't get a GAL for user object of '{0}'", aduser.DistinguishedName);
					}
				}
			}
			return AddressBookBase.GetGlobalAddressList(clientSecurityContext, configurationSession, recipientSession, adobjectId);
		}

		// Token: 0x060026A1 RID: 9889 RVA: 0x000A3A54 File Offset: 0x000A1C54
		internal static AddressBookBase GetGlobalAddressList(ClientSecurityContext clientSecurityContext, IConfigurationSession configurationSession, IRecipientSession recipientSession, ADObjectId globalAddressListFromAddressBookPolicy)
		{
			if (clientSecurityContext == null)
			{
				throw new ArgumentNullException("clientSecurityContext");
			}
			if (clientSecurityContext.UserSid == null)
			{
				throw new ArgumentException("clientSecurityContext has null user sid");
			}
			if (configurationSession == null)
			{
				throw new ArgumentNullException("configurationSession");
			}
			if (recipientSession == null)
			{
				throw new ArgumentNullException("recipientSession");
			}
			if (recipientSession.SessionSettings == null || recipientSession.ConfigScope == ConfigScopes.Global)
			{
				throw new ArgumentException("recipientSession is not properly scoped");
			}
			ExTraceGlobals.AddressListTracer.TraceDebug<SecurityIdentifier>(0L, "AddressBookBase.GetGlobalAddressList called for UserSid = '{0}'", clientSecurityContext.UserSid);
			if (globalAddressListFromAddressBookPolicy != null)
			{
				return configurationSession.Read<AddressBookBase>(globalAddressListFromAddressBookPolicy);
			}
			AddressBookBase addressBookBase = null;
			ADUser aduser = null;
			bool? flag = null;
			int? num = null;
			int num2 = 0;
			ADObjectId descendantId = configurationSession.GetOrgContainerId().GetDescendantId(GlobalAddressList.RdnGalContainerToOrganization);
			foreach (AddressBookBase addressBookBase2 in AddressBookBase.GetAllAddressLists(descendantId, clientSecurityContext, configurationSession, null))
			{
				ExTraceGlobals.AddressListTracer.TraceDebug<string>(0L, "Evaluating GAL '{0}'", addressBookBase2.Id.DistinguishedName);
				num2++;
				bool flag2 = false;
				bool? flag3 = null;
				int? num3 = null;
				if (addressBookBase2.Id.Equals(descendantId))
				{
					ExTraceGlobals.AddressListTracer.TraceDebug<string>(0L, "GAL '{0}' is container, skipping", addressBookBase2.Id.DistinguishedName);
				}
				else
				{
					if (addressBookBase == null)
					{
						ExTraceGlobals.AddressListTracer.TraceDebug<string>(0L, "Picking GAL '{0}' for now", addressBookBase2.Id.DistinguishedName);
						flag2 = true;
					}
					else
					{
						if (aduser == null)
						{
							ExTraceGlobals.AddressListTracer.TraceDebug<SecurityIdentifier>(0L, "Looking up user from sid '{0}'", clientSecurityContext.UserSid);
							aduser = (recipientSession.FindBySid(clientSecurityContext.UserSid) as ADUser);
							if (aduser == null)
							{
								ExTraceGlobals.AddressListTracer.TraceError<SecurityIdentifier>(0L, "Couldn't get a user object for sid '{0}'", clientSecurityContext.UserSid);
								return null;
							}
						}
						if (flag == null)
						{
							flag = new bool?(addressBookBase.Contains(aduser));
							ExTraceGlobals.AddressListTracer.TraceDebug<bool, string>(0L, "Current pick contains user: {0} ('{1}')", flag.Value, addressBookBase.Id.DistinguishedName);
						}
						flag3 = new bool?(addressBookBase2.Contains(aduser));
						ExTraceGlobals.AddressListTracer.TraceDebug<bool, string>(0L, "Candidate contains user: {0} ('{1}')", flag3.Value, addressBookBase2.Id.DistinguishedName);
						if (flag.Value == flag3)
						{
							if (num == null)
							{
								num = new int?(AddressBookBase.GetAddressListSize(configurationSession, addressBookBase.Id.ObjectGuid));
								ExTraceGlobals.AddressListTracer.TraceDebug<int?, string>(0L, "Current pick size: {0} ('{1}')", num, addressBookBase.Id.DistinguishedName);
							}
							num3 = new int?(AddressBookBase.GetAddressListSize(configurationSession, addressBookBase2.Id.ObjectGuid));
							ExTraceGlobals.AddressListTracer.TraceDebug<int?, string>(0L, "Candidate size: {0} ('{1}')", num3, addressBookBase2.Id.DistinguishedName);
							if (num3 > num)
							{
								ExTraceGlobals.AddressListTracer.TraceDebug<string, int?, int?>(0L, "Picking candidate since it is size ({1}) is larger than the previous pick ({2}) ('{0}')", addressBookBase2.Id.DistinguishedName, num3, num);
								flag2 = true;
							}
						}
						else if (flag3.Value)
						{
							ExTraceGlobals.AddressListTracer.TraceDebug<string>(0L, "Picking candidate since it contains user and the previous pick doesn't ('{0}')", addressBookBase2.Id.DistinguishedName);
							flag2 = true;
						}
					}
					if (flag2)
					{
						addressBookBase = addressBookBase2;
						num = num3;
						flag = flag3;
					}
				}
			}
			if (addressBookBase == null)
			{
				string text = clientSecurityContext.UserSid.ToString();
				string text2 = (aduser == null) ? "(null)" : aduser.DistinguishedName;
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_UnableToFindGALForUser, text, new object[]
				{
					text2,
					text
				});
			}
			ExTraceGlobals.AddressListTracer.TraceDebug<string, int>(0L, "Picked GAL = '{0}'. Total evaluated = {1}", (addressBookBase != null) ? addressBookBase.Id.DistinguishedName : "(null)", num2);
			return addressBookBase;
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x000A3E44 File Offset: 0x000A2044
		internal static IEnumerable<AddressBookBase> GetAllAddressLists(ClientSecurityContext clientSecurityContext, IConfigurationSession configurationSession, ADObjectId addressBookPolicyId, bool includeModernGroupsAddressList)
		{
			return AddressBookBase.GetAllAddressLists(configurationSession.GetOrgContainerId().GetDescendantId(AddressList.RdnAlContainerToOrganization), clientSecurityContext, configurationSession, addressBookPolicyId, includeModernGroupsAddressList);
		}

		// Token: 0x060026A3 RID: 9891 RVA: 0x000A3E5F File Offset: 0x000A205F
		internal static IEnumerable<AddressBookBase> GetAllAddressLists(ClientSecurityContext clientSecurityContext, IConfigurationSession configurationSession, ADObjectId addressBookPolicyId)
		{
			return AddressBookBase.GetAllAddressLists(configurationSession.GetOrgContainerId().GetDescendantId(AddressList.RdnAlContainerToOrganization), clientSecurityContext, configurationSession, addressBookPolicyId);
		}

		// Token: 0x060026A4 RID: 9892 RVA: 0x000A3E79 File Offset: 0x000A2079
		internal static IEnumerable<AddressBookBase> GetAllAddressLists(ADObjectId containerId, ClientSecurityContext clientSecurityContext, IConfigurationSession configurationSession, ADObjectId addressBookPolicyId)
		{
			return AddressBookBase.GetAllAddressLists(containerId, clientSecurityContext, configurationSession, addressBookPolicyId, false);
		}

		// Token: 0x060026A5 RID: 9893 RVA: 0x000A41EC File Offset: 0x000A23EC
		internal static IEnumerable<AddressBookBase> GetAllAddressLists(ADObjectId containerId, ClientSecurityContext clientSecurityContext, IConfigurationSession configurationSession, ADObjectId addressBookPolicyId, bool includeModernGroupsAddressList)
		{
			if (clientSecurityContext != null && clientSecurityContext.UserSid == null)
			{
				throw new ArgumentException("clientSecurityContext has null user sid");
			}
			if (addressBookPolicyId != null && clientSecurityContext == null)
			{
				throw new ArgumentException("addressBookPolicy exists without clientSecurityContext");
			}
			if (configurationSession == null)
			{
				throw new ArgumentNullException("configurationSession");
			}
			ExTraceGlobals.AddressListTracer.TraceDebug(0L, "AddressBookBase.GetAllAddressLists called for UserSid = '{0}'", new object[]
			{
				(clientSecurityContext != null) ? clientSecurityContext.UserSid : "(null)"
			});
			IEnumerable<AddressBookBase> addressBooks;
			if (addressBookPolicyId != null)
			{
				AddressBookBase.AddressBookHierarchyHelper addressBookHierarchyHelper = new AddressBookBase.AddressBookHierarchyHelper(clientSecurityContext, configurationSession, null);
				addressBooks = addressBookHierarchyHelper.BuildHierarchyOfAddressListsFromABP(addressBookPolicyId);
			}
			else if (clientSecurityContext != null)
			{
				AddressBookBase.AddressBookHierarchyHelper addressBookHierarchyHelper2 = new AddressBookBase.AddressBookHierarchyHelper(clientSecurityContext, configurationSession, null);
				addressBooks = addressBookHierarchyHelper2.BuildHierarchy(containerId);
			}
			else
			{
				addressBooks = configurationSession.FindPaged<AddressBookBase>(containerId, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, AddressBookBaseSchema.IsSystemAddressList, false), null, 0);
			}
			foreach (AddressBookBase addressList in addressBooks)
			{
				if (!addressList.IsInSystemAddressListContainer)
				{
					if (clientSecurityContext != null && !addressList.CanOpenAddressList(clientSecurityContext))
					{
						ExTraceGlobals.AddressListTracer.TraceDebug<string>(0L, "AddressList '{0}' is not accessible, skipping", addressList.Id.DistinguishedName);
					}
					else if (!includeModernGroupsAddressList && addressList.IsModernGroupsAddressList)
					{
						ExTraceGlobals.AddressListTracer.TraceDebug<string>(0L, "AddressList '{0}' is ModernGroups address list but the Modern Groups flight is not enabled, so skipping it", addressList.Id.DistinguishedName);
					}
					else
					{
						yield return addressList;
					}
				}
			}
			yield break;
		}

		// Token: 0x060026A6 RID: 9894 RVA: 0x000A4228 File Offset: 0x000A2428
		internal static IList<AddressBookBase> GetAllAddressListsByHierarchy(ClientSecurityContext clientSecurityContext, IConfigurationSession configurationSession, ADObjectId addressBookPolicyId, CultureInfo cultureInfo)
		{
			ADObjectId descendantId = configurationSession.GetOrgContainerId().GetDescendantId(AddressList.RdnAlContainerToOrganization);
			AddressBookBase.AddressBookHierarchyHelper addressBookHierarchyHelper = new AddressBookBase.AddressBookHierarchyHelper(clientSecurityContext, configurationSession, cultureInfo);
			if (addressBookPolicyId != null)
			{
				return addressBookHierarchyHelper.BuildHierarchyOfAddressListsFromABP(addressBookPolicyId);
			}
			return addressBookHierarchyHelper.BuildHierarchy(descendantId);
		}

		// Token: 0x060026A7 RID: 9895 RVA: 0x000A4264 File Offset: 0x000A2464
		internal static bool IsModernGroupsAddressListPresent(ClientSecurityContext clientSecurityContext, IConfigurationSession configurationSession, ADObjectId addressBookPolicyId)
		{
			IEnumerable<AddressBookBase> allAddressLists = AddressBookBase.GetAllAddressLists(clientSecurityContext, configurationSession, addressBookPolicyId, true);
			ExTraceGlobals.AddressListTracer.TraceDebug(0L, "AddressBookBase.GetModernGroupsAddressList called for UserSid = '{0}'", new object[]
			{
				(clientSecurityContext != null) ? clientSecurityContext.UserSid : "(null)"
			});
			foreach (AddressBookBase addressBookBase in allAddressLists)
			{
				if (addressBookBase.IsModernGroupsAddressList)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060026A8 RID: 9896 RVA: 0x000A42F0 File Offset: 0x000A24F0
		internal static AddressBookBase GetAllRoomsAddressList(ClientSecurityContext clientSecurityContext, IConfigurationSession configurationSession, ADObjectId addressBookPolicyId)
		{
			AddressBookBase addressBookBase = null;
			if (clientSecurityContext != null && clientSecurityContext.UserSid == null)
			{
				throw new ArgumentException("clientSecurityContext has null user sid");
			}
			if (configurationSession == null)
			{
				throw new ArgumentNullException("configurationSession");
			}
			ExTraceGlobals.AddressListTracer.TraceDebug(0L, "AddressBookBase.GetAllRoomsAddressList called for UserSid = '{0}'", new object[]
			{
				(clientSecurityContext != null) ? clientSecurityContext.UserSid : "(null)"
			});
			if (addressBookPolicyId == null)
			{
				Organization orgContainer = configurationSession.GetOrgContainer();
				MultiValuedProperty<ADObjectId> resourceAddressLists = orgContainer.ResourceAddressLists;
				if (resourceAddressLists.Count <= 0)
				{
					addressBookBase = null;
				}
				else
				{
					ADObjectId adobjectId = resourceAddressLists[0];
					AddressBookBase addressBookBase2 = configurationSession.Read<AddressBookBase>(adobjectId);
					if (addressBookBase2 == null)
					{
						ExTraceGlobals.AddressListTracer.TraceError(0L, "Could not find 'all rooms' entry by " + adobjectId.ToDNString() + " via " + configurationSession.ConfigurationNamingContext.ToDNString());
					}
					else if (clientSecurityContext == null || addressBookBase2.CanOpenAddressList(clientSecurityContext))
					{
						addressBookBase = addressBookBase2;
					}
				}
				ExTraceGlobals.AddressListTracer.TraceDebug<string>(0L, "All Rooms Address Book = {0}", (addressBookBase != null) ? addressBookBase.Id.DistinguishedName : "(null)");
				return addressBookBase;
			}
			AddressBookMailboxPolicy addressBookMailboxPolicy = configurationSession.Read<AddressBookMailboxPolicy>(addressBookPolicyId);
			if (addressBookMailboxPolicy != null)
			{
				return configurationSession.Read<AddressBookBase>(addressBookMailboxPolicy.RoomList);
			}
			return null;
		}

		// Token: 0x060026A9 RID: 9897 RVA: 0x000A4412 File Offset: 0x000A2612
		internal static int GetAddressListSize(IConfigurationSession session, Guid addressListObjectGuid)
		{
			return NspiVirtualListView.GetEstimatedRowCount(session, addressListObjectGuid);
		}

		// Token: 0x04001786 RID: 6022
		private const string MostDerivedClass = "addressBookContainer";

		// Token: 0x04001787 RID: 6023
		private static AddressBookBaseSchema schema = ObjectSchema.GetInstance<AddressBookBaseSchema>();

		// Token: 0x04001788 RID: 6024
		private static bool listObjectMode;

		// Token: 0x04001789 RID: 6025
		private static DateTime heuristicsNextUpdateTime;

		// Token: 0x0400178A RID: 6026
		private static object heuristicsLock = new object();

		// Token: 0x0400178B RID: 6027
		private WeakReference weakSecurityDescriptor;

		// Token: 0x0400178C RID: 6028
		private int depth;

		// Token: 0x02000338 RID: 824
		public enum RecipientCategory
		{
			// Token: 0x0400178E RID: 6030
			People,
			// Token: 0x0400178F RID: 6031
			Groups,
			// Token: 0x04001790 RID: 6032
			Rooms,
			// Token: 0x04001791 RID: 6033
			All
		}

		// Token: 0x02000339 RID: 825
		private static class CategoryFilters
		{
			// Token: 0x060026AB RID: 9899 RVA: 0x000A4431 File Offset: 0x000A2631
			internal static ComparisonFilter ObjectCategoryFilter(string category)
			{
				return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, category);
			}

			// Token: 0x04001792 RID: 6034
			private const string Person = "person";

			// Token: 0x04001793 RID: 6035
			private const string Group = "group";

			// Token: 0x04001794 RID: 6036
			private const string DynamicDistributionList = "msExchDynamicDistributionList";

			// Token: 0x04001795 RID: 6037
			internal static QueryFilter PersonFilter = AddressBookBase.CategoryFilters.ObjectCategoryFilter("person");

			// Token: 0x04001796 RID: 6038
			internal static QueryFilter GroupFilter = new OrFilter(new QueryFilter[]
			{
				AddressBookBase.CategoryFilters.ObjectCategoryFilter("group"),
				AddressBookBase.CategoryFilters.ObjectCategoryFilter("msExchDynamicDistributionList")
			});
		}

		// Token: 0x0200033A RID: 826
		private sealed class AddressBookHierarchyHelper
		{
			// Token: 0x060026AD RID: 9901 RVA: 0x000A4488 File Offset: 0x000A2688
			internal AddressBookHierarchyHelper(ClientSecurityContext clientSecurityContext, IConfigurationSession configurationSession, CultureInfo cultureInfo)
			{
				if (clientSecurityContext != null && clientSecurityContext.UserSid == null)
				{
					throw new ArgumentException("clientSecurityContext has null user sid");
				}
				if (configurationSession == null)
				{
					throw new ArgumentNullException("configurationSession");
				}
				ExTraceGlobals.AddressListTracer.TraceDebug(0L, "AddressBookHierarchyHelper called for UserSid = '{0}'", new object[]
				{
					(clientSecurityContext != null) ? clientSecurityContext.UserSid : "(null)"
				});
				this.clientSecurityContext = clientSecurityContext;
				this.configurationSession = configurationSession;
				if (cultureInfo != null)
				{
					this.nameComparer = new AddressBookBase.AddressBookHierarchyHelper.AddressListNameComparer(cultureInfo);
				}
			}

			// Token: 0x060026AE RID: 9902 RVA: 0x000A452F File Offset: 0x000A272F
			internal List<AddressBookBase> BuildHierarchy(ADObjectId containerId)
			{
				this.LoadAddressListDictionary(containerId);
				this.BuildDictionaryOfChildren();
				this.results = new List<AddressBookBase>(this.addressLists.Count);
				this.ProcessChildren(null, this.roots, 0, true);
				return this.results;
			}

			// Token: 0x060026AF RID: 9903 RVA: 0x000A456C File Offset: 0x000A276C
			internal List<AddressBookBase> BuildHierarchyOfAddressListsFromABP(ADObjectId addressBookPolicyId)
			{
				if (addressBookPolicyId == null)
				{
					throw new ArgumentNullException("addressBookPolicyId");
				}
				AddressBookMailboxPolicy addressBookMailboxPolicy = this.configurationSession.Read<AddressBookMailboxPolicy>(addressBookPolicyId);
				if (addressBookMailboxPolicy != null)
				{
					List<ADObjectId> list = new List<ADObjectId>(addressBookMailboxPolicy.AddressLists.ToArray());
					if (!list.Contains(addressBookMailboxPolicy.RoomList))
					{
						list.Add(addressBookMailboxPolicy.RoomList);
					}
					this.LoadAddressListDictionaryFromABP(list);
					this.BuildDictionaryOfChildren();
					this.results = new List<AddressBookBase>(this.addressLists.Count);
					this.ProcessChildren(null, this.roots, 0, true);
					return this.results;
				}
				return new List<AddressBookBase>();
			}

			// Token: 0x060026B0 RID: 9904 RVA: 0x000A4600 File Offset: 0x000A2800
			private void LoadAddressListDictionary(ADObjectId containerId)
			{
				ADPagedReader<AddressBookBase> adpagedReader = this.configurationSession.FindPaged<AddressBookBase>(containerId, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, AddressBookBaseSchema.IsSystemAddressList, false), null, 0);
				foreach (AddressBookBase addressBookBase in adpagedReader)
				{
					if (!addressBookBase.IsInSystemAddressListContainer)
					{
						this.addressLists.Add(addressBookBase.DistinguishedName, addressBookBase);
					}
				}
			}

			// Token: 0x060026B1 RID: 9905 RVA: 0x000A467C File Offset: 0x000A287C
			private void LoadAddressListDictionaryFromABP(List<ADObjectId> addressLists)
			{
				Result<AddressBookBase>[] array = this.configurationSession.FindByADObjectIds<AddressBookBase>(addressLists.ToArray());
				foreach (Result<AddressBookBase> result in array)
				{
					AddressBookBase data = result.Data;
					if (data != null && !data.IsInSystemAddressListContainer)
					{
						this.addressLists.Add(data.DistinguishedName, data);
					}
				}
			}

			// Token: 0x060026B2 RID: 9906 RVA: 0x000A46E4 File Offset: 0x000A28E4
			private void BuildDictionaryOfChildren()
			{
				foreach (AddressBookBase addressBookBase in this.addressLists.Values)
				{
					bool flag = false;
					string distinguishedName = addressBookBase.Id.Parent.DistinguishedName;
					List<AddressBookBase> list;
					if (this.addressLists.ContainsKey(distinguishedName))
					{
						if (!this.parents.TryGetValue(distinguishedName, out list))
						{
							list = new List<AddressBookBase>();
							this.parents.Add(distinguishedName, list);
						}
					}
					else
					{
						flag = true;
						list = this.roots;
					}
					ExTraceGlobals.AddressListTracer.TraceDebug<string, string, string>(0L, "AddressList '{0}', parent '{1}'{2}", addressBookBase.Id.DistinguishedName, distinguishedName, flag ? "(root)" : string.Empty);
					list.Add(addressBookBase);
				}
			}

			// Token: 0x060026B3 RID: 9907 RVA: 0x000A47C0 File Offset: 0x000A29C0
			private void ProcessChildren(AddressBookBase parentAddressList, List<AddressBookBase> lists, int depth, bool parentIsVisible)
			{
				if (this.nameComparer != null && lists.Count > 1)
				{
					lists.Sort(this.nameComparer);
				}
				foreach (AddressBookBase addressBookBase in lists)
				{
					addressBookBase.depth = depth;
					if (this.ObjectIsVisible(addressBookBase, parentAddressList, parentIsVisible))
					{
						this.results.Add(addressBookBase);
						List<AddressBookBase> lists2;
						if (this.parents.TryGetValue(addressBookBase.DistinguishedName, out lists2))
						{
							this.ProcessChildren(addressBookBase, lists2, depth + 1, addressBookBase.CanEnumerateChildren(this.clientSecurityContext));
						}
					}
				}
			}

			// Token: 0x060026B4 RID: 9908 RVA: 0x000A4870 File Offset: 0x000A2A70
			private bool ObjectIsVisible(AddressBookBase addressList, AddressBookBase parentAddressList, bool parentIsVisible)
			{
				if (parentIsVisible)
				{
					return true;
				}
				if (parentAddressList == null)
				{
					return true;
				}
				if (this.hasListObjectCache == null)
				{
					this.hasListObjectCache = new Dictionary<AddressBookBase, bool>();
				}
				bool flag;
				if (!this.hasListObjectCache.TryGetValue(parentAddressList, out flag))
				{
					flag = parentAddressList.CanListObject(this.clientSecurityContext);
					this.hasListObjectCache.Add(parentAddressList, flag);
				}
				if (!flag)
				{
					return false;
				}
				bool flag2;
				if (!this.hasListObjectCache.TryGetValue(addressList, out flag2))
				{
					flag2 = addressList.CanListObject(this.clientSecurityContext);
					this.hasListObjectCache.Add(addressList, flag2);
				}
				return flag2;
			}

			// Token: 0x04001797 RID: 6039
			private readonly ClientSecurityContext clientSecurityContext;

			// Token: 0x04001798 RID: 6040
			private readonly IConfigurationSession configurationSession;

			// Token: 0x04001799 RID: 6041
			private readonly Dictionary<string, AddressBookBase> addressLists = new Dictionary<string, AddressBookBase>();

			// Token: 0x0400179A RID: 6042
			private readonly Dictionary<string, List<AddressBookBase>> parents = new Dictionary<string, List<AddressBookBase>>();

			// Token: 0x0400179B RID: 6043
			private readonly List<AddressBookBase> roots = new List<AddressBookBase>();

			// Token: 0x0400179C RID: 6044
			private readonly IComparer<AddressBookBase> nameComparer;

			// Token: 0x0400179D RID: 6045
			private List<AddressBookBase> results;

			// Token: 0x0400179E RID: 6046
			private Dictionary<AddressBookBase, bool> hasListObjectCache;

			// Token: 0x0200033B RID: 827
			private class AddressListNameComparer : IComparer<AddressBookBase>
			{
				// Token: 0x060026B5 RID: 9909 RVA: 0x000A48F4 File Offset: 0x000A2AF4
				internal AddressListNameComparer(CultureInfo cultureInfo)
				{
					this.cultureInfo = cultureInfo;
				}

				// Token: 0x060026B6 RID: 9910 RVA: 0x000A4903 File Offset: 0x000A2B03
				public int Compare(AddressBookBase x, AddressBookBase y)
				{
					return string.Compare(x.DisplayName, y.DisplayName, true, this.cultureInfo);
				}

				// Token: 0x0400179F RID: 6047
				private readonly CultureInfo cultureInfo;
			}
		}
	}
}
