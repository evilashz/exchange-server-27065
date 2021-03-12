using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D41 RID: 3393
	[Cmdlet("Set", "UMIPGateway", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public class SetUMIPGateway : SetSystemConfigurationObjectTask<UMIPGatewayIdParameter, UMIPGateway>
	{
		// Token: 0x17002865 RID: 10341
		// (get) Token: 0x0600820A RID: 33290 RVA: 0x00213CC1 File Offset: 0x00211EC1
		// (set) Token: 0x0600820B RID: 33291 RVA: 0x00213CE7 File Offset: 0x00211EE7
		[Parameter]
		public SwitchParameter ForceUpgrade
		{
			get
			{
				return (SwitchParameter)(base.Fields["ForceUpgrade"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ForceUpgrade"] = value;
			}
		}

		// Token: 0x17002866 RID: 10342
		// (get) Token: 0x0600820C RID: 33292 RVA: 0x00213CFF File Offset: 0x00211EFF
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetUMIPGateway(this.Identity.ToString());
			}
		}

		// Token: 0x17002867 RID: 10343
		// (get) Token: 0x0600820D RID: 33293 RVA: 0x00213D11 File Offset: 0x00211F11
		protected override bool ExchangeVersionUpgradeSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600820E RID: 33294 RVA: 0x00213D14 File Offset: 0x00211F14
		protected override bool ShouldUpgradeExchangeVersion(ADObject adObject)
		{
			return true;
		}

		// Token: 0x0600820F RID: 33295 RVA: 0x00213D18 File Offset: 0x00211F18
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (!base.HasErrors)
			{
				if (CommonConstants.UseDataCenterCallRouting && this.DataObject.Address.IsIPAddress && this.DataObject.GlobalCallRoutingScheme != UMGlobalCallRoutingScheme.E164)
				{
					base.WriteError(new GatewayAddressRequiresFqdnException(), ErrorCategory.InvalidOperation, this.DataObject);
				}
				LocalizedException ex = NewUMIPGateway.ValidateFQDNInTenantAcceptedDomain(this.DataObject, (IConfigurationSession)base.DataSession);
				if (ex != null)
				{
					base.WriteError(ex, ErrorCategory.InvalidOperation, this.DataObject);
				}
				string text = this.DataObject.Address.ToString();
				this.CheckAndWriteError(new IPGatewayAlreadyExistsException(text), text);
				LocalizedException ex2 = NewUMIPGateway.ValidateIPAddressFamily(this.DataObject);
				if (ex2 != null)
				{
					base.WriteError(ex2, ErrorCategory.InvalidOperation, this.DataObject);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06008210 RID: 33296 RVA: 0x00213DDC File Offset: 0x00211FDC
		protected override void InternalProcessRecord()
		{
			if (!base.IsUpgrading || this.ForceUpgrade || base.ShouldContinue(Strings.ShouldUpgradeObjectVersion("UMIPGateway")))
			{
				base.InternalProcessRecord();
			}
		}

		// Token: 0x06008211 RID: 33297 RVA: 0x00213E0C File Offset: 0x0021200C
		private void CheckAndWriteError(LocalizedException ex, string addr)
		{
			UMIPGateway[] array = Utility.FindGatewayByIPAddress(addr, this.ConfigurationSession);
			if (array != null && array.Length > 0 && (array.Length != 1 || !array[0].Guid.Equals(this.DataObject.Guid)))
			{
				base.WriteError(ex, ErrorCategory.InvalidData, null);
			}
		}
	}
}
