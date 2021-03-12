using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000033 RID: 51
	internal sealed class NullResourceMonitor : ResourceMonitor
	{
		// Token: 0x0600011B RID: 283 RVA: 0x00005127 File Offset: 0x00003327
		public NullResourceMonitor(string displayName) : base(displayName, new ResourceManagerConfiguration.ResourceMonitorConfiguration(10, 8, 6))
		{
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005139 File Offset: 0x00003339
		protected override bool GetCurrentReading(out int currentReading)
		{
			currentReading = 0;
			return true;
		}

		// Token: 0x04000086 RID: 134
		private const int HighThreshold = 10;

		// Token: 0x04000087 RID: 135
		private const int MediumThreshold = 8;

		// Token: 0x04000088 RID: 136
		private const int NormalThreshold = 6;

		// Token: 0x04000089 RID: 137
		private const int SafeValue = 0;
	}
}
