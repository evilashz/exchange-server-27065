using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.UM
{
	// Token: 0x020004C1 RID: 1217
	internal class DiagnosticsItemBase
	{
		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001E6E RID: 7790 RVA: 0x000B7E8D File Offset: 0x000B608D
		// (set) Token: 0x06001E6F RID: 7791 RVA: 0x000B7E95 File Offset: 0x000B6095
		public int ErrorId { get; internal set; }

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001E70 RID: 7792 RVA: 0x000B7E9E File Offset: 0x000B609E
		public string Source
		{
			get
			{
				return this.GetValue("source");
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001E71 RID: 7793 RVA: 0x000B7EAB File Offset: 0x000B60AB
		public string Reason
		{
			get
			{
				return this.GetValue("reason");
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001E72 RID: 7794 RVA: 0x000B7EB8 File Offset: 0x000B60B8
		// (set) Token: 0x06001E73 RID: 7795 RVA: 0x000B7EC0 File Offset: 0x000B60C0
		public DateTime LocalTime { get; protected set; }

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001E74 RID: 7796 RVA: 0x000B7EC9 File Offset: 0x000B60C9
		public bool IsValid
		{
			get
			{
				return this.ErrorId != -1;
			}
		}

		// Token: 0x06001E75 RID: 7797 RVA: 0x000B7ED7 File Offset: 0x000B60D7
		internal bool ContainsKey(string key)
		{
			return this.data.ContainsKey(key);
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x000B7EE5 File Offset: 0x000B60E5
		internal void Add(string key, string value)
		{
			if (this.data.ContainsKey(key))
			{
				this.data[key] = value;
				return;
			}
			this.data.Add(key, value);
		}

		// Token: 0x06001E77 RID: 7799 RVA: 0x000B7F10 File Offset: 0x000B6110
		public string GetValue(string key)
		{
			if (this.data.ContainsKey(key))
			{
				return this.data[key];
			}
			return string.Empty;
		}

		// Token: 0x17000652 RID: 1618
		public string this[string key]
		{
			get
			{
				return this.GetValue(key);
			}
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x000B7F3B File Offset: 0x000B613B
		protected DiagnosticsItemBase()
		{
			this.LocalTime = DateTime.UtcNow;
			this.ErrorId = -1;
			this.data = new Dictionary<string, string>(3);
		}

		// Token: 0x040015CA RID: 5578
		public const int InvalidErrorId = -1;

		// Token: 0x040015CB RID: 5579
		private Dictionary<string, string> data;
	}
}
