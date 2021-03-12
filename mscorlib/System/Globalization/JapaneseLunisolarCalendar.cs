using System;

namespace System.Globalization
{
	// Token: 0x02000399 RID: 921
	[Serializable]
	public class JapaneseLunisolarCalendar : EastAsianLunisolarCalendar
	{
		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06002F2C RID: 12076 RVA: 0x000B4FF1 File Offset: 0x000B31F1
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return JapaneseLunisolarCalendar.minDate;
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06002F2D RID: 12077 RVA: 0x000B4FF8 File Offset: 0x000B31F8
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return JapaneseLunisolarCalendar.maxDate;
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06002F2E RID: 12078 RVA: 0x000B4FFF File Offset: 0x000B31FF
		protected override int DaysInYearBeforeMinSupportedYear
		{
			get
			{
				return 354;
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06002F2F RID: 12079 RVA: 0x000B5006 File Offset: 0x000B3206
		internal override int MinCalendarYear
		{
			get
			{
				return 1960;
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06002F30 RID: 12080 RVA: 0x000B500D File Offset: 0x000B320D
		internal override int MaxCalendarYear
		{
			get
			{
				return 2049;
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06002F31 RID: 12081 RVA: 0x000B5014 File Offset: 0x000B3214
		internal override DateTime MinDate
		{
			get
			{
				return JapaneseLunisolarCalendar.minDate;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06002F32 RID: 12082 RVA: 0x000B501B File Offset: 0x000B321B
		internal override DateTime MaxDate
		{
			get
			{
				return JapaneseLunisolarCalendar.maxDate;
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06002F33 RID: 12083 RVA: 0x000B5022 File Offset: 0x000B3222
		internal override EraInfo[] CalEraInfo
		{
			get
			{
				return JapaneseCalendar.GetEraInfo();
			}
		}

		// Token: 0x06002F34 RID: 12084 RVA: 0x000B502C File Offset: 0x000B322C
		internal override int GetYearInfo(int LunarYear, int Index)
		{
			if (LunarYear < 1960 || LunarYear > 2049)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 1960, 2049));
			}
			return JapaneseLunisolarCalendar.yinfo[LunarYear - 1960, Index];
		}

		// Token: 0x06002F35 RID: 12085 RVA: 0x000B508E File Offset: 0x000B328E
		internal override int GetYear(int year, DateTime time)
		{
			return this.helper.GetYear(year, time);
		}

		// Token: 0x06002F36 RID: 12086 RVA: 0x000B509D File Offset: 0x000B329D
		internal override int GetGregorianYear(int year, int era)
		{
			return this.helper.GetGregorianYear(year, era);
		}

		// Token: 0x06002F37 RID: 12087 RVA: 0x000B50AC File Offset: 0x000B32AC
		private static EraInfo[] TrimEras(EraInfo[] baseEras)
		{
			EraInfo[] array = new EraInfo[baseEras.Length];
			int num = 0;
			for (int i = 0; i < baseEras.Length; i++)
			{
				if (baseEras[i].yearOffset + baseEras[i].minEraYear < 2049)
				{
					if (baseEras[i].yearOffset + baseEras[i].maxEraYear < 1960)
					{
						break;
					}
					array[num] = baseEras[i];
					num++;
				}
			}
			if (num == 0)
			{
				return baseEras;
			}
			Array.Resize<EraInfo>(ref array, num);
			return array;
		}

		// Token: 0x06002F38 RID: 12088 RVA: 0x000B511A File Offset: 0x000B331A
		public JapaneseLunisolarCalendar()
		{
			this.helper = new GregorianCalendarHelper(this, JapaneseLunisolarCalendar.TrimEras(JapaneseCalendar.GetEraInfo()));
		}

		// Token: 0x06002F39 RID: 12089 RVA: 0x000B5138 File Offset: 0x000B3338
		public override int GetEra(DateTime time)
		{
			return this.helper.GetEra(time);
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06002F3A RID: 12090 RVA: 0x000B5146 File Offset: 0x000B3346
		internal override int BaseCalendarID
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06002F3B RID: 12091 RVA: 0x000B5149 File Offset: 0x000B3349
		internal override int ID
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06002F3C RID: 12092 RVA: 0x000B514D File Offset: 0x000B334D
		public override int[] Eras
		{
			get
			{
				return this.helper.Eras;
			}
		}

		// Token: 0x040013E3 RID: 5091
		public const int JapaneseEra = 1;

		// Token: 0x040013E4 RID: 5092
		internal GregorianCalendarHelper helper;

		// Token: 0x040013E5 RID: 5093
		internal const int MIN_LUNISOLAR_YEAR = 1960;

		// Token: 0x040013E6 RID: 5094
		internal const int MAX_LUNISOLAR_YEAR = 2049;

		// Token: 0x040013E7 RID: 5095
		internal const int MIN_GREGORIAN_YEAR = 1960;

		// Token: 0x040013E8 RID: 5096
		internal const int MIN_GREGORIAN_MONTH = 1;

		// Token: 0x040013E9 RID: 5097
		internal const int MIN_GREGORIAN_DAY = 28;

		// Token: 0x040013EA RID: 5098
		internal const int MAX_GREGORIAN_YEAR = 2050;

		// Token: 0x040013EB RID: 5099
		internal const int MAX_GREGORIAN_MONTH = 1;

		// Token: 0x040013EC RID: 5100
		internal const int MAX_GREGORIAN_DAY = 22;

		// Token: 0x040013ED RID: 5101
		internal static DateTime minDate = new DateTime(1960, 1, 28);

		// Token: 0x040013EE RID: 5102
		internal static DateTime maxDate = new DateTime(new DateTime(2050, 1, 22, 23, 59, 59, 999).Ticks + 9999L);

		// Token: 0x040013EF RID: 5103
		private static readonly int[,] yinfo = new int[,]
		{
			{
				6,
				1,
				28,
				44368
			},
			{
				0,
				2,
				15,
				43856
			},
			{
				0,
				2,
				5,
				19808
			},
			{
				4,
				1,
				25,
				42352
			},
			{
				0,
				2,
				13,
				42352
			},
			{
				0,
				2,
				2,
				21104
			},
			{
				3,
				1,
				22,
				26928
			},
			{
				0,
				2,
				9,
				55632
			},
			{
				7,
				1,
				30,
				27304
			},
			{
				0,
				2,
				17,
				22176
			},
			{
				0,
				2,
				6,
				39632
			},
			{
				5,
				1,
				27,
				19176
			},
			{
				0,
				2,
				15,
				19168
			},
			{
				0,
				2,
				3,
				42208
			},
			{
				4,
				1,
				23,
				53864
			},
			{
				0,
				2,
				11,
				53840
			},
			{
				8,
				1,
				31,
				54600
			},
			{
				0,
				2,
				18,
				46400
			},
			{
				0,
				2,
				7,
				54944
			},
			{
				6,
				1,
				28,
				38608
			},
			{
				0,
				2,
				16,
				38320
			},
			{
				0,
				2,
				5,
				18864
			},
			{
				4,
				1,
				25,
				42200
			},
			{
				0,
				2,
				13,
				42160
			},
			{
				10,
				2,
				2,
				45656
			},
			{
				0,
				2,
				20,
				27216
			},
			{
				0,
				2,
				9,
				27968
			},
			{
				6,
				1,
				29,
				46504
			},
			{
				0,
				2,
				18,
				11104
			},
			{
				0,
				2,
				6,
				38320
			},
			{
				5,
				1,
				27,
				18872
			},
			{
				0,
				2,
				15,
				18800
			},
			{
				0,
				2,
				4,
				25776
			},
			{
				3,
				1,
				23,
				27216
			},
			{
				0,
				2,
				10,
				59984
			},
			{
				8,
				1,
				31,
				27976
			},
			{
				0,
				2,
				19,
				23248
			},
			{
				0,
				2,
				8,
				11104
			},
			{
				5,
				1,
				28,
				37744
			},
			{
				0,
				2,
				16,
				37600
			},
			{
				0,
				2,
				5,
				51552
			},
			{
				4,
				1,
				24,
				58536
			},
			{
				0,
				2,
				12,
				54432
			},
			{
				0,
				2,
				1,
				55888
			},
			{
				2,
				1,
				22,
				23208
			},
			{
				0,
				2,
				9,
				22208
			},
			{
				7,
				1,
				29,
				43736
			},
			{
				0,
				2,
				18,
				9680
			},
			{
				0,
				2,
				7,
				37584
			},
			{
				5,
				1,
				26,
				51544
			},
			{
				0,
				2,
				14,
				43344
			},
			{
				0,
				2,
				3,
				46240
			},
			{
				3,
				1,
				23,
				47696
			},
			{
				0,
				2,
				10,
				46416
			},
			{
				9,
				1,
				31,
				21928
			},
			{
				0,
				2,
				19,
				19360
			},
			{
				0,
				2,
				8,
				42416
			},
			{
				5,
				1,
				28,
				21176
			},
			{
				0,
				2,
				16,
				21168
			},
			{
				0,
				2,
				5,
				43344
			},
			{
				4,
				1,
				25,
				46248
			},
			{
				0,
				2,
				12,
				27296
			},
			{
				0,
				2,
				1,
				44368
			},
			{
				2,
				1,
				22,
				21928
			},
			{
				0,
				2,
				10,
				19296
			},
			{
				6,
				1,
				29,
				42352
			},
			{
				0,
				2,
				17,
				42352
			},
			{
				0,
				2,
				7,
				21104
			},
			{
				5,
				1,
				27,
				26928
			},
			{
				0,
				2,
				13,
				55600
			},
			{
				0,
				2,
				3,
				23200
			},
			{
				3,
				1,
				23,
				43856
			},
			{
				0,
				2,
				11,
				38608
			},
			{
				11,
				1,
				31,
				19176
			},
			{
				0,
				2,
				19,
				19168
			},
			{
				0,
				2,
				8,
				42192
			},
			{
				6,
				1,
				28,
				53864
			},
			{
				0,
				2,
				15,
				53840
			},
			{
				0,
				2,
				4,
				54560
			},
			{
				5,
				1,
				24,
				55968
			},
			{
				0,
				2,
				12,
				46752
			},
			{
				0,
				2,
				1,
				38608
			},
			{
				2,
				1,
				22,
				19160
			},
			{
				0,
				2,
				10,
				18864
			},
			{
				7,
				1,
				30,
				42168
			},
			{
				0,
				2,
				17,
				42160
			},
			{
				0,
				2,
				6,
				45648
			},
			{
				5,
				1,
				26,
				46376
			},
			{
				0,
				2,
				14,
				27968
			},
			{
				0,
				2,
				2,
				44448
			}
		};
	}
}
