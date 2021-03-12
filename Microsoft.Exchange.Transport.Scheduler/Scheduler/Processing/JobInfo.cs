using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Scheduler.Contracts;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x02000019 RID: 25
	internal sealed class JobInfo
	{
		// Token: 0x06000066 RID: 102 RVA: 0x00003499 File Offset: 0x00001699
		public JobInfo(DateTime startTime, IEnumerable<IMessageScope> scopes)
		{
			ArgumentValidator.ThrowIfNull("scopes", scopes);
			this.id = Guid.NewGuid();
			this.startTime = startTime;
			this.scopes = scopes;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000034C5 File Offset: 0x000016C5
		// (set) Token: 0x06000068 RID: 104 RVA: 0x000034CD File Offset: 0x000016CD
		public Task ExecutingTask { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000034D6 File Offset: 0x000016D6
		public Guid Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000034DE File Offset: 0x000016DE
		public DateTime StartTime
		{
			get
			{
				return this.startTime;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000034E6 File Offset: 0x000016E6
		public IEnumerable<IMessageScope> Scopes
		{
			get
			{
				return this.scopes;
			}
		}

		// Token: 0x0400003D RID: 61
		private readonly Guid id;

		// Token: 0x0400003E RID: 62
		private readonly DateTime startTime;

		// Token: 0x0400003F RID: 63
		private readonly IEnumerable<IMessageScope> scopes;
	}
}
