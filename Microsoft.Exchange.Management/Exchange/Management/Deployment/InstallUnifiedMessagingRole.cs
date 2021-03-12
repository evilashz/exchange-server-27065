using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001FB RID: 507
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Install", "UnifiedMessagingRole", SupportsShouldProcess = true)]
	public sealed class InstallUnifiedMessagingRole : InstallUnifiedMessagingRoleBase
	{
		// Token: 0x0600114D RID: 4429 RVA: 0x0004C55B File Offset: 0x0004A75B
		public InstallUnifiedMessagingRole()
		{
			base.Fields["NoSelfSignedCertificates"] = false;
			base.Fields["CustomerFeedbackEnabled"] = null;
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x0600114E RID: 4430 RVA: 0x0004C58A File Offset: 0x0004A78A
		protected override LocalizedString Description
		{
			get
			{
				return Strings.InstallUnifiedMessagingRoleDescription;
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x0600114F RID: 4431 RVA: 0x0004C591 File Offset: 0x0004A791
		// (set) Token: 0x06001150 RID: 4432 RVA: 0x0004C5AD File Offset: 0x0004A7AD
		[Parameter(Mandatory = false)]
		public SwitchParameter NoSelfSignedCertificates
		{
			get
			{
				return new SwitchParameter((bool)base.Fields["NoSelfSignedCertificates"]);
			}
			set
			{
				base.Fields["NoSelfSignedCertificates"] = value.ToBool();
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001151 RID: 4433 RVA: 0x0004C5CB File Offset: 0x0004A7CB
		// (set) Token: 0x06001152 RID: 4434 RVA: 0x0004C5E2 File Offset: 0x0004A7E2
		[Parameter(Mandatory = false)]
		public bool? CustomerFeedbackEnabled
		{
			get
			{
				return (bool?)base.Fields["CustomerFeedbackEnabled"];
			}
			set
			{
				base.Fields["CustomerFeedbackEnabled"] = value;
			}
		}
	}
}
