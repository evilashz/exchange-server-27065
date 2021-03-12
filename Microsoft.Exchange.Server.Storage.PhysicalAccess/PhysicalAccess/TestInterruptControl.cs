using System;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x0200004B RID: 75
	public class TestInterruptControl : IInterruptControl
	{
		// Token: 0x0600035F RID: 863 RVA: 0x00011C1B File Offset: 0x0000FE1B
		public TestInterruptControl() : this(1, 1, 1, null)
		{
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00011C27 File Offset: 0x0000FE27
		public TestInterruptControl(int scanReadMax, int probeReadMax, int writeMax) : this(scanReadMax, probeReadMax, writeMax, null)
		{
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00011C33 File Offset: 0x0000FE33
		public TestInterruptControl(int scanReadMax, int probeReadMax, int writeMax, Func<bool> wantToInterrupt)
		{
			this.scanReadMax = scanReadMax;
			this.probeReadMax = probeReadMax;
			this.writeMax = writeMax;
			this.wantToInterrupt = wantToInterrupt;
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000362 RID: 866 RVA: 0x00011C58 File Offset: 0x0000FE58
		public bool WantToInterrupt
		{
			get
			{
				return this.scanReadCount >= this.scanReadMax || this.probeReadCount >= this.probeReadMax || this.writeCount >= this.writeMax || (this.wantToInterrupt != null && this.wantToInterrupt());
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00011CA6 File Offset: 0x0000FEA6
		public void RegisterRead(bool probe, TableClass tableClass)
		{
			if (probe)
			{
				this.probeReadCount++;
				return;
			}
			this.scanReadCount++;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00011CC8 File Offset: 0x0000FEC8
		public void RegisterWrite(TableClass tableClass)
		{
			this.writeCount++;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00011CD8 File Offset: 0x0000FED8
		public void Reset()
		{
			this.scanReadCount = 0;
			this.probeReadCount = 0;
			this.writeCount = 0;
		}

		// Token: 0x04000106 RID: 262
		private readonly int scanReadMax;

		// Token: 0x04000107 RID: 263
		private readonly int probeReadMax;

		// Token: 0x04000108 RID: 264
		private readonly int writeMax;

		// Token: 0x04000109 RID: 265
		private readonly Func<bool> wantToInterrupt;

		// Token: 0x0400010A RID: 266
		private int scanReadCount;

		// Token: 0x0400010B RID: 267
		private int probeReadCount;

		// Token: 0x0400010C RID: 268
		private int writeCount;
	}
}
