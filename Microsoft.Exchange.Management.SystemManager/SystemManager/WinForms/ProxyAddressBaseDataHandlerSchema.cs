using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000153 RID: 339
	internal class ProxyAddressBaseDataHandlerSchema
	{
		// Token: 0x0400058F RID: 1423
		public static readonly ADPropertyDefinition Address = new ADPropertyDefinition("Address", ExchangeObjectVersion.Exchange2003, typeof(string), "address", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, ProxyAddressBase.MaxLength - 2)
		}, null, null);

		// Token: 0x04000590 RID: 1424
		public static readonly ADPropertyDefinition Prefix = new ADPropertyDefinition("Prefix", ExchangeObjectVersion.Exchange2003, typeof(string), "prefix", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new AsciiCharactersOnlyConstraint(),
			new StringLengthConstraint(1, 9)
		}, null, null);
	}
}
