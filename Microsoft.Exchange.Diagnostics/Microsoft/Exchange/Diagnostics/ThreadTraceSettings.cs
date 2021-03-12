using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000AC RID: 172
	public sealed class ThreadTraceSettings
	{
		// Token: 0x06000445 RID: 1093 RVA: 0x0000FB43 File Offset: 0x0000DD43
		internal ThreadTraceSettings()
		{
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000FB4B File Offset: 0x0000DD4B
		public void EnableTracing()
		{
			this.enabledCount++;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000FB5B File Offset: 0x0000DD5B
		public void DisableTracing()
		{
			if (this.enabledCount == 0)
			{
				throw new InvalidOperationException("Mismatched number of calls to enable/disable tracing");
			}
			this.enabledCount--;
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x0000FB7E File Offset: 0x0000DD7E
		public bool IsEnabled
		{
			get
			{
				return this.enabledCount > 0;
			}
		}

		// Token: 0x0400035D RID: 861
		private int enabledCount;
	}
}
