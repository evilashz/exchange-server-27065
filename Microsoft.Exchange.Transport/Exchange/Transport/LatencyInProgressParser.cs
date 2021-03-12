using System;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000175 RID: 373
	internal class LatencyInProgressParser : LatencyParser
	{
		// Token: 0x0600105B RID: 4187 RVA: 0x00042648 File Offset: 0x00040848
		public LatencyInProgressParser() : base(ExTraceGlobals.SmtpReceiveTracer)
		{
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x00042660 File Offset: 0x00040860
		public DateTime PreDeliveryTime
		{
			get
			{
				return this.preDeliveryTime;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x0600105D RID: 4189 RVA: 0x00042668 File Offset: 0x00040868
		public int PreDeliveryTimeSeparatorIndex
		{
			get
			{
				return this.preDeliveryTimeSeparatorIndex;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x00042670 File Offset: 0x00040870
		public int ServerFqdnStartIndex
		{
			get
			{
				return this.serverFqdnStartIndex;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x0600105F RID: 4191 RVA: 0x00042678 File Offset: 0x00040878
		public int ServerFqdnLength
		{
			get
			{
				return this.serverFqdnLength;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001060 RID: 4192 RVA: 0x00042680 File Offset: 0x00040880
		public int ComponentsStartIndex
		{
			get
			{
				return this.componentsStartIndex;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x00042688 File Offset: 0x00040888
		public int ComponentsLength
		{
			get
			{
				return this.componentsLength;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x00042690 File Offset: 0x00040890
		public float TotalSeconds
		{
			get
			{
				return this.totalSeconds;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001063 RID: 4195 RVA: 0x00042698 File Offset: 0x00040898
		public int PendingStartIndex
		{
			get
			{
				return this.pendingStartIndex;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x000426A0 File Offset: 0x000408A0
		// (set) Token: 0x06001065 RID: 4197 RVA: 0x000426A8 File Offset: 0x000408A8
		public string PreviousHopProcess
		{
			get
			{
				return this.previousHopProcess;
			}
			private set
			{
				this.previousHopProcess = value;
			}
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x000426B4 File Offset: 0x000408B4
		public bool TryParse(string s)
		{
			if (!this.TryParsePreDeliveryTime(s))
			{
				return false;
			}
			this.serverFqdnStartIndex = -1;
			this.serverFqdnLength = -1;
			this.componentsStartIndex = -1;
			this.componentsLength = -1;
			this.totalSeconds = 65535f;
			this.pendingStartIndex = -1;
			return base.TryParse(s, 0, this.preDeliveryTimeSeparatorIndex);
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x00042708 File Offset: 0x00040908
		protected override bool HandleLocalServerFqdn(string s, int startIndex, int count)
		{
			this.serverFqdnStartIndex = startIndex;
			this.serverFqdnLength = count;
			base.Tracer.TraceDebug<int, int, string>(0L, "LatencyInProgress Parser: found server FQDN at position {0} (length={1}) in string '{2}'", this.serverFqdnStartIndex, this.serverFqdnLength, base.StringToParse);
			return true;
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x0004273D File Offset: 0x0004093D
		protected override bool HandleServerFqdn(string s, int startIndex, int count)
		{
			base.Tracer.TraceError<int, int, string>(0L, "LatencyInProgress Parser: non-local server FQDN found at position {0} (length={1}) in string '{2}'", startIndex, count, base.StringToParse);
			return false;
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x0004275C File Offset: 0x0004095C
		protected override bool HandleComponentLatency(string s, int componentNameStart, int componentNameLength, int latencyStart, int latencyLength)
		{
			if (this.componentsStartIndex == -1)
			{
				this.componentsStartIndex = componentNameStart;
				base.Tracer.TraceDebug<int, string>(0L, "LatencyInProgress Parser: components start at position {0} in string '{1}'", this.componentsStartIndex, base.StringToParse);
			}
			this.componentsLength = latencyStart + latencyLength - this.componentsStartIndex;
			return true;
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x000427AC File Offset: 0x000409AC
		protected override bool HandleTotalLatency(string s, int startIndex, int count)
		{
			if (!LatencyParser.TryParseLatency(s, startIndex, count, out this.totalSeconds))
			{
				base.Tracer.TraceError<int, int, string>(0L, "LatencyInProgress Parser: invalid TOTAL value at position {0} (length={1}) in string '{2}'", startIndex, count, base.StringToParse);
				return false;
			}
			base.Tracer.TraceDebug<float, string>(0L, "LatencyInProgress Parser: found TOTAL value {0} in string '{1}'", this.totalSeconds, base.StringToParse);
			return true;
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x00042804 File Offset: 0x00040A04
		protected override void HandleTotalComponent(string s, int startIndex, int count)
		{
			string text = s.Substring(startIndex, count);
			string[] array = text.Split(new char[]
			{
				'-'
			});
			if (array.Length == 2)
			{
				this.PreviousHopProcess = array[1];
			}
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x0004283D File Offset: 0x00040A3D
		protected override bool HandlePendingComponent(string s, int startIndex, int count)
		{
			if (this.pendingStartIndex == -1)
			{
				this.pendingStartIndex = startIndex;
				base.Tracer.TraceDebug<int, string>(0L, "LatencyInProgress Parser: pending components start at position {0} in string '{1}'", this.pendingStartIndex, base.StringToParse);
			}
			return true;
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x00042870 File Offset: 0x00040A70
		private bool TryParsePreDeliveryTime(string s)
		{
			this.preDeliveryTime = DateTime.MinValue;
			this.preDeliveryTimeSeparatorIndex = -1;
			this.preDeliveryTimeSeparatorIndex = s.LastIndexOf(';');
			if (this.preDeliveryTimeSeparatorIndex == -1)
			{
				base.Tracer.TraceError<string>(0L, "LatencyInProgress Parser: Cannot find the timestamp field in string '{0}'", s);
				return false;
			}
			int num = LatencyParser.SkipWhitespaces(s, this.preDeliveryTimeSeparatorIndex + 1, s.Length - this.preDeliveryTimeSeparatorIndex - 1);
			if (num == -1)
			{
				base.Tracer.TraceError<string>(0L, "LatencyInProgress Parser: Cannot find the timestamp value in string '{0}'", s);
				return false;
			}
			if (!LatencyParser.TryParseDateTime(s, num, s.Length - num, out this.preDeliveryTime))
			{
				base.Tracer.TraceError<int, string>(0L, "LatencyInProgress Parser: Failed to parse the timestamp value at position {0} in string '{1}'", num, s);
				return false;
			}
			return true;
		}

		// Token: 0x04000845 RID: 2117
		private DateTime preDeliveryTime;

		// Token: 0x04000846 RID: 2118
		private int preDeliveryTimeSeparatorIndex;

		// Token: 0x04000847 RID: 2119
		private int serverFqdnStartIndex;

		// Token: 0x04000848 RID: 2120
		private int serverFqdnLength;

		// Token: 0x04000849 RID: 2121
		private int componentsStartIndex;

		// Token: 0x0400084A RID: 2122
		private int componentsLength;

		// Token: 0x0400084B RID: 2123
		private float totalSeconds;

		// Token: 0x0400084C RID: 2124
		private int pendingStartIndex;

		// Token: 0x0400084D RID: 2125
		private string previousHopProcess = string.Empty;
	}
}
