using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Mwi
{
	// Token: 0x02000103 RID: 259
	internal class MailboxInfo
	{
		// Token: 0x06000A9A RID: 2714 RVA: 0x0004584C File Offset: 0x00043A4C
		internal MailboxInfo(Guid mailboxGuid, string displayName, OrganizationId orgId)
		{
			this.guid = mailboxGuid;
			this.displayName = displayName;
			this.mapiEvent = null;
			this.eventTimeUtc = ExDateTime.UtcNow;
			this.OrganizationId = orgId;
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0004587B File Offset: 0x00043A7B
		internal MailboxInfo(byte[] guidBytes, string displayName, OrganizationId orgId) : this(new Guid(guidBytes), displayName, orgId)
		{
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0004588B File Offset: 0x00043A8B
		internal MailboxInfo(MapiEvent mapiEvent, OrganizationId orgId) : this(mapiEvent.MailboxGuid, string.Empty, orgId)
		{
			this.mapiEvent = mapiEvent;
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x000458A6 File Offset: 0x00043AA6
		// (set) Token: 0x06000A9E RID: 2718 RVA: 0x000458AE File Offset: 0x00043AAE
		public OrganizationId OrganizationId { get; set; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x000458B7 File Offset: 0x00043AB7
		internal Guid Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x000458BF File Offset: 0x00043ABF
		// (set) Token: 0x06000AA1 RID: 2721 RVA: 0x000458C7 File Offset: 0x00043AC7
		internal string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.displayName = value;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x000458D0 File Offset: 0x00043AD0
		internal MapiEvent MapiEvent
		{
			get
			{
				return this.mapiEvent;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x000458D8 File Offset: 0x00043AD8
		// (set) Token: 0x06000AA4 RID: 2724 RVA: 0x000458E0 File Offset: 0x00043AE0
		internal ExDateTime EventTimeUtc
		{
			get
			{
				return this.eventTimeUtc;
			}
			set
			{
				this.eventTimeUtc = value;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x000458E9 File Offset: 0x00043AE9
		// (set) Token: 0x06000AA6 RID: 2726 RVA: 0x000458F1 File Offset: 0x00043AF1
		internal Guid DialPlanGuid
		{
			get
			{
				return this.dialPlanGuid;
			}
			set
			{
				this.dialPlanGuid = value;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x000458FA File Offset: 0x00043AFA
		// (set) Token: 0x06000AA8 RID: 2728 RVA: 0x00045902 File Offset: 0x00043B02
		internal string UMExtension
		{
			get
			{
				return this.umExtension;
			}
			set
			{
				this.umExtension = value;
			}
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0004590B File Offset: 0x00043B0B
		public override string ToString()
		{
			return string.Format("{0}({1})", this.DisplayName, this.Guid);
		}

		// Token: 0x040006D1 RID: 1745
		private Guid guid;

		// Token: 0x040006D2 RID: 1746
		private string displayName;

		// Token: 0x040006D3 RID: 1747
		private ExDateTime eventTimeUtc;

		// Token: 0x040006D4 RID: 1748
		private MapiEvent mapiEvent;

		// Token: 0x040006D5 RID: 1749
		private string umExtension;

		// Token: 0x040006D6 RID: 1750
		private Guid dialPlanGuid;
	}
}
