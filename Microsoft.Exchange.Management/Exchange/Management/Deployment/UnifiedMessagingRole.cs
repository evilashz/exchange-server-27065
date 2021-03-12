using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000270 RID: 624
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class UnifiedMessagingRole : Role
	{
		// Token: 0x06001752 RID: 5970 RVA: 0x00063464 File Offset: 0x00061664
		public UnifiedMessagingRole()
		{
			this.roleName = "UnifiedMessagingRole";
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06001753 RID: 5971 RVA: 0x00063477 File Offset: 0x00061677
		public override ServerRole ServerRole
		{
			get
			{
				return ServerRole.UnifiedMessaging;
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06001754 RID: 5972 RVA: 0x0006347B File Offset: 0x0006167B
		public override Task InstallTask
		{
			get
			{
				return new InstallUnifiedMessagingRole();
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06001755 RID: 5973 RVA: 0x00063482 File Offset: 0x00061682
		public override Task DisasterRecoveryTask
		{
			get
			{
				return new DisasterRecoveryUnifiedMessagingRole();
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06001756 RID: 5974 RVA: 0x00063489 File Offset: 0x00061689
		public override Task UninstallTask
		{
			get
			{
				return new UninstallUnifiedMessagingRole();
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06001757 RID: 5975 RVA: 0x00063490 File Offset: 0x00061690
		public override ValidatingTask ValidateTask
		{
			get
			{
				return new ValidateUnifiedMessagingRole();
			}
		}
	}
}
