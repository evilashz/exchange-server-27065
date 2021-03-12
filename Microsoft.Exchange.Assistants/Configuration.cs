using System;
using System.IO;
using System.Security;
using Microsoft.Exchange.Diagnostics.Components.Assistants;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Win32;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000024 RID: 36
	internal sealed class Configuration
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00005D0C File Offset: 0x00003F0C
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00005D13 File Offset: 0x00003F13
		public static TimeSpan EventPollingInterval { get; private set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00005D1B File Offset: 0x00003F1B
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00005D22 File Offset: 0x00003F22
		public static TimeSpan ActiveWatermarksSaveInterval { get; private set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00005D2A File Offset: 0x00003F2A
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00005D31 File Offset: 0x00003F31
		public static TimeSpan IdleWatermarksSaveInterval { get; private set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00005D39 File Offset: 0x00003F39
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00005D40 File Offset: 0x00003F40
		public static TimeSpan WatermarkCleanupInterval { get; private set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00005D48 File Offset: 0x00003F48
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00005D4F File Offset: 0x00003F4F
		public static int MaxThreadsForAllTimeBasedAssistants { get; private set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00005D57 File Offset: 0x00003F57
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00005D5E File Offset: 0x00003F5E
		public static int MaxThreadsPerTimeBasedAssistantType { get; private set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00005D66 File Offset: 0x00003F66
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00005D6D File Offset: 0x00003F6D
		public static TimeSpan HangDetectionTimeout { get; private set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00005D75 File Offset: 0x00003F75
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00005D7C File Offset: 0x00003F7C
		public static TimeSpan HangDetectionPeriod { get; private set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00005D84 File Offset: 0x00003F84
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00005D8B File Offset: 0x00003F8B
		public static int MaximumEventQueueSize { get; private set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00005D93 File Offset: 0x00003F93
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00005D9A File Offset: 0x00003F9A
		public static bool MemoryMonitorEnabled { get; private set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00005DA2 File Offset: 0x00003FA2
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00005DA9 File Offset: 0x00003FA9
		public static int MemoryBarrierNumberOfSamples { get; private set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00005DB1 File Offset: 0x00003FB1
		// (set) Token: 0x06000118 RID: 280 RVA: 0x00005DB8 File Offset: 0x00003FB8
		public static TimeSpan MemoryBarrierSamplingInterval { get; private set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00005DC0 File Offset: 0x00003FC0
		// (set) Token: 0x0600011A RID: 282 RVA: 0x00005DC7 File Offset: 0x00003FC7
		public static TimeSpan MemoryBarrierSamplingDelay { get; private set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00005DCF File Offset: 0x00003FCF
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00005DD6 File Offset: 0x00003FD6
		public static long MemoryBarrierPrivateBytesUsageLimit { get; private set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00005DDE File Offset: 0x00003FDE
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00005DE5 File Offset: 0x00003FE5
		public static TimeSpan WorkCycleUpdatePeriod { get; private set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00005DED File Offset: 0x00003FED
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00005DF4 File Offset: 0x00003FF4
		public static TimeSpan BatchDuration { get; private set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00005DFC File Offset: 0x00003FFC
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00005E03 File Offset: 0x00004003
		public static string ServiceRegistryKeyPath { get; private set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00005E0B File Offset: 0x0000400B
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00005E12 File Offset: 0x00004012
		public static string ParametersRegistryKeyPath { get; private set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00005E1A File Offset: 0x0000401A
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00005E21 File Offset: 0x00004021
		public static string LocalMachineParametersRegistryKeyPath { get; private set; }

		// Token: 0x06000127 RID: 295 RVA: 0x00005E30 File Offset: 0x00004030
		public static void Initialize(string serviceName)
		{
			Configuration.ServiceRegistryKeyPath = Path.Combine("System\\CurrentControlSet\\Services", serviceName);
			Configuration.ParametersRegistryKeyPath = Path.Combine(Configuration.ServiceRegistryKeyPath, "Parameters");
			Configuration.LocalMachineParametersRegistryKeyPath = Path.Combine("HKEY_LOCAL_MACHINE", Configuration.ParametersRegistryKeyPath);
			ExTraceGlobals.DatabaseManagerTracer.TraceDebug<string>(0L, "Configuration: Parameters Registry Key Path = {0}", Configuration.ParametersRegistryKeyPath);
			VariantConfiguration.UpdateCommitted += delegate(object sender, UpdateCommittedEventArgs args)
			{
				Configuration.Update();
			};
			Configuration.Update();
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005EB2 File Offset: 0x000040B2
		internal static void InitializeForTest()
		{
			Configuration.ServiceRegistryKeyPath = string.Empty;
			Configuration.ParametersRegistryKeyPath = string.Empty;
			Configuration.LocalMachineParametersRegistryKeyPath = string.Empty;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005ED4 File Offset: 0x000040D4
		private static void Update()
		{
			IMailboxAssistantServiceSettings mailboxAssistantService = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).MailboxAssistants.MailboxAssistantService;
			Configuration.EventPollingInterval = Configuration.RegistryConfiguration.EventPollingIntervalMsec.Value.GetValueOrDefault(mailboxAssistantService.EventPollingInterval);
			Configuration.ActiveWatermarksSaveInterval = Configuration.RegistryConfiguration.ActiveWatermarksSaveInterval.Value.GetValueOrDefault(mailboxAssistantService.ActiveWatermarksSaveInterval);
			Configuration.IdleWatermarksSaveInterval = Configuration.RegistryConfiguration.IdleWatermarksSaveInterval.Value.GetValueOrDefault(mailboxAssistantService.IdleWatermarksSaveInterval);
			Configuration.WatermarkCleanupInterval = Configuration.RegistryConfiguration.WatermarkCleanupInterval.Value.GetValueOrDefault(mailboxAssistantService.WatermarkCleanupInterval);
			Configuration.MaxThreadsForAllTimeBasedAssistants = Configuration.RegistryConfiguration.MaxThreadsForAllTimeBasedAssistants.Value.GetValueOrDefault(mailboxAssistantService.MaxThreadsForAllTimeBasedAssistants);
			Configuration.MaxThreadsPerTimeBasedAssistantType = Configuration.RegistryConfiguration.MaxThreadsPerTimeBasedAssistantType.Value.GetValueOrDefault(mailboxAssistantService.MaxThreadsPerTimeBasedAssistantType);
			Configuration.HangDetectionTimeout = Configuration.RegistryConfiguration.HangDetectionTimeout.Value.GetValueOrDefault(mailboxAssistantService.HangDetectionTimeout);
			Configuration.HangDetectionPeriod = Configuration.RegistryConfiguration.HangDetectionPeriod.Value.GetValueOrDefault(mailboxAssistantService.HangDetectionPeriod);
			Configuration.MaximumEventQueueSize = Configuration.RegistryConfiguration.MaximumEventQueueSize.Value.GetValueOrDefault(mailboxAssistantService.MaximumEventQueueSize);
			Configuration.MemoryMonitorEnabled = mailboxAssistantService.MemoryMonitorEnabled;
			Configuration.MemoryBarrierNumberOfSamples = Configuration.RegistryConfiguration.MemoryBarrierNumberOfSamples.Value.GetValueOrDefault(mailboxAssistantService.MemoryBarrierNumberOfSamples);
			Configuration.MemoryBarrierSamplingInterval = Configuration.RegistryConfiguration.MemoryBarrierSamplingInterval.Value.GetValueOrDefault(mailboxAssistantService.MemoryBarrierSamplingInterval);
			Configuration.MemoryBarrierSamplingDelay = Configuration.RegistryConfiguration.MemoryBarrierSamplingDelay.Value.GetValueOrDefault(mailboxAssistantService.MemoryBarrierSamplingDelay);
			Configuration.MemoryBarrierPrivateBytesUsageLimit = Configuration.RegistryConfiguration.MemoryBarrierPrivateBytesUsageLimit.Value.GetValueOrDefault(mailboxAssistantService.MemoryBarrierPrivateBytesUsageLimit / 1024L / 1024L);
			Configuration.WorkCycleUpdatePeriod = Configuration.RegistryConfiguration.WorkCycleUpdatePeriod.Value.GetValueOrDefault(mailboxAssistantService.WorkCycleUpdatePeriod);
			Configuration.BatchDuration = mailboxAssistantService.BatchDuration;
		}

		// Token: 0x040000FA RID: 250
		private const string ServicesRootRegistryKeyPath = "System\\CurrentControlSet\\Services";

		// Token: 0x02000025 RID: 37
		private static class RegistryConfiguration
		{
			// Token: 0x0400010F RID: 271
			public static readonly Configuration.ConfigurationProperty<TimeSpan> EventPollingIntervalMsec = new Configuration.ConfigurationProperty<TimeSpan>("EventPollingIntervalMsec", (object value) => TimeSpan.FromMilliseconds((double)((int)value)));

			// Token: 0x04000110 RID: 272
			public static readonly Configuration.ConfigurationProperty<TimeSpan> ActiveWatermarksSaveInterval = new Configuration.ConfigurationProperty<TimeSpan>("ActiveWatermarksSaveInterval", (object value) => TimeSpan.FromSeconds((double)((int)value)));

			// Token: 0x04000111 RID: 273
			public static readonly Configuration.ConfigurationProperty<TimeSpan> IdleWatermarksSaveInterval = new Configuration.ConfigurationProperty<TimeSpan>("IdleWatermarksSaveInterval", (object value) => TimeSpan.FromMinutes((double)((int)value)));

			// Token: 0x04000112 RID: 274
			public static readonly Configuration.ConfigurationProperty<TimeSpan> WatermarkCleanupInterval = new Configuration.ConfigurationProperty<TimeSpan>("WatermarkCleanupInterval", (object value) => TimeSpan.FromMinutes((double)((int)value)));

			// Token: 0x04000113 RID: 275
			public static readonly Configuration.ConfigurationProperty<int> MaxThreadsForAllTimeBasedAssistants = new Configuration.ConfigurationProperty<int>("MaxThreadsForAllTimeBasedAssistants");

			// Token: 0x04000114 RID: 276
			public static readonly Configuration.ConfigurationProperty<int> MaxThreadsPerTimeBasedAssistantType = new Configuration.ConfigurationProperty<int>("MaxThreadsPerTimeBasedAssistantType");

			// Token: 0x04000115 RID: 277
			public static readonly Configuration.ConfigurationProperty<TimeSpan> HangDetectionTimeout = new Configuration.ConfigurationProperty<TimeSpan>("HangDetectionTimeout", (object value) => TimeSpan.FromMinutes((double)((int)value)));

			// Token: 0x04000116 RID: 278
			public static readonly Configuration.ConfigurationProperty<TimeSpan> HangDetectionPeriod = new Configuration.ConfigurationProperty<TimeSpan>("HangDetectionPeriod", (object value) => TimeSpan.FromMinutes((double)((int)value)));

			// Token: 0x04000117 RID: 279
			public static readonly Configuration.ConfigurationProperty<int> MaximumEventQueueSize = new Configuration.ConfigurationProperty<int>("MaximumEventQueueSize");

			// Token: 0x04000118 RID: 280
			public static readonly Configuration.ConfigurationProperty<int> MemoryBarrierNumberOfSamples = new Configuration.ConfigurationProperty<int>("MemoryBarrierNumberOfSamples");

			// Token: 0x04000119 RID: 281
			public static readonly Configuration.ConfigurationProperty<TimeSpan> MemoryBarrierSamplingInterval = new Configuration.ConfigurationProperty<TimeSpan>("MemoryBarrierSamplingInterval", (object value) => TimeSpan.FromSeconds((double)((int)value)));

			// Token: 0x0400011A RID: 282
			public static readonly Configuration.ConfigurationProperty<TimeSpan> MemoryBarrierSamplingDelay = new Configuration.ConfigurationProperty<TimeSpan>("MemoryBarrierSamplingDelay", (object value) => TimeSpan.FromSeconds((double)((int)value)));

			// Token: 0x0400011B RID: 283
			public static readonly Configuration.ConfigurationProperty<long> MemoryBarrierPrivateBytesUsageLimit = new Configuration.ConfigurationProperty<long>("MemoryBarrierPrivateBytesUsageLimit");

			// Token: 0x0400011C RID: 284
			public static readonly Configuration.ConfigurationProperty<TimeSpan> WorkCycleUpdatePeriod = new Configuration.ConfigurationProperty<TimeSpan>("WorkCycleUpdatePeriod", (object value) => TimeSpan.FromSeconds((double)((int)value)));
		}

		// Token: 0x02000026 RID: 38
		internal class ConfigurationProperty<PropertyType> where PropertyType : struct
		{
			// Token: 0x06000136 RID: 310 RVA: 0x0000632C File Offset: 0x0000452C
			public ConfigurationProperty(string name) : this(name, null)
			{
			}

			// Token: 0x06000137 RID: 311 RVA: 0x00006336 File Offset: 0x00004536
			public ConfigurationProperty(string name, Configuration.ConfigurationProperty<PropertyType>.ConvertionDelegate convertion)
			{
				this.name = name;
				this.value = null;
				this.convertion = convertion;
			}

			// Token: 0x1700006A RID: 106
			// (get) Token: 0x06000138 RID: 312 RVA: 0x00006354 File Offset: 0x00004554
			public PropertyType? Value
			{
				get
				{
					if (!this.initialized)
					{
						object obj = Configuration.ConfigurationProperty<PropertyType>.ReadValue(this.name);
						if (obj != null && this.convertion != null)
						{
							obj = this.convertion(obj);
						}
						this.value = obj;
						this.initialized = true;
					}
					return (PropertyType?)this.value;
				}
			}

			// Token: 0x06000139 RID: 313 RVA: 0x000063AC File Offset: 0x000045AC
			private static object ReadValue(string valueName)
			{
				object obj = null;
				Exception ex = null;
				try
				{
					obj = Registry.GetValue(Configuration.LocalMachineParametersRegistryKeyPath, valueName, null);
				}
				catch (SecurityException ex2)
				{
					ex = ex2;
				}
				catch (IOException ex3)
				{
					ex = ex3;
				}
				catch (ArgumentException ex4)
				{
					ex = ex4;
				}
				if (ex != null)
				{
					ExTraceGlobals.DatabaseManagerTracer.TraceDebug<string, Exception>(0L, "Configuration: Exception while reading property {0}: {1}", valueName, ex);
				}
				ExTraceGlobals.DatabaseManagerTracer.TraceDebug<string, object>(0L, "Configuration: {0} = {1}", valueName, obj);
				return obj;
			}

			// Token: 0x04000126 RID: 294
			private bool initialized;

			// Token: 0x04000127 RID: 295
			private string name;

			// Token: 0x04000128 RID: 296
			private object value;

			// Token: 0x04000129 RID: 297
			private Configuration.ConfigurationProperty<PropertyType>.ConvertionDelegate convertion;

			// Token: 0x02000027 RID: 39
			// (Invoke) Token: 0x0600013B RID: 315
			public delegate PropertyType ConvertionDelegate(object value);
		}
	}
}
