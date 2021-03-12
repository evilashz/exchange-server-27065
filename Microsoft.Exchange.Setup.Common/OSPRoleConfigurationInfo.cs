using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000019 RID: 25
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class OSPRoleConfigurationInfo : InstallableUnitConfigurationInfo
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000607A File Offset: 0x0000427A
		public override string Name
		{
			get
			{
				return "OSPRole";
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00006081 File Offset: 0x00004281
		public override LocalizedString DisplayName
		{
			get
			{
				return Strings.OSPRoleDisplayName;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00006088 File Offset: 0x00004288
		public override decimal Size
		{
			get
			{
				return RequiredDiskSpaceStatistics.OSPRole;
			}
		}
	}
}
