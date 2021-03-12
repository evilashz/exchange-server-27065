using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200067A RID: 1658
	public class GetFederatedOrganizationIdentifierCommand : SyntheticCommandWithPipelineInput<FederatedOrganizationId, FederatedOrganizationId>
	{
		// Token: 0x06005892 RID: 22674 RVA: 0x0008ABD1 File Offset: 0x00088DD1
		private GetFederatedOrganizationIdentifierCommand() : base("Get-FederatedOrganizationIdentifier")
		{
		}

		// Token: 0x06005893 RID: 22675 RVA: 0x0008ABDE File Offset: 0x00088DDE
		public GetFederatedOrganizationIdentifierCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005894 RID: 22676 RVA: 0x0008ABED File Offset: 0x00088DED
		public virtual GetFederatedOrganizationIdentifierCommand SetParameters(GetFederatedOrganizationIdentifierCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005895 RID: 22677 RVA: 0x0008ABF7 File Offset: 0x00088DF7
		public virtual GetFederatedOrganizationIdentifierCommand SetParameters(GetFederatedOrganizationIdentifierCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200067B RID: 1659
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003757 RID: 14167
			// (set) Token: 0x06005896 RID: 22678 RVA: 0x0008AC01 File Offset: 0x00088E01
			public virtual SwitchParameter IncludeExtendedDomainInfo
			{
				set
				{
					base.PowerSharpParameters["IncludeExtendedDomainInfo"] = value;
				}
			}

			// Token: 0x17003758 RID: 14168
			// (set) Token: 0x06005897 RID: 22679 RVA: 0x0008AC19 File Offset: 0x00088E19
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003759 RID: 14169
			// (set) Token: 0x06005898 RID: 22680 RVA: 0x0008AC2C File Offset: 0x00088E2C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700375A RID: 14170
			// (set) Token: 0x06005899 RID: 22681 RVA: 0x0008AC44 File Offset: 0x00088E44
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700375B RID: 14171
			// (set) Token: 0x0600589A RID: 22682 RVA: 0x0008AC5C File Offset: 0x00088E5C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700375C RID: 14172
			// (set) Token: 0x0600589B RID: 22683 RVA: 0x0008AC74 File Offset: 0x00088E74
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200067C RID: 1660
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700375D RID: 14173
			// (set) Token: 0x0600589D RID: 22685 RVA: 0x0008AC94 File Offset: 0x00088E94
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700375E RID: 14174
			// (set) Token: 0x0600589E RID: 22686 RVA: 0x0008ACB2 File Offset: 0x00088EB2
			public virtual SwitchParameter IncludeExtendedDomainInfo
			{
				set
				{
					base.PowerSharpParameters["IncludeExtendedDomainInfo"] = value;
				}
			}

			// Token: 0x1700375F RID: 14175
			// (set) Token: 0x0600589F RID: 22687 RVA: 0x0008ACCA File Offset: 0x00088ECA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003760 RID: 14176
			// (set) Token: 0x060058A0 RID: 22688 RVA: 0x0008ACDD File Offset: 0x00088EDD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003761 RID: 14177
			// (set) Token: 0x060058A1 RID: 22689 RVA: 0x0008ACF5 File Offset: 0x00088EF5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003762 RID: 14178
			// (set) Token: 0x060058A2 RID: 22690 RVA: 0x0008AD0D File Offset: 0x00088F0D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003763 RID: 14179
			// (set) Token: 0x060058A3 RID: 22691 RVA: 0x0008AD25 File Offset: 0x00088F25
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
