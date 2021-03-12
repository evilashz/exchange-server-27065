using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000010 RID: 16
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class BridgeheadRoleConfigurationInfo : InstallableUnitConfigurationInfo
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00005850 File Offset: 0x00003A50
		public override string Name
		{
			get
			{
				return "BridgeheadRole";
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00005857 File Offset: 0x00003A57
		public override LocalizedString DisplayName
		{
			get
			{
				return Strings.BridgeheadRoleDisplayName;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000132 RID: 306 RVA: 0x0000585E File Offset: 0x00003A5E
		public override decimal Size
		{
			get
			{
				return RequiredDiskSpaceStatistics.BridgeheadRole;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00005865 File Offset: 0x00003A65
		public bool StartTransportService
		{
			get
			{
				return InstallableUnitConfigurationInfo.SetupContext.StartTransportService;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00005871 File Offset: 0x00003A71
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00005879 File Offset: 0x00003A79
		public ServerIdParameter LegacyRoutingServerId
		{
			get
			{
				return this.legacyRoutingServerId;
			}
			set
			{
				this.legacyRoutingServerId = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00005882 File Offset: 0x00003A82
		// (set) Token: 0x06000137 RID: 311 RVA: 0x0000588E File Offset: 0x00003A8E
		public bool DisableAMFiltering
		{
			get
			{
				return InstallableUnitConfigurationInfo.SetupContext.DisableAMFiltering;
			}
			set
			{
				InstallableUnitConfigurationInfo.SetupContext.DisableAMFiltering = value;
			}
		}

		// Token: 0x04000043 RID: 67
		private ServerIdParameter legacyRoutingServerId;
	}
}
