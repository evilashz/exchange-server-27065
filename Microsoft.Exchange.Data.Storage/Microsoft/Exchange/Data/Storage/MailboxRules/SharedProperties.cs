using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.MailboxRules
{
	// Token: 0x02000C06 RID: 3078
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class SharedProperties
	{
		// Token: 0x04003EAD RID: 16045
		public static readonly PropertyDefinition ItemMovedByJunkMailRule = new SimpleProviderPropertyDefinition("ItemMovedByJunkMailRule", ExchangeObjectVersion.Exchange2012, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
