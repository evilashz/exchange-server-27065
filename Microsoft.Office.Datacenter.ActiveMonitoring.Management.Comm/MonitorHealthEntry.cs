using System;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring.Management.Common
{
	// Token: 0x02000005 RID: 5
	[Serializable]
	public class MonitorHealthEntry : ConfigurableObject
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00002C6D File Offset: 0x00000E6D
		public MonitorHealthEntry() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002C7C File Offset: 0x00000E7C
		internal MonitorHealthEntry(string server, RpcGetMonitorHealthStatus.RpcMonitorHealthEntry rpcEntry) : this()
		{
			this.Server = server;
			this.Name = rpcEntry.Name;
			this.TargetResource = rpcEntry.TargetResource;
			this.Description = rpcEntry.Description;
			this.IsHaImpacting = rpcEntry.IsHaImpacting;
			this.RecurranceInterval = rpcEntry.RecurranceInterval;
			this.DefinitionCreatedTime = rpcEntry.DefinitionCreatedTime.ToLocalTime();
			this.HealthSetName = rpcEntry.HealthSetName;
			this.HealthSetDescription = rpcEntry.HealthSetDescription;
			this.HealthGroupName = rpcEntry.HealthGroupName;
			this.ServerComponentName = rpcEntry.ServerComponentName;
			this.AlertValue = this.ParseEnum<MonitorAlertState>(rpcEntry.AlertValue, MonitorAlertState.Unknown);
			this.CurrentHealthSetState = this.ParseEnum<MonitorServerComponentState>(rpcEntry.CurrentHealthSetState, MonitorServerComponentState.Unknown);
			this.FirstAlertObservedTime = rpcEntry.FirstAlertObservedTime.ToLocalTime();
			this.LastTransitionTime = rpcEntry.LastTransitionTime.ToLocalTime();
			this.LastExecutionTime = rpcEntry.LastExecutionTime.ToLocalTime();
			this.LastExecutionResult = this.ParseEnum<ResultType>(rpcEntry.LastExecutionResult, ResultType.Abandoned);
			this.WorkItemId = rpcEntry.WorkItemId;
			this.ResultId = rpcEntry.ResultId;
			this.IsStale = MonitorHealthEntry.IsEntryStale(rpcEntry);
			this.ServicePriority = rpcEntry.ServicePriority;
			this.Error = rpcEntry.Error;
			this.Exception = rpcEntry.Exception;
			this.IsNotified = rpcEntry.IsNotified;
			this.LastFailedProbeId = rpcEntry.LastFailedProbeId;
			this.LastFailedProbeResultId = rpcEntry.LastFailedProbeResultId;
			this[SimpleProviderObjectSchema.Identity] = new MonitorHealthEntry.MonitorHealthEntryId(this.HealthSetName, this.Name, this.TargetResource);
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002E19 File Offset: 0x00001019
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002E2B File Offset: 0x0000102B
		public string Server
		{
			get
			{
				return (string)this[MonitorHealthEntrySchema.Server];
			}
			private set
			{
				this[MonitorHealthEntrySchema.Server] = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002E39 File Offset: 0x00001039
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002E4B File Offset: 0x0000104B
		public MonitorServerComponentState CurrentHealthSetState
		{
			get
			{
				return (MonitorServerComponentState)this[MonitorHealthEntrySchema.CurrentHealthSetState];
			}
			private set
			{
				this[MonitorHealthEntrySchema.CurrentHealthSetState] = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002E5E File Offset: 0x0000105E
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002E70 File Offset: 0x00001070
		public string Name
		{
			get
			{
				return (string)this[MonitorHealthEntrySchema.Name];
			}
			private set
			{
				this[MonitorHealthEntrySchema.Name] = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002E7E File Offset: 0x0000107E
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002E90 File Offset: 0x00001090
		public string TargetResource
		{
			get
			{
				return (string)this[MonitorHealthEntrySchema.TargetResource];
			}
			private set
			{
				this[MonitorHealthEntrySchema.TargetResource] = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002E9E File Offset: 0x0000109E
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002EB0 File Offset: 0x000010B0
		public string HealthSetName
		{
			get
			{
				return (string)this[MonitorHealthEntrySchema.HealthSetName];
			}
			private set
			{
				this[MonitorHealthEntrySchema.HealthSetName] = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002EBE File Offset: 0x000010BE
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002ED0 File Offset: 0x000010D0
		public string HealthGroupName
		{
			get
			{
				return (string)this[MonitorHealthEntrySchema.HealthGroupName];
			}
			private set
			{
				this[MonitorHealthEntrySchema.HealthGroupName] = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002EDE File Offset: 0x000010DE
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002EF0 File Offset: 0x000010F0
		public MonitorAlertState AlertValue
		{
			get
			{
				return (MonitorAlertState)this[MonitorHealthEntrySchema.AlertValue];
			}
			private set
			{
				this[MonitorHealthEntrySchema.AlertValue] = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002F03 File Offset: 0x00001103
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002F15 File Offset: 0x00001115
		public DateTime FirstAlertObservedTime
		{
			get
			{
				return (DateTime)this[MonitorHealthEntrySchema.FirstAlertObservedTime];
			}
			private set
			{
				this[MonitorHealthEntrySchema.FirstAlertObservedTime] = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002F28 File Offset: 0x00001128
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002F3A File Offset: 0x0000113A
		public string Description
		{
			get
			{
				return (string)this[MonitorHealthEntrySchema.Description];
			}
			private set
			{
				this[MonitorHealthEntrySchema.Description] = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002F48 File Offset: 0x00001148
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002F64 File Offset: 0x00001164
		public bool IsHaImpacting
		{
			get
			{
				return (bool)(this[MonitorHealthEntrySchema.IsHaImpacting] ?? false);
			}
			private set
			{
				this[MonitorHealthEntrySchema.IsHaImpacting] = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002F77 File Offset: 0x00001177
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002F89 File Offset: 0x00001189
		public int RecurranceInterval
		{
			get
			{
				return (int)this[MonitorHealthEntrySchema.RecurranceInterval];
			}
			private set
			{
				this[MonitorHealthEntrySchema.RecurranceInterval] = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002F9C File Offset: 0x0000119C
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002FAE File Offset: 0x000011AE
		public DateTime DefinitionCreatedTime
		{
			get
			{
				return (DateTime)this[MonitorHealthEntrySchema.DefinitionCreatedTime];
			}
			private set
			{
				this[MonitorHealthEntrySchema.DefinitionCreatedTime] = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002FC1 File Offset: 0x000011C1
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002FD3 File Offset: 0x000011D3
		public string HealthSetDescription
		{
			get
			{
				return (string)this[MonitorHealthEntrySchema.HealthSetDescription];
			}
			private set
			{
				this[MonitorHealthEntrySchema.HealthSetDescription] = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002FE1 File Offset: 0x000011E1
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002FF3 File Offset: 0x000011F3
		public string ServerComponentName
		{
			get
			{
				return (string)this[MonitorHealthEntrySchema.ServerComponentName];
			}
			private set
			{
				this[MonitorHealthEntrySchema.ServerComponentName] = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003001 File Offset: 0x00001201
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00003013 File Offset: 0x00001213
		public DateTime LastTransitionTime
		{
			get
			{
				return (DateTime)this[MonitorHealthEntrySchema.LastTransitionTime];
			}
			private set
			{
				this[MonitorHealthEntrySchema.LastTransitionTime] = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003026 File Offset: 0x00001226
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00003038 File Offset: 0x00001238
		public DateTime LastExecutionTime
		{
			get
			{
				return (DateTime)this[MonitorHealthEntrySchema.LastExecutionTime];
			}
			private set
			{
				this[MonitorHealthEntrySchema.LastExecutionTime] = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000059 RID: 89 RVA: 0x0000304B File Offset: 0x0000124B
		// (set) Token: 0x0600005A RID: 90 RVA: 0x0000305D File Offset: 0x0000125D
		public ResultType LastExecutionResult
		{
			get
			{
				return (ResultType)this[MonitorHealthEntrySchema.LastExecutionResult];
			}
			private set
			{
				this[MonitorHealthEntrySchema.LastExecutionResult] = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00003070 File Offset: 0x00001270
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00003082 File Offset: 0x00001282
		public int ResultId
		{
			get
			{
				return (int)this[MonitorHealthEntrySchema.ResultId];
			}
			private set
			{
				this[MonitorHealthEntrySchema.ResultId] = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003095 File Offset: 0x00001295
		// (set) Token: 0x0600005E RID: 94 RVA: 0x000030A7 File Offset: 0x000012A7
		public int WorkItemId
		{
			get
			{
				return (int)this[MonitorHealthEntrySchema.WorkItemId];
			}
			private set
			{
				this[MonitorHealthEntrySchema.WorkItemId] = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000030BA File Offset: 0x000012BA
		// (set) Token: 0x06000060 RID: 96 RVA: 0x000030D6 File Offset: 0x000012D6
		public bool IsStale
		{
			get
			{
				return (bool)(this[MonitorHealthEntrySchema.IsStale] ?? false);
			}
			private set
			{
				this[MonitorHealthEntrySchema.IsStale] = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000030E9 File Offset: 0x000012E9
		// (set) Token: 0x06000062 RID: 98 RVA: 0x000030FB File Offset: 0x000012FB
		public string Error
		{
			get
			{
				return (string)this[MonitorHealthEntrySchema.Error];
			}
			private set
			{
				this[MonitorHealthEntrySchema.Error] = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003109 File Offset: 0x00001309
		// (set) Token: 0x06000064 RID: 100 RVA: 0x0000311B File Offset: 0x0000131B
		public string Exception
		{
			get
			{
				return (string)this[MonitorHealthEntrySchema.Exception];
			}
			private set
			{
				this[MonitorHealthEntrySchema.Exception] = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003129 File Offset: 0x00001329
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00003145 File Offset: 0x00001345
		public bool IsNotified
		{
			get
			{
				return (bool)(this[MonitorHealthEntrySchema.IsNotified] ?? false);
			}
			private set
			{
				this[MonitorHealthEntrySchema.IsNotified] = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003158 File Offset: 0x00001358
		// (set) Token: 0x06000068 RID: 104 RVA: 0x0000316A File Offset: 0x0000136A
		public int LastFailedProbeId
		{
			get
			{
				return (int)this[MonitorHealthEntrySchema.LastFailedProbeId];
			}
			private set
			{
				this[MonitorHealthEntrySchema.LastFailedProbeId] = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000069 RID: 105 RVA: 0x0000317D File Offset: 0x0000137D
		// (set) Token: 0x0600006A RID: 106 RVA: 0x0000318F File Offset: 0x0000138F
		public int LastFailedProbeResultId
		{
			get
			{
				return (int)this[MonitorHealthEntrySchema.LastFailedProbeResultId];
			}
			private set
			{
				this[MonitorHealthEntrySchema.LastFailedProbeResultId] = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000031A2 File Offset: 0x000013A2
		// (set) Token: 0x0600006C RID: 108 RVA: 0x000031B4 File Offset: 0x000013B4
		public int ServicePriority
		{
			get
			{
				return (int)this[MonitorHealthEntrySchema.ServicePriority];
			}
			private set
			{
				this[MonitorHealthEntrySchema.ServicePriority] = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000031C7 File Offset: 0x000013C7
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000031CE File Offset: 0x000013CE
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return MonitorHealthEntry.schema;
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000031D8 File Offset: 0x000013D8
		internal static bool IsEntryStale(RpcGetMonitorHealthStatus.RpcMonitorHealthEntry entry)
		{
			bool result = false;
			DateTime d = DateTime.UtcNow.ToLocalTime();
			if (entry.RecurranceInterval > 0)
			{
				DateTime dateTime = entry.LastExecutionTime;
				if (entry.DefinitionCreatedTime > dateTime)
				{
					dateTime = entry.DefinitionCreatedTime;
				}
				TimeSpan t = TimeSpan.FromSeconds((double)(entry.RecurranceInterval + entry.TimeoutSeconds + 120));
				if (dateTime < d - t)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003244 File Offset: 0x00001444
		internal T ParseEnum<T>(string strEnum, T defaultValue) where T : struct
		{
			T result = defaultValue;
			if (!string.IsNullOrEmpty(strEnum) && !Enum.TryParse<T>(strEnum, out result))
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x0400001A RID: 26
		private const int StaleGracePeriodInSeconds = 120;

		// Token: 0x0400001B RID: 27
		private static MonitorHealthEntrySchema schema = ObjectSchema.GetInstance<MonitorHealthEntrySchema>();

		// Token: 0x02000006 RID: 6
		[Serializable]
		public class MonitorHealthEntryId : ObjectId
		{
			// Token: 0x06000072 RID: 114 RVA: 0x00003274 File Offset: 0x00001474
			public MonitorHealthEntryId(string healthSetName, string monitorName, string targetResource)
			{
				this.identity = string.Format("{0}\\{1}\\{2}", healthSetName, monitorName, targetResource);
			}

			// Token: 0x06000073 RID: 115 RVA: 0x0000328F File Offset: 0x0000148F
			public override string ToString()
			{
				return this.identity;
			}

			// Token: 0x06000074 RID: 116 RVA: 0x00003297 File Offset: 0x00001497
			public override byte[] GetBytes()
			{
				return Encoding.Unicode.GetBytes(this.ToString());
			}

			// Token: 0x0400001C RID: 28
			private readonly string identity;
		}
	}
}
