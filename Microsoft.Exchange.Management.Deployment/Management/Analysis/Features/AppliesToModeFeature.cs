using System;

namespace Microsoft.Exchange.Management.Analysis.Features
{
	// Token: 0x0200005E RID: 94
	internal class AppliesToModeFeature : Feature
	{
		// Token: 0x0600023D RID: 573 RVA: 0x000082FA File Offset: 0x000064FA
		public AppliesToModeFeature(SetupMode modes) : base(true, true)
		{
			this.Mode = modes;
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000830B File Offset: 0x0000650B
		// (set) Token: 0x0600023F RID: 575 RVA: 0x00008313 File Offset: 0x00006513
		public SetupMode Mode { get; private set; }

		// Token: 0x06000240 RID: 576 RVA: 0x0000831C File Offset: 0x0000651C
		public bool Contains(SetupMode mode)
		{
			return (this.Mode & mode) > SetupMode.None;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00008329 File Offset: 0x00006529
		public override string ToString()
		{
			return string.Format("{0}({1})", base.ToString(), this.Mode.ToString());
		}
	}
}
