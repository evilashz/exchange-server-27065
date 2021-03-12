using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200068F RID: 1679
	public class NewSharingPolicyCommand : SyntheticCommandWithPipelineInput<SharingPolicy, SharingPolicy>
	{
		// Token: 0x0600592D RID: 22829 RVA: 0x0008B7C9 File Offset: 0x000899C9
		private NewSharingPolicyCommand() : base("New-SharingPolicy")
		{
		}

		// Token: 0x0600592E RID: 22830 RVA: 0x0008B7D6 File Offset: 0x000899D6
		public NewSharingPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600592F RID: 22831 RVA: 0x0008B7E5 File Offset: 0x000899E5
		public virtual NewSharingPolicyCommand SetParameters(NewSharingPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000690 RID: 1680
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170037C8 RID: 14280
			// (set) Token: 0x06005930 RID: 22832 RVA: 0x0008B7EF File Offset: 0x000899EF
			public virtual MultiValuedProperty<SharingPolicyDomain> Domains
			{
				set
				{
					base.PowerSharpParameters["Domains"] = value;
				}
			}

			// Token: 0x170037C9 RID: 14281
			// (set) Token: 0x06005931 RID: 22833 RVA: 0x0008B802 File Offset: 0x00089A02
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x170037CA RID: 14282
			// (set) Token: 0x06005932 RID: 22834 RVA: 0x0008B81A File Offset: 0x00089A1A
			public virtual SwitchParameter Default
			{
				set
				{
					base.PowerSharpParameters["Default"] = value;
				}
			}

			// Token: 0x170037CB RID: 14283
			// (set) Token: 0x06005933 RID: 22835 RVA: 0x0008B832 File Offset: 0x00089A32
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170037CC RID: 14284
			// (set) Token: 0x06005934 RID: 22836 RVA: 0x0008B850 File Offset: 0x00089A50
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170037CD RID: 14285
			// (set) Token: 0x06005935 RID: 22837 RVA: 0x0008B863 File Offset: 0x00089A63
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170037CE RID: 14286
			// (set) Token: 0x06005936 RID: 22838 RVA: 0x0008B876 File Offset: 0x00089A76
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170037CF RID: 14287
			// (set) Token: 0x06005937 RID: 22839 RVA: 0x0008B88E File Offset: 0x00089A8E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170037D0 RID: 14288
			// (set) Token: 0x06005938 RID: 22840 RVA: 0x0008B8A6 File Offset: 0x00089AA6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170037D1 RID: 14289
			// (set) Token: 0x06005939 RID: 22841 RVA: 0x0008B8BE File Offset: 0x00089ABE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170037D2 RID: 14290
			// (set) Token: 0x0600593A RID: 22842 RVA: 0x0008B8D6 File Offset: 0x00089AD6
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
