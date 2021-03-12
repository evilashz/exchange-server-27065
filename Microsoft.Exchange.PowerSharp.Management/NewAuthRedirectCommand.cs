using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020004DB RID: 1243
	public class NewAuthRedirectCommand : SyntheticCommandWithPipelineInput<AuthRedirect, AuthRedirect>
	{
		// Token: 0x060044B5 RID: 17589 RVA: 0x00070C32 File Offset: 0x0006EE32
		private NewAuthRedirectCommand() : base("New-AuthRedirect")
		{
		}

		// Token: 0x060044B6 RID: 17590 RVA: 0x00070C3F File Offset: 0x0006EE3F
		public NewAuthRedirectCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060044B7 RID: 17591 RVA: 0x00070C4E File Offset: 0x0006EE4E
		public virtual NewAuthRedirectCommand SetParameters(NewAuthRedirectCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020004DC RID: 1244
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170026B8 RID: 9912
			// (set) Token: 0x060044B8 RID: 17592 RVA: 0x00070C58 File Offset: 0x0006EE58
			public virtual AuthScheme AuthScheme
			{
				set
				{
					base.PowerSharpParameters["AuthScheme"] = value;
				}
			}

			// Token: 0x170026B9 RID: 9913
			// (set) Token: 0x060044B9 RID: 17593 RVA: 0x00070C70 File Offset: 0x0006EE70
			public virtual string TargetUrl
			{
				set
				{
					base.PowerSharpParameters["TargetUrl"] = value;
				}
			}

			// Token: 0x170026BA RID: 9914
			// (set) Token: 0x060044BA RID: 17594 RVA: 0x00070C83 File Offset: 0x0006EE83
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170026BB RID: 9915
			// (set) Token: 0x060044BB RID: 17595 RVA: 0x00070C96 File Offset: 0x0006EE96
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170026BC RID: 9916
			// (set) Token: 0x060044BC RID: 17596 RVA: 0x00070CAE File Offset: 0x0006EEAE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170026BD RID: 9917
			// (set) Token: 0x060044BD RID: 17597 RVA: 0x00070CC6 File Offset: 0x0006EEC6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170026BE RID: 9918
			// (set) Token: 0x060044BE RID: 17598 RVA: 0x00070CDE File Offset: 0x0006EEDE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170026BF RID: 9919
			// (set) Token: 0x060044BF RID: 17599 RVA: 0x00070CF6 File Offset: 0x0006EEF6
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
