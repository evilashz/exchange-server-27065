using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x0200001D RID: 29
	[Table]
	public sealed class ProbeResult : WorkItemResult, ICloneable, IPersistence, IWorkItemResultSerialization
	{
		// Token: 0x060001A2 RID: 418 RVA: 0x00009966 File Offset: 0x00007B66
		public ProbeResult(WorkDefinition definition) : base(definition)
		{
			this.FailureCategory = -1;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00009976 File Offset: 0x00007B76
		public ProbeResult()
		{
			this.FailureCategory = -1;
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00009985 File Offset: 0x00007B85
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x0000998D File Offset: 0x00007B8D
		[Column(IsPrimaryKey = true, IsDbGenerated = true)]
		public override int ResultId { get; protected internal set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00009996 File Offset: 0x00007B96
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x0000999E File Offset: 0x00007B9E
		[Column]
		public override string ServiceName { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x000099A7 File Offset: 0x00007BA7
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x000099AF File Offset: 0x00007BAF
		[Column]
		public override bool IsNotified { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001AA RID: 426 RVA: 0x000099B8 File Offset: 0x00007BB8
		// (set) Token: 0x060001AB RID: 427 RVA: 0x000099C0 File Offset: 0x00007BC0
		[Column]
		public override string ResultName { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001AC RID: 428 RVA: 0x000099C9 File Offset: 0x00007BC9
		// (set) Token: 0x060001AD RID: 429 RVA: 0x000099D1 File Offset: 0x00007BD1
		[Column]
		public override int WorkItemId { get; internal set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001AE RID: 430 RVA: 0x000099DA File Offset: 0x00007BDA
		// (set) Token: 0x060001AF RID: 431 RVA: 0x000099E2 File Offset: 0x00007BE2
		[Column]
		public override int DeploymentId { get; internal set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x000099EB File Offset: 0x00007BEB
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x000099F3 File Offset: 0x00007BF3
		[Column]
		public override string MachineName { get; internal set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x000099FC File Offset: 0x00007BFC
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x00009A04 File Offset: 0x00007C04
		[Column]
		public override string Error { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00009A0D File Offset: 0x00007C0D
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x00009A15 File Offset: 0x00007C15
		[Column]
		public override string Exception { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00009A1E File Offset: 0x00007C1E
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x00009A26 File Offset: 0x00007C26
		[Column]
		public override byte RetryCount { get; internal set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00009A2F File Offset: 0x00007C2F
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x00009A37 File Offset: 0x00007C37
		[Column]
		public override string StateAttribute1 { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00009A40 File Offset: 0x00007C40
		// (set) Token: 0x060001BB RID: 443 RVA: 0x00009A48 File Offset: 0x00007C48
		[Column]
		public override string StateAttribute2 { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00009A51 File Offset: 0x00007C51
		// (set) Token: 0x060001BD RID: 445 RVA: 0x00009A59 File Offset: 0x00007C59
		[Column]
		public override string StateAttribute3 { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00009A62 File Offset: 0x00007C62
		// (set) Token: 0x060001BF RID: 447 RVA: 0x00009A6A File Offset: 0x00007C6A
		[Column]
		public override string StateAttribute4 { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00009A73 File Offset: 0x00007C73
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x00009A7B File Offset: 0x00007C7B
		[Column]
		public override string StateAttribute5 { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00009A84 File Offset: 0x00007C84
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x00009A8C File Offset: 0x00007C8C
		[Column]
		public override double StateAttribute6 { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00009A95 File Offset: 0x00007C95
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x00009A9D File Offset: 0x00007C9D
		[Column]
		public override double StateAttribute7 { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00009AA6 File Offset: 0x00007CA6
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x00009AAE File Offset: 0x00007CAE
		[Column]
		public override double StateAttribute8 { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00009AB7 File Offset: 0x00007CB7
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x00009ABF File Offset: 0x00007CBF
		[Column]
		public override double StateAttribute9 { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00009AC8 File Offset: 0x00007CC8
		// (set) Token: 0x060001CB RID: 459 RVA: 0x00009AD0 File Offset: 0x00007CD0
		[Column]
		public override double StateAttribute10 { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00009AD9 File Offset: 0x00007CD9
		// (set) Token: 0x060001CD RID: 461 RVA: 0x00009AE1 File Offset: 0x00007CE1
		[Column]
		public string StateAttribute11 { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00009AEA File Offset: 0x00007CEA
		// (set) Token: 0x060001CF RID: 463 RVA: 0x00009AF2 File Offset: 0x00007CF2
		[Column]
		public string StateAttribute12 { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00009AFB File Offset: 0x00007CFB
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x00009B03 File Offset: 0x00007D03
		[Column]
		public string StateAttribute13 { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00009B0C File Offset: 0x00007D0C
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x00009B14 File Offset: 0x00007D14
		[Column]
		public string StateAttribute14 { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00009B1D File Offset: 0x00007D1D
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x00009B25 File Offset: 0x00007D25
		[Column]
		public string StateAttribute15 { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00009B2E File Offset: 0x00007D2E
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x00009B36 File Offset: 0x00007D36
		[Column]
		public double StateAttribute16 { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00009B3F File Offset: 0x00007D3F
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x00009B47 File Offset: 0x00007D47
		[Column]
		public double StateAttribute17 { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00009B50 File Offset: 0x00007D50
		// (set) Token: 0x060001DB RID: 475 RVA: 0x00009B58 File Offset: 0x00007D58
		[Column]
		public double StateAttribute18 { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00009B61 File Offset: 0x00007D61
		// (set) Token: 0x060001DD RID: 477 RVA: 0x00009B69 File Offset: 0x00007D69
		[Column]
		public double StateAttribute19 { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00009B72 File Offset: 0x00007D72
		// (set) Token: 0x060001DF RID: 479 RVA: 0x00009B7A File Offset: 0x00007D7A
		[Column]
		public double StateAttribute20 { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00009B83 File Offset: 0x00007D83
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x00009B8B File Offset: 0x00007D8B
		[Column]
		public string StateAttribute21 { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00009B94 File Offset: 0x00007D94
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x00009B9C File Offset: 0x00007D9C
		[Column]
		public string StateAttribute22 { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00009BA5 File Offset: 0x00007DA5
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x00009BAD File Offset: 0x00007DAD
		[Column]
		public string StateAttribute23 { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00009BB6 File Offset: 0x00007DB6
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x00009BBE File Offset: 0x00007DBE
		[Column]
		public string StateAttribute24 { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00009BC7 File Offset: 0x00007DC7
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x00009BCF File Offset: 0x00007DCF
		[Column]
		public string StateAttribute25 { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00009BD8 File Offset: 0x00007DD8
		// (set) Token: 0x060001EB RID: 491 RVA: 0x00009BE0 File Offset: 0x00007DE0
		[Column]
		public override ResultType ResultType { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00009BE9 File Offset: 0x00007DE9
		// (set) Token: 0x060001ED RID: 493 RVA: 0x00009BF1 File Offset: 0x00007DF1
		[Column]
		public override int ExecutionId { get; protected set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00009BFA File Offset: 0x00007DFA
		// (set) Token: 0x060001EF RID: 495 RVA: 0x00009C02 File Offset: 0x00007E02
		[Column]
		public override DateTime ExecutionStartTime { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00009C0B File Offset: 0x00007E0B
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x00009C13 File Offset: 0x00007E13
		[Column]
		public override DateTime ExecutionEndTime { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00009C1C File Offset: 0x00007E1C
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x00009C24 File Offset: 0x00007E24
		[Column]
		public override byte PoisonedCount { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00009C2D File Offset: 0x00007E2D
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x00009C35 File Offset: 0x00007E35
		[Column(Name = "NotificationParametersXml")]
		public string ExtensionXml { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00009C3E File Offset: 0x00007E3E
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x00009C46 File Offset: 0x00007E46
		[Column(Name = "Latency")]
		public double SampleValue { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00009C4F File Offset: 0x00007E4F
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x00009C57 File Offset: 0x00007E57
		[Column]
		public string ExecutionContext { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00009C60 File Offset: 0x00007E60
		// (set) Token: 0x060001FB RID: 507 RVA: 0x00009C68 File Offset: 0x00007E68
		[Column]
		public string FailureContext { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00009C71 File Offset: 0x00007E71
		// (set) Token: 0x060001FD RID: 509 RVA: 0x00009C79 File Offset: 0x00007E79
		[Column]
		public int FailureCategory { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00009C82 File Offset: 0x00007E82
		// (set) Token: 0x060001FF RID: 511 RVA: 0x00009C8A File Offset: 0x00007E8A
		[Column]
		public string ScopeName { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00009C93 File Offset: 0x00007E93
		// (set) Token: 0x06000201 RID: 513 RVA: 0x00009C9B File Offset: 0x00007E9B
		[Column]
		public string ScopeType { get; set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00009CA4 File Offset: 0x00007EA4
		// (set) Token: 0x06000203 RID: 515 RVA: 0x00009CAC File Offset: 0x00007EAC
		[Column]
		public string HealthSetName { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00009CB5 File Offset: 0x00007EB5
		// (set) Token: 0x06000205 RID: 517 RVA: 0x00009CBD File Offset: 0x00007EBD
		[Column]
		public string Data { get; set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00009CC6 File Offset: 0x00007EC6
		// (set) Token: 0x06000207 RID: 519 RVA: 0x00009CCE File Offset: 0x00007ECE
		[Column]
		internal override int Version { get; set; }

		// Token: 0x06000208 RID: 520 RVA: 0x00009CD8 File Offset: 0x00007ED8
		public override void SetCompleted(ResultType resultType)
		{
			this.StateAttribute11 = base.TruncateStringProperty(this.StateAttribute11, 1024);
			this.StateAttribute12 = base.TruncateStringProperty(this.StateAttribute12, 1024);
			this.StateAttribute13 = base.TruncateStringProperty(this.StateAttribute13, 1024);
			this.StateAttribute14 = base.TruncateStringProperty(this.StateAttribute14, 1024);
			this.StateAttribute15 = base.TruncateStringProperty(this.StateAttribute15, 1024);
			this.StateAttribute21 = base.TruncateStringProperty(this.StateAttribute21, 1024);
			this.StateAttribute22 = base.TruncateStringProperty(this.StateAttribute22, 1024);
			this.StateAttribute23 = base.TruncateStringProperty(this.StateAttribute23, 1024);
			this.StateAttribute24 = base.TruncateStringProperty(this.StateAttribute24, 1024);
			this.StateAttribute25 = base.TruncateStringProperty(this.StateAttribute25, 1024);
			this.ExecutionContext = base.TruncateStringProperty(this.ExecutionContext, 4000);
			this.FailureContext = base.TruncateStringProperty(this.FailureContext, 4000);
			base.SetCompleted(resultType);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00009E00 File Offset: 0x00008000
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00009E08 File Offset: 0x00008008
		// (set) Token: 0x0600020B RID: 523 RVA: 0x00009E10 File Offset: 0x00008010
		public LocalDataAccessMetaData LocalDataAccessMetaData { get; private set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00009E19 File Offset: 0x00008019
		internal static int SchemaVersion
		{
			get
			{
				return ProbeResult.schemaVersion;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00009E20 File Offset: 0x00008020
		// (set) Token: 0x0600020E RID: 526 RVA: 0x00009E27 File Offset: 0x00008027
		internal static Action<ProbeResult> ResultWriter { private get; set; }

		// Token: 0x0600020F RID: 527 RVA: 0x00009E2F File Offset: 0x0000802F
		public void Initialize(Dictionary<string, string> propertyBag, LocalDataAccessMetaData metaData)
		{
			this.LocalDataAccessMetaData = metaData;
			this.SetProperties(propertyBag);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00009E40 File Offset: 0x00008040
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
			if (propertyBag.TryGetValue("StateAttribute11", out text))
			{
				this.StateAttribute11 = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("StateAttribute12", out text))
			{
				this.StateAttribute12 = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("StateAttribute13", out text))
			{
				this.StateAttribute13 = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("StateAttribute14", out text))
			{
				this.StateAttribute14 = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("StateAttribute15", out text))
			{
				this.StateAttribute15 = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("StateAttribute16", out text) && !string.IsNullOrEmpty(text))
			{
				this.StateAttribute16 = CrimsonHelper.ParseDouble(text);
			}
			if (propertyBag.TryGetValue("StateAttribute17", out text) && !string.IsNullOrEmpty(text))
			{
				this.StateAttribute17 = CrimsonHelper.ParseDouble(text);
			}
			if (propertyBag.TryGetValue("StateAttribute18", out text) && !string.IsNullOrEmpty(text))
			{
				this.StateAttribute18 = CrimsonHelper.ParseDouble(text);
			}
			if (propertyBag.TryGetValue("StateAttribute19", out text) && !string.IsNullOrEmpty(text))
			{
				this.StateAttribute19 = CrimsonHelper.ParseDouble(text);
			}
			if (propertyBag.TryGetValue("StateAttribute20", out text) && !string.IsNullOrEmpty(text))
			{
				this.StateAttribute20 = CrimsonHelper.ParseDouble(text);
			}
			if (propertyBag.TryGetValue("StateAttribute21", out text))
			{
				this.StateAttribute21 = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("StateAttribute22", out text))
			{
				this.StateAttribute22 = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("StateAttribute23", out text))
			{
				this.StateAttribute23 = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("StateAttribute24", out text))
			{
				this.StateAttribute24 = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("StateAttribute25", out text))
			{
				this.StateAttribute25 = CrimsonHelper.NullDecode(text);
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
			if (propertyBag.TryGetValue("ExtensionXml", out text))
			{
				this.ExtensionXml = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("SampleValue", out text) && !string.IsNullOrEmpty(text))
			{
				this.SampleValue = CrimsonHelper.ParseDouble(text);
			}
			if (propertyBag.TryGetValue("ExecutionContext", out text))
			{
				this.ExecutionContext = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("FailureContext", out text))
			{
				this.FailureContext = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("FailureCategory", out text) && !string.IsNullOrEmpty(text))
			{
				this.FailureCategory = int.Parse(text);
			}
			if (propertyBag.TryGetValue("ScopeName", out text))
			{
				this.ScopeName = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("ScopeType", out text))
			{
				this.ScopeType = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("HealthSetName", out text))
			{
				this.HealthSetName = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("Data", out text))
			{
				this.Data = CrimsonHelper.NullDecode(text);
			}
			if (propertyBag.TryGetValue("Version", out text) && !string.IsNullOrEmpty(text))
			{
				this.Version = int.Parse(text);
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000A46A File Offset: 0x0000866A
		public override void AssignResultId()
		{
			if (this.ResultId == 0)
			{
				this.ResultId = ProbeResult.idGenerator.NextId();
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000A484 File Offset: 0x00008684
		public void Write(Action<IPersistence> preWriteHandler = null)
		{
			this.AssignResultId();
			if (preWriteHandler != null)
			{
				preWriteHandler(this);
			}
			if (ProbeResult.ResultWriter != null)
			{
				ProbeResult.ResultWriter(this);
				return;
			}
			NativeMethods.ProbeResultUnmanaged probeResultUnmanaged = this.ToUnmanaged();
			ResultSeverityLevel severity = CrimsonHelper.ConvertResultTypeToSeverityLevel(this.ResultType);
			NativeMethods.WriteProbeResult(ref probeResultUnmanaged, severity);
			LocalDataAccess.ProbeResultLogger.LogEvent(DateTime.UtcNow, this.ToDictionary());
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000A4E5 File Offset: 0x000086E5
		public string Serialize()
		{
			return CrimsonHelper.Serialize(this.ToDictionary(), false);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000A4F4 File Offset: 0x000086F4
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
			this.StateAttribute11 = CrimsonHelper.NullDecode(array[20]);
			this.StateAttribute12 = CrimsonHelper.NullDecode(array[21]);
			this.StateAttribute13 = CrimsonHelper.NullDecode(array[22]);
			this.StateAttribute14 = CrimsonHelper.NullDecode(array[23]);
			this.StateAttribute15 = CrimsonHelper.NullDecode(array[24]);
			if (!string.IsNullOrEmpty(array[25]))
			{
				this.StateAttribute16 = CrimsonHelper.ParseDouble(array[25]);
			}
			if (!string.IsNullOrEmpty(array[26]))
			{
				this.StateAttribute17 = CrimsonHelper.ParseDouble(array[26]);
			}
			if (!string.IsNullOrEmpty(array[27]))
			{
				this.StateAttribute18 = CrimsonHelper.ParseDouble(array[27]);
			}
			if (!string.IsNullOrEmpty(array[28]))
			{
				this.StateAttribute19 = CrimsonHelper.ParseDouble(array[28]);
			}
			if (!string.IsNullOrEmpty(array[29]))
			{
				this.StateAttribute20 = CrimsonHelper.ParseDouble(array[29]);
			}
			this.StateAttribute21 = CrimsonHelper.NullDecode(array[30]);
			this.StateAttribute22 = CrimsonHelper.NullDecode(array[31]);
			this.StateAttribute23 = CrimsonHelper.NullDecode(array[32]);
			this.StateAttribute24 = CrimsonHelper.NullDecode(array[33]);
			this.StateAttribute25 = CrimsonHelper.NullDecode(array[34]);
			if (!string.IsNullOrEmpty(array[35]))
			{
				this.ResultType = (ResultType)Enum.Parse(typeof(ResultType), array[35]);
			}
			if (!string.IsNullOrEmpty(array[36]))
			{
				this.ExecutionId = int.Parse(array[36]);
			}
			if (!string.IsNullOrEmpty(array[37]))
			{
				this.ExecutionStartTime = DateTime.Parse(array[37]).ToUniversalTime();
			}
			if (!string.IsNullOrEmpty(array[38]))
			{
				this.ExecutionEndTime = DateTime.Parse(array[38]).ToUniversalTime();
			}
			if (!string.IsNullOrEmpty(array[39]))
			{
				this.PoisonedCount = byte.Parse(array[39]);
			}
			this.ExtensionXml = CrimsonHelper.NullDecode(array[40]);
			if (!string.IsNullOrEmpty(array[41]))
			{
				this.SampleValue = CrimsonHelper.ParseDouble(array[41]);
			}
			this.ExecutionContext = CrimsonHelper.NullDecode(array[42]);
			this.FailureContext = CrimsonHelper.NullDecode(array[43]);
			if (!string.IsNullOrEmpty(array[44]))
			{
				this.FailureCategory = int.Parse(array[44]);
			}
			this.ScopeName = CrimsonHelper.NullDecode(array[45]);
			this.ScopeType = CrimsonHelper.NullDecode(array[46]);
			this.HealthSetName = CrimsonHelper.NullDecode(array[47]);
			this.Data = CrimsonHelper.NullDecode(array[48]);
			if (!string.IsNullOrEmpty(array[49]))
			{
				this.Version = int.Parse(array[49]);
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000A918 File Offset: 0x00008B18
		internal NativeMethods.ProbeResultUnmanaged ToUnmanaged()
		{
			return new NativeMethods.ProbeResultUnmanaged
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
				StateAttribute11 = CrimsonHelper.NullCode(this.StateAttribute11),
				StateAttribute12 = CrimsonHelper.NullCode(this.StateAttribute12),
				StateAttribute13 = CrimsonHelper.NullCode(this.StateAttribute13),
				StateAttribute14 = CrimsonHelper.NullCode(this.StateAttribute14),
				StateAttribute15 = CrimsonHelper.NullCode(this.StateAttribute15),
				StateAttribute16 = this.StateAttribute16,
				StateAttribute17 = this.StateAttribute17,
				StateAttribute18 = this.StateAttribute18,
				StateAttribute19 = this.StateAttribute19,
				StateAttribute20 = this.StateAttribute20,
				StateAttribute21 = CrimsonHelper.NullCode(this.StateAttribute21),
				StateAttribute22 = CrimsonHelper.NullCode(this.StateAttribute22),
				StateAttribute23 = CrimsonHelper.NullCode(this.StateAttribute23),
				StateAttribute24 = CrimsonHelper.NullCode(this.StateAttribute24),
				StateAttribute25 = CrimsonHelper.NullCode(this.StateAttribute25),
				ResultType = this.ResultType,
				ExecutionId = this.ExecutionId,
				ExecutionStartTime = this.ExecutionStartTime.ToUniversalTime().ToString("o"),
				ExecutionEndTime = this.ExecutionEndTime.ToUniversalTime().ToString("o"),
				PoisonedCount = this.PoisonedCount,
				ExtensionXml = CrimsonHelper.NullCode(this.ExtensionXml),
				SampleValue = this.SampleValue,
				ExecutionContext = CrimsonHelper.NullCode(this.ExecutionContext),
				FailureContext = CrimsonHelper.NullCode(this.FailureContext),
				FailureCategory = this.FailureCategory,
				ScopeName = CrimsonHelper.NullCode(this.ScopeName),
				ScopeType = CrimsonHelper.NullCode(this.ScopeType),
				HealthSetName = CrimsonHelper.NullCode(this.HealthSetName),
				Data = CrimsonHelper.NullCode(this.Data),
				Version = this.Version
			};
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000AC70 File Offset: 0x00008E70
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
					"StateAttribute11",
					this.StateAttribute11
				},
				{
					"StateAttribute12",
					this.StateAttribute12
				},
				{
					"StateAttribute13",
					this.StateAttribute13
				},
				{
					"StateAttribute14",
					this.StateAttribute14
				},
				{
					"StateAttribute15",
					this.StateAttribute15
				},
				{
					"StateAttribute16",
					this.StateAttribute16
				},
				{
					"StateAttribute17",
					this.StateAttribute17
				},
				{
					"StateAttribute18",
					this.StateAttribute18
				},
				{
					"StateAttribute19",
					this.StateAttribute19
				},
				{
					"StateAttribute20",
					this.StateAttribute20
				},
				{
					"StateAttribute21",
					this.StateAttribute21
				},
				{
					"StateAttribute22",
					this.StateAttribute22
				},
				{
					"StateAttribute23",
					this.StateAttribute23
				},
				{
					"StateAttribute24",
					this.StateAttribute24
				},
				{
					"StateAttribute25",
					this.StateAttribute25
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
					"ExtensionXml",
					this.ExtensionXml
				},
				{
					"SampleValue",
					this.SampleValue
				},
				{
					"ExecutionContext",
					this.ExecutionContext
				},
				{
					"FailureContext",
					this.FailureContext
				},
				{
					"FailureCategory",
					this.FailureCategory
				},
				{
					"ScopeName",
					this.ScopeName
				},
				{
					"ScopeType",
					this.ScopeType
				},
				{
					"HealthSetName",
					this.HealthSetName
				},
				{
					"Data",
					this.Data
				},
				{
					"Version",
					this.Version
				}
			};
		}

		// Token: 0x040001F3 RID: 499
		internal const int ContextColumnsSize = 4000;

		// Token: 0x040001F4 RID: 500
		private static int schemaVersion = 65536;

		// Token: 0x040001F5 RID: 501
		private static ProbeResultIdGenerator idGenerator = new ProbeResultIdGenerator();
	}
}
