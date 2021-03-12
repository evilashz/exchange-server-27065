using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020002EB RID: 747
	public class GetAuthServerCommand : SyntheticCommandWithPipelineInput<AuthServer, AuthServer>
	{
		// Token: 0x060032B6 RID: 12982 RVA: 0x00059AEE File Offset: 0x00057CEE
		private GetAuthServerCommand() : base("Get-AuthServer")
		{
		}

		// Token: 0x060032B7 RID: 12983 RVA: 0x00059AFB File Offset: 0x00057CFB
		public GetAuthServerCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060032B8 RID: 12984 RVA: 0x00059B0A File Offset: 0x00057D0A
		public virtual GetAuthServerCommand SetParameters(GetAuthServerCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060032B9 RID: 12985 RVA: 0x00059B14 File Offset: 0x00057D14
		public virtual GetAuthServerCommand SetParameters(GetAuthServerCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020002EC RID: 748
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001899 RID: 6297
			// (set) Token: 0x060032BA RID: 12986 RVA: 0x00059B1E File Offset: 0x00057D1E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700189A RID: 6298
			// (set) Token: 0x060032BB RID: 12987 RVA: 0x00059B31 File Offset: 0x00057D31
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700189B RID: 6299
			// (set) Token: 0x060032BC RID: 12988 RVA: 0x00059B49 File Offset: 0x00057D49
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700189C RID: 6300
			// (set) Token: 0x060032BD RID: 12989 RVA: 0x00059B61 File Offset: 0x00057D61
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700189D RID: 6301
			// (set) Token: 0x060032BE RID: 12990 RVA: 0x00059B79 File Offset: 0x00057D79
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020002ED RID: 749
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700189E RID: 6302
			// (set) Token: 0x060032C0 RID: 12992 RVA: 0x00059B99 File Offset: 0x00057D99
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AuthServerIdParameter(value) : null);
				}
			}

			// Token: 0x1700189F RID: 6303
			// (set) Token: 0x060032C1 RID: 12993 RVA: 0x00059BB7 File Offset: 0x00057DB7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170018A0 RID: 6304
			// (set) Token: 0x060032C2 RID: 12994 RVA: 0x00059BCA File Offset: 0x00057DCA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170018A1 RID: 6305
			// (set) Token: 0x060032C3 RID: 12995 RVA: 0x00059BE2 File Offset: 0x00057DE2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170018A2 RID: 6306
			// (set) Token: 0x060032C4 RID: 12996 RVA: 0x00059BFA File Offset: 0x00057DFA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170018A3 RID: 6307
			// (set) Token: 0x060032C5 RID: 12997 RVA: 0x00059C12 File Offset: 0x00057E12
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
