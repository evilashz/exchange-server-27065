using System;
using System.Management.Automation;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.DiagnosticsAggregation;

namespace Microsoft.Exchange.Management.QueueDigest
{
	// Token: 0x02000068 RID: 104
	internal class GetQueueDigestCmdletAdapter : GetQueueDigestAdapter
	{
		// Token: 0x060003AB RID: 939 RVA: 0x0000E06D File Offset: 0x0000C26D
		public GetQueueDigestCmdletAdapter(GetQueueDigest cmdlet)
		{
			this.cmdlet = cmdlet;
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0000E07C File Offset: 0x0000C27C
		public override QueueDigestGroupBy GroupBy
		{
			get
			{
				return this.cmdlet.GroupBy;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0000E089 File Offset: 0x0000C289
		public override DetailsLevel DetailsLevel
		{
			get
			{
				return this.cmdlet.DetailsLevel;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0000E096 File Offset: 0x0000C296
		public override string Filter
		{
			get
			{
				return this.cmdlet.Filter;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0000E0A3 File Offset: 0x0000C2A3
		public override SwitchParameter IncludeE14Servers
		{
			get
			{
				return this.cmdlet.IncludeE14Servers;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000E0B0 File Offset: 0x0000C2B0
		public override EnhancedTimeSpan Timeout
		{
			get
			{
				return this.cmdlet.Timeout;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000E0BD File Offset: 0x0000C2BD
		public override Unlimited<uint> ResultSize
		{
			get
			{
				return this.cmdlet.ResultSize;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0000E0CA File Offset: 0x0000C2CA
		public override bool IsVerbose
		{
			get
			{
				return this.cmdlet.IsVerbose;
			}
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000E0D7 File Offset: 0x0000C2D7
		public override void WriteDebug(string text)
		{
			this.cmdlet.WriteDebug(text);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000E0E5 File Offset: 0x0000C2E5
		public override void WriteDebug(LocalizedString text)
		{
			this.cmdlet.WriteDebug(text);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000E0F3 File Offset: 0x0000C2F3
		public override void WriteVerbose(LocalizedString text)
		{
			this.cmdlet.WriteVerbose(text);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000E101 File Offset: 0x0000C301
		public override void WriteWarning(LocalizedString text)
		{
			this.cmdlet.WriteWarning(text);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000E10F File Offset: 0x0000C30F
		public override void WriteObject(object sendToPipeline)
		{
			this.cmdlet.WriteObject(sendToPipeline);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000E11D File Offset: 0x0000C31D
		public override IDiagnosticsAggregationService CreateWebServiceClient(Binding binding, EndpointAddress endpoint)
		{
			return new DiagnosticsAggregationServiceClient(binding, endpoint);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000E126 File Offset: 0x0000C326
		public override void DisposeWebServiceClient(IDiagnosticsAggregationService client)
		{
			if (client != null)
			{
				WcfUtils.DisposeWcfClientGracefully((DiagnosticsAggregationServiceClient)client, false);
			}
		}

		// Token: 0x04000159 RID: 345
		private GetQueueDigest cmdlet;
	}
}
