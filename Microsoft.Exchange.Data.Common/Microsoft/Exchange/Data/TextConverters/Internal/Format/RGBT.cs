using System;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Format
{
	// Token: 0x02000297 RID: 663
	internal struct RGBT
	{
		// Token: 0x06001A1C RID: 6684 RVA: 0x000CF0D6 File Offset: 0x000CD2D6
		public RGBT(uint rawValue)
		{
			this.rawValue = rawValue;
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x000CF0DF File Offset: 0x000CD2DF
		public RGBT(uint red, uint green, uint blue)
		{
			this.rawValue = (red << 16 | green << 8 | blue);
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x000CF0F1 File Offset: 0x000CD2F1
		public RGBT(float redPercentage, float greenPercentage, float bluePercentage)
		{
			this.rawValue = ((uint)(redPercentage * 255f / 100f) << 16 | (uint)(greenPercentage * 255f / 100f) << 8 | (uint)(bluePercentage * 255f / 100f));
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x000CF12A File Offset: 0x000CD32A
		public RGBT(uint red, uint green, uint blue, uint transparency)
		{
			this.rawValue = (red << 16 | green << 8 | blue | transparency << 24);
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x000CF144 File Offset: 0x000CD344
		public RGBT(float redPercentage, float greenPercentage, float bluePercentage, float transparencyPercentage)
		{
			this.rawValue = ((uint)(redPercentage * 255f / 100f) << 16 | (uint)(greenPercentage * 255f / 100f) << 8 | (uint)(bluePercentage * 255f / 100f) | (uint)(transparencyPercentage * 7f / 100f));
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001A21 RID: 6689 RVA: 0x000CF198 File Offset: 0x000CD398
		public uint RawValue
		{
			get
			{
				return this.rawValue;
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001A22 RID: 6690 RVA: 0x000CF1A0 File Offset: 0x000CD3A0
		public uint RGB
		{
			get
			{
				return this.rawValue & 16777215U;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001A23 RID: 6691 RVA: 0x000CF1AE File Offset: 0x000CD3AE
		public bool IsTransparent
		{
			get
			{
				return this.Transparency == 7U;
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001A24 RID: 6692 RVA: 0x000CF1B9 File Offset: 0x000CD3B9
		public bool IsOpaque
		{
			get
			{
				return this.Transparency == 0U;
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06001A25 RID: 6693 RVA: 0x000CF1C4 File Offset: 0x000CD3C4
		public uint Transparency
		{
			get
			{
				return this.rawValue >> 24 & 7U;
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06001A26 RID: 6694 RVA: 0x000CF1D1 File Offset: 0x000CD3D1
		public uint Red
		{
			get
			{
				return this.rawValue >> 16 & 255U;
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001A27 RID: 6695 RVA: 0x000CF1E2 File Offset: 0x000CD3E2
		public uint Green
		{
			get
			{
				return this.rawValue >> 8 & 255U;
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001A28 RID: 6696 RVA: 0x000CF1F2 File Offset: 0x000CD3F2
		public uint Blue
		{
			get
			{
				return this.rawValue & 255U;
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001A29 RID: 6697 RVA: 0x000CF200 File Offset: 0x000CD400
		public float RedPercentage
		{
			get
			{
				return (this.rawValue >> 16 & 255U) * 0.39215687f;
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001A2A RID: 6698 RVA: 0x000CF219 File Offset: 0x000CD419
		public float GreenPercentage
		{
			get
			{
				return (this.rawValue >> 8 & 255U) * 0.39215687f;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001A2B RID: 6699 RVA: 0x000CF231 File Offset: 0x000CD431
		public float BluePercentage
		{
			get
			{
				return (this.rawValue & 255U) * 0.39215687f;
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001A2C RID: 6700 RVA: 0x000CF247 File Offset: 0x000CD447
		public float TransparencyPercentage
		{
			get
			{
				return (this.rawValue >> 24 & 7U) * 14.285714f;
			}
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x000CF25C File Offset: 0x000CD45C
		public override string ToString()
		{
			if (!this.IsTransparent)
			{
				return string.Concat(new string[]
				{
					"rgb(",
					this.Red.ToString(),
					", ",
					this.Green.ToString(),
					", ",
					this.Blue.ToString(),
					")",
					(this.Transparency != 0U) ? ("+t" + this.Transparency.ToString()) : string.Empty
				});
			}
			return "transparent";
		}

		// Token: 0x0400204C RID: 8268
		private uint rawValue;
	}
}
