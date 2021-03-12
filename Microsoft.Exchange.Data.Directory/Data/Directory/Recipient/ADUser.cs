using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.Net.WSTrust;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x020001EB RID: 491
	[Serializable]
	public class ADUser : ADMailboxRecipient, IADUser, IADMailboxRecipient, IADMailStorage, IADSecurityPrincipal, IADOrgPerson, IADRecipient, IADObject, IADRawEntry, IConfigurable, IPropertyBag, IReadOnlyPropertyBag, IOriginatingChangeTimestamp, IFederatedIdentityParameters, IProvisioningCacheInvalidation
	{
		// Token: 0x0600170F RID: 5903 RVA: 0x000670F1 File Offset: 0x000652F1
		internal static string GetMonitoringMailboxName(Guid guid)
		{
			return string.Format("HealthMailbox{0}", guid.ToString("N"));
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001710 RID: 5904 RVA: 0x00067109 File Offset: 0x00065309
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADUser.schema;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001711 RID: 5905 RVA: 0x00067110 File Offset: 0x00065310
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADUser.MostDerivedClass;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001712 RID: 5906 RVA: 0x00067117 File Offset: 0x00065317
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return ADUser.ImplicitFilterInternal;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001713 RID: 5907 RVA: 0x0006711E File Offset: 0x0006531E
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ADRecipient.PublicFolderMailboxObjectVersion;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001714 RID: 5908 RVA: 0x00067125 File Offset: 0x00065325
		internal override bool ExchangeVersionUpgradeSupported
		{
			get
			{
				return base.RecipientTypeDetails == RecipientTypeDetails.MailUser;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001715 RID: 5909 RVA: 0x00067135 File Offset: 0x00065335
		internal override string ObjectCategoryName
		{
			get
			{
				return ADUser.ObjectCategoryNameInternal;
			}
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x0006713C File Offset: 0x0006533C
		protected override void ValidateRead(List<ValidationError> errors)
		{
			base.ValidateRead(errors);
			if (RecipientType.UserMailbox == base.RecipientType)
			{
				if (base.ExchangeGuid == Guid.Empty)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.PropertyRequired(ADMailboxRecipientSchema.ExchangeGuid.Name, base.RecipientType.ToString()), ADMailboxRecipientSchema.ExchangeGuid, null));
				}
				if (base.Database == null)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.PropertyRequired(ADMailboxRecipientSchema.Database.Name, base.RecipientType.ToString()), ADMailboxRecipientSchema.Database, null));
				}
				if (this.MailboxContainerGuid == null)
				{
					if (this.AggregatedMailboxGuids.Count != 0)
					{
						errors.Add(new PropertyValidationError(DirectoryStrings.PropertyDependencyRequired(ADUserSchema.MailboxContainerGuid.Name, ADUserSchema.AggregatedMailboxGuids.Name), ADUserSchema.MailboxContainerGuid, null));
					}
				}
				else if (this.AggregatedMailboxGuids.Count != 0 && this.UnifiedMailbox != null)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.ErrorCannotAggregateAndLinkMailbox, ADUserSchema.MailboxContainerGuid, null));
				}
			}
			if (RecipientType.MailUser == base.RecipientType && base.ExternalEmailAddress == null)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ErrorNullExternalEmailAddress, ADRecipientSchema.ExternalEmailAddress, null));
			}
			if (RecipientTypeDetails.MailboxPlan == base.RecipientTypeDetails)
			{
				if (this.PersistedCapabilities.Count > 1)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.InvalidNumberOfCapabilitiesOnMailboxPlan(MultiValuedPropertyBase.FormatMultiValuedProperty(CapabilityHelper.AllowedSKUCapabilities)), ADUserSchema.PersistedCapabilities, null));
					return;
				}
				if (this.PersistedCapabilities.Count == 1 && !CapabilityHelper.IsAllowedSKUCapability(this.PersistedCapabilities[0]))
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.InvalidCapabilityOnMailboxPlan(this.PersistedCapabilities[0].ToString(), MultiValuedPropertyBase.FormatMultiValuedProperty(CapabilityHelper.AllowedSKUCapabilities)), ADUserSchema.PersistedCapabilities, this.PersistedCapabilities[0]));
				}
			}
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x00067320 File Offset: 0x00065520
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (base.UseDatabaseQuotaDefaults == null || !base.UseDatabaseQuotaDefaults.Value)
			{
				errors.AddRange(Microsoft.Exchange.Data.Directory.SystemConfiguration.Database.ValidateAscendingQuotas(this.propertyBag, new ProviderPropertyDefinition[]
				{
					IADMailStorageSchema.IssueWarningQuota,
					IADMailStorageSchema.ProhibitSendQuota,
					IADMailStorageSchema.ProhibitSendReceiveQuota
				}, this.Identity));
				errors.AddRange(Microsoft.Exchange.Data.Directory.SystemConfiguration.Database.ValidateAscendingQuotas(this.propertyBag, new ProviderPropertyDefinition[]
				{
					IADMailStorageSchema.RecoverableItemsWarningQuota,
					IADMailStorageSchema.RecoverableItemsQuota
				}, this.Identity));
				errors.AddRange(Microsoft.Exchange.Data.Directory.SystemConfiguration.Database.ValidateAscendingQuotas(this.propertyBag, new ProviderPropertyDefinition[]
				{
					IADMailStorageSchema.ArchiveWarningQuota,
					IADMailStorageSchema.ArchiveQuota
				}, this.Identity));
				errors.AddRange(Microsoft.Exchange.Data.Directory.SystemConfiguration.Database.ValidateAscendingQuotas(this.propertyBag, new ProviderPropertyDefinition[]
				{
					IADMailStorageSchema.CalendarLoggingQuota,
					IADMailStorageSchema.RecoverableItemsQuota
				}, this.Identity));
			}
			if (!this.RetentionHoldEnabled && (this.EndDateForRetentionHold != null || this.StartDateForRetentionHold != null))
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorElcSuspensionNotEnabled, this.Identity, string.Empty));
			}
			if (this.StartDateForRetentionHold != null && this.EndDateForRetentionHold != null && (this.StartDateForRetentionHold.Value - this.EndDateForRetentionHold.Value).TotalSeconds >= 0.0)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorStartDateAfterEndDate(this.StartDateForRetentionHold.Value.ToString(), this.EndDateForRetentionHold.Value.ToString()), this.Identity, string.Empty));
			}
			if (RecipientType.UserMailbox == base.RecipientType)
			{
				if (base.ExchangeGuid == Guid.Empty)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.PropertyRequired(ADMailboxRecipientSchema.ExchangeGuid.Name, base.RecipientType.ToString()), ADMailboxRecipientSchema.ExchangeGuid, null));
				}
				if (base.Database == null)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.PropertyRequired(ADMailboxRecipientSchema.Database.Name, base.RecipientType.ToString()), ADMailboxRecipientSchema.Database, null));
				}
			}
			if (RecipientType.MailUser == base.RecipientType && base.ExternalEmailAddress == null)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ErrorNullExternalEmailAddress, ADRecipientSchema.ExternalEmailAddress, null));
			}
			if (this.propertyBag.IsModified(ADMailboxRecipientSchema.WhenMailboxCreated) && base.WhenMailboxCreated == null)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ErrorNotNullProperty(ADMailboxRecipientSchema.WhenMailboxCreated.Name), ADMailboxRecipientSchema.WhenMailboxCreated, base.WhenMailboxCreated));
			}
			if (this.propertyBag.IsModified(ADUserSchema.Owners) && this.Owners != null && this.Owners.Count > 0 && !this.IsAllowedToModifyOwners)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ErrorOwnersUpdated, ADUserSchema.Owners, null));
			}
			if (this.propertyBag.IsModified(ADUserSchema.ArchiveName) && this.ArchiveName != null && this.ArchiveName.Count > 0)
			{
				foreach (string text in this.ArchiveName)
				{
					if (text.Length <= 0)
					{
						errors.Add(new PropertyValidationError(DirectoryStrings.ErrorEmptyArchiveName, ADUserSchema.ArchiveName, null));
						break;
					}
				}
			}
			if (this.MailboxLocations != null)
			{
				this.MailboxLocations.Validate(errors);
			}
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x00067704 File Offset: 0x00065904
		internal ADUser(IRecipientSession session, PropertyBag propertyBag) : base(session, propertyBag)
		{
			if (base.RecipientTypeDetails != RecipientTypeDetails.None && !base.IsReadOnly)
			{
				ExchangeObjectVersion maximumSupportedExchangeObjectVersion = ADUser.GetMaximumSupportedExchangeObjectVersion(base.RecipientTypeDetails, true);
				if (maximumSupportedExchangeObjectVersion.IsOlderThan(base.ExchangeVersion))
				{
					this.SetIsReadOnly(true);
				}
			}
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x00067758 File Offset: 0x00065958
		internal ADUser(IRecipientSession session, string commonName, ADObjectId containerId, UserObjectClass userObjectClass)
		{
			this.m_Session = session;
			base.SamAccountName = commonName;
			base.SetId(containerId.GetChildId("CN", commonName));
			base.SetObjectClass((userObjectClass == UserObjectClass.User) ? "user" : "inetOrgPerson");
			this.UserAccountControl = (UserAccountControlFlags.AccountDisabled | UserAccountControlFlags.NormalAccount);
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x000677B7 File Offset: 0x000659B7
		public ADUser()
		{
			base.SetObjectClass("user");
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x0600171B RID: 5915 RVA: 0x000677D5 File Offset: 0x000659D5
		// (set) Token: 0x0600171C RID: 5916 RVA: 0x000677E7 File Offset: 0x000659E7
		public UserAccountControlFlags ExchangeUserAccountControl
		{
			get
			{
				return (UserAccountControlFlags)this[ADUserSchema.ExchangeUserAccountControl];
			}
			set
			{
				this[ADUserSchema.ExchangeUserAccountControl] = value;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x0600171D RID: 5917 RVA: 0x000677FA File Offset: 0x000659FA
		// (set) Token: 0x0600171E RID: 5918 RVA: 0x0006780C File Offset: 0x00065A0C
		public MultiValuedProperty<int> LocaleID
		{
			get
			{
				return (MultiValuedProperty<int>)this[ADUserSchema.LocaleID];
			}
			set
			{
				this[ADUserSchema.LocaleID] = value;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x0600171F RID: 5919 RVA: 0x0006781A File Offset: 0x00065A1A
		// (set) Token: 0x06001720 RID: 5920 RVA: 0x0006782C File Offset: 0x00065A2C
		public bool RetentionHoldEnabled
		{
			get
			{
				return (bool)this[ADUserSchema.ElcExpirationSuspensionEnabled];
			}
			set
			{
				this[ADUserSchema.ElcExpirationSuspensionEnabled] = value;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001721 RID: 5921 RVA: 0x0006783F File Offset: 0x00065A3F
		// (set) Token: 0x06001722 RID: 5922 RVA: 0x00067851 File Offset: 0x00065A51
		public DateTime? EndDateForRetentionHold
		{
			get
			{
				return (DateTime?)this[ADUserSchema.ElcExpirationSuspensionEndDate];
			}
			set
			{
				this[ADUserSchema.ElcExpirationSuspensionEndDate] = value;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001723 RID: 5923 RVA: 0x00067864 File Offset: 0x00065A64
		// (set) Token: 0x06001724 RID: 5924 RVA: 0x00067876 File Offset: 0x00065A76
		public DateTime? StartDateForRetentionHold
		{
			get
			{
				return (DateTime?)this[ADUserSchema.ElcExpirationSuspensionStartDate];
			}
			set
			{
				this[ADUserSchema.ElcExpirationSuspensionStartDate] = value;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001725 RID: 5925 RVA: 0x00067889 File Offset: 0x00065A89
		internal bool IsInLitigationHoldOrInplaceHold
		{
			get
			{
				return this.LitigationHoldEnabled || (base.InPlaceHolds != null && base.InPlaceHolds.Count > 0);
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001726 RID: 5926 RVA: 0x000678AD File Offset: 0x00065AAD
		// (set) Token: 0x06001727 RID: 5927 RVA: 0x000678BF File Offset: 0x00065ABF
		public bool LitigationHoldEnabled
		{
			get
			{
				return (bool)this[IADMailStorageSchema.LitigationHoldEnabled];
			}
			set
			{
				this[IADMailStorageSchema.LitigationHoldEnabled] = value;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001728 RID: 5928 RVA: 0x000678D2 File Offset: 0x00065AD2
		// (set) Token: 0x06001729 RID: 5929 RVA: 0x000678E4 File Offset: 0x00065AE4
		public Unlimited<EnhancedTimeSpan>? LitigationHoldDuration
		{
			get
			{
				return (Unlimited<EnhancedTimeSpan>?)this[ADRecipientSchema.LitigationHoldDuration];
			}
			set
			{
				this[ADRecipientSchema.LitigationHoldDuration] = value;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x0600172A RID: 5930 RVA: 0x000678F7 File Offset: 0x00065AF7
		// (set) Token: 0x0600172B RID: 5931 RVA: 0x00067909 File Offset: 0x00065B09
		public bool SingleItemRecoveryEnabled
		{
			get
			{
				return (bool)this[IADMailStorageSchema.SingleItemRecoveryEnabled];
			}
			set
			{
				this[IADMailStorageSchema.SingleItemRecoveryEnabled] = value;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x0600172C RID: 5932 RVA: 0x0006791C File Offset: 0x00065B1C
		// (set) Token: 0x0600172D RID: 5933 RVA: 0x0006792E File Offset: 0x00065B2E
		public bool CalendarVersionStoreDisabled
		{
			get
			{
				return (bool)this[IADMailStorageSchema.CalendarVersionStoreDisabled];
			}
			set
			{
				this[IADMailStorageSchema.CalendarVersionStoreDisabled] = value;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x0600172E RID: 5934 RVA: 0x00067941 File Offset: 0x00065B41
		// (set) Token: 0x0600172F RID: 5935 RVA: 0x00067953 File Offset: 0x00065B53
		public bool SiteMailboxMessageDedupEnabled
		{
			get
			{
				return (bool)this[IADMailStorageSchema.SiteMailboxMessageDedupEnabled];
			}
			set
			{
				this[IADMailStorageSchema.SiteMailboxMessageDedupEnabled] = value;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001730 RID: 5936 RVA: 0x00067966 File Offset: 0x00065B66
		// (set) Token: 0x06001731 RID: 5937 RVA: 0x00067978 File Offset: 0x00065B78
		internal ElcMailboxFlags ElcMailboxFlags
		{
			get
			{
				return (ElcMailboxFlags)this[ADUserSchema.ElcMailboxFlags];
			}
			set
			{
				this[ADUserSchema.ElcMailboxFlags] = value;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001732 RID: 5938 RVA: 0x0006798B File Offset: 0x00065B8B
		// (set) Token: 0x06001733 RID: 5939 RVA: 0x0006799D File Offset: 0x00065B9D
		public string RetentionComment
		{
			get
			{
				return (string)this[ADUserSchema.RetentionComment];
			}
			set
			{
				this[ADUserSchema.RetentionComment] = value;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001734 RID: 5940 RVA: 0x000679AB File Offset: 0x00065BAB
		// (set) Token: 0x06001735 RID: 5941 RVA: 0x000679BD File Offset: 0x00065BBD
		public string RetentionUrl
		{
			get
			{
				return (string)this[ADUserSchema.RetentionUrl];
			}
			set
			{
				this[ADUserSchema.RetentionUrl] = value;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001736 RID: 5942 RVA: 0x000679CB File Offset: 0x00065BCB
		// (set) Token: 0x06001737 RID: 5943 RVA: 0x000679DD File Offset: 0x00065BDD
		public bool LEOEnabled
		{
			get
			{
				return (bool)this[ADRecipientSchema.LEOEnabled];
			}
			set
			{
				this[ADRecipientSchema.LEOEnabled] = value;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001738 RID: 5944 RVA: 0x000679F0 File Offset: 0x00065BF0
		// (set) Token: 0x06001739 RID: 5945 RVA: 0x00067A02 File Offset: 0x00065C02
		public DateTime? LitigationHoldDate
		{
			get
			{
				return (DateTime?)this[ADUserSchema.LitigationHoldDate];
			}
			set
			{
				this[ADUserSchema.LitigationHoldDate] = value;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x0600173A RID: 5946 RVA: 0x00067A15 File Offset: 0x00065C15
		// (set) Token: 0x0600173B RID: 5947 RVA: 0x00067A27 File Offset: 0x00065C27
		public string LitigationHoldOwner
		{
			get
			{
				return (string)this[ADUserSchema.LitigationHoldOwner];
			}
			set
			{
				this[ADUserSchema.LitigationHoldOwner] = value;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x0600173C RID: 5948 RVA: 0x00067A35 File Offset: 0x00065C35
		// (set) Token: 0x0600173D RID: 5949 RVA: 0x00067A47 File Offset: 0x00065C47
		public ADObjectId ManagedFolderMailboxPolicy
		{
			get
			{
				return (ADObjectId)this[ADUserSchema.ManagedFolderMailboxPolicy];
			}
			set
			{
				this[ADUserSchema.ManagedFolderMailboxPolicy] = value;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x0600173E RID: 5950 RVA: 0x00067A55 File Offset: 0x00065C55
		// (set) Token: 0x0600173F RID: 5951 RVA: 0x00067A67 File Offset: 0x00065C67
		public ADObjectId RetentionPolicy
		{
			get
			{
				return (ADObjectId)this[ADUserSchema.RetentionPolicy];
			}
			set
			{
				this[ADUserSchema.RetentionPolicy] = value;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001740 RID: 5952 RVA: 0x00067A75 File Offset: 0x00065C75
		// (set) Token: 0x06001741 RID: 5953 RVA: 0x00067A87 File Offset: 0x00065C87
		internal bool ShouldUseDefaultRetentionPolicy
		{
			get
			{
				return (bool)this[ADUserSchema.ShouldUseDefaultRetentionPolicy];
			}
			set
			{
				this[ADUserSchema.ShouldUseDefaultRetentionPolicy] = value;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001742 RID: 5954 RVA: 0x00067A9A File Offset: 0x00065C9A
		// (set) Token: 0x06001743 RID: 5955 RVA: 0x00067AAC File Offset: 0x00065CAC
		public ADObjectId SharingPolicy
		{
			get
			{
				return (ADObjectId)this[ADUserSchema.SharingPolicy];
			}
			set
			{
				this[ADUserSchema.SharingPolicy] = value;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001744 RID: 5956 RVA: 0x00067ABA File Offset: 0x00065CBA
		// (set) Token: 0x06001745 RID: 5957 RVA: 0x00067ACC File Offset: 0x00065CCC
		public ADObjectId RemoteAccountPolicy
		{
			get
			{
				return (ADObjectId)this[ADUserSchema.RemoteAccountPolicy];
			}
			set
			{
				this[ADUserSchema.RemoteAccountPolicy] = value;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001746 RID: 5958 RVA: 0x00067ADA File Offset: 0x00065CDA
		// (set) Token: 0x06001747 RID: 5959 RVA: 0x00067AEC File Offset: 0x00065CEC
		public bool CalendarRepairDisabled
		{
			get
			{
				return (bool)this[ADUserSchema.CalendarRepairDisabled];
			}
			set
			{
				this[ADUserSchema.CalendarRepairDisabled] = value;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001748 RID: 5960 RVA: 0x00067AFF File Offset: 0x00065CFF
		// (set) Token: 0x06001749 RID: 5961 RVA: 0x00067B11 File Offset: 0x00065D11
		public MobileFeaturesEnabled MobileFeaturesEnabled
		{
			get
			{
				return (MobileFeaturesEnabled)this[ADUserSchema.MobileFeaturesEnabled];
			}
			set
			{
				this[ADUserSchema.MobileFeaturesEnabled] = value;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x0600174A RID: 5962 RVA: 0x00067B24 File Offset: 0x00065D24
		// (set) Token: 0x0600174B RID: 5963 RVA: 0x00067B36 File Offset: 0x00065D36
		public MobileMailboxFlags MobileMailboxFlags
		{
			get
			{
				return (MobileMailboxFlags)this[ADUserSchema.MobileMailboxFlags];
			}
			set
			{
				this[ADUserSchema.MobileMailboxFlags] = value;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x0600174C RID: 5964 RVA: 0x00067B49 File Offset: 0x00065D49
		// (set) Token: 0x0600174D RID: 5965 RVA: 0x00067B5B File Offset: 0x00065D5B
		public ADObjectId QueryBaseDN
		{
			get
			{
				return (ADObjectId)this[ADUserSchema.QueryBaseDN];
			}
			set
			{
				this[ADUserSchema.QueryBaseDN] = value;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x0600174E RID: 5966 RVA: 0x00067B69 File Offset: 0x00065D69
		// (set) Token: 0x0600174F RID: 5967 RVA: 0x00067B7B File Offset: 0x00065D7B
		public bool QueryBaseDNRestrictionEnabled
		{
			get
			{
				return (bool)this[ADRecipientSchema.QueryBaseDNRestrictionEnabled];
			}
			set
			{
				this[ADRecipientSchema.QueryBaseDNRestrictionEnabled] = value;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001750 RID: 5968 RVA: 0x00067B8E File Offset: 0x00065D8E
		// (set) Token: 0x06001751 RID: 5969 RVA: 0x00067BA0 File Offset: 0x00065DA0
		public string MailboxPlanName
		{
			get
			{
				return (string)this[ADRecipientSchema.MailboxPlanName];
			}
			set
			{
				this[ADRecipientSchema.MailboxPlanName] = value;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001752 RID: 5970 RVA: 0x00067BAE File Offset: 0x00065DAE
		// (set) Token: 0x06001753 RID: 5971 RVA: 0x00067BC0 File Offset: 0x00065DC0
		public string IntendedMailboxPlanName
		{
			get
			{
				return (string)this[ADUserSchema.IntendedMailboxPlanName];
			}
			set
			{
				this[ADUserSchema.IntendedMailboxPlanName] = value;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001754 RID: 5972 RVA: 0x00067BCE File Offset: 0x00065DCE
		// (set) Token: 0x06001755 RID: 5973 RVA: 0x00067BE0 File Offset: 0x00065DE0
		public ADObjectId IntendedMailboxPlan
		{
			get
			{
				return (ADObjectId)this[ADUserSchema.IntendedMailboxPlan];
			}
			set
			{
				this[ADUserSchema.IntendedMailboxPlan] = value;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001756 RID: 5974 RVA: 0x00067BEE File Offset: 0x00065DEE
		// (set) Token: 0x06001757 RID: 5975 RVA: 0x00067C00 File Offset: 0x00065E00
		public bool IsExcludedFromServingHierarchy
		{
			get
			{
				return (bool)this[ADRecipientSchema.IsExcludedFromServingHierarchy];
			}
			set
			{
				this[ADRecipientSchema.IsExcludedFromServingHierarchy] = value;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001758 RID: 5976 RVA: 0x00067C13 File Offset: 0x00065E13
		// (set) Token: 0x06001759 RID: 5977 RVA: 0x00067C25 File Offset: 0x00065E25
		public bool IsHierarchyReady
		{
			get
			{
				return (bool)this[ADRecipientSchema.IsHierarchyReady];
			}
			set
			{
				this[ADRecipientSchema.IsHierarchyReady] = value;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x0600175A RID: 5978 RVA: 0x00067C38 File Offset: 0x00065E38
		// (set) Token: 0x0600175B RID: 5979 RVA: 0x00067C4A File Offset: 0x00065E4A
		public MailboxProvisioningConstraint MailboxProvisioningConstraint
		{
			get
			{
				return (MailboxProvisioningConstraint)this[ADRecipientSchema.MailboxProvisioningConstraint];
			}
			set
			{
				this[ADRecipientSchema.MailboxProvisioningConstraint] = value;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x0600175C RID: 5980 RVA: 0x00067C58 File Offset: 0x00065E58
		// (set) Token: 0x0600175D RID: 5981 RVA: 0x00067C6A File Offset: 0x00065E6A
		public MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
		{
			get
			{
				return (MultiValuedProperty<MailboxProvisioningConstraint>)this[ADRecipientSchema.MailboxProvisioningPreferences];
			}
			set
			{
				this[ADRecipientSchema.MailboxProvisioningPreferences] = value;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x0600175E RID: 5982 RVA: 0x00067C78 File Offset: 0x00065E78
		// (set) Token: 0x0600175F RID: 5983 RVA: 0x00067C8A File Offset: 0x00065E8A
		public MultiValuedProperty<string> Description
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADRecipientSchema.Description];
			}
			set
			{
				this[ADRecipientSchema.Description] = value;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001760 RID: 5984 RVA: 0x00067C98 File Offset: 0x00065E98
		// (set) Token: 0x06001761 RID: 5985 RVA: 0x00067CAA File Offset: 0x00065EAA
		public bool IsGroupMailboxConfigured
		{
			get
			{
				return (bool)this[ADRecipientSchema.IsGroupMailboxConfigured];
			}
			set
			{
				this[ADRecipientSchema.IsGroupMailboxConfigured] = value;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001762 RID: 5986 RVA: 0x00067CBD File Offset: 0x00065EBD
		// (set) Token: 0x06001763 RID: 5987 RVA: 0x00067CCF File Offset: 0x00065ECF
		public bool GroupMailboxExternalResourcesSet
		{
			get
			{
				return (bool)this[ADRecipientSchema.GroupMailboxExternalResourcesSet];
			}
			set
			{
				this[ADRecipientSchema.GroupMailboxExternalResourcesSet] = value;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001764 RID: 5988 RVA: 0x00067CE2 File Offset: 0x00065EE2
		public long? PasswordLastSetRaw
		{
			get
			{
				return (long?)this.propertyBag[ADUserSchema.PasswordLastSetRaw];
			}
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x00067CFC File Offset: 0x00065EFC
		internal static object PasswordLastSetGetter(IPropertyBag propertyBag)
		{
			long? num = (long?)propertyBag[ADUserSchema.PasswordLastSetRaw];
			if (num != null && num != -1L && num != 0L)
			{
				try
				{
					return new DateTime?(DateTime.FromFileTimeUtc(num.Value));
				}
				catch (ArgumentOutOfRangeException ex)
				{
					throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty(ADUserSchema.PasswordLastSet.Name, ex.Message), ADUserSchema.PasswordLastSet, propertyBag[ADUserSchema.PasswordLastSetRaw]), ex);
				}
			}
			return null;
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001766 RID: 5990 RVA: 0x00067DCC File Offset: 0x00065FCC
		public DateTime? PasswordLastSet
		{
			get
			{
				return (DateTime?)this[ADUserSchema.PasswordLastSet];
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06001767 RID: 5991 RVA: 0x00067DDE File Offset: 0x00065FDE
		// (set) Token: 0x06001768 RID: 5992 RVA: 0x00067DF0 File Offset: 0x00065FF0
		public bool ResetPasswordOnNextLogon
		{
			get
			{
				return (bool)this[ADUserSchema.ResetPasswordOnNextLogon];
			}
			set
			{
				this[ADUserSchema.ResetPasswordOnNextLogon] = value;
			}
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x00067E04 File Offset: 0x00066004
		internal static object ResetPasswordOnNextLogonGetter(IPropertyBag propertyBag)
		{
			long? num = (long?)propertyBag[ADUserSchema.PasswordLastSetRaw];
			if (num != null && num == 0L)
			{
				return true;
			}
			return false;
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x00067E54 File Offset: 0x00066054
		internal static void ResetPasswordOnNextLogonSetter(object value, IPropertyBag propertyBag)
		{
			bool flag = (bool)value;
			if (flag)
			{
				propertyBag[ADUserSchema.PasswordLastSetRaw] = new long?(0L);
				return;
			}
			propertyBag[ADUserSchema.PasswordLastSetRaw] = new long?(-1L);
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x0600176B RID: 5995 RVA: 0x00067E9A File Offset: 0x0006609A
		// (set) Token: 0x0600176C RID: 5996 RVA: 0x00067EAC File Offset: 0x000660AC
		public MultiValuedProperty<ADObjectId> RMSComputerAccounts
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADUserSchema.RMSComputerAccounts];
			}
			set
			{
				this[ADUserSchema.RMSComputerAccounts] = value;
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x0600176D RID: 5997 RVA: 0x00067EBA File Offset: 0x000660BA
		// (set) Token: 0x0600176E RID: 5998 RVA: 0x00067ECC File Offset: 0x000660CC
		internal bool UMEnabled
		{
			get
			{
				return (bool)this[ADUserSchema.UMEnabled];
			}
			set
			{
				this[ADUserSchema.UMEnabled] = value;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x0600176F RID: 5999 RVA: 0x00067EDF File Offset: 0x000660DF
		// (set) Token: 0x06001770 RID: 6000 RVA: 0x00067EF1 File Offset: 0x000660F1
		public UMEnabledFlags UMEnabledFlags
		{
			get
			{
				return (UMEnabledFlags)this[ADUserSchema.UMEnabledFlags];
			}
			set
			{
				this[ADUserSchema.UMEnabledFlags] = value;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001771 RID: 6001 RVA: 0x00067F04 File Offset: 0x00066104
		// (set) Token: 0x06001772 RID: 6002 RVA: 0x00067F16 File Offset: 0x00066116
		public int UMEnabledFlags2
		{
			get
			{
				return (int)this[ADUserSchema.UMEnabledFlags2];
			}
			internal set
			{
				this[ADUserSchema.UMEnabledFlags2] = value;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001773 RID: 6003 RVA: 0x00067F29 File Offset: 0x00066129
		// (set) Token: 0x06001774 RID: 6004 RVA: 0x00067F3B File Offset: 0x0006613B
		public string OperatorNumber
		{
			get
			{
				return (string)this[ADUserSchema.OperatorNumber];
			}
			set
			{
				this[ADUserSchema.OperatorNumber] = value;
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001775 RID: 6005 RVA: 0x00067F49 File Offset: 0x00066149
		// (set) Token: 0x06001776 RID: 6006 RVA: 0x00067F5B File Offset: 0x0006615B
		public string PhoneProviderId
		{
			get
			{
				return (string)this[ADUserSchema.PhoneProviderId];
			}
			set
			{
				this[ADUserSchema.PhoneProviderId] = value;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001777 RID: 6007 RVA: 0x00067F69 File Offset: 0x00066169
		// (set) Token: 0x06001778 RID: 6008 RVA: 0x00067F7B File Offset: 0x0006617B
		public byte[] UMPinChecksum
		{
			get
			{
				return (byte[])this[ADUserSchema.UMPinChecksum];
			}
			set
			{
				this[ADUserSchema.UMPinChecksum] = value;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001779 RID: 6009 RVA: 0x00067F89 File Offset: 0x00066189
		// (set) Token: 0x0600177A RID: 6010 RVA: 0x00067F9B File Offset: 0x0006619B
		public ADObjectId UMMailboxPolicy
		{
			get
			{
				return (ADObjectId)this[ADUserSchema.UMMailboxPolicy];
			}
			internal set
			{
				this[ADUserSchema.UMMailboxPolicy] = value;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x0600177B RID: 6011 RVA: 0x00067FA9 File Offset: 0x000661A9
		// (set) Token: 0x0600177C RID: 6012 RVA: 0x00067FBB File Offset: 0x000661BB
		public AudioCodecEnum? CallAnsweringAudioCodec
		{
			get
			{
				return (AudioCodecEnum?)this[ADUserSchema.CallAnsweringAudioCodec];
			}
			set
			{
				this[ADUserSchema.CallAnsweringAudioCodec] = value;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x0600177D RID: 6013 RVA: 0x00067FCE File Offset: 0x000661CE
		// (set) Token: 0x0600177E RID: 6014 RVA: 0x00067FE0 File Offset: 0x000661E0
		internal UMServerWritableFlagsBits UMServerWritableFlags
		{
			get
			{
				return (UMServerWritableFlagsBits)this[ADUserSchema.UMServerWritableFlags];
			}
			set
			{
				this[ADUserSchema.UMServerWritableFlags] = value;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x0600177F RID: 6015 RVA: 0x00067FF3 File Offset: 0x000661F3
		// (set) Token: 0x06001780 RID: 6016 RVA: 0x00068005 File Offset: 0x00066205
		public UserAccountControlFlags UserAccountControl
		{
			get
			{
				return (UserAccountControlFlags)this[ADUserSchema.UserAccountControl];
			}
			set
			{
				this[ADUserSchema.UserAccountControl] = value;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001781 RID: 6017 RVA: 0x00068018 File Offset: 0x00066218
		// (set) Token: 0x06001782 RID: 6018 RVA: 0x0006802A File Offset: 0x0006622A
		public MultiValuedProperty<byte[]> UserCertificate
		{
			get
			{
				return (MultiValuedProperty<byte[]>)this[ADRecipientSchema.Certificate];
			}
			set
			{
				this[ADRecipientSchema.Certificate] = value;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001783 RID: 6019 RVA: 0x00068038 File Offset: 0x00066238
		// (set) Token: 0x06001784 RID: 6020 RVA: 0x0006804A File Offset: 0x0006624A
		public string UserPrincipalName
		{
			get
			{
				return (string)this[ADUserSchema.UserPrincipalName];
			}
			set
			{
				this[ADUserSchema.UserPrincipalName] = value;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001785 RID: 6021 RVA: 0x00068058 File Offset: 0x00066258
		// (set) Token: 0x06001786 RID: 6022 RVA: 0x0006806A File Offset: 0x0006626A
		public MultiValuedProperty<byte[]> UserSMIMECertificate
		{
			get
			{
				return (MultiValuedProperty<byte[]>)this[ADRecipientSchema.SMimeCertificate];
			}
			set
			{
				this[ADRecipientSchema.SMimeCertificate] = value;
			}
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x00068078 File Offset: 0x00066278
		internal static object UserPrincipalNameGetter(IPropertyBag propertyBag)
		{
			return ADUser.MangleUserPrincipalNameDomain(propertyBag, (string)propertyBag[ADUserSchema.UserPrincipalNameRaw], '#', '.');
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x00068094 File Offset: 0x00066294
		internal static void UserPrincipalNameSetter(object value, IPropertyBag propertyBag)
		{
			string value2 = ADUser.MangleUserPrincipalNameDomain(propertyBag, (string)value, '.', '#');
			propertyBag[ADUserSchema.UserPrincipalNameRaw] = value2;
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x000680C0 File Offset: 0x000662C0
		private static string MangleUserPrincipalNameDomain(IPropertyBag propertyBag, string userPrincipalName, char fromChar, char toChar)
		{
			ADObjectId adobjectId = (ADObjectId)propertyBag[ADObjectSchema.Id];
			if (userPrincipalName != null && adobjectId != null && adobjectId.DistinguishedName.Contains(",OU=Soft Deleted Objects,"))
			{
				int num = userPrincipalName.IndexOf('@');
				if (num >= 0)
				{
					StringBuilder stringBuilder = new StringBuilder(userPrincipalName, userPrincipalName.Length);
					stringBuilder.Replace(fromChar, toChar, num + 1, userPrincipalName.Length - num - 1);
					userPrincipalName = stringBuilder.ToString();
				}
			}
			return userPrincipalName;
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x0600178A RID: 6026 RVA: 0x0006812F File Offset: 0x0006632F
		// (set) Token: 0x0600178B RID: 6027 RVA: 0x00068141 File Offset: 0x00066341
		public MultiValuedProperty<string> ActiveSyncAllowedDeviceIDs
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADUserSchema.ActiveSyncAllowedDeviceIDs];
			}
			set
			{
				this[ADUserSchema.ActiveSyncAllowedDeviceIDs] = value;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x0600178C RID: 6028 RVA: 0x0006814F File Offset: 0x0006634F
		// (set) Token: 0x0600178D RID: 6029 RVA: 0x00068161 File Offset: 0x00066361
		public MultiValuedProperty<string> ActiveSyncBlockedDeviceIDs
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADUserSchema.ActiveSyncBlockedDeviceIDs];
			}
			set
			{
				this[ADUserSchema.ActiveSyncBlockedDeviceIDs] = value;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x0600178E RID: 6030 RVA: 0x0006816F File Offset: 0x0006636F
		// (set) Token: 0x0600178F RID: 6031 RVA: 0x00068181 File Offset: 0x00066381
		public ADObjectId ActiveSyncMailboxPolicy
		{
			get
			{
				return (ADObjectId)this[ADUserSchema.ActiveSyncMailboxPolicy];
			}
			set
			{
				this[ADUserSchema.ActiveSyncMailboxPolicy] = value;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001790 RID: 6032 RVA: 0x0006818F File Offset: 0x0006638F
		// (set) Token: 0x06001791 RID: 6033 RVA: 0x000681A1 File Offset: 0x000663A1
		public MultiValuedProperty<Capability> PersistedCapabilities
		{
			get
			{
				return (MultiValuedProperty<Capability>)this[ADUserSchema.PersistedCapabilities];
			}
			set
			{
				this[ADUserSchema.PersistedCapabilities] = value;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001792 RID: 6034 RVA: 0x000681AF File Offset: 0x000663AF
		// (set) Token: 0x06001793 RID: 6035 RVA: 0x000681BC File Offset: 0x000663BC
		public Capability? SKUCapability
		{
			get
			{
				return CapabilityHelper.GetSKUCapability(this.PersistedCapabilities);
			}
			set
			{
				CapabilityHelper.SetSKUCapability(value, this.PersistedCapabilities);
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001794 RID: 6036 RVA: 0x000681CA File Offset: 0x000663CA
		// (set) Token: 0x06001795 RID: 6037 RVA: 0x000681DC File Offset: 0x000663DC
		public bool? SKUAssigned
		{
			get
			{
				return (bool?)this[ADRecipientSchema.SKUAssigned];
			}
			set
			{
				this[ADRecipientSchema.SKUAssigned] = value;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001796 RID: 6038 RVA: 0x000681EF File Offset: 0x000663EF
		// (set) Token: 0x06001797 RID: 6039 RVA: 0x00068201 File Offset: 0x00066401
		public ADObjectId OwaMailboxPolicy
		{
			get
			{
				return (ADObjectId)this[ADUserSchema.OwaMailboxPolicy];
			}
			set
			{
				this[ADUserSchema.OwaMailboxPolicy] = value;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001798 RID: 6040 RVA: 0x0006820F File Offset: 0x0006640F
		// (set) Token: 0x06001799 RID: 6041 RVA: 0x00068221 File Offset: 0x00066421
		internal ADObjectId PreviousDatabase
		{
			get
			{
				return (ADObjectId)this[IADMailStorageSchema.PreviousDatabase];
			}
			set
			{
				this[IADMailStorageSchema.PreviousDatabase] = value;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x0600179A RID: 6042 RVA: 0x0006822F File Offset: 0x0006642F
		// (set) Token: 0x0600179B RID: 6043 RVA: 0x00068241 File Offset: 0x00066441
		public bool UseDatabaseRetentionDefaults
		{
			get
			{
				return (bool)this[IADMailStorageSchema.UseDatabaseRetentionDefaults];
			}
			set
			{
				this[IADMailStorageSchema.UseDatabaseRetentionDefaults] = value;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x0600179C RID: 6044 RVA: 0x00068254 File Offset: 0x00066454
		// (set) Token: 0x0600179D RID: 6045 RVA: 0x00068266 File Offset: 0x00066466
		public bool RetainDeletedItemsUntilBackup
		{
			get
			{
				return (bool)this[IADMailStorageSchema.RetainDeletedItemsUntilBackup];
			}
			set
			{
				this[IADMailStorageSchema.RetainDeletedItemsUntilBackup] = value;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x0600179E RID: 6046 RVA: 0x00068279 File Offset: 0x00066479
		// (set) Token: 0x0600179F RID: 6047 RVA: 0x0006828B File Offset: 0x0006648B
		public Guid? MailboxContainerGuid
		{
			get
			{
				return (Guid?)this[ADUserSchema.MailboxContainerGuid];
			}
			set
			{
				this[ADUserSchema.MailboxContainerGuid] = value;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x060017A0 RID: 6048 RVA: 0x0006829E File Offset: 0x0006649E
		public MultiValuedProperty<Guid> AggregatedMailboxGuids
		{
			get
			{
				return (MultiValuedProperty<Guid>)this[ADUserSchema.AggregatedMailboxGuids];
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x060017A1 RID: 6049 RVA: 0x000682B0 File Offset: 0x000664B0
		// (set) Token: 0x060017A2 RID: 6050 RVA: 0x000682C2 File Offset: 0x000664C2
		public CrossTenantObjectId UnifiedMailbox
		{
			get
			{
				return (CrossTenantObjectId)this[ADUserSchema.UnifiedMailbox];
			}
			set
			{
				this[ADUserSchema.UnifiedMailbox] = value;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x060017A3 RID: 6051 RVA: 0x000682D0 File Offset: 0x000664D0
		// (set) Token: 0x060017A4 RID: 6052 RVA: 0x000682E2 File Offset: 0x000664E2
		public Guid PreviousExchangeGuid
		{
			get
			{
				return (Guid)this[IADMailStorageSchema.PreviousExchangeGuid];
			}
			set
			{
				this[IADMailStorageSchema.PreviousExchangeGuid] = value;
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x060017A5 RID: 6053 RVA: 0x000682F5 File Offset: 0x000664F5
		// (set) Token: 0x060017A6 RID: 6054 RVA: 0x0006830A File Offset: 0x0006650A
		public bool MessageTrackingReadStatusEnabled
		{
			get
			{
				return !(bool)this[ADRecipientSchema.MessageTrackingReadStatusDisabled];
			}
			set
			{
				this[ADRecipientSchema.MessageTrackingReadStatusDisabled] = !value;
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x060017A7 RID: 6055 RVA: 0x00068320 File Offset: 0x00066520
		internal MultiValuedProperty<string> Extensions
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADRecipientSchema.Extensions];
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x060017A8 RID: 6056 RVA: 0x00068332 File Offset: 0x00066532
		// (set) Token: 0x060017A9 RID: 6057 RVA: 0x00068344 File Offset: 0x00066544
		[ProvisionalClone(CloneSet.CloneExtendedSet)]
		public Unlimited<ByteQuantifiedSize> RecoverableItemsQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ADUserSchema.RecoverableItemsQuota];
			}
			set
			{
				this[ADUserSchema.RecoverableItemsQuota] = value;
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060017AA RID: 6058 RVA: 0x00068357 File Offset: 0x00066557
		// (set) Token: 0x060017AB RID: 6059 RVA: 0x00068369 File Offset: 0x00066569
		[ProvisionalClone(CloneSet.CloneExtendedSet)]
		public Unlimited<ByteQuantifiedSize> RecoverableItemsWarningQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ADUserSchema.RecoverableItemsWarningQuota];
			}
			set
			{
				this[ADUserSchema.RecoverableItemsWarningQuota] = value;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060017AC RID: 6060 RVA: 0x0006837C File Offset: 0x0006657C
		// (set) Token: 0x060017AD RID: 6061 RVA: 0x0006838E File Offset: 0x0006658E
		[ProvisionalClone(CloneSet.CloneExtendedSet)]
		public Unlimited<ByteQuantifiedSize> CalendarLoggingQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ADUserSchema.CalendarLoggingQuota];
			}
			set
			{
				this[ADUserSchema.CalendarLoggingQuota] = value;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060017AE RID: 6062 RVA: 0x000683A1 File Offset: 0x000665A1
		// (set) Token: 0x060017AF RID: 6063 RVA: 0x000683B3 File Offset: 0x000665B3
		public bool DowngradeHighPriorityMessagesEnabled
		{
			get
			{
				return (bool)this[ADUserSchema.DowngradeHighPriorityMessagesEnabled];
			}
			set
			{
				this[ADUserSchema.DowngradeHighPriorityMessagesEnabled] = value;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060017B0 RID: 6064 RVA: 0x000683C6 File Offset: 0x000665C6
		public string StorageGroupName
		{
			get
			{
				return (string)this[IADMailStorageSchema.StorageGroupName];
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060017B1 RID: 6065 RVA: 0x000683D8 File Offset: 0x000665D8
		// (set) Token: 0x060017B2 RID: 6066 RVA: 0x000683EA File Offset: 0x000665EA
		public NetID NetID
		{
			get
			{
				return (NetID)this[IADSecurityPrincipalSchema.NetID];
			}
			internal set
			{
				this[IADSecurityPrincipalSchema.NetID] = value;
				if (!string.IsNullOrEmpty((string)this[IADSecurityPrincipalSchema.NetIDSuffix]))
				{
					this.NetIDSuffix = this.netIDSuffixCopy;
				}
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060017B3 RID: 6067 RVA: 0x0006841B File Offset: 0x0006661B
		// (set) Token: 0x060017B4 RID: 6068 RVA: 0x00068441 File Offset: 0x00066641
		public string NetIDSuffix
		{
			get
			{
				if (!string.IsNullOrEmpty(this.netIDSuffixCopy))
				{
					return this.netIDSuffixCopy;
				}
				return (string)this[IADSecurityPrincipalSchema.NetIDSuffix];
			}
			internal set
			{
				this.netIDSuffixCopy = value;
				this[IADSecurityPrincipalSchema.NetIDSuffix] = value;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060017B5 RID: 6069 RVA: 0x00068456 File Offset: 0x00066656
		// (set) Token: 0x060017B6 RID: 6070 RVA: 0x00068468 File Offset: 0x00066668
		public NetID OriginalNetID
		{
			get
			{
				return (NetID)this[IADSecurityPrincipalSchema.OriginalNetID];
			}
			internal set
			{
				this[IADSecurityPrincipalSchema.OriginalNetID] = value;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060017B7 RID: 6071 RVA: 0x00068476 File Offset: 0x00066676
		// (set) Token: 0x060017B8 RID: 6072 RVA: 0x00068488 File Offset: 0x00066688
		public NetID ConsumerNetID
		{
			get
			{
				return (NetID)this[IADSecurityPrincipalSchema.ConsumerNetID];
			}
			internal set
			{
				this[IADSecurityPrincipalSchema.ConsumerNetID] = value;
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060017B9 RID: 6073 RVA: 0x00068496 File Offset: 0x00066696
		// (set) Token: 0x060017BA RID: 6074 RVA: 0x000684A8 File Offset: 0x000666A8
		public SmtpAddress WindowsLiveID
		{
			get
			{
				return (SmtpAddress)this[ADRecipientSchema.WindowsLiveID];
			}
			set
			{
				this[ADRecipientSchema.WindowsLiveID] = value;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060017BB RID: 6075 RVA: 0x000684BB File Offset: 0x000666BB
		// (set) Token: 0x060017BC RID: 6076 RVA: 0x000684CD File Offset: 0x000666CD
		public MultiValuedProperty<X509Identifier> CertificateSubject
		{
			get
			{
				return (MultiValuedProperty<X509Identifier>)this[ADUserSchema.CertificateSubject];
			}
			internal set
			{
				this[ADUserSchema.CertificateSubject] = value;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060017BD RID: 6077 RVA: 0x000684DB File Offset: 0x000666DB
		// (set) Token: 0x060017BE RID: 6078 RVA: 0x000684ED File Offset: 0x000666ED
		public ADObjectId ArchiveDatabaseRaw
		{
			get
			{
				return (ADObjectId)this[ADUserSchema.ArchiveDatabaseRaw];
			}
			set
			{
				this[ADUserSchema.ArchiveDatabaseRaw] = value;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060017BF RID: 6079 RVA: 0x000684FB File Offset: 0x000666FB
		// (set) Token: 0x060017C0 RID: 6080 RVA: 0x0006850D File Offset: 0x0006670D
		public ADObjectId ArchiveDatabase
		{
			get
			{
				return (ADObjectId)this[ADUserSchema.ArchiveDatabase];
			}
			set
			{
				this[ADUserSchema.ArchiveDatabase] = value;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060017C1 RID: 6081 RVA: 0x0006851B File Offset: 0x0006671B
		// (set) Token: 0x060017C2 RID: 6082 RVA: 0x0006852D File Offset: 0x0006672D
		public Guid ArchiveGuid
		{
			get
			{
				return (Guid)this[ADUserSchema.ArchiveGuid];
			}
			internal set
			{
				this[ADUserSchema.ArchiveGuid] = value;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060017C3 RID: 6083 RVA: 0x00068540 File Offset: 0x00066740
		// (set) Token: 0x060017C4 RID: 6084 RVA: 0x00068552 File Offset: 0x00066752
		public bool IsAuxMailbox
		{
			get
			{
				return (bool)this[ADUserSchema.IsAuxMailbox];
			}
			internal set
			{
				this[ADUserSchema.IsAuxMailbox] = value;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060017C5 RID: 6085 RVA: 0x00068565 File Offset: 0x00066765
		// (set) Token: 0x060017C6 RID: 6086 RVA: 0x00068577 File Offset: 0x00066777
		public ADObjectId AuxMailboxParentObjectId
		{
			get
			{
				return (ADObjectId)this[ADUserSchema.AuxMailboxParentObjectId];
			}
			set
			{
				this[ADUserSchema.AuxMailboxParentObjectId] = value;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060017C7 RID: 6087 RVA: 0x00068585 File Offset: 0x00066785
		public MultiValuedProperty<ADObjectId> ChildAuxMailboxObjectIds
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADUserSchema.AuxMailboxParentObjectIdBL];
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060017C8 RID: 6088 RVA: 0x00068597 File Offset: 0x00066797
		public MailboxRelationType MailboxRelationType
		{
			get
			{
				return (MailboxRelationType)this[ADUserSchema.MailboxRelationType];
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060017C9 RID: 6089 RVA: 0x000685A9 File Offset: 0x000667A9
		// (set) Token: 0x060017CA RID: 6090 RVA: 0x000685BB File Offset: 0x000667BB
		public MultiValuedProperty<string> ArchiveName
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADUserSchema.ArchiveName];
			}
			set
			{
				this[ADUserSchema.ArchiveName] = value;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060017CB RID: 6091 RVA: 0x000685C9 File Offset: 0x000667C9
		// (set) Token: 0x060017CC RID: 6092 RVA: 0x000685DB File Offset: 0x000667DB
		public Unlimited<ByteQuantifiedSize> ArchiveQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ADUserSchema.ArchiveQuota];
			}
			set
			{
				this[ADUserSchema.ArchiveQuota] = value;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060017CD RID: 6093 RVA: 0x000685EE File Offset: 0x000667EE
		// (set) Token: 0x060017CE RID: 6094 RVA: 0x00068600 File Offset: 0x00066800
		public Unlimited<ByteQuantifiedSize> ArchiveWarningQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ADUserSchema.ArchiveWarningQuota];
			}
			set
			{
				this[ADUserSchema.ArchiveWarningQuota] = value;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060017CF RID: 6095 RVA: 0x00068613 File Offset: 0x00066813
		// (set) Token: 0x060017D0 RID: 6096 RVA: 0x00068625 File Offset: 0x00066825
		public SmtpDomain ArchiveDomain
		{
			get
			{
				return (SmtpDomain)this[ADUserSchema.ArchiveDomain];
			}
			internal set
			{
				this[ADUserSchema.ArchiveDomain] = value;
				if (value != null)
				{
					this[ADUserSchema.ArchiveDatabase] = null;
				}
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060017D1 RID: 6097 RVA: 0x00068642 File Offset: 0x00066842
		// (set) Token: 0x060017D2 RID: 6098 RVA: 0x00068654 File Offset: 0x00066854
		public ArchiveStatusFlags ArchiveStatus
		{
			get
			{
				return (ArchiveStatusFlags)this[ADUserSchema.ArchiveStatus];
			}
			internal set
			{
				this[ADUserSchema.ArchiveStatus] = value;
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060017D3 RID: 6099 RVA: 0x00068667 File Offset: 0x00066867
		// (set) Token: 0x060017D4 RID: 6100 RVA: 0x00068679 File Offset: 0x00066879
		public ADObjectId DisabledArchiveDatabase
		{
			get
			{
				return (ADObjectId)this[ADUserSchema.DisabledArchiveDatabase];
			}
			internal set
			{
				this[ADUserSchema.DisabledArchiveDatabase] = value;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060017D5 RID: 6101 RVA: 0x00068687 File Offset: 0x00066887
		// (set) Token: 0x060017D6 RID: 6102 RVA: 0x00068699 File Offset: 0x00066899
		public Guid DisabledArchiveGuid
		{
			get
			{
				return (Guid)this[ADUserSchema.DisabledArchiveGuid];
			}
			internal set
			{
				this[ADUserSchema.DisabledArchiveGuid] = value;
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060017D7 RID: 6103 RVA: 0x000686AC File Offset: 0x000668AC
		// (set) Token: 0x060017D8 RID: 6104 RVA: 0x000686BE File Offset: 0x000668BE
		public ADObjectId MailboxMoveTargetMDB
		{
			get
			{
				return (ADObjectId)this[IADMailStorageSchema.MailboxMoveTargetMDB];
			}
			internal set
			{
				this[IADMailStorageSchema.MailboxMoveTargetMDB] = value;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060017D9 RID: 6105 RVA: 0x000686CC File Offset: 0x000668CC
		// (set) Token: 0x060017DA RID: 6106 RVA: 0x000686DE File Offset: 0x000668DE
		public ADObjectId MailboxMoveSourceMDB
		{
			get
			{
				return (ADObjectId)this[IADMailStorageSchema.MailboxMoveSourceMDB];
			}
			internal set
			{
				this[IADMailStorageSchema.MailboxMoveSourceMDB] = value;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060017DB RID: 6107 RVA: 0x000686EC File Offset: 0x000668EC
		// (set) Token: 0x060017DC RID: 6108 RVA: 0x000686FE File Offset: 0x000668FE
		public ADObjectId MailboxMoveTargetArchiveMDB
		{
			get
			{
				return (ADObjectId)this[IADMailStorageSchema.MailboxMoveTargetArchiveMDB];
			}
			internal set
			{
				this[IADMailStorageSchema.MailboxMoveTargetArchiveMDB] = value;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060017DD RID: 6109 RVA: 0x0006870C File Offset: 0x0006690C
		// (set) Token: 0x060017DE RID: 6110 RVA: 0x0006871E File Offset: 0x0006691E
		public ADObjectId MailboxMoveSourceArchiveMDB
		{
			get
			{
				return (ADObjectId)this[IADMailStorageSchema.MailboxMoveSourceArchiveMDB];
			}
			internal set
			{
				this[IADMailStorageSchema.MailboxMoveSourceArchiveMDB] = value;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060017DF RID: 6111 RVA: 0x0006872C File Offset: 0x0006692C
		// (set) Token: 0x060017E0 RID: 6112 RVA: 0x0006873E File Offset: 0x0006693E
		public RequestFlags MailboxMoveFlags
		{
			get
			{
				return (RequestFlags)this[IADMailStorageSchema.MailboxMoveFlags];
			}
			internal set
			{
				this[IADMailStorageSchema.MailboxMoveFlags] = value;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060017E1 RID: 6113 RVA: 0x00068751 File Offset: 0x00066951
		// (set) Token: 0x060017E2 RID: 6114 RVA: 0x00068763 File Offset: 0x00066963
		public string MailboxMoveRemoteHostName
		{
			get
			{
				return (string)this[IADMailStorageSchema.MailboxMoveRemoteHostName];
			}
			internal set
			{
				this[IADMailStorageSchema.MailboxMoveRemoteHostName] = value;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060017E3 RID: 6115 RVA: 0x00068771 File Offset: 0x00066971
		// (set) Token: 0x060017E4 RID: 6116 RVA: 0x00068783 File Offset: 0x00066983
		public string MailboxMoveBatchName
		{
			get
			{
				return (string)this[IADMailStorageSchema.MailboxMoveBatchName];
			}
			internal set
			{
				this[IADMailStorageSchema.MailboxMoveBatchName] = value;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060017E5 RID: 6117 RVA: 0x00068791 File Offset: 0x00066991
		// (set) Token: 0x060017E6 RID: 6118 RVA: 0x000687A3 File Offset: 0x000669A3
		public RequestStatus MailboxMoveStatus
		{
			get
			{
				return (RequestStatus)this[IADMailStorageSchema.MailboxMoveStatus];
			}
			internal set
			{
				this[IADMailStorageSchema.MailboxMoveStatus] = value;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060017E7 RID: 6119 RVA: 0x000687B8 File Offset: 0x000669B8
		// (set) Token: 0x060017E8 RID: 6120 RVA: 0x000687E2 File Offset: 0x000669E2
		public MailboxRelease MailboxRelease
		{
			get
			{
				MailboxRelease result;
				if (!Enum.TryParse<MailboxRelease>((string)this[ADUserSchema.MailboxRelease], true, out result))
				{
					return MailboxRelease.None;
				}
				return result;
			}
			internal set
			{
				this[ADUserSchema.MailboxRelease] = ((value == MailboxRelease.None) ? null : value.ToString());
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060017E9 RID: 6121 RVA: 0x00068800 File Offset: 0x00066A00
		// (set) Token: 0x060017EA RID: 6122 RVA: 0x0006882A File Offset: 0x00066A2A
		public MailboxRelease ArchiveRelease
		{
			get
			{
				MailboxRelease result;
				if (!Enum.TryParse<MailboxRelease>((string)this[ADUserSchema.ArchiveRelease], true, out result))
				{
					return MailboxRelease.None;
				}
				return result;
			}
			internal set
			{
				this[ADUserSchema.ArchiveRelease] = ((value == MailboxRelease.None) ? null : value.ToString());
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x060017EB RID: 6123 RVA: 0x00068848 File Offset: 0x00066A48
		// (set) Token: 0x060017EC RID: 6124 RVA: 0x0006885A File Offset: 0x00066A5A
		public string MailboxPlanIndex
		{
			get
			{
				return (string)this[ADRecipientSchema.MailboxPlanIndex];
			}
			set
			{
				this[ADRecipientSchema.MailboxPlanIndex] = value;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x060017ED RID: 6125 RVA: 0x00068868 File Offset: 0x00066A68
		// (set) Token: 0x060017EE RID: 6126 RVA: 0x0006887A File Offset: 0x00066A7A
		public DateTime? TeamMailboxClosedTime
		{
			get
			{
				return (DateTime?)this[ADUserSchema.TeamMailboxClosedTime];
			}
			internal set
			{
				this[ADUserSchema.TeamMailboxClosedTime] = value;
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x060017EF RID: 6127 RVA: 0x0006888D File Offset: 0x00066A8D
		// (set) Token: 0x060017F0 RID: 6128 RVA: 0x0006889F File Offset: 0x00066A9F
		public ADObjectId SharePointLinkedBy
		{
			get
			{
				return (ADObjectId)this[ADUserSchema.SharePointLinkedBy];
			}
			internal set
			{
				this[ADUserSchema.SharePointLinkedBy] = value;
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x060017F1 RID: 6129 RVA: 0x000688AD File Offset: 0x00066AAD
		// (set) Token: 0x060017F2 RID: 6130 RVA: 0x000688BF File Offset: 0x00066ABF
		public MultiValuedProperty<ADObjectId> Owners
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADUserSchema.Owners];
			}
			internal set
			{
				this[ADUserSchema.Owners] = value;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x060017F3 RID: 6131 RVA: 0x000688CD File Offset: 0x00066ACD
		// (set) Token: 0x060017F4 RID: 6132 RVA: 0x000688DF File Offset: 0x00066ADF
		public MultiValuedProperty<ADObjectId> TeamMailboxShowInClientList
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADUserSchema.TeamMailboxShowInClientList];
			}
			internal set
			{
				this[ADUserSchema.TeamMailboxShowInClientList] = value;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x060017F5 RID: 6133 RVA: 0x000688ED File Offset: 0x00066AED
		// (set) Token: 0x060017F6 RID: 6134 RVA: 0x000688FF File Offset: 0x00066AFF
		public UserConfigXML ConfigXML
		{
			get
			{
				return (UserConfigXML)this[ADRecipientSchema.ConfigurationXML];
			}
			set
			{
				this[ADRecipientSchema.ConfigurationXML] = value;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x060017F7 RID: 6135 RVA: 0x0006890D File Offset: 0x00066B0D
		// (set) Token: 0x060017F8 RID: 6136 RVA: 0x0006891F File Offset: 0x00066B1F
		public UpgradeStatusTypes UpgradeStatus
		{
			get
			{
				return (UpgradeStatusTypes)this[ADRecipientSchema.UpgradeStatus];
			}
			set
			{
				this[ADRecipientSchema.UpgradeStatus] = value;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060017F9 RID: 6137 RVA: 0x00068932 File Offset: 0x00066B32
		// (set) Token: 0x060017FA RID: 6138 RVA: 0x00068944 File Offset: 0x00066B44
		public UpgradeRequestTypes UpgradeRequest
		{
			get
			{
				return (UpgradeRequestTypes)this[ADRecipientSchema.UpgradeRequest];
			}
			set
			{
				this[ADRecipientSchema.UpgradeRequest] = value;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x060017FB RID: 6139 RVA: 0x00068957 File Offset: 0x00066B57
		// (set) Token: 0x060017FC RID: 6140 RVA: 0x00068969 File Offset: 0x00066B69
		public string UpgradeDetails
		{
			get
			{
				return (string)this[ADRecipientSchema.UpgradeDetails];
			}
			set
			{
				this[ADRecipientSchema.UpgradeDetails] = value;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x060017FD RID: 6141 RVA: 0x00068977 File Offset: 0x00066B77
		// (set) Token: 0x060017FE RID: 6142 RVA: 0x00068989 File Offset: 0x00066B89
		public string UpgradeMessage
		{
			get
			{
				return (string)this[ADRecipientSchema.UpgradeMessage];
			}
			set
			{
				this[ADRecipientSchema.UpgradeMessage] = value;
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x060017FF RID: 6143 RVA: 0x00068997 File Offset: 0x00066B97
		// (set) Token: 0x06001800 RID: 6144 RVA: 0x000689A9 File Offset: 0x00066BA9
		public UpgradeStage? UpgradeStage
		{
			get
			{
				return (UpgradeStage?)this[ADRecipientSchema.UpgradeStage];
			}
			set
			{
				this[ADRecipientSchema.UpgradeStage] = value;
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001801 RID: 6145 RVA: 0x000689BC File Offset: 0x00066BBC
		// (set) Token: 0x06001802 RID: 6146 RVA: 0x000689CE File Offset: 0x00066BCE
		public DateTime? UpgradeStageTimeStamp
		{
			get
			{
				return (DateTime?)this[ADRecipientSchema.UpgradeStageTimeStamp];
			}
			set
			{
				this[ADRecipientSchema.UpgradeStageTimeStamp] = value;
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001803 RID: 6147 RVA: 0x000689E1 File Offset: 0x00066BE1
		// (set) Token: 0x06001804 RID: 6148 RVA: 0x000689F3 File Offset: 0x00066BF3
		public ReleaseTrack? ReleaseTrack
		{
			get
			{
				return (ReleaseTrack?)this[ADRecipientSchema.ReleaseTrack];
			}
			set
			{
				this[ADRecipientSchema.ReleaseTrack] = value;
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001805 RID: 6149 RVA: 0x00068A06 File Offset: 0x00066C06
		public MultiValuedProperty<Guid> MailboxGuids
		{
			get
			{
				return (MultiValuedProperty<Guid>)this[ADRecipientSchema.MailboxGuidsRaw];
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001806 RID: 6150 RVA: 0x00068A18 File Offset: 0x00066C18
		// (set) Token: 0x06001807 RID: 6151 RVA: 0x00068A38 File Offset: 0x00066C38
		public IMailboxLocationCollection MailboxLocations
		{
			get
			{
				if (ADRecipient.IsMailboxLocationsEnabled(this))
				{
					return (IMailboxLocationCollection)this[ADRecipientSchema.MailboxLocations];
				}
				return new MailboxLocationCollection();
			}
			internal set
			{
				if (ADRecipient.IsMailboxLocationsEnabled(this))
				{
					this[ADRecipientSchema.MailboxLocations] = value;
				}
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001808 RID: 6152 RVA: 0x00068A4E File Offset: 0x00066C4E
		// (set) Token: 0x06001809 RID: 6153 RVA: 0x00068A60 File Offset: 0x00066C60
		internal SharingPartnerIdentityCollection SharingPartnerIdentities
		{
			get
			{
				return (SharingPartnerIdentityCollection)this[ADUserSchema.SharingPartnerIdentities];
			}
			set
			{
				this[ADUserSchema.SharingPartnerIdentities] = value;
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x0600180A RID: 6154 RVA: 0x00068A6E File Offset: 0x00066C6E
		// (set) Token: 0x0600180B RID: 6155 RVA: 0x00068A80 File Offset: 0x00066C80
		internal SharingAnonymousIdentityCollection SharingAnonymousIdentities
		{
			get
			{
				return (SharingAnonymousIdentityCollection)this[ADUserSchema.SharingAnonymousIdentities];
			}
			set
			{
				this[ADUserSchema.SharingAnonymousIdentities] = value;
			}
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x00068A8E File Offset: 0x00066C8E
		internal static string StorageGroupNameGetter(IPropertyBag propertyBag)
		{
			return ADUser.GetStorageGroupNameFromDatabase((ADObjectId)propertyBag[IADMailStorageSchema.Database]);
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x00068AA8 File Offset: 0x00066CA8
		internal static string GetStorageGroupNameFromDatabase(ADObjectId databaseId)
		{
			string result;
			try
			{
				if (databaseId != null && !string.IsNullOrEmpty(databaseId.DistinguishedName))
				{
					result = databaseId.AncestorDN(1).Rdn.UnescapedName;
				}
				else
				{
					result = string.Empty;
				}
			}
			catch (InvalidOperationException ex)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("StorageGroupName", ex.Message), IADMailStorageSchema.StorageGroupName, databaseId), ex);
			}
			return result;
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x00068B18 File Offset: 0x00066D18
		internal static object DatabaseNameGetter(IPropertyBag propertyBag)
		{
			object result;
			try
			{
				result = ADUser.DatabaseNameFromADObjectId((ADObjectId)propertyBag[IADMailStorageSchema.Database]);
			}
			catch (InvalidOperationException ex)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("DatabaseName", ex.Message), IADMailStorageSchema.DatabaseName, propertyBag[IADMailStorageSchema.Database]), ex);
			}
			return result;
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x00068B7C File Offset: 0x00066D7C
		internal static string DatabaseNameFromADObjectId(ADObjectId homMdb)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (homMdb != null && !string.IsNullOrEmpty(homMdb.DistinguishedName))
			{
				stringBuilder.Append(homMdb.AncestorDN(1).Rdn.UnescapedName);
				stringBuilder.Append('\\');
				stringBuilder.Append(homMdb.AncestorDN(0).Rdn.UnescapedName);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x00068BE0 File Offset: 0x00066DE0
		internal static object IsMailboxEnabledGetter(IPropertyBag propertyBag)
		{
			object obj = propertyBag[IADMailStorageSchema.Database];
			return obj != null;
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x00068C08 File Offset: 0x00066E08
		internal static QueryFilter IsMailboxEnabledFilterBuilder(SinglePropertyFilter filter)
		{
			bool flag = (bool)ADObject.PropertyValueFromEqualityFilter(filter);
			QueryFilter queryFilter = new ExistsFilter(IADMailStorageSchema.Database);
			if (!flag)
			{
				return new NotFilter(queryFilter);
			}
			return queryFilter;
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x00068C37 File Offset: 0x00066E37
		internal static object ServerNameGetter(IPropertyBag propertyBag)
		{
			return DNConvertor.ServerNameFromServerLegacyDN((string)propertyBag[IADMailStorageSchema.ServerLegacyDN]);
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x00068C50 File Offset: 0x00066E50
		internal static QueryFilter ServerNameFilterBuilder(SinglePropertyFilter filter)
		{
			if (!(filter is ComparisonFilter))
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForProperty(filter.Property.Name, filter.GetType(), typeof(ComparisonFilter)));
			}
			ComparisonFilter comparisonFilter = (ComparisonFilter)filter;
			QueryFilter queryFilter = new TextFilter(ADMailboxRecipientSchema.ServerLegacyDN, "/cn=Configuration/cn=Servers/cn=" + (string)comparisonFilter.PropertyValue, MatchOptions.Suffix, MatchFlags.Default);
			switch (comparisonFilter.ComparisonOperator)
			{
			case ComparisonOperator.Equal:
				return queryFilter;
			case ComparisonOperator.NotEqual:
				return new NotFilter(queryFilter);
			default:
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedOperatorForProperty(comparisonFilter.Property.Name, comparisonFilter.ComparisonOperator.ToString()));
			}
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x00068CF9 File Offset: 0x00066EF9
		internal static object UseDatabaseRetentionDefaultsGetter(IPropertyBag propertyBag)
		{
			return (DeletedItemRetention)propertyBag[IADMailStorageSchema.DeletedItemFlags] == DeletedItemRetention.DatabaseDefault;
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x00068D13 File Offset: 0x00066F13
		internal static void UseDatabaseRetentionDefaultsSetter(object value, IPropertyBag propertyBag)
		{
			if ((bool)value)
			{
				propertyBag[IADMailStorageSchema.DeletedItemFlags] = DeletedItemRetention.DatabaseDefault;
				return;
			}
			if ((DeletedItemRetention)propertyBag[IADMailStorageSchema.DeletedItemFlags] == DeletedItemRetention.DatabaseDefault)
			{
				propertyBag[IADMailStorageSchema.DeletedItemFlags] = DeletedItemRetention.RetainForCustomPeriod;
			}
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x00068D52 File Offset: 0x00066F52
		internal static object RetainDeletedItemsUntilBackupGetter(IPropertyBag propertyBag)
		{
			return (DeletedItemRetention)propertyBag[IADMailStorageSchema.DeletedItemFlags] == DeletedItemRetention.RetainUntilBackupOrCustomPeriod;
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x00068D6C File Offset: 0x00066F6C
		internal static void RetainDeletedItemsUntilBackupSetter(object value, IPropertyBag propertyBag)
		{
			if ((bool)value)
			{
				propertyBag[IADMailStorageSchema.DeletedItemFlags] = DeletedItemRetention.RetainUntilBackupOrCustomPeriod;
				return;
			}
			if ((DeletedItemRetention)propertyBag[IADMailStorageSchema.DeletedItemFlags] == DeletedItemRetention.RetainUntilBackupOrCustomPeriod)
			{
				propertyBag[IADMailStorageSchema.DeletedItemFlags] = DeletedItemRetention.RetainForCustomPeriod;
			}
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x00068DAC File Offset: 0x00066FAC
		internal static object DowngradeHighPriorityMessagesEnabledGetter(IPropertyBag propertyBag)
		{
			MultiValuedProperty<byte[]> multiValuedProperty = (MultiValuedProperty<byte[]>)propertyBag[ADUserSchema.SecurityProtocol];
			bool flag = false;
			if (0 < multiValuedProperty.Count)
			{
				if (1 < multiValuedProperty.Count)
				{
					throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty(ADUserSchema.DowngradeHighPriorityMessagesEnabled.Name, DirectoryStrings.TooManyDataInLdapProperty(ADUserSchema.SecurityProtocol.LdapDisplayName, 1)), ADUserSchema.DowngradeHighPriorityMessagesEnabled, multiValuedProperty));
				}
				byte[] array = multiValuedProperty[0];
				if (array.Length >= 4)
				{
					flag = (0 != (array[3] & 128));
				}
			}
			return flag;
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x00068E38 File Offset: 0x00067038
		internal static void DowngradeHighPriorityMessagesEnabledSetter(object value, IPropertyBag propertyBag)
		{
			bool flag = (bool)value;
			bool flag2 = (bool)ADUser.DowngradeHighPriorityMessagesEnabledGetter(propertyBag);
			if (flag != flag2)
			{
				MultiValuedProperty<byte[]> multiValuedProperty = (MultiValuedProperty<byte[]>)propertyBag[ADUserSchema.SecurityProtocol];
				int num = 4;
				if (0 < multiValuedProperty.Count)
				{
					num = Math.Max(num, multiValuedProperty[0].Length);
				}
				byte[] array = new byte[num];
				Array.Clear(array, 0, num);
				if (0 < multiValuedProperty.Count)
				{
					multiValuedProperty[0].CopyTo(array, 0);
				}
				if (flag)
				{
					byte[] array2 = array;
					int num2 = 3;
					array2[num2] |= 128;
				}
				else
				{
					byte[] array3 = array;
					int num3 = 3;
					array3[num3] &= 127;
				}
				if (0 < multiValuedProperty.Count)
				{
					multiValuedProperty.Clear();
				}
				multiValuedProperty.Add(array);
			}
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x00068F04 File Offset: 0x00067104
		internal static object ElcExpirationSuspensionEnabledGetter(IPropertyBag propertyBag)
		{
			ElcMailboxFlags elcMailboxFlags = (ElcMailboxFlags)propertyBag[ADUserSchema.ElcMailboxFlags];
			return (elcMailboxFlags & ElcMailboxFlags.ExpirationSuspended) == ElcMailboxFlags.ExpirationSuspended;
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x00068F30 File Offset: 0x00067130
		internal static void ElcExpirationSuspensionEnabledSetter(object value, IPropertyBag propertyBag)
		{
			bool flag = (bool)(value ?? false);
			ElcMailboxFlags elcMailboxFlags = (ElcMailboxFlags)propertyBag[ADUserSchema.ElcMailboxFlags];
			if (flag)
			{
				propertyBag[ADUserSchema.ElcMailboxFlags] = (elcMailboxFlags | ElcMailboxFlags.ExpirationSuspended);
				return;
			}
			propertyBag[ADUserSchema.ElcMailboxFlags] = (elcMailboxFlags & ~ElcMailboxFlags.ExpirationSuspended);
			propertyBag[ADUserSchema.ElcExpirationSuspensionStartDate] = null;
			propertyBag[ADUserSchema.ElcExpirationSuspensionEndDate] = null;
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x00068FA4 File Offset: 0x000671A4
		internal static object LitigationHoldEnabledGetter(IPropertyBag propertyBag)
		{
			ElcMailboxFlags elcMailboxFlags = (ElcMailboxFlags)propertyBag[ADUserSchema.ElcMailboxFlags];
			return (elcMailboxFlags & ElcMailboxFlags.LitigationHold) == ElcMailboxFlags.LitigationHold;
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x00068FD0 File Offset: 0x000671D0
		internal static void LitigationHoldEnabledSetter(object value, IPropertyBag propertyBag)
		{
			bool flag = (bool)(value ?? false);
			ElcMailboxFlags elcMailboxFlags = (ElcMailboxFlags)propertyBag[ADUserSchema.ElcMailboxFlags];
			if (flag)
			{
				propertyBag[ADUserSchema.ElcMailboxFlags] = (elcMailboxFlags | ElcMailboxFlags.LitigationHold);
				propertyBag[ADUserSchema.LitigationHoldDate] = DateTime.UtcNow;
				return;
			}
			propertyBag[ADUserSchema.ElcMailboxFlags] = (elcMailboxFlags & ~ElcMailboxFlags.LitigationHold);
			propertyBag[ADUserSchema.RetentionComment] = null;
			propertyBag[ADUserSchema.RetentionUrl] = null;
			propertyBag[ADUserSchema.LitigationHoldDate] = null;
			propertyBag[ADUserSchema.LitigationHoldOwner] = null;
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x00069070 File Offset: 0x00067270
		internal static object SingleItemRecoveryEnabledGetter(IPropertyBag propertyBag)
		{
			ElcMailboxFlags elcMailboxFlags = (ElcMailboxFlags)propertyBag[ADUserSchema.ElcMailboxFlags];
			return (elcMailboxFlags & ElcMailboxFlags.SingleItemRecovery) == ElcMailboxFlags.SingleItemRecovery;
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x0006909C File Offset: 0x0006729C
		internal static void SingleItemRecoveryEnabledSetter(object value, IPropertyBag propertyBag)
		{
			bool flag = (bool)(value ?? false);
			ElcMailboxFlags elcMailboxFlags = (ElcMailboxFlags)propertyBag[ADUserSchema.ElcMailboxFlags];
			if (flag)
			{
				propertyBag[ADUserSchema.ElcMailboxFlags] = (elcMailboxFlags | ElcMailboxFlags.SingleItemRecovery);
				return;
			}
			propertyBag[ADUserSchema.ElcMailboxFlags] = (elcMailboxFlags & ~ElcMailboxFlags.SingleItemRecovery);
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x000690F8 File Offset: 0x000672F8
		internal static object CalendarVersionStoreDisabledGetter(IPropertyBag propertyBag)
		{
			ElcMailboxFlags elcMailboxFlags = (ElcMailboxFlags)propertyBag[ADUserSchema.ElcMailboxFlags];
			return (elcMailboxFlags & ElcMailboxFlags.DisableCalendarLogging) == ElcMailboxFlags.DisableCalendarLogging;
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x00069124 File Offset: 0x00067324
		internal static void CalendarVersionStoreDisabledSetter(object value, IPropertyBag propertyBag)
		{
			bool flag = (bool)(value ?? false);
			ElcMailboxFlags elcMailboxFlags = (ElcMailboxFlags)propertyBag[ADUserSchema.ElcMailboxFlags];
			if (flag)
			{
				propertyBag[ADUserSchema.ElcMailboxFlags] = (elcMailboxFlags | ElcMailboxFlags.DisableCalendarLogging);
				return;
			}
			propertyBag[ADUserSchema.ElcMailboxFlags] = (elcMailboxFlags & ~ElcMailboxFlags.DisableCalendarLogging);
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x00069180 File Offset: 0x00067380
		internal static object SiteMailboxMessageDedupEnabledGetter(IPropertyBag propertyBag)
		{
			ElcMailboxFlags elcMailboxFlags = (ElcMailboxFlags)propertyBag[ADUserSchema.ElcMailboxFlags];
			return (elcMailboxFlags & ElcMailboxFlags.EnableSiteMailboxMessageDedup) == ElcMailboxFlags.EnableSiteMailboxMessageDedup;
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x000691B4 File Offset: 0x000673B4
		internal static void SiteMailboxMessageDedupEnabledSetter(object value, IPropertyBag propertyBag)
		{
			bool flag = (bool)(value ?? false);
			ElcMailboxFlags elcMailboxFlags = (ElcMailboxFlags)propertyBag[ADUserSchema.ElcMailboxFlags];
			if (flag)
			{
				propertyBag[ADUserSchema.ElcMailboxFlags] = (elcMailboxFlags | ElcMailboxFlags.EnableSiteMailboxMessageDedup);
				return;
			}
			propertyBag[ADUserSchema.ElcMailboxFlags] = (elcMailboxFlags & ~ElcMailboxFlags.EnableSiteMailboxMessageDedup);
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x00069215 File Offset: 0x00067415
		internal static QueryFilter ActiveSyncEnabledFilterBuilder(SinglePropertyFilter filter)
		{
			return ADObject.BoolFilterBuilder(filter, new NotFilter(new BitMaskAndFilter(ADUserSchema.MobileFeaturesEnabled, 4UL)));
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x0006922E File Offset: 0x0006742E
		internal static QueryFilter HasActiveSyncDevicePartnershipFilterBuilder(SinglePropertyFilter filter)
		{
			return ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(ADUserSchema.MobileMailboxFlags, 1UL));
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x00069242 File Offset: 0x00067442
		internal static QueryFilter OWAforDevicesEnabledFilterBuilder(SinglePropertyFilter filter)
		{
			return ADObject.BoolFilterBuilder(filter, new NotFilter(new BitMaskAndFilter(ADUserSchema.MobileFeaturesEnabled, 8UL)));
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x0006925B File Offset: 0x0006745B
		internal static object ManagedFolderMailboxPolicyGetter(IPropertyBag propertyBag)
		{
			if (((ElcMailboxFlags)(propertyBag[ADUserSchema.ElcMailboxFlags] ?? ElcMailboxFlags.None) & ElcMailboxFlags.ElcV2) == ElcMailboxFlags.None)
			{
				return (ADObjectId)propertyBag[ADUserSchema.ElcPolicyTemplate];
			}
			return null;
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x00069290 File Offset: 0x00067490
		internal static void ManagedFolderMailboxPolicySetter(object value, IPropertyBag propertyBag)
		{
			if (value != null)
			{
				propertyBag[IADMailStorageSchema.ElcMailboxFlags] = ((ElcMailboxFlags)propertyBag[ADUserSchema.ElcMailboxFlags] & ~ElcMailboxFlags.ElcV2);
				propertyBag[ADUserSchema.ElcPolicyTemplate] = value;
			}
			else if (((ElcMailboxFlags)(propertyBag[ADUserSchema.ElcMailboxFlags] ?? ElcMailboxFlags.None) & ElcMailboxFlags.ElcV2) == ElcMailboxFlags.None)
			{
				propertyBag[ADUserSchema.ElcPolicyTemplate] = value;
			}
			propertyBag[IADMailStorageSchema.ElcMailboxFlags] = ((ElcMailboxFlags)propertyBag[ADUserSchema.ElcMailboxFlags] & ~ElcMailboxFlags.ShouldUseDefaultRetentionPolicy);
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x00069321 File Offset: 0x00067521
		internal static object RetentionPolicyGetter(IPropertyBag propertyBag)
		{
			if (((ElcMailboxFlags)(propertyBag[ADUserSchema.ElcMailboxFlags] ?? ElcMailboxFlags.None) & ElcMailboxFlags.ElcV2) == ElcMailboxFlags.ElcV2)
			{
				return (ADObjectId)propertyBag[ADUserSchema.ElcPolicyTemplate];
			}
			return null;
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x00069354 File Offset: 0x00067554
		internal static void RetentionPolicySetter(object value, IPropertyBag propertyBag)
		{
			if (value != null)
			{
				propertyBag[IADMailStorageSchema.ElcMailboxFlags] = ((ElcMailboxFlags)propertyBag[ADUserSchema.ElcMailboxFlags] | ElcMailboxFlags.ElcV2);
				propertyBag[ADUserSchema.ElcPolicyTemplate] = value;
			}
			else if (((ElcMailboxFlags)(propertyBag[ADUserSchema.ElcMailboxFlags] ?? ElcMailboxFlags.None) & ElcMailboxFlags.ElcV2) == ElcMailboxFlags.ElcV2)
			{
				propertyBag[ADUserSchema.ElcPolicyTemplate] = value;
			}
			propertyBag[IADMailStorageSchema.ElcMailboxFlags] = ((ElcMailboxFlags)propertyBag[ADUserSchema.ElcMailboxFlags] & ~ElcMailboxFlags.ShouldUseDefaultRetentionPolicy);
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x000693E8 File Offset: 0x000675E8
		internal static object ArchiveStateGetter(IPropertyBag propertyBag)
		{
			if (propertyBag[ADUserSchema.ArchiveGuid] == null || !((Guid)propertyBag[ADUserSchema.ArchiveGuid] != Guid.Empty))
			{
				return ArchiveState.None;
			}
			if (propertyBag[ADUserSchema.ArchiveDatabase] != null || (propertyBag[ADMailboxRecipientSchema.Database] != null && propertyBag[ADUserSchema.ArchiveDomain] == null))
			{
				return ArchiveState.Local;
			}
			RecipientTypeDetails recipientTypeDetails = (RecipientTypeDetails)(propertyBag[ADRecipientSchema.RecipientTypeDetailsValue] ?? RecipientTypeDetails.None);
			if (propertyBag[ADUserSchema.ArchiveDomain] == null && !RemoteMailbox.IsRemoteMailbox(recipientTypeDetails))
			{
				return ArchiveState.OnPremise;
			}
			ArchiveStatusFlags archiveStatusFlags = (ArchiveStatusFlags)(propertyBag[ADUserSchema.ArchiveStatus] ?? ArchiveStatusFlags.None);
			if ((archiveStatusFlags & ArchiveStatusFlags.Active) == ArchiveStatusFlags.Active)
			{
				return ArchiveState.HostedProvisioned;
			}
			return ArchiveState.HostedPending;
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x000694C0 File Offset: 0x000676C0
		internal static bool ParseSecIDValue(string value, out NetID puid, out string netIdSuffix)
		{
			puid = null;
			netIdSuffix = string.Empty;
			return !string.IsNullOrEmpty(value) && (ADUser.TryParseNetIDAndSuffix(value, out puid, out netIdSuffix) || (value.StartsWith("KERBEROS:", StringComparison.OrdinalIgnoreCase) && value.EndsWith("@live.com", StringComparison.OrdinalIgnoreCase) && NetID.TryParse(value.Substring("KERBEROS:".Length, value.Length - "@live.com".Length - "KERBEROS:".Length), out puid)) || (value.StartsWith("MS-WLID:", StringComparison.OrdinalIgnoreCase) && NetID.TryParse(value.Substring("MS-WLID:".Length), out puid)));
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x00069568 File Offset: 0x00067768
		private static bool TryParseNetIDAndSuffix(string value, out NetID puid, out string netIdSuffix)
		{
			puid = null;
			netIdSuffix = string.Empty;
			int num = value.IndexOf('-');
			if (value.StartsWith("ExWLID:", StringComparison.OrdinalIgnoreCase) && num != -1 && NetID.TryParse(value.Substring("ExWLID:".Length, num - "ExWLID:".Length), out puid))
			{
				netIdSuffix = value.Substring(num + 1);
				return true;
			}
			return false;
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x000695CC File Offset: 0x000677CC
		internal static bool TryParseSecIDValueFromPropertyBag(IPropertyBag propertyBag, out NetID puid, out string netIdSuffix)
		{
			puid = null;
			netIdSuffix = string.Empty;
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[IADSecurityPrincipalSchema.AltSecurityIdentities];
			if (multiValuedProperty != null && multiValuedProperty.Count != 0)
			{
				foreach (string value in multiValuedProperty)
				{
					if (ADUser.ParseSecIDValue(value, out puid, out netIdSuffix))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x0006964C File Offset: 0x0006784C
		internal static void SetSecIDValue(NetID puid, string netIdSuffix, IPropertyBag propertyBag)
		{
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[IADSecurityPrincipalSchema.AltSecurityIdentities];
			if (multiValuedProperty != null && multiValuedProperty.Count != 0)
			{
				NetID netID = null;
				string empty = string.Empty;
				int i = 0;
				while (i < multiValuedProperty.Count)
				{
					if (ADUser.ParseSecIDValue(multiValuedProperty[i], out netID, out empty))
					{
						multiValuedProperty.RemoveAt(i);
					}
					else
					{
						i++;
					}
				}
			}
			if (puid != null)
			{
				if (!string.IsNullOrEmpty(netIdSuffix))
				{
					multiValuedProperty.Add(string.Format("ExWLID:{0}-{1}", puid, netIdSuffix));
					return;
				}
				multiValuedProperty.Add("KERBEROS:" + puid.ToString() + "@live.com");
				multiValuedProperty.Add("MS-WLID:" + puid.ToString());
			}
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x00069700 File Offset: 0x00067900
		internal static object NetIdGetter(IPropertyBag propertyBag)
		{
			NetID result = null;
			string empty = string.Empty;
			if (ADUser.TryParseSecIDValueFromPropertyBag(propertyBag, out result, out empty))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x00069724 File Offset: 0x00067924
		internal static void NetIdSetter(object value, IPropertyBag propertyBag)
		{
			NetID puid = (NetID)value;
			string netIdSuffix = (string)propertyBag[IADSecurityPrincipalSchema.NetIDSuffix];
			ADUser.SetSecIDValue(puid, netIdSuffix, propertyBag);
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x00069754 File Offset: 0x00067954
		internal static QueryFilter InPlaceHoldsFilterBuilder(SinglePropertyFilter filter)
		{
			if (!(filter is ComparisonFilter))
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForProperty(filter.Property.Name, filter.GetType(), typeof(ComparisonFilter)));
			}
			ComparisonFilter comparisonFilter = (ComparisonFilter)filter;
			QueryFilter queryFilter = new TextFilter(ADRecipientSchema.InPlaceHoldsRaw, comparisonFilter.PropertyValue.ToString(), MatchOptions.FullString, MatchFlags.IgnoreCase);
			switch (comparisonFilter.ComparisonOperator)
			{
			case ComparisonOperator.Equal:
				return queryFilter;
			case ComparisonOperator.NotEqual:
				return new NotFilter(queryFilter);
			default:
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedOperatorForProperty(comparisonFilter.Property.Name, comparisonFilter.ComparisonOperator.ToString()));
			}
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x000697F4 File Offset: 0x000679F4
		internal static QueryFilter NetIdFilterBuilder(SinglePropertyFilter filter)
		{
			if (!(filter is ComparisonFilter))
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForProperty(filter.Property.Name, filter.GetType(), typeof(ComparisonFilter)));
			}
			ComparisonFilter comparisonFilter = (ComparisonFilter)filter;
			QueryFilter queryFilter = new OrFilter(new QueryFilter[]
			{
				new TextFilter(IADSecurityPrincipalSchema.AltSecurityIdentities, "MS-WLID:" + ((NetID)comparisonFilter.PropertyValue).ToString(), MatchOptions.FullString, MatchFlags.IgnoreCase),
				new TextFilter(IADSecurityPrincipalSchema.AltSecurityIdentities, "KERBEROS:" + ((NetID)comparisonFilter.PropertyValue).ToString() + "@live.com", MatchOptions.FullString, MatchFlags.IgnoreCase),
				new TextFilter(IADSecurityPrincipalSchema.AltSecurityIdentities, "ExWLID:" + ((NetID)comparisonFilter.PropertyValue).ToString(), MatchOptions.Prefix, MatchFlags.IgnoreCase)
			});
			switch (comparisonFilter.ComparisonOperator)
			{
			case ComparisonOperator.Equal:
				return queryFilter;
			case ComparisonOperator.NotEqual:
				return new NotFilter(queryFilter);
			default:
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedOperatorForProperty(comparisonFilter.Property.Name, comparisonFilter.ComparisonOperator.ToString()));
			}
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x0006990C File Offset: 0x00067B0C
		internal static bool ParseConsumerSecIDValue(string value, out NetID puid)
		{
			return ADUser.ParseSecIDValue(value, "CS-WLID:", out puid);
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x0006991A File Offset: 0x00067B1A
		internal static bool ParseSecIDValue(string value, string prefix, out NetID puid)
		{
			puid = null;
			return !string.IsNullOrEmpty(value) && value.StartsWith(prefix, StringComparison.OrdinalIgnoreCase) && NetID.TryParse(value.Substring(prefix.Length), out puid);
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x00069948 File Offset: 0x00067B48
		internal static bool TryParseSecIDValueFromPropertyBag(IPropertyBag propertyBag, string prefix, out NetID puid)
		{
			puid = null;
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[IADSecurityPrincipalSchema.AltSecurityIdentities];
			if (multiValuedProperty != null && multiValuedProperty.Count != 0)
			{
				foreach (string value in multiValuedProperty)
				{
					if (ADUser.ParseSecIDValue(value, prefix, out puid))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x000699C0 File Offset: 0x00067BC0
		internal void UpdateSoftDeletedStatusForHold(bool enableHold)
		{
			int recipientSoftDeletedStatus;
			if (enableHold)
			{
				recipientSoftDeletedStatus = ((base.RecipientSoftDeletedStatus | 8) & -5);
			}
			else
			{
				recipientSoftDeletedStatus = ((base.RecipientSoftDeletedStatus & -9) | 4);
			}
			base.RecipientSoftDeletedStatus = recipientSoftDeletedStatus;
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x000699F4 File Offset: 0x00067BF4
		internal void SetLitigationHoldEnabledWellKnownInPlaceHoldGuid(bool litigationHoldEnabled)
		{
			MultiValuedProperty<string> multiValuedProperty = this.propertyBag[ADRecipientSchema.InPlaceHoldsRaw] as MultiValuedProperty<string>;
			if (litigationHoldEnabled)
			{
				if (multiValuedProperty == null)
				{
					multiValuedProperty = new MultiValuedProperty<string>();
				}
				if (!multiValuedProperty.Contains("98E9BABD09A04bcf8455A58C2AA74182"))
				{
					multiValuedProperty.Add("98E9BABD09A04bcf8455A58C2AA74182");
				}
				this.propertyBag[ADRecipientSchema.InPlaceHoldsRaw] = multiValuedProperty;
				return;
			}
			MultiValuedProperty<string> multiValuedProperty2 = new MultiValuedProperty<string>();
			if (multiValuedProperty != null && multiValuedProperty.Count > 0)
			{
				foreach (string text in multiValuedProperty)
				{
					if (!text.StartsWith("98E9BABD09A04bcf8455A58C2AA74182", StringComparison.OrdinalIgnoreCase))
					{
						multiValuedProperty2.Add(text);
					}
				}
			}
			this.propertyBag[ADRecipientSchema.InPlaceHoldsRaw] = multiValuedProperty2;
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x00069AC0 File Offset: 0x00067CC0
		internal static void SetBasicSecIDValue(NetID puid, string prefix, IPropertyBag propertyBag)
		{
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[IADSecurityPrincipalSchema.AltSecurityIdentities];
			if (multiValuedProperty != null && multiValuedProperty.Count != 0)
			{
				NetID netID = null;
				int i = 0;
				while (i < multiValuedProperty.Count)
				{
					if (ADUser.ParseSecIDValue(multiValuedProperty[i], prefix, out netID))
					{
						multiValuedProperty.RemoveAt(i);
					}
					else
					{
						i++;
					}
				}
			}
			if (puid != null)
			{
				multiValuedProperty.Add(prefix + puid.ToString());
			}
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x00069B34 File Offset: 0x00067D34
		internal static object ConsumerNetIdGetter(IPropertyBag propertyBag)
		{
			NetID result = null;
			if (ADUser.TryParseSecIDValueFromPropertyBag(propertyBag, "CS-WLID:", out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x00069B58 File Offset: 0x00067D58
		internal static void ConsumerNetIdSetter(object value, IPropertyBag propertyBag)
		{
			NetID puid = (NetID)value;
			ADUser.SetSecIDValue(puid, "CS-WLID:", propertyBag);
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x00069B78 File Offset: 0x00067D78
		internal static QueryFilter ConsumerNetIdFilterBuilder(SinglePropertyFilter filter)
		{
			return ADUser.BasicNetIdFilterBuilder("CS-WLID:", filter);
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x00069B88 File Offset: 0x00067D88
		internal static QueryFilter BasicNetIdFilterBuilder(string prefix, SinglePropertyFilter filter)
		{
			if (filter is ComparisonFilter)
			{
				ComparisonFilter comparisonFilter = (ComparisonFilter)filter;
				QueryFilter queryFilter = new TextFilter(IADSecurityPrincipalSchema.AltSecurityIdentities, prefix + ((NetID)comparisonFilter.PropertyValue).ToString(), MatchOptions.FullString, MatchFlags.IgnoreCase);
				switch (comparisonFilter.ComparisonOperator)
				{
				case ComparisonOperator.Equal:
					return queryFilter;
				case ComparisonOperator.NotEqual:
					return new NotFilter(queryFilter);
				default:
					throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedOperatorForProperty(comparisonFilter.Property.Name, comparisonFilter.ComparisonOperator.ToString()));
				}
			}
			else
			{
				if (filter is ExistsFilter)
				{
					return new TextFilter(IADSecurityPrincipalSchema.AltSecurityIdentities, prefix, MatchOptions.Prefix, MatchFlags.IgnoreCase);
				}
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForProperty(filter.Property.Name, filter.GetType(), typeof(ComparisonFilter)));
			}
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x00069C48 File Offset: 0x00067E48
		internal static object OriginalNetIdGetter(IPropertyBag propertyBag)
		{
			NetID result = null;
			if (ADUser.TryParseSecIDValueFromPropertyBag(propertyBag, "EXORIGNETID:", out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x00069C6C File Offset: 0x00067E6C
		internal static void OriginalNetIdSetter(object value, IPropertyBag propertyBag)
		{
			NetID puid = (NetID)value;
			ADUser.SetBasicSecIDValue(puid, "EXORIGNETID:", propertyBag);
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x00069C8C File Offset: 0x00067E8C
		internal static QueryFilter OriginalNetIdFilterBuilder(SinglePropertyFilter filter)
		{
			return ADUser.BasicNetIdFilterBuilder("EXORIGNETID:", filter);
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x00069C9C File Offset: 0x00067E9C
		internal static object NetIdSuffixGetter(IPropertyBag propertyBag)
		{
			NetID netID = null;
			string empty = string.Empty;
			if (ADUser.TryParseSecIDValueFromPropertyBag(propertyBag, out netID, out empty))
			{
				return empty;
			}
			return null;
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x00069CC0 File Offset: 0x00067EC0
		internal static void NetIdSuffixSetter(object value, IPropertyBag propertyBag)
		{
			string netIdSuffix = (string)value;
			NetID puid = (NetID)propertyBag[IADSecurityPrincipalSchema.NetID];
			ADUser.SetSecIDValue(puid, netIdSuffix, propertyBag);
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x00069CF0 File Offset: 0x00067EF0
		internal static object CertificateSubjectGetter(IPropertyBag propertyBag)
		{
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[ADUserSchema.AltSecurityIdentities];
			MultiValuedProperty<X509Identifier> multiValuedProperty2 = new MultiValuedProperty<X509Identifier>();
			foreach (string text in multiValuedProperty)
			{
				if (!string.IsNullOrEmpty(text) && text.StartsWith("X509:", StringComparison.OrdinalIgnoreCase))
				{
					try
					{
						multiValuedProperty2.Add(X509Identifier.Parse(text));
					}
					catch (FormatException ex)
					{
						throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty(ADUserSchema.CertificateSubject.Name, ex.Message), ADUserSchema.CertificateSubject, propertyBag[ADUserSchema.AltSecurityIdentities]), ex);
					}
					catch (InvalidOperationException ex2)
					{
						throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty(ADUserSchema.CertificateSubject.Name, ex2.Message), ADUserSchema.CertificateSubject, propertyBag[ADUserSchema.AltSecurityIdentities]), ex2);
					}
				}
			}
			return multiValuedProperty2;
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x00069DFC File Offset: 0x00067FFC
		internal static void CertificateSubjectSetter(object value, IPropertyBag propertyBag)
		{
			MultiValuedProperty<X509Identifier> multiValuedProperty = (MultiValuedProperty<X509Identifier>)value;
			MultiValuedProperty<string> multiValuedProperty2 = (MultiValuedProperty<string>)propertyBag[ADUserSchema.AltSecurityIdentities];
			for (int i = multiValuedProperty2.Count - 1; i >= 0; i--)
			{
				if (!string.IsNullOrEmpty(multiValuedProperty2[i]) && multiValuedProperty2[i].StartsWith("X509:", StringComparison.OrdinalIgnoreCase))
				{
					multiValuedProperty2.RemoveAt(i);
				}
			}
			foreach (X509Identifier x509Identifier in multiValuedProperty)
			{
				multiValuedProperty2.Add(x509Identifier.ToString());
			}
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x00069EA4 File Offset: 0x000680A4
		internal static QueryFilter CertificateSubjectFilterBuilder(SinglePropertyFilter filter)
		{
			X509Identifier x509Identifier = (X509Identifier)ADObject.PropertyValueFromEqualityFilter(filter);
			if (x509Identifier == null)
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForProperty(filter.Property.Name, filter.GetType(), typeof(ComparisonFilter)));
			}
			if (!(filter is ComparisonFilter))
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForProperty(filter.Property.Name, filter.GetType(), typeof(ComparisonFilter)));
			}
			ComparisonFilter comparisonFilter = (ComparisonFilter)filter;
			QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ADUserSchema.AltSecurityIdentities, x509Identifier.ToString());
			switch (comparisonFilter.ComparisonOperator)
			{
			case ComparisonOperator.Equal:
				return queryFilter;
			case ComparisonOperator.NotEqual:
				return new NotFilter(queryFilter);
			default:
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedOperatorForProperty(comparisonFilter.Property.Name, comparisonFilter.ComparisonOperator.ToString()));
			}
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x00069F78 File Offset: 0x00068178
		internal static QueryFilter GetCertificateMatchFilter(X509Identifier identifier)
		{
			QueryFilter queryFilter = null;
			if (identifier != null)
			{
				queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ADUserSchema.CertificateSubject, identifier);
				if (!identifier.IsGenericIdentifier)
				{
					queryFilter = new OrFilter(new QueryFilter[]
					{
						queryFilter,
						new ComparisonFilter(ComparisonOperator.Equal, ADUserSchema.CertificateSubject, new X509Identifier(identifier.Issuer))
					});
				}
			}
			return queryFilter;
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x00069FD4 File Offset: 0x000681D4
		internal static object SharingPartnerIdentitiesGetter(IPropertyBag propertyBag)
		{
			MultiValuedProperty<string> mvp = (MultiValuedProperty<string>)propertyBag[IADMailStorageSchema.SharingPartnerIdentitiesRaw];
			return new SharingPartnerIdentityCollection(mvp);
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x00069FF8 File Offset: 0x000681F8
		internal static void SharingPartnerIdentitiesSetter(object value, IPropertyBag propertyBag)
		{
			SharingPartnerIdentityCollection sharingPartnerIdentityCollection = (SharingPartnerIdentityCollection)value;
			propertyBag[IADMailStorageSchema.SharingPartnerIdentitiesRaw] = sharingPartnerIdentityCollection.InnerCollection;
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x0006A020 File Offset: 0x00068220
		internal static object SharingAnonymousIdentitiesGetter(IPropertyBag propertyBag)
		{
			MultiValuedProperty<string> sharingAnonymousIdentities = (MultiValuedProperty<string>)propertyBag[IADMailStorageSchema.SharingAnonymousIdentitiesRaw];
			return new SharingAnonymousIdentityCollection(sharingAnonymousIdentities);
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x0006A044 File Offset: 0x00068244
		internal static void SharingAnonymousIdentitiesSetter(object value, IPropertyBag propertyBag)
		{
			SharingAnonymousIdentityCollection sharingAnonymousIdentityCollection = (SharingAnonymousIdentityCollection)value;
			propertyBag[IADMailStorageSchema.SharingAnonymousIdentitiesRaw] = sharingAnonymousIdentityCollection.GetRawSharingAnonymousIdentities();
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x0006A06C File Offset: 0x0006826C
		internal static QueryFilter SKUAssignedFilterBuilder(SinglePropertyFilter filter)
		{
			QueryFilter queryFilter = new BitMaskAndFilter(ADRecipientSchema.ProvisioningFlags, 64UL);
			QueryFilter queryFilter2 = new BitMaskAndFilter(ADRecipientSchema.ProvisioningFlags, 128UL);
			if (filter is ComparisonFilter)
			{
				ComparisonFilter comparisonFilter = (ComparisonFilter)filter;
				if (comparisonFilter.ComparisonOperator != ComparisonOperator.Equal && ComparisonOperator.NotEqual != comparisonFilter.ComparisonOperator)
				{
					throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedOperatorForProperty(comparisonFilter.Property.Name, comparisonFilter.ComparisonOperator.ToString()));
				}
				QueryFilter queryFilter3;
				if (comparisonFilter.PropertyValue == null)
				{
					queryFilter3 = new AndFilter(new QueryFilter[]
					{
						new NotFilter(queryFilter),
						new NotFilter(queryFilter2)
					});
				}
				else if ((bool)comparisonFilter.PropertyValue)
				{
					queryFilter3 = new AndFilter(new QueryFilter[]
					{
						queryFilter,
						new NotFilter(queryFilter2)
					});
				}
				else
				{
					queryFilter3 = queryFilter2;
				}
				if (ComparisonOperator.NotEqual == comparisonFilter.ComparisonOperator)
				{
					queryFilter3 = new NotFilter(queryFilter3);
				}
				return queryFilter3;
			}
			else
			{
				if (filter is ExistsFilter)
				{
					return new OrFilter(new QueryFilter[]
					{
						queryFilter,
						queryFilter2
					});
				}
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForProperty(filter.Property.Name, filter.GetType(), typeof(ComparisonFilter)));
			}
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x0006A198 File Offset: 0x00068398
		internal static object SKUAssignedGetter(IPropertyBag bag)
		{
			bool flag = (bool)ADObject.FlagGetterDelegate(64, ADRecipientSchema.ProvisioningFlags)(bag);
			bool flag2 = (bool)ADObject.FlagGetterDelegate(128, ADRecipientSchema.ProvisioningFlags)(bag);
			if (flag2)
			{
				return false;
			}
			if (flag)
			{
				return true;
			}
			return null;
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x0006A1F0 File Offset: 0x000683F0
		internal static void SKUAssignedSetter(object value, IPropertyBag bag)
		{
			bool? flag = (bool?)value;
			bool flag2 = flag != null && flag.Value;
			bool flag3 = flag != null && !flag.Value;
			ADObject.FlagSetterDelegate(64, ADRecipientSchema.ProvisioningFlags)(flag2, bag);
			ADObject.FlagSetterDelegate(128, ADRecipientSchema.ProvisioningFlags)(flag3, bag);
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x0006A262 File Offset: 0x00068462
		internal static object ExchangeSecurityDescriptorGetter(IPropertyBag bag)
		{
			return bag[IADMailStorageSchema.ExchangeSecurityDescriptorRaw];
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x0006A270 File Offset: 0x00068470
		internal static void ExchangeSecurityDescriptorSetter(object value, IPropertyBag bag)
		{
			RawSecurityDescriptor rawSecurityDescriptor = value as RawSecurityDescriptor;
			if (rawSecurityDescriptor != null)
			{
				value = ExchangeSecurityDescriptorHelper.RemoveInheritedACEs(rawSecurityDescriptor);
			}
			bag[IADMailStorageSchema.ExchangeSecurityDescriptorRaw] = value;
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x0006A29C File Offset: 0x0006849C
		internal bool ShouldInvalidProvisioningCache(out OrganizationId orgId, out Guid[] keys)
		{
			orgId = null;
			keys = null;
			if (base.OrganizationId == null || base.RecipientTypeDetails != RecipientTypeDetails.MailboxPlan || base.ObjectState == ObjectState.Unchanged)
			{
				return false;
			}
			orgId = base.OrganizationId;
			keys = new Guid[3];
			keys[0] = CannedProvisioningCacheKeys.DefaultMailboxPlan;
			keys[1] = CannedProvisioningCacheKeys.CacheKeyMailboxPlanIdParameterId;
			keys[2] = CannedProvisioningCacheKeys.CacheKeyMailboxPlanId;
			return true;
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x0006A31D File Offset: 0x0006851D
		bool IProvisioningCacheInvalidation.ShouldInvalidProvisioningCache(out OrganizationId orgId, out Guid[] keys)
		{
			return this.ShouldInvalidProvisioningCache(out orgId, out keys);
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001852 RID: 6226 RVA: 0x0006A327 File Offset: 0x00068527
		// (set) Token: 0x06001853 RID: 6227 RVA: 0x0006A339 File Offset: 0x00068539
		public bool HasActiveSyncDevicePartnership
		{
			get
			{
				return (bool)this[ADUserSchema.HasActiveSyncDevicePartnership];
			}
			internal set
			{
				this[ADUserSchema.HasActiveSyncDevicePartnership] = value;
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001854 RID: 6228 RVA: 0x0006A34C File Offset: 0x0006854C
		public bool IsFromDatacenter
		{
			get
			{
				return this.NetID != null;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001855 RID: 6229 RVA: 0x0006A35A File Offset: 0x0006855A
		public ArchiveState ArchiveState
		{
			get
			{
				return (ArchiveState)this[ADUserSchema.ArchiveState];
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001856 RID: 6230 RVA: 0x0006A36C File Offset: 0x0006856C
		public bool IsAllowedToModifyOwners
		{
			get
			{
				return TeamMailbox.IsLocalTeamMailbox(this) || TeamMailbox.IsRemoteTeamMailbox(this) || GroupMailbox.IsLocalGroupMailbox(this);
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001857 RID: 6231 RVA: 0x0006A386 File Offset: 0x00068586
		public bool HasLocalArchive
		{
			get
			{
				return this.ArchiveState == ArchiveState.Local;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001858 RID: 6232 RVA: 0x0006A394 File Offset: 0x00068594
		public bool HasSeparatedArchive
		{
			get
			{
				return !(this.ArchiveGuid == Guid.Empty) && ((this.ArchiveDatabase != null && !this.ArchiveDatabase.Equals(base.Database)) || (base.Database != null && !this.HasLocalArchive));
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001859 RID: 6233 RVA: 0x0006A3E5 File Offset: 0x000685E5
		// (set) Token: 0x0600185A RID: 6234 RVA: 0x0006A3F7 File Offset: 0x000685F7
		public int? MaxSafeSenders
		{
			get
			{
				return (int?)this[ADUserSchema.MaxSafeSenders];
			}
			internal set
			{
				this[ADUserSchema.MaxSafeSenders] = value;
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x0600185B RID: 6235 RVA: 0x0006A40A File Offset: 0x0006860A
		// (set) Token: 0x0600185C RID: 6236 RVA: 0x0006A41C File Offset: 0x0006861C
		public int? MaxBlockedSenders
		{
			get
			{
				return (int?)this[ADUserSchema.MaxBlockedSenders];
			}
			internal set
			{
				this[ADUserSchema.MaxBlockedSenders] = value;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x0600185D RID: 6237 RVA: 0x0006A42F File Offset: 0x0006862F
		// (set) Token: 0x0600185E RID: 6238 RVA: 0x0006A441 File Offset: 0x00068641
		public bool IsDefault
		{
			get
			{
				return (bool)this[ADRecipientSchema.IsDefault];
			}
			internal set
			{
				this[ADRecipientSchema.IsDefault] = value;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x0600185F RID: 6239 RVA: 0x0006A454 File Offset: 0x00068654
		// (set) Token: 0x06001860 RID: 6240 RVA: 0x0006A466 File Offset: 0x00068666
		[ProvisionalClone(CloneSet.CloneLimitedSet)]
		public bool RemotePowerShellEnabled
		{
			get
			{
				return (bool)this[ADRecipientSchema.RemotePowerShellEnabled];
			}
			set
			{
				this[ADRecipientSchema.RemotePowerShellEnabled] = value;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001861 RID: 6241 RVA: 0x0006A479 File Offset: 0x00068679
		// (set) Token: 0x06001862 RID: 6242 RVA: 0x0006A486 File Offset: 0x00068686
		internal object DatabaseAndLocation
		{
			get
			{
				return this[IADMailStorageSchema.DatabaseAndLocation];
			}
			set
			{
				this[IADMailStorageSchema.DatabaseAndLocation] = value;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001863 RID: 6243 RVA: 0x0006A494 File Offset: 0x00068694
		// (set) Token: 0x06001864 RID: 6244 RVA: 0x0006A4A6 File Offset: 0x000686A6
		public RemoteRecipientType RemoteRecipientType
		{
			get
			{
				return (RemoteRecipientType)this[ADUserSchema.RemoteRecipientType];
			}
			internal set
			{
				this[ADUserSchema.RemoteRecipientType] = value;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001865 RID: 6245 RVA: 0x0006A4B9 File Offset: 0x000686B9
		// (set) Token: 0x06001866 RID: 6246 RVA: 0x0006A4CB File Offset: 0x000686CB
		public DateTime? LastExchangeChangedTime
		{
			get
			{
				return (DateTime?)this[ADRecipientSchema.LastExchangeChangedTime];
			}
			set
			{
				this[ADRecipientSchema.LastExchangeChangedTime] = value;
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001867 RID: 6247 RVA: 0x0006A4DE File Offset: 0x000686DE
		public MultiValuedProperty<ADObjectId> CatchAllRecipientBL
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADUserSchema.CatchAllRecipientBL];
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001868 RID: 6248 RVA: 0x0006A4F0 File Offset: 0x000686F0
		// (set) Token: 0x06001869 RID: 6249 RVA: 0x0006A502 File Offset: 0x00068702
		public bool AutoSubscribeNewGroupMembers
		{
			get
			{
				return (bool)this[ADRecipientSchema.AutoSubscribeNewGroupMembers];
			}
			set
			{
				this[ADRecipientSchema.AutoSubscribeNewGroupMembers] = value;
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x0600186A RID: 6250 RVA: 0x0006A515 File Offset: 0x00068715
		internal MultiValuedProperty<ADObjectId> GeneratedOfflineAddressBooks
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADRecipientSchema.GeneratedOfflineAddressBooks];
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x0600186B RID: 6251 RVA: 0x0006A527 File Offset: 0x00068727
		// (set) Token: 0x0600186C RID: 6252 RVA: 0x0006A539 File Offset: 0x00068739
		public bool AccountDisabled
		{
			get
			{
				return (bool)this[ADUserSchema.AccountDisabled];
			}
			set
			{
				this[ADUserSchema.AccountDisabled] = value;
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x0600186D RID: 6253 RVA: 0x0006A54C File Offset: 0x0006874C
		// (set) Token: 0x0600186E RID: 6254 RVA: 0x0006A55E File Offset: 0x0006875E
		public DateTime? StsRefreshTokensValidFrom
		{
			get
			{
				return (DateTime?)this[ADUserSchema.StsRefreshTokensValidFrom];
			}
			set
			{
				this[ADUserSchema.StsRefreshTokensValidFrom] = value;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x0600186F RID: 6255 RVA: 0x0006A571 File Offset: 0x00068771
		public ADObjectId ObjectId
		{
			get
			{
				return base.Id;
			}
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x0006A579 File Offset: 0x00068779
		internal FederatedIdentity GetFederatedIdentity()
		{
			return FederatedIdentityHelper.GetFederatedIdentity(this);
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x0006A581 File Offset: 0x00068781
		public SmtpAddress GetFederatedSmtpAddress()
		{
			return this.GetFederatedSmtpAddress(base.PrimarySmtpAddress);
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x0006A590 File Offset: 0x00068790
		internal SmtpAddress GetFederatedSmtpAddress(SmtpAddress preferredSmtpAddress)
		{
			OrganizationId organizationId = base.OrganizationId ?? OrganizationId.ForestWideOrgId;
			OrganizationIdCacheValue organizationIdCacheValue = OrganizationIdCache.Singleton.Get(organizationId);
			if (organizationIdCacheValue.FederatedDomains == null)
			{
				ExTraceGlobals.FederatedIdentityTracer.TraceDebug<OrganizationId>((long)this.GetHashCode(), "No federated domains found for tenant '{0}'", organizationId);
				throw new UserWithoutFederatedProxyAddressException();
			}
			if (organizationIdCacheValue.DefaultFederatedDomain != null)
			{
				foreach (ProxyAddress proxyAddress in base.EmailAddresses)
				{
					if (proxyAddress.Prefix == ProxyAddressPrefix.Smtp)
					{
						SmtpAddress smtpAddress = new SmtpAddress(proxyAddress.AddressString);
						if (StringComparer.OrdinalIgnoreCase.Equals(smtpAddress.Domain, organizationIdCacheValue.DefaultFederatedDomain))
						{
							ExTraceGlobals.FederatedIdentityTracer.TraceDebug<SmtpAddress, ADObjectId>((long)this.GetHashCode(), "Using SMTP address '{0}' for user {1} because it matches default federated domain", smtpAddress, base.Id);
							return smtpAddress;
						}
					}
				}
				ExTraceGlobals.FederatedIdentityTracer.TraceDebug<ADObjectId>((long)this.GetHashCode(), "No proxy address for user '{0}' matches the default federated domain.", base.Id);
			}
			List<string> source = new List<string>(organizationIdCacheValue.FederatedDomains);
			bool isValidAddress = preferredSmtpAddress.IsValidAddress;
			if (isValidAddress && !base.EmailAddresses.Contains(new SmtpProxyAddress(preferredSmtpAddress.ToString(), false)))
			{
				ExTraceGlobals.FederatedIdentityTracer.TraceDebug<SmtpAddress, ADObjectId>((long)this.GetHashCode(), "Preferred SMTP address '{0}' is not one of the proxy addresses for user '{1}' ", preferredSmtpAddress, base.Id);
				throw new ArgumentException("preferredSmtpAddress");
			}
			if (isValidAddress)
			{
				if (source.Contains(preferredSmtpAddress.Domain, StringComparer.OrdinalIgnoreCase))
				{
					ExTraceGlobals.FederatedIdentityTracer.TraceDebug<SmtpAddress, ADObjectId>((long)this.GetHashCode(), "Using preferred SMTP address '{0}' for user {1} because it is a federated domain", preferredSmtpAddress, base.Id);
					return preferredSmtpAddress;
				}
				if (base.PrimarySmtpAddress.IsValidAddress && !StringComparer.OrdinalIgnoreCase.Equals(base.PrimarySmtpAddress.Domain, preferredSmtpAddress.Domain) && source.Contains(base.PrimarySmtpAddress.Domain, StringComparer.OrdinalIgnoreCase))
				{
					ExTraceGlobals.FederatedIdentityTracer.TraceDebug<SmtpAddress, ADObjectId>((long)this.GetHashCode(), "Using primary SMTP address '{0}' for user {1} because it is a federated domain", base.PrimarySmtpAddress, base.Id);
					return base.PrimarySmtpAddress;
				}
			}
			foreach (ProxyAddress proxyAddress2 in base.EmailAddresses)
			{
				if (proxyAddress2.Prefix == ProxyAddressPrefix.Smtp)
				{
					SmtpAddress smtpAddress2 = new SmtpAddress(proxyAddress2.AddressString);
					if (source.Contains(smtpAddress2.Domain, StringComparer.OrdinalIgnoreCase))
					{
						ExTraceGlobals.FederatedIdentityTracer.TraceDebug<SmtpAddress, ADObjectId>((long)this.GetHashCode(), "Using secondary SMTP address '{0}' for user {1} because it is a federated domain", smtpAddress2, base.Id);
						return smtpAddress2;
					}
				}
			}
			ExTraceGlobals.FederatedIdentityTracer.TraceDebug<ADObjectId>((long)this.GetHashCode(), "Could not find a SMTP proxy address corresponding to a federated accepted domain for user {0}.", base.Id);
			throw new UserWithoutFederatedProxyAddressException();
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x0006A874 File Offset: 0x00068A74
		internal List<string> GetFederatedEmailAddresses()
		{
			OrganizationId organizationId = base.OrganizationId ?? OrganizationId.ForestWideOrgId;
			OrganizationIdCacheValue organizationIdCacheValue = OrganizationIdCache.Singleton.Get(organizationId);
			if (organizationIdCacheValue.FederatedDomains == null)
			{
				ExTraceGlobals.FederatedIdentityTracer.TraceDebug<OrganizationId>((long)this.GetHashCode(), "No federated domains found for tenant '{0}'", organizationId);
				return null;
			}
			List<string> list = new List<string>();
			List<string> source = new List<string>(organizationIdCacheValue.FederatedDomains);
			foreach (ProxyAddress proxyAddress in base.EmailAddresses)
			{
				if (proxyAddress.Prefix == ProxyAddressPrefix.Smtp)
				{
					SmtpAddress arg = new SmtpAddress(proxyAddress.AddressString);
					if (source.Contains(arg.Domain, StringComparer.OrdinalIgnoreCase))
					{
						ExTraceGlobals.FederatedIdentityTracer.TraceDebug<SmtpAddress>((long)this.GetHashCode(), "Adding address {0} to the list of email addresses", arg);
						list.Add(proxyAddress.AddressString);
					}
				}
			}
			return list;
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x0006A96C File Offset: 0x00068B6C
		internal FederatedOrganizationId LookupFederatedOrganizationId()
		{
			OrganizationId organizationId = base.OrganizationId ?? OrganizationId.ForestWideOrgId;
			OrganizationIdCacheValue organizationIdCacheValue = OrganizationIdCache.Singleton.Get(organizationId);
			FederatedOrganizationId federatedOrganizationId = organizationIdCacheValue.FederatedOrganizationId;
			if (federatedOrganizationId == null)
			{
				ExTraceGlobals.FederatedIdentityTracer.TraceError<ADObjectId>((long)this.GetHashCode(), "Unable to find federated organization id for user {0}", base.Id);
				throw new InvalidFederatedOrganizationIdException(DirectoryStrings.FederatedOrganizationIdNotFound);
			}
			if (!federatedOrganizationId.Enabled)
			{
				ExTraceGlobals.FederatedIdentityTracer.TraceError<ADObjectId>((long)this.GetHashCode(), "Federated organization id {0} is not enabled", federatedOrganizationId.Id);
				throw new InvalidFederatedOrganizationIdException(DirectoryStrings.FederatedOrganizationIdNotEnabled);
			}
			if (federatedOrganizationId.AccountNamespace == null)
			{
				ExTraceGlobals.FederatedIdentityTracer.TraceError<ADObjectId>((long)this.GetHashCode(), "Federated organization id {0} doesn't have AccountNamespace set", federatedOrganizationId.Id);
				throw new InvalidFederatedOrganizationIdException(DirectoryStrings.FederatedOrganizationIdNoNamespaceAccount);
			}
			if (organizationIdCacheValue.FederatedDomains == null)
			{
				ExTraceGlobals.FederatedIdentityTracer.TraceDebug<OrganizationId>((long)this.GetHashCode(), "Organization is not federated: {0}", organizationId);
				throw new InvalidFederatedOrganizationIdException(DirectoryStrings.FederatedOrganizationIdNoFederatedDomains);
			}
			return federatedOrganizationId;
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x0006AA50 File Offset: 0x00068C50
		internal static ExchangeObjectVersion GetMaximumSupportedExchangeObjectVersion(RecipientTypeDetails recipientTypeDetails, bool forReadObject = false)
		{
			if (recipientTypeDetails <= RecipientTypeDetails.DiscoveryMailbox)
			{
				if (recipientTypeDetails <= RecipientTypeDetails.MailUser)
				{
					if (recipientTypeDetails <= RecipientTypeDetails.SharedMailbox)
					{
						if (recipientTypeDetails != RecipientTypeDetails.UserMailbox && recipientTypeDetails != RecipientTypeDetails.SharedMailbox)
						{
							goto IL_175;
						}
					}
					else if (recipientTypeDetails != RecipientTypeDetails.RoomMailbox && recipientTypeDetails != RecipientTypeDetails.EquipmentMailbox)
					{
						if (recipientTypeDetails != RecipientTypeDetails.MailUser)
						{
							goto IL_175;
						}
						goto IL_165;
					}
					return ExchangeObjectVersion.Exchange2012;
				}
				if (recipientTypeDetails <= RecipientTypeDetails.DisabledUser)
				{
					if (recipientTypeDetails != RecipientTypeDetails.User && recipientTypeDetails != RecipientTypeDetails.DisabledUser)
					{
						goto IL_175;
					}
					goto IL_16D;
				}
				else if (recipientTypeDetails != RecipientTypeDetails.ArbitrationMailbox)
				{
					if (recipientTypeDetails == RecipientTypeDetails.LinkedUser)
					{
						goto IL_16D;
					}
					if (recipientTypeDetails != RecipientTypeDetails.DiscoveryMailbox)
					{
						goto IL_175;
					}
				}
			}
			else
			{
				if (recipientTypeDetails > RecipientTypeDetails.PublicFolderMailbox)
				{
					if (recipientTypeDetails <= RecipientTypeDetails.RemoteTeamMailbox)
					{
						if (recipientTypeDetails != RecipientTypeDetails.TeamMailbox)
						{
							if (recipientTypeDetails != RecipientTypeDetails.RemoteTeamMailbox)
							{
								goto IL_175;
							}
							goto IL_165;
						}
					}
					else if (recipientTypeDetails != RecipientTypeDetails.GroupMailbox)
					{
						if (recipientTypeDetails == RecipientTypeDetails.LinkedRoomMailbox)
						{
							goto IL_165;
						}
						if (recipientTypeDetails != RecipientTypeDetails.AuditLogMailbox)
						{
							goto IL_175;
						}
						goto IL_15D;
					}
					return ADRecipient.TeamMailboxObjectVersion;
				}
				if (recipientTypeDetails <= RecipientTypeDetails.RemoteRoomMailbox)
				{
					if (recipientTypeDetails != (RecipientTypeDetails)((ulong)-2147483648) && recipientTypeDetails != RecipientTypeDetails.RemoteRoomMailbox)
					{
						goto IL_175;
					}
					goto IL_165;
				}
				else
				{
					if (recipientTypeDetails == RecipientTypeDetails.RemoteEquipmentMailbox || recipientTypeDetails == RecipientTypeDetails.RemoteSharedMailbox)
					{
						goto IL_165;
					}
					if (recipientTypeDetails != RecipientTypeDetails.PublicFolderMailbox)
					{
						goto IL_175;
					}
					goto IL_16D;
				}
			}
			IL_15D:
			return ADRecipient.ArbitrationMailboxObjectVersion;
			IL_165:
			return ExchangeObjectVersion.Exchange2012;
			IL_16D:
			return ADRecipient.PublicFolderMailboxObjectVersion;
			IL_175:
			return forReadObject ? ADRecipient.PublicFolderMailboxObjectVersion : ExchangeObjectVersion.Exchange2012;
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001876 RID: 6262 RVA: 0x0006ABE3 File Offset: 0x00068DE3
		// (set) Token: 0x06001877 RID: 6263 RVA: 0x0006ABF5 File Offset: 0x00068DF5
		public string C
		{
			get
			{
				return (string)this[ADUserSchema.C];
			}
			set
			{
				this[ADUserSchema.C] = value;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001878 RID: 6264 RVA: 0x0006AC03 File Offset: 0x00068E03
		// (set) Token: 0x06001879 RID: 6265 RVA: 0x0006AC15 File Offset: 0x00068E15
		public string City
		{
			get
			{
				return (string)this[ADUserSchema.City];
			}
			set
			{
				this[ADUserSchema.City] = value;
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x0600187A RID: 6266 RVA: 0x0006AC23 File Offset: 0x00068E23
		// (set) Token: 0x0600187B RID: 6267 RVA: 0x0006AC35 File Offset: 0x00068E35
		public string Co
		{
			get
			{
				return (string)this[ADUserSchema.Co];
			}
			set
			{
				this[ADUserSchema.Co] = value;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x0600187C RID: 6268 RVA: 0x0006AC43 File Offset: 0x00068E43
		// (set) Token: 0x0600187D RID: 6269 RVA: 0x0006AC55 File Offset: 0x00068E55
		public string Company
		{
			get
			{
				return (string)this[ADUserSchema.Company];
			}
			set
			{
				this[ADUserSchema.Company] = value;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x0600187E RID: 6270 RVA: 0x0006AC63 File Offset: 0x00068E63
		// (set) Token: 0x0600187F RID: 6271 RVA: 0x0006AC75 File Offset: 0x00068E75
		public int CountryCode
		{
			get
			{
				return (int)this[ADUserSchema.CountryCode];
			}
			set
			{
				this[ADUserSchema.CountryCode] = value;
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001880 RID: 6272 RVA: 0x0006AC88 File Offset: 0x00068E88
		public string CountryOrRegionDisplayName
		{
			get
			{
				return (string)this[ADUserSchema.Co];
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001881 RID: 6273 RVA: 0x0006AC9A File Offset: 0x00068E9A
		// (set) Token: 0x06001882 RID: 6274 RVA: 0x0006ACAC File Offset: 0x00068EAC
		public CountryInfo CountryOrRegion
		{
			get
			{
				return (CountryInfo)this[ADUserSchema.CountryOrRegion];
			}
			set
			{
				this[ADUserSchema.CountryOrRegion] = value;
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x0006ACBA File Offset: 0x00068EBA
		// (set) Token: 0x06001884 RID: 6276 RVA: 0x0006ACCC File Offset: 0x00068ECC
		public string Department
		{
			get
			{
				return (string)this[ADUserSchema.Department];
			}
			set
			{
				this[ADUserSchema.Department] = value;
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001885 RID: 6277 RVA: 0x0006ACDA File Offset: 0x00068EDA
		public MultiValuedProperty<ADObjectId> DirectReports
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADUserSchema.DirectReports];
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001886 RID: 6278 RVA: 0x0006ACEC File Offset: 0x00068EEC
		// (set) Token: 0x06001887 RID: 6279 RVA: 0x0006ACFE File Offset: 0x00068EFE
		public string Fax
		{
			get
			{
				return (string)this[ADUserSchema.Fax];
			}
			set
			{
				this[ADUserSchema.Fax] = value;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001888 RID: 6280 RVA: 0x0006AD0C File Offset: 0x00068F0C
		// (set) Token: 0x06001889 RID: 6281 RVA: 0x0006AD1E File Offset: 0x00068F1E
		public string FirstName
		{
			get
			{
				return (string)this[ADUserSchema.FirstName];
			}
			set
			{
				this[ADUserSchema.FirstName] = value;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x0600188A RID: 6282 RVA: 0x0006AD2C File Offset: 0x00068F2C
		// (set) Token: 0x0600188B RID: 6283 RVA: 0x0006AD3E File Offset: 0x00068F3E
		public string HomePhone
		{
			get
			{
				return (string)this[ADUserSchema.HomePhone];
			}
			set
			{
				this[ADUserSchema.HomePhone] = value;
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x0600188C RID: 6284 RVA: 0x0006AD4C File Offset: 0x00068F4C
		public MultiValuedProperty<string> IndexedPhoneNumbers
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADRecipientSchema.IndexedPhoneNumbers];
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x0600188D RID: 6285 RVA: 0x0006AD5E File Offset: 0x00068F5E
		// (set) Token: 0x0600188E RID: 6286 RVA: 0x0006AD70 File Offset: 0x00068F70
		public string Initials
		{
			get
			{
				return (string)this[ADUserSchema.Initials];
			}
			set
			{
				this[ADUserSchema.Initials] = value;
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x0600188F RID: 6287 RVA: 0x0006AD7E File Offset: 0x00068F7E
		// (set) Token: 0x06001890 RID: 6288 RVA: 0x0006AD90 File Offset: 0x00068F90
		public MultiValuedProperty<CultureInfo> Languages
		{
			get
			{
				return (MultiValuedProperty<CultureInfo>)this[ADUserSchema.Languages];
			}
			set
			{
				this[ADUserSchema.Languages] = value;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001891 RID: 6289 RVA: 0x0006AD9E File Offset: 0x00068F9E
		// (set) Token: 0x06001892 RID: 6290 RVA: 0x0006ADB0 File Offset: 0x00068FB0
		public string LastName
		{
			get
			{
				return (string)this[ADUserSchema.LastName];
			}
			set
			{
				this[ADUserSchema.LastName] = value;
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001893 RID: 6291 RVA: 0x0006ADBE File Offset: 0x00068FBE
		// (set) Token: 0x06001894 RID: 6292 RVA: 0x0006ADD0 File Offset: 0x00068FD0
		public ADObjectId Manager
		{
			get
			{
				return (ADObjectId)this[ADUserSchema.Manager];
			}
			set
			{
				this[ADUserSchema.Manager] = value;
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001895 RID: 6293 RVA: 0x0006ADDE File Offset: 0x00068FDE
		// (set) Token: 0x06001896 RID: 6294 RVA: 0x0006ADF0 File Offset: 0x00068FF0
		public string MobilePhone
		{
			get
			{
				return (string)this[ADUserSchema.MobilePhone];
			}
			set
			{
				this[ADUserSchema.MobilePhone] = value;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001897 RID: 6295 RVA: 0x0006ADFE File Offset: 0x00068FFE
		// (set) Token: 0x06001898 RID: 6296 RVA: 0x0006AE10 File Offset: 0x00069010
		public string Office
		{
			get
			{
				return (string)this[ADUserSchema.Office];
			}
			set
			{
				this[ADUserSchema.Office] = value;
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06001899 RID: 6297 RVA: 0x0006AE1E File Offset: 0x0006901E
		// (set) Token: 0x0600189A RID: 6298 RVA: 0x0006AE30 File Offset: 0x00069030
		public MultiValuedProperty<string> OtherFax
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADUserSchema.OtherFax];
			}
			set
			{
				this[ADUserSchema.OtherFax] = value;
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x0600189B RID: 6299 RVA: 0x0006AE3E File Offset: 0x0006903E
		// (set) Token: 0x0600189C RID: 6300 RVA: 0x0006AE50 File Offset: 0x00069050
		public MultiValuedProperty<string> OtherHomePhone
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADUserSchema.OtherHomePhone];
			}
			set
			{
				this[ADUserSchema.OtherHomePhone] = value;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x0600189D RID: 6301 RVA: 0x0006AE5E File Offset: 0x0006905E
		// (set) Token: 0x0600189E RID: 6302 RVA: 0x0006AE70 File Offset: 0x00069070
		public MultiValuedProperty<string> OtherTelephone
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADUserSchema.OtherTelephone];
			}
			set
			{
				this[ADUserSchema.OtherTelephone] = value;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x0600189F RID: 6303 RVA: 0x0006AE7E File Offset: 0x0006907E
		// (set) Token: 0x060018A0 RID: 6304 RVA: 0x0006AE90 File Offset: 0x00069090
		public string Pager
		{
			get
			{
				return (string)this[ADUserSchema.Pager];
			}
			set
			{
				this[ADUserSchema.Pager] = value;
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x060018A1 RID: 6305 RVA: 0x0006AE9E File Offset: 0x0006909E
		// (set) Token: 0x060018A2 RID: 6306 RVA: 0x0006AEB0 File Offset: 0x000690B0
		public string Phone
		{
			get
			{
				return (string)this[ADUserSchema.Phone];
			}
			set
			{
				this[ADUserSchema.Phone] = value;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x060018A3 RID: 6307 RVA: 0x0006AEBE File Offset: 0x000690BE
		// (set) Token: 0x060018A4 RID: 6308 RVA: 0x0006AED0 File Offset: 0x000690D0
		public string PostalCode
		{
			get
			{
				return (string)this[ADUserSchema.PostalCode];
			}
			set
			{
				this[ADUserSchema.PostalCode] = value;
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x060018A5 RID: 6309 RVA: 0x0006AEDE File Offset: 0x000690DE
		// (set) Token: 0x060018A6 RID: 6310 RVA: 0x0006AEF0 File Offset: 0x000690F0
		public MultiValuedProperty<string> PostOfficeBox
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADUserSchema.PostOfficeBox];
			}
			set
			{
				this[ADUserSchema.PostOfficeBox] = value;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x060018A7 RID: 6311 RVA: 0x0006AEFE File Offset: 0x000690FE
		public string RtcSipLine
		{
			get
			{
				return (string)this[ADUserSchema.RtcSipLine];
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x060018A8 RID: 6312 RVA: 0x0006AF10 File Offset: 0x00069110
		public MultiValuedProperty<string> SanitizedPhoneNumbers
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADUserSchema.SanitizedPhoneNumbers];
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x060018A9 RID: 6313 RVA: 0x0006AF22 File Offset: 0x00069122
		// (set) Token: 0x060018AA RID: 6314 RVA: 0x0006AF34 File Offset: 0x00069134
		public string StateOrProvince
		{
			get
			{
				return (string)this[ADUserSchema.StateOrProvince];
			}
			set
			{
				this[ADUserSchema.StateOrProvince] = value;
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x060018AB RID: 6315 RVA: 0x0006AF42 File Offset: 0x00069142
		// (set) Token: 0x060018AC RID: 6316 RVA: 0x0006AF54 File Offset: 0x00069154
		public string StreetAddress
		{
			get
			{
				return (string)this[ADUserSchema.StreetAddress];
			}
			set
			{
				this[ADUserSchema.StreetAddress] = value;
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x060018AD RID: 6317 RVA: 0x0006AF62 File Offset: 0x00069162
		// (set) Token: 0x060018AE RID: 6318 RVA: 0x0006AF74 File Offset: 0x00069174
		public string TelephoneAssistant
		{
			get
			{
				return (string)this[ADUserSchema.TelephoneAssistant];
			}
			set
			{
				this[ADUserSchema.TelephoneAssistant] = value;
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x060018AF RID: 6319 RVA: 0x0006AF82 File Offset: 0x00069182
		// (set) Token: 0x060018B0 RID: 6320 RVA: 0x0006AF94 File Offset: 0x00069194
		public string Title
		{
			get
			{
				return (string)this[ADUserSchema.Title];
			}
			set
			{
				this[ADUserSchema.Title] = value;
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x060018B1 RID: 6321 RVA: 0x0006AFA2 File Offset: 0x000691A2
		// (set) Token: 0x060018B2 RID: 6322 RVA: 0x0006AFB4 File Offset: 0x000691B4
		public MultiValuedProperty<string> UMCallingLineIds
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADUserSchema.UMCallingLineIds];
			}
			set
			{
				this[ADUserSchema.UMCallingLineIds] = value;
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x060018B3 RID: 6323 RVA: 0x0006AFC2 File Offset: 0x000691C2
		// (set) Token: 0x060018B4 RID: 6324 RVA: 0x0006AFD4 File Offset: 0x000691D4
		public MultiValuedProperty<string> VoiceMailSettings
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADUserSchema.VoiceMailSettings];
			}
			set
			{
				this[ADUserSchema.VoiceMailSettings] = value;
			}
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x0006AFE2 File Offset: 0x000691E2
		public object[][] GetManagementChainView(bool getPeers, params PropertyDefinition[] returnProperties)
		{
			return ADOrgPerson.GetManagementChainView(base.Session, this, getPeers, returnProperties);
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x0006AFF2 File Offset: 0x000691F2
		public object[][] GetDirectReportsView(params PropertyDefinition[] returnProperties)
		{
			return ADOrgPerson.GetDirectReportsView(base.Session, this, returnProperties);
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x0006B004 File Offset: 0x00069204
		public override void PopulateDtmfMap(bool create)
		{
			string text = (this.FirstName + this.LastName).Trim();
			string lastFirst = (this.LastName + this.FirstName).Trim();
			if (string.IsNullOrEmpty(text))
			{
				text = base.DisplayName;
				lastFirst = base.DisplayName;
			}
			base.PopulateDtmfMap(create, text, lastFirst, base.PrimarySmtpAddress, this.SanitizedPhoneNumbers);
		}

		// Token: 0x04000B49 RID: 2889
		private const byte DowngradeMailBitMask = 128;

		// Token: 0x04000B4A RID: 2890
		private const int DowngradeMailBitIndex = 3;

		// Token: 0x04000B4B RID: 2891
		internal const string UserPrincipalNamePattern = "^.*@[^@]+$";

		// Token: 0x04000B4C RID: 2892
		private static readonly ADUserSchema schema = ObjectSchema.GetInstance<ADUserSchema>();

		// Token: 0x04000B4D RID: 2893
		internal static string MostDerivedClass = "user";

		// Token: 0x04000B4E RID: 2894
		internal static string ObjectCategoryNameInternal = "person";

		// Token: 0x04000B4F RID: 2895
		private string netIDSuffixCopy = string.Empty;

		// Token: 0x04000B50 RID: 2896
		internal static QueryFilter ImplicitFilterInternal = new AndFilter(new QueryFilter[]
		{
			ADObject.ObjectClassFilter(ADUser.MostDerivedClass, true),
			ADObject.ObjectCategoryFilter(ADUser.ObjectCategoryNameInternal)
		});

		// Token: 0x04000B51 RID: 2897
		public static readonly string SamAccountNameInvalidCharacters = "\"\\/[]:|<>+=;?,*\u0001\u0002\u0003\u0004\u0005\u0006\a\b\t\n\v\f\r\u000e\u000f\u0010\u0011\u0012\u0013\u0014\u0015\u0016\u0017\u0018\u0019\u001a\u001b\u001c\u001d\u001e\u001f@";
	}
}
