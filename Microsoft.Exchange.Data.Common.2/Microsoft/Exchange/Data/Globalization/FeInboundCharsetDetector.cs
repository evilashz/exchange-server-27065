using System;

namespace Microsoft.Exchange.Data.Globalization
{
	// Token: 0x02000119 RID: 281
	internal struct FeInboundCharsetDetector
	{
		// Token: 0x06000B17 RID: 2839 RVA: 0x00067D10 File Offset: 0x00065F10
		public FeInboundCharsetDetector(int defaultCodePage, bool strongDefault, bool enableIsoDetection, bool enableUtf8Detection, bool enableDbcsDetection)
		{
			this.defaultCodePage = (ushort)defaultCodePage;
			this.strongDefault = strongDefault;
			this.stateIso = FEData.ST.ERR;
			this.stateUtf8 = FEData.ST.ERR;
			this.stateGbkWan = FEData.ST.ERR;
			this.stateEucKrCn = FEData.ST.ERR;
			this.stateEucJp = FEData.ST.ERR;
			this.stateSJis = FEData.ST.ERR;
			this.stateBig5 = FEData.ST.ERR;
			this.stateJisEsc = FEData.JS.S0;
			this.countJapaneseEsc = 0;
			this.countKoreanDesignator = 0;
			this.countSo = 0;
			this.count8bit = 0;
			if (enableDbcsDetection)
			{
				if (defaultCodePage <= 20936)
				{
					if (defaultCodePage <= 936)
					{
						if (defaultCodePage == 0)
						{
							this.stateSJis = FEData.ST.ST0;
							this.stateEucJp = FEData.ST.ST0;
							this.stateGbkWan = FEData.ST.ST0;
							this.stateEucKrCn = FEData.ST.ST0;
							this.stateBig5 = FEData.ST.ST0;
							goto IL_1C7;
						}
						if (defaultCodePage != 932)
						{
							if (defaultCodePage != 936)
							{
								goto IL_1C7;
							}
							goto IL_1A9;
						}
					}
					else if (defaultCodePage <= 1361)
					{
						switch (defaultCodePage)
						{
						case 949:
							goto IL_199;
						case 950:
							this.stateBig5 = FEData.ST.ST0;
							this.stateGbkWan = FEData.ST.ST0;
							goto IL_1C7;
						default:
							if (defaultCodePage != 1361)
							{
								goto IL_1C7;
							}
							goto IL_199;
						}
					}
					else if (defaultCodePage != 20932)
					{
						if (defaultCodePage != 20936)
						{
							goto IL_1C7;
						}
						goto IL_1A9;
					}
				}
				else if (defaultCodePage <= 51932)
				{
					if (defaultCodePage == 20949)
					{
						goto IL_199;
					}
					switch (defaultCodePage)
					{
					case 50220:
					case 50221:
					case 50222:
						break;
					case 50223:
					case 50224:
					case 50226:
						goto IL_1C7;
					case 50225:
						goto IL_199;
					case 50227:
						goto IL_1A9;
					default:
						if (defaultCodePage != 51932)
						{
							goto IL_1C7;
						}
						break;
					}
				}
				else if (defaultCodePage <= 51949)
				{
					if (defaultCodePage == 51936)
					{
						goto IL_1A9;
					}
					if (defaultCodePage != 51949)
					{
						goto IL_1C7;
					}
					goto IL_199;
				}
				else
				{
					if (defaultCodePage != 52936 && defaultCodePage != 54936)
					{
						goto IL_1C7;
					}
					goto IL_1A9;
				}
				this.stateSJis = FEData.ST.ST0;
				this.stateEucJp = FEData.ST.ST0;
				goto IL_1C7;
				IL_199:
				this.stateGbkWan = FEData.ST.ST0;
				this.stateEucKrCn = FEData.ST.ST0;
				goto IL_1C7;
				IL_1A9:
				this.stateGbkWan = FEData.ST.ST0;
				this.stateEucKrCn = FEData.ST.ST0;
			}
			IL_1C7:
			if (enableIsoDetection)
			{
				this.stateIso = FEData.ST.ST0;
			}
			if (enableUtf8Detection)
			{
				this.stateUtf8 = FEData.ST.ST0;
			}
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00067EFC File Offset: 0x000660FC
		public static bool IsSupportedFarEastCharset(Charset charset)
		{
			int codePage = charset.CodePage;
			if (codePage <= 20936)
			{
				if (codePage <= 950)
				{
					if (codePage != 932 && codePage != 936)
					{
						switch (codePage)
						{
						case 949:
						case 950:
							break;
						default:
							return false;
						}
					}
				}
				else if (codePage != 1361 && codePage != 20932 && codePage != 20936)
				{
					return false;
				}
			}
			else if (codePage <= 51932)
			{
				if (codePage != 20949)
				{
					switch (codePage)
					{
					case 50220:
					case 50221:
					case 50222:
					case 50225:
					case 50227:
						break;
					case 50223:
					case 50224:
					case 50226:
						return false;
					default:
						if (codePage != 51932)
						{
							return false;
						}
						break;
					}
				}
			}
			else if (codePage <= 51949)
			{
				if (codePage != 51936 && codePage != 51949)
				{
					return false;
				}
			}
			else if (codePage != 52936 && codePage != 54936)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00067FDF File Offset: 0x000661DF
		public void Reset(int codePage, bool strongDefault, bool enableIsoDetection, bool enableUtf8Detection, bool enableDbcsDetection)
		{
			this = new FeInboundCharsetDetector(codePage, strongDefault, enableIsoDetection, enableUtf8Detection, enableDbcsDetection);
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x00067FF3 File Offset: 0x000661F3
		public void Reset()
		{
			this = new FeInboundCharsetDetector(0, false, true, true, true);
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x00068008 File Offset: 0x00066208
		public void RunJisStateMachine(byte bt)
		{
			FEData.JC jc = FEData.JisCharClass[(int)bt];
			this.stateJisEsc = FEData.JisEscNextState[(int)this.stateJisEsc, (int)jc];
			if ((this.stateJisEsc & FEData.JS.CNTA) != FEData.JS.S0)
			{
				if (this.stateJisEsc == FEData.JS.CNTA)
				{
					if (this.countSo != 255)
					{
						this.countSo += 1;
					}
				}
				else if (this.stateJisEsc == FEData.JS.CNTJ)
				{
					if (this.countJapaneseEsc != 255)
					{
						this.countJapaneseEsc += 1;
					}
				}
				else if (this.stateJisEsc == FEData.JS.CNTK && this.countKoreanDesignator != 255)
				{
					this.countKoreanDesignator += 1;
				}
				this.stateJisEsc = FEData.JS.S0;
			}
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x000680CC File Offset: 0x000662CC
		public void RunDbcsStateMachines(FEData.CC cc)
		{
			if (this.stateSJis != FEData.ST.ERR)
			{
				this.stateSJis = FEData.SJisNextState[(int)this.stateSJis, (int)cc];
			}
			if (this.stateEucJp != FEData.ST.ERR)
			{
				this.stateEucJp = FEData.EucJpNextState[(int)this.stateEucJp, (int)cc];
			}
			if (this.stateUtf8 != FEData.ST.ERR)
			{
				this.stateUtf8 = FEData.Utf8NextState[(int)this.stateUtf8, (int)cc];
			}
			if (this.stateGbkWan != FEData.ST.ERR)
			{
				this.stateGbkWan = FEData.GbkWanNextState[(int)this.stateGbkWan, (int)cc];
			}
			if (this.stateEucKrCn != FEData.ST.ERR)
			{
				this.stateEucKrCn = FEData.EucKrCnNextState[(int)this.stateEucKrCn, (int)cc];
			}
			if (this.stateBig5 != FEData.ST.ERR)
			{
				this.stateBig5 = FEData.Big5NextState[(int)this.stateBig5, (int)cc];
			}
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x000681A0 File Offset: 0x000663A0
		public void AddBytes(byte[] bytes, int offset, int length, bool eof)
		{
			if (this.stateSJis != FEData.ST.ERR || this.stateEucJp != FEData.ST.ERR || this.stateIso != FEData.ST.ERR || this.stateGbkWan != FEData.ST.ERR || this.stateEucKrCn != FEData.ST.ERR || this.stateBig5 != FEData.ST.ERR || this.stateUtf8 != FEData.ST.ERR)
			{
				int num = offset + length;
				while (offset < num)
				{
					byte b = bytes[offset++];
					if (b > 127 && this.count8bit != 255)
					{
						this.count8bit += 1;
					}
					if (this.stateIso != FEData.ST.ERR && b <= 127)
					{
						this.RunJisStateMachine(b);
					}
					FEData.CC cc = FEData.CharClass[(int)b];
					this.RunDbcsStateMachines(cc);
				}
				if (eof)
				{
					this.RunDbcsStateMachines(FEData.CC.eof);
				}
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x00068258 File Offset: 0x00066458
		public bool SJisPossible
		{
			get
			{
				return this.stateSJis != FEData.ST.ERR;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x00068267 File Offset: 0x00066467
		public bool EucJpPossible
		{
			get
			{
				return this.stateEucJp != FEData.ST.ERR;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x00068276 File Offset: 0x00066476
		public bool Iso2022JpPossible
		{
			get
			{
				return this.stateIso != FEData.ST.ERR;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x00068285 File Offset: 0x00066485
		public bool Iso2022KrPossible
		{
			get
			{
				return this.stateIso != FEData.ST.ERR;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x00068294 File Offset: 0x00066494
		public bool Utf8Possible
		{
			get
			{
				return this.stateUtf8 != FEData.ST.ERR;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x000682A3 File Offset: 0x000664A3
		public bool GbkPossible
		{
			get
			{
				return this.stateGbkWan != FEData.ST.ERR;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x000682B2 File Offset: 0x000664B2
		public bool WansungPossible
		{
			get
			{
				return this.stateGbkWan != FEData.ST.ERR;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x000682C1 File Offset: 0x000664C1
		public bool EucKrPossible
		{
			get
			{
				return this.stateEucKrCn != FEData.ST.ERR;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x000682D0 File Offset: 0x000664D0
		public bool EucCnPossible
		{
			get
			{
				return this.stateEucKrCn != FEData.ST.ERR;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x000682DF File Offset: 0x000664DF
		public bool Big5Possible
		{
			get
			{
				return this.stateBig5 != FEData.ST.ERR;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x000682EE File Offset: 0x000664EE
		public bool PureAscii
		{
			get
			{
				return this.count8bit + this.countKoreanDesignator + this.countJapaneseEsc + this.countSo == 0;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x0006830E File Offset: 0x0006650E
		public bool Iso2022JpVeryLikely
		{
			get
			{
				return this.Iso2022JpPossible && this.countJapaneseEsc > 0 && this.countKoreanDesignator == 0;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x0006832C File Offset: 0x0006652C
		public bool Iso2022JpLikely
		{
			get
			{
				return this.Iso2022JpPossible && (this.countJapaneseEsc > this.countKoreanDesignator || (this.countKoreanDesignator == 0 && this.countSo != 0));
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x0006835E File Offset: 0x0006655E
		public bool Iso2022KrVeryLikely
		{
			get
			{
				return this.Iso2022KrPossible && this.countKoreanDesignator > 0 && this.countJapaneseEsc == 0;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x0006837C File Offset: 0x0006657C
		public bool Iso2022KrLikely
		{
			get
			{
				return this.Iso2022KrPossible && (this.countKoreanDesignator > this.countJapaneseEsc || (this.countJapaneseEsc == 0 && this.countSo != 0));
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x000683AE File Offset: 0x000665AE
		public bool SJisLikely
		{
			get
			{
				return this.SJisPossible && this.count8bit >= 6;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x000683C6 File Offset: 0x000665C6
		public bool EucJpLikely
		{
			get
			{
				return this.EucJpPossible && this.count8bit >= 6;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x000683DE File Offset: 0x000665DE
		public bool Utf8Likely
		{
			get
			{
				return this.Utf8Possible && this.count8bit >= 6;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x000683F6 File Offset: 0x000665F6
		public bool Utf8VeryLikely
		{
			get
			{
				return this.Utf8Possible && this.count8bit >= 18;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x0006840F File Offset: 0x0006660F
		public bool GbkLikely
		{
			get
			{
				return this.GbkPossible && this.count8bit >= 6;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x00068427 File Offset: 0x00066627
		public bool WansungLikely
		{
			get
			{
				return this.WansungPossible && this.count8bit >= 6;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x0006843F File Offset: 0x0006663F
		public bool EucKrLikely
		{
			get
			{
				return this.EucKrPossible && this.count8bit >= 6;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x00068457 File Offset: 0x00066657
		public bool EucCnLikely
		{
			get
			{
				return this.EucCnPossible && this.count8bit >= 6;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x0006846F File Offset: 0x0006666F
		public bool Big5Likely
		{
			get
			{
				return this.Big5Possible && this.count8bit >= 6;
			}
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00068487 File Offset: 0x00066687
		public int GetCodePageChoice()
		{
			return this.GetCodePageChoice((int)this.defaultCodePage, this.strongDefault);
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0006849C File Offset: 0x0006669C
		public int GetCodePageChoice(int defaultCodePage, bool strongDefault)
		{
			if (this.PureAscii)
			{
				return defaultCodePage;
			}
			if (this.Iso2022JpVeryLikely)
			{
				return 50220;
			}
			if (this.Iso2022KrVeryLikely)
			{
				return 50225;
			}
			if (this.Utf8VeryLikely)
			{
				return 65001;
			}
			if (defaultCodePage == 50220 || defaultCodePage == 50221 || defaultCodePage == 50222)
			{
				defaultCodePage = 932;
			}
			else if (defaultCodePage == 50225)
			{
				defaultCodePage = 949;
			}
			else if (defaultCodePage == 20932)
			{
				defaultCodePage = 51932;
			}
			else if (defaultCodePage == 20949)
			{
				defaultCodePage = 51949;
			}
			if (defaultCodePage == 932 || defaultCodePage == 51932)
			{
				if (this.Iso2022JpLikely)
				{
					return 50222;
				}
				if (this.SJisPossible && defaultCodePage == 932 && strongDefault)
				{
					return 932;
				}
				if (this.EucJpPossible && defaultCodePage == 51932 && strongDefault)
				{
					return 51932;
				}
				if (!this.Utf8Likely)
				{
					if (this.EucJpPossible)
					{
						return 51932;
					}
					if (this.SJisPossible)
					{
						return 932;
					}
				}
			}
			else if (defaultCodePage == 949 || defaultCodePage == 51949)
			{
				if (this.Iso2022KrLikely)
				{
					return 50225;
				}
				if (this.WansungPossible && defaultCodePage == 949 && strongDefault)
				{
					return 949;
				}
				if (this.EucKrPossible && defaultCodePage == 51949 && strongDefault)
				{
					return 51949;
				}
				if (!this.Utf8Likely)
				{
					if (this.WansungPossible)
					{
						return 949;
					}
					if (this.EucKrPossible)
					{
						return 51949;
					}
				}
			}
			else if (defaultCodePage == 936 || defaultCodePage == 51936)
			{
				if (this.GbkPossible && defaultCodePage == 936 && strongDefault)
				{
					return 936;
				}
				if (this.EucCnPossible && defaultCodePage == 51936 && strongDefault)
				{
					return 51936;
				}
				if (!this.Utf8Likely)
				{
					if (this.GbkPossible)
					{
						return 936;
					}
					if (this.EucCnPossible)
					{
						return 51936;
					}
				}
			}
			else if (defaultCodePage == 950)
			{
				if (this.Big5Possible && strongDefault)
				{
					return 950;
				}
				if (!this.Utf8Likely)
				{
					if (this.Big5Possible)
					{
						return 950;
					}
					if (this.GbkLikely)
					{
						return 932;
					}
				}
			}
			if (this.Utf8Likely && !strongDefault)
			{
				return 65001;
			}
			return defaultCodePage;
		}

		// Token: 0x04000E38 RID: 3640
		private ushort defaultCodePage;

		// Token: 0x04000E39 RID: 3641
		private bool strongDefault;

		// Token: 0x04000E3A RID: 3642
		private FEData.ST stateIso;

		// Token: 0x04000E3B RID: 3643
		private FEData.ST stateUtf8;

		// Token: 0x04000E3C RID: 3644
		private FEData.ST stateSJis;

		// Token: 0x04000E3D RID: 3645
		private FEData.ST stateEucJp;

		// Token: 0x04000E3E RID: 3646
		private FEData.ST stateGbkWan;

		// Token: 0x04000E3F RID: 3647
		private FEData.ST stateEucKrCn;

		// Token: 0x04000E40 RID: 3648
		private FEData.ST stateBig5;

		// Token: 0x04000E41 RID: 3649
		private FEData.JS stateJisEsc;

		// Token: 0x04000E42 RID: 3650
		private byte countJapaneseEsc;

		// Token: 0x04000E43 RID: 3651
		private byte countKoreanDesignator;

		// Token: 0x04000E44 RID: 3652
		private byte countSo;

		// Token: 0x04000E45 RID: 3653
		private byte count8bit;
	}
}
