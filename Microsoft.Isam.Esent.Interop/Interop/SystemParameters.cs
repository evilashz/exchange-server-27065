using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000090 RID: 144
	public static class SystemParameters
	{
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x0000D2CB File Offset: 0x0000B4CB
		// (set) Token: 0x060005A8 RID: 1448 RVA: 0x0000D2D4 File Offset: 0x0000B4D4
		public static int CacheSizeMax
		{
			get
			{
				return SystemParameters.GetIntegerParameter(JET_param.CacheSizeMax);
			}
			set
			{
				SystemParameters.SetIntegerParameter(JET_param.CacheSizeMax, value);
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x0000D2DE File Offset: 0x0000B4DE
		// (set) Token: 0x060005AA RID: 1450 RVA: 0x0000D2E7 File Offset: 0x0000B4E7
		public static int CacheSize
		{
			get
			{
				return SystemParameters.GetIntegerParameter(JET_param.CacheSize);
			}
			set
			{
				SystemParameters.SetIntegerParameter(JET_param.CacheSize, value);
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x0000D2F1 File Offset: 0x0000B4F1
		// (set) Token: 0x060005AC RID: 1452 RVA: 0x0000D2FA File Offset: 0x0000B4FA
		public static int DatabasePageSize
		{
			get
			{
				return SystemParameters.GetIntegerParameter(JET_param.DatabasePageSize);
			}
			set
			{
				SystemParameters.SetIntegerParameter(JET_param.DatabasePageSize, value);
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x0000D304 File Offset: 0x0000B504
		// (set) Token: 0x060005AE RID: 1454 RVA: 0x0000D30D File Offset: 0x0000B50D
		public static int CacheSizeMin
		{
			get
			{
				return SystemParameters.GetIntegerParameter(JET_param.CacheSizeMin);
			}
			set
			{
				SystemParameters.SetIntegerParameter(JET_param.CacheSizeMin, value);
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x0000D317 File Offset: 0x0000B517
		// (set) Token: 0x060005B0 RID: 1456 RVA: 0x0000D320 File Offset: 0x0000B520
		public static int OutstandingIOMax
		{
			get
			{
				return SystemParameters.GetIntegerParameter(JET_param.OutstandingIOMax);
			}
			set
			{
				SystemParameters.SetIntegerParameter(JET_param.OutstandingIOMax, value);
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x0000D32A File Offset: 0x0000B52A
		// (set) Token: 0x060005B2 RID: 1458 RVA: 0x0000D333 File Offset: 0x0000B533
		public static int StartFlushThreshold
		{
			get
			{
				return SystemParameters.GetIntegerParameter(JET_param.StartFlushThreshold);
			}
			set
			{
				SystemParameters.SetIntegerParameter(JET_param.StartFlushThreshold, value);
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0000D33D File Offset: 0x0000B53D
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x0000D346 File Offset: 0x0000B546
		public static int StopFlushThreshold
		{
			get
			{
				return SystemParameters.GetIntegerParameter(JET_param.StopFlushThreshold);
			}
			set
			{
				SystemParameters.SetIntegerParameter(JET_param.StopFlushThreshold, value);
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0000D350 File Offset: 0x0000B550
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x0000D359 File Offset: 0x0000B559
		public static int MaxInstances
		{
			get
			{
				return SystemParameters.GetIntegerParameter(JET_param.MaxInstances);
			}
			set
			{
				SystemParameters.SetIntegerParameter(JET_param.MaxInstances, value);
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x0000D363 File Offset: 0x0000B563
		// (set) Token: 0x060005B8 RID: 1464 RVA: 0x0000D36C File Offset: 0x0000B56C
		public static int EventLoggingLevel
		{
			get
			{
				return SystemParameters.GetIntegerParameter(JET_param.EventLoggingLevel);
			}
			set
			{
				SystemParameters.SetIntegerParameter(JET_param.EventLoggingLevel, value);
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x0000D376 File Offset: 0x0000B576
		public static int KeyMost
		{
			get
			{
				if (EsentVersion.SupportsVistaFeatures)
				{
					return SystemParameters.GetIntegerParameter((JET_param)134);
				}
				return 255;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x0000D38F File Offset: 0x0000B58F
		public static int ColumnsKeyMost
		{
			get
			{
				return Api.Impl.Capabilities.ColumnsKeyMost;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x0000D3A0 File Offset: 0x0000B5A0
		public static int BookmarkMost
		{
			get
			{
				return SystemParameters.KeyMost + 1;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x0000D3A9 File Offset: 0x0000B5A9
		public static int LVChunkSizeMost
		{
			get
			{
				if (EsentVersion.SupportsWindows7Features)
				{
					return SystemParameters.GetIntegerParameter((JET_param)163);
				}
				return SystemParameters.GetIntegerParameter(JET_param.DatabasePageSize) - 82;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x0000D3C7 File Offset: 0x0000B5C7
		// (set) Token: 0x060005BE RID: 1470 RVA: 0x0000D3DC File Offset: 0x0000B5DC
		public static int Configuration
		{
			get
			{
				if (EsentVersion.SupportsVistaFeatures)
				{
					return SystemParameters.GetIntegerParameter((JET_param)129);
				}
				return 1;
			}
			set
			{
				if (EsentVersion.SupportsVistaFeatures)
				{
					SystemParameters.SetIntegerParameter((JET_param)129, value);
				}
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x0000D3F0 File Offset: 0x0000B5F0
		// (set) Token: 0x060005C0 RID: 1472 RVA: 0x0000D405 File Offset: 0x0000B605
		public static bool EnableAdvanced
		{
			get
			{
				return !EsentVersion.SupportsVistaFeatures || SystemParameters.GetBoolParameter((JET_param)130);
			}
			set
			{
				if (EsentVersion.SupportsVistaFeatures)
				{
					SystemParameters.SetBoolParameter((JET_param)130, value);
				}
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x0000D419 File Offset: 0x0000B619
		// (set) Token: 0x060005C2 RID: 1474 RVA: 0x0000D42E File Offset: 0x0000B62E
		public static int LegacyFileNames
		{
			get
			{
				if (EsentVersion.SupportsVistaFeatures)
				{
					return SystemParameters.GetIntegerParameter((JET_param)136);
				}
				return 1;
			}
			set
			{
				if (EsentVersion.SupportsVistaFeatures)
				{
					SystemParameters.SetIntegerParameter((JET_param)136, value);
				}
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x0000D442 File Offset: 0x0000B642
		// (set) Token: 0x060005C4 RID: 1476 RVA: 0x0000D44B File Offset: 0x0000B64B
		public static JET_ExceptionAction ExceptionAction
		{
			get
			{
				return (JET_ExceptionAction)SystemParameters.GetIntegerParameter(JET_param.ExceptionAction);
			}
			set
			{
				SystemParameters.SetIntegerParameter(JET_param.ExceptionAction, (int)value);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x0000D455 File Offset: 0x0000B655
		// (set) Token: 0x060005C6 RID: 1478 RVA: 0x0000D467 File Offset: 0x0000B667
		public static bool EnableFileCache
		{
			get
			{
				return EsentVersion.SupportsVistaFeatures && SystemParameters.GetBoolParameter((JET_param)126);
			}
			set
			{
				if (EsentVersion.SupportsVistaFeatures)
				{
					SystemParameters.SetBoolParameter((JET_param)126, value);
				}
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x0000D478 File Offset: 0x0000B678
		// (set) Token: 0x060005C8 RID: 1480 RVA: 0x0000D48A File Offset: 0x0000B68A
		public static bool EnableViewCache
		{
			get
			{
				return EsentVersion.SupportsVistaFeatures && SystemParameters.GetBoolParameter((JET_param)127);
			}
			set
			{
				if (EsentVersion.SupportsVistaFeatures)
				{
					SystemParameters.SetBoolParameter((JET_param)127, value);
				}
			}
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0000D49B File Offset: 0x0000B69B
		private static void SetStringParameter(JET_param param, string value)
		{
			Api.JetSetSystemParameter(JET_INSTANCE.Nil, JET_SESID.Nil, param, 0, value);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0000D4B0 File Offset: 0x0000B6B0
		private static string GetStringParameter(JET_param param)
		{
			int num = 0;
			string result;
			Api.JetGetSystemParameter(JET_INSTANCE.Nil, JET_SESID.Nil, param, ref num, out result, 1024);
			return result;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0000D4DA File Offset: 0x0000B6DA
		private static void SetIntegerParameter(JET_param param, int value)
		{
			Api.JetSetSystemParameter(JET_INSTANCE.Nil, JET_SESID.Nil, param, value, null);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0000D4F0 File Offset: 0x0000B6F0
		private static int GetIntegerParameter(JET_param param)
		{
			int result = 0;
			string text;
			Api.JetGetSystemParameter(JET_INSTANCE.Nil, JET_SESID.Nil, param, ref result, out text, 0);
			return result;
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0000D518 File Offset: 0x0000B718
		private static void SetBoolParameter(JET_param param, bool value)
		{
			int paramValue = value ? 1 : 0;
			Api.JetSetSystemParameter(JET_INSTANCE.Nil, JET_SESID.Nil, param, paramValue, null);
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0000D540 File Offset: 0x0000B740
		private static bool GetBoolParameter(JET_param param)
		{
			int num = 0;
			string text;
			Api.JetGetSystemParameter(JET_INSTANCE.Nil, JET_SESID.Nil, param, ref num, out text, 0);
			return num != 0;
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x0000D56C File Offset: 0x0000B76C
		// (set) Token: 0x060005D0 RID: 1488 RVA: 0x0000D578 File Offset: 0x0000B778
		public static int MinDataForXpress
		{
			get
			{
				return SystemParameters.GetIntegerParameter((JET_param)183);
			}
			set
			{
				SystemParameters.SetIntegerParameter((JET_param)183, value);
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x0000D585 File Offset: 0x0000B785
		// (set) Token: 0x060005D2 RID: 1490 RVA: 0x0000D591 File Offset: 0x0000B791
		public static int HungIOThreshold
		{
			get
			{
				return SystemParameters.GetIntegerParameter((JET_param)181);
			}
			set
			{
				SystemParameters.SetIntegerParameter((JET_param)181, value);
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x0000D59E File Offset: 0x0000B79E
		// (set) Token: 0x060005D4 RID: 1492 RVA: 0x0000D5AA File Offset: 0x0000B7AA
		public static int HungIOActions
		{
			get
			{
				return SystemParameters.GetIntegerParameter((JET_param)182);
			}
			set
			{
				SystemParameters.SetIntegerParameter((JET_param)182, value);
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x0000D5B7 File Offset: 0x0000B7B7
		// (set) Token: 0x060005D6 RID: 1494 RVA: 0x0000D5C3 File Offset: 0x0000B7C3
		public static string ProcessFriendlyName
		{
			get
			{
				return SystemParameters.GetStringParameter((JET_param)186);
			}
			set
			{
				SystemParameters.SetStringParameter((JET_param)186, value);
			}
		}

		// Token: 0x040002FA RID: 762
		public const int BaseNameLength = 3;

		// Token: 0x040002FB RID: 763
		public const int NameMost = 64;

		// Token: 0x040002FC RID: 764
		public const int ColumnMost = 255;

		// Token: 0x040002FD RID: 765
		public const int ColumnsMost = 65248;

		// Token: 0x040002FE RID: 766
		public const int ColumnsFixedMost = 127;

		// Token: 0x040002FF RID: 767
		public const int ColumnsVarMost = 128;

		// Token: 0x04000300 RID: 768
		public const int ColumnsTaggedMost = 64993;

		// Token: 0x04000301 RID: 769
		public const int PageTempDBSmallest = 14;

		// Token: 0x04000302 RID: 770
		public const int LocaleNameMaxLength = 85;
	}
}
