using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B6D RID: 2925
	public class NewUMDialPlanCommand : SyntheticCommandWithPipelineInput<UMDialPlan, UMDialPlan>
	{
		// Token: 0x06008DB4 RID: 36276 RVA: 0x000CF9FD File Offset: 0x000CDBFD
		private NewUMDialPlanCommand() : base("New-UMDialPlan")
		{
		}

		// Token: 0x06008DB5 RID: 36277 RVA: 0x000CFA0A File Offset: 0x000CDC0A
		public NewUMDialPlanCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008DB6 RID: 36278 RVA: 0x000CFA19 File Offset: 0x000CDC19
		public virtual NewUMDialPlanCommand SetParameters(NewUMDialPlanCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B6E RID: 2926
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006293 RID: 25235
			// (set) Token: 0x06008DB7 RID: 36279 RVA: 0x000CFA23 File Offset: 0x000CDC23
			public virtual int NumberOfDigitsInExtension
			{
				set
				{
					base.PowerSharpParameters["NumberOfDigitsInExtension"] = value;
				}
			}

			// Token: 0x17006294 RID: 25236
			// (set) Token: 0x06008DB8 RID: 36280 RVA: 0x000CFA3B File Offset: 0x000CDC3B
			public virtual UMUriType URIType
			{
				set
				{
					base.PowerSharpParameters["URIType"] = value;
				}
			}

			// Token: 0x17006295 RID: 25237
			// (set) Token: 0x06008DB9 RID: 36281 RVA: 0x000CFA53 File Offset: 0x000CDC53
			public virtual UMSubscriberType SubscriberType
			{
				set
				{
					base.PowerSharpParameters["SubscriberType"] = value;
				}
			}

			// Token: 0x17006296 RID: 25238
			// (set) Token: 0x06008DBA RID: 36282 RVA: 0x000CFA6B File Offset: 0x000CDC6B
			public virtual UMVoIPSecurityType VoIPSecurity
			{
				set
				{
					base.PowerSharpParameters["VoIPSecurity"] = value;
				}
			}

			// Token: 0x17006297 RID: 25239
			// (set) Token: 0x06008DBB RID: 36283 RVA: 0x000CFA83 File Offset: 0x000CDC83
			public virtual MultiValuedProperty<string> AccessTelephoneNumbers
			{
				set
				{
					base.PowerSharpParameters["AccessTelephoneNumbers"] = value;
				}
			}

			// Token: 0x17006298 RID: 25240
			// (set) Token: 0x06008DBC RID: 36284 RVA: 0x000CFA96 File Offset: 0x000CDC96
			public virtual bool FaxEnabled
			{
				set
				{
					base.PowerSharpParameters["FaxEnabled"] = value;
				}
			}

			// Token: 0x17006299 RID: 25241
			// (set) Token: 0x06008DBD RID: 36285 RVA: 0x000CFAAE File Offset: 0x000CDCAE
			public virtual bool SipResourceIdentifierRequired
			{
				set
				{
					base.PowerSharpParameters["SipResourceIdentifierRequired"] = value;
				}
			}

			// Token: 0x1700629A RID: 25242
			// (set) Token: 0x06008DBE RID: 36286 RVA: 0x000CFAC6 File Offset: 0x000CDCC6
			public virtual string DefaultOutboundCallingLineId
			{
				set
				{
					base.PowerSharpParameters["DefaultOutboundCallingLineId"] = value;
				}
			}

			// Token: 0x1700629B RID: 25243
			// (set) Token: 0x06008DBF RID: 36287 RVA: 0x000CFAD9 File Offset: 0x000CDCD9
			public virtual bool GenerateUMMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["GenerateUMMailboxPolicy"] = value;
				}
			}

			// Token: 0x1700629C RID: 25244
			// (set) Token: 0x06008DC0 RID: 36288 RVA: 0x000CFAF1 File Offset: 0x000CDCF1
			public virtual string CountryOrRegionCode
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegionCode"] = value;
				}
			}

			// Token: 0x1700629D RID: 25245
			// (set) Token: 0x06008DC1 RID: 36289 RVA: 0x000CFB04 File Offset: 0x000CDD04
			public virtual UMGlobalCallRoutingScheme GlobalCallRoutingScheme
			{
				set
				{
					base.PowerSharpParameters["GlobalCallRoutingScheme"] = value;
				}
			}

			// Token: 0x1700629E RID: 25246
			// (set) Token: 0x06008DC2 RID: 36290 RVA: 0x000CFB1C File Offset: 0x000CDD1C
			public virtual UMLanguage DefaultLanguage
			{
				set
				{
					base.PowerSharpParameters["DefaultLanguage"] = value;
				}
			}

			// Token: 0x1700629F RID: 25247
			// (set) Token: 0x06008DC3 RID: 36291 RVA: 0x000CFB2F File Offset: 0x000CDD2F
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170062A0 RID: 25248
			// (set) Token: 0x06008DC4 RID: 36292 RVA: 0x000CFB4D File Offset: 0x000CDD4D
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170062A1 RID: 25249
			// (set) Token: 0x06008DC5 RID: 36293 RVA: 0x000CFB60 File Offset: 0x000CDD60
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170062A2 RID: 25250
			// (set) Token: 0x06008DC6 RID: 36294 RVA: 0x000CFB73 File Offset: 0x000CDD73
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170062A3 RID: 25251
			// (set) Token: 0x06008DC7 RID: 36295 RVA: 0x000CFB8B File Offset: 0x000CDD8B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170062A4 RID: 25252
			// (set) Token: 0x06008DC8 RID: 36296 RVA: 0x000CFBA3 File Offset: 0x000CDDA3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170062A5 RID: 25253
			// (set) Token: 0x06008DC9 RID: 36297 RVA: 0x000CFBBB File Offset: 0x000CDDBB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170062A6 RID: 25254
			// (set) Token: 0x06008DCA RID: 36298 RVA: 0x000CFBD3 File Offset: 0x000CDDD3
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
