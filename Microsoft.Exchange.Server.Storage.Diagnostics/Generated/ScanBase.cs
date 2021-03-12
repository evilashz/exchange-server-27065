using System;
using mppg;

namespace Microsoft.Exchange.Server.Storage.Diagnostics.Generated
{
	// Token: 0x0200001E RID: 30
	public abstract class ScanBase : AScanner<Token, LexLocation>
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000DF RID: 223
		// (set) Token: 0x060000E0 RID: 224
		protected abstract int CurrentSc { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x0000598D File Offset: 0x00003B8D
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00005995 File Offset: 0x00003B95
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
