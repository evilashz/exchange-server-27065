using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000379 RID: 889
	internal class ADPowerShellCommonVirtualDirectorySchema : ExchangeVirtualDirectorySchema
	{
		// Token: 0x04001929 RID: 6441
		public static readonly ADPropertyDefinition VirtualDirectoryType = new ADPropertyDefinition("VirtualDirectoryType", ExchangeObjectVersion.Exchange2010, typeof(PowerShellVirtualDirectoryType), "msExchVirtualDirectoryFlags", ADPropertyDefinitionFlags.None, PowerShellVirtualDirectoryType.PowerShell, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
