using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.MailTips;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.InfoWorker.Common.OrganizationConfiguration;

namespace Microsoft.Exchange.InfoWorker.Common.MailTips
{
	// Token: 0x02000116 RID: 278
	internal class MailTips
	{
		// Token: 0x06000799 RID: 1945 RVA: 0x0002293F File Offset: 0x00020B3F
		public MailTips(EmailAddress emailAddress)
		{
			this.emailAddress = emailAddress;
			this.unavailableMailTips = MailTipTypes.All;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00022964 File Offset: 0x00020B64
		public MailTips(RecipientData recipientData) : this(recipientData.EmailAddress)
		{
			this.recipientData = recipientData;
			if (!recipientData.IsEmpty)
			{
				this.configuration = CachedOrganizationConfiguration.GetInstance(recipientData.OrganizationId, CachedOrganizationConfiguration.ConfigurationTypes.All);
				return;
			}
			this.permission = MailTipsPermission.AllAccess;
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x000229A3 File Offset: 0x00020BA3
		public MailTips(EmailAddress emailAddress, Exception exception) : this(emailAddress)
		{
			this.exception = exception;
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x000229B3 File Offset: 0x00020BB3
		internal MailTips(EmailAddress emailAddress, MailTipTypes unavailableMailTips, MailTipTypes pendingMailTips)
		{
			this.emailAddress = emailAddress;
			this.unavailableMailTips = unavailableMailTips;
			this.pendingMailTips = pendingMailTips;
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x000229DB File Offset: 0x00020BDB
		public EmailAddress EmailAddress
		{
			get
			{
				return this.emailAddress;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x000229E3 File Offset: 0x00020BE3
		// (set) Token: 0x0600079F RID: 1951 RVA: 0x000229EB File Offset: 0x00020BEB
		public RecipientData RecipientData
		{
			get
			{
				return this.recipientData;
			}
			internal set
			{
				this.recipientData = value;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x000229F4 File Offset: 0x00020BF4
		public MailTipTypes UnavailableMailTips
		{
			get
			{
				return this.unavailableMailTips;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x000229FC File Offset: 0x00020BFC
		public MailTipTypes PendingMailTips
		{
			get
			{
				return this.pendingMailTips;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x00022A04 File Offset: 0x00020C04
		// (set) Token: 0x060007A3 RID: 1955 RVA: 0x00022A0C File Offset: 0x00020C0C
		public string OutOfOfficeMessage
		{
			get
			{
				return this.outOfOfficeMessage;
			}
			set
			{
				this.outOfOfficeMessage = value;
				this.MarkAsAvailable(MailTipTypes.OutOfOfficeMessage);
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x00022A1C File Offset: 0x00020C1C
		// (set) Token: 0x060007A5 RID: 1957 RVA: 0x00022A24 File Offset: 0x00020C24
		public string OutOfOfficeMessageLanguage
		{
			get
			{
				return this.outOfOfficeMessageLanguage;
			}
			set
			{
				this.outOfOfficeMessageLanguage = value;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x00022A2D File Offset: 0x00020C2D
		// (set) Token: 0x060007A7 RID: 1959 RVA: 0x00022A35 File Offset: 0x00020C35
		public Duration OutOfOfficeDuration
		{
			get
			{
				return this.outOfOfficeDuration;
			}
			set
			{
				this.outOfOfficeDuration = value;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x00022A3E File Offset: 0x00020C3E
		// (set) Token: 0x060007A9 RID: 1961 RVA: 0x00022A46 File Offset: 0x00020C46
		public bool MailboxFull
		{
			get
			{
				return this.mailboxFull;
			}
			set
			{
				this.mailboxFull = value;
				this.MarkAsAvailable(MailTipTypes.MailboxFullStatus);
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x00022A56 File Offset: 0x00020C56
		// (set) Token: 0x060007AB RID: 1963 RVA: 0x00022A5E File Offset: 0x00020C5E
		public string CustomMailTip
		{
			get
			{
				return this.customMailTip;
			}
			set
			{
				this.customMailTip = value;
				this.MarkAsAvailable(MailTipTypes.CustomMailTip);
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x00022A6E File Offset: 0x00020C6E
		// (set) Token: 0x060007AD RID: 1965 RVA: 0x00022A76 File Offset: 0x00020C76
		public int TotalMemberCount
		{
			get
			{
				return this.totalMemberCount;
			}
			set
			{
				this.totalMemberCount = value;
				this.MarkAsAvailable(MailTipTypes.TotalMemberCount);
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x00022A87 File Offset: 0x00020C87
		// (set) Token: 0x060007AF RID: 1967 RVA: 0x00022A8F File Offset: 0x00020C8F
		public int ExternalMemberCount
		{
			get
			{
				return this.externalMemberCount;
			}
			set
			{
				this.externalMemberCount = value;
				this.MarkAsAvailable(MailTipTypes.ExternalMemberCount);
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x00022AA0 File Offset: 0x00020CA0
		// (set) Token: 0x060007B1 RID: 1969 RVA: 0x00022AA8 File Offset: 0x00020CA8
		public bool DeliveryRestricted
		{
			get
			{
				return this.deliveryRestricted;
			}
			set
			{
				this.deliveryRestricted = value;
				this.MarkAsAvailable(MailTipTypes.DeliveryRestriction);
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x00022ABC File Offset: 0x00020CBC
		// (set) Token: 0x060007B3 RID: 1971 RVA: 0x00022AC4 File Offset: 0x00020CC4
		public bool IsModerated
		{
			get
			{
				return this.isModerated;
			}
			set
			{
				this.isModerated = value;
				this.MarkAsAvailable(MailTipTypes.ModerationStatus);
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x00022AD8 File Offset: 0x00020CD8
		// (set) Token: 0x060007B5 RID: 1973 RVA: 0x00022AE0 File Offset: 0x00020CE0
		public bool InvalidRecipient
		{
			get
			{
				return this.invalidRecipient;
			}
			set
			{
				this.invalidRecipient = value;
				this.MarkAsAvailable(MailTipTypes.InvalidRecipient);
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x00022AF4 File Offset: 0x00020CF4
		// (set) Token: 0x060007B7 RID: 1975 RVA: 0x00022AFC File Offset: 0x00020CFC
		public ScopeTypes Scope
		{
			get
			{
				return this.scope;
			}
			set
			{
				this.scope = value;
				this.MarkAsAvailable(MailTipTypes.Scope);
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x00022B10 File Offset: 0x00020D10
		// (set) Token: 0x060007B9 RID: 1977 RVA: 0x00022B18 File Offset: 0x00020D18
		public int MaxMessageSize
		{
			get
			{
				return this.maxMessageSize;
			}
			set
			{
				this.maxMessageSize = value;
				this.MarkAsAvailable(MailTipTypes.MaxMessageSize);
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x00022B29 File Offset: 0x00020D29
		// (set) Token: 0x060007BB RID: 1979 RVA: 0x00022B31 File Offset: 0x00020D31
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
			internal set
			{
				this.exception = value;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x00022B3A File Offset: 0x00020D3A
		// (set) Token: 0x060007BD RID: 1981 RVA: 0x00022B42 File Offset: 0x00020D42
		public bool NeedMerge
		{
			get
			{
				return this.needMerge;
			}
			internal set
			{
				this.needMerge = value;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x00022B4B File Offset: 0x00020D4B
		// (set) Token: 0x060007BF RID: 1983 RVA: 0x00022B53 File Offset: 0x00020D53
		public CachedOrganizationConfiguration Configuration
		{
			get
			{
				return this.configuration;
			}
			set
			{
				this.configuration = value;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x00022B5C File Offset: 0x00020D5C
		// (set) Token: 0x060007C1 RID: 1985 RVA: 0x00022B64 File Offset: 0x00020D64
		public MailTipsPermission Permission
		{
			get
			{
				return this.permission;
			}
			set
			{
				this.permission = value;
			}
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00022B6D File Offset: 0x00020D6D
		public bool IsAvailable(MailTipTypes mailTipType)
		{
			return (this.unavailableMailTips & mailTipType) == MailTipTypes.None;
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00022B7C File Offset: 0x00020D7C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "EmailAddress={0}, Size={1}, ExternalSize={2}, Restricted={3}, Invalid={4}, Moderated={5}, MaxMessageSize={6}, Full={7}, Custom={8}, OOFDuration:{9}, OOFLanguage={10}, OOF={11}, Exception={12}", new object[]
			{
				this.emailAddress,
				this.totalMemberCount,
				this.externalMemberCount,
				this.deliveryRestricted,
				this.invalidRecipient,
				this.isModerated,
				this.maxMessageSize,
				this.mailboxFull,
				this.customMailTip,
				this.outOfOfficeDuration,
				this.outOfOfficeMessageLanguage,
				this.outOfOfficeMessage,
				this.exception
			});
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00022C40 File Offset: 0x00020E40
		internal void MarkAsPending(MailTipTypes mailTips)
		{
			lock (this.flagAccessSynchronizer)
			{
				this.unavailableMailTips &= ~mailTips;
				this.pendingMailTips |= mailTips;
			}
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00022C98 File Offset: 0x00020E98
		internal void MarkAsUnavailable(MailTipTypes mailTipType)
		{
			lock (this.flagAccessSynchronizer)
			{
				this.unavailableMailTips |= mailTipType;
				this.pendingMailTips &= ~mailTipType;
			}
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00022CF0 File Offset: 0x00020EF0
		private void MarkAsAvailable(MailTipTypes mailTipType)
		{
			lock (this.flagAccessSynchronizer)
			{
				this.unavailableMailTips &= ~mailTipType;
				this.pendingMailTips &= ~mailTipType;
			}
		}

		// Token: 0x04000495 RID: 1173
		public const string EventSource = "MSExchange MailTips";

		// Token: 0x04000496 RID: 1174
		public static readonly Trace GetMailTipsTracer = ExTraceGlobals.GetMailTipsTracer;

		// Token: 0x04000497 RID: 1175
		public static readonly ExEventLog Logger = new ExEventLog(MailTips.GetMailTipsTracer.Category, "MSExchange MailTips");

		// Token: 0x04000498 RID: 1176
		private EmailAddress emailAddress;

		// Token: 0x04000499 RID: 1177
		private RecipientData recipientData;

		// Token: 0x0400049A RID: 1178
		private MailTipTypes unavailableMailTips;

		// Token: 0x0400049B RID: 1179
		private MailTipTypes pendingMailTips;

		// Token: 0x0400049C RID: 1180
		private string outOfOfficeMessage;

		// Token: 0x0400049D RID: 1181
		private string outOfOfficeMessageLanguage;

		// Token: 0x0400049E RID: 1182
		private Duration outOfOfficeDuration;

		// Token: 0x0400049F RID: 1183
		private bool mailboxFull;

		// Token: 0x040004A0 RID: 1184
		private string customMailTip;

		// Token: 0x040004A1 RID: 1185
		private int totalMemberCount;

		// Token: 0x040004A2 RID: 1186
		private int externalMemberCount;

		// Token: 0x040004A3 RID: 1187
		private bool invalidRecipient;

		// Token: 0x040004A4 RID: 1188
		private ScopeTypes scope;

		// Token: 0x040004A5 RID: 1189
		private int maxMessageSize;

		// Token: 0x040004A6 RID: 1190
		private bool deliveryRestricted;

		// Token: 0x040004A7 RID: 1191
		private bool isModerated;

		// Token: 0x040004A8 RID: 1192
		private object flagAccessSynchronizer = new object();

		// Token: 0x040004A9 RID: 1193
		private Exception exception;

		// Token: 0x040004AA RID: 1194
		private bool needMerge;

		// Token: 0x040004AB RID: 1195
		private CachedOrganizationConfiguration configuration;

		// Token: 0x040004AC RID: 1196
		private MailTipsPermission permission;
	}
}
