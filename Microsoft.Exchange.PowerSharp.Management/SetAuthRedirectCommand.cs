using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020004E0 RID: 1248
	public class SetAuthRedirectCommand : SyntheticCommandWithPipelineInputNoOutput<AuthRedirect>
	{
		// Token: 0x060044D6 RID: 17622 RVA: 0x00070EBA File Offset: 0x0006F0BA
		private SetAuthRedirectCommand() : base("Set-AuthRedirect")
		{
		}

		// Token: 0x060044D7 RID: 17623 RVA: 0x00070EC7 File Offset: 0x0006F0C7
		public SetAuthRedirectCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060044D8 RID: 17624 RVA: 0x00070ED6 File Offset: 0x0006F0D6
		public virtual SetAuthRedirectCommand SetParameters(SetAuthRedirectCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060044D9 RID: 17625 RVA: 0x00070EE0 File Offset: 0x0006F0E0
		public virtual SetAuthRedirectCommand SetParameters(SetAuthRedirectCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020004E1 RID: 1249
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170026CF RID: 9935
			// (set) Token: 0x060044DA RID: 17626 RVA: 0x00070EEA File Offset: 0x0006F0EA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170026D0 RID: 9936
			// (set) Token: 0x060044DB RID: 17627 RVA: 0x00070EFD File Offset: 0x0006F0FD
			public virtual string TargetUrl
			{
				set
				{
					base.PowerSharpParameters["TargetUrl"] = value;
				}
			}

			// Token: 0x170026D1 RID: 9937
			// (set) Token: 0x060044DC RID: 17628 RVA: 0x00070F10 File Offset: 0x0006F110
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170026D2 RID: 9938
			// (set) Token: 0x060044DD RID: 17629 RVA: 0x00070F28 File Offset: 0x0006F128
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170026D3 RID: 9939
			// (set) Token: 0x060044DE RID: 17630 RVA: 0x00070F40 File Offset: 0x0006F140
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170026D4 RID: 9940
			// (set) Token: 0x060044DF RID: 17631 RVA: 0x00070F58 File Offset: 0x0006F158
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170026D5 RID: 9941
			// (set) Token: 0x060044E0 RID: 17632 RVA: 0x00070F70 File Offset: 0x0006F170
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020004E2 RID: 1250
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170026D6 RID: 9942
			// (set) Token: 0x060044E2 RID: 17634 RVA: 0x00070F90 File Offset: 0x0006F190
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AuthRedirectIdParameter(value) : null);
				}
			}

			// Token: 0x170026D7 RID: 9943
			// (set) Token: 0x060044E3 RID: 17635 RVA: 0x00070FAE File Offset: 0x0006F1AE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170026D8 RID: 9944
			// (set) Token: 0x060044E4 RID: 17636 RVA: 0x00070FC1 File Offset: 0x0006F1C1
			public virtual string TargetUrl
			{
				set
				{
					base.PowerSharpParameters["TargetUrl"] = value;
				}
			}

			// Token: 0x170026D9 RID: 9945
			// (set) Token: 0x060044E5 RID: 17637 RVA: 0x00070FD4 File Offset: 0x0006F1D4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170026DA RID: 9946
			// (set) Token: 0x060044E6 RID: 17638 RVA: 0x00070FEC File Offset: 0x0006F1EC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170026DB RID: 9947
			// (set) Token: 0x060044E7 RID: 17639 RVA: 0x00071004 File Offset: 0x0006F204
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170026DC RID: 9948
			// (set) Token: 0x060044E8 RID: 17640 RVA: 0x0007101C File Offset: 0x0006F21C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170026DD RID: 9949
			// (set) Token: 0x060044E9 RID: 17641 RVA: 0x00071034 File Offset: 0x0006F234
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
