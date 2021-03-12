using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020007E5 RID: 2021
	public class NewMSERVEntryCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x060064AC RID: 25772 RVA: 0x0009A026 File Offset: 0x00098226
		private NewMSERVEntryCommand() : base("New-MSERVEntry")
		{
		}

		// Token: 0x060064AD RID: 25773 RVA: 0x0009A033 File Offset: 0x00098233
		public NewMSERVEntryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060064AE RID: 25774 RVA: 0x0009A042 File Offset: 0x00098242
		public virtual NewMSERVEntryCommand SetParameters(NewMSERVEntryCommand.AddressParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060064AF RID: 25775 RVA: 0x0009A04C File Offset: 0x0009824C
		public virtual NewMSERVEntryCommand SetParameters(NewMSERVEntryCommand.DomainNameParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060064B0 RID: 25776 RVA: 0x0009A056 File Offset: 0x00098256
		public virtual NewMSERVEntryCommand SetParameters(NewMSERVEntryCommand.ExternalDirectoryOrganizationIdParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020007E6 RID: 2022
		public class AddressParameterSetParameters : ParametersBase
		{
			// Token: 0x1700409B RID: 16539
			// (set) Token: 0x060064B1 RID: 25777 RVA: 0x0009A060 File Offset: 0x00098260
			public virtual int PartnerId
			{
				set
				{
					base.PowerSharpParameters["PartnerId"] = value;
				}
			}

			// Token: 0x1700409C RID: 16540
			// (set) Token: 0x060064B2 RID: 25778 RVA: 0x0009A078 File Offset: 0x00098278
			public virtual int MinorPartnerId
			{
				set
				{
					base.PowerSharpParameters["MinorPartnerId"] = value;
				}
			}

			// Token: 0x1700409D RID: 16541
			// (set) Token: 0x060064B3 RID: 25779 RVA: 0x0009A090 File Offset: 0x00098290
			public virtual string Address
			{
				set
				{
					base.PowerSharpParameters["Address"] = value;
				}
			}

			// Token: 0x1700409E RID: 16542
			// (set) Token: 0x060064B4 RID: 25780 RVA: 0x0009A0A3 File Offset: 0x000982A3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700409F RID: 16543
			// (set) Token: 0x060064B5 RID: 25781 RVA: 0x0009A0BB File Offset: 0x000982BB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170040A0 RID: 16544
			// (set) Token: 0x060064B6 RID: 25782 RVA: 0x0009A0D3 File Offset: 0x000982D3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170040A1 RID: 16545
			// (set) Token: 0x060064B7 RID: 25783 RVA: 0x0009A0EB File Offset: 0x000982EB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170040A2 RID: 16546
			// (set) Token: 0x060064B8 RID: 25784 RVA: 0x0009A103 File Offset: 0x00098303
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020007E7 RID: 2023
		public class DomainNameParameterSetParameters : ParametersBase
		{
			// Token: 0x170040A3 RID: 16547
			// (set) Token: 0x060064BA RID: 25786 RVA: 0x0009A123 File Offset: 0x00098323
			public virtual int PartnerId
			{
				set
				{
					base.PowerSharpParameters["PartnerId"] = value;
				}
			}

			// Token: 0x170040A4 RID: 16548
			// (set) Token: 0x060064BB RID: 25787 RVA: 0x0009A13B File Offset: 0x0009833B
			public virtual int MinorPartnerId
			{
				set
				{
					base.PowerSharpParameters["MinorPartnerId"] = value;
				}
			}

			// Token: 0x170040A5 RID: 16549
			// (set) Token: 0x060064BC RID: 25788 RVA: 0x0009A153 File Offset: 0x00098353
			public virtual SmtpDomain DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x170040A6 RID: 16550
			// (set) Token: 0x060064BD RID: 25789 RVA: 0x0009A166 File Offset: 0x00098366
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170040A7 RID: 16551
			// (set) Token: 0x060064BE RID: 25790 RVA: 0x0009A17E File Offset: 0x0009837E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170040A8 RID: 16552
			// (set) Token: 0x060064BF RID: 25791 RVA: 0x0009A196 File Offset: 0x00098396
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170040A9 RID: 16553
			// (set) Token: 0x060064C0 RID: 25792 RVA: 0x0009A1AE File Offset: 0x000983AE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170040AA RID: 16554
			// (set) Token: 0x060064C1 RID: 25793 RVA: 0x0009A1C6 File Offset: 0x000983C6
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020007E8 RID: 2024
		public class ExternalDirectoryOrganizationIdParameterSetParameters : ParametersBase
		{
			// Token: 0x170040AB RID: 16555
			// (set) Token: 0x060064C3 RID: 25795 RVA: 0x0009A1E6 File Offset: 0x000983E6
			public virtual int PartnerId
			{
				set
				{
					base.PowerSharpParameters["PartnerId"] = value;
				}
			}

			// Token: 0x170040AC RID: 16556
			// (set) Token: 0x060064C4 RID: 25796 RVA: 0x0009A1FE File Offset: 0x000983FE
			public virtual int MinorPartnerId
			{
				set
				{
					base.PowerSharpParameters["MinorPartnerId"] = value;
				}
			}

			// Token: 0x170040AD RID: 16557
			// (set) Token: 0x060064C5 RID: 25797 RVA: 0x0009A216 File Offset: 0x00098416
			public virtual Guid ExternalDirectoryOrganizationId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryOrganizationId"] = value;
				}
			}

			// Token: 0x170040AE RID: 16558
			// (set) Token: 0x060064C6 RID: 25798 RVA: 0x0009A22E File Offset: 0x0009842E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170040AF RID: 16559
			// (set) Token: 0x060064C7 RID: 25799 RVA: 0x0009A246 File Offset: 0x00098446
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170040B0 RID: 16560
			// (set) Token: 0x060064C8 RID: 25800 RVA: 0x0009A25E File Offset: 0x0009845E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170040B1 RID: 16561
			// (set) Token: 0x060064C9 RID: 25801 RVA: 0x0009A276 File Offset: 0x00098476
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170040B2 RID: 16562
			// (set) Token: 0x060064CA RID: 25802 RVA: 0x0009A28E File Offset: 0x0009848E
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
