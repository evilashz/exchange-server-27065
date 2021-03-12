using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x0200008A RID: 138
	[Serializable]
	internal class HygienePropertyDefinition : ADPropertyDefinition
	{
		// Token: 0x06000501 RID: 1281 RVA: 0x00010C91 File Offset: 0x0000EE91
		public HygienePropertyDefinition(string name, Type type) : this(name, type, type.IsValueType ? Activator.CreateInstance(type) : ((type == typeof(string)) ? string.Empty : null), ADPropertyDefinitionFlags.None)
		{
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00010CC6 File Offset: 0x0000EEC6
		public HygienePropertyDefinition(string name, Type type, object defaultValue, ADPropertyDefinitionFlags flags = ADPropertyDefinitionFlags.PersistDefaultValue) : this(name, type, defaultValue, ExchangeObjectVersion.Exchange2010, flags)
		{
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00010CD8 File Offset: 0x0000EED8
		public HygienePropertyDefinition(string name, Type type, object defaultValue, ExchangeObjectVersion version, ADPropertyDefinitionFlags flags = ADPropertyDefinitionFlags.PersistDefaultValue) : base(name, version, type, name, flags, defaultValue, new PropertyDefinitionConstraint[0], new PropertyDefinitionConstraint[0], null, null)
		{
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00010D04 File Offset: 0x0000EF04
		public HygienePropertyDefinition(string name, Type type, object defaultValue, ExchangeObjectVersion version, string displayName, ADPropertyDefinitionFlags flags) : base(name, version, type, displayName, flags, defaultValue, new PropertyDefinitionConstraint[0], new PropertyDefinitionConstraint[0], null, null)
		{
		}
	}
}
