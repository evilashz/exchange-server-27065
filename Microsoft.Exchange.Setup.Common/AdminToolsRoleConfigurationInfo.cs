using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200000F RID: 15
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AdminToolsRoleConfigurationInfo : InstallableUnitConfigurationInfo
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00005833 File Offset: 0x00003A33
		public override string Name
		{
			get
			{
				return "AdminToolsRole";
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600012D RID: 301 RVA: 0x0000583A File Offset: 0x00003A3A
		public override LocalizedString DisplayName
		{
			get
			{
				return Strings.AdminToolsRoleDisplayName;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00005841 File Offset: 0x00003A41
		public override decimal Size
		{
			get
			{
				return RequiredDiskSpaceStatistics.AdminTools;
			}
		}
	}
}
