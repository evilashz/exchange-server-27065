using System;
using System.Management.Automation;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020008ED RID: 2285
	public class GetTransportRuleActionCommand : SyntheticCommandWithPipelineInput<TransportRuleAction, TransportRuleAction>
	{
		// Token: 0x06007287 RID: 29319 RVA: 0x000AC5C8 File Offset: 0x000AA7C8
		private GetTransportRuleActionCommand() : base("Get-TransportRuleAction")
		{
		}

		// Token: 0x06007288 RID: 29320 RVA: 0x000AC5D5 File Offset: 0x000AA7D5
		public GetTransportRuleActionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007289 RID: 29321 RVA: 0x000AC5E4 File Offset: 0x000AA7E4
		public virtual GetTransportRuleActionCommand SetParameters(GetTransportRuleActionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020008EE RID: 2286
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004C66 RID: 19558
			// (set) Token: 0x0600728A RID: 29322 RVA: 0x000AC5EE File Offset: 0x000AA7EE
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004C67 RID: 19559
			// (set) Token: 0x0600728B RID: 29323 RVA: 0x000AC601 File Offset: 0x000AA801
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004C68 RID: 19560
			// (set) Token: 0x0600728C RID: 29324 RVA: 0x000AC619 File Offset: 0x000AA819
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004C69 RID: 19561
			// (set) Token: 0x0600728D RID: 29325 RVA: 0x000AC631 File Offset: 0x000AA831
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004C6A RID: 19562
			// (set) Token: 0x0600728E RID: 29326 RVA: 0x000AC649 File Offset: 0x000AA849
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
