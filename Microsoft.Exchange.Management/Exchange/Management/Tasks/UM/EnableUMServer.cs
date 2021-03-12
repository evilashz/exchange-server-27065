using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D12 RID: 3346
	[Cmdlet("Enable", "UMService", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public class EnableUMServer : SystemConfigurationObjectActionTask<UMServerIdParameter, Server>
	{
		// Token: 0x170027D0 RID: 10192
		// (get) Token: 0x0600806C RID: 32876 RVA: 0x0020D2FA File Offset: 0x0020B4FA
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageEnableUMServer(this.Identity.ToString());
			}
		}

		// Token: 0x0600806D RID: 32877 RVA: 0x0020D30C File Offset: 0x0020B50C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (!base.HasErrors)
			{
				if (this.DataObject.IsE15OrLater)
				{
					base.WriteError(new StatusChangeException(this.DataObject.Name), ErrorCategory.InvalidOperation, null);
				}
				if (this.DataObject.Status == ServerStatus.Enabled)
				{
					UMServerAlreadEnabledException exception = new UMServerAlreadEnabledException(this.DataObject.Name);
					base.WriteError(exception, ErrorCategory.InvalidOperation, null);
					return;
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600806E RID: 32878 RVA: 0x0020D380 File Offset: 0x0020B580
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			this.DataObject.Status = ServerStatus.Enabled;
			base.InternalProcessRecord();
			if (!base.HasErrors)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMServerEnabled, null, new object[]
				{
					this.DataObject.Name
				});
			}
			TaskLogger.LogExit();
		}
	}
}
