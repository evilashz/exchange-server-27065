using System;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000D65 RID: 3429
	public class SetRemoteMailboxCommand : SyntheticCommandWithPipelineInputNoOutput<RemoteMailbox>
	{
		// Token: 0x0600B59D RID: 46493 RVA: 0x001056B6 File Offset: 0x001038B6
		private SetRemoteMailboxCommand() : base("Set-RemoteMailbox")
		{
		}

		// Token: 0x0600B59E RID: 46494 RVA: 0x001056C3 File Offset: 0x001038C3
		public SetRemoteMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600B59F RID: 46495 RVA: 0x001056D2 File Offset: 0x001038D2
		public virtual SetRemoteMailboxCommand SetParameters(SetRemoteMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B5A0 RID: 46496 RVA: 0x001056DC File Offset: 0x001038DC
		public virtual SetRemoteMailboxCommand SetParameters(SetRemoteMailboxCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000D66 RID: 3430
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700868C RID: 34444
			// (set) Token: 0x0600B5A1 RID: 46497 RVA: 0x001056E6 File Offset: 0x001038E6
			public virtual ConvertibleRemoteMailboxSubType Type
			{
				set
				{
					base.PowerSharpParameters["Type"] = value;
				}
			}

			// Token: 0x1700868D RID: 34445
			// (set) Token: 0x0600B5A2 RID: 46498 RVA: 0x001056FE File Offset: 0x001038FE
			public virtual SwitchParameter ACLableSyncedObjectEnabled
			{
				set
				{
					base.PowerSharpParameters["ACLableSyncedObjectEnabled"] = value;
				}
			}

			// Token: 0x1700868E RID: 34446
			// (set) Token: 0x0600B5A3 RID: 46499 RVA: 0x00105716 File Offset: 0x00103916
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x1700868F RID: 34447
			// (set) Token: 0x0600B5A4 RID: 46500 RVA: 0x00105729 File Offset: 0x00103929
			public virtual string SecondaryAddress
			{
				set
				{
					base.PowerSharpParameters["SecondaryAddress"] = value;
				}
			}

			// Token: 0x17008690 RID: 34448
			// (set) Token: 0x0600B5A5 RID: 46501 RVA: 0x0010573C File Offset: 0x0010393C
			public virtual string SecondaryDialPlan
			{
				set
				{
					base.PowerSharpParameters["SecondaryDialPlan"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17008691 RID: 34449
			// (set) Token: 0x0600B5A6 RID: 46502 RVA: 0x0010575A File Offset: 0x0010395A
			public virtual SwitchParameter RemovePicture
			{
				set
				{
					base.PowerSharpParameters["RemovePicture"] = value;
				}
			}

			// Token: 0x17008692 RID: 34450
			// (set) Token: 0x0600B5A7 RID: 46503 RVA: 0x00105772 File Offset: 0x00103972
			public virtual SwitchParameter RemoveSpokenName
			{
				set
				{
					base.PowerSharpParameters["RemoveSpokenName"] = value;
				}
			}

			// Token: 0x17008693 RID: 34451
			// (set) Token: 0x0600B5A8 RID: 46504 RVA: 0x0010578A File Offset: 0x0010398A
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17008694 RID: 34452
			// (set) Token: 0x0600B5A9 RID: 46505 RVA: 0x0010579D File Offset: 0x0010399D
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17008695 RID: 34453
			// (set) Token: 0x0600B5AA RID: 46506 RVA: 0x001057B0 File Offset: 0x001039B0
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x17008696 RID: 34454
			// (set) Token: 0x0600B5AB RID: 46507 RVA: 0x001057C3 File Offset: 0x001039C3
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17008697 RID: 34455
			// (set) Token: 0x0600B5AC RID: 46508 RVA: 0x001057E1 File Offset: 0x001039E1
			public virtual MultiValuedProperty<RecipientIdParameter> GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17008698 RID: 34456
			// (set) Token: 0x0600B5AD RID: 46509 RVA: 0x001057F4 File Offset: 0x001039F4
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17008699 RID: 34457
			// (set) Token: 0x0600B5AE RID: 46510 RVA: 0x00105807 File Offset: 0x00103A07
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x1700869A RID: 34458
			// (set) Token: 0x0600B5AF RID: 46511 RVA: 0x0010581A File Offset: 0x00103A1A
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x1700869B RID: 34459
			// (set) Token: 0x0600B5B0 RID: 46512 RVA: 0x0010582D File Offset: 0x00103A2D
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x1700869C RID: 34460
			// (set) Token: 0x0600B5B1 RID: 46513 RVA: 0x00105840 File Offset: 0x00103A40
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x1700869D RID: 34461
			// (set) Token: 0x0600B5B2 RID: 46514 RVA: 0x00105853 File Offset: 0x00103A53
			public virtual bool CreateDTMFMap
			{
				set
				{
					base.PowerSharpParameters["CreateDTMFMap"] = value;
				}
			}

			// Token: 0x1700869E RID: 34462
			// (set) Token: 0x0600B5B3 RID: 46515 RVA: 0x0010586B File Offset: 0x00103A6B
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700869F RID: 34463
			// (set) Token: 0x0600B5B4 RID: 46516 RVA: 0x00105883 File Offset: 0x00103A83
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170086A0 RID: 34464
			// (set) Token: 0x0600B5B5 RID: 46517 RVA: 0x00105896 File Offset: 0x00103A96
			public virtual ProxyAddress RemoteRoutingAddress
			{
				set
				{
					base.PowerSharpParameters["RemoteRoutingAddress"] = value;
				}
			}

			// Token: 0x170086A1 RID: 34465
			// (set) Token: 0x0600B5B6 RID: 46518 RVA: 0x001058A9 File Offset: 0x00103AA9
			public virtual Guid ExchangeGuid
			{
				set
				{
					base.PowerSharpParameters["ExchangeGuid"] = value;
				}
			}

			// Token: 0x170086A2 RID: 34466
			// (set) Token: 0x0600B5B7 RID: 46519 RVA: 0x001058C1 File Offset: 0x00103AC1
			public virtual Guid? MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x170086A3 RID: 34467
			// (set) Token: 0x0600B5B8 RID: 46520 RVA: 0x001058D9 File Offset: 0x00103AD9
			public virtual MultiValuedProperty<Guid> AggregatedMailboxGuids
			{
				set
				{
					base.PowerSharpParameters["AggregatedMailboxGuids"] = value;
				}
			}

			// Token: 0x170086A4 RID: 34468
			// (set) Token: 0x0600B5B9 RID: 46521 RVA: 0x001058EC File Offset: 0x00103AEC
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x170086A5 RID: 34469
			// (set) Token: 0x0600B5BA RID: 46522 RVA: 0x00105904 File Offset: 0x00103B04
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x170086A6 RID: 34470
			// (set) Token: 0x0600B5BB RID: 46523 RVA: 0x00105917 File Offset: 0x00103B17
			public virtual Unlimited<ByteQuantifiedSize> ArchiveQuota
			{
				set
				{
					base.PowerSharpParameters["ArchiveQuota"] = value;
				}
			}

			// Token: 0x170086A7 RID: 34471
			// (set) Token: 0x0600B5BC RID: 46524 RVA: 0x0010592F File Offset: 0x00103B2F
			public virtual Unlimited<ByteQuantifiedSize> ArchiveWarningQuota
			{
				set
				{
					base.PowerSharpParameters["ArchiveWarningQuota"] = value;
				}
			}

			// Token: 0x170086A8 RID: 34472
			// (set) Token: 0x0600B5BD RID: 46525 RVA: 0x00105947 File Offset: 0x00103B47
			public virtual SmtpAddress JournalArchiveAddress
			{
				set
				{
					base.PowerSharpParameters["JournalArchiveAddress"] = value;
				}
			}

			// Token: 0x170086A9 RID: 34473
			// (set) Token: 0x0600B5BE RID: 46526 RVA: 0x0010595F File Offset: 0x00103B5F
			public virtual Unlimited<int> RecipientLimits
			{
				set
				{
					base.PowerSharpParameters["RecipientLimits"] = value;
				}
			}

			// Token: 0x170086AA RID: 34474
			// (set) Token: 0x0600B5BF RID: 46527 RVA: 0x00105977 File Offset: 0x00103B77
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x170086AB RID: 34475
			// (set) Token: 0x0600B5C0 RID: 46528 RVA: 0x0010598A File Offset: 0x00103B8A
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x170086AC RID: 34476
			// (set) Token: 0x0600B5C1 RID: 46529 RVA: 0x0010599D File Offset: 0x00103B9D
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x170086AD RID: 34477
			// (set) Token: 0x0600B5C2 RID: 46530 RVA: 0x001059B0 File Offset: 0x00103BB0
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x170086AE RID: 34478
			// (set) Token: 0x0600B5C3 RID: 46531 RVA: 0x001059C8 File Offset: 0x00103BC8
			public virtual bool LitigationHoldEnabled
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldEnabled"] = value;
				}
			}

			// Token: 0x170086AF RID: 34479
			// (set) Token: 0x0600B5C4 RID: 46532 RVA: 0x001059E0 File Offset: 0x00103BE0
			public virtual bool SingleItemRecoveryEnabled
			{
				set
				{
					base.PowerSharpParameters["SingleItemRecoveryEnabled"] = value;
				}
			}

			// Token: 0x170086B0 RID: 34480
			// (set) Token: 0x0600B5C5 RID: 46533 RVA: 0x001059F8 File Offset: 0x00103BF8
			public virtual bool RetentionHoldEnabled
			{
				set
				{
					base.PowerSharpParameters["RetentionHoldEnabled"] = value;
				}
			}

			// Token: 0x170086B1 RID: 34481
			// (set) Token: 0x0600B5C6 RID: 46534 RVA: 0x00105A10 File Offset: 0x00103C10
			public virtual DateTime? EndDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["EndDateForRetentionHold"] = value;
				}
			}

			// Token: 0x170086B2 RID: 34482
			// (set) Token: 0x0600B5C7 RID: 46535 RVA: 0x00105A28 File Offset: 0x00103C28
			public virtual DateTime? StartDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["StartDateForRetentionHold"] = value;
				}
			}

			// Token: 0x170086B3 RID: 34483
			// (set) Token: 0x0600B5C8 RID: 46536 RVA: 0x00105A40 File Offset: 0x00103C40
			public virtual string RetentionComment
			{
				set
				{
					base.PowerSharpParameters["RetentionComment"] = value;
				}
			}

			// Token: 0x170086B4 RID: 34484
			// (set) Token: 0x0600B5C9 RID: 46537 RVA: 0x00105A53 File Offset: 0x00103C53
			public virtual string RetentionUrl
			{
				set
				{
					base.PowerSharpParameters["RetentionUrl"] = value;
				}
			}

			// Token: 0x170086B5 RID: 34485
			// (set) Token: 0x0600B5CA RID: 46538 RVA: 0x00105A66 File Offset: 0x00103C66
			public virtual DateTime? LitigationHoldDate
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldDate"] = value;
				}
			}

			// Token: 0x170086B6 RID: 34486
			// (set) Token: 0x0600B5CB RID: 46539 RVA: 0x00105A7E File Offset: 0x00103C7E
			public virtual string LitigationHoldOwner
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldOwner"] = value;
				}
			}

			// Token: 0x170086B7 RID: 34487
			// (set) Token: 0x0600B5CC RID: 46540 RVA: 0x00105A91 File Offset: 0x00103C91
			public virtual EnhancedTimeSpan RetainDeletedItemsFor
			{
				set
				{
					base.PowerSharpParameters["RetainDeletedItemsFor"] = value;
				}
			}

			// Token: 0x170086B8 RID: 34488
			// (set) Token: 0x0600B5CD RID: 46541 RVA: 0x00105AA9 File Offset: 0x00103CA9
			public virtual bool CalendarVersionStoreDisabled
			{
				set
				{
					base.PowerSharpParameters["CalendarVersionStoreDisabled"] = value;
				}
			}

			// Token: 0x170086B9 RID: 34489
			// (set) Token: 0x0600B5CE RID: 46542 RVA: 0x00105AC1 File Offset: 0x00103CC1
			public virtual Unlimited<ByteQuantifiedSize> RecoverableItemsQuota
			{
				set
				{
					base.PowerSharpParameters["RecoverableItemsQuota"] = value;
				}
			}

			// Token: 0x170086BA RID: 34490
			// (set) Token: 0x0600B5CF RID: 46543 RVA: 0x00105AD9 File Offset: 0x00103CD9
			public virtual Unlimited<ByteQuantifiedSize> RecoverableItemsWarningQuota
			{
				set
				{
					base.PowerSharpParameters["RecoverableItemsWarningQuota"] = value;
				}
			}

			// Token: 0x170086BB RID: 34491
			// (set) Token: 0x0600B5D0 RID: 46544 RVA: 0x00105AF1 File Offset: 0x00103CF1
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x170086BC RID: 34492
			// (set) Token: 0x0600B5D1 RID: 46545 RVA: 0x00105B04 File Offset: 0x00103D04
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x170086BD RID: 34493
			// (set) Token: 0x0600B5D2 RID: 46546 RVA: 0x00105B17 File Offset: 0x00103D17
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x170086BE RID: 34494
			// (set) Token: 0x0600B5D3 RID: 46547 RVA: 0x00105B2A File Offset: 0x00103D2A
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x170086BF RID: 34495
			// (set) Token: 0x0600B5D4 RID: 46548 RVA: 0x00105B3D File Offset: 0x00103D3D
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x170086C0 RID: 34496
			// (set) Token: 0x0600B5D5 RID: 46549 RVA: 0x00105B50 File Offset: 0x00103D50
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x170086C1 RID: 34497
			// (set) Token: 0x0600B5D6 RID: 46550 RVA: 0x00105B63 File Offset: 0x00103D63
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x170086C2 RID: 34498
			// (set) Token: 0x0600B5D7 RID: 46551 RVA: 0x00105B76 File Offset: 0x00103D76
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x170086C3 RID: 34499
			// (set) Token: 0x0600B5D8 RID: 46552 RVA: 0x00105B89 File Offset: 0x00103D89
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x170086C4 RID: 34500
			// (set) Token: 0x0600B5D9 RID: 46553 RVA: 0x00105B9C File Offset: 0x00103D9C
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x170086C5 RID: 34501
			// (set) Token: 0x0600B5DA RID: 46554 RVA: 0x00105BAF File Offset: 0x00103DAF
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x170086C6 RID: 34502
			// (set) Token: 0x0600B5DB RID: 46555 RVA: 0x00105BC2 File Offset: 0x00103DC2
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x170086C7 RID: 34503
			// (set) Token: 0x0600B5DC RID: 46556 RVA: 0x00105BD5 File Offset: 0x00103DD5
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x170086C8 RID: 34504
			// (set) Token: 0x0600B5DD RID: 46557 RVA: 0x00105BE8 File Offset: 0x00103DE8
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x170086C9 RID: 34505
			// (set) Token: 0x0600B5DE RID: 46558 RVA: 0x00105BFB File Offset: 0x00103DFB
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x170086CA RID: 34506
			// (set) Token: 0x0600B5DF RID: 46559 RVA: 0x00105C0E File Offset: 0x00103E0E
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x170086CB RID: 34507
			// (set) Token: 0x0600B5E0 RID: 46560 RVA: 0x00105C21 File Offset: 0x00103E21
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x170086CC RID: 34508
			// (set) Token: 0x0600B5E1 RID: 46561 RVA: 0x00105C34 File Offset: 0x00103E34
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x170086CD RID: 34509
			// (set) Token: 0x0600B5E2 RID: 46562 RVA: 0x00105C47 File Offset: 0x00103E47
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x170086CE RID: 34510
			// (set) Token: 0x0600B5E3 RID: 46563 RVA: 0x00105C5A File Offset: 0x00103E5A
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x170086CF RID: 34511
			// (set) Token: 0x0600B5E4 RID: 46564 RVA: 0x00105C6D File Offset: 0x00103E6D
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x170086D0 RID: 34512
			// (set) Token: 0x0600B5E5 RID: 46565 RVA: 0x00105C80 File Offset: 0x00103E80
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x170086D1 RID: 34513
			// (set) Token: 0x0600B5E6 RID: 46566 RVA: 0x00105C93 File Offset: 0x00103E93
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x170086D2 RID: 34514
			// (set) Token: 0x0600B5E7 RID: 46567 RVA: 0x00105CA6 File Offset: 0x00103EA6
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x170086D3 RID: 34515
			// (set) Token: 0x0600B5E8 RID: 46568 RVA: 0x00105CB9 File Offset: 0x00103EB9
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x170086D4 RID: 34516
			// (set) Token: 0x0600B5E9 RID: 46569 RVA: 0x00105CCC File Offset: 0x00103ECC
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x170086D5 RID: 34517
			// (set) Token: 0x0600B5EA RID: 46570 RVA: 0x00105CE4 File Offset: 0x00103EE4
			public virtual Unlimited<ByteQuantifiedSize> MaxSendSize
			{
				set
				{
					base.PowerSharpParameters["MaxSendSize"] = value;
				}
			}

			// Token: 0x170086D6 RID: 34518
			// (set) Token: 0x0600B5EB RID: 46571 RVA: 0x00105CFC File Offset: 0x00103EFC
			public virtual Unlimited<ByteQuantifiedSize> MaxReceiveSize
			{
				set
				{
					base.PowerSharpParameters["MaxReceiveSize"] = value;
				}
			}

			// Token: 0x170086D7 RID: 34519
			// (set) Token: 0x0600B5EC RID: 46572 RVA: 0x00105D14 File Offset: 0x00103F14
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x170086D8 RID: 34520
			// (set) Token: 0x0600B5ED RID: 46573 RVA: 0x00105D2C File Offset: 0x00103F2C
			public virtual bool EmailAddressPolicyEnabled
			{
				set
				{
					base.PowerSharpParameters["EmailAddressPolicyEnabled"] = value;
				}
			}

			// Token: 0x170086D9 RID: 34521
			// (set) Token: 0x0600B5EE RID: 46574 RVA: 0x00105D44 File Offset: 0x00103F44
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x170086DA RID: 34522
			// (set) Token: 0x0600B5EF RID: 46575 RVA: 0x00105D5C File Offset: 0x00103F5C
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x170086DB RID: 34523
			// (set) Token: 0x0600B5F0 RID: 46576 RVA: 0x00105D74 File Offset: 0x00103F74
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x170086DC RID: 34524
			// (set) Token: 0x0600B5F1 RID: 46577 RVA: 0x00105D87 File Offset: 0x00103F87
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x170086DD RID: 34525
			// (set) Token: 0x0600B5F2 RID: 46578 RVA: 0x00105D9F File Offset: 0x00103F9F
			public virtual MultiValuedProperty<string> UMDtmfMap
			{
				set
				{
					base.PowerSharpParameters["UMDtmfMap"] = value;
				}
			}

			// Token: 0x170086DE RID: 34526
			// (set) Token: 0x0600B5F3 RID: 46579 RVA: 0x00105DB2 File Offset: 0x00103FB2
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x170086DF RID: 34527
			// (set) Token: 0x0600B5F4 RID: 46580 RVA: 0x00105DCA File Offset: 0x00103FCA
			public virtual string MailTip
			{
				set
				{
					base.PowerSharpParameters["MailTip"] = value;
				}
			}

			// Token: 0x170086E0 RID: 34528
			// (set) Token: 0x0600B5F5 RID: 46581 RVA: 0x00105DDD File Offset: 0x00103FDD
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x170086E1 RID: 34529
			// (set) Token: 0x0600B5F6 RID: 46582 RVA: 0x00105DF0 File Offset: 0x00103FF0
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170086E2 RID: 34530
			// (set) Token: 0x0600B5F7 RID: 46583 RVA: 0x00105E03 File Offset: 0x00104003
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170086E3 RID: 34531
			// (set) Token: 0x0600B5F8 RID: 46584 RVA: 0x00105E1B File Offset: 0x0010401B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170086E4 RID: 34532
			// (set) Token: 0x0600B5F9 RID: 46585 RVA: 0x00105E33 File Offset: 0x00104033
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170086E5 RID: 34533
			// (set) Token: 0x0600B5FA RID: 46586 RVA: 0x00105E4B File Offset: 0x0010404B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170086E6 RID: 34534
			// (set) Token: 0x0600B5FB RID: 46587 RVA: 0x00105E63 File Offset: 0x00104063
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D67 RID: 3431
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170086E7 RID: 34535
			// (set) Token: 0x0600B5FD RID: 46589 RVA: 0x00105E83 File Offset: 0x00104083
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RemoteMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170086E8 RID: 34536
			// (set) Token: 0x0600B5FE RID: 46590 RVA: 0x00105EA1 File Offset: 0x001040A1
			public virtual ConvertibleRemoteMailboxSubType Type
			{
				set
				{
					base.PowerSharpParameters["Type"] = value;
				}
			}

			// Token: 0x170086E9 RID: 34537
			// (set) Token: 0x0600B5FF RID: 46591 RVA: 0x00105EB9 File Offset: 0x001040B9
			public virtual SwitchParameter ACLableSyncedObjectEnabled
			{
				set
				{
					base.PowerSharpParameters["ACLableSyncedObjectEnabled"] = value;
				}
			}

			// Token: 0x170086EA RID: 34538
			// (set) Token: 0x0600B600 RID: 46592 RVA: 0x00105ED1 File Offset: 0x001040D1
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x170086EB RID: 34539
			// (set) Token: 0x0600B601 RID: 46593 RVA: 0x00105EE4 File Offset: 0x001040E4
			public virtual string SecondaryAddress
			{
				set
				{
					base.PowerSharpParameters["SecondaryAddress"] = value;
				}
			}

			// Token: 0x170086EC RID: 34540
			// (set) Token: 0x0600B602 RID: 46594 RVA: 0x00105EF7 File Offset: 0x001040F7
			public virtual string SecondaryDialPlan
			{
				set
				{
					base.PowerSharpParameters["SecondaryDialPlan"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x170086ED RID: 34541
			// (set) Token: 0x0600B603 RID: 46595 RVA: 0x00105F15 File Offset: 0x00104115
			public virtual SwitchParameter RemovePicture
			{
				set
				{
					base.PowerSharpParameters["RemovePicture"] = value;
				}
			}

			// Token: 0x170086EE RID: 34542
			// (set) Token: 0x0600B604 RID: 46596 RVA: 0x00105F2D File Offset: 0x0010412D
			public virtual SwitchParameter RemoveSpokenName
			{
				set
				{
					base.PowerSharpParameters["RemoveSpokenName"] = value;
				}
			}

			// Token: 0x170086EF RID: 34543
			// (set) Token: 0x0600B605 RID: 46597 RVA: 0x00105F45 File Offset: 0x00104145
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x170086F0 RID: 34544
			// (set) Token: 0x0600B606 RID: 46598 RVA: 0x00105F58 File Offset: 0x00104158
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x170086F1 RID: 34545
			// (set) Token: 0x0600B607 RID: 46599 RVA: 0x00105F6B File Offset: 0x0010416B
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x170086F2 RID: 34546
			// (set) Token: 0x0600B608 RID: 46600 RVA: 0x00105F7E File Offset: 0x0010417E
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170086F3 RID: 34547
			// (set) Token: 0x0600B609 RID: 46601 RVA: 0x00105F9C File Offset: 0x0010419C
			public virtual MultiValuedProperty<RecipientIdParameter> GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x170086F4 RID: 34548
			// (set) Token: 0x0600B60A RID: 46602 RVA: 0x00105FAF File Offset: 0x001041AF
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x170086F5 RID: 34549
			// (set) Token: 0x0600B60B RID: 46603 RVA: 0x00105FC2 File Offset: 0x001041C2
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x170086F6 RID: 34550
			// (set) Token: 0x0600B60C RID: 46604 RVA: 0x00105FD5 File Offset: 0x001041D5
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x170086F7 RID: 34551
			// (set) Token: 0x0600B60D RID: 46605 RVA: 0x00105FE8 File Offset: 0x001041E8
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x170086F8 RID: 34552
			// (set) Token: 0x0600B60E RID: 46606 RVA: 0x00105FFB File Offset: 0x001041FB
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x170086F9 RID: 34553
			// (set) Token: 0x0600B60F RID: 46607 RVA: 0x0010600E File Offset: 0x0010420E
			public virtual bool CreateDTMFMap
			{
				set
				{
					base.PowerSharpParameters["CreateDTMFMap"] = value;
				}
			}

			// Token: 0x170086FA RID: 34554
			// (set) Token: 0x0600B610 RID: 46608 RVA: 0x00106026 File Offset: 0x00104226
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170086FB RID: 34555
			// (set) Token: 0x0600B611 RID: 46609 RVA: 0x0010603E File Offset: 0x0010423E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170086FC RID: 34556
			// (set) Token: 0x0600B612 RID: 46610 RVA: 0x00106051 File Offset: 0x00104251
			public virtual ProxyAddress RemoteRoutingAddress
			{
				set
				{
					base.PowerSharpParameters["RemoteRoutingAddress"] = value;
				}
			}

			// Token: 0x170086FD RID: 34557
			// (set) Token: 0x0600B613 RID: 46611 RVA: 0x00106064 File Offset: 0x00104264
			public virtual Guid ExchangeGuid
			{
				set
				{
					base.PowerSharpParameters["ExchangeGuid"] = value;
				}
			}

			// Token: 0x170086FE RID: 34558
			// (set) Token: 0x0600B614 RID: 46612 RVA: 0x0010607C File Offset: 0x0010427C
			public virtual Guid? MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x170086FF RID: 34559
			// (set) Token: 0x0600B615 RID: 46613 RVA: 0x00106094 File Offset: 0x00104294
			public virtual MultiValuedProperty<Guid> AggregatedMailboxGuids
			{
				set
				{
					base.PowerSharpParameters["AggregatedMailboxGuids"] = value;
				}
			}

			// Token: 0x17008700 RID: 34560
			// (set) Token: 0x0600B616 RID: 46614 RVA: 0x001060A7 File Offset: 0x001042A7
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x17008701 RID: 34561
			// (set) Token: 0x0600B617 RID: 46615 RVA: 0x001060BF File Offset: 0x001042BF
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x17008702 RID: 34562
			// (set) Token: 0x0600B618 RID: 46616 RVA: 0x001060D2 File Offset: 0x001042D2
			public virtual Unlimited<ByteQuantifiedSize> ArchiveQuota
			{
				set
				{
					base.PowerSharpParameters["ArchiveQuota"] = value;
				}
			}

			// Token: 0x17008703 RID: 34563
			// (set) Token: 0x0600B619 RID: 46617 RVA: 0x001060EA File Offset: 0x001042EA
			public virtual Unlimited<ByteQuantifiedSize> ArchiveWarningQuota
			{
				set
				{
					base.PowerSharpParameters["ArchiveWarningQuota"] = value;
				}
			}

			// Token: 0x17008704 RID: 34564
			// (set) Token: 0x0600B61A RID: 46618 RVA: 0x00106102 File Offset: 0x00104302
			public virtual SmtpAddress JournalArchiveAddress
			{
				set
				{
					base.PowerSharpParameters["JournalArchiveAddress"] = value;
				}
			}

			// Token: 0x17008705 RID: 34565
			// (set) Token: 0x0600B61B RID: 46619 RVA: 0x0010611A File Offset: 0x0010431A
			public virtual Unlimited<int> RecipientLimits
			{
				set
				{
					base.PowerSharpParameters["RecipientLimits"] = value;
				}
			}

			// Token: 0x17008706 RID: 34566
			// (set) Token: 0x0600B61C RID: 46620 RVA: 0x00106132 File Offset: 0x00104332
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17008707 RID: 34567
			// (set) Token: 0x0600B61D RID: 46621 RVA: 0x00106145 File Offset: 0x00104345
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17008708 RID: 34568
			// (set) Token: 0x0600B61E RID: 46622 RVA: 0x00106158 File Offset: 0x00104358
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17008709 RID: 34569
			// (set) Token: 0x0600B61F RID: 46623 RVA: 0x0010616B File Offset: 0x0010436B
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x1700870A RID: 34570
			// (set) Token: 0x0600B620 RID: 46624 RVA: 0x00106183 File Offset: 0x00104383
			public virtual bool LitigationHoldEnabled
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldEnabled"] = value;
				}
			}

			// Token: 0x1700870B RID: 34571
			// (set) Token: 0x0600B621 RID: 46625 RVA: 0x0010619B File Offset: 0x0010439B
			public virtual bool SingleItemRecoveryEnabled
			{
				set
				{
					base.PowerSharpParameters["SingleItemRecoveryEnabled"] = value;
				}
			}

			// Token: 0x1700870C RID: 34572
			// (set) Token: 0x0600B622 RID: 46626 RVA: 0x001061B3 File Offset: 0x001043B3
			public virtual bool RetentionHoldEnabled
			{
				set
				{
					base.PowerSharpParameters["RetentionHoldEnabled"] = value;
				}
			}

			// Token: 0x1700870D RID: 34573
			// (set) Token: 0x0600B623 RID: 46627 RVA: 0x001061CB File Offset: 0x001043CB
			public virtual DateTime? EndDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["EndDateForRetentionHold"] = value;
				}
			}

			// Token: 0x1700870E RID: 34574
			// (set) Token: 0x0600B624 RID: 46628 RVA: 0x001061E3 File Offset: 0x001043E3
			public virtual DateTime? StartDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["StartDateForRetentionHold"] = value;
				}
			}

			// Token: 0x1700870F RID: 34575
			// (set) Token: 0x0600B625 RID: 46629 RVA: 0x001061FB File Offset: 0x001043FB
			public virtual string RetentionComment
			{
				set
				{
					base.PowerSharpParameters["RetentionComment"] = value;
				}
			}

			// Token: 0x17008710 RID: 34576
			// (set) Token: 0x0600B626 RID: 46630 RVA: 0x0010620E File Offset: 0x0010440E
			public virtual string RetentionUrl
			{
				set
				{
					base.PowerSharpParameters["RetentionUrl"] = value;
				}
			}

			// Token: 0x17008711 RID: 34577
			// (set) Token: 0x0600B627 RID: 46631 RVA: 0x00106221 File Offset: 0x00104421
			public virtual DateTime? LitigationHoldDate
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldDate"] = value;
				}
			}

			// Token: 0x17008712 RID: 34578
			// (set) Token: 0x0600B628 RID: 46632 RVA: 0x00106239 File Offset: 0x00104439
			public virtual string LitigationHoldOwner
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldOwner"] = value;
				}
			}

			// Token: 0x17008713 RID: 34579
			// (set) Token: 0x0600B629 RID: 46633 RVA: 0x0010624C File Offset: 0x0010444C
			public virtual EnhancedTimeSpan RetainDeletedItemsFor
			{
				set
				{
					base.PowerSharpParameters["RetainDeletedItemsFor"] = value;
				}
			}

			// Token: 0x17008714 RID: 34580
			// (set) Token: 0x0600B62A RID: 46634 RVA: 0x00106264 File Offset: 0x00104464
			public virtual bool CalendarVersionStoreDisabled
			{
				set
				{
					base.PowerSharpParameters["CalendarVersionStoreDisabled"] = value;
				}
			}

			// Token: 0x17008715 RID: 34581
			// (set) Token: 0x0600B62B RID: 46635 RVA: 0x0010627C File Offset: 0x0010447C
			public virtual Unlimited<ByteQuantifiedSize> RecoverableItemsQuota
			{
				set
				{
					base.PowerSharpParameters["RecoverableItemsQuota"] = value;
				}
			}

			// Token: 0x17008716 RID: 34582
			// (set) Token: 0x0600B62C RID: 46636 RVA: 0x00106294 File Offset: 0x00104494
			public virtual Unlimited<ByteQuantifiedSize> RecoverableItemsWarningQuota
			{
				set
				{
					base.PowerSharpParameters["RecoverableItemsWarningQuota"] = value;
				}
			}

			// Token: 0x17008717 RID: 34583
			// (set) Token: 0x0600B62D RID: 46637 RVA: 0x001062AC File Offset: 0x001044AC
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x17008718 RID: 34584
			// (set) Token: 0x0600B62E RID: 46638 RVA: 0x001062BF File Offset: 0x001044BF
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x17008719 RID: 34585
			// (set) Token: 0x0600B62F RID: 46639 RVA: 0x001062D2 File Offset: 0x001044D2
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700871A RID: 34586
			// (set) Token: 0x0600B630 RID: 46640 RVA: 0x001062E5 File Offset: 0x001044E5
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x1700871B RID: 34587
			// (set) Token: 0x0600B631 RID: 46641 RVA: 0x001062F8 File Offset: 0x001044F8
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x1700871C RID: 34588
			// (set) Token: 0x0600B632 RID: 46642 RVA: 0x0010630B File Offset: 0x0010450B
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x1700871D RID: 34589
			// (set) Token: 0x0600B633 RID: 46643 RVA: 0x0010631E File Offset: 0x0010451E
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x1700871E RID: 34590
			// (set) Token: 0x0600B634 RID: 46644 RVA: 0x00106331 File Offset: 0x00104531
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x1700871F RID: 34591
			// (set) Token: 0x0600B635 RID: 46645 RVA: 0x00106344 File Offset: 0x00104544
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17008720 RID: 34592
			// (set) Token: 0x0600B636 RID: 46646 RVA: 0x00106357 File Offset: 0x00104557
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17008721 RID: 34593
			// (set) Token: 0x0600B637 RID: 46647 RVA: 0x0010636A File Offset: 0x0010456A
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17008722 RID: 34594
			// (set) Token: 0x0600B638 RID: 46648 RVA: 0x0010637D File Offset: 0x0010457D
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17008723 RID: 34595
			// (set) Token: 0x0600B639 RID: 46649 RVA: 0x00106390 File Offset: 0x00104590
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x17008724 RID: 34596
			// (set) Token: 0x0600B63A RID: 46650 RVA: 0x001063A3 File Offset: 0x001045A3
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17008725 RID: 34597
			// (set) Token: 0x0600B63B RID: 46651 RVA: 0x001063B6 File Offset: 0x001045B6
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17008726 RID: 34598
			// (set) Token: 0x0600B63C RID: 46652 RVA: 0x001063C9 File Offset: 0x001045C9
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17008727 RID: 34599
			// (set) Token: 0x0600B63D RID: 46653 RVA: 0x001063DC File Offset: 0x001045DC
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17008728 RID: 34600
			// (set) Token: 0x0600B63E RID: 46654 RVA: 0x001063EF File Offset: 0x001045EF
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x17008729 RID: 34601
			// (set) Token: 0x0600B63F RID: 46655 RVA: 0x00106402 File Offset: 0x00104602
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x1700872A RID: 34602
			// (set) Token: 0x0600B640 RID: 46656 RVA: 0x00106415 File Offset: 0x00104615
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x1700872B RID: 34603
			// (set) Token: 0x0600B641 RID: 46657 RVA: 0x00106428 File Offset: 0x00104628
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x1700872C RID: 34604
			// (set) Token: 0x0600B642 RID: 46658 RVA: 0x0010643B File Offset: 0x0010463B
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x1700872D RID: 34605
			// (set) Token: 0x0600B643 RID: 46659 RVA: 0x0010644E File Offset: 0x0010464E
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x1700872E RID: 34606
			// (set) Token: 0x0600B644 RID: 46660 RVA: 0x00106461 File Offset: 0x00104661
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700872F RID: 34607
			// (set) Token: 0x0600B645 RID: 46661 RVA: 0x00106474 File Offset: 0x00104674
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17008730 RID: 34608
			// (set) Token: 0x0600B646 RID: 46662 RVA: 0x00106487 File Offset: 0x00104687
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17008731 RID: 34609
			// (set) Token: 0x0600B647 RID: 46663 RVA: 0x0010649F File Offset: 0x0010469F
			public virtual Unlimited<ByteQuantifiedSize> MaxSendSize
			{
				set
				{
					base.PowerSharpParameters["MaxSendSize"] = value;
				}
			}

			// Token: 0x17008732 RID: 34610
			// (set) Token: 0x0600B648 RID: 46664 RVA: 0x001064B7 File Offset: 0x001046B7
			public virtual Unlimited<ByteQuantifiedSize> MaxReceiveSize
			{
				set
				{
					base.PowerSharpParameters["MaxReceiveSize"] = value;
				}
			}

			// Token: 0x17008733 RID: 34611
			// (set) Token: 0x0600B649 RID: 46665 RVA: 0x001064CF File Offset: 0x001046CF
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17008734 RID: 34612
			// (set) Token: 0x0600B64A RID: 46666 RVA: 0x001064E7 File Offset: 0x001046E7
			public virtual bool EmailAddressPolicyEnabled
			{
				set
				{
					base.PowerSharpParameters["EmailAddressPolicyEnabled"] = value;
				}
			}

			// Token: 0x17008735 RID: 34613
			// (set) Token: 0x0600B64B RID: 46667 RVA: 0x001064FF File Offset: 0x001046FF
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17008736 RID: 34614
			// (set) Token: 0x0600B64C RID: 46668 RVA: 0x00106517 File Offset: 0x00104717
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17008737 RID: 34615
			// (set) Token: 0x0600B64D RID: 46669 RVA: 0x0010652F File Offset: 0x0010472F
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x17008738 RID: 34616
			// (set) Token: 0x0600B64E RID: 46670 RVA: 0x00106542 File Offset: 0x00104742
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17008739 RID: 34617
			// (set) Token: 0x0600B64F RID: 46671 RVA: 0x0010655A File Offset: 0x0010475A
			public virtual MultiValuedProperty<string> UMDtmfMap
			{
				set
				{
					base.PowerSharpParameters["UMDtmfMap"] = value;
				}
			}

			// Token: 0x1700873A RID: 34618
			// (set) Token: 0x0600B650 RID: 46672 RVA: 0x0010656D File Offset: 0x0010476D
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x1700873B RID: 34619
			// (set) Token: 0x0600B651 RID: 46673 RVA: 0x00106585 File Offset: 0x00104785
			public virtual string MailTip
			{
				set
				{
					base.PowerSharpParameters["MailTip"] = value;
				}
			}

			// Token: 0x1700873C RID: 34620
			// (set) Token: 0x0600B652 RID: 46674 RVA: 0x00106598 File Offset: 0x00104798
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x1700873D RID: 34621
			// (set) Token: 0x0600B653 RID: 46675 RVA: 0x001065AB File Offset: 0x001047AB
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700873E RID: 34622
			// (set) Token: 0x0600B654 RID: 46676 RVA: 0x001065BE File Offset: 0x001047BE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700873F RID: 34623
			// (set) Token: 0x0600B655 RID: 46677 RVA: 0x001065D6 File Offset: 0x001047D6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008740 RID: 34624
			// (set) Token: 0x0600B656 RID: 46678 RVA: 0x001065EE File Offset: 0x001047EE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008741 RID: 34625
			// (set) Token: 0x0600B657 RID: 46679 RVA: 0x00106606 File Offset: 0x00104806
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008742 RID: 34626
			// (set) Token: 0x0600B658 RID: 46680 RVA: 0x0010661E File Offset: 0x0010481E
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
