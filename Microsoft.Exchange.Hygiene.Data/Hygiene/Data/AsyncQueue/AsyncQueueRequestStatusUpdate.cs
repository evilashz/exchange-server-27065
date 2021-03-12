using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.AsyncQueue
{
	// Token: 0x02000023 RID: 35
	internal class AsyncQueueRequestStatusUpdate : ConfigurablePropertyBag
	{
		// Token: 0x0600011F RID: 287 RVA: 0x00004736 File Offset: 0x00002936
		public AsyncQueueRequestStatusUpdate(Guid organizationalUnitRoot, Guid requestId)
		{
			this.OrganizationalUnitRoot = organizationalUnitRoot;
			this.RequestId = requestId;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000474C File Offset: 0x0000294C
		public AsyncQueueRequestStatusUpdate(Guid organizationalUnitRoot, Guid requestId, Guid requestStepId) : this(organizationalUnitRoot, requestId)
		{
			this.RequestStepId = requestStepId;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000475D File Offset: 0x0000295D
		private AsyncQueueRequestStatusUpdate()
		{
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00004768 File Offset: 0x00002968
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.RequestId.ToString());
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000123 RID: 291 RVA: 0x0000478E File Offset: 0x0000298E
		// (set) Token: 0x06000124 RID: 292 RVA: 0x000047A0 File Offset: 0x000029A0
		public Guid RequestId
		{
			get
			{
				return (Guid)this[AsyncQueueRequestStatusUpdateSchema.RequestIdProperty];
			}
			set
			{
				this[AsyncQueueRequestStatusUpdateSchema.RequestIdProperty] = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000125 RID: 293 RVA: 0x000047B3 File Offset: 0x000029B3
		// (set) Token: 0x06000126 RID: 294 RVA: 0x000047C5 File Offset: 0x000029C5
		public Guid OrganizationalUnitRoot
		{
			get
			{
				return (Guid)this[AsyncQueueRequestStatusUpdateSchema.OrganizationalUnitRootProperty];
			}
			set
			{
				this[AsyncQueueRequestStatusUpdateSchema.OrganizationalUnitRootProperty] = value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000127 RID: 295 RVA: 0x000047D8 File Offset: 0x000029D8
		// (set) Token: 0x06000128 RID: 296 RVA: 0x000047EA File Offset: 0x000029EA
		public Guid RequestStepId
		{
			get
			{
				return (Guid)this[AsyncQueueRequestStatusUpdateSchema.RequestStepIdProperty];
			}
			set
			{
				this[AsyncQueueRequestStatusUpdateSchema.RequestStepIdProperty] = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000129 RID: 297 RVA: 0x000047FD File Offset: 0x000029FD
		// (set) Token: 0x0600012A RID: 298 RVA: 0x0000480F File Offset: 0x00002A0F
		public AsyncQueueStatus CurrentStatus
		{
			get
			{
				return (AsyncQueueStatus)this[AsyncQueueRequestStatusUpdateSchema.CurrentStatusProperty];
			}
			set
			{
				this[AsyncQueueRequestStatusUpdateSchema.CurrentStatusProperty] = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00004822 File Offset: 0x00002A22
		// (set) Token: 0x0600012C RID: 300 RVA: 0x00004834 File Offset: 0x00002A34
		public AsyncQueueStatus Status
		{
			get
			{
				return (AsyncQueueStatus)this[AsyncQueueRequestStatusUpdateSchema.StatusProperty];
			}
			set
			{
				this[AsyncQueueRequestStatusUpdateSchema.StatusProperty] = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00004847 File Offset: 0x00002A47
		// (set) Token: 0x0600012E RID: 302 RVA: 0x00004859 File Offset: 0x00002A59
		public string Cookie
		{
			get
			{
				return (string)this[AsyncQueueRequestStatusUpdateSchema.CookieProperty];
			}
			set
			{
				this[AsyncQueueRequestStatusUpdateSchema.CookieProperty] = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00004867 File Offset: 0x00002A67
		// (set) Token: 0x06000130 RID: 304 RVA: 0x00004879 File Offset: 0x00002A79
		public string ProcessInstanceName
		{
			get
			{
				return (string)this[AsyncQueueRequestStatusUpdateSchema.ProcessInstanceNameProperty];
			}
			set
			{
				this[AsyncQueueRequestStatusUpdateSchema.ProcessInstanceNameProperty] = value;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00004887 File Offset: 0x00002A87
		// (set) Token: 0x06000132 RID: 306 RVA: 0x00004899 File Offset: 0x00002A99
		public int RetryInterval
		{
			get
			{
				return (int)this[AsyncQueueRequestStatusUpdateSchema.RetryIntervalProperty];
			}
			set
			{
				this[AsyncQueueRequestStatusUpdateSchema.RetryIntervalProperty] = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000133 RID: 307 RVA: 0x000048AC File Offset: 0x00002AAC
		public bool RequestComplete
		{
			get
			{
				return (bool)this[AsyncQueueRequestStatusUpdateSchema.RequestCompleteProperty];
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000134 RID: 308 RVA: 0x000048BE File Offset: 0x00002ABE
		public AsyncQueueStatus RequestStatus
		{
			get
			{
				return (AsyncQueueStatus)this[AsyncQueueRequestStatusUpdateSchema.RequestStatusProperty];
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000135 RID: 309 RVA: 0x000048D0 File Offset: 0x00002AD0
		public DateTime? RequestStartDatetime
		{
			get
			{
				return (DateTime?)this[AsyncQueueRequestStatusUpdateSchema.RequestStartDatetimeProperty];
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000136 RID: 310 RVA: 0x000048E2 File Offset: 0x00002AE2
		public DateTime? RequestEndDatetime
		{
			get
			{
				return (DateTime?)this[AsyncQueueRequestStatusUpdateSchema.RequestEndDatetimeProperty];
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000048F4 File Offset: 0x00002AF4
		public override Type GetSchemaType()
		{
			return typeof(AsyncQueueRequestStatusUpdateSchema);
		}
	}
}
