using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200033D RID: 829
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class AddressBookMailboxPolicy : MailboxPolicy
	{
		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x060026B9 RID: 9913 RVA: 0x000A4A32 File Offset: 0x000A2C32
		internal override ADObjectSchema Schema
		{
			get
			{
				return AddressBookMailboxPolicy.schema;
			}
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x060026BA RID: 9914 RVA: 0x000A4A39 File Offset: 0x000A2C39
		internal override string MostDerivedObjectClass
		{
			get
			{
				return AddressBookMailboxPolicy.mostDerivedClass;
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x060026BB RID: 9915 RVA: 0x000A4A40 File Offset: 0x000A2C40
		internal override ADObjectId ParentPath
		{
			get
			{
				return AddressBookMailboxPolicy.parentPath;
			}
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x060026BC RID: 9916 RVA: 0x000A4A47 File Offset: 0x000A2C47
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x060026BD RID: 9917 RVA: 0x000A4A50 File Offset: 0x000A2C50
		internal override bool CheckForAssociatedUsers()
		{
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.DistinguishedName, base.Id.DistinguishedName),
				new ExistsFilter(AddressBookMailboxPolicySchema.AssociatedUsers)
			});
			if (base.Session != null)
			{
				AddressBookMailboxPolicy[] array = base.Session.Find<AddressBookMailboxPolicy>(null, QueryScope.SubTree, filter, null, 1);
				return array != null && array.Length > 0;
			}
			return true;
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x060026BE RID: 9918 RVA: 0x000A4AB7 File Offset: 0x000A2CB7
		// (set) Token: 0x060026BF RID: 9919 RVA: 0x000A4AC9 File Offset: 0x000A2CC9
		public MultiValuedProperty<ADObjectId> AddressLists
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[AddressBookMailboxPolicySchema.AddressLists];
			}
			set
			{
				this[AddressBookMailboxPolicySchema.AddressLists] = value;
			}
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x060026C0 RID: 9920 RVA: 0x000A4AD7 File Offset: 0x000A2CD7
		// (set) Token: 0x060026C1 RID: 9921 RVA: 0x000A4AE9 File Offset: 0x000A2CE9
		public ADObjectId GlobalAddressList
		{
			get
			{
				return (ADObjectId)this[AddressBookMailboxPolicySchema.GlobalAddressList];
			}
			set
			{
				this[AddressBookMailboxPolicySchema.GlobalAddressList] = value;
			}
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x060026C2 RID: 9922 RVA: 0x000A4AF7 File Offset: 0x000A2CF7
		// (set) Token: 0x060026C3 RID: 9923 RVA: 0x000A4B09 File Offset: 0x000A2D09
		public ADObjectId RoomList
		{
			get
			{
				return (ADObjectId)this[AddressBookMailboxPolicySchema.RoomList];
			}
			set
			{
				this[AddressBookMailboxPolicySchema.RoomList] = value;
			}
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x060026C4 RID: 9924 RVA: 0x000A4B17 File Offset: 0x000A2D17
		// (set) Token: 0x060026C5 RID: 9925 RVA: 0x000A4B29 File Offset: 0x000A2D29
		public ADObjectId OfflineAddressBook
		{
			get
			{
				return (ADObjectId)this[AddressBookMailboxPolicySchema.OfflineAddressBook];
			}
			set
			{
				this[AddressBookMailboxPolicySchema.OfflineAddressBook] = value;
			}
		}

		// Token: 0x060026C6 RID: 9926 RVA: 0x000A4B37 File Offset: 0x000A2D37
		internal ValidationError AddAddressListToPolicy(IConfigurationSession session, AddressBookBase addressListToAdd)
		{
			this.AddressLists.Add(addressListToAdd.Id);
			return null;
		}

		// Token: 0x040017A5 RID: 6053
		private static AddressBookMailboxPolicySchema schema = ObjectSchema.GetInstance<AddressBookMailboxPolicySchema>();

		// Token: 0x040017A6 RID: 6054
		private static string mostDerivedClass = "msExchAddressBookMailboxPolicy";

		// Token: 0x040017A7 RID: 6055
		private static ADObjectId parentPath = new ADObjectId("CN=AddressBook Mailbox Policies");
	}
}
