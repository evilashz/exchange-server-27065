using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020008D8 RID: 2264
	public class SetOutboundConnectorCommand : SyntheticCommandWithPipelineInputNoOutput<TenantOutboundConnector>
	{
		// Token: 0x06007132 RID: 28978 RVA: 0x000AA928 File Offset: 0x000A8B28
		private SetOutboundConnectorCommand() : base("Set-OutboundConnector")
		{
		}

		// Token: 0x06007133 RID: 28979 RVA: 0x000AA935 File Offset: 0x000A8B35
		public SetOutboundConnectorCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007134 RID: 28980 RVA: 0x000AA944 File Offset: 0x000A8B44
		public virtual SetOutboundConnectorCommand SetParameters(SetOutboundConnectorCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007135 RID: 28981 RVA: 0x000AA94E File Offset: 0x000A8B4E
		public virtual SetOutboundConnectorCommand SetParameters(SetOutboundConnectorCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020008D9 RID: 2265
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004B3B RID: 19259
			// (set) Token: 0x06007136 RID: 28982 RVA: 0x000AA958 File Offset: 0x000A8B58
			public virtual bool BypassValidation
			{
				set
				{
					base.PowerSharpParameters["BypassValidation"] = value;
				}
			}

			// Token: 0x17004B3C RID: 19260
			// (set) Token: 0x06007137 RID: 28983 RVA: 0x000AA970 File Offset: 0x000A8B70
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004B3D RID: 19261
			// (set) Token: 0x06007138 RID: 28984 RVA: 0x000AA983 File Offset: 0x000A8B83
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17004B3E RID: 19262
			// (set) Token: 0x06007139 RID: 28985 RVA: 0x000AA99B File Offset: 0x000A8B9B
			public virtual bool UseMXRecord
			{
				set
				{
					base.PowerSharpParameters["UseMXRecord"] = value;
				}
			}

			// Token: 0x17004B3F RID: 19263
			// (set) Token: 0x0600713A RID: 28986 RVA: 0x000AA9B3 File Offset: 0x000A8BB3
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x17004B40 RID: 19264
			// (set) Token: 0x0600713B RID: 28987 RVA: 0x000AA9C6 File Offset: 0x000A8BC6
			public virtual TenantConnectorType ConnectorType
			{
				set
				{
					base.PowerSharpParameters["ConnectorType"] = value;
				}
			}

			// Token: 0x17004B41 RID: 19265
			// (set) Token: 0x0600713C RID: 28988 RVA: 0x000AA9DE File Offset: 0x000A8BDE
			public virtual TenantConnectorSource ConnectorSource
			{
				set
				{
					base.PowerSharpParameters["ConnectorSource"] = value;
				}
			}

			// Token: 0x17004B42 RID: 19266
			// (set) Token: 0x0600713D RID: 28989 RVA: 0x000AA9F6 File Offset: 0x000A8BF6
			public virtual MultiValuedProperty<SmtpDomainWithSubdomains> RecipientDomains
			{
				set
				{
					base.PowerSharpParameters["RecipientDomains"] = value;
				}
			}

			// Token: 0x17004B43 RID: 19267
			// (set) Token: 0x0600713E RID: 28990 RVA: 0x000AAA09 File Offset: 0x000A8C09
			public virtual MultiValuedProperty<SmartHost> SmartHosts
			{
				set
				{
					base.PowerSharpParameters["SmartHosts"] = value;
				}
			}

			// Token: 0x17004B44 RID: 19268
			// (set) Token: 0x0600713F RID: 28991 RVA: 0x000AAA1C File Offset: 0x000A8C1C
			public virtual SmtpDomainWithSubdomains TlsDomain
			{
				set
				{
					base.PowerSharpParameters["TlsDomain"] = value;
				}
			}

			// Token: 0x17004B45 RID: 19269
			// (set) Token: 0x06007140 RID: 28992 RVA: 0x000AAA2F File Offset: 0x000A8C2F
			public virtual TlsAuthLevel? TlsSettings
			{
				set
				{
					base.PowerSharpParameters["TlsSettings"] = value;
				}
			}

			// Token: 0x17004B46 RID: 19270
			// (set) Token: 0x06007141 RID: 28993 RVA: 0x000AAA47 File Offset: 0x000A8C47
			public virtual bool IsTransportRuleScoped
			{
				set
				{
					base.PowerSharpParameters["IsTransportRuleScoped"] = value;
				}
			}

			// Token: 0x17004B47 RID: 19271
			// (set) Token: 0x06007142 RID: 28994 RVA: 0x000AAA5F File Offset: 0x000A8C5F
			public virtual bool RouteAllMessagesViaOnPremises
			{
				set
				{
					base.PowerSharpParameters["RouteAllMessagesViaOnPremises"] = value;
				}
			}

			// Token: 0x17004B48 RID: 19272
			// (set) Token: 0x06007143 RID: 28995 RVA: 0x000AAA77 File Offset: 0x000A8C77
			public virtual bool CloudServicesMailEnabled
			{
				set
				{
					base.PowerSharpParameters["CloudServicesMailEnabled"] = value;
				}
			}

			// Token: 0x17004B49 RID: 19273
			// (set) Token: 0x06007144 RID: 28996 RVA: 0x000AAA8F File Offset: 0x000A8C8F
			public virtual bool AllAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["AllAcceptedDomains"] = value;
				}
			}

			// Token: 0x17004B4A RID: 19274
			// (set) Token: 0x06007145 RID: 28997 RVA: 0x000AAAA7 File Offset: 0x000A8CA7
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004B4B RID: 19275
			// (set) Token: 0x06007146 RID: 28998 RVA: 0x000AAABA File Offset: 0x000A8CBA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004B4C RID: 19276
			// (set) Token: 0x06007147 RID: 28999 RVA: 0x000AAAD2 File Offset: 0x000A8CD2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004B4D RID: 19277
			// (set) Token: 0x06007148 RID: 29000 RVA: 0x000AAAEA File Offset: 0x000A8CEA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004B4E RID: 19278
			// (set) Token: 0x06007149 RID: 29001 RVA: 0x000AAB02 File Offset: 0x000A8D02
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004B4F RID: 19279
			// (set) Token: 0x0600714A RID: 29002 RVA: 0x000AAB1A File Offset: 0x000A8D1A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020008DA RID: 2266
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004B50 RID: 19280
			// (set) Token: 0x0600714C RID: 29004 RVA: 0x000AAB3A File Offset: 0x000A8D3A
			public virtual OutboundConnectorIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17004B51 RID: 19281
			// (set) Token: 0x0600714D RID: 29005 RVA: 0x000AAB4D File Offset: 0x000A8D4D
			public virtual bool BypassValidation
			{
				set
				{
					base.PowerSharpParameters["BypassValidation"] = value;
				}
			}

			// Token: 0x17004B52 RID: 19282
			// (set) Token: 0x0600714E RID: 29006 RVA: 0x000AAB65 File Offset: 0x000A8D65
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004B53 RID: 19283
			// (set) Token: 0x0600714F RID: 29007 RVA: 0x000AAB78 File Offset: 0x000A8D78
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17004B54 RID: 19284
			// (set) Token: 0x06007150 RID: 29008 RVA: 0x000AAB90 File Offset: 0x000A8D90
			public virtual bool UseMXRecord
			{
				set
				{
					base.PowerSharpParameters["UseMXRecord"] = value;
				}
			}

			// Token: 0x17004B55 RID: 19285
			// (set) Token: 0x06007151 RID: 29009 RVA: 0x000AABA8 File Offset: 0x000A8DA8
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x17004B56 RID: 19286
			// (set) Token: 0x06007152 RID: 29010 RVA: 0x000AABBB File Offset: 0x000A8DBB
			public virtual TenantConnectorType ConnectorType
			{
				set
				{
					base.PowerSharpParameters["ConnectorType"] = value;
				}
			}

			// Token: 0x17004B57 RID: 19287
			// (set) Token: 0x06007153 RID: 29011 RVA: 0x000AABD3 File Offset: 0x000A8DD3
			public virtual TenantConnectorSource ConnectorSource
			{
				set
				{
					base.PowerSharpParameters["ConnectorSource"] = value;
				}
			}

			// Token: 0x17004B58 RID: 19288
			// (set) Token: 0x06007154 RID: 29012 RVA: 0x000AABEB File Offset: 0x000A8DEB
			public virtual MultiValuedProperty<SmtpDomainWithSubdomains> RecipientDomains
			{
				set
				{
					base.PowerSharpParameters["RecipientDomains"] = value;
				}
			}

			// Token: 0x17004B59 RID: 19289
			// (set) Token: 0x06007155 RID: 29013 RVA: 0x000AABFE File Offset: 0x000A8DFE
			public virtual MultiValuedProperty<SmartHost> SmartHosts
			{
				set
				{
					base.PowerSharpParameters["SmartHosts"] = value;
				}
			}

			// Token: 0x17004B5A RID: 19290
			// (set) Token: 0x06007156 RID: 29014 RVA: 0x000AAC11 File Offset: 0x000A8E11
			public virtual SmtpDomainWithSubdomains TlsDomain
			{
				set
				{
					base.PowerSharpParameters["TlsDomain"] = value;
				}
			}

			// Token: 0x17004B5B RID: 19291
			// (set) Token: 0x06007157 RID: 29015 RVA: 0x000AAC24 File Offset: 0x000A8E24
			public virtual TlsAuthLevel? TlsSettings
			{
				set
				{
					base.PowerSharpParameters["TlsSettings"] = value;
				}
			}

			// Token: 0x17004B5C RID: 19292
			// (set) Token: 0x06007158 RID: 29016 RVA: 0x000AAC3C File Offset: 0x000A8E3C
			public virtual bool IsTransportRuleScoped
			{
				set
				{
					base.PowerSharpParameters["IsTransportRuleScoped"] = value;
				}
			}

			// Token: 0x17004B5D RID: 19293
			// (set) Token: 0x06007159 RID: 29017 RVA: 0x000AAC54 File Offset: 0x000A8E54
			public virtual bool RouteAllMessagesViaOnPremises
			{
				set
				{
					base.PowerSharpParameters["RouteAllMessagesViaOnPremises"] = value;
				}
			}

			// Token: 0x17004B5E RID: 19294
			// (set) Token: 0x0600715A RID: 29018 RVA: 0x000AAC6C File Offset: 0x000A8E6C
			public virtual bool CloudServicesMailEnabled
			{
				set
				{
					base.PowerSharpParameters["CloudServicesMailEnabled"] = value;
				}
			}

			// Token: 0x17004B5F RID: 19295
			// (set) Token: 0x0600715B RID: 29019 RVA: 0x000AAC84 File Offset: 0x000A8E84
			public virtual bool AllAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["AllAcceptedDomains"] = value;
				}
			}

			// Token: 0x17004B60 RID: 19296
			// (set) Token: 0x0600715C RID: 29020 RVA: 0x000AAC9C File Offset: 0x000A8E9C
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004B61 RID: 19297
			// (set) Token: 0x0600715D RID: 29021 RVA: 0x000AACAF File Offset: 0x000A8EAF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004B62 RID: 19298
			// (set) Token: 0x0600715E RID: 29022 RVA: 0x000AACC7 File Offset: 0x000A8EC7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004B63 RID: 19299
			// (set) Token: 0x0600715F RID: 29023 RVA: 0x000AACDF File Offset: 0x000A8EDF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004B64 RID: 19300
			// (set) Token: 0x06007160 RID: 29024 RVA: 0x000AACF7 File Offset: 0x000A8EF7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004B65 RID: 19301
			// (set) Token: 0x06007161 RID: 29025 RVA: 0x000AAD0F File Offset: 0x000A8F0F
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
