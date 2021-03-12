using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core
{
	// Token: 0x0200002D RID: 45
	internal static class Countries
	{
		// Token: 0x0600061D RID: 1565 RVA: 0x0001343C File Offset: 0x0001163C
		static Countries()
		{
			Countries.stringIDs.Add(3216539067U, "BT_64_34");
			Countries.stringIDs.Add(1699208365U, "DZ_12_4");
			Countries.stringIDs.Add(886545238U, "KI_296_133");
			Countries.stringIDs.Add(1759591854U, "LS_426_146");
			Countries.stringIDs.Add(2344949245U, "TC_796_349");
			Countries.stringIDs.Add(53559490U, "LI_438_145");
			Countries.stringIDs.Add(170381786U, "HR_191_108");
			Countries.stringIDs.Add(1977258562U, "VC_670_248");
			Countries.stringIDs.Add(74285194U, "AN_530_333");
			Countries.stringIDs.Add(1999176122U, "JO_400_126");
			Countries.stringIDs.Add(2368586127U, "PM_666_206");
			Countries.stringIDs.Add(1498891191U, "RU_643_203");
			Countries.stringIDs.Add(3794883283U, "DE_276_94");
			Countries.stringIDs.Add(2103499475U, "PY_600_185");
			Countries.stringIDs.Add(2019047508U, "KE_404_129");
			Countries.stringIDs.Add(1082660990U, "CN_156_45");
			Countries.stringIDs.Add(2512148763U, "CL_152_46");
			Countries.stringIDs.Add(1330749269U, "TG_768_232");
			Countries.stringIDs.Add(2005956254U, "BR_76_32");
			Countries.stringIDs.Add(1592266550U, "IS_352_110");
			Countries.stringIDs.Add(1174698536U, "ST_678_233");
			Countries.stringIDs.Add(377801761U, "SA_682_205");
			Countries.stringIDs.Add(3159776897U, "ZW_716_264");
			Countries.stringIDs.Add(12936108U, "UZ_860_247");
			Countries.stringIDs.Add(3194236654U, "MA_504_159");
			Countries.stringIDs.Add(1422904081U, "GB_826_242");
			Countries.stringIDs.Add(2764932561U, "SS_728_0");
			Countries.stringIDs.Add(1200282230U, "VE_862_249");
			Countries.stringIDs.Add(1037502137U, "AO_24_9");
			Countries.stringIDs.Add(2125592240U, "CC_166_311");
			Countries.stringIDs.Add(621889182U, "CM_120_49");
			Countries.stringIDs.Add(999565292U, "CY_196_59");
			Countries.stringIDs.Add(373651598U, "NU_570_335");
			Countries.stringIDs.Add(55331012U, "TR_792_235");
			Countries.stringIDs.Add(645986668U, "KG_417_130");
			Countries.stringIDs.Add(702227408U, "SL_694_213");
			Countries.stringIDs.Add(3602902706U, "BG_100_35");
			Countries.stringIDs.Add(3029734706U, "LT_440_141");
			Countries.stringIDs.Add(2562573117U, "TK_772_347");
			Countries.stringIDs.Add(1004246475U, "GF_254_317");
			Countries.stringIDs.Add(4002790908U, "IL_376_117");
			Countries.stringIDs.Add(3087470301U, "MH_584_199");
			Countries.stringIDs.Add(56143679U, "NP_524_178");
			Countries.stringIDs.Add(4104315101U, "BI_108_38");
			Countries.stringIDs.Add(636004392U, "HT_332_103");
			Countries.stringIDs.Add(2620723795U, "JE_832_328");
			Countries.stringIDs.Add(1441347144U, "VU_548_174");
			Countries.stringIDs.Add(2320250384U, "BY_112_29");
			Countries.stringIDs.Add(3646896638U, "QA_634_197");
			Countries.stringIDs.Add(2144138889U, "UY_858_246");
			Countries.stringIDs.Add(3789356486U, "GE_268_88");
			Countries.stringIDs.Add(2818474250U, "BW_72_19");
			Countries.stringIDs.Add(1718335698U, "NZ_554_183");
			Countries.stringIDs.Add(151849765U, "GU_316_322");
			Countries.stringIDs.Add(1843367414U, "BJ_204_28");
			Countries.stringIDs.Add(4222597191U, "AM_51_7");
			Countries.stringIDs.Add(1017460496U, "LA_418_138");
			Countries.stringIDs.Add(2132819958U, "LC_662_218");
			Countries.stringIDs.Add(3469746324U, "GL_304_93");
			Countries.stringIDs.Add(46288124U, "GW_624_196");
			Countries.stringIDs.Add(359940844U, "WF_876_352");
			Countries.stringIDs.Add(673912308U, "HN_340_106");
			Countries.stringIDs.Add(3393077234U, "BD_50_23");
			Countries.stringIDs.Add(3346454225U, "MO_446_151");
			Countries.stringIDs.Add(471386291U, "AX_248_0");
			Countries.stringIDs.Add(1333137622U, "XX_581_329");
			Countries.stringIDs.Add(4169766046U, "KZ_398_137");
			Countries.stringIDs.Add(3768655288U, "SJ_744_125");
			Countries.stringIDs.Add(1626681835U, "UA_804_241");
			Countries.stringIDs.Add(1734702041U, "SY_760_222");
			Countries.stringIDs.Add(1952734284U, "IO_86_114");
			Countries.stringIDs.Add(2248145120U, "GT_320_99");
			Countries.stringIDs.Add(1528190210U, "ER_232_71");
			Countries.stringIDs.Add(1633154011U, "BO_68_26");
			Countries.stringIDs.Add(19448000U, "BV_74_306");
			Countries.stringIDs.Add(1476028295U, "FJ_242_78");
			Countries.stringIDs.Add(803870055U, "MP_580_337");
			Countries.stringIDs.Add(2821667208U, "HU_348_109");
			Countries.stringIDs.Add(1635698251U, "SD_736_219");
			Countries.stringIDs.Add(987373943U, "LY_434_148");
			Countries.stringIDs.Add(940316003U, "JM_388_124");
			Countries.stringIDs.Add(1798672883U, "TV_798_236");
			Countries.stringIDs.Add(237449392U, "CZ_203_75");
			Countries.stringIDs.Add(3044481890U, "DJ_262_62");
			Countries.stringIDs.Add(2547794864U, "TT_780_225");
			Countries.stringIDs.Add(2237927463U, "DO_214_65");
			Countries.stringIDs.Add(2346964842U, "MM_104_27");
			Countries.stringIDs.Add(1696279285U, "MZ_508_168");
			Countries.stringIDs.Add(1175635471U, "BM_60_20");
			Countries.stringIDs.Add(1364962133U, "PH_608_201");
			Countries.stringIDs.Add(340243436U, "SN_686_210");
			Countries.stringIDs.Add(2135437507U, "NG_566_175");
			Countries.stringIDs.Add(980293189U, "HK_344_104");
			Countries.stringIDs.Add(3086115215U, "SE_752_221");
			Countries.stringIDs.Add(925955849U, "ZM_894_263");
			Countries.stringIDs.Add(363631353U, "LU_442_147");
			Countries.stringIDs.Add(1242399272U, "MX_484_166");
			Countries.stringIDs.Add(3729559185U, "PE_604_187");
			Countries.stringIDs.Add(588242161U, "MW_454_156");
			Countries.stringIDs.Add(2083627458U, "AU_36_12");
			Countries.stringIDs.Add(2041103264U, "MN_496_154");
			Countries.stringIDs.Add(1824151771U, "GA_266_87");
			Countries.stringIDs.Add(563124314U, "CF_140_55");
			Countries.stringIDs.Add(3453824596U, "NA_516_254");
			Countries.stringIDs.Add(2482828915U, "ES_724_217");
			Countries.stringIDs.Add(1308671137U, "GN_324_100");
			Countries.stringIDs.Add(1369040140U, "SM_674_214");
			Countries.stringIDs.Add(2073326121U, "EC_218_66");
			Countries.stringIDs.Add(4095102297U, "MY_458_167");
			Countries.stringIDs.Add(2075754043U, "PF_258_318");
			Countries.stringIDs.Add(2648439129U, "HM_334_325");
			Countries.stringIDs.Add(3696500301U, "VN_704_251");
			Countries.stringIDs.Add(3293351338U, "LK_144_42");
			Countries.stringIDs.Add(95964139U, "FK_238_315");
			Countries.stringIDs.Add(294596775U, "VI_850_252");
			Countries.stringIDs.Add(1733922887U, "PG_598_194");
			Countries.stringIDs.Add(4107113277U, "GM_270_86");
			Countries.stringIDs.Add(2753544182U, "IE_372_68");
			Countries.stringIDs.Add(592549877U, "CW_531_273");
			Countries.stringIDs.Add(2583116736U, "RE_638_198");
			Countries.stringIDs.Add(747893188U, "BB_52_18");
			Countries.stringIDs.Add(3330283586U, "ME_499_0");
			Countries.stringIDs.Add(3004793540U, "TN_788_234");
			Countries.stringIDs.Add(2161146289U, "MC_492_158");
			Countries.stringIDs.Add(2758059055U, "XX_581_258");
			Countries.stringIDs.Add(3896219658U, "AR_32_11");
			Countries.stringIDs.Add(2554496014U, "SZ_748_260");
			Countries.stringIDs.Add(782746222U, "CD_180_44");
			Countries.stringIDs.Add(2165366304U, "AT_40_14");
			Countries.stringIDs.Add(1341011849U, "TZ_834_239");
			Countries.stringIDs.Add(4274738721U, "CR_188_54");
			Countries.stringIDs.Add(170580533U, "VA_336_253");
			Countries.stringIDs.Add(1584863906U, "SO_706_216");
			Countries.stringIDs.Add(2354249789U, "SI_705_212");
			Countries.stringIDs.Add(1201614501U, "MS_500_332");
			Countries.stringIDs.Add(2946275728U, "XX_581_305");
			Countries.stringIDs.Add(656316569U, "ET_231_73");
			Countries.stringIDs.Add(2040666425U, "FM_583_80");
			Countries.stringIDs.Add(545947107U, "BL_652_0");
			Countries.stringIDs.Add(1565053238U, "CO_170_51");
			Countries.stringIDs.Add(1428988741U, "FO_234_81");
			Countries.stringIDs.Add(2985947664U, "PK_586_190");
			Countries.stringIDs.Add(928593759U, "GG_831_324");
			Countries.stringIDs.Add(2976346167U, "RW_646_204");
			Countries.stringIDs.Add(437931514U, "SK_703_143");
			Countries.stringIDs.Add(4052652744U, "KY_136_307");
			Countries.stringIDs.Add(3719348580U, "CX_162_309");
			Countries.stringIDs.Add(4062020976U, "XX_581_338");
			Countries.stringIDs.Add(1754296421U, "X1_581_0");
			Countries.stringIDs.Add(3225390919U, "CA_124_39");
			Countries.stringIDs.Add(1365152447U, "SB_90_30");
			Countries.stringIDs.Add(3035186627U, "NE_562_173");
			Countries.stringIDs.Add(1688203950U, "BF_854_245");
			Countries.stringIDs.Add(1655549906U, "NF_574_336");
			Countries.stringIDs.Add(486955070U, "LR_430_142");
			Countries.stringIDs.Add(1018128401U, "VG_92_351");
			Countries.stringIDs.Add(599535093U, "KW_414_136");
			Countries.stringIDs.Add(396161002U, "SR_740_181");
			Countries.stringIDs.Add(3388400993U, "MR_478_162");
			Countries.stringIDs.Add(1705178486U, "SJ_744_220");
			Countries.stringIDs.Add(2587334274U, "GD_308_91");
			Countries.stringIDs.Add(3436089648U, "NO_578_177");
			Countries.stringIDs.Add(238207211U, "YT_175_331");
			Countries.stringIDs.Add(3688589511U, "CU_192_56");
			Countries.stringIDs.Add(1919917050U, "BZ_84_24");
			Countries.stringIDs.Add(1544599245U, "TW_158_237");
			Countries.stringIDs.Add(1783361531U, "CV_132_57");
			Countries.stringIDs.Add(537487922U, "WS_882_259");
			Countries.stringIDs.Add(1814940642U, "SH_654_343");
			Countries.stringIDs.Add(477577130U, "SV_222_72");
			Countries.stringIDs.Add(2287217386U, "MD_498_152");
			Countries.stringIDs.Add(3284921140U, "UM_581_0");
			Countries.stringIDs.Add(67238401U, "CK_184_312");
			Countries.stringIDs.Add(506016315U, "TL_626_7299303");
			Countries.stringIDs.Add(3734185636U, "AS_16_10");
			Countries.stringIDs.Add(1701642011U, "FI_246_77");
			Countries.stringIDs.Add(1849186379U, "EH_732_0");
			Countries.stringIDs.Add(2420224391U, "PT_620_193");
			Countries.stringIDs.Add(3729747244U, "IT_380_118");
			Countries.stringIDs.Add(3852071526U, "ZA_710_209");
			Countries.stringIDs.Add(2003796335U, "MU_480_160");
			Countries.stringIDs.Add(2583256387U, "BE_56_21");
			Countries.stringIDs.Add(2128751088U, "PN_612_339");
			Countries.stringIDs.Add(593733822U, "BQ_535_161832258");
			Countries.stringIDs.Add(367215151U, "MG_450_149");
			Countries.stringIDs.Add(194553287U, "GQ_226_69");
			Countries.stringIDs.Add(4278334414U, "YE_887_261");
			Countries.stringIDs.Add(4045474244U, "PA_591_192");
			Countries.stringIDs.Add(3843922323U, "GY_328_101");
			Countries.stringIDs.Add(4027338091U, "GR_300_98");
			Countries.stringIDs.Add(1024316098U, "PW_585_195");
			Countries.stringIDs.Add(4109075144U, "XX_581_327");
			Countries.stringIDs.Add(4154687243U, "PR_630_202");
			Countries.stringIDs.Add(610628279U, "BA_70_25");
			Countries.stringIDs.Add(764528929U, "IM_833_15126");
			Countries.stringIDs.Add(3143800767U, "OM_512_164");
			Countries.stringIDs.Add(1276695365U, "LV_428_140");
			Countries.stringIDs.Add(1638715076U, "IN_356_113");
			Countries.stringIDs.Add(764391797U, "TJ_762_228");
			Countries.stringIDs.Add(3187096181U, "LB_422_139");
			Countries.stringIDs.Add(1924338336U, "TD_148_41");
			Countries.stringIDs.Add(1501151314U, "XX_581_127");
			Countries.stringIDs.Add(4280564641U, "KM_174_50");
			Countries.stringIDs.Add(1604790229U, "ID_360_111");
			Countries.stringIDs.Add(1267503226U, "DK_208_61");
			Countries.stringIDs.Add(1299098906U, "KR_410_134");
			Countries.stringIDs.Add(1861222396U, "MT_470_163");
			Countries.stringIDs.Add(2962213296U, "KN_659_207");
			Countries.stringIDs.Add(159636800U, "NR_520_180");
			Countries.stringIDs.Add(1551997871U, "AD_20_8");
			Countries.stringIDs.Add(2053873167U, "RO_642_200");
			Countries.stringIDs.Add(3427917068U, "MF_663_0");
			Countries.stringIDs.Add(306446999U, "JP_392_122");
			Countries.stringIDs.Add(2769383184U, "AW_533_302");
			Countries.stringIDs.Add(399548618U, "EG_818_67");
			Countries.stringIDs.Add(1028635198U, "GH_288_89");
			Countries.stringIDs.Add(2656889581U, "CS_891_269");
			Countries.stringIDs.Add(2166136329U, "BS_44_22");
			Countries.stringIDs.Add(479226287U, "AI_660_300");
			Countries.stringIDs.Add(1882146854U, "BH_48_17");
			Countries.stringIDs.Add(3433977374U, "PS_275_184");
			Countries.stringIDs.Add(2602129354U, "KP_408_131");
			Countries.stringIDs.Add(752455398U, "MK_807_19618");
			Countries.stringIDs.Add(4274021177U, "DM_212_63");
			Countries.stringIDs.Add(2679938597U, "KH_116_40");
			Countries.stringIDs.Add(1528703389U, "MV_462_165");
			Countries.stringIDs.Add(1267550736U, "NI_558_182");
			Countries.stringIDs.Add(3553295807U, "BN_96_37");
			Countries.stringIDs.Add(2813375133U, "CG_178_43");
			Countries.stringIDs.Add(4203270843U, "AQ_10_301");
			Countries.stringIDs.Add(3386522572U, "TH_764_227");
			Countries.stringIDs.Add(448115262U, "CI_384_119");
			Countries.stringIDs.Add(1819434036U, "GI_292_90");
			Countries.stringIDs.Add(3422027552U, "NC_540_334");
			Countries.stringIDs.Add(3231516104U, "SX_534_30967");
			Countries.stringIDs.Add(3501602525U, "TO_776_231");
			Countries.stringIDs.Add(1985192657U, "SC_690_208");
			Countries.stringIDs.Add(175597935U, "GP_312_321");
			Countries.stringIDs.Add(399345023U, "MQ_474_330");
			Countries.stringIDs.Add(2965338442U, "PL_616_191");
			Countries.stringIDs.Add(2643193431U, "XX_581_21242");
			Countries.stringIDs.Add(17747661U, "TF_260_319");
			Countries.stringIDs.Add(1096093064U, "US_840_244");
			Countries.stringIDs.Add(2542991203U, "XX_581_326");
			Countries.stringIDs.Add(4043652412U, "UG_800_240");
			Countries.stringIDs.Add(1010461863U, "AE_784_224");
			Countries.stringIDs.Add(5918779U, "TM_795_238");
			Countries.stringIDs.Add(148087037U, "NL_528_176");
			Countries.stringIDs.Add(1712846428U, "AG_28_2");
			Countries.stringIDs.Add(2482983095U, "SG_702_215");
			Countries.stringIDs.Add(2173922407U, "AL_8_6");
			Countries.stringIDs.Add(477647416U, "AF_4_3");
			Countries.stringIDs.Add(2022988190U, "IR_364_116");
			Countries.stringIDs.Add(3265625955U, "FR_250_84");
			Countries.stringIDs.Add(3212103298U, "CH_756_223");
			Countries.stringIDs.Add(566440899U, "IQ_368_121");
			Countries.stringIDs.Add(2841443953U, "EE_233_70");
			Countries.stringIDs.Add(2954305905U, "GS_239_342");
			Countries.stringIDs.Add(727743244U, "ML_466_157");
			Countries.stringIDs.Add(2845896855U, "RS_688_0");
			Countries.stringIDs.Add(495964926U, "AZ_31_5");
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x000148DF File Offset: 0x00012ADF
		public static LocalizedString BT_64_34
		{
			get
			{
				return new LocalizedString("BT_64_34", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x000148F6 File Offset: 0x00012AF6
		public static LocalizedString DZ_12_4
		{
			get
			{
				return new LocalizedString("DZ_12_4", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x0001490D File Offset: 0x00012B0D
		public static LocalizedString KI_296_133
		{
			get
			{
				return new LocalizedString("KI_296_133", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x00014924 File Offset: 0x00012B24
		public static LocalizedString LS_426_146
		{
			get
			{
				return new LocalizedString("LS_426_146", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x0001493B File Offset: 0x00012B3B
		public static LocalizedString TC_796_349
		{
			get
			{
				return new LocalizedString("TC_796_349", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x00014952 File Offset: 0x00012B52
		public static LocalizedString LI_438_145
		{
			get
			{
				return new LocalizedString("LI_438_145", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x00014969 File Offset: 0x00012B69
		public static LocalizedString HR_191_108
		{
			get
			{
				return new LocalizedString("HR_191_108", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x00014980 File Offset: 0x00012B80
		public static LocalizedString VC_670_248
		{
			get
			{
				return new LocalizedString("VC_670_248", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x00014997 File Offset: 0x00012B97
		public static LocalizedString AN_530_333
		{
			get
			{
				return new LocalizedString("AN_530_333", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x000149AE File Offset: 0x00012BAE
		public static LocalizedString JO_400_126
		{
			get
			{
				return new LocalizedString("JO_400_126", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x000149C5 File Offset: 0x00012BC5
		public static LocalizedString PM_666_206
		{
			get
			{
				return new LocalizedString("PM_666_206", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x000149DC File Offset: 0x00012BDC
		public static LocalizedString RU_643_203
		{
			get
			{
				return new LocalizedString("RU_643_203", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x000149F3 File Offset: 0x00012BF3
		public static LocalizedString DE_276_94
		{
			get
			{
				return new LocalizedString("DE_276_94", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x00014A0A File Offset: 0x00012C0A
		public static LocalizedString PY_600_185
		{
			get
			{
				return new LocalizedString("PY_600_185", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x00014A21 File Offset: 0x00012C21
		public static LocalizedString KE_404_129
		{
			get
			{
				return new LocalizedString("KE_404_129", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x00014A38 File Offset: 0x00012C38
		public static LocalizedString CN_156_45
		{
			get
			{
				return new LocalizedString("CN_156_45", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x00014A4F File Offset: 0x00012C4F
		public static LocalizedString CL_152_46
		{
			get
			{
				return new LocalizedString("CL_152_46", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x00014A66 File Offset: 0x00012C66
		public static LocalizedString TG_768_232
		{
			get
			{
				return new LocalizedString("TG_768_232", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x00014A7D File Offset: 0x00012C7D
		public static LocalizedString BR_76_32
		{
			get
			{
				return new LocalizedString("BR_76_32", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x00014A94 File Offset: 0x00012C94
		public static LocalizedString IS_352_110
		{
			get
			{
				return new LocalizedString("IS_352_110", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x00014AAB File Offset: 0x00012CAB
		public static LocalizedString ST_678_233
		{
			get
			{
				return new LocalizedString("ST_678_233", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x00014AC2 File Offset: 0x00012CC2
		public static LocalizedString SA_682_205
		{
			get
			{
				return new LocalizedString("SA_682_205", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x00014AD9 File Offset: 0x00012CD9
		public static LocalizedString ZW_716_264
		{
			get
			{
				return new LocalizedString("ZW_716_264", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x00014AF0 File Offset: 0x00012CF0
		public static LocalizedString UZ_860_247
		{
			get
			{
				return new LocalizedString("UZ_860_247", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x00014B07 File Offset: 0x00012D07
		public static LocalizedString MA_504_159
		{
			get
			{
				return new LocalizedString("MA_504_159", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x00014B1E File Offset: 0x00012D1E
		public static LocalizedString GB_826_242
		{
			get
			{
				return new LocalizedString("GB_826_242", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x00014B35 File Offset: 0x00012D35
		public static LocalizedString SS_728_0
		{
			get
			{
				return new LocalizedString("SS_728_0", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x00014B4C File Offset: 0x00012D4C
		public static LocalizedString VE_862_249
		{
			get
			{
				return new LocalizedString("VE_862_249", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x00014B63 File Offset: 0x00012D63
		public static LocalizedString AO_24_9
		{
			get
			{
				return new LocalizedString("AO_24_9", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x00014B7A File Offset: 0x00012D7A
		public static LocalizedString CC_166_311
		{
			get
			{
				return new LocalizedString("CC_166_311", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x00014B91 File Offset: 0x00012D91
		public static LocalizedString CM_120_49
		{
			get
			{
				return new LocalizedString("CM_120_49", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x00014BA8 File Offset: 0x00012DA8
		public static LocalizedString CY_196_59
		{
			get
			{
				return new LocalizedString("CY_196_59", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x00014BBF File Offset: 0x00012DBF
		public static LocalizedString NU_570_335
		{
			get
			{
				return new LocalizedString("NU_570_335", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x00014BD6 File Offset: 0x00012DD6
		public static LocalizedString TR_792_235
		{
			get
			{
				return new LocalizedString("TR_792_235", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x00014BED File Offset: 0x00012DED
		public static LocalizedString KG_417_130
		{
			get
			{
				return new LocalizedString("KG_417_130", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x00014C04 File Offset: 0x00012E04
		public static LocalizedString SL_694_213
		{
			get
			{
				return new LocalizedString("SL_694_213", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x00014C1B File Offset: 0x00012E1B
		public static LocalizedString BG_100_35
		{
			get
			{
				return new LocalizedString("BG_100_35", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x00014C32 File Offset: 0x00012E32
		public static LocalizedString LT_440_141
		{
			get
			{
				return new LocalizedString("LT_440_141", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x00014C49 File Offset: 0x00012E49
		public static LocalizedString TK_772_347
		{
			get
			{
				return new LocalizedString("TK_772_347", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x00014C60 File Offset: 0x00012E60
		public static LocalizedString GF_254_317
		{
			get
			{
				return new LocalizedString("GF_254_317", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x00014C77 File Offset: 0x00012E77
		public static LocalizedString IL_376_117
		{
			get
			{
				return new LocalizedString("IL_376_117", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x00014C8E File Offset: 0x00012E8E
		public static LocalizedString MH_584_199
		{
			get
			{
				return new LocalizedString("MH_584_199", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x00014CA5 File Offset: 0x00012EA5
		public static LocalizedString NP_524_178
		{
			get
			{
				return new LocalizedString("NP_524_178", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x00014CBC File Offset: 0x00012EBC
		public static LocalizedString BI_108_38
		{
			get
			{
				return new LocalizedString("BI_108_38", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x00014CD3 File Offset: 0x00012ED3
		public static LocalizedString HT_332_103
		{
			get
			{
				return new LocalizedString("HT_332_103", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x00014CEA File Offset: 0x00012EEA
		public static LocalizedString JE_832_328
		{
			get
			{
				return new LocalizedString("JE_832_328", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x00014D01 File Offset: 0x00012F01
		public static LocalizedString VU_548_174
		{
			get
			{
				return new LocalizedString("VU_548_174", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x00014D18 File Offset: 0x00012F18
		public static LocalizedString BY_112_29
		{
			get
			{
				return new LocalizedString("BY_112_29", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x00014D2F File Offset: 0x00012F2F
		public static LocalizedString QA_634_197
		{
			get
			{
				return new LocalizedString("QA_634_197", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x00014D46 File Offset: 0x00012F46
		public static LocalizedString UY_858_246
		{
			get
			{
				return new LocalizedString("UY_858_246", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x00014D5D File Offset: 0x00012F5D
		public static LocalizedString GE_268_88
		{
			get
			{
				return new LocalizedString("GE_268_88", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x00014D74 File Offset: 0x00012F74
		public static LocalizedString BW_72_19
		{
			get
			{
				return new LocalizedString("BW_72_19", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x00014D8B File Offset: 0x00012F8B
		public static LocalizedString NZ_554_183
		{
			get
			{
				return new LocalizedString("NZ_554_183", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x00014DA2 File Offset: 0x00012FA2
		public static LocalizedString GU_316_322
		{
			get
			{
				return new LocalizedString("GU_316_322", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x00014DB9 File Offset: 0x00012FB9
		public static LocalizedString BJ_204_28
		{
			get
			{
				return new LocalizedString("BJ_204_28", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x00014DD0 File Offset: 0x00012FD0
		public static LocalizedString AM_51_7
		{
			get
			{
				return new LocalizedString("AM_51_7", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x00014DE7 File Offset: 0x00012FE7
		public static LocalizedString LA_418_138
		{
			get
			{
				return new LocalizedString("LA_418_138", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x00014DFE File Offset: 0x00012FFE
		public static LocalizedString LC_662_218
		{
			get
			{
				return new LocalizedString("LC_662_218", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x00014E15 File Offset: 0x00013015
		public static LocalizedString GL_304_93
		{
			get
			{
				return new LocalizedString("GL_304_93", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x00014E2C File Offset: 0x0001302C
		public static LocalizedString GW_624_196
		{
			get
			{
				return new LocalizedString("GW_624_196", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x00014E43 File Offset: 0x00013043
		public static LocalizedString WF_876_352
		{
			get
			{
				return new LocalizedString("WF_876_352", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x00014E5A File Offset: 0x0001305A
		public static LocalizedString HN_340_106
		{
			get
			{
				return new LocalizedString("HN_340_106", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x00014E71 File Offset: 0x00013071
		public static LocalizedString BD_50_23
		{
			get
			{
				return new LocalizedString("BD_50_23", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x00014E88 File Offset: 0x00013088
		public static LocalizedString MO_446_151
		{
			get
			{
				return new LocalizedString("MO_446_151", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x00014E9F File Offset: 0x0001309F
		public static LocalizedString AX_248_0
		{
			get
			{
				return new LocalizedString("AX_248_0", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x00014EB6 File Offset: 0x000130B6
		public static LocalizedString XX_581_329
		{
			get
			{
				return new LocalizedString("XX_581_329", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x00014ECD File Offset: 0x000130CD
		public static LocalizedString KZ_398_137
		{
			get
			{
				return new LocalizedString("KZ_398_137", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x00014EE4 File Offset: 0x000130E4
		public static LocalizedString SJ_744_125
		{
			get
			{
				return new LocalizedString("SJ_744_125", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x00014EFB File Offset: 0x000130FB
		public static LocalizedString UA_804_241
		{
			get
			{
				return new LocalizedString("UA_804_241", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x00014F12 File Offset: 0x00013112
		public static LocalizedString SY_760_222
		{
			get
			{
				return new LocalizedString("SY_760_222", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x00014F29 File Offset: 0x00013129
		public static LocalizedString IO_86_114
		{
			get
			{
				return new LocalizedString("IO_86_114", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x00014F40 File Offset: 0x00013140
		public static LocalizedString GT_320_99
		{
			get
			{
				return new LocalizedString("GT_320_99", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x00014F57 File Offset: 0x00013157
		public static LocalizedString ER_232_71
		{
			get
			{
				return new LocalizedString("ER_232_71", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x00014F6E File Offset: 0x0001316E
		public static LocalizedString BO_68_26
		{
			get
			{
				return new LocalizedString("BO_68_26", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x00014F85 File Offset: 0x00013185
		public static LocalizedString BV_74_306
		{
			get
			{
				return new LocalizedString("BV_74_306", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x00014F9C File Offset: 0x0001319C
		public static LocalizedString FJ_242_78
		{
			get
			{
				return new LocalizedString("FJ_242_78", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x00014FB3 File Offset: 0x000131B3
		public static LocalizedString MP_580_337
		{
			get
			{
				return new LocalizedString("MP_580_337", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x00014FCA File Offset: 0x000131CA
		public static LocalizedString HU_348_109
		{
			get
			{
				return new LocalizedString("HU_348_109", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x00014FE1 File Offset: 0x000131E1
		public static LocalizedString SD_736_219
		{
			get
			{
				return new LocalizedString("SD_736_219", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x00014FF8 File Offset: 0x000131F8
		public static LocalizedString LY_434_148
		{
			get
			{
				return new LocalizedString("LY_434_148", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x0001500F File Offset: 0x0001320F
		public static LocalizedString JM_388_124
		{
			get
			{
				return new LocalizedString("JM_388_124", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x00015026 File Offset: 0x00013226
		public static LocalizedString TV_798_236
		{
			get
			{
				return new LocalizedString("TV_798_236", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x0001503D File Offset: 0x0001323D
		public static LocalizedString CZ_203_75
		{
			get
			{
				return new LocalizedString("CZ_203_75", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x00015054 File Offset: 0x00013254
		public static LocalizedString DJ_262_62
		{
			get
			{
				return new LocalizedString("DJ_262_62", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x0001506B File Offset: 0x0001326B
		public static LocalizedString TT_780_225
		{
			get
			{
				return new LocalizedString("TT_780_225", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x00015082 File Offset: 0x00013282
		public static LocalizedString DO_214_65
		{
			get
			{
				return new LocalizedString("DO_214_65", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x00015099 File Offset: 0x00013299
		public static LocalizedString MM_104_27
		{
			get
			{
				return new LocalizedString("MM_104_27", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x000150B0 File Offset: 0x000132B0
		public static LocalizedString MZ_508_168
		{
			get
			{
				return new LocalizedString("MZ_508_168", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x000150C7 File Offset: 0x000132C7
		public static LocalizedString BM_60_20
		{
			get
			{
				return new LocalizedString("BM_60_20", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x000150DE File Offset: 0x000132DE
		public static LocalizedString PH_608_201
		{
			get
			{
				return new LocalizedString("PH_608_201", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x000150F5 File Offset: 0x000132F5
		public static LocalizedString SN_686_210
		{
			get
			{
				return new LocalizedString("SN_686_210", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x0001510C File Offset: 0x0001330C
		public static LocalizedString NG_566_175
		{
			get
			{
				return new LocalizedString("NG_566_175", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x00015123 File Offset: 0x00013323
		public static LocalizedString HK_344_104
		{
			get
			{
				return new LocalizedString("HK_344_104", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x0001513A File Offset: 0x0001333A
		public static LocalizedString SE_752_221
		{
			get
			{
				return new LocalizedString("SE_752_221", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x00015151 File Offset: 0x00013351
		public static LocalizedString ZM_894_263
		{
			get
			{
				return new LocalizedString("ZM_894_263", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x00015168 File Offset: 0x00013368
		public static LocalizedString LU_442_147
		{
			get
			{
				return new LocalizedString("LU_442_147", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x0001517F File Offset: 0x0001337F
		public static LocalizedString MX_484_166
		{
			get
			{
				return new LocalizedString("MX_484_166", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x00015196 File Offset: 0x00013396
		public static LocalizedString PE_604_187
		{
			get
			{
				return new LocalizedString("PE_604_187", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x000151AD File Offset: 0x000133AD
		public static LocalizedString MW_454_156
		{
			get
			{
				return new LocalizedString("MW_454_156", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x000151C4 File Offset: 0x000133C4
		public static LocalizedString AU_36_12
		{
			get
			{
				return new LocalizedString("AU_36_12", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x000151DB File Offset: 0x000133DB
		public static LocalizedString MN_496_154
		{
			get
			{
				return new LocalizedString("MN_496_154", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x000151F2 File Offset: 0x000133F2
		public static LocalizedString GA_266_87
		{
			get
			{
				return new LocalizedString("GA_266_87", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x00015209 File Offset: 0x00013409
		public static LocalizedString CF_140_55
		{
			get
			{
				return new LocalizedString("CF_140_55", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x00015220 File Offset: 0x00013420
		public static LocalizedString NA_516_254
		{
			get
			{
				return new LocalizedString("NA_516_254", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x00015237 File Offset: 0x00013437
		public static LocalizedString ES_724_217
		{
			get
			{
				return new LocalizedString("ES_724_217", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x0001524E File Offset: 0x0001344E
		public static LocalizedString GN_324_100
		{
			get
			{
				return new LocalizedString("GN_324_100", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x00015265 File Offset: 0x00013465
		public static LocalizedString SM_674_214
		{
			get
			{
				return new LocalizedString("SM_674_214", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x0001527C File Offset: 0x0001347C
		public static LocalizedString EC_218_66
		{
			get
			{
				return new LocalizedString("EC_218_66", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x00015293 File Offset: 0x00013493
		public static LocalizedString MY_458_167
		{
			get
			{
				return new LocalizedString("MY_458_167", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x000152AA File Offset: 0x000134AA
		public static LocalizedString PF_258_318
		{
			get
			{
				return new LocalizedString("PF_258_318", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x000152C1 File Offset: 0x000134C1
		public static LocalizedString HM_334_325
		{
			get
			{
				return new LocalizedString("HM_334_325", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x000152D8 File Offset: 0x000134D8
		public static LocalizedString VN_704_251
		{
			get
			{
				return new LocalizedString("VN_704_251", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x000152EF File Offset: 0x000134EF
		public static LocalizedString LK_144_42
		{
			get
			{
				return new LocalizedString("LK_144_42", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x00015306 File Offset: 0x00013506
		public static LocalizedString FK_238_315
		{
			get
			{
				return new LocalizedString("FK_238_315", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x0001531D File Offset: 0x0001351D
		public static LocalizedString VI_850_252
		{
			get
			{
				return new LocalizedString("VI_850_252", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x00015334 File Offset: 0x00013534
		public static LocalizedString PG_598_194
		{
			get
			{
				return new LocalizedString("PG_598_194", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x0001534B File Offset: 0x0001354B
		public static LocalizedString GM_270_86
		{
			get
			{
				return new LocalizedString("GM_270_86", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x00015362 File Offset: 0x00013562
		public static LocalizedString IE_372_68
		{
			get
			{
				return new LocalizedString("IE_372_68", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x00015379 File Offset: 0x00013579
		public static LocalizedString CW_531_273
		{
			get
			{
				return new LocalizedString("CW_531_273", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x00015390 File Offset: 0x00013590
		public static LocalizedString RE_638_198
		{
			get
			{
				return new LocalizedString("RE_638_198", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x000153A7 File Offset: 0x000135A7
		public static LocalizedString BB_52_18
		{
			get
			{
				return new LocalizedString("BB_52_18", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x000153BE File Offset: 0x000135BE
		public static LocalizedString ME_499_0
		{
			get
			{
				return new LocalizedString("ME_499_0", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x000153D5 File Offset: 0x000135D5
		public static LocalizedString TN_788_234
		{
			get
			{
				return new LocalizedString("TN_788_234", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x000153EC File Offset: 0x000135EC
		public static LocalizedString MC_492_158
		{
			get
			{
				return new LocalizedString("MC_492_158", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x00015403 File Offset: 0x00013603
		public static LocalizedString XX_581_258
		{
			get
			{
				return new LocalizedString("XX_581_258", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x0001541A File Offset: 0x0001361A
		public static LocalizedString AR_32_11
		{
			get
			{
				return new LocalizedString("AR_32_11", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x00015431 File Offset: 0x00013631
		public static LocalizedString SZ_748_260
		{
			get
			{
				return new LocalizedString("SZ_748_260", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x00015448 File Offset: 0x00013648
		public static LocalizedString CD_180_44
		{
			get
			{
				return new LocalizedString("CD_180_44", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x0001545F File Offset: 0x0001365F
		public static LocalizedString AT_40_14
		{
			get
			{
				return new LocalizedString("AT_40_14", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x00015476 File Offset: 0x00013676
		public static LocalizedString TZ_834_239
		{
			get
			{
				return new LocalizedString("TZ_834_239", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x0001548D File Offset: 0x0001368D
		public static LocalizedString CR_188_54
		{
			get
			{
				return new LocalizedString("CR_188_54", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x000154A4 File Offset: 0x000136A4
		public static LocalizedString VA_336_253
		{
			get
			{
				return new LocalizedString("VA_336_253", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x000154BB File Offset: 0x000136BB
		public static LocalizedString SO_706_216
		{
			get
			{
				return new LocalizedString("SO_706_216", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x000154D2 File Offset: 0x000136D2
		public static LocalizedString SI_705_212
		{
			get
			{
				return new LocalizedString("SI_705_212", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x000154E9 File Offset: 0x000136E9
		public static LocalizedString MS_500_332
		{
			get
			{
				return new LocalizedString("MS_500_332", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x00015500 File Offset: 0x00013700
		public static LocalizedString XX_581_305
		{
			get
			{
				return new LocalizedString("XX_581_305", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x00015517 File Offset: 0x00013717
		public static LocalizedString ET_231_73
		{
			get
			{
				return new LocalizedString("ET_231_73", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0001552E File Offset: 0x0001372E
		public static LocalizedString FM_583_80
		{
			get
			{
				return new LocalizedString("FM_583_80", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x00015545 File Offset: 0x00013745
		public static LocalizedString BL_652_0
		{
			get
			{
				return new LocalizedString("BL_652_0", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0001555C File Offset: 0x0001375C
		public static LocalizedString CO_170_51
		{
			get
			{
				return new LocalizedString("CO_170_51", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x00015573 File Offset: 0x00013773
		public static LocalizedString FO_234_81
		{
			get
			{
				return new LocalizedString("FO_234_81", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x0001558A File Offset: 0x0001378A
		public static LocalizedString PK_586_190
		{
			get
			{
				return new LocalizedString("PK_586_190", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x000155A1 File Offset: 0x000137A1
		public static LocalizedString GG_831_324
		{
			get
			{
				return new LocalizedString("GG_831_324", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x000155B8 File Offset: 0x000137B8
		public static LocalizedString RW_646_204
		{
			get
			{
				return new LocalizedString("RW_646_204", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x000155CF File Offset: 0x000137CF
		public static LocalizedString SK_703_143
		{
			get
			{
				return new LocalizedString("SK_703_143", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x000155E6 File Offset: 0x000137E6
		public static LocalizedString KY_136_307
		{
			get
			{
				return new LocalizedString("KY_136_307", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x000155FD File Offset: 0x000137FD
		public static LocalizedString CX_162_309
		{
			get
			{
				return new LocalizedString("CX_162_309", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x00015614 File Offset: 0x00013814
		public static LocalizedString XX_581_338
		{
			get
			{
				return new LocalizedString("XX_581_338", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x0001562B File Offset: 0x0001382B
		public static LocalizedString X1_581_0
		{
			get
			{
				return new LocalizedString("X1_581_0", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x00015642 File Offset: 0x00013842
		public static LocalizedString CA_124_39
		{
			get
			{
				return new LocalizedString("CA_124_39", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00015659 File Offset: 0x00013859
		public static LocalizedString SB_90_30
		{
			get
			{
				return new LocalizedString("SB_90_30", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x00015670 File Offset: 0x00013870
		public static LocalizedString NE_562_173
		{
			get
			{
				return new LocalizedString("NE_562_173", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x00015687 File Offset: 0x00013887
		public static LocalizedString BF_854_245
		{
			get
			{
				return new LocalizedString("BF_854_245", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x0001569E File Offset: 0x0001389E
		public static LocalizedString NF_574_336
		{
			get
			{
				return new LocalizedString("NF_574_336", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x000156B5 File Offset: 0x000138B5
		public static LocalizedString LR_430_142
		{
			get
			{
				return new LocalizedString("LR_430_142", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x000156CC File Offset: 0x000138CC
		public static LocalizedString VG_92_351
		{
			get
			{
				return new LocalizedString("VG_92_351", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x000156E3 File Offset: 0x000138E3
		public static LocalizedString KW_414_136
		{
			get
			{
				return new LocalizedString("KW_414_136", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x000156FA File Offset: 0x000138FA
		public static LocalizedString SR_740_181
		{
			get
			{
				return new LocalizedString("SR_740_181", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x00015711 File Offset: 0x00013911
		public static LocalizedString MR_478_162
		{
			get
			{
				return new LocalizedString("MR_478_162", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x00015728 File Offset: 0x00013928
		public static LocalizedString SJ_744_220
		{
			get
			{
				return new LocalizedString("SJ_744_220", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x0001573F File Offset: 0x0001393F
		public static LocalizedString GD_308_91
		{
			get
			{
				return new LocalizedString("GD_308_91", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x00015756 File Offset: 0x00013956
		public static LocalizedString NO_578_177
		{
			get
			{
				return new LocalizedString("NO_578_177", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0001576D File Offset: 0x0001396D
		public static LocalizedString YT_175_331
		{
			get
			{
				return new LocalizedString("YT_175_331", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x00015784 File Offset: 0x00013984
		public static LocalizedString CU_192_56
		{
			get
			{
				return new LocalizedString("CU_192_56", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x0001579B File Offset: 0x0001399B
		public static LocalizedString BZ_84_24
		{
			get
			{
				return new LocalizedString("BZ_84_24", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x000157B2 File Offset: 0x000139B2
		public static LocalizedString TW_158_237
		{
			get
			{
				return new LocalizedString("TW_158_237", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x000157C9 File Offset: 0x000139C9
		public static LocalizedString CV_132_57
		{
			get
			{
				return new LocalizedString("CV_132_57", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x000157E0 File Offset: 0x000139E0
		public static LocalizedString WS_882_259
		{
			get
			{
				return new LocalizedString("WS_882_259", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x000157F7 File Offset: 0x000139F7
		public static LocalizedString SH_654_343
		{
			get
			{
				return new LocalizedString("SH_654_343", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0001580E File Offset: 0x00013A0E
		public static LocalizedString SV_222_72
		{
			get
			{
				return new LocalizedString("SV_222_72", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x00015825 File Offset: 0x00013A25
		public static LocalizedString MD_498_152
		{
			get
			{
				return new LocalizedString("MD_498_152", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x0001583C File Offset: 0x00013A3C
		public static LocalizedString UM_581_0
		{
			get
			{
				return new LocalizedString("UM_581_0", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x00015853 File Offset: 0x00013A53
		public static LocalizedString CK_184_312
		{
			get
			{
				return new LocalizedString("CK_184_312", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x0001586A File Offset: 0x00013A6A
		public static LocalizedString TL_626_7299303
		{
			get
			{
				return new LocalizedString("TL_626_7299303", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x00015881 File Offset: 0x00013A81
		public static LocalizedString AS_16_10
		{
			get
			{
				return new LocalizedString("AS_16_10", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x00015898 File Offset: 0x00013A98
		public static LocalizedString FI_246_77
		{
			get
			{
				return new LocalizedString("FI_246_77", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x000158AF File Offset: 0x00013AAF
		public static LocalizedString EH_732_0
		{
			get
			{
				return new LocalizedString("EH_732_0", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x000158C6 File Offset: 0x00013AC6
		public static LocalizedString PT_620_193
		{
			get
			{
				return new LocalizedString("PT_620_193", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x000158DD File Offset: 0x00013ADD
		public static LocalizedString IT_380_118
		{
			get
			{
				return new LocalizedString("IT_380_118", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x000158F4 File Offset: 0x00013AF4
		public static LocalizedString ZA_710_209
		{
			get
			{
				return new LocalizedString("ZA_710_209", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0001590B File Offset: 0x00013B0B
		public static LocalizedString MU_480_160
		{
			get
			{
				return new LocalizedString("MU_480_160", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x00015922 File Offset: 0x00013B22
		public static LocalizedString BE_56_21
		{
			get
			{
				return new LocalizedString("BE_56_21", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x00015939 File Offset: 0x00013B39
		public static LocalizedString PN_612_339
		{
			get
			{
				return new LocalizedString("PN_612_339", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x00015950 File Offset: 0x00013B50
		public static LocalizedString BQ_535_161832258
		{
			get
			{
				return new LocalizedString("BQ_535_161832258", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x00015967 File Offset: 0x00013B67
		public static LocalizedString MG_450_149
		{
			get
			{
				return new LocalizedString("MG_450_149", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x0001597E File Offset: 0x00013B7E
		public static LocalizedString GQ_226_69
		{
			get
			{
				return new LocalizedString("GQ_226_69", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x00015995 File Offset: 0x00013B95
		public static LocalizedString YE_887_261
		{
			get
			{
				return new LocalizedString("YE_887_261", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x000159AC File Offset: 0x00013BAC
		public static LocalizedString PA_591_192
		{
			get
			{
				return new LocalizedString("PA_591_192", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x000159C3 File Offset: 0x00013BC3
		public static LocalizedString GY_328_101
		{
			get
			{
				return new LocalizedString("GY_328_101", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x000159DA File Offset: 0x00013BDA
		public static LocalizedString GR_300_98
		{
			get
			{
				return new LocalizedString("GR_300_98", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x000159F1 File Offset: 0x00013BF1
		public static LocalizedString PW_585_195
		{
			get
			{
				return new LocalizedString("PW_585_195", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x00015A08 File Offset: 0x00013C08
		public static LocalizedString XX_581_327
		{
			get
			{
				return new LocalizedString("XX_581_327", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x00015A1F File Offset: 0x00013C1F
		public static LocalizedString PR_630_202
		{
			get
			{
				return new LocalizedString("PR_630_202", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x00015A36 File Offset: 0x00013C36
		public static LocalizedString BA_70_25
		{
			get
			{
				return new LocalizedString("BA_70_25", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x00015A4D File Offset: 0x00013C4D
		public static LocalizedString IM_833_15126
		{
			get
			{
				return new LocalizedString("IM_833_15126", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x00015A64 File Offset: 0x00013C64
		public static LocalizedString OM_512_164
		{
			get
			{
				return new LocalizedString("OM_512_164", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x00015A7B File Offset: 0x00013C7B
		public static LocalizedString LV_428_140
		{
			get
			{
				return new LocalizedString("LV_428_140", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x00015A92 File Offset: 0x00013C92
		public static LocalizedString IN_356_113
		{
			get
			{
				return new LocalizedString("IN_356_113", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x00015AA9 File Offset: 0x00013CA9
		public static LocalizedString TJ_762_228
		{
			get
			{
				return new LocalizedString("TJ_762_228", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x00015AC0 File Offset: 0x00013CC0
		public static LocalizedString LB_422_139
		{
			get
			{
				return new LocalizedString("LB_422_139", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x00015AD7 File Offset: 0x00013CD7
		public static LocalizedString TD_148_41
		{
			get
			{
				return new LocalizedString("TD_148_41", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x00015AEE File Offset: 0x00013CEE
		public static LocalizedString XX_581_127
		{
			get
			{
				return new LocalizedString("XX_581_127", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x00015B05 File Offset: 0x00013D05
		public static LocalizedString KM_174_50
		{
			get
			{
				return new LocalizedString("KM_174_50", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060006E9 RID: 1769 RVA: 0x00015B1C File Offset: 0x00013D1C
		public static LocalizedString ID_360_111
		{
			get
			{
				return new LocalizedString("ID_360_111", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x00015B33 File Offset: 0x00013D33
		public static LocalizedString DK_208_61
		{
			get
			{
				return new LocalizedString("DK_208_61", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x00015B4A File Offset: 0x00013D4A
		public static LocalizedString KR_410_134
		{
			get
			{
				return new LocalizedString("KR_410_134", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x00015B61 File Offset: 0x00013D61
		public static LocalizedString MT_470_163
		{
			get
			{
				return new LocalizedString("MT_470_163", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x00015B78 File Offset: 0x00013D78
		public static LocalizedString KN_659_207
		{
			get
			{
				return new LocalizedString("KN_659_207", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x00015B8F File Offset: 0x00013D8F
		public static LocalizedString NR_520_180
		{
			get
			{
				return new LocalizedString("NR_520_180", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x00015BA6 File Offset: 0x00013DA6
		public static LocalizedString AD_20_8
		{
			get
			{
				return new LocalizedString("AD_20_8", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x00015BBD File Offset: 0x00013DBD
		public static LocalizedString RO_642_200
		{
			get
			{
				return new LocalizedString("RO_642_200", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x00015BD4 File Offset: 0x00013DD4
		public static LocalizedString MF_663_0
		{
			get
			{
				return new LocalizedString("MF_663_0", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x00015BEB File Offset: 0x00013DEB
		public static LocalizedString JP_392_122
		{
			get
			{
				return new LocalizedString("JP_392_122", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x00015C02 File Offset: 0x00013E02
		public static LocalizedString AW_533_302
		{
			get
			{
				return new LocalizedString("AW_533_302", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x00015C19 File Offset: 0x00013E19
		public static LocalizedString EG_818_67
		{
			get
			{
				return new LocalizedString("EG_818_67", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x00015C30 File Offset: 0x00013E30
		public static LocalizedString GH_288_89
		{
			get
			{
				return new LocalizedString("GH_288_89", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x00015C47 File Offset: 0x00013E47
		public static LocalizedString CS_891_269
		{
			get
			{
				return new LocalizedString("CS_891_269", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x00015C5E File Offset: 0x00013E5E
		public static LocalizedString BS_44_22
		{
			get
			{
				return new LocalizedString("BS_44_22", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x00015C75 File Offset: 0x00013E75
		public static LocalizedString AI_660_300
		{
			get
			{
				return new LocalizedString("AI_660_300", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x00015C8C File Offset: 0x00013E8C
		public static LocalizedString BH_48_17
		{
			get
			{
				return new LocalizedString("BH_48_17", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x00015CA3 File Offset: 0x00013EA3
		public static LocalizedString PS_275_184
		{
			get
			{
				return new LocalizedString("PS_275_184", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x00015CBA File Offset: 0x00013EBA
		public static LocalizedString KP_408_131
		{
			get
			{
				return new LocalizedString("KP_408_131", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x00015CD1 File Offset: 0x00013ED1
		public static LocalizedString MK_807_19618
		{
			get
			{
				return new LocalizedString("MK_807_19618", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x00015CE8 File Offset: 0x00013EE8
		public static LocalizedString DM_212_63
		{
			get
			{
				return new LocalizedString("DM_212_63", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x00015CFF File Offset: 0x00013EFF
		public static LocalizedString KH_116_40
		{
			get
			{
				return new LocalizedString("KH_116_40", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x00015D16 File Offset: 0x00013F16
		public static LocalizedString MV_462_165
		{
			get
			{
				return new LocalizedString("MV_462_165", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x00015D2D File Offset: 0x00013F2D
		public static LocalizedString NI_558_182
		{
			get
			{
				return new LocalizedString("NI_558_182", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x00015D44 File Offset: 0x00013F44
		public static LocalizedString BN_96_37
		{
			get
			{
				return new LocalizedString("BN_96_37", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x00015D5B File Offset: 0x00013F5B
		public static LocalizedString CG_178_43
		{
			get
			{
				return new LocalizedString("CG_178_43", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x00015D72 File Offset: 0x00013F72
		public static LocalizedString AQ_10_301
		{
			get
			{
				return new LocalizedString("AQ_10_301", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x00015D89 File Offset: 0x00013F89
		public static LocalizedString TH_764_227
		{
			get
			{
				return new LocalizedString("TH_764_227", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x00015DA0 File Offset: 0x00013FA0
		public static LocalizedString CI_384_119
		{
			get
			{
				return new LocalizedString("CI_384_119", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x00015DB7 File Offset: 0x00013FB7
		public static LocalizedString GI_292_90
		{
			get
			{
				return new LocalizedString("GI_292_90", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x00015DCE File Offset: 0x00013FCE
		public static LocalizedString NC_540_334
		{
			get
			{
				return new LocalizedString("NC_540_334", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x00015DE5 File Offset: 0x00013FE5
		public static LocalizedString SX_534_30967
		{
			get
			{
				return new LocalizedString("SX_534_30967", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x00015DFC File Offset: 0x00013FFC
		public static LocalizedString TO_776_231
		{
			get
			{
				return new LocalizedString("TO_776_231", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x00015E13 File Offset: 0x00014013
		public static LocalizedString SC_690_208
		{
			get
			{
				return new LocalizedString("SC_690_208", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x00015E2A File Offset: 0x0001402A
		public static LocalizedString GP_312_321
		{
			get
			{
				return new LocalizedString("GP_312_321", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x00015E41 File Offset: 0x00014041
		public static LocalizedString MQ_474_330
		{
			get
			{
				return new LocalizedString("MQ_474_330", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x00015E58 File Offset: 0x00014058
		public static LocalizedString PL_616_191
		{
			get
			{
				return new LocalizedString("PL_616_191", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x00015E6F File Offset: 0x0001406F
		public static LocalizedString XX_581_21242
		{
			get
			{
				return new LocalizedString("XX_581_21242", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x00015E86 File Offset: 0x00014086
		public static LocalizedString TF_260_319
		{
			get
			{
				return new LocalizedString("TF_260_319", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x00015E9D File Offset: 0x0001409D
		public static LocalizedString US_840_244
		{
			get
			{
				return new LocalizedString("US_840_244", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x00015EB4 File Offset: 0x000140B4
		public static LocalizedString XX_581_326
		{
			get
			{
				return new LocalizedString("XX_581_326", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x00015ECB File Offset: 0x000140CB
		public static LocalizedString UG_800_240
		{
			get
			{
				return new LocalizedString("UG_800_240", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x00015EE2 File Offset: 0x000140E2
		public static LocalizedString AE_784_224
		{
			get
			{
				return new LocalizedString("AE_784_224", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x00015EF9 File Offset: 0x000140F9
		public static LocalizedString TM_795_238
		{
			get
			{
				return new LocalizedString("TM_795_238", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x00015F10 File Offset: 0x00014110
		public static LocalizedString NL_528_176
		{
			get
			{
				return new LocalizedString("NL_528_176", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x00015F27 File Offset: 0x00014127
		public static LocalizedString AG_28_2
		{
			get
			{
				return new LocalizedString("AG_28_2", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x00015F3E File Offset: 0x0001413E
		public static LocalizedString SG_702_215
		{
			get
			{
				return new LocalizedString("SG_702_215", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x00015F55 File Offset: 0x00014155
		public static LocalizedString AL_8_6
		{
			get
			{
				return new LocalizedString("AL_8_6", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x00015F6C File Offset: 0x0001416C
		public static LocalizedString AF_4_3
		{
			get
			{
				return new LocalizedString("AF_4_3", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x00015F83 File Offset: 0x00014183
		public static LocalizedString IR_364_116
		{
			get
			{
				return new LocalizedString("IR_364_116", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x00015F9A File Offset: 0x0001419A
		public static LocalizedString FR_250_84
		{
			get
			{
				return new LocalizedString("FR_250_84", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x00015FB1 File Offset: 0x000141B1
		public static LocalizedString CH_756_223
		{
			get
			{
				return new LocalizedString("CH_756_223", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x00015FC8 File Offset: 0x000141C8
		public static LocalizedString IQ_368_121
		{
			get
			{
				return new LocalizedString("IQ_368_121", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x00015FDF File Offset: 0x000141DF
		public static LocalizedString EE_233_70
		{
			get
			{
				return new LocalizedString("EE_233_70", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x00015FF6 File Offset: 0x000141F6
		public static LocalizedString GS_239_342
		{
			get
			{
				return new LocalizedString("GS_239_342", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x0001600D File Offset: 0x0001420D
		public static LocalizedString ML_466_157
		{
			get
			{
				return new LocalizedString("ML_466_157", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x00016024 File Offset: 0x00014224
		public static LocalizedString RS_688_0
		{
			get
			{
				return new LocalizedString("RS_688_0", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x0001603B File Offset: 0x0001423B
		public static LocalizedString AZ_31_5
		{
			get
			{
				return new LocalizedString("AZ_31_5", Countries.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00016052 File Offset: 0x00014252
		public static LocalizedString GetLocalizedString(Countries.IDs key)
		{
			return new LocalizedString(Countries.stringIDs[(uint)key], Countries.ResourceManager, new object[0]);
		}

		// Token: 0x040005A4 RID: 1444
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(261);

		// Token: 0x040005A5 RID: 1445
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Core.Countries", typeof(Countries).GetTypeInfo().Assembly);

		// Token: 0x0200002E RID: 46
		public enum IDs : uint
		{
			// Token: 0x040005A7 RID: 1447
			BT_64_34 = 3216539067U,
			// Token: 0x040005A8 RID: 1448
			DZ_12_4 = 1699208365U,
			// Token: 0x040005A9 RID: 1449
			KI_296_133 = 886545238U,
			// Token: 0x040005AA RID: 1450
			LS_426_146 = 1759591854U,
			// Token: 0x040005AB RID: 1451
			TC_796_349 = 2344949245U,
			// Token: 0x040005AC RID: 1452
			LI_438_145 = 53559490U,
			// Token: 0x040005AD RID: 1453
			HR_191_108 = 170381786U,
			// Token: 0x040005AE RID: 1454
			VC_670_248 = 1977258562U,
			// Token: 0x040005AF RID: 1455
			AN_530_333 = 74285194U,
			// Token: 0x040005B0 RID: 1456
			JO_400_126 = 1999176122U,
			// Token: 0x040005B1 RID: 1457
			PM_666_206 = 2368586127U,
			// Token: 0x040005B2 RID: 1458
			RU_643_203 = 1498891191U,
			// Token: 0x040005B3 RID: 1459
			DE_276_94 = 3794883283U,
			// Token: 0x040005B4 RID: 1460
			PY_600_185 = 2103499475U,
			// Token: 0x040005B5 RID: 1461
			KE_404_129 = 2019047508U,
			// Token: 0x040005B6 RID: 1462
			CN_156_45 = 1082660990U,
			// Token: 0x040005B7 RID: 1463
			CL_152_46 = 2512148763U,
			// Token: 0x040005B8 RID: 1464
			TG_768_232 = 1330749269U,
			// Token: 0x040005B9 RID: 1465
			BR_76_32 = 2005956254U,
			// Token: 0x040005BA RID: 1466
			IS_352_110 = 1592266550U,
			// Token: 0x040005BB RID: 1467
			ST_678_233 = 1174698536U,
			// Token: 0x040005BC RID: 1468
			SA_682_205 = 377801761U,
			// Token: 0x040005BD RID: 1469
			ZW_716_264 = 3159776897U,
			// Token: 0x040005BE RID: 1470
			UZ_860_247 = 12936108U,
			// Token: 0x040005BF RID: 1471
			MA_504_159 = 3194236654U,
			// Token: 0x040005C0 RID: 1472
			GB_826_242 = 1422904081U,
			// Token: 0x040005C1 RID: 1473
			SS_728_0 = 2764932561U,
			// Token: 0x040005C2 RID: 1474
			VE_862_249 = 1200282230U,
			// Token: 0x040005C3 RID: 1475
			AO_24_9 = 1037502137U,
			// Token: 0x040005C4 RID: 1476
			CC_166_311 = 2125592240U,
			// Token: 0x040005C5 RID: 1477
			CM_120_49 = 621889182U,
			// Token: 0x040005C6 RID: 1478
			CY_196_59 = 999565292U,
			// Token: 0x040005C7 RID: 1479
			NU_570_335 = 373651598U,
			// Token: 0x040005C8 RID: 1480
			TR_792_235 = 55331012U,
			// Token: 0x040005C9 RID: 1481
			KG_417_130 = 645986668U,
			// Token: 0x040005CA RID: 1482
			SL_694_213 = 702227408U,
			// Token: 0x040005CB RID: 1483
			BG_100_35 = 3602902706U,
			// Token: 0x040005CC RID: 1484
			LT_440_141 = 3029734706U,
			// Token: 0x040005CD RID: 1485
			TK_772_347 = 2562573117U,
			// Token: 0x040005CE RID: 1486
			GF_254_317 = 1004246475U,
			// Token: 0x040005CF RID: 1487
			IL_376_117 = 4002790908U,
			// Token: 0x040005D0 RID: 1488
			MH_584_199 = 3087470301U,
			// Token: 0x040005D1 RID: 1489
			NP_524_178 = 56143679U,
			// Token: 0x040005D2 RID: 1490
			BI_108_38 = 4104315101U,
			// Token: 0x040005D3 RID: 1491
			HT_332_103 = 636004392U,
			// Token: 0x040005D4 RID: 1492
			JE_832_328 = 2620723795U,
			// Token: 0x040005D5 RID: 1493
			VU_548_174 = 1441347144U,
			// Token: 0x040005D6 RID: 1494
			BY_112_29 = 2320250384U,
			// Token: 0x040005D7 RID: 1495
			QA_634_197 = 3646896638U,
			// Token: 0x040005D8 RID: 1496
			UY_858_246 = 2144138889U,
			// Token: 0x040005D9 RID: 1497
			GE_268_88 = 3789356486U,
			// Token: 0x040005DA RID: 1498
			BW_72_19 = 2818474250U,
			// Token: 0x040005DB RID: 1499
			NZ_554_183 = 1718335698U,
			// Token: 0x040005DC RID: 1500
			GU_316_322 = 151849765U,
			// Token: 0x040005DD RID: 1501
			BJ_204_28 = 1843367414U,
			// Token: 0x040005DE RID: 1502
			AM_51_7 = 4222597191U,
			// Token: 0x040005DF RID: 1503
			LA_418_138 = 1017460496U,
			// Token: 0x040005E0 RID: 1504
			LC_662_218 = 2132819958U,
			// Token: 0x040005E1 RID: 1505
			GL_304_93 = 3469746324U,
			// Token: 0x040005E2 RID: 1506
			GW_624_196 = 46288124U,
			// Token: 0x040005E3 RID: 1507
			WF_876_352 = 359940844U,
			// Token: 0x040005E4 RID: 1508
			HN_340_106 = 673912308U,
			// Token: 0x040005E5 RID: 1509
			BD_50_23 = 3393077234U,
			// Token: 0x040005E6 RID: 1510
			MO_446_151 = 3346454225U,
			// Token: 0x040005E7 RID: 1511
			AX_248_0 = 471386291U,
			// Token: 0x040005E8 RID: 1512
			XX_581_329 = 1333137622U,
			// Token: 0x040005E9 RID: 1513
			KZ_398_137 = 4169766046U,
			// Token: 0x040005EA RID: 1514
			SJ_744_125 = 3768655288U,
			// Token: 0x040005EB RID: 1515
			UA_804_241 = 1626681835U,
			// Token: 0x040005EC RID: 1516
			SY_760_222 = 1734702041U,
			// Token: 0x040005ED RID: 1517
			IO_86_114 = 1952734284U,
			// Token: 0x040005EE RID: 1518
			GT_320_99 = 2248145120U,
			// Token: 0x040005EF RID: 1519
			ER_232_71 = 1528190210U,
			// Token: 0x040005F0 RID: 1520
			BO_68_26 = 1633154011U,
			// Token: 0x040005F1 RID: 1521
			BV_74_306 = 19448000U,
			// Token: 0x040005F2 RID: 1522
			FJ_242_78 = 1476028295U,
			// Token: 0x040005F3 RID: 1523
			MP_580_337 = 803870055U,
			// Token: 0x040005F4 RID: 1524
			HU_348_109 = 2821667208U,
			// Token: 0x040005F5 RID: 1525
			SD_736_219 = 1635698251U,
			// Token: 0x040005F6 RID: 1526
			LY_434_148 = 987373943U,
			// Token: 0x040005F7 RID: 1527
			JM_388_124 = 940316003U,
			// Token: 0x040005F8 RID: 1528
			TV_798_236 = 1798672883U,
			// Token: 0x040005F9 RID: 1529
			CZ_203_75 = 237449392U,
			// Token: 0x040005FA RID: 1530
			DJ_262_62 = 3044481890U,
			// Token: 0x040005FB RID: 1531
			TT_780_225 = 2547794864U,
			// Token: 0x040005FC RID: 1532
			DO_214_65 = 2237927463U,
			// Token: 0x040005FD RID: 1533
			MM_104_27 = 2346964842U,
			// Token: 0x040005FE RID: 1534
			MZ_508_168 = 1696279285U,
			// Token: 0x040005FF RID: 1535
			BM_60_20 = 1175635471U,
			// Token: 0x04000600 RID: 1536
			PH_608_201 = 1364962133U,
			// Token: 0x04000601 RID: 1537
			SN_686_210 = 340243436U,
			// Token: 0x04000602 RID: 1538
			NG_566_175 = 2135437507U,
			// Token: 0x04000603 RID: 1539
			HK_344_104 = 980293189U,
			// Token: 0x04000604 RID: 1540
			SE_752_221 = 3086115215U,
			// Token: 0x04000605 RID: 1541
			ZM_894_263 = 925955849U,
			// Token: 0x04000606 RID: 1542
			LU_442_147 = 363631353U,
			// Token: 0x04000607 RID: 1543
			MX_484_166 = 1242399272U,
			// Token: 0x04000608 RID: 1544
			PE_604_187 = 3729559185U,
			// Token: 0x04000609 RID: 1545
			MW_454_156 = 588242161U,
			// Token: 0x0400060A RID: 1546
			AU_36_12 = 2083627458U,
			// Token: 0x0400060B RID: 1547
			MN_496_154 = 2041103264U,
			// Token: 0x0400060C RID: 1548
			GA_266_87 = 1824151771U,
			// Token: 0x0400060D RID: 1549
			CF_140_55 = 563124314U,
			// Token: 0x0400060E RID: 1550
			NA_516_254 = 3453824596U,
			// Token: 0x0400060F RID: 1551
			ES_724_217 = 2482828915U,
			// Token: 0x04000610 RID: 1552
			GN_324_100 = 1308671137U,
			// Token: 0x04000611 RID: 1553
			SM_674_214 = 1369040140U,
			// Token: 0x04000612 RID: 1554
			EC_218_66 = 2073326121U,
			// Token: 0x04000613 RID: 1555
			MY_458_167 = 4095102297U,
			// Token: 0x04000614 RID: 1556
			PF_258_318 = 2075754043U,
			// Token: 0x04000615 RID: 1557
			HM_334_325 = 2648439129U,
			// Token: 0x04000616 RID: 1558
			VN_704_251 = 3696500301U,
			// Token: 0x04000617 RID: 1559
			LK_144_42 = 3293351338U,
			// Token: 0x04000618 RID: 1560
			FK_238_315 = 95964139U,
			// Token: 0x04000619 RID: 1561
			VI_850_252 = 294596775U,
			// Token: 0x0400061A RID: 1562
			PG_598_194 = 1733922887U,
			// Token: 0x0400061B RID: 1563
			GM_270_86 = 4107113277U,
			// Token: 0x0400061C RID: 1564
			IE_372_68 = 2753544182U,
			// Token: 0x0400061D RID: 1565
			CW_531_273 = 592549877U,
			// Token: 0x0400061E RID: 1566
			RE_638_198 = 2583116736U,
			// Token: 0x0400061F RID: 1567
			BB_52_18 = 747893188U,
			// Token: 0x04000620 RID: 1568
			ME_499_0 = 3330283586U,
			// Token: 0x04000621 RID: 1569
			TN_788_234 = 3004793540U,
			// Token: 0x04000622 RID: 1570
			MC_492_158 = 2161146289U,
			// Token: 0x04000623 RID: 1571
			XX_581_258 = 2758059055U,
			// Token: 0x04000624 RID: 1572
			AR_32_11 = 3896219658U,
			// Token: 0x04000625 RID: 1573
			SZ_748_260 = 2554496014U,
			// Token: 0x04000626 RID: 1574
			CD_180_44 = 782746222U,
			// Token: 0x04000627 RID: 1575
			AT_40_14 = 2165366304U,
			// Token: 0x04000628 RID: 1576
			TZ_834_239 = 1341011849U,
			// Token: 0x04000629 RID: 1577
			CR_188_54 = 4274738721U,
			// Token: 0x0400062A RID: 1578
			VA_336_253 = 170580533U,
			// Token: 0x0400062B RID: 1579
			SO_706_216 = 1584863906U,
			// Token: 0x0400062C RID: 1580
			SI_705_212 = 2354249789U,
			// Token: 0x0400062D RID: 1581
			MS_500_332 = 1201614501U,
			// Token: 0x0400062E RID: 1582
			XX_581_305 = 2946275728U,
			// Token: 0x0400062F RID: 1583
			ET_231_73 = 656316569U,
			// Token: 0x04000630 RID: 1584
			FM_583_80 = 2040666425U,
			// Token: 0x04000631 RID: 1585
			BL_652_0 = 545947107U,
			// Token: 0x04000632 RID: 1586
			CO_170_51 = 1565053238U,
			// Token: 0x04000633 RID: 1587
			FO_234_81 = 1428988741U,
			// Token: 0x04000634 RID: 1588
			PK_586_190 = 2985947664U,
			// Token: 0x04000635 RID: 1589
			GG_831_324 = 928593759U,
			// Token: 0x04000636 RID: 1590
			RW_646_204 = 2976346167U,
			// Token: 0x04000637 RID: 1591
			SK_703_143 = 437931514U,
			// Token: 0x04000638 RID: 1592
			KY_136_307 = 4052652744U,
			// Token: 0x04000639 RID: 1593
			CX_162_309 = 3719348580U,
			// Token: 0x0400063A RID: 1594
			XX_581_338 = 4062020976U,
			// Token: 0x0400063B RID: 1595
			X1_581_0 = 1754296421U,
			// Token: 0x0400063C RID: 1596
			CA_124_39 = 3225390919U,
			// Token: 0x0400063D RID: 1597
			SB_90_30 = 1365152447U,
			// Token: 0x0400063E RID: 1598
			NE_562_173 = 3035186627U,
			// Token: 0x0400063F RID: 1599
			BF_854_245 = 1688203950U,
			// Token: 0x04000640 RID: 1600
			NF_574_336 = 1655549906U,
			// Token: 0x04000641 RID: 1601
			LR_430_142 = 486955070U,
			// Token: 0x04000642 RID: 1602
			VG_92_351 = 1018128401U,
			// Token: 0x04000643 RID: 1603
			KW_414_136 = 599535093U,
			// Token: 0x04000644 RID: 1604
			SR_740_181 = 396161002U,
			// Token: 0x04000645 RID: 1605
			MR_478_162 = 3388400993U,
			// Token: 0x04000646 RID: 1606
			SJ_744_220 = 1705178486U,
			// Token: 0x04000647 RID: 1607
			GD_308_91 = 2587334274U,
			// Token: 0x04000648 RID: 1608
			NO_578_177 = 3436089648U,
			// Token: 0x04000649 RID: 1609
			YT_175_331 = 238207211U,
			// Token: 0x0400064A RID: 1610
			CU_192_56 = 3688589511U,
			// Token: 0x0400064B RID: 1611
			BZ_84_24 = 1919917050U,
			// Token: 0x0400064C RID: 1612
			TW_158_237 = 1544599245U,
			// Token: 0x0400064D RID: 1613
			CV_132_57 = 1783361531U,
			// Token: 0x0400064E RID: 1614
			WS_882_259 = 537487922U,
			// Token: 0x0400064F RID: 1615
			SH_654_343 = 1814940642U,
			// Token: 0x04000650 RID: 1616
			SV_222_72 = 477577130U,
			// Token: 0x04000651 RID: 1617
			MD_498_152 = 2287217386U,
			// Token: 0x04000652 RID: 1618
			UM_581_0 = 3284921140U,
			// Token: 0x04000653 RID: 1619
			CK_184_312 = 67238401U,
			// Token: 0x04000654 RID: 1620
			TL_626_7299303 = 506016315U,
			// Token: 0x04000655 RID: 1621
			AS_16_10 = 3734185636U,
			// Token: 0x04000656 RID: 1622
			FI_246_77 = 1701642011U,
			// Token: 0x04000657 RID: 1623
			EH_732_0 = 1849186379U,
			// Token: 0x04000658 RID: 1624
			PT_620_193 = 2420224391U,
			// Token: 0x04000659 RID: 1625
			IT_380_118 = 3729747244U,
			// Token: 0x0400065A RID: 1626
			ZA_710_209 = 3852071526U,
			// Token: 0x0400065B RID: 1627
			MU_480_160 = 2003796335U,
			// Token: 0x0400065C RID: 1628
			BE_56_21 = 2583256387U,
			// Token: 0x0400065D RID: 1629
			PN_612_339 = 2128751088U,
			// Token: 0x0400065E RID: 1630
			BQ_535_161832258 = 593733822U,
			// Token: 0x0400065F RID: 1631
			MG_450_149 = 367215151U,
			// Token: 0x04000660 RID: 1632
			GQ_226_69 = 194553287U,
			// Token: 0x04000661 RID: 1633
			YE_887_261 = 4278334414U,
			// Token: 0x04000662 RID: 1634
			PA_591_192 = 4045474244U,
			// Token: 0x04000663 RID: 1635
			GY_328_101 = 3843922323U,
			// Token: 0x04000664 RID: 1636
			GR_300_98 = 4027338091U,
			// Token: 0x04000665 RID: 1637
			PW_585_195 = 1024316098U,
			// Token: 0x04000666 RID: 1638
			XX_581_327 = 4109075144U,
			// Token: 0x04000667 RID: 1639
			PR_630_202 = 4154687243U,
			// Token: 0x04000668 RID: 1640
			BA_70_25 = 610628279U,
			// Token: 0x04000669 RID: 1641
			IM_833_15126 = 764528929U,
			// Token: 0x0400066A RID: 1642
			OM_512_164 = 3143800767U,
			// Token: 0x0400066B RID: 1643
			LV_428_140 = 1276695365U,
			// Token: 0x0400066C RID: 1644
			IN_356_113 = 1638715076U,
			// Token: 0x0400066D RID: 1645
			TJ_762_228 = 764391797U,
			// Token: 0x0400066E RID: 1646
			LB_422_139 = 3187096181U,
			// Token: 0x0400066F RID: 1647
			TD_148_41 = 1924338336U,
			// Token: 0x04000670 RID: 1648
			XX_581_127 = 1501151314U,
			// Token: 0x04000671 RID: 1649
			KM_174_50 = 4280564641U,
			// Token: 0x04000672 RID: 1650
			ID_360_111 = 1604790229U,
			// Token: 0x04000673 RID: 1651
			DK_208_61 = 1267503226U,
			// Token: 0x04000674 RID: 1652
			KR_410_134 = 1299098906U,
			// Token: 0x04000675 RID: 1653
			MT_470_163 = 1861222396U,
			// Token: 0x04000676 RID: 1654
			KN_659_207 = 2962213296U,
			// Token: 0x04000677 RID: 1655
			NR_520_180 = 159636800U,
			// Token: 0x04000678 RID: 1656
			AD_20_8 = 1551997871U,
			// Token: 0x04000679 RID: 1657
			RO_642_200 = 2053873167U,
			// Token: 0x0400067A RID: 1658
			MF_663_0 = 3427917068U,
			// Token: 0x0400067B RID: 1659
			JP_392_122 = 306446999U,
			// Token: 0x0400067C RID: 1660
			AW_533_302 = 2769383184U,
			// Token: 0x0400067D RID: 1661
			EG_818_67 = 399548618U,
			// Token: 0x0400067E RID: 1662
			GH_288_89 = 1028635198U,
			// Token: 0x0400067F RID: 1663
			CS_891_269 = 2656889581U,
			// Token: 0x04000680 RID: 1664
			BS_44_22 = 2166136329U,
			// Token: 0x04000681 RID: 1665
			AI_660_300 = 479226287U,
			// Token: 0x04000682 RID: 1666
			BH_48_17 = 1882146854U,
			// Token: 0x04000683 RID: 1667
			PS_275_184 = 3433977374U,
			// Token: 0x04000684 RID: 1668
			KP_408_131 = 2602129354U,
			// Token: 0x04000685 RID: 1669
			MK_807_19618 = 752455398U,
			// Token: 0x04000686 RID: 1670
			DM_212_63 = 4274021177U,
			// Token: 0x04000687 RID: 1671
			KH_116_40 = 2679938597U,
			// Token: 0x04000688 RID: 1672
			MV_462_165 = 1528703389U,
			// Token: 0x04000689 RID: 1673
			NI_558_182 = 1267550736U,
			// Token: 0x0400068A RID: 1674
			BN_96_37 = 3553295807U,
			// Token: 0x0400068B RID: 1675
			CG_178_43 = 2813375133U,
			// Token: 0x0400068C RID: 1676
			AQ_10_301 = 4203270843U,
			// Token: 0x0400068D RID: 1677
			TH_764_227 = 3386522572U,
			// Token: 0x0400068E RID: 1678
			CI_384_119 = 448115262U,
			// Token: 0x0400068F RID: 1679
			GI_292_90 = 1819434036U,
			// Token: 0x04000690 RID: 1680
			NC_540_334 = 3422027552U,
			// Token: 0x04000691 RID: 1681
			SX_534_30967 = 3231516104U,
			// Token: 0x04000692 RID: 1682
			TO_776_231 = 3501602525U,
			// Token: 0x04000693 RID: 1683
			SC_690_208 = 1985192657U,
			// Token: 0x04000694 RID: 1684
			GP_312_321 = 175597935U,
			// Token: 0x04000695 RID: 1685
			MQ_474_330 = 399345023U,
			// Token: 0x04000696 RID: 1686
			PL_616_191 = 2965338442U,
			// Token: 0x04000697 RID: 1687
			XX_581_21242 = 2643193431U,
			// Token: 0x04000698 RID: 1688
			TF_260_319 = 17747661U,
			// Token: 0x04000699 RID: 1689
			US_840_244 = 1096093064U,
			// Token: 0x0400069A RID: 1690
			XX_581_326 = 2542991203U,
			// Token: 0x0400069B RID: 1691
			UG_800_240 = 4043652412U,
			// Token: 0x0400069C RID: 1692
			AE_784_224 = 1010461863U,
			// Token: 0x0400069D RID: 1693
			TM_795_238 = 5918779U,
			// Token: 0x0400069E RID: 1694
			NL_528_176 = 148087037U,
			// Token: 0x0400069F RID: 1695
			AG_28_2 = 1712846428U,
			// Token: 0x040006A0 RID: 1696
			SG_702_215 = 2482983095U,
			// Token: 0x040006A1 RID: 1697
			AL_8_6 = 2173922407U,
			// Token: 0x040006A2 RID: 1698
			AF_4_3 = 477647416U,
			// Token: 0x040006A3 RID: 1699
			IR_364_116 = 2022988190U,
			// Token: 0x040006A4 RID: 1700
			FR_250_84 = 3265625955U,
			// Token: 0x040006A5 RID: 1701
			CH_756_223 = 3212103298U,
			// Token: 0x040006A6 RID: 1702
			IQ_368_121 = 566440899U,
			// Token: 0x040006A7 RID: 1703
			EE_233_70 = 2841443953U,
			// Token: 0x040006A8 RID: 1704
			GS_239_342 = 2954305905U,
			// Token: 0x040006A9 RID: 1705
			ML_466_157 = 727743244U,
			// Token: 0x040006AA RID: 1706
			RS_688_0 = 2845896855U,
			// Token: 0x040006AB RID: 1707
			AZ_31_5 = 495964926U
		}
	}
}
