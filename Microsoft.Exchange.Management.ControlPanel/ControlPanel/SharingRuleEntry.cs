using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001EF RID: 495
	[DataContract]
	public class SharingRuleEntry
	{
		// Token: 0x06002627 RID: 9767 RVA: 0x00075894 File Offset: 0x00073A94
		public SharingRuleEntry(SharingPolicyDomain policyDomain)
		{
			if (policyDomain == null)
			{
				throw new ArgumentNullException("policyDomain");
			}
			this.IsAnyDomain = ("*" == policyDomain.Domain);
			if (!this.IsAnyDomain)
			{
				this.Domain = policyDomain.Domain;
			}
			this.IsCalendarSharing = ((SharingPolicyAction)0 != (~SharingPolicyAction.ContactsSharing & policyDomain.Actions));
			this.CalendarSharing = (this.IsCalendarSharing ? (~SharingPolicyAction.ContactsSharing & policyDomain.Actions).ToString() : SharingPolicyAction.CalendarSharingFreeBusySimple.ToString());
			this.IsContactsSharing = ((SharingPolicyAction)0 != (policyDomain.Actions & SharingPolicyAction.ContactsSharing));
		}

		// Token: 0x17001BCD RID: 7117
		// (get) Token: 0x06002628 RID: 9768 RVA: 0x00075936 File Offset: 0x00073B36
		// (set) Token: 0x06002629 RID: 9769 RVA: 0x0007593E File Offset: 0x00073B3E
		[DataMember]
		public bool IsAnyDomain { get; private set; }

		// Token: 0x17001BCE RID: 7118
		// (get) Token: 0x0600262A RID: 9770 RVA: 0x00075947 File Offset: 0x00073B47
		// (set) Token: 0x0600262B RID: 9771 RVA: 0x0007594F File Offset: 0x00073B4F
		[DataMember]
		public string Domain { get; private set; }

		// Token: 0x17001BCF RID: 7119
		// (get) Token: 0x0600262C RID: 9772 RVA: 0x00075958 File Offset: 0x00073B58
		// (set) Token: 0x0600262D RID: 9773 RVA: 0x00075973 File Offset: 0x00073B73
		[DataMember]
		public string FormattedDomain
		{
			get
			{
				if (!this.IsAnyDomain)
				{
					return this.Domain;
				}
				return Strings.SharingDomainOptionAll;
			}
			private set
			{
			}
		}

		// Token: 0x17001BD0 RID: 7120
		// (get) Token: 0x0600262E RID: 9774 RVA: 0x00075975 File Offset: 0x00073B75
		// (set) Token: 0x0600262F RID: 9775 RVA: 0x0007597D File Offset: 0x00073B7D
		[DataMember]
		public bool IsCalendarSharing { get; private set; }

		// Token: 0x17001BD1 RID: 7121
		// (get) Token: 0x06002630 RID: 9776 RVA: 0x00075986 File Offset: 0x00073B86
		// (set) Token: 0x06002631 RID: 9777 RVA: 0x0007598E File Offset: 0x00073B8E
		[DataMember]
		public string CalendarSharing { get; private set; }

		// Token: 0x17001BD2 RID: 7122
		// (get) Token: 0x06002632 RID: 9778 RVA: 0x00075997 File Offset: 0x00073B97
		// (set) Token: 0x06002633 RID: 9779 RVA: 0x0007599F File Offset: 0x00073B9F
		[DataMember]
		public bool IsContactsSharing { get; private set; }

		// Token: 0x06002634 RID: 9780 RVA: 0x000759A8 File Offset: 0x00073BA8
		public static explicit operator SharingPolicyDomain(SharingRuleEntry ruleEntry)
		{
			if (ruleEntry != null)
			{
				string domain = ruleEntry.IsAnyDomain ? "*" : ruleEntry.Domain;
				SharingPolicyAction sharingPolicyAction = (SharingPolicyAction)0;
				if (ruleEntry.IsCalendarSharing)
				{
					sharingPolicyAction = (SharingPolicyAction)Enum.Parse(typeof(SharingPolicyAction), ruleEntry.CalendarSharing);
				}
				if (ruleEntry.IsContactsSharing)
				{
					sharingPolicyAction |= SharingPolicyAction.ContactsSharing;
				}
				return new SharingPolicyDomain(domain, sharingPolicyAction);
			}
			return null;
		}

		// Token: 0x06002635 RID: 9781 RVA: 0x00075A08 File Offset: 0x00073C08
		public static explicit operator SharingRuleEntry(SharingPolicyDomain policyDomain)
		{
			if (policyDomain != null)
			{
				return new SharingRuleEntry(policyDomain);
			}
			return null;
		}
	}
}
