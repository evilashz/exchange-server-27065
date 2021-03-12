using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A37 RID: 2615
	public class NewMailboxRelocationRequestCommand : SyntheticCommandWithPipelineInput<MailboxRelocationRequest, MailboxRelocationRequest>
	{
		// Token: 0x06008264 RID: 33380 RVA: 0x000C10A1 File Offset: 0x000BF2A1
		private NewMailboxRelocationRequestCommand() : base("New-MailboxRelocationRequest")
		{
		}

		// Token: 0x06008265 RID: 33381 RVA: 0x000C10AE File Offset: 0x000BF2AE
		public NewMailboxRelocationRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008266 RID: 33382 RVA: 0x000C10BD File Offset: 0x000BF2BD
		public virtual NewMailboxRelocationRequestCommand SetParameters(NewMailboxRelocationRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008267 RID: 33383 RVA: 0x000C10C7 File Offset: 0x000BF2C7
		public virtual NewMailboxRelocationRequestCommand SetParameters(NewMailboxRelocationRequestCommand.MailboxRelocationSplitParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008268 RID: 33384 RVA: 0x000C10D1 File Offset: 0x000BF2D1
		public virtual NewMailboxRelocationRequestCommand SetParameters(NewMailboxRelocationRequestCommand.MailboxRelocationJoinParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A38 RID: 2616
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170059AF RID: 22959
			// (set) Token: 0x06008269 RID: 33385 RVA: 0x000C10DB File Offset: 0x000BF2DB
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x170059B0 RID: 22960
			// (set) Token: 0x0600826A RID: 33386 RVA: 0x000C10F9 File Offset: 0x000BF2F9
			public virtual SkippableMoveComponent SkipMoving
			{
				set
				{
					base.PowerSharpParameters["SkipMoving"] = value;
				}
			}

			// Token: 0x170059B1 RID: 22961
			// (set) Token: 0x0600826B RID: 33387 RVA: 0x000C1111 File Offset: 0x000BF311
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x170059B2 RID: 22962
			// (set) Token: 0x0600826C RID: 33388 RVA: 0x000C1129 File Offset: 0x000BF329
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x170059B3 RID: 22963
			// (set) Token: 0x0600826D RID: 33389 RVA: 0x000C1141 File Offset: 0x000BF341
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x170059B4 RID: 22964
			// (set) Token: 0x0600826E RID: 33390 RVA: 0x000C1154 File Offset: 0x000BF354
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170059B5 RID: 22965
			// (set) Token: 0x0600826F RID: 33391 RVA: 0x000C1167 File Offset: 0x000BF367
			public virtual SwitchParameter Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x170059B6 RID: 22966
			// (set) Token: 0x06008270 RID: 33392 RVA: 0x000C117F File Offset: 0x000BF37F
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x170059B7 RID: 22967
			// (set) Token: 0x06008271 RID: 33393 RVA: 0x000C1192 File Offset: 0x000BF392
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170059B8 RID: 22968
			// (set) Token: 0x06008272 RID: 33394 RVA: 0x000C11A5 File Offset: 0x000BF3A5
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x170059B9 RID: 22969
			// (set) Token: 0x06008273 RID: 33395 RVA: 0x000C11BD File Offset: 0x000BF3BD
			public virtual RequestWorkloadType WorkloadType
			{
				set
				{
					base.PowerSharpParameters["WorkloadType"] = value;
				}
			}

			// Token: 0x170059BA RID: 22970
			// (set) Token: 0x06008274 RID: 33396 RVA: 0x000C11D5 File Offset: 0x000BF3D5
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x170059BB RID: 22971
			// (set) Token: 0x06008275 RID: 33397 RVA: 0x000C11ED File Offset: 0x000BF3ED
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x170059BC RID: 22972
			// (set) Token: 0x06008276 RID: 33398 RVA: 0x000C1205 File Offset: 0x000BF405
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170059BD RID: 22973
			// (set) Token: 0x06008277 RID: 33399 RVA: 0x000C121D File Offset: 0x000BF41D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170059BE RID: 22974
			// (set) Token: 0x06008278 RID: 33400 RVA: 0x000C1235 File Offset: 0x000BF435
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170059BF RID: 22975
			// (set) Token: 0x06008279 RID: 33401 RVA: 0x000C124D File Offset: 0x000BF44D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170059C0 RID: 22976
			// (set) Token: 0x0600827A RID: 33402 RVA: 0x000C1265 File Offset: 0x000BF465
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000A39 RID: 2617
		public class MailboxRelocationSplitParameters : ParametersBase
		{
			// Token: 0x170059C1 RID: 22977
			// (set) Token: 0x0600827C RID: 33404 RVA: 0x000C1285 File Offset: 0x000BF485
			public virtual DatabaseIdParameter TargetDatabase
			{
				set
				{
					base.PowerSharpParameters["TargetDatabase"] = value;
				}
			}

			// Token: 0x170059C2 RID: 22978
			// (set) Token: 0x0600827D RID: 33405 RVA: 0x000C1298 File Offset: 0x000BF498
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x170059C3 RID: 22979
			// (set) Token: 0x0600827E RID: 33406 RVA: 0x000C12B6 File Offset: 0x000BF4B6
			public virtual SkippableMoveComponent SkipMoving
			{
				set
				{
					base.PowerSharpParameters["SkipMoving"] = value;
				}
			}

			// Token: 0x170059C4 RID: 22980
			// (set) Token: 0x0600827F RID: 33407 RVA: 0x000C12CE File Offset: 0x000BF4CE
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x170059C5 RID: 22981
			// (set) Token: 0x06008280 RID: 33408 RVA: 0x000C12E6 File Offset: 0x000BF4E6
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x170059C6 RID: 22982
			// (set) Token: 0x06008281 RID: 33409 RVA: 0x000C12FE File Offset: 0x000BF4FE
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x170059C7 RID: 22983
			// (set) Token: 0x06008282 RID: 33410 RVA: 0x000C1311 File Offset: 0x000BF511
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170059C8 RID: 22984
			// (set) Token: 0x06008283 RID: 33411 RVA: 0x000C1324 File Offset: 0x000BF524
			public virtual SwitchParameter Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x170059C9 RID: 22985
			// (set) Token: 0x06008284 RID: 33412 RVA: 0x000C133C File Offset: 0x000BF53C
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x170059CA RID: 22986
			// (set) Token: 0x06008285 RID: 33413 RVA: 0x000C134F File Offset: 0x000BF54F
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170059CB RID: 22987
			// (set) Token: 0x06008286 RID: 33414 RVA: 0x000C1362 File Offset: 0x000BF562
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x170059CC RID: 22988
			// (set) Token: 0x06008287 RID: 33415 RVA: 0x000C137A File Offset: 0x000BF57A
			public virtual RequestWorkloadType WorkloadType
			{
				set
				{
					base.PowerSharpParameters["WorkloadType"] = value;
				}
			}

			// Token: 0x170059CD RID: 22989
			// (set) Token: 0x06008288 RID: 33416 RVA: 0x000C1392 File Offset: 0x000BF592
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x170059CE RID: 22990
			// (set) Token: 0x06008289 RID: 33417 RVA: 0x000C13AA File Offset: 0x000BF5AA
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x170059CF RID: 22991
			// (set) Token: 0x0600828A RID: 33418 RVA: 0x000C13C2 File Offset: 0x000BF5C2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170059D0 RID: 22992
			// (set) Token: 0x0600828B RID: 33419 RVA: 0x000C13DA File Offset: 0x000BF5DA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170059D1 RID: 22993
			// (set) Token: 0x0600828C RID: 33420 RVA: 0x000C13F2 File Offset: 0x000BF5F2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170059D2 RID: 22994
			// (set) Token: 0x0600828D RID: 33421 RVA: 0x000C140A File Offset: 0x000BF60A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170059D3 RID: 22995
			// (set) Token: 0x0600828E RID: 33422 RVA: 0x000C1422 File Offset: 0x000BF622
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000A3A RID: 2618
		public class MailboxRelocationJoinParameters : ParametersBase
		{
			// Token: 0x170059D4 RID: 22996
			// (set) Token: 0x06008290 RID: 33424 RVA: 0x000C1442 File Offset: 0x000BF642
			public virtual string TargetContainer
			{
				set
				{
					base.PowerSharpParameters["TargetContainer"] = ((value != null) ? new MailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x170059D5 RID: 22997
			// (set) Token: 0x06008291 RID: 33425 RVA: 0x000C1460 File Offset: 0x000BF660
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x170059D6 RID: 22998
			// (set) Token: 0x06008292 RID: 33426 RVA: 0x000C147E File Offset: 0x000BF67E
			public virtual SkippableMoveComponent SkipMoving
			{
				set
				{
					base.PowerSharpParameters["SkipMoving"] = value;
				}
			}

			// Token: 0x170059D7 RID: 22999
			// (set) Token: 0x06008293 RID: 33427 RVA: 0x000C1496 File Offset: 0x000BF696
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x170059D8 RID: 23000
			// (set) Token: 0x06008294 RID: 33428 RVA: 0x000C14AE File Offset: 0x000BF6AE
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x170059D9 RID: 23001
			// (set) Token: 0x06008295 RID: 33429 RVA: 0x000C14C6 File Offset: 0x000BF6C6
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x170059DA RID: 23002
			// (set) Token: 0x06008296 RID: 33430 RVA: 0x000C14D9 File Offset: 0x000BF6D9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170059DB RID: 23003
			// (set) Token: 0x06008297 RID: 33431 RVA: 0x000C14EC File Offset: 0x000BF6EC
			public virtual SwitchParameter Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x170059DC RID: 23004
			// (set) Token: 0x06008298 RID: 33432 RVA: 0x000C1504 File Offset: 0x000BF704
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x170059DD RID: 23005
			// (set) Token: 0x06008299 RID: 33433 RVA: 0x000C1517 File Offset: 0x000BF717
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170059DE RID: 23006
			// (set) Token: 0x0600829A RID: 33434 RVA: 0x000C152A File Offset: 0x000BF72A
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x170059DF RID: 23007
			// (set) Token: 0x0600829B RID: 33435 RVA: 0x000C1542 File Offset: 0x000BF742
			public virtual RequestWorkloadType WorkloadType
			{
				set
				{
					base.PowerSharpParameters["WorkloadType"] = value;
				}
			}

			// Token: 0x170059E0 RID: 23008
			// (set) Token: 0x0600829C RID: 33436 RVA: 0x000C155A File Offset: 0x000BF75A
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x170059E1 RID: 23009
			// (set) Token: 0x0600829D RID: 33437 RVA: 0x000C1572 File Offset: 0x000BF772
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x170059E2 RID: 23010
			// (set) Token: 0x0600829E RID: 33438 RVA: 0x000C158A File Offset: 0x000BF78A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170059E3 RID: 23011
			// (set) Token: 0x0600829F RID: 33439 RVA: 0x000C15A2 File Offset: 0x000BF7A2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170059E4 RID: 23012
			// (set) Token: 0x060082A0 RID: 33440 RVA: 0x000C15BA File Offset: 0x000BF7BA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170059E5 RID: 23013
			// (set) Token: 0x060082A1 RID: 33441 RVA: 0x000C15D2 File Offset: 0x000BF7D2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170059E6 RID: 23014
			// (set) Token: 0x060082A2 RID: 33442 RVA: 0x000C15EA File Offset: 0x000BF7EA
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
