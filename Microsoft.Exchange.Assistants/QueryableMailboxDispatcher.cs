using System;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x020000AE RID: 174
	internal class QueryableMailboxDispatcher : QueryableObjectImplBase<QueryableMailboxDispatcherObjectSchema>
	{
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x0001A854 File Offset: 0x00018A54
		// (set) Token: 0x0600053F RID: 1343 RVA: 0x0001A866 File Offset: 0x00018A66
		public Guid MailboxGuid
		{
			get
			{
				return (Guid)this[QueryableMailboxDispatcherObjectSchema.MailboxGuid];
			}
			set
			{
				this[QueryableMailboxDispatcherObjectSchema.MailboxGuid] = value;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x0001A879 File Offset: 0x00018A79
		// (set) Token: 0x06000541 RID: 1345 RVA: 0x0001A88B File Offset: 0x00018A8B
		public long DecayedEventCounter
		{
			get
			{
				return (long)this[QueryableMailboxDispatcherObjectSchema.DecayedEventCounter];
			}
			set
			{
				this[QueryableMailboxDispatcherObjectSchema.DecayedEventCounter] = value;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x0001A89E File Offset: 0x00018A9E
		// (set) Token: 0x06000543 RID: 1347 RVA: 0x0001A8B0 File Offset: 0x00018AB0
		public int NumberOfActiveDispatchers
		{
			get
			{
				return (int)this[QueryableMailboxDispatcherObjectSchema.NumberOfActiveDispatchers];
			}
			set
			{
				this[QueryableMailboxDispatcherObjectSchema.NumberOfActiveDispatchers] = value;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x0001A8C3 File Offset: 0x00018AC3
		// (set) Token: 0x06000545 RID: 1349 RVA: 0x0001A8D5 File Offset: 0x00018AD5
		public bool IsMailboxDead
		{
			get
			{
				return (bool)this[QueryableMailboxDispatcherObjectSchema.IsMailboxDead];
			}
			set
			{
				this[QueryableMailboxDispatcherObjectSchema.IsMailboxDead] = value;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x0001A8E8 File Offset: 0x00018AE8
		// (set) Token: 0x06000547 RID: 1351 RVA: 0x0001A8FA File Offset: 0x00018AFA
		public bool IsIdle
		{
			get
			{
				return (bool)this[QueryableMailboxDispatcherObjectSchema.IsIdle];
			}
			set
			{
				this[QueryableMailboxDispatcherObjectSchema.IsIdle] = value;
			}
		}
	}
}
