using System;
using System.Globalization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000015 RID: 21
	[Serializable]
	public class TopologyServiceOutcome : ConfigurableObject
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x0000452C File Offset: 0x0000272C
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return TopologyServiceOutcome.schema;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00004534 File Offset: 0x00002734
		public string LatencyInMillisecondsString
		{
			get
			{
				return Math.Round(this.Latency.TotalMilliseconds, 2).ToString("F2", CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00004567 File Offset: 0x00002767
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00004579 File Offset: 0x00002779
		public string Server
		{
			get
			{
				return (string)this[TopologyServiceOutcomeSchema.Server];
			}
			internal set
			{
				this[TopologyServiceOutcomeSchema.Server] = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00004587 File Offset: 0x00002787
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00004599 File Offset: 0x00002799
		public string OperationType
		{
			get
			{
				return (string)this[TopologyServiceOutcomeSchema.OperationType];
			}
			internal set
			{
				this[TopologyServiceOutcomeSchema.OperationType] = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000AD RID: 173 RVA: 0x000045A7 File Offset: 0x000027A7
		// (set) Token: 0x060000AE RID: 174 RVA: 0x000045B9 File Offset: 0x000027B9
		public TopologyServiceResult Result
		{
			get
			{
				return (TopologyServiceResult)this[TopologyServiceOutcomeSchema.Result];
			}
			internal set
			{
				this[TopologyServiceOutcomeSchema.Result] = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000045C7 File Offset: 0x000027C7
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x000045E7 File Offset: 0x000027E7
		public TimeSpan Latency
		{
			get
			{
				return (TimeSpan)(this[TopologyServiceOutcomeSchema.Latency] ?? TimeSpan.Zero);
			}
			internal set
			{
				this[TopologyServiceOutcomeSchema.Latency] = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000045FA File Offset: 0x000027FA
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00004611 File Offset: 0x00002811
		public string Error
		{
			get
			{
				return (string)this.propertyBag[TopologyServiceOutcomeSchema.Error];
			}
			internal set
			{
				this.propertyBag[TopologyServiceOutcomeSchema.Error] = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00004624 File Offset: 0x00002824
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x0000463B File Offset: 0x0000283B
		public string Output
		{
			get
			{
				return (string)this.propertyBag[TopologyServiceOutcomeSchema.Output];
			}
			internal set
			{
				this.propertyBag[TopologyServiceOutcomeSchema.Output] = value;
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000464E File Offset: 0x0000284E
		public TopologyServiceOutcome(string server, string operationType) : base(new SimpleProviderPropertyBag())
		{
			this.Server = server;
			this.OperationType = operationType;
			this.Result = new TopologyServiceResult(TopologyServiceResultEnum.Undefined);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004680 File Offset: 0x00002880
		internal void Update(TopologyServiceResultEnum resultEnum, TimeSpan latency, string error, string output)
		{
			lock (this.thisLock)
			{
				this.Result = new TopologyServiceResult(resultEnum);
				this.Latency = latency;
				this.Error = (error ?? string.Empty);
				this.Output = (output ?? string.Empty);
			}
		}

		// Token: 0x04000061 RID: 97
		[NonSerialized]
		private object thisLock = new object();

		// Token: 0x04000062 RID: 98
		private static TopologyServiceOutcomeSchema schema = ObjectSchema.GetInstance<TopologyServiceOutcomeSchema>();
	}
}
