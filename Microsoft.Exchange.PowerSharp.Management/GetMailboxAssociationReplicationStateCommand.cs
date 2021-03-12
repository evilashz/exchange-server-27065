using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020001F4 RID: 500
	public class GetMailboxAssociationReplicationStateCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x0600288F RID: 10383 RVA: 0x0004C6C2 File Offset: 0x0004A8C2
		private GetMailboxAssociationReplicationStateCommand() : base("Get-MailboxAssociationReplicationState")
		{
		}

		// Token: 0x06002890 RID: 10384 RVA: 0x0004C6CF File Offset: 0x0004A8CF
		public GetMailboxAssociationReplicationStateCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002891 RID: 10385 RVA: 0x0004C6DE File Offset: 0x0004A8DE
		public virtual GetMailboxAssociationReplicationStateCommand SetParameters(GetMailboxAssociationReplicationStateCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020001F5 RID: 501
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001060 RID: 4192
			// (set) Token: 0x06002892 RID: 10386 RVA: 0x0004C6E8 File Offset: 0x0004A8E8
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17001061 RID: 4193
			// (set) Token: 0x06002893 RID: 10387 RVA: 0x0004C706 File Offset: 0x0004A906
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17001062 RID: 4194
			// (set) Token: 0x06002894 RID: 10388 RVA: 0x0004C719 File Offset: 0x0004A919
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001063 RID: 4195
			// (set) Token: 0x06002895 RID: 10389 RVA: 0x0004C731 File Offset: 0x0004A931
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17001064 RID: 4196
			// (set) Token: 0x06002896 RID: 10390 RVA: 0x0004C749 File Offset: 0x0004A949
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001065 RID: 4197
			// (set) Token: 0x06002897 RID: 10391 RVA: 0x0004C75C File Offset: 0x0004A95C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001066 RID: 4198
			// (set) Token: 0x06002898 RID: 10392 RVA: 0x0004C774 File Offset: 0x0004A974
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001067 RID: 4199
			// (set) Token: 0x06002899 RID: 10393 RVA: 0x0004C78C File Offset: 0x0004A98C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001068 RID: 4200
			// (set) Token: 0x0600289A RID: 10394 RVA: 0x0004C7A4 File Offset: 0x0004A9A4
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
