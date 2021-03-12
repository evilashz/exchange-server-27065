using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200059B RID: 1435
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class SharingPolicy : ADConfigurationObject
	{
		// Token: 0x170015C8 RID: 5576
		// (get) Token: 0x060042AD RID: 17069 RVA: 0x000FB419 File Offset: 0x000F9619
		// (set) Token: 0x060042AE RID: 17070 RVA: 0x000FB42B File Offset: 0x000F962B
		public MultiValuedProperty<SharingPolicyDomain> Domains
		{
			get
			{
				return (MultiValuedProperty<SharingPolicyDomain>)this[SharingPolicySchema.Domains];
			}
			set
			{
				this[SharingPolicySchema.Domains] = value;
			}
		}

		// Token: 0x170015C9 RID: 5577
		// (get) Token: 0x060042AF RID: 17071 RVA: 0x000FB439 File Offset: 0x000F9639
		// (set) Token: 0x060042B0 RID: 17072 RVA: 0x000FB44B File Offset: 0x000F964B
		public bool Enabled
		{
			get
			{
				return (bool)this[SharingPolicySchema.Enabled];
			}
			set
			{
				this[SharingPolicySchema.Enabled] = value;
			}
		}

		// Token: 0x170015CA RID: 5578
		// (get) Token: 0x060042B1 RID: 17073 RVA: 0x000FB45E File Offset: 0x000F965E
		// (set) Token: 0x060042B2 RID: 17074 RVA: 0x000FB470 File Offset: 0x000F9670
		public bool Default
		{
			get
			{
				return (bool)this[SharingPolicySchema.Default];
			}
			set
			{
				this[SharingPolicySchema.Default] = value;
			}
		}

		// Token: 0x170015CB RID: 5579
		// (get) Token: 0x060042B3 RID: 17075 RVA: 0x000FB483 File Offset: 0x000F9683
		internal override ADObjectSchema Schema
		{
			get
			{
				return SharingPolicy.SchemaObject;
			}
		}

		// Token: 0x170015CC RID: 5580
		// (get) Token: 0x060042B4 RID: 17076 RVA: 0x000FB48A File Offset: 0x000F968A
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchSharingPolicy";
			}
		}

		// Token: 0x170015CD RID: 5581
		// (get) Token: 0x060042B5 RID: 17077 RVA: 0x000FB491 File Offset: 0x000F9691
		internal override ADObjectId ParentPath
		{
			get
			{
				return FederatedOrganizationId.Container;
			}
		}

		// Token: 0x170015CE RID: 5582
		// (get) Token: 0x060042B6 RID: 17078 RVA: 0x000FB498 File Offset: 0x000F9698
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x170015CF RID: 5583
		// (get) Token: 0x060042B7 RID: 17079 RVA: 0x000FB49F File Offset: 0x000F969F
		internal override bool IsShareable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060042B8 RID: 17080 RVA: 0x000FB4A4 File Offset: 0x000F96A4
		internal bool IsAllowedForAnySharing(string domain, SharingPolicyAction actions)
		{
			if (this.Enabled)
			{
				foreach (SharingPolicyDomain sharingPolicyDomain in this.Domains)
				{
					if ((sharingPolicyDomain.Actions & actions) != (SharingPolicyAction)0 && SharingPolicy.IsDomainMatch(sharingPolicyDomain.Domain, domain))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060042B9 RID: 17081 RVA: 0x000FB518 File Offset: 0x000F9718
		internal SharingPolicyAction GetAllowed(string domain)
		{
			SharingPolicyAction sharingPolicyAction = (SharingPolicyAction)0;
			if (this.Enabled)
			{
				foreach (SharingPolicyDomain sharingPolicyDomain in this.Domains)
				{
					if (SharingPolicy.IsDomainMatch(sharingPolicyDomain.Domain, domain))
					{
						sharingPolicyAction |= sharingPolicyDomain.Actions;
					}
				}
			}
			return sharingPolicyAction;
		}

		// Token: 0x060042BA RID: 17082 RVA: 0x000FB588 File Offset: 0x000F9788
		internal bool IsAllowedForAnonymousCalendarSharing()
		{
			return this.GetAllowedForAnonymousCalendarSharing() != (SharingPolicyAction)0;
		}

		// Token: 0x060042BB RID: 17083 RVA: 0x000FB596 File Offset: 0x000F9796
		internal bool IsAllowedForAnonymousMeetingSharing()
		{
			return this.GetAllowedForAnonymousMeetingSharing() != (SharingPolicyAction)0;
		}

		// Token: 0x060042BC RID: 17084 RVA: 0x000FB5A4 File Offset: 0x000F97A4
		internal bool IsAllowedForAnyAnonymousFeature()
		{
			return this.IsAllowedForAnonymousMeetingSharing() || this.IsAllowedForAnonymousCalendarSharing();
		}

		// Token: 0x060042BD RID: 17085 RVA: 0x000FB5B8 File Offset: 0x000F97B8
		internal SharingPolicyAction GetAllowedForAnonymousCalendarSharing()
		{
			SharingPolicyAction sharingPolicyAction = (SharingPolicyAction)0;
			if (this.Enabled)
			{
				foreach (SharingPolicyDomain sharingPolicyDomain in this.Domains)
				{
					if (sharingPolicyDomain.Domain == "Anonymous")
					{
						if ((sharingPolicyDomain.Actions & SharingPolicyAction.CalendarSharingFreeBusyDetail) == SharingPolicyAction.CalendarSharingFreeBusyDetail)
						{
							sharingPolicyAction |= SharingPolicyAction.CalendarSharingFreeBusyDetail;
						}
						if ((sharingPolicyDomain.Actions & SharingPolicyAction.CalendarSharingFreeBusySimple) == SharingPolicyAction.CalendarSharingFreeBusySimple)
						{
							sharingPolicyAction |= SharingPolicyAction.CalendarSharingFreeBusySimple;
						}
						if ((sharingPolicyDomain.Actions & SharingPolicyAction.CalendarSharingFreeBusyReviewer) == SharingPolicyAction.CalendarSharingFreeBusyReviewer)
						{
							sharingPolicyAction |= SharingPolicyAction.CalendarSharingFreeBusyReviewer;
							break;
						}
						break;
					}
				}
			}
			return sharingPolicyAction;
		}

		// Token: 0x060042BE RID: 17086 RVA: 0x000FB650 File Offset: 0x000F9850
		internal SharingPolicyAction GetAllowedForAnonymousMeetingSharing()
		{
			SharingPolicyAction sharingPolicyAction = (SharingPolicyAction)0;
			if (this.Enabled)
			{
				foreach (SharingPolicyDomain sharingPolicyDomain in this.Domains)
				{
					if (sharingPolicyDomain.Domain == "Anonymous")
					{
						if ((sharingPolicyDomain.Actions & SharingPolicyAction.MeetingFullDetails) == SharingPolicyAction.MeetingFullDetails)
						{
							sharingPolicyAction |= SharingPolicyAction.MeetingFullDetails;
						}
						if ((sharingPolicyDomain.Actions & SharingPolicyAction.MeetingFullDetailsWithAttendees) == SharingPolicyAction.MeetingFullDetailsWithAttendees)
						{
							sharingPolicyAction |= SharingPolicyAction.MeetingFullDetailsWithAttendees;
						}
						if ((sharingPolicyDomain.Actions & SharingPolicyAction.MeetingLimitedDetails) == SharingPolicyAction.MeetingLimitedDetails)
						{
							sharingPolicyAction |= SharingPolicyAction.MeetingLimitedDetails;
							break;
						}
						break;
					}
				}
			}
			return sharingPolicyAction;
		}

		// Token: 0x060042BF RID: 17087 RVA: 0x000FB6F4 File Offset: 0x000F98F4
		private static bool IsDomainMatch(string rule, string domain)
		{
			return rule == "*" || StringComparer.OrdinalIgnoreCase.Equals(rule, domain);
		}

		// Token: 0x04002D5C RID: 11612
		internal const string TaskNoun = "SharingPolicy";

		// Token: 0x04002D5D RID: 11613
		internal const string LdapName = "msExchSharingPolicy";

		// Token: 0x04002D5E RID: 11614
		private static readonly SharingPolicySchema SchemaObject = ObjectSchema.GetInstance<SharingPolicySchema>();
	}
}
