using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D07 RID: 3335
	[Cmdlet("Disable", "UMIPGateway", DefaultParameterSetName = "Identity", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public class DisableUMIPGateway : SystemConfigurationObjectActionTask<UMIPGatewayIdParameter, UMIPGateway>
	{
		// Token: 0x170027B5 RID: 10165
		// (get) Token: 0x06008017 RID: 32791 RVA: 0x0020BC52 File Offset: 0x00209E52
		// (set) Token: 0x06008018 RID: 32792 RVA: 0x0020BC69 File Offset: 0x00209E69
		[Parameter(Mandatory = false)]
		public bool Immediate
		{
			get
			{
				return (bool)base.Fields["Immediate"];
			}
			set
			{
				base.Fields["Immediate"] = value;
			}
		}

		// Token: 0x170027B6 RID: 10166
		// (get) Token: 0x06008019 RID: 32793 RVA: 0x0020BC81 File Offset: 0x00209E81
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (base.Fields["Immediate"] != null && (bool)base.Fields["Immediate"])
				{
					return Strings.ConfirmationMessageDisableUMIPGatewayImmediately;
				}
				return Strings.ConfirmationMessageDisableUMIPGateway;
			}
		}

		// Token: 0x0600801A RID: 32794 RVA: 0x0020BCB8 File Offset: 0x00209EB8
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (!base.HasErrors)
			{
				if (base.Fields["Immediate"] != null && (bool)base.Fields["Immediate"])
				{
					if (this.DataObject.Status == GatewayStatus.Disabled)
					{
						IPGatewayAlreadDisabledException exception = new IPGatewayAlreadDisabledException(this.DataObject.Name);
						base.WriteError(exception, ErrorCategory.InvalidOperation, null);
						return;
					}
				}
				else
				{
					if (this.DataObject.Status == GatewayStatus.NoNewCalls)
					{
						IPGatewayAlreadDisabledException exception2 = new IPGatewayAlreadDisabledException(this.DataObject.Name);
						base.WriteError(exception2, ErrorCategory.InvalidOperation, null);
						return;
					}
					if (this.DataObject.Status == GatewayStatus.Disabled)
					{
						InvalidIPGatewayStateOperationException exception3 = new InvalidIPGatewayStateOperationException(this.DataObject.Name);
						base.WriteError(exception3, ErrorCategory.InvalidOperation, null);
						return;
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600801B RID: 32795 RVA: 0x0020BD84 File Offset: 0x00209F84
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (base.Fields["Immediate"] != null && (bool)base.Fields["Immediate"])
			{
				this.DataObject.Status = GatewayStatus.Disabled;
			}
			else
			{
				this.DataObject.Status = GatewayStatus.NoNewCalls;
			}
			base.InternalProcessRecord();
			if (!base.HasErrors)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_IPGatewayDisabled, null, new object[]
				{
					this.DataObject.Name,
					this.DataObject.Address.ToString()
				});
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04003ED4 RID: 16084
		private const string ImmediateField = "Immediate";
	}
}
