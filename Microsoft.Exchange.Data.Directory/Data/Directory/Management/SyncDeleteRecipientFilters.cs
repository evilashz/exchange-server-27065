using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200075B RID: 1883
	internal class SyncDeleteRecipientFilters
	{
		// Token: 0x06005B60 RID: 23392 RVA: 0x0013FA45 File Offset: 0x0013DC45
		public static QueryFilter GetFilter(SyncRecipientType recipientType)
		{
			return SyncDeleteRecipientFilters.Filters[recipientType];
		}

		// Token: 0x04003E2C RID: 15916
		private static readonly QueryFilter Mailbox = new AndFilter(new QueryFilter[]
		{
			new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, ADUser.MostDerivedClass),
			new ExistsFilter(IADMailStorageSchema.ServerLegacyDN)
		});

		// Token: 0x04003E2D RID: 15917
		private static readonly QueryFilter MailContact = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, ADContact.MostDerivedClass);

		// Token: 0x04003E2E RID: 15918
		private static readonly QueryFilter MailUser = new AndFilter(new QueryFilter[]
		{
			new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, ADUser.MostDerivedClass),
			new NotFilter(new ExistsFilter(IADMailStorageSchema.ServerLegacyDN))
		});

		// Token: 0x04003E2F RID: 15919
		private static readonly QueryFilter DistributionGroup = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, ADGroup.MostDerivedClass);

		// Token: 0x04003E30 RID: 15920
		private static readonly QueryFilter DynamicDistributionGroup = new AndFilter(new QueryFilter[]
		{
			new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, ADDynamicGroup.MostDerivedClass)
		});

		// Token: 0x04003E31 RID: 15921
		private static readonly QueryFilter All = new ExistsFilter(ADObjectSchema.ObjectClass);

		// Token: 0x04003E32 RID: 15922
		private static readonly IDictionary<SyncRecipientType, QueryFilter> Filters = new Dictionary<SyncRecipientType, QueryFilter>
		{
			{
				SyncRecipientType.Mailbox,
				SyncDeleteRecipientFilters.Mailbox
			},
			{
				SyncRecipientType.MailContact,
				SyncDeleteRecipientFilters.MailContact
			},
			{
				SyncRecipientType.MailUser,
				SyncDeleteRecipientFilters.MailUser
			},
			{
				SyncRecipientType.DistributionGroup,
				SyncDeleteRecipientFilters.DistributionGroup
			},
			{
				SyncRecipientType.DynamicDistributionGroup,
				SyncDeleteRecipientFilters.DynamicDistributionGroup
			},
			{
				SyncRecipientType.All,
				SyncDeleteRecipientFilters.All
			}
		};
	}
}
