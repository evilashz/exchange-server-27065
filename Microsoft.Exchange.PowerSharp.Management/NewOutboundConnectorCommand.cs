using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020008A7 RID: 2215
	public class NewOutboundConnectorCommand : SyntheticCommandWithPipelineInput<TenantOutboundConnector, TenantOutboundConnector>
	{
		// Token: 0x06006DC4 RID: 28100 RVA: 0x000A5F85 File Offset: 0x000A4185
		private NewOutboundConnectorCommand() : base("New-OutboundConnector")
		{
		}

		// Token: 0x06006DC5 RID: 28101 RVA: 0x000A5F92 File Offset: 0x000A4192
		public NewOutboundConnectorCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006DC6 RID: 28102 RVA: 0x000A5FA1 File Offset: 0x000A41A1
		public virtual NewOutboundConnectorCommand SetParameters(NewOutboundConnectorCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020008A8 RID: 2216
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700482F RID: 18479
			// (set) Token: 0x06006DC7 RID: 28103 RVA: 0x000A5FAB File Offset: 0x000A41AB
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17004830 RID: 18480
			// (set) Token: 0x06006DC8 RID: 28104 RVA: 0x000A5FC3 File Offset: 0x000A41C3
			public virtual bool UseMXRecord
			{
				set
				{
					base.PowerSharpParameters["UseMXRecord"] = value;
				}
			}

			// Token: 0x17004831 RID: 18481
			// (set) Token: 0x06006DC9 RID: 28105 RVA: 0x000A5FDB File Offset: 0x000A41DB
			public virtual TenantConnectorType ConnectorType
			{
				set
				{
					base.PowerSharpParameters["ConnectorType"] = value;
				}
			}

			// Token: 0x17004832 RID: 18482
			// (set) Token: 0x06006DCA RID: 28106 RVA: 0x000A5FF3 File Offset: 0x000A41F3
			public virtual TenantConnectorSource ConnectorSource
			{
				set
				{
					base.PowerSharpParameters["ConnectorSource"] = value;
				}
			}

			// Token: 0x17004833 RID: 18483
			// (set) Token: 0x06006DCB RID: 28107 RVA: 0x000A600B File Offset: 0x000A420B
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x17004834 RID: 18484
			// (set) Token: 0x06006DCC RID: 28108 RVA: 0x000A601E File Offset: 0x000A421E
			public virtual MultiValuedProperty<SmtpDomainWithSubdomains> RecipientDomains
			{
				set
				{
					base.PowerSharpParameters["RecipientDomains"] = value;
				}
			}

			// Token: 0x17004835 RID: 18485
			// (set) Token: 0x06006DCD RID: 28109 RVA: 0x000A6031 File Offset: 0x000A4231
			public virtual MultiValuedProperty<SmartHost> SmartHosts
			{
				set
				{
					base.PowerSharpParameters["SmartHosts"] = value;
				}
			}

			// Token: 0x17004836 RID: 18486
			// (set) Token: 0x06006DCE RID: 28110 RVA: 0x000A6044 File Offset: 0x000A4244
			public virtual SmtpDomainWithSubdomains TlsDomain
			{
				set
				{
					base.PowerSharpParameters["TlsDomain"] = value;
				}
			}

			// Token: 0x17004837 RID: 18487
			// (set) Token: 0x06006DCF RID: 28111 RVA: 0x000A6057 File Offset: 0x000A4257
			public virtual TlsAuthLevel? TlsSettings
			{
				set
				{
					base.PowerSharpParameters["TlsSettings"] = value;
				}
			}

			// Token: 0x17004838 RID: 18488
			// (set) Token: 0x06006DD0 RID: 28112 RVA: 0x000A606F File Offset: 0x000A426F
			public virtual bool IsTransportRuleScoped
			{
				set
				{
					base.PowerSharpParameters["IsTransportRuleScoped"] = value;
				}
			}

			// Token: 0x17004839 RID: 18489
			// (set) Token: 0x06006DD1 RID: 28113 RVA: 0x000A6087 File Offset: 0x000A4287
			public virtual bool RouteAllMessagesViaOnPremises
			{
				set
				{
					base.PowerSharpParameters["RouteAllMessagesViaOnPremises"] = value;
				}
			}

			// Token: 0x1700483A RID: 18490
			// (set) Token: 0x06006DD2 RID: 28114 RVA: 0x000A609F File Offset: 0x000A429F
			public virtual bool BypassValidation
			{
				set
				{
					base.PowerSharpParameters["BypassValidation"] = value;
				}
			}

			// Token: 0x1700483B RID: 18491
			// (set) Token: 0x06006DD3 RID: 28115 RVA: 0x000A60B7 File Offset: 0x000A42B7
			public virtual bool CloudServicesMailEnabled
			{
				set
				{
					base.PowerSharpParameters["CloudServicesMailEnabled"] = value;
				}
			}

			// Token: 0x1700483C RID: 18492
			// (set) Token: 0x06006DD4 RID: 28116 RVA: 0x000A60CF File Offset: 0x000A42CF
			public virtual bool AllAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["AllAcceptedDomains"] = value;
				}
			}

			// Token: 0x1700483D RID: 18493
			// (set) Token: 0x06006DD5 RID: 28117 RVA: 0x000A60E7 File Offset: 0x000A42E7
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700483E RID: 18494
			// (set) Token: 0x06006DD6 RID: 28118 RVA: 0x000A6105 File Offset: 0x000A4305
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700483F RID: 18495
			// (set) Token: 0x06006DD7 RID: 28119 RVA: 0x000A6118 File Offset: 0x000A4318
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004840 RID: 18496
			// (set) Token: 0x06006DD8 RID: 28120 RVA: 0x000A612B File Offset: 0x000A432B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004841 RID: 18497
			// (set) Token: 0x06006DD9 RID: 28121 RVA: 0x000A6143 File Offset: 0x000A4343
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004842 RID: 18498
			// (set) Token: 0x06006DDA RID: 28122 RVA: 0x000A615B File Offset: 0x000A435B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004843 RID: 18499
			// (set) Token: 0x06006DDB RID: 28123 RVA: 0x000A6173 File Offset: 0x000A4373
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004844 RID: 18500
			// (set) Token: 0x06006DDC RID: 28124 RVA: 0x000A618B File Offset: 0x000A438B
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
