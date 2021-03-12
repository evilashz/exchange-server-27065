using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020004D8 RID: 1240
	public class GetAuthRedirectCommand : SyntheticCommandWithPipelineInput<AuthRedirect, AuthRedirect>
	{
		// Token: 0x060044A4 RID: 17572 RVA: 0x00070AEE File Offset: 0x0006ECEE
		private GetAuthRedirectCommand() : base("Get-AuthRedirect")
		{
		}

		// Token: 0x060044A5 RID: 17573 RVA: 0x00070AFB File Offset: 0x0006ECFB
		public GetAuthRedirectCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060044A6 RID: 17574 RVA: 0x00070B0A File Offset: 0x0006ED0A
		public virtual GetAuthRedirectCommand SetParameters(GetAuthRedirectCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060044A7 RID: 17575 RVA: 0x00070B14 File Offset: 0x0006ED14
		public virtual GetAuthRedirectCommand SetParameters(GetAuthRedirectCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020004D9 RID: 1241
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170026AD RID: 9901
			// (set) Token: 0x060044A8 RID: 17576 RVA: 0x00070B1E File Offset: 0x0006ED1E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170026AE RID: 9902
			// (set) Token: 0x060044A9 RID: 17577 RVA: 0x00070B31 File Offset: 0x0006ED31
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170026AF RID: 9903
			// (set) Token: 0x060044AA RID: 17578 RVA: 0x00070B49 File Offset: 0x0006ED49
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170026B0 RID: 9904
			// (set) Token: 0x060044AB RID: 17579 RVA: 0x00070B61 File Offset: 0x0006ED61
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170026B1 RID: 9905
			// (set) Token: 0x060044AC RID: 17580 RVA: 0x00070B79 File Offset: 0x0006ED79
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020004DA RID: 1242
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170026B2 RID: 9906
			// (set) Token: 0x060044AE RID: 17582 RVA: 0x00070B99 File Offset: 0x0006ED99
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AuthRedirectIdParameter(value) : null);
				}
			}

			// Token: 0x170026B3 RID: 9907
			// (set) Token: 0x060044AF RID: 17583 RVA: 0x00070BB7 File Offset: 0x0006EDB7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170026B4 RID: 9908
			// (set) Token: 0x060044B0 RID: 17584 RVA: 0x00070BCA File Offset: 0x0006EDCA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170026B5 RID: 9909
			// (set) Token: 0x060044B1 RID: 17585 RVA: 0x00070BE2 File Offset: 0x0006EDE2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170026B6 RID: 9910
			// (set) Token: 0x060044B2 RID: 17586 RVA: 0x00070BFA File Offset: 0x0006EDFA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170026B7 RID: 9911
			// (set) Token: 0x060044B3 RID: 17587 RVA: 0x00070C12 File Offset: 0x0006EE12
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
