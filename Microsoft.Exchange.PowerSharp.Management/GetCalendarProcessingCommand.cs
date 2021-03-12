using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000446 RID: 1094
	public class GetCalendarProcessingCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x06003F39 RID: 16185 RVA: 0x00069CD2 File Offset: 0x00067ED2
		private GetCalendarProcessingCommand() : base("Get-CalendarProcessing")
		{
		}

		// Token: 0x06003F3A RID: 16186 RVA: 0x00069CDF File Offset: 0x00067EDF
		public GetCalendarProcessingCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003F3B RID: 16187 RVA: 0x00069CEE File Offset: 0x00067EEE
		public virtual GetCalendarProcessingCommand SetParameters(GetCalendarProcessingCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003F3C RID: 16188 RVA: 0x00069CF8 File Offset: 0x00067EF8
		public virtual GetCalendarProcessingCommand SetParameters(GetCalendarProcessingCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000447 RID: 1095
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002266 RID: 8806
			// (set) Token: 0x06003F3D RID: 16189 RVA: 0x00069D02 File Offset: 0x00067F02
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17002267 RID: 8807
			// (set) Token: 0x06003F3E RID: 16190 RVA: 0x00069D20 File Offset: 0x00067F20
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17002268 RID: 8808
			// (set) Token: 0x06003F3F RID: 16191 RVA: 0x00069D38 File Offset: 0x00067F38
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17002269 RID: 8809
			// (set) Token: 0x06003F40 RID: 16192 RVA: 0x00069D50 File Offset: 0x00067F50
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700226A RID: 8810
			// (set) Token: 0x06003F41 RID: 16193 RVA: 0x00069D63 File Offset: 0x00067F63
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700226B RID: 8811
			// (set) Token: 0x06003F42 RID: 16194 RVA: 0x00069D7B File Offset: 0x00067F7B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700226C RID: 8812
			// (set) Token: 0x06003F43 RID: 16195 RVA: 0x00069D93 File Offset: 0x00067F93
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700226D RID: 8813
			// (set) Token: 0x06003F44 RID: 16196 RVA: 0x00069DAB File Offset: 0x00067FAB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000448 RID: 1096
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700226E RID: 8814
			// (set) Token: 0x06003F46 RID: 16198 RVA: 0x00069DCB File Offset: 0x00067FCB
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700226F RID: 8815
			// (set) Token: 0x06003F47 RID: 16199 RVA: 0x00069DE3 File Offset: 0x00067FE3
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17002270 RID: 8816
			// (set) Token: 0x06003F48 RID: 16200 RVA: 0x00069DFB File Offset: 0x00067FFB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002271 RID: 8817
			// (set) Token: 0x06003F49 RID: 16201 RVA: 0x00069E0E File Offset: 0x0006800E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002272 RID: 8818
			// (set) Token: 0x06003F4A RID: 16202 RVA: 0x00069E26 File Offset: 0x00068026
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002273 RID: 8819
			// (set) Token: 0x06003F4B RID: 16203 RVA: 0x00069E3E File Offset: 0x0006803E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002274 RID: 8820
			// (set) Token: 0x06003F4C RID: 16204 RVA: 0x00069E56 File Offset: 0x00068056
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
