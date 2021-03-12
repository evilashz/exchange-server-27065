using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200033A RID: 826
	internal class FullEndOfLog
	{
		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06002181 RID: 8577 RVA: 0x0009B8C8 File Offset: 0x00099AC8
		// (set) Token: 0x06002182 RID: 8578 RVA: 0x0009B8D0 File Offset: 0x00099AD0
		public bool PositionInE00 { get; set; }

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06002183 RID: 8579 RVA: 0x0009B8D9 File Offset: 0x00099AD9
		// (set) Token: 0x06002184 RID: 8580 RVA: 0x0009B8E1 File Offset: 0x00099AE1
		public long Generation { get; set; }

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06002185 RID: 8581 RVA: 0x0009B8EA File Offset: 0x00099AEA
		// (set) Token: 0x06002186 RID: 8582 RVA: 0x0009B8F2 File Offset: 0x00099AF2
		public int Sector { get; set; }

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06002187 RID: 8583 RVA: 0x0009B8FB File Offset: 0x00099AFB
		// (set) Token: 0x06002188 RID: 8584 RVA: 0x0009B903 File Offset: 0x00099B03
		public int ByteOffset { get; set; }

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06002189 RID: 8585 RVA: 0x0009B90C File Offset: 0x00099B0C
		// (set) Token: 0x0600218A RID: 8586 RVA: 0x0009B914 File Offset: 0x00099B14
		public DateTime Utc { get; set; }

		// Token: 0x0600218B RID: 8587 RVA: 0x0009B91D File Offset: 0x00099B1D
		public void CopyTo(FullEndOfLog target)
		{
			target.PositionInE00 = this.PositionInE00;
			target.Generation = this.Generation;
			target.Sector = this.Sector;
			target.ByteOffset = this.ByteOffset;
			target.Utc = this.Utc;
		}

		// Token: 0x0600218C RID: 8588 RVA: 0x0009B95C File Offset: 0x00099B5C
		public override string ToString()
		{
			return string.Format("Gen=0x{0:X} Sector=0x{1:X} E00={2} UTC:{3:s}", new object[]
			{
				this.Generation,
				this.Sector,
				this.PositionInE00,
				this.Utc
			});
		}
	}
}
