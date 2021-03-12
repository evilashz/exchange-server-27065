using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000038 RID: 56
	[Table]
	public sealed class MaintenanceDefinition : WorkDefinition, IPersistence
	{
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x00011010 File Offset: 0x0000F210
		public override string SecondaryExternalStorageKey
		{
			get
			{
				return string.Format("{0}_{1}_{2}_{3}", new object[]
				{
					Settings.InstanceName,
					Settings.MachineName,
					base.DeploymentSourceName,
					this.Id
				});
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x00011056 File Offset: 0x0000F256
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x0001105E File Offset: 0x0000F25E
		[Column]
		public int MaxRestartRequestAllowedPerHour { get; set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x00011067 File Offset: 0x0000F267
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x0001106F File Offset: 0x0000F26F
		[Column(IsPrimaryKey = true, IsDbGenerated = true)]
		public override int Id { get; internal set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x00011078 File Offset: 0x0000F278
		// (set) Token: 0x06000450 RID: 1104 RVA: 0x00011080 File Offset: 0x0000F280
		[Column]
		public override string AssemblyPath { get; set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x00011089 File Offset: 0x0000F289
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x00011091 File Offset: 0x0000F291
		[Column]
		public override string TypeName { get; set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x0001109A File Offset: 0x0000F29A
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x000110A2 File Offset: 0x0000F2A2
		[Column]
		public override string Name { get; set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x000110AB File Offset: 0x0000F2AB
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x000110B3 File Offset: 0x0000F2B3
		[Column]
		public override string WorkItemVersion { get; set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x000110BC File Offset: 0x0000F2BC
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x000110C4 File Offset: 0x0000F2C4
		[Column]
		public override string ServiceName { get; set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x000110CD File Offset: 0x0000F2CD
		// (set) Token: 0x0600045A RID: 1114 RVA: 0x000110D5 File Offset: 0x0000F2D5
		[Column]
		public override int DeploymentId { get; set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x000110DE File Offset: 0x0000F2DE
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x000110E6 File Offset: 0x0000F2E6
		[Column]
		public override string ExecutionLocation { get; set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x000110EF File Offset: 0x0000F2EF
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x000110F7 File Offset: 0x0000F2F7
		[Column]
		public override DateTime CreatedTime { get; internal set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x00011100 File Offset: 0x0000F300
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x00011108 File Offset: 0x0000F308
		[Column]
		public override bool Enabled { get; set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x00011111 File Offset: 0x0000F311
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x00011119 File Offset: 0x0000F319
		[Column]
		public override string TargetPartition { get; set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00011122 File Offset: 0x0000F322
		// (set) Token: 0x06000464 RID: 1124 RVA: 0x0001112A File Offset: 0x0000F32A
		[Column]
		public override string TargetGroup { get; set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x00011133 File Offset: 0x0000F333
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x0001113B File Offset: 0x0000F33B
		[Column]
		public override string TargetResource { get; set; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00011144 File Offset: 0x0000F344
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x0001114C File Offset: 0x0000F34C
		[Column]
		public override string TargetExtension { get; set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x00011155 File Offset: 0x0000F355
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x0001115D File Offset: 0x0000F35D
		[Column]
		public override string TargetVersion { get; set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x00011166 File Offset: 0x0000F366
		// (set) Token: 0x0600046C RID: 1132 RVA: 0x0001116E File Offset: 0x0000F36E
		[Column]
		public override int RecurrenceIntervalSeconds { get; set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x00011177 File Offset: 0x0000F377
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x0001117F File Offset: 0x0000F37F
		[Column]
		public override int TimeoutSeconds { get; set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x00011188 File Offset: 0x0000F388
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x00011190 File Offset: 0x0000F390
		[Column]
		public override DateTime StartTime { get; set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x00011199 File Offset: 0x0000F399
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x000111A1 File Offset: 0x0000F3A1
		[Column]
		public override DateTime UpdateTime { get; internal set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x000111AA File Offset: 0x0000F3AA
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x000111B2 File Offset: 0x0000F3B2
		[Column]
		public override int MaxRetryAttempts { get; set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x000111BB File Offset: 0x0000F3BB
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x000111C3 File Offset: 0x0000F3C3
		[Column]
		public override string ExtensionAttributes { get; internal set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x000111CC File Offset: 0x0000F3CC
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x000111D4 File Offset: 0x0000F3D4
		[Column]
		public override int CreatedById { get; set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x000111DD File Offset: 0x0000F3DD
		public override string InternalStorageKey
		{
			get
			{
				return base.DeploymentSourceName;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x000111E5 File Offset: 0x0000F3E5
		// (set) Token: 0x0600047B RID: 1147 RVA: 0x000111ED File Offset: 0x0000F3ED
		[Column]
		internal override int Version { get; set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x000111F6 File Offset: 0x0000F3F6
		internal int ProbeDefinitionScopeTokenCount
		{
			get
			{
				return this.probeDefinitionScopeTokenCount;
			}
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x000111FE File Offset: 0x0000F3FE
		internal void SetGeneratedDefinitionScopeTokenCount(int numberOfScopeTokens)
		{
			this.probeDefinitionScopeTokenCount = numberOfScopeTokens;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00011207 File Offset: 0x0000F407
		internal override WorkItemResult CreateResult()
		{
			return new MaintenanceResult(this);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0001120F File Offset: 0x0000F40F
		internal string GetKeyForGeneratedItems()
		{
			return string.Format("{0}_{1}", this.Id, this.TargetVersion);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0001122C File Offset: 0x0000F42C
		internal string GetKeyForGeneratedItems(string state)
		{
			return string.Format("{0}_{1}", this.Id, state);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00011244 File Offset: 0x0000F444
		internal void SignalAndRemove(Action remover)
		{
			lock (this.definitionLock)
			{
				this.Enabled = false;
				remover();
			}
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0001128C File Offset: 0x0000F48C
		internal void SignalAndRegenerate(Action regenerator)
		{
			lock (this.definitionLock)
			{
				if (this.Enabled)
				{
					regenerator();
				}
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x000112D4 File Offset: 0x0000F4D4
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x000112DC File Offset: 0x0000F4DC
		public LocalDataAccessMetaData LocalDataAccessMetaData { get; private set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x000112E5 File Offset: 0x0000F4E5
		internal static int SchemaVersion
		{
			get
			{
				return MaintenanceDefinition.schemaVersion;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x000112EC File Offset: 0x0000F4EC
		// (set) Token: 0x06000487 RID: 1159 RVA: 0x000112F3 File Offset: 0x0000F4F3
		internal static IEnumerable<WorkDefinitionOverride> GlobalOverrides { get; set; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x000112FB File Offset: 0x0000F4FB
		// (set) Token: 0x06000489 RID: 1161 RVA: 0x00011302 File Offset: 0x0000F502
		internal static IEnumerable<WorkDefinitionOverride> LocalOverrides { get; set; }

		// Token: 0x0600048A RID: 1162 RVA: 0x0001130A File Offset: 0x0000F50A
		public void Initialize(Dictionary<string, string> propertyBag, LocalDataAccessMetaData metaData)
		{
			this.LocalDataAccessMetaData = metaData;
			this.SetProperties(propertyBag);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0001131C File Offset: 0x0000F51C
		public void SetProperties(Dictionary<string, string> propertyBag)
		{
			string text;
			if (propertyBag.TryGetValue("MaxRestartRequestAllowedPerHour", out text) && !string.IsNullOrEmpty(text))
			{
				this.MaxRestartRequestAllowedPerHour = int.Parse(text);
			}
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
			if (propertyBag.TryGetValue("CreatedById", out text) && !string.IsNullOrEmpty(text))
			{
				this.CreatedById = int.Parse(text);
			}
			if (propertyBag.TryGetValue("Version", out text) && !string.IsNullOrEmpty(text))
			{
				this.Version = int.Parse(text);
			}
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0001162C File Offset: 0x0000F82C
		public void Write(Action<IPersistence> preWriteHandler = null)
		{
			DefinitionIdGenerator<MaintenanceDefinition>.AssignId(this);
			Update<MaintenanceDefinition>.ApplyUpdates(this);
			if (MaintenanceDefinition.GlobalOverrides != null)
			{
				foreach (WorkDefinitionOverride definitionOverride in MaintenanceDefinition.GlobalOverrides)
				{
					definitionOverride.TryApplyTo(this, base.TraceContext);
				}
			}
			if (MaintenanceDefinition.LocalOverrides != null)
			{
				foreach (WorkDefinitionOverride definitionOverride2 in MaintenanceDefinition.LocalOverrides)
				{
					definitionOverride2.TryApplyTo(this, base.TraceContext);
				}
			}
			if (preWriteHandler != null)
			{
				preWriteHandler(this);
			}
			NativeMethods.MaintenanceDefinitionUnmanaged maintenanceDefinitionUnmanaged = this.ToUnmanaged();
			NativeMethods.WriteMaintenanceDefinition(ref maintenanceDefinitionUnmanaged);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x000116F8 File Offset: 0x0000F8F8
		internal NativeMethods.MaintenanceDefinitionUnmanaged ToUnmanaged()
		{
			return new NativeMethods.MaintenanceDefinitionUnmanaged
			{
				MaxRestartRequestAllowedPerHour = this.MaxRestartRequestAllowedPerHour,
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
				CreatedById = this.CreatedById,
				Version = this.Version
			};
		}

		// Token: 0x0400031E RID: 798
		private const string generatedItemKeyFormat = "{0}_{1}";

		// Token: 0x0400031F RID: 799
		private object definitionLock = new object();

		// Token: 0x04000320 RID: 800
		private int probeDefinitionScopeTokenCount = 3;

		// Token: 0x04000321 RID: 801
		private static int schemaVersion = 65536;
	}
}
