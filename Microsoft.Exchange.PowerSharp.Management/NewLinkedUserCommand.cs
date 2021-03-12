using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C60 RID: 3168
	public class NewLinkedUserCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x06009B6C RID: 39788 RVA: 0x000E1925 File Offset: 0x000DFB25
		private NewLinkedUserCommand() : base("New-LinkedUser")
		{
		}

		// Token: 0x06009B6D RID: 39789 RVA: 0x000E1932 File Offset: 0x000DFB32
		public NewLinkedUserCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06009B6E RID: 39790 RVA: 0x000E1941 File Offset: 0x000DFB41
		public virtual NewLinkedUserCommand SetParameters(NewLinkedUserCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C61 RID: 3169
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006E65 RID: 28261
			// (set) Token: 0x06009B6F RID: 39791 RVA: 0x000E194B File Offset: 0x000DFB4B
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17006E66 RID: 28262
			// (set) Token: 0x06009B70 RID: 39792 RVA: 0x000E195E File Offset: 0x000DFB5E
			public virtual string LinkedMasterAccount
			{
				set
				{
					base.PowerSharpParameters["LinkedMasterAccount"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17006E67 RID: 28263
			// (set) Token: 0x06009B71 RID: 39793 RVA: 0x000E197C File Offset: 0x000DFB7C
			public virtual string LinkedDomainController
			{
				set
				{
					base.PowerSharpParameters["LinkedDomainController"] = value;
				}
			}

			// Token: 0x17006E68 RID: 28264
			// (set) Token: 0x06009B72 RID: 39794 RVA: 0x000E198F File Offset: 0x000DFB8F
			public virtual PSCredential LinkedCredential
			{
				set
				{
					base.PowerSharpParameters["LinkedCredential"] = value;
				}
			}

			// Token: 0x17006E69 RID: 28265
			// (set) Token: 0x06009B73 RID: 39795 RVA: 0x000E19A2 File Offset: 0x000DFBA2
			public virtual MultiValuedProperty<X509Identifier> CertificateSubject
			{
				set
				{
					base.PowerSharpParameters["CertificateSubject"] = value;
				}
			}

			// Token: 0x17006E6A RID: 28266
			// (set) Token: 0x06009B74 RID: 39796 RVA: 0x000E19B5 File Offset: 0x000DFBB5
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17006E6B RID: 28267
			// (set) Token: 0x06009B75 RID: 39797 RVA: 0x000E19C8 File Offset: 0x000DFBC8
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17006E6C RID: 28268
			// (set) Token: 0x06009B76 RID: 39798 RVA: 0x000E19DB File Offset: 0x000DFBDB
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17006E6D RID: 28269
			// (set) Token: 0x06009B77 RID: 39799 RVA: 0x000E19F9 File Offset: 0x000DFBF9
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006E6E RID: 28270
			// (set) Token: 0x06009B78 RID: 39800 RVA: 0x000E1A17 File Offset: 0x000DFC17
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006E6F RID: 28271
			// (set) Token: 0x06009B79 RID: 39801 RVA: 0x000E1A2A File Offset: 0x000DFC2A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006E70 RID: 28272
			// (set) Token: 0x06009B7A RID: 39802 RVA: 0x000E1A42 File Offset: 0x000DFC42
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006E71 RID: 28273
			// (set) Token: 0x06009B7B RID: 39803 RVA: 0x000E1A5A File Offset: 0x000DFC5A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006E72 RID: 28274
			// (set) Token: 0x06009B7C RID: 39804 RVA: 0x000E1A72 File Offset: 0x000DFC72
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006E73 RID: 28275
			// (set) Token: 0x06009B7D RID: 39805 RVA: 0x000E1A8A File Offset: 0x000DFC8A
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
