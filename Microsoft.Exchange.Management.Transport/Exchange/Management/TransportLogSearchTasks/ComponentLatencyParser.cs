using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.Tracking;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Management.TransportLogSearchTasks
{
	// Token: 0x0200004D RID: 77
	internal sealed class ComponentLatencyParser : LatencyParser
	{
		// Token: 0x060002D3 RID: 723 RVA: 0x0000B9A4 File Offset: 0x00009BA4
		public ComponentLatencyParser() : base(ExTraceGlobals.TaskTracer)
		{
			this.currentServerName = string.Empty;
			this.componentSequenceNumber = 0;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000B9C4 File Offset: 0x00009BC4
		public bool TryParse(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return false;
			}
			DateTime dateTime;
			int num;
			ComponentLatencyParser.TryParseOriginalArrivalTime(s, out dateTime, out num);
			return num < s.Length && base.TryParse(s, num, s.Length - num);
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000BA01 File Offset: 0x00009C01
		public IEnumerable<LatencyComponent> Components
		{
			get
			{
				return this.components ?? ComponentLatencyParser.EmptyComponentArray;
			}
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000BA12 File Offset: 0x00009C12
		protected override bool HandleLocalServerFqdn(string s, int startIndex, int count)
		{
			this.currentServerName = s.Substring(startIndex, count);
			this.componentSequenceNumber = 0;
			return true;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000BA2A File Offset: 0x00009C2A
		protected override bool HandleServerFqdn(string s, int startIndex, int count)
		{
			this.currentServerName = s.Substring(startIndex, count);
			this.componentSequenceNumber = 0;
			return true;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000BA44 File Offset: 0x00009C44
		protected override bool HandleTotalLatency(string s, int startIndex, int count)
		{
			ushort latency;
			if (LatencyParser.TryParseLatency(s, startIndex, count, out latency))
			{
				this.AddComponent(this.currentServerName, "TOTAL", latency);
			}
			return true;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000BA70 File Offset: 0x00009C70
		protected override bool HandleComponentLatency(string s, int componentNameStart, int componentNameLength, int latencyStart, int latencyLength)
		{
			ushort latency;
			if (LatencyParser.TryParseLatency(s, latencyStart, latencyLength, out latency))
			{
				this.AddComponent(this.currentServerName, s.Substring(componentNameStart, componentNameLength), latency);
			}
			return true;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000BAA4 File Offset: 0x00009CA4
		internal static bool TryParseOriginalArrivalTime(string s, out DateTime originalArrivalTime, out int latencyInfoStartIndex)
		{
			int num = s.IndexOf(';');
			if (num < 0)
			{
				DateTime dateTime;
				if (LatencyParser.TryParseDateTime(s, 0, s.Length, out dateTime))
				{
					originalArrivalTime = dateTime;
					latencyInfoStartIndex = s.Length;
					return true;
				}
				originalArrivalTime = DateTime.MinValue;
				latencyInfoStartIndex = 0;
				return false;
			}
			else
			{
				DateTime dateTime2;
				if (LatencyParser.TryParseDateTime(s, 0, num, out dateTime2))
				{
					originalArrivalTime = dateTime2;
					latencyInfoStartIndex = num + 1;
					return true;
				}
				originalArrivalTime = DateTime.MinValue;
				latencyInfoStartIndex = 0;
				return false;
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000BB1C File Offset: 0x00009D1C
		private void AddComponent(string serverName, string code, ushort latency)
		{
			if (this.components == null)
			{
				this.components = new LinkedList<LatencyComponent>();
			}
			LocalizedString fullName = LatencyTracker.GetFullName(code);
			if (LocalizedString.Empty.Equals(fullName))
			{
				fullName = new LocalizedString(code);
			}
			LatencyComponent value = new LatencyComponent(serverName, code, fullName, latency, this.componentSequenceNumber++);
			this.components.AddLast(value);
		}

		// Token: 0x04000108 RID: 264
		private const string TotalComponentCode = "TOTAL";

		// Token: 0x04000109 RID: 265
		private static readonly IEnumerable<LatencyComponent> EmptyComponentArray = new LatencyComponent[0];

		// Token: 0x0400010A RID: 266
		private LinkedList<LatencyComponent> components;

		// Token: 0x0400010B RID: 267
		private string currentServerName;

		// Token: 0x0400010C RID: 268
		private int componentSequenceNumber;
	}
}
