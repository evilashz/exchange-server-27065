using System;

namespace Microsoft.Exchange.EdgeSync.Ehf
{
	// Token: 0x0200002D RID: 45
	internal class EhfSyncAppConfig
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000E249 File Offset: 0x0000C449
		public virtual int BatchSize
		{
			get
			{
				return EhfSyncAppConfig.AppConfigBatchSize;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000E250 File Offset: 0x0000C450
		public virtual int MaxMessageSize
		{
			get
			{
				return EhfSyncAppConfig.AppConfigMaxMessageSize;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000E257 File Offset: 0x0000C457
		public virtual int TransientExceptionRetryCount
		{
			get
			{
				return EhfSyncAppConfig.AppConfigTransientExceptionRetryCount;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000E25E File Offset: 0x0000C45E
		public virtual int EhfAdminSyncMaxAccumulatedChangeSize
		{
			get
			{
				return EhfSyncAppConfig.AppConfigEhfAdminSyncMaxAccumulatedChangeSize;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000E265 File Offset: 0x0000C465
		public virtual int EhfAdminSyncMaxAccumulatedDeleteChangeSize
		{
			get
			{
				return EhfSyncAppConfig.AppConfigEhfAdminSyncMaxAccumulatedDeleteChangeSize;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000E26C File Offset: 0x0000C46C
		public virtual int EhfAdminSyncMaxFailureCount
		{
			get
			{
				return EhfSyncAppConfig.AppConfigEhfAdminSyncMaxFailureCount;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000E273 File Offset: 0x0000C473
		public virtual int EhfAdminSyncMaxTargetAdminStateSize
		{
			get
			{
				return EhfSyncAppConfig.AppConfigEhfAdminSyncMaxTargetAdminStateSize;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000E27A File Offset: 0x0000C47A
		public virtual int EhfAdminSyncTransientFailureRetryThreshold
		{
			get
			{
				return EhfSyncAppConfig.AppConfigEhfAdminSyncTransientFailureRetryThreshold;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000E281 File Offset: 0x0000C481
		public virtual TimeSpan EhfAdminSyncInterval
		{
			get
			{
				return EhfSyncAppConfig.AppConfigEhfAdminSyncInterval;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000E288 File Offset: 0x0000C488
		public virtual TimeSpan RequestTimeout
		{
			get
			{
				return EhfSyncAppConfig.AppConfigRequestTimeout;
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000E28F File Offset: 0x0000C48F
		private static int GetMaxMessageSizeFromAppConfig()
		{
			return EhfSyncAppConfig.GetConfigInt("EhfMaxMessageSize", 10240, 256000, 102400);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000E2AA File Offset: 0x0000C4AA
		private static int GetBatchSizeFromAppConfig()
		{
			return EhfSyncAppConfig.GetConfigInt("EhfBatchSize", 1, 100, 20);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000E2BC File Offset: 0x0000C4BC
		private static TimeSpan GetRequestTimeoutFromAppConfig()
		{
			TimeSpan lowerBound = TimeSpan.FromSeconds(1.0);
			TimeSpan upperBound = TimeSpan.FromMinutes(60.0);
			return EhfSyncAppConfig.GetConfigTimeSpan("EhfRequestTimeout", lowerBound, upperBound, EhfSyncAppConfig.DefaultRequestTimeout);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000E2F8 File Offset: 0x0000C4F8
		private static int GetTransientExceptionRetryCountFromAppConfig()
		{
			return EhfSyncAppConfig.GetConfigInt("TransientExceptionRetryCount", 0, 100, 3);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000E308 File Offset: 0x0000C508
		private static int GetAdminSyncTransientFailureRetryThresholdFromAppConfig()
		{
			return EhfSyncAppConfig.GetConfigInt("EhfAdminSyncTransientFailureRetryThreshold", 0, int.MaxValue, 10);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000E31C File Offset: 0x0000C51C
		private static int GetAdminSyncMaxFailureCountFromAppConfig()
		{
			return EhfSyncAppConfig.GetConfigInt("EhfAdminSyncMaxFailureCount", 0, int.MaxValue, 10);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000E330 File Offset: 0x0000C530
		private static int GetAdminSyncMaxTargetAdminStateSizeFromAppConfig()
		{
			return EhfSyncAppConfig.GetConfigInt("EhfAdminSyncMaxTargetAdminStateSize", 0, 400, 50);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000E344 File Offset: 0x0000C544
		private static int GetEhfAdminSyncMaxAccumulatedChangeSizeFromAppConfig()
		{
			return EhfSyncAppConfig.GetConfigInt("EhfAdminSyncMaxAccumulatedChangeSize", 0, int.MaxValue, 100);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000E358 File Offset: 0x0000C558
		private static int GetEhfAdminSyncMaxAccumulatedDeleteChangeSizeFromAppConfig()
		{
			return EhfSyncAppConfig.GetConfigInt("EhfAdminSyncMaxAccumulatedDeleteChangeSize", 0, int.MaxValue, 1000);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000E370 File Offset: 0x0000C570
		private static TimeSpan GetEhfAdminSyncIntervalFromAppConfig()
		{
			TimeSpan lowerBound = TimeSpan.FromSeconds(1.0);
			TimeSpan maxValue = TimeSpan.MaxValue;
			return EhfSyncAppConfig.GetConfigTimeSpan("EhfAdminSyncInterval", lowerBound, maxValue, EhfSyncAppConfig.DefaultEhfAdminSyncInterval);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000E3A3 File Offset: 0x0000C5A3
		private static int GetConfigInt(string label, int lowerBound, int upperBound, int defaultValue)
		{
			if (EdgeSyncSvc.EdgeSync == null || EdgeSyncSvc.EdgeSync.AppConfig == null)
			{
				return defaultValue;
			}
			return EdgeSyncSvc.EdgeSync.AppConfig.GetConfigInt(label, lowerBound, upperBound, defaultValue);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000E3CD File Offset: 0x0000C5CD
		private static TimeSpan GetConfigTimeSpan(string label, TimeSpan lowerBound, TimeSpan upperBound, TimeSpan defaultValue)
		{
			if (EdgeSyncSvc.EdgeSync == null || EdgeSyncSvc.EdgeSync.AppConfig == null)
			{
				return defaultValue;
			}
			return EdgeSyncSvc.EdgeSync.AppConfig.GetConfigTimeSpan(label, lowerBound, upperBound, defaultValue);
		}

		// Token: 0x040000B3 RID: 179
		private const int DefaultMaxMessageSize = 102400;

		// Token: 0x040000B4 RID: 180
		private const int DefaultBatchSize = 20;

		// Token: 0x040000B5 RID: 181
		private const int DefaultEhfAdminSyncTransientFailureRetryThreshold = 10;

		// Token: 0x040000B6 RID: 182
		private const int DefaultEhfAdminSyncMaxFailureCount = 10;

		// Token: 0x040000B7 RID: 183
		private const int DefaultEhfAdminSyncMaxTargetAdminStateSize = 50;

		// Token: 0x040000B8 RID: 184
		private const int DefaultEhfAdminSyncMaxAccumulatedChangeSize = 100;

		// Token: 0x040000B9 RID: 185
		private const int DefaultEhfAdminSyncMaxAccumulatedDeleteChangeSize = 1000;

		// Token: 0x040000BA RID: 186
		public static readonly TimeSpan AppConfigEhfAdminSyncInterval = EhfSyncAppConfig.GetEhfAdminSyncIntervalFromAppConfig();

		// Token: 0x040000BB RID: 187
		public static readonly int AppConfigEhfAdminSyncMaxFailureCount = EhfSyncAppConfig.GetAdminSyncMaxFailureCountFromAppConfig();

		// Token: 0x040000BC RID: 188
		public static readonly int AppConfigEhfAdminSyncMaxTargetAdminStateSize = EhfSyncAppConfig.GetAdminSyncMaxTargetAdminStateSizeFromAppConfig();

		// Token: 0x040000BD RID: 189
		public static readonly int AppConfigEhfAdminSyncTransientFailureRetryThreshold = EhfSyncAppConfig.GetAdminSyncTransientFailureRetryThresholdFromAppConfig();

		// Token: 0x040000BE RID: 190
		public static readonly int AppConfigEhfAdminSyncMaxAccumulatedChangeSize = EhfSyncAppConfig.GetEhfAdminSyncMaxAccumulatedChangeSizeFromAppConfig();

		// Token: 0x040000BF RID: 191
		public static readonly int AppConfigEhfAdminSyncMaxAccumulatedDeleteChangeSize = EhfSyncAppConfig.GetEhfAdminSyncMaxAccumulatedDeleteChangeSizeFromAppConfig();

		// Token: 0x040000C0 RID: 192
		private static readonly TimeSpan DefaultRequestTimeout = TimeSpan.FromMinutes(2.0);

		// Token: 0x040000C1 RID: 193
		private static readonly TimeSpan DefaultEhfAdminSyncInterval = TimeSpan.FromMinutes(3.0);

		// Token: 0x040000C2 RID: 194
		private static readonly int AppConfigMaxMessageSize = EhfSyncAppConfig.GetMaxMessageSizeFromAppConfig();

		// Token: 0x040000C3 RID: 195
		private static readonly int AppConfigBatchSize = EhfSyncAppConfig.GetBatchSizeFromAppConfig();

		// Token: 0x040000C4 RID: 196
		private static readonly TimeSpan AppConfigRequestTimeout = EhfSyncAppConfig.GetRequestTimeoutFromAppConfig();

		// Token: 0x040000C5 RID: 197
		private static readonly int AppConfigTransientExceptionRetryCount = EhfSyncAppConfig.GetTransientExceptionRetryCountFromAppConfig();
	}
}
