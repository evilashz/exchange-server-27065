using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001E1 RID: 481
	[Serializable]
	public abstract class RequestBase : ConfigurableObject
	{
		// Token: 0x06001401 RID: 5121 RVA: 0x0002DE43 File Offset: 0x0002C043
		public RequestBase() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x0002DE50 File Offset: 0x0002C050
		internal RequestBase(IRequestIndexEntry index) : this()
		{
			this.Initialize(index);
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001403 RID: 5123 RVA: 0x0002DE5F File Offset: 0x0002C05F
		// (set) Token: 0x06001404 RID: 5124 RVA: 0x0002DE71 File Offset: 0x0002C071
		public string Name
		{
			get
			{
				return (string)this[RequestSchema.Name];
			}
			private set
			{
				this[RequestSchema.Name] = value;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001405 RID: 5125 RVA: 0x0002DE7F File Offset: 0x0002C07F
		// (set) Token: 0x06001406 RID: 5126 RVA: 0x0002DE91 File Offset: 0x0002C091
		public Guid RequestGuid
		{
			get
			{
				return (Guid)this[RequestSchema.RequestGuid];
			}
			private set
			{
				this[RequestSchema.RequestGuid] = value;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001407 RID: 5127 RVA: 0x0002DEA4 File Offset: 0x0002C0A4
		// (set) Token: 0x06001408 RID: 5128 RVA: 0x0002DEB6 File Offset: 0x0002C0B6
		public ADObjectId RequestQueue
		{
			get
			{
				return (ADObjectId)this[RequestSchema.RequestQueue];
			}
			private set
			{
				this[RequestSchema.RequestQueue] = value;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001409 RID: 5129 RVA: 0x0002DEC4 File Offset: 0x0002C0C4
		// (set) Token: 0x0600140A RID: 5130 RVA: 0x0002DED6 File Offset: 0x0002C0D6
		public RequestFlags Flags
		{
			get
			{
				return (RequestFlags)this[RequestSchema.Flags];
			}
			private set
			{
				this[RequestSchema.Flags] = value;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x0600140B RID: 5131 RVA: 0x0002DEE9 File Offset: 0x0002C0E9
		// (set) Token: 0x0600140C RID: 5132 RVA: 0x0002DEFB File Offset: 0x0002C0FB
		public string BatchName
		{
			get
			{
				return (string)this[RequestSchema.BatchName];
			}
			private set
			{
				this[RequestSchema.BatchName] = value;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x0002DF09 File Offset: 0x0002C109
		// (set) Token: 0x0600140E RID: 5134 RVA: 0x0002DF1B File Offset: 0x0002C11B
		public RequestStatus Status
		{
			get
			{
				return (RequestStatus)this[RequestSchema.Status];
			}
			private set
			{
				this[RequestSchema.Status] = value;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x0002DF2E File Offset: 0x0002C12E
		public bool Protect
		{
			get
			{
				return (this.Flags & RequestFlags.Protected) != RequestFlags.None;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001410 RID: 5136 RVA: 0x0002DF3F File Offset: 0x0002C13F
		public bool Suspend
		{
			get
			{
				return (this.Flags & RequestFlags.Suspend) != RequestFlags.None;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x0002DF53 File Offset: 0x0002C153
		public RequestDirection Direction
		{
			get
			{
				if ((this.Flags & RequestFlags.Push) == RequestFlags.None)
				{
					return RequestDirection.Pull;
				}
				return RequestDirection.Push;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001412 RID: 5138 RVA: 0x0002DF62 File Offset: 0x0002C162
		public RequestStyle RequestStyle
		{
			get
			{
				if ((this.Flags & RequestFlags.CrossOrg) == RequestFlags.None)
				{
					return RequestStyle.IntraOrg;
				}
				return RequestStyle.CrossOrg;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x0002DF71 File Offset: 0x0002C171
		// (set) Token: 0x06001414 RID: 5140 RVA: 0x0002DF83 File Offset: 0x0002C183
		public OrganizationId OrganizationId
		{
			get
			{
				return (OrganizationId)this[RequestSchema.OrganizationId];
			}
			private set
			{
				this[RequestSchema.OrganizationId] = value;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06001415 RID: 5141 RVA: 0x0002DF91 File Offset: 0x0002C191
		// (set) Token: 0x06001416 RID: 5142 RVA: 0x0002DFA3 File Offset: 0x0002C1A3
		public DateTime? WhenChanged
		{
			get
			{
				return (DateTime?)this[RequestSchema.WhenChanged];
			}
			private set
			{
				this[RequestSchema.WhenChanged] = value;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06001417 RID: 5143 RVA: 0x0002DFB6 File Offset: 0x0002C1B6
		// (set) Token: 0x06001418 RID: 5144 RVA: 0x0002DFC8 File Offset: 0x0002C1C8
		public DateTime? WhenCreated
		{
			get
			{
				return (DateTime?)this[RequestSchema.WhenCreated];
			}
			private set
			{
				this[RequestSchema.WhenCreated] = value;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06001419 RID: 5145 RVA: 0x0002DFDB File Offset: 0x0002C1DB
		// (set) Token: 0x0600141A RID: 5146 RVA: 0x0002DFED File Offset: 0x0002C1ED
		public DateTime? WhenChangedUTC
		{
			get
			{
				return (DateTime?)this[RequestSchema.WhenChangedUTC];
			}
			private set
			{
				this[RequestSchema.WhenChangedUTC] = value;
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x0600141B RID: 5147 RVA: 0x0002E000 File Offset: 0x0002C200
		// (set) Token: 0x0600141C RID: 5148 RVA: 0x0002E012 File Offset: 0x0002C212
		public DateTime? WhenCreatedUTC
		{
			get
			{
				return (DateTime?)this[RequestSchema.WhenCreatedUTC];
			}
			private set
			{
				this[RequestSchema.WhenCreatedUTC] = value;
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x0600141D RID: 5149 RVA: 0x0002E025 File Offset: 0x0002C225
		// (set) Token: 0x0600141E RID: 5150 RVA: 0x0002E037 File Offset: 0x0002C237
		internal string RemoteHostName
		{
			get
			{
				return (string)this[RequestSchema.RemoteHostName];
			}
			private set
			{
				this[RequestSchema.RemoteHostName] = value;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x0600141F RID: 5151 RVA: 0x0002E045 File Offset: 0x0002C245
		internal bool SuspendWhenReadyToComplete
		{
			get
			{
				return (this.Flags & RequestFlags.SuspendWhenReadyToComplete) != RequestFlags.None;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001420 RID: 5152 RVA: 0x0002E059 File Offset: 0x0002C259
		// (set) Token: 0x06001421 RID: 5153 RVA: 0x0002E06B File Offset: 0x0002C26B
		internal ADObjectId SourceDatabase
		{
			get
			{
				return (ADObjectId)this[RequestSchema.SourceDatabase];
			}
			private set
			{
				this[RequestSchema.SourceDatabase] = value;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06001422 RID: 5154 RVA: 0x0002E079 File Offset: 0x0002C279
		// (set) Token: 0x06001423 RID: 5155 RVA: 0x0002E08B File Offset: 0x0002C28B
		internal ADObjectId TargetDatabase
		{
			get
			{
				return (ADObjectId)this[RequestSchema.TargetDatabase];
			}
			private set
			{
				this[RequestSchema.TargetDatabase] = value;
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06001424 RID: 5156 RVA: 0x0002E099 File Offset: 0x0002C299
		// (set) Token: 0x06001425 RID: 5157 RVA: 0x0002E0AB File Offset: 0x0002C2AB
		internal string FilePath
		{
			get
			{
				return (string)this[RequestSchema.FilePath];
			}
			private set
			{
				this[RequestSchema.FilePath] = value;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x0002E0B9 File Offset: 0x0002C2B9
		// (set) Token: 0x06001427 RID: 5159 RVA: 0x0002E0CB File Offset: 0x0002C2CB
		internal ADObjectId SourceMailbox
		{
			get
			{
				return (ADObjectId)this[RequestSchema.SourceMailbox];
			}
			private set
			{
				this[RequestSchema.SourceMailbox] = value;
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x0002E0D9 File Offset: 0x0002C2D9
		// (set) Token: 0x06001429 RID: 5161 RVA: 0x0002E0EB File Offset: 0x0002C2EB
		internal ADObjectId TargetMailbox
		{
			get
			{
				return (ADObjectId)this[RequestSchema.TargetMailbox];
			}
			private set
			{
				this[RequestSchema.TargetMailbox] = value;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x0002E0F9 File Offset: 0x0002C2F9
		// (set) Token: 0x0600142B RID: 5163 RVA: 0x0002E101 File Offset: 0x0002C301
		internal MRSRequestType Type { get; private set; }

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x0002E10A File Offset: 0x0002C30A
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return RequestBase.schema;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x0600142D RID: 5165 RVA: 0x0002E111 File Offset: 0x0002C311
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x0002E118 File Offset: 0x0002C318
		public override string ToString()
		{
			if (this.RequestGuid != Guid.Empty)
			{
				return this.RequestGuid.ToString();
			}
			return base.ToString();
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x0002E154 File Offset: 0x0002C354
		internal virtual void Initialize(IRequestIndexEntry index)
		{
			if (index == null)
			{
				throw new ArgumentNullException("index");
			}
			this[SimpleProviderObjectSchema.Identity] = index.GetRequestIndexEntryId(this);
			this.Name = index.Name;
			this.Status = index.Status;
			this.Flags = index.Flags;
			this.RemoteHostName = index.RemoteHostName;
			this.SourceDatabase = index.SourceMDB;
			this.TargetDatabase = index.TargetMDB;
			this.FilePath = index.FilePath;
			this.SourceMailbox = index.SourceUserId;
			this.TargetMailbox = index.TargetUserId;
			this.RequestGuid = index.RequestGuid;
			this.RequestQueue = index.StorageMDB;
			this.OrganizationId = index.OrganizationId;
			this.BatchName = index.BatchName;
			this.Type = index.Type;
			this.WhenChanged = index.WhenChanged;
			this.WhenCreated = index.WhenCreated;
			this.WhenChangedUTC = index.WhenChangedUTC;
			this.WhenCreatedUTC = index.WhenCreatedUTC;
		}

		// Token: 0x04000A3A RID: 2618
		private static ObjectSchema schema = ObjectSchema.GetInstance<RequestSchema>();
	}
}
