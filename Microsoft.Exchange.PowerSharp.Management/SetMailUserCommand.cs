using System;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000D10 RID: 3344
	public class SetMailUserCommand : SyntheticCommandWithPipelineInputNoOutput<MailUser>
	{
		// Token: 0x0600B0F6 RID: 45302 RVA: 0x000FF587 File Offset: 0x000FD787
		private SetMailUserCommand() : base("Set-MailUser")
		{
		}

		// Token: 0x0600B0F7 RID: 45303 RVA: 0x000FF594 File Offset: 0x000FD794
		public SetMailUserCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600B0F8 RID: 45304 RVA: 0x000FF5A3 File Offset: 0x000FD7A3
		public virtual SetMailUserCommand SetParameters(SetMailUserCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B0F9 RID: 45305 RVA: 0x000FF5AD File Offset: 0x000FD7AD
		public virtual SetMailUserCommand SetParameters(SetMailUserCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000D11 RID: 3345
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700828F RID: 33423
			// (set) Token: 0x0600B0FA RID: 45306 RVA: 0x000FF5B7 File Offset: 0x000FD7B7
			public virtual SwitchParameter ForceUpgrade
			{
				set
				{
					base.PowerSharpParameters["ForceUpgrade"] = value;
				}
			}

			// Token: 0x17008290 RID: 33424
			// (set) Token: 0x0600B0FB RID: 45307 RVA: 0x000FF5CF File Offset: 0x000FD7CF
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17008291 RID: 33425
			// (set) Token: 0x0600B0FC RID: 45308 RVA: 0x000FF5E2 File Offset: 0x000FD7E2
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17008292 RID: 33426
			// (set) Token: 0x0600B0FD RID: 45309 RVA: 0x000FF5F5 File Offset: 0x000FD7F5
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x17008293 RID: 33427
			// (set) Token: 0x0600B0FE RID: 45310 RVA: 0x000FF60D File Offset: 0x000FD80D
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x17008294 RID: 33428
			// (set) Token: 0x0600B0FF RID: 45311 RVA: 0x000FF620 File Offset: 0x000FD820
			public virtual SwitchParameter BypassLiveId
			{
				set
				{
					base.PowerSharpParameters["BypassLiveId"] = value;
				}
			}

			// Token: 0x17008295 RID: 33429
			// (set) Token: 0x0600B100 RID: 45312 RVA: 0x000FF638 File Offset: 0x000FD838
			public virtual NetID NetID
			{
				set
				{
					base.PowerSharpParameters["NetID"] = value;
				}
			}

			// Token: 0x17008296 RID: 33430
			// (set) Token: 0x0600B101 RID: 45313 RVA: 0x000FF64B File Offset: 0x000FD84B
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17008297 RID: 33431
			// (set) Token: 0x0600B102 RID: 45314 RVA: 0x000FF65E File Offset: 0x000FD85E
			public virtual string FederatedIdentity
			{
				set
				{
					base.PowerSharpParameters["FederatedIdentity"] = value;
				}
			}

			// Token: 0x17008298 RID: 33432
			// (set) Token: 0x0600B103 RID: 45315 RVA: 0x000FF671 File Offset: 0x000FD871
			public virtual string SecondaryAddress
			{
				set
				{
					base.PowerSharpParameters["SecondaryAddress"] = value;
				}
			}

			// Token: 0x17008299 RID: 33433
			// (set) Token: 0x0600B104 RID: 45316 RVA: 0x000FF684 File Offset: 0x000FD884
			public virtual string SecondaryDialPlan
			{
				set
				{
					base.PowerSharpParameters["SecondaryDialPlan"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x1700829A RID: 33434
			// (set) Token: 0x0600B105 RID: 45317 RVA: 0x000FF6A2 File Offset: 0x000FD8A2
			public virtual SwitchParameter RemovePicture
			{
				set
				{
					base.PowerSharpParameters["RemovePicture"] = value;
				}
			}

			// Token: 0x1700829B RID: 33435
			// (set) Token: 0x0600B106 RID: 45318 RVA: 0x000FF6BA File Offset: 0x000FD8BA
			public virtual SwitchParameter RemoveSpokenName
			{
				set
				{
					base.PowerSharpParameters["RemoveSpokenName"] = value;
				}
			}

			// Token: 0x1700829C RID: 33436
			// (set) Token: 0x0600B107 RID: 45319 RVA: 0x000FF6D2 File Offset: 0x000FD8D2
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x1700829D RID: 33437
			// (set) Token: 0x0600B108 RID: 45320 RVA: 0x000FF6E5 File Offset: 0x000FD8E5
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x1700829E RID: 33438
			// (set) Token: 0x0600B109 RID: 45321 RVA: 0x000FF6F8 File Offset: 0x000FD8F8
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x1700829F RID: 33439
			// (set) Token: 0x0600B10A RID: 45322 RVA: 0x000FF70B File Offset: 0x000FD90B
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170082A0 RID: 33440
			// (set) Token: 0x0600B10B RID: 45323 RVA: 0x000FF729 File Offset: 0x000FD929
			public virtual MultiValuedProperty<RecipientIdParameter> GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x170082A1 RID: 33441
			// (set) Token: 0x0600B10C RID: 45324 RVA: 0x000FF73C File Offset: 0x000FD93C
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x170082A2 RID: 33442
			// (set) Token: 0x0600B10D RID: 45325 RVA: 0x000FF74F File Offset: 0x000FD94F
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x170082A3 RID: 33443
			// (set) Token: 0x0600B10E RID: 45326 RVA: 0x000FF762 File Offset: 0x000FD962
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x170082A4 RID: 33444
			// (set) Token: 0x0600B10F RID: 45327 RVA: 0x000FF775 File Offset: 0x000FD975
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x170082A5 RID: 33445
			// (set) Token: 0x0600B110 RID: 45328 RVA: 0x000FF788 File Offset: 0x000FD988
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x170082A6 RID: 33446
			// (set) Token: 0x0600B111 RID: 45329 RVA: 0x000FF79B File Offset: 0x000FD99B
			public virtual bool CreateDTMFMap
			{
				set
				{
					base.PowerSharpParameters["CreateDTMFMap"] = value;
				}
			}

			// Token: 0x170082A7 RID: 33447
			// (set) Token: 0x0600B112 RID: 45330 RVA: 0x000FF7B3 File Offset: 0x000FD9B3
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170082A8 RID: 33448
			// (set) Token: 0x0600B113 RID: 45331 RVA: 0x000FF7CB File Offset: 0x000FD9CB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170082A9 RID: 33449
			// (set) Token: 0x0600B114 RID: 45332 RVA: 0x000FF7DE File Offset: 0x000FD9DE
			public virtual Guid ExchangeGuid
			{
				set
				{
					base.PowerSharpParameters["ExchangeGuid"] = value;
				}
			}

			// Token: 0x170082AA RID: 33450
			// (set) Token: 0x0600B115 RID: 45333 RVA: 0x000FF7F6 File Offset: 0x000FD9F6
			public virtual Guid? MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x170082AB RID: 33451
			// (set) Token: 0x0600B116 RID: 45334 RVA: 0x000FF80E File Offset: 0x000FDA0E
			public virtual MultiValuedProperty<Guid> AggregatedMailboxGuids
			{
				set
				{
					base.PowerSharpParameters["AggregatedMailboxGuids"] = value;
				}
			}

			// Token: 0x170082AC RID: 33452
			// (set) Token: 0x0600B117 RID: 45335 RVA: 0x000FF821 File Offset: 0x000FDA21
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x170082AD RID: 33453
			// (set) Token: 0x0600B118 RID: 45336 RVA: 0x000FF839 File Offset: 0x000FDA39
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x170082AE RID: 33454
			// (set) Token: 0x0600B119 RID: 45337 RVA: 0x000FF84C File Offset: 0x000FDA4C
			public virtual Unlimited<ByteQuantifiedSize> ArchiveQuota
			{
				set
				{
					base.PowerSharpParameters["ArchiveQuota"] = value;
				}
			}

			// Token: 0x170082AF RID: 33455
			// (set) Token: 0x0600B11A RID: 45338 RVA: 0x000FF864 File Offset: 0x000FDA64
			public virtual Unlimited<ByteQuantifiedSize> ArchiveWarningQuota
			{
				set
				{
					base.PowerSharpParameters["ArchiveWarningQuota"] = value;
				}
			}

			// Token: 0x170082B0 RID: 33456
			// (set) Token: 0x0600B11B RID: 45339 RVA: 0x000FF87C File Offset: 0x000FDA7C
			public virtual ProxyAddress ExternalEmailAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalEmailAddress"] = value;
				}
			}

			// Token: 0x170082B1 RID: 33457
			// (set) Token: 0x0600B11C RID: 45340 RVA: 0x000FF88F File Offset: 0x000FDA8F
			public virtual bool UsePreferMessageFormat
			{
				set
				{
					base.PowerSharpParameters["UsePreferMessageFormat"] = value;
				}
			}

			// Token: 0x170082B2 RID: 33458
			// (set) Token: 0x0600B11D RID: 45341 RVA: 0x000FF8A7 File Offset: 0x000FDAA7
			public virtual SmtpAddress JournalArchiveAddress
			{
				set
				{
					base.PowerSharpParameters["JournalArchiveAddress"] = value;
				}
			}

			// Token: 0x170082B3 RID: 33459
			// (set) Token: 0x0600B11E RID: 45342 RVA: 0x000FF8BF File Offset: 0x000FDABF
			public virtual MessageFormat MessageFormat
			{
				set
				{
					base.PowerSharpParameters["MessageFormat"] = value;
				}
			}

			// Token: 0x170082B4 RID: 33460
			// (set) Token: 0x0600B11F RID: 45343 RVA: 0x000FF8D7 File Offset: 0x000FDAD7
			public virtual MessageBodyFormat MessageBodyFormat
			{
				set
				{
					base.PowerSharpParameters["MessageBodyFormat"] = value;
				}
			}

			// Token: 0x170082B5 RID: 33461
			// (set) Token: 0x0600B120 RID: 45344 RVA: 0x000FF8EF File Offset: 0x000FDAEF
			public virtual MacAttachmentFormat MacAttachmentFormat
			{
				set
				{
					base.PowerSharpParameters["MacAttachmentFormat"] = value;
				}
			}

			// Token: 0x170082B6 RID: 33462
			// (set) Token: 0x0600B121 RID: 45345 RVA: 0x000FF907 File Offset: 0x000FDB07
			public virtual Unlimited<int> RecipientLimits
			{
				set
				{
					base.PowerSharpParameters["RecipientLimits"] = value;
				}
			}

			// Token: 0x170082B7 RID: 33463
			// (set) Token: 0x0600B122 RID: 45346 RVA: 0x000FF91F File Offset: 0x000FDB1F
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x170082B8 RID: 33464
			// (set) Token: 0x0600B123 RID: 45347 RVA: 0x000FF932 File Offset: 0x000FDB32
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x170082B9 RID: 33465
			// (set) Token: 0x0600B124 RID: 45348 RVA: 0x000FF94A File Offset: 0x000FDB4A
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x170082BA RID: 33466
			// (set) Token: 0x0600B125 RID: 45349 RVA: 0x000FF95D File Offset: 0x000FDB5D
			public virtual SmtpAddress WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x170082BB RID: 33467
			// (set) Token: 0x0600B126 RID: 45350 RVA: 0x000FF975 File Offset: 0x000FDB75
			public virtual SmtpAddress MicrosoftOnlineServicesID
			{
				set
				{
					base.PowerSharpParameters["MicrosoftOnlineServicesID"] = value;
				}
			}

			// Token: 0x170082BC RID: 33468
			// (set) Token: 0x0600B127 RID: 45351 RVA: 0x000FF98D File Offset: 0x000FDB8D
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x170082BD RID: 33469
			// (set) Token: 0x0600B128 RID: 45352 RVA: 0x000FF9A0 File Offset: 0x000FDBA0
			public virtual bool? SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x170082BE RID: 33470
			// (set) Token: 0x0600B129 RID: 45353 RVA: 0x000FF9B8 File Offset: 0x000FDBB8
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x170082BF RID: 33471
			// (set) Token: 0x0600B12A RID: 45354 RVA: 0x000FF9D0 File Offset: 0x000FDBD0
			public virtual bool LitigationHoldEnabled
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldEnabled"] = value;
				}
			}

			// Token: 0x170082C0 RID: 33472
			// (set) Token: 0x0600B12B RID: 45355 RVA: 0x000FF9E8 File Offset: 0x000FDBE8
			public virtual bool SingleItemRecoveryEnabled
			{
				set
				{
					base.PowerSharpParameters["SingleItemRecoveryEnabled"] = value;
				}
			}

			// Token: 0x170082C1 RID: 33473
			// (set) Token: 0x0600B12C RID: 45356 RVA: 0x000FFA00 File Offset: 0x000FDC00
			public virtual bool RetentionHoldEnabled
			{
				set
				{
					base.PowerSharpParameters["RetentionHoldEnabled"] = value;
				}
			}

			// Token: 0x170082C2 RID: 33474
			// (set) Token: 0x0600B12D RID: 45357 RVA: 0x000FFA18 File Offset: 0x000FDC18
			public virtual DateTime? EndDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["EndDateForRetentionHold"] = value;
				}
			}

			// Token: 0x170082C3 RID: 33475
			// (set) Token: 0x0600B12E RID: 45358 RVA: 0x000FFA30 File Offset: 0x000FDC30
			public virtual DateTime? StartDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["StartDateForRetentionHold"] = value;
				}
			}

			// Token: 0x170082C4 RID: 33476
			// (set) Token: 0x0600B12F RID: 45359 RVA: 0x000FFA48 File Offset: 0x000FDC48
			public virtual string RetentionComment
			{
				set
				{
					base.PowerSharpParameters["RetentionComment"] = value;
				}
			}

			// Token: 0x170082C5 RID: 33477
			// (set) Token: 0x0600B130 RID: 45360 RVA: 0x000FFA5B File Offset: 0x000FDC5B
			public virtual string RetentionUrl
			{
				set
				{
					base.PowerSharpParameters["RetentionUrl"] = value;
				}
			}

			// Token: 0x170082C6 RID: 33478
			// (set) Token: 0x0600B131 RID: 45361 RVA: 0x000FFA6E File Offset: 0x000FDC6E
			public virtual DateTime? LitigationHoldDate
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldDate"] = value;
				}
			}

			// Token: 0x170082C7 RID: 33479
			// (set) Token: 0x0600B132 RID: 45362 RVA: 0x000FFA86 File Offset: 0x000FDC86
			public virtual string LitigationHoldOwner
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldOwner"] = value;
				}
			}

			// Token: 0x170082C8 RID: 33480
			// (set) Token: 0x0600B133 RID: 45363 RVA: 0x000FFA99 File Offset: 0x000FDC99
			public virtual EnhancedTimeSpan RetainDeletedItemsFor
			{
				set
				{
					base.PowerSharpParameters["RetainDeletedItemsFor"] = value;
				}
			}

			// Token: 0x170082C9 RID: 33481
			// (set) Token: 0x0600B134 RID: 45364 RVA: 0x000FFAB1 File Offset: 0x000FDCB1
			public virtual bool CalendarVersionStoreDisabled
			{
				set
				{
					base.PowerSharpParameters["CalendarVersionStoreDisabled"] = value;
				}
			}

			// Token: 0x170082CA RID: 33482
			// (set) Token: 0x0600B135 RID: 45365 RVA: 0x000FFAC9 File Offset: 0x000FDCC9
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x170082CB RID: 33483
			// (set) Token: 0x0600B136 RID: 45366 RVA: 0x000FFADC File Offset: 0x000FDCDC
			public virtual Unlimited<ByteQuantifiedSize> RecoverableItemsQuota
			{
				set
				{
					base.PowerSharpParameters["RecoverableItemsQuota"] = value;
				}
			}

			// Token: 0x170082CC RID: 33484
			// (set) Token: 0x0600B137 RID: 45367 RVA: 0x000FFAF4 File Offset: 0x000FDCF4
			public virtual Unlimited<ByteQuantifiedSize> RecoverableItemsWarningQuota
			{
				set
				{
					base.PowerSharpParameters["RecoverableItemsWarningQuota"] = value;
				}
			}

			// Token: 0x170082CD RID: 33485
			// (set) Token: 0x0600B138 RID: 45368 RVA: 0x000FFB0C File Offset: 0x000FDD0C
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x170082CE RID: 33486
			// (set) Token: 0x0600B139 RID: 45369 RVA: 0x000FFB1F File Offset: 0x000FDD1F
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x170082CF RID: 33487
			// (set) Token: 0x0600B13A RID: 45370 RVA: 0x000FFB32 File Offset: 0x000FDD32
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x170082D0 RID: 33488
			// (set) Token: 0x0600B13B RID: 45371 RVA: 0x000FFB45 File Offset: 0x000FDD45
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x170082D1 RID: 33489
			// (set) Token: 0x0600B13C RID: 45372 RVA: 0x000FFB58 File Offset: 0x000FDD58
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x170082D2 RID: 33490
			// (set) Token: 0x0600B13D RID: 45373 RVA: 0x000FFB6B File Offset: 0x000FDD6B
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x170082D3 RID: 33491
			// (set) Token: 0x0600B13E RID: 45374 RVA: 0x000FFB7E File Offset: 0x000FDD7E
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x170082D4 RID: 33492
			// (set) Token: 0x0600B13F RID: 45375 RVA: 0x000FFB91 File Offset: 0x000FDD91
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x170082D5 RID: 33493
			// (set) Token: 0x0600B140 RID: 45376 RVA: 0x000FFBA4 File Offset: 0x000FDDA4
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x170082D6 RID: 33494
			// (set) Token: 0x0600B141 RID: 45377 RVA: 0x000FFBB7 File Offset: 0x000FDDB7
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x170082D7 RID: 33495
			// (set) Token: 0x0600B142 RID: 45378 RVA: 0x000FFBCA File Offset: 0x000FDDCA
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x170082D8 RID: 33496
			// (set) Token: 0x0600B143 RID: 45379 RVA: 0x000FFBDD File Offset: 0x000FDDDD
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x170082D9 RID: 33497
			// (set) Token: 0x0600B144 RID: 45380 RVA: 0x000FFBF0 File Offset: 0x000FDDF0
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x170082DA RID: 33498
			// (set) Token: 0x0600B145 RID: 45381 RVA: 0x000FFC03 File Offset: 0x000FDE03
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x170082DB RID: 33499
			// (set) Token: 0x0600B146 RID: 45382 RVA: 0x000FFC16 File Offset: 0x000FDE16
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x170082DC RID: 33500
			// (set) Token: 0x0600B147 RID: 45383 RVA: 0x000FFC29 File Offset: 0x000FDE29
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x170082DD RID: 33501
			// (set) Token: 0x0600B148 RID: 45384 RVA: 0x000FFC3C File Offset: 0x000FDE3C
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x170082DE RID: 33502
			// (set) Token: 0x0600B149 RID: 45385 RVA: 0x000FFC4F File Offset: 0x000FDE4F
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x170082DF RID: 33503
			// (set) Token: 0x0600B14A RID: 45386 RVA: 0x000FFC62 File Offset: 0x000FDE62
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x170082E0 RID: 33504
			// (set) Token: 0x0600B14B RID: 45387 RVA: 0x000FFC75 File Offset: 0x000FDE75
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x170082E1 RID: 33505
			// (set) Token: 0x0600B14C RID: 45388 RVA: 0x000FFC88 File Offset: 0x000FDE88
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x170082E2 RID: 33506
			// (set) Token: 0x0600B14D RID: 45389 RVA: 0x000FFC9B File Offset: 0x000FDE9B
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x170082E3 RID: 33507
			// (set) Token: 0x0600B14E RID: 45390 RVA: 0x000FFCAE File Offset: 0x000FDEAE
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x170082E4 RID: 33508
			// (set) Token: 0x0600B14F RID: 45391 RVA: 0x000FFCC1 File Offset: 0x000FDEC1
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x170082E5 RID: 33509
			// (set) Token: 0x0600B150 RID: 45392 RVA: 0x000FFCD4 File Offset: 0x000FDED4
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x170082E6 RID: 33510
			// (set) Token: 0x0600B151 RID: 45393 RVA: 0x000FFCE7 File Offset: 0x000FDEE7
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x170082E7 RID: 33511
			// (set) Token: 0x0600B152 RID: 45394 RVA: 0x000FFCFF File Offset: 0x000FDEFF
			public virtual Unlimited<ByteQuantifiedSize> MaxSendSize
			{
				set
				{
					base.PowerSharpParameters["MaxSendSize"] = value;
				}
			}

			// Token: 0x170082E8 RID: 33512
			// (set) Token: 0x0600B153 RID: 45395 RVA: 0x000FFD17 File Offset: 0x000FDF17
			public virtual Unlimited<ByteQuantifiedSize> MaxReceiveSize
			{
				set
				{
					base.PowerSharpParameters["MaxReceiveSize"] = value;
				}
			}

			// Token: 0x170082E9 RID: 33513
			// (set) Token: 0x0600B154 RID: 45396 RVA: 0x000FFD2F File Offset: 0x000FDF2F
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x170082EA RID: 33514
			// (set) Token: 0x0600B155 RID: 45397 RVA: 0x000FFD47 File Offset: 0x000FDF47
			public virtual bool EmailAddressPolicyEnabled
			{
				set
				{
					base.PowerSharpParameters["EmailAddressPolicyEnabled"] = value;
				}
			}

			// Token: 0x170082EB RID: 33515
			// (set) Token: 0x0600B156 RID: 45398 RVA: 0x000FFD5F File Offset: 0x000FDF5F
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x170082EC RID: 33516
			// (set) Token: 0x0600B157 RID: 45399 RVA: 0x000FFD77 File Offset: 0x000FDF77
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x170082ED RID: 33517
			// (set) Token: 0x0600B158 RID: 45400 RVA: 0x000FFD8F File Offset: 0x000FDF8F
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x170082EE RID: 33518
			// (set) Token: 0x0600B159 RID: 45401 RVA: 0x000FFDA2 File Offset: 0x000FDFA2
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x170082EF RID: 33519
			// (set) Token: 0x0600B15A RID: 45402 RVA: 0x000FFDBA File Offset: 0x000FDFBA
			public virtual MultiValuedProperty<string> UMDtmfMap
			{
				set
				{
					base.PowerSharpParameters["UMDtmfMap"] = value;
				}
			}

			// Token: 0x170082F0 RID: 33520
			// (set) Token: 0x0600B15B RID: 45403 RVA: 0x000FFDCD File Offset: 0x000FDFCD
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x170082F1 RID: 33521
			// (set) Token: 0x0600B15C RID: 45404 RVA: 0x000FFDE5 File Offset: 0x000FDFE5
			public virtual string MailTip
			{
				set
				{
					base.PowerSharpParameters["MailTip"] = value;
				}
			}

			// Token: 0x170082F2 RID: 33522
			// (set) Token: 0x0600B15D RID: 45405 RVA: 0x000FFDF8 File Offset: 0x000FDFF8
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x170082F3 RID: 33523
			// (set) Token: 0x0600B15E RID: 45406 RVA: 0x000FFE0B File Offset: 0x000FE00B
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170082F4 RID: 33524
			// (set) Token: 0x0600B15F RID: 45407 RVA: 0x000FFE1E File Offset: 0x000FE01E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170082F5 RID: 33525
			// (set) Token: 0x0600B160 RID: 45408 RVA: 0x000FFE36 File Offset: 0x000FE036
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170082F6 RID: 33526
			// (set) Token: 0x0600B161 RID: 45409 RVA: 0x000FFE4E File Offset: 0x000FE04E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170082F7 RID: 33527
			// (set) Token: 0x0600B162 RID: 45410 RVA: 0x000FFE66 File Offset: 0x000FE066
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170082F8 RID: 33528
			// (set) Token: 0x0600B163 RID: 45411 RVA: 0x000FFE7E File Offset: 0x000FE07E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D12 RID: 3346
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170082F9 RID: 33529
			// (set) Token: 0x0600B165 RID: 45413 RVA: 0x000FFE9E File Offset: 0x000FE09E
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailUserIdParameter(value) : null);
				}
			}

			// Token: 0x170082FA RID: 33530
			// (set) Token: 0x0600B166 RID: 45414 RVA: 0x000FFEBC File Offset: 0x000FE0BC
			public virtual SwitchParameter ForceUpgrade
			{
				set
				{
					base.PowerSharpParameters["ForceUpgrade"] = value;
				}
			}

			// Token: 0x170082FB RID: 33531
			// (set) Token: 0x0600B167 RID: 45415 RVA: 0x000FFED4 File Offset: 0x000FE0D4
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x170082FC RID: 33532
			// (set) Token: 0x0600B168 RID: 45416 RVA: 0x000FFEE7 File Offset: 0x000FE0E7
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x170082FD RID: 33533
			// (set) Token: 0x0600B169 RID: 45417 RVA: 0x000FFEFA File Offset: 0x000FE0FA
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x170082FE RID: 33534
			// (set) Token: 0x0600B16A RID: 45418 RVA: 0x000FFF12 File Offset: 0x000FE112
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x170082FF RID: 33535
			// (set) Token: 0x0600B16B RID: 45419 RVA: 0x000FFF25 File Offset: 0x000FE125
			public virtual SwitchParameter BypassLiveId
			{
				set
				{
					base.PowerSharpParameters["BypassLiveId"] = value;
				}
			}

			// Token: 0x17008300 RID: 33536
			// (set) Token: 0x0600B16C RID: 45420 RVA: 0x000FFF3D File Offset: 0x000FE13D
			public virtual NetID NetID
			{
				set
				{
					base.PowerSharpParameters["NetID"] = value;
				}
			}

			// Token: 0x17008301 RID: 33537
			// (set) Token: 0x0600B16D RID: 45421 RVA: 0x000FFF50 File Offset: 0x000FE150
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17008302 RID: 33538
			// (set) Token: 0x0600B16E RID: 45422 RVA: 0x000FFF63 File Offset: 0x000FE163
			public virtual string FederatedIdentity
			{
				set
				{
					base.PowerSharpParameters["FederatedIdentity"] = value;
				}
			}

			// Token: 0x17008303 RID: 33539
			// (set) Token: 0x0600B16F RID: 45423 RVA: 0x000FFF76 File Offset: 0x000FE176
			public virtual string SecondaryAddress
			{
				set
				{
					base.PowerSharpParameters["SecondaryAddress"] = value;
				}
			}

			// Token: 0x17008304 RID: 33540
			// (set) Token: 0x0600B170 RID: 45424 RVA: 0x000FFF89 File Offset: 0x000FE189
			public virtual string SecondaryDialPlan
			{
				set
				{
					base.PowerSharpParameters["SecondaryDialPlan"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17008305 RID: 33541
			// (set) Token: 0x0600B171 RID: 45425 RVA: 0x000FFFA7 File Offset: 0x000FE1A7
			public virtual SwitchParameter RemovePicture
			{
				set
				{
					base.PowerSharpParameters["RemovePicture"] = value;
				}
			}

			// Token: 0x17008306 RID: 33542
			// (set) Token: 0x0600B172 RID: 45426 RVA: 0x000FFFBF File Offset: 0x000FE1BF
			public virtual SwitchParameter RemoveSpokenName
			{
				set
				{
					base.PowerSharpParameters["RemoveSpokenName"] = value;
				}
			}

			// Token: 0x17008307 RID: 33543
			// (set) Token: 0x0600B173 RID: 45427 RVA: 0x000FFFD7 File Offset: 0x000FE1D7
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17008308 RID: 33544
			// (set) Token: 0x0600B174 RID: 45428 RVA: 0x000FFFEA File Offset: 0x000FE1EA
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17008309 RID: 33545
			// (set) Token: 0x0600B175 RID: 45429 RVA: 0x000FFFFD File Offset: 0x000FE1FD
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x1700830A RID: 33546
			// (set) Token: 0x0600B176 RID: 45430 RVA: 0x00100010 File Offset: 0x000FE210
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700830B RID: 33547
			// (set) Token: 0x0600B177 RID: 45431 RVA: 0x0010002E File Offset: 0x000FE22E
			public virtual MultiValuedProperty<RecipientIdParameter> GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x1700830C RID: 33548
			// (set) Token: 0x0600B178 RID: 45432 RVA: 0x00100041 File Offset: 0x000FE241
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x1700830D RID: 33549
			// (set) Token: 0x0600B179 RID: 45433 RVA: 0x00100054 File Offset: 0x000FE254
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x1700830E RID: 33550
			// (set) Token: 0x0600B17A RID: 45434 RVA: 0x00100067 File Offset: 0x000FE267
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x1700830F RID: 33551
			// (set) Token: 0x0600B17B RID: 45435 RVA: 0x0010007A File Offset: 0x000FE27A
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x17008310 RID: 33552
			// (set) Token: 0x0600B17C RID: 45436 RVA: 0x0010008D File Offset: 0x000FE28D
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x17008311 RID: 33553
			// (set) Token: 0x0600B17D RID: 45437 RVA: 0x001000A0 File Offset: 0x000FE2A0
			public virtual bool CreateDTMFMap
			{
				set
				{
					base.PowerSharpParameters["CreateDTMFMap"] = value;
				}
			}

			// Token: 0x17008312 RID: 33554
			// (set) Token: 0x0600B17E RID: 45438 RVA: 0x001000B8 File Offset: 0x000FE2B8
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17008313 RID: 33555
			// (set) Token: 0x0600B17F RID: 45439 RVA: 0x001000D0 File Offset: 0x000FE2D0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008314 RID: 33556
			// (set) Token: 0x0600B180 RID: 45440 RVA: 0x001000E3 File Offset: 0x000FE2E3
			public virtual Guid ExchangeGuid
			{
				set
				{
					base.PowerSharpParameters["ExchangeGuid"] = value;
				}
			}

			// Token: 0x17008315 RID: 33557
			// (set) Token: 0x0600B181 RID: 45441 RVA: 0x001000FB File Offset: 0x000FE2FB
			public virtual Guid? MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x17008316 RID: 33558
			// (set) Token: 0x0600B182 RID: 45442 RVA: 0x00100113 File Offset: 0x000FE313
			public virtual MultiValuedProperty<Guid> AggregatedMailboxGuids
			{
				set
				{
					base.PowerSharpParameters["AggregatedMailboxGuids"] = value;
				}
			}

			// Token: 0x17008317 RID: 33559
			// (set) Token: 0x0600B183 RID: 45443 RVA: 0x00100126 File Offset: 0x000FE326
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x17008318 RID: 33560
			// (set) Token: 0x0600B184 RID: 45444 RVA: 0x0010013E File Offset: 0x000FE33E
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x17008319 RID: 33561
			// (set) Token: 0x0600B185 RID: 45445 RVA: 0x00100151 File Offset: 0x000FE351
			public virtual Unlimited<ByteQuantifiedSize> ArchiveQuota
			{
				set
				{
					base.PowerSharpParameters["ArchiveQuota"] = value;
				}
			}

			// Token: 0x1700831A RID: 33562
			// (set) Token: 0x0600B186 RID: 45446 RVA: 0x00100169 File Offset: 0x000FE369
			public virtual Unlimited<ByteQuantifiedSize> ArchiveWarningQuota
			{
				set
				{
					base.PowerSharpParameters["ArchiveWarningQuota"] = value;
				}
			}

			// Token: 0x1700831B RID: 33563
			// (set) Token: 0x0600B187 RID: 45447 RVA: 0x00100181 File Offset: 0x000FE381
			public virtual ProxyAddress ExternalEmailAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalEmailAddress"] = value;
				}
			}

			// Token: 0x1700831C RID: 33564
			// (set) Token: 0x0600B188 RID: 45448 RVA: 0x00100194 File Offset: 0x000FE394
			public virtual bool UsePreferMessageFormat
			{
				set
				{
					base.PowerSharpParameters["UsePreferMessageFormat"] = value;
				}
			}

			// Token: 0x1700831D RID: 33565
			// (set) Token: 0x0600B189 RID: 45449 RVA: 0x001001AC File Offset: 0x000FE3AC
			public virtual SmtpAddress JournalArchiveAddress
			{
				set
				{
					base.PowerSharpParameters["JournalArchiveAddress"] = value;
				}
			}

			// Token: 0x1700831E RID: 33566
			// (set) Token: 0x0600B18A RID: 45450 RVA: 0x001001C4 File Offset: 0x000FE3C4
			public virtual MessageFormat MessageFormat
			{
				set
				{
					base.PowerSharpParameters["MessageFormat"] = value;
				}
			}

			// Token: 0x1700831F RID: 33567
			// (set) Token: 0x0600B18B RID: 45451 RVA: 0x001001DC File Offset: 0x000FE3DC
			public virtual MessageBodyFormat MessageBodyFormat
			{
				set
				{
					base.PowerSharpParameters["MessageBodyFormat"] = value;
				}
			}

			// Token: 0x17008320 RID: 33568
			// (set) Token: 0x0600B18C RID: 45452 RVA: 0x001001F4 File Offset: 0x000FE3F4
			public virtual MacAttachmentFormat MacAttachmentFormat
			{
				set
				{
					base.PowerSharpParameters["MacAttachmentFormat"] = value;
				}
			}

			// Token: 0x17008321 RID: 33569
			// (set) Token: 0x0600B18D RID: 45453 RVA: 0x0010020C File Offset: 0x000FE40C
			public virtual Unlimited<int> RecipientLimits
			{
				set
				{
					base.PowerSharpParameters["RecipientLimits"] = value;
				}
			}

			// Token: 0x17008322 RID: 33570
			// (set) Token: 0x0600B18E RID: 45454 RVA: 0x00100224 File Offset: 0x000FE424
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17008323 RID: 33571
			// (set) Token: 0x0600B18F RID: 45455 RVA: 0x00100237 File Offset: 0x000FE437
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x17008324 RID: 33572
			// (set) Token: 0x0600B190 RID: 45456 RVA: 0x0010024F File Offset: 0x000FE44F
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17008325 RID: 33573
			// (set) Token: 0x0600B191 RID: 45457 RVA: 0x00100262 File Offset: 0x000FE462
			public virtual SmtpAddress WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x17008326 RID: 33574
			// (set) Token: 0x0600B192 RID: 45458 RVA: 0x0010027A File Offset: 0x000FE47A
			public virtual SmtpAddress MicrosoftOnlineServicesID
			{
				set
				{
					base.PowerSharpParameters["MicrosoftOnlineServicesID"] = value;
				}
			}

			// Token: 0x17008327 RID: 33575
			// (set) Token: 0x0600B193 RID: 45459 RVA: 0x00100292 File Offset: 0x000FE492
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17008328 RID: 33576
			// (set) Token: 0x0600B194 RID: 45460 RVA: 0x001002A5 File Offset: 0x000FE4A5
			public virtual bool? SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x17008329 RID: 33577
			// (set) Token: 0x0600B195 RID: 45461 RVA: 0x001002BD File Offset: 0x000FE4BD
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x1700832A RID: 33578
			// (set) Token: 0x0600B196 RID: 45462 RVA: 0x001002D5 File Offset: 0x000FE4D5
			public virtual bool LitigationHoldEnabled
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldEnabled"] = value;
				}
			}

			// Token: 0x1700832B RID: 33579
			// (set) Token: 0x0600B197 RID: 45463 RVA: 0x001002ED File Offset: 0x000FE4ED
			public virtual bool SingleItemRecoveryEnabled
			{
				set
				{
					base.PowerSharpParameters["SingleItemRecoveryEnabled"] = value;
				}
			}

			// Token: 0x1700832C RID: 33580
			// (set) Token: 0x0600B198 RID: 45464 RVA: 0x00100305 File Offset: 0x000FE505
			public virtual bool RetentionHoldEnabled
			{
				set
				{
					base.PowerSharpParameters["RetentionHoldEnabled"] = value;
				}
			}

			// Token: 0x1700832D RID: 33581
			// (set) Token: 0x0600B199 RID: 45465 RVA: 0x0010031D File Offset: 0x000FE51D
			public virtual DateTime? EndDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["EndDateForRetentionHold"] = value;
				}
			}

			// Token: 0x1700832E RID: 33582
			// (set) Token: 0x0600B19A RID: 45466 RVA: 0x00100335 File Offset: 0x000FE535
			public virtual DateTime? StartDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["StartDateForRetentionHold"] = value;
				}
			}

			// Token: 0x1700832F RID: 33583
			// (set) Token: 0x0600B19B RID: 45467 RVA: 0x0010034D File Offset: 0x000FE54D
			public virtual string RetentionComment
			{
				set
				{
					base.PowerSharpParameters["RetentionComment"] = value;
				}
			}

			// Token: 0x17008330 RID: 33584
			// (set) Token: 0x0600B19C RID: 45468 RVA: 0x00100360 File Offset: 0x000FE560
			public virtual string RetentionUrl
			{
				set
				{
					base.PowerSharpParameters["RetentionUrl"] = value;
				}
			}

			// Token: 0x17008331 RID: 33585
			// (set) Token: 0x0600B19D RID: 45469 RVA: 0x00100373 File Offset: 0x000FE573
			public virtual DateTime? LitigationHoldDate
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldDate"] = value;
				}
			}

			// Token: 0x17008332 RID: 33586
			// (set) Token: 0x0600B19E RID: 45470 RVA: 0x0010038B File Offset: 0x000FE58B
			public virtual string LitigationHoldOwner
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldOwner"] = value;
				}
			}

			// Token: 0x17008333 RID: 33587
			// (set) Token: 0x0600B19F RID: 45471 RVA: 0x0010039E File Offset: 0x000FE59E
			public virtual EnhancedTimeSpan RetainDeletedItemsFor
			{
				set
				{
					base.PowerSharpParameters["RetainDeletedItemsFor"] = value;
				}
			}

			// Token: 0x17008334 RID: 33588
			// (set) Token: 0x0600B1A0 RID: 45472 RVA: 0x001003B6 File Offset: 0x000FE5B6
			public virtual bool CalendarVersionStoreDisabled
			{
				set
				{
					base.PowerSharpParameters["CalendarVersionStoreDisabled"] = value;
				}
			}

			// Token: 0x17008335 RID: 33589
			// (set) Token: 0x0600B1A1 RID: 45473 RVA: 0x001003CE File Offset: 0x000FE5CE
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x17008336 RID: 33590
			// (set) Token: 0x0600B1A2 RID: 45474 RVA: 0x001003E1 File Offset: 0x000FE5E1
			public virtual Unlimited<ByteQuantifiedSize> RecoverableItemsQuota
			{
				set
				{
					base.PowerSharpParameters["RecoverableItemsQuota"] = value;
				}
			}

			// Token: 0x17008337 RID: 33591
			// (set) Token: 0x0600B1A3 RID: 45475 RVA: 0x001003F9 File Offset: 0x000FE5F9
			public virtual Unlimited<ByteQuantifiedSize> RecoverableItemsWarningQuota
			{
				set
				{
					base.PowerSharpParameters["RecoverableItemsWarningQuota"] = value;
				}
			}

			// Token: 0x17008338 RID: 33592
			// (set) Token: 0x0600B1A4 RID: 45476 RVA: 0x00100411 File Offset: 0x000FE611
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x17008339 RID: 33593
			// (set) Token: 0x0600B1A5 RID: 45477 RVA: 0x00100424 File Offset: 0x000FE624
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x1700833A RID: 33594
			// (set) Token: 0x0600B1A6 RID: 45478 RVA: 0x00100437 File Offset: 0x000FE637
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700833B RID: 33595
			// (set) Token: 0x0600B1A7 RID: 45479 RVA: 0x0010044A File Offset: 0x000FE64A
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x1700833C RID: 33596
			// (set) Token: 0x0600B1A8 RID: 45480 RVA: 0x0010045D File Offset: 0x000FE65D
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x1700833D RID: 33597
			// (set) Token: 0x0600B1A9 RID: 45481 RVA: 0x00100470 File Offset: 0x000FE670
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x1700833E RID: 33598
			// (set) Token: 0x0600B1AA RID: 45482 RVA: 0x00100483 File Offset: 0x000FE683
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x1700833F RID: 33599
			// (set) Token: 0x0600B1AB RID: 45483 RVA: 0x00100496 File Offset: 0x000FE696
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x17008340 RID: 33600
			// (set) Token: 0x0600B1AC RID: 45484 RVA: 0x001004A9 File Offset: 0x000FE6A9
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17008341 RID: 33601
			// (set) Token: 0x0600B1AD RID: 45485 RVA: 0x001004BC File Offset: 0x000FE6BC
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17008342 RID: 33602
			// (set) Token: 0x0600B1AE RID: 45486 RVA: 0x001004CF File Offset: 0x000FE6CF
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17008343 RID: 33603
			// (set) Token: 0x0600B1AF RID: 45487 RVA: 0x001004E2 File Offset: 0x000FE6E2
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17008344 RID: 33604
			// (set) Token: 0x0600B1B0 RID: 45488 RVA: 0x001004F5 File Offset: 0x000FE6F5
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x17008345 RID: 33605
			// (set) Token: 0x0600B1B1 RID: 45489 RVA: 0x00100508 File Offset: 0x000FE708
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17008346 RID: 33606
			// (set) Token: 0x0600B1B2 RID: 45490 RVA: 0x0010051B File Offset: 0x000FE71B
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17008347 RID: 33607
			// (set) Token: 0x0600B1B3 RID: 45491 RVA: 0x0010052E File Offset: 0x000FE72E
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17008348 RID: 33608
			// (set) Token: 0x0600B1B4 RID: 45492 RVA: 0x00100541 File Offset: 0x000FE741
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17008349 RID: 33609
			// (set) Token: 0x0600B1B5 RID: 45493 RVA: 0x00100554 File Offset: 0x000FE754
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x1700834A RID: 33610
			// (set) Token: 0x0600B1B6 RID: 45494 RVA: 0x00100567 File Offset: 0x000FE767
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x1700834B RID: 33611
			// (set) Token: 0x0600B1B7 RID: 45495 RVA: 0x0010057A File Offset: 0x000FE77A
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x1700834C RID: 33612
			// (set) Token: 0x0600B1B8 RID: 45496 RVA: 0x0010058D File Offset: 0x000FE78D
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x1700834D RID: 33613
			// (set) Token: 0x0600B1B9 RID: 45497 RVA: 0x001005A0 File Offset: 0x000FE7A0
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x1700834E RID: 33614
			// (set) Token: 0x0600B1BA RID: 45498 RVA: 0x001005B3 File Offset: 0x000FE7B3
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x1700834F RID: 33615
			// (set) Token: 0x0600B1BB RID: 45499 RVA: 0x001005C6 File Offset: 0x000FE7C6
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17008350 RID: 33616
			// (set) Token: 0x0600B1BC RID: 45500 RVA: 0x001005D9 File Offset: 0x000FE7D9
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17008351 RID: 33617
			// (set) Token: 0x0600B1BD RID: 45501 RVA: 0x001005EC File Offset: 0x000FE7EC
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17008352 RID: 33618
			// (set) Token: 0x0600B1BE RID: 45502 RVA: 0x00100604 File Offset: 0x000FE804
			public virtual Unlimited<ByteQuantifiedSize> MaxSendSize
			{
				set
				{
					base.PowerSharpParameters["MaxSendSize"] = value;
				}
			}

			// Token: 0x17008353 RID: 33619
			// (set) Token: 0x0600B1BF RID: 45503 RVA: 0x0010061C File Offset: 0x000FE81C
			public virtual Unlimited<ByteQuantifiedSize> MaxReceiveSize
			{
				set
				{
					base.PowerSharpParameters["MaxReceiveSize"] = value;
				}
			}

			// Token: 0x17008354 RID: 33620
			// (set) Token: 0x0600B1C0 RID: 45504 RVA: 0x00100634 File Offset: 0x000FE834
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17008355 RID: 33621
			// (set) Token: 0x0600B1C1 RID: 45505 RVA: 0x0010064C File Offset: 0x000FE84C
			public virtual bool EmailAddressPolicyEnabled
			{
				set
				{
					base.PowerSharpParameters["EmailAddressPolicyEnabled"] = value;
				}
			}

			// Token: 0x17008356 RID: 33622
			// (set) Token: 0x0600B1C2 RID: 45506 RVA: 0x00100664 File Offset: 0x000FE864
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17008357 RID: 33623
			// (set) Token: 0x0600B1C3 RID: 45507 RVA: 0x0010067C File Offset: 0x000FE87C
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17008358 RID: 33624
			// (set) Token: 0x0600B1C4 RID: 45508 RVA: 0x00100694 File Offset: 0x000FE894
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x17008359 RID: 33625
			// (set) Token: 0x0600B1C5 RID: 45509 RVA: 0x001006A7 File Offset: 0x000FE8A7
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x1700835A RID: 33626
			// (set) Token: 0x0600B1C6 RID: 45510 RVA: 0x001006BF File Offset: 0x000FE8BF
			public virtual MultiValuedProperty<string> UMDtmfMap
			{
				set
				{
					base.PowerSharpParameters["UMDtmfMap"] = value;
				}
			}

			// Token: 0x1700835B RID: 33627
			// (set) Token: 0x0600B1C7 RID: 45511 RVA: 0x001006D2 File Offset: 0x000FE8D2
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x1700835C RID: 33628
			// (set) Token: 0x0600B1C8 RID: 45512 RVA: 0x001006EA File Offset: 0x000FE8EA
			public virtual string MailTip
			{
				set
				{
					base.PowerSharpParameters["MailTip"] = value;
				}
			}

			// Token: 0x1700835D RID: 33629
			// (set) Token: 0x0600B1C9 RID: 45513 RVA: 0x001006FD File Offset: 0x000FE8FD
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x1700835E RID: 33630
			// (set) Token: 0x0600B1CA RID: 45514 RVA: 0x00100710 File Offset: 0x000FE910
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700835F RID: 33631
			// (set) Token: 0x0600B1CB RID: 45515 RVA: 0x00100723 File Offset: 0x000FE923
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008360 RID: 33632
			// (set) Token: 0x0600B1CC RID: 45516 RVA: 0x0010073B File Offset: 0x000FE93B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008361 RID: 33633
			// (set) Token: 0x0600B1CD RID: 45517 RVA: 0x00100753 File Offset: 0x000FE953
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008362 RID: 33634
			// (set) Token: 0x0600B1CE RID: 45518 RVA: 0x0010076B File Offset: 0x000FE96B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008363 RID: 33635
			// (set) Token: 0x0600B1CF RID: 45519 RVA: 0x00100783 File Offset: 0x000FE983
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
