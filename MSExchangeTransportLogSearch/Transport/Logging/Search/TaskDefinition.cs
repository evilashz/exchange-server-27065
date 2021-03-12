using System;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x0200003F RID: 63
	internal class TaskDefinition<T> where T : struct
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000944A File Offset: 0x0000764A
		internal string TaskPropertyName
		{
			get
			{
				return this.eventDefinition.TaskPropertyName;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00009457 File Offset: 0x00007657
		internal string TaskName
		{
			get
			{
				return this.taskName;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600014F RID: 335 RVA: 0x0000945F File Offset: 0x0000765F
		internal string IdPropertyName
		{
			get
			{
				return this.eventDefinition.IdPropertyName;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000150 RID: 336 RVA: 0x0000946C File Offset: 0x0000766C
		internal string RtsPropertyName
		{
			get
			{
				return this.eventDefinition.RtsPropertyName;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00009479 File Offset: 0x00007679
		internal string OperationPropertyName
		{
			get
			{
				return this.eventDefinition.OperationPropertyName;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00009486 File Offset: 0x00007686
		internal string[] OperationNames
		{
			get
			{
				return this.operationNames;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000948E File Offset: 0x0000768E
		internal string[] OperationNamesWithCountSuffix
		{
			get
			{
				return this.operationNamesWithCountSuffix;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00009496 File Offset: 0x00007696
		internal string OperationTypePropertyName
		{
			get
			{
				return this.eventDefinition.OperationTypePropertyName;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000155 RID: 341 RVA: 0x000094A3 File Offset: 0x000076A3
		internal string StartOperationName
		{
			get
			{
				return this.startOperationName;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000156 RID: 342 RVA: 0x000094AB File Offset: 0x000076AB
		internal string EndOperationName
		{
			get
			{
				return this.endOperationName;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000157 RID: 343 RVA: 0x000094B3 File Offset: 0x000076B3
		internal int OperationCount
		{
			get
			{
				return this.operationNames.Length;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000158 RID: 344 RVA: 0x000094BD File Offset: 0x000076BD
		internal string CountPropertyName
		{
			get
			{
				return this.eventDefinition.CountPropertyName;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000159 RID: 345 RVA: 0x000094CA File Offset: 0x000076CA
		internal string ErrorPropertyName
		{
			get
			{
				return this.eventDefinition.ErrorPropertyName;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600015A RID: 346 RVA: 0x000094D7 File Offset: 0x000076D7
		internal int[] LatencyDistributionBoundaries
		{
			get
			{
				return this.latencyDistributionBoundaries;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600015B RID: 347 RVA: 0x000094DF File Offset: 0x000076DF
		internal HealthMonitoringEvents LatencyEvent
		{
			get
			{
				return this.latencyEvent;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600015C RID: 348 RVA: 0x000094E7 File Offset: 0x000076E7
		internal HealthMonitoringEvents ErrorEvent
		{
			get
			{
				return this.errorEvent;
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000094F0 File Offset: 0x000076F0
		internal TaskDefinition(EventDefinition eventDefinition, string taskName, string startOperationName, string endOperationName, int[] latencyDistributionBoundaries, HealthMonitoringEvents latencyEvent, HealthMonitoringEvents errorEvent)
		{
			this.eventDefinition = eventDefinition;
			this.taskName = taskName;
			this.operationNames = Enum.GetNames(typeof(T));
			this.operationNamesWithCountSuffix = new string[this.operationNames.Length];
			for (int i = 0; i < this.operationNames.Length; i++)
			{
				this.operationNamesWithCountSuffix[i] = this.operationNames[i] + "Ct";
			}
			this.startOperationName = startOperationName;
			this.endOperationName = endOperationName;
			this.latencyDistributionBoundaries = latencyDistributionBoundaries;
			this.latencyEvent = latencyEvent;
			this.errorEvent = errorEvent;
		}

		// Token: 0x040000F7 RID: 247
		private EventDefinition eventDefinition;

		// Token: 0x040000F8 RID: 248
		private string taskName;

		// Token: 0x040000F9 RID: 249
		private string startOperationName;

		// Token: 0x040000FA RID: 250
		private string endOperationName;

		// Token: 0x040000FB RID: 251
		private string[] operationNames;

		// Token: 0x040000FC RID: 252
		private string[] operationNamesWithCountSuffix;

		// Token: 0x040000FD RID: 253
		private int[] latencyDistributionBoundaries;

		// Token: 0x040000FE RID: 254
		private HealthMonitoringEvents latencyEvent;

		// Token: 0x040000FF RID: 255
		private HealthMonitoringEvents errorEvent;
	}
}
