using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000800 RID: 2048
	public class UpdateOfflineAddressBookCommand : SyntheticCommandWithPipelineInput<OfflineAddressBook, OfflineAddressBook>
	{
		// Token: 0x060065A0 RID: 26016 RVA: 0x0009B398 File Offset: 0x00099598
		private UpdateOfflineAddressBookCommand() : base("Update-OfflineAddressBook")
		{
		}

		// Token: 0x060065A1 RID: 26017 RVA: 0x0009B3A5 File Offset: 0x000995A5
		public UpdateOfflineAddressBookCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060065A2 RID: 26018 RVA: 0x0009B3B4 File Offset: 0x000995B4
		public virtual UpdateOfflineAddressBookCommand SetParameters(UpdateOfflineAddressBookCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060065A3 RID: 26019 RVA: 0x0009B3BE File Offset: 0x000995BE
		public virtual UpdateOfflineAddressBookCommand SetParameters(UpdateOfflineAddressBookCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000801 RID: 2049
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004159 RID: 16729
			// (set) Token: 0x060065A4 RID: 26020 RVA: 0x0009B3C8 File Offset: 0x000995C8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700415A RID: 16730
			// (set) Token: 0x060065A5 RID: 26021 RVA: 0x0009B3DB File Offset: 0x000995DB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700415B RID: 16731
			// (set) Token: 0x060065A6 RID: 26022 RVA: 0x0009B3F3 File Offset: 0x000995F3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700415C RID: 16732
			// (set) Token: 0x060065A7 RID: 26023 RVA: 0x0009B40B File Offset: 0x0009960B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700415D RID: 16733
			// (set) Token: 0x060065A8 RID: 26024 RVA: 0x0009B423 File Offset: 0x00099623
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700415E RID: 16734
			// (set) Token: 0x060065A9 RID: 26025 RVA: 0x0009B43B File Offset: 0x0009963B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000802 RID: 2050
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700415F RID: 16735
			// (set) Token: 0x060065AB RID: 26027 RVA: 0x0009B45B File Offset: 0x0009965B
			public virtual OfflineAddressBookIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17004160 RID: 16736
			// (set) Token: 0x060065AC RID: 26028 RVA: 0x0009B46E File Offset: 0x0009966E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004161 RID: 16737
			// (set) Token: 0x060065AD RID: 26029 RVA: 0x0009B481 File Offset: 0x00099681
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004162 RID: 16738
			// (set) Token: 0x060065AE RID: 26030 RVA: 0x0009B499 File Offset: 0x00099699
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004163 RID: 16739
			// (set) Token: 0x060065AF RID: 26031 RVA: 0x0009B4B1 File Offset: 0x000996B1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004164 RID: 16740
			// (set) Token: 0x060065B0 RID: 26032 RVA: 0x0009B4C9 File Offset: 0x000996C9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004165 RID: 16741
			// (set) Token: 0x060065B1 RID: 26033 RVA: 0x0009B4E1 File Offset: 0x000996E1
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
