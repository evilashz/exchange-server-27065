using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000884 RID: 2180
	public class GetDeliveryAgentConnectorCommand : SyntheticCommandWithPipelineInput<DeliveryAgentConnector, DeliveryAgentConnector>
	{
		// Token: 0x06006CBB RID: 27835 RVA: 0x000A4B3B File Offset: 0x000A2D3B
		private GetDeliveryAgentConnectorCommand() : base("Get-DeliveryAgentConnector")
		{
		}

		// Token: 0x06006CBC RID: 27836 RVA: 0x000A4B48 File Offset: 0x000A2D48
		public GetDeliveryAgentConnectorCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006CBD RID: 27837 RVA: 0x000A4B57 File Offset: 0x000A2D57
		public virtual GetDeliveryAgentConnectorCommand SetParameters(GetDeliveryAgentConnectorCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006CBE RID: 27838 RVA: 0x000A4B61 File Offset: 0x000A2D61
		public virtual GetDeliveryAgentConnectorCommand SetParameters(GetDeliveryAgentConnectorCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000885 RID: 2181
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700476C RID: 18284
			// (set) Token: 0x06006CBF RID: 27839 RVA: 0x000A4B6B File Offset: 0x000A2D6B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700476D RID: 18285
			// (set) Token: 0x06006CC0 RID: 27840 RVA: 0x000A4B7E File Offset: 0x000A2D7E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700476E RID: 18286
			// (set) Token: 0x06006CC1 RID: 27841 RVA: 0x000A4B96 File Offset: 0x000A2D96
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700476F RID: 18287
			// (set) Token: 0x06006CC2 RID: 27842 RVA: 0x000A4BAE File Offset: 0x000A2DAE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004770 RID: 18288
			// (set) Token: 0x06006CC3 RID: 27843 RVA: 0x000A4BC6 File Offset: 0x000A2DC6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000886 RID: 2182
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004771 RID: 18289
			// (set) Token: 0x06006CC5 RID: 27845 RVA: 0x000A4BE6 File Offset: 0x000A2DE6
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new DeliveryAgentConnectorIdParameter(value) : null);
				}
			}

			// Token: 0x17004772 RID: 18290
			// (set) Token: 0x06006CC6 RID: 27846 RVA: 0x000A4C04 File Offset: 0x000A2E04
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004773 RID: 18291
			// (set) Token: 0x06006CC7 RID: 27847 RVA: 0x000A4C17 File Offset: 0x000A2E17
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004774 RID: 18292
			// (set) Token: 0x06006CC8 RID: 27848 RVA: 0x000A4C2F File Offset: 0x000A2E2F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004775 RID: 18293
			// (set) Token: 0x06006CC9 RID: 27849 RVA: 0x000A4C47 File Offset: 0x000A2E47
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004776 RID: 18294
			// (set) Token: 0x06006CCA RID: 27850 RVA: 0x000A4C5F File Offset: 0x000A2E5F
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
