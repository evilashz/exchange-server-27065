using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.BackgroundJobBackend
{
	// Token: 0x02000033 RID: 51
	internal sealed class BackgroundJobBackendPropertyDefinition : SimpleProviderPropertyDefinition
	{
		// Token: 0x06000191 RID: 401 RVA: 0x00005F06 File Offset: 0x00004106
		public BackgroundJobBackendPropertyDefinition(string name, Type type, PropertyDefinitionFlags flags, object defaultValue) : base(name, ExchangeObjectVersion.Exchange2012, type, flags, defaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None)
		{
		}
	}
}
