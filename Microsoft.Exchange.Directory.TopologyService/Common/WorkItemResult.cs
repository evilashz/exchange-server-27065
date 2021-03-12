using System;

namespace Microsoft.Exchange.Directory.TopologyService.Common
{
	// Token: 0x0200002A RID: 42
	internal class WorkItemResult<T> : IWorkItemResult where T : class
	{
		// Token: 0x06000151 RID: 337 RVA: 0x0000AAD0 File Offset: 0x00008CD0
		public WorkItemResult(WorkItem<T> workItem)
		{
			if (workItem == null)
			{
				throw new ArgumentNullException("workItem");
			}
			this.WorkItemId = workItem.Id;
			this.WhenStarted = workItem.WhenStarted;
			this.WhenCompleted = workItem.WhenCompleted;
			this.ResultType = workItem.ResultType;
			this.Data = workItem.Data;
			this.Latency = (int)(this.WhenCompleted - this.WhenStarted).TotalMilliseconds;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000AB4D File Offset: 0x00008D4D
		public WorkItemResult(WorkItem<T> workItem, AggregateException e) : this(workItem)
		{
			this.Exception = null;
			if (e != null)
			{
				this.Exception = e.Flatten();
				this.Exception = this.Exception.InnerException;
				this.ResultType = ResultType.Failed;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000AB84 File Offset: 0x00008D84
		// (set) Token: 0x06000154 RID: 340 RVA: 0x0000AB8C File Offset: 0x00008D8C
		public T Data { get; private set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000AB95 File Offset: 0x00008D95
		// (set) Token: 0x06000156 RID: 342 RVA: 0x0000AB9D File Offset: 0x00008D9D
		public Exception Exception { get; private set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000157 RID: 343 RVA: 0x0000ABA6 File Offset: 0x00008DA6
		// (set) Token: 0x06000158 RID: 344 RVA: 0x0000ABAE File Offset: 0x00008DAE
		public string WorkItemId { get; private set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000159 RID: 345 RVA: 0x0000ABB7 File Offset: 0x00008DB7
		// (set) Token: 0x0600015A RID: 346 RVA: 0x0000ABBF File Offset: 0x00008DBF
		public ResultType ResultType { get; private set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600015B RID: 347 RVA: 0x0000ABC8 File Offset: 0x00008DC8
		// (set) Token: 0x0600015C RID: 348 RVA: 0x0000ABD0 File Offset: 0x00008DD0
		public int Latency { get; private set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600015D RID: 349 RVA: 0x0000ABD9 File Offset: 0x00008DD9
		// (set) Token: 0x0600015E RID: 350 RVA: 0x0000ABE1 File Offset: 0x00008DE1
		public DateTime WhenStarted { get; private set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600015F RID: 351 RVA: 0x0000ABEA File Offset: 0x00008DEA
		// (set) Token: 0x06000160 RID: 352 RVA: 0x0000ABF2 File Offset: 0x00008DF2
		public DateTime WhenCompleted { get; private set; }

		// Token: 0x06000161 RID: 353 RVA: 0x0000ABFC File Offset: 0x00008DFC
		public override string ToString()
		{
			string format = "WorkItemId: {0} ResultType: {1} Latency: {2} WorkItemException: {3} [Data : {4}]";
			object[] array = new object[5];
			array[0] = this.WorkItemId;
			array[1] = this.ResultType;
			array[2] = this.Latency;
			array[3] = ((this.Exception == null) ? "<NULL>" : this.Exception.ToString());
			object[] array2 = array;
			int num = 4;
			string text;
			if (this.Data != null)
			{
				T data = this.Data;
				text = data.ToString();
			}
			else
			{
				text = "<NULL>";
			}
			array2[num] = text;
			return string.Format(format, array);
		}
	}
}
