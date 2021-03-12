using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000019 RID: 25
	internal class NullFilteringServiceInvoker : FilteringServiceInvoker
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00004500 File Offset: 0x00002700
		private NullFilteringServiceInvoker()
		{
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004508 File Offset: 0x00002708
		internal static NullFilteringServiceInvoker Factory()
		{
			return new NullFilteringServiceInvoker();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000450F File Offset: 0x0000270F
		public override FilteringServiceInvoker.BeginScanResult BeginScan(FilteringServiceInvokerRequest filteringServiceInvokerRequest, ITracer tracer, Dictionary<string, string> classificationsToLookFor, FilteringServiceInvoker.ScanCompleteCallback scanCompleteCallback)
		{
			return FilteringServiceInvoker.BeginScanResult.Failure;
		}
	}
}
