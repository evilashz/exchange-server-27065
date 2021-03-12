using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000331 RID: 817
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WlmResourceUnhealthyException : ResourceReservationException
	{
		// Token: 0x060025AB RID: 9643 RVA: 0x00051D68 File Offset: 0x0004FF68
		public WlmResourceUnhealthyException(string resourceName, string resourceType, string wlmResourceKey, int wlmResourceMetricType, double reportedLoadRatio, string reportedLoadState, string metric) : base(MrsStrings.ErrorWlmResourceUnhealthy1(resourceName, resourceType, wlmResourceKey, wlmResourceMetricType, reportedLoadRatio, reportedLoadState, metric))
		{
			this.resourceName = resourceName;
			this.resourceType = resourceType;
			this.wlmResourceKey = wlmResourceKey;
			this.wlmResourceMetricType = wlmResourceMetricType;
			this.reportedLoadRatio = reportedLoadRatio;
			this.reportedLoadState = reportedLoadState;
			this.metric = metric;
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x00051DC0 File Offset: 0x0004FFC0
		public WlmResourceUnhealthyException(string resourceName, string resourceType, string wlmResourceKey, int wlmResourceMetricType, double reportedLoadRatio, string reportedLoadState, string metric, Exception innerException) : base(MrsStrings.ErrorWlmResourceUnhealthy1(resourceName, resourceType, wlmResourceKey, wlmResourceMetricType, reportedLoadRatio, reportedLoadState, metric), innerException)
		{
			this.resourceName = resourceName;
			this.resourceType = resourceType;
			this.wlmResourceKey = wlmResourceKey;
			this.wlmResourceMetricType = wlmResourceMetricType;
			this.reportedLoadRatio = reportedLoadRatio;
			this.reportedLoadState = reportedLoadState;
			this.metric = metric;
		}

		// Token: 0x060025AD RID: 9645 RVA: 0x00051E1C File Offset: 0x0005001C
		protected WlmResourceUnhealthyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.resourceName = (string)info.GetValue("resourceName", typeof(string));
			this.resourceType = (string)info.GetValue("resourceType", typeof(string));
			this.wlmResourceKey = (string)info.GetValue("wlmResourceKey", typeof(string));
			this.wlmResourceMetricType = (int)info.GetValue("wlmResourceMetricType", typeof(int));
			this.reportedLoadRatio = (double)info.GetValue("reportedLoadRatio", typeof(double));
			this.reportedLoadState = (string)info.GetValue("reportedLoadState", typeof(string));
			this.metric = (string)info.GetValue("metric", typeof(string));
		}

		// Token: 0x060025AE RID: 9646 RVA: 0x00051F14 File Offset: 0x00050114
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("resourceName", this.resourceName);
			info.AddValue("resourceType", this.resourceType);
			info.AddValue("wlmResourceKey", this.wlmResourceKey);
			info.AddValue("wlmResourceMetricType", this.wlmResourceMetricType);
			info.AddValue("reportedLoadRatio", this.reportedLoadRatio);
			info.AddValue("reportedLoadState", this.reportedLoadState);
			info.AddValue("metric", this.metric);
		}

		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x060025AF RID: 9647 RVA: 0x00051FA0 File Offset: 0x000501A0
		public string ResourceName
		{
			get
			{
				return this.resourceName;
			}
		}

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x060025B0 RID: 9648 RVA: 0x00051FA8 File Offset: 0x000501A8
		public string ResourceType
		{
			get
			{
				return this.resourceType;
			}
		}

		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x060025B1 RID: 9649 RVA: 0x00051FB0 File Offset: 0x000501B0
		public string WlmResourceKey
		{
			get
			{
				return this.wlmResourceKey;
			}
		}

		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x060025B2 RID: 9650 RVA: 0x00051FB8 File Offset: 0x000501B8
		public int WlmResourceMetricType
		{
			get
			{
				return this.wlmResourceMetricType;
			}
		}

		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x060025B3 RID: 9651 RVA: 0x00051FC0 File Offset: 0x000501C0
		public double ReportedLoadRatio
		{
			get
			{
				return this.reportedLoadRatio;
			}
		}

		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x060025B4 RID: 9652 RVA: 0x00051FC8 File Offset: 0x000501C8
		public string ReportedLoadState
		{
			get
			{
				return this.reportedLoadState;
			}
		}

		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x060025B5 RID: 9653 RVA: 0x00051FD0 File Offset: 0x000501D0
		public string Metric
		{
			get
			{
				return this.metric;
			}
		}

		// Token: 0x04001030 RID: 4144
		private readonly string resourceName;

		// Token: 0x04001031 RID: 4145
		private readonly string resourceType;

		// Token: 0x04001032 RID: 4146
		private readonly string wlmResourceKey;

		// Token: 0x04001033 RID: 4147
		private readonly int wlmResourceMetricType;

		// Token: 0x04001034 RID: 4148
		private readonly double reportedLoadRatio;

		// Token: 0x04001035 RID: 4149
		private readonly string reportedLoadState;

		// Token: 0x04001036 RID: 4150
		private readonly string metric;
	}
}
