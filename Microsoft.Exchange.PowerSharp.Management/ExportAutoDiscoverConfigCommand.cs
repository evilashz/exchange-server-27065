using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020004F7 RID: 1271
	public class ExportAutoDiscoverConfigCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x0600456F RID: 17775 RVA: 0x00071A9A File Offset: 0x0006FC9A
		private ExportAutoDiscoverConfigCommand() : base("Export-AutoDiscoverConfig")
		{
		}

		// Token: 0x06004570 RID: 17776 RVA: 0x00071AA7 File Offset: 0x0006FCA7
		public ExportAutoDiscoverConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004571 RID: 17777 RVA: 0x00071AB6 File Offset: 0x0006FCB6
		public virtual ExportAutoDiscoverConfigCommand SetParameters(ExportAutoDiscoverConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020004F8 RID: 1272
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700273A RID: 10042
			// (set) Token: 0x06004572 RID: 17778 RVA: 0x00071AC0 File Offset: 0x0006FCC0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700273B RID: 10043
			// (set) Token: 0x06004573 RID: 17779 RVA: 0x00071AD3 File Offset: 0x0006FCD3
			public virtual PSCredential SourceForestCredential
			{
				set
				{
					base.PowerSharpParameters["SourceForestCredential"] = value;
				}
			}

			// Token: 0x1700273C RID: 10044
			// (set) Token: 0x06004574 RID: 17780 RVA: 0x00071AE6 File Offset: 0x0006FCE6
			public virtual Fqdn PreferredSourceFqdn
			{
				set
				{
					base.PowerSharpParameters["PreferredSourceFqdn"] = value;
				}
			}

			// Token: 0x1700273D RID: 10045
			// (set) Token: 0x06004575 RID: 17781 RVA: 0x00071AF9 File Offset: 0x0006FCF9
			public virtual bool? DeleteConfig
			{
				set
				{
					base.PowerSharpParameters["DeleteConfig"] = value;
				}
			}

			// Token: 0x1700273E RID: 10046
			// (set) Token: 0x06004576 RID: 17782 RVA: 0x00071B11 File Offset: 0x0006FD11
			public virtual string TargetForestDomainController
			{
				set
				{
					base.PowerSharpParameters["TargetForestDomainController"] = value;
				}
			}

			// Token: 0x1700273F RID: 10047
			// (set) Token: 0x06004577 RID: 17783 RVA: 0x00071B24 File Offset: 0x0006FD24
			public virtual PSCredential TargetForestCredential
			{
				set
				{
					base.PowerSharpParameters["TargetForestCredential"] = value;
				}
			}

			// Token: 0x17002740 RID: 10048
			// (set) Token: 0x06004578 RID: 17784 RVA: 0x00071B37 File Offset: 0x0006FD37
			public virtual bool MultipleExchangeDeployments
			{
				set
				{
					base.PowerSharpParameters["MultipleExchangeDeployments"] = value;
				}
			}

			// Token: 0x17002741 RID: 10049
			// (set) Token: 0x06004579 RID: 17785 RVA: 0x00071B4F File Offset: 0x0006FD4F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002742 RID: 10050
			// (set) Token: 0x0600457A RID: 17786 RVA: 0x00071B67 File Offset: 0x0006FD67
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002743 RID: 10051
			// (set) Token: 0x0600457B RID: 17787 RVA: 0x00071B7F File Offset: 0x0006FD7F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002744 RID: 10052
			// (set) Token: 0x0600457C RID: 17788 RVA: 0x00071B97 File Offset: 0x0006FD97
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002745 RID: 10053
			// (set) Token: 0x0600457D RID: 17789 RVA: 0x00071BAF File Offset: 0x0006FDAF
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
