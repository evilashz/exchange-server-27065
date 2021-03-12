using System;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x020000AC RID: 172
	internal class QueryableDatabaseManager : QueryableObjectImplBase<QueryableDatabaseManagerObjectSchema>
	{
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x0001A795 File Offset: 0x00018995
		// (set) Token: 0x06000534 RID: 1332 RVA: 0x0001A7A7 File Offset: 0x000189A7
		public string StartState
		{
			get
			{
				return (string)this[QueryableDatabaseManagerObjectSchema.StartState];
			}
			set
			{
				this[QueryableDatabaseManagerObjectSchema.StartState] = value;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x0001A7B5 File Offset: 0x000189B5
		// (set) Token: 0x06000536 RID: 1334 RVA: 0x0001A7C7 File Offset: 0x000189C7
		public bool IsStopping
		{
			get
			{
				return (bool)this[QueryableDatabaseManagerObjectSchema.IsStopping];
			}
			set
			{
				this[QueryableDatabaseManagerObjectSchema.IsStopping] = value;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x0001A7DA File Offset: 0x000189DA
		// (set) Token: 0x06000538 RID: 1336 RVA: 0x0001A7EC File Offset: 0x000189EC
		public QueryableThrottle Throttle
		{
			get
			{
				return (QueryableThrottle)this[QueryableDatabaseManagerObjectSchema.Throttle];
			}
			set
			{
				this[QueryableDatabaseManagerObjectSchema.Throttle] = value;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x0001A7FA File Offset: 0x000189FA
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x0001A80C File Offset: 0x00018A0C
		public QueryableThrottleGovernor Governor
		{
			get
			{
				return (QueryableThrottleGovernor)this[QueryableDatabaseManagerObjectSchema.Governor];
			}
			set
			{
				this[QueryableDatabaseManagerObjectSchema.Governor] = value;
			}
		}
	}
}
