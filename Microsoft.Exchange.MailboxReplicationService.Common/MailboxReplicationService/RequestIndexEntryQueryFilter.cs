using System;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001DC RID: 476
	public class RequestIndexEntryQueryFilter : QueryFilter
	{
		// Token: 0x060013AC RID: 5036 RVA: 0x0002C980 File Offset: 0x0002AB80
		public RequestIndexEntryQueryFilter()
		{
			this.requestGuid = Guid.Empty;
			this.requestQueueId = null;
			this.requestType = MRSRequestType.Move;
			this.requestName = null;
			this.mailboxId = null;
			this.dbId = null;
			this.looseMailboxSearch = false;
			this.wildcardedNameSearch = false;
			this.indexId = null;
			this.batchName = null;
			this.sourceMailbox = null;
			this.targetMailbox = null;
			this.sourceDatabase = null;
			this.targetDatabase = null;
			this.status = RequestStatus.None;
			this.flags = RequestFlags.None;
			this.notFlags = RequestFlags.None;
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x0002CA10 File Offset: 0x0002AC10
		public RequestIndexEntryQueryFilter(RequestIndexEntryObjectId requestIndexEntryId)
		{
			this.requestGuid = requestIndexEntryId.RequestGuid;
			this.requestQueueId = null;
			this.requestType = requestIndexEntryId.RequestType;
			this.indexId = requestIndexEntryId.IndexId;
			this.requestName = null;
			this.mailboxId = null;
			this.dbId = null;
			this.looseMailboxSearch = false;
			this.wildcardedNameSearch = false;
			this.batchName = null;
			this.sourceMailbox = null;
			this.targetMailbox = null;
			this.sourceDatabase = null;
			this.targetDatabase = null;
			this.status = RequestStatus.None;
			this.flags = RequestFlags.None;
			this.notFlags = RequestFlags.None;
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x0002CAAC File Offset: 0x0002ACAC
		public RequestIndexEntryQueryFilter(string name, ADObjectId id, MRSRequestType type, RequestIndexId idx, bool mbxSearch)
		{
			this.requestName = name;
			this.mailboxId = (mbxSearch ? id : null);
			this.dbId = (mbxSearch ? null : id);
			this.looseMailboxSearch = false;
			this.wildcardedNameSearch = false;
			this.requestType = type;
			this.indexId = idx;
			this.requestGuid = Guid.Empty;
			this.requestQueueId = null;
			this.batchName = null;
			this.sourceMailbox = null;
			this.targetMailbox = null;
			this.sourceDatabase = null;
			this.status = RequestStatus.None;
			this.flags = RequestFlags.None;
			this.notFlags = RequestFlags.None;
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x060013AF RID: 5039 RVA: 0x0002CB42 File Offset: 0x0002AD42
		// (set) Token: 0x060013B0 RID: 5040 RVA: 0x0002CB4A File Offset: 0x0002AD4A
		public Guid RequestGuid
		{
			get
			{
				return this.requestGuid;
			}
			internal set
			{
				this.requestGuid = value;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x0002CB53 File Offset: 0x0002AD53
		// (set) Token: 0x060013B2 RID: 5042 RVA: 0x0002CB5B File Offset: 0x0002AD5B
		public ADObjectId RequestQueueId
		{
			get
			{
				return this.requestQueueId;
			}
			internal set
			{
				this.requestQueueId = value;
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x0002CB64 File Offset: 0x0002AD64
		// (set) Token: 0x060013B4 RID: 5044 RVA: 0x0002CB6C File Offset: 0x0002AD6C
		public MRSRequestType RequestType
		{
			get
			{
				return this.requestType;
			}
			internal set
			{
				this.requestType = value;
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x060013B5 RID: 5045 RVA: 0x0002CB75 File Offset: 0x0002AD75
		// (set) Token: 0x060013B6 RID: 5046 RVA: 0x0002CB7D File Offset: 0x0002AD7D
		public string RequestName
		{
			get
			{
				return this.requestName;
			}
			internal set
			{
				this.requestName = value;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x060013B7 RID: 5047 RVA: 0x0002CB86 File Offset: 0x0002AD86
		// (set) Token: 0x060013B8 RID: 5048 RVA: 0x0002CB8E File Offset: 0x0002AD8E
		public ADObjectId MailboxId
		{
			get
			{
				return this.mailboxId;
			}
			internal set
			{
				this.mailboxId = value;
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x060013B9 RID: 5049 RVA: 0x0002CB97 File Offset: 0x0002AD97
		// (set) Token: 0x060013BA RID: 5050 RVA: 0x0002CB9F File Offset: 0x0002AD9F
		public bool LooseMailboxSearch
		{
			get
			{
				return this.looseMailboxSearch;
			}
			internal set
			{
				this.looseMailboxSearch = value;
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x060013BB RID: 5051 RVA: 0x0002CBA8 File Offset: 0x0002ADA8
		// (set) Token: 0x060013BC RID: 5052 RVA: 0x0002CBB0 File Offset: 0x0002ADB0
		public bool WildcardedNameSearch
		{
			get
			{
				return this.wildcardedNameSearch;
			}
			internal set
			{
				this.wildcardedNameSearch = value;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x060013BD RID: 5053 RVA: 0x0002CBB9 File Offset: 0x0002ADB9
		// (set) Token: 0x060013BE RID: 5054 RVA: 0x0002CBC1 File Offset: 0x0002ADC1
		public string BatchName
		{
			get
			{
				return this.batchName;
			}
			internal set
			{
				this.batchName = value;
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x060013BF RID: 5055 RVA: 0x0002CBCA File Offset: 0x0002ADCA
		// (set) Token: 0x060013C0 RID: 5056 RVA: 0x0002CBD2 File Offset: 0x0002ADD2
		public ADObjectId SourceMailbox
		{
			get
			{
				return this.sourceMailbox;
			}
			internal set
			{
				this.sourceMailbox = value;
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x060013C1 RID: 5057 RVA: 0x0002CBDB File Offset: 0x0002ADDB
		// (set) Token: 0x060013C2 RID: 5058 RVA: 0x0002CBE3 File Offset: 0x0002ADE3
		public ADObjectId TargetMailbox
		{
			get
			{
				return this.targetMailbox;
			}
			internal set
			{
				this.targetMailbox = value;
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x060013C3 RID: 5059 RVA: 0x0002CBEC File Offset: 0x0002ADEC
		// (set) Token: 0x060013C4 RID: 5060 RVA: 0x0002CBF4 File Offset: 0x0002ADF4
		public ADObjectId DBId
		{
			get
			{
				return this.dbId;
			}
			internal set
			{
				this.dbId = value;
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x060013C5 RID: 5061 RVA: 0x0002CBFD File Offset: 0x0002ADFD
		// (set) Token: 0x060013C6 RID: 5062 RVA: 0x0002CC05 File Offset: 0x0002AE05
		public ADObjectId SourceDatabase
		{
			get
			{
				return this.sourceDatabase;
			}
			internal set
			{
				this.sourceDatabase = value;
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x060013C7 RID: 5063 RVA: 0x0002CC0E File Offset: 0x0002AE0E
		// (set) Token: 0x060013C8 RID: 5064 RVA: 0x0002CC16 File Offset: 0x0002AE16
		public ADObjectId TargetDatabase
		{
			get
			{
				return this.targetDatabase;
			}
			internal set
			{
				this.targetDatabase = value;
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x060013C9 RID: 5065 RVA: 0x0002CC1F File Offset: 0x0002AE1F
		// (set) Token: 0x060013CA RID: 5066 RVA: 0x0002CC27 File Offset: 0x0002AE27
		public RequestStatus Status
		{
			get
			{
				return this.status;
			}
			internal set
			{
				this.status = value;
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x060013CB RID: 5067 RVA: 0x0002CC30 File Offset: 0x0002AE30
		// (set) Token: 0x060013CC RID: 5068 RVA: 0x0002CC38 File Offset: 0x0002AE38
		public RequestFlags Flags
		{
			get
			{
				return this.flags;
			}
			internal set
			{
				this.flags = value;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x060013CD RID: 5069 RVA: 0x0002CC41 File Offset: 0x0002AE41
		// (set) Token: 0x060013CE RID: 5070 RVA: 0x0002CC49 File Offset: 0x0002AE49
		public RequestFlags NotFlags
		{
			get
			{
				return this.notFlags;
			}
			internal set
			{
				this.notFlags = value;
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x060013CF RID: 5071 RVA: 0x0002CC52 File Offset: 0x0002AE52
		// (set) Token: 0x060013D0 RID: 5072 RVA: 0x0002CC5A File Offset: 0x0002AE5A
		public RequestIndexId IndexId
		{
			get
			{
				return this.indexId;
			}
			internal set
			{
				this.indexId = value;
			}
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x0002CC64 File Offset: 0x0002AE64
		public override void ToString(StringBuilder sb)
		{
			sb.Append("(");
			sb.Append("type=");
			sb.Append(this.requestType.ToString());
			if (this.requestGuid != Guid.Empty)
			{
				sb.Append(",requestGuid=");
				sb.Append(this.requestGuid.ToString());
			}
			if (this.requestQueueId != null)
			{
				sb.Append(",requestQueueId=");
				sb.Append(this.requestQueueId.ToString());
			}
			if (!string.IsNullOrEmpty(this.requestName))
			{
				sb.Append(",requestName=");
				sb.Append(this.requestName);
			}
			if (this.indexId != null)
			{
				sb.Append(",index=");
				sb.Append(this.indexId.ToString());
			}
			if (this.mailboxId != null)
			{
				sb.Append(",mailbox=");
				sb.Append(this.mailboxId.ToString());
				sb.Append(",search=");
				sb.Append(this.looseMailboxSearch ? "loose" : "strict");
			}
			if (this.batchName != null)
			{
				sb.Append(",batchName=");
				sb.Append(this.batchName);
			}
			if (this.sourceMailbox != null)
			{
				sb.Append(",sourceMailbox=");
				sb.Append(this.sourceMailbox.ToString());
			}
			if (this.targetMailbox != null)
			{
				sb.Append(",targetMailbox=");
				sb.Append(this.targetMailbox.ToString());
			}
			if (this.dbId != null)
			{
				sb.Append(",database=");
				sb.Append(this.dbId.ToString());
			}
			if (this.sourceDatabase != null)
			{
				sb.Append(",sourceDatabase=");
				sb.Append(this.sourceDatabase.ToString());
			}
			if (this.targetDatabase != null)
			{
				sb.Append(",targetDatabase=");
				sb.Append(this.targetDatabase.ToString());
			}
			if (this.status != RequestStatus.None)
			{
				sb.Append(",status=");
				sb.Append(this.status.ToString());
			}
			if (this.flags != RequestFlags.None)
			{
				sb.Append(",flags contains ");
				sb.Append(this.flags.ToString());
			}
			if (this.notFlags != RequestFlags.None)
			{
				sb.Append(",flags don't contain ");
				sb.Append(this.notFlags.ToString());
			}
			sb.Append(")");
		}

		// Token: 0x04000A1A RID: 2586
		private Guid requestGuid;

		// Token: 0x04000A1B RID: 2587
		private ADObjectId requestQueueId;

		// Token: 0x04000A1C RID: 2588
		private MRSRequestType requestType;

		// Token: 0x04000A1D RID: 2589
		private string requestName;

		// Token: 0x04000A1E RID: 2590
		private ADObjectId mailboxId;

		// Token: 0x04000A1F RID: 2591
		private ADObjectId dbId;

		// Token: 0x04000A20 RID: 2592
		private bool looseMailboxSearch;

		// Token: 0x04000A21 RID: 2593
		private bool wildcardedNameSearch;

		// Token: 0x04000A22 RID: 2594
		private string batchName;

		// Token: 0x04000A23 RID: 2595
		private ADObjectId sourceMailbox;

		// Token: 0x04000A24 RID: 2596
		private ADObjectId targetMailbox;

		// Token: 0x04000A25 RID: 2597
		private ADObjectId sourceDatabase;

		// Token: 0x04000A26 RID: 2598
		private ADObjectId targetDatabase;

		// Token: 0x04000A27 RID: 2599
		private RequestStatus status;

		// Token: 0x04000A28 RID: 2600
		private RequestFlags flags;

		// Token: 0x04000A29 RID: 2601
		private RequestFlags notFlags;

		// Token: 0x04000A2A RID: 2602
		private RequestIndexId indexId;
	}
}
