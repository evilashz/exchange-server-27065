using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x0200097F RID: 2431
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PerformanceCounter
	{
		// Token: 0x170018C9 RID: 6345
		// (get) Token: 0x060059D8 RID: 23000 RVA: 0x0017499E File Offset: 0x00172B9E
		public Dictionary<OperationType, OperationCounter> OperationCounters
		{
			get
			{
				return this.operationCounters;
			}
		}

		// Token: 0x170018CA RID: 6346
		// (get) Token: 0x060059D9 RID: 23001 RVA: 0x001749A6 File Offset: 0x00172BA6
		public Dictionary<ChangeType, int> ChangeCounters
		{
			get
			{
				return this.changeCounters;
			}
		}

		// Token: 0x170018CB RID: 6347
		// (get) Token: 0x060059DA RID: 23002 RVA: 0x001749AE File Offset: 0x00172BAE
		// (set) Token: 0x060059DB RID: 23003 RVA: 0x001749B6 File Offset: 0x00172BB6
		public int CurrentIoOperations { get; set; }

		// Token: 0x060059DC RID: 23004 RVA: 0x001749BF File Offset: 0x00172BBF
		private void LazyInitialize(OperationType type)
		{
			if (!this.operationCounters.ContainsKey(type))
			{
				this.operationCounters.Add(type, new OperationCounter(type));
			}
		}

		// Token: 0x060059DD RID: 23005 RVA: 0x001749E1 File Offset: 0x00172BE1
		private void LazyInitialize(ChangeType type)
		{
			if (!this.changeCounters.ContainsKey(type))
			{
				this.changeCounters.Add(type, 0);
			}
		}

		// Token: 0x060059DE RID: 23006 RVA: 0x00174A00 File Offset: 0x00172C00
		public void Increment(ChangeType type)
		{
			this.LazyInitialize(type);
			Dictionary<ChangeType, int> dictionary;
			(dictionary = this.changeCounters)[type] = dictionary[type] + 1;
		}

		// Token: 0x060059DF RID: 23007 RVA: 0x00174A2D File Offset: 0x00172C2D
		public void Start(OperationType type)
		{
			this.LazyInitialize(type);
			this.operationCounters[type].StopWatch.Reset();
			this.operationCounters[type].StopWatch.Start();
		}

		// Token: 0x060059E0 RID: 23008 RVA: 0x00174A64 File Offset: 0x00172C64
		public void Stop(OperationType type, int ioOperations = 1)
		{
			this.operationCounters[type].StopWatch.Stop();
			this.operationCounters[type].Count++;
			this.operationCounters[type].TotalElapsedTime += this.operationCounters[type].StopWatch.Elapsed;
			if (this.operationCounters[type].StopWatch.Elapsed > this.operationCounters[type].MaximumElapsedTime)
			{
				this.operationCounters[type].MaximumElapsedTime = this.operationCounters[type].StopWatch.Elapsed;
			}
			if (type == OperationType.AddFile || type == OperationType.AddFolder || type == OperationType.DeleteItem || type == OperationType.UpdateFile || type == OperationType.UpdateFolder || type == OperationType.MoveFile)
			{
				this.CurrentIoOperations += ioOperations;
			}
		}

		// Token: 0x060059E1 RID: 23009 RVA: 0x00174B50 File Offset: 0x00172D50
		public string[] GetLogLine()
		{
			List<string> list = new List<string>();
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<ChangeType, int> keyValuePair in this.changeCounters)
			{
				stringBuilder.AppendFormat("{0}:{1}, ", keyValuePair.Key, keyValuePair.Value);
			}
			if (stringBuilder.Length > 0)
			{
				list.Add(stringBuilder.ToString());
			}
			foreach (KeyValuePair<OperationType, OperationCounter> keyValuePair2 in this.operationCounters)
			{
				list.Add(keyValuePair2.Value.GetLogLine());
			}
			return list.ToArray();
		}

		// Token: 0x0400316B RID: 12651
		private readonly Dictionary<OperationType, OperationCounter> operationCounters = new Dictionary<OperationType, OperationCounter>();

		// Token: 0x0400316C RID: 12652
		private readonly Dictionary<ChangeType, int> changeCounters = new Dictionary<ChangeType, int>();
	}
}
