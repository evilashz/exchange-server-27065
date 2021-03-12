using System;
using System.Management.Automation;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Net.DiagnosticsAggregation;

namespace Microsoft.Exchange.Management.QueueDigest
{
	// Token: 0x02000067 RID: 103
	internal abstract class GetQueueDigestAdapter
	{
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600039C RID: 924
		public abstract QueueDigestGroupBy GroupBy { get; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600039D RID: 925
		public abstract DetailsLevel DetailsLevel { get; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600039E RID: 926
		public abstract string Filter { get; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600039F RID: 927
		public abstract SwitchParameter IncludeE14Servers { get; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060003A0 RID: 928
		public abstract EnhancedTimeSpan Timeout { get; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060003A1 RID: 929
		public abstract Unlimited<uint> ResultSize { get; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060003A2 RID: 930
		public abstract bool IsVerbose { get; }

		// Token: 0x060003A3 RID: 931
		public abstract void WriteDebug(string text);

		// Token: 0x060003A4 RID: 932
		public abstract void WriteDebug(LocalizedString text);

		// Token: 0x060003A5 RID: 933
		public abstract void WriteVerbose(LocalizedString text);

		// Token: 0x060003A6 RID: 934
		public abstract void WriteWarning(LocalizedString text);

		// Token: 0x060003A7 RID: 935
		public abstract void WriteObject(object sendToPipeline);

		// Token: 0x060003A8 RID: 936
		public abstract IDiagnosticsAggregationService CreateWebServiceClient(Binding binding, EndpointAddress endpoint);

		// Token: 0x060003A9 RID: 937
		public abstract void DisposeWebServiceClient(IDiagnosticsAggregationService client);
	}
}
