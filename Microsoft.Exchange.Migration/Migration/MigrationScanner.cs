using System;
using System.Threading;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200001F RID: 31
	internal sealed class MigrationScanner : MigrationComponent
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x000062EE File Offset: 0x000044EE
		internal MigrationScanner(string name, WaitHandle stopEvent) : base(name, stopEvent)
		{
			this.nextProcessTime = ExDateTime.MinValue;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00006303 File Offset: 0x00004503
		internal override bool ShouldProcess()
		{
			return ExDateTime.UtcNow >= this.nextProcessTime;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00006318 File Offset: 0x00004518
		internal override bool Process(IMigrationJobCache data)
		{
			MigrationUtil.ThrowOnNullArgument(data, "data");
			this.nextProcessTime = ExDateTime.UtcNow.Add(MigrationScanner.processDelay);
			return data.SyncWithStore();
		}

		// Token: 0x0400003D RID: 61
		private static readonly TimeSpan processDelay = TimeSpan.FromMinutes(5.0);

		// Token: 0x0400003E RID: 62
		private ExDateTime nextProcessTime;
	}
}
