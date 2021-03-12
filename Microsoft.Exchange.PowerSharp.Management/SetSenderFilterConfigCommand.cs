using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200079D RID: 1949
	public class SetSenderFilterConfigCommand : SyntheticCommandWithPipelineInputNoOutput<SenderFilterConfig>
	{
		// Token: 0x06006207 RID: 25095 RVA: 0x00096A8A File Offset: 0x00094C8A
		private SetSenderFilterConfigCommand() : base("Set-SenderFilterConfig")
		{
		}

		// Token: 0x06006208 RID: 25096 RVA: 0x00096A97 File Offset: 0x00094C97
		public SetSenderFilterConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006209 RID: 25097 RVA: 0x00096AA6 File Offset: 0x00094CA6
		public virtual SetSenderFilterConfigCommand SetParameters(SetSenderFilterConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200079E RID: 1950
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003E86 RID: 16006
			// (set) Token: 0x0600620A RID: 25098 RVA: 0x00096AB0 File Offset: 0x00094CB0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003E87 RID: 16007
			// (set) Token: 0x0600620B RID: 25099 RVA: 0x00096AC3 File Offset: 0x00094CC3
			public virtual MultiValuedProperty<SmtpAddress> BlockedSenders
			{
				set
				{
					base.PowerSharpParameters["BlockedSenders"] = value;
				}
			}

			// Token: 0x17003E88 RID: 16008
			// (set) Token: 0x0600620C RID: 25100 RVA: 0x00096AD6 File Offset: 0x00094CD6
			public virtual MultiValuedProperty<SmtpDomain> BlockedDomains
			{
				set
				{
					base.PowerSharpParameters["BlockedDomains"] = value;
				}
			}

			// Token: 0x17003E89 RID: 16009
			// (set) Token: 0x0600620D RID: 25101 RVA: 0x00096AE9 File Offset: 0x00094CE9
			public virtual MultiValuedProperty<SmtpDomain> BlockedDomainsAndSubdomains
			{
				set
				{
					base.PowerSharpParameters["BlockedDomainsAndSubdomains"] = value;
				}
			}

			// Token: 0x17003E8A RID: 16010
			// (set) Token: 0x0600620E RID: 25102 RVA: 0x00096AFC File Offset: 0x00094CFC
			public virtual BlockedSenderAction Action
			{
				set
				{
					base.PowerSharpParameters["Action"] = value;
				}
			}

			// Token: 0x17003E8B RID: 16011
			// (set) Token: 0x0600620F RID: 25103 RVA: 0x00096B14 File Offset: 0x00094D14
			public virtual bool BlankSenderBlockingEnabled
			{
				set
				{
					base.PowerSharpParameters["BlankSenderBlockingEnabled"] = value;
				}
			}

			// Token: 0x17003E8C RID: 16012
			// (set) Token: 0x06006210 RID: 25104 RVA: 0x00096B2C File Offset: 0x00094D2C
			public virtual RecipientBlockedSenderAction RecipientBlockedSenderAction
			{
				set
				{
					base.PowerSharpParameters["RecipientBlockedSenderAction"] = value;
				}
			}

			// Token: 0x17003E8D RID: 16013
			// (set) Token: 0x06006211 RID: 25105 RVA: 0x00096B44 File Offset: 0x00094D44
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17003E8E RID: 16014
			// (set) Token: 0x06006212 RID: 25106 RVA: 0x00096B5C File Offset: 0x00094D5C
			public virtual bool ExternalMailEnabled
			{
				set
				{
					base.PowerSharpParameters["ExternalMailEnabled"] = value;
				}
			}

			// Token: 0x17003E8F RID: 16015
			// (set) Token: 0x06006213 RID: 25107 RVA: 0x00096B74 File Offset: 0x00094D74
			public virtual bool InternalMailEnabled
			{
				set
				{
					base.PowerSharpParameters["InternalMailEnabled"] = value;
				}
			}

			// Token: 0x17003E90 RID: 16016
			// (set) Token: 0x06006214 RID: 25108 RVA: 0x00096B8C File Offset: 0x00094D8C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003E91 RID: 16017
			// (set) Token: 0x06006215 RID: 25109 RVA: 0x00096BA4 File Offset: 0x00094DA4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003E92 RID: 16018
			// (set) Token: 0x06006216 RID: 25110 RVA: 0x00096BBC File Offset: 0x00094DBC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003E93 RID: 16019
			// (set) Token: 0x06006217 RID: 25111 RVA: 0x00096BD4 File Offset: 0x00094DD4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003E94 RID: 16020
			// (set) Token: 0x06006218 RID: 25112 RVA: 0x00096BEC File Offset: 0x00094DEC
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
