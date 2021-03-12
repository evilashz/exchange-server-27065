using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C8D RID: 3213
	public class GetRemovedMailboxCommand : SyntheticCommandWithPipelineInput<RemovedMailbox, RemovedMailbox>
	{
		// Token: 0x06009EA0 RID: 40608 RVA: 0x000E5F58 File Offset: 0x000E4158
		private GetRemovedMailboxCommand() : base("Get-RemovedMailbox")
		{
		}

		// Token: 0x06009EA1 RID: 40609 RVA: 0x000E5F65 File Offset: 0x000E4165
		public GetRemovedMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06009EA2 RID: 40610 RVA: 0x000E5F74 File Offset: 0x000E4174
		public virtual GetRemovedMailboxCommand SetParameters(GetRemovedMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009EA3 RID: 40611 RVA: 0x000E5F7E File Offset: 0x000E417E
		public virtual GetRemovedMailboxCommand SetParameters(GetRemovedMailboxCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C8E RID: 3214
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700713F RID: 28991
			// (set) Token: 0x06009EA4 RID: 40612 RVA: 0x000E5F88 File Offset: 0x000E4188
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007140 RID: 28992
			// (set) Token: 0x06009EA5 RID: 40613 RVA: 0x000E5FA6 File Offset: 0x000E41A6
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17007141 RID: 28993
			// (set) Token: 0x06009EA6 RID: 40614 RVA: 0x000E5FBE File Offset: 0x000E41BE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007142 RID: 28994
			// (set) Token: 0x06009EA7 RID: 40615 RVA: 0x000E5FD1 File Offset: 0x000E41D1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007143 RID: 28995
			// (set) Token: 0x06009EA8 RID: 40616 RVA: 0x000E5FE9 File Offset: 0x000E41E9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007144 RID: 28996
			// (set) Token: 0x06009EA9 RID: 40617 RVA: 0x000E6001 File Offset: 0x000E4201
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007145 RID: 28997
			// (set) Token: 0x06009EAA RID: 40618 RVA: 0x000E6019 File Offset: 0x000E4219
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000C8F RID: 3215
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17007146 RID: 28998
			// (set) Token: 0x06009EAC RID: 40620 RVA: 0x000E6039 File Offset: 0x000E4239
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RemovedMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007147 RID: 28999
			// (set) Token: 0x06009EAD RID: 40621 RVA: 0x000E6057 File Offset: 0x000E4257
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007148 RID: 29000
			// (set) Token: 0x06009EAE RID: 40622 RVA: 0x000E6075 File Offset: 0x000E4275
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17007149 RID: 29001
			// (set) Token: 0x06009EAF RID: 40623 RVA: 0x000E608D File Offset: 0x000E428D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700714A RID: 29002
			// (set) Token: 0x06009EB0 RID: 40624 RVA: 0x000E60A0 File Offset: 0x000E42A0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700714B RID: 29003
			// (set) Token: 0x06009EB1 RID: 40625 RVA: 0x000E60B8 File Offset: 0x000E42B8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700714C RID: 29004
			// (set) Token: 0x06009EB2 RID: 40626 RVA: 0x000E60D0 File Offset: 0x000E42D0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700714D RID: 29005
			// (set) Token: 0x06009EB3 RID: 40627 RVA: 0x000E60E8 File Offset: 0x000E42E8
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
