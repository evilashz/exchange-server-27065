using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B79 RID: 2937
	public class RemoveUMIPGatewayCommand : SyntheticCommandWithPipelineInput<UMIPGateway, UMIPGateway>
	{
		// Token: 0x06008E18 RID: 36376 RVA: 0x000D0207 File Offset: 0x000CE407
		private RemoveUMIPGatewayCommand() : base("Remove-UMIPGateway")
		{
		}

		// Token: 0x06008E19 RID: 36377 RVA: 0x000D0214 File Offset: 0x000CE414
		public RemoveUMIPGatewayCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008E1A RID: 36378 RVA: 0x000D0223 File Offset: 0x000CE423
		public virtual RemoveUMIPGatewayCommand SetParameters(RemoveUMIPGatewayCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008E1B RID: 36379 RVA: 0x000D022D File Offset: 0x000CE42D
		public virtual RemoveUMIPGatewayCommand SetParameters(RemoveUMIPGatewayCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B7A RID: 2938
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170062DF RID: 25311
			// (set) Token: 0x06008E1C RID: 36380 RVA: 0x000D0237 File Offset: 0x000CE437
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170062E0 RID: 25312
			// (set) Token: 0x06008E1D RID: 36381 RVA: 0x000D024A File Offset: 0x000CE44A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170062E1 RID: 25313
			// (set) Token: 0x06008E1E RID: 36382 RVA: 0x000D0262 File Offset: 0x000CE462
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170062E2 RID: 25314
			// (set) Token: 0x06008E1F RID: 36383 RVA: 0x000D027A File Offset: 0x000CE47A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170062E3 RID: 25315
			// (set) Token: 0x06008E20 RID: 36384 RVA: 0x000D0292 File Offset: 0x000CE492
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170062E4 RID: 25316
			// (set) Token: 0x06008E21 RID: 36385 RVA: 0x000D02AA File Offset: 0x000CE4AA
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170062E5 RID: 25317
			// (set) Token: 0x06008E22 RID: 36386 RVA: 0x000D02C2 File Offset: 0x000CE4C2
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000B7B RID: 2939
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170062E6 RID: 25318
			// (set) Token: 0x06008E24 RID: 36388 RVA: 0x000D02E2 File Offset: 0x000CE4E2
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UMIPGatewayIdParameter(value) : null);
				}
			}

			// Token: 0x170062E7 RID: 25319
			// (set) Token: 0x06008E25 RID: 36389 RVA: 0x000D0300 File Offset: 0x000CE500
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170062E8 RID: 25320
			// (set) Token: 0x06008E26 RID: 36390 RVA: 0x000D0313 File Offset: 0x000CE513
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170062E9 RID: 25321
			// (set) Token: 0x06008E27 RID: 36391 RVA: 0x000D032B File Offset: 0x000CE52B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170062EA RID: 25322
			// (set) Token: 0x06008E28 RID: 36392 RVA: 0x000D0343 File Offset: 0x000CE543
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170062EB RID: 25323
			// (set) Token: 0x06008E29 RID: 36393 RVA: 0x000D035B File Offset: 0x000CE55B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170062EC RID: 25324
			// (set) Token: 0x06008E2A RID: 36394 RVA: 0x000D0373 File Offset: 0x000CE573
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170062ED RID: 25325
			// (set) Token: 0x06008E2B RID: 36395 RVA: 0x000D038B File Offset: 0x000CE58B
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}
	}
}
