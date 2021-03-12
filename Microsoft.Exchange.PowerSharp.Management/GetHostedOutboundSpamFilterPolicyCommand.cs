using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200072B RID: 1835
	public class GetHostedOutboundSpamFilterPolicyCommand : SyntheticCommandWithPipelineInput<HostedOutboundSpamFilterPolicy, HostedOutboundSpamFilterPolicy>
	{
		// Token: 0x06005E8F RID: 24207 RVA: 0x0009256A File Offset: 0x0009076A
		private GetHostedOutboundSpamFilterPolicyCommand() : base("Get-HostedOutboundSpamFilterPolicy")
		{
		}

		// Token: 0x06005E90 RID: 24208 RVA: 0x00092577 File Offset: 0x00090777
		public GetHostedOutboundSpamFilterPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005E91 RID: 24209 RVA: 0x00092586 File Offset: 0x00090786
		public virtual GetHostedOutboundSpamFilterPolicyCommand SetParameters(GetHostedOutboundSpamFilterPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005E92 RID: 24210 RVA: 0x00092590 File Offset: 0x00090790
		public virtual GetHostedOutboundSpamFilterPolicyCommand SetParameters(GetHostedOutboundSpamFilterPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200072C RID: 1836
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003BF2 RID: 15346
			// (set) Token: 0x06005E93 RID: 24211 RVA: 0x0009259A File Offset: 0x0009079A
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17003BF3 RID: 15347
			// (set) Token: 0x06005E94 RID: 24212 RVA: 0x000925B2 File Offset: 0x000907B2
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17003BF4 RID: 15348
			// (set) Token: 0x06005E95 RID: 24213 RVA: 0x000925D0 File Offset: 0x000907D0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003BF5 RID: 15349
			// (set) Token: 0x06005E96 RID: 24214 RVA: 0x000925E3 File Offset: 0x000907E3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003BF6 RID: 15350
			// (set) Token: 0x06005E97 RID: 24215 RVA: 0x000925FB File Offset: 0x000907FB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003BF7 RID: 15351
			// (set) Token: 0x06005E98 RID: 24216 RVA: 0x00092613 File Offset: 0x00090813
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003BF8 RID: 15352
			// (set) Token: 0x06005E99 RID: 24217 RVA: 0x0009262B File Offset: 0x0009082B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200072D RID: 1837
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003BF9 RID: 15353
			// (set) Token: 0x06005E9B RID: 24219 RVA: 0x0009264B File Offset: 0x0009084B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new HostedOutboundSpamFilterPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17003BFA RID: 15354
			// (set) Token: 0x06005E9C RID: 24220 RVA: 0x00092669 File Offset: 0x00090869
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17003BFB RID: 15355
			// (set) Token: 0x06005E9D RID: 24221 RVA: 0x00092681 File Offset: 0x00090881
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17003BFC RID: 15356
			// (set) Token: 0x06005E9E RID: 24222 RVA: 0x0009269F File Offset: 0x0009089F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003BFD RID: 15357
			// (set) Token: 0x06005E9F RID: 24223 RVA: 0x000926B2 File Offset: 0x000908B2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003BFE RID: 15358
			// (set) Token: 0x06005EA0 RID: 24224 RVA: 0x000926CA File Offset: 0x000908CA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003BFF RID: 15359
			// (set) Token: 0x06005EA1 RID: 24225 RVA: 0x000926E2 File Offset: 0x000908E2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003C00 RID: 15360
			// (set) Token: 0x06005EA2 RID: 24226 RVA: 0x000926FA File Offset: 0x000908FA
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
