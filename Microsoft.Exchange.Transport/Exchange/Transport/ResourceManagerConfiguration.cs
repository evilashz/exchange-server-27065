using System;
using System.Xml.Linq;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000047 RID: 71
	internal class ResourceManagerConfiguration
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x000089EB File Offset: 0x00006BEB
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x000089F3 File Offset: 0x00006BF3
		public bool EnableResourceMonitoring
		{
			get
			{
				return this.enableResourceMonitoring;
			}
			protected set
			{
				this.enableResourceMonitoring = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x000089FC File Offset: 0x00006BFC
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x00008A04 File Offset: 0x00006C04
		public TimeSpan ResourceMonitoringInterval
		{
			get
			{
				return this.resourceMonitoringInterval;
			}
			protected set
			{
				this.resourceMonitoringInterval = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00008A0D File Offset: 0x00006C0D
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x00008A15 File Offset: 0x00006C15
		public bool DehydrateMessagesUnderMemoryPressure
		{
			get
			{
				return this.dehydrateMessagesUnderMemoryPressure;
			}
			protected set
			{
				this.dehydrateMessagesUnderMemoryPressure = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00008A1E File Offset: 0x00006C1E
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x00008A26 File Offset: 0x00006C26
		public ResourceManagerConfiguration.StabilizedResourceMonitorConfiguration PrivateBytesResourceMonitor
		{
			get
			{
				return this.privateBytesResourceMonitor;
			}
			protected set
			{
				this.privateBytesResourceMonitor = value;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00008A2F File Offset: 0x00006C2F
		// (set) Token: 0x060001BA RID: 442 RVA: 0x00008A37 File Offset: 0x00006C37
		public ResourceManagerConfiguration.ResourceMonitorConfiguration DatabaseDiskSpaceResourceMonitor
		{
			get
			{
				return this.databaseDiskSpaceResourceMonitor;
			}
			protected set
			{
				this.databaseDiskSpaceResourceMonitor = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00008A40 File Offset: 0x00006C40
		// (set) Token: 0x060001BC RID: 444 RVA: 0x00008A48 File Offset: 0x00006C48
		public ResourceManagerConfiguration.ResourceMonitorConfiguration DatabaseLoggingDiskSpaceResourceMonitor
		{
			get
			{
				return this.databaseLoggingDiskSpaceResourceMonitor;
			}
			protected set
			{
				this.databaseLoggingDiskSpaceResourceMonitor = value;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00008A51 File Offset: 0x00006C51
		// (set) Token: 0x060001BE RID: 446 RVA: 0x00008A59 File Offset: 0x00006C59
		public ResourceManagerConfiguration.StabilizedResourceMonitorConfiguration VersionBucketsResourceMonitor
		{
			get
			{
				return this.versionBucketsResourceMonitor;
			}
			protected set
			{
				this.versionBucketsResourceMonitor = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00008A62 File Offset: 0x00006C62
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x00008A6A File Offset: 0x00006C6A
		public ResourceManagerConfiguration.StabilizedResourceMonitorConfiguration SubmissionQueueResourceMonitor
		{
			get
			{
				return this.submissionQueueResourceMonitor;
			}
			protected set
			{
				this.submissionQueueResourceMonitor = value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00008A73 File Offset: 0x00006C73
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x00008A7B File Offset: 0x00006C7B
		public ResourceManagerConfiguration.ResourceMonitorConfiguration MemoryTotalBytesResourceMonitor
		{
			get
			{
				return this.memoryTotalBytesResourceMonitor;
			}
			protected set
			{
				this.memoryTotalBytesResourceMonitor = value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00008A84 File Offset: 0x00006C84
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x00008A8C File Offset: 0x00006C8C
		public ResourceManagerConfiguration.ThrottlingControllerConfiguration ThrottlingControllerConfig
		{
			get
			{
				return this.throttlingControllerConfiguration;
			}
			protected set
			{
				this.throttlingControllerConfiguration = value;
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00008A98 File Offset: 0x00006C98
		public void AddDiagnosticInfo(XElement parent)
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			parent.Add(new object[]
			{
				new XElement("enableResourceMonitoring", this.enableResourceMonitoring),
				new XElement("resourceMonitoringInterval", this.resourceMonitoringInterval),
				new XElement("dehydrateMessagesUnderMemoryPressure", this.dehydrateMessagesUnderMemoryPressure)
			});
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00008B18 File Offset: 0x00006D18
		public virtual void Load()
		{
			this.enableResourceMonitoring = Components.TransportAppConfig.ResourceManager.EnableResourceMonitoring;
			this.resourceMonitoringInterval = Components.TransportAppConfig.ResourceManager.ResourceMonitoringInterval;
			this.dehydrateMessagesUnderMemoryPressure = Components.TransportAppConfig.ResourceManager.DehydrateMessagesUnderMemoryPressure;
			this.privateBytesResourceMonitor = new ResourceManagerConfiguration.StabilizedResourceMonitorConfiguration(Components.TransportAppConfig.ResourceManager.PercentagePrivateBytesHighThreshold, Components.TransportAppConfig.ResourceManager.PercentagePrivateBytesMediumThreshold, Components.TransportAppConfig.ResourceManager.PercentagePrivateBytesNormalThreshold, Components.TransportAppConfig.ResourceManager.PrivateBytesHistoryDepth);
			this.databaseDiskSpaceResourceMonitor = new ResourceManagerConfiguration.ResourceMonitorConfiguration(Components.TransportAppConfig.ResourceManager.PercentageDatabaseDiskSpaceHighThreshold, Components.TransportAppConfig.ResourceManager.PercentageDatabaseDiskSpaceMediumThreshold, Components.TransportAppConfig.ResourceManager.PercentageDatabaseDiskSpaceNormalThreshold);
			this.databaseLoggingDiskSpaceResourceMonitor = new ResourceManagerConfiguration.ResourceMonitorConfiguration(Components.TransportAppConfig.ResourceManager.PercentageDatabaseLoggingDiskSpaceHighThreshold, Components.TransportAppConfig.ResourceManager.PercentageDatabaseLoggingDiskSpaceMediumThreshold, Components.TransportAppConfig.ResourceManager.PercentageDatabaseLoggingDiskSpaceNormalThreshold);
			this.versionBucketsResourceMonitor = new ResourceManagerConfiguration.StabilizedResourceMonitorConfiguration(Components.TransportAppConfig.ResourceManager.VersionBucketsHighThreshold, Components.TransportAppConfig.ResourceManager.VersionBucketsMediumThreshold, Components.TransportAppConfig.ResourceManager.VersionBucketsNormalThreshold, Components.TransportAppConfig.ResourceManager.VersionBucketsHistoryDepth);
			this.submissionQueueResourceMonitor = new ResourceManagerConfiguration.StabilizedResourceMonitorConfiguration(Components.TransportAppConfig.ResourceManager.SubmissionQueueHighThreshold, Components.TransportAppConfig.ResourceManager.SubmissionQueueMediumThreshold, Components.TransportAppConfig.ResourceManager.SubmissionQueueNormalThreshold, Components.TransportAppConfig.ResourceManager.SubmissionQueueHistoryDepth);
			this.memoryTotalBytesResourceMonitor = new ResourceManagerConfiguration.ResourceMonitorConfiguration(Components.TransportAppConfig.ResourceManager.PercentagePhysicalMemoryUsedLimit, Components.TransportAppConfig.ResourceManager.PercentagePhysicalMemoryUsedLimit - 5, Components.TransportAppConfig.ResourceManager.PercentagePhysicalMemoryUsedLimit - 10);
			this.throttlingControllerConfiguration = new ResourceManagerConfiguration.ThrottlingControllerConfiguration(Components.TransportAppConfig.ResourceManager.BaseThrottlingDelayInterval, Components.TransportAppConfig.ResourceManager.StartThrottlingDelayInterval, Components.TransportAppConfig.ResourceManager.StepThrottlingDelayInterval, Components.TransportAppConfig.ResourceManager.MaxThrottlingDelayInterval);
		}

		// Token: 0x0400010A RID: 266
		private bool enableResourceMonitoring;

		// Token: 0x0400010B RID: 267
		private TimeSpan resourceMonitoringInterval;

		// Token: 0x0400010C RID: 268
		private bool dehydrateMessagesUnderMemoryPressure;

		// Token: 0x0400010D RID: 269
		private ResourceManagerConfiguration.StabilizedResourceMonitorConfiguration privateBytesResourceMonitor;

		// Token: 0x0400010E RID: 270
		private ResourceManagerConfiguration.ResourceMonitorConfiguration databaseDiskSpaceResourceMonitor;

		// Token: 0x0400010F RID: 271
		private ResourceManagerConfiguration.ResourceMonitorConfiguration databaseLoggingDiskSpaceResourceMonitor;

		// Token: 0x04000110 RID: 272
		private ResourceManagerConfiguration.StabilizedResourceMonitorConfiguration versionBucketsResourceMonitor;

		// Token: 0x04000111 RID: 273
		private ResourceManagerConfiguration.StabilizedResourceMonitorConfiguration submissionQueueResourceMonitor;

		// Token: 0x04000112 RID: 274
		private ResourceManagerConfiguration.ResourceMonitorConfiguration memoryTotalBytesResourceMonitor;

		// Token: 0x04000113 RID: 275
		private ResourceManagerConfiguration.ThrottlingControllerConfiguration throttlingControllerConfiguration;

		// Token: 0x02000048 RID: 72
		internal class ResourceMonitorConfiguration
		{
			// Token: 0x060001C7 RID: 455 RVA: 0x00008D2D File Offset: 0x00006F2D
			public ResourceMonitorConfiguration(int highThreshold, int mediumThreshold, int normalThreshold)
			{
				this.highThreshold = highThreshold;
				this.mediumThreshold = mediumThreshold;
				this.normalThreshold = normalThreshold;
			}

			// Token: 0x17000073 RID: 115
			// (get) Token: 0x060001C8 RID: 456 RVA: 0x00008D4A File Offset: 0x00006F4A
			public int HighThreshold
			{
				get
				{
					return this.highThreshold;
				}
			}

			// Token: 0x17000074 RID: 116
			// (get) Token: 0x060001C9 RID: 457 RVA: 0x00008D52 File Offset: 0x00006F52
			public int MediumThreshold
			{
				get
				{
					return this.mediumThreshold;
				}
			}

			// Token: 0x17000075 RID: 117
			// (get) Token: 0x060001CA RID: 458 RVA: 0x00008D5A File Offset: 0x00006F5A
			public int NormalThreshold
			{
				get
				{
					return this.normalThreshold;
				}
			}

			// Token: 0x060001CB RID: 459 RVA: 0x00008D64 File Offset: 0x00006F64
			public virtual void AddDiagnosticInfo(XElement parent)
			{
				if (parent == null)
				{
					throw new ArgumentNullException("parent");
				}
				parent.Add(new object[]
				{
					new XElement("normalThreshold", this.normalThreshold),
					new XElement("mediumThreshold", this.mediumThreshold),
					new XElement("highThreshold", this.highThreshold)
				});
			}

			// Token: 0x04000114 RID: 276
			private int highThreshold;

			// Token: 0x04000115 RID: 277
			private int mediumThreshold;

			// Token: 0x04000116 RID: 278
			private int normalThreshold;
		}

		// Token: 0x02000049 RID: 73
		internal class StabilizedResourceMonitorConfiguration : ResourceManagerConfiguration.ResourceMonitorConfiguration
		{
			// Token: 0x060001CC RID: 460 RVA: 0x00008DE4 File Offset: 0x00006FE4
			public StabilizedResourceMonitorConfiguration(int highThreshold, int mediumThreshold, int normalThreshold, int historyDepth) : base(highThreshold, mediumThreshold, normalThreshold)
			{
				this.historyDepth = historyDepth;
			}

			// Token: 0x17000076 RID: 118
			// (get) Token: 0x060001CD RID: 461 RVA: 0x00008DF7 File Offset: 0x00006FF7
			public int HistoryDepth
			{
				get
				{
					return this.historyDepth;
				}
			}

			// Token: 0x060001CE RID: 462 RVA: 0x00008DFF File Offset: 0x00006FFF
			public override void AddDiagnosticInfo(XElement parent)
			{
				base.AddDiagnosticInfo(parent);
				parent.Add(new XElement("historyDepth", this.historyDepth));
			}

			// Token: 0x04000117 RID: 279
			private int historyDepth;
		}

		// Token: 0x0200004A RID: 74
		internal class ThrottlingControllerConfiguration
		{
			// Token: 0x060001CF RID: 463 RVA: 0x00008E28 File Offset: 0x00007028
			public ThrottlingControllerConfiguration(TimeSpan baseThrottlingDelayInterval, TimeSpan startThrottlingDelayInterval, TimeSpan stepThrottlingDelayInterval, TimeSpan maxThrottlingDelayInterval)
			{
				this.baseThrottlingDelayInterval = baseThrottlingDelayInterval;
				this.maxThrottlingDelayInterval = maxThrottlingDelayInterval;
				this.stepThrottlingDelayInterval = stepThrottlingDelayInterval;
				this.startThrottlingDelayInterval = startThrottlingDelayInterval;
			}

			// Token: 0x17000077 RID: 119
			// (get) Token: 0x060001D0 RID: 464 RVA: 0x00008E4D File Offset: 0x0000704D
			public TimeSpan BaseThrottlingDelayInterval
			{
				get
				{
					return this.baseThrottlingDelayInterval;
				}
			}

			// Token: 0x17000078 RID: 120
			// (get) Token: 0x060001D1 RID: 465 RVA: 0x00008E55 File Offset: 0x00007055
			public TimeSpan MaxThrottlingDelayInterval
			{
				get
				{
					return this.maxThrottlingDelayInterval;
				}
			}

			// Token: 0x17000079 RID: 121
			// (get) Token: 0x060001D2 RID: 466 RVA: 0x00008E5D File Offset: 0x0000705D
			public TimeSpan StepThrottlingDelayInterval
			{
				get
				{
					return this.stepThrottlingDelayInterval;
				}
			}

			// Token: 0x1700007A RID: 122
			// (get) Token: 0x060001D3 RID: 467 RVA: 0x00008E65 File Offset: 0x00007065
			public TimeSpan StartThrottlingDelayInterval
			{
				get
				{
					return this.startThrottlingDelayInterval;
				}
			}

			// Token: 0x060001D4 RID: 468 RVA: 0x00008E70 File Offset: 0x00007070
			public void AddDiagnosticInfo(XElement parent)
			{
				if (parent == null)
				{
					throw new ArgumentNullException("parent");
				}
				parent.Add(new object[]
				{
					new XElement("baseThrottlingDelayInterval", this.baseThrottlingDelayInterval),
					new XElement("startThrottlingDelayInterval", this.startThrottlingDelayInterval),
					new XElement("stepThrottlingDelayInterval", this.stepThrottlingDelayInterval),
					new XElement("maxThrottlingDelayInterval", this.maxThrottlingDelayInterval)
				});
			}

			// Token: 0x04000118 RID: 280
			private TimeSpan baseThrottlingDelayInterval;

			// Token: 0x04000119 RID: 281
			private TimeSpan maxThrottlingDelayInterval;

			// Token: 0x0400011A RID: 282
			private TimeSpan stepThrottlingDelayInterval;

			// Token: 0x0400011B RID: 283
			private TimeSpan startThrottlingDelayInterval;
		}
	}
}
