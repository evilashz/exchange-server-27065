using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000446 RID: 1094
	[ObjectScope(ConfigScopes.Global)]
	[Serializable]
	public sealed class ExchangeConfigurationContainerWithAddressLists : ExchangeConfigurationContainer
	{
		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x0600317E RID: 12670 RVA: 0x000C6E37 File Offset: 0x000C5037
		public MultiValuedProperty<ADObjectId> AddressBookRoots
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ExchangeConfigurationContainerSchemaWithAddressLists.AddressBookRoots];
			}
		}

		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x0600317F RID: 12671 RVA: 0x000C6E49 File Offset: 0x000C5049
		public MultiValuedProperty<ADObjectId> DefaultGlobalAddressList
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ExchangeConfigurationContainerSchemaWithAddressLists.DefaultGlobalAddressList];
			}
		}

		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x06003180 RID: 12672 RVA: 0x000C6E5B File Offset: 0x000C505B
		public MultiValuedProperty<ADObjectId> AddressBookRoots2
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ExchangeConfigurationContainerSchemaWithAddressLists.AddressBookRoots2];
			}
		}

		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x06003181 RID: 12673 RVA: 0x000C6E6D File Offset: 0x000C506D
		public MultiValuedProperty<ADObjectId> DefaultGlobalAddressList2
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ExchangeConfigurationContainerSchemaWithAddressLists.DefaultGlobalAddressList2];
			}
		}

		// Token: 0x06003182 RID: 12674 RVA: 0x000C6E80 File Offset: 0x000C5080
		internal AddressBookBase GetDefaultGlobalAddressList()
		{
			MultiValuedProperty<ADObjectId> defaultGlobalAddressList = this.DefaultGlobalAddressList;
			AddressBookBase result = null;
			ADObjectId adobjectId = null;
			foreach (ADObjectId adobjectId2 in defaultGlobalAddressList)
			{
				adobjectId = adobjectId2;
				if (adobjectId2.IsDescendantOf(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest()))
				{
					break;
				}
			}
			if (adobjectId != null)
			{
				result = base.Session.Read<AddressBookBase>(adobjectId);
			}
			return result;
		}

		// Token: 0x06003183 RID: 12675 RVA: 0x000C6EF4 File Offset: 0x000C50F4
		internal bool LinkedAddressBookRootAttributesPresent()
		{
			ADSchemaAttributeObject[] array = base.Session.Find<ADSchemaAttributeObject>(base.Session.SchemaNamingContext, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, ADSchemaAttributeSchema.LdapDisplayName, ExchangeConfigurationContainerSchemaWithAddressLists.DefaultGlobalAddressList2.LdapDisplayName), null, 1);
			return array.Length > 0;
		}

		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x06003184 RID: 12676 RVA: 0x000C6F39 File Offset: 0x000C5139
		internal override ADObjectSchema Schema
		{
			get
			{
				return ExchangeConfigurationContainerWithAddressLists.schema;
			}
		}

		// Token: 0x04002141 RID: 8513
		private static ExchangeConfigurationContainerSchemaWithAddressLists schema = ObjectSchema.GetInstance<ExchangeConfigurationContainerSchemaWithAddressLists>();
	}
}
