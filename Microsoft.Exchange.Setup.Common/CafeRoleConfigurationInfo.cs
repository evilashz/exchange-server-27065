using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000011 RID: 17
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CafeRoleConfigurationInfo : InstallableUnitConfigurationInfo
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000139 RID: 313 RVA: 0x000058A3 File Offset: 0x00003AA3
		public override string Name
		{
			get
			{
				return "CafeRole";
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600013A RID: 314 RVA: 0x000058AA File Offset: 0x00003AAA
		public override LocalizedString DisplayName
		{
			get
			{
				return Strings.CafeRoleDisplayName;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600013B RID: 315 RVA: 0x000058B1 File Offset: 0x00003AB1
		public override decimal Size
		{
			get
			{
				return RequiredDiskSpaceStatistics.CafeRole;
			}
		}
	}
}
