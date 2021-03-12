using System;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x0200001A RID: 26
	internal class CiMdbCopyInfo
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x00004BD7 File Offset: 0x00002DD7
		public CiMdbCopyInfo(string server, bool mounted, int metric)
		{
			this.Server = server;
			this.IsAvailable = true;
			this.Mounted = mounted;
			this.Metric = metric;
			this.Used = false;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004C02 File Offset: 0x00002E02
		public CiMdbCopyInfo(string server)
		{
			this.Server = server;
			this.IsAvailable = false;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00004C18 File Offset: 0x00002E18
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00004C20 File Offset: 0x00002E20
		public string Server { get; private set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00004C29 File Offset: 0x00002E29
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00004C31 File Offset: 0x00002E31
		public bool IsAvailable { get; private set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00004C3A File Offset: 0x00002E3A
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00004C42 File Offset: 0x00002E42
		public bool Mounted { get; private set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00004C4B File Offset: 0x00002E4B
		// (set) Token: 0x060000FB RID: 251 RVA: 0x00004C53 File Offset: 0x00002E53
		public int Metric { get; private set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00004C5C File Offset: 0x00002E5C
		// (set) Token: 0x060000FD RID: 253 RVA: 0x00004C64 File Offset: 0x00002E64
		public bool Used { get; internal set; }

		// Token: 0x060000FE RID: 254 RVA: 0x00004C70 File Offset: 0x00002E70
		public override string ToString()
		{
			if (this.IsAvailable)
			{
				return string.Concat(new object[]
				{
					this.Server,
					",",
					this.Mounted ? "Mounted" : string.Empty,
					",",
					this.Metric,
					",",
					this.Used ? "Used" : string.Empty
				});
			}
			return this.Server + ",NoStatus";
		}
	}
}
