using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000D3F RID: 3391
	public class SetRecipientInternalOptionCommand : SyntheticCommandWithPipelineInputNoOutput<ADRecipient>
	{
		// Token: 0x0600B3A4 RID: 45988 RVA: 0x00102D96 File Offset: 0x00100F96
		private SetRecipientInternalOptionCommand() : base("Set-RecipientInternalOption")
		{
		}

		// Token: 0x0600B3A5 RID: 45989 RVA: 0x00102DA3 File Offset: 0x00100FA3
		public SetRecipientInternalOptionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600B3A6 RID: 45990 RVA: 0x00102DB2 File Offset: 0x00100FB2
		public virtual SetRecipientInternalOptionCommand SetParameters(SetRecipientInternalOptionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B3A7 RID: 45991 RVA: 0x00102DBC File Offset: 0x00100FBC
		public virtual SetRecipientInternalOptionCommand SetParameters(SetRecipientInternalOptionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000D40 RID: 3392
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170084DF RID: 34015
			// (set) Token: 0x0600B3A8 RID: 45992 RVA: 0x00102DC6 File Offset: 0x00100FC6
			public virtual SwitchParameter InternalOnly
			{
				set
				{
					base.PowerSharpParameters["InternalOnly"] = value;
				}
			}

			// Token: 0x170084E0 RID: 34016
			// (set) Token: 0x0600B3A9 RID: 45993 RVA: 0x00102DDE File Offset: 0x00100FDE
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170084E1 RID: 34017
			// (set) Token: 0x0600B3AA RID: 45994 RVA: 0x00102DF6 File Offset: 0x00100FF6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170084E2 RID: 34018
			// (set) Token: 0x0600B3AB RID: 45995 RVA: 0x00102E09 File Offset: 0x00101009
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170084E3 RID: 34019
			// (set) Token: 0x0600B3AC RID: 45996 RVA: 0x00102E1C File Offset: 0x0010101C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170084E4 RID: 34020
			// (set) Token: 0x0600B3AD RID: 45997 RVA: 0x00102E34 File Offset: 0x00101034
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170084E5 RID: 34021
			// (set) Token: 0x0600B3AE RID: 45998 RVA: 0x00102E4C File Offset: 0x0010104C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170084E6 RID: 34022
			// (set) Token: 0x0600B3AF RID: 45999 RVA: 0x00102E64 File Offset: 0x00101064
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170084E7 RID: 34023
			// (set) Token: 0x0600B3B0 RID: 46000 RVA: 0x00102E7C File Offset: 0x0010107C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D41 RID: 3393
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170084E8 RID: 34024
			// (set) Token: 0x0600B3B2 RID: 46002 RVA: 0x00102E9C File Offset: 0x0010109C
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x170084E9 RID: 34025
			// (set) Token: 0x0600B3B3 RID: 46003 RVA: 0x00102EBA File Offset: 0x001010BA
			public virtual SwitchParameter InternalOnly
			{
				set
				{
					base.PowerSharpParameters["InternalOnly"] = value;
				}
			}

			// Token: 0x170084EA RID: 34026
			// (set) Token: 0x0600B3B4 RID: 46004 RVA: 0x00102ED2 File Offset: 0x001010D2
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170084EB RID: 34027
			// (set) Token: 0x0600B3B5 RID: 46005 RVA: 0x00102EEA File Offset: 0x001010EA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170084EC RID: 34028
			// (set) Token: 0x0600B3B6 RID: 46006 RVA: 0x00102EFD File Offset: 0x001010FD
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170084ED RID: 34029
			// (set) Token: 0x0600B3B7 RID: 46007 RVA: 0x00102F10 File Offset: 0x00101110
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170084EE RID: 34030
			// (set) Token: 0x0600B3B8 RID: 46008 RVA: 0x00102F28 File Offset: 0x00101128
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170084EF RID: 34031
			// (set) Token: 0x0600B3B9 RID: 46009 RVA: 0x00102F40 File Offset: 0x00101140
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170084F0 RID: 34032
			// (set) Token: 0x0600B3BA RID: 46010 RVA: 0x00102F58 File Offset: 0x00101158
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170084F1 RID: 34033
			// (set) Token: 0x0600B3BB RID: 46011 RVA: 0x00102F70 File Offset: 0x00101170
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
