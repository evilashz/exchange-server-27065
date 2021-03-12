using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Hygiene.Data.AsyncQueue
{
	// Token: 0x0200001F RID: 31
	internal class AsyncQueueRequest : ConfigurablePropertyBag
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x00004336 File Offset: 0x00002536
		public AsyncQueueRequest(string ownerId, Guid organizationalUnitRoot) : this(ownerId, organizationalUnitRoot, AsyncQueueRequest.defaultPriority)
		{
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004345 File Offset: 0x00002545
		public AsyncQueueRequest(string ownerId, Guid organizationalUnitRoot, AsyncQueuePriority priority) : this()
		{
			this.OwnerId = ownerId;
			this.OrganizationalUnitRoot = organizationalUnitRoot;
			this.Priority = priority;
			this.RequestStatus = AsyncQueueStatus.NotStarted;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000436A File Offset: 0x0000256A
		public AsyncQueueRequest()
		{
			this.RequestId = CombGuidGenerator.NewGuid();
			this.Steps = new MultiValuedProperty<AsyncQueueStep>();
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00004388 File Offset: 0x00002588
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.RequestId.ToString());
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x000043AE File Offset: 0x000025AE
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x000043C0 File Offset: 0x000025C0
		public AsyncQueuePriority Priority
		{
			get
			{
				return (AsyncQueuePriority)this[AsyncQueueRequestSchema.PriorityProperty];
			}
			set
			{
				this[AsyncQueueRequestSchema.PriorityProperty] = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000043D3 File Offset: 0x000025D3
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x000043E5 File Offset: 0x000025E5
		public Guid RequestId
		{
			get
			{
				return (Guid)this[AsyncQueueRequestSchema.RequestIdProperty];
			}
			private set
			{
				this[AsyncQueueRequestSchema.RequestIdProperty] = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000FA RID: 250 RVA: 0x000043F8 File Offset: 0x000025F8
		// (set) Token: 0x060000FB RID: 251 RVA: 0x0000440A File Offset: 0x0000260A
		public AsyncQueueFlags RequestFlags
		{
			get
			{
				return (AsyncQueueFlags)this[AsyncQueueRequestSchema.RequestFlagsProperty];
			}
			set
			{
				this[AsyncQueueRequestSchema.RequestFlagsProperty] = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000FC RID: 252 RVA: 0x0000441D File Offset: 0x0000261D
		// (set) Token: 0x060000FD RID: 253 RVA: 0x0000442F File Offset: 0x0000262F
		public string OwnerId
		{
			get
			{
				return (string)this[AsyncQueueRequestSchema.OwnerIdProperty];
			}
			set
			{
				this[AsyncQueueRequestSchema.OwnerIdProperty] = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000FE RID: 254 RVA: 0x0000443D File Offset: 0x0000263D
		// (set) Token: 0x060000FF RID: 255 RVA: 0x0000444F File Offset: 0x0000264F
		public string FriendlyName
		{
			get
			{
				return (string)this[AsyncQueueRequestSchema.FriendlyNameProperty];
			}
			set
			{
				this[AsyncQueueRequestSchema.FriendlyNameProperty] = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000100 RID: 256 RVA: 0x0000445D File Offset: 0x0000265D
		// (set) Token: 0x06000101 RID: 257 RVA: 0x0000446F File Offset: 0x0000266F
		public Guid OrganizationalUnitRoot
		{
			get
			{
				return (Guid)this[AsyncQueueRequestSchema.OrganizationalUnitRootProperty];
			}
			set
			{
				this[AsyncQueueRequestSchema.OrganizationalUnitRootProperty] = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00004482 File Offset: 0x00002682
		// (set) Token: 0x06000103 RID: 259 RVA: 0x00004494 File Offset: 0x00002694
		public Guid? DependantOrganizationalUnitRoot
		{
			get
			{
				return (Guid?)this[AsyncQueueRequestSchema.DependantOrganizationalUnitRootProperty];
			}
			set
			{
				this[AsyncQueueRequestSchema.DependantOrganizationalUnitRootProperty] = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000104 RID: 260 RVA: 0x000044A7 File Offset: 0x000026A7
		// (set) Token: 0x06000105 RID: 261 RVA: 0x000044B9 File Offset: 0x000026B9
		public Guid? DependantRequestId
		{
			get
			{
				return (Guid?)this[AsyncQueueRequestSchema.DependantRequestIdProperty];
			}
			set
			{
				this[AsyncQueueRequestSchema.DependantRequestIdProperty] = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000106 RID: 262 RVA: 0x000044CC File Offset: 0x000026CC
		// (set) Token: 0x06000107 RID: 263 RVA: 0x000044DE File Offset: 0x000026DE
		public AsyncQueueStatus RequestStatus
		{
			get
			{
				return (AsyncQueueStatus)this[AsyncQueueRequestSchema.RequestStatusProperty];
			}
			set
			{
				this[AsyncQueueRequestSchema.RequestStatusProperty] = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000108 RID: 264 RVA: 0x000044F1 File Offset: 0x000026F1
		public DateTime CreationTime
		{
			get
			{
				return (DateTime)this[AsyncQueueRequestSchema.CreatedDatetimeProperty];
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00004503 File Offset: 0x00002703
		public DateTime LastModifiedTime
		{
			get
			{
				return (DateTime)this[AsyncQueueRequestSchema.LastModifiedDatetimeProperty];
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00004515 File Offset: 0x00002715
		// (set) Token: 0x0600010B RID: 267 RVA: 0x00004527 File Offset: 0x00002727
		public bool RejectIfExists
		{
			get
			{
				return (bool)this[AsyncQueueRequestSchema.RejectIfExistsProperty];
			}
			set
			{
				this[AsyncQueueRequestSchema.RejectIfExistsProperty] = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600010C RID: 268 RVA: 0x0000453A File Offset: 0x0000273A
		// (set) Token: 0x0600010D RID: 269 RVA: 0x0000454C File Offset: 0x0000274C
		public bool FailIfDependencyMissing
		{
			get
			{
				return (bool)this[AsyncQueueRequestSchema.FailIfDependencyMissingProperty];
			}
			set
			{
				this[AsyncQueueRequestSchema.FailIfDependencyMissingProperty] = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600010E RID: 270 RVA: 0x0000455F File Offset: 0x0000275F
		// (set) Token: 0x0600010F RID: 271 RVA: 0x00004571 File Offset: 0x00002771
		public string Cookie
		{
			get
			{
				return (string)this[AsyncQueueRequestSchema.RequestCookieProperty];
			}
			set
			{
				this[AsyncQueueRequestSchema.RequestCookieProperty] = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000110 RID: 272 RVA: 0x0000457F File Offset: 0x0000277F
		// (set) Token: 0x06000111 RID: 273 RVA: 0x00004591 File Offset: 0x00002791
		public MultiValuedProperty<AsyncQueueStep> Steps
		{
			get
			{
				return (MultiValuedProperty<AsyncQueueStep>)this[AsyncQueueRequestSchema.AsyncQueueStepsProperty];
			}
			set
			{
				this[AsyncQueueRequestSchema.AsyncQueueStepsProperty] = value;
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000459F File Offset: 0x0000279F
		public void Add(AsyncQueueStep step)
		{
			if (step == null)
			{
				throw new ArgumentNullException("step");
			}
			step.RequestId = this.RequestId;
			step.OrganizationalUnitRoot = this.OrganizationalUnitRoot;
			this.Steps.Add(step);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000045D3 File Offset: 0x000027D3
		public override Type GetSchemaType()
		{
			return typeof(AsyncQueueRequestSchema);
		}

		// Token: 0x0400007D RID: 125
		private static readonly AsyncQueuePriority defaultPriority = AsyncQueuePriority.Low;
	}
}
