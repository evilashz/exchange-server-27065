using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000013 RID: 19
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CentralAdminRoleConfigurationInfo : InstallableUnitConfigurationInfo
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000141 RID: 321 RVA: 0x000058DD File Offset: 0x00003ADD
		public override string Name
		{
			get
			{
				return "CentralAdminRole";
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000142 RID: 322 RVA: 0x000058E4 File Offset: 0x00003AE4
		public override LocalizedString DisplayName
		{
			get
			{
				return Strings.CentralAdminRoleDisplayName;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000143 RID: 323 RVA: 0x000058EB File Offset: 0x00003AEB
		public override decimal Size
		{
			get
			{
				return RequiredDiskSpaceStatistics.CentralAdminRole;
			}
		}
	}
}
