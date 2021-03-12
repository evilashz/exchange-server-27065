using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006D8 RID: 1752
	[Serializable]
	public sealed class GlobalAddressList : AddressListBase
	{
		// Token: 0x17001AAA RID: 6826
		// (get) Token: 0x06005126 RID: 20774 RVA: 0x0012C849 File Offset: 0x0012AA49
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return GlobalAddressList.schema;
			}
		}

		// Token: 0x06005127 RID: 20775 RVA: 0x0012C850 File Offset: 0x0012AA50
		public GlobalAddressList()
		{
		}

		// Token: 0x06005128 RID: 20776 RVA: 0x0012C858 File Offset: 0x0012AA58
		public GlobalAddressList(AddressBookBase dataObject) : base(dataObject)
		{
		}

		// Token: 0x06005129 RID: 20777 RVA: 0x0012C861 File Offset: 0x0012AA61
		internal static GlobalAddressList FromDataObject(AddressBookBase dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			return new GlobalAddressList(dataObject);
		}

		// Token: 0x17001AAB RID: 6827
		// (get) Token: 0x0600512A RID: 20778 RVA: 0x0012C86E File Offset: 0x0012AA6E
		public bool IsDefaultGlobalAddressList
		{
			get
			{
				return (bool)this[GlobalAddressListSchema.IsDefaultGlobalAddressList];
			}
		}

		// Token: 0x04003703 RID: 14083
		private static GlobalAddressListSchema schema = ObjectSchema.GetInstance<GlobalAddressListSchema>();

		// Token: 0x04003704 RID: 14084
		public static readonly QueryFilter RecipientFilterForDefaultGal = new AndFilter(new QueryFilter[]
		{
			new ExistsFilter(ADRecipientSchema.Alias),
			new OrFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, "user"),
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, "contact"),
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, "msExchSystemMailbox"),
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, "msExchDynamicDistributionList"),
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, "group"),
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, "publicFolder")
			})
		});

		// Token: 0x04003705 RID: 14085
		public static readonly ADObjectId RdnGalContainerToOrganization = new ADObjectId("CN=All Global Address Lists,CN=Address Lists Container");
	}
}
