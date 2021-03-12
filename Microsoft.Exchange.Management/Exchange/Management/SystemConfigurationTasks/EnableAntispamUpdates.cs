using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A5A RID: 2650
	[Cmdlet("Enable", "AntispamUpdates", SupportsShouldProcess = true)]
	public class EnableAntispamUpdates : Task
	{
		// Token: 0x17001C98 RID: 7320
		// (get) Token: 0x06005EF0 RID: 24304 RVA: 0x0018E54C File Offset: 0x0018C74C
		// (set) Token: 0x06005EF1 RID: 24305 RVA: 0x0018E563 File Offset: 0x0018C763
		[Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true)]
		public virtual ServerIdParameter Identity
		{
			get
			{
				return (ServerIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17001C99 RID: 7321
		// (get) Token: 0x06005EF2 RID: 24306 RVA: 0x0018E576 File Offset: 0x0018C776
		// (set) Token: 0x06005EF3 RID: 24307 RVA: 0x0018E597 File Offset: 0x0018C797
		[Parameter(Mandatory = false)]
		public bool SpamSignatureUpdatesEnabled
		{
			get
			{
				return (bool)(base.Fields["SpamSignatureUpdatesEnabled"] ?? false);
			}
			set
			{
				base.Fields["SpamSignatureUpdatesEnabled"] = value;
			}
		}

		// Token: 0x17001C9A RID: 7322
		// (get) Token: 0x06005EF4 RID: 24308 RVA: 0x0018E5AF File Offset: 0x0018C7AF
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageEnableAntiSpamUpdates;
			}
		}

		// Token: 0x06005EF5 RID: 24309 RVA: 0x0018E5B6 File Offset: 0x0018C7B6
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			this.WriteWarning(Strings.EnableAntiSpamUpdatesDeprecated);
			TaskLogger.LogExit();
		}

		// Token: 0x04003501 RID: 13569
		private const string CmdletNoun = "AntispamUpdates";

		// Token: 0x04003502 RID: 13570
		private const string ParamSpamSignatureUpdatesEnabled = "SpamSignatureUpdatesEnabled";
	}
}
