using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000083 RID: 131
	[Cmdlet("Disable", "MailUser", SupportsShouldProcess = true, DefaultParameterSetName = "Identity", ConfirmImpact = ConfirmImpact.High)]
	public sealed class DisableMailUser : DisableMailUserBase<MailUserIdParameter>
	{
		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x0002740E File Offset: 0x0002560E
		// (set) Token: 0x06000936 RID: 2358 RVA: 0x00027434 File Offset: 0x00025634
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeSoftDeletedObjects
		{
			get
			{
				return (SwitchParameter)(base.Fields["SoftDeletedMailUser"] ?? false);
			}
			set
			{
				base.Fields["SoftDeletedMailUser"] = value;
			}
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0002744C File Offset: 0x0002564C
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter();
			base.InternalStateReset();
			if (this.IncludeSoftDeletedObjects)
			{
				base.SessionSettings.IncludeSoftDeletedObjects = true;
			}
			TaskLogger.LogExit();
		}
	}
}
