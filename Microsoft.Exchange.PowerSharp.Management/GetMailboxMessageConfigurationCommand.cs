using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200048F RID: 1167
	public class GetMailboxMessageConfigurationCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x060041BD RID: 16829 RVA: 0x0006D0B6 File Offset: 0x0006B2B6
		private GetMailboxMessageConfigurationCommand() : base("Get-MailboxMessageConfiguration")
		{
		}

		// Token: 0x060041BE RID: 16830 RVA: 0x0006D0C3 File Offset: 0x0006B2C3
		public GetMailboxMessageConfigurationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060041BF RID: 16831 RVA: 0x0006D0D2 File Offset: 0x0006B2D2
		public virtual GetMailboxMessageConfigurationCommand SetParameters(GetMailboxMessageConfigurationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060041C0 RID: 16832 RVA: 0x0006D0DC File Offset: 0x0006B2DC
		public virtual GetMailboxMessageConfigurationCommand SetParameters(GetMailboxMessageConfigurationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000490 RID: 1168
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002458 RID: 9304
			// (set) Token: 0x060041C1 RID: 16833 RVA: 0x0006D0E6 File Offset: 0x0006B2E6
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17002459 RID: 9305
			// (set) Token: 0x060041C2 RID: 16834 RVA: 0x0006D104 File Offset: 0x0006B304
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700245A RID: 9306
			// (set) Token: 0x060041C3 RID: 16835 RVA: 0x0006D117 File Offset: 0x0006B317
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700245B RID: 9307
			// (set) Token: 0x060041C4 RID: 16836 RVA: 0x0006D12F File Offset: 0x0006B32F
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700245C RID: 9308
			// (set) Token: 0x060041C5 RID: 16837 RVA: 0x0006D147 File Offset: 0x0006B347
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700245D RID: 9309
			// (set) Token: 0x060041C6 RID: 16838 RVA: 0x0006D15A File Offset: 0x0006B35A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700245E RID: 9310
			// (set) Token: 0x060041C7 RID: 16839 RVA: 0x0006D172 File Offset: 0x0006B372
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700245F RID: 9311
			// (set) Token: 0x060041C8 RID: 16840 RVA: 0x0006D18A File Offset: 0x0006B38A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002460 RID: 9312
			// (set) Token: 0x060041C9 RID: 16841 RVA: 0x0006D1A2 File Offset: 0x0006B3A2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000491 RID: 1169
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002461 RID: 9313
			// (set) Token: 0x060041CB RID: 16843 RVA: 0x0006D1C2 File Offset: 0x0006B3C2
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17002462 RID: 9314
			// (set) Token: 0x060041CC RID: 16844 RVA: 0x0006D1D5 File Offset: 0x0006B3D5
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17002463 RID: 9315
			// (set) Token: 0x060041CD RID: 16845 RVA: 0x0006D1ED File Offset: 0x0006B3ED
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17002464 RID: 9316
			// (set) Token: 0x060041CE RID: 16846 RVA: 0x0006D205 File Offset: 0x0006B405
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002465 RID: 9317
			// (set) Token: 0x060041CF RID: 16847 RVA: 0x0006D218 File Offset: 0x0006B418
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002466 RID: 9318
			// (set) Token: 0x060041D0 RID: 16848 RVA: 0x0006D230 File Offset: 0x0006B430
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002467 RID: 9319
			// (set) Token: 0x060041D1 RID: 16849 RVA: 0x0006D248 File Offset: 0x0006B448
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002468 RID: 9320
			// (set) Token: 0x060041D2 RID: 16850 RVA: 0x0006D260 File Offset: 0x0006B460
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
