using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A42 RID: 2626
	public class SetMailboxRelocationRequestCommand : SyntheticCommandWithPipelineInputNoOutput<MailboxRelocationRequestIdParameter>
	{
		// Token: 0x060082D7 RID: 33495 RVA: 0x000C1A02 File Offset: 0x000BFC02
		private SetMailboxRelocationRequestCommand() : base("Set-MailboxRelocationRequest")
		{
		}

		// Token: 0x060082D8 RID: 33496 RVA: 0x000C1A0F File Offset: 0x000BFC0F
		public SetMailboxRelocationRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060082D9 RID: 33497 RVA: 0x000C1A1E File Offset: 0x000BFC1E
		public virtual SetMailboxRelocationRequestCommand SetParameters(SetMailboxRelocationRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060082DA RID: 33498 RVA: 0x000C1A28 File Offset: 0x000BFC28
		public virtual SetMailboxRelocationRequestCommand SetParameters(SetMailboxRelocationRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060082DB RID: 33499 RVA: 0x000C1A32 File Offset: 0x000BFC32
		public virtual SetMailboxRelocationRequestCommand SetParameters(SetMailboxRelocationRequestCommand.RehomeParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A43 RID: 2627
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005A0C RID: 23052
			// (set) Token: 0x060082DC RID: 33500 RVA: 0x000C1A3C File Offset: 0x000BFC3C
			public virtual SkippableMoveComponent SkipMoving
			{
				set
				{
					base.PowerSharpParameters["SkipMoving"] = value;
				}
			}

			// Token: 0x17005A0D RID: 23053
			// (set) Token: 0x060082DD RID: 33501 RVA: 0x000C1A54 File Offset: 0x000BFC54
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x17005A0E RID: 23054
			// (set) Token: 0x060082DE RID: 33502 RVA: 0x000C1A6C File Offset: 0x000BFC6C
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x17005A0F RID: 23055
			// (set) Token: 0x060082DF RID: 33503 RVA: 0x000C1A84 File Offset: 0x000BFC84
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005A10 RID: 23056
			// (set) Token: 0x060082E0 RID: 33504 RVA: 0x000C1A97 File Offset: 0x000BFC97
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17005A11 RID: 23057
			// (set) Token: 0x060082E1 RID: 33505 RVA: 0x000C1AAF File Offset: 0x000BFCAF
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x17005A12 RID: 23058
			// (set) Token: 0x060082E2 RID: 33506 RVA: 0x000C1AC7 File Offset: 0x000BFCC7
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x17005A13 RID: 23059
			// (set) Token: 0x060082E3 RID: 33507 RVA: 0x000C1ADF File Offset: 0x000BFCDF
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxRelocationRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005A14 RID: 23060
			// (set) Token: 0x060082E4 RID: 33508 RVA: 0x000C1AFD File Offset: 0x000BFCFD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005A15 RID: 23061
			// (set) Token: 0x060082E5 RID: 33509 RVA: 0x000C1B10 File Offset: 0x000BFD10
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005A16 RID: 23062
			// (set) Token: 0x060082E6 RID: 33510 RVA: 0x000C1B28 File Offset: 0x000BFD28
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005A17 RID: 23063
			// (set) Token: 0x060082E7 RID: 33511 RVA: 0x000C1B40 File Offset: 0x000BFD40
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005A18 RID: 23064
			// (set) Token: 0x060082E8 RID: 33512 RVA: 0x000C1B58 File Offset: 0x000BFD58
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005A19 RID: 23065
			// (set) Token: 0x060082E9 RID: 33513 RVA: 0x000C1B70 File Offset: 0x000BFD70
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000A44 RID: 2628
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005A1A RID: 23066
			// (set) Token: 0x060082EB RID: 33515 RVA: 0x000C1B90 File Offset: 0x000BFD90
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxRelocationRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005A1B RID: 23067
			// (set) Token: 0x060082EC RID: 33516 RVA: 0x000C1BAE File Offset: 0x000BFDAE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005A1C RID: 23068
			// (set) Token: 0x060082ED RID: 33517 RVA: 0x000C1BC1 File Offset: 0x000BFDC1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005A1D RID: 23069
			// (set) Token: 0x060082EE RID: 33518 RVA: 0x000C1BD9 File Offset: 0x000BFDD9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005A1E RID: 23070
			// (set) Token: 0x060082EF RID: 33519 RVA: 0x000C1BF1 File Offset: 0x000BFDF1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005A1F RID: 23071
			// (set) Token: 0x060082F0 RID: 33520 RVA: 0x000C1C09 File Offset: 0x000BFE09
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005A20 RID: 23072
			// (set) Token: 0x060082F1 RID: 33521 RVA: 0x000C1C21 File Offset: 0x000BFE21
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000A45 RID: 2629
		public class RehomeParameters : ParametersBase
		{
			// Token: 0x17005A21 RID: 23073
			// (set) Token: 0x060082F3 RID: 33523 RVA: 0x000C1C41 File Offset: 0x000BFE41
			public virtual SwitchParameter RehomeRequest
			{
				set
				{
					base.PowerSharpParameters["RehomeRequest"] = value;
				}
			}

			// Token: 0x17005A22 RID: 23074
			// (set) Token: 0x060082F4 RID: 33524 RVA: 0x000C1C59 File Offset: 0x000BFE59
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxRelocationRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005A23 RID: 23075
			// (set) Token: 0x060082F5 RID: 33525 RVA: 0x000C1C77 File Offset: 0x000BFE77
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005A24 RID: 23076
			// (set) Token: 0x060082F6 RID: 33526 RVA: 0x000C1C8A File Offset: 0x000BFE8A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005A25 RID: 23077
			// (set) Token: 0x060082F7 RID: 33527 RVA: 0x000C1CA2 File Offset: 0x000BFEA2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005A26 RID: 23078
			// (set) Token: 0x060082F8 RID: 33528 RVA: 0x000C1CBA File Offset: 0x000BFEBA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005A27 RID: 23079
			// (set) Token: 0x060082F9 RID: 33529 RVA: 0x000C1CD2 File Offset: 0x000BFED2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005A28 RID: 23080
			// (set) Token: 0x060082FA RID: 33530 RVA: 0x000C1CEA File Offset: 0x000BFEEA
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
