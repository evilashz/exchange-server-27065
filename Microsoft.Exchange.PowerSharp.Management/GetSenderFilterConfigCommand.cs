using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200079A RID: 1946
	public class GetSenderFilterConfigCommand : SyntheticCommandWithPipelineInput<SenderFilterConfig, SenderFilterConfig>
	{
		// Token: 0x060061F6 RID: 25078 RVA: 0x00096946 File Offset: 0x00094B46
		private GetSenderFilterConfigCommand() : base("Get-SenderFilterConfig")
		{
		}

		// Token: 0x060061F7 RID: 25079 RVA: 0x00096953 File Offset: 0x00094B53
		public GetSenderFilterConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060061F8 RID: 25080 RVA: 0x00096962 File Offset: 0x00094B62
		public virtual GetSenderFilterConfigCommand SetParameters(GetSenderFilterConfigCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060061F9 RID: 25081 RVA: 0x0009696C File Offset: 0x00094B6C
		public virtual GetSenderFilterConfigCommand SetParameters(GetSenderFilterConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200079B RID: 1947
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003E7B RID: 15995
			// (set) Token: 0x060061FA RID: 25082 RVA: 0x00096976 File Offset: 0x00094B76
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17003E7C RID: 15996
			// (set) Token: 0x060061FB RID: 25083 RVA: 0x00096994 File Offset: 0x00094B94
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003E7D RID: 15997
			// (set) Token: 0x060061FC RID: 25084 RVA: 0x000969A7 File Offset: 0x00094BA7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003E7E RID: 15998
			// (set) Token: 0x060061FD RID: 25085 RVA: 0x000969BF File Offset: 0x00094BBF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003E7F RID: 15999
			// (set) Token: 0x060061FE RID: 25086 RVA: 0x000969D7 File Offset: 0x00094BD7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003E80 RID: 16000
			// (set) Token: 0x060061FF RID: 25087 RVA: 0x000969EF File Offset: 0x00094BEF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200079C RID: 1948
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003E81 RID: 16001
			// (set) Token: 0x06006201 RID: 25089 RVA: 0x00096A0F File Offset: 0x00094C0F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003E82 RID: 16002
			// (set) Token: 0x06006202 RID: 25090 RVA: 0x00096A22 File Offset: 0x00094C22
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003E83 RID: 16003
			// (set) Token: 0x06006203 RID: 25091 RVA: 0x00096A3A File Offset: 0x00094C3A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003E84 RID: 16004
			// (set) Token: 0x06006204 RID: 25092 RVA: 0x00096A52 File Offset: 0x00094C52
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003E85 RID: 16005
			// (set) Token: 0x06006205 RID: 25093 RVA: 0x00096A6A File Offset: 0x00094C6A
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
