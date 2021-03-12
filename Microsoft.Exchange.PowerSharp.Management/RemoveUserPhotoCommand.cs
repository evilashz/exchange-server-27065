using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000BA6 RID: 2982
	public class RemoveUserPhotoCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x06009084 RID: 36996 RVA: 0x000D350C File Offset: 0x000D170C
		private RemoveUserPhotoCommand() : base("Remove-UserPhoto")
		{
		}

		// Token: 0x06009085 RID: 36997 RVA: 0x000D3519 File Offset: 0x000D1719
		public RemoveUserPhotoCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06009086 RID: 36998 RVA: 0x000D3528 File Offset: 0x000D1728
		public virtual RemoveUserPhotoCommand SetParameters(RemoveUserPhotoCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009087 RID: 36999 RVA: 0x000D3532 File Offset: 0x000D1732
		public virtual RemoveUserPhotoCommand SetParameters(RemoveUserPhotoCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000BA7 RID: 2983
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170064F1 RID: 25841
			// (set) Token: 0x06009088 RID: 37000 RVA: 0x000D353C File Offset: 0x000D173C
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170064F2 RID: 25842
			// (set) Token: 0x06009089 RID: 37001 RVA: 0x000D3554 File Offset: 0x000D1754
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170064F3 RID: 25843
			// (set) Token: 0x0600908A RID: 37002 RVA: 0x000D3567 File Offset: 0x000D1767
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170064F4 RID: 25844
			// (set) Token: 0x0600908B RID: 37003 RVA: 0x000D357F File Offset: 0x000D177F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170064F5 RID: 25845
			// (set) Token: 0x0600908C RID: 37004 RVA: 0x000D3597 File Offset: 0x000D1797
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170064F6 RID: 25846
			// (set) Token: 0x0600908D RID: 37005 RVA: 0x000D35AF File Offset: 0x000D17AF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170064F7 RID: 25847
			// (set) Token: 0x0600908E RID: 37006 RVA: 0x000D35C7 File Offset: 0x000D17C7
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170064F8 RID: 25848
			// (set) Token: 0x0600908F RID: 37007 RVA: 0x000D35DF File Offset: 0x000D17DF
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000BA8 RID: 2984
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170064F9 RID: 25849
			// (set) Token: 0x06009091 RID: 37009 RVA: 0x000D35FF File Offset: 0x000D17FF
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170064FA RID: 25850
			// (set) Token: 0x06009092 RID: 37010 RVA: 0x000D361D File Offset: 0x000D181D
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170064FB RID: 25851
			// (set) Token: 0x06009093 RID: 37011 RVA: 0x000D3635 File Offset: 0x000D1835
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170064FC RID: 25852
			// (set) Token: 0x06009094 RID: 37012 RVA: 0x000D3648 File Offset: 0x000D1848
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170064FD RID: 25853
			// (set) Token: 0x06009095 RID: 37013 RVA: 0x000D3660 File Offset: 0x000D1860
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170064FE RID: 25854
			// (set) Token: 0x06009096 RID: 37014 RVA: 0x000D3678 File Offset: 0x000D1878
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170064FF RID: 25855
			// (set) Token: 0x06009097 RID: 37015 RVA: 0x000D3690 File Offset: 0x000D1890
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006500 RID: 25856
			// (set) Token: 0x06009098 RID: 37016 RVA: 0x000D36A8 File Offset: 0x000D18A8
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17006501 RID: 25857
			// (set) Token: 0x06009099 RID: 37017 RVA: 0x000D36C0 File Offset: 0x000D18C0
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
