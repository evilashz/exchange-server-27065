using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003A1 RID: 929
	internal class ADWebServicesVirtualDirectorySchema : ExchangeVirtualDirectorySchema
	{
		// Token: 0x06002AE1 RID: 10977 RVA: 0x000B2D94 File Offset: 0x000B0F94
		internal static object MRSProxyEnabledGetter(IPropertyBag propertyBag)
		{
			MRSProxyFlagsEnum mrsproxyFlagsEnum = (MRSProxyFlagsEnum)propertyBag[ADWebServicesVirtualDirectorySchema.MRSProxyFlags];
			return (mrsproxyFlagsEnum & MRSProxyFlagsEnum.Enabled) == MRSProxyFlagsEnum.Enabled;
		}

		// Token: 0x06002AE2 RID: 10978 RVA: 0x000B2DC0 File Offset: 0x000B0FC0
		internal static void MRSProxyEnabledSetter(object value, IPropertyBag propertyBag)
		{
			bool flag = (bool)value;
			MRSProxyFlagsEnum mrsproxyFlagsEnum = (MRSProxyFlagsEnum)propertyBag[ADWebServicesVirtualDirectorySchema.MRSProxyFlags];
			if (flag)
			{
				propertyBag[ADWebServicesVirtualDirectorySchema.MRSProxyFlags] = (mrsproxyFlagsEnum | MRSProxyFlagsEnum.Enabled);
				return;
			}
			propertyBag[ADWebServicesVirtualDirectorySchema.MRSProxyFlags] = (mrsproxyFlagsEnum & ~MRSProxyFlagsEnum.Enabled);
		}

		// Token: 0x040019C0 RID: 6592
		public static readonly ADPropertyDefinition InternalNLBBypassUrl = new ADPropertyDefinition("InternalNLBBypassURL", ExchangeObjectVersion.Exchange2007, typeof(Uri), "msExchInternalNLBBypassHostName", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, null, null);

		// Token: 0x040019C1 RID: 6593
		public static readonly ADPropertyDefinition ADGzipLevel = new ADPropertyDefinition("GzipLevel", ExchangeObjectVersion.Exchange2007, typeof(GzipLevel), null, ADPropertyDefinitionFlags.TaskPopulated, GzipLevel.High, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040019C2 RID: 6594
		public static readonly ADPropertyDefinition MRSProxyFlags = new ADPropertyDefinition("MRSProxyFlags", ExchangeObjectVersion.Exchange2010, typeof(MRSProxyFlagsEnum), "msExchMRSProxyFlags", ADPropertyDefinitionFlags.PersistDefaultValue, MRSProxyFlagsEnum.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040019C3 RID: 6595
		public static readonly ADPropertyDefinition MRSProxyEnabled = new ADPropertyDefinition("MRSProxyEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADWebServicesVirtualDirectorySchema.MRSProxyFlags
		}, null, new GetterDelegate(ADWebServicesVirtualDirectorySchema.MRSProxyEnabledGetter), new SetterDelegate(ADWebServicesVirtualDirectorySchema.MRSProxyEnabledSetter), null, null);
	}
}
