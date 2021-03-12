using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D0E RID: 3342
	[Cmdlet("Enable", "UMIPGateway", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public class EnableUMIPGateway : SystemConfigurationObjectActionTask<UMIPGatewayIdParameter, UMIPGateway>
	{
		// Token: 0x170027BE RID: 10174
		// (get) Token: 0x06008045 RID: 32837 RVA: 0x0020CA34 File Offset: 0x0020AC34
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageEnableUMIPGateway(this.Identity.ToString());
			}
		}

		// Token: 0x06008046 RID: 32838 RVA: 0x0020CA48 File Offset: 0x0020AC48
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (!base.HasErrors)
			{
				if (this.DataObject.Status == GatewayStatus.Enabled)
				{
					IPGatewayAlreadEnabledException exception = new IPGatewayAlreadEnabledException(this.DataObject.Name);
					base.WriteError(exception, ErrorCategory.InvalidOperation, null);
					return;
				}
				LocalizedException ex = NewUMIPGateway.ValidateFQDNInTenantAcceptedDomain(this.DataObject, (IConfigurationSession)base.DataSession);
				if (ex != null)
				{
					base.WriteError(ex, ErrorCategory.InvalidOperation, this.DataObject);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06008047 RID: 32839 RVA: 0x0020CAC0 File Offset: 0x0020ACC0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			this.DataObject.Status = GatewayStatus.Enabled;
			base.InternalProcessRecord();
			if (!base.HasErrors)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_IPGatewayEnabled, null, new object[]
				{
					this.DataObject.Name,
					this.DataObject.Address.ToString()
				});
			}
			TaskLogger.LogExit();
		}
	}
}
