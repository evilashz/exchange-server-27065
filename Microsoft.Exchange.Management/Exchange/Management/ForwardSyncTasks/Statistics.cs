using System;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02000355 RID: 853
	public class Statistics<T, AverageT, SumT>
	{
		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06001D6E RID: 7534 RVA: 0x00081DAC File Offset: 0x0007FFAC
		// (set) Token: 0x06001D6F RID: 7535 RVA: 0x00081DB4 File Offset: 0x0007FFB4
		public AverageT Average { get; set; }

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06001D70 RID: 7536 RVA: 0x00081DBD File Offset: 0x0007FFBD
		// (set) Token: 0x06001D71 RID: 7537 RVA: 0x00081DC5 File Offset: 0x0007FFC5
		public T Maximum { get; set; }

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06001D72 RID: 7538 RVA: 0x00081DCE File Offset: 0x0007FFCE
		// (set) Token: 0x06001D73 RID: 7539 RVA: 0x00081DD6 File Offset: 0x0007FFD6
		public T Minimum { get; set; }

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06001D74 RID: 7540 RVA: 0x00081DDF File Offset: 0x0007FFDF
		// (set) Token: 0x06001D75 RID: 7541 RVA: 0x00081DE7 File Offset: 0x0007FFE7
		public SumT Sum { get; set; }

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06001D76 RID: 7542 RVA: 0x00081DF0 File Offset: 0x0007FFF0
		// (set) Token: 0x06001D77 RID: 7543 RVA: 0x00081DF8 File Offset: 0x0007FFF8
		public int SampleCount { get; set; }

		// Token: 0x06001D78 RID: 7544 RVA: 0x00081E04 File Offset: 0x00080004
		public override string ToString()
		{
			string format = "Avg: {0,-15:F} Max: {1,-15} Min: {2,-15}";
			return string.Format(format, this.Average, this.Maximum, this.Minimum);
		}
	}
}
