using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200081B RID: 2075
	public class SetMServSyncConfigFlagsCommand : SyntheticCommandWithPipelineInputNoOutput<ADOrganizationalUnit>
	{
		// Token: 0x06006662 RID: 26210 RVA: 0x0009C255 File Offset: 0x0009A455
		private SetMServSyncConfigFlagsCommand() : base("Set-MServSyncConfigFlags")
		{
		}

		// Token: 0x06006663 RID: 26211 RVA: 0x0009C262 File Offset: 0x0009A462
		public SetMServSyncConfigFlagsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006664 RID: 26212 RVA: 0x0009C271 File Offset: 0x0009A471
		public virtual SetMServSyncConfigFlagsCommand SetParameters(SetMServSyncConfigFlagsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006665 RID: 26213 RVA: 0x0009C27B File Offset: 0x0009A47B
		public virtual SetMServSyncConfigFlagsCommand SetParameters(SetMServSyncConfigFlagsCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200081C RID: 2076
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170041E5 RID: 16869
			// (set) Token: 0x06006666 RID: 26214 RVA: 0x0009C285 File Offset: 0x0009A485
			public virtual SwitchParameter MSOSyncEnabled
			{
				set
				{
					base.PowerSharpParameters["MSOSyncEnabled"] = value;
				}
			}

			// Token: 0x170041E6 RID: 16870
			// (set) Token: 0x06006667 RID: 26215 RVA: 0x0009C29D File Offset: 0x0009A49D
			public virtual SwitchParameter SMTPAddressCheckWithAcceptedDomain
			{
				set
				{
					base.PowerSharpParameters["SMTPAddressCheckWithAcceptedDomain"] = value;
				}
			}

			// Token: 0x170041E7 RID: 16871
			// (set) Token: 0x06006668 RID: 26216 RVA: 0x0009C2B5 File Offset: 0x0009A4B5
			public virtual SwitchParameter SyncMBXAndDLToMserv
			{
				set
				{
					base.PowerSharpParameters["SyncMBXAndDLToMserv"] = value;
				}
			}

			// Token: 0x170041E8 RID: 16872
			// (set) Token: 0x06006669 RID: 26217 RVA: 0x0009C2CD File Offset: 0x0009A4CD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170041E9 RID: 16873
			// (set) Token: 0x0600666A RID: 26218 RVA: 0x0009C2E0 File Offset: 0x0009A4E0
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170041EA RID: 16874
			// (set) Token: 0x0600666B RID: 26219 RVA: 0x0009C2F3 File Offset: 0x0009A4F3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170041EB RID: 16875
			// (set) Token: 0x0600666C RID: 26220 RVA: 0x0009C30B File Offset: 0x0009A50B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170041EC RID: 16876
			// (set) Token: 0x0600666D RID: 26221 RVA: 0x0009C323 File Offset: 0x0009A523
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170041ED RID: 16877
			// (set) Token: 0x0600666E RID: 26222 RVA: 0x0009C33B File Offset: 0x0009A53B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170041EE RID: 16878
			// (set) Token: 0x0600666F RID: 26223 RVA: 0x0009C353 File Offset: 0x0009A553
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200081D RID: 2077
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170041EF RID: 16879
			// (set) Token: 0x06006671 RID: 26225 RVA: 0x0009C373 File Offset: 0x0009A573
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170041F0 RID: 16880
			// (set) Token: 0x06006672 RID: 26226 RVA: 0x0009C391 File Offset: 0x0009A591
			public virtual SwitchParameter MSOSyncEnabled
			{
				set
				{
					base.PowerSharpParameters["MSOSyncEnabled"] = value;
				}
			}

			// Token: 0x170041F1 RID: 16881
			// (set) Token: 0x06006673 RID: 26227 RVA: 0x0009C3A9 File Offset: 0x0009A5A9
			public virtual SwitchParameter SMTPAddressCheckWithAcceptedDomain
			{
				set
				{
					base.PowerSharpParameters["SMTPAddressCheckWithAcceptedDomain"] = value;
				}
			}

			// Token: 0x170041F2 RID: 16882
			// (set) Token: 0x06006674 RID: 26228 RVA: 0x0009C3C1 File Offset: 0x0009A5C1
			public virtual SwitchParameter SyncMBXAndDLToMserv
			{
				set
				{
					base.PowerSharpParameters["SyncMBXAndDLToMserv"] = value;
				}
			}

			// Token: 0x170041F3 RID: 16883
			// (set) Token: 0x06006675 RID: 26229 RVA: 0x0009C3D9 File Offset: 0x0009A5D9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170041F4 RID: 16884
			// (set) Token: 0x06006676 RID: 26230 RVA: 0x0009C3EC File Offset: 0x0009A5EC
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170041F5 RID: 16885
			// (set) Token: 0x06006677 RID: 26231 RVA: 0x0009C3FF File Offset: 0x0009A5FF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170041F6 RID: 16886
			// (set) Token: 0x06006678 RID: 26232 RVA: 0x0009C417 File Offset: 0x0009A617
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170041F7 RID: 16887
			// (set) Token: 0x06006679 RID: 26233 RVA: 0x0009C42F File Offset: 0x0009A62F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170041F8 RID: 16888
			// (set) Token: 0x0600667A RID: 26234 RVA: 0x0009C447 File Offset: 0x0009A647
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170041F9 RID: 16889
			// (set) Token: 0x0600667B RID: 26235 RVA: 0x0009C45F File Offset: 0x0009A65F
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
