using System;
using System.Diagnostics;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x02000980 RID: 2432
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class OperationCounter
	{
		// Token: 0x170018CC RID: 6348
		// (get) Token: 0x060059E3 RID: 23011 RVA: 0x00174C56 File Offset: 0x00172E56
		// (set) Token: 0x060059E4 RID: 23012 RVA: 0x00174C5E File Offset: 0x00172E5E
		public Stopwatch StopWatch { get; private set; }

		// Token: 0x170018CD RID: 6349
		// (get) Token: 0x060059E5 RID: 23013 RVA: 0x00174C67 File Offset: 0x00172E67
		// (set) Token: 0x060059E6 RID: 23014 RVA: 0x00174C6F File Offset: 0x00172E6F
		public OperationType Name { get; private set; }

		// Token: 0x170018CE RID: 6350
		// (get) Token: 0x060059E7 RID: 23015 RVA: 0x00174C78 File Offset: 0x00172E78
		// (set) Token: 0x060059E8 RID: 23016 RVA: 0x00174C80 File Offset: 0x00172E80
		public int Count { get; set; }

		// Token: 0x170018CF RID: 6351
		// (get) Token: 0x060059E9 RID: 23017 RVA: 0x00174C89 File Offset: 0x00172E89
		// (set) Token: 0x060059EA RID: 23018 RVA: 0x00174C91 File Offset: 0x00172E91
		public TimeSpan TotalElapsedTime { get; set; }

		// Token: 0x170018D0 RID: 6352
		// (get) Token: 0x060059EB RID: 23019 RVA: 0x00174C9A File Offset: 0x00172E9A
		// (set) Token: 0x060059EC RID: 23020 RVA: 0x00174CA2 File Offset: 0x00172EA2
		public TimeSpan MaximumElapsedTime { get; set; }

		// Token: 0x170018D1 RID: 6353
		// (get) Token: 0x060059ED RID: 23021 RVA: 0x00174CAC File Offset: 0x00172EAC
		public TimeSpan AveragedElapsedTime
		{
			get
			{
				if (this.Count <= 0)
				{
					return TimeSpan.FromMilliseconds(0.0);
				}
				return TimeSpan.FromMilliseconds((double)((int)this.TotalElapsedTime.TotalMilliseconds / this.Count));
			}
		}

		// Token: 0x060059EE RID: 23022 RVA: 0x00174CF0 File Offset: 0x00172EF0
		public string GetLogLine()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Name:{0}, Count:{1}, TotalElaspedTime:{2}, AverageElapsedTime:{3}, MaxElapsedTime:{4} ", new object[]
			{
				this.Name,
				this.Count,
				this.TotalElapsedTime,
				this.AveragedElapsedTime,
				this.MaximumElapsedTime
			});
			return stringBuilder.ToString();
		}

		// Token: 0x060059EF RID: 23023 RVA: 0x00174D63 File Offset: 0x00172F63
		public OperationCounter(OperationType name)
		{
			this.Name = name;
			this.StopWatch = new Stopwatch();
		}
	}
}
