using System;
using System.Reflection;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000294 RID: 660
	internal class TransportQueueSchemaHelper
	{
		// Token: 0x060017E0 RID: 6112 RVA: 0x0004AF00 File Offset: 0x00049100
		internal static SimpleProviderPropertyDefinition CreatePropertyDefinition(string name, Type type)
		{
			return TransportQueueSchemaHelper.CreatePropertyDefinition(name, ExchangeObjectVersion.Exchange2010, type, PropertyDefinitionFlags.None, type.GetTypeInfo().IsValueType ? Activator.CreateInstance(type) : ((type == typeof(string)) ? string.Empty : null), PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x0004AF53 File Offset: 0x00049153
		internal static SimpleProviderPropertyDefinition CreatePropertyDefinition(string name, Type type, object defaultValue, PropertyDefinitionFlags flags = PropertyDefinitionFlags.PersistDefaultValue)
		{
			return TransportQueueSchemaHelper.CreatePropertyDefinition(name, ExchangeObjectVersion.Exchange2010, type, flags, defaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x0004AF6D File Offset: 0x0004916D
		internal static SimpleProviderPropertyDefinition CreatePropertyDefinition(string name, ExchangeObjectVersion versionAdded, Type type, PropertyDefinitionFlags flags, object defaultValue, PropertyDefinitionConstraint[] readConstraints, PropertyDefinitionConstraint[] writeConstraints)
		{
			return new SimpleProviderPropertyDefinition(name, versionAdded, type, flags, defaultValue, readConstraints, writeConstraints);
		}
	}
}
