using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A8C RID: 2700
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class XsoDictionaryPropertyDefinition : SimpleProviderPropertyDefinition
	{
		// Token: 0x17001B6C RID: 7020
		// (get) Token: 0x060062EF RID: 25327 RVA: 0x001A1CFC File Offset: 0x0019FEFC
		// (set) Token: 0x060062F0 RID: 25328 RVA: 0x001A1D04 File Offset: 0x0019FF04
		public string UserConfigurationName { get; private set; }

		// Token: 0x060062F1 RID: 25329 RVA: 0x001A1D0D File Offset: 0x0019FF0D
		public XsoDictionaryPropertyDefinition(string configurationName, string name, ExchangeObjectVersion versionAdded, Type type, PropertyDefinitionFlags flags, object defaultValue, PropertyDefinitionConstraint[] readConstraints, PropertyDefinitionConstraint[] writeConstraints) : base(name, versionAdded, type, flags, defaultValue, readConstraints, writeConstraints)
		{
			this.UserConfigurationName = configurationName;
		}
	}
}
