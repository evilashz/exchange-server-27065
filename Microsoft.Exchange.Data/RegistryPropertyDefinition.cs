using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200029A RID: 666
	[Serializable]
	internal class RegistryPropertyDefinition : SimpleProviderPropertyDefinition
	{
		// Token: 0x0600181D RID: 6173 RVA: 0x0004BB5D File Offset: 0x00049D5D
		public RegistryPropertyDefinition(string name, Type type, object defaultValue) : this(name, type, PropertyDefinitionFlags.None, defaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None)
		{
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x0004BB74 File Offset: 0x00049D74
		public RegistryPropertyDefinition(string name, Type type, PropertyDefinitionFlags flags, object defaultValue, PropertyDefinitionConstraint[] readConstraints, PropertyDefinitionConstraint[] writeConstraints) : this(name, type, flags, defaultValue, readConstraints, writeConstraints, ProviderPropertyDefinition.None, null, null, null)
		{
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x0004BB98 File Offset: 0x00049D98
		internal RegistryPropertyDefinition(string name, Type type, PropertyDefinitionFlags flags, object defaultValue, PropertyDefinitionConstraint[] readConstraints, PropertyDefinitionConstraint[] writeConstraints, ProviderPropertyDefinition[] supportingProperties, CustomFilterBuilderDelegate customFilterBuilderDelegate, GetterDelegate getterDelegate, SetterDelegate setterDelegate) : base(name, ExchangeObjectVersion.Exchange2003, type, (defaultValue != null) ? (flags | PropertyDefinitionFlags.PersistDefaultValue) : flags, defaultValue, readConstraints, writeConstraints, supportingProperties, customFilterBuilderDelegate, getterDelegate, setterDelegate)
		{
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x0004BBCB File Offset: 0x00049DCB
		public override bool Equals(ProviderPropertyDefinition other)
		{
			return object.ReferenceEquals(other, this) || (base.Equals(other) && (other as RegistryPropertyDefinition).Flags == base.Flags);
		}

		// Token: 0x04000E3D RID: 3645
		public new static RegistryPropertyDefinition[] None = new RegistryPropertyDefinition[0];
	}
}
