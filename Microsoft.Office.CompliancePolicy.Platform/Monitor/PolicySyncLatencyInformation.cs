using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Monitor
{
	// Token: 0x02000081 RID: 129
	internal class PolicySyncLatencyInformation
	{
		// Token: 0x06000351 RID: 849 RVA: 0x0000B7C2 File Offset: 0x000099C2
		public PolicySyncLatencyInformation(ConfigurationObjectType objectType, int count) : this(objectType, count, null)
		{
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000B7CD File Offset: 0x000099CD
		internal PolicySyncLatencyInformation(ConfigurationObjectType objectType, int count, Func<TimeSpan, TimeSpan> getLatencyValueDelegate)
		{
			this.Latencies = new Dictionary<LatencyType, TimeSpan>();
			this.ObjectType = objectType;
			this.Count = count;
			this.getLatencyValueDelegate = getLatencyValueDelegate;
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000B7F5 File Offset: 0x000099F5
		// (set) Token: 0x06000354 RID: 852 RVA: 0x0000B7FD File Offset: 0x000099FD
		public ConfigurationObjectType ObjectType { get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000B806 File Offset: 0x00009A06
		// (set) Token: 0x06000356 RID: 854 RVA: 0x0000B80E File Offset: 0x00009A0E
		public int Count { get; set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000B817 File Offset: 0x00009A17
		// (set) Token: 0x06000358 RID: 856 RVA: 0x0000B81F File Offset: 0x00009A1F
		public Dictionary<LatencyType, TimeSpan> Latencies { get; private set; }

		// Token: 0x06000359 RID: 857 RVA: 0x0000B868 File Offset: 0x00009A68
		public override string ToString()
		{
			ArgumentValidator.ThrowIfCollectionNullOrEmpty<KeyValuePair<LatencyType, TimeSpan>>("Latencies", this.Latencies);
			string[] value = (from entry in this.Latencies
			select string.Format("{0}={1}", entry.Key, (int)this.GetLatencyValue(entry.Value).TotalSeconds)).ToArray<string>();
			return string.Format("Object={0},Count={1},{2};", this.ObjectType, this.Count, string.Join(",", value));
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000B8CD File Offset: 0x00009ACD
		private TimeSpan GetLatencyValue(TimeSpan latency)
		{
			if (this.getLatencyValueDelegate == null)
			{
				return latency;
			}
			return this.getLatencyValueDelegate(latency);
		}

		// Token: 0x04000222 RID: 546
		private readonly Func<TimeSpan, TimeSpan> getLatencyValueDelegate;
	}
}
