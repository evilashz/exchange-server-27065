using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using Microsoft.Exchange.Data.Directory.EventLog;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x020001EF RID: 495
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class ADDynamicGroup : ADRecipient, IADDistributionList
	{
		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x0600191D RID: 6429 RVA: 0x0006B712 File Offset: 0x00069912
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADDynamicGroup.schema;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x0600191E RID: 6430 RVA: 0x0006B719 File Offset: 0x00069919
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADDynamicGroup.MostDerivedClass;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x0600191F RID: 6431 RVA: 0x0006B720 File Offset: 0x00069920
		public override string ObjectCategoryCN
		{
			get
			{
				return ADDynamicGroup.ObjectCategoryCNInternal;
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001920 RID: 6432 RVA: 0x0006B727 File Offset: 0x00069927
		internal override string ObjectCategoryName
		{
			get
			{
				return ADDynamicGroup.ObjectCategoryNameInternal;
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001921 RID: 6433 RVA: 0x0006B72E File Offset: 0x0006992E
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, this.ObjectCategoryName);
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001922 RID: 6434 RVA: 0x0006B741 File Offset: 0x00069941
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001923 RID: 6435 RVA: 0x0006B748 File Offset: 0x00069948
		internal override bool ExchangeVersionUpgradeSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x0006B74B File Offset: 0x0006994B
		internal ADDynamicGroup(IRecipientSession session, PropertyBag propertyBag) : base(session, propertyBag)
		{
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x0006B755 File Offset: 0x00069955
		internal ADDynamicGroup(IRecipientSession session, string commonName, ADObjectId containerId)
		{
			this.m_Session = session;
			base.SetId(containerId.GetChildId("CN", commonName));
			base.SetObjectClass(this.MostDerivedObjectClass);
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x0006B782 File Offset: 0x00069982
		public ADDynamicGroup()
		{
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001927 RID: 6439 RVA: 0x0006B78A File Offset: 0x0006998A
		// (set) Token: 0x06001928 RID: 6440 RVA: 0x0006B79C File Offset: 0x0006999C
		public ADObjectId RecipientContainer
		{
			get
			{
				return (ADObjectId)this[ADDynamicGroupSchema.RecipientContainer];
			}
			internal set
			{
				this[ADDynamicGroupSchema.RecipientContainer] = value;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001929 RID: 6441 RVA: 0x0006B7AA File Offset: 0x000699AA
		// (set) Token: 0x0600192A RID: 6442 RVA: 0x0006B7BC File Offset: 0x000699BC
		public string LdapRecipientFilter
		{
			get
			{
				return (string)this[ADDynamicGroupSchema.LdapRecipientFilter];
			}
			internal set
			{
				this[ADDynamicGroupSchema.LdapRecipientFilter] = value;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x0600192B RID: 6443 RVA: 0x0006B7CA File Offset: 0x000699CA
		// (set) Token: 0x0600192C RID: 6444 RVA: 0x0006B7DC File Offset: 0x000699DC
		public string RecipientFilter
		{
			get
			{
				return (string)this[ADDynamicGroupSchema.RecipientFilter];
			}
			internal set
			{
				this[ADDynamicGroupSchema.RecipientFilter] = value;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x0600192D RID: 6445 RVA: 0x0006B7EA File Offset: 0x000699EA
		// (set) Token: 0x0600192E RID: 6446 RVA: 0x0006B7FC File Offset: 0x000699FC
		public WellKnownRecipientType? IncludedRecipients
		{
			get
			{
				return (WellKnownRecipientType?)this[ADDynamicGroupSchema.IncludedRecipients];
			}
			internal set
			{
				this[ADDynamicGroupSchema.IncludedRecipients] = value;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x0600192F RID: 6447 RVA: 0x0006B80F File Offset: 0x00069A0F
		// (set) Token: 0x06001930 RID: 6448 RVA: 0x0006B821 File Offset: 0x00069A21
		public MultiValuedProperty<string> ConditionalDepartment
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADDynamicGroupSchema.ConditionalDepartment];
			}
			internal set
			{
				this[ADDynamicGroupSchema.ConditionalDepartment] = value;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001931 RID: 6449 RVA: 0x0006B82F File Offset: 0x00069A2F
		// (set) Token: 0x06001932 RID: 6450 RVA: 0x0006B841 File Offset: 0x00069A41
		public MultiValuedProperty<string> ConditionalCompany
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADDynamicGroupSchema.ConditionalCompany];
			}
			internal set
			{
				this[ADDynamicGroupSchema.ConditionalCompany] = value;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001933 RID: 6451 RVA: 0x0006B84F File Offset: 0x00069A4F
		// (set) Token: 0x06001934 RID: 6452 RVA: 0x0006B861 File Offset: 0x00069A61
		public MultiValuedProperty<string> ConditionalStateOrProvince
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADDynamicGroupSchema.ConditionalStateOrProvince];
			}
			internal set
			{
				this[ADDynamicGroupSchema.ConditionalStateOrProvince] = value;
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06001935 RID: 6453 RVA: 0x0006B86F File Offset: 0x00069A6F
		// (set) Token: 0x06001936 RID: 6454 RVA: 0x0006B881 File Offset: 0x00069A81
		public MultiValuedProperty<string> ConditionalCustomAttribute1
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADDynamicGroupSchema.ConditionalCustomAttribute1];
			}
			internal set
			{
				this[ADDynamicGroupSchema.ConditionalCustomAttribute1] = value;
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001937 RID: 6455 RVA: 0x0006B88F File Offset: 0x00069A8F
		// (set) Token: 0x06001938 RID: 6456 RVA: 0x0006B8A1 File Offset: 0x00069AA1
		public MultiValuedProperty<string> ConditionalCustomAttribute2
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADDynamicGroupSchema.ConditionalCustomAttribute2];
			}
			internal set
			{
				this[ADDynamicGroupSchema.ConditionalCustomAttribute2] = value;
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001939 RID: 6457 RVA: 0x0006B8AF File Offset: 0x00069AAF
		// (set) Token: 0x0600193A RID: 6458 RVA: 0x0006B8C1 File Offset: 0x00069AC1
		public MultiValuedProperty<string> ConditionalCustomAttribute3
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADDynamicGroupSchema.ConditionalCustomAttribute3];
			}
			internal set
			{
				this[ADDynamicGroupSchema.ConditionalCustomAttribute3] = value;
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x0600193B RID: 6459 RVA: 0x0006B8CF File Offset: 0x00069ACF
		// (set) Token: 0x0600193C RID: 6460 RVA: 0x0006B8E1 File Offset: 0x00069AE1
		public MultiValuedProperty<string> ConditionalCustomAttribute4
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADDynamicGroupSchema.ConditionalCustomAttribute4];
			}
			internal set
			{
				this[ADDynamicGroupSchema.ConditionalCustomAttribute4] = value;
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x0600193D RID: 6461 RVA: 0x0006B8EF File Offset: 0x00069AEF
		// (set) Token: 0x0600193E RID: 6462 RVA: 0x0006B901 File Offset: 0x00069B01
		public MultiValuedProperty<string> ConditionalCustomAttribute5
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADDynamicGroupSchema.ConditionalCustomAttribute5];
			}
			internal set
			{
				this[ADDynamicGroupSchema.ConditionalCustomAttribute5] = value;
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x0600193F RID: 6463 RVA: 0x0006B90F File Offset: 0x00069B0F
		// (set) Token: 0x06001940 RID: 6464 RVA: 0x0006B921 File Offset: 0x00069B21
		public MultiValuedProperty<string> ConditionalCustomAttribute6
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADDynamicGroupSchema.ConditionalCustomAttribute6];
			}
			internal set
			{
				this[ADDynamicGroupSchema.ConditionalCustomAttribute6] = value;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001941 RID: 6465 RVA: 0x0006B92F File Offset: 0x00069B2F
		// (set) Token: 0x06001942 RID: 6466 RVA: 0x0006B941 File Offset: 0x00069B41
		public MultiValuedProperty<string> ConditionalCustomAttribute7
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADDynamicGroupSchema.ConditionalCustomAttribute7];
			}
			internal set
			{
				this[ADDynamicGroupSchema.ConditionalCustomAttribute7] = value;
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001943 RID: 6467 RVA: 0x0006B94F File Offset: 0x00069B4F
		// (set) Token: 0x06001944 RID: 6468 RVA: 0x0006B961 File Offset: 0x00069B61
		public MultiValuedProperty<string> ConditionalCustomAttribute8
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADDynamicGroupSchema.ConditionalCustomAttribute8];
			}
			internal set
			{
				this[ADDynamicGroupSchema.ConditionalCustomAttribute8] = value;
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001945 RID: 6469 RVA: 0x0006B96F File Offset: 0x00069B6F
		// (set) Token: 0x06001946 RID: 6470 RVA: 0x0006B981 File Offset: 0x00069B81
		public MultiValuedProperty<string> ConditionalCustomAttribute9
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADDynamicGroupSchema.ConditionalCustomAttribute9];
			}
			internal set
			{
				this[ADDynamicGroupSchema.ConditionalCustomAttribute9] = value;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001947 RID: 6471 RVA: 0x0006B98F File Offset: 0x00069B8F
		// (set) Token: 0x06001948 RID: 6472 RVA: 0x0006B9A1 File Offset: 0x00069BA1
		public MultiValuedProperty<string> ConditionalCustomAttribute10
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADDynamicGroupSchema.ConditionalCustomAttribute10];
			}
			internal set
			{
				this[ADDynamicGroupSchema.ConditionalCustomAttribute10] = value;
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001949 RID: 6473 RVA: 0x0006B9AF File Offset: 0x00069BAF
		// (set) Token: 0x0600194A RID: 6474 RVA: 0x0006B9C1 File Offset: 0x00069BC1
		public MultiValuedProperty<string> ConditionalCustomAttribute11
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADDynamicGroupSchema.ConditionalCustomAttribute11];
			}
			internal set
			{
				this[ADDynamicGroupSchema.ConditionalCustomAttribute11] = value;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x0600194B RID: 6475 RVA: 0x0006B9CF File Offset: 0x00069BCF
		// (set) Token: 0x0600194C RID: 6476 RVA: 0x0006B9E1 File Offset: 0x00069BE1
		public MultiValuedProperty<string> ConditionalCustomAttribute12
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADDynamicGroupSchema.ConditionalCustomAttribute12];
			}
			internal set
			{
				this[ADDynamicGroupSchema.ConditionalCustomAttribute12] = value;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x0600194D RID: 6477 RVA: 0x0006B9EF File Offset: 0x00069BEF
		// (set) Token: 0x0600194E RID: 6478 RVA: 0x0006BA01 File Offset: 0x00069C01
		public MultiValuedProperty<string> ConditionalCustomAttribute13
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADDynamicGroupSchema.ConditionalCustomAttribute13];
			}
			internal set
			{
				this[ADDynamicGroupSchema.ConditionalCustomAttribute13] = value;
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x0600194F RID: 6479 RVA: 0x0006BA0F File Offset: 0x00069C0F
		// (set) Token: 0x06001950 RID: 6480 RVA: 0x0006BA21 File Offset: 0x00069C21
		public MultiValuedProperty<string> ConditionalCustomAttribute14
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADDynamicGroupSchema.ConditionalCustomAttribute14];
			}
			internal set
			{
				this[ADDynamicGroupSchema.ConditionalCustomAttribute14] = value;
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001951 RID: 6481 RVA: 0x0006BA2F File Offset: 0x00069C2F
		// (set) Token: 0x06001952 RID: 6482 RVA: 0x0006BA41 File Offset: 0x00069C41
		public MultiValuedProperty<string> ConditionalCustomAttribute15
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADDynamicGroupSchema.ConditionalCustomAttribute15];
			}
			internal set
			{
				this[ADDynamicGroupSchema.ConditionalCustomAttribute15] = value;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001953 RID: 6483 RVA: 0x0006BA4F File Offset: 0x00069C4F
		public WellKnownRecipientFilterType RecipientFilterType
		{
			get
			{
				return (WellKnownRecipientFilterType)this[ADDynamicGroupSchema.RecipientFilterType];
			}
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x0006BA61 File Offset: 0x00069C61
		internal static QueryFilter IncludeRecipientFilterBuilder(SinglePropertyFilter filter)
		{
			return ADDynamicGroup.PrecannedRecipientFilterFilterBuilder(filter, ADDynamicGroupSchema.RecipientFilterMetadata, "Microsoft.Exchange12.8f91d340bc0c47e4b4058a479602f94c:IncludedRecipients=");
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x0006BA74 File Offset: 0x00069C74
		internal void SetRecipientFilter(QueryFilter filter)
		{
			if (filter == null)
			{
				this[ADDynamicGroupSchema.RecipientFilter] = string.Empty;
				this[ADDynamicGroupSchema.LdapRecipientFilter] = string.Empty;
			}
			else
			{
				QueryFilter queryFilter = new AndFilter(new QueryFilter[]
				{
					filter,
					RecipientFilterHelper.ExcludingSystemMailboxFilter,
					RecipientFilterHelper.ExcludingCasMailboxFilter,
					RecipientFilterHelper.ExcludingMailboxPlanFilter,
					RecipientFilterHelper.ExcludingDiscoveryMailboxFilter,
					RecipientFilterHelper.ExcludingPublicFolderMailboxFilter,
					RecipientFilterHelper.ExcludingArbitrationMailboxFilter,
					RecipientFilterHelper.ExcludingAuditLogMailboxFilter
				});
				this[ADDynamicGroupSchema.RecipientFilter] = queryFilter.GenerateInfixString(FilterLanguage.Monad);
				this[ADDynamicGroupSchema.LdapRecipientFilter] = LdapFilterBuilder.LdapFilterFromQueryFilter(queryFilter);
			}
			RecipientFilterHelper.SetRecipientFilterType(WellKnownRecipientFilterType.Custom, this.propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata);
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x0006BB24 File Offset: 0x00069D24
		private static QueryFilter PrecannedRecipientFilterFilterBuilder(SinglePropertyFilter filter, ADPropertyDefinition filterMetadata, string attributePrefix)
		{
			object obj = ADObject.PropertyValueFromEqualityFilter(filter);
			ComparisonFilter comparisonFilter = (ComparisonFilter)filter;
			if (obj == null)
			{
				return new NotFilter(new TextFilter(filterMetadata, attributePrefix, MatchOptions.Prefix, MatchFlags.IgnoreCase));
			}
			return new ComparisonFilter(comparisonFilter.ComparisonOperator, filterMetadata, attributePrefix + obj.ToString());
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001957 RID: 6487 RVA: 0x0006BB69 File Offset: 0x00069D69
		// (set) Token: 0x06001958 RID: 6488 RVA: 0x0006BB7B File Offset: 0x00069D7B
		public int GroupMemberCount
		{
			get
			{
				return (int)this[ADGroupSchema.GroupMemberCount];
			}
			set
			{
				this[ADGroupSchema.GroupMemberCount] = value;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001959 RID: 6489 RVA: 0x0006BB8E File Offset: 0x00069D8E
		// (set) Token: 0x0600195A RID: 6490 RVA: 0x0006BBA0 File Offset: 0x00069DA0
		public int GroupExternalMemberCount
		{
			get
			{
				return (int)this[ADGroupSchema.GroupExternalMemberCount];
			}
			set
			{
				this[ADGroupSchema.GroupExternalMemberCount] = value;
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x0600195B RID: 6491 RVA: 0x0006BBB3 File Offset: 0x00069DB3
		// (set) Token: 0x0600195C RID: 6492 RVA: 0x0006BBC5 File Offset: 0x00069DC5
		public ADObjectId HomeMTA
		{
			get
			{
				return (ADObjectId)this[ADRecipientSchema.HomeMTA];
			}
			internal set
			{
				this[ADRecipientSchema.HomeMTA] = value;
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x0600195D RID: 6493 RVA: 0x0006BBD3 File Offset: 0x00069DD3
		// (set) Token: 0x0600195E RID: 6494 RVA: 0x0006BBE5 File Offset: 0x00069DE5
		public string ExpansionServer
		{
			get
			{
				return (string)this[ADDynamicGroupSchema.ExpansionServer];
			}
			internal set
			{
				this[ADDynamicGroupSchema.ExpansionServer] = value;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x0600195F RID: 6495 RVA: 0x0006BBF3 File Offset: 0x00069DF3
		// (set) Token: 0x06001960 RID: 6496 RVA: 0x0006BC05 File Offset: 0x00069E05
		public ADObjectId ManagedBy
		{
			get
			{
				return (ADObjectId)this[ADDynamicGroupSchema.ManagedBy];
			}
			internal set
			{
				this[ADDynamicGroupSchema.ManagedBy] = value;
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001961 RID: 6497 RVA: 0x0006BC13 File Offset: 0x00069E13
		// (set) Token: 0x06001962 RID: 6498 RVA: 0x0006BC1B File Offset: 0x00069E1B
		ADObjectId IADDistributionList.ManagedBy
		{
			get
			{
				return this.ManagedBy;
			}
			set
			{
				this.ManagedBy = value;
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001963 RID: 6499 RVA: 0x0006BC24 File Offset: 0x00069E24
		// (set) Token: 0x06001964 RID: 6500 RVA: 0x0006BC36 File Offset: 0x00069E36
		public bool ReportToManagerEnabled
		{
			get
			{
				return (bool)this[ADDynamicGroupSchema.ReportToManagerEnabled];
			}
			internal set
			{
				this[ADDynamicGroupSchema.ReportToManagerEnabled] = value;
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001965 RID: 6501 RVA: 0x0006BC49 File Offset: 0x00069E49
		// (set) Token: 0x06001966 RID: 6502 RVA: 0x0006BC5B File Offset: 0x00069E5B
		public bool ReportToOriginatorEnabled
		{
			get
			{
				return (bool)this[ADDynamicGroupSchema.ReportToOriginatorEnabled];
			}
			internal set
			{
				this[ADDynamicGroupSchema.ReportToOriginatorEnabled] = value;
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001967 RID: 6503 RVA: 0x0006BC6E File Offset: 0x00069E6E
		// (set) Token: 0x06001968 RID: 6504 RVA: 0x0006BC80 File Offset: 0x00069E80
		public DeliveryReportsReceiver SendDeliveryReportsTo
		{
			get
			{
				return (DeliveryReportsReceiver)this[ADDynamicGroupSchema.SendDeliveryReportsTo];
			}
			internal set
			{
				this[ADDynamicGroupSchema.SendDeliveryReportsTo] = value;
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001969 RID: 6505 RVA: 0x0006BC93 File Offset: 0x00069E93
		// (set) Token: 0x0600196A RID: 6506 RVA: 0x0006BC9B File Offset: 0x00069E9B
		DeliveryReportsReceiver IADDistributionList.SendDeliveryReportsTo
		{
			get
			{
				return this.SendDeliveryReportsTo;
			}
			set
			{
				this.SendDeliveryReportsTo = value;
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x0600196B RID: 6507 RVA: 0x0006BCA4 File Offset: 0x00069EA4
		// (set) Token: 0x0600196C RID: 6508 RVA: 0x0006BCB6 File Offset: 0x00069EB6
		public bool SendOofMessageToOriginatorEnabled
		{
			get
			{
				return (bool)this[ADDynamicGroupSchema.SendOofMessageToOriginatorEnabled];
			}
			internal set
			{
				this[ADDynamicGroupSchema.SendOofMessageToOriginatorEnabled] = value;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x0600196D RID: 6509 RVA: 0x0006BCC9 File Offset: 0x00069EC9
		// (set) Token: 0x0600196E RID: 6510 RVA: 0x0006BCD1 File Offset: 0x00069ED1
		bool IADDistributionList.SendOofMessageToOriginatorEnabled
		{
			get
			{
				return this.SendOofMessageToOriginatorEnabled;
			}
			set
			{
				this.SendOofMessageToOriginatorEnabled = value;
			}
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x0006BCDC File Offset: 0x00069EDC
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (this.ReportToManagerEnabled && this.ManagedBy == null)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorReportToManagedEnabledWithoutManager(this.Identity.ToString(), ADDynamicGroupSchema.ReportToManagerEnabled.Name), this.Identity, string.Empty));
			}
			if (this.ReportToManagerEnabled && this.ReportToOriginatorEnabled)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorReportToBothManagerAndOriginator(this.Identity.ToString(), ADDynamicGroupSchema.ReportToManagerEnabled.Name, ADDynamicGroupSchema.ReportToOriginatorEnabled.Name), this.Identity, string.Empty));
			}
			if (this.RecipientContainer == null)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ErrorRecipientContainerCanNotNull, ADDynamicGroupSchema.RecipientContainer, string.Empty));
			}
			if (string.IsNullOrEmpty(this.RecipientFilter) && (base.IsModified(ADDynamicGroupSchema.RecipientFilter) || base.ObjectState == ObjectState.New))
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ErrorInvalidOpathFilter(this.RecipientFilter ?? string.Empty), ADDynamicGroupSchema.RecipientFilter, string.Empty));
			}
			ValidationError validationError = RecipientFilterHelper.ValidatePrecannedRecipientFilter(this.propertyBag, ADDynamicGroupSchema.RecipientFilterMetadata, ADDynamicGroupSchema.RecipientFilter, ADDynamicGroupSchema.IncludedRecipients, this.Identity);
			if (validationError != null)
			{
				errors.Add(validationError);
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001970 RID: 6512 RVA: 0x0006BE12 File Offset: 0x0006A012
		MultiValuedProperty<ADObjectId> IADDistributionList.Members
		{
			get
			{
				return ADDynamicGroup.ddlMembersMvp;
			}
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x0006BE19 File Offset: 0x0006A019
		internal ADPagedReader<ADRawEntry> Expand(int pageSize, params PropertyDefinition[] properties)
		{
			if (this.CheckEmptyLdapRecipientFilter())
			{
				return new ADPagedReader<ADRawEntry>();
			}
			return new ADDynamicGroupPagedReader<ADRawEntry>(base.Session, this.RecipientContainer, QueryScope.SubTree, this.LdapRecipientFilter, pageSize, new CustomExceptionHandler(this.DDLExpansionHandler), properties);
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x0006BE4F File Offset: 0x0006A04F
		internal ADPagedReader<TEntry> Expand<TEntry>(int pageSize, params PropertyDefinition[] properties) where TEntry : MiniRecipient, new()
		{
			if (this.CheckEmptyLdapRecipientFilter())
			{
				return new ADPagedReader<TEntry>();
			}
			return new ADDynamicGroupPagedReader<TEntry>(base.Session, this.RecipientContainer, QueryScope.SubTree, this.LdapRecipientFilter, pageSize, new CustomExceptionHandler(this.DDLExpansionHandler), properties);
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x0006BE85 File Offset: 0x0006A085
		ADPagedReader<ADRawEntry> IADDistributionList.Expand(int pageSize, params PropertyDefinition[] properties)
		{
			return this.Expand(pageSize, properties);
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x0006BE8F File Offset: 0x0006A08F
		ADPagedReader<TEntry> IADDistributionList.Expand<TEntry>(int pageSize, params PropertyDefinition[] properties)
		{
			return this.Expand<TEntry>(pageSize, properties);
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x0006BE99 File Offset: 0x0006A099
		internal ADPagedReader<ADRecipient> Expand(int pageSize)
		{
			if (this.CheckEmptyLdapRecipientFilter())
			{
				return new ADPagedReader<ADRecipient>();
			}
			return new ADDynamicGroupPagedReader<ADRecipient>(base.Session, this.RecipientContainer, QueryScope.SubTree, this.LdapRecipientFilter, pageSize, new CustomExceptionHandler(this.DDLExpansionHandler), null);
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x0006BECF File Offset: 0x0006A0CF
		ADPagedReader<ADRecipient> IADDistributionList.Expand(int pageSize)
		{
			return this.Expand(pageSize);
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x0006BED8 File Offset: 0x0006A0D8
		private bool CheckEmptyLdapRecipientFilter()
		{
			if (string.IsNullOrEmpty(this.LdapRecipientFilter))
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_DynamicDistributionGroupFilterError, base.Id.ToString(), new object[]
				{
					base.Id.ToDNString(),
					base.OriginatingServer,
					DirectoryStrings.ErrorDDLFilterMissing
				});
				return true;
			}
			return false;
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x0006BF38 File Offset: 0x0006A138
		private void DDLExpansionHandler(DirectoryException de)
		{
			PropertyValidationError propertyValidationError = this.CheckForDDLMisconfiguration(de);
			if (propertyValidationError != null)
			{
				ADProviderPerf.UpdateProcessCounter(Counter.ProcessRateCriticalValidationFailures, UpdateType.Update, 1U);
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_DynamicDistributionGroupFilterError, base.Id.ToString(), new object[]
				{
					base.Id.ToDNString(),
					base.OriginatingServer,
					propertyValidationError.Description
				});
				throw new DataValidationException(propertyValidationError, de);
			}
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x0006BFA4 File Offset: 0x0006A1A4
		private PropertyValidationError CheckForDDLMisconfiguration(DirectoryException e)
		{
			DirectoryOperationException ex = e as DirectoryOperationException;
			if (ex != null)
			{
				if (ex.Response == null)
				{
					return null;
				}
				ResultCode resultCode = ex.Response.ResultCode;
				if (resultCode == ResultCode.OperationsError)
				{
					return new PropertyValidationError(DirectoryStrings.ErrorDDLOperationsError, ADDynamicGroupSchema.RecipientContainer, this.RecipientContainer);
				}
				switch (resultCode)
				{
				case ResultCode.ReferralV2:
				case ResultCode.Referral:
					return new PropertyValidationError(DirectoryStrings.ErrorDDLReferral, ADDynamicGroupSchema.RecipientContainer, this.RecipientContainer);
				default:
					switch (resultCode)
					{
					case ResultCode.NoSuchObject:
						return new PropertyValidationError(DirectoryStrings.ErrorDDLNoSuchObject, ADDynamicGroupSchema.RecipientContainer, this.RecipientContainer);
					case ResultCode.InvalidDNSyntax:
						return new PropertyValidationError(DirectoryStrings.ErrorDDLInvalidDNSyntax, ADDynamicGroupSchema.RecipientContainer, this.RecipientContainer);
					}
					return null;
				}
			}
			else
			{
				LdapException ex2 = e as LdapException;
				if (ex2 != null && ex2.ErrorCode == 87)
				{
					return new PropertyValidationError(DirectoryStrings.ErrorDDLFilterError, ADDynamicGroupSchema.RecipientFilter, this.RecipientFilter);
				}
				return null;
			}
		}

		// Token: 0x04000B59 RID: 2905
		private static readonly ADDynamicGroupSchema schema = ObjectSchema.GetInstance<ADDynamicGroupSchema>();

		// Token: 0x04000B5A RID: 2906
		private static ADObjectId[] emptyIdArray = new ADObjectId[0];

		// Token: 0x04000B5B RID: 2907
		internal static string MostDerivedClass = "msExchDynamicDistributionList";

		// Token: 0x04000B5C RID: 2908
		internal static string ObjectCategoryNameInternal = "msExchDynamicDistributionList";

		// Token: 0x04000B5D RID: 2909
		internal static string ObjectCategoryCNInternal = "ms-Exch-Dynamic-Distribution-List";

		// Token: 0x04000B5E RID: 2910
		private static MultiValuedProperty<ADObjectId> ddlMembersMvp = new MultiValuedProperty<ADObjectId>();
	}
}
