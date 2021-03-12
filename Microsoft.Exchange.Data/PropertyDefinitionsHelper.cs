using System;
using System.Reflection;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000265 RID: 613
	internal class PropertyDefinitionsHelper
	{
		// Token: 0x06001489 RID: 5257 RVA: 0x000408B8 File Offset: 0x0003EAB8
		internal static SimpleProviderPropertyDefinition CreatePropertyDefinition(string name, Type type)
		{
			return PropertyDefinitionsHelper.CreatePropertyDefinition(name, ExchangeObjectVersion.Exchange2010, type, PropertyDefinitionFlags.None, type.GetTypeInfo().IsValueType ? Activator.CreateInstance(type) : ((type == typeof(string)) ? string.Empty : null), PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x0004090B File Offset: 0x0003EB0B
		internal static SimpleProviderPropertyDefinition CreatePropertyDefinition(string name, Type type, object defaultValue, PropertyDefinitionFlags flags = PropertyDefinitionFlags.PersistDefaultValue)
		{
			return PropertyDefinitionsHelper.CreatePropertyDefinition(name, ExchangeObjectVersion.Exchange2010, type, flags, defaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x00040925 File Offset: 0x0003EB25
		internal static SimpleProviderPropertyDefinition CreatePropertyDefinition(string name, ExchangeObjectVersion versionAdded, Type type, PropertyDefinitionFlags flags, object defaultValue, PropertyDefinitionConstraint[] readConstraints, PropertyDefinitionConstraint[] writeConstraints)
		{
			return new SimpleProviderPropertyDefinition(name, versionAdded, type, flags, defaultValue, readConstraints, writeConstraints);
		}
	}
}
