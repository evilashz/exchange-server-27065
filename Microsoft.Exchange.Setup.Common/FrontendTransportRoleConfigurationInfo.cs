using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000024 RID: 36
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FrontendTransportRoleConfigurationInfo : InstallableUnitConfigurationInfo
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00006F75 File Offset: 0x00005175
		public override string Name
		{
			get
			{
				return "FrontendTransportRole";
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00006F7C File Offset: 0x0000517C
		public override LocalizedString DisplayName
		{
			get
			{
				return Strings.FrontendTransportRoleDisplayName;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00006F83 File Offset: 0x00005183
		public override decimal Size
		{
			get
			{
				return RequiredDiskSpaceStatistics.FrontendTransportRole;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00006F8A File Offset: 0x0000518A
		public bool StartTransportService
		{
			get
			{
				return InstallableUnitConfigurationInfo.SetupContext.StartTransportService;
			}
		}
	}
}
