using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000ED RID: 237
	internal class OnDemandQueryRequest : ConfigurablePropertyBag
	{
		// Token: 0x0600093F RID: 2367 RVA: 0x0001D019 File Offset: 0x0001B219
		public OnDemandQueryRequest(Guid tenantId, Guid requestId) : this()
		{
			this.TenantId = tenantId;
			this.RequestId = requestId;
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0001D02F File Offset: 0x0001B22F
		public OnDemandQueryRequest()
		{
			this[ADObjectSchema.Id] = CombGuidGenerator.NewGuid();
			this[OnDemandQueryRequestSchema.Container] = OnDemandQueryRequestStatus.NotStarted.ToString();
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x0001D064 File Offset: 0x0001B264
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.RequestId.ToString());
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x0001D08A File Offset: 0x0001B28A
		// (set) Token: 0x06000943 RID: 2371 RVA: 0x0001D09C File Offset: 0x0001B29C
		public Guid TenantId
		{
			get
			{
				return (Guid)this[OnDemandQueryRequestSchema.TenantId];
			}
			set
			{
				this[OnDemandQueryRequestSchema.TenantId] = value;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x0001D0AF File Offset: 0x0001B2AF
		// (set) Token: 0x06000945 RID: 2373 RVA: 0x0001D0C6 File Offset: 0x0001B2C6
		public Guid RequestId
		{
			get
			{
				return Guid.Parse((string)this[OnDemandQueryRequestSchema.RequestId]);
			}
			set
			{
				this[OnDemandQueryRequestSchema.RequestId] = value.ToString();
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x0001D0E0 File Offset: 0x0001B2E0
		// (set) Token: 0x06000947 RID: 2375 RVA: 0x0001D0F2 File Offset: 0x0001B2F2
		public DateTime SubmissionTime
		{
			get
			{
				return (DateTime)this[OnDemandQueryRequestSchema.SubmissionTime];
			}
			set
			{
				this[OnDemandQueryRequestSchema.SubmissionTime] = value;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x0001D105 File Offset: 0x0001B305
		// (set) Token: 0x06000949 RID: 2377 RVA: 0x0001D117 File Offset: 0x0001B317
		public OnDemandQueryRequestStatus RequestStatus
		{
			get
			{
				return (OnDemandQueryRequestStatus)this[OnDemandQueryRequestSchema.RequestStatus];
			}
			set
			{
				this[OnDemandQueryRequestSchema.RequestStatus] = value;
				this[OnDemandQueryRequestSchema.Container] = value.ToString();
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x0001D140 File Offset: 0x0001B340
		// (set) Token: 0x0600094B RID: 2379 RVA: 0x0001D152 File Offset: 0x0001B352
		public string QueryDefinition
		{
			get
			{
				return (string)this[OnDemandQueryRequestSchema.QueryDefinition];
			}
			set
			{
				this[OnDemandQueryRequestSchema.QueryDefinition] = value;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x0001D160 File Offset: 0x0001B360
		// (set) Token: 0x0600094D RID: 2381 RVA: 0x0001D172 File Offset: 0x0001B372
		public Guid? BatchId
		{
			get
			{
				return (Guid?)this[OnDemandQueryRequestSchema.BatchId];
			}
			set
			{
				this[OnDemandQueryRequestSchema.BatchId] = value;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x0001D185 File Offset: 0x0001B385
		// (set) Token: 0x0600094F RID: 2383 RVA: 0x0001D197 File Offset: 0x0001B397
		public string Region
		{
			get
			{
				return (string)this[OnDemandQueryRequestSchema.Region];
			}
			set
			{
				this[OnDemandQueryRequestSchema.Region] = value;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x0001D1A5 File Offset: 0x0001B3A5
		// (set) Token: 0x06000951 RID: 2385 RVA: 0x0001D1B7 File Offset: 0x0001B3B7
		public string QuerySubject
		{
			get
			{
				return (string)this[OnDemandQueryRequestSchema.QuerySubject];
			}
			set
			{
				this[OnDemandQueryRequestSchema.QuerySubject] = value;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000952 RID: 2386 RVA: 0x0001D1C5 File Offset: 0x0001B3C5
		// (set) Token: 0x06000953 RID: 2387 RVA: 0x0001D1D7 File Offset: 0x0001B3D7
		public OnDemandQueryType QueryType
		{
			get
			{
				return (OnDemandQueryType)this[OnDemandQueryRequestSchema.QueryType];
			}
			set
			{
				this[OnDemandQueryRequestSchema.QueryType] = value;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x0001D1EA File Offset: 0x0001B3EA
		// (set) Token: 0x06000955 RID: 2389 RVA: 0x0001D1FC File Offset: 0x0001B3FC
		public OnDemandQueryPriority QueryPriority
		{
			get
			{
				return (OnDemandQueryPriority)this[OnDemandQueryRequestSchema.QueryPriority];
			}
			set
			{
				this[OnDemandQueryRequestSchema.QueryPriority] = value;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x0001D20F File Offset: 0x0001B40F
		// (set) Token: 0x06000957 RID: 2391 RVA: 0x0001D221 File Offset: 0x0001B421
		public OnDemandQueryCallerType CallerType
		{
			get
			{
				return (OnDemandQueryCallerType)this[OnDemandQueryRequestSchema.CallerType];
			}
			set
			{
				this[OnDemandQueryRequestSchema.CallerType] = value;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x0001D234 File Offset: 0x0001B434
		// (set) Token: 0x06000959 RID: 2393 RVA: 0x0001D246 File Offset: 0x0001B446
		public long ResultSize
		{
			get
			{
				return (long)this[OnDemandQueryRequestSchema.ResultSize];
			}
			set
			{
				this[OnDemandQueryRequestSchema.ResultSize] = value;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x0001D259 File Offset: 0x0001B459
		// (set) Token: 0x0600095B RID: 2395 RVA: 0x0001D26B File Offset: 0x0001B46B
		public int MatchRowCounts
		{
			get
			{
				return (int)this[OnDemandQueryRequestSchema.MatchRowCounts];
			}
			set
			{
				this[OnDemandQueryRequestSchema.MatchRowCounts] = value;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x0001D27E File Offset: 0x0001B47E
		// (set) Token: 0x0600095D RID: 2397 RVA: 0x0001D290 File Offset: 0x0001B490
		public int ResultRowCounts
		{
			get
			{
				return (int)this[OnDemandQueryRequestSchema.ResultRowCounts];
			}
			set
			{
				this[OnDemandQueryRequestSchema.ResultRowCounts] = value;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x0001D2A3 File Offset: 0x0001B4A3
		// (set) Token: 0x0600095F RID: 2399 RVA: 0x0001D2B5 File Offset: 0x0001B4B5
		public int ViewCounts
		{
			get
			{
				return (int)this[OnDemandQueryRequestSchema.ViewCounts];
			}
			set
			{
				this[OnDemandQueryRequestSchema.ViewCounts] = value;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x0001D2C8 File Offset: 0x0001B4C8
		// (set) Token: 0x06000961 RID: 2401 RVA: 0x0001D2DA File Offset: 0x0001B4DA
		public string CosmosResultUri
		{
			get
			{
				return (string)this[OnDemandQueryRequestSchema.CosmosResultUri];
			}
			set
			{
				this[OnDemandQueryRequestSchema.CosmosResultUri] = value;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x0001D2E8 File Offset: 0x0001B4E8
		// (set) Token: 0x06000963 RID: 2403 RVA: 0x0001D2FA File Offset: 0x0001B4FA
		public Guid? CosmosJobId
		{
			get
			{
				return (Guid?)this[OnDemandQueryRequestSchema.CosmosJobId];
			}
			set
			{
				this[OnDemandQueryRequestSchema.CosmosJobId] = value;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x0001D30D File Offset: 0x0001B50D
		// (set) Token: 0x06000965 RID: 2405 RVA: 0x0001D31F File Offset: 0x0001B51F
		public int InBatchQueryId
		{
			get
			{
				return (int)this[OnDemandQueryRequestSchema.InBatchQueryId];
			}
			set
			{
				this[OnDemandQueryRequestSchema.InBatchQueryId] = value;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x0001D332 File Offset: 0x0001B532
		// (set) Token: 0x06000967 RID: 2407 RVA: 0x0001D344 File Offset: 0x0001B544
		public string NotificationEmail
		{
			get
			{
				return (string)this[OnDemandQueryRequestSchema.NotificationEmail];
			}
			set
			{
				this[OnDemandQueryRequestSchema.NotificationEmail] = value;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x0001D352 File Offset: 0x0001B552
		// (set) Token: 0x06000969 RID: 2409 RVA: 0x0001D364 File Offset: 0x0001B564
		public int RetryCount
		{
			get
			{
				return (int)this[OnDemandQueryRequestSchema.RetryCount];
			}
			set
			{
				this[OnDemandQueryRequestSchema.RetryCount] = value;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x0001D377 File Offset: 0x0001B577
		// (set) Token: 0x0600096B RID: 2411 RVA: 0x0001D389 File Offset: 0x0001B589
		public string ResultLocale
		{
			get
			{
				return (string)this[OnDemandQueryRequestSchema.ResultLocale];
			}
			set
			{
				this[OnDemandQueryRequestSchema.ResultLocale] = value;
			}
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0001D397 File Offset: 0x0001B597
		public override Type GetSchemaType()
		{
			return typeof(OnDemandQueryRequestSchema);
		}

		// Token: 0x040004D3 RID: 1235
		public static string DefaultContainer = OnDemandQueryRequestStatus.NotStarted.ToString();
	}
}
