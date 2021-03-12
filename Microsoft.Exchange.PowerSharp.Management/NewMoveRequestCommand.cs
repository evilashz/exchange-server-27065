using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020009D4 RID: 2516
	public class NewMoveRequestCommand : SyntheticCommandWithPipelineInput<TransactionalRequestJob, TransactionalRequestJob>
	{
		// Token: 0x06007DF8 RID: 32248 RVA: 0x000BB423 File Offset: 0x000B9623
		private NewMoveRequestCommand() : base("New-MoveRequest")
		{
		}

		// Token: 0x06007DF9 RID: 32249 RVA: 0x000BB430 File Offset: 0x000B9630
		public NewMoveRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007DFA RID: 32250 RVA: 0x000BB43F File Offset: 0x000B963F
		public virtual NewMoveRequestCommand SetParameters(NewMoveRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007DFB RID: 32251 RVA: 0x000BB449 File Offset: 0x000B9649
		public virtual NewMoveRequestCommand SetParameters(NewMoveRequestCommand.MigrationRemoteLegacyParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007DFC RID: 32252 RVA: 0x000BB453 File Offset: 0x000B9653
		public virtual NewMoveRequestCommand SetParameters(NewMoveRequestCommand.MigrationRemoteParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007DFD RID: 32253 RVA: 0x000BB45D File Offset: 0x000B965D
		public virtual NewMoveRequestCommand SetParameters(NewMoveRequestCommand.MigrationLocalParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007DFE RID: 32254 RVA: 0x000BB467 File Offset: 0x000B9667
		public virtual NewMoveRequestCommand SetParameters(NewMoveRequestCommand.MigrationOutboundParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020009D5 RID: 2517
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005609 RID: 22025
			// (set) Token: 0x06007DFF RID: 32255 RVA: 0x000BB471 File Offset: 0x000B9671
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x1700560A RID: 22026
			// (set) Token: 0x06007E00 RID: 32256 RVA: 0x000BB48F File Offset: 0x000B968F
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x1700560B RID: 22027
			// (set) Token: 0x06007E01 RID: 32257 RVA: 0x000BB4A7 File Offset: 0x000B96A7
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x1700560C RID: 22028
			// (set) Token: 0x06007E02 RID: 32258 RVA: 0x000BB4BF File Offset: 0x000B96BF
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x1700560D RID: 22029
			// (set) Token: 0x06007E03 RID: 32259 RVA: 0x000BB4D7 File Offset: 0x000B96D7
			public virtual SwitchParameter AllowLargeItems
			{
				set
				{
					base.PowerSharpParameters["AllowLargeItems"] = value;
				}
			}

			// Token: 0x1700560E RID: 22030
			// (set) Token: 0x06007E04 RID: 32260 RVA: 0x000BB4EF File Offset: 0x000B96EF
			public virtual SwitchParameter CheckInitialProvisioningSetting
			{
				set
				{
					base.PowerSharpParameters["CheckInitialProvisioningSetting"] = value;
				}
			}

			// Token: 0x1700560F RID: 22031
			// (set) Token: 0x06007E05 RID: 32261 RVA: 0x000BB507 File Offset: 0x000B9707
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005610 RID: 22032
			// (set) Token: 0x06007E06 RID: 32262 RVA: 0x000BB51A File Offset: 0x000B971A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005611 RID: 22033
			// (set) Token: 0x06007E07 RID: 32263 RVA: 0x000BB52D File Offset: 0x000B972D
			public virtual SwitchParameter Protect
			{
				set
				{
					base.PowerSharpParameters["Protect"] = value;
				}
			}

			// Token: 0x17005612 RID: 22034
			// (set) Token: 0x06007E08 RID: 32264 RVA: 0x000BB545 File Offset: 0x000B9745
			public virtual SwitchParameter SuspendWhenReadyToComplete
			{
				set
				{
					base.PowerSharpParameters["SuspendWhenReadyToComplete"] = value;
				}
			}

			// Token: 0x17005613 RID: 22035
			// (set) Token: 0x06007E09 RID: 32265 RVA: 0x000BB55D File Offset: 0x000B975D
			public virtual SwitchParameter Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x17005614 RID: 22036
			// (set) Token: 0x06007E0A RID: 32266 RVA: 0x000BB575 File Offset: 0x000B9775
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005615 RID: 22037
			// (set) Token: 0x06007E0B RID: 32267 RVA: 0x000BB588 File Offset: 0x000B9788
			public virtual SwitchParameter IgnoreRuleLimitErrors
			{
				set
				{
					base.PowerSharpParameters["IgnoreRuleLimitErrors"] = value;
				}
			}

			// Token: 0x17005616 RID: 22038
			// (set) Token: 0x06007E0C RID: 32268 RVA: 0x000BB5A0 File Offset: 0x000B97A0
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17005617 RID: 22039
			// (set) Token: 0x06007E0D RID: 32269 RVA: 0x000BB5B8 File Offset: 0x000B97B8
			public virtual RequestWorkloadType WorkloadType
			{
				set
				{
					base.PowerSharpParameters["WorkloadType"] = value;
				}
			}

			// Token: 0x17005618 RID: 22040
			// (set) Token: 0x06007E0E RID: 32270 RVA: 0x000BB5D0 File Offset: 0x000B97D0
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x17005619 RID: 22041
			// (set) Token: 0x06007E0F RID: 32271 RVA: 0x000BB5E8 File Offset: 0x000B97E8
			public virtual SwitchParameter ForceOffline
			{
				set
				{
					base.PowerSharpParameters["ForceOffline"] = value;
				}
			}

			// Token: 0x1700561A RID: 22042
			// (set) Token: 0x06007E10 RID: 32272 RVA: 0x000BB600 File Offset: 0x000B9800
			public virtual SwitchParameter PreventCompletion
			{
				set
				{
					base.PowerSharpParameters["PreventCompletion"] = value;
				}
			}

			// Token: 0x1700561B RID: 22043
			// (set) Token: 0x06007E11 RID: 32273 RVA: 0x000BB618 File Offset: 0x000B9818
			public virtual SkippableMoveComponent SkipMoving
			{
				set
				{
					base.PowerSharpParameters["SkipMoving"] = value;
				}
			}

			// Token: 0x1700561C RID: 22044
			// (set) Token: 0x06007E12 RID: 32274 RVA: 0x000BB630 File Offset: 0x000B9830
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x1700561D RID: 22045
			// (set) Token: 0x06007E13 RID: 32275 RVA: 0x000BB648 File Offset: 0x000B9848
			public virtual DateTime StartAfter
			{
				set
				{
					base.PowerSharpParameters["StartAfter"] = value;
				}
			}

			// Token: 0x1700561E RID: 22046
			// (set) Token: 0x06007E14 RID: 32276 RVA: 0x000BB660 File Offset: 0x000B9860
			public virtual DateTime CompleteAfter
			{
				set
				{
					base.PowerSharpParameters["CompleteAfter"] = value;
				}
			}

			// Token: 0x1700561F RID: 22047
			// (set) Token: 0x06007E15 RID: 32277 RVA: 0x000BB678 File Offset: 0x000B9878
			public virtual TimeSpan IncrementalSyncInterval
			{
				set
				{
					base.PowerSharpParameters["IncrementalSyncInterval"] = value;
				}
			}

			// Token: 0x17005620 RID: 22048
			// (set) Token: 0x06007E16 RID: 32278 RVA: 0x000BB690 File Offset: 0x000B9890
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005621 RID: 22049
			// (set) Token: 0x06007E17 RID: 32279 RVA: 0x000BB6A8 File Offset: 0x000B98A8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005622 RID: 22050
			// (set) Token: 0x06007E18 RID: 32280 RVA: 0x000BB6C0 File Offset: 0x000B98C0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005623 RID: 22051
			// (set) Token: 0x06007E19 RID: 32281 RVA: 0x000BB6D8 File Offset: 0x000B98D8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005624 RID: 22052
			// (set) Token: 0x06007E1A RID: 32282 RVA: 0x000BB6F0 File Offset: 0x000B98F0
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020009D6 RID: 2518
		public class MigrationRemoteLegacyParameters : ParametersBase
		{
			// Token: 0x17005625 RID: 22053
			// (set) Token: 0x06007E1C RID: 32284 RVA: 0x000BB710 File Offset: 0x000B9910
			public virtual DatabaseIdParameter TargetDatabase
			{
				set
				{
					base.PowerSharpParameters["TargetDatabase"] = value;
				}
			}

			// Token: 0x17005626 RID: 22054
			// (set) Token: 0x06007E1D RID: 32285 RVA: 0x000BB723 File Offset: 0x000B9923
			public virtual SwitchParameter IgnoreTenantMigrationPolicies
			{
				set
				{
					base.PowerSharpParameters["IgnoreTenantMigrationPolicies"] = value;
				}
			}

			// Token: 0x17005627 RID: 22055
			// (set) Token: 0x06007E1E RID: 32286 RVA: 0x000BB73B File Offset: 0x000B993B
			public virtual string RemoteTargetDatabase
			{
				set
				{
					base.PowerSharpParameters["RemoteTargetDatabase"] = value;
				}
			}

			// Token: 0x17005628 RID: 22056
			// (set) Token: 0x06007E1F RID: 32287 RVA: 0x000BB74E File Offset: 0x000B994E
			public virtual PSCredential RemoteCredential
			{
				set
				{
					base.PowerSharpParameters["RemoteCredential"] = value;
				}
			}

			// Token: 0x17005629 RID: 22057
			// (set) Token: 0x06007E20 RID: 32288 RVA: 0x000BB761 File Offset: 0x000B9961
			public virtual SwitchParameter RemoteLegacy
			{
				set
				{
					base.PowerSharpParameters["RemoteLegacy"] = value;
				}
			}

			// Token: 0x1700562A RID: 22058
			// (set) Token: 0x06007E21 RID: 32289 RVA: 0x000BB779 File Offset: 0x000B9979
			public virtual Fqdn RemoteGlobalCatalog
			{
				set
				{
					base.PowerSharpParameters["RemoteGlobalCatalog"] = value;
				}
			}

			// Token: 0x1700562B RID: 22059
			// (set) Token: 0x06007E22 RID: 32290 RVA: 0x000BB78C File Offset: 0x000B998C
			public virtual Fqdn TargetDeliveryDomain
			{
				set
				{
					base.PowerSharpParameters["TargetDeliveryDomain"] = value;
				}
			}

			// Token: 0x1700562C RID: 22060
			// (set) Token: 0x06007E23 RID: 32291 RVA: 0x000BB79F File Offset: 0x000B999F
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x1700562D RID: 22061
			// (set) Token: 0x06007E24 RID: 32292 RVA: 0x000BB7BD File Offset: 0x000B99BD
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x1700562E RID: 22062
			// (set) Token: 0x06007E25 RID: 32293 RVA: 0x000BB7D5 File Offset: 0x000B99D5
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x1700562F RID: 22063
			// (set) Token: 0x06007E26 RID: 32294 RVA: 0x000BB7ED File Offset: 0x000B99ED
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x17005630 RID: 22064
			// (set) Token: 0x06007E27 RID: 32295 RVA: 0x000BB805 File Offset: 0x000B9A05
			public virtual SwitchParameter AllowLargeItems
			{
				set
				{
					base.PowerSharpParameters["AllowLargeItems"] = value;
				}
			}

			// Token: 0x17005631 RID: 22065
			// (set) Token: 0x06007E28 RID: 32296 RVA: 0x000BB81D File Offset: 0x000B9A1D
			public virtual SwitchParameter CheckInitialProvisioningSetting
			{
				set
				{
					base.PowerSharpParameters["CheckInitialProvisioningSetting"] = value;
				}
			}

			// Token: 0x17005632 RID: 22066
			// (set) Token: 0x06007E29 RID: 32297 RVA: 0x000BB835 File Offset: 0x000B9A35
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005633 RID: 22067
			// (set) Token: 0x06007E2A RID: 32298 RVA: 0x000BB848 File Offset: 0x000B9A48
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005634 RID: 22068
			// (set) Token: 0x06007E2B RID: 32299 RVA: 0x000BB85B File Offset: 0x000B9A5B
			public virtual SwitchParameter Protect
			{
				set
				{
					base.PowerSharpParameters["Protect"] = value;
				}
			}

			// Token: 0x17005635 RID: 22069
			// (set) Token: 0x06007E2C RID: 32300 RVA: 0x000BB873 File Offset: 0x000B9A73
			public virtual SwitchParameter SuspendWhenReadyToComplete
			{
				set
				{
					base.PowerSharpParameters["SuspendWhenReadyToComplete"] = value;
				}
			}

			// Token: 0x17005636 RID: 22070
			// (set) Token: 0x06007E2D RID: 32301 RVA: 0x000BB88B File Offset: 0x000B9A8B
			public virtual SwitchParameter Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x17005637 RID: 22071
			// (set) Token: 0x06007E2E RID: 32302 RVA: 0x000BB8A3 File Offset: 0x000B9AA3
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005638 RID: 22072
			// (set) Token: 0x06007E2F RID: 32303 RVA: 0x000BB8B6 File Offset: 0x000B9AB6
			public virtual SwitchParameter IgnoreRuleLimitErrors
			{
				set
				{
					base.PowerSharpParameters["IgnoreRuleLimitErrors"] = value;
				}
			}

			// Token: 0x17005639 RID: 22073
			// (set) Token: 0x06007E30 RID: 32304 RVA: 0x000BB8CE File Offset: 0x000B9ACE
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x1700563A RID: 22074
			// (set) Token: 0x06007E31 RID: 32305 RVA: 0x000BB8E6 File Offset: 0x000B9AE6
			public virtual RequestWorkloadType WorkloadType
			{
				set
				{
					base.PowerSharpParameters["WorkloadType"] = value;
				}
			}

			// Token: 0x1700563B RID: 22075
			// (set) Token: 0x06007E32 RID: 32306 RVA: 0x000BB8FE File Offset: 0x000B9AFE
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x1700563C RID: 22076
			// (set) Token: 0x06007E33 RID: 32307 RVA: 0x000BB916 File Offset: 0x000B9B16
			public virtual SwitchParameter ForceOffline
			{
				set
				{
					base.PowerSharpParameters["ForceOffline"] = value;
				}
			}

			// Token: 0x1700563D RID: 22077
			// (set) Token: 0x06007E34 RID: 32308 RVA: 0x000BB92E File Offset: 0x000B9B2E
			public virtual SwitchParameter PreventCompletion
			{
				set
				{
					base.PowerSharpParameters["PreventCompletion"] = value;
				}
			}

			// Token: 0x1700563E RID: 22078
			// (set) Token: 0x06007E35 RID: 32309 RVA: 0x000BB946 File Offset: 0x000B9B46
			public virtual SkippableMoveComponent SkipMoving
			{
				set
				{
					base.PowerSharpParameters["SkipMoving"] = value;
				}
			}

			// Token: 0x1700563F RID: 22079
			// (set) Token: 0x06007E36 RID: 32310 RVA: 0x000BB95E File Offset: 0x000B9B5E
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x17005640 RID: 22080
			// (set) Token: 0x06007E37 RID: 32311 RVA: 0x000BB976 File Offset: 0x000B9B76
			public virtual DateTime StartAfter
			{
				set
				{
					base.PowerSharpParameters["StartAfter"] = value;
				}
			}

			// Token: 0x17005641 RID: 22081
			// (set) Token: 0x06007E38 RID: 32312 RVA: 0x000BB98E File Offset: 0x000B9B8E
			public virtual DateTime CompleteAfter
			{
				set
				{
					base.PowerSharpParameters["CompleteAfter"] = value;
				}
			}

			// Token: 0x17005642 RID: 22082
			// (set) Token: 0x06007E39 RID: 32313 RVA: 0x000BB9A6 File Offset: 0x000B9BA6
			public virtual TimeSpan IncrementalSyncInterval
			{
				set
				{
					base.PowerSharpParameters["IncrementalSyncInterval"] = value;
				}
			}

			// Token: 0x17005643 RID: 22083
			// (set) Token: 0x06007E3A RID: 32314 RVA: 0x000BB9BE File Offset: 0x000B9BBE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005644 RID: 22084
			// (set) Token: 0x06007E3B RID: 32315 RVA: 0x000BB9D6 File Offset: 0x000B9BD6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005645 RID: 22085
			// (set) Token: 0x06007E3C RID: 32316 RVA: 0x000BB9EE File Offset: 0x000B9BEE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005646 RID: 22086
			// (set) Token: 0x06007E3D RID: 32317 RVA: 0x000BBA06 File Offset: 0x000B9C06
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005647 RID: 22087
			// (set) Token: 0x06007E3E RID: 32318 RVA: 0x000BBA1E File Offset: 0x000B9C1E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020009D7 RID: 2519
		public class MigrationRemoteParameters : ParametersBase
		{
			// Token: 0x17005648 RID: 22088
			// (set) Token: 0x06007E40 RID: 32320 RVA: 0x000BBA3E File Offset: 0x000B9C3E
			public virtual DatabaseIdParameter TargetDatabase
			{
				set
				{
					base.PowerSharpParameters["TargetDatabase"] = value;
				}
			}

			// Token: 0x17005649 RID: 22089
			// (set) Token: 0x06007E41 RID: 32321 RVA: 0x000BBA51 File Offset: 0x000B9C51
			public virtual DatabaseIdParameter ArchiveTargetDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveTargetDatabase"] = value;
				}
			}

			// Token: 0x1700564A RID: 22090
			// (set) Token: 0x06007E42 RID: 32322 RVA: 0x000BBA64 File Offset: 0x000B9C64
			public virtual SwitchParameter PrimaryOnly
			{
				set
				{
					base.PowerSharpParameters["PrimaryOnly"] = value;
				}
			}

			// Token: 0x1700564B RID: 22091
			// (set) Token: 0x06007E43 RID: 32323 RVA: 0x000BBA7C File Offset: 0x000B9C7C
			public virtual SwitchParameter ArchiveOnly
			{
				set
				{
					base.PowerSharpParameters["ArchiveOnly"] = value;
				}
			}

			// Token: 0x1700564C RID: 22092
			// (set) Token: 0x06007E44 RID: 32324 RVA: 0x000BBA94 File Offset: 0x000B9C94
			public virtual SwitchParameter IgnoreTenantMigrationPolicies
			{
				set
				{
					base.PowerSharpParameters["IgnoreTenantMigrationPolicies"] = value;
				}
			}

			// Token: 0x1700564D RID: 22093
			// (set) Token: 0x06007E45 RID: 32325 RVA: 0x000BBAAC File Offset: 0x000B9CAC
			public virtual Fqdn RemoteHostName
			{
				set
				{
					base.PowerSharpParameters["RemoteHostName"] = value;
				}
			}

			// Token: 0x1700564E RID: 22094
			// (set) Token: 0x06007E46 RID: 32326 RVA: 0x000BBABF File Offset: 0x000B9CBF
			public virtual string RemoteOrganizationName
			{
				set
				{
					base.PowerSharpParameters["RemoteOrganizationName"] = value;
				}
			}

			// Token: 0x1700564F RID: 22095
			// (set) Token: 0x06007E47 RID: 32327 RVA: 0x000BBAD2 File Offset: 0x000B9CD2
			public virtual string ArchiveDomain
			{
				set
				{
					base.PowerSharpParameters["ArchiveDomain"] = value;
				}
			}

			// Token: 0x17005650 RID: 22096
			// (set) Token: 0x06007E48 RID: 32328 RVA: 0x000BBAE5 File Offset: 0x000B9CE5
			public virtual PSCredential RemoteCredential
			{
				set
				{
					base.PowerSharpParameters["RemoteCredential"] = value;
				}
			}

			// Token: 0x17005651 RID: 22097
			// (set) Token: 0x06007E49 RID: 32329 RVA: 0x000BBAF8 File Offset: 0x000B9CF8
			public virtual SwitchParameter Remote
			{
				set
				{
					base.PowerSharpParameters["Remote"] = value;
				}
			}

			// Token: 0x17005652 RID: 22098
			// (set) Token: 0x06007E4A RID: 32330 RVA: 0x000BBB10 File Offset: 0x000B9D10
			public virtual Fqdn RemoteGlobalCatalog
			{
				set
				{
					base.PowerSharpParameters["RemoteGlobalCatalog"] = value;
				}
			}

			// Token: 0x17005653 RID: 22099
			// (set) Token: 0x06007E4B RID: 32331 RVA: 0x000BBB23 File Offset: 0x000B9D23
			public virtual Fqdn TargetDeliveryDomain
			{
				set
				{
					base.PowerSharpParameters["TargetDeliveryDomain"] = value;
				}
			}

			// Token: 0x17005654 RID: 22100
			// (set) Token: 0x06007E4C RID: 32332 RVA: 0x000BBB36 File Offset: 0x000B9D36
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x17005655 RID: 22101
			// (set) Token: 0x06007E4D RID: 32333 RVA: 0x000BBB54 File Offset: 0x000B9D54
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x17005656 RID: 22102
			// (set) Token: 0x06007E4E RID: 32334 RVA: 0x000BBB6C File Offset: 0x000B9D6C
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x17005657 RID: 22103
			// (set) Token: 0x06007E4F RID: 32335 RVA: 0x000BBB84 File Offset: 0x000B9D84
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x17005658 RID: 22104
			// (set) Token: 0x06007E50 RID: 32336 RVA: 0x000BBB9C File Offset: 0x000B9D9C
			public virtual SwitchParameter AllowLargeItems
			{
				set
				{
					base.PowerSharpParameters["AllowLargeItems"] = value;
				}
			}

			// Token: 0x17005659 RID: 22105
			// (set) Token: 0x06007E51 RID: 32337 RVA: 0x000BBBB4 File Offset: 0x000B9DB4
			public virtual SwitchParameter CheckInitialProvisioningSetting
			{
				set
				{
					base.PowerSharpParameters["CheckInitialProvisioningSetting"] = value;
				}
			}

			// Token: 0x1700565A RID: 22106
			// (set) Token: 0x06007E52 RID: 32338 RVA: 0x000BBBCC File Offset: 0x000B9DCC
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x1700565B RID: 22107
			// (set) Token: 0x06007E53 RID: 32339 RVA: 0x000BBBDF File Offset: 0x000B9DDF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700565C RID: 22108
			// (set) Token: 0x06007E54 RID: 32340 RVA: 0x000BBBF2 File Offset: 0x000B9DF2
			public virtual SwitchParameter Protect
			{
				set
				{
					base.PowerSharpParameters["Protect"] = value;
				}
			}

			// Token: 0x1700565D RID: 22109
			// (set) Token: 0x06007E55 RID: 32341 RVA: 0x000BBC0A File Offset: 0x000B9E0A
			public virtual SwitchParameter SuspendWhenReadyToComplete
			{
				set
				{
					base.PowerSharpParameters["SuspendWhenReadyToComplete"] = value;
				}
			}

			// Token: 0x1700565E RID: 22110
			// (set) Token: 0x06007E56 RID: 32342 RVA: 0x000BBC22 File Offset: 0x000B9E22
			public virtual SwitchParameter Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x1700565F RID: 22111
			// (set) Token: 0x06007E57 RID: 32343 RVA: 0x000BBC3A File Offset: 0x000B9E3A
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005660 RID: 22112
			// (set) Token: 0x06007E58 RID: 32344 RVA: 0x000BBC4D File Offset: 0x000B9E4D
			public virtual SwitchParameter IgnoreRuleLimitErrors
			{
				set
				{
					base.PowerSharpParameters["IgnoreRuleLimitErrors"] = value;
				}
			}

			// Token: 0x17005661 RID: 22113
			// (set) Token: 0x06007E59 RID: 32345 RVA: 0x000BBC65 File Offset: 0x000B9E65
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17005662 RID: 22114
			// (set) Token: 0x06007E5A RID: 32346 RVA: 0x000BBC7D File Offset: 0x000B9E7D
			public virtual RequestWorkloadType WorkloadType
			{
				set
				{
					base.PowerSharpParameters["WorkloadType"] = value;
				}
			}

			// Token: 0x17005663 RID: 22115
			// (set) Token: 0x06007E5B RID: 32347 RVA: 0x000BBC95 File Offset: 0x000B9E95
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x17005664 RID: 22116
			// (set) Token: 0x06007E5C RID: 32348 RVA: 0x000BBCAD File Offset: 0x000B9EAD
			public virtual SwitchParameter ForceOffline
			{
				set
				{
					base.PowerSharpParameters["ForceOffline"] = value;
				}
			}

			// Token: 0x17005665 RID: 22117
			// (set) Token: 0x06007E5D RID: 32349 RVA: 0x000BBCC5 File Offset: 0x000B9EC5
			public virtual SwitchParameter PreventCompletion
			{
				set
				{
					base.PowerSharpParameters["PreventCompletion"] = value;
				}
			}

			// Token: 0x17005666 RID: 22118
			// (set) Token: 0x06007E5E RID: 32350 RVA: 0x000BBCDD File Offset: 0x000B9EDD
			public virtual SkippableMoveComponent SkipMoving
			{
				set
				{
					base.PowerSharpParameters["SkipMoving"] = value;
				}
			}

			// Token: 0x17005667 RID: 22119
			// (set) Token: 0x06007E5F RID: 32351 RVA: 0x000BBCF5 File Offset: 0x000B9EF5
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x17005668 RID: 22120
			// (set) Token: 0x06007E60 RID: 32352 RVA: 0x000BBD0D File Offset: 0x000B9F0D
			public virtual DateTime StartAfter
			{
				set
				{
					base.PowerSharpParameters["StartAfter"] = value;
				}
			}

			// Token: 0x17005669 RID: 22121
			// (set) Token: 0x06007E61 RID: 32353 RVA: 0x000BBD25 File Offset: 0x000B9F25
			public virtual DateTime CompleteAfter
			{
				set
				{
					base.PowerSharpParameters["CompleteAfter"] = value;
				}
			}

			// Token: 0x1700566A RID: 22122
			// (set) Token: 0x06007E62 RID: 32354 RVA: 0x000BBD3D File Offset: 0x000B9F3D
			public virtual TimeSpan IncrementalSyncInterval
			{
				set
				{
					base.PowerSharpParameters["IncrementalSyncInterval"] = value;
				}
			}

			// Token: 0x1700566B RID: 22123
			// (set) Token: 0x06007E63 RID: 32355 RVA: 0x000BBD55 File Offset: 0x000B9F55
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700566C RID: 22124
			// (set) Token: 0x06007E64 RID: 32356 RVA: 0x000BBD6D File Offset: 0x000B9F6D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700566D RID: 22125
			// (set) Token: 0x06007E65 RID: 32357 RVA: 0x000BBD85 File Offset: 0x000B9F85
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700566E RID: 22126
			// (set) Token: 0x06007E66 RID: 32358 RVA: 0x000BBD9D File Offset: 0x000B9F9D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700566F RID: 22127
			// (set) Token: 0x06007E67 RID: 32359 RVA: 0x000BBDB5 File Offset: 0x000B9FB5
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020009D8 RID: 2520
		public class MigrationLocalParameters : ParametersBase
		{
			// Token: 0x17005670 RID: 22128
			// (set) Token: 0x06007E69 RID: 32361 RVA: 0x000BBDD5 File Offset: 0x000B9FD5
			public virtual DatabaseIdParameter TargetDatabase
			{
				set
				{
					base.PowerSharpParameters["TargetDatabase"] = value;
				}
			}

			// Token: 0x17005671 RID: 22129
			// (set) Token: 0x06007E6A RID: 32362 RVA: 0x000BBDE8 File Offset: 0x000B9FE8
			public virtual DatabaseIdParameter ArchiveTargetDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveTargetDatabase"] = value;
				}
			}

			// Token: 0x17005672 RID: 22130
			// (set) Token: 0x06007E6B RID: 32363 RVA: 0x000BBDFB File Offset: 0x000B9FFB
			public virtual SwitchParameter PrimaryOnly
			{
				set
				{
					base.PowerSharpParameters["PrimaryOnly"] = value;
				}
			}

			// Token: 0x17005673 RID: 22131
			// (set) Token: 0x06007E6C RID: 32364 RVA: 0x000BBE13 File Offset: 0x000BA013
			public virtual SwitchParameter ArchiveOnly
			{
				set
				{
					base.PowerSharpParameters["ArchiveOnly"] = value;
				}
			}

			// Token: 0x17005674 RID: 22132
			// (set) Token: 0x06007E6D RID: 32365 RVA: 0x000BBE2B File Offset: 0x000BA02B
			public virtual SwitchParameter DoNotPreserveMailboxSignature
			{
				set
				{
					base.PowerSharpParameters["DoNotPreserveMailboxSignature"] = value;
				}
			}

			// Token: 0x17005675 RID: 22133
			// (set) Token: 0x06007E6E RID: 32366 RVA: 0x000BBE43 File Offset: 0x000BA043
			public virtual SwitchParameter ForcePull
			{
				set
				{
					base.PowerSharpParameters["ForcePull"] = value;
				}
			}

			// Token: 0x17005676 RID: 22134
			// (set) Token: 0x06007E6F RID: 32367 RVA: 0x000BBE5B File Offset: 0x000BA05B
			public virtual SwitchParameter ForcePush
			{
				set
				{
					base.PowerSharpParameters["ForcePush"] = value;
				}
			}

			// Token: 0x17005677 RID: 22135
			// (set) Token: 0x06007E70 RID: 32368 RVA: 0x000BBE73 File Offset: 0x000BA073
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x17005678 RID: 22136
			// (set) Token: 0x06007E71 RID: 32369 RVA: 0x000BBE91 File Offset: 0x000BA091
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x17005679 RID: 22137
			// (set) Token: 0x06007E72 RID: 32370 RVA: 0x000BBEA9 File Offset: 0x000BA0A9
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x1700567A RID: 22138
			// (set) Token: 0x06007E73 RID: 32371 RVA: 0x000BBEC1 File Offset: 0x000BA0C1
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x1700567B RID: 22139
			// (set) Token: 0x06007E74 RID: 32372 RVA: 0x000BBED9 File Offset: 0x000BA0D9
			public virtual SwitchParameter AllowLargeItems
			{
				set
				{
					base.PowerSharpParameters["AllowLargeItems"] = value;
				}
			}

			// Token: 0x1700567C RID: 22140
			// (set) Token: 0x06007E75 RID: 32373 RVA: 0x000BBEF1 File Offset: 0x000BA0F1
			public virtual SwitchParameter CheckInitialProvisioningSetting
			{
				set
				{
					base.PowerSharpParameters["CheckInitialProvisioningSetting"] = value;
				}
			}

			// Token: 0x1700567D RID: 22141
			// (set) Token: 0x06007E76 RID: 32374 RVA: 0x000BBF09 File Offset: 0x000BA109
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x1700567E RID: 22142
			// (set) Token: 0x06007E77 RID: 32375 RVA: 0x000BBF1C File Offset: 0x000BA11C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700567F RID: 22143
			// (set) Token: 0x06007E78 RID: 32376 RVA: 0x000BBF2F File Offset: 0x000BA12F
			public virtual SwitchParameter Protect
			{
				set
				{
					base.PowerSharpParameters["Protect"] = value;
				}
			}

			// Token: 0x17005680 RID: 22144
			// (set) Token: 0x06007E79 RID: 32377 RVA: 0x000BBF47 File Offset: 0x000BA147
			public virtual SwitchParameter SuspendWhenReadyToComplete
			{
				set
				{
					base.PowerSharpParameters["SuspendWhenReadyToComplete"] = value;
				}
			}

			// Token: 0x17005681 RID: 22145
			// (set) Token: 0x06007E7A RID: 32378 RVA: 0x000BBF5F File Offset: 0x000BA15F
			public virtual SwitchParameter Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x17005682 RID: 22146
			// (set) Token: 0x06007E7B RID: 32379 RVA: 0x000BBF77 File Offset: 0x000BA177
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005683 RID: 22147
			// (set) Token: 0x06007E7C RID: 32380 RVA: 0x000BBF8A File Offset: 0x000BA18A
			public virtual SwitchParameter IgnoreRuleLimitErrors
			{
				set
				{
					base.PowerSharpParameters["IgnoreRuleLimitErrors"] = value;
				}
			}

			// Token: 0x17005684 RID: 22148
			// (set) Token: 0x06007E7D RID: 32381 RVA: 0x000BBFA2 File Offset: 0x000BA1A2
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17005685 RID: 22149
			// (set) Token: 0x06007E7E RID: 32382 RVA: 0x000BBFBA File Offset: 0x000BA1BA
			public virtual RequestWorkloadType WorkloadType
			{
				set
				{
					base.PowerSharpParameters["WorkloadType"] = value;
				}
			}

			// Token: 0x17005686 RID: 22150
			// (set) Token: 0x06007E7F RID: 32383 RVA: 0x000BBFD2 File Offset: 0x000BA1D2
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x17005687 RID: 22151
			// (set) Token: 0x06007E80 RID: 32384 RVA: 0x000BBFEA File Offset: 0x000BA1EA
			public virtual SwitchParameter ForceOffline
			{
				set
				{
					base.PowerSharpParameters["ForceOffline"] = value;
				}
			}

			// Token: 0x17005688 RID: 22152
			// (set) Token: 0x06007E81 RID: 32385 RVA: 0x000BC002 File Offset: 0x000BA202
			public virtual SwitchParameter PreventCompletion
			{
				set
				{
					base.PowerSharpParameters["PreventCompletion"] = value;
				}
			}

			// Token: 0x17005689 RID: 22153
			// (set) Token: 0x06007E82 RID: 32386 RVA: 0x000BC01A File Offset: 0x000BA21A
			public virtual SkippableMoveComponent SkipMoving
			{
				set
				{
					base.PowerSharpParameters["SkipMoving"] = value;
				}
			}

			// Token: 0x1700568A RID: 22154
			// (set) Token: 0x06007E83 RID: 32387 RVA: 0x000BC032 File Offset: 0x000BA232
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x1700568B RID: 22155
			// (set) Token: 0x06007E84 RID: 32388 RVA: 0x000BC04A File Offset: 0x000BA24A
			public virtual DateTime StartAfter
			{
				set
				{
					base.PowerSharpParameters["StartAfter"] = value;
				}
			}

			// Token: 0x1700568C RID: 22156
			// (set) Token: 0x06007E85 RID: 32389 RVA: 0x000BC062 File Offset: 0x000BA262
			public virtual DateTime CompleteAfter
			{
				set
				{
					base.PowerSharpParameters["CompleteAfter"] = value;
				}
			}

			// Token: 0x1700568D RID: 22157
			// (set) Token: 0x06007E86 RID: 32390 RVA: 0x000BC07A File Offset: 0x000BA27A
			public virtual TimeSpan IncrementalSyncInterval
			{
				set
				{
					base.PowerSharpParameters["IncrementalSyncInterval"] = value;
				}
			}

			// Token: 0x1700568E RID: 22158
			// (set) Token: 0x06007E87 RID: 32391 RVA: 0x000BC092 File Offset: 0x000BA292
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700568F RID: 22159
			// (set) Token: 0x06007E88 RID: 32392 RVA: 0x000BC0AA File Offset: 0x000BA2AA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005690 RID: 22160
			// (set) Token: 0x06007E89 RID: 32393 RVA: 0x000BC0C2 File Offset: 0x000BA2C2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005691 RID: 22161
			// (set) Token: 0x06007E8A RID: 32394 RVA: 0x000BC0DA File Offset: 0x000BA2DA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005692 RID: 22162
			// (set) Token: 0x06007E8B RID: 32395 RVA: 0x000BC0F2 File Offset: 0x000BA2F2
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020009D9 RID: 2521
		public class MigrationOutboundParameters : ParametersBase
		{
			// Token: 0x17005693 RID: 22163
			// (set) Token: 0x06007E8D RID: 32397 RVA: 0x000BC112 File Offset: 0x000BA312
			public virtual SwitchParameter PrimaryOnly
			{
				set
				{
					base.PowerSharpParameters["PrimaryOnly"] = value;
				}
			}

			// Token: 0x17005694 RID: 22164
			// (set) Token: 0x06007E8E RID: 32398 RVA: 0x000BC12A File Offset: 0x000BA32A
			public virtual SwitchParameter ArchiveOnly
			{
				set
				{
					base.PowerSharpParameters["ArchiveOnly"] = value;
				}
			}

			// Token: 0x17005695 RID: 22165
			// (set) Token: 0x06007E8F RID: 32399 RVA: 0x000BC142 File Offset: 0x000BA342
			public virtual SwitchParameter IgnoreTenantMigrationPolicies
			{
				set
				{
					base.PowerSharpParameters["IgnoreTenantMigrationPolicies"] = value;
				}
			}

			// Token: 0x17005696 RID: 22166
			// (set) Token: 0x06007E90 RID: 32400 RVA: 0x000BC15A File Offset: 0x000BA35A
			public virtual Fqdn RemoteHostName
			{
				set
				{
					base.PowerSharpParameters["RemoteHostName"] = value;
				}
			}

			// Token: 0x17005697 RID: 22167
			// (set) Token: 0x06007E91 RID: 32401 RVA: 0x000BC16D File Offset: 0x000BA36D
			public virtual string RemoteTargetDatabase
			{
				set
				{
					base.PowerSharpParameters["RemoteTargetDatabase"] = value;
				}
			}

			// Token: 0x17005698 RID: 22168
			// (set) Token: 0x06007E92 RID: 32402 RVA: 0x000BC180 File Offset: 0x000BA380
			public virtual string RemoteArchiveTargetDatabase
			{
				set
				{
					base.PowerSharpParameters["RemoteArchiveTargetDatabase"] = value;
				}
			}

			// Token: 0x17005699 RID: 22169
			// (set) Token: 0x06007E93 RID: 32403 RVA: 0x000BC193 File Offset: 0x000BA393
			public virtual string RemoteOrganizationName
			{
				set
				{
					base.PowerSharpParameters["RemoteOrganizationName"] = value;
				}
			}

			// Token: 0x1700569A RID: 22170
			// (set) Token: 0x06007E94 RID: 32404 RVA: 0x000BC1A6 File Offset: 0x000BA3A6
			public virtual string ArchiveDomain
			{
				set
				{
					base.PowerSharpParameters["ArchiveDomain"] = value;
				}
			}

			// Token: 0x1700569B RID: 22171
			// (set) Token: 0x06007E95 RID: 32405 RVA: 0x000BC1B9 File Offset: 0x000BA3B9
			public virtual PSCredential RemoteCredential
			{
				set
				{
					base.PowerSharpParameters["RemoteCredential"] = value;
				}
			}

			// Token: 0x1700569C RID: 22172
			// (set) Token: 0x06007E96 RID: 32406 RVA: 0x000BC1CC File Offset: 0x000BA3CC
			public virtual SwitchParameter Outbound
			{
				set
				{
					base.PowerSharpParameters["Outbound"] = value;
				}
			}

			// Token: 0x1700569D RID: 22173
			// (set) Token: 0x06007E97 RID: 32407 RVA: 0x000BC1E4 File Offset: 0x000BA3E4
			public virtual Fqdn RemoteGlobalCatalog
			{
				set
				{
					base.PowerSharpParameters["RemoteGlobalCatalog"] = value;
				}
			}

			// Token: 0x1700569E RID: 22174
			// (set) Token: 0x06007E98 RID: 32408 RVA: 0x000BC1F7 File Offset: 0x000BA3F7
			public virtual Fqdn TargetDeliveryDomain
			{
				set
				{
					base.PowerSharpParameters["TargetDeliveryDomain"] = value;
				}
			}

			// Token: 0x1700569F RID: 22175
			// (set) Token: 0x06007E99 RID: 32409 RVA: 0x000BC20A File Offset: 0x000BA40A
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x170056A0 RID: 22176
			// (set) Token: 0x06007E9A RID: 32410 RVA: 0x000BC228 File Offset: 0x000BA428
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x170056A1 RID: 22177
			// (set) Token: 0x06007E9B RID: 32411 RVA: 0x000BC240 File Offset: 0x000BA440
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x170056A2 RID: 22178
			// (set) Token: 0x06007E9C RID: 32412 RVA: 0x000BC258 File Offset: 0x000BA458
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x170056A3 RID: 22179
			// (set) Token: 0x06007E9D RID: 32413 RVA: 0x000BC270 File Offset: 0x000BA470
			public virtual SwitchParameter AllowLargeItems
			{
				set
				{
					base.PowerSharpParameters["AllowLargeItems"] = value;
				}
			}

			// Token: 0x170056A4 RID: 22180
			// (set) Token: 0x06007E9E RID: 32414 RVA: 0x000BC288 File Offset: 0x000BA488
			public virtual SwitchParameter CheckInitialProvisioningSetting
			{
				set
				{
					base.PowerSharpParameters["CheckInitialProvisioningSetting"] = value;
				}
			}

			// Token: 0x170056A5 RID: 22181
			// (set) Token: 0x06007E9F RID: 32415 RVA: 0x000BC2A0 File Offset: 0x000BA4A0
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x170056A6 RID: 22182
			// (set) Token: 0x06007EA0 RID: 32416 RVA: 0x000BC2B3 File Offset: 0x000BA4B3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170056A7 RID: 22183
			// (set) Token: 0x06007EA1 RID: 32417 RVA: 0x000BC2C6 File Offset: 0x000BA4C6
			public virtual SwitchParameter Protect
			{
				set
				{
					base.PowerSharpParameters["Protect"] = value;
				}
			}

			// Token: 0x170056A8 RID: 22184
			// (set) Token: 0x06007EA2 RID: 32418 RVA: 0x000BC2DE File Offset: 0x000BA4DE
			public virtual SwitchParameter SuspendWhenReadyToComplete
			{
				set
				{
					base.PowerSharpParameters["SuspendWhenReadyToComplete"] = value;
				}
			}

			// Token: 0x170056A9 RID: 22185
			// (set) Token: 0x06007EA3 RID: 32419 RVA: 0x000BC2F6 File Offset: 0x000BA4F6
			public virtual SwitchParameter Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x170056AA RID: 22186
			// (set) Token: 0x06007EA4 RID: 32420 RVA: 0x000BC30E File Offset: 0x000BA50E
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x170056AB RID: 22187
			// (set) Token: 0x06007EA5 RID: 32421 RVA: 0x000BC321 File Offset: 0x000BA521
			public virtual SwitchParameter IgnoreRuleLimitErrors
			{
				set
				{
					base.PowerSharpParameters["IgnoreRuleLimitErrors"] = value;
				}
			}

			// Token: 0x170056AC RID: 22188
			// (set) Token: 0x06007EA6 RID: 32422 RVA: 0x000BC339 File Offset: 0x000BA539
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x170056AD RID: 22189
			// (set) Token: 0x06007EA7 RID: 32423 RVA: 0x000BC351 File Offset: 0x000BA551
			public virtual RequestWorkloadType WorkloadType
			{
				set
				{
					base.PowerSharpParameters["WorkloadType"] = value;
				}
			}

			// Token: 0x170056AE RID: 22190
			// (set) Token: 0x06007EA8 RID: 32424 RVA: 0x000BC369 File Offset: 0x000BA569
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x170056AF RID: 22191
			// (set) Token: 0x06007EA9 RID: 32425 RVA: 0x000BC381 File Offset: 0x000BA581
			public virtual SwitchParameter ForceOffline
			{
				set
				{
					base.PowerSharpParameters["ForceOffline"] = value;
				}
			}

			// Token: 0x170056B0 RID: 22192
			// (set) Token: 0x06007EAA RID: 32426 RVA: 0x000BC399 File Offset: 0x000BA599
			public virtual SwitchParameter PreventCompletion
			{
				set
				{
					base.PowerSharpParameters["PreventCompletion"] = value;
				}
			}

			// Token: 0x170056B1 RID: 22193
			// (set) Token: 0x06007EAB RID: 32427 RVA: 0x000BC3B1 File Offset: 0x000BA5B1
			public virtual SkippableMoveComponent SkipMoving
			{
				set
				{
					base.PowerSharpParameters["SkipMoving"] = value;
				}
			}

			// Token: 0x170056B2 RID: 22194
			// (set) Token: 0x06007EAC RID: 32428 RVA: 0x000BC3C9 File Offset: 0x000BA5C9
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x170056B3 RID: 22195
			// (set) Token: 0x06007EAD RID: 32429 RVA: 0x000BC3E1 File Offset: 0x000BA5E1
			public virtual DateTime StartAfter
			{
				set
				{
					base.PowerSharpParameters["StartAfter"] = value;
				}
			}

			// Token: 0x170056B4 RID: 22196
			// (set) Token: 0x06007EAE RID: 32430 RVA: 0x000BC3F9 File Offset: 0x000BA5F9
			public virtual DateTime CompleteAfter
			{
				set
				{
					base.PowerSharpParameters["CompleteAfter"] = value;
				}
			}

			// Token: 0x170056B5 RID: 22197
			// (set) Token: 0x06007EAF RID: 32431 RVA: 0x000BC411 File Offset: 0x000BA611
			public virtual TimeSpan IncrementalSyncInterval
			{
				set
				{
					base.PowerSharpParameters["IncrementalSyncInterval"] = value;
				}
			}

			// Token: 0x170056B6 RID: 22198
			// (set) Token: 0x06007EB0 RID: 32432 RVA: 0x000BC429 File Offset: 0x000BA629
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170056B7 RID: 22199
			// (set) Token: 0x06007EB1 RID: 32433 RVA: 0x000BC441 File Offset: 0x000BA641
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170056B8 RID: 22200
			// (set) Token: 0x06007EB2 RID: 32434 RVA: 0x000BC459 File Offset: 0x000BA659
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170056B9 RID: 22201
			// (set) Token: 0x06007EB3 RID: 32435 RVA: 0x000BC471 File Offset: 0x000BA671
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170056BA RID: 22202
			// (set) Token: 0x06007EB4 RID: 32436 RVA: 0x000BC489 File Offset: 0x000BA689
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
