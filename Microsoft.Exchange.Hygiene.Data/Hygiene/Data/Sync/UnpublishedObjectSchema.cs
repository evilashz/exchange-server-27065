using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Sync
{
	// Token: 0x0200022C RID: 556
	internal class UnpublishedObjectSchema : ADObjectSchema
	{
		// Token: 0x04000B60 RID: 2912
		public static readonly HygienePropertyDefinition TenantIdProp = CommonSyncProperties.TenantIdProp;

		// Token: 0x04000B61 RID: 2913
		public static readonly HygienePropertyDefinition ObjectIdProp = CommonSyncProperties.ObjectIdProp;

		// Token: 0x04000B62 RID: 2914
		public static readonly HygienePropertyDefinition ObjectTypeProp = CommonSyncProperties.ObjectTypeProp;

		// Token: 0x04000B63 RID: 2915
		public static readonly HygienePropertyDefinition ServiceInstanceProp = CommonSyncProperties.ServiceInstanceProp;

		// Token: 0x04000B64 RID: 2916
		public static readonly HygienePropertyDefinition CreatedDateProp = new HygienePropertyDefinition("CreatedDate", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000B65 RID: 2917
		public static readonly HygienePropertyDefinition LastRetriedDateProp = new HygienePropertyDefinition("LastRetriedDate", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000B66 RID: 2918
		public static readonly HygienePropertyDefinition ErrorMessageProp = new HygienePropertyDefinition("ErrorMessage", typeof(string));
	}
}
