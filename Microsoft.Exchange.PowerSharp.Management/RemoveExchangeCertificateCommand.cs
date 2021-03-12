using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020007D2 RID: 2002
	public class RemoveExchangeCertificateCommand : SyntheticCommandWithPipelineInput<Server, Server>
	{
		// Token: 0x060063FF RID: 25599 RVA: 0x00099251 File Offset: 0x00097451
		private RemoveExchangeCertificateCommand() : base("Remove-ExchangeCertificate")
		{
		}

		// Token: 0x06006400 RID: 25600 RVA: 0x0009925E File Offset: 0x0009745E
		public RemoveExchangeCertificateCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006401 RID: 25601 RVA: 0x0009926D File Offset: 0x0009746D
		public virtual RemoveExchangeCertificateCommand SetParameters(RemoveExchangeCertificateCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006402 RID: 25602 RVA: 0x00099277 File Offset: 0x00097477
		public virtual RemoveExchangeCertificateCommand SetParameters(RemoveExchangeCertificateCommand.ThumbprintParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006403 RID: 25603 RVA: 0x00099281 File Offset: 0x00097481
		public virtual RemoveExchangeCertificateCommand SetParameters(RemoveExchangeCertificateCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020007D3 RID: 2003
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004014 RID: 16404
			// (set) Token: 0x06006404 RID: 25604 RVA: 0x0009928B File Offset: 0x0009748B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ExchangeCertificateIdParameter(value) : null);
				}
			}

			// Token: 0x17004015 RID: 16405
			// (set) Token: 0x06006405 RID: 25605 RVA: 0x000992A9 File Offset: 0x000974A9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004016 RID: 16406
			// (set) Token: 0x06006406 RID: 25606 RVA: 0x000992BC File Offset: 0x000974BC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004017 RID: 16407
			// (set) Token: 0x06006407 RID: 25607 RVA: 0x000992D4 File Offset: 0x000974D4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004018 RID: 16408
			// (set) Token: 0x06006408 RID: 25608 RVA: 0x000992EC File Offset: 0x000974EC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004019 RID: 16409
			// (set) Token: 0x06006409 RID: 25609 RVA: 0x00099304 File Offset: 0x00097504
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700401A RID: 16410
			// (set) Token: 0x0600640A RID: 25610 RVA: 0x0009931C File Offset: 0x0009751C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700401B RID: 16411
			// (set) Token: 0x0600640B RID: 25611 RVA: 0x00099334 File Offset: 0x00097534
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020007D4 RID: 2004
		public class ThumbprintParameters : ParametersBase
		{
			// Token: 0x1700401C RID: 16412
			// (set) Token: 0x0600640D RID: 25613 RVA: 0x00099354 File Offset: 0x00097554
			public virtual string Thumbprint
			{
				set
				{
					base.PowerSharpParameters["Thumbprint"] = value;
				}
			}

			// Token: 0x1700401D RID: 16413
			// (set) Token: 0x0600640E RID: 25614 RVA: 0x00099367 File Offset: 0x00097567
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x1700401E RID: 16414
			// (set) Token: 0x0600640F RID: 25615 RVA: 0x0009937A File Offset: 0x0009757A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700401F RID: 16415
			// (set) Token: 0x06006410 RID: 25616 RVA: 0x0009938D File Offset: 0x0009758D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004020 RID: 16416
			// (set) Token: 0x06006411 RID: 25617 RVA: 0x000993A5 File Offset: 0x000975A5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004021 RID: 16417
			// (set) Token: 0x06006412 RID: 25618 RVA: 0x000993BD File Offset: 0x000975BD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004022 RID: 16418
			// (set) Token: 0x06006413 RID: 25619 RVA: 0x000993D5 File Offset: 0x000975D5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004023 RID: 16419
			// (set) Token: 0x06006414 RID: 25620 RVA: 0x000993ED File Offset: 0x000975ED
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17004024 RID: 16420
			// (set) Token: 0x06006415 RID: 25621 RVA: 0x00099405 File Offset: 0x00097605
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020007D5 RID: 2005
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004025 RID: 16421
			// (set) Token: 0x06006417 RID: 25623 RVA: 0x00099425 File Offset: 0x00097625
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004026 RID: 16422
			// (set) Token: 0x06006418 RID: 25624 RVA: 0x00099438 File Offset: 0x00097638
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004027 RID: 16423
			// (set) Token: 0x06006419 RID: 25625 RVA: 0x00099450 File Offset: 0x00097650
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004028 RID: 16424
			// (set) Token: 0x0600641A RID: 25626 RVA: 0x00099468 File Offset: 0x00097668
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004029 RID: 16425
			// (set) Token: 0x0600641B RID: 25627 RVA: 0x00099480 File Offset: 0x00097680
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700402A RID: 16426
			// (set) Token: 0x0600641C RID: 25628 RVA: 0x00099498 File Offset: 0x00097698
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700402B RID: 16427
			// (set) Token: 0x0600641D RID: 25629 RVA: 0x000994B0 File Offset: 0x000976B0
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
