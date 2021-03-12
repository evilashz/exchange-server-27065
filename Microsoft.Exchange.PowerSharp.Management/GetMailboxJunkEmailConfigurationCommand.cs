using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200049B RID: 1179
	public class GetMailboxJunkEmailConfigurationCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x0600424B RID: 16971 RVA: 0x0006DC48 File Offset: 0x0006BE48
		private GetMailboxJunkEmailConfigurationCommand() : base("Get-MailboxJunkEmailConfiguration")
		{
		}

		// Token: 0x0600424C RID: 16972 RVA: 0x0006DC55 File Offset: 0x0006BE55
		public GetMailboxJunkEmailConfigurationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600424D RID: 16973 RVA: 0x0006DC64 File Offset: 0x0006BE64
		public virtual GetMailboxJunkEmailConfigurationCommand SetParameters(GetMailboxJunkEmailConfigurationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600424E RID: 16974 RVA: 0x0006DC6E File Offset: 0x0006BE6E
		public virtual GetMailboxJunkEmailConfigurationCommand SetParameters(GetMailboxJunkEmailConfigurationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200049C RID: 1180
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170024CE RID: 9422
			// (set) Token: 0x0600424F RID: 16975 RVA: 0x0006DC78 File Offset: 0x0006BE78
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170024CF RID: 9423
			// (set) Token: 0x06004250 RID: 16976 RVA: 0x0006DC96 File Offset: 0x0006BE96
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170024D0 RID: 9424
			// (set) Token: 0x06004251 RID: 16977 RVA: 0x0006DCA9 File Offset: 0x0006BEA9
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170024D1 RID: 9425
			// (set) Token: 0x06004252 RID: 16978 RVA: 0x0006DCC1 File Offset: 0x0006BEC1
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170024D2 RID: 9426
			// (set) Token: 0x06004253 RID: 16979 RVA: 0x0006DCD9 File Offset: 0x0006BED9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170024D3 RID: 9427
			// (set) Token: 0x06004254 RID: 16980 RVA: 0x0006DCEC File Offset: 0x0006BEEC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170024D4 RID: 9428
			// (set) Token: 0x06004255 RID: 16981 RVA: 0x0006DD04 File Offset: 0x0006BF04
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170024D5 RID: 9429
			// (set) Token: 0x06004256 RID: 16982 RVA: 0x0006DD1C File Offset: 0x0006BF1C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170024D6 RID: 9430
			// (set) Token: 0x06004257 RID: 16983 RVA: 0x0006DD34 File Offset: 0x0006BF34
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200049D RID: 1181
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170024D7 RID: 9431
			// (set) Token: 0x06004259 RID: 16985 RVA: 0x0006DD54 File Offset: 0x0006BF54
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170024D8 RID: 9432
			// (set) Token: 0x0600425A RID: 16986 RVA: 0x0006DD67 File Offset: 0x0006BF67
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170024D9 RID: 9433
			// (set) Token: 0x0600425B RID: 16987 RVA: 0x0006DD7F File Offset: 0x0006BF7F
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170024DA RID: 9434
			// (set) Token: 0x0600425C RID: 16988 RVA: 0x0006DD97 File Offset: 0x0006BF97
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170024DB RID: 9435
			// (set) Token: 0x0600425D RID: 16989 RVA: 0x0006DDAA File Offset: 0x0006BFAA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170024DC RID: 9436
			// (set) Token: 0x0600425E RID: 16990 RVA: 0x0006DDC2 File Offset: 0x0006BFC2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170024DD RID: 9437
			// (set) Token: 0x0600425F RID: 16991 RVA: 0x0006DDDA File Offset: 0x0006BFDA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170024DE RID: 9438
			// (set) Token: 0x06004260 RID: 16992 RVA: 0x0006DDF2 File Offset: 0x0006BFF2
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
