using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000012 RID: 18
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CentralAdminDatabaseRoleConfigurationInfo : InstallableUnitConfigurationInfo
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600013D RID: 317 RVA: 0x000058C0 File Offset: 0x00003AC0
		public override string Name
		{
			get
			{
				return "CentralAdminDatabaseRole";
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600013E RID: 318 RVA: 0x000058C7 File Offset: 0x00003AC7
		public override LocalizedString DisplayName
		{
			get
			{
				return Strings.CentralAdminDatabaseRoleDisplayName;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600013F RID: 319 RVA: 0x000058CE File Offset: 0x00003ACE
		public override decimal Size
		{
			get
			{
				return RequiredDiskSpaceStatistics.CentralAdminDatabaseRole;
			}
		}
	}
}
