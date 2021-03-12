using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Net.MonitoringWebClient.E4e
{
	// Token: 0x020007A1 RID: 1953
	internal class E4eExceptionAnalyzer : BaseExceptionAnalyzer
	{
		// Token: 0x0600276D RID: 10093 RVA: 0x0005393F File Offset: 0x00051B3F
		public E4eExceptionAnalyzer(Dictionary<string, RequestTarget> hostNameSourceMapping) : base(hostNameSourceMapping)
		{
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x0600276E RID: 10094 RVA: 0x00053948 File Offset: 0x00051B48
		protected override Dictionary<RequestTarget, Dictionary<FailureReason, FailingComponent>> ComponentMatrix
		{
			get
			{
				return E4eExceptionAnalyzer.FailureMatrix;
			}
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x0600276F RID: 10095 RVA: 0x0005394F File Offset: 0x00051B4F
		protected override FailingComponent DefaultComponent
		{
			get
			{
				return FailingComponent.E4e;
			}
		}

		// Token: 0x040023A0 RID: 9120
		private static readonly Dictionary<RequestTarget, Dictionary<FailureReason, FailingComponent>> FailureMatrix = new Dictionary<RequestTarget, Dictionary<FailureReason, FailingComponent>>();
	}
}
