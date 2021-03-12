using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000015 RID: 21
	internal class ClientAccessRoleConfigurationInfo : InstallableUnitConfigurationInfo
	{
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00005917 File Offset: 0x00003B17
		public override string Name
		{
			get
			{
				return "ClientAccessRole";
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600014A RID: 330 RVA: 0x0000591E File Offset: 0x00003B1E
		public override LocalizedString DisplayName
		{
			get
			{
				return Strings.ClientAccessRoleDisplayName;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00005925 File Offset: 0x00003B25
		public override decimal Size
		{
			get
			{
				return RequiredDiskSpaceStatistics.ClientAccessRole;
			}
		}
	}
}
