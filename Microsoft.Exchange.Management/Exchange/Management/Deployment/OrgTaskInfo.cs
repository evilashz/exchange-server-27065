using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000189 RID: 393
	[ClassAccessLevel(AccessLevel.Consumer)]
	public class OrgTaskInfo : TaskInfo
	{
		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000E9F RID: 3743 RVA: 0x00041BC5 File Offset: 0x0003FDC5
		// (set) Token: 0x06000EA0 RID: 3744 RVA: 0x00041BCE File Offset: 0x0003FDCE
		public OrgTaskInfoBlock Install
		{
			get
			{
				return base.GetBlock<OrgTaskInfoBlock>(InstallationModes.Install);
			}
			set
			{
				base.SetBlock(InstallationModes.Install, value);
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000EA1 RID: 3745 RVA: 0x00041BD8 File Offset: 0x0003FDD8
		// (set) Token: 0x06000EA2 RID: 3746 RVA: 0x00041BE1 File Offset: 0x0003FDE1
		public OrgTaskInfoBlock BuildToBuildUpgrade
		{
			get
			{
				return base.GetBlock<OrgTaskInfoBlock>(InstallationModes.BuildToBuildUpgrade);
			}
			set
			{
				base.SetBlock(InstallationModes.BuildToBuildUpgrade, value);
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x00041BEB File Offset: 0x0003FDEB
		// (set) Token: 0x06000EA4 RID: 3748 RVA: 0x00041BF4 File Offset: 0x0003FDF4
		public OrgTaskInfoBlock Uninstall
		{
			get
			{
				return base.GetBlock<OrgTaskInfoBlock>(InstallationModes.Uninstall);
			}
			set
			{
				base.SetBlock(InstallationModes.Uninstall, value);
			}
		}
	}
}
