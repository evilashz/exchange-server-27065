using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200038E RID: 910
	public class SetTenantRelocationRequestCommand : SyntheticCommandWithPipelineInputNoOutput<TenantRelocationRequest>
	{
		// Token: 0x06003908 RID: 14600 RVA: 0x00061D79 File Offset: 0x0005FF79
		private SetTenantRelocationRequestCommand() : base("Set-TenantRelocationRequest")
		{
		}

		// Token: 0x06003909 RID: 14601 RVA: 0x00061D86 File Offset: 0x0005FF86
		public SetTenantRelocationRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600390A RID: 14602 RVA: 0x00061D95 File Offset: 0x0005FF95
		public virtual SetTenantRelocationRequestCommand SetParameters(SetTenantRelocationRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600390B RID: 14603 RVA: 0x00061D9F File Offset: 0x0005FF9F
		public virtual SetTenantRelocationRequestCommand SetParameters(SetTenantRelocationRequestCommand.DefaultParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600390C RID: 14604 RVA: 0x00061DA9 File Offset: 0x0005FFA9
		public virtual SetTenantRelocationRequestCommand SetParameters(SetTenantRelocationRequestCommand.SuspendParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600390D RID: 14605 RVA: 0x00061DB3 File Offset: 0x0005FFB3
		public virtual SetTenantRelocationRequestCommand SetParameters(SetTenantRelocationRequestCommand.ResumeParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600390E RID: 14606 RVA: 0x00061DBD File Offset: 0x0005FFBD
		public virtual SetTenantRelocationRequestCommand SetParameters(SetTenantRelocationRequestCommand.ResetPermanentErrorParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600390F RID: 14607 RVA: 0x00061DC7 File Offset: 0x0005FFC7
		public virtual SetTenantRelocationRequestCommand SetParameters(SetTenantRelocationRequestCommand.ResetTransitionCounterParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200038F RID: 911
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001DA5 RID: 7589
			// (set) Token: 0x06003910 RID: 14608 RVA: 0x00061DD1 File Offset: 0x0005FFD1
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new TenantRelocationRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17001DA6 RID: 7590
			// (set) Token: 0x06003911 RID: 14609 RVA: 0x00061DEF File Offset: 0x0005FFEF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001DA7 RID: 7591
			// (set) Token: 0x06003912 RID: 14610 RVA: 0x00061E02 File Offset: 0x00060002
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001DA8 RID: 7592
			// (set) Token: 0x06003913 RID: 14611 RVA: 0x00061E1A File Offset: 0x0006001A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001DA9 RID: 7593
			// (set) Token: 0x06003914 RID: 14612 RVA: 0x00061E32 File Offset: 0x00060032
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001DAA RID: 7594
			// (set) Token: 0x06003915 RID: 14613 RVA: 0x00061E4A File Offset: 0x0006004A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001DAB RID: 7595
			// (set) Token: 0x06003916 RID: 14614 RVA: 0x00061E62 File Offset: 0x00060062
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000390 RID: 912
		public class DefaultParameterSetParameters : ParametersBase
		{
			// Token: 0x17001DAC RID: 7596
			// (set) Token: 0x06003918 RID: 14616 RVA: 0x00061E82 File Offset: 0x00060082
			public virtual RelocationStateRequestedByCmdlet RelocationStateRequested
			{
				set
				{
					base.PowerSharpParameters["RelocationStateRequested"] = value;
				}
			}

			// Token: 0x17001DAD RID: 7597
			// (set) Token: 0x06003919 RID: 14617 RVA: 0x00061E9A File Offset: 0x0006009A
			public virtual bool AutoCompletionEnabled
			{
				set
				{
					base.PowerSharpParameters["AutoCompletionEnabled"] = value;
				}
			}

			// Token: 0x17001DAE RID: 7598
			// (set) Token: 0x0600391A RID: 14618 RVA: 0x00061EB2 File Offset: 0x000600B2
			public virtual bool LargeTenantModeEnabled
			{
				set
				{
					base.PowerSharpParameters["LargeTenantModeEnabled"] = value;
				}
			}

			// Token: 0x17001DAF RID: 7599
			// (set) Token: 0x0600391B RID: 14619 RVA: 0x00061ECA File Offset: 0x000600CA
			public virtual Schedule SafeLockdownSchedule
			{
				set
				{
					base.PowerSharpParameters["SafeLockdownSchedule"] = value;
				}
			}

			// Token: 0x17001DB0 RID: 7600
			// (set) Token: 0x0600391C RID: 14620 RVA: 0x00061EDD File Offset: 0x000600DD
			public virtual SwitchParameter RollbackGls
			{
				set
				{
					base.PowerSharpParameters["RollbackGls"] = value;
				}
			}

			// Token: 0x17001DB1 RID: 7601
			// (set) Token: 0x0600391D RID: 14621 RVA: 0x00061EF5 File Offset: 0x000600F5
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new TenantRelocationRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17001DB2 RID: 7602
			// (set) Token: 0x0600391E RID: 14622 RVA: 0x00061F13 File Offset: 0x00060113
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001DB3 RID: 7603
			// (set) Token: 0x0600391F RID: 14623 RVA: 0x00061F26 File Offset: 0x00060126
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001DB4 RID: 7604
			// (set) Token: 0x06003920 RID: 14624 RVA: 0x00061F3E File Offset: 0x0006013E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001DB5 RID: 7605
			// (set) Token: 0x06003921 RID: 14625 RVA: 0x00061F56 File Offset: 0x00060156
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001DB6 RID: 7606
			// (set) Token: 0x06003922 RID: 14626 RVA: 0x00061F6E File Offset: 0x0006016E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001DB7 RID: 7607
			// (set) Token: 0x06003923 RID: 14627 RVA: 0x00061F86 File Offset: 0x00060186
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000391 RID: 913
		public class SuspendParameterSetParameters : ParametersBase
		{
			// Token: 0x17001DB8 RID: 7608
			// (set) Token: 0x06003925 RID: 14629 RVA: 0x00061FA6 File Offset: 0x000601A6
			public virtual SwitchParameter Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x17001DB9 RID: 7609
			// (set) Token: 0x06003926 RID: 14630 RVA: 0x00061FBE File Offset: 0x000601BE
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new TenantRelocationRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17001DBA RID: 7610
			// (set) Token: 0x06003927 RID: 14631 RVA: 0x00061FDC File Offset: 0x000601DC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001DBB RID: 7611
			// (set) Token: 0x06003928 RID: 14632 RVA: 0x00061FEF File Offset: 0x000601EF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001DBC RID: 7612
			// (set) Token: 0x06003929 RID: 14633 RVA: 0x00062007 File Offset: 0x00060207
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001DBD RID: 7613
			// (set) Token: 0x0600392A RID: 14634 RVA: 0x0006201F File Offset: 0x0006021F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001DBE RID: 7614
			// (set) Token: 0x0600392B RID: 14635 RVA: 0x00062037 File Offset: 0x00060237
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001DBF RID: 7615
			// (set) Token: 0x0600392C RID: 14636 RVA: 0x0006204F File Offset: 0x0006024F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000392 RID: 914
		public class ResumeParameterSetParameters : ParametersBase
		{
			// Token: 0x17001DC0 RID: 7616
			// (set) Token: 0x0600392E RID: 14638 RVA: 0x0006206F File Offset: 0x0006026F
			public virtual SwitchParameter Resume
			{
				set
				{
					base.PowerSharpParameters["Resume"] = value;
				}
			}

			// Token: 0x17001DC1 RID: 7617
			// (set) Token: 0x0600392F RID: 14639 RVA: 0x00062087 File Offset: 0x00060287
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new TenantRelocationRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17001DC2 RID: 7618
			// (set) Token: 0x06003930 RID: 14640 RVA: 0x000620A5 File Offset: 0x000602A5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001DC3 RID: 7619
			// (set) Token: 0x06003931 RID: 14641 RVA: 0x000620B8 File Offset: 0x000602B8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001DC4 RID: 7620
			// (set) Token: 0x06003932 RID: 14642 RVA: 0x000620D0 File Offset: 0x000602D0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001DC5 RID: 7621
			// (set) Token: 0x06003933 RID: 14643 RVA: 0x000620E8 File Offset: 0x000602E8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001DC6 RID: 7622
			// (set) Token: 0x06003934 RID: 14644 RVA: 0x00062100 File Offset: 0x00060300
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001DC7 RID: 7623
			// (set) Token: 0x06003935 RID: 14645 RVA: 0x00062118 File Offset: 0x00060318
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000393 RID: 915
		public class ResetPermanentErrorParameterSetParameters : ParametersBase
		{
			// Token: 0x17001DC8 RID: 7624
			// (set) Token: 0x06003937 RID: 14647 RVA: 0x00062138 File Offset: 0x00060338
			public virtual SwitchParameter ResetPermanentError
			{
				set
				{
					base.PowerSharpParameters["ResetPermanentError"] = value;
				}
			}

			// Token: 0x17001DC9 RID: 7625
			// (set) Token: 0x06003938 RID: 14648 RVA: 0x00062150 File Offset: 0x00060350
			public virtual SwitchParameter ResetStartSyncTime
			{
				set
				{
					base.PowerSharpParameters["ResetStartSyncTime"] = value;
				}
			}

			// Token: 0x17001DCA RID: 7626
			// (set) Token: 0x06003939 RID: 14649 RVA: 0x00062168 File Offset: 0x00060368
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new TenantRelocationRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17001DCB RID: 7627
			// (set) Token: 0x0600393A RID: 14650 RVA: 0x00062186 File Offset: 0x00060386
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001DCC RID: 7628
			// (set) Token: 0x0600393B RID: 14651 RVA: 0x00062199 File Offset: 0x00060399
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001DCD RID: 7629
			// (set) Token: 0x0600393C RID: 14652 RVA: 0x000621B1 File Offset: 0x000603B1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001DCE RID: 7630
			// (set) Token: 0x0600393D RID: 14653 RVA: 0x000621C9 File Offset: 0x000603C9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001DCF RID: 7631
			// (set) Token: 0x0600393E RID: 14654 RVA: 0x000621E1 File Offset: 0x000603E1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001DD0 RID: 7632
			// (set) Token: 0x0600393F RID: 14655 RVA: 0x000621F9 File Offset: 0x000603F9
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000394 RID: 916
		public class ResetTransitionCounterParameterSetParameters : ParametersBase
		{
			// Token: 0x17001DD1 RID: 7633
			// (set) Token: 0x06003941 RID: 14657 RVA: 0x00062219 File Offset: 0x00060419
			public virtual SwitchParameter ResetTransitionCounter
			{
				set
				{
					base.PowerSharpParameters["ResetTransitionCounter"] = value;
				}
			}

			// Token: 0x17001DD2 RID: 7634
			// (set) Token: 0x06003942 RID: 14658 RVA: 0x00062231 File Offset: 0x00060431
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new TenantRelocationRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17001DD3 RID: 7635
			// (set) Token: 0x06003943 RID: 14659 RVA: 0x0006224F File Offset: 0x0006044F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001DD4 RID: 7636
			// (set) Token: 0x06003944 RID: 14660 RVA: 0x00062262 File Offset: 0x00060462
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001DD5 RID: 7637
			// (set) Token: 0x06003945 RID: 14661 RVA: 0x0006227A File Offset: 0x0006047A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001DD6 RID: 7638
			// (set) Token: 0x06003946 RID: 14662 RVA: 0x00062292 File Offset: 0x00060492
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001DD7 RID: 7639
			// (set) Token: 0x06003947 RID: 14663 RVA: 0x000622AA File Offset: 0x000604AA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001DD8 RID: 7640
			// (set) Token: 0x06003948 RID: 14664 RVA: 0x000622C2 File Offset: 0x000604C2
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
