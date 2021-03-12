using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020007A1 RID: 1953
	public class SetSenderIdConfigCommand : SyntheticCommandWithPipelineInputNoOutput<SenderIdConfig>
	{
		// Token: 0x06006223 RID: 25123 RVA: 0x00096CAD File Offset: 0x00094EAD
		private SetSenderIdConfigCommand() : base("Set-SenderIdConfig")
		{
		}

		// Token: 0x06006224 RID: 25124 RVA: 0x00096CBA File Offset: 0x00094EBA
		public SetSenderIdConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006225 RID: 25125 RVA: 0x00096CC9 File Offset: 0x00094EC9
		public virtual SetSenderIdConfigCommand SetParameters(SetSenderIdConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020007A2 RID: 1954
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003E9A RID: 16026
			// (set) Token: 0x06006226 RID: 25126 RVA: 0x00096CD3 File Offset: 0x00094ED3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003E9B RID: 16027
			// (set) Token: 0x06006227 RID: 25127 RVA: 0x00096CE6 File Offset: 0x00094EE6
			public virtual SenderIdAction SpoofedDomainAction
			{
				set
				{
					base.PowerSharpParameters["SpoofedDomainAction"] = value;
				}
			}

			// Token: 0x17003E9C RID: 16028
			// (set) Token: 0x06006228 RID: 25128 RVA: 0x00096CFE File Offset: 0x00094EFE
			public virtual SenderIdAction TempErrorAction
			{
				set
				{
					base.PowerSharpParameters["TempErrorAction"] = value;
				}
			}

			// Token: 0x17003E9D RID: 16029
			// (set) Token: 0x06006229 RID: 25129 RVA: 0x00096D16 File Offset: 0x00094F16
			public virtual MultiValuedProperty<SmtpAddress> BypassedRecipients
			{
				set
				{
					base.PowerSharpParameters["BypassedRecipients"] = value;
				}
			}

			// Token: 0x17003E9E RID: 16030
			// (set) Token: 0x0600622A RID: 25130 RVA: 0x00096D29 File Offset: 0x00094F29
			public virtual MultiValuedProperty<SmtpDomain> BypassedSenderDomains
			{
				set
				{
					base.PowerSharpParameters["BypassedSenderDomains"] = value;
				}
			}

			// Token: 0x17003E9F RID: 16031
			// (set) Token: 0x0600622B RID: 25131 RVA: 0x00096D3C File Offset: 0x00094F3C
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17003EA0 RID: 16032
			// (set) Token: 0x0600622C RID: 25132 RVA: 0x00096D54 File Offset: 0x00094F54
			public virtual bool ExternalMailEnabled
			{
				set
				{
					base.PowerSharpParameters["ExternalMailEnabled"] = value;
				}
			}

			// Token: 0x17003EA1 RID: 16033
			// (set) Token: 0x0600622D RID: 25133 RVA: 0x00096D6C File Offset: 0x00094F6C
			public virtual bool InternalMailEnabled
			{
				set
				{
					base.PowerSharpParameters["InternalMailEnabled"] = value;
				}
			}

			// Token: 0x17003EA2 RID: 16034
			// (set) Token: 0x0600622E RID: 25134 RVA: 0x00096D84 File Offset: 0x00094F84
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003EA3 RID: 16035
			// (set) Token: 0x0600622F RID: 25135 RVA: 0x00096D9C File Offset: 0x00094F9C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003EA4 RID: 16036
			// (set) Token: 0x06006230 RID: 25136 RVA: 0x00096DB4 File Offset: 0x00094FB4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003EA5 RID: 16037
			// (set) Token: 0x06006231 RID: 25137 RVA: 0x00096DCC File Offset: 0x00094FCC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003EA6 RID: 16038
			// (set) Token: 0x06006232 RID: 25138 RVA: 0x00096DE4 File Offset: 0x00094FE4
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
