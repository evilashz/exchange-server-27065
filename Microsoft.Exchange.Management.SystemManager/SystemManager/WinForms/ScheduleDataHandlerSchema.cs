using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200015D RID: 349
	internal class ScheduleDataHandlerSchema : ObjectSchema
	{
		// Token: 0x040005A2 RID: 1442
		public static readonly ProviderPropertyDefinition IntervalHours = new ADPropertyDefinition("IntervalHours", ExchangeObjectVersion.Exchange2003, typeof(Unlimited<uint>), "IntervalHours", ADPropertyDefinitionFlags.None, Unlimited<uint>.UnlimitedValue, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<uint>(1U, 48U)
		}, null, null);
	}
}
