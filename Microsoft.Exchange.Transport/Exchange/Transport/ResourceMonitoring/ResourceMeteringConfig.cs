using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Metering.ResourceMonitoring;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Common;

namespace Microsoft.Exchange.Transport.ResourceMonitoring
{
	// Token: 0x020002EC RID: 748
	internal class ResourceMeteringConfig : TransportAppConfig
	{
		// Token: 0x06002127 RID: 8487 RVA: 0x0007D950 File Offset: 0x0007BB50
		public ResourceMeteringConfig(int maxVersionBuckets, NameValueCollection appSettings = null) : base(appSettings)
		{
			ArgumentValidator.ThrowIfInvalidValue<int>("maxVersionBuckets", maxVersionBuckets, (int versionBuckets) => versionBuckets > 100);
			this.maxPressureTransitions.Add("UsedVersionBuckets", new PressureTransitions((long)(maxVersionBuckets - 97), (long)(maxVersionBuckets - 98), (long)(maxVersionBuckets - 99), (long)(maxVersionBuckets - 100)));
			this.isResourceTrackingEnabled = base.GetConfigBool("ResourceTrackingEnabled", false);
			if (this.isResourceTrackingEnabled)
			{
				this.statusPublishInterval = base.GetConfigTimeSpan("StatusPublishInterval", TimeSpan.FromSeconds(10.0), TimeSpan.FromMinutes(30.0), TimeSpan.FromMinutes(1.0));
				this.disabledResources = this.GetDisabledResources();
				this.resourceMeteringInterval = base.GetConfigTimeSpan("ResourceMeteringInterval", TimeSpan.FromSeconds(1.0), TimeSpan.FromMinutes(30.0), TimeSpan.FromSeconds(2.0));
				this.resourceLoggingInterval = base.GetConfigTimeSpan("ResourceLoggingInterval", TimeSpan.FromSeconds(1.0), TimeSpan.FromMinutes(30.0), TimeSpan.FromSeconds(2.0));
				this.resourceMeterTimeout = base.GetConfigTimeSpan("ResourceMeterTimeout", TimeSpan.FromSeconds(1.0), TimeSpan.FromMinutes(30.0), TimeSpan.FromMinutes(5.0));
				this.maxTransientExceptionsAllowed = base.GetConfigInt("MaxTransientExceptionsAllowed", 1, 100, 5);
				this.versionBucketsStabilizationSamples = base.GetConfigInt("VersionBucketsStabilizationSamples", 2, 1000, 10);
				this.privateBytesStabilizationSamples = base.GetConfigInt("PrivateBytesStabilizationSamples", 2, 1000, 30);
				this.submissionQueueStabilizationSamples = base.GetConfigInt("SubmissionQueueStabilizationSamples", 2, 1000, 300);
				this.sustainedDuration = base.GetConfigTimeSpan("SustainedDuration", TimeSpan.FromMinutes(1.0), TimeSpan.FromHours(1.0), TimeSpan.FromMinutes(5.0));
			}
			this.resourceLogBackgroundWriteInterval = base.GetConfigTimeSpan("ResourceLogBackgroundWriteInterval", TimeSpan.FromMilliseconds(100.0), TimeSpan.FromSeconds(20.0), TimeSpan.FromSeconds(15.0));
			this.resourceLogBufferSize = base.GetConfigInt("ResourceLogBufferSize", 0, (int)ByteQuantifiedSize.FromMB(10UL).ToBytes(), (int)ByteQuantifiedSize.FromKB(64UL).ToBytes());
			this.resourceLogFlushInterval = base.GetConfigTimeSpan("ResourceLogFlushInterval", TimeSpan.Zero, TimeSpan.MaxValue, TimeSpan.FromSeconds(60.0));
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06002128 RID: 8488 RVA: 0x0007DE0A File Offset: 0x0007C00A
		public TimeSpan SustainedDuration
		{
			get
			{
				return this.sustainedDuration;
			}
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06002129 RID: 8489 RVA: 0x0007DE12 File Offset: 0x0007C012
		public TimeSpan StatusPublishInterval
		{
			get
			{
				return this.statusPublishInterval;
			}
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x0007DE1A File Offset: 0x0007C01A
		public Dictionary<ResourceIdentifier, PressureTransitions> GetPressureTransitionsForResources(IEnumerable<ResourceIdentifier> meteredResources)
		{
			return this.FetchPressureTransitions(meteredResources);
		}

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x0600212B RID: 8491 RVA: 0x0007DE23 File Offset: 0x0007C023
		public IEnumerable<ResourceIdentifier> DisabledResources
		{
			get
			{
				return this.disabledResources;
			}
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x0600212C RID: 8492 RVA: 0x0007DE2B File Offset: 0x0007C02B
		public TimeSpan ResourceMeteringInterval
		{
			get
			{
				return this.resourceMeteringInterval;
			}
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x0600212D RID: 8493 RVA: 0x0007DE33 File Offset: 0x0007C033
		public TimeSpan ResourceLoggingInterval
		{
			get
			{
				return this.resourceLoggingInterval;
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x0600212E RID: 8494 RVA: 0x0007DE3B File Offset: 0x0007C03B
		public TimeSpan ResourceLogFlushInterval
		{
			get
			{
				return this.resourceLogFlushInterval;
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x0600212F RID: 8495 RVA: 0x0007DE43 File Offset: 0x0007C043
		public TimeSpan ResourceLogBackgroundWriteInterval
		{
			get
			{
				return this.resourceLogBackgroundWriteInterval;
			}
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06002130 RID: 8496 RVA: 0x0007DE4B File Offset: 0x0007C04B
		public int ResourceLogBufferSize
		{
			get
			{
				return this.resourceLogBufferSize;
			}
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06002131 RID: 8497 RVA: 0x0007DE53 File Offset: 0x0007C053
		public TimeSpan ResourceMeterTimeout
		{
			get
			{
				return this.resourceMeterTimeout;
			}
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06002132 RID: 8498 RVA: 0x0007DE5B File Offset: 0x0007C05B
		public int MaxTransientExceptionsAllowed
		{
			get
			{
				return this.maxTransientExceptionsAllowed;
			}
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06002133 RID: 8499 RVA: 0x0007DE63 File Offset: 0x0007C063
		public int VersionBucketsStabilizationSamples
		{
			get
			{
				return this.versionBucketsStabilizationSamples;
			}
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06002134 RID: 8500 RVA: 0x0007DE6B File Offset: 0x0007C06B
		public int PrivateBytesStabilizationSamples
		{
			get
			{
				return this.privateBytesStabilizationSamples;
			}
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06002135 RID: 8501 RVA: 0x0007DE73 File Offset: 0x0007C073
		public int SubmissionQueueStabilizationSamples
		{
			get
			{
				return this.submissionQueueStabilizationSamples;
			}
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06002136 RID: 8502 RVA: 0x0007DE7B File Offset: 0x0007C07B
		public bool IsResourceTrackingEnabled
		{
			get
			{
				return this.isResourceTrackingEnabled;
			}
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x0007DE84 File Offset: 0x0007C084
		private static PressureTransitions GetDefaultPrivateBytesPressureTransitions()
		{
			ulong val;
			if (Environment.Is64BitProcess)
			{
				val = 1099511627776UL;
			}
			else if (ResourceMeteringConfig.GetTotalVirtualMemory() > ByteQuantifiedSize.FromGB(2UL).ToBytes())
			{
				val = 1887436800UL;
			}
			else
			{
				val = 838860800UL;
			}
			ulong num = Math.Min(ResourceMeteringConfig.TotalPhysicalMemory * 75UL / 100UL, val);
			int num2 = (int)(num * 100UL / ResourceMeteringConfig.TotalPhysicalMemory);
			return new PressureTransitions((long)num2, (long)(num2 - 2), (long)(num2 - 3), (long)(num2 - 4));
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x0007DF00 File Offset: 0x0007C100
		private static ulong GetTotalVirtualMemory()
		{
			INativeMethodsWrapper nativeMethodsWrapper = NativeMethodsWrapperFactory.CreateNativeMethodsWrapper();
			ulong result;
			if (!nativeMethodsWrapper.GetTotalVirtualMemory(out result))
			{
				throw new Win32Exception();
			}
			return result;
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x0007DF24 File Offset: 0x0007C124
		private static ulong GetTotalPhysicalMemory()
		{
			INativeMethodsWrapper nativeMethodsWrapper = NativeMethodsWrapperFactory.CreateNativeMethodsWrapper();
			ulong result;
			if (!nativeMethodsWrapper.GetTotalSystemMemory(out result))
			{
				throw new Win32Exception();
			}
			return result;
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x0007DF74 File Offset: 0x0007C174
		private Dictionary<ResourceIdentifier, PressureTransitions> FetchPressureTransitions(IEnumerable<ResourceIdentifier> meteredResources)
		{
			Dictionary<ResourceIdentifier, PressureTransitions> dictionary = new Dictionary<ResourceIdentifier, PressureTransitions>();
			IEnumerable<ResourceIdentifier> source = this.GetDisabledResources();
			using (IEnumerator<ResourceIdentifier> enumerator = meteredResources.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ResourceMeteringConfig.<>c__DisplayClass8 CS$<>8__locals1 = new ResourceMeteringConfig.<>c__DisplayClass8();
					CS$<>8__locals1.resource = enumerator.Current;
					ResourceIdentifier instanceAgnosticResource = new ResourceIdentifier(CS$<>8__locals1.resource.Name, "");
					if (!source.Any((ResourceIdentifier disabled) => CS$<>8__locals1.resource == disabled) && !source.Any((ResourceIdentifier disabled) => instanceAgnosticResource == disabled) && !dictionary.ContainsKey(CS$<>8__locals1.resource))
					{
						PressureTransitions value = this.FetchPressureTransitionForResource(CS$<>8__locals1.resource);
						dictionary.Add(CS$<>8__locals1.resource, value);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x0007E064 File Offset: 0x0007C264
		private PressureTransitions FetchPressureTransitionForResource(ResourceIdentifier resource)
		{
			PressureTransitions pressureTransitions = this.minPressureTransitions[resource.Name];
			PressureTransitions pressureTransitions2 = this.maxPressureTransitions[resource.Name];
			PressureTransitions pressureTransitions3 = this.defaultPressureTransitions[resource.Name];
			long pressureTransitionValue = this.GetPressureTransitionValue(resource, "MediumToHigh", pressureTransitions.MediumToHigh, pressureTransitions2.MediumToHigh, pressureTransitions3.MediumToHigh);
			long pressureTransitionValue2 = this.GetPressureTransitionValue(resource, "HighToMedium", pressureTransitions.HighToMedium, pressureTransitions2.HighToMedium, pressureTransitions3.HighToMedium);
			long pressureTransitionValue3 = this.GetPressureTransitionValue(resource, "LowToMedium", pressureTransitions.LowToMedium, pressureTransitions2.LowToMedium, pressureTransitions3.LowToMedium);
			long pressureTransitionValue4 = this.GetPressureTransitionValue(resource, "MediumToLow", pressureTransitions.MediumToLow, pressureTransitions2.MediumToLow, pressureTransitions3.MediumToLow);
			return new PressureTransitions(pressureTransitionValue, pressureTransitionValue2, pressureTransitionValue3, pressureTransitionValue4);
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x0007E140 File Offset: 0x0007C340
		private long GetPressureTransitionValue(ResourceIdentifier resource, string transitionName, long minValue, long maxValue, long defaultValue)
		{
			string configKeyForResourceTransition = this.GetConfigKeyForResourceTransition(resource, transitionName);
			if (string.IsNullOrEmpty(this.AppSettings[configKeyForResourceTransition]))
			{
				ResourceIdentifier resource2 = new ResourceIdentifier(resource.Name, "");
				configKeyForResourceTransition = this.GetConfigKeyForResourceTransition(resource2, transitionName);
			}
			return base.GetConfigLong(configKeyForResourceTransition, minValue, maxValue, defaultValue);
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x0007E190 File Offset: 0x0007C390
		private IEnumerable<ResourceIdentifier> GetDisabledResources()
		{
			return base.GetConfigList<ResourceIdentifier>("DisabledResourceMeters", ';', new AppConfig.TryParse<ResourceIdentifier>(ResourceIdentifier.TryParse));
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x0007E1B8 File Offset: 0x0007C3B8
		private string GetConfigKeyForResourceTransition(ResourceIdentifier resource, string transition)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
			{
				resource.ToString(),
				transition
			});
		}

		// Token: 0x04001153 RID: 4435
		private const int MaxStabilizationSampleCount = 1000;

		// Token: 0x04001154 RID: 4436
		private const ulong PrivateBytesLimit2GB = 838860800UL;

		// Token: 0x04001155 RID: 4437
		private const ulong PrivateBytesLimit3GB = 1887436800UL;

		// Token: 0x04001156 RID: 4438
		private const ulong PrivateBytesLimit64Bit = 1099511627776UL;

		// Token: 0x04001157 RID: 4439
		private readonly TimeSpan statusPublishInterval;

		// Token: 0x04001158 RID: 4440
		public static readonly ulong TotalPhysicalMemory = ResourceMeteringConfig.GetTotalPhysicalMemory();

		// Token: 0x04001159 RID: 4441
		private readonly Dictionary<string, PressureTransitions> minPressureTransitions = new Dictionary<string, PressureTransitions>
		{
			{
				"PrivateBytes",
				new PressureTransitions(4L, 3L, 2L, 1L)
			},
			{
				"QueueLength",
				new PressureTransitions(4L, 3L, 2L, 1L)
			},
			{
				"UsedVersionBuckets",
				new PressureTransitions(4L, 3L, 2L, 1L)
			},
			{
				"DatabaseUsedSpace",
				new PressureTransitions(10L, 5L, 4L, 3L)
			},
			{
				"UsedDiskSpace",
				new PressureTransitions(10L, 5L, 4L, 3L)
			},
			{
				"SystemMemory",
				new PressureTransitions(85L, 80L, 75L, 70L)
			}
		};

		// Token: 0x0400115A RID: 4442
		private readonly Dictionary<string, PressureTransitions> defaultPressureTransitions = new Dictionary<string, PressureTransitions>
		{
			{
				"PrivateBytes",
				ResourceMeteringConfig.GetDefaultPrivateBytesPressureTransitions()
			},
			{
				"QueueLength",
				new PressureTransitions(15000L, 10000L, 9999L, 2000L)
			},
			{
				"UsedVersionBuckets",
				new PressureTransitions(2500L, 2000L, 1999L, 1750L)
			},
			{
				"DatabaseUsedSpace",
				new PressureTransitions(100L, 98L, 97L, 96L)
			},
			{
				"UsedDiskSpace",
				new PressureTransitions(99L, 90L, 89L, 80L)
			},
			{
				"SystemMemory",
				new PressureTransitions(94L, 89L, 88L, 84L)
			}
		};

		// Token: 0x0400115B RID: 4443
		private readonly Dictionary<string, PressureTransitions> maxPressureTransitions = new Dictionary<string, PressureTransitions>
		{
			{
				"PrivateBytes",
				new PressureTransitions(100L, 99L, 98L, 97L)
			},
			{
				"QueueLength",
				new PressureTransitions(50000L, 45000L, 40000L, 35000L)
			},
			{
				"DatabaseUsedSpace",
				new PressureTransitions(100L, 98L, 97L, 96L)
			},
			{
				"UsedDiskSpace",
				new PressureTransitions(100L, 98L, 97L, 96L)
			},
			{
				"SystemMemory",
				new PressureTransitions(94L, 90L, 85L, 80L)
			}
		};

		// Token: 0x0400115C RID: 4444
		private readonly IEnumerable<ResourceIdentifier> disabledResources;

		// Token: 0x0400115D RID: 4445
		private readonly TimeSpan resourceMeteringInterval;

		// Token: 0x0400115E RID: 4446
		private readonly TimeSpan resourceLoggingInterval;

		// Token: 0x0400115F RID: 4447
		private readonly TimeSpan resourceMeterTimeout;

		// Token: 0x04001160 RID: 4448
		private readonly int maxTransientExceptionsAllowed;

		// Token: 0x04001161 RID: 4449
		private readonly int versionBucketsStabilizationSamples;

		// Token: 0x04001162 RID: 4450
		private readonly int privateBytesStabilizationSamples;

		// Token: 0x04001163 RID: 4451
		private readonly int submissionQueueStabilizationSamples;

		// Token: 0x04001164 RID: 4452
		private readonly bool isResourceTrackingEnabled;

		// Token: 0x04001165 RID: 4453
		private readonly TimeSpan resourceLogFlushInterval;

		// Token: 0x04001166 RID: 4454
		private readonly TimeSpan resourceLogBackgroundWriteInterval;

		// Token: 0x04001167 RID: 4455
		private readonly int resourceLogBufferSize;

		// Token: 0x04001168 RID: 4456
		private readonly TimeSpan sustainedDuration;
	}
}
