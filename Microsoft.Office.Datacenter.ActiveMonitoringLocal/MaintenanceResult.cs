using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000035 RID: 53
	[Table]
	public sealed class MaintenanceResult : WorkItemResult, IPersistence, IWorkItemResultSerialization
	{
		// Token: 0x060003D5 RID: 981 RVA: 0x0000FDA4 File Offset: 0x0000DFA4
		public MaintenanceResult(WorkDefinition definition) : base(definition)
		{
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000FDAD File Offset: 0x0000DFAD
		public MaintenanceResult()
		{
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x0000FDB5 File Offset: 0x0000DFB5
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x0000FDBD File Offset: 0x0000DFBD
		[Column(IsPrimaryKey = true, IsDbGenerated = true)]
		public override int ResultId { get; protected internal set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0000FDC6 File Offset: 0x0000DFC6
		// (set) Token: 0x060003DA RID: 986 RVA: 0x0000FDCE File Offset: 0x0000DFCE
		[Column]
		public override string ServiceName { get; set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000FDD7 File Offset: 0x0000DFD7
		// (set) Token: 0x060003DC RID: 988 RVA: 0x0000FDDF File Offset: 0x0000DFDF
		[Column]
		public override bool IsNotified { get; set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0000FDE8 File Offset: 0x0000DFE8
		// (set) Token: 0x060003DE RID: 990 RVA: 0x0000FDF0 File Offset: 0x0000DFF0
		[Column]
		public override string ResultName { get; set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0000FDF9 File Offset: 0x0000DFF9
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x0000FE01 File Offset: 0x0000E001
		[Column]
		public override int WorkItemId { get; internal set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0000FE0A File Offset: 0x0000E00A
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x0000FE12 File Offset: 0x0000E012
		[Column]
		public override int DeploymentId { get; internal set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0000FE1B File Offset: 0x0000E01B
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x0000FE23 File Offset: 0x0000E023
		[Column]
		public override string MachineName { get; internal set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0000FE2C File Offset: 0x0000E02C
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x0000FE34 File Offset: 0x0000E034
		[Column]
		public override string Error { get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000FE3D File Offset: 0x0000E03D
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x0000FE45 File Offset: 0x0000E045
		[Column]
		public override string Exception { get; set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000FE4E File Offset: 0x0000E04E
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x0000FE56 File Offset: 0x0000E056
		[Column]
		public override byte RetryCount { get; internal set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000FE5F File Offset: 0x0000E05F
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x0000FE67 File Offset: 0x0000E067
		[Column]
		public override string StateAttribute1 { get; set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000FE70 File Offset: 0x0000E070
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x0000FE78 File Offset: 0x0000E078
		[Column]
		public override string StateAttribute2 { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000FE81 File Offset: 0x0000E081
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x0000FE89 File Offset: 0x0000E089
		[Column]
		public override string StateAttribute3 { get; set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0000FE92 File Offset: 0x0000E092
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x0000FE9A File Offset: 0x0000E09A
		[Column]
		public override string StateAttribute4 { get; set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0000FEA3 File Offset: 0x0000E0A3
		// (set) Token: 0x060003F4 RID: 1012 RVA: 0x0000FEAB File Offset: 0x0000E0AB
		[Column]
		public override string StateAttribute5 { get; set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0000FEB4 File Offset: 0x0000E0B4
		// (set) Token: 0x060003F6 RID: 1014 RVA: 0x0000FEBC File Offset: 0x0000E0BC
		[Column]
		public override double StateAttribute6 { get; set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0000FEC5 File Offset: 0x0000E0C5
		// (set) Token: 0x060003F8 RID: 1016 RVA: 0x0000FECD File Offset: 0x0000E0CD
		[Column]
		public override double StateAttribute7 { get; set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x0000FED6 File Offset: 0x0000E0D6
		// (set) Token: 0x060003FA RID: 1018 RVA: 0x0000FEDE File Offset: 0x0000E0DE
		[Column]
		public override double StateAttribute8 { get; set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0000FEE7 File Offset: 0x0000E0E7
		// (set) Token: 0x060003FC RID: 1020 RVA: 0x0000FEEF File Offset: 0x0000E0EF
		[Column]
		public override double StateAttribute9 { get; set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x0000FEF8 File Offset: 0x0000E0F8
		// (set) Token: 0x060003FE RID: 1022 RVA: 0x0000FF00 File Offset: 0x0000E100
		[Column]
		public override double StateAttribute10 { get; set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0000FF09 File Offset: 0x0000E109
		// (set) Token: 0x06000400 RID: 1024 RVA: 0x0000FF11 File Offset: 0x0000E111
		[Column]
		public override ResultType ResultType { get; set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0000FF1A File Offset: 0x0000E11A
		// (set) Token: 0x06000402 RID: 1026 RVA: 0x0000FF22 File Offset: 0x0000E122
		[Column]
		public override int ExecutionId { get; protected set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x0000FF2B File Offset: 0x0000E12B
		// (set) Token: 0x06000404 RID: 1028 RVA: 0x0000FF33 File Offset: 0x0000E133
		[Column]
		public override DateTime ExecutionStartTime { get; set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0000FF3C File Offset: 0x0000E13C
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x0000FF44 File Offset: 0x0000E144
		[Column]
		public override DateTime ExecutionEndTime { get; set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x0000FF4D File Offset: 0x0000E14D
		// (set) Token: 0x06000408 RID: 1032 RVA: 0x0000FF55 File Offset: 0x0000E155
		[Column]
		public override byte PoisonedCount { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0000FF5E File Offset: 0x0000E15E
		// (set) Token: 0x0600040A RID: 1034 RVA: 0x0000FF66 File Offset: 0x0000E166
		[Column]
		internal override int Version { get; set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x0000FF6F File Offset: 0x0000E16F
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x0000FF77 File Offset: 0x0000E177
		public LocalDataAccessMetaData LocalDataAccessMetaData { get; private set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0000FF80 File Offset: 0x0000E180
		internal static int SchemaVersion
		{
			get
			{
				return MaintenanceResult.schemaVersion;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0000FF87 File Offset: 0x0000E187
		// (set) Token: 0x0600040F RID: 1039 RVA: 0x0000FF8E File Offset: 0x0000E18E
		internal static Action<MaintenanceResult> ResultWriter { private get; set; }

		// Token: 0x06000410 RID: 1040 RVA: 0x0000FF96 File Offset: 0x0000E196
		public void Initialize(Dictionary<string, string> propertyBag, LocalDataAccessMetaData metaData)
		{
			this.LocalDataAccessMetaData = metaData;
			this.SetProperties(propertyBag);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000FFA8 File Offset: 0x0000E1A8
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
			if (propertyBag.TryGetValue("Version", out text) && !string.IsNullOrEmpty(text))
			{
				this.Version = int.Parse(text);
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00010312 File Offset: 0x0000E512
		public override void AssignResultId()
		{
			if (this.ResultId == 0)
			{
				this.ResultId = MaintenanceResult.idGenerator.NextId();
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0001032C File Offset: 0x0000E52C
		public void Write(Action<IPersistence> preWriteHandler = null)
		{
			this.AssignResultId();
			if (preWriteHandler != null)
			{
				preWriteHandler(this);
			}
			if (MaintenanceResult.ResultWriter != null)
			{
				MaintenanceResult.ResultWriter(this);
				return;
			}
			NativeMethods.MaintenanceResultUnmanaged maintenanceResultUnmanaged = this.ToUnmanaged();
			ResultSeverityLevel severity = CrimsonHelper.ConvertResultTypeToSeverityLevel(this.ResultType);
			NativeMethods.WriteMaintenanceResult(ref maintenanceResultUnmanaged, severity);
			LocalDataAccess.MaintenanceResultLogger.LogEvent(DateTime.UtcNow, this.ToDictionary());
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0001038D File Offset: 0x0000E58D
		public string Serialize()
		{
			return CrimsonHelper.Serialize(this.ToDictionary(), false);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0001039C File Offset: 0x0000E59C
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
				this.Version = int.Parse(array[25]);
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0001060C File Offset: 0x0000E80C
		internal NativeMethods.MaintenanceResultUnmanaged ToUnmanaged()
		{
			return new NativeMethods.MaintenanceResultUnmanaged
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
				Version = this.Version
			};
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000107D8 File Offset: 0x0000E9D8
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
					"Version",
					this.Version
				}
			};
		}

		// Token: 0x040002FB RID: 763
		private static int schemaVersion = 65536;

		// Token: 0x040002FC RID: 764
		private static MaintenanceResultIdGenerator idGenerator = new MaintenanceResultIdGenerator();
	}
}
