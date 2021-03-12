using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000156 RID: 342
	[Serializable]
	internal class MServPropertyDefinition : SimpleProviderPropertyDefinition
	{
		// Token: 0x06000ED1 RID: 3793 RVA: 0x00047124 File Offset: 0x00045324
		internal MServPropertyDefinition(string name, Type type, PropertyDefinitionFlags flags, object defaultValue, ProviderPropertyDefinition[] supportingProperties, GetterDelegate getterDelegate = null, SetterDelegate setterDelegate = null) : this(name, ExchangeObjectVersion.Exchange2003, type, flags, defaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, supportingProperties, getterDelegate, setterDelegate)
		{
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x00047154 File Offset: 0x00045354
		internal MServPropertyDefinition(string name, ExchangeObjectVersion versionAdded, Type type, PropertyDefinitionFlags flags, object defaultValue, PropertyDefinitionConstraint[] readConstraints, PropertyDefinitionConstraint[] writeConstraints, ProviderPropertyDefinition[] supportingProperties, GetterDelegate getterDelegate, SetterDelegate setterDelegate) : base(name, versionAdded, type, flags, defaultValue, readConstraints, writeConstraints, supportingProperties, null, getterDelegate, setterDelegate)
		{
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x00047179 File Offset: 0x00045379
		public override int GetHashCode()
		{
			if (this.hashcode == 0)
			{
				this.hashcode = base.Name.GetHashCodeCaseInsensitive();
			}
			return this.hashcode;
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x0004719C File Offset: 0x0004539C
		internal static MServPropertyDefinition RawRecordPropertyDefinition(string name, PropertyDefinitionFlags flags = PropertyDefinitionFlags.None)
		{
			return new MServPropertyDefinition(name, ExchangeObjectVersion.Exchange2003, typeof(MservRecord), flags, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null);
		}
	}
}
