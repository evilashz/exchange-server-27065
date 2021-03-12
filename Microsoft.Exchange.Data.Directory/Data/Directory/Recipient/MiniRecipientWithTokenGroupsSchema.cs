using System;
using System.Security.Principal;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000253 RID: 595
	internal class MiniRecipientWithTokenGroupsSchema : MiniRecipientSchema
	{
		// Token: 0x04000DD3 RID: 3539
		public static readonly ADPropertyDefinition TokenGroupsGlobalAndUniversal = new ADPropertyDefinition("tokenGroupsGlobalAndUniversal", ExchangeObjectVersion.Exchange2003, typeof(SecurityIdentifier), "tokenGroupsGlobalAndUniversal", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Binary | ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
