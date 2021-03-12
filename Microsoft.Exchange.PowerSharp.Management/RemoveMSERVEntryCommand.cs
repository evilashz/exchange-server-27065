using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020007E9 RID: 2025
	public class RemoveMSERVEntryCommand : SyntheticCommandWithPipelineInputNoOutput<Guid>
	{
		// Token: 0x060064CC RID: 25804 RVA: 0x0009A2AE File Offset: 0x000984AE
		private RemoveMSERVEntryCommand() : base("Remove-MSERVEntry")
		{
		}

		// Token: 0x060064CD RID: 25805 RVA: 0x0009A2BB File Offset: 0x000984BB
		public RemoveMSERVEntryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060064CE RID: 25806 RVA: 0x0009A2CA File Offset: 0x000984CA
		public virtual RemoveMSERVEntryCommand SetParameters(RemoveMSERVEntryCommand.ExternalDirectoryOrganizationIdParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060064CF RID: 25807 RVA: 0x0009A2D4 File Offset: 0x000984D4
		public virtual RemoveMSERVEntryCommand SetParameters(RemoveMSERVEntryCommand.DomainNameParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060064D0 RID: 25808 RVA: 0x0009A2DE File Offset: 0x000984DE
		public virtual RemoveMSERVEntryCommand SetParameters(RemoveMSERVEntryCommand.AddressParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020007EA RID: 2026
		public class ExternalDirectoryOrganizationIdParameterSetParameters : ParametersBase
		{
			// Token: 0x170040B3 RID: 16563
			// (set) Token: 0x060064D1 RID: 25809 RVA: 0x0009A2E8 File Offset: 0x000984E8
			public virtual Guid ExternalDirectoryOrganizationId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryOrganizationId"] = value;
				}
			}

			// Token: 0x170040B4 RID: 16564
			// (set) Token: 0x060064D2 RID: 25810 RVA: 0x0009A300 File Offset: 0x00098500
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170040B5 RID: 16565
			// (set) Token: 0x060064D3 RID: 25811 RVA: 0x0009A318 File Offset: 0x00098518
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170040B6 RID: 16566
			// (set) Token: 0x060064D4 RID: 25812 RVA: 0x0009A330 File Offset: 0x00098530
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170040B7 RID: 16567
			// (set) Token: 0x060064D5 RID: 25813 RVA: 0x0009A348 File Offset: 0x00098548
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170040B8 RID: 16568
			// (set) Token: 0x060064D6 RID: 25814 RVA: 0x0009A360 File Offset: 0x00098560
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170040B9 RID: 16569
			// (set) Token: 0x060064D7 RID: 25815 RVA: 0x0009A378 File Offset: 0x00098578
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020007EB RID: 2027
		public class DomainNameParameterSetParameters : ParametersBase
		{
			// Token: 0x170040BA RID: 16570
			// (set) Token: 0x060064D9 RID: 25817 RVA: 0x0009A398 File Offset: 0x00098598
			public virtual SmtpDomain DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x170040BB RID: 16571
			// (set) Token: 0x060064DA RID: 25818 RVA: 0x0009A3AB File Offset: 0x000985AB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170040BC RID: 16572
			// (set) Token: 0x060064DB RID: 25819 RVA: 0x0009A3C3 File Offset: 0x000985C3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170040BD RID: 16573
			// (set) Token: 0x060064DC RID: 25820 RVA: 0x0009A3DB File Offset: 0x000985DB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170040BE RID: 16574
			// (set) Token: 0x060064DD RID: 25821 RVA: 0x0009A3F3 File Offset: 0x000985F3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170040BF RID: 16575
			// (set) Token: 0x060064DE RID: 25822 RVA: 0x0009A40B File Offset: 0x0009860B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170040C0 RID: 16576
			// (set) Token: 0x060064DF RID: 25823 RVA: 0x0009A423 File Offset: 0x00098623
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020007EC RID: 2028
		public class AddressParameterSetParameters : ParametersBase
		{
			// Token: 0x170040C1 RID: 16577
			// (set) Token: 0x060064E1 RID: 25825 RVA: 0x0009A443 File Offset: 0x00098643
			public virtual string Address
			{
				set
				{
					base.PowerSharpParameters["Address"] = value;
				}
			}

			// Token: 0x170040C2 RID: 16578
			// (set) Token: 0x060064E2 RID: 25826 RVA: 0x0009A456 File Offset: 0x00098656
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170040C3 RID: 16579
			// (set) Token: 0x060064E3 RID: 25827 RVA: 0x0009A46E File Offset: 0x0009866E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170040C4 RID: 16580
			// (set) Token: 0x060064E4 RID: 25828 RVA: 0x0009A486 File Offset: 0x00098686
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170040C5 RID: 16581
			// (set) Token: 0x060064E5 RID: 25829 RVA: 0x0009A49E File Offset: 0x0009869E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170040C6 RID: 16582
			// (set) Token: 0x060064E6 RID: 25830 RVA: 0x0009A4B6 File Offset: 0x000986B6
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170040C7 RID: 16583
			// (set) Token: 0x060064E7 RID: 25831 RVA: 0x0009A4CE File Offset: 0x000986CE
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
