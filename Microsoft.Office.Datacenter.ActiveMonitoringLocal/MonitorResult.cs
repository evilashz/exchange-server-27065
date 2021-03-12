using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x0200000C RID: 12
	[Table]
	public sealed class MonitorResult : WorkItemResult, IPersistence, IWorkItemResultSerialization
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00004ED4 File Offset: 0x000030D4
		public MonitorResult(WorkDefinition definition) : base(definition)
		{
			this.LastFailedProbeId = -1;
			this.LastFailedProbeResultId = -1;
			this.HealthStateTransitionId = -1;
			this.Component = ((MonitorDefinition)definition).Component;
			this.IsHaImpacting = ((MonitorDefinition)definition).IsHaImpacting;
			this.SourceScope = ((MonitorDefinition)definition).SourceScope;
			this.TargetScopes = ((MonitorDefinition)definition).TargetScopes;
			this.HaScope = ((MonitorDefinition)definition).HaScope;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00004F52 File Offset: 0x00003152
		public MonitorResult()
		{
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00004F5A File Offset: 0x0000315A
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00004F62 File Offset: 0x00003162
		[Column(IsPrimaryKey = true, IsDbGenerated = true)]
		public override int ResultId { get; protected internal set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00004F6B File Offset: 0x0000316B
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00004F73 File Offset: 0x00003173
		[Column]
		public override string ServiceName { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00004F7C File Offset: 0x0000317C
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00004F84 File Offset: 0x00003184
		[Column]
		public override bool IsNotified { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00004F8D File Offset: 0x0000318D
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00004F95 File Offset: 0x00003195
		[Column]
		public override string ResultName { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00004F9E File Offset: 0x0000319E
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00004FA6 File Offset: 0x000031A6
		[Column]
		public override int WorkItemId { get; internal set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00004FAF File Offset: 0x000031AF
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00004FB7 File Offset: 0x000031B7
		[Column]
		public override int DeploymentId { get; internal set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00004FC0 File Offset: 0x000031C0
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00004FC8 File Offset: 0x000031C8
		[Column]
		public override string MachineName { get; internal set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00004FD1 File Offset: 0x000031D1
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00004FD9 File Offset: 0x000031D9
		[Column]
		public override string Error { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00004FE2 File Offset: 0x000031E2
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00004FEA File Offset: 0x000031EA
		[Column]
		public override string Exception { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00004FF3 File Offset: 0x000031F3
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00004FFB File Offset: 0x000031FB
		[Column]
		public override byte RetryCount { get; internal set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00005004 File Offset: 0x00003204
		// (set) Token: 0x06000043 RID: 67 RVA: 0x0000500C File Offset: 0x0000320C
		[Column]
		public override string StateAttribute1 { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00005015 File Offset: 0x00003215
		// (set) Token: 0x06000045 RID: 69 RVA: 0x0000501D File Offset: 0x0000321D
		[Column]
		public override string StateAttribute2 { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00005026 File Offset: 0x00003226
		// (set) Token: 0x06000047 RID: 71 RVA: 0x0000502E File Offset: 0x0000322E
		[Column]
		public override string StateAttribute3 { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00005037 File Offset: 0x00003237
		// (set) Token: 0x06000049 RID: 73 RVA: 0x0000503F File Offset: 0x0000323F
		[Column]
		public override string StateAttribute4 { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00005048 File Offset: 0x00003248
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00005050 File Offset: 0x00003250
		[Column]
		public override string StateAttribute5 { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00005059 File Offset: 0x00003259
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00005061 File Offset: 0x00003261
		[Column]
		public override double StateAttribute6 { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000506A File Offset: 0x0000326A
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00005072 File Offset: 0x00003272
		[Column]
		public override double StateAttribute7 { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000050 RID: 80 RVA: 0x0000507B File Offset: 0x0000327B
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00005083 File Offset: 0x00003283
		[Column]
		public override double StateAttribute8 { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000052 RID: 82 RVA: 0x0000508C File Offset: 0x0000328C
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00005094 File Offset: 0x00003294
		[Column]
		public override double StateAttribute9 { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000054 RID: 84 RVA: 0x0000509D File Offset: 0x0000329D
		// (set) Token: 0x06000055 RID: 85 RVA: 0x000050A5 File Offset: 0x000032A5
		[Column]
		public override double StateAttribute10 { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000050AE File Offset: 0x000032AE
		// (set) Token: 0x06000057 RID: 87 RVA: 0x000050B6 File Offset: 0x000032B6
		[Column]
		public override ResultType ResultType { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000050BF File Offset: 0x000032BF
		// (set) Token: 0x06000059 RID: 89 RVA: 0x000050C7 File Offset: 0x000032C7
		[Column]
		public override int ExecutionId { get; protected set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000050D0 File Offset: 0x000032D0
		// (set) Token: 0x0600005B RID: 91 RVA: 0x000050D8 File Offset: 0x000032D8
		[Column]
		public override DateTime ExecutionStartTime { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000050E1 File Offset: 0x000032E1
		// (set) Token: 0x0600005D RID: 93 RVA: 0x000050E9 File Offset: 0x000032E9
		[Column]
		public override DateTime ExecutionEndTime { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000050F2 File Offset: 0x000032F2
		// (set) Token: 0x0600005F RID: 95 RVA: 0x000050FA File Offset: 0x000032FA
		[Column]
		public override byte PoisonedCount { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00005103 File Offset: 0x00003303
		// (set) Token: 0x06000061 RID: 97 RVA: 0x0000510B File Offset: 0x0000330B
		[Column]
		public bool IsAlert { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00005114 File Offset: 0x00003314
		// (set) Token: 0x06000063 RID: 99 RVA: 0x0000511C File Offset: 0x0000331C
		[Column]
		public double TotalValue { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00005125 File Offset: 0x00003325
		// (set) Token: 0x06000065 RID: 101 RVA: 0x0000512D File Offset: 0x0000332D
		[Column]
		public int TotalSampleCount { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00005136 File Offset: 0x00003336
		// (set) Token: 0x06000067 RID: 103 RVA: 0x0000513E File Offset: 0x0000333E
		[Column]
		public int TotalFailedCount { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00005147 File Offset: 0x00003347
		// (set) Token: 0x06000069 RID: 105 RVA: 0x0000514F File Offset: 0x0000334F
		[Column]
		public double NewValue { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00005158 File Offset: 0x00003358
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00005160 File Offset: 0x00003360
		[Column]
		public int NewSampleCount { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00005169 File Offset: 0x00003369
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00005171 File Offset: 0x00003371
		[Column]
		public int NewFailedCount { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600006E RID: 110 RVA: 0x0000517A File Offset: 0x0000337A
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00005182 File Offset: 0x00003382
		[Column]
		public int LastFailedProbeId { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000070 RID: 112 RVA: 0x0000518B File Offset: 0x0000338B
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00005193 File Offset: 0x00003393
		[Column]
		public int LastFailedProbeResultId { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000072 RID: 114 RVA: 0x0000519C File Offset: 0x0000339C
		// (set) Token: 0x06000073 RID: 115 RVA: 0x000051A4 File Offset: 0x000033A4
		[Column]
		public ServiceHealthStatus HealthState { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000051AD File Offset: 0x000033AD
		// (set) Token: 0x06000075 RID: 117 RVA: 0x000051B5 File Offset: 0x000033B5
		[Column]
		public int HealthStateTransitionId { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000051BE File Offset: 0x000033BE
		// (set) Token: 0x06000077 RID: 119 RVA: 0x000051C6 File Offset: 0x000033C6
		[Column]
		public DateTime? HealthStateChangedTime { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000051CF File Offset: 0x000033CF
		// (set) Token: 0x06000079 RID: 121 RVA: 0x000051D7 File Offset: 0x000033D7
		[Column]
		public DateTime? FirstAlertObservedTime { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000051E0 File Offset: 0x000033E0
		// (set) Token: 0x0600007B RID: 123 RVA: 0x000051E8 File Offset: 0x000033E8
		[Column]
		public DateTime? FirstInsufficientSamplesObservedTime { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600007C RID: 124 RVA: 0x000051F1 File Offset: 0x000033F1
		// (set) Token: 0x0600007D RID: 125 RVA: 0x000051F9 File Offset: 0x000033F9
		public Component Component { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00005202 File Offset: 0x00003402
		// (set) Token: 0x0600007F RID: 127 RVA: 0x0000520A File Offset: 0x0000340A
		[Column]
		public string NewStateAttribute1Value { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00005213 File Offset: 0x00003413
		// (set) Token: 0x06000081 RID: 129 RVA: 0x0000521B File Offset: 0x0000341B
		[Column]
		public int NewStateAttribute1Count { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00005224 File Offset: 0x00003424
		// (set) Token: 0x06000083 RID: 131 RVA: 0x0000522C File Offset: 0x0000342C
		[Column]
		public double NewStateAttribute1Percent { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00005235 File Offset: 0x00003435
		// (set) Token: 0x06000085 RID: 133 RVA: 0x0000523D File Offset: 0x0000343D
		[Column]
		public string TotalStateAttribute1Value { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00005246 File Offset: 0x00003446
		// (set) Token: 0x06000087 RID: 135 RVA: 0x0000524E File Offset: 0x0000344E
		[Column]
		public int TotalStateAttribute1Count { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00005257 File Offset: 0x00003457
		// (set) Token: 0x06000089 RID: 137 RVA: 0x0000525F File Offset: 0x0000345F
		[Column]
		public double TotalStateAttribute1Percent { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00005268 File Offset: 0x00003468
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00005270 File Offset: 0x00003470
		[Column]
		public int NewFailureCategoryValue { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00005279 File Offset: 0x00003479
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00005281 File Offset: 0x00003481
		[Column]
		public int NewFailureCategoryCount { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600008E RID: 142 RVA: 0x0000528A File Offset: 0x0000348A
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00005292 File Offset: 0x00003492
		[Column]
		public double NewFailureCategoryPercent { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000090 RID: 144 RVA: 0x0000529B File Offset: 0x0000349B
		// (set) Token: 0x06000091 RID: 145 RVA: 0x000052A3 File Offset: 0x000034A3
		[Column]
		public int TotalFailureCategoryValue { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000052AC File Offset: 0x000034AC
		// (set) Token: 0x06000093 RID: 147 RVA: 0x000052B4 File Offset: 0x000034B4
		[Column]
		public int TotalFailureCategoryCount { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000094 RID: 148 RVA: 0x000052BD File Offset: 0x000034BD
		// (set) Token: 0x06000095 RID: 149 RVA: 0x000052C5 File Offset: 0x000034C5
		[Column]
		public double TotalFailureCategoryPercent { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000052CE File Offset: 0x000034CE
		// (set) Token: 0x06000097 RID: 151 RVA: 0x000052EF File Offset: 0x000034EF
		[Column]
		internal string ComponentName
		{
			get
			{
				if (!(this.Component != null))
				{
					return string.Empty;
				}
				return this.Component.ToString();
			}
			set
			{
				this.Component = new Component(value);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000052FD File Offset: 0x000034FD
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00005305 File Offset: 0x00003505
		[Column]
		internal bool IsHaImpacting { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600009A RID: 154 RVA: 0x0000530E File Offset: 0x0000350E
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00005316 File Offset: 0x00003516
		[Column]
		public string SourceScope { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600009C RID: 156 RVA: 0x0000531F File Offset: 0x0000351F
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00005327 File Offset: 0x00003527
		[Column]
		public string TargetScopes { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00005330 File Offset: 0x00003530
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00005338 File Offset: 0x00003538
		[Column]
		public string HaScope { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00005341 File Offset: 0x00003541
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00005349 File Offset: 0x00003549
		[Column]
		internal override int Version { get; set; }

		// Token: 0x060000A2 RID: 162 RVA: 0x00005352 File Offset: 0x00003552
		public HaScopeEnum GetHaScope()
		{
			return MonitorDefinition.HaScopeToEnum(this.HaScope);
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x0000535F File Offset: 0x0000355F
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00005367 File Offset: 0x00003567
		public LocalDataAccessMetaData LocalDataAccessMetaData { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00005370 File Offset: 0x00003570
		internal static int SchemaVersion
		{
			get
			{
				return MonitorResult.schemaVersion;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00005377 File Offset: 0x00003577
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x0000537E File Offset: 0x0000357E
		internal static Action<MonitorResult> ResultWriter { private get; set; }

		// Token: 0x060000A8 RID: 168 RVA: 0x00005386 File Offset: 0x00003586
		public void Initialize(Dictionary<string, string> propertyBag, LocalDataAccessMetaData metaData)
		{
			this.LocalDataAccessMetaData = metaData;
			this.SetProperties(propertyBag);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00005398 File Offset: 0x00003598
		public void SetProperties(Dictionary<string, string> propertyBag)
		{
			string text;
			if (propertyBag.TryGetValue("ResultId", out text) && !string.IsNullOrEmpty(text))
			{
				this.ResultId = int.Parse(text);
			}
			if (propertyBag.TryGetValue("ServiceName", out text))
			{
				this.ServiceName = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("IsNotified", out text) && !string.IsNullOrEmpty(text))
			{
				this.IsNotified = CrimsonHelper.ParseIntStringAsBool(text);
			}
			if (propertyBag.TryGetValue("ResultName", out text))
			{
				this.ResultName = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("WorkItemId", out text) && !string.IsNullOrEmpty(text))
			{
				this.WorkItemId = int.Parse(text);
			}
			if (propertyBag.TryGetValue("DeploymentId", out text) && !string.IsNullOrEmpty(text))
			{
				this.DeploymentId = int.Parse(text);
			}
			if (propertyBag.TryGetValue("MachineName", out text))
			{
				this.MachineName = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("Error", out text))
			{
				this.Error = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("Exception", out text))
			{
				this.Exception = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("RetryCount", out text) && !string.IsNullOrEmpty(text))
			{
				this.RetryCount = byte.Parse(text);
			}
			if (propertyBag.TryGetValue("StateAttribute1", out text))
			{
				this.StateAttribute1 = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("StateAttribute2", out text))
			{
				this.StateAttribute2 = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("StateAttribute3", out text))
			{
				this.StateAttribute3 = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("StateAttribute4", out text))
			{
				this.StateAttribute4 = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("StateAttribute5", out text))
			{
				this.StateAttribute5 = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("StateAttribute6", out text) && !string.IsNullOrEmpty(text))
			{
				this.StateAttribute6 = CrimsonHelper.ParseDouble(text);
			}
			if (propertyBag.TryGetValue("StateAttribute7", out text) && !string.IsNullOrEmpty(text))
			{
				this.StateAttribute7 = CrimsonHelper.ParseDouble(text);
			}
			if (propertyBag.TryGetValue("StateAttribute8", out text) && !string.IsNullOrEmpty(text))
			{
				this.StateAttribute8 = CrimsonHelper.ParseDouble(text);
			}
			if (propertyBag.TryGetValue("StateAttribute9", out text) && !string.IsNullOrEmpty(text))
			{
				this.StateAttribute9 = CrimsonHelper.ParseDouble(text);
			}
			if (propertyBag.TryGetValue("StateAttribute10", out text) && !string.IsNullOrEmpty(text))
			{
				this.StateAttribute10 = CrimsonHelper.ParseDouble(text);
			}
			if (propertyBag.TryGetValue("ResultType", out text) && !string.IsNullOrEmpty(text))
			{
				this.ResultType = (ResultType)Enum.Parse(typeof(ResultType), text);
			}
			if (propertyBag.TryGetValue("ExecutionId", out text) && !string.IsNullOrEmpty(text))
			{
				this.ExecutionId = int.Parse(text);
			}
			if (propertyBag.TryGetValue("ExecutionStartTime", out text) && !string.IsNullOrEmpty(text))
			{
				this.ExecutionStartTime = DateTime.Parse(text).ToUniversalTime();
			}
			if (propertyBag.TryGetValue("ExecutionEndTime", out text) && !string.IsNullOrEmpty(text))
			{
				this.ExecutionEndTime = DateTime.Parse(text).ToUniversalTime();
			}
			if (propertyBag.TryGetValue("PoisonedCount", out text) && !string.IsNullOrEmpty(text))
			{
				this.PoisonedCount = byte.Parse(text);
			}
			if (propertyBag.TryGetValue("IsAlert", out text) && !string.IsNullOrEmpty(text))
			{
				this.IsAlert = CrimsonHelper.ParseIntStringAsBool(text);
			}
			if (propertyBag.TryGetValue("TotalValue", out text) && !string.IsNullOrEmpty(text))
			{
				this.TotalValue = CrimsonHelper.ParseDouble(text);
			}
			if (propertyBag.TryGetValue("TotalSampleCount", out text) && !string.IsNullOrEmpty(text))
			{
				this.TotalSampleCount = int.Parse(text);
			}
			if (propertyBag.TryGetValue("TotalFailedCount", out text) && !string.IsNullOrEmpty(text))
			{
				this.TotalFailedCount = int.Parse(text);
			}
			if (propertyBag.TryGetValue("NewValue", out text) && !string.IsNullOrEmpty(text))
			{
				this.NewValue = CrimsonHelper.ParseDouble(text);
			}
			if (propertyBag.TryGetValue("NewSampleCount", out text) && !string.IsNullOrEmpty(text))
			{
				this.NewSampleCount = int.Parse(text);
			}
			if (propertyBag.TryGetValue("NewFailedCount", out text) && !string.IsNullOrEmpty(text))
			{
				this.NewFailedCount = int.Parse(text);
			}
			if (propertyBag.TryGetValue("LastFailedProbeId", out text) && !string.IsNullOrEmpty(text))
			{
				this.LastFailedProbeId = int.Parse(text);
			}
			if (propertyBag.TryGetValue("LastFailedProbeResultId", out text) && !string.IsNullOrEmpty(text))
			{
				this.LastFailedProbeResultId = int.Parse(text);
			}
			if (propertyBag.TryGetValue("HealthState", out text) && !string.IsNullOrEmpty(text))
			{
				this.HealthState = (ServiceHealthStatus)Enum.Parse(typeof(ServiceHealthStatus), text);
			}
			if (propertyBag.TryGetValue("HealthStateTransitionId", out text) && !string.IsNullOrEmpty(text))
			{
				this.HealthStateTransitionId = int.Parse(text);
			}
			if (propertyBag.TryGetValue("HealthStateChangedTime", out text) && !string.IsNullOrEmpty(text))
			{
				this.HealthStateChangedTime = (string.Equals(text, "[null]", StringComparison.OrdinalIgnoreCase) ? null : new DateTime?(DateTime.Parse(text).ToUniversalTime()));
			}
			if (propertyBag.TryGetValue("FirstAlertObservedTime", out text) && !string.IsNullOrEmpty(text))
			{
				this.FirstAlertObservedTime = (string.Equals(text, "[null]", StringComparison.OrdinalIgnoreCase) ? null : new DateTime?(DateTime.Parse(text).ToUniversalTime()));
			}
			if (propertyBag.TryGetValue("FirstInsufficientSamplesObservedTime", out text) && !string.IsNullOrEmpty(text))
			{
				this.FirstInsufficientSamplesObservedTime = (string.Equals(text, "[null]", StringComparison.OrdinalIgnoreCase) ? null : new DateTime?(DateTime.Parse(text).ToUniversalTime()));
			}
			if (propertyBag.TryGetValue("NewStateAttribute1Value", out text))
			{
				this.NewStateAttribute1Value = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("NewStateAttribute1Count", out text) && !string.IsNullOrEmpty(text))
			{
				this.NewStateAttribute1Count = int.Parse(text);
			}
			if (propertyBag.TryGetValue("NewStateAttribute1Percent", out text) && !string.IsNullOrEmpty(text))
			{
				this.NewStateAttribute1Percent = CrimsonHelper.ParseDouble(text);
			}
			if (propertyBag.TryGetValue("TotalStateAttribute1Value", out text))
			{
				this.TotalStateAttribute1Value = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("TotalStateAttribute1Count", out text) && !string.IsNullOrEmpty(text))
			{
				this.TotalStateAttribute1Count = int.Parse(text);
			}
			if (propertyBag.TryGetValue("TotalStateAttribute1Percent", out text) && !string.IsNullOrEmpty(text))
			{
				this.TotalStateAttribute1Percent = CrimsonHelper.ParseDouble(text);
			}
			if (propertyBag.TryGetValue("NewFailureCategoryValue", out text) && !string.IsNullOrEmpty(text))
			{
				this.NewFailureCategoryValue = int.Parse(text);
			}
			if (propertyBag.TryGetValue("NewFailureCategoryCount", out text) && !string.IsNullOrEmpty(text))
			{
				this.NewFailureCategoryCount = int.Parse(text);
			}
			if (propertyBag.TryGetValue("NewFailureCategoryPercent", out text) && !string.IsNullOrEmpty(text))
			{
				this.NewFailureCategoryPercent = CrimsonHelper.ParseDouble(text);
			}
			if (propertyBag.TryGetValue("TotalFailureCategoryValue", out text) && !string.IsNullOrEmpty(text))
			{
				this.TotalFailureCategoryValue = int.Parse(text);
			}
			if (propertyBag.TryGetValue("TotalFailureCategoryCount", out text) && !string.IsNullOrEmpty(text))
			{
				this.TotalFailureCategoryCount = int.Parse(text);
			}
			if (propertyBag.TryGetValue("TotalFailureCategoryPercent", out text) && !string.IsNullOrEmpty(text))
			{
				this.TotalFailureCategoryPercent = CrimsonHelper.ParseDouble(text);
			}
			if (propertyBag.TryGetValue("ComponentName", out text))
			{
				this.ComponentName = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("IsHaImpacting", out text) && !string.IsNullOrEmpty(text))
			{
				this.IsHaImpacting = CrimsonHelper.ParseIntStringAsBool(text);
			}
			if (propertyBag.TryGetValue("SourceScope", out text))
			{
				this.SourceScope = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("TargetScopes", out text))
			{
				this.TargetScopes = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("HaScope", out text))
			{
				this.HaScope = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("Version", out text) && !string.IsNullOrEmpty(text))
			{
				this.Version = int.Parse(text);
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00005B95 File Offset: 0x00003D95
		public override void AssignResultId()
		{
			if (this.ResultId == 0)
			{
				this.ResultId = MonitorResult.idGenerator.NextId();
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00005BB0 File Offset: 0x00003DB0
		public void Write(Action<IPersistence> preWriteHandler = null)
		{
			this.AssignResultId();
			if (preWriteHandler != null)
			{
				preWriteHandler(this);
			}
			if (MonitorResult.ResultWriter != null)
			{
				MonitorResult.ResultWriter(this);
				return;
			}
			NativeMethods.MonitorResultUnmanaged monitorResultUnmanaged = this.ToUnmanaged();
			ResultSeverityLevel severity = CrimsonHelper.ConvertResultTypeToSeverityLevel(this.ResultType);
			NativeMethods.WriteMonitorResult(ref monitorResultUnmanaged, severity);
			LocalDataAccess.MonitorResultLogger.LogEvent(DateTime.UtcNow, this.ToDictionary());
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00005C11 File Offset: 0x00003E11
		public string Serialize()
		{
			return CrimsonHelper.Serialize(this.ToDictionary(), false);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00005C20 File Offset: 0x00003E20
		public void Deserialize(string result)
		{
			string[] array = CrimsonHelper.ClearResultString(result).Split(new char[]
			{
				'|'
			});
			if (!string.IsNullOrEmpty(array[0]))
			{
				this.ResultId = int.Parse(array[0]);
			}
			this.ServiceName = CrimsonHelper.NullDecode(array[1]);
			if (!string.IsNullOrEmpty(array[2]))
			{
				this.IsNotified = bool.Parse(array[2]);
			}
			this.ResultName = CrimsonHelper.NullDecode(array[3]);
			if (!string.IsNullOrEmpty(array[4]))
			{
				this.WorkItemId = int.Parse(array[4]);
			}
			if (!string.IsNullOrEmpty(array[5]))
			{
				this.DeploymentId = int.Parse(array[5]);
			}
			this.MachineName = CrimsonHelper.NullDecode(array[6]);
			this.Error = CrimsonHelper.NullDecode(array[7]);
			this.Exception = CrimsonHelper.NullDecode(array[8]);
			if (!string.IsNullOrEmpty(array[9]))
			{
				this.RetryCount = byte.Parse(array[9]);
			}
			this.StateAttribute1 = CrimsonHelper.NullDecode(array[10]);
			this.StateAttribute2 = CrimsonHelper.NullDecode(array[11]);
			this.StateAttribute3 = CrimsonHelper.NullDecode(array[12]);
			this.StateAttribute4 = CrimsonHelper.NullDecode(array[13]);
			this.StateAttribute5 = CrimsonHelper.NullDecode(array[14]);
			if (!string.IsNullOrEmpty(array[15]))
			{
				this.StateAttribute6 = CrimsonHelper.ParseDouble(array[15]);
			}
			if (!string.IsNullOrEmpty(array[16]))
			{
				this.StateAttribute7 = CrimsonHelper.ParseDouble(array[16]);
			}
			if (!string.IsNullOrEmpty(array[17]))
			{
				this.StateAttribute8 = CrimsonHelper.ParseDouble(array[17]);
			}
			if (!string.IsNullOrEmpty(array[18]))
			{
				this.StateAttribute9 = CrimsonHelper.ParseDouble(array[18]);
			}
			if (!string.IsNullOrEmpty(array[19]))
			{
				this.StateAttribute10 = CrimsonHelper.ParseDouble(array[19]);
			}
			if (!string.IsNullOrEmpty(array[20]))
			{
				this.ResultType = (ResultType)Enum.Parse(typeof(ResultType), array[20]);
			}
			if (!string.IsNullOrEmpty(array[21]))
			{
				this.ExecutionId = int.Parse(array[21]);
			}
			if (!string.IsNullOrEmpty(array[22]))
			{
				this.ExecutionStartTime = DateTime.Parse(array[22]).ToUniversalTime();
			}
			if (!string.IsNullOrEmpty(array[23]))
			{
				this.ExecutionEndTime = DateTime.Parse(array[23]).ToUniversalTime();
			}
			if (!string.IsNullOrEmpty(array[24]))
			{
				this.PoisonedCount = byte.Parse(array[24]);
			}
			if (!string.IsNullOrEmpty(array[25]))
			{
				this.IsAlert = bool.Parse(array[25]);
			}
			if (!string.IsNullOrEmpty(array[26]))
			{
				this.TotalValue = CrimsonHelper.ParseDouble(array[26]);
			}
			if (!string.IsNullOrEmpty(array[27]))
			{
				this.TotalSampleCount = int.Parse(array[27]);
			}
			if (!string.IsNullOrEmpty(array[28]))
			{
				this.TotalFailedCount = int.Parse(array[28]);
			}
			if (!string.IsNullOrEmpty(array[29]))
			{
				this.NewValue = CrimsonHelper.ParseDouble(array[29]);
			}
			if (!string.IsNullOrEmpty(array[30]))
			{
				this.NewSampleCount = int.Parse(array[30]);
			}
			if (!string.IsNullOrEmpty(array[31]))
			{
				this.NewFailedCount = int.Parse(array[31]);
			}
			if (!string.IsNullOrEmpty(array[32]))
			{
				this.LastFailedProbeId = int.Parse(array[32]);
			}
			if (!string.IsNullOrEmpty(array[33]))
			{
				this.LastFailedProbeResultId = int.Parse(array[33]);
			}
			if (!string.IsNullOrEmpty(array[34]))
			{
				this.HealthState = (ServiceHealthStatus)Enum.Parse(typeof(ServiceHealthStatus), array[34]);
			}
			if (!string.IsNullOrEmpty(array[35]))
			{
				this.HealthStateTransitionId = int.Parse(array[35]);
			}
			if (!string.IsNullOrEmpty(array[36]))
			{
				this.HealthStateChangedTime = (string.Equals(array[36], "[null]", StringComparison.OrdinalIgnoreCase) ? null : new DateTime?(DateTime.Parse(array[36]).ToUniversalTime()));
			}
			if (!string.IsNullOrEmpty(array[37]))
			{
				this.FirstAlertObservedTime = (string.Equals(array[37], "[null]", StringComparison.OrdinalIgnoreCase) ? null : new DateTime?(DateTime.Parse(array[37]).ToUniversalTime()));
			}
			if (!string.IsNullOrEmpty(array[38]))
			{
				this.FirstInsufficientSamplesObservedTime = (string.Equals(array[38], "[null]", StringComparison.OrdinalIgnoreCase) ? null : new DateTime?(DateTime.Parse(array[38]).ToUniversalTime()));
			}
			this.NewStateAttribute1Value = CrimsonHelper.NullDecode(array[39]);
			if (!string.IsNullOrEmpty(array[40]))
			{
				this.NewStateAttribute1Count = int.Parse(array[40]);
			}
			if (!string.IsNullOrEmpty(array[41]))
			{
				this.NewStateAttribute1Percent = CrimsonHelper.ParseDouble(array[41]);
			}
			this.TotalStateAttribute1Value = CrimsonHelper.NullDecode(array[42]);
			if (!string.IsNullOrEmpty(array[43]))
			{
				this.TotalStateAttribute1Count = int.Parse(array[43]);
			}
			if (!string.IsNullOrEmpty(array[44]))
			{
				this.TotalStateAttribute1Percent = CrimsonHelper.ParseDouble(array[44]);
			}
			if (!string.IsNullOrEmpty(array[45]))
			{
				this.NewFailureCategoryValue = int.Parse(array[45]);
			}
			if (!string.IsNullOrEmpty(array[46]))
			{
				this.NewFailureCategoryCount = int.Parse(array[46]);
			}
			if (!string.IsNullOrEmpty(array[47]))
			{
				this.NewFailureCategoryPercent = CrimsonHelper.ParseDouble(array[47]);
			}
			if (!string.IsNullOrEmpty(array[48]))
			{
				this.TotalFailureCategoryValue = int.Parse(array[48]);
			}
			if (!string.IsNullOrEmpty(array[49]))
			{
				this.TotalFailureCategoryCount = int.Parse(array[49]);
			}
			if (!string.IsNullOrEmpty(array[50]))
			{
				this.TotalFailureCategoryPercent = CrimsonHelper.ParseDouble(array[50]);
			}
			this.ComponentName = CrimsonHelper.NullDecode(array[51]);
			if (!string.IsNullOrEmpty(array[52]))
			{
				this.IsHaImpacting = bool.Parse(array[52]);
			}
			this.SourceScope = CrimsonHelper.NullDecode(array[53]);
			this.TargetScopes = CrimsonHelper.NullDecode(array[54]);
			this.HaScope = CrimsonHelper.NullDecode(array[55]);
			if (!string.IsNullOrEmpty(array[56]))
			{
				this.Version = int.Parse(array[56]);
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00006204 File Offset: 0x00004404
		internal NativeMethods.MonitorResultUnmanaged ToUnmanaged()
		{
			return new NativeMethods.MonitorResultUnmanaged
			{
				ResultId = this.ResultId,
				ServiceName = CrimsonHelper.NullCode(this.ServiceName),
				IsNotified = (this.IsNotified ? 1 : 0),
				ResultName = CrimsonHelper.NullCode(this.ResultName),
				WorkItemId = this.WorkItemId,
				DeploymentId = this.DeploymentId,
				MachineName = CrimsonHelper.NullCode(this.MachineName),
				Error = CrimsonHelper.NullCode(this.Error),
				Exception = CrimsonHelper.NullCode(this.Exception),
				RetryCount = this.RetryCount,
				StateAttribute1 = CrimsonHelper.NullCode(this.StateAttribute1),
				StateAttribute2 = CrimsonHelper.NullCode(this.StateAttribute2),
				StateAttribute3 = CrimsonHelper.NullCode(this.StateAttribute3),
				StateAttribute4 = CrimsonHelper.NullCode(this.StateAttribute4),
				StateAttribute5 = CrimsonHelper.NullCode(this.StateAttribute5),
				StateAttribute6 = this.StateAttribute6,
				StateAttribute7 = this.StateAttribute7,
				StateAttribute8 = this.StateAttribute8,
				StateAttribute9 = this.StateAttribute9,
				StateAttribute10 = this.StateAttribute10,
				ResultType = this.ResultType,
				ExecutionId = this.ExecutionId,
				ExecutionStartTime = this.ExecutionStartTime.ToUniversalTime().ToString("o"),
				ExecutionEndTime = this.ExecutionEndTime.ToUniversalTime().ToString("o"),
				PoisonedCount = this.PoisonedCount,
				IsAlert = (this.IsAlert ? 1 : 0),
				TotalValue = this.TotalValue,
				TotalSampleCount = this.TotalSampleCount,
				TotalFailedCount = this.TotalFailedCount,
				NewValue = this.NewValue,
				NewSampleCount = this.NewSampleCount,
				NewFailedCount = this.NewFailedCount,
				LastFailedProbeId = this.LastFailedProbeId,
				LastFailedProbeResultId = this.LastFailedProbeResultId,
				HealthState = this.HealthState,
				HealthStateTransitionId = this.HealthStateTransitionId,
				HealthStateChangedTime = ((this.HealthStateChangedTime != null) ? this.HealthStateChangedTime.Value.ToUniversalTime().ToString("o") : "[null]"),
				FirstAlertObservedTime = ((this.FirstAlertObservedTime != null) ? this.FirstAlertObservedTime.Value.ToUniversalTime().ToString("o") : "[null]"),
				FirstInsufficientSamplesObservedTime = ((this.FirstInsufficientSamplesObservedTime != null) ? this.FirstInsufficientSamplesObservedTime.Value.ToUniversalTime().ToString("o") : "[null]"),
				NewStateAttribute1Value = CrimsonHelper.NullCode(this.NewStateAttribute1Value),
				NewStateAttribute1Count = this.NewStateAttribute1Count,
				NewStateAttribute1Percent = this.NewStateAttribute1Percent,
				TotalStateAttribute1Value = CrimsonHelper.NullCode(this.TotalStateAttribute1Value),
				TotalStateAttribute1Count = this.TotalStateAttribute1Count,
				TotalStateAttribute1Percent = this.TotalStateAttribute1Percent,
				NewFailureCategoryValue = this.NewFailureCategoryValue,
				NewFailureCategoryCount = this.NewFailureCategoryCount,
				NewFailureCategoryPercent = this.NewFailureCategoryPercent,
				TotalFailureCategoryValue = this.TotalFailureCategoryValue,
				TotalFailureCategoryCount = this.TotalFailureCategoryCount,
				TotalFailureCategoryPercent = this.TotalFailureCategoryPercent,
				ComponentName = CrimsonHelper.NullCode(this.ComponentName),
				IsHaImpacting = (this.IsHaImpacting ? 1 : 0),
				SourceScope = CrimsonHelper.NullCode(this.SourceScope),
				TargetScopes = CrimsonHelper.NullCode(this.TargetScopes),
				HaScope = CrimsonHelper.NullCode(this.HaScope),
				Version = this.Version
			};
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00006634 File Offset: 0x00004834
		internal Dictionary<string, object> ToDictionary()
		{
			return new Dictionary<string, object>(50)
			{
				{
					"ResultId",
					this.ResultId
				},
				{
					"ServiceName",
					this.ServiceName
				},
				{
					"IsNotified",
					this.IsNotified
				},
				{
					"ResultName",
					this.ResultName
				},
				{
					"WorkItemId",
					this.WorkItemId
				},
				{
					"DeploymentId",
					this.DeploymentId
				},
				{
					"MachineName",
					this.MachineName
				},
				{
					"Error",
					this.Error
				},
				{
					"Exception",
					this.Exception
				},
				{
					"RetryCount",
					this.RetryCount
				},
				{
					"StateAttribute1",
					this.StateAttribute1
				},
				{
					"StateAttribute2",
					this.StateAttribute2
				},
				{
					"StateAttribute3",
					this.StateAttribute3
				},
				{
					"StateAttribute4",
					this.StateAttribute4
				},
				{
					"StateAttribute5",
					this.StateAttribute5
				},
				{
					"StateAttribute6",
					this.StateAttribute6
				},
				{
					"StateAttribute7",
					this.StateAttribute7
				},
				{
					"StateAttribute8",
					this.StateAttribute8
				},
				{
					"StateAttribute9",
					this.StateAttribute9
				},
				{
					"StateAttribute10",
					this.StateAttribute10
				},
				{
					"ResultType",
					this.ResultType
				},
				{
					"ExecutionId",
					this.ExecutionId
				},
				{
					"ExecutionStartTime",
					this.ExecutionStartTime
				},
				{
					"ExecutionEndTime",
					this.ExecutionEndTime
				},
				{
					"PoisonedCount",
					this.PoisonedCount
				},
				{
					"IsAlert",
					this.IsAlert
				},
				{
					"TotalValue",
					this.TotalValue
				},
				{
					"TotalSampleCount",
					this.TotalSampleCount
				},
				{
					"TotalFailedCount",
					this.TotalFailedCount
				},
				{
					"NewValue",
					this.NewValue
				},
				{
					"NewSampleCount",
					this.NewSampleCount
				},
				{
					"NewFailedCount",
					this.NewFailedCount
				},
				{
					"LastFailedProbeId",
					this.LastFailedProbeId
				},
				{
					"LastFailedProbeResultId",
					this.LastFailedProbeResultId
				},
				{
					"HealthState",
					this.HealthState
				},
				{
					"HealthStateTransitionId",
					this.HealthStateTransitionId
				},
				{
					"HealthStateChangedTime",
					this.HealthStateChangedTime
				},
				{
					"FirstAlertObservedTime",
					this.FirstAlertObservedTime
				},
				{
					"FirstInsufficientSamplesObservedTime",
					this.FirstInsufficientSamplesObservedTime
				},
				{
					"NewStateAttribute1Value",
					this.NewStateAttribute1Value
				},
				{
					"NewStateAttribute1Count",
					this.NewStateAttribute1Count
				},
				{
					"NewStateAttribute1Percent",
					this.NewStateAttribute1Percent
				},
				{
					"TotalStateAttribute1Value",
					this.TotalStateAttribute1Value
				},
				{
					"TotalStateAttribute1Count",
					this.TotalStateAttribute1Count
				},
				{
					"TotalStateAttribute1Percent",
					this.TotalStateAttribute1Percent
				},
				{
					"NewFailureCategoryValue",
					this.NewFailureCategoryValue
				},
				{
					"NewFailureCategoryCount",
					this.NewFailureCategoryCount
				},
				{
					"NewFailureCategoryPercent",
					this.NewFailureCategoryPercent
				},
				{
					"TotalFailureCategoryValue",
					this.TotalFailureCategoryValue
				},
				{
					"TotalFailureCategoryCount",
					this.TotalFailureCategoryCount
				},
				{
					"TotalFailureCategoryPercent",
					this.TotalFailureCategoryPercent
				},
				{
					"ComponentName",
					this.ComponentName
				},
				{
					"IsHaImpacting",
					this.IsHaImpacting
				},
				{
					"SourceScope",
					this.SourceScope
				},
				{
					"TargetScopes",
					this.TargetScopes
				},
				{
					"HaScope",
					this.HaScope
				},
				{
					"Version",
					this.Version
				}
			};
		}

		// Token: 0x0400015A RID: 346
		private static int schemaVersion = 65536;

		// Token: 0x0400015B RID: 347
		private static MonitorResultIdGenerator idGenerator = new MonitorResultIdGenerator();
	}
}
