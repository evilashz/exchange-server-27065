using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000091 RID: 145
	internal sealed class ExTraceConfiguration
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600032D RID: 813 RVA: 0x0000B374 File Offset: 0x00009574
		// (remove) Token: 0x0600032E RID: 814 RVA: 0x0000B3AC File Offset: 0x000095AC
		public event Action OnConfigurationChange;

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000B3E1 File Offset: 0x000095E1
		public static ExTraceConfiguration Instance
		{
			get
			{
				return ExTraceConfiguration.instanceConfig;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000B3E8 File Offset: 0x000095E8
		// (set) Token: 0x06000331 RID: 817 RVA: 0x0000B3EF File Offset: 0x000095EF
		public static HashSet<uint> DisabledLids
		{
			get
			{
				return ExTraceConfiguration.disabledLids;
			}
			set
			{
				ExTraceConfiguration.disabledLids = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000B3F7 File Offset: 0x000095F7
		// (set) Token: 0x06000333 RID: 819 RVA: 0x0000B3FE File Offset: 0x000095FE
		public static HashSet<Guid> DisableAllTraces
		{
			get
			{
				return ExTraceConfiguration.disableAllTraces;
			}
			set
			{
				ExTraceConfiguration.disableAllTraces = value;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000B406 File Offset: 0x00009606
		public int Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000B410 File Offset: 0x00009610
		internal bool PerThreadTracingConfigured
		{
			get
			{
				return this.perThreadTracingConfigured;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000B418 File Offset: 0x00009618
		internal bool InMemoryTracingEnabled
		{
			get
			{
				return this.inMemoryTracingEnabled;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000B420 File Offset: 0x00009620
		internal bool ConsoleTracingEnabled
		{
			get
			{
				return this.consoleTracingEnabled;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0000B428 File Offset: 0x00009628
		internal bool SystemDiagnosticsTracingEnabled
		{
			get
			{
				return this.systemDiagnosticsTracingEnabled;
			}
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000B430 File Offset: 0x00009630
		public bool IsEnabled(TraceType value)
		{
			return this.typeFlags[(int)value];
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000B43E File Offset: 0x0000963E
		public BitArray EnabledTypesArray()
		{
			return this.typeFlags;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000B446 File Offset: 0x00009646
		public BitArray EnabledTagArray(Guid componentGuid)
		{
			return this.componentDictionary[componentGuid];
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000B454 File Offset: 0x00009654
		public BitArray EnabledInMemoryTagArray(Guid componentGuid)
		{
			return this.inMemoryComponentDictionary[componentGuid];
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000B462 File Offset: 0x00009662
		public BitArray PerThreadModeTagArray(Guid componentGuid)
		{
			return this.perThreadModeComponentDictionary[componentGuid];
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0000B470 File Offset: 0x00009670
		public Dictionary<string, List<string>> CustomParameters
		{
			get
			{
				return this.customParameters;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000B478 File Offset: 0x00009678
		internal FaultInjectionConfig FaultInjectionConfiguration
		{
			get
			{
				return this.faultInjectionConfiguration;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000B480 File Offset: 0x00009680
		internal ExceptionInjection ExceptionInjection
		{
			get
			{
				return this.exceptionInjection;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000B488 File Offset: 0x00009688
		// (set) Token: 0x06000342 RID: 834 RVA: 0x0000B490 File Offset: 0x00009690
		internal ComponentInjectionCallback ComponentInjection
		{
			get
			{
				return this.componentInjection;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentException("Component identifier is null.");
				}
				lock (this.locker)
				{
					this.componentInjection = value;
				}
			}
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000B4E0 File Offset: 0x000096E0
		private static Dictionary<Guid, BitArray> DeepCopyComponentDictionary()
		{
			Dictionary<Guid, BitArray> dictionary = new Dictionary<Guid, BitArray>(ComponentDictionary.InnerDictionary);
			foreach (Guid key in ComponentDictionary.InnerDictionary.Keys)
			{
				dictionary[key] = new BitArray(dictionary[key].Length);
			}
			return dictionary;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000B55C File Offset: 0x0000975C
		private ExTraceConfiguration()
		{
			ConfigFiles.Trace.FileHandler.Changed += this.TraceConfigFileChangeHandler;
			ConfigFiles.InMemory.FileHandler.Changed += this.InMemoryTraceConfigFileChangeHandler;
			ConfigFiles.FaultInjection.FileHandler.Changed += this.FaultInjectionConfigFileChangeHandler;
			lock (this.locker)
			{
				this.TraceConfigUpdate();
				this.InMemoryTraceConfigUpdate();
				this.FaultInjectionConfigUpdate();
				TraceException.Setup();
			}
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000B67C File Offset: 0x0000987C
		public void EnableInMemoryTracing(Guid componentGuid, bool enable)
		{
			lock (this.locker)
			{
				BitArray bitArray;
				if (!this.inMemoryComponentDictionary.TryGetValue(componentGuid, out bitArray))
				{
					throw new ArgumentException("Component " + componentGuid + " does not exist");
				}
				bitArray.SetAll(enable);
				this.typeFlags.SetAll(enable);
				this.typeFlags[1] = false;
				this.typeFlags[7] = false;
				this.inMemoryTracingEnabled = true;
				this.version++;
			}
			this.InvokeOnConfigurationChange();
			InternalBypassTrace.TracingConfigurationTracer.TraceDebug(0, (long)this.GetHashCode(), "In Memory tracing {0} for all tags in component {1}.", new object[]
			{
				this.inMemoryTracingEnabled ? "enabled" : "disabled",
				componentGuid
			});
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000B76C File Offset: 0x0000996C
		private void TraceConfigFileChangeHandler()
		{
			lock (this.locker)
			{
				this.TraceConfigUpdate();
			}
			this.InvokeOnConfigurationChange();
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000B7B4 File Offset: 0x000099B4
		private void InMemoryTraceConfigFileChangeHandler()
		{
			lock (this.locker)
			{
				this.InMemoryTraceConfigUpdate();
			}
			this.InvokeOnConfigurationChange();
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000B7FC File Offset: 0x000099FC
		private void FaultInjectionConfigFileChangeHandler()
		{
			lock (this.locker)
			{
				this.FaultInjectionConfigUpdate();
			}
			this.InvokeOnConfigurationChange();
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000B844 File Offset: 0x00009A44
		private void InvokeOnConfigurationChange()
		{
			Action onConfigurationChange = this.OnConfigurationChange;
			if (onConfigurationChange != null)
			{
				onConfigurationChange();
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000B864 File Offset: 0x00009A64
		private void TraceConfigUpdate()
		{
			ConfigurationDocument configurationDocument = ConfigurationDocument.LoadFromFile(ConfigFiles.Trace.ConfigFilePath);
			this.UpdateTrace(configurationDocument);
			InternalBypassTrace.TracingConfigurationTracer.TraceDebug(39471, 0L, "New tracing configuration took effect", new object[0]);
			TraceConfigSync.Signal(configurationDocument.FileContentHash, this.ComponentInjection(), InternalBypassTrace.TracingConfigurationTracer);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000B8C0 File Offset: 0x00009AC0
		private void InMemoryTraceConfigUpdate()
		{
			ConfigurationDocument configurationDocument = ConfigurationDocument.LoadFromFile(ConfigFiles.InMemory.ConfigFilePath);
			this.UpdateInMemory(configurationDocument);
			InternalBypassTrace.TracingConfigurationTracer.TraceDebug(49839, 0L, "New in-memory tracing configuration took effect", new object[0]);
			TraceConfigSync.Signal(configurationDocument.FileContentHash, this.ComponentInjection(), InternalBypassTrace.TracingConfigurationTracer);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000B91C File Offset: 0x00009B1C
		private void FaultInjectionConfigUpdate()
		{
			ConfigurationDocument configurationDocument = ConfigurationDocument.LoadFaultInjectionFromFile(ConfigFiles.FaultInjection.ConfigFilePath);
			this.faultInjectionConfiguration = configurationDocument.FaultInjectionConfig;
			InternalBypassTrace.FaultInjectionConfigurationTracer.TraceDebug(64047, 0L, "New FI configuration took effect", new object[0]);
			TraceConfigSync.Signal(configurationDocument.FileContentHash, this.ComponentInjection(), InternalBypassTrace.FaultInjectionConfigurationTracer);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000B97C File Offset: 0x00009B7C
		private void UpdateTrace(ConfigurationDocument traceConfigDoc)
		{
			List<TraceComponentInfo> enabledComponentsFromFile;
			List<TraceComponentInfo> enabledComponentsFromFile2;
			if (traceConfigDoc.FilteredTracingEnabled)
			{
				enabledComponentsFromFile = traceConfigDoc.BypassFilterEnabledComponentsList;
				enabledComponentsFromFile2 = traceConfigDoc.EnabledComponentsList;
			}
			else
			{
				enabledComponentsFromFile = traceConfigDoc.EnabledComponentsList;
				enabledComponentsFromFile2 = new List<TraceComponentInfo>();
			}
			this.UpdateComponentsState(enabledComponentsFromFile, this.componentDictionary);
			this.UpdateComponentsState(enabledComponentsFromFile2, this.perThreadModeComponentDictionary);
			traceConfigDoc.GetEnabledTypes(this.typeFlags, false);
			this.perThreadTracingConfigured = traceConfigDoc.FilteredTracingEnabled;
			this.customParameters = traceConfigDoc.CustomParameters;
			this.consoleTracingEnabled = traceConfigDoc.ConsoleTracingEnabled;
			this.systemDiagnosticsTracingEnabled = traceConfigDoc.SystemDiagnosticsTracingEnabled;
			bool anyExchangeTracingProvidersEnabled = ETWTrace.IsEnabled || this.InMemoryTracingEnabled || this.ConsoleTracingEnabled || this.SystemDiagnosticsTracingEnabled || this.FaultInjectionConfiguration.Count > 0;
			SystemTraceControl.Update(this.componentDictionary, this.EnabledTypesArray(), anyExchangeTracingProvidersEnabled);
			this.version++;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000BA60 File Offset: 0x00009C60
		private void UpdateInMemory(ConfigurationDocument inMemoryTraceConfigDoc)
		{
			List<TraceComponentInfo> list;
			if (inMemoryTraceConfigDoc.FilteredTracingEnabled)
			{
				list = inMemoryTraceConfigDoc.BypassFilterEnabledComponentsList;
			}
			else
			{
				list = inMemoryTraceConfigDoc.EnabledComponentsList;
			}
			this.UpdateComponentsState(list, this.inMemoryComponentDictionary);
			inMemoryTraceConfigDoc.GetEnabledTypes(this.typeFlags, true);
			if (list != null && list.Count > 0)
			{
				this.inMemoryTracingEnabled = true;
			}
			else
			{
				this.inMemoryTracingEnabled = false;
			}
			InternalBypassTrace.TracingConfigurationTracer.TraceDebug(0, (long)this.GetHashCode(), "In Memory tracing is {0}", new object[]
			{
				this.inMemoryTracingEnabled ? "enabled" : "disabled"
			});
			this.version++;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000BB08 File Offset: 0x00009D08
		private void UpdateComponentsState(List<TraceComponentInfo> enabledComponentsFromFile, Dictionary<Guid, BitArray> componentConfigurationInMemory)
		{
			HashSet<Guid> hashSet = new HashSet<Guid>();
			foreach (TraceComponentInfo traceComponentInfo in enabledComponentsFromFile)
			{
				BitArray bitArray = componentConfigurationInMemory[traceComponentInfo.ComponentGuid];
				BitArray bitArray2 = new BitArray(bitArray.Length);
				bitArray2.SetAll(false);
				TraceTagInfo[] tagInfoList = traceComponentInfo.TagInfoList;
				foreach (TraceTagInfo traceTagInfo in tagInfoList)
				{
					bitArray2[traceTagInfo.NumericValue] = true;
				}
				for (int j = 0; j < bitArray2.Length; j++)
				{
					bitArray[j] = bitArray2[j];
				}
				hashSet.Add(traceComponentInfo.ComponentGuid);
			}
			foreach (Guid guid in this.componentDictionary.Keys)
			{
				if (!hashSet.Contains(guid))
				{
					BitArray bitArray3 = componentConfigurationInMemory[guid];
					bitArray3.SetAll(false);
				}
			}
		}

		// Token: 0x040002F2 RID: 754
		private static readonly ExTraceConfiguration instanceConfig = new ExTraceConfiguration();

		// Token: 0x040002F3 RID: 755
		[ThreadStatic]
		private static HashSet<uint> disabledLids;

		// Token: 0x040002F4 RID: 756
		[ThreadStatic]
		private static HashSet<Guid> disableAllTraces;

		// Token: 0x040002F5 RID: 757
		private readonly BitArray typeFlags = new BitArray(ConfigurationDocument.TraceTypesCount + 1);

		// Token: 0x040002F6 RID: 758
		private readonly Dictionary<Guid, BitArray> componentDictionary = ComponentDictionary.InnerDictionary;

		// Token: 0x040002F7 RID: 759
		private readonly Dictionary<Guid, BitArray> inMemoryComponentDictionary = ExTraceConfiguration.DeepCopyComponentDictionary();

		// Token: 0x040002F8 RID: 760
		private bool perThreadTracingConfigured;

		// Token: 0x040002F9 RID: 761
		private bool inMemoryTracingEnabled;

		// Token: 0x040002FA RID: 762
		private bool consoleTracingEnabled;

		// Token: 0x040002FB RID: 763
		private bool systemDiagnosticsTracingEnabled;

		// Token: 0x040002FC RID: 764
		private Dictionary<string, List<string>> customParameters;

		// Token: 0x040002FD RID: 765
		private Dictionary<Guid, BitArray> perThreadModeComponentDictionary = ExTraceConfiguration.DeepCopyComponentDictionary();

		// Token: 0x040002FE RID: 766
		private FaultInjectionConfig faultInjectionConfiguration = new FaultInjectionConfig();

		// Token: 0x040002FF RID: 767
		private ExceptionInjection exceptionInjection = new ExceptionInjection();

		// Token: 0x04000300 RID: 768
		private ComponentInjectionCallback componentInjection = () => string.Empty;

		// Token: 0x04000301 RID: 769
		private volatile int version;

		// Token: 0x04000302 RID: 770
		private object locker = new object();
	}
}
