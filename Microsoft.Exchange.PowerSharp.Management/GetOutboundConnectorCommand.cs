using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000893 RID: 2195
	public class GetOutboundConnectorCommand : SyntheticCommandWithPipelineInput<TenantOutboundConnector, TenantOutboundConnector>
	{
		// Token: 0x06006D14 RID: 27924 RVA: 0x000A51E5 File Offset: 0x000A33E5
		private GetOutboundConnectorCommand() : base("Get-OutboundConnector")
		{
		}

		// Token: 0x06006D15 RID: 27925 RVA: 0x000A51F2 File Offset: 0x000A33F2
		public GetOutboundConnectorCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006D16 RID: 27926 RVA: 0x000A5201 File Offset: 0x000A3401
		public virtual GetOutboundConnectorCommand SetParameters(GetOutboundConnectorCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006D17 RID: 27927 RVA: 0x000A520B File Offset: 0x000A340B
		public virtual GetOutboundConnectorCommand SetParameters(GetOutboundConnectorCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000894 RID: 2196
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170047A7 RID: 18343
			// (set) Token: 0x06006D18 RID: 27928 RVA: 0x000A5215 File Offset: 0x000A3415
			public virtual bool IsTransportRuleScoped
			{
				set
				{
					base.PowerSharpParameters["IsTransportRuleScoped"] = value;
				}
			}

			// Token: 0x170047A8 RID: 18344
			// (set) Token: 0x06006D19 RID: 27929 RVA: 0x000A522D File Offset: 0x000A342D
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170047A9 RID: 18345
			// (set) Token: 0x06006D1A RID: 27930 RVA: 0x000A524B File Offset: 0x000A344B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170047AA RID: 18346
			// (set) Token: 0x06006D1B RID: 27931 RVA: 0x000A525E File Offset: 0x000A345E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170047AB RID: 18347
			// (set) Token: 0x06006D1C RID: 27932 RVA: 0x000A5276 File Offset: 0x000A3476
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170047AC RID: 18348
			// (set) Token: 0x06006D1D RID: 27933 RVA: 0x000A528E File Offset: 0x000A348E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170047AD RID: 18349
			// (set) Token: 0x06006D1E RID: 27934 RVA: 0x000A52A6 File Offset: 0x000A34A6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000895 RID: 2197
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170047AE RID: 18350
			// (set) Token: 0x06006D20 RID: 27936 RVA: 0x000A52C6 File Offset: 0x000A34C6
			public virtual OutboundConnectorIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170047AF RID: 18351
			// (set) Token: 0x06006D21 RID: 27937 RVA: 0x000A52D9 File Offset: 0x000A34D9
			public virtual bool IsTransportRuleScoped
			{
				set
				{
					base.PowerSharpParameters["IsTransportRuleScoped"] = value;
				}
			}

			// Token: 0x170047B0 RID: 18352
			// (set) Token: 0x06006D22 RID: 27938 RVA: 0x000A52F1 File Offset: 0x000A34F1
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170047B1 RID: 18353
			// (set) Token: 0x06006D23 RID: 27939 RVA: 0x000A530F File Offset: 0x000A350F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170047B2 RID: 18354
			// (set) Token: 0x06006D24 RID: 27940 RVA: 0x000A5322 File Offset: 0x000A3522
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170047B3 RID: 18355
			// (set) Token: 0x06006D25 RID: 27941 RVA: 0x000A533A File Offset: 0x000A353A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170047B4 RID: 18356
			// (set) Token: 0x06006D26 RID: 27942 RVA: 0x000A5352 File Offset: 0x000A3552
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170047B5 RID: 18357
			// (set) Token: 0x06006D27 RID: 27943 RVA: 0x000A536A File Offset: 0x000A356A
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
