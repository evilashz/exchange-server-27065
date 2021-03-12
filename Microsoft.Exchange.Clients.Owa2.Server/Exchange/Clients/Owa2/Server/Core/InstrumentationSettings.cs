using System;
using System.Collections.Specialized;
using System.Configuration;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000336 RID: 822
	internal class InstrumentationSettings
	{
		// Token: 0x06001B23 RID: 6947 RVA: 0x00066FB2 File Offset: 0x000651B2
		public InstrumentationSettings()
		{
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x00066FBC File Offset: 0x000651BC
		public InstrumentationSettings(NameValueCollection settings)
		{
			float analyticsProbability;
			if (float.TryParse(settings["AnalyticsProbability"], out analyticsProbability))
			{
				this.AnalyticsProbability = analyticsProbability;
			}
			float coreAnalyticsProbability;
			if (float.TryParse(settings["CoreAnalyticsProbability"], out coreAnalyticsProbability))
			{
				this.CoreAnalyticsProbability = coreAnalyticsProbability;
			}
			bool isInferenceEnabled;
			if (bool.TryParse(settings["InferenceEnabled"], out isInferenceEnabled))
			{
				this.IsInferenceEnabled = isInferenceEnabled;
			}
			bool isConsoleTracingEnabled;
			if (bool.TryParse(settings["ConsoleTracingEnabled"], out isConsoleTracingEnabled))
			{
				this.IsConsoleTracingEnabled = isConsoleTracingEnabled;
			}
			TraceLevel defaultTraceLevel;
			if (Enum.TryParse<TraceLevel>(settings["DefaultTraceLevel"], out defaultTraceLevel))
			{
				this.DefaultTraceLevel = defaultTraceLevel;
			}
			JsMvvmPerfTraceLevel jsMvvmPerfTraceLevel;
			if (Enum.TryParse<JsMvvmPerfTraceLevel>(settings["DefaultPerfTraceLevel"], out jsMvvmPerfTraceLevel))
			{
				this.DefaultJsMvvmPerfTraceLevel = jsMvvmPerfTraceLevel;
				this.DefaultPerfTraceLevel = InstrumentationSettings.ConvertToOldPerfTraceLevel(jsMvvmPerfTraceLevel);
			}
			string value = settings["TraceInfoComponents"];
			if (!string.IsNullOrEmpty(value))
			{
				this.TraceInfoComponents = InstrumentationSettings.CommaSeperatedStringToArray(value);
			}
			string value2 = settings["TracePerfComponents"];
			if (!string.IsNullOrEmpty(value2))
			{
				this.TracePerfComponents = InstrumentationSettings.CommaSeperatedStringToArray(value2);
			}
			string value3 = settings["TraceVerboseComponents"];
			if (!string.IsNullOrEmpty(value3))
			{
				this.TraceVerboseComponents = InstrumentationSettings.CommaSeperatedStringToArray(value3);
			}
			string value4 = settings["TraceWarningComponents"];
			if (!string.IsNullOrEmpty(value4))
			{
				this.TraceWarningComponents = InstrumentationSettings.CommaSeperatedStringToArray(value4);
			}
			bool isClientWatsonEnabled;
			if (bool.TryParse(settings["ClientWatsonEnabled"], out isClientWatsonEnabled))
			{
				this.IsClientWatsonEnabled = isClientWatsonEnabled;
			}
			TimeSpan sendInterval;
			if (TimeSpan.TryParse(settings["SendInterval"], out sendInterval))
			{
				this.SendInterval = sendInterval;
			}
			bool isManualPerfTracerEnabled;
			if (bool.TryParse(settings["ManualPerfTracerEnabled"], out isManualPerfTracerEnabled))
			{
				this.IsManualPerfTracerEnabled = isManualPerfTracerEnabled;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001B25 RID: 6949 RVA: 0x00067160 File Offset: 0x00065360
		public static InstrumentationSettings Instance
		{
			get
			{
				if (InstrumentationSettings.instance == null)
				{
					InstrumentationSettings.instance = new InstrumentationSettings(ConfigurationManager.AppSettings);
				}
				return InstrumentationSettings.instance;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001B26 RID: 6950 RVA: 0x0006717D File Offset: 0x0006537D
		// (set) Token: 0x06001B27 RID: 6951 RVA: 0x00067185 File Offset: 0x00065385
		public float AnalyticsProbability { get; set; }

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001B28 RID: 6952 RVA: 0x0006718E File Offset: 0x0006538E
		// (set) Token: 0x06001B29 RID: 6953 RVA: 0x00067196 File Offset: 0x00065396
		public float CoreAnalyticsProbability { get; set; }

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001B2A RID: 6954 RVA: 0x0006719F File Offset: 0x0006539F
		// (set) Token: 0x06001B2B RID: 6955 RVA: 0x000671A7 File Offset: 0x000653A7
		public bool IsInferenceEnabled { get; set; }

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001B2C RID: 6956 RVA: 0x000671B0 File Offset: 0x000653B0
		// (set) Token: 0x06001B2D RID: 6957 RVA: 0x000671B8 File Offset: 0x000653B8
		public bool IsConsoleTracingEnabled { get; set; }

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001B2E RID: 6958 RVA: 0x000671C1 File Offset: 0x000653C1
		// (set) Token: 0x06001B2F RID: 6959 RVA: 0x000671C9 File Offset: 0x000653C9
		public TraceLevel DefaultTraceLevel { get; set; }

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001B30 RID: 6960 RVA: 0x000671D2 File Offset: 0x000653D2
		// (set) Token: 0x06001B31 RID: 6961 RVA: 0x000671DA File Offset: 0x000653DA
		public PerfTraceLevel DefaultPerfTraceLevel { get; set; }

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06001B32 RID: 6962 RVA: 0x000671E3 File Offset: 0x000653E3
		// (set) Token: 0x06001B33 RID: 6963 RVA: 0x000671EB File Offset: 0x000653EB
		public JsMvvmPerfTraceLevel DefaultJsMvvmPerfTraceLevel { get; set; }

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06001B34 RID: 6964 RVA: 0x000671F4 File Offset: 0x000653F4
		// (set) Token: 0x06001B35 RID: 6965 RVA: 0x000671FC File Offset: 0x000653FC
		public string[] TraceInfoComponents { get; set; }

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06001B36 RID: 6966 RVA: 0x00067205 File Offset: 0x00065405
		// (set) Token: 0x06001B37 RID: 6967 RVA: 0x0006720D File Offset: 0x0006540D
		public string[] TracePerfComponents { get; set; }

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06001B38 RID: 6968 RVA: 0x00067216 File Offset: 0x00065416
		// (set) Token: 0x06001B39 RID: 6969 RVA: 0x0006721E File Offset: 0x0006541E
		public string[] TraceVerboseComponents { get; set; }

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001B3A RID: 6970 RVA: 0x00067227 File Offset: 0x00065427
		// (set) Token: 0x06001B3B RID: 6971 RVA: 0x0006722F File Offset: 0x0006542F
		public string[] TraceWarningComponents { get; set; }

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001B3C RID: 6972 RVA: 0x00067238 File Offset: 0x00065438
		// (set) Token: 0x06001B3D RID: 6973 RVA: 0x00067240 File Offset: 0x00065440
		public bool IsClientWatsonEnabled { get; set; }

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001B3E RID: 6974 RVA: 0x00067249 File Offset: 0x00065449
		// (set) Token: 0x06001B3F RID: 6975 RVA: 0x00067251 File Offset: 0x00065451
		public TimeSpan SendInterval { get; set; }

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001B40 RID: 6976 RVA: 0x0006725A File Offset: 0x0006545A
		// (set) Token: 0x06001B41 RID: 6977 RVA: 0x00067262 File Offset: 0x00065462
		public bool IsManualPerfTracerEnabled { get; set; }

		// Token: 0x06001B42 RID: 6978 RVA: 0x0006726C File Offset: 0x0006546C
		public bool IsInstrumentationEnabled()
		{
			return this.DefaultTraceLevel != TraceLevel.Off || this.DefaultPerfTraceLevel != PerfTraceLevel.Off || this.IsInferenceEnabled || this.IsClientWatsonEnabled || this.CoreAnalyticsProbability > 0f || this.AnalyticsProbability > 0f;
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x000672B8 File Offset: 0x000654B8
		internal static PerfTraceLevel ConvertToOldPerfTraceLevel(JsMvvmPerfTraceLevel perfLevel)
		{
			switch (perfLevel)
			{
			case JsMvvmPerfTraceLevel.Essential:
				return PerfTraceLevel.Execution;
			case JsMvvmPerfTraceLevel.Info:
				return PerfTraceLevel.Detailed;
			case JsMvvmPerfTraceLevel.Verbose:
				return PerfTraceLevel.Component;
			case JsMvvmPerfTraceLevel.Debug:
				return PerfTraceLevel.Logging;
			default:
				return PerfTraceLevel.Off;
			}
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x000672F8 File Offset: 0x000654F8
		private static string[] CommaSeperatedStringToArray(string value)
		{
			return Array.FindAll<string>(value.Replace(" ", string.Empty).Split(new char[]
			{
				','
			}), (string component) => !string.IsNullOrEmpty(component));
		}

		// Token: 0x04000F1A RID: 3866
		public const string AnalyticsProbabilityKey = "AnalyticsProbability";

		// Token: 0x04000F1B RID: 3867
		public const string CoreAnalyticsProbabilityKey = "CoreAnalyticsProbability";

		// Token: 0x04000F1C RID: 3868
		public const string InferenceEnabledKey = "InferenceEnabled";

		// Token: 0x04000F1D RID: 3869
		public const string ConsoleTracingEnabledKey = "ConsoleTracingEnabled";

		// Token: 0x04000F1E RID: 3870
		public const string DefaultTraceLevelKey = "DefaultTraceLevel";

		// Token: 0x04000F1F RID: 3871
		public const string DefaultPerfTraceLevelKey = "DefaultPerfTraceLevel";

		// Token: 0x04000F20 RID: 3872
		public const string TraceInfoComponentsKey = "TraceInfoComponents";

		// Token: 0x04000F21 RID: 3873
		public const string TracePerfComponentsKey = "TracePerfComponents";

		// Token: 0x04000F22 RID: 3874
		public const string TraceVerboseComponentsKey = "TraceVerboseComponents";

		// Token: 0x04000F23 RID: 3875
		public const string TraceWarningComponentsKey = "TraceWarningComponents";

		// Token: 0x04000F24 RID: 3876
		public const string ClientWatsonEnabledKey = "ClientWatsonEnabled";

		// Token: 0x04000F25 RID: 3877
		public const string SendIntervalKey = "SendInterval";

		// Token: 0x04000F26 RID: 3878
		public const string ManualPerfTracerEnabled = "ManualPerfTracerEnabled";

		// Token: 0x04000F27 RID: 3879
		private static InstrumentationSettings instance;
	}
}
