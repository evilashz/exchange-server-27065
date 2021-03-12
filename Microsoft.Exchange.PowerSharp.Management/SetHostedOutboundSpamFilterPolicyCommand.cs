using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000731 RID: 1841
	public class SetHostedOutboundSpamFilterPolicyCommand : SyntheticCommandWithPipelineInputNoOutput<HostedOutboundSpamFilterPolicy>
	{
		// Token: 0x06005EB5 RID: 24245 RVA: 0x0009285E File Offset: 0x00090A5E
		private SetHostedOutboundSpamFilterPolicyCommand() : base("Set-HostedOutboundSpamFilterPolicy")
		{
		}

		// Token: 0x06005EB6 RID: 24246 RVA: 0x0009286B File Offset: 0x00090A6B
		public SetHostedOutboundSpamFilterPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005EB7 RID: 24247 RVA: 0x0009287A File Offset: 0x00090A7A
		public virtual SetHostedOutboundSpamFilterPolicyCommand SetParameters(SetHostedOutboundSpamFilterPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005EB8 RID: 24248 RVA: 0x00092884 File Offset: 0x00090A84
		public virtual SetHostedOutboundSpamFilterPolicyCommand SetParameters(SetHostedOutboundSpamFilterPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000732 RID: 1842
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003C0C RID: 15372
			// (set) Token: 0x06005EB9 RID: 24249 RVA: 0x0009288E File Offset: 0x00090A8E
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17003C0D RID: 15373
			// (set) Token: 0x06005EBA RID: 24250 RVA: 0x000928A6 File Offset: 0x00090AA6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003C0E RID: 15374
			// (set) Token: 0x06005EBB RID: 24251 RVA: 0x000928B9 File Offset: 0x00090AB9
			public virtual string AdminDisplayName
			{
				set
				{
					base.PowerSharpParameters["AdminDisplayName"] = value;
				}
			}

			// Token: 0x17003C0F RID: 15375
			// (set) Token: 0x06005EBC RID: 24252 RVA: 0x000928CC File Offset: 0x00090ACC
			public virtual MultiValuedProperty<SmtpAddress> NotifyOutboundSpamRecipients
			{
				set
				{
					base.PowerSharpParameters["NotifyOutboundSpamRecipients"] = value;
				}
			}

			// Token: 0x17003C10 RID: 15376
			// (set) Token: 0x06005EBD RID: 24253 RVA: 0x000928DF File Offset: 0x00090ADF
			public virtual MultiValuedProperty<SmtpAddress> BccSuspiciousOutboundAdditionalRecipients
			{
				set
				{
					base.PowerSharpParameters["BccSuspiciousOutboundAdditionalRecipients"] = value;
				}
			}

			// Token: 0x17003C11 RID: 15377
			// (set) Token: 0x06005EBE RID: 24254 RVA: 0x000928F2 File Offset: 0x00090AF2
			public virtual bool BccSuspiciousOutboundMail
			{
				set
				{
					base.PowerSharpParameters["BccSuspiciousOutboundMail"] = value;
				}
			}

			// Token: 0x17003C12 RID: 15378
			// (set) Token: 0x06005EBF RID: 24255 RVA: 0x0009290A File Offset: 0x00090B0A
			public virtual bool NotifyOutboundSpam
			{
				set
				{
					base.PowerSharpParameters["NotifyOutboundSpam"] = value;
				}
			}

			// Token: 0x17003C13 RID: 15379
			// (set) Token: 0x06005EC0 RID: 24256 RVA: 0x00092922 File Offset: 0x00090B22
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17003C14 RID: 15380
			// (set) Token: 0x06005EC1 RID: 24257 RVA: 0x00092935 File Offset: 0x00090B35
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003C15 RID: 15381
			// (set) Token: 0x06005EC2 RID: 24258 RVA: 0x0009294D File Offset: 0x00090B4D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003C16 RID: 15382
			// (set) Token: 0x06005EC3 RID: 24259 RVA: 0x00092965 File Offset: 0x00090B65
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003C17 RID: 15383
			// (set) Token: 0x06005EC4 RID: 24260 RVA: 0x0009297D File Offset: 0x00090B7D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003C18 RID: 15384
			// (set) Token: 0x06005EC5 RID: 24261 RVA: 0x00092995 File Offset: 0x00090B95
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000733 RID: 1843
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003C19 RID: 15385
			// (set) Token: 0x06005EC7 RID: 24263 RVA: 0x000929B5 File Offset: 0x00090BB5
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new HostedOutboundSpamFilterPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17003C1A RID: 15386
			// (set) Token: 0x06005EC8 RID: 24264 RVA: 0x000929D3 File Offset: 0x00090BD3
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17003C1B RID: 15387
			// (set) Token: 0x06005EC9 RID: 24265 RVA: 0x000929EB File Offset: 0x00090BEB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003C1C RID: 15388
			// (set) Token: 0x06005ECA RID: 24266 RVA: 0x000929FE File Offset: 0x00090BFE
			public virtual string AdminDisplayName
			{
				set
				{
					base.PowerSharpParameters["AdminDisplayName"] = value;
				}
			}

			// Token: 0x17003C1D RID: 15389
			// (set) Token: 0x06005ECB RID: 24267 RVA: 0x00092A11 File Offset: 0x00090C11
			public virtual MultiValuedProperty<SmtpAddress> NotifyOutboundSpamRecipients
			{
				set
				{
					base.PowerSharpParameters["NotifyOutboundSpamRecipients"] = value;
				}
			}

			// Token: 0x17003C1E RID: 15390
			// (set) Token: 0x06005ECC RID: 24268 RVA: 0x00092A24 File Offset: 0x00090C24
			public virtual MultiValuedProperty<SmtpAddress> BccSuspiciousOutboundAdditionalRecipients
			{
				set
				{
					base.PowerSharpParameters["BccSuspiciousOutboundAdditionalRecipients"] = value;
				}
			}

			// Token: 0x17003C1F RID: 15391
			// (set) Token: 0x06005ECD RID: 24269 RVA: 0x00092A37 File Offset: 0x00090C37
			public virtual bool BccSuspiciousOutboundMail
			{
				set
				{
					base.PowerSharpParameters["BccSuspiciousOutboundMail"] = value;
				}
			}

			// Token: 0x17003C20 RID: 15392
			// (set) Token: 0x06005ECE RID: 24270 RVA: 0x00092A4F File Offset: 0x00090C4F
			public virtual bool NotifyOutboundSpam
			{
				set
				{
					base.PowerSharpParameters["NotifyOutboundSpam"] = value;
				}
			}

			// Token: 0x17003C21 RID: 15393
			// (set) Token: 0x06005ECF RID: 24271 RVA: 0x00092A67 File Offset: 0x00090C67
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17003C22 RID: 15394
			// (set) Token: 0x06005ED0 RID: 24272 RVA: 0x00092A7A File Offset: 0x00090C7A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003C23 RID: 15395
			// (set) Token: 0x06005ED1 RID: 24273 RVA: 0x00092A92 File Offset: 0x00090C92
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003C24 RID: 15396
			// (set) Token: 0x06005ED2 RID: 24274 RVA: 0x00092AAA File Offset: 0x00090CAA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003C25 RID: 15397
			// (set) Token: 0x06005ED3 RID: 24275 RVA: 0x00092AC2 File Offset: 0x00090CC2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003C26 RID: 15398
			// (set) Token: 0x06005ED4 RID: 24276 RVA: 0x00092ADA File Offset: 0x00090CDA
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
