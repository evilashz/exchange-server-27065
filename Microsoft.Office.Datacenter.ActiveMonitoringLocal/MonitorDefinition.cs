using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000015 RID: 21
	[Table]
	public sealed class MonitorDefinition : WorkDefinition, IPersistence
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00007207 File Offset: 0x00005407
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x0000720F File Offset: 0x0000540F
		[Column(IsPrimaryKey = true, IsDbGenerated = true)]
		public override int Id { get; internal set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00007218 File Offset: 0x00005418
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00007220 File Offset: 0x00005420
		[Column]
		public override string AssemblyPath { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00007229 File Offset: 0x00005429
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00007231 File Offset: 0x00005431
		[Column]
		public override string TypeName { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000EC RID: 236 RVA: 0x0000723A File Offset: 0x0000543A
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00007242 File Offset: 0x00005442
		[Column]
		public override string Name { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000EE RID: 238 RVA: 0x0000724B File Offset: 0x0000544B
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00007253 File Offset: 0x00005453
		[Column]
		public override string WorkItemVersion { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x0000725C File Offset: 0x0000545C
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00007264 File Offset: 0x00005464
		[Column]
		public override string ServiceName { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x0000726D File Offset: 0x0000546D
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00007275 File Offset: 0x00005475
		[Column]
		public override int DeploymentId { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x0000727E File Offset: 0x0000547E
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00007286 File Offset: 0x00005486
		[Column]
		public override string ExecutionLocation { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x0000728F File Offset: 0x0000548F
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00007297 File Offset: 0x00005497
		[Column]
		public override DateTime CreatedTime { get; internal set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000072A0 File Offset: 0x000054A0
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x000072A8 File Offset: 0x000054A8
		[Column]
		public override bool Enabled { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000FA RID: 250 RVA: 0x000072B1 File Offset: 0x000054B1
		// (set) Token: 0x060000FB RID: 251 RVA: 0x000072B9 File Offset: 0x000054B9
		[Column]
		public override string TargetPartition { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000FC RID: 252 RVA: 0x000072C2 File Offset: 0x000054C2
		// (set) Token: 0x060000FD RID: 253 RVA: 0x000072CA File Offset: 0x000054CA
		[Column]
		public override string TargetGroup { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000FE RID: 254 RVA: 0x000072D3 File Offset: 0x000054D3
		// (set) Token: 0x060000FF RID: 255 RVA: 0x000072DB File Offset: 0x000054DB
		[Column]
		public override string TargetResource { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000100 RID: 256 RVA: 0x000072E4 File Offset: 0x000054E4
		// (set) Token: 0x06000101 RID: 257 RVA: 0x000072EC File Offset: 0x000054EC
		[Column]
		public override string TargetExtension { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000102 RID: 258 RVA: 0x000072F5 File Offset: 0x000054F5
		// (set) Token: 0x06000103 RID: 259 RVA: 0x000072FD File Offset: 0x000054FD
		[Column]
		public override string TargetVersion { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00007306 File Offset: 0x00005506
		// (set) Token: 0x06000105 RID: 261 RVA: 0x0000730E File Offset: 0x0000550E
		[Column]
		public override int RecurrenceIntervalSeconds { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00007317 File Offset: 0x00005517
		// (set) Token: 0x06000107 RID: 263 RVA: 0x0000731F File Offset: 0x0000551F
		[Column]
		public override int TimeoutSeconds { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00007328 File Offset: 0x00005528
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00007330 File Offset: 0x00005530
		[Column]
		public override DateTime StartTime { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00007339 File Offset: 0x00005539
		// (set) Token: 0x0600010B RID: 267 RVA: 0x00007341 File Offset: 0x00005541
		[Column]
		public override DateTime UpdateTime { get; internal set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600010C RID: 268 RVA: 0x0000734A File Offset: 0x0000554A
		// (set) Token: 0x0600010D RID: 269 RVA: 0x00007352 File Offset: 0x00005552
		[Column]
		public override int MaxRetryAttempts { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600010E RID: 270 RVA: 0x0000735B File Offset: 0x0000555B
		// (set) Token: 0x0600010F RID: 271 RVA: 0x00007363 File Offset: 0x00005563
		[Column]
		public override string ExtensionAttributes { get; internal set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000110 RID: 272 RVA: 0x0000736C File Offset: 0x0000556C
		// (set) Token: 0x06000111 RID: 273 RVA: 0x00007374 File Offset: 0x00005574
		[Column]
		[PropertyInformation("The filter (by default, a prefix filter over ProbeResult.SampleName) used to find the included samples.", false)]
		public string SampleMask { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000112 RID: 274 RVA: 0x0000737D File Offset: 0x0000557D
		// (set) Token: 0x06000113 RID: 275 RVA: 0x00007385 File Offset: 0x00005585
		[Column]
		[PropertyInformation("The time window used in calculating the monitoring state.", false)]
		public int MonitoringIntervalSeconds { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000114 RID: 276 RVA: 0x0000738E File Offset: 0x0000558E
		// (set) Token: 0x06000115 RID: 277 RVA: 0x00007396 File Offset: 0x00005596
		[PropertyInformation("The minimum number of errors to look for before firing the monitor.", false)]
		[Column]
		public int MinimumErrorCount { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000116 RID: 278 RVA: 0x0000739F File Offset: 0x0000559F
		// (set) Token: 0x06000117 RID: 279 RVA: 0x000073A7 File Offset: 0x000055A7
		[PropertyInformation("The threshold used in calculating the monitoring state.", false)]
		[Column]
		public double MonitoringThreshold { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000118 RID: 280 RVA: 0x000073B0 File Offset: 0x000055B0
		// (set) Token: 0x06000119 RID: 281 RVA: 0x000073B8 File Offset: 0x000055B8
		[PropertyInformation("The secondary threshold used in calculating the monitoring state.", false)]
		[Column]
		public double SecondaryMonitoringThreshold { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600011A RID: 282 RVA: 0x000073C1 File Offset: 0x000055C1
		// (set) Token: 0x0600011B RID: 283 RVA: 0x000073C9 File Offset: 0x000055C9
		[PropertyInformation("The percentage of monitoring samples that need to meet MonitoringThreshold.", false)]
		[Column]
		public double MonitoringSamplesThreshold { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600011C RID: 284 RVA: 0x000073D2 File Offset: 0x000055D2
		// (set) Token: 0x0600011D RID: 285 RVA: 0x000073DA File Offset: 0x000055DA
		[Column]
		public int ServicePriority { get; internal set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600011E RID: 286 RVA: 0x000073E3 File Offset: 0x000055E3
		// (set) Token: 0x0600011F RID: 287 RVA: 0x000073EB File Offset: 0x000055EB
		[Column]
		public ServiceSeverity ServiceSeverity { get; internal set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000120 RID: 288 RVA: 0x000073F4 File Offset: 0x000055F4
		// (set) Token: 0x06000121 RID: 289 RVA: 0x000073FC File Offset: 0x000055FC
		[Column]
		public bool IsHaImpacting { get; internal set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00007405 File Offset: 0x00005605
		// (set) Token: 0x06000123 RID: 291 RVA: 0x0000740D File Offset: 0x0000560D
		[Column]
		public override int CreatedById { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00007416 File Offset: 0x00005616
		// (set) Token: 0x06000125 RID: 293 RVA: 0x0000741E File Offset: 0x0000561E
		[Column]
		public int InsufficientSamplesIntervalSeconds { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00007427 File Offset: 0x00005627
		// (set) Token: 0x06000127 RID: 295 RVA: 0x0000742F File Offset: 0x0000562F
		public Component Component { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00007438 File Offset: 0x00005638
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00007440 File Offset: 0x00005640
		[Column]
		public string StateAttribute1Mask { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00007449 File Offset: 0x00005649
		// (set) Token: 0x0600012B RID: 299 RVA: 0x00007451 File Offset: 0x00005651
		[Column]
		public int FailureCategoryMask { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600012C RID: 300 RVA: 0x0000745A File Offset: 0x0000565A
		// (set) Token: 0x0600012D RID: 301 RVA: 0x0000747B File Offset: 0x0000567B
		[Column]
		public string ComponentName
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

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00007489 File Offset: 0x00005689
		// (set) Token: 0x0600012F RID: 303 RVA: 0x00007491 File Offset: 0x00005691
		[Column]
		public string StateTransitionsXml
		{
			get
			{
				return this.stateTransitionsXml;
			}
			set
			{
				this.stateTransitionsXml = value;
				this.monitorStateTransitions = this.ConvertXmlToStateTransitions(this.stateTransitionsXml);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000130 RID: 304 RVA: 0x000074AC File Offset: 0x000056AC
		// (set) Token: 0x06000131 RID: 305 RVA: 0x000074B4 File Offset: 0x000056B4
		[Column]
		public bool AllowCorrelationToMonitor { get; internal set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000132 RID: 306 RVA: 0x000074BD File Offset: 0x000056BD
		// (set) Token: 0x06000133 RID: 307 RVA: 0x000074C5 File Offset: 0x000056C5
		[Column]
		public string ScenarioDescription { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000134 RID: 308 RVA: 0x000074CE File Offset: 0x000056CE
		// (set) Token: 0x06000135 RID: 309 RVA: 0x000074D6 File Offset: 0x000056D6
		[Column]
		public string SourceScope { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000136 RID: 310 RVA: 0x000074DF File Offset: 0x000056DF
		// (set) Token: 0x06000137 RID: 311 RVA: 0x000074E7 File Offset: 0x000056E7
		[Column]
		public string TargetScopes { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000138 RID: 312 RVA: 0x000074F0 File Offset: 0x000056F0
		// (set) Token: 0x06000139 RID: 313 RVA: 0x000074F8 File Offset: 0x000056F8
		[Column]
		public string HaScope
		{
			get
			{
				return this.haScopeString;
			}
			internal set
			{
				this.haScopeEnum = MonitorDefinition.HaScopeToEnum(value);
				this.haScopeString = this.haScopeEnum.ToString();
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000751C File Offset: 0x0000571C
		public static HaScopeEnum HaScopeToEnum(string scope)
		{
			HaScopeEnum result = HaScopeEnum.Server;
			Enum.TryParse<HaScopeEnum>(scope, true, out result);
			return result;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00007536 File Offset: 0x00005736
		public void SetHaScope(HaScopeEnum eScope)
		{
			this.haScopeEnum = eScope;
			this.haScopeString = eScope.ToString();
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00007550 File Offset: 0x00005750
		public HaScopeEnum GetHaScope()
		{
			return this.haScopeEnum;
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00007558 File Offset: 0x00005758
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00007560 File Offset: 0x00005760
		[Column]
		internal override int Version { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00007569 File Offset: 0x00005769
		// (set) Token: 0x06000140 RID: 320 RVA: 0x00007571 File Offset: 0x00005771
		public MonitorStateTransition[] MonitorStateTransitions
		{
			get
			{
				return this.monitorStateTransitions;
			}
			set
			{
				this.monitorStateTransitions = value;
				this.stateTransitionsXml = this.ConvertStateTransitionsToXml(this.monitorStateTransitions);
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000758C File Offset: 0x0000578C
		public MonitorDefinition()
		{
			this.ServicePriority = 2;
			this.MonitoringSamplesThreshold = 100.0;
			this.SetHaScope(HaScopeEnum.Server);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000075B4 File Offset: 0x000057B4
		public override void FromXml(XmlNode definition)
		{
			base.FromXml(definition);
			this.SampleMask = base.GetMandatoryXmlAttribute<string>(definition, "SampleMask");
			this.MonitoringIntervalSeconds = base.GetMandatoryXmlAttribute<int>(definition, "MonitoringIntervalSeconds");
			this.ComponentName = base.GetMandatoryXmlAttribute<string>(definition, "ComponentName");
			this.MonitoringThreshold = base.GetOptionalXmlAttribute<double>(definition, "MonitoringThreshold", 0.0);
			this.SecondaryMonitoringThreshold = base.GetOptionalXmlAttribute<double>(definition, "SecondaryMonitoringThreshold", 0.0);
			this.ServicePriority = base.GetOptionalXmlAttribute<int>(definition, "ServicePriority", 1);
			this.ServiceSeverity = base.GetOptionalXmlEnumAttribute<ServiceSeverity>(definition, "ServiceSeverity", ServiceSeverity.Critical);
			this.MonitorStateTransitions = this.GetStateTransitions(definition);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00007668 File Offset: 0x00005868
		internal string ConvertStateTransitionsToXml(MonitorStateTransition[] transitions)
		{
			if (transitions != null && transitions.Length > 0)
			{
				XElement xelement = new XElement("StateTransitions");
				foreach (MonitorStateTransition monitorStateTransition in transitions)
				{
					xelement.Add(new XElement("Transition", new object[]
					{
						new XAttribute("ToState", monitorStateTransition.ToState),
						new XAttribute("TimeoutInSeconds", (int)monitorStateTransition.TransitionTimeout.TotalSeconds)
					}));
				}
				return xelement.ToString();
			}
			return null;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00007718 File Offset: 0x00005918
		internal MonitorStateTransition[] ConvertXmlToStateTransitions(string stateTransitionXml)
		{
			if (!string.IsNullOrEmpty(stateTransitionXml))
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(stateTransitionXml);
				return this.GetStateTransitions(xmlDocument);
			}
			return new MonitorStateTransition[0];
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00007748 File Offset: 0x00005948
		internal MonitorStateTransition[] GetStateTransitions(XmlNode definition)
		{
			List<MonitorStateTransition> list = new List<MonitorStateTransition>(4);
			XmlNode xmlNode = definition.SelectSingleNode("StateTransitions");
			if (xmlNode != null)
			{
				using (XmlNodeList childNodes = xmlNode.ChildNodes)
				{
					if (childNodes != null)
					{
						foreach (object obj in childNodes)
						{
							XmlNode definition2 = (XmlNode)obj;
							ServiceHealthStatus mandatoryXmlEnumAttribute = base.GetMandatoryXmlEnumAttribute<ServiceHealthStatus>(definition2, "ToState");
							int mandatoryXmlAttribute = base.GetMandatoryXmlAttribute<int>(definition2, "TimeoutInSeconds");
							MonitorStateTransition monitorStateTransition = new MonitorStateTransition(mandatoryXmlEnumAttribute, mandatoryXmlAttribute);
							WTFDiagnostics.TraceDebug<ServiceHealthStatus, TimeSpan>(WTFLog.ManagedAvailability, base.TraceContext, "[Transition] {0} Timeout:{1}", monitorStateTransition.ToState, monitorStateTransition.TransitionTimeout, null, "GetStateTransitions", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\MonitorWorkDefinition.cs", 528);
							list.Add(monitorStateTransition);
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00007844 File Offset: 0x00005A44
		internal override WorkItemResult CreateResult()
		{
			return new MonitorResult(this);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000784C File Offset: 0x00005A4C
		internal override bool Validate(List<string> errors)
		{
			int count = errors.Count;
			base.Validate(errors);
			if (string.IsNullOrWhiteSpace(this.SampleMask))
			{
				errors.Add("SampleMask cannot be null or empty. ");
			}
			if (this.MonitoringIntervalSeconds <= 0)
			{
				errors.Add("MonitoringIntervalSeconds cannot be less than or equal to 0. ");
			}
			if (this.Component == null)
			{
				errors.Add("Component cannot be null. ");
			}
			return count == errors.Count;
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000148 RID: 328 RVA: 0x000078B6 File Offset: 0x00005AB6
		// (set) Token: 0x06000149 RID: 329 RVA: 0x000078BE File Offset: 0x00005ABE
		public LocalDataAccessMetaData LocalDataAccessMetaData { get; private set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000078C7 File Offset: 0x00005AC7
		internal static int SchemaVersion
		{
			get
			{
				return MonitorDefinition.schemaVersion;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600014B RID: 331 RVA: 0x000078CE File Offset: 0x00005ACE
		// (set) Token: 0x0600014C RID: 332 RVA: 0x000078D5 File Offset: 0x00005AD5
		internal static IEnumerable<WorkDefinitionOverride> GlobalOverrides { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600014D RID: 333 RVA: 0x000078DD File Offset: 0x00005ADD
		// (set) Token: 0x0600014E RID: 334 RVA: 0x000078E4 File Offset: 0x00005AE4
		internal static IEnumerable<WorkDefinitionOverride> LocalOverrides { get; set; }

		// Token: 0x0600014F RID: 335 RVA: 0x000078EC File Offset: 0x00005AEC
		public void Initialize(Dictionary<string, string> propertyBag, LocalDataAccessMetaData metaData)
		{
			this.LocalDataAccessMetaData = metaData;
			this.SetProperties(propertyBag);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000078FC File Offset: 0x00005AFC
		public void SetProperties(Dictionary<string, string> propertyBag)
		{
			string text;
			if (propertyBag.TryGetValue("Id", out text) && !string.IsNullOrEmpty(text))
			{
				this.Id = int.Parse(text);
			}
			if (propertyBag.TryGetValue("AssemblyPath", out text))
			{
				this.AssemblyPath = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("TypeName", out text))
			{
				this.TypeName = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("Name", out text))
			{
				this.Name = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("WorkItemVersion", out text))
			{
				this.WorkItemVersion = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("ServiceName", out text))
			{
				this.ServiceName = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("DeploymentId", out text) && !string.IsNullOrEmpty(text))
			{
				this.DeploymentId = int.Parse(text);
			}
			if (propertyBag.TryGetValue("ExecutionLocation", out text))
			{
				this.ExecutionLocation = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("CreatedTime", out text) && !string.IsNullOrEmpty(text))
			{
				this.CreatedTime = DateTime.Parse(text).ToUniversalTime();
			}
			if (propertyBag.TryGetValue("Enabled", out text) && !string.IsNullOrEmpty(text))
			{
				this.Enabled = CrimsonHelper.ParseIntStringAsBool(text);
			}
			if (propertyBag.TryGetValue("TargetPartition", out text))
			{
				this.TargetPartition = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("TargetGroup", out text))
			{
				this.TargetGroup = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("TargetResource", out text))
			{
				this.TargetResource = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("TargetExtension", out text))
			{
				this.TargetExtension = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("TargetVersion", out text))
			{
				this.TargetVersion = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("RecurrenceIntervalSeconds", out text) && !string.IsNullOrEmpty(text))
			{
				this.RecurrenceIntervalSeconds = int.Parse(text);
			}
			if (propertyBag.TryGetValue("TimeoutSeconds", out text) && !string.IsNullOrEmpty(text))
			{
				this.TimeoutSeconds = int.Parse(text);
			}
			if (propertyBag.TryGetValue("StartTime", out text) && !string.IsNullOrEmpty(text))
			{
				this.StartTime = DateTime.Parse(text).ToUniversalTime();
			}
			if (propertyBag.TryGetValue("UpdateTime", out text) && !string.IsNullOrEmpty(text))
			{
				this.UpdateTime = DateTime.Parse(text).ToUniversalTime();
			}
			if (propertyBag.TryGetValue("MaxRetryAttempts", out text) && !string.IsNullOrEmpty(text))
			{
				this.MaxRetryAttempts = int.Parse(text);
			}
			if (propertyBag.TryGetValue("ExtensionAttributes", out text))
			{
				this.ExtensionAttributes = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("SampleMask", out text))
			{
				this.SampleMask = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("MonitoringIntervalSeconds", out text) && !string.IsNullOrEmpty(text))
			{
				this.MonitoringIntervalSeconds = int.Parse(text);
			}
			if (propertyBag.TryGetValue("MinimumErrorCount", out text) && !string.IsNullOrEmpty(text))
			{
				this.MinimumErrorCount = int.Parse(text);
			}
			if (propertyBag.TryGetValue("MonitoringThreshold", out text) && !string.IsNullOrEmpty(text))
			{
				this.MonitoringThreshold = CrimsonHelper.ParseDouble(text);
			}
			if (propertyBag.TryGetValue("SecondaryMonitoringThreshold", out text) && !string.IsNullOrEmpty(text))
			{
				this.SecondaryMonitoringThreshold = CrimsonHelper.ParseDouble(text);
			}
			if (propertyBag.TryGetValue("MonitoringSamplesThreshold", out text) && !string.IsNullOrEmpty(text))
			{
				this.MonitoringSamplesThreshold = CrimsonHelper.ParseDouble(text);
			}
			if (propertyBag.TryGetValue("ServicePriority", out text) && !string.IsNullOrEmpty(text))
			{
				this.ServicePriority = int.Parse(text);
			}
			if (propertyBag.TryGetValue("ServiceSeverity", out text) && !string.IsNullOrEmpty(text))
			{
				this.ServiceSeverity = (ServiceSeverity)Enum.Parse(typeof(ServiceSeverity), text);
			}
			if (propertyBag.TryGetValue("IsHaImpacting", out text) && !string.IsNullOrEmpty(text))
			{
				this.IsHaImpacting = CrimsonHelper.ParseIntStringAsBool(text);
			}
			if (propertyBag.TryGetValue("CreatedById", out text) && !string.IsNullOrEmpty(text))
			{
				this.CreatedById = int.Parse(text);
			}
			if (propertyBag.TryGetValue("InsufficientSamplesIntervalSeconds", out text) && !string.IsNullOrEmpty(text))
			{
				this.InsufficientSamplesIntervalSeconds = int.Parse(text);
			}
			if (propertyBag.TryGetValue("StateAttribute1Mask", out text))
			{
				this.StateAttribute1Mask = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("FailureCategoryMask", out text) && !string.IsNullOrEmpty(text))
			{
				this.FailureCategoryMask = int.Parse(text);
			}
			if (propertyBag.TryGetValue("ComponentName", out text))
			{
				this.ComponentName = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("StateTransitionsXml", out text))
			{
				this.StateTransitionsXml = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("AllowCorrelationToMonitor", out text) && !string.IsNullOrEmpty(text))
			{
				this.AllowCorrelationToMonitor = CrimsonHelper.ParseIntStringAsBool(text);
			}
			if (propertyBag.TryGetValue("ScenarioDescription", out text))
			{
				this.ScenarioDescription = CrimsonHelper.NullDecode(text);
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

		// Token: 0x06000151 RID: 337 RVA: 0x00007E50 File Offset: 0x00006050
		public void Write(Action<IPersistence> preWriteHandler = null)
		{
			DefinitionIdGenerator<MonitorDefinition>.AssignId(this);
			Update<MonitorDefinition>.ApplyUpdates(this);
			if (MonitorDefinition.GlobalOverrides != null)
			{
				foreach (WorkDefinitionOverride definitionOverride in MonitorDefinition.GlobalOverrides)
				{
					definitionOverride.TryApplyTo(this, base.TraceContext);
				}
			}
			if (MonitorDefinition.LocalOverrides != null)
			{
				foreach (WorkDefinitionOverride definitionOverride2 in MonitorDefinition.LocalOverrides)
				{
					definitionOverride2.TryApplyTo(this, base.TraceContext);
				}
			}
			if (preWriteHandler != null)
			{
				preWriteHandler(this);
			}
			NativeMethods.MonitorDefinitionUnmanaged monitorDefinitionUnmanaged = this.ToUnmanaged();
			NativeMethods.WriteMonitorDefinition(ref monitorDefinitionUnmanaged);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00007F1C File Offset: 0x0000611C
		internal NativeMethods.MonitorDefinitionUnmanaged ToUnmanaged()
		{
			return new NativeMethods.MonitorDefinitionUnmanaged
			{
				Id = this.Id,
				AssemblyPath = CrimsonHelper.NullCode(this.AssemblyPath),
				TypeName = CrimsonHelper.NullCode(this.TypeName),
				Name = CrimsonHelper.NullCode(this.Name),
				WorkItemVersion = CrimsonHelper.NullCode(this.WorkItemVersion),
				ServiceName = CrimsonHelper.NullCode(this.ServiceName),
				DeploymentId = this.DeploymentId,
				ExecutionLocation = CrimsonHelper.NullCode(this.ExecutionLocation),
				CreatedTime = this.CreatedTime.ToUniversalTime().ToString("o"),
				Enabled = (this.Enabled ? 1 : 0),
				TargetPartition = CrimsonHelper.NullCode(this.TargetPartition),
				TargetGroup = CrimsonHelper.NullCode(this.TargetGroup),
				TargetResource = CrimsonHelper.NullCode(this.TargetResource),
				TargetExtension = CrimsonHelper.NullCode(this.TargetExtension),
				TargetVersion = CrimsonHelper.NullCode(this.TargetVersion),
				RecurrenceIntervalSeconds = this.RecurrenceIntervalSeconds,
				TimeoutSeconds = this.TimeoutSeconds,
				StartTime = this.StartTime.ToUniversalTime().ToString("o"),
				UpdateTime = this.UpdateTime.ToUniversalTime().ToString("o"),
				MaxRetryAttempts = this.MaxRetryAttempts,
				ExtensionAttributes = CrimsonHelper.NullCode(this.ExtensionAttributes),
				SampleMask = CrimsonHelper.NullCode(this.SampleMask),
				MonitoringIntervalSeconds = this.MonitoringIntervalSeconds,
				MinimumErrorCount = this.MinimumErrorCount,
				MonitoringThreshold = this.MonitoringThreshold,
				SecondaryMonitoringThreshold = this.SecondaryMonitoringThreshold,
				MonitoringSamplesThreshold = this.MonitoringSamplesThreshold,
				ServicePriority = this.ServicePriority,
				ServiceSeverity = this.ServiceSeverity,
				IsHaImpacting = (this.IsHaImpacting ? 1 : 0),
				CreatedById = this.CreatedById,
				InsufficientSamplesIntervalSeconds = this.InsufficientSamplesIntervalSeconds,
				StateAttribute1Mask = CrimsonHelper.NullCode(this.StateAttribute1Mask),
				FailureCategoryMask = this.FailureCategoryMask,
				ComponentName = CrimsonHelper.NullCode(this.ComponentName),
				StateTransitionsXml = CrimsonHelper.NullCode(this.StateTransitionsXml),
				AllowCorrelationToMonitor = (this.AllowCorrelationToMonitor ? 1 : 0),
				ScenarioDescription = CrimsonHelper.NullCode(this.ScenarioDescription),
				SourceScope = CrimsonHelper.NullCode(this.SourceScope),
				TargetScopes = CrimsonHelper.NullCode(this.TargetScopes),
				HaScope = CrimsonHelper.NullCode(this.HaScope),
				Version = this.Version
			};
		}

		// Token: 0x040001A6 RID: 422
		private string haScopeString;

		// Token: 0x040001A7 RID: 423
		private HaScopeEnum haScopeEnum;

		// Token: 0x040001A8 RID: 424
		private MonitorStateTransition[] monitorStateTransitions;

		// Token: 0x040001A9 RID: 425
		private string stateTransitionsXml;

		// Token: 0x040001AA RID: 426
		private static int schemaVersion = 65536;
	}
}
