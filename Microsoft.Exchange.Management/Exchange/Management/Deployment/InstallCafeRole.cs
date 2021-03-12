using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001E1 RID: 481
	[Cmdlet("Install", "CafeRole", SupportsShouldProcess = true)]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class InstallCafeRole : ManageRole
	{
		// Token: 0x06001079 RID: 4217 RVA: 0x00049106 File Offset: 0x00047306
		public InstallCafeRole()
		{
			base.Fields["CustomerFeedbackEnabled"] = null;
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x0600107A RID: 4218 RVA: 0x0004911F File Offset: 0x0004731F
		protected override LocalizedString Description
		{
			get
			{
				return Strings.InstallCafeRoleDescription;
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x0600107B RID: 4219 RVA: 0x00049126 File Offset: 0x00047326
		// (set) Token: 0x0600107C RID: 4220 RVA: 0x0004913D File Offset: 0x0004733D
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
