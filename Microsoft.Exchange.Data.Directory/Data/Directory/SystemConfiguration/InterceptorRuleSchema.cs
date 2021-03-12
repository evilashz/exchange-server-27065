using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200048C RID: 1164
	internal sealed class InterceptorRuleSchema : ADConfigurationObjectSchema
	{
		// Token: 0x060034C1 RID: 13505 RVA: 0x000D1C08 File Offset: 0x000CFE08
		private static void ExpireTimeUtcSetter(object value, IPropertyBag propertybag)
		{
			DateTime dateTime = (DateTime)value;
			propertybag[InterceptorRuleSchema.ExpireTime] = dateTime.Ticks;
		}

		// Token: 0x060034C2 RID: 13506 RVA: 0x000D1C34 File Offset: 0x000CFE34
		private static object ExpireTimeUtcGetter(IPropertyBag propertyBag)
		{
			long ticks = (long)propertyBag[InterceptorRuleSchema.ExpireTime];
			return new DateTime(ticks, DateTimeKind.Utc);
		}

		// Token: 0x040023F5 RID: 9205
		public static readonly ADPropertyDefinition Priority = new ADPropertyDefinition("RulePriority", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchTransportRulePriority", ADPropertyDefinitionFlags.PersistDefaultValue, 0, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040023F6 RID: 9206
		public static readonly ADPropertyDefinition Xml = new ADPropertyDefinition("RuleXml", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchTransportRuleXml", ADPropertyDefinitionFlags.PersistDefaultValue, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040023F7 RID: 9207
		internal static readonly ADPropertyDefinition ImmutableId = new ADPropertyDefinition("ImmutableId", ExchangeObjectVersion.Exchange2007, typeof(Guid), "wWWHomePage", ADPropertyDefinitionFlags.WriteOnce, System.Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040023F8 RID: 9208
		public static readonly ADPropertyDefinition Version = new ADPropertyDefinition("RuleVersion", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchTransportRuleVersion", ADPropertyDefinitionFlags.PersistDefaultValue, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040023F9 RID: 9209
		public static readonly ADPropertyDefinition Target = new ADPropertyDefinition("RuleTarget", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchTransportRuleTargetLink", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040023FA RID: 9210
		public static readonly ADPropertyDefinition ExpireTime = new ADPropertyDefinition("RuleExpireTime", ExchangeObjectVersion.Exchange2010, typeof(long), "msExchTransportRuleExpireTime", ADPropertyDefinitionFlags.PersistDefaultValue, 0L, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040023FB RID: 9211
		public static readonly ADPropertyDefinition ExpireTimeUtc = new ADPropertyDefinition("ExpireTimeUtc", ExchangeObjectVersion.Exchange2010, typeof(DateTime), null, ADPropertyDefinitionFlags.Calculated, DateTime.MinValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			InterceptorRuleSchema.ExpireTime
		}, null, new GetterDelegate(InterceptorRuleSchema.ExpireTimeUtcGetter), new SetterDelegate(InterceptorRuleSchema.ExpireTimeUtcSetter), null, null);
	}
}
