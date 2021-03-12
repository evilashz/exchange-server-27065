using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000876 RID: 2166
	public class NewThrottlingPolicyCommand : SyntheticCommandWithPipelineInput<ThrottlingPolicy, ThrottlingPolicy>
	{
		// Token: 0x06006B31 RID: 27441 RVA: 0x000A27CF File Offset: 0x000A09CF
		private NewThrottlingPolicyCommand() : base("New-ThrottlingPolicy")
		{
		}

		// Token: 0x06006B32 RID: 27442 RVA: 0x000A27DC File Offset: 0x000A09DC
		public NewThrottlingPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006B33 RID: 27443 RVA: 0x000A27EB File Offset: 0x000A09EB
		public virtual NewThrottlingPolicyCommand SetParameters(NewThrottlingPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000877 RID: 2167
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170045FE RID: 17918
			// (set) Token: 0x06006B34 RID: 27444 RVA: 0x000A27F5 File Offset: 0x000A09F5
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x170045FF RID: 17919
			// (set) Token: 0x06006B35 RID: 27445 RVA: 0x000A280D File Offset: 0x000A0A0D
			public virtual Unlimited<uint>? AnonymousMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["AnonymousMaxConcurrency"] = value;
				}
			}

			// Token: 0x17004600 RID: 17920
			// (set) Token: 0x06006B36 RID: 27446 RVA: 0x000A2825 File Offset: 0x000A0A25
			public virtual Unlimited<uint>? AnonymousMaxBurst
			{
				set
				{
					base.PowerSharpParameters["AnonymousMaxBurst"] = value;
				}
			}

			// Token: 0x17004601 RID: 17921
			// (set) Token: 0x06006B37 RID: 27447 RVA: 0x000A283D File Offset: 0x000A0A3D
			public virtual Unlimited<uint>? AnonymousRechargeRate
			{
				set
				{
					base.PowerSharpParameters["AnonymousRechargeRate"] = value;
				}
			}

			// Token: 0x17004602 RID: 17922
			// (set) Token: 0x06006B38 RID: 27448 RVA: 0x000A2855 File Offset: 0x000A0A55
			public virtual Unlimited<uint>? AnonymousCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["AnonymousCutoffBalance"] = value;
				}
			}

			// Token: 0x17004603 RID: 17923
			// (set) Token: 0x06006B39 RID: 27449 RVA: 0x000A286D File Offset: 0x000A0A6D
			public virtual Unlimited<uint>? EasMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["EasMaxConcurrency"] = value;
				}
			}

			// Token: 0x17004604 RID: 17924
			// (set) Token: 0x06006B3A RID: 27450 RVA: 0x000A2885 File Offset: 0x000A0A85
			public virtual Unlimited<uint>? EasMaxBurst
			{
				set
				{
					base.PowerSharpParameters["EasMaxBurst"] = value;
				}
			}

			// Token: 0x17004605 RID: 17925
			// (set) Token: 0x06006B3B RID: 27451 RVA: 0x000A289D File Offset: 0x000A0A9D
			public virtual Unlimited<uint>? EasRechargeRate
			{
				set
				{
					base.PowerSharpParameters["EasRechargeRate"] = value;
				}
			}

			// Token: 0x17004606 RID: 17926
			// (set) Token: 0x06006B3C RID: 27452 RVA: 0x000A28B5 File Offset: 0x000A0AB5
			public virtual Unlimited<uint>? EasCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["EasCutoffBalance"] = value;
				}
			}

			// Token: 0x17004607 RID: 17927
			// (set) Token: 0x06006B3D RID: 27453 RVA: 0x000A28CD File Offset: 0x000A0ACD
			public virtual Unlimited<uint>? EasMaxDevices
			{
				set
				{
					base.PowerSharpParameters["EasMaxDevices"] = value;
				}
			}

			// Token: 0x17004608 RID: 17928
			// (set) Token: 0x06006B3E RID: 27454 RVA: 0x000A28E5 File Offset: 0x000A0AE5
			public virtual Unlimited<uint>? EasMaxDeviceDeletesPerMonth
			{
				set
				{
					base.PowerSharpParameters["EasMaxDeviceDeletesPerMonth"] = value;
				}
			}

			// Token: 0x17004609 RID: 17929
			// (set) Token: 0x06006B3F RID: 27455 RVA: 0x000A28FD File Offset: 0x000A0AFD
			public virtual Unlimited<uint>? EasMaxInactivityForDeviceCleanup
			{
				set
				{
					base.PowerSharpParameters["EasMaxInactivityForDeviceCleanup"] = value;
				}
			}

			// Token: 0x1700460A RID: 17930
			// (set) Token: 0x06006B40 RID: 27456 RVA: 0x000A2915 File Offset: 0x000A0B15
			public virtual Unlimited<uint>? EwsMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["EwsMaxConcurrency"] = value;
				}
			}

			// Token: 0x1700460B RID: 17931
			// (set) Token: 0x06006B41 RID: 27457 RVA: 0x000A292D File Offset: 0x000A0B2D
			public virtual Unlimited<uint>? EwsMaxBurst
			{
				set
				{
					base.PowerSharpParameters["EwsMaxBurst"] = value;
				}
			}

			// Token: 0x1700460C RID: 17932
			// (set) Token: 0x06006B42 RID: 27458 RVA: 0x000A2945 File Offset: 0x000A0B45
			public virtual Unlimited<uint>? EwsRechargeRate
			{
				set
				{
					base.PowerSharpParameters["EwsRechargeRate"] = value;
				}
			}

			// Token: 0x1700460D RID: 17933
			// (set) Token: 0x06006B43 RID: 27459 RVA: 0x000A295D File Offset: 0x000A0B5D
			public virtual Unlimited<uint>? EwsCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["EwsCutoffBalance"] = value;
				}
			}

			// Token: 0x1700460E RID: 17934
			// (set) Token: 0x06006B44 RID: 27460 RVA: 0x000A2975 File Offset: 0x000A0B75
			public virtual Unlimited<uint>? EwsMaxSubscriptions
			{
				set
				{
					base.PowerSharpParameters["EwsMaxSubscriptions"] = value;
				}
			}

			// Token: 0x1700460F RID: 17935
			// (set) Token: 0x06006B45 RID: 27461 RVA: 0x000A298D File Offset: 0x000A0B8D
			public virtual Unlimited<uint>? ImapMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["ImapMaxConcurrency"] = value;
				}
			}

			// Token: 0x17004610 RID: 17936
			// (set) Token: 0x06006B46 RID: 27462 RVA: 0x000A29A5 File Offset: 0x000A0BA5
			public virtual Unlimited<uint>? ImapMaxBurst
			{
				set
				{
					base.PowerSharpParameters["ImapMaxBurst"] = value;
				}
			}

			// Token: 0x17004611 RID: 17937
			// (set) Token: 0x06006B47 RID: 27463 RVA: 0x000A29BD File Offset: 0x000A0BBD
			public virtual Unlimited<uint>? ImapRechargeRate
			{
				set
				{
					base.PowerSharpParameters["ImapRechargeRate"] = value;
				}
			}

			// Token: 0x17004612 RID: 17938
			// (set) Token: 0x06006B48 RID: 27464 RVA: 0x000A29D5 File Offset: 0x000A0BD5
			public virtual Unlimited<uint>? ImapCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["ImapCutoffBalance"] = value;
				}
			}

			// Token: 0x17004613 RID: 17939
			// (set) Token: 0x06006B49 RID: 27465 RVA: 0x000A29ED File Offset: 0x000A0BED
			public virtual Unlimited<uint>? OutlookServiceMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["OutlookServiceMaxConcurrency"] = value;
				}
			}

			// Token: 0x17004614 RID: 17940
			// (set) Token: 0x06006B4A RID: 27466 RVA: 0x000A2A05 File Offset: 0x000A0C05
			public virtual Unlimited<uint>? OutlookServiceMaxBurst
			{
				set
				{
					base.PowerSharpParameters["OutlookServiceMaxBurst"] = value;
				}
			}

			// Token: 0x17004615 RID: 17941
			// (set) Token: 0x06006B4B RID: 27467 RVA: 0x000A2A1D File Offset: 0x000A0C1D
			public virtual Unlimited<uint>? OutlookServiceRechargeRate
			{
				set
				{
					base.PowerSharpParameters["OutlookServiceRechargeRate"] = value;
				}
			}

			// Token: 0x17004616 RID: 17942
			// (set) Token: 0x06006B4C RID: 27468 RVA: 0x000A2A35 File Offset: 0x000A0C35
			public virtual Unlimited<uint>? OutlookServiceCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["OutlookServiceCutoffBalance"] = value;
				}
			}

			// Token: 0x17004617 RID: 17943
			// (set) Token: 0x06006B4D RID: 27469 RVA: 0x000A2A4D File Offset: 0x000A0C4D
			public virtual Unlimited<uint>? OutlookServiceMaxSubscriptions
			{
				set
				{
					base.PowerSharpParameters["OutlookServiceMaxSubscriptions"] = value;
				}
			}

			// Token: 0x17004618 RID: 17944
			// (set) Token: 0x06006B4E RID: 27470 RVA: 0x000A2A65 File Offset: 0x000A0C65
			public virtual Unlimited<uint>? OutlookServiceMaxSocketConnectionsPerDevice
			{
				set
				{
					base.PowerSharpParameters["OutlookServiceMaxSocketConnectionsPerDevice"] = value;
				}
			}

			// Token: 0x17004619 RID: 17945
			// (set) Token: 0x06006B4F RID: 27471 RVA: 0x000A2A7D File Offset: 0x000A0C7D
			public virtual Unlimited<uint>? OutlookServiceMaxSocketConnectionsPerUser
			{
				set
				{
					base.PowerSharpParameters["OutlookServiceMaxSocketConnectionsPerUser"] = value;
				}
			}

			// Token: 0x1700461A RID: 17946
			// (set) Token: 0x06006B50 RID: 27472 RVA: 0x000A2A95 File Offset: 0x000A0C95
			public virtual Unlimited<uint>? OwaMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["OwaMaxConcurrency"] = value;
				}
			}

			// Token: 0x1700461B RID: 17947
			// (set) Token: 0x06006B51 RID: 27473 RVA: 0x000A2AAD File Offset: 0x000A0CAD
			public virtual Unlimited<uint>? OwaMaxBurst
			{
				set
				{
					base.PowerSharpParameters["OwaMaxBurst"] = value;
				}
			}

			// Token: 0x1700461C RID: 17948
			// (set) Token: 0x06006B52 RID: 27474 RVA: 0x000A2AC5 File Offset: 0x000A0CC5
			public virtual Unlimited<uint>? OwaRechargeRate
			{
				set
				{
					base.PowerSharpParameters["OwaRechargeRate"] = value;
				}
			}

			// Token: 0x1700461D RID: 17949
			// (set) Token: 0x06006B53 RID: 27475 RVA: 0x000A2ADD File Offset: 0x000A0CDD
			public virtual Unlimited<uint>? OwaCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["OwaCutoffBalance"] = value;
				}
			}

			// Token: 0x1700461E RID: 17950
			// (set) Token: 0x06006B54 RID: 27476 RVA: 0x000A2AF5 File Offset: 0x000A0CF5
			public virtual Unlimited<uint>? OwaVoiceMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["OwaVoiceMaxConcurrency"] = value;
				}
			}

			// Token: 0x1700461F RID: 17951
			// (set) Token: 0x06006B55 RID: 27477 RVA: 0x000A2B0D File Offset: 0x000A0D0D
			public virtual Unlimited<uint>? OwaVoiceMaxBurst
			{
				set
				{
					base.PowerSharpParameters["OwaVoiceMaxBurst"] = value;
				}
			}

			// Token: 0x17004620 RID: 17952
			// (set) Token: 0x06006B56 RID: 27478 RVA: 0x000A2B25 File Offset: 0x000A0D25
			public virtual Unlimited<uint>? OwaVoiceRechargeRate
			{
				set
				{
					base.PowerSharpParameters["OwaVoiceRechargeRate"] = value;
				}
			}

			// Token: 0x17004621 RID: 17953
			// (set) Token: 0x06006B57 RID: 27479 RVA: 0x000A2B3D File Offset: 0x000A0D3D
			public virtual Unlimited<uint>? OwaVoiceCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["OwaVoiceCutoffBalance"] = value;
				}
			}

			// Token: 0x17004622 RID: 17954
			// (set) Token: 0x06006B58 RID: 27480 RVA: 0x000A2B55 File Offset: 0x000A0D55
			public virtual Unlimited<uint>? EncryptionSenderMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["EncryptionSenderMaxConcurrency"] = value;
				}
			}

			// Token: 0x17004623 RID: 17955
			// (set) Token: 0x06006B59 RID: 27481 RVA: 0x000A2B6D File Offset: 0x000A0D6D
			public virtual Unlimited<uint>? EncryptionSenderMaxBurst
			{
				set
				{
					base.PowerSharpParameters["EncryptionSenderMaxBurst"] = value;
				}
			}

			// Token: 0x17004624 RID: 17956
			// (set) Token: 0x06006B5A RID: 27482 RVA: 0x000A2B85 File Offset: 0x000A0D85
			public virtual Unlimited<uint>? EncryptionSenderRechargeRate
			{
				set
				{
					base.PowerSharpParameters["EncryptionSenderRechargeRate"] = value;
				}
			}

			// Token: 0x17004625 RID: 17957
			// (set) Token: 0x06006B5B RID: 27483 RVA: 0x000A2B9D File Offset: 0x000A0D9D
			public virtual Unlimited<uint>? EncryptionSenderCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["EncryptionSenderCutoffBalance"] = value;
				}
			}

			// Token: 0x17004626 RID: 17958
			// (set) Token: 0x06006B5C RID: 27484 RVA: 0x000A2BB5 File Offset: 0x000A0DB5
			public virtual Unlimited<uint>? EncryptionRecipientMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["EncryptionRecipientMaxConcurrency"] = value;
				}
			}

			// Token: 0x17004627 RID: 17959
			// (set) Token: 0x06006B5D RID: 27485 RVA: 0x000A2BCD File Offset: 0x000A0DCD
			public virtual Unlimited<uint>? EncryptionRecipientMaxBurst
			{
				set
				{
					base.PowerSharpParameters["EncryptionRecipientMaxBurst"] = value;
				}
			}

			// Token: 0x17004628 RID: 17960
			// (set) Token: 0x06006B5E RID: 27486 RVA: 0x000A2BE5 File Offset: 0x000A0DE5
			public virtual Unlimited<uint>? EncryptionRecipientRechargeRate
			{
				set
				{
					base.PowerSharpParameters["EncryptionRecipientRechargeRate"] = value;
				}
			}

			// Token: 0x17004629 RID: 17961
			// (set) Token: 0x06006B5F RID: 27487 RVA: 0x000A2BFD File Offset: 0x000A0DFD
			public virtual Unlimited<uint>? EncryptionRecipientCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["EncryptionRecipientCutoffBalance"] = value;
				}
			}

			// Token: 0x1700462A RID: 17962
			// (set) Token: 0x06006B60 RID: 27488 RVA: 0x000A2C15 File Offset: 0x000A0E15
			public virtual Unlimited<uint>? PopMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["PopMaxConcurrency"] = value;
				}
			}

			// Token: 0x1700462B RID: 17963
			// (set) Token: 0x06006B61 RID: 27489 RVA: 0x000A2C2D File Offset: 0x000A0E2D
			public virtual Unlimited<uint>? PopMaxBurst
			{
				set
				{
					base.PowerSharpParameters["PopMaxBurst"] = value;
				}
			}

			// Token: 0x1700462C RID: 17964
			// (set) Token: 0x06006B62 RID: 27490 RVA: 0x000A2C45 File Offset: 0x000A0E45
			public virtual Unlimited<uint>? PopRechargeRate
			{
				set
				{
					base.PowerSharpParameters["PopRechargeRate"] = value;
				}
			}

			// Token: 0x1700462D RID: 17965
			// (set) Token: 0x06006B63 RID: 27491 RVA: 0x000A2C5D File Offset: 0x000A0E5D
			public virtual Unlimited<uint>? PopCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["PopCutoffBalance"] = value;
				}
			}

			// Token: 0x1700462E RID: 17966
			// (set) Token: 0x06006B64 RID: 27492 RVA: 0x000A2C75 File Offset: 0x000A0E75
			public virtual Unlimited<uint>? PowerShellMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["PowerShellMaxConcurrency"] = value;
				}
			}

			// Token: 0x1700462F RID: 17967
			// (set) Token: 0x06006B65 RID: 27493 RVA: 0x000A2C8D File Offset: 0x000A0E8D
			public virtual Unlimited<uint>? PowerShellMaxBurst
			{
				set
				{
					base.PowerSharpParameters["PowerShellMaxBurst"] = value;
				}
			}

			// Token: 0x17004630 RID: 17968
			// (set) Token: 0x06006B66 RID: 27494 RVA: 0x000A2CA5 File Offset: 0x000A0EA5
			public virtual Unlimited<uint>? PowerShellRechargeRate
			{
				set
				{
					base.PowerSharpParameters["PowerShellRechargeRate"] = value;
				}
			}

			// Token: 0x17004631 RID: 17969
			// (set) Token: 0x06006B67 RID: 27495 RVA: 0x000A2CBD File Offset: 0x000A0EBD
			public virtual Unlimited<uint>? PowerShellCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["PowerShellCutoffBalance"] = value;
				}
			}

			// Token: 0x17004632 RID: 17970
			// (set) Token: 0x06006B68 RID: 27496 RVA: 0x000A2CD5 File Offset: 0x000A0ED5
			public virtual Unlimited<uint>? PowerShellMaxTenantConcurrency
			{
				set
				{
					base.PowerSharpParameters["PowerShellMaxTenantConcurrency"] = value;
				}
			}

			// Token: 0x17004633 RID: 17971
			// (set) Token: 0x06006B69 RID: 27497 RVA: 0x000A2CED File Offset: 0x000A0EED
			public virtual Unlimited<uint>? PowerShellMaxOperations
			{
				set
				{
					base.PowerSharpParameters["PowerShellMaxOperations"] = value;
				}
			}

			// Token: 0x17004634 RID: 17972
			// (set) Token: 0x06006B6A RID: 27498 RVA: 0x000A2D05 File Offset: 0x000A0F05
			public virtual Unlimited<uint>? ExchangeMaxCmdlets
			{
				set
				{
					base.PowerSharpParameters["ExchangeMaxCmdlets"] = value;
				}
			}

			// Token: 0x17004635 RID: 17973
			// (set) Token: 0x06006B6B RID: 27499 RVA: 0x000A2D1D File Offset: 0x000A0F1D
			public virtual Unlimited<uint>? PowerShellMaxCmdletsTimePeriod
			{
				set
				{
					base.PowerSharpParameters["PowerShellMaxCmdletsTimePeriod"] = value;
				}
			}

			// Token: 0x17004636 RID: 17974
			// (set) Token: 0x06006B6C RID: 27500 RVA: 0x000A2D35 File Offset: 0x000A0F35
			public virtual Unlimited<uint>? PowerShellMaxCmdletQueueDepth
			{
				set
				{
					base.PowerSharpParameters["PowerShellMaxCmdletQueueDepth"] = value;
				}
			}

			// Token: 0x17004637 RID: 17975
			// (set) Token: 0x06006B6D RID: 27501 RVA: 0x000A2D4D File Offset: 0x000A0F4D
			public virtual Unlimited<uint>? PowerShellMaxDestructiveCmdlets
			{
				set
				{
					base.PowerSharpParameters["PowerShellMaxDestructiveCmdlets"] = value;
				}
			}

			// Token: 0x17004638 RID: 17976
			// (set) Token: 0x06006B6E RID: 27502 RVA: 0x000A2D65 File Offset: 0x000A0F65
			public virtual Unlimited<uint>? PowerShellMaxDestructiveCmdletsTimePeriod
			{
				set
				{
					base.PowerSharpParameters["PowerShellMaxDestructiveCmdletsTimePeriod"] = value;
				}
			}

			// Token: 0x17004639 RID: 17977
			// (set) Token: 0x06006B6F RID: 27503 RVA: 0x000A2D7D File Offset: 0x000A0F7D
			public virtual Unlimited<uint>? PowerShellMaxCmdlets
			{
				set
				{
					base.PowerSharpParameters["PowerShellMaxCmdlets"] = value;
				}
			}

			// Token: 0x1700463A RID: 17978
			// (set) Token: 0x06006B70 RID: 27504 RVA: 0x000A2D95 File Offset: 0x000A0F95
			public virtual Unlimited<uint>? PowerShellMaxRunspaces
			{
				set
				{
					base.PowerSharpParameters["PowerShellMaxRunspaces"] = value;
				}
			}

			// Token: 0x1700463B RID: 17979
			// (set) Token: 0x06006B71 RID: 27505 RVA: 0x000A2DAD File Offset: 0x000A0FAD
			public virtual Unlimited<uint>? PowerShellMaxTenantRunspaces
			{
				set
				{
					base.PowerSharpParameters["PowerShellMaxTenantRunspaces"] = value;
				}
			}

			// Token: 0x1700463C RID: 17980
			// (set) Token: 0x06006B72 RID: 27506 RVA: 0x000A2DC5 File Offset: 0x000A0FC5
			public virtual Unlimited<uint>? PowerShellMaxRunspacesTimePeriod
			{
				set
				{
					base.PowerSharpParameters["PowerShellMaxRunspacesTimePeriod"] = value;
				}
			}

			// Token: 0x1700463D RID: 17981
			// (set) Token: 0x06006B73 RID: 27507 RVA: 0x000A2DDD File Offset: 0x000A0FDD
			public virtual Unlimited<uint>? PswsMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["PswsMaxConcurrency"] = value;
				}
			}

			// Token: 0x1700463E RID: 17982
			// (set) Token: 0x06006B74 RID: 27508 RVA: 0x000A2DF5 File Offset: 0x000A0FF5
			public virtual Unlimited<uint>? PswsMaxRequest
			{
				set
				{
					base.PowerSharpParameters["PswsMaxRequest"] = value;
				}
			}

			// Token: 0x1700463F RID: 17983
			// (set) Token: 0x06006B75 RID: 27509 RVA: 0x000A2E0D File Offset: 0x000A100D
			public virtual Unlimited<uint>? PswsMaxRequestTimePeriod
			{
				set
				{
					base.PowerSharpParameters["PswsMaxRequestTimePeriod"] = value;
				}
			}

			// Token: 0x17004640 RID: 17984
			// (set) Token: 0x06006B76 RID: 27510 RVA: 0x000A2E25 File Offset: 0x000A1025
			public virtual Unlimited<uint>? RcaMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["RcaMaxConcurrency"] = value;
				}
			}

			// Token: 0x17004641 RID: 17985
			// (set) Token: 0x06006B77 RID: 27511 RVA: 0x000A2E3D File Offset: 0x000A103D
			public virtual Unlimited<uint>? RcaMaxBurst
			{
				set
				{
					base.PowerSharpParameters["RcaMaxBurst"] = value;
				}
			}

			// Token: 0x17004642 RID: 17986
			// (set) Token: 0x06006B78 RID: 27512 RVA: 0x000A2E55 File Offset: 0x000A1055
			public virtual Unlimited<uint>? RcaRechargeRate
			{
				set
				{
					base.PowerSharpParameters["RcaRechargeRate"] = value;
				}
			}

			// Token: 0x17004643 RID: 17987
			// (set) Token: 0x06006B79 RID: 27513 RVA: 0x000A2E6D File Offset: 0x000A106D
			public virtual Unlimited<uint>? RcaCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["RcaCutoffBalance"] = value;
				}
			}

			// Token: 0x17004644 RID: 17988
			// (set) Token: 0x06006B7A RID: 27514 RVA: 0x000A2E85 File Offset: 0x000A1085
			public virtual Unlimited<uint>? CpaMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["CpaMaxConcurrency"] = value;
				}
			}

			// Token: 0x17004645 RID: 17989
			// (set) Token: 0x06006B7B RID: 27515 RVA: 0x000A2E9D File Offset: 0x000A109D
			public virtual Unlimited<uint>? CpaMaxBurst
			{
				set
				{
					base.PowerSharpParameters["CpaMaxBurst"] = value;
				}
			}

			// Token: 0x17004646 RID: 17990
			// (set) Token: 0x06006B7C RID: 27516 RVA: 0x000A2EB5 File Offset: 0x000A10B5
			public virtual Unlimited<uint>? CpaRechargeRate
			{
				set
				{
					base.PowerSharpParameters["CpaRechargeRate"] = value;
				}
			}

			// Token: 0x17004647 RID: 17991
			// (set) Token: 0x06006B7D RID: 27517 RVA: 0x000A2ECD File Offset: 0x000A10CD
			public virtual Unlimited<uint>? CpaCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["CpaCutoffBalance"] = value;
				}
			}

			// Token: 0x17004648 RID: 17992
			// (set) Token: 0x06006B7E RID: 27518 RVA: 0x000A2EE5 File Offset: 0x000A10E5
			public virtual Unlimited<uint>? MessageRateLimit
			{
				set
				{
					base.PowerSharpParameters["MessageRateLimit"] = value;
				}
			}

			// Token: 0x17004649 RID: 17993
			// (set) Token: 0x06006B7F RID: 27519 RVA: 0x000A2EFD File Offset: 0x000A10FD
			public virtual Unlimited<uint>? RecipientRateLimit
			{
				set
				{
					base.PowerSharpParameters["RecipientRateLimit"] = value;
				}
			}

			// Token: 0x1700464A RID: 17994
			// (set) Token: 0x06006B80 RID: 27520 RVA: 0x000A2F15 File Offset: 0x000A1115
			public virtual Unlimited<uint>? ForwardeeLimit
			{
				set
				{
					base.PowerSharpParameters["ForwardeeLimit"] = value;
				}
			}

			// Token: 0x1700464B RID: 17995
			// (set) Token: 0x06006B81 RID: 27521 RVA: 0x000A2F2D File Offset: 0x000A112D
			public virtual Unlimited<uint>? DiscoveryMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["DiscoveryMaxConcurrency"] = value;
				}
			}

			// Token: 0x1700464C RID: 17996
			// (set) Token: 0x06006B82 RID: 27522 RVA: 0x000A2F45 File Offset: 0x000A1145
			public virtual Unlimited<uint>? DiscoveryMaxMailboxes
			{
				set
				{
					base.PowerSharpParameters["DiscoveryMaxMailboxes"] = value;
				}
			}

			// Token: 0x1700464D RID: 17997
			// (set) Token: 0x06006B83 RID: 27523 RVA: 0x000A2F5D File Offset: 0x000A115D
			public virtual Unlimited<uint>? DiscoveryMaxKeywords
			{
				set
				{
					base.PowerSharpParameters["DiscoveryMaxKeywords"] = value;
				}
			}

			// Token: 0x1700464E RID: 17998
			// (set) Token: 0x06006B84 RID: 27524 RVA: 0x000A2F75 File Offset: 0x000A1175
			public virtual Unlimited<uint>? DiscoveryMaxPreviewSearchMailboxes
			{
				set
				{
					base.PowerSharpParameters["DiscoveryMaxPreviewSearchMailboxes"] = value;
				}
			}

			// Token: 0x1700464F RID: 17999
			// (set) Token: 0x06006B85 RID: 27525 RVA: 0x000A2F8D File Offset: 0x000A118D
			public virtual Unlimited<uint>? DiscoveryMaxStatsSearchMailboxes
			{
				set
				{
					base.PowerSharpParameters["DiscoveryMaxStatsSearchMailboxes"] = value;
				}
			}

			// Token: 0x17004650 RID: 18000
			// (set) Token: 0x06006B86 RID: 27526 RVA: 0x000A2FA5 File Offset: 0x000A11A5
			public virtual Unlimited<uint>? DiscoveryPreviewSearchResultsPageSize
			{
				set
				{
					base.PowerSharpParameters["DiscoveryPreviewSearchResultsPageSize"] = value;
				}
			}

			// Token: 0x17004651 RID: 18001
			// (set) Token: 0x06006B87 RID: 27527 RVA: 0x000A2FBD File Offset: 0x000A11BD
			public virtual Unlimited<uint>? DiscoveryMaxKeywordsPerPage
			{
				set
				{
					base.PowerSharpParameters["DiscoveryMaxKeywordsPerPage"] = value;
				}
			}

			// Token: 0x17004652 RID: 18002
			// (set) Token: 0x06006B88 RID: 27528 RVA: 0x000A2FD5 File Offset: 0x000A11D5
			public virtual Unlimited<uint>? DiscoveryMaxRefinerResults
			{
				set
				{
					base.PowerSharpParameters["DiscoveryMaxRefinerResults"] = value;
				}
			}

			// Token: 0x17004653 RID: 18003
			// (set) Token: 0x06006B89 RID: 27529 RVA: 0x000A2FED File Offset: 0x000A11ED
			public virtual Unlimited<uint>? DiscoveryMaxSearchQueueDepth
			{
				set
				{
					base.PowerSharpParameters["DiscoveryMaxSearchQueueDepth"] = value;
				}
			}

			// Token: 0x17004654 RID: 18004
			// (set) Token: 0x06006B8A RID: 27530 RVA: 0x000A3005 File Offset: 0x000A1205
			public virtual Unlimited<uint>? DiscoverySearchTimeoutPeriod
			{
				set
				{
					base.PowerSharpParameters["DiscoverySearchTimeoutPeriod"] = value;
				}
			}

			// Token: 0x17004655 RID: 18005
			// (set) Token: 0x06006B8B RID: 27531 RVA: 0x000A301D File Offset: 0x000A121D
			public virtual Unlimited<uint>? ComplianceMaxExpansionDGRecipients
			{
				set
				{
					base.PowerSharpParameters["ComplianceMaxExpansionDGRecipients"] = value;
				}
			}

			// Token: 0x17004656 RID: 18006
			// (set) Token: 0x06006B8C RID: 27532 RVA: 0x000A3035 File Offset: 0x000A1235
			public virtual Unlimited<uint>? ComplianceMaxExpansionNestedDGs
			{
				set
				{
					base.PowerSharpParameters["ComplianceMaxExpansionNestedDGs"] = value;
				}
			}

			// Token: 0x17004657 RID: 18007
			// (set) Token: 0x06006B8D RID: 27533 RVA: 0x000A304D File Offset: 0x000A124D
			public virtual Unlimited<uint>? PushNotificationMaxConcurrency
			{
				set
				{
					base.PowerSharpParameters["PushNotificationMaxConcurrency"] = value;
				}
			}

			// Token: 0x17004658 RID: 18008
			// (set) Token: 0x06006B8E RID: 27534 RVA: 0x000A3065 File Offset: 0x000A1265
			public virtual Unlimited<uint>? PushNotificationMaxBurst
			{
				set
				{
					base.PowerSharpParameters["PushNotificationMaxBurst"] = value;
				}
			}

			// Token: 0x17004659 RID: 18009
			// (set) Token: 0x06006B8F RID: 27535 RVA: 0x000A307D File Offset: 0x000A127D
			public virtual Unlimited<uint>? PushNotificationRechargeRate
			{
				set
				{
					base.PowerSharpParameters["PushNotificationRechargeRate"] = value;
				}
			}

			// Token: 0x1700465A RID: 18010
			// (set) Token: 0x06006B90 RID: 27536 RVA: 0x000A3095 File Offset: 0x000A1295
			public virtual Unlimited<uint>? PushNotificationCutoffBalance
			{
				set
				{
					base.PowerSharpParameters["PushNotificationCutoffBalance"] = value;
				}
			}

			// Token: 0x1700465B RID: 18011
			// (set) Token: 0x06006B91 RID: 27537 RVA: 0x000A30AD File Offset: 0x000A12AD
			public virtual Unlimited<uint>? PushNotificationMaxBurstPerDevice
			{
				set
				{
					base.PowerSharpParameters["PushNotificationMaxBurstPerDevice"] = value;
				}
			}

			// Token: 0x1700465C RID: 18012
			// (set) Token: 0x06006B92 RID: 27538 RVA: 0x000A30C5 File Offset: 0x000A12C5
			public virtual Unlimited<uint>? PushNotificationRechargeRatePerDevice
			{
				set
				{
					base.PowerSharpParameters["PushNotificationRechargeRatePerDevice"] = value;
				}
			}

			// Token: 0x1700465D RID: 18013
			// (set) Token: 0x06006B93 RID: 27539 RVA: 0x000A30DD File Offset: 0x000A12DD
			public virtual Unlimited<uint>? PushNotificationSamplingPeriodPerDevice
			{
				set
				{
					base.PowerSharpParameters["PushNotificationSamplingPeriodPerDevice"] = value;
				}
			}

			// Token: 0x1700465E RID: 18014
			// (set) Token: 0x06006B94 RID: 27540 RVA: 0x000A30F5 File Offset: 0x000A12F5
			public virtual SwitchParameter IsServiceAccount
			{
				set
				{
					base.PowerSharpParameters["IsServiceAccount"] = value;
				}
			}

			// Token: 0x1700465F RID: 18015
			// (set) Token: 0x06006B95 RID: 27541 RVA: 0x000A310D File Offset: 0x000A130D
			public virtual ThrottlingPolicyScopeType ThrottlingPolicyScope
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicyScope"] = value;
				}
			}

			// Token: 0x17004660 RID: 18016
			// (set) Token: 0x06006B96 RID: 27542 RVA: 0x000A3125 File Offset: 0x000A1325
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17004661 RID: 18017
			// (set) Token: 0x06006B97 RID: 27543 RVA: 0x000A3143 File Offset: 0x000A1343
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004662 RID: 18018
			// (set) Token: 0x06006B98 RID: 27544 RVA: 0x000A3156 File Offset: 0x000A1356
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004663 RID: 18019
			// (set) Token: 0x06006B99 RID: 27545 RVA: 0x000A3169 File Offset: 0x000A1369
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004664 RID: 18020
			// (set) Token: 0x06006B9A RID: 27546 RVA: 0x000A3181 File Offset: 0x000A1381
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004665 RID: 18021
			// (set) Token: 0x06006B9B RID: 27547 RVA: 0x000A3199 File Offset: 0x000A1399
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004666 RID: 18022
			// (set) Token: 0x06006B9C RID: 27548 RVA: 0x000A31B1 File Offset: 0x000A13B1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004667 RID: 18023
			// (set) Token: 0x06006B9D RID: 27549 RVA: 0x000A31C9 File Offset: 0x000A13C9
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
