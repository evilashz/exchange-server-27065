using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.UM
{
	// Token: 0x020004C3 RID: 1219
	internal abstract class DiagnosticsItemWithServiceBase : DiagnosticsItemBase
	{
		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001E80 RID: 7808 RVA: 0x000B81D2 File Offset: 0x000B63D2
		public string FullService
		{
			get
			{
				return base.GetValue("service");
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001E81 RID: 7809 RVA: 0x000B81E0 File Offset: 0x000B63E0
		public string Service
		{
			get
			{
				string fullService = this.FullService;
				int num = fullService.IndexOf('/');
				if (num > 0)
				{
					return fullService.Substring(0, num);
				}
				return fullService;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001E82 RID: 7810 RVA: 0x000B820C File Offset: 0x000B640C
		public string Version
		{
			get
			{
				string fullService = this.FullService;
				int num = fullService.IndexOf('/');
				if (num > 0)
				{
					return fullService.Substring(num + 1);
				}
				return string.Empty;
			}
		}
	}
}
