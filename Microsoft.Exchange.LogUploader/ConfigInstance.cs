using System;
using System.Configuration;
using System.Threading;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x0200002C RID: 44
	internal class ConfigInstance : ConfigurationElement
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000AF22 File Offset: 0x00009122
		[ConfigurationProperty("Name", IsRequired = true)]
		public string Name
		{
			get
			{
				return (string)base["Name"];
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000AF34 File Offset: 0x00009134
		// (set) Token: 0x0600021E RID: 542 RVA: 0x0000AF46 File Offset: 0x00009146
		[IntegerValidator(MinValue = 1)]
		[ConfigurationProperty("BatchSizeInBytes", IsRequired = false, DefaultValue = 4096)]
		public int BatchSizeInBytes
		{
			get
			{
				return (int)base["BatchSizeInBytes"];
			}
			internal set
			{
				base["BatchSizeInBytes"] = value;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000AF59 File Offset: 0x00009159
		// (set) Token: 0x06000220 RID: 544 RVA: 0x0000AF6B File Offset: 0x0000916B
		[ConfigurationProperty("MaxNumOfReaders", IsRequired = false, DefaultValue = 1)]
		[IntegerValidator(MinValue = 1, MaxValue = 10)]
		public int MaxNumOfReaders
		{
			get
			{
				return (int)base["MaxNumOfReaders"];
			}
			internal set
			{
				base["MaxNumOfReaders"] = value;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0000AF7E File Offset: 0x0000917E
		// (set) Token: 0x06000222 RID: 546 RVA: 0x0000AF90 File Offset: 0x00009190
		[IntegerValidator(MinValue = 1, MaxValue = 10)]
		[ConfigurationProperty("MaxNumOfWriters", IsRequired = false, DefaultValue = 1)]
		public int MaxNumOfWriters
		{
			get
			{
				return (int)base["MaxNumOfWriters"];
			}
			internal set
			{
				base["MaxNumOfWriters"] = value;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000AFA3 File Offset: 0x000091A3
		// (set) Token: 0x06000224 RID: 548 RVA: 0x0000AFB5 File Offset: 0x000091B5
		[ConfigurationProperty("ReaderSleepTime", IsRequired = false, DefaultValue = "00:00:04")]
		[TimeSpanValidator(MinValueString = "00:00:00.100")]
		public TimeSpan ReaderSleepTime
		{
			get
			{
				return (TimeSpan)base["ReaderSleepTime"];
			}
			internal set
			{
				base["ReaderSleepTime"] = value;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000AFC8 File Offset: 0x000091C8
		// (set) Token: 0x06000226 RID: 550 RVA: 0x0000AFDA File Offset: 0x000091DA
		[TimeSpanValidator(MinValueString = "00:00:00.100", MaxValueString = "00:15:00")]
		[ConfigurationProperty("ReaderSleepTimeRandomRange", IsRequired = false, DefaultValue = "00:00:01")]
		public TimeSpan ReaderSleepTimeRandomRange
		{
			get
			{
				return (TimeSpan)base["ReaderSleepTimeRandomRange"];
			}
			internal set
			{
				base["ReaderSleepTimeRandomRange"] = value;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000AFED File Offset: 0x000091ED
		// (set) Token: 0x06000228 RID: 552 RVA: 0x0000AFFF File Offset: 0x000091FF
		[ConfigurationProperty("SleepTimeForTransientDBError", IsRequired = false, DefaultValue = "00:00:05")]
		[TimeSpanValidator(MinValueString = "00:00:00.100")]
		public TimeSpan SleepTimeForTransientDBError
		{
			get
			{
				return (TimeSpan)base["SleepTimeForTransientDBError"];
			}
			internal set
			{
				base["SleepTimeForTransientDBError"] = value;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000B012 File Offset: 0x00009212
		// (set) Token: 0x0600022A RID: 554 RVA: 0x0000B024 File Offset: 0x00009224
		[TimeSpanValidator(MinValueString = "00:00:01")]
		[ConfigurationProperty("LogDirCheckInterval", IsRequired = false, DefaultValue = "00:00:15")]
		public TimeSpan LogDirCheckInterval
		{
			get
			{
				return (TimeSpan)base["LogDirCheckInterval"];
			}
			internal set
			{
				base["LogDirCheckInterval"] = value;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000B037 File Offset: 0x00009237
		// (set) Token: 0x0600022C RID: 556 RVA: 0x0000B049 File Offset: 0x00009249
		[ConfigurationProperty("QueueCapacity", IsRequired = false, DefaultValue = 100)]
		[IntegerValidator(MinValue = 1)]
		public int QueueCapacity
		{
			get
			{
				return (int)base["QueueCapacity"];
			}
			internal set
			{
				base["QueueCapacity"] = value;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000B05C File Offset: 0x0000925C
		// (set) Token: 0x0600022E RID: 558 RVA: 0x0000B06E File Offset: 0x0000926E
		[TimeSpanValidator(MinValueString = "00:00:01")]
		[ConfigurationProperty("ServiceStopWaitTime", IsRequired = false, DefaultValue = "00:02:00")]
		public TimeSpan ServiceStopWaitTime
		{
			get
			{
				return (TimeSpan)base["ServiceStopWaitTime"];
			}
			internal set
			{
				base["ServiceStopWaitTime"] = value;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000B081 File Offset: 0x00009281
		// (set) Token: 0x06000230 RID: 560 RVA: 0x0000B093 File Offset: 0x00009293
		[ConfigurationProperty("OpenFileRetryInterval", IsRequired = false, DefaultValue = "00:00:05")]
		[TimeSpanValidator(MinValueString = "00:00:01")]
		public TimeSpan OpenFileRetryInterval
		{
			get
			{
				return (TimeSpan)base["OpenFileRetryInterval"];
			}
			internal set
			{
				base["OpenFileRetryInterval"] = value;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000B0A6 File Offset: 0x000092A6
		// (set) Token: 0x06000232 RID: 562 RVA: 0x0000B0B8 File Offset: 0x000092B8
		[TimeSpanValidator(MinValueString = "00:00:01")]
		[ConfigurationProperty("BacklogAlertNonUrgentThreshold", IsRequired = false, DefaultValue = "01:00:00")]
		public TimeSpan BacklogAlertNonUrgentThreshold
		{
			get
			{
				return (TimeSpan)base["BacklogAlertNonUrgentThreshold"];
			}
			internal set
			{
				base["BacklogAlertNonUrgentThreshold"] = value;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000B0CB File Offset: 0x000092CB
		// (set) Token: 0x06000234 RID: 564 RVA: 0x0000B0DD File Offset: 0x000092DD
		[ConfigurationProperty("BacklogAlertUrgentThreshold", IsRequired = false, DefaultValue = "04:00:00")]
		[TimeSpanValidator(MinValueString = "00:00:01")]
		public TimeSpan BacklogAlertUrgentThreshold
		{
			get
			{
				return (TimeSpan)base["BacklogAlertUrgentThreshold"];
			}
			internal set
			{
				base["BacklogAlertUrgentThreshold"] = value;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000B0F0 File Offset: 0x000092F0
		// (set) Token: 0x06000236 RID: 566 RVA: 0x0000B102 File Offset: 0x00009302
		[ConfigurationProperty("BacklogAlwaysAlertThreshold", IsRequired = false, DefaultValue = "12:00:00")]
		[TimeSpanValidator(MinValueString = "00:00:01")]
		public TimeSpan BacklogAlwaysAlertThreshold
		{
			get
			{
				return (TimeSpan)base["BacklogAlwaysAlertThreshold"];
			}
			internal set
			{
				base["BacklogAlwaysAlertThreshold"] = value;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000B115 File Offset: 0x00009315
		// (set) Token: 0x06000238 RID: 568 RVA: 0x0000B127 File Offset: 0x00009327
		[TimeSpanValidator(MinValueString = "00:00:01")]
		[ConfigurationProperty("WaitTimeToReprocessActiveFile", IsRequired = false, DefaultValue = "00:00:45")]
		public TimeSpan WaitTimeToReprocessActiveFile
		{
			get
			{
				return (TimeSpan)base["WaitTimeToReprocessActiveFile"];
			}
			internal set
			{
				base["WaitTimeToReprocessActiveFile"] = value;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000B13A File Offset: 0x0000933A
		// (set) Token: 0x0600023A RID: 570 RVA: 0x0000B14C File Offset: 0x0000934C
		[ConfigurationProperty("WaitTimeToReprocessActiveFileRandomRange", IsRequired = false, DefaultValue = "00:00:45")]
		[TimeSpanValidator(MinValueString = "00:00:01", MaxValueString = "00:15:00")]
		public TimeSpan WaitTimeToReprocessActiveFileRandomRange
		{
			get
			{
				return (TimeSpan)base["WaitTimeToReprocessActiveFileRandomRange"];
			}
			internal set
			{
				base["WaitTimeToReprocessActiveFileRandomRange"] = value;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000B15F File Offset: 0x0000935F
		// (set) Token: 0x0600023C RID: 572 RVA: 0x0000B171 File Offset: 0x00009371
		[ConfigurationProperty("BatchFlushInterval", IsRequired = false, DefaultValue = "00:00:30")]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "00:05:00")]
		public TimeSpan BatchFlushInterval
		{
			get
			{
				return (TimeSpan)base["BatchFlushInterval"];
			}
			internal set
			{
				base["BatchFlushInterval"] = value;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000B184 File Offset: 0x00009384
		[TimeSpanValidator(MinValueString = "00:00:10", MaxValueString = "12:00:00")]
		[ConfigurationProperty("RegionalFilteringFileRolloverTime", IsRequired = false, DefaultValue = "00:10:00")]
		public TimeSpan RegionalLogFilteringOutputRolloverTime
		{
			get
			{
				return (TimeSpan)base["RegionalFilteringFileRolloverTime"];
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000B196 File Offset: 0x00009396
		// (set) Token: 0x0600023F RID: 575 RVA: 0x0000B1A8 File Offset: 0x000093A8
		[ConfigurationProperty("RetriesBeforeAlert", IsRequired = false, DefaultValue = "10")]
		[IntegerValidator(MinValue = 1)]
		public int RetriesBeforeAlert
		{
			get
			{
				return (int)base["RetriesBeforeAlert"];
			}
			internal set
			{
				base["RetriesBeforeAlert"] = value;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000B1BB File Offset: 0x000093BB
		// (set) Token: 0x06000241 RID: 577 RVA: 0x0000B1CD File Offset: 0x000093CD
		[IntegerValidator(MinValue = 1)]
		[ConfigurationProperty("RetryCount", IsRequired = false, DefaultValue = "60")]
		public int RetryCount
		{
			get
			{
				return (int)base["RetryCount"];
			}
			internal set
			{
				base["RetryCount"] = value;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000B1E0 File Offset: 0x000093E0
		// (set) Token: 0x06000243 RID: 579 RVA: 0x0000B1F2 File Offset: 0x000093F2
		[ConfigurationProperty("ProcessAllSplitLogPartitionsInParallel", IsRequired = false, DefaultValue = "false")]
		public bool ProcessAllSplitLogPartitionsInParallel
		{
			get
			{
				return (bool)base["ProcessAllSplitLogPartitionsInParallel"];
			}
			internal set
			{
				base["ProcessAllSplitLogPartitionsInParallel"] = value;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000B205 File Offset: 0x00009405
		// (set) Token: 0x06000245 RID: 581 RVA: 0x0000B217 File Offset: 0x00009417
		[ConfigurationProperty("InputBufferMaxBatchCount", IsRequired = false, DefaultValue = "0")]
		public int InputBufferMaximumBatchCount
		{
			get
			{
				return (int)base["InputBufferMaxBatchCount"];
			}
			internal set
			{
				base["InputBufferMaxBatchCount"] = value;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000B22A File Offset: 0x0000942A
		[ConfigurationProperty("ReaderPriority", IsRequired = false, DefaultValue = "Normal")]
		public ThreadPriority ReaderPrioritySetting
		{
			get
			{
				return (ThreadPriority)base["ReaderPriority"];
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000B23C File Offset: 0x0000943C
		[ConfigurationProperty("WriterPriority", IsRequired = false, DefaultValue = "Normal")]
		public ThreadPriority WriterPrioritySetting
		{
			get
			{
				return (ThreadPriority)base["WriterPriority"];
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000B24E File Offset: 0x0000944E
		[ConfigurationProperty("SaveLogForRegion", IsRequired = false, DefaultValue = "Unknown")]
		public Region SaveLogForRegion
		{
			get
			{
				return (Region)base["SaveLogForRegion"];
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000B260 File Offset: 0x00009460
		// (set) Token: 0x0600024A RID: 586 RVA: 0x0000B272 File Offset: 0x00009472
		[ConfigurationProperty("EnableMultipleWriters", IsRequired = false, DefaultValue = false)]
		public bool EnableMultipleWriters
		{
			get
			{
				return (bool)base["EnableMultipleWriters"];
			}
			internal set
			{
				base["EnableMultipleWriters"] = value;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000B285 File Offset: 0x00009485
		// (set) Token: 0x0600024C RID: 588 RVA: 0x0000B297 File Offset: 0x00009497
		[ConfigurationProperty("ActiveLogFileIdleTimeout", IsRequired = false, DefaultValue = "2.00:00:00")]
		public TimeSpan ActiveLogFileIdleTimeout
		{
			get
			{
				return (TimeSpan)base["ActiveLogFileIdleTimeout"];
			}
			internal set
			{
				base["ActiveLogFileIdleTimeout"] = value;
			}
		}

		// Token: 0x04000146 RID: 326
		public const string NameKey = "Name";

		// Token: 0x04000147 RID: 327
		public const string BatchSizeKey = "BatchSizeInBytes";

		// Token: 0x04000148 RID: 328
		public const string MaxNumOfReadersKey = "MaxNumOfReaders";

		// Token: 0x04000149 RID: 329
		public const string ReaderPriority = "ReaderPriority";

		// Token: 0x0400014A RID: 330
		public const string MaxNumOfWritersKey = "MaxNumOfWriters";

		// Token: 0x0400014B RID: 331
		public const string WriterPriority = "WriterPriority";

		// Token: 0x0400014C RID: 332
		public const string ReaderSleepTimeKey = "ReaderSleepTime";

		// Token: 0x0400014D RID: 333
		public const string ReaderSleepTimeRandomRangeKey = "ReaderSleepTimeRandomRange";

		// Token: 0x0400014E RID: 334
		public const string SleepTimeForTransientDBErrorKey = "SleepTimeForTransientDBError";

		// Token: 0x0400014F RID: 335
		public const string LogDirCheckIntervalKey = "LogDirCheckInterval";

		// Token: 0x04000150 RID: 336
		public const string QueueCapacityKey = "QueueCapacity";

		// Token: 0x04000151 RID: 337
		public const string ServiceStopWaitTimeKey = "ServiceStopWaitTime";

		// Token: 0x04000152 RID: 338
		public const string OpenFileRetryIntervalKey = "OpenFileRetryInterval";

		// Token: 0x04000153 RID: 339
		public const string BacklogAlertNonUrgentThresholdKey = "BacklogAlertNonUrgentThreshold";

		// Token: 0x04000154 RID: 340
		public const string BacklogAlertUrgentThresholdKey = "BacklogAlertUrgentThreshold";

		// Token: 0x04000155 RID: 341
		public const string BacklogAlwaysAlertThresholdKey = "BacklogAlwaysAlertThreshold";

		// Token: 0x04000156 RID: 342
		public const string WaitTimeToReprocessActiveFileKey = "WaitTimeToReprocessActiveFile";

		// Token: 0x04000157 RID: 343
		public const string WaitTimeToReprocessActiveFileRandomRangeKey = "WaitTimeToReprocessActiveFileRandomRange";

		// Token: 0x04000158 RID: 344
		public const string BatchFlushIntervalKey = "BatchFlushInterval";

		// Token: 0x04000159 RID: 345
		public const string RetriesBeforeAlertKey = "RetriesBeforeAlert";

		// Token: 0x0400015A RID: 346
		public const string RetryCountKey = "RetryCount";

		// Token: 0x0400015B RID: 347
		public const string ProcessAllSplitLogPartitionsInParallelKey = "ProcessAllSplitLogPartitionsInParallel";

		// Token: 0x0400015C RID: 348
		public const string MaxInputBufferBatchCount = "InputBufferMaxBatchCount";

		// Token: 0x0400015D RID: 349
		public const string EnableMultipleWritersKey = "EnableMultipleWriters";

		// Token: 0x0400015E RID: 350
		public const string ActiveLogFileIdleTimeoutKey = "ActiveLogFileIdleTimeout";
	}
}
