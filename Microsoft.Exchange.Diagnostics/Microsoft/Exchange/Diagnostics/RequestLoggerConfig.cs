using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000151 RID: 337
	internal class RequestLoggerConfig
	{
		// Token: 0x0600099B RID: 2459 RVA: 0x00023FF4 File Offset: 0x000221F4
		public RequestLoggerConfig(string logType, string logFilePrefix, string logComponent, string folderPathAppSettingsKey, string fallbackLogFolderPath, Enum genericInfoColumn, List<KeyValuePair<string, Enum>> columns, int defaultLatencyDictionarySize) : this(logType, logFilePrefix, logComponent, folderPathAppSettingsKey, fallbackLogFolderPath, TimeSpan.FromDays(30.0), 5368709120L, 10485760L, genericInfoColumn, columns, defaultLatencyDictionarySize)
		{
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00024034 File Offset: 0x00022234
		public RequestLoggerConfig(string logType, string logFilePrefix, string logComponent, string folderPathAppSettingsKey, string fallbackLogFolderPath, TimeSpan maxAge, long maxDirectorySize, long maxLogFileSize, Enum genericInfoColumn, List<KeyValuePair<string, Enum>> columns, int defaultLatencyDictionarySize)
		{
			this.LogType = logType;
			this.LogFilePrefix = logFilePrefix;
			this.LogComponent = logComponent;
			this.FolderPathAppSettingsKey = folderPathAppSettingsKey;
			this.FallbackLogFolderPath = fallbackLogFolderPath;
			this.MaxAge = maxAge;
			this.MaxDirectorySize = maxDirectorySize;
			this.MaxLogFileSize = maxLogFileSize;
			this.GenericInfoColumn = genericInfoColumn;
			this.Columns = new ReadOnlyCollection<KeyValuePair<string, Enum>>(new List<KeyValuePair<string, Enum>>(columns));
			this.DefaultLatencyDictionarySize = defaultLatencyDictionarySize;
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x000240A6 File Offset: 0x000222A6
		// (set) Token: 0x0600099E RID: 2462 RVA: 0x000240AE File Offset: 0x000222AE
		public string LogType { get; private set; }

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x000240B7 File Offset: 0x000222B7
		// (set) Token: 0x060009A0 RID: 2464 RVA: 0x000240BF File Offset: 0x000222BF
		public string LogFilePrefix { get; private set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x000240C8 File Offset: 0x000222C8
		// (set) Token: 0x060009A2 RID: 2466 RVA: 0x000240D0 File Offset: 0x000222D0
		public string LogComponent { get; private set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x000240D9 File Offset: 0x000222D9
		// (set) Token: 0x060009A4 RID: 2468 RVA: 0x000240E1 File Offset: 0x000222E1
		public string FolderPathAppSettingsKey { get; private set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x000240EA File Offset: 0x000222EA
		// (set) Token: 0x060009A6 RID: 2470 RVA: 0x000240F2 File Offset: 0x000222F2
		public string FallbackLogFolderPath { get; private set; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x000240FB File Offset: 0x000222FB
		// (set) Token: 0x060009A8 RID: 2472 RVA: 0x00024103 File Offset: 0x00022303
		public TimeSpan MaxAge { get; private set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x0002410C File Offset: 0x0002230C
		// (set) Token: 0x060009AA RID: 2474 RVA: 0x00024114 File Offset: 0x00022314
		public long MaxDirectorySize { get; private set; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x0002411D File Offset: 0x0002231D
		// (set) Token: 0x060009AC RID: 2476 RVA: 0x00024125 File Offset: 0x00022325
		public long MaxLogFileSize { get; private set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x0002412E File Offset: 0x0002232E
		// (set) Token: 0x060009AE RID: 2478 RVA: 0x00024136 File Offset: 0x00022336
		public Enum GenericInfoColumn { get; private set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x0002413F File Offset: 0x0002233F
		// (set) Token: 0x060009B0 RID: 2480 RVA: 0x00024147 File Offset: 0x00022347
		public ReadOnlyCollection<KeyValuePair<string, Enum>> Columns { get; private set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x00024150 File Offset: 0x00022350
		// (set) Token: 0x060009B2 RID: 2482 RVA: 0x00024158 File Offset: 0x00022358
		public int DefaultLatencyDictionarySize { get; private set; }
	}
}
