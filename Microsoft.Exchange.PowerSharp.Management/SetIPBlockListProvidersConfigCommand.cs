using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000770 RID: 1904
	public class SetIPBlockListProvidersConfigCommand : SyntheticCommandWithPipelineInputNoOutput<IPBlockListProviderConfig>
	{
		// Token: 0x06006087 RID: 24711 RVA: 0x00094C17 File Offset: 0x00092E17
		private SetIPBlockListProvidersConfigCommand() : base("Set-IPBlockListProvidersConfig")
		{
		}

		// Token: 0x06006088 RID: 24712 RVA: 0x00094C24 File Offset: 0x00092E24
		public SetIPBlockListProvidersConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006089 RID: 24713 RVA: 0x00094C33 File Offset: 0x00092E33
		public virtual SetIPBlockListProvidersConfigCommand SetParameters(SetIPBlockListProvidersConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000771 RID: 1905
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003D60 RID: 15712
			// (set) Token: 0x0600608A RID: 24714 RVA: 0x00094C3D File Offset: 0x00092E3D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003D61 RID: 15713
			// (set) Token: 0x0600608B RID: 24715 RVA: 0x00094C50 File Offset: 0x00092E50
			public virtual MultiValuedProperty<SmtpAddress> BypassedRecipients
			{
				set
				{
					base.PowerSharpParameters["BypassedRecipients"] = value;
				}
			}

			// Token: 0x17003D62 RID: 15714
			// (set) Token: 0x0600608C RID: 24716 RVA: 0x00094C63 File Offset: 0x00092E63
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17003D63 RID: 15715
			// (set) Token: 0x0600608D RID: 24717 RVA: 0x00094C7B File Offset: 0x00092E7B
			public virtual bool ExternalMailEnabled
			{
				set
				{
					base.PowerSharpParameters["ExternalMailEnabled"] = value;
				}
			}

			// Token: 0x17003D64 RID: 15716
			// (set) Token: 0x0600608E RID: 24718 RVA: 0x00094C93 File Offset: 0x00092E93
			public virtual bool InternalMailEnabled
			{
				set
				{
					base.PowerSharpParameters["InternalMailEnabled"] = value;
				}
			}

			// Token: 0x17003D65 RID: 15717
			// (set) Token: 0x0600608F RID: 24719 RVA: 0x00094CAB File Offset: 0x00092EAB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003D66 RID: 15718
			// (set) Token: 0x06006090 RID: 24720 RVA: 0x00094CC3 File Offset: 0x00092EC3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003D67 RID: 15719
			// (set) Token: 0x06006091 RID: 24721 RVA: 0x00094CDB File Offset: 0x00092EDB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003D68 RID: 15720
			// (set) Token: 0x06006092 RID: 24722 RVA: 0x00094CF3 File Offset: 0x00092EF3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003D69 RID: 15721
			// (set) Token: 0x06006093 RID: 24723 RVA: 0x00094D0B File Offset: 0x00092F0B
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
