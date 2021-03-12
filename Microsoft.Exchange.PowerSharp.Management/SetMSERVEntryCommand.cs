using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020007ED RID: 2029
	public class SetMSERVEntryCommand : SyntheticCommandWithPipelineInputNoOutput<Guid>
	{
		// Token: 0x060064E9 RID: 25833 RVA: 0x0009A4EE File Offset: 0x000986EE
		private SetMSERVEntryCommand() : base("Set-MSERVEntry")
		{
		}

		// Token: 0x060064EA RID: 25834 RVA: 0x0009A4FB File Offset: 0x000986FB
		public SetMSERVEntryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060064EB RID: 25835 RVA: 0x0009A50A File Offset: 0x0009870A
		public virtual SetMSERVEntryCommand SetParameters(SetMSERVEntryCommand.ExternalDirectoryOrganizationIdParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060064EC RID: 25836 RVA: 0x0009A514 File Offset: 0x00098714
		public virtual SetMSERVEntryCommand SetParameters(SetMSERVEntryCommand.AddressParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060064ED RID: 25837 RVA: 0x0009A51E File Offset: 0x0009871E
		public virtual SetMSERVEntryCommand SetParameters(SetMSERVEntryCommand.DomainNameParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020007EE RID: 2030
		public class ExternalDirectoryOrganizationIdParameterSetParameters : ParametersBase
		{
			// Token: 0x170040C8 RID: 16584
			// (set) Token: 0x060064EE RID: 25838 RVA: 0x0009A528 File Offset: 0x00098728
			public virtual int PartnerId
			{
				set
				{
					base.PowerSharpParameters["PartnerId"] = value;
				}
			}

			// Token: 0x170040C9 RID: 16585
			// (set) Token: 0x060064EF RID: 25839 RVA: 0x0009A540 File Offset: 0x00098740
			public virtual int MinorPartnerId
			{
				set
				{
					base.PowerSharpParameters["MinorPartnerId"] = value;
				}
			}

			// Token: 0x170040CA RID: 16586
			// (set) Token: 0x060064F0 RID: 25840 RVA: 0x0009A558 File Offset: 0x00098758
			public virtual Guid ExternalDirectoryOrganizationId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryOrganizationId"] = value;
				}
			}

			// Token: 0x170040CB RID: 16587
			// (set) Token: 0x060064F1 RID: 25841 RVA: 0x0009A570 File Offset: 0x00098770
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170040CC RID: 16588
			// (set) Token: 0x060064F2 RID: 25842 RVA: 0x0009A588 File Offset: 0x00098788
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170040CD RID: 16589
			// (set) Token: 0x060064F3 RID: 25843 RVA: 0x0009A5A0 File Offset: 0x000987A0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170040CE RID: 16590
			// (set) Token: 0x060064F4 RID: 25844 RVA: 0x0009A5B8 File Offset: 0x000987B8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170040CF RID: 16591
			// (set) Token: 0x060064F5 RID: 25845 RVA: 0x0009A5D0 File Offset: 0x000987D0
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170040D0 RID: 16592
			// (set) Token: 0x060064F6 RID: 25846 RVA: 0x0009A5E8 File Offset: 0x000987E8
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020007EF RID: 2031
		public class AddressParameterSetParameters : ParametersBase
		{
			// Token: 0x170040D1 RID: 16593
			// (set) Token: 0x060064F8 RID: 25848 RVA: 0x0009A608 File Offset: 0x00098808
			public virtual int PartnerId
			{
				set
				{
					base.PowerSharpParameters["PartnerId"] = value;
				}
			}

			// Token: 0x170040D2 RID: 16594
			// (set) Token: 0x060064F9 RID: 25849 RVA: 0x0009A620 File Offset: 0x00098820
			public virtual int MinorPartnerId
			{
				set
				{
					base.PowerSharpParameters["MinorPartnerId"] = value;
				}
			}

			// Token: 0x170040D3 RID: 16595
			// (set) Token: 0x060064FA RID: 25850 RVA: 0x0009A638 File Offset: 0x00098838
			public virtual string Address
			{
				set
				{
					base.PowerSharpParameters["Address"] = value;
				}
			}

			// Token: 0x170040D4 RID: 16596
			// (set) Token: 0x060064FB RID: 25851 RVA: 0x0009A64B File Offset: 0x0009884B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170040D5 RID: 16597
			// (set) Token: 0x060064FC RID: 25852 RVA: 0x0009A663 File Offset: 0x00098863
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170040D6 RID: 16598
			// (set) Token: 0x060064FD RID: 25853 RVA: 0x0009A67B File Offset: 0x0009887B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170040D7 RID: 16599
			// (set) Token: 0x060064FE RID: 25854 RVA: 0x0009A693 File Offset: 0x00098893
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170040D8 RID: 16600
			// (set) Token: 0x060064FF RID: 25855 RVA: 0x0009A6AB File Offset: 0x000988AB
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170040D9 RID: 16601
			// (set) Token: 0x06006500 RID: 25856 RVA: 0x0009A6C3 File Offset: 0x000988C3
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020007F0 RID: 2032
		public class DomainNameParameterSetParameters : ParametersBase
		{
			// Token: 0x170040DA RID: 16602
			// (set) Token: 0x06006502 RID: 25858 RVA: 0x0009A6E3 File Offset: 0x000988E3
			public virtual int PartnerId
			{
				set
				{
					base.PowerSharpParameters["PartnerId"] = value;
				}
			}

			// Token: 0x170040DB RID: 16603
			// (set) Token: 0x06006503 RID: 25859 RVA: 0x0009A6FB File Offset: 0x000988FB
			public virtual int MinorPartnerId
			{
				set
				{
					base.PowerSharpParameters["MinorPartnerId"] = value;
				}
			}

			// Token: 0x170040DC RID: 16604
			// (set) Token: 0x06006504 RID: 25860 RVA: 0x0009A713 File Offset: 0x00098913
			public virtual SmtpDomain DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x170040DD RID: 16605
			// (set) Token: 0x06006505 RID: 25861 RVA: 0x0009A726 File Offset: 0x00098926
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170040DE RID: 16606
			// (set) Token: 0x06006506 RID: 25862 RVA: 0x0009A73E File Offset: 0x0009893E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170040DF RID: 16607
			// (set) Token: 0x06006507 RID: 25863 RVA: 0x0009A756 File Offset: 0x00098956
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170040E0 RID: 16608
			// (set) Token: 0x06006508 RID: 25864 RVA: 0x0009A76E File Offset: 0x0009896E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170040E1 RID: 16609
			// (set) Token: 0x06006509 RID: 25865 RVA: 0x0009A786 File Offset: 0x00098986
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170040E2 RID: 16610
			// (set) Token: 0x0600650A RID: 25866 RVA: 0x0009A79E File Offset: 0x0009899E
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
