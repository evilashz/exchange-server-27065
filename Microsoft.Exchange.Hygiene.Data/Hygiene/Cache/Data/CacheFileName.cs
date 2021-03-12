using System;

namespace Microsoft.Exchange.Hygiene.Cache.Data
{
	// Token: 0x02000053 RID: 83
	internal class CacheFileName
	{
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00009EF3 File Offset: 0x000080F3
		internal static string CookieFileNameFormat
		{
			get
			{
				return "Cookie_{0}_{1}.bin";
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00009EFA File Offset: 0x000080FA
		internal static string ContentFileNameFormat
		{
			get
			{
				return "Content_{0}_{1}.bin";
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00009F01 File Offset: 0x00008101
		internal static string FileNameFormatWithTSVersion
		{
			get
			{
				return "{0}_{1}_{2}_{3}.bin";
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00009F08 File Offset: 0x00008108
		internal static string CacheFileFolderNameFormat
		{
			get
			{
				return "{0}_{1}";
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000348 RID: 840 RVA: 0x00009F0F File Offset: 0x0000810F
		internal static string CacheZipFileNameFormat
		{
			get
			{
				return "{0}_{1}_{2}_{3}_{4}.zip";
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00009F16 File Offset: 0x00008116
		internal static string FileNameDateTimeFormat
		{
			get
			{
				return "yyyy-MM-dd-HH-mm-ss";
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600034A RID: 842 RVA: 0x00009F1D File Offset: 0x0000811D
		internal static string CookieFileNameSearchFormat
		{
			get
			{
				return "Cookie_{0}_{1}*.bin";
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00009F24 File Offset: 0x00008124
		internal static string ContentFileNameSearchFormat
		{
			get
			{
				return "Content_{0}_{1}*.bin";
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600034C RID: 844 RVA: 0x00009F2B File Offset: 0x0000812B
		internal static string CacheZipFileNameSearchFormat
		{
			get
			{
				return "{0}_{1}*.zip";
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600034D RID: 845 RVA: 0x00009F32 File Offset: 0x00008132
		internal static string CookieFileNamePrefix
		{
			get
			{
				return "Cookie_";
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00009F39 File Offset: 0x00008139
		internal static string ContentFileNamePrefix
		{
			get
			{
				return "Content_";
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00009F40 File Offset: 0x00008140
		internal static string BaselineBloomFileFormat
		{
			get
			{
				return "{0}.bff";
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000350 RID: 848 RVA: 0x00009F47 File Offset: 0x00008147
		internal static string BaselineBloomFileTempZipFormat
		{
			get
			{
				return "{0}-{1}.zip.tmp";
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00009F4E File Offset: 0x0000814E
		internal static string BloomFilterZipFileNameSearchFormat
		{
			get
			{
				return "{0}*.zip";
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000352 RID: 850 RVA: 0x00009F55 File Offset: 0x00008155
		internal static string ZipFileFolder
		{
			get
			{
				return "ZipFile";
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000353 RID: 851 RVA: 0x00009F5C File Offset: 0x0000815C
		internal static string CurrentFileFolder1
		{
			get
			{
				return "Current1";
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000354 RID: 852 RVA: 0x00009F63 File Offset: 0x00008163
		internal static string CurrentFileFolder2
		{
			get
			{
				return "Current2";
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00009F6A File Offset: 0x0000816A
		internal static string WorkInProgress
		{
			get
			{
				return "WorkInProgress";
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000356 RID: 854 RVA: 0x00009F71 File Offset: 0x00008171
		internal static string WatermarkFileName
		{
			get
			{
				return "Watermark.txt";
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00009F78 File Offset: 0x00008178
		internal static string GetBaselineBloomFilterFileName(string entityName)
		{
			return string.Format(CacheFileName.BaselineBloomFileFormat, entityName);
		}
	}
}
