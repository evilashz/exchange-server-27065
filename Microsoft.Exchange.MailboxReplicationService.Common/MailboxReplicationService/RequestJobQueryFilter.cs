using System;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200020C RID: 524
	public class RequestJobQueryFilter : QueryFilter
	{
		// Token: 0x06001B30 RID: 6960 RVA: 0x0003850C File Offset: 0x0003670C
		public RequestJobQueryFilter(RequestJobObjectId requestJobId)
		{
			this.RequestGuid = requestJobId.RequestGuid;
			this.MdbGuid = requestJobId.MdbGuid;
			this.RequestType = null;
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x00038546 File Offset: 0x00036746
		public RequestJobQueryFilter(Guid req, Guid mdb, MRSRequestType type)
		{
			this.RequestGuid = req;
			this.MdbGuid = mdb;
			this.RequestType = new MRSRequestType?(type);
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06001B32 RID: 6962 RVA: 0x00038568 File Offset: 0x00036768
		// (set) Token: 0x06001B33 RID: 6963 RVA: 0x00038570 File Offset: 0x00036770
		public Guid RequestGuid { get; private set; }

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06001B34 RID: 6964 RVA: 0x00038579 File Offset: 0x00036779
		// (set) Token: 0x06001B35 RID: 6965 RVA: 0x00038581 File Offset: 0x00036781
		public Guid MdbGuid { get; private set; }

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06001B36 RID: 6966 RVA: 0x0003858A File Offset: 0x0003678A
		// (set) Token: 0x06001B37 RID: 6967 RVA: 0x00038592 File Offset: 0x00036792
		public MRSRequestType? RequestType { get; private set; }

		// Token: 0x06001B38 RID: 6968 RVA: 0x0003859C File Offset: 0x0003679C
		public override void ToString(StringBuilder sb)
		{
			sb.Append("(database=");
			sb.Append(this.MdbGuid.ToString());
			if (this.RequestGuid != Guid.Empty)
			{
				sb.Append(",request=");
				sb.Append(this.RequestGuid.ToString());
			}
			if (this.RequestType != null)
			{
				sb.Append(",type=");
				sb.Append(this.RequestType.ToString());
			}
			sb.Append(")");
		}
	}
}
