using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200062C RID: 1580
	public class GetEdgeSubscriptionCommand : SyntheticCommandWithPipelineInput<Server, Server>
	{
		// Token: 0x0600507E RID: 20606 RVA: 0x0007F926 File Offset: 0x0007DB26
		private GetEdgeSubscriptionCommand() : base("Get-EdgeSubscription")
		{
		}

		// Token: 0x0600507F RID: 20607 RVA: 0x0007F933 File Offset: 0x0007DB33
		public GetEdgeSubscriptionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005080 RID: 20608 RVA: 0x0007F942 File Offset: 0x0007DB42
		public virtual GetEdgeSubscriptionCommand SetParameters(GetEdgeSubscriptionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005081 RID: 20609 RVA: 0x0007F94C File Offset: 0x0007DB4C
		public virtual GetEdgeSubscriptionCommand SetParameters(GetEdgeSubscriptionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200062D RID: 1581
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002FDF RID: 12255
			// (set) Token: 0x06005082 RID: 20610 RVA: 0x0007F956 File Offset: 0x0007DB56
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002FE0 RID: 12256
			// (set) Token: 0x06005083 RID: 20611 RVA: 0x0007F969 File Offset: 0x0007DB69
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002FE1 RID: 12257
			// (set) Token: 0x06005084 RID: 20612 RVA: 0x0007F981 File Offset: 0x0007DB81
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002FE2 RID: 12258
			// (set) Token: 0x06005085 RID: 20613 RVA: 0x0007F999 File Offset: 0x0007DB99
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002FE3 RID: 12259
			// (set) Token: 0x06005086 RID: 20614 RVA: 0x0007F9B1 File Offset: 0x0007DBB1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200062E RID: 1582
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002FE4 RID: 12260
			// (set) Token: 0x06005088 RID: 20616 RVA: 0x0007F9D1 File Offset: 0x0007DBD1
			public virtual TransportServerIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002FE5 RID: 12261
			// (set) Token: 0x06005089 RID: 20617 RVA: 0x0007F9E4 File Offset: 0x0007DBE4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002FE6 RID: 12262
			// (set) Token: 0x0600508A RID: 20618 RVA: 0x0007F9F7 File Offset: 0x0007DBF7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002FE7 RID: 12263
			// (set) Token: 0x0600508B RID: 20619 RVA: 0x0007FA0F File Offset: 0x0007DC0F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002FE8 RID: 12264
			// (set) Token: 0x0600508C RID: 20620 RVA: 0x0007FA27 File Offset: 0x0007DC27
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002FE9 RID: 12265
			// (set) Token: 0x0600508D RID: 20621 RVA: 0x0007FA3F File Offset: 0x0007DC3F
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
