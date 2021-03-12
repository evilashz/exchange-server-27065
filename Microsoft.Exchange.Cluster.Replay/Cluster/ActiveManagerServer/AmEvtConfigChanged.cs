using System;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000077 RID: 119
	internal class AmEvtConfigChanged : AmEvtBase
	{
		// Token: 0x060004FB RID: 1275 RVA: 0x0001A96B File Offset: 0x00018B6B
		internal AmEvtConfigChanged(AmConfigChangedFlags changeFlags, AmConfig previousConfig, AmConfig newConfig)
		{
			this.ChangeFlags = changeFlags;
			this.PreviousConfig = previousConfig;
			this.NewConfig = newConfig;
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x0001A988 File Offset: 0x00018B88
		// (set) Token: 0x060004FD RID: 1277 RVA: 0x0001A990 File Offset: 0x00018B90
		internal AmConfigChangedFlags ChangeFlags { get; set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x0001A999 File Offset: 0x00018B99
		// (set) Token: 0x060004FF RID: 1279 RVA: 0x0001A9A1 File Offset: 0x00018BA1
		internal AmConfig PreviousConfig { get; set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x0001A9AA File Offset: 0x00018BAA
		// (set) Token: 0x06000501 RID: 1281 RVA: 0x0001A9B2 File Offset: 0x00018BB2
		internal AmConfig NewConfig { get; set; }

		// Token: 0x06000502 RID: 1282 RVA: 0x0001A9BC File Offset: 0x00018BBC
		public override string ToString()
		{
			return string.Format("{0}: Params: (ChangeFlags={1}, PrevCfgRole={2}, NewCfgRole={3})", new object[]
			{
				base.GetType().Name,
				this.ChangeFlags,
				this.PreviousConfig.Role,
				this.NewConfig.Role
			});
		}
	}
}
