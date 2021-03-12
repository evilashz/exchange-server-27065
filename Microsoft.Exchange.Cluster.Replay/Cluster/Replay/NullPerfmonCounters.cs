using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000121 RID: 289
	internal class NullPerfmonCounters : IPerfmonCounters
	{
		// Token: 0x06000AF4 RID: 2804 RVA: 0x000313C5 File Offset: 0x0002F5C5
		public void Reset()
		{
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x000313C7 File Offset: 0x0002F5C7
		// (set) Token: 0x06000AF6 RID: 2806 RVA: 0x000313CB File Offset: 0x0002F5CB
		public long CopyNotificationGenerationNumber
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		// Token: 0x17000253 RID: 595
		// (set) Token: 0x06000AF7 RID: 2807 RVA: 0x000313CD File Offset: 0x0002F5CD
		public long CopyGenerationNumber
		{
			set
			{
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x000313CF File Offset: 0x0002F5CF
		public float CopyNotificationGenerationsPerSecond
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x000313D6 File Offset: 0x0002F5D6
		public float InspectorGenerationsPerSecond
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x000313DD File Offset: 0x0002F5DD
		// (set) Token: 0x06000AFB RID: 2811 RVA: 0x000313E1 File Offset: 0x0002F5E1
		public long InspectorGenerationNumber
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x000313E3 File Offset: 0x0002F5E3
		public long CopyQueueLength
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x000313E7 File Offset: 0x0002F5E7
		public long RawCopyQueueLength
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x000313EB File Offset: 0x0002F5EB
		// (set) Token: 0x06000AFF RID: 2815 RVA: 0x000313EF File Offset: 0x0002F5EF
		public long CopyQueueNotKeepingUp
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		// Token: 0x1700025A RID: 602
		// (set) Token: 0x06000B00 RID: 2816 RVA: 0x000313F1 File Offset: 0x0002F5F1
		public long ReplayNotificationGenerationNumber
		{
			set
			{
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x000313F3 File Offset: 0x0002F5F3
		// (set) Token: 0x06000B02 RID: 2818 RVA: 0x000313F7 File Offset: 0x0002F5F7
		public long ReplayGenerationNumber
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x000313F9 File Offset: 0x0002F5F9
		public float ReplayGenerationsPerSecond
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000B04 RID: 2820 RVA: 0x00031400 File Offset: 0x0002F600
		public long ReplayQueueLength
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x00031404 File Offset: 0x0002F604
		// (set) Token: 0x06000B06 RID: 2822 RVA: 0x00031408 File Offset: 0x0002F608
		public long ReplayQueueNotKeepingUp
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x0003140A File Offset: 0x0002F60A
		// (set) Token: 0x06000B08 RID: 2824 RVA: 0x0003140E File Offset: 0x0002F60E
		public long Failed
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x00031410 File Offset: 0x0002F610
		// (set) Token: 0x06000B0A RID: 2826 RVA: 0x00031414 File Offset: 0x0002F614
		public long Initializing
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x00031416 File Offset: 0x0002F616
		// (set) Token: 0x06000B0C RID: 2828 RVA: 0x0003141A File Offset: 0x0002F61A
		public long Resynchronizing
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x0003141C File Offset: 0x0002F61C
		// (set) Token: 0x06000B0E RID: 2830 RVA: 0x00031420 File Offset: 0x0002F620
		public long ActivationSuspended
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x00031422 File Offset: 0x0002F622
		// (set) Token: 0x06000B10 RID: 2832 RVA: 0x00031426 File Offset: 0x0002F626
		public long ReplayLagDisabled
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x00031428 File Offset: 0x0002F628
		// (set) Token: 0x06000B12 RID: 2834 RVA: 0x0003142C File Offset: 0x0002F62C
		public long ReplayLagPercentage
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x0003142E File Offset: 0x0002F62E
		// (set) Token: 0x06000B14 RID: 2836 RVA: 0x00031432 File Offset: 0x0002F632
		public long FailedSuspended
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x00031434 File Offset: 0x0002F634
		// (set) Token: 0x06000B16 RID: 2838 RVA: 0x00031438 File Offset: 0x0002F638
		public long SinglePageRestore
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x0003143A File Offset: 0x0002F63A
		// (set) Token: 0x06000B18 RID: 2840 RVA: 0x0003143E File Offset: 0x0002F63E
		public long Disconnected
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x00031440 File Offset: 0x0002F640
		// (set) Token: 0x06000B1A RID: 2842 RVA: 0x00031444 File Offset: 0x0002F644
		public long Suspended
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x00031446 File Offset: 0x0002F646
		// (set) Token: 0x06000B1C RID: 2844 RVA: 0x0003144A File Offset: 0x0002F64A
		public long SuspendedAndNotSeeding
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x0003144C File Offset: 0x0002F64C
		// (set) Token: 0x06000B1E RID: 2846 RVA: 0x00031450 File Offset: 0x0002F650
		public long Seeding
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x00031452 File Offset: 0x0002F652
		// (set) Token: 0x06000B20 RID: 2848 RVA: 0x0003145A File Offset: 0x0002F65A
		public long CompressionEnabled { get; set; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x00031463 File Offset: 0x0002F663
		// (set) Token: 0x06000B22 RID: 2850 RVA: 0x0003146B File Offset: 0x0002F66B
		public long EncryptionEnabled { get; set; }

		// Token: 0x1700026D RID: 621
		// (set) Token: 0x06000B23 RID: 2851 RVA: 0x00031474 File Offset: 0x0002F674
		public long TruncatedGenerationNumber
		{
			set
			{
			}
		}

		// Token: 0x1700026E RID: 622
		// (set) Token: 0x06000B24 RID: 2852 RVA: 0x00031476 File Offset: 0x0002F676
		public long IncReseedDBPagesReadNumber
		{
			set
			{
			}
		}

		// Token: 0x1700026F RID: 623
		// (set) Token: 0x06000B25 RID: 2853 RVA: 0x00031478 File Offset: 0x0002F678
		public long IncReseedLogCopiedNumber
		{
			set
			{
			}
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0003147A File Offset: 0x0002F67A
		public void RecordLogCopyThruput(long bytesCopied)
		{
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x0003147C File Offset: 0x0002F67C
		// (set) Token: 0x06000B28 RID: 2856 RVA: 0x00031480 File Offset: 0x0002F680
		public long GranularReplication
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00031482 File Offset: 0x0002F682
		public void RecordLogCopierNetworkReadLatency(long tics)
		{
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00031484 File Offset: 0x0002F684
		public void RecordBlockModeConsumerWriteLatency(long tics)
		{
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00031486 File Offset: 0x0002F686
		public void RecordFileModeWriteLatency(long tics)
		{
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x00031488 File Offset: 0x0002F688
		// (set) Token: 0x06000B2D RID: 2861 RVA: 0x0003148C File Offset: 0x0002F68C
		public long PassiveSeedingSource
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0003148E File Offset: 0x0002F68E
		public void RecordGranularBytesReceived(long bytesCopied, bool logIsComplete)
		{
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00031490 File Offset: 0x0002F690
		public void RecordOneGetCopyStatusCall()
		{
		}
	}
}
