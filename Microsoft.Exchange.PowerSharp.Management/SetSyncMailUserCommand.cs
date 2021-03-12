using System;
using System.Globalization;
using System.Management.Automation;
using System.Security;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000DCC RID: 3532
	public class SetSyncMailUserCommand : SyntheticCommandWithPipelineInputNoOutput<SyncMailUser>
	{
		// Token: 0x0600D155 RID: 53589 RVA: 0x00129FD7 File Offset: 0x001281D7
		private SetSyncMailUserCommand() : base("Set-SyncMailUser")
		{
		}

		// Token: 0x0600D156 RID: 53590 RVA: 0x00129FE4 File Offset: 0x001281E4
		public SetSyncMailUserCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D157 RID: 53591 RVA: 0x00129FF3 File Offset: 0x001281F3
		public virtual SetSyncMailUserCommand SetParameters(SetSyncMailUserCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D158 RID: 53592 RVA: 0x00129FFD File Offset: 0x001281FD
		public virtual SetSyncMailUserCommand SetParameters(SetSyncMailUserCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000DCD RID: 3533
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A176 RID: 41334
			// (set) Token: 0x0600D159 RID: 53593 RVA: 0x0012A007 File Offset: 0x00128207
			public virtual string ForwardingAddress
			{
				set
				{
					base.PowerSharpParameters["ForwardingAddress"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x1700A177 RID: 41335
			// (set) Token: 0x0600D15A RID: 53594 RVA: 0x0012A025 File Offset: 0x00128225
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x1700A178 RID: 41336
			// (set) Token: 0x0600D15B RID: 53595 RVA: 0x0012A043 File Offset: 0x00128243
			public virtual string IntendedMailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["IntendedMailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x1700A179 RID: 41337
			// (set) Token: 0x0600D15C RID: 53596 RVA: 0x0012A061 File Offset: 0x00128261
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x1700A17A RID: 41338
			// (set) Token: 0x0600D15D RID: 53597 RVA: 0x0012A074 File Offset: 0x00128274
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x1700A17B RID: 41339
			// (set) Token: 0x0600D15E RID: 53598 RVA: 0x0012A087 File Offset: 0x00128287
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>> RawAcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["RawAcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x1700A17C RID: 41340
			// (set) Token: 0x0600D15F RID: 53599 RVA: 0x0012A09A File Offset: 0x0012829A
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>> RawBypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["RawBypassModerationFrom"] = value;
				}
			}

			// Token: 0x1700A17D RID: 41341
			// (set) Token: 0x0600D160 RID: 53600 RVA: 0x0012A0AD File Offset: 0x001282AD
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>> RawRejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RawRejectMessagesFrom"] = value;
				}
			}

			// Token: 0x1700A17E RID: 41342
			// (set) Token: 0x0600D161 RID: 53601 RVA: 0x0012A0C0 File Offset: 0x001282C0
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<RecipientIdParameter>> RawGrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["RawGrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x1700A17F RID: 41343
			// (set) Token: 0x0600D162 RID: 53602 RVA: 0x0012A0D3 File Offset: 0x001282D3
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<ModeratorIDParameter>> RawModeratedBy
			{
				set
				{
					base.PowerSharpParameters["RawModeratedBy"] = value;
				}
			}

			// Token: 0x1700A180 RID: 41344
			// (set) Token: 0x0600D163 RID: 53603 RVA: 0x0012A0E6 File Offset: 0x001282E6
			public virtual RecipientWithAdUserIdParameter<RecipientIdParameter> RawForwardingAddress
			{
				set
				{
					base.PowerSharpParameters["RawForwardingAddress"] = value;
				}
			}

			// Token: 0x1700A181 RID: 41345
			// (set) Token: 0x0600D164 RID: 53604 RVA: 0x0012A0F9 File Offset: 0x001282F9
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x1700A182 RID: 41346
			// (set) Token: 0x0600D165 RID: 53605 RVA: 0x0012A10C File Offset: 0x0012830C
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x1700A183 RID: 41347
			// (set) Token: 0x0600D166 RID: 53606 RVA: 0x0012A11F File Offset: 0x0012831F
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x1700A184 RID: 41348
			// (set) Token: 0x0600D167 RID: 53607 RVA: 0x0012A137 File Offset: 0x00128337
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x1700A185 RID: 41349
			// (set) Token: 0x0600D168 RID: 53608 RVA: 0x0012A14A File Offset: 0x0012834A
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x1700A186 RID: 41350
			// (set) Token: 0x0600D169 RID: 53609 RVA: 0x0012A15D File Offset: 0x0012835D
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x1700A187 RID: 41351
			// (set) Token: 0x0600D16A RID: 53610 RVA: 0x0012A175 File Offset: 0x00128375
			public virtual SwitchParameter SoftDeletedMailUser
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedMailUser"] = value;
				}
			}

			// Token: 0x1700A188 RID: 41352
			// (set) Token: 0x0600D16B RID: 53611 RVA: 0x0012A18D File Offset: 0x0012838D
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<RecipientIdParameter>> RawSiteMailboxOwners
			{
				set
				{
					base.PowerSharpParameters["RawSiteMailboxOwners"] = value;
				}
			}

			// Token: 0x1700A189 RID: 41353
			// (set) Token: 0x0600D16C RID: 53612 RVA: 0x0012A1A0 File Offset: 0x001283A0
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<RecipientIdParameter>> RawSiteMailboxUsers
			{
				set
				{
					base.PowerSharpParameters["RawSiteMailboxUsers"] = value;
				}
			}

			// Token: 0x1700A18A RID: 41354
			// (set) Token: 0x0600D16D RID: 53613 RVA: 0x0012A1B3 File Offset: 0x001283B3
			public virtual MultiValuedProperty<RecipientIdParameter> SiteMailboxOwners
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxOwners"] = value;
				}
			}

			// Token: 0x1700A18B RID: 41355
			// (set) Token: 0x0600D16E RID: 53614 RVA: 0x0012A1C6 File Offset: 0x001283C6
			public virtual MultiValuedProperty<RecipientIdParameter> SiteMailboxUsers
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxUsers"] = value;
				}
			}

			// Token: 0x1700A18C RID: 41356
			// (set) Token: 0x0600D16F RID: 53615 RVA: 0x0012A1D9 File Offset: 0x001283D9
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x1700A18D RID: 41357
			// (set) Token: 0x0600D170 RID: 53616 RVA: 0x0012A1F1 File Offset: 0x001283F1
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x1700A18E RID: 41358
			// (set) Token: 0x0600D171 RID: 53617 RVA: 0x0012A204 File Offset: 0x00128404
			public virtual SwitchParameter BypassLiveId
			{
				set
				{
					base.PowerSharpParameters["BypassLiveId"] = value;
				}
			}

			// Token: 0x1700A18F RID: 41359
			// (set) Token: 0x0600D172 RID: 53618 RVA: 0x0012A21C File Offset: 0x0012841C
			public virtual NetID NetID
			{
				set
				{
					base.PowerSharpParameters["NetID"] = value;
				}
			}

			// Token: 0x1700A190 RID: 41360
			// (set) Token: 0x0600D173 RID: 53619 RVA: 0x0012A22F File Offset: 0x0012842F
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x1700A191 RID: 41361
			// (set) Token: 0x0600D174 RID: 53620 RVA: 0x0012A242 File Offset: 0x00128442
			public virtual string FederatedIdentity
			{
				set
				{
					base.PowerSharpParameters["FederatedIdentity"] = value;
				}
			}

			// Token: 0x1700A192 RID: 41362
			// (set) Token: 0x0600D175 RID: 53621 RVA: 0x0012A255 File Offset: 0x00128455
			public virtual string SecondaryAddress
			{
				set
				{
					base.PowerSharpParameters["SecondaryAddress"] = value;
				}
			}

			// Token: 0x1700A193 RID: 41363
			// (set) Token: 0x0600D176 RID: 53622 RVA: 0x0012A268 File Offset: 0x00128468
			public virtual string SecondaryDialPlan
			{
				set
				{
					base.PowerSharpParameters["SecondaryDialPlan"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x1700A194 RID: 41364
			// (set) Token: 0x0600D177 RID: 53623 RVA: 0x0012A286 File Offset: 0x00128486
			public virtual SwitchParameter RemovePicture
			{
				set
				{
					base.PowerSharpParameters["RemovePicture"] = value;
				}
			}

			// Token: 0x1700A195 RID: 41365
			// (set) Token: 0x0600D178 RID: 53624 RVA: 0x0012A29E File Offset: 0x0012849E
			public virtual SwitchParameter RemoveSpokenName
			{
				set
				{
					base.PowerSharpParameters["RemoveSpokenName"] = value;
				}
			}

			// Token: 0x1700A196 RID: 41366
			// (set) Token: 0x0600D179 RID: 53625 RVA: 0x0012A2B6 File Offset: 0x001284B6
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x1700A197 RID: 41367
			// (set) Token: 0x0600D17A RID: 53626 RVA: 0x0012A2C9 File Offset: 0x001284C9
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x1700A198 RID: 41368
			// (set) Token: 0x0600D17B RID: 53627 RVA: 0x0012A2DC File Offset: 0x001284DC
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x1700A199 RID: 41369
			// (set) Token: 0x0600D17C RID: 53628 RVA: 0x0012A2EF File Offset: 0x001284EF
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A19A RID: 41370
			// (set) Token: 0x0600D17D RID: 53629 RVA: 0x0012A30D File Offset: 0x0012850D
			public virtual MultiValuedProperty<RecipientIdParameter> GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x1700A19B RID: 41371
			// (set) Token: 0x0600D17E RID: 53630 RVA: 0x0012A320 File Offset: 0x00128520
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x1700A19C RID: 41372
			// (set) Token: 0x0600D17F RID: 53631 RVA: 0x0012A333 File Offset: 0x00128533
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x1700A19D RID: 41373
			// (set) Token: 0x0600D180 RID: 53632 RVA: 0x0012A346 File Offset: 0x00128546
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x1700A19E RID: 41374
			// (set) Token: 0x0600D181 RID: 53633 RVA: 0x0012A359 File Offset: 0x00128559
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x1700A19F RID: 41375
			// (set) Token: 0x0600D182 RID: 53634 RVA: 0x0012A36C File Offset: 0x0012856C
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x1700A1A0 RID: 41376
			// (set) Token: 0x0600D183 RID: 53635 RVA: 0x0012A37F File Offset: 0x0012857F
			public virtual bool CreateDTMFMap
			{
				set
				{
					base.PowerSharpParameters["CreateDTMFMap"] = value;
				}
			}

			// Token: 0x1700A1A1 RID: 41377
			// (set) Token: 0x0600D184 RID: 53636 RVA: 0x0012A397 File Offset: 0x00128597
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700A1A2 RID: 41378
			// (set) Token: 0x0600D185 RID: 53637 RVA: 0x0012A3AF File Offset: 0x001285AF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A1A3 RID: 41379
			// (set) Token: 0x0600D186 RID: 53638 RVA: 0x0012A3C2 File Offset: 0x001285C2
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x1700A1A4 RID: 41380
			// (set) Token: 0x0600D187 RID: 53639 RVA: 0x0012A3D5 File Offset: 0x001285D5
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x1700A1A5 RID: 41381
			// (set) Token: 0x0600D188 RID: 53640 RVA: 0x0012A3ED File Offset: 0x001285ED
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x1700A1A6 RID: 41382
			// (set) Token: 0x0600D189 RID: 53641 RVA: 0x0012A400 File Offset: 0x00128600
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x1700A1A7 RID: 41383
			// (set) Token: 0x0600D18A RID: 53642 RVA: 0x0012A418 File Offset: 0x00128618
			public virtual SecurityIdentifier MasterAccountSid
			{
				set
				{
					base.PowerSharpParameters["MasterAccountSid"] = value;
				}
			}

			// Token: 0x1700A1A8 RID: 41384
			// (set) Token: 0x0600D18B RID: 53643 RVA: 0x0012A42B File Offset: 0x0012862B
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x1700A1A9 RID: 41385
			// (set) Token: 0x0600D18C RID: 53644 RVA: 0x0012A43E File Offset: 0x0012863E
			public virtual RecipientDisplayType? RecipientDisplayType
			{
				set
				{
					base.PowerSharpParameters["RecipientDisplayType"] = value;
				}
			}

			// Token: 0x1700A1AA RID: 41386
			// (set) Token: 0x0600D18D RID: 53645 RVA: 0x0012A456 File Offset: 0x00128656
			public virtual ExchangeResourceType? ResourceType
			{
				set
				{
					base.PowerSharpParameters["ResourceType"] = value;
				}
			}

			// Token: 0x1700A1AB RID: 41387
			// (set) Token: 0x0600D18E RID: 53646 RVA: 0x0012A46E File Offset: 0x0012866E
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x1700A1AC RID: 41388
			// (set) Token: 0x0600D18F RID: 53647 RVA: 0x0012A486 File Offset: 0x00128686
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x1700A1AD RID: 41389
			// (set) Token: 0x0600D190 RID: 53648 RVA: 0x0012A49E File Offset: 0x0012869E
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x1700A1AE RID: 41390
			// (set) Token: 0x0600D191 RID: 53649 RVA: 0x0012A4B6 File Offset: 0x001286B6
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x1700A1AF RID: 41391
			// (set) Token: 0x0600D192 RID: 53650 RVA: 0x0012A4C9 File Offset: 0x001286C9
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x1700A1B0 RID: 41392
			// (set) Token: 0x0600D193 RID: 53651 RVA: 0x0012A4DC File Offset: 0x001286DC
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x1700A1B1 RID: 41393
			// (set) Token: 0x0600D194 RID: 53652 RVA: 0x0012A4EF File Offset: 0x001286EF
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x1700A1B2 RID: 41394
			// (set) Token: 0x0600D195 RID: 53653 RVA: 0x0012A502 File Offset: 0x00128702
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x1700A1B3 RID: 41395
			// (set) Token: 0x0600D196 RID: 53654 RVA: 0x0012A51A File Offset: 0x0012871A
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x1700A1B4 RID: 41396
			// (set) Token: 0x0600D197 RID: 53655 RVA: 0x0012A52D File Offset: 0x0012872D
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x1700A1B5 RID: 41397
			// (set) Token: 0x0600D198 RID: 53656 RVA: 0x0012A540 File Offset: 0x00128740
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x1700A1B6 RID: 41398
			// (set) Token: 0x0600D199 RID: 53657 RVA: 0x0012A553 File Offset: 0x00128753
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x1700A1B7 RID: 41399
			// (set) Token: 0x0600D19A RID: 53658 RVA: 0x0012A566 File Offset: 0x00128766
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x1700A1B8 RID: 41400
			// (set) Token: 0x0600D19B RID: 53659 RVA: 0x0012A579 File Offset: 0x00128779
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x1700A1B9 RID: 41401
			// (set) Token: 0x0600D19C RID: 53660 RVA: 0x0012A58C File Offset: 0x0012878C
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x1700A1BA RID: 41402
			// (set) Token: 0x0600D19D RID: 53661 RVA: 0x0012A59F File Offset: 0x0012879F
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x1700A1BB RID: 41403
			// (set) Token: 0x0600D19E RID: 53662 RVA: 0x0012A5B2 File Offset: 0x001287B2
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x1700A1BC RID: 41404
			// (set) Token: 0x0600D19F RID: 53663 RVA: 0x0012A5C5 File Offset: 0x001287C5
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x1700A1BD RID: 41405
			// (set) Token: 0x0600D1A0 RID: 53664 RVA: 0x0012A5D8 File Offset: 0x001287D8
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x1700A1BE RID: 41406
			// (set) Token: 0x0600D1A1 RID: 53665 RVA: 0x0012A5EB File Offset: 0x001287EB
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x1700A1BF RID: 41407
			// (set) Token: 0x0600D1A2 RID: 53666 RVA: 0x0012A5FE File Offset: 0x001287FE
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x1700A1C0 RID: 41408
			// (set) Token: 0x0600D1A3 RID: 53667 RVA: 0x0012A611 File Offset: 0x00128811
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x1700A1C1 RID: 41409
			// (set) Token: 0x0600D1A4 RID: 53668 RVA: 0x0012A624 File Offset: 0x00128824
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x1700A1C2 RID: 41410
			// (set) Token: 0x0600D1A5 RID: 53669 RVA: 0x0012A63C File Offset: 0x0012883C
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x1700A1C3 RID: 41411
			// (set) Token: 0x0600D1A6 RID: 53670 RVA: 0x0012A64F File Offset: 0x0012884F
			public virtual MultiValuedProperty<string> ResourceMetaData
			{
				set
				{
					base.PowerSharpParameters["ResourceMetaData"] = value;
				}
			}

			// Token: 0x1700A1C4 RID: 41412
			// (set) Token: 0x0600D1A7 RID: 53671 RVA: 0x0012A662 File Offset: 0x00128862
			public virtual string ResourcePropertiesDisplay
			{
				set
				{
					base.PowerSharpParameters["ResourcePropertiesDisplay"] = value;
				}
			}

			// Token: 0x1700A1C5 RID: 41413
			// (set) Token: 0x0600D1A8 RID: 53672 RVA: 0x0012A675 File Offset: 0x00128875
			public virtual MultiValuedProperty<string> ResourceSearchProperties
			{
				set
				{
					base.PowerSharpParameters["ResourceSearchProperties"] = value;
				}
			}

			// Token: 0x1700A1C6 RID: 41414
			// (set) Token: 0x0600D1A9 RID: 53673 RVA: 0x0012A688 File Offset: 0x00128888
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x1700A1C7 RID: 41415
			// (set) Token: 0x0600D1AA RID: 53674 RVA: 0x0012A69B File Offset: 0x0012889B
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x1700A1C8 RID: 41416
			// (set) Token: 0x0600D1AB RID: 53675 RVA: 0x0012A6AE File Offset: 0x001288AE
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x1700A1C9 RID: 41417
			// (set) Token: 0x0600D1AC RID: 53676 RVA: 0x0012A6C1 File Offset: 0x001288C1
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x1700A1CA RID: 41418
			// (set) Token: 0x0600D1AD RID: 53677 RVA: 0x0012A6D4 File Offset: 0x001288D4
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x1700A1CB RID: 41419
			// (set) Token: 0x0600D1AE RID: 53678 RVA: 0x0012A6E7 File Offset: 0x001288E7
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x1700A1CC RID: 41420
			// (set) Token: 0x0600D1AF RID: 53679 RVA: 0x0012A6FF File Offset: 0x001288FF
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x1700A1CD RID: 41421
			// (set) Token: 0x0600D1B0 RID: 53680 RVA: 0x0012A712 File Offset: 0x00128912
			public virtual bool IsCalculatedTargetAddress
			{
				set
				{
					base.PowerSharpParameters["IsCalculatedTargetAddress"] = value;
				}
			}

			// Token: 0x1700A1CE RID: 41422
			// (set) Token: 0x0600D1B1 RID: 53681 RVA: 0x0012A72A File Offset: 0x0012892A
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x1700A1CF RID: 41423
			// (set) Token: 0x0600D1B2 RID: 53682 RVA: 0x0012A73D File Offset: 0x0012893D
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x1700A1D0 RID: 41424
			// (set) Token: 0x0600D1B3 RID: 53683 RVA: 0x0012A755 File Offset: 0x00128955
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x1700A1D1 RID: 41425
			// (set) Token: 0x0600D1B4 RID: 53684 RVA: 0x0012A768 File Offset: 0x00128968
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x1700A1D2 RID: 41426
			// (set) Token: 0x0600D1B5 RID: 53685 RVA: 0x0012A780 File Offset: 0x00128980
			public virtual bool ExcludedFromBackSync
			{
				set
				{
					base.PowerSharpParameters["ExcludedFromBackSync"] = value;
				}
			}

			// Token: 0x1700A1D3 RID: 41427
			// (set) Token: 0x0600D1B6 RID: 53686 RVA: 0x0012A798 File Offset: 0x00128998
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x1700A1D4 RID: 41428
			// (set) Token: 0x0600D1B7 RID: 53687 RVA: 0x0012A7AB File Offset: 0x001289AB
			public virtual ElcMailboxFlags ElcMailboxFlags
			{
				set
				{
					base.PowerSharpParameters["ElcMailboxFlags"] = value;
				}
			}

			// Token: 0x1700A1D5 RID: 41429
			// (set) Token: 0x0600D1B8 RID: 53688 RVA: 0x0012A7C3 File Offset: 0x001289C3
			public virtual bool MailboxAuditEnabled
			{
				set
				{
					base.PowerSharpParameters["MailboxAuditEnabled"] = value;
				}
			}

			// Token: 0x1700A1D6 RID: 41430
			// (set) Token: 0x0600D1B9 RID: 53689 RVA: 0x0012A7DB File Offset: 0x001289DB
			public virtual EnhancedTimeSpan MailboxAuditLogAgeLimit
			{
				set
				{
					base.PowerSharpParameters["MailboxAuditLogAgeLimit"] = value;
				}
			}

			// Token: 0x1700A1D7 RID: 41431
			// (set) Token: 0x0600D1BA RID: 53690 RVA: 0x0012A7F3 File Offset: 0x001289F3
			public virtual MailboxAuditOperations AuditAdminOperations
			{
				set
				{
					base.PowerSharpParameters["AuditAdminOperations"] = value;
				}
			}

			// Token: 0x1700A1D8 RID: 41432
			// (set) Token: 0x0600D1BB RID: 53691 RVA: 0x0012A80B File Offset: 0x00128A0B
			public virtual MailboxAuditOperations AuditDelegateAdminOperations
			{
				set
				{
					base.PowerSharpParameters["AuditDelegateAdminOperations"] = value;
				}
			}

			// Token: 0x1700A1D9 RID: 41433
			// (set) Token: 0x0600D1BC RID: 53692 RVA: 0x0012A823 File Offset: 0x00128A23
			public virtual MailboxAuditOperations AuditDelegateOperations
			{
				set
				{
					base.PowerSharpParameters["AuditDelegateOperations"] = value;
				}
			}

			// Token: 0x1700A1DA RID: 41434
			// (set) Token: 0x0600D1BD RID: 53693 RVA: 0x0012A83B File Offset: 0x00128A3B
			public virtual MailboxAuditOperations AuditOwnerOperations
			{
				set
				{
					base.PowerSharpParameters["AuditOwnerOperations"] = value;
				}
			}

			// Token: 0x1700A1DB RID: 41435
			// (set) Token: 0x0600D1BE RID: 53694 RVA: 0x0012A853 File Offset: 0x00128A53
			public virtual bool BypassAudit
			{
				set
				{
					base.PowerSharpParameters["BypassAudit"] = value;
				}
			}

			// Token: 0x1700A1DC RID: 41436
			// (set) Token: 0x0600D1BF RID: 53695 RVA: 0x0012A86B File Offset: 0x00128A6B
			public virtual DateTime? SiteMailboxClosedTime
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxClosedTime"] = value;
				}
			}

			// Token: 0x1700A1DD RID: 41437
			// (set) Token: 0x0600D1C0 RID: 53696 RVA: 0x0012A883 File Offset: 0x00128A83
			public virtual Uri SharePointUrl
			{
				set
				{
					base.PowerSharpParameters["SharePointUrl"] = value;
				}
			}

			// Token: 0x1700A1DE RID: 41438
			// (set) Token: 0x0600D1C1 RID: 53697 RVA: 0x0012A896 File Offset: 0x00128A96
			public virtual bool AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x1700A1DF RID: 41439
			// (set) Token: 0x0600D1C2 RID: 53698 RVA: 0x0012A8AE File Offset: 0x00128AAE
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x1700A1E0 RID: 41440
			// (set) Token: 0x0600D1C3 RID: 53699 RVA: 0x0012A8C6 File Offset: 0x00128AC6
			public virtual Guid ExchangeGuid
			{
				set
				{
					base.PowerSharpParameters["ExchangeGuid"] = value;
				}
			}

			// Token: 0x1700A1E1 RID: 41441
			// (set) Token: 0x0600D1C4 RID: 53700 RVA: 0x0012A8DE File Offset: 0x00128ADE
			public virtual Guid? MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x1700A1E2 RID: 41442
			// (set) Token: 0x0600D1C5 RID: 53701 RVA: 0x0012A8F6 File Offset: 0x00128AF6
			public virtual MultiValuedProperty<Guid> AggregatedMailboxGuids
			{
				set
				{
					base.PowerSharpParameters["AggregatedMailboxGuids"] = value;
				}
			}

			// Token: 0x1700A1E3 RID: 41443
			// (set) Token: 0x0600D1C6 RID: 53702 RVA: 0x0012A909 File Offset: 0x00128B09
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x1700A1E4 RID: 41444
			// (set) Token: 0x0600D1C7 RID: 53703 RVA: 0x0012A921 File Offset: 0x00128B21
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x1700A1E5 RID: 41445
			// (set) Token: 0x0600D1C8 RID: 53704 RVA: 0x0012A934 File Offset: 0x00128B34
			public virtual Unlimited<ByteQuantifiedSize> ArchiveQuota
			{
				set
				{
					base.PowerSharpParameters["ArchiveQuota"] = value;
				}
			}

			// Token: 0x1700A1E6 RID: 41446
			// (set) Token: 0x0600D1C9 RID: 53705 RVA: 0x0012A94C File Offset: 0x00128B4C
			public virtual Unlimited<ByteQuantifiedSize> ArchiveWarningQuota
			{
				set
				{
					base.PowerSharpParameters["ArchiveWarningQuota"] = value;
				}
			}

			// Token: 0x1700A1E7 RID: 41447
			// (set) Token: 0x0600D1CA RID: 53706 RVA: 0x0012A964 File Offset: 0x00128B64
			public virtual ProxyAddress ExternalEmailAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalEmailAddress"] = value;
				}
			}

			// Token: 0x1700A1E8 RID: 41448
			// (set) Token: 0x0600D1CB RID: 53707 RVA: 0x0012A977 File Offset: 0x00128B77
			public virtual bool UsePreferMessageFormat
			{
				set
				{
					base.PowerSharpParameters["UsePreferMessageFormat"] = value;
				}
			}

			// Token: 0x1700A1E9 RID: 41449
			// (set) Token: 0x0600D1CC RID: 53708 RVA: 0x0012A98F File Offset: 0x00128B8F
			public virtual SmtpAddress JournalArchiveAddress
			{
				set
				{
					base.PowerSharpParameters["JournalArchiveAddress"] = value;
				}
			}

			// Token: 0x1700A1EA RID: 41450
			// (set) Token: 0x0600D1CD RID: 53709 RVA: 0x0012A9A7 File Offset: 0x00128BA7
			public virtual MessageFormat MessageFormat
			{
				set
				{
					base.PowerSharpParameters["MessageFormat"] = value;
				}
			}

			// Token: 0x1700A1EB RID: 41451
			// (set) Token: 0x0600D1CE RID: 53710 RVA: 0x0012A9BF File Offset: 0x00128BBF
			public virtual MessageBodyFormat MessageBodyFormat
			{
				set
				{
					base.PowerSharpParameters["MessageBodyFormat"] = value;
				}
			}

			// Token: 0x1700A1EC RID: 41452
			// (set) Token: 0x0600D1CF RID: 53711 RVA: 0x0012A9D7 File Offset: 0x00128BD7
			public virtual MacAttachmentFormat MacAttachmentFormat
			{
				set
				{
					base.PowerSharpParameters["MacAttachmentFormat"] = value;
				}
			}

			// Token: 0x1700A1ED RID: 41453
			// (set) Token: 0x0600D1D0 RID: 53712 RVA: 0x0012A9EF File Offset: 0x00128BEF
			public virtual Unlimited<int> RecipientLimits
			{
				set
				{
					base.PowerSharpParameters["RecipientLimits"] = value;
				}
			}

			// Token: 0x1700A1EE RID: 41454
			// (set) Token: 0x0600D1D1 RID: 53713 RVA: 0x0012AA07 File Offset: 0x00128C07
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x1700A1EF RID: 41455
			// (set) Token: 0x0600D1D2 RID: 53714 RVA: 0x0012AA1A File Offset: 0x00128C1A
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x1700A1F0 RID: 41456
			// (set) Token: 0x0600D1D3 RID: 53715 RVA: 0x0012AA32 File Offset: 0x00128C32
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x1700A1F1 RID: 41457
			// (set) Token: 0x0600D1D4 RID: 53716 RVA: 0x0012AA45 File Offset: 0x00128C45
			public virtual SmtpAddress WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x1700A1F2 RID: 41458
			// (set) Token: 0x0600D1D5 RID: 53717 RVA: 0x0012AA5D File Offset: 0x00128C5D
			public virtual SmtpAddress MicrosoftOnlineServicesID
			{
				set
				{
					base.PowerSharpParameters["MicrosoftOnlineServicesID"] = value;
				}
			}

			// Token: 0x1700A1F3 RID: 41459
			// (set) Token: 0x0600D1D6 RID: 53718 RVA: 0x0012AA75 File Offset: 0x00128C75
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x1700A1F4 RID: 41460
			// (set) Token: 0x0600D1D7 RID: 53719 RVA: 0x0012AA88 File Offset: 0x00128C88
			public virtual bool? SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x1700A1F5 RID: 41461
			// (set) Token: 0x0600D1D8 RID: 53720 RVA: 0x0012AAA0 File Offset: 0x00128CA0
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x1700A1F6 RID: 41462
			// (set) Token: 0x0600D1D9 RID: 53721 RVA: 0x0012AAB8 File Offset: 0x00128CB8
			public virtual bool LitigationHoldEnabled
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldEnabled"] = value;
				}
			}

			// Token: 0x1700A1F7 RID: 41463
			// (set) Token: 0x0600D1DA RID: 53722 RVA: 0x0012AAD0 File Offset: 0x00128CD0
			public virtual bool SingleItemRecoveryEnabled
			{
				set
				{
					base.PowerSharpParameters["SingleItemRecoveryEnabled"] = value;
				}
			}

			// Token: 0x1700A1F8 RID: 41464
			// (set) Token: 0x0600D1DB RID: 53723 RVA: 0x0012AAE8 File Offset: 0x00128CE8
			public virtual bool RetentionHoldEnabled
			{
				set
				{
					base.PowerSharpParameters["RetentionHoldEnabled"] = value;
				}
			}

			// Token: 0x1700A1F9 RID: 41465
			// (set) Token: 0x0600D1DC RID: 53724 RVA: 0x0012AB00 File Offset: 0x00128D00
			public virtual DateTime? EndDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["EndDateForRetentionHold"] = value;
				}
			}

			// Token: 0x1700A1FA RID: 41466
			// (set) Token: 0x0600D1DD RID: 53725 RVA: 0x0012AB18 File Offset: 0x00128D18
			public virtual DateTime? StartDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["StartDateForRetentionHold"] = value;
				}
			}

			// Token: 0x1700A1FB RID: 41467
			// (set) Token: 0x0600D1DE RID: 53726 RVA: 0x0012AB30 File Offset: 0x00128D30
			public virtual string RetentionComment
			{
				set
				{
					base.PowerSharpParameters["RetentionComment"] = value;
				}
			}

			// Token: 0x1700A1FC RID: 41468
			// (set) Token: 0x0600D1DF RID: 53727 RVA: 0x0012AB43 File Offset: 0x00128D43
			public virtual string RetentionUrl
			{
				set
				{
					base.PowerSharpParameters["RetentionUrl"] = value;
				}
			}

			// Token: 0x1700A1FD RID: 41469
			// (set) Token: 0x0600D1E0 RID: 53728 RVA: 0x0012AB56 File Offset: 0x00128D56
			public virtual DateTime? LitigationHoldDate
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldDate"] = value;
				}
			}

			// Token: 0x1700A1FE RID: 41470
			// (set) Token: 0x0600D1E1 RID: 53729 RVA: 0x0012AB6E File Offset: 0x00128D6E
			public virtual string LitigationHoldOwner
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldOwner"] = value;
				}
			}

			// Token: 0x1700A1FF RID: 41471
			// (set) Token: 0x0600D1E2 RID: 53730 RVA: 0x0012AB81 File Offset: 0x00128D81
			public virtual EnhancedTimeSpan RetainDeletedItemsFor
			{
				set
				{
					base.PowerSharpParameters["RetainDeletedItemsFor"] = value;
				}
			}

			// Token: 0x1700A200 RID: 41472
			// (set) Token: 0x0600D1E3 RID: 53731 RVA: 0x0012AB99 File Offset: 0x00128D99
			public virtual bool CalendarVersionStoreDisabled
			{
				set
				{
					base.PowerSharpParameters["CalendarVersionStoreDisabled"] = value;
				}
			}

			// Token: 0x1700A201 RID: 41473
			// (set) Token: 0x0600D1E4 RID: 53732 RVA: 0x0012ABB1 File Offset: 0x00128DB1
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x1700A202 RID: 41474
			// (set) Token: 0x0600D1E5 RID: 53733 RVA: 0x0012ABC4 File Offset: 0x00128DC4
			public virtual Unlimited<ByteQuantifiedSize> RecoverableItemsQuota
			{
				set
				{
					base.PowerSharpParameters["RecoverableItemsQuota"] = value;
				}
			}

			// Token: 0x1700A203 RID: 41475
			// (set) Token: 0x0600D1E6 RID: 53734 RVA: 0x0012ABDC File Offset: 0x00128DDC
			public virtual Unlimited<ByteQuantifiedSize> RecoverableItemsWarningQuota
			{
				set
				{
					base.PowerSharpParameters["RecoverableItemsWarningQuota"] = value;
				}
			}

			// Token: 0x1700A204 RID: 41476
			// (set) Token: 0x0600D1E7 RID: 53735 RVA: 0x0012ABF4 File Offset: 0x00128DF4
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x1700A205 RID: 41477
			// (set) Token: 0x0600D1E8 RID: 53736 RVA: 0x0012AC07 File Offset: 0x00128E07
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x1700A206 RID: 41478
			// (set) Token: 0x0600D1E9 RID: 53737 RVA: 0x0012AC1A File Offset: 0x00128E1A
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700A207 RID: 41479
			// (set) Token: 0x0600D1EA RID: 53738 RVA: 0x0012AC2D File Offset: 0x00128E2D
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x1700A208 RID: 41480
			// (set) Token: 0x0600D1EB RID: 53739 RVA: 0x0012AC40 File Offset: 0x00128E40
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x1700A209 RID: 41481
			// (set) Token: 0x0600D1EC RID: 53740 RVA: 0x0012AC53 File Offset: 0x00128E53
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x1700A20A RID: 41482
			// (set) Token: 0x0600D1ED RID: 53741 RVA: 0x0012AC66 File Offset: 0x00128E66
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x1700A20B RID: 41483
			// (set) Token: 0x0600D1EE RID: 53742 RVA: 0x0012AC79 File Offset: 0x00128E79
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x1700A20C RID: 41484
			// (set) Token: 0x0600D1EF RID: 53743 RVA: 0x0012AC8C File Offset: 0x00128E8C
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x1700A20D RID: 41485
			// (set) Token: 0x0600D1F0 RID: 53744 RVA: 0x0012AC9F File Offset: 0x00128E9F
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x1700A20E RID: 41486
			// (set) Token: 0x0600D1F1 RID: 53745 RVA: 0x0012ACB2 File Offset: 0x00128EB2
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x1700A20F RID: 41487
			// (set) Token: 0x0600D1F2 RID: 53746 RVA: 0x0012ACC5 File Offset: 0x00128EC5
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x1700A210 RID: 41488
			// (set) Token: 0x0600D1F3 RID: 53747 RVA: 0x0012ACD8 File Offset: 0x00128ED8
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x1700A211 RID: 41489
			// (set) Token: 0x0600D1F4 RID: 53748 RVA: 0x0012ACEB File Offset: 0x00128EEB
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x1700A212 RID: 41490
			// (set) Token: 0x0600D1F5 RID: 53749 RVA: 0x0012ACFE File Offset: 0x00128EFE
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x1700A213 RID: 41491
			// (set) Token: 0x0600D1F6 RID: 53750 RVA: 0x0012AD11 File Offset: 0x00128F11
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x1700A214 RID: 41492
			// (set) Token: 0x0600D1F7 RID: 53751 RVA: 0x0012AD24 File Offset: 0x00128F24
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x1700A215 RID: 41493
			// (set) Token: 0x0600D1F8 RID: 53752 RVA: 0x0012AD37 File Offset: 0x00128F37
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x1700A216 RID: 41494
			// (set) Token: 0x0600D1F9 RID: 53753 RVA: 0x0012AD4A File Offset: 0x00128F4A
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x1700A217 RID: 41495
			// (set) Token: 0x0600D1FA RID: 53754 RVA: 0x0012AD5D File Offset: 0x00128F5D
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x1700A218 RID: 41496
			// (set) Token: 0x0600D1FB RID: 53755 RVA: 0x0012AD70 File Offset: 0x00128F70
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x1700A219 RID: 41497
			// (set) Token: 0x0600D1FC RID: 53756 RVA: 0x0012AD83 File Offset: 0x00128F83
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x1700A21A RID: 41498
			// (set) Token: 0x0600D1FD RID: 53757 RVA: 0x0012AD96 File Offset: 0x00128F96
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x1700A21B RID: 41499
			// (set) Token: 0x0600D1FE RID: 53758 RVA: 0x0012ADA9 File Offset: 0x00128FA9
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700A21C RID: 41500
			// (set) Token: 0x0600D1FF RID: 53759 RVA: 0x0012ADBC File Offset: 0x00128FBC
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x1700A21D RID: 41501
			// (set) Token: 0x0600D200 RID: 53760 RVA: 0x0012ADCF File Offset: 0x00128FCF
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x1700A21E RID: 41502
			// (set) Token: 0x0600D201 RID: 53761 RVA: 0x0012ADE7 File Offset: 0x00128FE7
			public virtual Unlimited<ByteQuantifiedSize> MaxSendSize
			{
				set
				{
					base.PowerSharpParameters["MaxSendSize"] = value;
				}
			}

			// Token: 0x1700A21F RID: 41503
			// (set) Token: 0x0600D202 RID: 53762 RVA: 0x0012ADFF File Offset: 0x00128FFF
			public virtual Unlimited<ByteQuantifiedSize> MaxReceiveSize
			{
				set
				{
					base.PowerSharpParameters["MaxReceiveSize"] = value;
				}
			}

			// Token: 0x1700A220 RID: 41504
			// (set) Token: 0x0600D203 RID: 53763 RVA: 0x0012AE17 File Offset: 0x00129017
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x1700A221 RID: 41505
			// (set) Token: 0x0600D204 RID: 53764 RVA: 0x0012AE2F File Offset: 0x0012902F
			public virtual bool EmailAddressPolicyEnabled
			{
				set
				{
					base.PowerSharpParameters["EmailAddressPolicyEnabled"] = value;
				}
			}

			// Token: 0x1700A222 RID: 41506
			// (set) Token: 0x0600D205 RID: 53765 RVA: 0x0012AE47 File Offset: 0x00129047
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x1700A223 RID: 41507
			// (set) Token: 0x0600D206 RID: 53766 RVA: 0x0012AE5F File Offset: 0x0012905F
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x1700A224 RID: 41508
			// (set) Token: 0x0600D207 RID: 53767 RVA: 0x0012AE77 File Offset: 0x00129077
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x1700A225 RID: 41509
			// (set) Token: 0x0600D208 RID: 53768 RVA: 0x0012AE8A File Offset: 0x0012908A
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x1700A226 RID: 41510
			// (set) Token: 0x0600D209 RID: 53769 RVA: 0x0012AEA2 File Offset: 0x001290A2
			public virtual MultiValuedProperty<string> UMDtmfMap
			{
				set
				{
					base.PowerSharpParameters["UMDtmfMap"] = value;
				}
			}

			// Token: 0x1700A227 RID: 41511
			// (set) Token: 0x0600D20A RID: 53770 RVA: 0x0012AEB5 File Offset: 0x001290B5
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x1700A228 RID: 41512
			// (set) Token: 0x0600D20B RID: 53771 RVA: 0x0012AECD File Offset: 0x001290CD
			public virtual string MailTip
			{
				set
				{
					base.PowerSharpParameters["MailTip"] = value;
				}
			}

			// Token: 0x1700A229 RID: 41513
			// (set) Token: 0x0600D20C RID: 53772 RVA: 0x0012AEE0 File Offset: 0x001290E0
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x1700A22A RID: 41514
			// (set) Token: 0x0600D20D RID: 53773 RVA: 0x0012AEF3 File Offset: 0x001290F3
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700A22B RID: 41515
			// (set) Token: 0x0600D20E RID: 53774 RVA: 0x0012AF06 File Offset: 0x00129106
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A22C RID: 41516
			// (set) Token: 0x0600D20F RID: 53775 RVA: 0x0012AF1E File Offset: 0x0012911E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A22D RID: 41517
			// (set) Token: 0x0600D210 RID: 53776 RVA: 0x0012AF36 File Offset: 0x00129136
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A22E RID: 41518
			// (set) Token: 0x0600D211 RID: 53777 RVA: 0x0012AF4E File Offset: 0x0012914E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A22F RID: 41519
			// (set) Token: 0x0600D212 RID: 53778 RVA: 0x0012AF66 File Offset: 0x00129166
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000DCE RID: 3534
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A230 RID: 41520
			// (set) Token: 0x0600D214 RID: 53780 RVA: 0x0012AF86 File Offset: 0x00129186
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailUserIdParameter(value) : null);
				}
			}

			// Token: 0x1700A231 RID: 41521
			// (set) Token: 0x0600D215 RID: 53781 RVA: 0x0012AFA4 File Offset: 0x001291A4
			public virtual string ForwardingAddress
			{
				set
				{
					base.PowerSharpParameters["ForwardingAddress"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x1700A232 RID: 41522
			// (set) Token: 0x0600D216 RID: 53782 RVA: 0x0012AFC2 File Offset: 0x001291C2
			public virtual string Manager
			{
				set
				{
					base.PowerSharpParameters["Manager"] = ((value != null) ? new UserContactIdParameter(value) : null);
				}
			}

			// Token: 0x1700A233 RID: 41523
			// (set) Token: 0x0600D217 RID: 53783 RVA: 0x0012AFE0 File Offset: 0x001291E0
			public virtual string IntendedMailboxPlanName
			{
				set
				{
					base.PowerSharpParameters["IntendedMailboxPlanName"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x1700A234 RID: 41524
			// (set) Token: 0x0600D218 RID: 53784 RVA: 0x0012AFFE File Offset: 0x001291FE
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFrom"] = value;
				}
			}

			// Token: 0x1700A235 RID: 41525
			// (set) Token: 0x0600D219 RID: 53785 RVA: 0x0012B011 File Offset: 0x00129211
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromDLMembers"] = value;
				}
			}

			// Token: 0x1700A236 RID: 41526
			// (set) Token: 0x0600D21A RID: 53786 RVA: 0x0012B024 File Offset: 0x00129224
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>> RawAcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["RawAcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x1700A237 RID: 41527
			// (set) Token: 0x0600D21B RID: 53787 RVA: 0x0012B037 File Offset: 0x00129237
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>> RawBypassModerationFrom
			{
				set
				{
					base.PowerSharpParameters["RawBypassModerationFrom"] = value;
				}
			}

			// Token: 0x1700A238 RID: 41528
			// (set) Token: 0x0600D21C RID: 53788 RVA: 0x0012B04A File Offset: 0x0012924A
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>> RawRejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RawRejectMessagesFrom"] = value;
				}
			}

			// Token: 0x1700A239 RID: 41529
			// (set) Token: 0x0600D21D RID: 53789 RVA: 0x0012B05D File Offset: 0x0012925D
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<RecipientIdParameter>> RawGrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["RawGrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x1700A23A RID: 41530
			// (set) Token: 0x0600D21E RID: 53790 RVA: 0x0012B070 File Offset: 0x00129270
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<ModeratorIDParameter>> RawModeratedBy
			{
				set
				{
					base.PowerSharpParameters["RawModeratedBy"] = value;
				}
			}

			// Token: 0x1700A23B RID: 41531
			// (set) Token: 0x0600D21F RID: 53791 RVA: 0x0012B083 File Offset: 0x00129283
			public virtual RecipientWithAdUserIdParameter<RecipientIdParameter> RawForwardingAddress
			{
				set
				{
					base.PowerSharpParameters["RawForwardingAddress"] = value;
				}
			}

			// Token: 0x1700A23C RID: 41532
			// (set) Token: 0x0600D220 RID: 53792 RVA: 0x0012B096 File Offset: 0x00129296
			public virtual string C
			{
				set
				{
					base.PowerSharpParameters["C"] = value;
				}
			}

			// Token: 0x1700A23D RID: 41533
			// (set) Token: 0x0600D221 RID: 53793 RVA: 0x0012B0A9 File Offset: 0x001292A9
			public virtual string Co
			{
				set
				{
					base.PowerSharpParameters["Co"] = value;
				}
			}

			// Token: 0x1700A23E RID: 41534
			// (set) Token: 0x0600D222 RID: 53794 RVA: 0x0012B0BC File Offset: 0x001292BC
			public virtual SwitchParameter DoNotCheckAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["DoNotCheckAcceptedDomains"] = value;
				}
			}

			// Token: 0x1700A23F RID: 41535
			// (set) Token: 0x0600D223 RID: 53795 RVA: 0x0012B0D4 File Offset: 0x001292D4
			public virtual ProxyAddressCollection SmtpAndX500Addresses
			{
				set
				{
					base.PowerSharpParameters["SmtpAndX500Addresses"] = value;
				}
			}

			// Token: 0x1700A240 RID: 41536
			// (set) Token: 0x0600D224 RID: 53796 RVA: 0x0012B0E7 File Offset: 0x001292E7
			public virtual ProxyAddressCollection SipAddresses
			{
				set
				{
					base.PowerSharpParameters["SipAddresses"] = value;
				}
			}

			// Token: 0x1700A241 RID: 41537
			// (set) Token: 0x0600D225 RID: 53797 RVA: 0x0012B0FA File Offset: 0x001292FA
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x1700A242 RID: 41538
			// (set) Token: 0x0600D226 RID: 53798 RVA: 0x0012B112 File Offset: 0x00129312
			public virtual SwitchParameter SoftDeletedMailUser
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedMailUser"] = value;
				}
			}

			// Token: 0x1700A243 RID: 41539
			// (set) Token: 0x0600D227 RID: 53799 RVA: 0x0012B12A File Offset: 0x0012932A
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<RecipientIdParameter>> RawSiteMailboxOwners
			{
				set
				{
					base.PowerSharpParameters["RawSiteMailboxOwners"] = value;
				}
			}

			// Token: 0x1700A244 RID: 41540
			// (set) Token: 0x0600D228 RID: 53800 RVA: 0x0012B13D File Offset: 0x0012933D
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<RecipientIdParameter>> RawSiteMailboxUsers
			{
				set
				{
					base.PowerSharpParameters["RawSiteMailboxUsers"] = value;
				}
			}

			// Token: 0x1700A245 RID: 41541
			// (set) Token: 0x0600D229 RID: 53801 RVA: 0x0012B150 File Offset: 0x00129350
			public virtual MultiValuedProperty<RecipientIdParameter> SiteMailboxOwners
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxOwners"] = value;
				}
			}

			// Token: 0x1700A246 RID: 41542
			// (set) Token: 0x0600D22A RID: 53802 RVA: 0x0012B163 File Offset: 0x00129363
			public virtual MultiValuedProperty<RecipientIdParameter> SiteMailboxUsers
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxUsers"] = value;
				}
			}

			// Token: 0x1700A247 RID: 41543
			// (set) Token: 0x0600D22B RID: 53803 RVA: 0x0012B176 File Offset: 0x00129376
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x1700A248 RID: 41544
			// (set) Token: 0x0600D22C RID: 53804 RVA: 0x0012B18E File Offset: 0x0012938E
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x1700A249 RID: 41545
			// (set) Token: 0x0600D22D RID: 53805 RVA: 0x0012B1A1 File Offset: 0x001293A1
			public virtual SwitchParameter BypassLiveId
			{
				set
				{
					base.PowerSharpParameters["BypassLiveId"] = value;
				}
			}

			// Token: 0x1700A24A RID: 41546
			// (set) Token: 0x0600D22E RID: 53806 RVA: 0x0012B1B9 File Offset: 0x001293B9
			public virtual NetID NetID
			{
				set
				{
					base.PowerSharpParameters["NetID"] = value;
				}
			}

			// Token: 0x1700A24B RID: 41547
			// (set) Token: 0x0600D22F RID: 53807 RVA: 0x0012B1CC File Offset: 0x001293CC
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x1700A24C RID: 41548
			// (set) Token: 0x0600D230 RID: 53808 RVA: 0x0012B1DF File Offset: 0x001293DF
			public virtual string FederatedIdentity
			{
				set
				{
					base.PowerSharpParameters["FederatedIdentity"] = value;
				}
			}

			// Token: 0x1700A24D RID: 41549
			// (set) Token: 0x0600D231 RID: 53809 RVA: 0x0012B1F2 File Offset: 0x001293F2
			public virtual string SecondaryAddress
			{
				set
				{
					base.PowerSharpParameters["SecondaryAddress"] = value;
				}
			}

			// Token: 0x1700A24E RID: 41550
			// (set) Token: 0x0600D232 RID: 53810 RVA: 0x0012B205 File Offset: 0x00129405
			public virtual string SecondaryDialPlan
			{
				set
				{
					base.PowerSharpParameters["SecondaryDialPlan"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x1700A24F RID: 41551
			// (set) Token: 0x0600D233 RID: 53811 RVA: 0x0012B223 File Offset: 0x00129423
			public virtual SwitchParameter RemovePicture
			{
				set
				{
					base.PowerSharpParameters["RemovePicture"] = value;
				}
			}

			// Token: 0x1700A250 RID: 41552
			// (set) Token: 0x0600D234 RID: 53812 RVA: 0x0012B23B File Offset: 0x0012943B
			public virtual SwitchParameter RemoveSpokenName
			{
				set
				{
					base.PowerSharpParameters["RemoveSpokenName"] = value;
				}
			}

			// Token: 0x1700A251 RID: 41553
			// (set) Token: 0x0600D235 RID: 53813 RVA: 0x0012B253 File Offset: 0x00129453
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x1700A252 RID: 41554
			// (set) Token: 0x0600D236 RID: 53814 RVA: 0x0012B266 File Offset: 0x00129466
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x1700A253 RID: 41555
			// (set) Token: 0x0600D237 RID: 53815 RVA: 0x0012B279 File Offset: 0x00129479
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x1700A254 RID: 41556
			// (set) Token: 0x0600D238 RID: 53816 RVA: 0x0012B28C File Offset: 0x0012948C
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A255 RID: 41557
			// (set) Token: 0x0600D239 RID: 53817 RVA: 0x0012B2AA File Offset: 0x001294AA
			public virtual MultiValuedProperty<RecipientIdParameter> GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x1700A256 RID: 41558
			// (set) Token: 0x0600D23A RID: 53818 RVA: 0x0012B2BD File Offset: 0x001294BD
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x1700A257 RID: 41559
			// (set) Token: 0x0600D23B RID: 53819 RVA: 0x0012B2D0 File Offset: 0x001294D0
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x1700A258 RID: 41560
			// (set) Token: 0x0600D23C RID: 53820 RVA: 0x0012B2E3 File Offset: 0x001294E3
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x1700A259 RID: 41561
			// (set) Token: 0x0600D23D RID: 53821 RVA: 0x0012B2F6 File Offset: 0x001294F6
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x1700A25A RID: 41562
			// (set) Token: 0x0600D23E RID: 53822 RVA: 0x0012B309 File Offset: 0x00129509
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x1700A25B RID: 41563
			// (set) Token: 0x0600D23F RID: 53823 RVA: 0x0012B31C File Offset: 0x0012951C
			public virtual bool CreateDTMFMap
			{
				set
				{
					base.PowerSharpParameters["CreateDTMFMap"] = value;
				}
			}

			// Token: 0x1700A25C RID: 41564
			// (set) Token: 0x0600D240 RID: 53824 RVA: 0x0012B334 File Offset: 0x00129534
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700A25D RID: 41565
			// (set) Token: 0x0600D241 RID: 53825 RVA: 0x0012B34C File Offset: 0x0012954C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A25E RID: 41566
			// (set) Token: 0x0600D242 RID: 53826 RVA: 0x0012B35F File Offset: 0x0012955F
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x1700A25F RID: 41567
			// (set) Token: 0x0600D243 RID: 53827 RVA: 0x0012B372 File Offset: 0x00129572
			public virtual bool DeliverToMailboxAndForward
			{
				set
				{
					base.PowerSharpParameters["DeliverToMailboxAndForward"] = value;
				}
			}

			// Token: 0x1700A260 RID: 41568
			// (set) Token: 0x0600D244 RID: 53828 RVA: 0x0012B38A File Offset: 0x0012958A
			public virtual string AssistantName
			{
				set
				{
					base.PowerSharpParameters["AssistantName"] = value;
				}
			}

			// Token: 0x1700A261 RID: 41569
			// (set) Token: 0x0600D245 RID: 53829 RVA: 0x0012B39D File Offset: 0x0012959D
			public virtual byte BlockedSendersHash
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersHash"] = value;
				}
			}

			// Token: 0x1700A262 RID: 41570
			// (set) Token: 0x0600D246 RID: 53830 RVA: 0x0012B3B5 File Offset: 0x001295B5
			public virtual SecurityIdentifier MasterAccountSid
			{
				set
				{
					base.PowerSharpParameters["MasterAccountSid"] = value;
				}
			}

			// Token: 0x1700A263 RID: 41571
			// (set) Token: 0x0600D247 RID: 53831 RVA: 0x0012B3C8 File Offset: 0x001295C8
			public virtual string Notes
			{
				set
				{
					base.PowerSharpParameters["Notes"] = value;
				}
			}

			// Token: 0x1700A264 RID: 41572
			// (set) Token: 0x0600D248 RID: 53832 RVA: 0x0012B3DB File Offset: 0x001295DB
			public virtual RecipientDisplayType? RecipientDisplayType
			{
				set
				{
					base.PowerSharpParameters["RecipientDisplayType"] = value;
				}
			}

			// Token: 0x1700A265 RID: 41573
			// (set) Token: 0x0600D249 RID: 53833 RVA: 0x0012B3F3 File Offset: 0x001295F3
			public virtual ExchangeResourceType? ResourceType
			{
				set
				{
					base.PowerSharpParameters["ResourceType"] = value;
				}
			}

			// Token: 0x1700A266 RID: 41574
			// (set) Token: 0x0600D24A RID: 53834 RVA: 0x0012B40B File Offset: 0x0012960B
			public virtual byte SafeRecipientsHash
			{
				set
				{
					base.PowerSharpParameters["SafeRecipientsHash"] = value;
				}
			}

			// Token: 0x1700A267 RID: 41575
			// (set) Token: 0x0600D24B RID: 53835 RVA: 0x0012B423 File Offset: 0x00129623
			public virtual byte SafeSendersHash
			{
				set
				{
					base.PowerSharpParameters["SafeSendersHash"] = value;
				}
			}

			// Token: 0x1700A268 RID: 41576
			// (set) Token: 0x0600D24C RID: 53836 RVA: 0x0012B43B File Offset: 0x0012963B
			public virtual byte Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x1700A269 RID: 41577
			// (set) Token: 0x0600D24D RID: 53837 RVA: 0x0012B453 File Offset: 0x00129653
			public virtual string DirSyncId
			{
				set
				{
					base.PowerSharpParameters["DirSyncId"] = value;
				}
			}

			// Token: 0x1700A26A RID: 41578
			// (set) Token: 0x0600D24E RID: 53838 RVA: 0x0012B466 File Offset: 0x00129666
			public virtual string City
			{
				set
				{
					base.PowerSharpParameters["City"] = value;
				}
			}

			// Token: 0x1700A26B RID: 41579
			// (set) Token: 0x0600D24F RID: 53839 RVA: 0x0012B479 File Offset: 0x00129679
			public virtual string Company
			{
				set
				{
					base.PowerSharpParameters["Company"] = value;
				}
			}

			// Token: 0x1700A26C RID: 41580
			// (set) Token: 0x0600D250 RID: 53840 RVA: 0x0012B48C File Offset: 0x0012968C
			public virtual CountryInfo CountryOrRegion
			{
				set
				{
					base.PowerSharpParameters["CountryOrRegion"] = value;
				}
			}

			// Token: 0x1700A26D RID: 41581
			// (set) Token: 0x0600D251 RID: 53841 RVA: 0x0012B49F File Offset: 0x0012969F
			public virtual int CountryCode
			{
				set
				{
					base.PowerSharpParameters["CountryCode"] = value;
				}
			}

			// Token: 0x1700A26E RID: 41582
			// (set) Token: 0x0600D252 RID: 53842 RVA: 0x0012B4B7 File Offset: 0x001296B7
			public virtual string Department
			{
				set
				{
					base.PowerSharpParameters["Department"] = value;
				}
			}

			// Token: 0x1700A26F RID: 41583
			// (set) Token: 0x0600D253 RID: 53843 RVA: 0x0012B4CA File Offset: 0x001296CA
			public virtual string Fax
			{
				set
				{
					base.PowerSharpParameters["Fax"] = value;
				}
			}

			// Token: 0x1700A270 RID: 41584
			// (set) Token: 0x0600D254 RID: 53844 RVA: 0x0012B4DD File Offset: 0x001296DD
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x1700A271 RID: 41585
			// (set) Token: 0x0600D255 RID: 53845 RVA: 0x0012B4F0 File Offset: 0x001296F0
			public virtual string HomePhone
			{
				set
				{
					base.PowerSharpParameters["HomePhone"] = value;
				}
			}

			// Token: 0x1700A272 RID: 41586
			// (set) Token: 0x0600D256 RID: 53846 RVA: 0x0012B503 File Offset: 0x00129703
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x1700A273 RID: 41587
			// (set) Token: 0x0600D257 RID: 53847 RVA: 0x0012B516 File Offset: 0x00129716
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x1700A274 RID: 41588
			// (set) Token: 0x0600D258 RID: 53848 RVA: 0x0012B529 File Offset: 0x00129729
			public virtual string MobilePhone
			{
				set
				{
					base.PowerSharpParameters["MobilePhone"] = value;
				}
			}

			// Token: 0x1700A275 RID: 41589
			// (set) Token: 0x0600D259 RID: 53849 RVA: 0x0012B53C File Offset: 0x0012973C
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x1700A276 RID: 41590
			// (set) Token: 0x0600D25A RID: 53850 RVA: 0x0012B54F File Offset: 0x0012974F
			public virtual MultiValuedProperty<string> OtherFax
			{
				set
				{
					base.PowerSharpParameters["OtherFax"] = value;
				}
			}

			// Token: 0x1700A277 RID: 41591
			// (set) Token: 0x0600D25B RID: 53851 RVA: 0x0012B562 File Offset: 0x00129762
			public virtual MultiValuedProperty<string> OtherHomePhone
			{
				set
				{
					base.PowerSharpParameters["OtherHomePhone"] = value;
				}
			}

			// Token: 0x1700A278 RID: 41592
			// (set) Token: 0x0600D25C RID: 53852 RVA: 0x0012B575 File Offset: 0x00129775
			public virtual MultiValuedProperty<string> OtherTelephone
			{
				set
				{
					base.PowerSharpParameters["OtherTelephone"] = value;
				}
			}

			// Token: 0x1700A279 RID: 41593
			// (set) Token: 0x0600D25D RID: 53853 RVA: 0x0012B588 File Offset: 0x00129788
			public virtual string Pager
			{
				set
				{
					base.PowerSharpParameters["Pager"] = value;
				}
			}

			// Token: 0x1700A27A RID: 41594
			// (set) Token: 0x0600D25E RID: 53854 RVA: 0x0012B59B File Offset: 0x0012979B
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x1700A27B RID: 41595
			// (set) Token: 0x0600D25F RID: 53855 RVA: 0x0012B5AE File Offset: 0x001297AE
			public virtual string PostalCode
			{
				set
				{
					base.PowerSharpParameters["PostalCode"] = value;
				}
			}

			// Token: 0x1700A27C RID: 41596
			// (set) Token: 0x0600D260 RID: 53856 RVA: 0x0012B5C1 File Offset: 0x001297C1
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x1700A27D RID: 41597
			// (set) Token: 0x0600D261 RID: 53857 RVA: 0x0012B5D9 File Offset: 0x001297D9
			public virtual MultiValuedProperty<string> ResourceCustom
			{
				set
				{
					base.PowerSharpParameters["ResourceCustom"] = value;
				}
			}

			// Token: 0x1700A27E RID: 41598
			// (set) Token: 0x0600D262 RID: 53858 RVA: 0x0012B5EC File Offset: 0x001297EC
			public virtual MultiValuedProperty<string> ResourceMetaData
			{
				set
				{
					base.PowerSharpParameters["ResourceMetaData"] = value;
				}
			}

			// Token: 0x1700A27F RID: 41599
			// (set) Token: 0x0600D263 RID: 53859 RVA: 0x0012B5FF File Offset: 0x001297FF
			public virtual string ResourcePropertiesDisplay
			{
				set
				{
					base.PowerSharpParameters["ResourcePropertiesDisplay"] = value;
				}
			}

			// Token: 0x1700A280 RID: 41600
			// (set) Token: 0x0600D264 RID: 53860 RVA: 0x0012B612 File Offset: 0x00129812
			public virtual MultiValuedProperty<string> ResourceSearchProperties
			{
				set
				{
					base.PowerSharpParameters["ResourceSearchProperties"] = value;
				}
			}

			// Token: 0x1700A281 RID: 41601
			// (set) Token: 0x0600D265 RID: 53861 RVA: 0x0012B625 File Offset: 0x00129825
			public virtual string StateOrProvince
			{
				set
				{
					base.PowerSharpParameters["StateOrProvince"] = value;
				}
			}

			// Token: 0x1700A282 RID: 41602
			// (set) Token: 0x0600D266 RID: 53862 RVA: 0x0012B638 File Offset: 0x00129838
			public virtual string StreetAddress
			{
				set
				{
					base.PowerSharpParameters["StreetAddress"] = value;
				}
			}

			// Token: 0x1700A283 RID: 41603
			// (set) Token: 0x0600D267 RID: 53863 RVA: 0x0012B64B File Offset: 0x0012984B
			public virtual string TelephoneAssistant
			{
				set
				{
					base.PowerSharpParameters["TelephoneAssistant"] = value;
				}
			}

			// Token: 0x1700A284 RID: 41604
			// (set) Token: 0x0600D268 RID: 53864 RVA: 0x0012B65E File Offset: 0x0012985E
			public virtual string Title
			{
				set
				{
					base.PowerSharpParameters["Title"] = value;
				}
			}

			// Token: 0x1700A285 RID: 41605
			// (set) Token: 0x0600D269 RID: 53865 RVA: 0x0012B671 File Offset: 0x00129871
			public virtual string WebPage
			{
				set
				{
					base.PowerSharpParameters["WebPage"] = value;
				}
			}

			// Token: 0x1700A286 RID: 41606
			// (set) Token: 0x0600D26A RID: 53866 RVA: 0x0012B684 File Offset: 0x00129884
			public virtual int? SeniorityIndex
			{
				set
				{
					base.PowerSharpParameters["SeniorityIndex"] = value;
				}
			}

			// Token: 0x1700A287 RID: 41607
			// (set) Token: 0x0600D26B RID: 53867 RVA: 0x0012B69C File Offset: 0x0012989C
			public virtual string PhoneticDisplayName
			{
				set
				{
					base.PowerSharpParameters["PhoneticDisplayName"] = value;
				}
			}

			// Token: 0x1700A288 RID: 41608
			// (set) Token: 0x0600D26C RID: 53868 RVA: 0x0012B6AF File Offset: 0x001298AF
			public virtual bool IsCalculatedTargetAddress
			{
				set
				{
					base.PowerSharpParameters["IsCalculatedTargetAddress"] = value;
				}
			}

			// Token: 0x1700A289 RID: 41609
			// (set) Token: 0x0600D26D RID: 53869 RVA: 0x0012B6C7 File Offset: 0x001298C7
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x1700A28A RID: 41610
			// (set) Token: 0x0600D26E RID: 53870 RVA: 0x0012B6DA File Offset: 0x001298DA
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x1700A28B RID: 41611
			// (set) Token: 0x0600D26F RID: 53871 RVA: 0x0012B6F2 File Offset: 0x001298F2
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x1700A28C RID: 41612
			// (set) Token: 0x0600D270 RID: 53872 RVA: 0x0012B705 File Offset: 0x00129905
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x1700A28D RID: 41613
			// (set) Token: 0x0600D271 RID: 53873 RVA: 0x0012B71D File Offset: 0x0012991D
			public virtual bool ExcludedFromBackSync
			{
				set
				{
					base.PowerSharpParameters["ExcludedFromBackSync"] = value;
				}
			}

			// Token: 0x1700A28E RID: 41614
			// (set) Token: 0x0600D272 RID: 53874 RVA: 0x0012B735 File Offset: 0x00129935
			public virtual MultiValuedProperty<string> InPlaceHoldsRaw
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldsRaw"] = value;
				}
			}

			// Token: 0x1700A28F RID: 41615
			// (set) Token: 0x0600D273 RID: 53875 RVA: 0x0012B748 File Offset: 0x00129948
			public virtual ElcMailboxFlags ElcMailboxFlags
			{
				set
				{
					base.PowerSharpParameters["ElcMailboxFlags"] = value;
				}
			}

			// Token: 0x1700A290 RID: 41616
			// (set) Token: 0x0600D274 RID: 53876 RVA: 0x0012B760 File Offset: 0x00129960
			public virtual bool MailboxAuditEnabled
			{
				set
				{
					base.PowerSharpParameters["MailboxAuditEnabled"] = value;
				}
			}

			// Token: 0x1700A291 RID: 41617
			// (set) Token: 0x0600D275 RID: 53877 RVA: 0x0012B778 File Offset: 0x00129978
			public virtual EnhancedTimeSpan MailboxAuditLogAgeLimit
			{
				set
				{
					base.PowerSharpParameters["MailboxAuditLogAgeLimit"] = value;
				}
			}

			// Token: 0x1700A292 RID: 41618
			// (set) Token: 0x0600D276 RID: 53878 RVA: 0x0012B790 File Offset: 0x00129990
			public virtual MailboxAuditOperations AuditAdminOperations
			{
				set
				{
					base.PowerSharpParameters["AuditAdminOperations"] = value;
				}
			}

			// Token: 0x1700A293 RID: 41619
			// (set) Token: 0x0600D277 RID: 53879 RVA: 0x0012B7A8 File Offset: 0x001299A8
			public virtual MailboxAuditOperations AuditDelegateAdminOperations
			{
				set
				{
					base.PowerSharpParameters["AuditDelegateAdminOperations"] = value;
				}
			}

			// Token: 0x1700A294 RID: 41620
			// (set) Token: 0x0600D278 RID: 53880 RVA: 0x0012B7C0 File Offset: 0x001299C0
			public virtual MailboxAuditOperations AuditDelegateOperations
			{
				set
				{
					base.PowerSharpParameters["AuditDelegateOperations"] = value;
				}
			}

			// Token: 0x1700A295 RID: 41621
			// (set) Token: 0x0600D279 RID: 53881 RVA: 0x0012B7D8 File Offset: 0x001299D8
			public virtual MailboxAuditOperations AuditOwnerOperations
			{
				set
				{
					base.PowerSharpParameters["AuditOwnerOperations"] = value;
				}
			}

			// Token: 0x1700A296 RID: 41622
			// (set) Token: 0x0600D27A RID: 53882 RVA: 0x0012B7F0 File Offset: 0x001299F0
			public virtual bool BypassAudit
			{
				set
				{
					base.PowerSharpParameters["BypassAudit"] = value;
				}
			}

			// Token: 0x1700A297 RID: 41623
			// (set) Token: 0x0600D27B RID: 53883 RVA: 0x0012B808 File Offset: 0x00129A08
			public virtual DateTime? SiteMailboxClosedTime
			{
				set
				{
					base.PowerSharpParameters["SiteMailboxClosedTime"] = value;
				}
			}

			// Token: 0x1700A298 RID: 41624
			// (set) Token: 0x0600D27C RID: 53884 RVA: 0x0012B820 File Offset: 0x00129A20
			public virtual Uri SharePointUrl
			{
				set
				{
					base.PowerSharpParameters["SharePointUrl"] = value;
				}
			}

			// Token: 0x1700A299 RID: 41625
			// (set) Token: 0x0600D27D RID: 53885 RVA: 0x0012B833 File Offset: 0x00129A33
			public virtual bool AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x1700A29A RID: 41626
			// (set) Token: 0x0600D27E RID: 53886 RVA: 0x0012B84B File Offset: 0x00129A4B
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x1700A29B RID: 41627
			// (set) Token: 0x0600D27F RID: 53887 RVA: 0x0012B863 File Offset: 0x00129A63
			public virtual Guid ExchangeGuid
			{
				set
				{
					base.PowerSharpParameters["ExchangeGuid"] = value;
				}
			}

			// Token: 0x1700A29C RID: 41628
			// (set) Token: 0x0600D280 RID: 53888 RVA: 0x0012B87B File Offset: 0x00129A7B
			public virtual Guid? MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x1700A29D RID: 41629
			// (set) Token: 0x0600D281 RID: 53889 RVA: 0x0012B893 File Offset: 0x00129A93
			public virtual MultiValuedProperty<Guid> AggregatedMailboxGuids
			{
				set
				{
					base.PowerSharpParameters["AggregatedMailboxGuids"] = value;
				}
			}

			// Token: 0x1700A29E RID: 41630
			// (set) Token: 0x0600D282 RID: 53890 RVA: 0x0012B8A6 File Offset: 0x00129AA6
			public virtual Guid ArchiveGuid
			{
				set
				{
					base.PowerSharpParameters["ArchiveGuid"] = value;
				}
			}

			// Token: 0x1700A29F RID: 41631
			// (set) Token: 0x0600D283 RID: 53891 RVA: 0x0012B8BE File Offset: 0x00129ABE
			public virtual MultiValuedProperty<string> ArchiveName
			{
				set
				{
					base.PowerSharpParameters["ArchiveName"] = value;
				}
			}

			// Token: 0x1700A2A0 RID: 41632
			// (set) Token: 0x0600D284 RID: 53892 RVA: 0x0012B8D1 File Offset: 0x00129AD1
			public virtual Unlimited<ByteQuantifiedSize> ArchiveQuota
			{
				set
				{
					base.PowerSharpParameters["ArchiveQuota"] = value;
				}
			}

			// Token: 0x1700A2A1 RID: 41633
			// (set) Token: 0x0600D285 RID: 53893 RVA: 0x0012B8E9 File Offset: 0x00129AE9
			public virtual Unlimited<ByteQuantifiedSize> ArchiveWarningQuota
			{
				set
				{
					base.PowerSharpParameters["ArchiveWarningQuota"] = value;
				}
			}

			// Token: 0x1700A2A2 RID: 41634
			// (set) Token: 0x0600D286 RID: 53894 RVA: 0x0012B901 File Offset: 0x00129B01
			public virtual ProxyAddress ExternalEmailAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalEmailAddress"] = value;
				}
			}

			// Token: 0x1700A2A3 RID: 41635
			// (set) Token: 0x0600D287 RID: 53895 RVA: 0x0012B914 File Offset: 0x00129B14
			public virtual bool UsePreferMessageFormat
			{
				set
				{
					base.PowerSharpParameters["UsePreferMessageFormat"] = value;
				}
			}

			// Token: 0x1700A2A4 RID: 41636
			// (set) Token: 0x0600D288 RID: 53896 RVA: 0x0012B92C File Offset: 0x00129B2C
			public virtual SmtpAddress JournalArchiveAddress
			{
				set
				{
					base.PowerSharpParameters["JournalArchiveAddress"] = value;
				}
			}

			// Token: 0x1700A2A5 RID: 41637
			// (set) Token: 0x0600D289 RID: 53897 RVA: 0x0012B944 File Offset: 0x00129B44
			public virtual MessageFormat MessageFormat
			{
				set
				{
					base.PowerSharpParameters["MessageFormat"] = value;
				}
			}

			// Token: 0x1700A2A6 RID: 41638
			// (set) Token: 0x0600D28A RID: 53898 RVA: 0x0012B95C File Offset: 0x00129B5C
			public virtual MessageBodyFormat MessageBodyFormat
			{
				set
				{
					base.PowerSharpParameters["MessageBodyFormat"] = value;
				}
			}

			// Token: 0x1700A2A7 RID: 41639
			// (set) Token: 0x0600D28B RID: 53899 RVA: 0x0012B974 File Offset: 0x00129B74
			public virtual MacAttachmentFormat MacAttachmentFormat
			{
				set
				{
					base.PowerSharpParameters["MacAttachmentFormat"] = value;
				}
			}

			// Token: 0x1700A2A8 RID: 41640
			// (set) Token: 0x0600D28C RID: 53900 RVA: 0x0012B98C File Offset: 0x00129B8C
			public virtual Unlimited<int> RecipientLimits
			{
				set
				{
					base.PowerSharpParameters["RecipientLimits"] = value;
				}
			}

			// Token: 0x1700A2A9 RID: 41641
			// (set) Token: 0x0600D28D RID: 53901 RVA: 0x0012B9A4 File Offset: 0x00129BA4
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x1700A2AA RID: 41642
			// (set) Token: 0x0600D28E RID: 53902 RVA: 0x0012B9B7 File Offset: 0x00129BB7
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x1700A2AB RID: 41643
			// (set) Token: 0x0600D28F RID: 53903 RVA: 0x0012B9CF File Offset: 0x00129BCF
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x1700A2AC RID: 41644
			// (set) Token: 0x0600D290 RID: 53904 RVA: 0x0012B9E2 File Offset: 0x00129BE2
			public virtual SmtpAddress WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x1700A2AD RID: 41645
			// (set) Token: 0x0600D291 RID: 53905 RVA: 0x0012B9FA File Offset: 0x00129BFA
			public virtual SmtpAddress MicrosoftOnlineServicesID
			{
				set
				{
					base.PowerSharpParameters["MicrosoftOnlineServicesID"] = value;
				}
			}

			// Token: 0x1700A2AE RID: 41646
			// (set) Token: 0x0600D292 RID: 53906 RVA: 0x0012BA12 File Offset: 0x00129C12
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x1700A2AF RID: 41647
			// (set) Token: 0x0600D293 RID: 53907 RVA: 0x0012BA25 File Offset: 0x00129C25
			public virtual bool? SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x1700A2B0 RID: 41648
			// (set) Token: 0x0600D294 RID: 53908 RVA: 0x0012BA3D File Offset: 0x00129C3D
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x1700A2B1 RID: 41649
			// (set) Token: 0x0600D295 RID: 53909 RVA: 0x0012BA55 File Offset: 0x00129C55
			public virtual bool LitigationHoldEnabled
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldEnabled"] = value;
				}
			}

			// Token: 0x1700A2B2 RID: 41650
			// (set) Token: 0x0600D296 RID: 53910 RVA: 0x0012BA6D File Offset: 0x00129C6D
			public virtual bool SingleItemRecoveryEnabled
			{
				set
				{
					base.PowerSharpParameters["SingleItemRecoveryEnabled"] = value;
				}
			}

			// Token: 0x1700A2B3 RID: 41651
			// (set) Token: 0x0600D297 RID: 53911 RVA: 0x0012BA85 File Offset: 0x00129C85
			public virtual bool RetentionHoldEnabled
			{
				set
				{
					base.PowerSharpParameters["RetentionHoldEnabled"] = value;
				}
			}

			// Token: 0x1700A2B4 RID: 41652
			// (set) Token: 0x0600D298 RID: 53912 RVA: 0x0012BA9D File Offset: 0x00129C9D
			public virtual DateTime? EndDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["EndDateForRetentionHold"] = value;
				}
			}

			// Token: 0x1700A2B5 RID: 41653
			// (set) Token: 0x0600D299 RID: 53913 RVA: 0x0012BAB5 File Offset: 0x00129CB5
			public virtual DateTime? StartDateForRetentionHold
			{
				set
				{
					base.PowerSharpParameters["StartDateForRetentionHold"] = value;
				}
			}

			// Token: 0x1700A2B6 RID: 41654
			// (set) Token: 0x0600D29A RID: 53914 RVA: 0x0012BACD File Offset: 0x00129CCD
			public virtual string RetentionComment
			{
				set
				{
					base.PowerSharpParameters["RetentionComment"] = value;
				}
			}

			// Token: 0x1700A2B7 RID: 41655
			// (set) Token: 0x0600D29B RID: 53915 RVA: 0x0012BAE0 File Offset: 0x00129CE0
			public virtual string RetentionUrl
			{
				set
				{
					base.PowerSharpParameters["RetentionUrl"] = value;
				}
			}

			// Token: 0x1700A2B8 RID: 41656
			// (set) Token: 0x0600D29C RID: 53916 RVA: 0x0012BAF3 File Offset: 0x00129CF3
			public virtual DateTime? LitigationHoldDate
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldDate"] = value;
				}
			}

			// Token: 0x1700A2B9 RID: 41657
			// (set) Token: 0x0600D29D RID: 53917 RVA: 0x0012BB0B File Offset: 0x00129D0B
			public virtual string LitigationHoldOwner
			{
				set
				{
					base.PowerSharpParameters["LitigationHoldOwner"] = value;
				}
			}

			// Token: 0x1700A2BA RID: 41658
			// (set) Token: 0x0600D29E RID: 53918 RVA: 0x0012BB1E File Offset: 0x00129D1E
			public virtual EnhancedTimeSpan RetainDeletedItemsFor
			{
				set
				{
					base.PowerSharpParameters["RetainDeletedItemsFor"] = value;
				}
			}

			// Token: 0x1700A2BB RID: 41659
			// (set) Token: 0x0600D29F RID: 53919 RVA: 0x0012BB36 File Offset: 0x00129D36
			public virtual bool CalendarVersionStoreDisabled
			{
				set
				{
					base.PowerSharpParameters["CalendarVersionStoreDisabled"] = value;
				}
			}

			// Token: 0x1700A2BC RID: 41660
			// (set) Token: 0x0600D2A0 RID: 53920 RVA: 0x0012BB4E File Offset: 0x00129D4E
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x1700A2BD RID: 41661
			// (set) Token: 0x0600D2A1 RID: 53921 RVA: 0x0012BB61 File Offset: 0x00129D61
			public virtual Unlimited<ByteQuantifiedSize> RecoverableItemsQuota
			{
				set
				{
					base.PowerSharpParameters["RecoverableItemsQuota"] = value;
				}
			}

			// Token: 0x1700A2BE RID: 41662
			// (set) Token: 0x0600D2A2 RID: 53922 RVA: 0x0012BB79 File Offset: 0x00129D79
			public virtual Unlimited<ByteQuantifiedSize> RecoverableItemsWarningQuota
			{
				set
				{
					base.PowerSharpParameters["RecoverableItemsWarningQuota"] = value;
				}
			}

			// Token: 0x1700A2BF RID: 41663
			// (set) Token: 0x0600D2A3 RID: 53923 RVA: 0x0012BB91 File Offset: 0x00129D91
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x1700A2C0 RID: 41664
			// (set) Token: 0x0600D2A4 RID: 53924 RVA: 0x0012BBA4 File Offset: 0x00129DA4
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x1700A2C1 RID: 41665
			// (set) Token: 0x0600D2A5 RID: 53925 RVA: 0x0012BBB7 File Offset: 0x00129DB7
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700A2C2 RID: 41666
			// (set) Token: 0x0600D2A6 RID: 53926 RVA: 0x0012BBCA File Offset: 0x00129DCA
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x1700A2C3 RID: 41667
			// (set) Token: 0x0600D2A7 RID: 53927 RVA: 0x0012BBDD File Offset: 0x00129DDD
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x1700A2C4 RID: 41668
			// (set) Token: 0x0600D2A8 RID: 53928 RVA: 0x0012BBF0 File Offset: 0x00129DF0
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x1700A2C5 RID: 41669
			// (set) Token: 0x0600D2A9 RID: 53929 RVA: 0x0012BC03 File Offset: 0x00129E03
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x1700A2C6 RID: 41670
			// (set) Token: 0x0600D2AA RID: 53930 RVA: 0x0012BC16 File Offset: 0x00129E16
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x1700A2C7 RID: 41671
			// (set) Token: 0x0600D2AB RID: 53931 RVA: 0x0012BC29 File Offset: 0x00129E29
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x1700A2C8 RID: 41672
			// (set) Token: 0x0600D2AC RID: 53932 RVA: 0x0012BC3C File Offset: 0x00129E3C
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x1700A2C9 RID: 41673
			// (set) Token: 0x0600D2AD RID: 53933 RVA: 0x0012BC4F File Offset: 0x00129E4F
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x1700A2CA RID: 41674
			// (set) Token: 0x0600D2AE RID: 53934 RVA: 0x0012BC62 File Offset: 0x00129E62
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x1700A2CB RID: 41675
			// (set) Token: 0x0600D2AF RID: 53935 RVA: 0x0012BC75 File Offset: 0x00129E75
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x1700A2CC RID: 41676
			// (set) Token: 0x0600D2B0 RID: 53936 RVA: 0x0012BC88 File Offset: 0x00129E88
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x1700A2CD RID: 41677
			// (set) Token: 0x0600D2B1 RID: 53937 RVA: 0x0012BC9B File Offset: 0x00129E9B
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x1700A2CE RID: 41678
			// (set) Token: 0x0600D2B2 RID: 53938 RVA: 0x0012BCAE File Offset: 0x00129EAE
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x1700A2CF RID: 41679
			// (set) Token: 0x0600D2B3 RID: 53939 RVA: 0x0012BCC1 File Offset: 0x00129EC1
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x1700A2D0 RID: 41680
			// (set) Token: 0x0600D2B4 RID: 53940 RVA: 0x0012BCD4 File Offset: 0x00129ED4
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x1700A2D1 RID: 41681
			// (set) Token: 0x0600D2B5 RID: 53941 RVA: 0x0012BCE7 File Offset: 0x00129EE7
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x1700A2D2 RID: 41682
			// (set) Token: 0x0600D2B6 RID: 53942 RVA: 0x0012BCFA File Offset: 0x00129EFA
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x1700A2D3 RID: 41683
			// (set) Token: 0x0600D2B7 RID: 53943 RVA: 0x0012BD0D File Offset: 0x00129F0D
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x1700A2D4 RID: 41684
			// (set) Token: 0x0600D2B8 RID: 53944 RVA: 0x0012BD20 File Offset: 0x00129F20
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x1700A2D5 RID: 41685
			// (set) Token: 0x0600D2B9 RID: 53945 RVA: 0x0012BD33 File Offset: 0x00129F33
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x1700A2D6 RID: 41686
			// (set) Token: 0x0600D2BA RID: 53946 RVA: 0x0012BD46 File Offset: 0x00129F46
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700A2D7 RID: 41687
			// (set) Token: 0x0600D2BB RID: 53947 RVA: 0x0012BD59 File Offset: 0x00129F59
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x1700A2D8 RID: 41688
			// (set) Token: 0x0600D2BC RID: 53948 RVA: 0x0012BD6C File Offset: 0x00129F6C
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x1700A2D9 RID: 41689
			// (set) Token: 0x0600D2BD RID: 53949 RVA: 0x0012BD84 File Offset: 0x00129F84
			public virtual Unlimited<ByteQuantifiedSize> MaxSendSize
			{
				set
				{
					base.PowerSharpParameters["MaxSendSize"] = value;
				}
			}

			// Token: 0x1700A2DA RID: 41690
			// (set) Token: 0x0600D2BE RID: 53950 RVA: 0x0012BD9C File Offset: 0x00129F9C
			public virtual Unlimited<ByteQuantifiedSize> MaxReceiveSize
			{
				set
				{
					base.PowerSharpParameters["MaxReceiveSize"] = value;
				}
			}

			// Token: 0x1700A2DB RID: 41691
			// (set) Token: 0x0600D2BF RID: 53951 RVA: 0x0012BDB4 File Offset: 0x00129FB4
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x1700A2DC RID: 41692
			// (set) Token: 0x0600D2C0 RID: 53952 RVA: 0x0012BDCC File Offset: 0x00129FCC
			public virtual bool EmailAddressPolicyEnabled
			{
				set
				{
					base.PowerSharpParameters["EmailAddressPolicyEnabled"] = value;
				}
			}

			// Token: 0x1700A2DD RID: 41693
			// (set) Token: 0x0600D2C1 RID: 53953 RVA: 0x0012BDE4 File Offset: 0x00129FE4
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x1700A2DE RID: 41694
			// (set) Token: 0x0600D2C2 RID: 53954 RVA: 0x0012BDFC File Offset: 0x00129FFC
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x1700A2DF RID: 41695
			// (set) Token: 0x0600D2C3 RID: 53955 RVA: 0x0012BE14 File Offset: 0x0012A014
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x1700A2E0 RID: 41696
			// (set) Token: 0x0600D2C4 RID: 53956 RVA: 0x0012BE27 File Offset: 0x0012A027
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x1700A2E1 RID: 41697
			// (set) Token: 0x0600D2C5 RID: 53957 RVA: 0x0012BE3F File Offset: 0x0012A03F
			public virtual MultiValuedProperty<string> UMDtmfMap
			{
				set
				{
					base.PowerSharpParameters["UMDtmfMap"] = value;
				}
			}

			// Token: 0x1700A2E2 RID: 41698
			// (set) Token: 0x0600D2C6 RID: 53958 RVA: 0x0012BE52 File Offset: 0x0012A052
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x1700A2E3 RID: 41699
			// (set) Token: 0x0600D2C7 RID: 53959 RVA: 0x0012BE6A File Offset: 0x0012A06A
			public virtual string MailTip
			{
				set
				{
					base.PowerSharpParameters["MailTip"] = value;
				}
			}

			// Token: 0x1700A2E4 RID: 41700
			// (set) Token: 0x0600D2C8 RID: 53960 RVA: 0x0012BE7D File Offset: 0x0012A07D
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x1700A2E5 RID: 41701
			// (set) Token: 0x0600D2C9 RID: 53961 RVA: 0x0012BE90 File Offset: 0x0012A090
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700A2E6 RID: 41702
			// (set) Token: 0x0600D2CA RID: 53962 RVA: 0x0012BEA3 File Offset: 0x0012A0A3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A2E7 RID: 41703
			// (set) Token: 0x0600D2CB RID: 53963 RVA: 0x0012BEBB File Offset: 0x0012A0BB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A2E8 RID: 41704
			// (set) Token: 0x0600D2CC RID: 53964 RVA: 0x0012BED3 File Offset: 0x0012A0D3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A2E9 RID: 41705
			// (set) Token: 0x0600D2CD RID: 53965 RVA: 0x0012BEEB File Offset: 0x0012A0EB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A2EA RID: 41706
			// (set) Token: 0x0600D2CE RID: 53966 RVA: 0x0012BF03 File Offset: 0x0012A103
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
