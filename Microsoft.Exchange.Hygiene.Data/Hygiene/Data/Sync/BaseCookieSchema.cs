using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Sync
{
	// Token: 0x02000217 RID: 535
	internal class BaseCookieSchema : ADObjectSchema
	{
		// Token: 0x04000B21 RID: 2849
		public static readonly HygienePropertyDefinition IdentityProp = new HygienePropertyDefinition("Identity", typeof(ADObjectId));

		// Token: 0x04000B22 RID: 2850
		public static readonly HygienePropertyDefinition DataProp = new HygienePropertyDefinition("Data", typeof(byte[]));

		// Token: 0x04000B23 RID: 2851
		public static readonly HygienePropertyDefinition ServiceInstanceProp = CommonSyncProperties.ServiceInstanceProp;

		// Token: 0x04000B24 RID: 2852
		public static readonly HygienePropertyDefinition VersionProp = new HygienePropertyDefinition("Version", typeof(string));

		// Token: 0x04000B25 RID: 2853
		public static readonly HygienePropertyDefinition ActiveMachineProperty = new HygienePropertyDefinition("ActiveMachine", typeof(string));

		// Token: 0x04000B26 RID: 2854
		public static readonly HygienePropertyDefinition CallerProp = new HygienePropertyDefinition("Caller", typeof(Guid));

		// Token: 0x04000B27 RID: 2855
		public static readonly HygienePropertyDefinition BatchIdProp = new HygienePropertyDefinition("BatchId", typeof(Guid));

		// Token: 0x04000B28 RID: 2856
		public static readonly HygienePropertyDefinition AllowNullCookieProp = new HygienePropertyDefinition("AllowNullCookie", typeof(bool));

		// Token: 0x04000B29 RID: 2857
		public static readonly HygienePropertyDefinition AcquireCookieLockProp = new HygienePropertyDefinition("AcquireCookieLock", typeof(bool));

		// Token: 0x04000B2A RID: 2858
		public static readonly HygienePropertyDefinition LastChangedProp = new HygienePropertyDefinition("LastChanged", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000B2B RID: 2859
		public static readonly HygienePropertyDefinition ProvisioningFlagsProperty = CommonSyncProperties.ProvisioningFlagsProperty;

		// Token: 0x04000B2C RID: 2860
		public static readonly HygienePropertyDefinition CompleteProp = new HygienePropertyDefinition("Complete", typeof(bool), false, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000B2D RID: 2861
		public static readonly HygienePropertyDefinition LastUpdatedCutoffThresholdQueryProp = new HygienePropertyDefinition("LastUpdatedCutoffThreshold", typeof(DateTime?));

		// Token: 0x04000B2E RID: 2862
		public static readonly HygienePropertyDefinition LastCompletedCutoffThresholdQueryProp = new HygienePropertyDefinition("LastCompletedCutoffThreshold", typeof(DateTime?));
	}
}
