using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000014 RID: 20
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CentralAdminFrontEndRoleConfigurationInfo : InstallableUnitConfigurationInfo
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000145 RID: 325 RVA: 0x000058FA File Offset: 0x00003AFA
		public override string Name
		{
			get
			{
				return "CentralAdminFrontEndRole";
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00005901 File Offset: 0x00003B01
		public override LocalizedString DisplayName
		{
			get
			{
				return Strings.CentralAdminFrontEndRoleDisplayName;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00005908 File Offset: 0x00003B08
		public override decimal Size
		{
			get
			{
				return RequiredDiskSpaceStatistics.CentralAdminFrontEndRole;
			}
		}
	}
}
