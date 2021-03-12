using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000099 RID: 153
	public class RecipientWrapper
	{
		// Token: 0x060003C3 RID: 963 RVA: 0x00005A90 File Offset: 0x00003C90
		public RecipientWrapper(string identity)
		{
			this.Identity = identity;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00005A9F File Offset: 0x00003C9F
		public RecipientWrapper(Mailbox mailbox)
		{
			this.Identity = mailbox.Identity.ToString();
			this.PersistedCapabilities = mailbox.PersistedCapabilities;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00005AC4 File Offset: 0x00003CC4
		public RecipientWrapper(ADObjectId id, RequestStatus moveStatus = RequestStatus.None, string moveBatchName = null, RequestFlags requestFlags = RequestFlags.None, RecipientType recipientType = RecipientType.UserMailbox, RecipientTypeDetails recipientTypeDetails = RecipientTypeDetails.None)
		{
			this.Id = id;
			this.MoveStatus = moveStatus;
			this.MailboxMoveBatchName = moveBatchName;
			this.RequestFlags = requestFlags;
			this.RecipientType = recipientType;
			this.RecipientTypeDetails = recipientTypeDetails;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00005AFC File Offset: 0x00003CFC
		public RecipientWrapper(User user)
		{
			this.Id = user.Id;
			this.RecipientType = user.RecipientType;
			this.UpgradeStatus = user.UpgradeStatus;
			this.UpgradeRequest = user.UpgradeRequest;
			this.UpgradeMessage = user.UpgradeMessage;
			this.UpgradeDetails = user.UpgradeDetails;
			this.UpgradeStage = user.UpgradeStage;
			this.UpgradeStageTimeStamp = user.UpgradeStageTimeStamp;
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x00005B6F File Offset: 0x00003D6F
		// (set) Token: 0x060003C8 RID: 968 RVA: 0x00005B77 File Offset: 0x00003D77
		public ADObjectId Id { get; private set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x00005B80 File Offset: 0x00003D80
		// (set) Token: 0x060003CA RID: 970 RVA: 0x00005B88 File Offset: 0x00003D88
		public RequestStatus MoveStatus { get; private set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060003CB RID: 971 RVA: 0x00005B91 File Offset: 0x00003D91
		// (set) Token: 0x060003CC RID: 972 RVA: 0x00005B99 File Offset: 0x00003D99
		public RecipientType RecipientType { get; private set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060003CD RID: 973 RVA: 0x00005BA2 File Offset: 0x00003DA2
		// (set) Token: 0x060003CE RID: 974 RVA: 0x00005BAA File Offset: 0x00003DAA
		public RequestFlags RequestFlags { get; private set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060003CF RID: 975 RVA: 0x00005BB3 File Offset: 0x00003DB3
		// (set) Token: 0x060003D0 RID: 976 RVA: 0x00005BBB File Offset: 0x00003DBB
		public string MailboxMoveBatchName { get; private set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x00005BC4 File Offset: 0x00003DC4
		// (set) Token: 0x060003D2 RID: 978 RVA: 0x00005BCC File Offset: 0x00003DCC
		public RecipientTypeDetails RecipientTypeDetails { get; private set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x00005BD5 File Offset: 0x00003DD5
		// (set) Token: 0x060003D4 RID: 980 RVA: 0x00005BDD File Offset: 0x00003DDD
		public MultiValuedProperty<Capability> PersistedCapabilities { get; set; }

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x00005BE6 File Offset: 0x00003DE6
		// (set) Token: 0x060003D6 RID: 982 RVA: 0x00005BEE File Offset: 0x00003DEE
		public string Identity { get; set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x00005BF7 File Offset: 0x00003DF7
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x00005BFF File Offset: 0x00003DFF
		public UpgradeStatusTypes UpgradeStatus { get; set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x00005C08 File Offset: 0x00003E08
		// (set) Token: 0x060003DA RID: 986 RVA: 0x00005C10 File Offset: 0x00003E10
		public UpgradeRequestTypes UpgradeRequest { get; set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060003DB RID: 987 RVA: 0x00005C19 File Offset: 0x00003E19
		// (set) Token: 0x060003DC RID: 988 RVA: 0x00005C21 File Offset: 0x00003E21
		public string UpgradeMessage { get; set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060003DD RID: 989 RVA: 0x00005C2A File Offset: 0x00003E2A
		// (set) Token: 0x060003DE RID: 990 RVA: 0x00005C32 File Offset: 0x00003E32
		public string UpgradeDetails { get; set; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00005C3B File Offset: 0x00003E3B
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x00005C43 File Offset: 0x00003E43
		public UpgradeStage? UpgradeStage { get; set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x00005C4C File Offset: 0x00003E4C
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x00005C54 File Offset: 0x00003E54
		public DateTime? UpgradeStageTimeStamp { get; set; }
	}
}
