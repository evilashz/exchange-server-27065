using System;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000045 RID: 69
	internal class RetryException : Exception
	{
		// Token: 0x0600061C RID: 1564 RVA: 0x00016D1B File Offset: 0x00014F1B
		public RetryException(Exception innerException, bool resetRetryCounter = false) : base(string.Empty, innerException)
		{
			this.ResetRetryCounter = resetRetryCounter;
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x00016D30 File Offset: 0x00014F30
		// (set) Token: 0x0600061E RID: 1566 RVA: 0x00016D38 File Offset: 0x00014F38
		public bool ResetRetryCounter { get; private set; }
	}
}
