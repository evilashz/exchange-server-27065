using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200039B RID: 923
	[Serializable]
	public class AdvancedSecurityContainer : ADLegacyVersionableObject
	{
		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x06002AD6 RID: 10966 RVA: 0x000B2C9B File Offset: 0x000B0E9B
		internal override ADObjectSchema Schema
		{
			get
			{
				return AdvancedSecurityContainer.schema;
			}
		}

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x06002AD7 RID: 10967 RVA: 0x000B2CA2 File Offset: 0x000B0EA2
		internal override string MostDerivedObjectClass
		{
			get
			{
				return AdvancedSecurityContainer.mostDerivedClass;
			}
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x000B2CB1 File Offset: 0x000B0EB1
		internal override void StampPersistableDefaultValues()
		{
			if (!base.IsModified(ADConfigurationObjectSchema.SystemFlags))
			{
				this[ADConfigurationObjectSchema.SystemFlags] = SystemFlagsEnum.None;
			}
			base.StampPersistableDefaultValues();
		}

		// Token: 0x04001996 RID: 6550
		public const string DefaultName = "Advanced Security";

		// Token: 0x04001997 RID: 6551
		private static AdvancedSecurityContainerSchema schema = ObjectSchema.GetInstance<AdvancedSecurityContainerSchema>();

		// Token: 0x04001998 RID: 6552
		private static string mostDerivedClass = "msExchAdvancedSecurityContainer";
	}
}
