using System;
using mppg;

namespace Microsoft.Exchange.Data.Generated
{
	// Token: 0x0200023E RID: 574
	public abstract class ScanBase : AScanner<LexValue, LexLocation>
	{
		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x060013BA RID: 5050
		// (set) Token: 0x060013BB RID: 5051
		protected abstract int CurrentSc { get; set; }

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x060013BC RID: 5052 RVA: 0x0003C42E File Offset: 0x0003A62E
		// (set) Token: 0x060013BD RID: 5053 RVA: 0x0003C436 File Offset: 0x0003A636
		public virtual int EolState
		{
			get
			{
				return this.CurrentSc;
			}
			set
			{
				this.CurrentSc = value;
			}
		}
	}
}
