using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000521 RID: 1313
	internal class CasTransactionResultSchema : ObjectSchema
	{
		// Token: 0x040021D9 RID: 8665
		public static readonly SimpleProviderPropertyDefinition Value = new SimpleProviderPropertyDefinition("Value", ExchangeObjectVersion.Exchange2010, typeof(CasTransactionResultEnum), PropertyDefinitionFlags.None, CasTransactionResultEnum.Success, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
