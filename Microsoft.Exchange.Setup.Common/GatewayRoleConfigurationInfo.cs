using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000025 RID: 37
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class GatewayRoleConfigurationInfo : InstallableUnitConfigurationInfo
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00006F9E File Offset: 0x0000519E
		public override string Name
		{
			get
			{
				return "GatewayRole";
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00006FA5 File Offset: 0x000051A5
		public override LocalizedString DisplayName
		{
			get
			{
				return Strings.GatewayRoleDisplayName;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00006FAC File Offset: 0x000051AC
		public override decimal Size
		{
			get
			{
				return RequiredDiskSpaceStatistics.GatewayRole;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00006FB3 File Offset: 0x000051B3
		public bool StartTransportService
		{
			get
			{
				return InstallableUnitConfigurationInfo.SetupContext.StartTransportService;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00006FBF File Offset: 0x000051BF
		// (set) Token: 0x060001BA RID: 442 RVA: 0x00006FCB File Offset: 0x000051CB
		public ushort AdamLdapPort
		{
			get
			{
				return InstallableUnitConfigurationInfo.SetupContext.AdamLdapPort;
			}
			set
			{
				InstallableUnitConfigurationInfo.SetupContext.AdamLdapPort = value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00006FD8 File Offset: 0x000051D8
		// (set) Token: 0x060001BC RID: 444 RVA: 0x00006FE4 File Offset: 0x000051E4
		public ushort AdamSslPort
		{
			get
			{
				return InstallableUnitConfigurationInfo.SetupContext.AdamSslPort;
			}
			set
			{
				InstallableUnitConfigurationInfo.SetupContext.AdamSslPort = value;
			}
		}
	}
}
