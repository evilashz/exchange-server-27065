using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000044 RID: 68
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MonitoringRoleConfigurationInfo : InstallableUnitConfigurationInfo
	{
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000B001 File Offset: 0x00009201
		public override string Name
		{
			get
			{
				return "MonitoringRole";
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0000B008 File Offset: 0x00009208
		public override LocalizedString DisplayName
		{
			get
			{
				return Strings.MonitoringRoleDisplayName;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000B00F File Offset: 0x0000920F
		public override decimal Size
		{
			get
			{
				return RequiredDiskSpaceStatistics.MonitoringRole;
			}
		}
	}
}
