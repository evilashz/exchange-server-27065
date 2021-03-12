using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200064C RID: 1612
	public class NewExchangeServerCommand : SyntheticCommandWithPipelineInput<Server, Server>
	{
		// Token: 0x06005138 RID: 20792 RVA: 0x000806BE File Offset: 0x0007E8BE
		private NewExchangeServerCommand() : base("New-ExchangeServer")
		{
		}

		// Token: 0x06005139 RID: 20793 RVA: 0x000806CB File Offset: 0x0007E8CB
		public NewExchangeServerCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600513A RID: 20794 RVA: 0x000806DA File Offset: 0x0007E8DA
		public virtual NewExchangeServerCommand SetParameters(NewExchangeServerCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200064D RID: 1613
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003059 RID: 12377
			// (set) Token: 0x0600513B RID: 20795 RVA: 0x000806E4 File Offset: 0x0007E8E4
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700305A RID: 12378
			// (set) Token: 0x0600513C RID: 20796 RVA: 0x000806F7 File Offset: 0x0007E8F7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700305B RID: 12379
			// (set) Token: 0x0600513D RID: 20797 RVA: 0x0008070A File Offset: 0x0007E90A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700305C RID: 12380
			// (set) Token: 0x0600513E RID: 20798 RVA: 0x00080722 File Offset: 0x0007E922
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700305D RID: 12381
			// (set) Token: 0x0600513F RID: 20799 RVA: 0x0008073A File Offset: 0x0007E93A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700305E RID: 12382
			// (set) Token: 0x06005140 RID: 20800 RVA: 0x00080752 File Offset: 0x0007E952
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
