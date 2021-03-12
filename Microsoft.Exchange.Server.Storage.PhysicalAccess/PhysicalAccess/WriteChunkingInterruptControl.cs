using System;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x0200004A RID: 74
	public class WriteChunkingInterruptControl : IInterruptControl
	{
		// Token: 0x0600035A RID: 858 RVA: 0x00011BC0 File Offset: 0x0000FDC0
		public WriteChunkingInterruptControl(int writeMax, Func<bool> canContinue)
		{
			this.canContinue = canContinue;
			this.writeMax = writeMax;
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00011BD6 File Offset: 0x0000FDD6
		public bool WantToInterrupt
		{
			get
			{
				return this.writeCount >= this.writeMax || (this.canContinue != null && !this.canContinue());
			}
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00011C00 File Offset: 0x0000FE00
		public void RegisterRead(bool probe, TableClass tableClass)
		{
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00011C02 File Offset: 0x0000FE02
		public void RegisterWrite(TableClass tableClass)
		{
			this.writeCount++;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00011C12 File Offset: 0x0000FE12
		public void Reset()
		{
			this.writeCount = 0;
		}

		// Token: 0x04000103 RID: 259
		private readonly Func<bool> canContinue;

		// Token: 0x04000104 RID: 260
		private readonly int writeMax;

		// Token: 0x04000105 RID: 261
		private int writeCount;
	}
}
