using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000234 RID: 564
	public struct PtTraceTags
	{
		// Token: 0x04000D18 RID: 3352
		public const int DS_DsAccess = 10;

		// Token: 0x04000D19 RID: 3353
		public const int DS_Mprw = 11;

		// Token: 0x04000D1A RID: 3354
		public const int DS_DlkHash = 12;

		// Token: 0x04000D1B RID: 3355
		public const int DS_MdCache = 13;

		// Token: 0x04000D1C RID: 3356
		public const int DS_Tokenm = 14;

		// Token: 0x04000D1D RID: 3357
		public const int DS_IevntLog = 15;

		// Token: 0x04000D1E RID: 3358
		public const int DS_EvntWrap = 16;

		// Token: 0x04000D1F RID: 3359
		public const int DS_ADTopoSvc = 17;

		// Token: 0x04000D20 RID: 3360
		public const int DS_CacheAttrs = 21;

		// Token: 0x04000D21 RID: 3361
		public const int DS_Callback = 22;

		// Token: 0x04000D22 RID: 3362
		public const int DS_Conn = 23;

		// Token: 0x04000D23 RID: 3363
		public const int DS_ContextCache = 24;

		// Token: 0x04000D24 RID: 3364
		public const int DS_DscEvent = 25;

		// Token: 0x04000D25 RID: 3365
		public const int DS_Group = 26;

		// Token: 0x04000D26 RID: 3366
		public const int DS_Incar = 27;

		// Token: 0x04000D27 RID: 3367
		public const int DS_IncarCache = 28;

		// Token: 0x04000D28 RID: 3368
		public const int DS_LdapApi = 29;

		// Token: 0x04000D29 RID: 3369
		public const int DS_MprwLock = 30;

		// Token: 0x04000D2A RID: 3370
		public const int DS_MsgCache = 31;

		// Token: 0x04000D2B RID: 3371
		public const int DS_Other = 32;

		// Token: 0x04000D2C RID: 3372
		public const int DS_Perf = 33;

		// Token: 0x04000D2D RID: 3373
		public const int DS_Reachability = 34;

		// Token: 0x04000D2E RID: 3374
		public const int DS_ReqCache = 35;

		// Token: 0x04000D2F RID: 3375
		public const int DS_ShmToPvFailure = 36;

		// Token: 0x04000D30 RID: 3376
		public const int DS_ShmToPv_NULL_handle = 37;

		// Token: 0x04000D31 RID: 3377
		public const int DS_Topo = 38;

		// Token: 0x04000D32 RID: 3378
		public const int DS_TopoDNS = 39;

		// Token: 0x04000D33 RID: 3379
		public const int DS_TopoServerLists = 40;

		// Token: 0x04000D34 RID: 3380
		public const int DS_TopoTiming = 41;

		// Token: 0x04000D35 RID: 3381
		public const int common_epoxy = 50;

		// Token: 0x04000D36 RID: 3382
		public const int common_smtpaddr = 51;

		// Token: 0x04000D37 RID: 3383
		public const int common_parse821 = 52;

		// Token: 0x04000D38 RID: 3384
		public const int common_excache2 = 53;

		// Token: 0x04000D39 RID: 3385
		public const int common_requery = 54;

		// Token: 0x04000D3A RID: 3386
		public const int common_exrw = 55;

		// Token: 0x04000D3B RID: 3387
		public const int admin_admin = 60;

		// Token: 0x04000D3C RID: 3388
		public const int admin_ds2mb = 61;

		// Token: 0x04000D3D RID: 3389
		public const int cluster_getcomp = 63;

		// Token: 0x04000D3E RID: 3390
		public const int deployment_main = 64;

		// Token: 0x04000D3F RID: 3391
		public const int dsa_oabgen = 70;

		// Token: 0x04000D40 RID: 3392
		public const int cal_Debug = 100;

		// Token: 0x04000D41 RID: 3393
		public const int cal_XProcCache = 101;

		// Token: 0x04000D42 RID: 3394
		public const int cal_DsaMgr = 102;

		// Token: 0x04000D43 RID: 3395
		public const int cal_Url = 103;

		// Token: 0x04000D44 RID: 3396
		public const int cal_Xml = 104;

		// Token: 0x04000D45 RID: 3397
		public const int cal_Rfp = 105;

		// Token: 0x04000D46 RID: 3398
		public const int cal_exfcache = 106;

		// Token: 0x04000D47 RID: 3399
		public const int cal_ExOleDb = 110;

		// Token: 0x04000D48 RID: 3400
		public const int cal_ExOleDb_Errors = 111;

		// Token: 0x04000D49 RID: 3401
		public const int cal_ExOleDb_Events = 112;

		// Token: 0x04000D4A RID: 3402
		public const int cal_ExOleDb_ThreadPool = 113;

		// Token: 0x04000D4B RID: 3403
		public const int cal_ExOleDb_Transactions = 114;

		// Token: 0x04000D4C RID: 3404
		public const int cal_ExOleDb_SystemEvents = 115;

		// Token: 0x04000D4D RID: 3405
		public const int cal_ExOleDb_ClientControl = 116;

		// Token: 0x04000D4E RID: 3406
		public const int cal_ExOleDb_EntryExit = 117;

		// Token: 0x04000D4F RID: 3407
		public const int cal_ExOleDb_Impersonation = 118;

		// Token: 0x04000D50 RID: 3408
		public const int cal_ExOleDb_Hsots = 119;

		// Token: 0x04000D51 RID: 3409
		public const int cal_Prx = 120;

		// Token: 0x04000D52 RID: 3410
		public const int cal_PrxConn = 121;

		// Token: 0x04000D53 RID: 3411
		public const int cal_PrxParser = 122;

		// Token: 0x04000D54 RID: 3412
		public const int cal_PrxReplMgr = 123;

		// Token: 0x04000D55 RID: 3413
		public const int cal_PrxRequest = 124;

		// Token: 0x04000D56 RID: 3414
		public const int cal_PrxSrv = 125;

		// Token: 0x04000D57 RID: 3415
		public const int cal_PrxSec = 126;

		// Token: 0x04000D58 RID: 3416
		public const int cal_IdleThrd = 130;

		// Token: 0x04000D59 RID: 3417
		public const int cal_LinkFix = 131;

		// Token: 0x04000D5A RID: 3418
		public const int cal_Nmspc = 132;

		// Token: 0x04000D5B RID: 3419
		public const int cal_StringBlock = 133;

		// Token: 0x04000D5C RID: 3420
		public const int cal_Schema = 134;

		// Token: 0x04000D5D RID: 3421
		public const int cal_SchemaPop = 135;

		// Token: 0x04000D5E RID: 3422
		public const int cal_DbCommandTree = 136;

		// Token: 0x04000D5F RID: 3423
		public const int cal_Sql = 137;

		// Token: 0x04000D60 RID: 3424
		public const int cal_Exoledbesh_Errors = 138;

		// Token: 0x04000D61 RID: 3425
		public const int cal_CalcProps = 140;

		// Token: 0x04000D62 RID: 3426
		public const int cal_MdbInst = 141;

		// Token: 0x04000D63 RID: 3427
		public const int cal_LogCallback = 142;

		// Token: 0x04000D64 RID: 3428
		public const int cal_AdminLogon = 143;

		// Token: 0x04000D65 RID: 3429
		public const int cal_StorextErr = 144;

		// Token: 0x04000D66 RID: 3430
		public const int cal_Davex = 150;

		// Token: 0x04000D67 RID: 3431
		public const int cal_DavexDbgHeaders = 151;

		// Token: 0x04000D68 RID: 3432
		public const int cal_Epoxy = 152;

		// Token: 0x04000D69 RID: 3433
		public const int cal_Repl = 153;

		// Token: 0x04000D6A RID: 3434
		public const int cal_Ifs = 154;

		// Token: 0x04000D6B RID: 3435
		public const int cal_IfsCache = 155;

		// Token: 0x04000D6C RID: 3436
		public const int cal_WebClient = 156;

		// Token: 0x04000D6D RID: 3437
		public const int cal_FileStream = 157;

		// Token: 0x04000D6E RID: 3438
		public const int cal_PackedResponse = 158;

		// Token: 0x04000D6F RID: 3439
		public const int cal_Exdav = 160;

		// Token: 0x04000D70 RID: 3440
		public const int cal_Notif = 161;

		// Token: 0x04000D71 RID: 3441
		public const int cal_Props = 162;

		// Token: 0x04000D72 RID: 3442
		public const int cal_Search = 163;

		// Token: 0x04000D73 RID: 3443
		public const int cal_SessMgr = 164;

		// Token: 0x04000D74 RID: 3444
		public const int cal_Locks = 165;

		// Token: 0x04000D75 RID: 3445
		public const int cal_EnumAtts = 166;

		// Token: 0x04000D76 RID: 3446
		public const int cal_PropFind = 167;

		// Token: 0x04000D77 RID: 3447
		public const int cal_OWASMimeAlgorithm = 170;

		// Token: 0x04000D78 RID: 3448
		public const int cal_OWASMimeData = 171;

		// Token: 0x04000D79 RID: 3449
		public const int cal_OWASMimeCall = 172;

		// Token: 0x04000D7A RID: 3450
		public const int cal_OWAFilteringAlgorithm = 173;

		// Token: 0x04000D7B RID: 3451
		public const int cal_OWAFilteringCall = 174;

		// Token: 0x04000D7C RID: 3452
		public const int cal_OWAFilteringAction = 175;

		// Token: 0x04000D7D RID: 3453
		public const int cal_OWAJunkEmail = 176;

		// Token: 0x04000D7E RID: 3454
		public const int cal_Actv = 180;

		// Token: 0x04000D7F RID: 3455
		public const int cal_BodyStream = 181;

		// Token: 0x04000D80 RID: 3456
		public const int cal_Content = 182;

		// Token: 0x04000D81 RID: 3457
		public const int cal_Ecb = 183;

		// Token: 0x04000D82 RID: 3458
		public const int cal_EcbLogging = 184;

		// Token: 0x04000D83 RID: 3459
		public const int cal_EcbStream = 185;

		// Token: 0x04000D84 RID: 3460
		public const int cal_Event = 186;

		// Token: 0x04000D85 RID: 3461
		public const int cal_Lock = 187;

		// Token: 0x04000D86 RID: 3462
		public const int cal_Method = 188;

		// Token: 0x04000D87 RID: 3463
		public const int cal_Persist = 189;

		// Token: 0x04000D88 RID: 3464
		public const int cal_Request = 190;

		// Token: 0x04000D89 RID: 3465
		public const int cal_Response = 191;

		// Token: 0x04000D8A RID: 3466
		public const int cal_ScriptMap = 192;

		// Token: 0x04000D8B RID: 3467
		public const int cal_Transmit = 193;

		// Token: 0x04000D8C RID: 3468
		public const int cal_DavprsDbgHeaders = 194;

		// Token: 0x04000D8D RID: 3469
		public const int cal_Metabase = 195;

		// Token: 0x04000D8E RID: 3470
		public const int cal_Unpack = 199;

		// Token: 0x04000D8F RID: 3471
		public const int OWAAuth_General = 200;

		// Token: 0x04000D90 RID: 3472
		public const int OWAAuth_Algorithm = 201;

		// Token: 0x04000D91 RID: 3473
		public const int OWAAuth_ExtensionError = 202;

		// Token: 0x04000D92 RID: 3474
		public const int OWAAuth_FilterError = 203;

		// Token: 0x04000D93 RID: 3475
		public const int OWAAuth_CryptoError = 204;

		// Token: 0x04000D94 RID: 3476
		public const int OWAAuth_MetabaseInfo = 205;

		// Token: 0x04000D95 RID: 3477
		public const int OWAAuth_MetabaseError = 206;

		// Token: 0x04000D96 RID: 3478
		public const int OWAAuth_Debug = 209;

		// Token: 0x04000D97 RID: 3479
		public const int OWAAuth_RPCInfo = 210;

		// Token: 0x04000D98 RID: 3480
		public const int OWAAuth_RPCError = 211;

		// Token: 0x04000D99 RID: 3481
		public const int EASFilter_Requests = 250;

		// Token: 0x04000D9A RID: 3482
		public const int ExFba_General = 270;

		// Token: 0x04000D9B RID: 3483
		public const int ExFba_ServiceInfo = 271;

		// Token: 0x04000D9C RID: 3484
		public const int ExFba_ServiceError = 272;

		// Token: 0x04000D9D RID: 3485
		public const int ExFba_CryptoError = 273;

		// Token: 0x04000D9E RID: 3486
		public const int ExFba_TombstoneInfo = 274;

		// Token: 0x04000D9F RID: 3487
		public const int ExFba_TombstoneError = 275;

		// Token: 0x04000DA0 RID: 3488
		public const int ExFba_Algorithm = 276;

		// Token: 0x04000DA1 RID: 3489
		public const int ExFba_RPCInfo = 277;

		// Token: 0x04000DA2 RID: 3490
		public const int ExFba_RPCError = 278;

		// Token: 0x04000DA3 RID: 3491
		public const int ExFba_Debug = 279;

		// Token: 0x04000DA4 RID: 3492
		public const int ExFba_CASInfo = 281;

		// Token: 0x04000DA5 RID: 3493
		public const int ExFba_CASError = 282;

		// Token: 0x04000DA6 RID: 3494
		public const int AuthModuleFilter_Requests = 280;

		// Token: 0x04000DA7 RID: 3495
		public const int codeinject_main = 600;

		// Token: 0x04000DA8 RID: 3496
		public const int dsaccess_test = 601;

		// Token: 0x04000DA9 RID: 3497
		public static Guid guid = new Guid("1b88b5f7-be69-4d19-b065-9c30b6df8185");
	}
}
