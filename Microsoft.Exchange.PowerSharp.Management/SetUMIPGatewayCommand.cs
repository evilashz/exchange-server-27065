using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B8B RID: 2955
	public class SetUMIPGatewayCommand : SyntheticCommandWithPipelineInputNoOutput<UMIPGateway>
	{
		// Token: 0x06008F0B RID: 36619 RVA: 0x000D15D7 File Offset: 0x000CF7D7
		private SetUMIPGatewayCommand() : base("Set-UMIPGateway")
		{
		}

		// Token: 0x06008F0C RID: 36620 RVA: 0x000D15E4 File Offset: 0x000CF7E4
		public SetUMIPGatewayCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008F0D RID: 36621 RVA: 0x000D15F3 File Offset: 0x000CF7F3
		public virtual SetUMIPGatewayCommand SetParameters(SetUMIPGatewayCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008F0E RID: 36622 RVA: 0x000D15FD File Offset: 0x000CF7FD
		public virtual SetUMIPGatewayCommand SetParameters(SetUMIPGatewayCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B8C RID: 2956
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170063AE RID: 25518
			// (set) Token: 0x06008F0F RID: 36623 RVA: 0x000D1607 File Offset: 0x000CF807
			public virtual SwitchParameter ForceUpgrade
			{
				set
				{
					base.PowerSharpParameters["ForceUpgrade"] = value;
				}
			}

			// Token: 0x170063AF RID: 25519
			// (set) Token: 0x06008F10 RID: 36624 RVA: 0x000D161F File Offset: 0x000CF81F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170063B0 RID: 25520
			// (set) Token: 0x06008F11 RID: 36625 RVA: 0x000D1632 File Offset: 0x000CF832
			public virtual UMSmartHost Address
			{
				set
				{
					base.PowerSharpParameters["Address"] = value;
				}
			}

			// Token: 0x170063B1 RID: 25521
			// (set) Token: 0x06008F12 RID: 36626 RVA: 0x000D1645 File Offset: 0x000CF845
			public virtual bool OutcallsAllowed
			{
				set
				{
					base.PowerSharpParameters["OutcallsAllowed"] = value;
				}
			}

			// Token: 0x170063B2 RID: 25522
			// (set) Token: 0x06008F13 RID: 36627 RVA: 0x000D165D File Offset: 0x000CF85D
			public virtual GatewayStatus Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x170063B3 RID: 25523
			// (set) Token: 0x06008F14 RID: 36628 RVA: 0x000D1675 File Offset: 0x000CF875
			public virtual int Port
			{
				set
				{
					base.PowerSharpParameters["Port"] = value;
				}
			}

			// Token: 0x170063B4 RID: 25524
			// (set) Token: 0x06008F15 RID: 36629 RVA: 0x000D168D File Offset: 0x000CF88D
			public virtual bool Simulator
			{
				set
				{
					base.PowerSharpParameters["Simulator"] = value;
				}
			}

			// Token: 0x170063B5 RID: 25525
			// (set) Token: 0x06008F16 RID: 36630 RVA: 0x000D16A5 File Offset: 0x000CF8A5
			public virtual IPAddressFamily IPAddressFamily
			{
				set
				{
					base.PowerSharpParameters["IPAddressFamily"] = value;
				}
			}

			// Token: 0x170063B6 RID: 25526
			// (set) Token: 0x06008F17 RID: 36631 RVA: 0x000D16BD File Offset: 0x000CF8BD
			public virtual bool DelayedSourcePartyInfoEnabled
			{
				set
				{
					base.PowerSharpParameters["DelayedSourcePartyInfoEnabled"] = value;
				}
			}

			// Token: 0x170063B7 RID: 25527
			// (set) Token: 0x06008F18 RID: 36632 RVA: 0x000D16D5 File Offset: 0x000CF8D5
			public virtual bool MessageWaitingIndicatorAllowed
			{
				set
				{
					base.PowerSharpParameters["MessageWaitingIndicatorAllowed"] = value;
				}
			}

			// Token: 0x170063B8 RID: 25528
			// (set) Token: 0x06008F19 RID: 36633 RVA: 0x000D16ED File Offset: 0x000CF8ED
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170063B9 RID: 25529
			// (set) Token: 0x06008F1A RID: 36634 RVA: 0x000D1700 File Offset: 0x000CF900
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170063BA RID: 25530
			// (set) Token: 0x06008F1B RID: 36635 RVA: 0x000D1718 File Offset: 0x000CF918
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170063BB RID: 25531
			// (set) Token: 0x06008F1C RID: 36636 RVA: 0x000D1730 File Offset: 0x000CF930
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170063BC RID: 25532
			// (set) Token: 0x06008F1D RID: 36637 RVA: 0x000D1748 File Offset: 0x000CF948
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170063BD RID: 25533
			// (set) Token: 0x06008F1E RID: 36638 RVA: 0x000D1760 File Offset: 0x000CF960
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000B8D RID: 2957
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170063BE RID: 25534
			// (set) Token: 0x06008F20 RID: 36640 RVA: 0x000D1780 File Offset: 0x000CF980
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UMIPGatewayIdParameter(value) : null);
				}
			}

			// Token: 0x170063BF RID: 25535
			// (set) Token: 0x06008F21 RID: 36641 RVA: 0x000D179E File Offset: 0x000CF99E
			public virtual SwitchParameter ForceUpgrade
			{
				set
				{
					base.PowerSharpParameters["ForceUpgrade"] = value;
				}
			}

			// Token: 0x170063C0 RID: 25536
			// (set) Token: 0x06008F22 RID: 36642 RVA: 0x000D17B6 File Offset: 0x000CF9B6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170063C1 RID: 25537
			// (set) Token: 0x06008F23 RID: 36643 RVA: 0x000D17C9 File Offset: 0x000CF9C9
			public virtual UMSmartHost Address
			{
				set
				{
					base.PowerSharpParameters["Address"] = value;
				}
			}

			// Token: 0x170063C2 RID: 25538
			// (set) Token: 0x06008F24 RID: 36644 RVA: 0x000D17DC File Offset: 0x000CF9DC
			public virtual bool OutcallsAllowed
			{
				set
				{
					base.PowerSharpParameters["OutcallsAllowed"] = value;
				}
			}

			// Token: 0x170063C3 RID: 25539
			// (set) Token: 0x06008F25 RID: 36645 RVA: 0x000D17F4 File Offset: 0x000CF9F4
			public virtual GatewayStatus Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x170063C4 RID: 25540
			// (set) Token: 0x06008F26 RID: 36646 RVA: 0x000D180C File Offset: 0x000CFA0C
			public virtual int Port
			{
				set
				{
					base.PowerSharpParameters["Port"] = value;
				}
			}

			// Token: 0x170063C5 RID: 25541
			// (set) Token: 0x06008F27 RID: 36647 RVA: 0x000D1824 File Offset: 0x000CFA24
			public virtual bool Simulator
			{
				set
				{
					base.PowerSharpParameters["Simulator"] = value;
				}
			}

			// Token: 0x170063C6 RID: 25542
			// (set) Token: 0x06008F28 RID: 36648 RVA: 0x000D183C File Offset: 0x000CFA3C
			public virtual IPAddressFamily IPAddressFamily
			{
				set
				{
					base.PowerSharpParameters["IPAddressFamily"] = value;
				}
			}

			// Token: 0x170063C7 RID: 25543
			// (set) Token: 0x06008F29 RID: 36649 RVA: 0x000D1854 File Offset: 0x000CFA54
			public virtual bool DelayedSourcePartyInfoEnabled
			{
				set
				{
					base.PowerSharpParameters["DelayedSourcePartyInfoEnabled"] = value;
				}
			}

			// Token: 0x170063C8 RID: 25544
			// (set) Token: 0x06008F2A RID: 36650 RVA: 0x000D186C File Offset: 0x000CFA6C
			public virtual bool MessageWaitingIndicatorAllowed
			{
				set
				{
					base.PowerSharpParameters["MessageWaitingIndicatorAllowed"] = value;
				}
			}

			// Token: 0x170063C9 RID: 25545
			// (set) Token: 0x06008F2B RID: 36651 RVA: 0x000D1884 File Offset: 0x000CFA84
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170063CA RID: 25546
			// (set) Token: 0x06008F2C RID: 36652 RVA: 0x000D1897 File Offset: 0x000CFA97
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170063CB RID: 25547
			// (set) Token: 0x06008F2D RID: 36653 RVA: 0x000D18AF File Offset: 0x000CFAAF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170063CC RID: 25548
			// (set) Token: 0x06008F2E RID: 36654 RVA: 0x000D18C7 File Offset: 0x000CFAC7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170063CD RID: 25549
			// (set) Token: 0x06008F2F RID: 36655 RVA: 0x000D18DF File Offset: 0x000CFADF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170063CE RID: 25550
			// (set) Token: 0x06008F30 RID: 36656 RVA: 0x000D18F7 File Offset: 0x000CFAF7
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
