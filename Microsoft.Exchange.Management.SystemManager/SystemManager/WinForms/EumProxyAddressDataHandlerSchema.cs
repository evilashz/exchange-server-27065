using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000155 RID: 341
	internal class EumProxyAddressDataHandlerSchema
	{
		// Token: 0x04000591 RID: 1425
		public static int FixedLength = "phone-context=".Length + ";".Length + ProxyAddressPrefix.UM.DisplayName.Length + 1;

		// Token: 0x04000592 RID: 1426
		public static readonly ADPropertyDefinition Extension = new ADPropertyDefinition("Extension", ExchangeObjectVersion.Exchange2003, typeof(string), "Extension", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, ProxyAddressBase.MaxLength - EumProxyAddressDataHandlerSchema.FixedLength)
		}, null, null);
	}
}
