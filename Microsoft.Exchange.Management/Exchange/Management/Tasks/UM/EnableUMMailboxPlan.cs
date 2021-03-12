using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D11 RID: 3345
	[Cmdlet("Enable", "UMMailboxPlan", SupportsShouldProcess = true)]
	public sealed class EnableUMMailboxPlan : EnableUMMailboxBase<MailboxPlanIdParameter>
	{
		// Token: 0x170027CD RID: 10189
		// (get) Token: 0x06008067 RID: 32871 RVA: 0x0020D2D5 File Offset: 0x0020B4D5
		protected override bool ShouldSavePin
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170027CE RID: 10190
		// (get) Token: 0x06008068 RID: 32872 RVA: 0x0020D2D8 File Offset: 0x0020B4D8
		protected override bool ShouldSubmitWelcomeMessage
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170027CF RID: 10191
		// (get) Token: 0x06008069 RID: 32873 RVA: 0x0020D2DB File Offset: 0x0020B4DB
		protected override bool ShouldInitUMMailbox
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600806A RID: 32874 RVA: 0x0020D2DE File Offset: 0x0020B4DE
		protected override void InternalProcessRecord()
		{
			this.DataObject.UMProvisioningRequested = true;
			base.InternalProcessRecord();
		}
	}
}
