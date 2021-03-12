using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200028F RID: 655
	[DataContract]
	public class SetAccount : SetOrgPerson
	{
		// Token: 0x06002A99 RID: 10905 RVA: 0x00085B7C File Offset: 0x00083D7C
		public SetAccount()
		{
			this.OnDeserializing(default(StreamingContext));
		}

		// Token: 0x17001D14 RID: 7444
		// (get) Token: 0x06002A9A RID: 10906 RVA: 0x00085B9E File Offset: 0x00083D9E
		// (set) Token: 0x06002A9B RID: 10907 RVA: 0x00085BA6 File Offset: 0x00083DA6
		public bool? EnableUM { get; private set; }

		// Token: 0x17001D15 RID: 7445
		// (get) Token: 0x06002A9C RID: 10908 RVA: 0x00085BAF File Offset: 0x00083DAF
		// (set) Token: 0x06002A9D RID: 10909 RVA: 0x00085BB7 File Offset: 0x00083DB7
		public SetMailbox SetMailbox { get; private set; }

		// Token: 0x17001D16 RID: 7446
		// (get) Token: 0x06002A9E RID: 10910 RVA: 0x00085BC0 File Offset: 0x00083DC0
		// (set) Token: 0x06002A9F RID: 10911 RVA: 0x00085BC8 File Offset: 0x00083DC8
		public SetCasMailbox SetCasMailbox { get; private set; }

		// Token: 0x17001D17 RID: 7447
		// (get) Token: 0x06002AA0 RID: 10912 RVA: 0x00085BD1 File Offset: 0x00083DD1
		// (set) Token: 0x06002AA1 RID: 10913 RVA: 0x00085BD9 File Offset: 0x00083DD9
		public SetCalendarProcessing SetCalendarProcessing { get; private set; }

		// Token: 0x17001D18 RID: 7448
		// (get) Token: 0x06002AA2 RID: 10914 RVA: 0x00085BE2 File Offset: 0x00083DE2
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-User";
			}
		}

		// Token: 0x17001D19 RID: 7449
		// (get) Token: 0x06002AA3 RID: 10915 RVA: 0x00085BE9 File Offset: 0x00083DE9
		public override string RbacScope
		{
			get
			{
				return "@W:Self|Organization";
			}
		}

		// Token: 0x17001D1A RID: 7450
		// (get) Token: 0x06002AA4 RID: 10916 RVA: 0x00085BF0 File Offset: 0x00083DF0
		// (set) Token: 0x06002AA5 RID: 10917 RVA: 0x00085BFD File Offset: 0x00083DFD
		[DataMember]
		public IEnumerable<string> EmailAddresses
		{
			get
			{
				return this.SetMailbox.EmailAddresses;
			}
			set
			{
				this.SetMailbox.EmailAddresses = value;
			}
		}

		// Token: 0x17001D1B RID: 7451
		// (get) Token: 0x06002AA6 RID: 10918 RVA: 0x00085C0B File Offset: 0x00083E0B
		// (set) Token: 0x06002AA7 RID: 10919 RVA: 0x00085C18 File Offset: 0x00083E18
		[DataMember]
		public string MailTip
		{
			get
			{
				return this.SetMailbox.MailTip;
			}
			set
			{
				this.SetMailbox.MailTip = value;
			}
		}

		// Token: 0x17001D1C RID: 7452
		// (get) Token: 0x06002AA8 RID: 10920 RVA: 0x00085C26 File Offset: 0x00083E26
		// (set) Token: 0x06002AA9 RID: 10921 RVA: 0x00085C33 File Offset: 0x00083E33
		[DataMember]
		public string MailboxPlan
		{
			get
			{
				return this.SetMailbox.MailboxPlan;
			}
			set
			{
				this.SetMailbox.MailboxPlan = value;
			}
		}

		// Token: 0x17001D1D RID: 7453
		// (get) Token: 0x06002AAA RID: 10922 RVA: 0x00085C41 File Offset: 0x00083E41
		// (set) Token: 0x06002AAB RID: 10923 RVA: 0x00085C4E File Offset: 0x00083E4E
		[DataMember]
		public string RoleAssignmentPolicy
		{
			get
			{
				return this.SetMailbox.RoleAssignmentPolicy;
			}
			set
			{
				this.SetMailbox.RoleAssignmentPolicy = value;
			}
		}

		// Token: 0x17001D1E RID: 7454
		// (get) Token: 0x06002AAC RID: 10924 RVA: 0x00085C5C File Offset: 0x00083E5C
		// (set) Token: 0x06002AAD RID: 10925 RVA: 0x00085C69 File Offset: 0x00083E69
		[DataMember]
		public string RetentionPolicy
		{
			get
			{
				return this.SetMailbox.RetentionPolicy;
			}
			set
			{
				this.SetMailbox.RetentionPolicy = value;
			}
		}

		// Token: 0x17001D1F RID: 7455
		// (get) Token: 0x06002AAE RID: 10926 RVA: 0x00085C77 File Offset: 0x00083E77
		// (set) Token: 0x06002AAF RID: 10927 RVA: 0x00085C84 File Offset: 0x00083E84
		[DataMember]
		public string ResourceCapacity
		{
			get
			{
				return this.SetMailbox.ResourceCapacity;
			}
			set
			{
				this.SetMailbox.ResourceCapacity = value;
			}
		}

		// Token: 0x17001D20 RID: 7456
		// (get) Token: 0x06002AB0 RID: 10928 RVA: 0x00085C92 File Offset: 0x00083E92
		// (set) Token: 0x06002AB1 RID: 10929 RVA: 0x00085C9A File Offset: 0x00083E9A
		public bool? EnableLitigationHold { get; private set; }

		// Token: 0x17001D21 RID: 7457
		// (get) Token: 0x06002AB2 RID: 10930 RVA: 0x00085CA3 File Offset: 0x00083EA3
		// (set) Token: 0x06002AB3 RID: 10931 RVA: 0x00085CB0 File Offset: 0x00083EB0
		[DataMember]
		public bool? LitigationHoldEnabled
		{
			get
			{
				return this.SetMailbox.LitigationHoldEnabled;
			}
			set
			{
				this.SetMailbox.LitigationHoldEnabled = value;
			}
		}

		// Token: 0x17001D22 RID: 7458
		// (get) Token: 0x06002AB4 RID: 10932 RVA: 0x00085CBE File Offset: 0x00083EBE
		// (set) Token: 0x06002AB5 RID: 10933 RVA: 0x00085CCB File Offset: 0x00083ECB
		[DataMember]
		public string RetentionComment
		{
			get
			{
				return this.SetMailbox.RetentionComment;
			}
			set
			{
				this.SetMailbox.RetentionComment = value;
			}
		}

		// Token: 0x17001D23 RID: 7459
		// (get) Token: 0x06002AB6 RID: 10934 RVA: 0x00085CD9 File Offset: 0x00083ED9
		// (set) Token: 0x06002AB7 RID: 10935 RVA: 0x00085CE6 File Offset: 0x00083EE6
		[DataMember]
		public string RetentionUrl
		{
			get
			{
				return this.SetMailbox.RetentionUrl;
			}
			set
			{
				this.SetMailbox.RetentionUrl = value;
			}
		}

		// Token: 0x17001D24 RID: 7460
		// (get) Token: 0x06002AB8 RID: 10936 RVA: 0x00085CF4 File Offset: 0x00083EF4
		// (set) Token: 0x06002AB9 RID: 10937 RVA: 0x00085D01 File Offset: 0x00083F01
		[DataMember]
		public string AutomaticBooking
		{
			get
			{
				return this.SetCalendarProcessing.AutomaticBooking;
			}
			set
			{
				this.SetCalendarProcessing.AutomaticBooking = value;
			}
		}

		// Token: 0x17001D25 RID: 7461
		// (get) Token: 0x06002ABA RID: 10938 RVA: 0x00085D0F File Offset: 0x00083F0F
		// (set) Token: 0x06002ABB RID: 10939 RVA: 0x00085D1C File Offset: 0x00083F1C
		[DataMember]
		public Identity[] ResourceDelegates
		{
			get
			{
				return this.SetCalendarProcessing.ResourceDelegates;
			}
			set
			{
				this.SetCalendarProcessing.ResourceDelegates = value;
			}
		}

		// Token: 0x17001D26 RID: 7462
		// (get) Token: 0x06002ABC RID: 10940 RVA: 0x00085D2A File Offset: 0x00083F2A
		// (set) Token: 0x06002ABD RID: 10941 RVA: 0x00085D32 File Offset: 0x00083F32
		[DataMember]
		public IEnumerable<string> AllowedSenders { [PrincipalPermission(SecurityAction.Demand, Role = "Get-SupervisionListEntry?Identity@R:Organization")] get; [PrincipalPermission(SecurityAction.Demand, Role = "Get-User?Identity@R:Organization+Get-Mailbox?Identity@R:Organization+Add-SupervisionListEntry?Identity@W:Self+Remove-SupervisionListEntry?Identity@W:Self")] [PrincipalPermission(SecurityAction.Demand, Role = "Get-User?Identity@R:Organization+Get-Mailbox?Identity@R:Organization+Add-SupervisionListEntry?Identity@W:Organization+Remove-SupervisionListEntry?Identity@W:Organization")] set; }

		// Token: 0x17001D27 RID: 7463
		// (get) Token: 0x06002ABE RID: 10942 RVA: 0x00085D3B File Offset: 0x00083F3B
		// (set) Token: 0x06002ABF RID: 10943 RVA: 0x00085D43 File Offset: 0x00083F43
		[DataMember]
		public IEnumerable<string> BlockedSenders { [PrincipalPermission(SecurityAction.Demand, Role = "Get-SupervisionListEntry?Identity@R:Organization")] get; [PrincipalPermission(SecurityAction.Demand, Role = "Get-User?Identity@R:Organization+Get-Mailbox?Identity@R:Organization+Add-SupervisionListEntry?Identity@W:Self+Remove-SupervisionListEntry?Identity@W:Self")] [PrincipalPermission(SecurityAction.Demand, Role = "Get-User?Identity@R:Organization+Get-Mailbox?Identity@R:Organization+Add-SupervisionListEntry?Identity@W:Organization+Remove-SupervisionListEntry?Identity@W:Organization")] set; }

		// Token: 0x17001D28 RID: 7464
		// (get) Token: 0x06002AC0 RID: 10944 RVA: 0x00085D4C File Offset: 0x00083F4C
		// (set) Token: 0x06002AC1 RID: 10945 RVA: 0x00085D50 File Offset: 0x00083F50
		[DataMember]
		private IEnumerable<MailboxFeatureInfo> PhoneAndVoiceFeatures
		{
			get
			{
				return null;
			}
			set
			{
				foreach (MailboxFeatureInfo mailboxFeatureInfo in value)
				{
					UMMailboxFeatureInfo ummailboxFeatureInfo = mailboxFeatureInfo as UMMailboxFeatureInfo;
					if (ummailboxFeatureInfo != null)
					{
						this.EnableUM = MailboxFeatureInfo.IsEnabled(ummailboxFeatureInfo.Status);
					}
					else if (mailboxFeatureInfo is EASMailboxFeatureInfo)
					{
						EASMailboxFeatureInfo easmailboxFeatureInfo = mailboxFeatureInfo as EASMailboxFeatureInfo;
						this.SetCasMailbox.ActiveSyncEnabled = new bool?(easmailboxFeatureInfo.Status == ClientStrings.EnabledDisplayText || easmailboxFeatureInfo.Status == ClientStrings.EnabledPendingDisplayText);
					}
				}
			}
		}

		// Token: 0x06002AC2 RID: 10946 RVA: 0x00085DF4 File Offset: 0x00083FF4
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			this.SetMailbox = new SetMailbox();
			this.SetCasMailbox = new SetCasMailbox();
			this.SetCalendarProcessing = new SetCalendarProcessing();
		}
	}
}
