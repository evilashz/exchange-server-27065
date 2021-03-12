using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009A8 RID: 2472
	internal class BudgetMetadataSchema : ObjectSchema
	{
		// Token: 0x0600721F RID: 29215 RVA: 0x00179F95 File Offset: 0x00178195
		internal static SimpleProviderPropertyDefinition BuildStringPropDef(string name)
		{
			return BudgetMetadataSchema.BuildRefTypePropDef<string>(name);
		}

		// Token: 0x06007220 RID: 29216 RVA: 0x00179F9D File Offset: 0x0017819D
		internal static SimpleProviderPropertyDefinition BuildRefTypePropDef<T>(string name)
		{
			return new SimpleProviderPropertyDefinition(name, ExchangeObjectVersion.Current, typeof(T), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}

		// Token: 0x06007221 RID: 29217 RVA: 0x00179FC0 File Offset: 0x001781C0
		internal static SimpleProviderPropertyDefinition BuildValueTypePropDef<T>(string name, T defaultValue)
		{
			return new SimpleProviderPropertyDefinition(name, ExchangeObjectVersion.Current, typeof(T), PropertyDefinitionFlags.PersistDefaultValue, defaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}

		// Token: 0x06007222 RID: 29218 RVA: 0x00179FE9 File Offset: 0x001781E9
		internal static SimpleProviderPropertyDefinition BuildUnlimitedPropDef(string name)
		{
			return new SimpleProviderPropertyDefinition(name, ExchangeObjectVersion.Current, typeof(Unlimited<uint>), PropertyDefinitionFlags.None, Unlimited<uint>.UnlimitedValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}

		// Token: 0x06007223 RID: 29219 RVA: 0x0017A018 File Offset: 0x00178218
		internal static SimpleProviderPropertyDefinition BuildValueTypePropDef<T>(string name)
		{
			return BudgetMetadataSchema.BuildValueTypePropDef<T>(name, default(T));
		}

		// Token: 0x040049DB RID: 18907
		public static readonly SimpleProviderPropertyDefinition InCutoff = BudgetMetadataSchema.BuildValueTypePropDef<bool>("InCutoff");

		// Token: 0x040049DC RID: 18908
		public static readonly SimpleProviderPropertyDefinition InMicroDelay = BudgetMetadataSchema.BuildValueTypePropDef<bool>("InMicroDelay");

		// Token: 0x040049DD RID: 18909
		public static readonly SimpleProviderPropertyDefinition NotThrottled = BudgetMetadataSchema.BuildValueTypePropDef<bool>("NotThrottled");

		// Token: 0x040049DE RID: 18910
		public static readonly SimpleProviderPropertyDefinition Connections = BudgetMetadataSchema.BuildValueTypePropDef<int>("Connections");

		// Token: 0x040049DF RID: 18911
		public static readonly SimpleProviderPropertyDefinition Balance = BudgetMetadataSchema.BuildValueTypePropDef<float>("Balance");

		// Token: 0x040049E0 RID: 18912
		public static readonly SimpleProviderPropertyDefinition OutstandingActionCount = BudgetMetadataSchema.BuildValueTypePropDef<int>("OutstandingActionCount");

		// Token: 0x040049E1 RID: 18913
		public static readonly SimpleProviderPropertyDefinition IsServiceAccount = BudgetMetadataSchema.BuildValueTypePropDef<bool>("IsServiceAccount");

		// Token: 0x040049E2 RID: 18914
		public static readonly SimpleProviderPropertyDefinition ThrottlingPolicy = BudgetMetadataSchema.BuildStringPropDef("ThrottlingPolicy");

		// Token: 0x040049E3 RID: 18915
		public static readonly SimpleProviderPropertyDefinition LiveTime = BudgetMetadataSchema.BuildValueTypePropDef<TimeSpan>("LiveTime");

		// Token: 0x040049E4 RID: 18916
		public static readonly SimpleProviderPropertyDefinition Name = BudgetMetadataSchema.BuildStringPropDef("Name");

		// Token: 0x040049E5 RID: 18917
		public static readonly SimpleProviderPropertyDefinition IsGlobalPolicy = BudgetMetadataSchema.BuildValueTypePropDef<bool>("IsGlobalPolicy");

		// Token: 0x040049E6 RID: 18918
		public static readonly SimpleProviderPropertyDefinition IsOrgPolicy = BudgetMetadataSchema.BuildValueTypePropDef<bool>("IsOrgPolicy");

		// Token: 0x040049E7 RID: 18919
		public static readonly SimpleProviderPropertyDefinition IsRegularPolicy = BudgetMetadataSchema.BuildValueTypePropDef<bool>("IsRegularPolicy");
	}
}
