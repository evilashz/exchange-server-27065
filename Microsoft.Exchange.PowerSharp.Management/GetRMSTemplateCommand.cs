using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.RightsManagement;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003E2 RID: 994
	public class GetRMSTemplateCommand : SyntheticCommandWithPipelineInput<RmsTemplatePresentation, RmsTemplatePresentation>
	{
		// Token: 0x06003B5A RID: 15194 RVA: 0x00064CD1 File Offset: 0x00062ED1
		private GetRMSTemplateCommand() : base("Get-RMSTemplate")
		{
		}

		// Token: 0x06003B5B RID: 15195 RVA: 0x00064CDE File Offset: 0x00062EDE
		public GetRMSTemplateCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003B5C RID: 15196 RVA: 0x00064CED File Offset: 0x00062EED
		public virtual GetRMSTemplateCommand SetParameters(GetRMSTemplateCommand.OrganizationSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003B5D RID: 15197 RVA: 0x00064CF7 File Offset: 0x00062EF7
		public virtual GetRMSTemplateCommand SetParameters(GetRMSTemplateCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003E3 RID: 995
		public class OrganizationSetParameters : ParametersBase
		{
			// Token: 0x17001F4F RID: 8015
			// (set) Token: 0x06003B5E RID: 15198 RVA: 0x00064D01 File Offset: 0x00062F01
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001F50 RID: 8016
			// (set) Token: 0x06003B5F RID: 15199 RVA: 0x00064D1F File Offset: 0x00062F1F
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RmsTemplateIdParameter(value) : null);
				}
			}

			// Token: 0x17001F51 RID: 8017
			// (set) Token: 0x06003B60 RID: 15200 RVA: 0x00064D3D File Offset: 0x00062F3D
			public virtual string TrustedPublishingDomain
			{
				set
				{
					base.PowerSharpParameters["TrustedPublishingDomain"] = ((value != null) ? new RmsTrustedPublishingDomainIdParameter(value) : null);
				}
			}

			// Token: 0x17001F52 RID: 8018
			// (set) Token: 0x06003B61 RID: 15201 RVA: 0x00064D5B File Offset: 0x00062F5B
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001F53 RID: 8019
			// (set) Token: 0x06003B62 RID: 15202 RVA: 0x00064D73 File Offset: 0x00062F73
			public virtual RmsTemplateType Type
			{
				set
				{
					base.PowerSharpParameters["Type"] = value;
				}
			}

			// Token: 0x17001F54 RID: 8020
			// (set) Token: 0x06003B63 RID: 15203 RVA: 0x00064D8B File Offset: 0x00062F8B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001F55 RID: 8021
			// (set) Token: 0x06003B64 RID: 15204 RVA: 0x00064D9E File Offset: 0x00062F9E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001F56 RID: 8022
			// (set) Token: 0x06003B65 RID: 15205 RVA: 0x00064DB6 File Offset: 0x00062FB6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001F57 RID: 8023
			// (set) Token: 0x06003B66 RID: 15206 RVA: 0x00064DCE File Offset: 0x00062FCE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001F58 RID: 8024
			// (set) Token: 0x06003B67 RID: 15207 RVA: 0x00064DE6 File Offset: 0x00062FE6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020003E4 RID: 996
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001F59 RID: 8025
			// (set) Token: 0x06003B69 RID: 15209 RVA: 0x00064E06 File Offset: 0x00063006
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RmsTemplateIdParameter(value) : null);
				}
			}

			// Token: 0x17001F5A RID: 8026
			// (set) Token: 0x06003B6A RID: 15210 RVA: 0x00064E24 File Offset: 0x00063024
			public virtual string TrustedPublishingDomain
			{
				set
				{
					base.PowerSharpParameters["TrustedPublishingDomain"] = ((value != null) ? new RmsTrustedPublishingDomainIdParameter(value) : null);
				}
			}

			// Token: 0x17001F5B RID: 8027
			// (set) Token: 0x06003B6B RID: 15211 RVA: 0x00064E42 File Offset: 0x00063042
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001F5C RID: 8028
			// (set) Token: 0x06003B6C RID: 15212 RVA: 0x00064E5A File Offset: 0x0006305A
			public virtual RmsTemplateType Type
			{
				set
				{
					base.PowerSharpParameters["Type"] = value;
				}
			}

			// Token: 0x17001F5D RID: 8029
			// (set) Token: 0x06003B6D RID: 15213 RVA: 0x00064E72 File Offset: 0x00063072
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001F5E RID: 8030
			// (set) Token: 0x06003B6E RID: 15214 RVA: 0x00064E85 File Offset: 0x00063085
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001F5F RID: 8031
			// (set) Token: 0x06003B6F RID: 15215 RVA: 0x00064E9D File Offset: 0x0006309D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001F60 RID: 8032
			// (set) Token: 0x06003B70 RID: 15216 RVA: 0x00064EB5 File Offset: 0x000630B5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001F61 RID: 8033
			// (set) Token: 0x06003B71 RID: 15217 RVA: 0x00064ECD File Offset: 0x000630CD
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
