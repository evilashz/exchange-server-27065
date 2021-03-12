using System;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x02000018 RID: 24
	public abstract class WorkItemResult : IWorkData
	{
		// Token: 0x06000252 RID: 594 RVA: 0x00009E6A File Offset: 0x0000806A
		public WorkItemResult()
		{
			this.ExecutionStartTime = DateTime.MaxValue;
			this.ExecutionEndTime = DateTime.MaxValue;
			this.ResultType = ResultType.Abandoned;
			this.MachineName = Environment.MachineName;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00009E9C File Offset: 0x0000809C
		public WorkItemResult(WorkDefinition definition) : this()
		{
			this.MachineName = Settings.MachineName;
			this.PoisonedCount = definition.PoisonedCount;
			this.ExecutionId = definition.ExecutionId;
			this.ResultName = definition.ConstructWorkItemResultName();
			this.DeploymentId = definition.DeploymentId;
			this.WorkItemId = definition.Id;
			this.ServiceName = definition.ServiceName;
			this.DefinitionName = definition.Name;
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00009F0E File Offset: 0x0000810E
		// (set) Token: 0x06000255 RID: 597 RVA: 0x00009F16 File Offset: 0x00008116
		public virtual int ResultId { get; protected internal set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000256 RID: 598
		// (set) Token: 0x06000257 RID: 599
		public abstract string ServiceName { get; set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000258 RID: 600
		// (set) Token: 0x06000259 RID: 601
		public abstract bool IsNotified { get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600025A RID: 602
		// (set) Token: 0x0600025B RID: 603
		public abstract string ResultName { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600025C RID: 604
		// (set) Token: 0x0600025D RID: 605
		public abstract int WorkItemId { get; internal set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600025E RID: 606
		// (set) Token: 0x0600025F RID: 607
		public abstract int DeploymentId { get; internal set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000260 RID: 608
		// (set) Token: 0x06000261 RID: 609
		public abstract string MachineName { get; internal set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000262 RID: 610
		// (set) Token: 0x06000263 RID: 611
		public abstract string Error { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000264 RID: 612
		// (set) Token: 0x06000265 RID: 613
		public abstract string Exception { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000266 RID: 614
		// (set) Token: 0x06000267 RID: 615
		public abstract byte RetryCount { get; internal set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000268 RID: 616
		// (set) Token: 0x06000269 RID: 617
		public abstract string StateAttribute1 { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600026A RID: 618
		// (set) Token: 0x0600026B RID: 619
		public abstract string StateAttribute2 { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600026C RID: 620
		// (set) Token: 0x0600026D RID: 621
		public abstract string StateAttribute3 { get; set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600026E RID: 622
		// (set) Token: 0x0600026F RID: 623
		public abstract string StateAttribute4 { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000270 RID: 624
		// (set) Token: 0x06000271 RID: 625
		public abstract string StateAttribute5 { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000272 RID: 626
		// (set) Token: 0x06000273 RID: 627
		public abstract double StateAttribute6 { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000274 RID: 628
		// (set) Token: 0x06000275 RID: 629
		public abstract double StateAttribute7 { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000276 RID: 630
		// (set) Token: 0x06000277 RID: 631
		public abstract double StateAttribute8 { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000278 RID: 632
		// (set) Token: 0x06000279 RID: 633
		public abstract double StateAttribute9 { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600027A RID: 634
		// (set) Token: 0x0600027B RID: 635
		public abstract double StateAttribute10 { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600027C RID: 636
		// (set) Token: 0x0600027D RID: 637
		public abstract ResultType ResultType { get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600027E RID: 638
		// (set) Token: 0x0600027F RID: 639
		public abstract int ExecutionId { get; protected set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000280 RID: 640
		// (set) Token: 0x06000281 RID: 641
		public abstract DateTime ExecutionStartTime { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000282 RID: 642
		// (set) Token: 0x06000283 RID: 643
		public abstract DateTime ExecutionEndTime { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000284 RID: 644
		// (set) Token: 0x06000285 RID: 645
		public abstract byte PoisonedCount { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000286 RID: 646 RVA: 0x00009F1F File Offset: 0x0000811F
		public virtual string InternalStorageKey
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00009F26 File Offset: 0x00008126
		public virtual string ExternalStorageKey
		{
			get
			{
				return this.DefinitionName;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000288 RID: 648 RVA: 0x00009F30 File Offset: 0x00008130
		public virtual string SecondaryExternalStorageKey
		{
			get
			{
				return string.Format("{0}_{1}_{2}_{3}_{4}", new object[]
				{
					this.ExecutionEndTime.Ticks,
					this.WorkItemId,
					this.ResultId,
					Settings.InstanceName,
					Settings.MachineName
				});
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000289 RID: 649
		// (set) Token: 0x0600028A RID: 650
		internal abstract int Version { get; set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00009F91 File Offset: 0x00008191
		// (set) Token: 0x0600028C RID: 652 RVA: 0x00009F99 File Offset: 0x00008199
		internal string DefinitionName { get; private set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00009FA4 File Offset: 0x000081A4
		protected internal TracingContext TraceContext
		{
			get
			{
				return new TracingContext(null)
				{
					LId = this.WorkItemId,
					Id = this.ExecutionId
				};
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00009FD1 File Offset: 0x000081D1
		public virtual void AssignResultId()
		{
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00009FD4 File Offset: 0x000081D4
		public virtual void SetCompleted(ResultType resultType)
		{
			if (this.ExecutionStartTime == DateTime.MaxValue)
			{
				this.ExecutionStartTime = DateTime.UtcNow;
			}
			this.ExecutionEndTime = DateTime.UtcNow;
			this.ResultType = resultType;
			this.ResultName = this.TruncateStringProperty(this.ResultName, 1280);
			this.StateAttribute1 = this.TruncateStringProperty(this.StateAttribute1, 1024);
			this.StateAttribute2 = this.TruncateStringProperty(this.StateAttribute2, 1024);
			this.StateAttribute3 = this.TruncateStringProperty(this.StateAttribute3, 1024);
			this.StateAttribute4 = this.TruncateStringProperty(this.StateAttribute4, 1024);
			this.StateAttribute5 = this.TruncateStringProperty(this.StateAttribute5, 1024);
			if (string.IsNullOrEmpty(this.Error) && this.ResultType != ResultType.Succeeded)
			{
				this.Error = this.TruncateStringProperty(CoreResources.WorkItemFailedDefaultError(resultType.ToString()), 4000);
			}
			WTFDiagnostics.TraceDebug<ResultType>(WTFLog.Core, this.TraceContext, "[WorkItemResult.SetResult]: workitem completed with result: {0}", resultType, null, "SetCompleted", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\WorkItemResult.cs", 309);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000A0F8 File Offset: 0x000082F8
		public void SetCompleted(ResultType resultType, Exception e)
		{
			if (e is AggregateException)
			{
				e = ((AggregateException)e).Flatten().InnerException;
			}
			this.Exception = e.ToString();
			this.Exception = this.TruncateStringProperty(this.Exception, 4000);
			this.Error = e.Message;
			this.Error = this.TruncateStringProperty(this.Error, 4000);
			this.SetCompleted(resultType);
			WTFDiagnostics.TraceError<string>(WTFLog.Core, this.TraceContext, "[WorkItemResult.SetResult]: workitem completed with exception: {0}", this.Exception, null, "SetCompleted", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\WorkItemResult.cs", 331);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000A197 File Offset: 0x00008397
		public override string ToString()
		{
			return string.Format("{0}: {1}", base.ToString(), this.WorkItemId);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000A1B4 File Offset: 0x000083B4
		internal string TruncateStringProperty(string property, int maxLength)
		{
			if (!string.IsNullOrEmpty(property) && property.Length > maxLength)
			{
				property = property.Substring(0, maxLength);
			}
			return property;
		}

		// Token: 0x040000E3 RID: 227
		internal const int StateAttributeStringColumnSize = 1024;

		// Token: 0x040000E4 RID: 228
		private const int ErrorAttributeColumnSize = 4000;

		// Token: 0x040000E5 RID: 229
		private const int ResultNameColumnSize = 1280;
	}
}
