using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mapi;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200028D RID: 653
	[DataContract]
	[KnownType(typeof(Account))]
	public class Account : OrgPerson
	{
		// Token: 0x06002A65 RID: 10853 RVA: 0x0008512C File Offset: 0x0008332C
		public Account(Mailbox mailbox) : base(mailbox)
		{
			if (base.IsRoom)
			{
				this.UserNameLabel = OwaOptionStrings.RoomEmailAddressLabel;
				base.EmailAddress = base.PrimaryEmailAddress;
			}
			else
			{
				SmtpAddress windowsLiveID = mailbox.WindowsLiveID;
				if (SmtpAddress.Empty != windowsLiveID)
				{
					base.EmailAddress = windowsLiveID.ToString();
					this.UserName = windowsLiveID.Local;
					this.Domain = windowsLiveID.Domain;
					DomainCacheValue domainCacheValue = DomainCache.Singleton.Get(new SmtpDomainWithSubdomains(this.Domain), mailbox.OrganizationId);
					this.UserNameLabel = ((domainCacheValue != null && domainCacheValue.LiveIdInstanceType == LiveIdInstanceType.Business) ? OwaOptionStrings.UserNameMOSIDLabel : OwaOptionStrings.UserNameWLIDLabel);
				}
				else
				{
					this.UserNameLabel = OwaOptionStrings.UserLogonNameLabel;
					string userPrincipalName = mailbox.UserPrincipalName;
					base.EmailAddress = userPrincipalName;
					int num = userPrincipalName.IndexOf('@');
					if (num > 0)
					{
						this.UserName = userPrincipalName.Substring(0, num);
						this.Domain = userPrincipalName.Substring(num + 1);
					}
					else
					{
						this.UserName = null;
						this.Domain = null;
					}
				}
			}
			this.UserPhotoUrl = string.Format(CultureInfo.InvariantCulture, "~/Download.aspx?Identity={0}&handlerClass=UserPhotoDownloadHandler&preview=false", new object[]
			{
				base.PrimaryEmailAddress
			});
			this.userPhotoPreviewUrl = string.Format(CultureInfo.InvariantCulture, "~/Download.aspx?Identity={0}&handlerClass=UserPhotoDownloadHandler&preview=true", new object[]
			{
				base.PrimaryEmailAddress
			});
		}

		// Token: 0x17001CF7 RID: 7415
		// (get) Token: 0x06002A66 RID: 10854 RVA: 0x000852AD File Offset: 0x000834AD
		// (set) Token: 0x06002A67 RID: 10855 RVA: 0x000852B5 File Offset: 0x000834B5
		public MailboxStatistics Statistics { get; set; }

		// Token: 0x17001CF8 RID: 7416
		// (get) Token: 0x06002A68 RID: 10856 RVA: 0x000852BE File Offset: 0x000834BE
		// (set) Token: 0x06002A69 RID: 10857 RVA: 0x000852C6 File Offset: 0x000834C6
		public CalendarConfiguration CalendarConfiguration { get; set; }

		// Token: 0x17001CF9 RID: 7417
		// (get) Token: 0x06002A6A RID: 10858 RVA: 0x000852CF File Offset: 0x000834CF
		public Mailbox Mailbox
		{
			get
			{
				return (Mailbox)base.MailEnabledOrgPerson;
			}
		}

		// Token: 0x17001CFA RID: 7418
		// (get) Token: 0x06002A6B RID: 10859 RVA: 0x000852DC File Offset: 0x000834DC
		// (set) Token: 0x06002A6C RID: 10860 RVA: 0x000852E4 File Offset: 0x000834E4
		public bool IsProfilePage { get; internal set; }

		// Token: 0x17001CFB RID: 7419
		// (get) Token: 0x06002A6D RID: 10861 RVA: 0x000852ED File Offset: 0x000834ED
		// (set) Token: 0x06002A6E RID: 10862 RVA: 0x000852F5 File Offset: 0x000834F5
		public CASMailbox CasMailbox { get; set; }

		// Token: 0x17001CFC RID: 7420
		// (get) Token: 0x06002A6F RID: 10863 RVA: 0x000852FE File Offset: 0x000834FE
		// (set) Token: 0x06002A70 RID: 10864 RVA: 0x00085306 File Offset: 0x00083506
		[DataMember]
		public string UserName { get; private set; }

		// Token: 0x17001CFD RID: 7421
		// (get) Token: 0x06002A71 RID: 10865 RVA: 0x0008530F File Offset: 0x0008350F
		// (set) Token: 0x06002A72 RID: 10866 RVA: 0x00085317 File Offset: 0x00083517
		[DataMember]
		public string UserPhotoUrl
		{
			get
			{
				return this.userPhotoUrl;
			}
			private set
			{
				this.userPhotoUrl = value;
			}
		}

		// Token: 0x17001CFE RID: 7422
		// (get) Token: 0x06002A73 RID: 10867 RVA: 0x00085320 File Offset: 0x00083520
		// (set) Token: 0x06002A74 RID: 10868 RVA: 0x00085328 File Offset: 0x00083528
		[DataMember]
		public string UserPhotoPreviewUrl
		{
			get
			{
				return this.userPhotoPreviewUrl;
			}
			private set
			{
				this.userPhotoPreviewUrl = value;
			}
		}

		// Token: 0x17001CFF RID: 7423
		// (get) Token: 0x06002A75 RID: 10869 RVA: 0x00085331 File Offset: 0x00083531
		// (set) Token: 0x06002A76 RID: 10870 RVA: 0x00085339 File Offset: 0x00083539
		[DataMember]
		public string Domain { get; private set; }

		// Token: 0x17001D00 RID: 7424
		// (get) Token: 0x06002A77 RID: 10871 RVA: 0x00085342 File Offset: 0x00083542
		// (set) Token: 0x06002A78 RID: 10872 RVA: 0x0008534A File Offset: 0x0008354A
		[DataMember]
		public string UserNameLabel { get; private set; }

		// Token: 0x17001D01 RID: 7425
		// (get) Token: 0x06002A79 RID: 10873 RVA: 0x00085354 File Offset: 0x00083554
		// (set) Token: 0x06002A7A RID: 10874 RVA: 0x000853B0 File Offset: 0x000835B0
		[DataMember]
		public Identity MailboxPlan
		{
			get
			{
				if (this.Mailbox == null || this.Mailbox.MailboxPlan == null)
				{
					return null;
				}
				return new Identity(this.Mailbox.MailboxPlan.ObjectGuid.ToString(), MailboxPlanResolverExtensions.ResolveMailboxPlan(this.Mailbox.MailboxPlan).DisplayName);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001D02 RID: 7426
		// (get) Token: 0x06002A7B RID: 10875 RVA: 0x000853B8 File Offset: 0x000835B8
		// (set) Token: 0x06002A7C RID: 10876 RVA: 0x0008540F File Offset: 0x0008360F
		[DataMember]
		public Identity RoleAssignmentPolicy
		{
			get
			{
				if (this.Mailbox == null || this.Mailbox.RoleAssignmentPolicy == null)
				{
					return null;
				}
				return new Identity(this.Mailbox.RoleAssignmentPolicy.ObjectGuid.ToString(), this.Mailbox.RoleAssignmentPolicy.Name);
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001D03 RID: 7427
		// (get) Token: 0x06002A7D RID: 10877 RVA: 0x00085418 File Offset: 0x00083618
		// (set) Token: 0x06002A7E RID: 10878 RVA: 0x0008546F File Offset: 0x0008366F
		[DataMember]
		public Identity RetentionPolicy
		{
			get
			{
				if (this.Mailbox == null || this.Mailbox.RetentionPolicy == null)
				{
					return null;
				}
				return new Identity(this.Mailbox.RetentionPolicy.ObjectGuid.ToString(), this.Mailbox.RetentionPolicy.Name);
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001D04 RID: 7428
		// (get) Token: 0x06002A7F RID: 10879 RVA: 0x00085476 File Offset: 0x00083676
		// (set) Token: 0x06002A80 RID: 10880 RVA: 0x0008547E File Offset: 0x0008367E
		[DataMember]
		public IEnumerable<string> AllowedSenders { get; set; }

		// Token: 0x17001D05 RID: 7429
		// (get) Token: 0x06002A81 RID: 10881 RVA: 0x00085487 File Offset: 0x00083687
		// (set) Token: 0x06002A82 RID: 10882 RVA: 0x0008548F File Offset: 0x0008368F
		[DataMember]
		public IEnumerable<string> BlockedSenders { get; set; }

		// Token: 0x17001D06 RID: 7430
		// (get) Token: 0x06002A83 RID: 10883 RVA: 0x00085498 File Offset: 0x00083698
		// (set) Token: 0x06002A84 RID: 10884 RVA: 0x0008551A File Offset: 0x0008371A
		[DataMember]
		public string AutomaticBooking
		{
			get
			{
				if (this.CalendarConfiguration == null)
				{
					return null;
				}
				if (this.CalendarConfiguration.AutomateProcessing == CalendarProcessingFlags.AutoAccept && this.CalendarConfiguration.AllBookInPolicy && !this.CalendarConfiguration.AllRequestInPolicy)
				{
					return true.ToJsonString(null);
				}
				if (this.CalendarConfiguration.AutomateProcessing == CalendarProcessingFlags.AutoAccept && !this.CalendarConfiguration.AllBookInPolicy && this.CalendarConfiguration.AllRequestInPolicy)
				{
					return false.ToJsonString(null);
				}
				return null;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001D07 RID: 7431
		// (get) Token: 0x06002A85 RID: 10885 RVA: 0x00085521 File Offset: 0x00083721
		// (set) Token: 0x06002A86 RID: 10886 RVA: 0x0008554F File Offset: 0x0008374F
		[DataMember]
		public IEnumerable<RecipientObjectResolverRow> ResourceDelegates
		{
			get
			{
				if (this.CalendarConfiguration != null && this.CalendarConfiguration.ResourceDelegates != null)
				{
					return RecipientObjectResolver.Instance.ResolveObjects(this.CalendarConfiguration.ResourceDelegates);
				}
				return null;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001D08 RID: 7432
		// (get) Token: 0x06002A87 RID: 10887 RVA: 0x00085558 File Offset: 0x00083758
		// (set) Token: 0x06002A88 RID: 10888 RVA: 0x00085582 File Offset: 0x00083782
		[DataMember(EmitDefaultValue = false)]
		public int? ResourceCapacity
		{
			get
			{
				if (this.Mailbox == null)
				{
					return null;
				}
				return this.Mailbox.ResourceCapacity;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001D09 RID: 7433
		// (get) Token: 0x06002A89 RID: 10889 RVA: 0x00085591 File Offset: 0x00083791
		[DataMember]
		public IEnumerable<MailboxFeatureInfo> PhoneAndVoiceFeatures
		{
			get
			{
				return from feature in this.GetAllPhoneAndVoiceFeatures()
				where feature.Visible
				select feature;
			}
		}

		// Token: 0x06002A8A RID: 10890 RVA: 0x000856D8 File Offset: 0x000838D8
		private IEnumerable<MailboxFeatureInfo> GetAllPhoneAndVoiceFeatures()
		{
			if (this.CasMailbox != null)
			{
				yield return new EASMailboxFeatureInfo(this.CasMailbox);
			}
			if (this.Mailbox != null && Util.IsMicrosoftHostedOnly)
			{
				yield return new UMMailboxFeatureInfo(this.Mailbox);
			}
			yield break;
		}

		// Token: 0x17001D0A RID: 7434
		// (get) Token: 0x06002A8B RID: 10891 RVA: 0x000856F8 File Offset: 0x000838F8
		private bool UseDatabaseQuota
		{
			get
			{
				return this.Mailbox.UseDatabaseQuotaDefaults != null && this.Mailbox.UseDatabaseQuotaDefaults.Value;
			}
		}

		// Token: 0x17001D0B RID: 7435
		// (get) Token: 0x06002A8C RID: 10892 RVA: 0x0008572F File Offset: 0x0008392F
		private Unlimited<ByteQuantifiedSize> IssueWarningQuota
		{
			get
			{
				if (!this.UseDatabaseQuota)
				{
					return this.Mailbox.IssueWarningQuota;
				}
				return this.Statistics.DatabaseIssueWarningQuota;
			}
		}

		// Token: 0x17001D0C RID: 7436
		// (get) Token: 0x06002A8D RID: 10893 RVA: 0x00085750 File Offset: 0x00083950
		private Unlimited<ByteQuantifiedSize> ActualQuota
		{
			get
			{
				Unlimited<ByteQuantifiedSize> result = this.UseDatabaseQuota ? this.Statistics.DatabaseProhibitSendQuota : this.Mailbox.ProhibitSendQuota;
				if (!result.IsUnlimited)
				{
					return result;
				}
				if (!this.UseDatabaseQuota)
				{
					return this.Mailbox.ProhibitSendReceiveQuota;
				}
				return this.Statistics.DatabaseProhibitSendReceiveQuota;
			}
		}

		// Token: 0x17001D0D RID: 7437
		// (get) Token: 0x06002A8E RID: 10894 RVA: 0x000857A8 File Offset: 0x000839A8
		private uint UsagePercentage
		{
			get
			{
				if (this.Statistics == null || this.Mailbox == null)
				{
					return 0U;
				}
				double num = this.Statistics.TotalItemSize.Value.ToBytes();
				if ((uint)num == 0U)
				{
					return 0U;
				}
				if (this.ActualQuota.IsUnlimited)
				{
					return 3U;
				}
				return (uint)(num / this.ActualQuota.Value.ToBytes() * 100.0);
			}
		}

		// Token: 0x17001D0E RID: 7438
		// (get) Token: 0x06002A8F RID: 10895 RVA: 0x00085824 File Offset: 0x00083A24
		private StatisticsBarState UsageState
		{
			get
			{
				if (this.Mailbox == null || this.Statistics == null)
				{
					return StatisticsBarState.Normal;
				}
				if (this.Statistics.StorageLimitStatus != null)
				{
					if (this.Statistics.StorageLimitStatus == StorageLimitStatus.ProhibitSend || this.Statistics.StorageLimitStatus == StorageLimitStatus.MailboxDisabled)
					{
						return StatisticsBarState.Exceeded;
					}
					if (this.Statistics.StorageLimitStatus == StorageLimitStatus.IssueWarning)
					{
						return StatisticsBarState.Warning;
					}
				}
				else
				{
					ulong num = this.Statistics.TotalItemSize.Value.ToBytes();
					if (!this.ActualQuota.IsUnlimited && num > this.ActualQuota.Value.ToBytes())
					{
						return StatisticsBarState.Exceeded;
					}
					if (!this.IssueWarningQuota.IsUnlimited && num > this.IssueWarningQuota.Value.ToBytes())
					{
						return StatisticsBarState.Warning;
					}
				}
				return StatisticsBarState.Normal;
			}
		}

		// Token: 0x17001D0F RID: 7439
		// (get) Token: 0x06002A90 RID: 10896 RVA: 0x0008593F File Offset: 0x00083B3F
		private string UsageText
		{
			get
			{
				if (this.Mailbox == null || this.Statistics == null)
				{
					return null;
				}
				return string.Format(OwaOptionStrings.MailboxUsageLegacyText, this.Statistics.TotalItemSize.ToAppropriateUnitFormatString());
			}
		}

		// Token: 0x17001D10 RID: 7440
		// (get) Token: 0x06002A91 RID: 10897 RVA: 0x00085974 File Offset: 0x00083B74
		private string AdditionalInfoText
		{
			get
			{
				if (this.Mailbox == null || this.Statistics == null)
				{
					return OwaOptionStrings.MailboxUsageUnavailable;
				}
				if (this.ActualQuota.IsUnlimited)
				{
					return OwaOptionStrings.MailboxUsageUnlimitedText;
				}
				return string.Format((this.UsageState == StatisticsBarState.Exceeded) ? OwaOptionStrings.MailboxUsageExceededText : OwaOptionStrings.MailboxUsageWarningText, this.ActualQuota.ToAppropriateUnitFormatString());
			}
		}

		// Token: 0x17001D11 RID: 7441
		// (get) Token: 0x06002A92 RID: 10898 RVA: 0x000859E1 File Offset: 0x00083BE1
		// (set) Token: 0x06002A93 RID: 10899 RVA: 0x00085A20 File Offset: 0x00083C20
		[DataMember]
		public StatisticsBarData MailboxUsage
		{
			get
			{
				if (this.IsProfilePage)
				{
					return new StatisticsBarData(this.UsagePercentage, this.UsageState, this.UsageText, this.AdditionalInfoText);
				}
				return new StatisticsBarData(this.UsagePercentage, this.UsageState, this.UsageText);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x04002150 RID: 8528
		private string userPhotoUrl;

		// Token: 0x04002151 RID: 8529
		private string userPhotoPreviewUrl;
	}
}
