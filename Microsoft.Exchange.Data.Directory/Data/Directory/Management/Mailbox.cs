using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using System.Reflection;
using System.Security.AccessControl;
using Microsoft.Exchange.Data.Directory.Provisioning;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000729 RID: 1833
	[ProvisioningObjectTag("Mailbox")]
	[Serializable]
	public class Mailbox : MailEnabledOrgPerson
	{
		// Token: 0x17001D2E RID: 7470
		// (get) Token: 0x060056D8 RID: 22232 RVA: 0x0013872E File Offset: 0x0013692E
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return Mailbox.schema;
			}
		}

		// Token: 0x060056D9 RID: 22233 RVA: 0x00138735 File Offset: 0x00136935
		public Mailbox()
		{
			base.SetObjectClass("user");
		}

		// Token: 0x060056DA RID: 22234 RVA: 0x00138748 File Offset: 0x00136948
		public Mailbox(ADUser dataObject) : base(dataObject)
		{
		}

		// Token: 0x060056DB RID: 22235 RVA: 0x00138751 File Offset: 0x00136951
		internal static Mailbox FromDataObject(ADUser dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			return new Mailbox(dataObject);
		}

		// Token: 0x17001D2F RID: 7471
		// (get) Token: 0x060056DC RID: 22236 RVA: 0x0013875E File Offset: 0x0013695E
		protected override IEnumerable<PropertyInfo> CloneableProperties
		{
			get
			{
				IEnumerable<PropertyInfo> result;
				if ((result = Mailbox.cloneableProps) == null)
				{
					result = (Mailbox.cloneableProps = ADPresentationObject.GetCloneableProperties(this));
				}
				return result;
			}
		}

		// Token: 0x17001D30 RID: 7472
		// (get) Token: 0x060056DD RID: 22237 RVA: 0x00138775 File Offset: 0x00136975
		protected override IEnumerable<PropertyInfo> CloneableOnceProperties
		{
			get
			{
				IEnumerable<PropertyInfo> result;
				if ((result = Mailbox.cloneableOnceProps) == null)
				{
					result = (Mailbox.cloneableOnceProps = ADPresentationObject.GetCloneableOnceProperties(this));
				}
				return result;
			}
		}

		// Token: 0x17001D31 RID: 7473
		// (get) Token: 0x060056DE RID: 22238 RVA: 0x0013878C File Offset: 0x0013698C
		protected override IEnumerable<PropertyInfo> CloneableEnabledStateProperties
		{
			get
			{
				IEnumerable<PropertyInfo> result;
				if ((result = Mailbox.cloneableEnabledStateProps) == null)
				{
					result = (Mailbox.cloneableEnabledStateProps = ADPresentationObject.GetCloneableEnabledStateProperties(this));
				}
				return result;
			}
		}

		// Token: 0x17001D32 RID: 7474
		// (get) Token: 0x060056DF RID: 22239 RVA: 0x001387A3 File Offset: 0x001369A3
		// (set) Token: 0x060056E0 RID: 22240 RVA: 0x001387B5 File Offset: 0x001369B5
		public ADObjectId Database
		{
			get
			{
				return (ADObjectId)this[MailboxSchema.Database];
			}
			internal set
			{
				this[MailboxSchema.Database] = value;
			}
		}

		// Token: 0x17001D33 RID: 7475
		// (get) Token: 0x060056E1 RID: 22241 RVA: 0x001387C3 File Offset: 0x001369C3
		public MailboxProvisioningConstraint MailboxProvisioningConstraint
		{
			get
			{
				return (MailboxProvisioningConstraint)this[MailboxSchema.MailboxProvisioningConstraint];
			}
		}

		// Token: 0x17001D34 RID: 7476
		// (get) Token: 0x060056E2 RID: 22242 RVA: 0x001387D5 File Offset: 0x001369D5
		public bool MessageCopyForSentAsEnabled
		{
			get
			{
				return (bool)this[MailboxSchema.MessageCopyForSentAsEnabled];
			}
		}

		// Token: 0x17001D35 RID: 7477
		// (get) Token: 0x060056E3 RID: 22243 RVA: 0x001387E7 File Offset: 0x001369E7
		public bool MessageCopyForSendOnBehalfEnabled
		{
			get
			{
				return (bool)this[MailboxSchema.MessageCopyForSendOnBehalfEnabled];
			}
		}

		// Token: 0x17001D36 RID: 7478
		// (get) Token: 0x060056E4 RID: 22244 RVA: 0x001387F9 File Offset: 0x001369F9
		public MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
		{
			get
			{
				return (MultiValuedProperty<MailboxProvisioningConstraint>)this[MailboxSchema.MailboxProvisioningPreferences];
			}
		}

		// Token: 0x17001D37 RID: 7479
		// (get) Token: 0x060056E5 RID: 22245 RVA: 0x0013880B File Offset: 0x00136A0B
		// (set) Token: 0x060056E6 RID: 22246 RVA: 0x0013881D File Offset: 0x00136A1D
		internal ADObjectId PreviousDatabase
		{
			get
			{
				return (ADObjectId)this[MailboxSchema.PreviousDatabase];
			}
			set
			{
				this[MailboxSchema.PreviousDatabase] = value;
			}
		}

		// Token: 0x17001D38 RID: 7480
		// (get) Token: 0x060056E7 RID: 22247 RVA: 0x0013882B File Offset: 0x00136A2B
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ADRecipient.PublicFolderMailboxObjectVersion;
			}
		}

		// Token: 0x17001D39 RID: 7481
		// (get) Token: 0x060056E8 RID: 22248 RVA: 0x00138832 File Offset: 0x00136A32
		// (set) Token: 0x060056E9 RID: 22249 RVA: 0x00138844 File Offset: 0x00136A44
		[Parameter(Mandatory = false)]
		[ProvisionalCloneOnce(CloneSet.CloneLimitedSet)]
		public bool UseDatabaseRetentionDefaults
		{
			get
			{
				return (bool)this[MailboxSchema.UseDatabaseRetentionDefaults];
			}
			set
			{
				this[MailboxSchema.UseDatabaseRetentionDefaults] = value;
			}
		}

		// Token: 0x17001D3A RID: 7482
		// (get) Token: 0x060056EA RID: 22250 RVA: 0x00138857 File Offset: 0x00136A57
		// (set) Token: 0x060056EB RID: 22251 RVA: 0x00138869 File Offset: 0x00136A69
		[Parameter(Mandatory = false)]
		public bool RetainDeletedItemsUntilBackup
		{
			get
			{
				return (bool)this[MailboxSchema.RetainDeletedItemsUntilBackup];
			}
			set
			{
				this[MailboxSchema.RetainDeletedItemsUntilBackup] = value;
			}
		}

		// Token: 0x17001D3B RID: 7483
		// (get) Token: 0x060056EC RID: 22252 RVA: 0x0013887C File Offset: 0x00136A7C
		// (set) Token: 0x060056ED RID: 22253 RVA: 0x0013888E File Offset: 0x00136A8E
		[Parameter(Mandatory = false)]
		public bool DeliverToMailboxAndForward
		{
			get
			{
				return (bool)this[MailboxSchema.DeliverToMailboxAndForward];
			}
			set
			{
				this[MailboxSchema.DeliverToMailboxAndForward] = value;
			}
		}

		// Token: 0x17001D3C RID: 7484
		// (get) Token: 0x060056EE RID: 22254 RVA: 0x001388A1 File Offset: 0x00136AA1
		// (set) Token: 0x060056EF RID: 22255 RVA: 0x001388B3 File Offset: 0x00136AB3
		[Parameter(Mandatory = false)]
		public bool IsExcludedFromServingHierarchy
		{
			get
			{
				return (bool)this[MailboxSchema.IsExcludedFromServingHierarchy];
			}
			set
			{
				this[MailboxSchema.IsExcludedFromServingHierarchy] = value;
			}
		}

		// Token: 0x17001D3D RID: 7485
		// (get) Token: 0x060056F0 RID: 22256 RVA: 0x001388C6 File Offset: 0x00136AC6
		// (set) Token: 0x060056F1 RID: 22257 RVA: 0x001388D8 File Offset: 0x00136AD8
		[Parameter(Mandatory = false)]
		public bool IsHierarchyReady
		{
			get
			{
				return (bool)this[MailboxSchema.IsHierarchyReady];
			}
			set
			{
				this[MailboxSchema.IsHierarchyReady] = value;
			}
		}

		// Token: 0x17001D3E RID: 7486
		// (get) Token: 0x060056F2 RID: 22258 RVA: 0x001388EB File Offset: 0x00136AEB
		// (set) Token: 0x060056F3 RID: 22259 RVA: 0x001388FD File Offset: 0x00136AFD
		[Parameter(Mandatory = false)]
		public bool LitigationHoldEnabled
		{
			get
			{
				return (bool)this[MailboxSchema.LitigationHoldEnabled];
			}
			set
			{
				this[MailboxSchema.LitigationHoldEnabled] = value;
			}
		}

		// Token: 0x17001D3F RID: 7487
		// (get) Token: 0x060056F4 RID: 22260 RVA: 0x00138910 File Offset: 0x00136B10
		// (set) Token: 0x060056F5 RID: 22261 RVA: 0x00138922 File Offset: 0x00136B22
		[ProvisionalCloneOnce(CloneSet.CloneExtendedSet)]
		[Parameter(Mandatory = false)]
		public bool SingleItemRecoveryEnabled
		{
			get
			{
				return (bool)this[MailboxSchema.SingleItemRecoveryEnabled];
			}
			set
			{
				this[MailboxSchema.SingleItemRecoveryEnabled] = value;
			}
		}

		// Token: 0x17001D40 RID: 7488
		// (get) Token: 0x060056F6 RID: 22262 RVA: 0x00138935 File Offset: 0x00136B35
		// (set) Token: 0x060056F7 RID: 22263 RVA: 0x00138947 File Offset: 0x00136B47
		[Parameter(Mandatory = false)]
		public bool RetentionHoldEnabled
		{
			get
			{
				return (bool)this[MailboxSchema.ElcExpirationSuspensionEnabled];
			}
			set
			{
				this[MailboxSchema.ElcExpirationSuspensionEnabled] = value;
			}
		}

		// Token: 0x17001D41 RID: 7489
		// (get) Token: 0x060056F8 RID: 22264 RVA: 0x0013895A File Offset: 0x00136B5A
		// (set) Token: 0x060056F9 RID: 22265 RVA: 0x0013896C File Offset: 0x00136B6C
		[Parameter(Mandatory = false)]
		public DateTime? EndDateForRetentionHold
		{
			get
			{
				return (DateTime?)this[MailboxSchema.ElcExpirationSuspensionEndDate];
			}
			set
			{
				this[MailboxSchema.ElcExpirationSuspensionEndDate] = value;
			}
		}

		// Token: 0x17001D42 RID: 7490
		// (get) Token: 0x060056FA RID: 22266 RVA: 0x0013897F File Offset: 0x00136B7F
		// (set) Token: 0x060056FB RID: 22267 RVA: 0x00138991 File Offset: 0x00136B91
		[Parameter(Mandatory = false)]
		public DateTime? StartDateForRetentionHold
		{
			get
			{
				return (DateTime?)this[MailboxSchema.ElcExpirationSuspensionStartDate];
			}
			set
			{
				this[MailboxSchema.ElcExpirationSuspensionStartDate] = value;
			}
		}

		// Token: 0x17001D43 RID: 7491
		// (get) Token: 0x060056FC RID: 22268 RVA: 0x001389A4 File Offset: 0x00136BA4
		// (set) Token: 0x060056FD RID: 22269 RVA: 0x001389B6 File Offset: 0x00136BB6
		[Parameter(Mandatory = false)]
		public string RetentionComment
		{
			get
			{
				return (string)this[MailboxSchema.RetentionComment];
			}
			set
			{
				this[MailboxSchema.RetentionComment] = value;
			}
		}

		// Token: 0x17001D44 RID: 7492
		// (get) Token: 0x060056FE RID: 22270 RVA: 0x001389C4 File Offset: 0x00136BC4
		// (set) Token: 0x060056FF RID: 22271 RVA: 0x001389D6 File Offset: 0x00136BD6
		[Parameter(Mandatory = false)]
		public string RetentionUrl
		{
			get
			{
				return (string)this[MailboxSchema.RetentionUrl];
			}
			set
			{
				this[MailboxSchema.RetentionUrl] = value;
			}
		}

		// Token: 0x17001D45 RID: 7493
		// (get) Token: 0x06005700 RID: 22272 RVA: 0x001389E4 File Offset: 0x00136BE4
		// (set) Token: 0x06005701 RID: 22273 RVA: 0x001389F6 File Offset: 0x00136BF6
		[Parameter(Mandatory = false)]
		public DateTime? LitigationHoldDate
		{
			get
			{
				return (DateTime?)this[MailboxSchema.LitigationHoldDate];
			}
			set
			{
				this[MailboxSchema.LitigationHoldDate] = value;
			}
		}

		// Token: 0x17001D46 RID: 7494
		// (get) Token: 0x06005702 RID: 22274 RVA: 0x00138A09 File Offset: 0x00136C09
		// (set) Token: 0x06005703 RID: 22275 RVA: 0x00138A1B File Offset: 0x00136C1B
		[Parameter(Mandatory = false)]
		public string LitigationHoldOwner
		{
			get
			{
				return (string)this[MailboxSchema.LitigationHoldOwner];
			}
			set
			{
				this[MailboxSchema.LitigationHoldOwner] = value;
			}
		}

		// Token: 0x17001D47 RID: 7495
		// (get) Token: 0x06005704 RID: 22276 RVA: 0x00138A2C File Offset: 0x00136C2C
		// (set) Token: 0x06005705 RID: 22277 RVA: 0x00138A5F File Offset: 0x00136C5F
		public Unlimited<EnhancedTimeSpan>? LitigationHoldDuration
		{
			get
			{
				Unlimited<EnhancedTimeSpan>? result = (Unlimited<EnhancedTimeSpan>?)this[MailboxSchema.LitigationHoldDuration];
				if (result == null)
				{
					return new Unlimited<EnhancedTimeSpan>?(Unlimited<EnhancedTimeSpan>.UnlimitedValue);
				}
				return result;
			}
			set
			{
				this[MailboxSchema.LitigationHoldDuration] = value;
			}
		}

		// Token: 0x17001D48 RID: 7496
		// (get) Token: 0x06005706 RID: 22278 RVA: 0x00138A72 File Offset: 0x00136C72
		// (set) Token: 0x06005707 RID: 22279 RVA: 0x00138A84 File Offset: 0x00136C84
		public ADObjectId ManagedFolderMailboxPolicy
		{
			get
			{
				return (ADObjectId)this[MailboxSchema.ManagedFolderMailboxPolicy];
			}
			set
			{
				this[MailboxSchema.ManagedFolderMailboxPolicy] = value;
			}
		}

		// Token: 0x17001D49 RID: 7497
		// (get) Token: 0x06005708 RID: 22280 RVA: 0x00138A92 File Offset: 0x00136C92
		// (set) Token: 0x06005709 RID: 22281 RVA: 0x00138AA4 File Offset: 0x00136CA4
		[ProvisionalCloneOnce(CloneSet.CloneLimitedSet)]
		public ADObjectId RetentionPolicy
		{
			get
			{
				return (ADObjectId)this[MailboxSchema.RetentionPolicy];
			}
			set
			{
				this[MailboxSchema.RetentionPolicy] = value;
			}
		}

		// Token: 0x17001D4A RID: 7498
		// (get) Token: 0x0600570A RID: 22282 RVA: 0x00138AB2 File Offset: 0x00136CB2
		// (set) Token: 0x0600570B RID: 22283 RVA: 0x00138AC4 File Offset: 0x00136CC4
		public ADObjectId AddressBookPolicy
		{
			get
			{
				return (ADObjectId)this[MailboxSchema.AddressBookPolicy];
			}
			set
			{
				this[MailboxSchema.AddressBookPolicy] = value;
			}
		}

		// Token: 0x17001D4B RID: 7499
		// (get) Token: 0x0600570C RID: 22284 RVA: 0x00138AD2 File Offset: 0x00136CD2
		// (set) Token: 0x0600570D RID: 22285 RVA: 0x00138AE4 File Offset: 0x00136CE4
		internal bool ShouldUseDefaultRetentionPolicy
		{
			get
			{
				return (bool)this[MailboxSchema.ShouldUseDefaultRetentionPolicy];
			}
			set
			{
				this[MailboxSchema.ShouldUseDefaultRetentionPolicy] = value;
			}
		}

		// Token: 0x17001D4C RID: 7500
		// (get) Token: 0x0600570E RID: 22286 RVA: 0x00138AF7 File Offset: 0x00136CF7
		// (set) Token: 0x0600570F RID: 22287 RVA: 0x00138B09 File Offset: 0x00136D09
		[Parameter(Mandatory = false)]
		public bool CalendarRepairDisabled
		{
			get
			{
				return (bool)this[MailboxSchema.CalendarRepairDisabled];
			}
			set
			{
				this[MailboxSchema.CalendarRepairDisabled] = value;
			}
		}

		// Token: 0x17001D4D RID: 7501
		// (get) Token: 0x06005710 RID: 22288 RVA: 0x00138B1C File Offset: 0x00136D1C
		public Guid ExchangeGuid
		{
			get
			{
				return (Guid)this[MailboxSchema.ExchangeGuid];
			}
		}

		// Token: 0x17001D4E RID: 7502
		// (get) Token: 0x06005711 RID: 22289 RVA: 0x00138B2E File Offset: 0x00136D2E
		// (set) Token: 0x06005712 RID: 22290 RVA: 0x00138B40 File Offset: 0x00136D40
		public Guid? MailboxContainerGuid
		{
			get
			{
				return (Guid?)this[MailboxSchema.MailboxContainerGuid];
			}
			set
			{
				this[MailboxSchema.MailboxContainerGuid] = value;
			}
		}

		// Token: 0x17001D4F RID: 7503
		// (get) Token: 0x06005713 RID: 22291 RVA: 0x00138B53 File Offset: 0x00136D53
		// (set) Token: 0x06005714 RID: 22292 RVA: 0x00138B65 File Offset: 0x00136D65
		public CrossTenantObjectId UnifiedMailbox
		{
			get
			{
				return (CrossTenantObjectId)this[MailboxSchema.UnifiedMailbox];
			}
			set
			{
				this[MailboxSchema.UnifiedMailbox] = value;
			}
		}

		// Token: 0x17001D50 RID: 7504
		// (get) Token: 0x06005715 RID: 22293 RVA: 0x00138B74 File Offset: 0x00136D74
		public IList<IMailboxLocationInfo> MailboxLocations
		{
			get
			{
				MailboxLocationCollection mailboxLocationCollection = (MailboxLocationCollection)this[MailboxSchema.MailboxLocations];
				if (mailboxLocationCollection != null)
				{
					return mailboxLocationCollection.GetMailboxLocations();
				}
				return Mailbox.EmptyMailboxLocationInfo;
			}
		}

		// Token: 0x17001D51 RID: 7505
		// (get) Token: 0x06005716 RID: 22294 RVA: 0x00138BA1 File Offset: 0x00136DA1
		public MultiValuedProperty<Guid> AggregatedMailboxGuids
		{
			get
			{
				return (MultiValuedProperty<Guid>)this[MailboxSchema.AggregatedMailboxGuids];
			}
		}

		// Token: 0x17001D52 RID: 7506
		// (get) Token: 0x06005717 RID: 22295 RVA: 0x00138BB3 File Offset: 0x00136DB3
		public RawSecurityDescriptor ExchangeSecurityDescriptor
		{
			get
			{
				return (RawSecurityDescriptor)this[MailboxSchema.ExchangeSecurityDescriptor];
			}
		}

		// Token: 0x17001D53 RID: 7507
		// (get) Token: 0x06005718 RID: 22296 RVA: 0x00138BC5 File Offset: 0x00136DC5
		public UserAccountControlFlags ExchangeUserAccountControl
		{
			get
			{
				return (UserAccountControlFlags)this[MailboxSchema.ExchangeUserAccountControl];
			}
		}

		// Token: 0x17001D54 RID: 7508
		// (get) Token: 0x06005719 RID: 22297 RVA: 0x00138BD7 File Offset: 0x00136DD7
		// (set) Token: 0x0600571A RID: 22298 RVA: 0x00138BE9 File Offset: 0x00136DE9
		public ServerVersion AdminDisplayVersion
		{
			get
			{
				return (ServerVersion)this[MailboxSchema.AdminDisplayVersion];
			}
			internal set
			{
				this[MailboxSchema.AdminDisplayVersion] = value;
			}
		}

		// Token: 0x17001D55 RID: 7509
		// (get) Token: 0x0600571B RID: 22299 RVA: 0x00138BF7 File Offset: 0x00136DF7
		// (set) Token: 0x0600571C RID: 22300 RVA: 0x00138C0C File Offset: 0x00136E0C
		[Parameter(Mandatory = false)]
		public bool MessageTrackingReadStatusEnabled
		{
			get
			{
				return !(bool)this[MailboxSchema.MessageTrackingReadStatusDisabled];
			}
			set
			{
				this[MailboxSchema.MessageTrackingReadStatusDisabled] = !value;
			}
		}

		// Token: 0x17001D56 RID: 7510
		// (get) Token: 0x0600571D RID: 22301 RVA: 0x00138C22 File Offset: 0x00136E22
		// (set) Token: 0x0600571E RID: 22302 RVA: 0x00138C34 File Offset: 0x00136E34
		[Parameter(Mandatory = false)]
		public ExternalOofOptions ExternalOofOptions
		{
			get
			{
				return (ExternalOofOptions)this[MailboxSchema.ExternalOofOptions];
			}
			set
			{
				this[MailboxSchema.ExternalOofOptions] = value;
			}
		}

		// Token: 0x17001D57 RID: 7511
		// (get) Token: 0x0600571F RID: 22303 RVA: 0x00138C47 File Offset: 0x00136E47
		// (set) Token: 0x06005720 RID: 22304 RVA: 0x00138C59 File Offset: 0x00136E59
		public ADObjectId ForwardingAddress
		{
			get
			{
				return (ADObjectId)this[MailboxSchema.ForwardingAddress];
			}
			set
			{
				this[MailboxSchema.ForwardingAddress] = value;
			}
		}

		// Token: 0x17001D58 RID: 7512
		// (get) Token: 0x06005721 RID: 22305 RVA: 0x00138C67 File Offset: 0x00136E67
		// (set) Token: 0x06005722 RID: 22306 RVA: 0x00138C79 File Offset: 0x00136E79
		[Parameter(Mandatory = false)]
		public ProxyAddress ForwardingSmtpAddress
		{
			get
			{
				return (ProxyAddress)this[MailboxSchema.ForwardingSmtpAddress];
			}
			set
			{
				this[MailboxSchema.ForwardingSmtpAddress] = value;
			}
		}

		// Token: 0x17001D59 RID: 7513
		// (get) Token: 0x06005723 RID: 22307 RVA: 0x00138C87 File Offset: 0x00136E87
		// (set) Token: 0x06005724 RID: 22308 RVA: 0x00138C99 File Offset: 0x00136E99
		[ProvisionalClone(CloneSet.CloneLimitedSet)]
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan RetainDeletedItemsFor
		{
			get
			{
				return (EnhancedTimeSpan)this[MailboxSchema.RetainDeletedItemsFor];
			}
			set
			{
				this[MailboxSchema.RetainDeletedItemsFor] = value;
			}
		}

		// Token: 0x17001D5A RID: 7514
		// (get) Token: 0x06005725 RID: 22309 RVA: 0x00138CAC File Offset: 0x00136EAC
		public bool IsMailboxEnabled
		{
			get
			{
				return (bool)this[MailboxSchema.IsMailboxEnabled];
			}
		}

		// Token: 0x17001D5B RID: 7515
		// (get) Token: 0x06005726 RID: 22310 RVA: 0x00138CBE File Offset: 0x00136EBE
		// (set) Token: 0x06005727 RID: 22311 RVA: 0x00138CD0 File Offset: 0x00136ED0
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<CultureInfo> Languages
		{
			get
			{
				return (MultiValuedProperty<CultureInfo>)this[MailboxSchema.Languages];
			}
			set
			{
				this[MailboxSchema.Languages] = value;
			}
		}

		// Token: 0x17001D5C RID: 7516
		// (get) Token: 0x06005728 RID: 22312 RVA: 0x00138CDE File Offset: 0x00136EDE
		// (set) Token: 0x06005729 RID: 22313 RVA: 0x00138CF0 File Offset: 0x00136EF0
		public ADObjectId OfflineAddressBook
		{
			get
			{
				return (ADObjectId)this[MailboxSchema.OfflineAddressBook];
			}
			set
			{
				this[MailboxSchema.OfflineAddressBook] = value;
			}
		}

		// Token: 0x17001D5D RID: 7517
		// (get) Token: 0x0600572A RID: 22314 RVA: 0x00138CFE File Offset: 0x00136EFE
		// (set) Token: 0x0600572B RID: 22315 RVA: 0x00138D10 File Offset: 0x00136F10
		[ProvisionalClone(CloneSet.CloneExtendedSet)]
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ProhibitSendQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxSchema.ProhibitSendQuota];
			}
			set
			{
				this[MailboxSchema.ProhibitSendQuota] = value;
			}
		}

		// Token: 0x17001D5E RID: 7518
		// (get) Token: 0x0600572C RID: 22316 RVA: 0x00138D23 File Offset: 0x00136F23
		// (set) Token: 0x0600572D RID: 22317 RVA: 0x00138D35 File Offset: 0x00136F35
		[Parameter(Mandatory = false)]
		[ProvisionalClone(CloneSet.CloneExtendedSet)]
		public Unlimited<ByteQuantifiedSize> ProhibitSendReceiveQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxSchema.ProhibitSendReceiveQuota];
			}
			set
			{
				this[MailboxSchema.ProhibitSendReceiveQuota] = value;
			}
		}

		// Token: 0x17001D5F RID: 7519
		// (get) Token: 0x0600572E RID: 22318 RVA: 0x00138D48 File Offset: 0x00136F48
		// (set) Token: 0x0600572F RID: 22319 RVA: 0x00138D5A File Offset: 0x00136F5A
		[ProvisionalClone(CloneSet.CloneExtendedSet)]
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> RecoverableItemsQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxSchema.RecoverableItemsQuota];
			}
			set
			{
				this[MailboxSchema.RecoverableItemsQuota] = value;
			}
		}

		// Token: 0x17001D60 RID: 7520
		// (get) Token: 0x06005730 RID: 22320 RVA: 0x00138D6D File Offset: 0x00136F6D
		// (set) Token: 0x06005731 RID: 22321 RVA: 0x00138D7F File Offset: 0x00136F7F
		[ProvisionalClone(CloneSet.CloneExtendedSet)]
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> RecoverableItemsWarningQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxSchema.RecoverableItemsWarningQuota];
			}
			set
			{
				this[MailboxSchema.RecoverableItemsWarningQuota] = value;
			}
		}

		// Token: 0x17001D61 RID: 7521
		// (get) Token: 0x06005732 RID: 22322 RVA: 0x00138D92 File Offset: 0x00136F92
		// (set) Token: 0x06005733 RID: 22323 RVA: 0x00138DA4 File Offset: 0x00136FA4
		[ProvisionalClone(CloneSet.CloneExtendedSet)]
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> CalendarLoggingQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxSchema.CalendarLoggingQuota];
			}
			set
			{
				this[MailboxSchema.CalendarLoggingQuota] = value;
			}
		}

		// Token: 0x17001D62 RID: 7522
		// (get) Token: 0x06005734 RID: 22324 RVA: 0x00138DB7 File Offset: 0x00136FB7
		// (set) Token: 0x06005735 RID: 22325 RVA: 0x00138DC9 File Offset: 0x00136FC9
		[Parameter(Mandatory = false)]
		public bool DowngradeHighPriorityMessagesEnabled
		{
			get
			{
				return (bool)this[MailboxSchema.DowngradeHighPriorityMessagesEnabled];
			}
			set
			{
				this[MailboxSchema.DowngradeHighPriorityMessagesEnabled] = value;
			}
		}

		// Token: 0x17001D63 RID: 7523
		// (get) Token: 0x06005736 RID: 22326 RVA: 0x00138DDC File Offset: 0x00136FDC
		public MultiValuedProperty<string> ProtocolSettings
		{
			get
			{
				return (MultiValuedProperty<string>)this[MailboxSchema.ProtocolSettings];
			}
		}

		// Token: 0x17001D64 RID: 7524
		// (get) Token: 0x06005737 RID: 22327 RVA: 0x00138DEE File Offset: 0x00136FEE
		// (set) Token: 0x06005738 RID: 22328 RVA: 0x00138E00 File Offset: 0x00137000
		[ProvisionalClone(CloneSet.CloneLimitedSet)]
		[Parameter(Mandatory = false)]
		public Unlimited<int> RecipientLimits
		{
			get
			{
				return (Unlimited<int>)this[MailboxSchema.RecipientLimits];
			}
			set
			{
				this[MailboxSchema.RecipientLimits] = value;
			}
		}

		// Token: 0x17001D65 RID: 7525
		// (get) Token: 0x06005739 RID: 22329 RVA: 0x00138E13 File Offset: 0x00137013
		// (set) Token: 0x0600573A RID: 22330 RVA: 0x00138E25 File Offset: 0x00137025
		[Parameter(Mandatory = false)]
		public bool ImListMigrationCompleted
		{
			get
			{
				return (bool)this[UMMailboxSchema.UCSImListMigrationCompleted];
			}
			set
			{
				this[UMMailboxSchema.UCSImListMigrationCompleted] = value;
			}
		}

		// Token: 0x17001D66 RID: 7526
		// (get) Token: 0x0600573B RID: 22331 RVA: 0x00138E38 File Offset: 0x00137038
		public bool IsResource
		{
			get
			{
				return (bool)this[MailboxSchema.IsResource];
			}
		}

		// Token: 0x17001D67 RID: 7527
		// (get) Token: 0x0600573C RID: 22332 RVA: 0x00138E4A File Offset: 0x0013704A
		public bool IsLinked
		{
			get
			{
				return (bool)this[MailboxSchema.IsLinked];
			}
		}

		// Token: 0x17001D68 RID: 7528
		// (get) Token: 0x0600573D RID: 22333 RVA: 0x00138E5C File Offset: 0x0013705C
		public bool IsShared
		{
			get
			{
				return (bool)this[MailboxSchema.IsShared];
			}
		}

		// Token: 0x17001D69 RID: 7529
		// (get) Token: 0x0600573E RID: 22334 RVA: 0x00138E6E File Offset: 0x0013706E
		// (set) Token: 0x0600573F RID: 22335 RVA: 0x00138E76 File Offset: 0x00137076
		public bool IsRootPublicFolderMailbox { get; internal set; }

		// Token: 0x17001D6A RID: 7530
		// (get) Token: 0x06005740 RID: 22336 RVA: 0x00138E7F File Offset: 0x0013707F
		public string LinkedMasterAccount
		{
			get
			{
				return (string)this[MailboxSchema.LinkedMasterAccount];
			}
		}

		// Token: 0x17001D6B RID: 7531
		// (get) Token: 0x06005741 RID: 22337 RVA: 0x00138E91 File Offset: 0x00137091
		// (set) Token: 0x06005742 RID: 22338 RVA: 0x00138EA3 File Offset: 0x001370A3
		[Parameter(Mandatory = false)]
		public bool ResetPasswordOnNextLogon
		{
			get
			{
				return (bool)this[MailboxSchema.ResetPasswordOnNextLogon];
			}
			set
			{
				this[MailboxSchema.ResetPasswordOnNextLogon] = value;
			}
		}

		// Token: 0x17001D6C RID: 7532
		// (get) Token: 0x06005743 RID: 22339 RVA: 0x00138EB6 File Offset: 0x001370B6
		// (set) Token: 0x06005744 RID: 22340 RVA: 0x00138EC8 File Offset: 0x001370C8
		[Parameter(Mandatory = false)]
		public int? ResourceCapacity
		{
			get
			{
				return (int?)this[MailboxSchema.ResourceCapacity];
			}
			set
			{
				this[MailboxSchema.ResourceCapacity] = value;
			}
		}

		// Token: 0x17001D6D RID: 7533
		// (get) Token: 0x06005745 RID: 22341 RVA: 0x00138EDB File Offset: 0x001370DB
		// (set) Token: 0x06005746 RID: 22342 RVA: 0x00138EED File Offset: 0x001370ED
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ResourceCustom
		{
			get
			{
				return (MultiValuedProperty<string>)this[MailboxSchema.ResourceCustom];
			}
			set
			{
				this[MailboxSchema.ResourceCustom] = value;
			}
		}

		// Token: 0x17001D6E RID: 7534
		// (get) Token: 0x06005747 RID: 22343 RVA: 0x00138EFB File Offset: 0x001370FB
		public ExchangeResourceType? ResourceType
		{
			get
			{
				return (ExchangeResourceType?)this[MailboxSchema.ResourceType];
			}
		}

		// Token: 0x17001D6F RID: 7535
		// (get) Token: 0x06005748 RID: 22344 RVA: 0x00138F10 File Offset: 0x00137110
		public bool? RoomMailboxAccountEnabled
		{
			get
			{
				if (this.ResourceType != ExchangeResourceType.Room)
				{
					return null;
				}
				return new bool?((this.ExchangeUserAccountControl & UserAccountControlFlags.AccountDisabled) == UserAccountControlFlags.None);
			}
		}

		// Token: 0x17001D70 RID: 7536
		// (get) Token: 0x06005749 RID: 22345 RVA: 0x00138F56 File Offset: 0x00137156
		// (set) Token: 0x0600574A RID: 22346 RVA: 0x00138F68 File Offset: 0x00137168
		[Parameter(Mandatory = false)]
		public string SamAccountName
		{
			get
			{
				return (string)this[MailboxSchema.SamAccountName];
			}
			set
			{
				this[MailboxSchema.SamAccountName] = value;
			}
		}

		// Token: 0x17001D71 RID: 7537
		// (get) Token: 0x0600574B RID: 22347 RVA: 0x00138F76 File Offset: 0x00137176
		// (set) Token: 0x0600574C RID: 22348 RVA: 0x00138F88 File Offset: 0x00137188
		[Parameter(Mandatory = false)]
		public int? SCLDeleteThreshold
		{
			get
			{
				return (int?)this[MailboxSchema.SCLDeleteThreshold];
			}
			set
			{
				this[MailboxSchema.SCLDeleteThreshold] = value;
			}
		}

		// Token: 0x17001D72 RID: 7538
		// (get) Token: 0x0600574D RID: 22349 RVA: 0x00138F9B File Offset: 0x0013719B
		// (set) Token: 0x0600574E RID: 22350 RVA: 0x00138FAD File Offset: 0x001371AD
		[Parameter(Mandatory = false)]
		public bool? SCLDeleteEnabled
		{
			get
			{
				return (bool?)this[MailboxSchema.SCLDeleteEnabled];
			}
			set
			{
				this[MailboxSchema.SCLDeleteEnabled] = value;
			}
		}

		// Token: 0x17001D73 RID: 7539
		// (get) Token: 0x0600574F RID: 22351 RVA: 0x00138FC0 File Offset: 0x001371C0
		// (set) Token: 0x06005750 RID: 22352 RVA: 0x00138FD2 File Offset: 0x001371D2
		[Parameter(Mandatory = false)]
		public int? SCLRejectThreshold
		{
			get
			{
				return (int?)this[MailboxSchema.SCLRejectThreshold];
			}
			set
			{
				this[MailboxSchema.SCLRejectThreshold] = value;
			}
		}

		// Token: 0x17001D74 RID: 7540
		// (get) Token: 0x06005751 RID: 22353 RVA: 0x00138FE5 File Offset: 0x001371E5
		// (set) Token: 0x06005752 RID: 22354 RVA: 0x00138FF7 File Offset: 0x001371F7
		[Parameter(Mandatory = false)]
		public bool? SCLRejectEnabled
		{
			get
			{
				return (bool?)this[MailboxSchema.SCLRejectEnabled];
			}
			set
			{
				this[MailboxSchema.SCLRejectEnabled] = value;
			}
		}

		// Token: 0x17001D75 RID: 7541
		// (get) Token: 0x06005753 RID: 22355 RVA: 0x0013900A File Offset: 0x0013720A
		// (set) Token: 0x06005754 RID: 22356 RVA: 0x0013901C File Offset: 0x0013721C
		[Parameter(Mandatory = false)]
		public int? SCLQuarantineThreshold
		{
			get
			{
				return (int?)this[MailboxSchema.SCLQuarantineThreshold];
			}
			set
			{
				this[MailboxSchema.SCLQuarantineThreshold] = value;
			}
		}

		// Token: 0x17001D76 RID: 7542
		// (get) Token: 0x06005755 RID: 22357 RVA: 0x0013902F File Offset: 0x0013722F
		// (set) Token: 0x06005756 RID: 22358 RVA: 0x00139041 File Offset: 0x00137241
		[Parameter(Mandatory = false)]
		public bool? SCLQuarantineEnabled
		{
			get
			{
				return (bool?)this[MailboxSchema.SCLQuarantineEnabled];
			}
			set
			{
				this[MailboxSchema.SCLQuarantineEnabled] = value;
			}
		}

		// Token: 0x17001D77 RID: 7543
		// (get) Token: 0x06005757 RID: 22359 RVA: 0x00139054 File Offset: 0x00137254
		// (set) Token: 0x06005758 RID: 22360 RVA: 0x00139066 File Offset: 0x00137266
		[Parameter(Mandatory = false)]
		public int? SCLJunkThreshold
		{
			get
			{
				return (int?)this[MailboxSchema.SCLJunkThreshold];
			}
			set
			{
				this[MailboxSchema.SCLJunkThreshold] = value;
			}
		}

		// Token: 0x17001D78 RID: 7544
		// (get) Token: 0x06005759 RID: 22361 RVA: 0x00139079 File Offset: 0x00137279
		// (set) Token: 0x0600575A RID: 22362 RVA: 0x0013908B File Offset: 0x0013728B
		[Parameter(Mandatory = false)]
		public bool? SCLJunkEnabled
		{
			get
			{
				return (bool?)this[MailboxSchema.SCLJunkEnabled];
			}
			set
			{
				this[MailboxSchema.SCLJunkEnabled] = value;
			}
		}

		// Token: 0x17001D79 RID: 7545
		// (get) Token: 0x0600575B RID: 22363 RVA: 0x0013909E File Offset: 0x0013729E
		// (set) Token: 0x0600575C RID: 22364 RVA: 0x001390B0 File Offset: 0x001372B0
		[Parameter(Mandatory = false)]
		public bool AntispamBypassEnabled
		{
			get
			{
				return (bool)this[MailboxSchema.AntispamBypassEnabled];
			}
			set
			{
				this[MailboxSchema.AntispamBypassEnabled] = value;
			}
		}

		// Token: 0x17001D7A RID: 7546
		// (get) Token: 0x0600575D RID: 22365 RVA: 0x001390C3 File Offset: 0x001372C3
		public string ServerLegacyDN
		{
			get
			{
				return (string)this[MailboxSchema.ServerLegacyDN];
			}
		}

		// Token: 0x17001D7B RID: 7547
		// (get) Token: 0x0600575E RID: 22366 RVA: 0x001390D5 File Offset: 0x001372D5
		public string ServerName
		{
			get
			{
				return (string)this[MailboxSchema.ServerName];
			}
		}

		// Token: 0x17001D7C RID: 7548
		// (get) Token: 0x0600575F RID: 22367 RVA: 0x001390E7 File Offset: 0x001372E7
		// (set) Token: 0x06005760 RID: 22368 RVA: 0x001390F9 File Offset: 0x001372F9
		[ProvisionalClone(CloneSet.CloneExtendedSet)]
		[Parameter(Mandatory = false)]
		public bool? UseDatabaseQuotaDefaults
		{
			get
			{
				return (bool?)this[MailboxSchema.UseDatabaseQuotaDefaults];
			}
			set
			{
				this[MailboxSchema.UseDatabaseQuotaDefaults] = value;
			}
		}

		// Token: 0x17001D7D RID: 7549
		// (get) Token: 0x06005761 RID: 22369 RVA: 0x0013910C File Offset: 0x0013730C
		// (set) Token: 0x06005762 RID: 22370 RVA: 0x0013911E File Offset: 0x0013731E
		[ProvisionalClone(CloneSet.CloneExtendedSet)]
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> IssueWarningQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxSchema.IssueWarningQuota];
			}
			set
			{
				this[MailboxSchema.IssueWarningQuota] = value;
			}
		}

		// Token: 0x17001D7E RID: 7550
		// (get) Token: 0x06005763 RID: 22371 RVA: 0x00139131 File Offset: 0x00137331
		// (set) Token: 0x06005764 RID: 22372 RVA: 0x00139143 File Offset: 0x00137343
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize RulesQuota
		{
			get
			{
				return (ByteQuantifiedSize)this[MailboxSchema.RulesQuota];
			}
			set
			{
				this[MailboxSchema.RulesQuota] = value;
			}
		}

		// Token: 0x17001D7F RID: 7551
		// (get) Token: 0x06005765 RID: 22373 RVA: 0x00139156 File Offset: 0x00137356
		// (set) Token: 0x06005766 RID: 22374 RVA: 0x00139168 File Offset: 0x00137368
		[Parameter(Mandatory = false)]
		public string Office
		{
			get
			{
				return (string)this[MailboxSchema.Office];
			}
			set
			{
				this[MailboxSchema.Office] = value;
			}
		}

		// Token: 0x17001D80 RID: 7552
		// (get) Token: 0x06005767 RID: 22375 RVA: 0x00139176 File Offset: 0x00137376
		// (set) Token: 0x06005768 RID: 22376 RVA: 0x00139188 File Offset: 0x00137388
		[Parameter(Mandatory = false)]
		public string UserPrincipalName
		{
			get
			{
				return (string)this[MailboxSchema.UserPrincipalName];
			}
			set
			{
				this[MailboxSchema.UserPrincipalName] = value;
			}
		}

		// Token: 0x17001D81 RID: 7553
		// (get) Token: 0x06005769 RID: 22377 RVA: 0x00139196 File Offset: 0x00137396
		public bool UMEnabled
		{
			get
			{
				return (bool)this[MailboxSchema.UMEnabled];
			}
		}

		// Token: 0x17001D82 RID: 7554
		// (get) Token: 0x0600576A RID: 22378 RVA: 0x001391A8 File Offset: 0x001373A8
		// (set) Token: 0x0600576B RID: 22379 RVA: 0x001391BA File Offset: 0x001373BA
		[Parameter(Mandatory = false)]
		public int? MaxSafeSenders
		{
			get
			{
				return (int?)this[MailboxSchema.MaxSafeSenders];
			}
			set
			{
				this[MailboxSchema.MaxSafeSenders] = value;
			}
		}

		// Token: 0x17001D83 RID: 7555
		// (get) Token: 0x0600576C RID: 22380 RVA: 0x001391CD File Offset: 0x001373CD
		// (set) Token: 0x0600576D RID: 22381 RVA: 0x001391DF File Offset: 0x001373DF
		[Parameter(Mandatory = false)]
		public int? MaxBlockedSenders
		{
			get
			{
				return (int?)this[MailboxSchema.MaxBlockedSenders];
			}
			set
			{
				this[MailboxSchema.MaxBlockedSenders] = value;
			}
		}

		// Token: 0x17001D84 RID: 7556
		// (get) Token: 0x0600576E RID: 22382 RVA: 0x001391F2 File Offset: 0x001373F2
		public NetID NetID
		{
			get
			{
				return (NetID)this[MailboxSchema.NetID];
			}
		}

		// Token: 0x17001D85 RID: 7557
		// (get) Token: 0x0600576F RID: 22383 RVA: 0x00139204 File Offset: 0x00137404
		// (set) Token: 0x06005770 RID: 22384 RVA: 0x0013920C File Offset: 0x0013740C
		public NetID ReconciliationId { get; internal set; }

		// Token: 0x17001D86 RID: 7558
		// (get) Token: 0x06005771 RID: 22385 RVA: 0x00139215 File Offset: 0x00137415
		// (set) Token: 0x06005772 RID: 22386 RVA: 0x00139227 File Offset: 0x00137427
		[Parameter(Mandatory = false)]
		public SmtpAddress WindowsLiveID
		{
			get
			{
				return (SmtpAddress)this[MailboxSchema.WindowsLiveID];
			}
			set
			{
				this[MailboxSchema.WindowsLiveID] = value;
			}
		}

		// Token: 0x17001D87 RID: 7559
		// (get) Token: 0x06005773 RID: 22387 RVA: 0x0013923A File Offset: 0x0013743A
		// (set) Token: 0x06005774 RID: 22388 RVA: 0x00139242 File Offset: 0x00137442
		[Parameter(Mandatory = false)]
		public SmtpAddress MicrosoftOnlineServicesID
		{
			get
			{
				return this.WindowsLiveID;
			}
			set
			{
				this.WindowsLiveID = value;
			}
		}

		// Token: 0x17001D88 RID: 7560
		// (get) Token: 0x06005775 RID: 22389 RVA: 0x0013924B File Offset: 0x0013744B
		// (set) Token: 0x06005776 RID: 22390 RVA: 0x0013925D File Offset: 0x0013745D
		[ProvisionalClone(CloneSet.CloneLimitedSet)]
		public ADObjectId ThrottlingPolicy
		{
			get
			{
				return (ADObjectId)this[MailboxSchema.ThrottlingPolicy];
			}
			internal set
			{
				this[MailboxSchema.ThrottlingPolicy] = value;
			}
		}

		// Token: 0x17001D89 RID: 7561
		// (get) Token: 0x06005777 RID: 22391 RVA: 0x0013926B File Offset: 0x0013746B
		// (set) Token: 0x06005778 RID: 22392 RVA: 0x0013927D File Offset: 0x0013747D
		[ProvisionalClone(CloneSet.CloneLimitedSet)]
		public ADObjectId RoleAssignmentPolicy
		{
			get
			{
				return (ADObjectId)this[MailboxSchema.RoleAssignmentPolicy];
			}
			internal set
			{
				this[MailboxSchema.RoleAssignmentPolicy] = value;
			}
		}

		// Token: 0x17001D8A RID: 7562
		// (get) Token: 0x06005779 RID: 22393 RVA: 0x0013928B File Offset: 0x0013748B
		// (set) Token: 0x0600577A RID: 22394 RVA: 0x00139293 File Offset: 0x00137493
		public ADObjectId DefaultPublicFolderMailbox { get; internal set; }

		// Token: 0x17001D8B RID: 7563
		// (get) Token: 0x0600577B RID: 22395 RVA: 0x0013929C File Offset: 0x0013749C
		// (set) Token: 0x0600577C RID: 22396 RVA: 0x001392AE File Offset: 0x001374AE
		internal ADObjectId DefaultPublicFolderMailboxValue
		{
			get
			{
				return (ADObjectId)this[MailboxSchema.DefaultPublicFolderMailboxValue];
			}
			set
			{
				this[MailboxSchema.DefaultPublicFolderMailboxValue] = value;
			}
		}

		// Token: 0x17001D8C RID: 7564
		// (get) Token: 0x0600577D RID: 22397 RVA: 0x001392BC File Offset: 0x001374BC
		// (set) Token: 0x0600577E RID: 22398 RVA: 0x001392D8 File Offset: 0x001374D8
		public ADObjectId SharingPolicy
		{
			get
			{
				return this.sharingPolicy ?? ((ADObjectId)this[MailboxSchema.SharingPolicy]);
			}
			internal set
			{
				this.sharingPolicy = value;
			}
		}

		// Token: 0x17001D8D RID: 7565
		// (get) Token: 0x0600577F RID: 22399 RVA: 0x001392E1 File Offset: 0x001374E1
		// (set) Token: 0x06005780 RID: 22400 RVA: 0x001392F3 File Offset: 0x001374F3
		[ProvisionalClone(CloneSet.CloneLimitedSet)]
		public ADObjectId RemoteAccountPolicy
		{
			get
			{
				return (ADObjectId)this[MailboxSchema.RemoteAccountPolicy];
			}
			set
			{
				this[MailboxSchema.RemoteAccountPolicy] = value;
			}
		}

		// Token: 0x17001D8E RID: 7566
		// (get) Token: 0x06005781 RID: 22401 RVA: 0x00139301 File Offset: 0x00137501
		// (set) Token: 0x06005782 RID: 22402 RVA: 0x00139313 File Offset: 0x00137513
		public ADObjectId MailboxPlan
		{
			get
			{
				return (ADObjectId)this[MailboxSchema.MailboxPlan];
			}
			set
			{
				this[MailboxSchema.MailboxPlan] = value;
			}
		}

		// Token: 0x17001D8F RID: 7567
		// (get) Token: 0x06005783 RID: 22403 RVA: 0x00139321 File Offset: 0x00137521
		// (set) Token: 0x06005784 RID: 22404 RVA: 0x00139333 File Offset: 0x00137533
		public ADObjectId ArchiveDatabase
		{
			get
			{
				return (ADObjectId)this[MailboxSchema.ArchiveDatabase];
			}
			internal set
			{
				this[MailboxSchema.ArchiveDatabase] = value;
			}
		}

		// Token: 0x17001D90 RID: 7568
		// (get) Token: 0x06005785 RID: 22405 RVA: 0x00139341 File Offset: 0x00137541
		public Guid ArchiveGuid
		{
			get
			{
				return (Guid)this[MailboxSchema.ArchiveGuid];
			}
		}

		// Token: 0x17001D91 RID: 7569
		// (get) Token: 0x06005786 RID: 22406 RVA: 0x00139353 File Offset: 0x00137553
		// (set) Token: 0x06005787 RID: 22407 RVA: 0x00139365 File Offset: 0x00137565
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ArchiveName
		{
			get
			{
				return (MultiValuedProperty<string>)this[MailboxSchema.ArchiveName];
			}
			set
			{
				this[MailboxSchema.ArchiveName] = value;
			}
		}

		// Token: 0x17001D92 RID: 7570
		// (get) Token: 0x06005788 RID: 22408 RVA: 0x00139373 File Offset: 0x00137573
		// (set) Token: 0x06005789 RID: 22409 RVA: 0x00139385 File Offset: 0x00137585
		[Parameter(Mandatory = false)]
		public SmtpAddress JournalArchiveAddress
		{
			get
			{
				return (SmtpAddress)this[MailboxSchema.JournalArchiveAddress];
			}
			set
			{
				this[MailboxSchema.JournalArchiveAddress] = value;
			}
		}

		// Token: 0x17001D93 RID: 7571
		// (get) Token: 0x0600578A RID: 22410 RVA: 0x00139398 File Offset: 0x00137598
		// (set) Token: 0x0600578B RID: 22411 RVA: 0x001393AA File Offset: 0x001375AA
		[Parameter(Mandatory = false)]
		[ProvisionalClone(CloneSet.CloneExtendedSet)]
		public Unlimited<ByteQuantifiedSize> ArchiveQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxSchema.ArchiveQuota];
			}
			set
			{
				this[MailboxSchema.ArchiveQuota] = value;
			}
		}

		// Token: 0x17001D94 RID: 7572
		// (get) Token: 0x0600578C RID: 22412 RVA: 0x001393BD File Offset: 0x001375BD
		// (set) Token: 0x0600578D RID: 22413 RVA: 0x001393CF File Offset: 0x001375CF
		[ProvisionalClone(CloneSet.CloneExtendedSet)]
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ArchiveWarningQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxSchema.ArchiveWarningQuota];
			}
			set
			{
				this[MailboxSchema.ArchiveWarningQuota] = value;
			}
		}

		// Token: 0x17001D95 RID: 7573
		// (get) Token: 0x0600578E RID: 22414 RVA: 0x001393E2 File Offset: 0x001375E2
		// (set) Token: 0x0600578F RID: 22415 RVA: 0x001393F4 File Offset: 0x001375F4
		[Parameter(Mandatory = false)]
		public SmtpDomain ArchiveDomain
		{
			get
			{
				return (SmtpDomain)this[MailboxSchema.ArchiveDomain];
			}
			set
			{
				this[MailboxSchema.ArchiveDomain] = value;
			}
		}

		// Token: 0x17001D96 RID: 7574
		// (get) Token: 0x06005790 RID: 22416 RVA: 0x00139402 File Offset: 0x00137602
		// (set) Token: 0x06005791 RID: 22417 RVA: 0x00139414 File Offset: 0x00137614
		[Parameter(Mandatory = false)]
		public ArchiveStatusFlags ArchiveStatus
		{
			get
			{
				return (ArchiveStatusFlags)this[MailboxSchema.ArchiveStatus];
			}
			set
			{
				this[MailboxSchema.ArchiveStatus] = value;
			}
		}

		// Token: 0x17001D97 RID: 7575
		// (get) Token: 0x06005792 RID: 22418 RVA: 0x00139427 File Offset: 0x00137627
		public ArchiveState ArchiveState
		{
			get
			{
				return (ArchiveState)this[MailboxSchema.ArchiveState];
			}
		}

		// Token: 0x17001D98 RID: 7576
		// (get) Token: 0x06005793 RID: 22419 RVA: 0x00139439 File Offset: 0x00137639
		public bool IsAuxMailbox
		{
			get
			{
				return (bool)this[MailboxSchema.IsAuxMailbox];
			}
		}

		// Token: 0x17001D99 RID: 7577
		// (get) Token: 0x06005794 RID: 22420 RVA: 0x0013944B File Offset: 0x0013764B
		// (set) Token: 0x06005795 RID: 22421 RVA: 0x0013945D File Offset: 0x0013765D
		public ADObjectId AuxMailboxParentObjectId
		{
			get
			{
				return (ADObjectId)this[MailboxSchema.AuxMailboxParentObjectId];
			}
			set
			{
				this[MailboxSchema.AuxMailboxParentObjectId] = value;
			}
		}

		// Token: 0x17001D9A RID: 7578
		// (get) Token: 0x06005796 RID: 22422 RVA: 0x0013946B File Offset: 0x0013766B
		public MultiValuedProperty<ADObjectId> ChildAuxMailboxObjectIds
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[MailboxSchema.ChildAuxMailboxObjectIds];
			}
		}

		// Token: 0x17001D9B RID: 7579
		// (get) Token: 0x06005797 RID: 22423 RVA: 0x0013947D File Offset: 0x0013767D
		public MailboxRelationType MailboxRelationType
		{
			get
			{
				return (MailboxRelationType)this[MailboxSchema.MailboxRelationType];
			}
		}

		// Token: 0x17001D9C RID: 7580
		// (get) Token: 0x06005798 RID: 22424 RVA: 0x0013948F File Offset: 0x0013768F
		// (set) Token: 0x06005799 RID: 22425 RVA: 0x001394A1 File Offset: 0x001376A1
		[Parameter(Mandatory = false)]
		public RemoteRecipientType RemoteRecipientType
		{
			get
			{
				return (RemoteRecipientType)this[MailboxSchema.RemoteRecipientType];
			}
			set
			{
				this[MailboxSchema.RemoteRecipientType] = value;
			}
		}

		// Token: 0x17001D9D RID: 7581
		// (get) Token: 0x0600579A RID: 22426 RVA: 0x001394B4 File Offset: 0x001376B4
		// (set) Token: 0x0600579B RID: 22427 RVA: 0x001394C6 File Offset: 0x001376C6
		public ADObjectId DisabledArchiveDatabase
		{
			get
			{
				return (ADObjectId)this[MailboxSchema.DisabledArchiveDatabase];
			}
			internal set
			{
				this[MailboxSchema.DisabledArchiveDatabase] = value;
			}
		}

		// Token: 0x17001D9E RID: 7582
		// (get) Token: 0x0600579C RID: 22428 RVA: 0x001394D4 File Offset: 0x001376D4
		public Guid DisabledArchiveGuid
		{
			get
			{
				return (Guid)this[MailboxSchema.DisabledArchiveGuid];
			}
		}

		// Token: 0x17001D9F RID: 7583
		// (get) Token: 0x0600579D RID: 22429 RVA: 0x001394E6 File Offset: 0x001376E6
		public ADObjectId QueryBaseDN
		{
			get
			{
				return (ADObjectId)this[MailboxSchema.QueryBaseDN];
			}
		}

		// Token: 0x17001DA0 RID: 7584
		// (get) Token: 0x0600579E RID: 22430 RVA: 0x001394F8 File Offset: 0x001376F8
		// (set) Token: 0x0600579F RID: 22431 RVA: 0x0013950A File Offset: 0x0013770A
		[ProvisionalClone(CloneSet.CloneLimitedSet)]
		[Parameter(Mandatory = false)]
		public bool QueryBaseDNRestrictionEnabled
		{
			get
			{
				return (bool)this[MailboxSchema.QueryBaseDNRestrictionEnabled];
			}
			set
			{
				this[MailboxSchema.QueryBaseDNRestrictionEnabled] = value;
			}
		}

		// Token: 0x17001DA1 RID: 7585
		// (get) Token: 0x060057A0 RID: 22432 RVA: 0x0013951D File Offset: 0x0013771D
		public ADObjectId MailboxMoveTargetMDB
		{
			get
			{
				return (ADObjectId)this[MailboxSchema.MailboxMoveTargetMDB];
			}
		}

		// Token: 0x17001DA2 RID: 7586
		// (get) Token: 0x060057A1 RID: 22433 RVA: 0x0013952F File Offset: 0x0013772F
		public ADObjectId MailboxMoveSourceMDB
		{
			get
			{
				return (ADObjectId)this[MailboxSchema.MailboxMoveSourceMDB];
			}
		}

		// Token: 0x17001DA3 RID: 7587
		// (get) Token: 0x060057A2 RID: 22434 RVA: 0x00139541 File Offset: 0x00137741
		public RequestFlags MailboxMoveFlags
		{
			get
			{
				return (RequestFlags)this[MailboxSchema.MailboxMoveFlags];
			}
		}

		// Token: 0x17001DA4 RID: 7588
		// (get) Token: 0x060057A3 RID: 22435 RVA: 0x00139553 File Offset: 0x00137753
		public string MailboxMoveRemoteHostName
		{
			get
			{
				return (string)this[MailboxSchema.MailboxMoveRemoteHostName];
			}
		}

		// Token: 0x17001DA5 RID: 7589
		// (get) Token: 0x060057A4 RID: 22436 RVA: 0x00139565 File Offset: 0x00137765
		public string MailboxMoveBatchName
		{
			get
			{
				return (string)this[MailboxSchema.MailboxMoveBatchName];
			}
		}

		// Token: 0x17001DA6 RID: 7590
		// (get) Token: 0x060057A5 RID: 22437 RVA: 0x00139577 File Offset: 0x00137777
		public RequestStatus MailboxMoveStatus
		{
			get
			{
				return (RequestStatus)this[MailboxSchema.MailboxMoveStatus];
			}
		}

		// Token: 0x17001DA7 RID: 7591
		// (get) Token: 0x060057A6 RID: 22438 RVA: 0x00139589 File Offset: 0x00137789
		public string MailboxRelease
		{
			get
			{
				return (string)this[MailboxSchema.MailboxRelease];
			}
		}

		// Token: 0x17001DA8 RID: 7592
		// (get) Token: 0x060057A7 RID: 22439 RVA: 0x0013959B File Offset: 0x0013779B
		public string ArchiveRelease
		{
			get
			{
				return (string)this[MailboxSchema.ArchiveRelease];
			}
		}

		// Token: 0x17001DA9 RID: 7593
		// (get) Token: 0x060057A8 RID: 22440 RVA: 0x001395AD File Offset: 0x001377AD
		public bool IsPersonToPersonTextMessagingEnabled
		{
			get
			{
				return (bool)this[MailboxSchema.IsPersonToPersonTextMessagingEnabled];
			}
		}

		// Token: 0x17001DAA RID: 7594
		// (get) Token: 0x060057A9 RID: 22441 RVA: 0x001395BF File Offset: 0x001377BF
		public bool IsMachineToPersonTextMessagingEnabled
		{
			get
			{
				return (bool)this[MailboxSchema.IsMachineToPersonTextMessagingEnabled];
			}
		}

		// Token: 0x17001DAB RID: 7595
		// (get) Token: 0x060057AA RID: 22442 RVA: 0x001395D1 File Offset: 0x001377D1
		// (set) Token: 0x060057AB RID: 22443 RVA: 0x001395E3 File Offset: 0x001377E3
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<byte[]> UserSMimeCertificate
		{
			get
			{
				return (MultiValuedProperty<byte[]>)this[MailboxSchema.UserSMimeCertificate];
			}
			set
			{
				this[MailboxSchema.UserSMimeCertificate] = value;
			}
		}

		// Token: 0x17001DAC RID: 7596
		// (get) Token: 0x060057AC RID: 22444 RVA: 0x001395F1 File Offset: 0x001377F1
		// (set) Token: 0x060057AD RID: 22445 RVA: 0x00139603 File Offset: 0x00137803
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<byte[]> UserCertificate
		{
			get
			{
				return (MultiValuedProperty<byte[]>)this[MailboxSchema.UserCertificate];
			}
			set
			{
				this[MailboxSchema.UserCertificate] = value;
			}
		}

		// Token: 0x17001DAD RID: 7597
		// (get) Token: 0x060057AE RID: 22446 RVA: 0x00139611 File Offset: 0x00137811
		// (set) Token: 0x060057AF RID: 22447 RVA: 0x00139623 File Offset: 0x00137823
		[Parameter(Mandatory = false)]
		public bool CalendarVersionStoreDisabled
		{
			get
			{
				return (bool)this[MailboxSchema.CalendarVersionStoreDisabled];
			}
			set
			{
				this[MailboxSchema.CalendarVersionStoreDisabled] = value;
			}
		}

		// Token: 0x17001DAE RID: 7598
		// (get) Token: 0x060057B0 RID: 22448 RVA: 0x00139636 File Offset: 0x00137836
		// (set) Token: 0x060057B1 RID: 22449 RVA: 0x00139648 File Offset: 0x00137848
		[Parameter(Mandatory = false)]
		public string ImmutableId
		{
			get
			{
				return (string)this[MailboxSchema.ImmutableId];
			}
			set
			{
				this[MailboxSchema.ImmutableId] = value;
			}
		}

		// Token: 0x17001DAF RID: 7599
		// (get) Token: 0x060057B2 RID: 22450 RVA: 0x00139656 File Offset: 0x00137856
		[ProvisionalClone(CloneSet.CloneLimitedSet)]
		public MultiValuedProperty<Capability> PersistedCapabilities
		{
			get
			{
				return (MultiValuedProperty<Capability>)this[MailboxSchema.PersistedCapabilities];
			}
		}

		// Token: 0x17001DB0 RID: 7600
		// (get) Token: 0x060057B3 RID: 22451 RVA: 0x00139668 File Offset: 0x00137868
		// (set) Token: 0x060057B4 RID: 22452 RVA: 0x0013967A File Offset: 0x0013787A
		[Parameter(Mandatory = false)]
		public bool? SKUAssigned
		{
			get
			{
				return (bool?)this[MailboxSchema.SKUAssigned];
			}
			set
			{
				this[MailboxSchema.SKUAssigned] = value;
			}
		}

		// Token: 0x17001DB1 RID: 7601
		// (get) Token: 0x060057B5 RID: 22453 RVA: 0x0013968D File Offset: 0x0013788D
		// (set) Token: 0x060057B6 RID: 22454 RVA: 0x0013969F File Offset: 0x0013789F
		[ProvisionalCloneOnce(CloneSet.CloneExtendedSet)]
		[Parameter(Mandatory = false)]
		public bool AuditEnabled
		{
			get
			{
				return (bool)this[MailboxSchema.AuditEnabled];
			}
			set
			{
				this[MailboxSchema.AuditEnabled] = value;
			}
		}

		// Token: 0x17001DB2 RID: 7602
		// (get) Token: 0x060057B7 RID: 22455 RVA: 0x001396B2 File Offset: 0x001378B2
		// (set) Token: 0x060057B8 RID: 22456 RVA: 0x001396C4 File Offset: 0x001378C4
		[Parameter(Mandatory = false)]
		[ProvisionalCloneOnce(CloneSet.CloneExtendedSet)]
		public EnhancedTimeSpan AuditLogAgeLimit
		{
			get
			{
				return (EnhancedTimeSpan)this[MailboxSchema.AuditLogAgeLimit];
			}
			set
			{
				this[MailboxSchema.AuditLogAgeLimit] = value;
			}
		}

		// Token: 0x17001DB3 RID: 7603
		// (get) Token: 0x060057B9 RID: 22457 RVA: 0x001396D7 File Offset: 0x001378D7
		// (set) Token: 0x060057BA RID: 22458 RVA: 0x001396E9 File Offset: 0x001378E9
		[Parameter(Mandatory = false)]
		[ProvisionalCloneOnce(CloneSet.CloneExtendedSet)]
		public MultiValuedProperty<MailboxAuditOperations> AuditAdmin
		{
			get
			{
				return (MultiValuedProperty<MailboxAuditOperations>)this[MailboxSchema.AuditAdmin];
			}
			set
			{
				this[MailboxSchema.AuditAdmin] = value;
				this.AuditDelegateAdmin = value;
			}
		}

		// Token: 0x17001DB4 RID: 7604
		// (get) Token: 0x060057BB RID: 22459 RVA: 0x001396FE File Offset: 0x001378FE
		// (set) Token: 0x060057BC RID: 22460 RVA: 0x00139710 File Offset: 0x00137910
		[Parameter(Mandatory = false)]
		[ProvisionalCloneOnce(CloneSet.CloneExtendedSet)]
		public MultiValuedProperty<MailboxAuditOperations> AuditDelegate
		{
			get
			{
				return (MultiValuedProperty<MailboxAuditOperations>)this[MailboxSchema.AuditDelegate];
			}
			set
			{
				this[MailboxSchema.AuditDelegate] = value;
			}
		}

		// Token: 0x17001DB5 RID: 7605
		// (get) Token: 0x060057BD RID: 22461 RVA: 0x0013971E File Offset: 0x0013791E
		// (set) Token: 0x060057BE RID: 22462 RVA: 0x00139730 File Offset: 0x00137930
		internal MultiValuedProperty<MailboxAuditOperations> AuditDelegateAdmin
		{
			get
			{
				return (MultiValuedProperty<MailboxAuditOperations>)this[MailboxSchema.AuditDelegateAdmin];
			}
			set
			{
				this[MailboxSchema.AuditDelegateAdmin] = value;
			}
		}

		// Token: 0x17001DB6 RID: 7606
		// (get) Token: 0x060057BF RID: 22463 RVA: 0x0013973E File Offset: 0x0013793E
		// (set) Token: 0x060057C0 RID: 22464 RVA: 0x00139750 File Offset: 0x00137950
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<MailboxAuditOperations> AuditOwner
		{
			get
			{
				return (MultiValuedProperty<MailboxAuditOperations>)this[MailboxSchema.AuditOwner];
			}
			set
			{
				this[MailboxSchema.AuditOwner] = value;
			}
		}

		// Token: 0x17001DB7 RID: 7607
		// (get) Token: 0x060057C1 RID: 22465 RVA: 0x0013975E File Offset: 0x0013795E
		public DateTime? WhenMailboxCreated
		{
			get
			{
				return (DateTime?)this[MailboxSchema.WhenMailboxCreated];
			}
		}

		// Token: 0x17001DB8 RID: 7608
		// (get) Token: 0x060057C2 RID: 22466 RVA: 0x00139770 File Offset: 0x00137970
		public string SourceAnchor
		{
			get
			{
				return (string)this[MailboxSchema.SourceAnchor];
			}
		}

		// Token: 0x17001DB9 RID: 7609
		// (get) Token: 0x060057C3 RID: 22467 RVA: 0x00139782 File Offset: 0x00137982
		// (set) Token: 0x060057C4 RID: 22468 RVA: 0x00139794 File Offset: 0x00137994
		[Parameter(Mandatory = false)]
		public CountryInfo UsageLocation
		{
			get
			{
				return (CountryInfo)this[MailboxSchema.UsageLocation];
			}
			set
			{
				this[MailboxSchema.UsageLocation] = value;
			}
		}

		// Token: 0x17001DBA RID: 7610
		// (get) Token: 0x060057C5 RID: 22469 RVA: 0x001397A2 File Offset: 0x001379A2
		// (set) Token: 0x060057C6 RID: 22470 RVA: 0x001397B4 File Offset: 0x001379B4
		public bool IsSoftDeletedByRemove
		{
			get
			{
				return (bool)this[MailboxSchema.IsSoftDeletedByRemove];
			}
			set
			{
				this[MailboxSchema.IsSoftDeletedByRemove] = value;
			}
		}

		// Token: 0x17001DBB RID: 7611
		// (get) Token: 0x060057C7 RID: 22471 RVA: 0x001397C7 File Offset: 0x001379C7
		// (set) Token: 0x060057C8 RID: 22472 RVA: 0x001397D9 File Offset: 0x001379D9
		public bool IsSoftDeletedByDisable
		{
			get
			{
				return (bool)this[MailboxSchema.IsSoftDeletedByDisable];
			}
			set
			{
				this[MailboxSchema.IsSoftDeletedByDisable] = value;
			}
		}

		// Token: 0x17001DBC RID: 7612
		// (get) Token: 0x060057C9 RID: 22473 RVA: 0x001397EC File Offset: 0x001379EC
		// (set) Token: 0x060057CA RID: 22474 RVA: 0x001397FE File Offset: 0x001379FE
		public bool IsInactiveMailbox
		{
			get
			{
				return (bool)this[MailboxSchema.IsInactiveMailbox];
			}
			set
			{
				this[MailboxSchema.IsInactiveMailbox] = value;
			}
		}

		// Token: 0x17001DBD RID: 7613
		// (get) Token: 0x060057CB RID: 22475 RVA: 0x00139811 File Offset: 0x00137A11
		// (set) Token: 0x060057CC RID: 22476 RVA: 0x00139823 File Offset: 0x00137A23
		public bool IncludeInGarbageCollection
		{
			get
			{
				return (bool)this[MailboxSchema.IncludeInGarbageCollection];
			}
			set
			{
				this[MailboxSchema.IncludeInGarbageCollection] = value;
			}
		}

		// Token: 0x17001DBE RID: 7614
		// (get) Token: 0x060057CD RID: 22477 RVA: 0x00139836 File Offset: 0x00137A36
		// (set) Token: 0x060057CE RID: 22478 RVA: 0x00139848 File Offset: 0x00137A48
		public DateTime? WhenSoftDeleted
		{
			get
			{
				return (DateTime?)this[MailboxSchema.WhenSoftDeleted];
			}
			set
			{
				this[MailboxSchema.WhenSoftDeleted] = value;
			}
		}

		// Token: 0x17001DBF RID: 7615
		// (get) Token: 0x060057CF RID: 22479 RVA: 0x0013985B File Offset: 0x00137A5B
		// (set) Token: 0x060057D0 RID: 22480 RVA: 0x0013986D File Offset: 0x00137A6D
		public MultiValuedProperty<string> InPlaceHolds
		{
			get
			{
				return (MultiValuedProperty<string>)this[MailboxSchema.InPlaceHolds];
			}
			set
			{
				this[MailboxSchema.InPlaceHolds] = value;
			}
		}

		// Token: 0x17001DC0 RID: 7616
		// (get) Token: 0x060057D1 RID: 22481 RVA: 0x0013987B File Offset: 0x00137A7B
		public MultiValuedProperty<ADObjectId> GeneratedOfflineAddressBooks
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[MailboxSchema.GeneratedOfflineAddressBooks];
			}
		}

		// Token: 0x04003B39 RID: 15161
		private static MailboxSchema schema = ObjectSchema.GetInstance<MailboxSchema>();

		// Token: 0x04003B3A RID: 15162
		private static readonly IMailboxLocationInfo[] EmptyMailboxLocationInfo = new IMailboxLocationInfo[0];

		// Token: 0x04003B3B RID: 15163
		private static IEnumerable<PropertyInfo> cloneableProps;

		// Token: 0x04003B3C RID: 15164
		private static IEnumerable<PropertyInfo> cloneableOnceProps;

		// Token: 0x04003B3D RID: 15165
		private static IEnumerable<PropertyInfo> cloneableEnabledStateProps;

		// Token: 0x04003B3E RID: 15166
		private ADObjectId sharingPolicy;
	}
}
