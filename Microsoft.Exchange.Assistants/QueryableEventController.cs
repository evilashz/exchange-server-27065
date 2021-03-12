using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x020000A8 RID: 168
	internal class QueryableEventController : QueryableObjectImplBase<QueryableEventControllerObjectSchema>
	{
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x0001A376 File Offset: 0x00018576
		// (set) Token: 0x06000502 RID: 1282 RVA: 0x0001A388 File Offset: 0x00018588
		public string ShutdownState
		{
			get
			{
				return (string)this[QueryableEventControllerObjectSchema.ShutdownState];
			}
			set
			{
				this[QueryableEventControllerObjectSchema.ShutdownState] = value;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x0001A396 File Offset: 0x00018596
		// (set) Token: 0x06000504 RID: 1284 RVA: 0x0001A3A8 File Offset: 0x000185A8
		public DateTime TimeToSaveWatermarks
		{
			get
			{
				return (DateTime)this[QueryableEventControllerObjectSchema.TimeToSaveWatermarks];
			}
			set
			{
				this[QueryableEventControllerObjectSchema.TimeToSaveWatermarks] = value;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x0001A3BB File Offset: 0x000185BB
		// (set) Token: 0x06000506 RID: 1286 RVA: 0x0001A3CD File Offset: 0x000185CD
		public long HighestEventPolled
		{
			get
			{
				return (long)this[QueryableEventControllerObjectSchema.HighestEventPolled];
			}
			set
			{
				this[QueryableEventControllerObjectSchema.HighestEventPolled] = value;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x0001A3E0 File Offset: 0x000185E0
		// (set) Token: 0x06000508 RID: 1288 RVA: 0x0001A3F2 File Offset: 0x000185F2
		public long NumberEventsInQueueCurrent
		{
			get
			{
				return (long)this[QueryableEventControllerObjectSchema.NumberEventsInQueueCurrent];
			}
			set
			{
				this[QueryableEventControllerObjectSchema.NumberEventsInQueueCurrent] = value;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x0001A405 File Offset: 0x00018605
		// (set) Token: 0x0600050A RID: 1290 RVA: 0x0001A417 File Offset: 0x00018617
		public string EventFilter
		{
			get
			{
				return (string)this[QueryableEventControllerObjectSchema.EventFilter];
			}
			set
			{
				this[QueryableEventControllerObjectSchema.EventFilter] = value;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x0001A425 File Offset: 0x00018625
		// (set) Token: 0x0600050C RID: 1292 RVA: 0x0001A437 File Offset: 0x00018637
		public bool RestartRequired
		{
			get
			{
				return (bool)this[QueryableEventControllerObjectSchema.RestartRequired];
			}
			set
			{
				this[QueryableEventControllerObjectSchema.RestartRequired] = value;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x0001A44A File Offset: 0x0001864A
		// (set) Token: 0x0600050E RID: 1294 RVA: 0x0001A45C File Offset: 0x0001865C
		public DateTime TimeToUpdateIdleWatermarks
		{
			get
			{
				return (DateTime)this[QueryableEventControllerObjectSchema.TimeToUpdateIdleWatermarks];
			}
			set
			{
				this[QueryableEventControllerObjectSchema.TimeToUpdateIdleWatermarks] = value;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x0001A46F File Offset: 0x0001866F
		// (set) Token: 0x06000510 RID: 1296 RVA: 0x0001A481 File Offset: 0x00018681
		public MultiValuedProperty<Guid> ActiveMailboxes
		{
			get
			{
				return (MultiValuedProperty<Guid>)this[QueryableEventControllerObjectSchema.ActiveMailboxes];
			}
			set
			{
				this[QueryableEventControllerObjectSchema.ActiveMailboxes] = value;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x0001A48F File Offset: 0x0001868F
		// (set) Token: 0x06000512 RID: 1298 RVA: 0x0001A4A1 File Offset: 0x000186A1
		public MultiValuedProperty<Guid> UpToDateMailboxes
		{
			get
			{
				return (MultiValuedProperty<Guid>)this[QueryableEventControllerObjectSchema.UpToDateMailboxes];
			}
			set
			{
				this[QueryableEventControllerObjectSchema.UpToDateMailboxes] = value;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x0001A4AF File Offset: 0x000186AF
		// (set) Token: 0x06000514 RID: 1300 RVA: 0x0001A4C1 File Offset: 0x000186C1
		public MultiValuedProperty<Guid> DeadMailboxes
		{
			get
			{
				return (MultiValuedProperty<Guid>)this[QueryableEventControllerObjectSchema.DeadMailboxes];
			}
			set
			{
				this[QueryableEventControllerObjectSchema.DeadMailboxes] = value;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x0001A4CF File Offset: 0x000186CF
		// (set) Token: 0x06000516 RID: 1302 RVA: 0x0001A4E1 File Offset: 0x000186E1
		public MultiValuedProperty<Guid> RecoveryEventDispatcheres
		{
			get
			{
				return (MultiValuedProperty<Guid>)this[QueryableEventControllerObjectSchema.RecoveryEventDispatcheres];
			}
			set
			{
				this[QueryableEventControllerObjectSchema.RecoveryEventDispatcheres] = value;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x0001A4EF File Offset: 0x000186EF
		// (set) Token: 0x06000518 RID: 1304 RVA: 0x0001A501 File Offset: 0x00018701
		public QueryableThrottleGovernor Governor
		{
			get
			{
				return (QueryableThrottleGovernor)this[QueryableEventControllerObjectSchema.Governor];
			}
			set
			{
				this[QueryableEventControllerObjectSchema.Governor] = value;
			}
		}
	}
}
