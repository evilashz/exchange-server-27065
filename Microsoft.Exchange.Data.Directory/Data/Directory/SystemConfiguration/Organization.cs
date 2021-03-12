using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002D5 RID: 725
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class Organization : ADLegacyVersionableObject
	{
		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x0600208E RID: 8334 RVA: 0x000930CC File Offset: 0x000912CC
		public static int OrgConfigurationVersion
		{
			get
			{
				return OrganizationSchema.OrgConfigurationVersion;
			}
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x0600208F RID: 8335 RVA: 0x000930D3 File Offset: 0x000912D3
		// (set) Token: 0x06002090 RID: 8336 RVA: 0x000930DB File Offset: 0x000912DB
		public new string Name
		{
			get
			{
				return base.Name;
			}
			internal set
			{
				base.Name = value;
			}
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06002091 RID: 8337 RVA: 0x000930E4 File Offset: 0x000912E4
		internal override ADObjectSchema Schema
		{
			get
			{
				return Organization.schema;
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06002092 RID: 8338 RVA: 0x000930EB File Offset: 0x000912EB
		internal override string MostDerivedObjectClass
		{
			get
			{
				return Organization.MostDerivedClass;
			}
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06002093 RID: 8339 RVA: 0x000930F2 File Offset: 0x000912F2
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return Organization.CurrentExchangeRootOrgVersion;
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06002095 RID: 8341 RVA: 0x00093101 File Offset: 0x00091301
		// (set) Token: 0x06002096 RID: 8342 RVA: 0x00093113 File Offset: 0x00091313
		public bool AllowDeleteOfExternalIdentityUponRemove
		{
			get
			{
				return (bool)this[OrganizationSchema.AllowDeleteOfExternalIdentityUponRemove];
			}
			set
			{
				this[OrganizationSchema.AllowDeleteOfExternalIdentityUponRemove] = value;
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06002097 RID: 8343 RVA: 0x00093126 File Offset: 0x00091326
		// (set) Token: 0x06002098 RID: 8344 RVA: 0x00093138 File Offset: 0x00091338
		public bool HostingDeploymentEnabled
		{
			get
			{
				return (bool)this[OrganizationSchema.HostingDeploymentEnabled];
			}
			internal set
			{
				this[OrganizationSchema.HostingDeploymentEnabled] = value;
			}
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06002099 RID: 8345 RVA: 0x0009314B File Offset: 0x0009134B
		// (set) Token: 0x0600209A RID: 8346 RVA: 0x0009315D File Offset: 0x0009135D
		public string LegacyExchangeDN
		{
			get
			{
				return (string)this[OrganizationSchema.LegacyExchangeDN];
			}
			internal set
			{
				this[OrganizationSchema.LegacyExchangeDN] = value;
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x0600209B RID: 8347 RVA: 0x0009316B File Offset: 0x0009136B
		// (set) Token: 0x0600209C RID: 8348 RVA: 0x0009317D File Offset: 0x0009137D
		public HeuristicsFlags Heuristics
		{
			get
			{
				return (HeuristicsFlags)this[OrganizationSchema.Heuristics];
			}
			internal set
			{
				this[OrganizationSchema.Heuristics] = value;
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x0600209D RID: 8349 RVA: 0x00093190 File Offset: 0x00091390
		// (set) Token: 0x0600209E RID: 8350 RVA: 0x000931A2 File Offset: 0x000913A2
		public MultiValuedProperty<ADObjectId> ResourceAddressLists
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[OrganizationSchema.ResourceAddressLists];
			}
			internal set
			{
				this[OrganizationSchema.ResourceAddressLists] = value;
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x0600209F RID: 8351 RVA: 0x000931B0 File Offset: 0x000913B0
		public bool IsMixedMode
		{
			get
			{
				return (bool)this[OrganizationSchema.IsMixedMode];
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x060020A0 RID: 8352 RVA: 0x000931C2 File Offset: 0x000913C2
		// (set) Token: 0x060020A1 RID: 8353 RVA: 0x000931D4 File Offset: 0x000913D4
		public bool IsAddressListPagingEnabled
		{
			get
			{
				return (bool)this[OrganizationSchema.IsAddressListPagingEnabled];
			}
			internal set
			{
				this[OrganizationSchema.IsAddressListPagingEnabled] = value;
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x060020A2 RID: 8354 RVA: 0x000931E7 File Offset: 0x000913E7
		// (set) Token: 0x060020A3 RID: 8355 RVA: 0x000931F9 File Offset: 0x000913F9
		public virtual string BuildNumber
		{
			get
			{
				return (string)this[OrganizationSchema.BuildNumber];
			}
			set
			{
				this[OrganizationSchema.BuildNumber] = value;
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x060020A4 RID: 8356 RVA: 0x00093207 File Offset: 0x00091407
		// (set) Token: 0x060020A5 RID: 8357 RVA: 0x00093219 File Offset: 0x00091419
		public virtual string ManagedFolderHomepage
		{
			get
			{
				return (string)this[OrganizationSchema.ManagedFolderHomepage];
			}
			set
			{
				this[OrganizationSchema.ManagedFolderHomepage] = value;
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x060020A6 RID: 8358 RVA: 0x00093227 File Offset: 0x00091427
		// (set) Token: 0x060020A7 RID: 8359 RVA: 0x00093239 File Offset: 0x00091439
		public virtual EnhancedTimeSpan? DefaultPublicFolderAgeLimit
		{
			get
			{
				return (EnhancedTimeSpan?)this[OrganizationSchema.DefaultPublicFolderAgeLimit];
			}
			set
			{
				this[OrganizationSchema.DefaultPublicFolderAgeLimit] = value;
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x060020A8 RID: 8360 RVA: 0x0009324C File Offset: 0x0009144C
		// (set) Token: 0x060020A9 RID: 8361 RVA: 0x0009325E File Offset: 0x0009145E
		public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderIssueWarningQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[OrganizationSchema.DefaultPublicFolderIssueWarningQuota];
			}
			set
			{
				this[OrganizationSchema.DefaultPublicFolderIssueWarningQuota] = value;
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x060020AA RID: 8362 RVA: 0x00093271 File Offset: 0x00091471
		// (set) Token: 0x060020AB RID: 8363 RVA: 0x00093283 File Offset: 0x00091483
		public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderProhibitPostQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[OrganizationSchema.DefaultPublicFolderProhibitPostQuota];
			}
			set
			{
				this[OrganizationSchema.DefaultPublicFolderProhibitPostQuota] = value;
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x060020AC RID: 8364 RVA: 0x00093296 File Offset: 0x00091496
		// (set) Token: 0x060020AD RID: 8365 RVA: 0x000932A8 File Offset: 0x000914A8
		public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderMaxItemSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[OrganizationSchema.DefaultPublicFolderMaxItemSize];
			}
			set
			{
				this[OrganizationSchema.DefaultPublicFolderMaxItemSize] = value;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x060020AE RID: 8366 RVA: 0x000932BB File Offset: 0x000914BB
		// (set) Token: 0x060020AF RID: 8367 RVA: 0x000932CD File Offset: 0x000914CD
		public virtual EnhancedTimeSpan? DefaultPublicFolderDeletedItemRetention
		{
			get
			{
				return (EnhancedTimeSpan?)this[OrganizationSchema.DefaultPublicFolderDeletedItemRetention];
			}
			set
			{
				this[OrganizationSchema.DefaultPublicFolderDeletedItemRetention] = value;
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x060020B0 RID: 8368 RVA: 0x000932E0 File Offset: 0x000914E0
		// (set) Token: 0x060020B1 RID: 8369 RVA: 0x000932F2 File Offset: 0x000914F2
		public virtual EnhancedTimeSpan? DefaultPublicFolderMovedItemRetention
		{
			get
			{
				return (EnhancedTimeSpan?)this[OrganizationSchema.DefaultPublicFolderMovedItemRetention];
			}
			set
			{
				this[OrganizationSchema.DefaultPublicFolderMovedItemRetention] = value;
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x060020B2 RID: 8370 RVA: 0x00093305 File Offset: 0x00091505
		// (set) Token: 0x060020B3 RID: 8371 RVA: 0x00093317 File Offset: 0x00091517
		public virtual string ServiceEndpoints
		{
			get
			{
				return (string)this[OrganizationSchema.ServiceEndpoints];
			}
			set
			{
				this[OrganizationSchema.ServiceEndpoints] = value;
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x060020B4 RID: 8372 RVA: 0x00093325 File Offset: 0x00091525
		// (set) Token: 0x060020B5 RID: 8373 RVA: 0x00093337 File Offset: 0x00091537
		public MultiValuedProperty<string> ForeignForestFQDN
		{
			get
			{
				return (MultiValuedProperty<string>)this[OrganizationSchema.ForeignForestFQDN];
			}
			internal set
			{
				this[OrganizationSchema.ForeignForestFQDN] = value;
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x060020B6 RID: 8374 RVA: 0x00093345 File Offset: 0x00091545
		// (set) Token: 0x060020B7 RID: 8375 RVA: 0x00093357 File Offset: 0x00091557
		public SecurityIdentifier ForeignForestOrgAdminUSGSid
		{
			get
			{
				return (SecurityIdentifier)this[OrganizationSchema.ForeignForestOrgAdminUSGSid];
			}
			internal set
			{
				this[OrganizationSchema.ForeignForestOrgAdminUSGSid] = value;
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x060020B8 RID: 8376 RVA: 0x00093365 File Offset: 0x00091565
		// (set) Token: 0x060020B9 RID: 8377 RVA: 0x00093377 File Offset: 0x00091577
		public SecurityIdentifier ForeignForestRecipientAdminUSGSid
		{
			get
			{
				return (SecurityIdentifier)this[OrganizationSchema.ForeignForestRecipientAdminUSGSid];
			}
			internal set
			{
				this[OrganizationSchema.ForeignForestRecipientAdminUSGSid] = value;
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x060020BA RID: 8378 RVA: 0x00093385 File Offset: 0x00091585
		// (set) Token: 0x060020BB RID: 8379 RVA: 0x00093397 File Offset: 0x00091597
		public SecurityIdentifier ForeignForestViewOnlyAdminUSGSid
		{
			get
			{
				return (SecurityIdentifier)this[OrganizationSchema.ForeignForestViewOnlyAdminUSGSid];
			}
			internal set
			{
				this[OrganizationSchema.ForeignForestViewOnlyAdminUSGSid] = value;
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x060020BC RID: 8380 RVA: 0x000933A5 File Offset: 0x000915A5
		// (set) Token: 0x060020BD RID: 8381 RVA: 0x000933BC File Offset: 0x000915BC
		public int ObjectVersion
		{
			get
			{
				return (int)this.propertyBag[OrganizationSchema.ObjectVersion];
			}
			internal set
			{
				this.propertyBag[OrganizationSchema.ObjectVersion] = value;
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x060020BE RID: 8382 RVA: 0x000933D4 File Offset: 0x000915D4
		// (set) Token: 0x060020BF RID: 8383 RVA: 0x000933E6 File Offset: 0x000915E6
		public int SCLJunkThreshold
		{
			get
			{
				return (int)this[OrganizationSchema.SCLJunkThreshold];
			}
			set
			{
				this[OrganizationSchema.SCLJunkThreshold] = value;
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x060020C0 RID: 8384 RVA: 0x000933F9 File Offset: 0x000915F9
		// (set) Token: 0x060020C1 RID: 8385 RVA: 0x0009340B File Offset: 0x0009160B
		public MultiValuedProperty<string> AcceptedDomainNames
		{
			get
			{
				return (MultiValuedProperty<string>)this[OrganizationSchema.AcceptedDomainNames];
			}
			set
			{
				this[OrganizationSchema.AcceptedDomainNames] = value;
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x060020C2 RID: 8386 RVA: 0x00093419 File Offset: 0x00091619
		// (set) Token: 0x060020C3 RID: 8387 RVA: 0x0009342B File Offset: 0x0009162B
		public MultiValuedProperty<string> MimeTypes
		{
			get
			{
				return (MultiValuedProperty<string>)this[OrganizationSchema.MimeTypes];
			}
			internal set
			{
				this[OrganizationSchema.MimeTypes] = value;
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x060020C4 RID: 8388 RVA: 0x00093439 File Offset: 0x00091639
		// (set) Token: 0x060020C5 RID: 8389 RVA: 0x0009344B File Offset: 0x0009164B
		public virtual ProxyAddressCollection MicrosoftExchangeRecipientEmailAddresses
		{
			get
			{
				return (ProxyAddressCollection)this[OrganizationSchema.MicrosoftExchangeRecipientEmailAddresses];
			}
			set
			{
				this[OrganizationSchema.MicrosoftExchangeRecipientEmailAddresses] = value;
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x060020C6 RID: 8390 RVA: 0x00093459 File Offset: 0x00091659
		// (set) Token: 0x060020C7 RID: 8391 RVA: 0x0009346B File Offset: 0x0009166B
		public virtual ADObjectId MicrosoftExchangeRecipientReplyRecipient
		{
			get
			{
				return (ADObjectId)this[OrganizationSchema.MicrosoftExchangeRecipientReplyRecipient];
			}
			set
			{
				this[OrganizationSchema.MicrosoftExchangeRecipientReplyRecipient] = value;
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x060020C8 RID: 8392 RVA: 0x00093479 File Offset: 0x00091679
		// (set) Token: 0x060020C9 RID: 8393 RVA: 0x0009348B File Offset: 0x0009168B
		public int? MaxAddressBookPolicies
		{
			get
			{
				return (int?)this[OrganizationSchema.MaxAddressBookPolicies];
			}
			set
			{
				this[OrganizationSchema.MaxAddressBookPolicies] = value;
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x060020CA RID: 8394 RVA: 0x0009349E File Offset: 0x0009169E
		// (set) Token: 0x060020CB RID: 8395 RVA: 0x000934B0 File Offset: 0x000916B0
		public int? MaxOfflineAddressBooks
		{
			get
			{
				return (int?)this[OrganizationSchema.MaxOfflineAddressBooks];
			}
			set
			{
				this[OrganizationSchema.MaxOfflineAddressBooks] = value;
			}
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x060020CC RID: 8396 RVA: 0x000934C3 File Offset: 0x000916C3
		// (set) Token: 0x060020CD RID: 8397 RVA: 0x000934D5 File Offset: 0x000916D5
		public virtual SmtpAddress MicrosoftExchangeRecipientPrimarySmtpAddress
		{
			get
			{
				return (SmtpAddress)this[OrganizationSchema.MicrosoftExchangeRecipientPrimarySmtpAddress];
			}
			set
			{
				this[OrganizationSchema.MicrosoftExchangeRecipientPrimarySmtpAddress] = value;
			}
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x060020CE RID: 8398 RVA: 0x000934E8 File Offset: 0x000916E8
		// (set) Token: 0x060020CF RID: 8399 RVA: 0x000934FA File Offset: 0x000916FA
		public virtual bool MicrosoftExchangeRecipientEmailAddressPolicyEnabled
		{
			get
			{
				return (bool)this[OrganizationSchema.MicrosoftExchangeRecipientEmailAddressPolicyEnabled];
			}
			set
			{
				this[OrganizationSchema.MicrosoftExchangeRecipientEmailAddressPolicyEnabled] = value;
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x060020D0 RID: 8400 RVA: 0x0009350D File Offset: 0x0009170D
		// (set) Token: 0x060020D1 RID: 8401 RVA: 0x0009351F File Offset: 0x0009171F
		public virtual IndustryType Industry
		{
			get
			{
				return (IndustryType)this[OrganizationSchema.Industry];
			}
			set
			{
				this[OrganizationSchema.Industry] = value;
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x060020D2 RID: 8402 RVA: 0x00093532 File Offset: 0x00091732
		// (set) Token: 0x060020D3 RID: 8403 RVA: 0x00093544 File Offset: 0x00091744
		public virtual bool? CustomerFeedbackEnabled
		{
			get
			{
				return (bool?)this[OrganizationSchema.CustomerFeedbackEnabled];
			}
			set
			{
				this[OrganizationSchema.CustomerFeedbackEnabled] = value;
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x060020D4 RID: 8404 RVA: 0x00093557 File Offset: 0x00091757
		// (set) Token: 0x060020D5 RID: 8405 RVA: 0x00093569 File Offset: 0x00091769
		public virtual MultiValuedProperty<OrganizationSummaryEntry> OrganizationSummary
		{
			get
			{
				return (MultiValuedProperty<OrganizationSummaryEntry>)this[OrganizationSchema.OrganizationSummary];
			}
			set
			{
				this[OrganizationSchema.OrganizationSummary] = value;
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x060020D6 RID: 8406 RVA: 0x00093577 File Offset: 0x00091777
		// (set) Token: 0x060020D7 RID: 8407 RVA: 0x00093589 File Offset: 0x00091789
		public virtual bool MailTipsExternalRecipientsTipsEnabled
		{
			get
			{
				return (bool)this[OrganizationSchema.MailTipsExternalRecipientsTipsEnabled];
			}
			set
			{
				this[OrganizationSchema.MailTipsExternalRecipientsTipsEnabled] = value;
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x060020D8 RID: 8408 RVA: 0x0009359C File Offset: 0x0009179C
		// (set) Token: 0x060020D9 RID: 8409 RVA: 0x000935AF File Offset: 0x000917AF
		public virtual uint MailTipsLargeAudienceThreshold
		{
			get
			{
				return (uint)((long)this[OrganizationSchema.MailTipsLargeAudienceThreshold]);
			}
			set
			{
				this[OrganizationSchema.MailTipsLargeAudienceThreshold] = (long)((ulong)value);
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x060020DA RID: 8410 RVA: 0x000935C3 File Offset: 0x000917C3
		// (set) Token: 0x060020DB RID: 8411 RVA: 0x000935D5 File Offset: 0x000917D5
		public virtual bool MailTipsMailboxSourcedTipsEnabled
		{
			get
			{
				return (bool)this[OrganizationSchema.MailTipsMailboxSourcedTipsEnabled];
			}
			set
			{
				this[OrganizationSchema.MailTipsMailboxSourcedTipsEnabled] = value;
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x060020DC RID: 8412 RVA: 0x000935E8 File Offset: 0x000917E8
		// (set) Token: 0x060020DD RID: 8413 RVA: 0x000935FA File Offset: 0x000917FA
		public virtual bool MailTipsGroupMetricsEnabled
		{
			get
			{
				return (bool)this[OrganizationSchema.MailTipsGroupMetricsEnabled];
			}
			set
			{
				this[OrganizationSchema.MailTipsGroupMetricsEnabled] = value;
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x060020DE RID: 8414 RVA: 0x0009360D File Offset: 0x0009180D
		// (set) Token: 0x060020DF RID: 8415 RVA: 0x0009361F File Offset: 0x0009181F
		public virtual bool MailTipsAllTipsEnabled
		{
			get
			{
				return (bool)this[OrganizationSchema.MailTipsAllTipsEnabled];
			}
			set
			{
				this[OrganizationSchema.MailTipsAllTipsEnabled] = value;
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x060020E0 RID: 8416 RVA: 0x00093632 File Offset: 0x00091832
		// (set) Token: 0x060020E1 RID: 8417 RVA: 0x00093644 File Offset: 0x00091844
		public virtual bool IsProcessEhaMigratedMessagesEnabled
		{
			get
			{
				return (bool)this[OrganizationSchema.IsProcessEhaMigratedMessagesEnabled];
			}
			set
			{
				this[OrganizationSchema.IsProcessEhaMigratedMessagesEnabled] = value;
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x060020E2 RID: 8418 RVA: 0x00093657 File Offset: 0x00091857
		// (set) Token: 0x060020E3 RID: 8419 RVA: 0x00093669 File Offset: 0x00091869
		public virtual bool ReadTrackingEnabled
		{
			get
			{
				return (bool)this[OrganizationSchema.ReadTrackingEnabled];
			}
			set
			{
				this[OrganizationSchema.ReadTrackingEnabled] = value;
			}
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x060020E4 RID: 8420 RVA: 0x0009367C File Offset: 0x0009187C
		// (set) Token: 0x060020E5 RID: 8421 RVA: 0x0009368E File Offset: 0x0009188E
		public ADObjectId DistributionGroupDefaultOU
		{
			get
			{
				return (ADObjectId)this[OrganizationSchema.DistributionGroupDefaultOU];
			}
			set
			{
				this[OrganizationSchema.DistributionGroupDefaultOU] = value;
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x060020E6 RID: 8422 RVA: 0x0009369C File Offset: 0x0009189C
		// (set) Token: 0x060020E7 RID: 8423 RVA: 0x000936AE File Offset: 0x000918AE
		public virtual MultiValuedProperty<string> DistributionGroupNameBlockedWordsList
		{
			get
			{
				return (MultiValuedProperty<string>)this[OrganizationSchema.DistributionGroupNameBlockedWordsList];
			}
			set
			{
				this[OrganizationSchema.DistributionGroupNameBlockedWordsList] = value;
			}
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x060020E8 RID: 8424 RVA: 0x000936BC File Offset: 0x000918BC
		// (set) Token: 0x060020E9 RID: 8425 RVA: 0x000936CE File Offset: 0x000918CE
		public virtual DistributionGroupNamingPolicy DistributionGroupNamingPolicy
		{
			get
			{
				return (DistributionGroupNamingPolicy)this[OrganizationSchema.DistributionGroupNamingPolicy];
			}
			set
			{
				this[OrganizationSchema.DistributionGroupNamingPolicy] = value;
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x060020EA RID: 8426 RVA: 0x000936DC File Offset: 0x000918DC
		// (set) Token: 0x060020EB RID: 8427 RVA: 0x000936EE File Offset: 0x000918EE
		public virtual bool IsExcludedFromOnboardMigration
		{
			get
			{
				return (bool)this[OrganizationSchema.IsExcludedFromOnboardMigration];
			}
			set
			{
				this[OrganizationSchema.IsExcludedFromOnboardMigration] = value;
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x060020EC RID: 8428 RVA: 0x00093701 File Offset: 0x00091901
		// (set) Token: 0x060020ED RID: 8429 RVA: 0x00093713 File Offset: 0x00091913
		public virtual bool IsExcludedFromOffboardMigration
		{
			get
			{
				return (bool)this[OrganizationSchema.IsExcludedFromOffboardMigration];
			}
			set
			{
				this[OrganizationSchema.IsExcludedFromOffboardMigration] = value;
			}
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x060020EE RID: 8430 RVA: 0x00093726 File Offset: 0x00091926
		// (set) Token: 0x060020EF RID: 8431 RVA: 0x00093738 File Offset: 0x00091938
		public virtual bool IsFfoMigrationInProgress
		{
			get
			{
				return (bool)this[OrganizationSchema.IsFfoMigrationInProgress];
			}
			set
			{
				this[OrganizationSchema.IsFfoMigrationInProgress] = value;
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x060020F0 RID: 8432 RVA: 0x0009374B File Offset: 0x0009194B
		// (set) Token: 0x060020F1 RID: 8433 RVA: 0x0009375D File Offset: 0x0009195D
		public virtual bool TenantRelocationsAllowed
		{
			get
			{
				return (bool)this[OrganizationSchema.TenantRelocationsAllowed];
			}
			set
			{
				this[OrganizationSchema.TenantRelocationsAllowed] = value;
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x060020F2 RID: 8434 RVA: 0x00093770 File Offset: 0x00091970
		// (set) Token: 0x060020F3 RID: 8435 RVA: 0x00093782 File Offset: 0x00091982
		public virtual bool ACLableSyncedObjectEnabled
		{
			get
			{
				return (bool)this[OrganizationSchema.ACLableSyncedObjectEnabled];
			}
			set
			{
				this[OrganizationSchema.ACLableSyncedObjectEnabled] = value;
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x060020F4 RID: 8436 RVA: 0x00093795 File Offset: 0x00091995
		// (set) Token: 0x060020F5 RID: 8437 RVA: 0x000937A7 File Offset: 0x000919A7
		public bool OpenTenantFull
		{
			get
			{
				return (bool)this[OrganizationSchema.OpenTenantFull];
			}
			set
			{
				this[OrganizationSchema.OpenTenantFull] = value;
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x060020F6 RID: 8438 RVA: 0x000937BA File Offset: 0x000919BA
		// (set) Token: 0x060020F7 RID: 8439 RVA: 0x000937CC File Offset: 0x000919CC
		public virtual bool MapiHttpEnabled
		{
			get
			{
				return (bool)this[OrganizationSchema.MapiHttpEnabled];
			}
			set
			{
				this[OrganizationSchema.MapiHttpEnabled] = value;
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x060020F8 RID: 8440 RVA: 0x000937DF File Offset: 0x000919DF
		// (set) Token: 0x060020F9 RID: 8441 RVA: 0x000937F1 File Offset: 0x000919F1
		public virtual bool OAuth2ClientProfileEnabled
		{
			get
			{
				return (bool)this[OrganizationSchema.OAuth2ClientProfileEnabled];
			}
			set
			{
				this[OrganizationSchema.OAuth2ClientProfileEnabled] = value;
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x060020FA RID: 8442 RVA: 0x00093804 File Offset: 0x00091A04
		// (set) Token: 0x060020FB RID: 8443 RVA: 0x00093816 File Offset: 0x00091A16
		public virtual bool IntuneManagedStatus
		{
			get
			{
				return (bool)this[OrganizationSchema.IntuneManagedStatus];
			}
			set
			{
				this[OrganizationSchema.IntuneManagedStatus] = value;
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x060020FC RID: 8444 RVA: 0x00093829 File Offset: 0x00091A29
		// (set) Token: 0x060020FD RID: 8445 RVA: 0x00093849 File Offset: 0x00091A49
		public virtual Unlimited<int> MaxConcurrentMigrations
		{
			get
			{
				return (Unlimited<int>)(this[OrganizationSchema.MaxConcurrentMigrations] ?? Unlimited<int>.UnlimitedValue);
			}
			set
			{
				this[OrganizationSchema.MaxConcurrentMigrations] = value;
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x060020FE RID: 8446 RVA: 0x0009385C File Offset: 0x00091A5C
		// (set) Token: 0x060020FF RID: 8447 RVA: 0x0009386E File Offset: 0x00091A6E
		public virtual string WACDiscoveryEndpoint
		{
			get
			{
				return (string)this[OrganizationSchema.WACDiscoveryEndpoint];
			}
			set
			{
				this[OrganizationSchema.WACDiscoveryEndpoint] = value;
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06002100 RID: 8448 RVA: 0x0009387C File Offset: 0x00091A7C
		// (set) Token: 0x06002101 RID: 8449 RVA: 0x0009388E File Offset: 0x00091A8E
		public virtual bool ForwardSyncLiveIdBusinessInstance
		{
			get
			{
				return (bool)this[OrganizationSchema.ForwardSyncLiveIdBusinessInstance];
			}
			set
			{
				this[OrganizationSchema.ForwardSyncLiveIdBusinessInstance] = value;
			}
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06002102 RID: 8450 RVA: 0x000938A1 File Offset: 0x00091AA1
		// (set) Token: 0x06002103 RID: 8451 RVA: 0x000938B3 File Offset: 0x00091AB3
		public ADObjectId HierarchicalAddressBookRoot
		{
			get
			{
				return (ADObjectId)this[OrganizationSchema.HABRootDepartmentLink];
			}
			internal set
			{
				this[OrganizationSchema.HABRootDepartmentLink] = value;
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06002104 RID: 8452 RVA: 0x000938C1 File Offset: 0x00091AC1
		// (set) Token: 0x06002105 RID: 8453 RVA: 0x000938D3 File Offset: 0x00091AD3
		public virtual ProtocolConnectionSettings SIPAccessService
		{
			get
			{
				return (ProtocolConnectionSettings)this[OrganizationSchema.SIPAccessService];
			}
			set
			{
				this[OrganizationSchema.SIPAccessService] = value;
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06002106 RID: 8454 RVA: 0x000938E1 File Offset: 0x00091AE1
		// (set) Token: 0x06002107 RID: 8455 RVA: 0x000938F3 File Offset: 0x00091AF3
		public virtual ProtocolConnectionSettings AVAuthenticationService
		{
			get
			{
				return (ProtocolConnectionSettings)this[OrganizationSchema.AVAuthenticationService];
			}
			set
			{
				this[OrganizationSchema.AVAuthenticationService] = value;
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06002108 RID: 8456 RVA: 0x00093901 File Offset: 0x00091B01
		// (set) Token: 0x06002109 RID: 8457 RVA: 0x00093913 File Offset: 0x00091B13
		public virtual ProtocolConnectionSettings SIPSessionBorderController
		{
			get
			{
				return (ProtocolConnectionSettings)this[OrganizationSchema.SIPSessionBorderController];
			}
			set
			{
				this[OrganizationSchema.SIPSessionBorderController] = value;
			}
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x0600210A RID: 8458 RVA: 0x00093921 File Offset: 0x00091B21
		// (set) Token: 0x0600210B RID: 8459 RVA: 0x00093933 File Offset: 0x00091B33
		public virtual bool ExchangeNotificationEnabled
		{
			get
			{
				return (bool)this[OrganizationSchema.ExchangeNotificationEnabled];
			}
			set
			{
				this[OrganizationSchema.ExchangeNotificationEnabled] = value;
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x0600210C RID: 8460 RVA: 0x00093946 File Offset: 0x00091B46
		// (set) Token: 0x0600210D RID: 8461 RVA: 0x00093958 File Offset: 0x00091B58
		public MultiValuedProperty<SmtpAddress> ExchangeNotificationRecipients
		{
			get
			{
				return (MultiValuedProperty<SmtpAddress>)this[OrganizationSchema.ExchangeNotificationRecipients];
			}
			set
			{
				this[OrganizationSchema.ExchangeNotificationRecipients] = value;
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x0600210E RID: 8462 RVA: 0x00093966 File Offset: 0x00091B66
		// (set) Token: 0x0600210F RID: 8463 RVA: 0x0009397D File Offset: 0x00091B7D
		public virtual bool? EwsEnabled
		{
			get
			{
				return CASMailboxHelper.ToBooleanNullable((int?)this[OrganizationSchema.EwsEnabled]);
			}
			set
			{
				this[OrganizationSchema.EwsEnabled] = CASMailboxHelper.ToInt32Nullable(value);
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06002110 RID: 8464 RVA: 0x00093995 File Offset: 0x00091B95
		// (set) Token: 0x06002111 RID: 8465 RVA: 0x000939A7 File Offset: 0x00091BA7
		public virtual bool? EwsAllowOutlook
		{
			get
			{
				return (bool?)this[OrganizationSchema.EwsAllowOutlook];
			}
			set
			{
				this[OrganizationSchema.EwsAllowOutlook] = value;
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06002112 RID: 8466 RVA: 0x000939BA File Offset: 0x00091BBA
		// (set) Token: 0x06002113 RID: 8467 RVA: 0x000939CC File Offset: 0x00091BCC
		public virtual bool? EwsAllowMacOutlook
		{
			get
			{
				return (bool?)this[OrganizationSchema.EwsAllowMacOutlook];
			}
			set
			{
				this[OrganizationSchema.EwsAllowMacOutlook] = value;
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06002114 RID: 8468 RVA: 0x000939DF File Offset: 0x00091BDF
		// (set) Token: 0x06002115 RID: 8469 RVA: 0x000939F1 File Offset: 0x00091BF1
		public virtual bool? EwsAllowEntourage
		{
			get
			{
				return (bool?)this[OrganizationSchema.EwsAllowEntourage];
			}
			set
			{
				this[OrganizationSchema.EwsAllowEntourage] = value;
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06002116 RID: 8470 RVA: 0x00093A04 File Offset: 0x00091C04
		// (set) Token: 0x06002117 RID: 8471 RVA: 0x00093A16 File Offset: 0x00091C16
		public virtual EwsApplicationAccessPolicy? EwsApplicationAccessPolicy
		{
			get
			{
				return (EwsApplicationAccessPolicy?)this[OrganizationSchema.EwsApplicationAccessPolicy];
			}
			set
			{
				this[OrganizationSchema.EwsApplicationAccessPolicy] = value;
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06002118 RID: 8472 RVA: 0x00093A2C File Offset: 0x00091C2C
		// (set) Token: 0x06002119 RID: 8473 RVA: 0x00093A73 File Offset: 0x00091C73
		public virtual MultiValuedProperty<string> EwsAllowList
		{
			get
			{
				if ((EwsApplicationAccessPolicy?)this[OrganizationSchema.EwsApplicationAccessPolicy] == Microsoft.Exchange.Data.Directory.EwsApplicationAccessPolicy.EnforceAllowList)
				{
					return (MultiValuedProperty<string>)this[OrganizationSchema.EwsExceptions];
				}
				return null;
			}
			set
			{
				this[OrganizationSchema.EwsExceptions] = value;
				this.ewsAllowListSpecified = true;
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x0600211A RID: 8474 RVA: 0x00093A88 File Offset: 0x00091C88
		// (set) Token: 0x0600211B RID: 8475 RVA: 0x00093AD0 File Offset: 0x00091CD0
		public virtual MultiValuedProperty<string> EwsBlockList
		{
			get
			{
				if ((EwsApplicationAccessPolicy?)this[OrganizationSchema.EwsApplicationAccessPolicy] == Microsoft.Exchange.Data.Directory.EwsApplicationAccessPolicy.EnforceBlockList)
				{
					return (MultiValuedProperty<string>)this[OrganizationSchema.EwsExceptions];
				}
				return null;
			}
			set
			{
				this[OrganizationSchema.EwsExceptions] = value;
				this.ewsBlockListSpecified = true;
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x0600211C RID: 8476 RVA: 0x00093AE5 File Offset: 0x00091CE5
		// (set) Token: 0x0600211D RID: 8477 RVA: 0x00093AF7 File Offset: 0x00091CF7
		internal MultiValuedProperty<string> EwsExceptions
		{
			get
			{
				return (MultiValuedProperty<string>)this[OrganizationSchema.EwsExceptions];
			}
			set
			{
				this[OrganizationSchema.EwsExceptions] = value;
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x0600211E RID: 8478 RVA: 0x00093B05 File Offset: 0x00091D05
		internal bool EwsAllowListSpecified
		{
			get
			{
				return this.ewsAllowListSpecified;
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x0600211F RID: 8479 RVA: 0x00093B0D File Offset: 0x00091D0D
		internal bool EwsBlockListSpecified
		{
			get
			{
				return this.ewsBlockListSpecified;
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06002120 RID: 8480 RVA: 0x00093B15 File Offset: 0x00091D15
		internal int BuildMajor
		{
			get
			{
				return (int)this[OrganizationSchema.BuildMajor];
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06002121 RID: 8481 RVA: 0x00093B27 File Offset: 0x00091D27
		internal int BuildMinor
		{
			get
			{
				return (int)this[OrganizationSchema.BuildMinor];
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06002122 RID: 8482 RVA: 0x00093B39 File Offset: 0x00091D39
		// (set) Token: 0x06002123 RID: 8483 RVA: 0x00093B4B File Offset: 0x00091D4B
		public virtual EnhancedTimeSpan ActivityBasedAuthenticationTimeoutInterval
		{
			get
			{
				return (EnhancedTimeSpan)this[OrganizationSchema.ActivityBasedAuthenticationTimeoutInterval];
			}
			set
			{
				this[OrganizationSchema.ActivityBasedAuthenticationTimeoutInterval] = value;
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06002124 RID: 8484 RVA: 0x00093B5E File Offset: 0x00091D5E
		// (set) Token: 0x06002125 RID: 8485 RVA: 0x00093B73 File Offset: 0x00091D73
		public virtual bool ActivityBasedAuthenticationTimeoutEnabled
		{
			get
			{
				return !(bool)this[OrganizationSchema.ActivityBasedAuthenticationTimeoutDisabled];
			}
			set
			{
				this[OrganizationSchema.ActivityBasedAuthenticationTimeoutDisabled] = !value;
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06002126 RID: 8486 RVA: 0x00093B89 File Offset: 0x00091D89
		// (set) Token: 0x06002127 RID: 8487 RVA: 0x00093B9E File Offset: 0x00091D9E
		public virtual bool ActivityBasedAuthenticationTimeoutWithSingleSignOnEnabled
		{
			get
			{
				return !(bool)this[OrganizationSchema.ActivityBasedAuthenticationTimeoutWithSingleSignOnDisabled];
			}
			set
			{
				this[OrganizationSchema.ActivityBasedAuthenticationTimeoutWithSingleSignOnDisabled] = !value;
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06002128 RID: 8488 RVA: 0x00093BB4 File Offset: 0x00091DB4
		internal OfflineAuthenticationProvisioningFlags OfflineAuthFlags
		{
			get
			{
				if (this[OrganizationSchema.OrganizationFlags2] != null)
				{
					return (OfflineAuthenticationProvisioningFlags)(((int)this[OrganizationSchema.OrganizationFlags2] & 24) >> 3);
				}
				return OfflineAuthenticationProvisioningFlags.Disabled;
			}
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x00093BDC File Offset: 0x00091DDC
		internal static int MapIntToPreferredInternetCodePageForShiftJis(int preferredInternetCodePageForShiftJis)
		{
			PreferredInternetCodePageForShiftJisEnum result;
			switch (preferredInternetCodePageForShiftJis)
			{
			case 0:
				result = PreferredInternetCodePageForShiftJisEnum.Undefined;
				break;
			case 1:
				result = PreferredInternetCodePageForShiftJisEnum.Iso2022Jp;
				break;
			case 2:
				result = PreferredInternetCodePageForShiftJisEnum.Esc2022Jp;
				break;
			case 3:
				result = PreferredInternetCodePageForShiftJisEnum.Sio2022Jp;
				break;
			default:
				result = PreferredInternetCodePageForShiftJisEnum.Undefined;
				break;
			}
			return (int)result;
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x0600212A RID: 8490 RVA: 0x00093C22 File Offset: 0x00091E22
		// (set) Token: 0x0600212B RID: 8491 RVA: 0x00093C3C File Offset: 0x00091E3C
		public virtual int PreferredInternetCodePageForShiftJis
		{
			get
			{
				return Organization.MapIntToPreferredInternetCodePageForShiftJis((int)this[OrganizationSchema.PreferredInternetCodePageForShiftJis]);
			}
			set
			{
				PreferredInternetCodePageForShiftJisToIntEnum preferredInternetCodePageForShiftJisToIntEnum;
				if (value != 0)
				{
					switch (value)
					{
					case 50220:
						preferredInternetCodePageForShiftJisToIntEnum = PreferredInternetCodePageForShiftJisToIntEnum.Iso2022Jp;
						break;
					case 50221:
						preferredInternetCodePageForShiftJisToIntEnum = PreferredInternetCodePageForShiftJisToIntEnum.Esc2022Jp;
						break;
					case 50222:
						preferredInternetCodePageForShiftJisToIntEnum = PreferredInternetCodePageForShiftJisToIntEnum.Sio2022Jp;
						break;
					default:
						preferredInternetCodePageForShiftJisToIntEnum = PreferredInternetCodePageForShiftJisToIntEnum.Undefined;
						break;
					}
				}
				else
				{
					preferredInternetCodePageForShiftJisToIntEnum = PreferredInternetCodePageForShiftJisToIntEnum.Undefined;
				}
				this[OrganizationSchema.PreferredInternetCodePageForShiftJis] = (int)preferredInternetCodePageForShiftJisToIntEnum;
			}
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x00093C8C File Offset: 0x00091E8C
		internal static void RequiredCharsetCoverageSetter(object value, IPropertyBag propertyBag)
		{
			int num = (int)propertyBag[OrganizationSchema.ContentConversionFlags];
			int num2 = (int)value;
			int num3 = 127;
			num &= ~(num3 << 5);
			num |= (num2 & num3) << 5;
			num |= 524288;
			propertyBag[OrganizationSchema.ContentConversionFlags] = num;
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x00093CDC File Offset: 0x00091EDC
		internal static object RequiredCharsetCoverageGetter(IPropertyBag propertyBag)
		{
			int num = (int)propertyBag[OrganizationSchema.ContentConversionFlags];
			int num2 = 127;
			int num3 = num2 & num >> 5;
			if ((num & 524288) != 0 || num3 != 0)
			{
				return num3;
			}
			return OrganizationSchema.RequiredCharsetCoverage.DefaultValue;
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x0600212E RID: 8494 RVA: 0x00093D28 File Offset: 0x00091F28
		// (set) Token: 0x0600212F RID: 8495 RVA: 0x00093D3A File Offset: 0x00091F3A
		public virtual int RequiredCharsetCoverage
		{
			get
			{
				return (int)this[OrganizationSchema.RequiredCharsetCoverage];
			}
			set
			{
				this[OrganizationSchema.RequiredCharsetCoverage] = value;
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06002130 RID: 8496 RVA: 0x00093D4D File Offset: 0x00091F4D
		// (set) Token: 0x06002131 RID: 8497 RVA: 0x00093D5F File Offset: 0x00091F5F
		public virtual int ByteEncoderTypeFor7BitCharsets
		{
			get
			{
				return (int)this[OrganizationSchema.ByteEncoderTypeFor7BitCharsets];
			}
			set
			{
				this[OrganizationSchema.ByteEncoderTypeFor7BitCharsets] = value;
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06002132 RID: 8498 RVA: 0x00093D72 File Offset: 0x00091F72
		// (set) Token: 0x06002133 RID: 8499 RVA: 0x00093D84 File Offset: 0x00091F84
		public SoftDeletedFeatureStatusFlags SoftDeletedFeatureStatus
		{
			get
			{
				return (SoftDeletedFeatureStatusFlags)this[OrganizationSchema.SoftDeletedFeatureStatus];
			}
			set
			{
				this[OrganizationSchema.SoftDeletedFeatureStatus] = (int)value;
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06002134 RID: 8500 RVA: 0x00093D97 File Offset: 0x00091F97
		// (set) Token: 0x06002135 RID: 8501 RVA: 0x00093DAC File Offset: 0x00091FAC
		public virtual bool AppsForOfficeEnabled
		{
			get
			{
				return !(bool)this[OrganizationSchema.AppsForOfficeDisabled];
			}
			set
			{
				this[OrganizationSchema.AppsForOfficeDisabled] = !value;
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06002136 RID: 8502 RVA: 0x00093DC2 File Offset: 0x00091FC2
		// (set) Token: 0x06002137 RID: 8503 RVA: 0x00093DD4 File Offset: 0x00091FD4
		public OrganizationConfigXML ConfigXML
		{
			get
			{
				return (OrganizationConfigXML)this[OrganizationSchema.ConfigurationXML];
			}
			set
			{
				this[OrganizationSchema.ConfigurationXML] = value;
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06002138 RID: 8504 RVA: 0x00093DE2 File Offset: 0x00091FE2
		// (set) Token: 0x06002139 RID: 8505 RVA: 0x00093DF4 File Offset: 0x00091FF4
		public int DefaultMovePriority
		{
			get
			{
				return (int)this[OrganizationSchema.DefaultMovePriority];
			}
			set
			{
				this[OrganizationSchema.DefaultMovePriority] = value;
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x0600213A RID: 8506 RVA: 0x00093E07 File Offset: 0x00092007
		// (set) Token: 0x0600213B RID: 8507 RVA: 0x00093E19 File Offset: 0x00092019
		public string UpgradeMessage
		{
			get
			{
				return (string)this[OrganizationSchema.UpgradeMessage];
			}
			set
			{
				this[OrganizationSchema.UpgradeMessage] = value;
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x0600213C RID: 8508 RVA: 0x00093E27 File Offset: 0x00092027
		// (set) Token: 0x0600213D RID: 8509 RVA: 0x00093E39 File Offset: 0x00092039
		public string UpgradeDetails
		{
			get
			{
				return (string)this[OrganizationSchema.UpgradeDetails];
			}
			set
			{
				this[OrganizationSchema.UpgradeDetails] = value;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x0600213E RID: 8510 RVA: 0x00093E47 File Offset: 0x00092047
		// (set) Token: 0x0600213F RID: 8511 RVA: 0x00093E59 File Offset: 0x00092059
		public UpgradeConstraintArray UpgradeConstraints
		{
			get
			{
				return (UpgradeConstraintArray)this[OrganizationSchema.UpgradeConstraints];
			}
			set
			{
				this[OrganizationSchema.UpgradeConstraints] = value;
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06002140 RID: 8512 RVA: 0x00093E67 File Offset: 0x00092067
		// (set) Token: 0x06002141 RID: 8513 RVA: 0x00093E79 File Offset: 0x00092079
		public UpgradeStage? UpgradeStage
		{
			get
			{
				return (UpgradeStage?)this[OrganizationSchema.UpgradeStage];
			}
			set
			{
				this[OrganizationSchema.UpgradeStage] = value;
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06002142 RID: 8514 RVA: 0x00093E8C File Offset: 0x0009208C
		// (set) Token: 0x06002143 RID: 8515 RVA: 0x00093E9E File Offset: 0x0009209E
		public DateTime? UpgradeStageTimeStamp
		{
			get
			{
				return (DateTime?)this[OrganizationSchema.UpgradeStageTimeStamp];
			}
			set
			{
				this[OrganizationSchema.UpgradeStageTimeStamp] = value;
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06002144 RID: 8516 RVA: 0x00093EB1 File Offset: 0x000920B1
		// (set) Token: 0x06002145 RID: 8517 RVA: 0x00093EC3 File Offset: 0x000920C3
		public int? UpgradeE14MbxCountForCurrentStage
		{
			get
			{
				return (int?)this[OrganizationSchema.UpgradeE14MbxCountForCurrentStage];
			}
			set
			{
				this[OrganizationSchema.UpgradeE14MbxCountForCurrentStage] = value;
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06002146 RID: 8518 RVA: 0x00093ED6 File Offset: 0x000920D6
		// (set) Token: 0x06002147 RID: 8519 RVA: 0x00093EE8 File Offset: 0x000920E8
		public int? UpgradeE14RequestCountForCurrentStage
		{
			get
			{
				return (int?)this[OrganizationSchema.UpgradeE14RequestCountForCurrentStage];
			}
			set
			{
				this[OrganizationSchema.UpgradeE14RequestCountForCurrentStage] = value;
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06002148 RID: 8520 RVA: 0x00093EFB File Offset: 0x000920FB
		// (set) Token: 0x06002149 RID: 8521 RVA: 0x00093F0D File Offset: 0x0009210D
		public bool? UpgradeConstraintsDisabled
		{
			get
			{
				return (bool?)this[OrganizationSchema.UpgradeConstraintsDisabled];
			}
			set
			{
				this[OrganizationSchema.UpgradeConstraintsDisabled] = value;
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x0600214A RID: 8522 RVA: 0x00093F20 File Offset: 0x00092120
		// (set) Token: 0x0600214B RID: 8523 RVA: 0x00093F32 File Offset: 0x00092132
		public int? UpgradeUnitsOverride
		{
			get
			{
				return (int?)this[OrganizationSchema.UpgradeUnitsOverride];
			}
			set
			{
				this[OrganizationSchema.UpgradeUnitsOverride] = value;
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x0600214C RID: 8524 RVA: 0x00093F45 File Offset: 0x00092145
		// (set) Token: 0x0600214D RID: 8525 RVA: 0x00093F57 File Offset: 0x00092157
		public DateTime? UpgradeLastE14CountsUpdateTime
		{
			get
			{
				return (DateTime?)this[OrganizationSchema.UpgradeLastE14CountsUpdateTime];
			}
			set
			{
				this[OrganizationSchema.UpgradeLastE14CountsUpdateTime] = value;
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x0600214E RID: 8526 RVA: 0x00093F6A File Offset: 0x0009216A
		// (set) Token: 0x0600214F RID: 8527 RVA: 0x00093F7C File Offset: 0x0009217C
		internal RelocationConstraintArray PersistedRelocationConstraints
		{
			get
			{
				return (RelocationConstraintArray)this[OrganizationSchema.PersistedRelocationConstraints];
			}
			set
			{
				this[OrganizationSchema.PersistedRelocationConstraints] = value;
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06002150 RID: 8528 RVA: 0x00093F8A File Offset: 0x0009218A
		internal bool OriginatedInDifferentForest
		{
			get
			{
				return (bool)this[OrganizationSchema.OriginatedInDifferentForest];
			}
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x00093F9C File Offset: 0x0009219C
		internal static object MimeTypesGetter(IPropertyBag propertyBag)
		{
			return Organization.ParseMimeTypesBlob((byte[])propertyBag[OrganizationSchema.BlobMimeTypes]);
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x00093FB3 File Offset: 0x000921B3
		internal static void MimeTypesSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[OrganizationSchema.BlobMimeTypes] = Organization.BuildMimeTypesBlob((MultiValuedProperty<string>)value);
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x00093FCC File Offset: 0x000921CC
		private static MultiValuedProperty<string> ParseMimeTypesBlob(byte[] blob)
		{
			MultiValuedProperty<string> multiValuedProperty = new MultiValuedProperty<string>();
			if (blob == null)
			{
				return multiValuedProperty;
			}
			int num = 0;
			for (int i = 0; i < blob.Length; i++)
			{
				if (blob[i] == 0)
				{
					int num2 = i - num;
					if (num2 > 0)
					{
						try
						{
							string @string = Encoding.ASCII.GetString(blob, num, num2);
							if (!multiValuedProperty.Contains(@string))
							{
								multiValuedProperty.Add(@string);
							}
						}
						catch (ArgumentOutOfRangeException innerException)
						{
							throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotParseMimeTypes, OrganizationSchema.BlobMimeTypes, blob), innerException);
						}
					}
					num = i + 1;
				}
			}
			return multiValuedProperty;
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x00094054 File Offset: 0x00092254
		private static byte[] BuildMimeTypesBlob(MultiValuedProperty<string> mimeTypes)
		{
			if (mimeTypes == null || mimeTypes.Count == 0)
			{
				return null;
			}
			int num = 0;
			foreach (string text in mimeTypes)
			{
				num += text.Length + 1;
			}
			num++;
			byte[] array = new byte[num];
			using (MemoryStream memoryStream = new MemoryStream(array))
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					foreach (string s in mimeTypes)
					{
						binaryWriter.Write(Encoding.ASCII.GetBytes(s));
						binaryWriter.Write(0);
					}
					binaryWriter.Write(0);
					binaryWriter.Flush();
				}
			}
			return array;
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x00094168 File Offset: 0x00092368
		internal static object ForeignForestFQDNGetter(IPropertyBag propertyBag)
		{
			string text = (string)propertyBag[OrganizationSchema.ForeignForestFQDNRaw];
			MultiValuedProperty<string> multiValuedProperty = new MultiValuedProperty<string>();
			if (string.IsNullOrEmpty(text))
			{
				return multiValuedProperty;
			}
			if (text.IndexOf(',') != -1)
			{
				string[] array = text.Split(new char[]
				{
					','
				}, StringSplitOptions.RemoveEmptyEntries);
				if (array != null && array.Length != 0)
				{
					foreach (string item in array)
					{
						multiValuedProperty.Add(item);
					}
				}
			}
			else
			{
				multiValuedProperty.Add(text);
			}
			return multiValuedProperty;
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x000941F0 File Offset: 0x000923F0
		internal static void ForeignForestFQDNSetter(object value, IPropertyBag propertyBag)
		{
			string value2 = null;
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)value;
			if (multiValuedProperty != null && multiValuedProperty.Count != 0)
			{
				StringBuilder stringBuilder = new StringBuilder(64);
				foreach (string value3 in multiValuedProperty)
				{
					stringBuilder.Append(value3);
					stringBuilder.Append(',');
				}
				stringBuilder.Length--;
				value2 = stringBuilder.ToString();
			}
			propertyBag[OrganizationSchema.ForeignForestFQDNRaw] = value2;
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x00094288 File Offset: 0x00092488
		internal static object DistributionGroupNamingPolicyGetter(IPropertyBag propertyBag)
		{
			string namingPolicy = (string)propertyBag[OrganizationSchema.DistributionGroupNamingPolicyRaw];
			object result;
			try
			{
				result = DistributionGroupNamingPolicy.Parse(namingPolicy);
			}
			catch (FormatException ex)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty(OrganizationSchema.DistributionGroupNamingPolicy.Name, ex.Message), ADUserSchema.CertificateSubject, propertyBag[OrganizationSchema.DistributionGroupNamingPolicyRaw]), ex);
			}
			return result;
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x000942F4 File Offset: 0x000924F4
		internal static void DistributionGroupNamingPolicySetter(object value, IPropertyBag propertyBag)
		{
			string value2 = null;
			if (value != null)
			{
				value2 = ((DistributionGroupNamingPolicy)value).ToString();
			}
			propertyBag[OrganizationSchema.DistributionGroupNamingPolicyRaw] = value2;
		}

		// Token: 0x06002159 RID: 8537 RVA: 0x0009431E File Offset: 0x0009251E
		internal static object OriginatedInDifferentForestGetter(IPropertyBag propertyBag)
		{
			return propertyBag[ADObjectSchema.CorrelationIdRaw] != null && (Guid)propertyBag[ADObjectSchema.CorrelationIdRaw] != Guid.Empty;
		}

		// Token: 0x0600215A RID: 8538 RVA: 0x0009434F File Offset: 0x0009254F
		internal static object ShowAdminAccessWarningGetter(IPropertyBag propertyBag)
		{
			return !(bool)propertyBag[OrganizationSchema.HideAdminAccessWarning] && !Organization.IsLegacyIOwnServicePlan((string)propertyBag[ExchangeConfigurationUnitSchema.ServicePlan]);
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x00094384 File Offset: 0x00092584
		internal static MultiValuedProperty<UMLanguage> UMAvailableLanguagesGetter(IPropertyBag propertyBag)
		{
			MultiValuedProperty<int> multiValuedProperty = (MultiValuedProperty<int>)propertyBag[OrganizationSchema.UMAvailableLanguagesRaw];
			MultiValuedProperty<UMLanguage> multiValuedProperty2 = new MultiValuedProperty<UMLanguage>();
			if (multiValuedProperty != null)
			{
				foreach (int lcid in multiValuedProperty)
				{
					try
					{
						UMLanguage item = new UMLanguage(lcid);
						multiValuedProperty2.Add(item);
					}
					catch (ArgumentException)
					{
					}
				}
			}
			return multiValuedProperty2;
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x00094408 File Offset: 0x00092608
		internal static void UMAvailableLanguagesSetter(object value, IPropertyBag propertyBag)
		{
			MultiValuedProperty<UMLanguage> multiValuedProperty = value as MultiValuedProperty<UMLanguage>;
			if (multiValuedProperty != null)
			{
				MultiValuedProperty<int> multiValuedProperty2 = new MultiValuedProperty<int>();
				foreach (UMLanguage umlanguage in multiValuedProperty)
				{
					multiValuedProperty2.Add(umlanguage.LCID);
				}
				propertyBag[OrganizationSchema.UMAvailableLanguagesRaw] = multiValuedProperty2;
				return;
			}
			propertyBag[OrganizationSchema.UMAvailableLanguagesRaw] = null;
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x00094484 File Offset: 0x00092684
		internal static QueryFilter EnableAsSharedConfigurationFilterBuilder(SinglePropertyFilter filter)
		{
			return ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(OrganizationSchema.ProvisioningFlags, 1UL));
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x00094498 File Offset: 0x00092698
		internal static QueryFilter ImmutableConfigurationFilterBuilder(SinglePropertyFilter filter)
		{
			return ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(OrganizationSchema.ProvisioningFlags, 1024UL));
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x000944B0 File Offset: 0x000926B0
		internal static QueryFilter HostingDeploymentEnabledFilterBuilder(SinglePropertyFilter filter)
		{
			return ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(OrganizationSchema.ProvisioningFlags, 32UL));
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x000944C5 File Offset: 0x000926C5
		internal static QueryFilter LicensingEnforcedFilterBuilder(SinglePropertyFilter filter)
		{
			return ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(OrganizationSchema.ProvisioningFlags, 2UL));
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x000944D9 File Offset: 0x000926D9
		internal static QueryFilter IsTenantAccessBlockedFilterBuilder(SinglePropertyFilter filter)
		{
			return ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(OrganizationSchema.ProvisioningFlags, 4194304UL));
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x000944F1 File Offset: 0x000926F1
		internal static QueryFilter UseServicePlanAsCounterInstanceNameFilterBuilder(SinglePropertyFilter filter)
		{
			return ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(OrganizationSchema.ProvisioningFlags, 64UL));
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x00094508 File Offset: 0x00092708
		internal static object AdminDisplayVersionGetter(IPropertyBag propertyBag)
		{
			int num = (int)propertyBag[OrganizationSchema.ObjectVersion];
			int num2 = (int)OrganizationSchema.BuildMajor.GetterDelegate(propertyBag);
			int num3 = (int)OrganizationSchema.BuildMinor.GetterDelegate(propertyBag);
			byte majorBuild = 0;
			byte minorBuild = 0;
			if (num >= 15000)
			{
				majorBuild = 15;
			}
			else if (num >= 12000)
			{
				majorBuild = ExchangeObjectVersion.Exchange2010.ExchangeBuild.Major;
				if (num >= 14700)
				{
					minorBuild = 17;
				}
				else if (num >= 14000)
				{
					minorBuild = 16;
				}
				else if (num >= 13000)
				{
					minorBuild = 15;
				}
			}
			return new ExchangeObjectVersion(ExchangeObjectVersion.Current.Major, ExchangeObjectVersion.Current.Minor, majorBuild, minorBuild, (ushort)num2, (ushort)num3);
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x000945C4 File Offset: 0x000927C4
		internal void SetBuildVersion(int major, int minor)
		{
			this[OrganizationSchema.BuildMajor] = major;
			this[OrganizationSchema.BuildMinor] = minor;
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x000945E8 File Offset: 0x000927E8
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (this.ResourceAddressLists.Count > 1)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorOrganizationResourceAddressListsCount, this.Identity, base.OriginatingServer));
			}
			if (this.SupportedSharedConfigurations.Count != 0 && this.SharedConfigurationInfo != null)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorSharedConfigurationBothRoles, this.Identity, base.OriginatingServer));
			}
			if (this.EnableAsSharedConfiguration)
			{
				if (this.SupportedSharedConfigurations.Count != 0)
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.ErrorSharedConfigurationCannotBeEnabled, this.Identity, base.OriginatingServer));
				}
				if (this.SharedConfigurationInfo == null)
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.ErrorNoSharedConfigurationInfo, this.Identity, base.OriginatingServer));
				}
			}
			if (this.propertyBag.IsModified(OrganizationSchema.AdfsAuthenticationRawConfiguration))
			{
				AdfsAuthenticationConfig adfsAuthenticationConfig = Organization.GetAdfsAuthenticationConfig(this.propertyBag);
				adfsAuthenticationConfig.Validate(errors);
			}
			if (this.TenantRelocationsAllowed && !base.OrganizationId.Equals(OrganizationId.ForestWideOrgId))
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorTenantRelocationsAllowedOnlyForRootOrg, this.Identity, base.OriginatingServer));
			}
			errors.AddRange(Database.ValidateAscendingQuotas(this.propertyBag, new ProviderPropertyDefinition[]
			{
				OrganizationSchema.DefaultPublicFolderIssueWarningQuota,
				OrganizationSchema.DefaultPublicFolderProhibitPostQuota
			}, this.Identity));
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x00094742 File Offset: 0x00092942
		internal override void StampPersistableDefaultValues()
		{
			if (!base.IsModified(OrganizationSchema.MailTipsLargeAudienceThreshold))
			{
				this.MailTipsLargeAudienceThreshold = 25U;
			}
			if (!base.IsModified(OrganizationSchema.UMAvailableLanguages))
			{
				this.UMAvailableLanguages = new MultiValuedProperty<UMLanguage>(UMLanguage.DefaultLanguage);
			}
			base.StampPersistableDefaultValues();
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x0009477C File Offset: 0x0009297C
		private static bool IsLegacyIOwnServicePlan(string servicePlanName)
		{
			if (string.IsNullOrEmpty(servicePlanName))
			{
				return false;
			}
			foreach (string value in Organization.r3iOwnServicePlans)
			{
				if (servicePlanName.Equals(value, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06002168 RID: 8552 RVA: 0x000947BC File Offset: 0x000929BC
		// (set) Token: 0x06002169 RID: 8553 RVA: 0x000947CE File Offset: 0x000929CE
		public virtual bool IsGuidPrefixedLegacyDnDisabled
		{
			get
			{
				return (bool)this[OrganizationSchema.IsGuidPrefixedLegacyDnDisabled];
			}
			set
			{
				this[OrganizationSchema.IsGuidPrefixedLegacyDnDisabled] = value;
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x0600216A RID: 8554 RVA: 0x000947E1 File Offset: 0x000929E1
		// (set) Token: 0x0600216B RID: 8555 RVA: 0x000947F3 File Offset: 0x000929F3
		public virtual bool IsMailboxForcedReplicationDisabled
		{
			get
			{
				return (bool)this[OrganizationSchema.IsMailboxForcedReplicationDisabled];
			}
			set
			{
				this[OrganizationSchema.IsMailboxForcedReplicationDisabled] = value;
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x0600216C RID: 8556 RVA: 0x00094806 File Offset: 0x00092A06
		// (set) Token: 0x0600216D RID: 8557 RVA: 0x00094818 File Offset: 0x00092A18
		public virtual bool IsSyncPropertySetUpgradeAllowed
		{
			get
			{
				return (bool)this[OrganizationSchema.IsSyncPropertySetUpgradeAllowed];
			}
			set
			{
				this[OrganizationSchema.IsSyncPropertySetUpgradeAllowed] = value;
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x0600216E RID: 8558 RVA: 0x0009482B File Offset: 0x00092A2B
		// (set) Token: 0x0600216F RID: 8559 RVA: 0x0009483D File Offset: 0x00092A3D
		public MultiValuedProperty<ADObjectId> SupportedSharedConfigurations
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[OrganizationSchema.SupportedSharedConfigurations];
			}
			internal set
			{
				this[OrganizationSchema.SupportedSharedConfigurations] = value;
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06002170 RID: 8560 RVA: 0x0009484B File Offset: 0x00092A4B
		// (set) Token: 0x06002171 RID: 8561 RVA: 0x0009485D File Offset: 0x00092A5D
		public SharedConfigurationInfo SharedConfigurationInfo
		{
			get
			{
				return (SharedConfigurationInfo)this[OrganizationSchema.SharedConfigurationInfo];
			}
			internal set
			{
				this[OrganizationSchema.SharedConfigurationInfo] = value;
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06002172 RID: 8562 RVA: 0x0009486B File Offset: 0x00092A6B
		// (set) Token: 0x06002173 RID: 8563 RVA: 0x0009487D File Offset: 0x00092A7D
		public bool EnableAsSharedConfiguration
		{
			get
			{
				return (bool)this[OrganizationSchema.EnableAsSharedConfiguration];
			}
			internal set
			{
				this[OrganizationSchema.EnableAsSharedConfiguration] = value;
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06002174 RID: 8564 RVA: 0x00094890 File Offset: 0x00092A90
		// (set) Token: 0x06002175 RID: 8565 RVA: 0x000948A2 File Offset: 0x00092AA2
		public bool ImmutableConfiguration
		{
			get
			{
				return (bool)this[OrganizationSchema.ImmutableConfiguration];
			}
			internal set
			{
				this[OrganizationSchema.ImmutableConfiguration] = value;
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06002176 RID: 8566 RVA: 0x000948B5 File Offset: 0x00092AB5
		// (set) Token: 0x06002177 RID: 8567 RVA: 0x000948C7 File Offset: 0x00092AC7
		public bool IsLicensingEnforced
		{
			get
			{
				return (bool)this[OrganizationSchema.IsLicensingEnforced];
			}
			internal set
			{
				this[OrganizationSchema.IsLicensingEnforced] = value;
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06002178 RID: 8568 RVA: 0x000948DA File Offset: 0x00092ADA
		// (set) Token: 0x06002179 RID: 8569 RVA: 0x000948EC File Offset: 0x00092AEC
		public bool IsTenantAccessBlocked
		{
			get
			{
				return (bool)this[OrganizationSchema.IsTenantAccessBlocked];
			}
			internal set
			{
				this[OrganizationSchema.IsTenantAccessBlocked] = value;
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x0600217A RID: 8570 RVA: 0x000948FF File Offset: 0x00092AFF
		// (set) Token: 0x0600217B RID: 8571 RVA: 0x00094911 File Offset: 0x00092B11
		public bool IsDehydrated
		{
			get
			{
				return (bool)this[OrganizationSchema.IsDehydrated];
			}
			internal set
			{
				this[OrganizationSchema.IsDehydrated] = value;
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x0600217C RID: 8572 RVA: 0x00094924 File Offset: 0x00092B24
		// (set) Token: 0x0600217D RID: 8573 RVA: 0x00094936 File Offset: 0x00092B36
		public bool UseServicePlanAsCounterInstanceName
		{
			get
			{
				return (bool)this[OrganizationSchema.UseServicePlanAsCounterInstanceName];
			}
			set
			{
				this[OrganizationSchema.UseServicePlanAsCounterInstanceName] = value;
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x0600217E RID: 8574 RVA: 0x00094949 File Offset: 0x00092B49
		// (set) Token: 0x0600217F RID: 8575 RVA: 0x0009495B File Offset: 0x00092B5B
		public ExchangeObjectVersion RBACConfigurationVersion
		{
			get
			{
				return (ExchangeObjectVersion)this[OrganizationSchema.RBACConfigurationVersion];
			}
			internal set
			{
				this[OrganizationSchema.RBACConfigurationVersion] = value;
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06002180 RID: 8576 RVA: 0x00094969 File Offset: 0x00092B69
		// (set) Token: 0x06002181 RID: 8577 RVA: 0x0009497B File Offset: 0x00092B7B
		public virtual bool CalendarVersionStoreEnabled
		{
			get
			{
				return (bool)this[OrganizationSchema.CalendarVersionStoreEnabled];
			}
			set
			{
				this[OrganizationSchema.CalendarVersionStoreEnabled] = value;
			}
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06002182 RID: 8578 RVA: 0x0009498E File Offset: 0x00092B8E
		// (set) Token: 0x06002183 RID: 8579 RVA: 0x000949A0 File Offset: 0x00092BA0
		public virtual PublicFolderInformation DefaultPublicFolderMailbox
		{
			get
			{
				return (PublicFolderInformation)this[OrganizationSchema.DefaultPublicFolderMailbox];
			}
			set
			{
				this[OrganizationSchema.DefaultPublicFolderMailbox] = value;
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06002184 RID: 8580 RVA: 0x000949AE File Offset: 0x00092BAE
		// (set) Token: 0x06002185 RID: 8581 RVA: 0x000949C0 File Offset: 0x00092BC0
		public MultiValuedProperty<ADObjectId> RemotePublicFolderMailboxes
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[OrganizationSchema.RemotePublicFolderMailboxes];
			}
			set
			{
				this[OrganizationSchema.RemotePublicFolderMailboxes] = value;
			}
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06002186 RID: 8582 RVA: 0x000949CE File Offset: 0x00092BCE
		// (set) Token: 0x06002187 RID: 8583 RVA: 0x000949E0 File Offset: 0x00092BE0
		public virtual PublicFoldersDeployment PublicFoldersEnabled
		{
			get
			{
				return (PublicFoldersDeployment)this[OrganizationSchema.PublicFoldersEnabled];
			}
			set
			{
				this[OrganizationSchema.PublicFoldersEnabled] = value;
			}
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06002188 RID: 8584 RVA: 0x000949F3 File Offset: 0x00092BF3
		public ForestModeFlags ForestMode
		{
			get
			{
				return (ForestModeFlags)this[OrganizationSchema.ForestMode];
			}
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06002189 RID: 8585 RVA: 0x00094A05 File Offset: 0x00092C05
		// (set) Token: 0x0600218A RID: 8586 RVA: 0x00094A17 File Offset: 0x00092C17
		public virtual MultiValuedProperty<UMLanguage> UMAvailableLanguages
		{
			get
			{
				return (MultiValuedProperty<UMLanguage>)this[OrganizationSchema.UMAvailableLanguages];
			}
			set
			{
				this[OrganizationSchema.UMAvailableLanguages] = value;
			}
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x00094A28 File Offset: 0x00092C28
		private static AdfsAuthenticationConfig GetAdfsAuthenticationConfig(IPropertyBag propertyBag)
		{
			string encodedRawConfig = (string)propertyBag[OrganizationSchema.AdfsAuthenticationRawConfiguration];
			return new AdfsAuthenticationConfig(encodedRawConfig);
		}

		// Token: 0x0600218C RID: 8588 RVA: 0x00094A4E File Offset: 0x00092C4E
		private static void SetAdfsAuthenticationConfig(IPropertyBag propertyBag, AdfsAuthenticationConfig obj)
		{
			propertyBag[OrganizationSchema.AdfsAuthenticationRawConfiguration] = obj.EncodedConfig;
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x0600218D RID: 8589 RVA: 0x00094A64 File Offset: 0x00092C64
		// (set) Token: 0x0600218E RID: 8590 RVA: 0x00094A8C File Offset: 0x00092C8C
		public virtual string AdfsAuthenticationConfiguration
		{
			get
			{
				string result = null;
				AdfsAuthenticationConfig.TryDecode((string)this[OrganizationSchema.AdfsAuthenticationRawConfiguration], out result);
				return result;
			}
			set
			{
				this[OrganizationSchema.AdfsAuthenticationRawConfiguration] = AdfsAuthenticationConfig.Encode(value);
			}
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x0600218F RID: 8591 RVA: 0x00094A9F File Offset: 0x00092C9F
		// (set) Token: 0x06002190 RID: 8592 RVA: 0x00094AB1 File Offset: 0x00092CB1
		public virtual Uri AdfsIssuer
		{
			get
			{
				return (Uri)this[OrganizationSchema.AdfsIssuer];
			}
			set
			{
				this[OrganizationSchema.AdfsIssuer] = value;
			}
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x00094ABF File Offset: 0x00092CBF
		internal static object AdfsIssuerGetter(IPropertyBag propertyBag)
		{
			return Organization.GetAdfsAuthenticationConfig(propertyBag).Issuer;
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x00094ACC File Offset: 0x00092CCC
		internal static void AdfsIssuerSetter(object value, IPropertyBag propertyBag)
		{
			AdfsAuthenticationConfig adfsAuthenticationConfig = Organization.GetAdfsAuthenticationConfig(propertyBag);
			adfsAuthenticationConfig.Issuer = (value as Uri);
			Organization.SetAdfsAuthenticationConfig(propertyBag, adfsAuthenticationConfig);
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06002193 RID: 8595 RVA: 0x00094AF3 File Offset: 0x00092CF3
		// (set) Token: 0x06002194 RID: 8596 RVA: 0x00094B05 File Offset: 0x00092D05
		public virtual MultiValuedProperty<Uri> AdfsAudienceUris
		{
			get
			{
				return (MultiValuedProperty<Uri>)this[OrganizationSchema.AdfsAudienceUris];
			}
			set
			{
				this[OrganizationSchema.AdfsAudienceUris] = value;
			}
		}

		// Token: 0x06002195 RID: 8597 RVA: 0x00094B13 File Offset: 0x00092D13
		internal static object AdfsAudienceUrisGetter(IPropertyBag propertyBag)
		{
			return Organization.GetAdfsAuthenticationConfig(propertyBag).AudienceUris;
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x00094B20 File Offset: 0x00092D20
		internal static void AdfsAudienceUrisSetter(object value, IPropertyBag propertyBag)
		{
			AdfsAuthenticationConfig adfsAuthenticationConfig = Organization.GetAdfsAuthenticationConfig(propertyBag);
			adfsAuthenticationConfig.AudienceUris = (value as MultiValuedProperty<Uri>);
			Organization.SetAdfsAuthenticationConfig(propertyBag, adfsAuthenticationConfig);
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06002197 RID: 8599 RVA: 0x00094B47 File Offset: 0x00092D47
		// (set) Token: 0x06002198 RID: 8600 RVA: 0x00094B59 File Offset: 0x00092D59
		public virtual MultiValuedProperty<string> AdfsSignCertificateThumbprints
		{
			get
			{
				return (MultiValuedProperty<string>)this[OrganizationSchema.AdfsSignCertificateThumbprints];
			}
			set
			{
				this[OrganizationSchema.AdfsSignCertificateThumbprints] = value;
			}
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x00094B67 File Offset: 0x00092D67
		internal static object AdfsSignCertificateThumbprintsGetter(IPropertyBag propertyBag)
		{
			return Organization.GetAdfsAuthenticationConfig(propertyBag).SignCertificateThumbprints;
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x00094B74 File Offset: 0x00092D74
		internal static void AdfsSignCertificateThumbprintsSetter(object value, IPropertyBag propertyBag)
		{
			AdfsAuthenticationConfig adfsAuthenticationConfig = Organization.GetAdfsAuthenticationConfig(propertyBag);
			adfsAuthenticationConfig.SignCertificateThumbprints = (value as MultiValuedProperty<string>);
			Organization.SetAdfsAuthenticationConfig(propertyBag, adfsAuthenticationConfig);
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x0600219B RID: 8603 RVA: 0x00094B9B File Offset: 0x00092D9B
		// (set) Token: 0x0600219C RID: 8604 RVA: 0x00094BAD File Offset: 0x00092DAD
		public virtual string AdfsEncryptCertificateThumbprint
		{
			get
			{
				return (string)this[OrganizationSchema.AdfsEncryptCertificateThumbprint];
			}
			set
			{
				this[OrganizationSchema.AdfsEncryptCertificateThumbprint] = value;
			}
		}

		// Token: 0x0600219D RID: 8605 RVA: 0x00094BBB File Offset: 0x00092DBB
		internal static object AdfsEncryptCertificateThumbprintGetter(IPropertyBag propertyBag)
		{
			return Organization.GetAdfsAuthenticationConfig(propertyBag).EncryptCertificateThumbprint;
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x00094BC8 File Offset: 0x00092DC8
		internal static void AdfsEncryptCertificateThumbprintSetter(object value, IPropertyBag propertyBag)
		{
			AdfsAuthenticationConfig adfsAuthenticationConfig = Organization.GetAdfsAuthenticationConfig(propertyBag);
			adfsAuthenticationConfig.EncryptCertificateThumbprint = (value as string);
			Organization.SetAdfsAuthenticationConfig(propertyBag, adfsAuthenticationConfig);
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x0600219F RID: 8607 RVA: 0x00094BEF File Offset: 0x00092DEF
		// (set) Token: 0x060021A0 RID: 8608 RVA: 0x00094C01 File Offset: 0x00092E01
		public virtual Uri SiteMailboxCreationURL
		{
			get
			{
				return (Uri)this[OrganizationSchema.SiteMailboxCreationURL];
			}
			set
			{
				this[OrganizationSchema.SiteMailboxCreationURL] = value;
			}
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x00094C10 File Offset: 0x00092E10
		internal static object PublicFoldersEnabledGetter(IPropertyBag propertyBag)
		{
			if ((bool)OrganizationSchema.HeuristicsFlagsGetter(HeuristicsFlags.PublicFoldersDisabled, OrganizationSchema.Heuristics, propertyBag))
			{
				return PublicFoldersDeployment.None;
			}
			if ((bool)OrganizationSchema.HeuristicsFlagsGetter(HeuristicsFlags.RemotePublicFolders, OrganizationSchema.Heuristics, propertyBag))
			{
				return PublicFoldersDeployment.Remote;
			}
			return PublicFoldersDeployment.Local;
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x00094C60 File Offset: 0x00092E60
		internal static void PublicFoldersEnabledSetter(object value, IPropertyBag propertyBag)
		{
			PublicFoldersDeployment publicFoldersDeployment = (PublicFoldersDeployment)value;
			if (publicFoldersDeployment == PublicFoldersDeployment.Local)
			{
				OrganizationSchema.HeuristicsFlagsSetter(HeuristicsFlags.PublicFoldersDisabled, OrganizationSchema.Heuristics, false, propertyBag);
				OrganizationSchema.HeuristicsFlagsSetter(HeuristicsFlags.RemotePublicFolders, OrganizationSchema.Heuristics, false, propertyBag);
				return;
			}
			if (publicFoldersDeployment == PublicFoldersDeployment.Remote)
			{
				OrganizationSchema.HeuristicsFlagsSetter(HeuristicsFlags.PublicFoldersDisabled, OrganizationSchema.Heuristics, false, propertyBag);
				OrganizationSchema.HeuristicsFlagsSetter(HeuristicsFlags.RemotePublicFolders, OrganizationSchema.Heuristics, true, propertyBag);
				return;
			}
			OrganizationSchema.HeuristicsFlagsSetter(HeuristicsFlags.PublicFoldersDisabled, OrganizationSchema.Heuristics, true, propertyBag);
			OrganizationSchema.HeuristicsFlagsSetter(HeuristicsFlags.RemotePublicFolders, OrganizationSchema.Heuristics, false, propertyBag);
		}

		// Token: 0x060021A3 RID: 8611 RVA: 0x00094D04 File Offset: 0x00092F04
		internal static object SiteMailboxCreationURLGetter(IPropertyBag propertyBag)
		{
			Uri result = null;
			string serviceEndPoints = (string)propertyBag[OrganizationSchema.ServiceEndpoints];
			Dictionary<string, string> dictionary = Organization.ParseServiceEndpointsAttribute(serviceEndPoints);
			if (dictionary.ContainsKey(OrganizationSchema.SiteMailboxCreationURL.Name))
			{
				result = new Uri(dictionary[OrganizationSchema.SiteMailboxCreationURL.Name]);
			}
			return result;
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x00094D54 File Offset: 0x00092F54
		internal static void SiteMailboxCreationURLSetter(object value, IPropertyBag propertyBag)
		{
			string serviceEndPoints = (string)propertyBag[OrganizationSchema.ServiceEndpoints];
			Dictionary<string, string> dictionary = Organization.ParseServiceEndpointsAttribute(serviceEndPoints);
			dictionary[OrganizationSchema.SiteMailboxCreationURL.Name] = ((value == null) ? string.Empty : ((Uri)value).AbsoluteUri);
			propertyBag[OrganizationSchema.ServiceEndpoints] = Organization.FormatServiceEndpointsAttribute(dictionary);
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x00094DB0 File Offset: 0x00092FB0
		internal static Dictionary<string, string> ParseServiceEndpointsAttribute(string serviceEndPoints)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (!string.IsNullOrWhiteSpace(serviceEndPoints))
			{
				string[] array = serviceEndPoints.Split(new char[]
				{
					Organization.serviceEndpointSeparator
				});
				string key = string.Empty;
				for (int i = 0; i < array.Length; i++)
				{
					if (i % 2 == 0)
					{
						key = array[i];
					}
					else
					{
						string text = array[i];
						if (!dictionary.ContainsKey(key) && Uri.IsWellFormedUriString(text, UriKind.Absolute))
						{
							dictionary.Add(key, text);
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x00094E28 File Offset: 0x00093028
		private static string FormatServiceEndpointsAttribute(Dictionary<string, string> serviceEndpointCollection)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (serviceEndpointCollection != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in serviceEndpointCollection)
				{
					if (!string.IsNullOrWhiteSpace(keyValuePair.Value))
					{
						stringBuilder.AppendFormat("{1}{0}{2}{0}", Organization.serviceEndpointSeparator, keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x060021A7 RID: 8615 RVA: 0x00094EB0 File Offset: 0x000930B0
		// (set) Token: 0x060021A8 RID: 8616 RVA: 0x00094EB8 File Offset: 0x000930B8
		public int MaxAddressBookMigrations { get; set; }

		// Token: 0x0400153A RID: 5434
		public const int DefaultSCLJunkThreshold = 4;

		// Token: 0x0400153B RID: 5435
		private static OrganizationSchema schema = ObjectSchema.GetInstance<OrganizationSchema>();

		// Token: 0x0400153C RID: 5436
		internal static readonly string MostDerivedClass = "msExchOrganizationContainer";

		// Token: 0x0400153D RID: 5437
		private static string[] r3iOwnServicePlans = new string[]
		{
			"EDU_I_Own_E14_R3",
			"SegmentedGalEDU_I_Own_E14_R3"
		};

		// Token: 0x0400153E RID: 5438
		private static char serviceEndpointSeparator = ';';

		// Token: 0x0400153F RID: 5439
		private bool ewsAllowListSpecified;

		// Token: 0x04001540 RID: 5440
		private bool ewsBlockListSpecified;

		// Token: 0x04001541 RID: 5441
		internal static readonly ExchangeObjectVersion CurrentExchangeRootOrgVersion = new ExchangeObjectVersion(2, 0, ExchangeObjectVersion.Exchange2012.ExchangeBuild);
	}
}
