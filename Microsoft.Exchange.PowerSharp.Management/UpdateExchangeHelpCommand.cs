using System;
using System.Management.Automation;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020008FC RID: 2300
	public class UpdateExchangeHelpCommand : SyntheticCommand<object>
	{
		// Token: 0x060074E1 RID: 29921 RVA: 0x000AFA48 File Offset: 0x000ADC48
		private UpdateExchangeHelpCommand() : base("Update-ExchangeHelp")
		{
		}

		// Token: 0x060074E2 RID: 29922 RVA: 0x000AFA55 File Offset: 0x000ADC55
		public UpdateExchangeHelpCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060074E3 RID: 29923 RVA: 0x000AFA64 File Offset: 0x000ADC64
		public virtual UpdateExchangeHelpCommand SetParameters(UpdateExchangeHelpCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020008FD RID: 2301
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004EA2 RID: 20130
			// (set) Token: 0x060074E4 RID: 29924 RVA: 0x000AFA6E File Offset: 0x000ADC6E
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17004EA3 RID: 20131
			// (set) Token: 0x060074E5 RID: 29925 RVA: 0x000AFA86 File Offset: 0x000ADC86
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004EA4 RID: 20132
			// (set) Token: 0x060074E6 RID: 29926 RVA: 0x000AFA9E File Offset: 0x000ADC9E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004EA5 RID: 20133
			// (set) Token: 0x060074E7 RID: 29927 RVA: 0x000AFAB6 File Offset: 0x000ADCB6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004EA6 RID: 20134
			// (set) Token: 0x060074E8 RID: 29928 RVA: 0x000AFACE File Offset: 0x000ADCCE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}
