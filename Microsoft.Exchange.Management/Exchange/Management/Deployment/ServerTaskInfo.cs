using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000186 RID: 390
	[ClassAccessLevel(AccessLevel.Consumer)]
	public class ServerTaskInfo : TaskInfo
	{
		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000E8F RID: 3727 RVA: 0x00041AFC File Offset: 0x0003FCFC
		// (set) Token: 0x06000E90 RID: 3728 RVA: 0x00041B05 File Offset: 0x0003FD05
		public ServerTaskInfoBlock Install
		{
			get
			{
				return base.GetBlock<ServerTaskInfoBlock>(InstallationModes.Install);
			}
			set
			{
				base.SetBlock(InstallationModes.Install, value);
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000E91 RID: 3729 RVA: 0x00041B0F File Offset: 0x0003FD0F
		// (set) Token: 0x06000E92 RID: 3730 RVA: 0x00041B18 File Offset: 0x0003FD18
		public ServerTaskInfoBlock BuildToBuildUpgrade
		{
			get
			{
				return base.GetBlock<ServerTaskInfoBlock>(InstallationModes.BuildToBuildUpgrade);
			}
			set
			{
				base.SetBlock(InstallationModes.BuildToBuildUpgrade, value);
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000E93 RID: 3731 RVA: 0x00041B22 File Offset: 0x0003FD22
		// (set) Token: 0x06000E94 RID: 3732 RVA: 0x00041B2B File Offset: 0x0003FD2B
		public ServerTaskInfoBlock DisasterRecovery
		{
			get
			{
				return base.GetBlock<ServerTaskInfoBlock>(InstallationModes.DisasterRecovery);
			}
			set
			{
				base.SetBlock(InstallationModes.DisasterRecovery, value);
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000E95 RID: 3733 RVA: 0x00041B35 File Offset: 0x0003FD35
		// (set) Token: 0x06000E96 RID: 3734 RVA: 0x00041B3E File Offset: 0x0003FD3E
		public ServerTaskInfoBlock Uninstall
		{
			get
			{
				return base.GetBlock<ServerTaskInfoBlock>(InstallationModes.Uninstall);
			}
			set
			{
				base.SetBlock(InstallationModes.Uninstall, value);
			}
		}
	}
}
