using System;

namespace Microsoft.Exchange.Hygiene.Data.BackgroundJobBackend
{
	// Token: 0x0200003A RID: 58
	internal sealed class JobDefinition : BackgroundJobBackendBase
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00007526 File Offset: 0x00005726
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x00007538 File Offset: 0x00005738
		public Guid BackgroundJobId
		{
			get
			{
				return (Guid)this[JobDefinition.BackgroundJobIdProperty];
			}
			set
			{
				this[JobDefinition.BackgroundJobIdProperty] = value;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000754B File Offset: 0x0000574B
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x0000755D File Offset: 0x0000575D
		public string Name
		{
			get
			{
				return (string)this[JobDefinition.NameProperty];
			}
			set
			{
				this[JobDefinition.NameProperty] = value;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000756B File Offset: 0x0000576B
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x0000757D File Offset: 0x0000577D
		public string Path
		{
			get
			{
				return (string)this[JobDefinition.PathProperty];
			}
			set
			{
				this[JobDefinition.PathProperty] = value;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x0000758B File Offset: 0x0000578B
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x0000759D File Offset: 0x0000579D
		public string CommandLine
		{
			get
			{
				return (string)this[JobDefinition.CommandLineProperty];
			}
			set
			{
				this[JobDefinition.CommandLineProperty] = value;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x000075AB File Offset: 0x000057AB
		// (set) Token: 0x060001EA RID: 490 RVA: 0x000075BD File Offset: 0x000057BD
		public string Description
		{
			get
			{
				return (string)this[JobDefinition.DescriptionProperty];
			}
			set
			{
				this[JobDefinition.DescriptionProperty] = value;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001EB RID: 491 RVA: 0x000075CB File Offset: 0x000057CB
		// (set) Token: 0x060001EC RID: 492 RVA: 0x000075DD File Offset: 0x000057DD
		public Guid RoleId
		{
			get
			{
				return (Guid)this[JobDefinition.RoleIdProperty];
			}
			set
			{
				this[JobDefinition.RoleIdProperty] = value;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001ED RID: 493 RVA: 0x000075F0 File Offset: 0x000057F0
		// (set) Token: 0x060001EE RID: 494 RVA: 0x00007602 File Offset: 0x00005802
		public bool SingleInstancePerMachine
		{
			get
			{
				return (bool)this[JobDefinition.SingleInstancePerMachineProperty];
			}
			set
			{
				this[JobDefinition.SingleInstancePerMachineProperty] = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00007615 File Offset: 0x00005815
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x00007627 File Offset: 0x00005827
		public SchedulingStrategyType SchedulingStrategy
		{
			get
			{
				return (SchedulingStrategyType)this[JobDefinition.SchedulingStrategyProperty];
			}
			set
			{
				this[JobDefinition.SchedulingStrategyProperty] = (byte)value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000763A File Offset: 0x0000583A
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x0000764C File Offset: 0x0000584C
		public int Timeout
		{
			get
			{
				return (int)this[JobDefinition.TimeoutProperty];
			}
			set
			{
				this[JobDefinition.TimeoutProperty] = value;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000765F File Offset: 0x0000585F
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x00007671 File Offset: 0x00005871
		public byte MaxLocalRetryCount
		{
			get
			{
				return (byte)this[JobDefinition.MaxLocalRetryCountProperty];
			}
			set
			{
				this[JobDefinition.MaxLocalRetryCountProperty] = value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00007684 File Offset: 0x00005884
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x00007696 File Offset: 0x00005896
		public short MaxFailoverCount
		{
			get
			{
				return (short)this[JobDefinition.MaxFailoverCountProperty];
			}
			set
			{
				this[JobDefinition.MaxFailoverCountProperty] = value;
			}
		}

		// Token: 0x04000148 RID: 328
		internal static readonly BackgroundJobBackendPropertyDefinition BackgroundJobIdProperty = ScheduleItemProperties.BackgroundJobIdProperty;

		// Token: 0x04000149 RID: 329
		internal static readonly BackgroundJobBackendPropertyDefinition NameProperty = JobDefinitionProperties.NameProperty;

		// Token: 0x0400014A RID: 330
		internal static readonly BackgroundJobBackendPropertyDefinition PathProperty = JobDefinitionProperties.PathProperty;

		// Token: 0x0400014B RID: 331
		internal static readonly BackgroundJobBackendPropertyDefinition CommandLineProperty = JobDefinitionProperties.CommandLineProperty;

		// Token: 0x0400014C RID: 332
		internal static readonly BackgroundJobBackendPropertyDefinition DescriptionProperty = JobDefinitionProperties.DescriptionProperty;

		// Token: 0x0400014D RID: 333
		internal static readonly BackgroundJobBackendPropertyDefinition RoleIdProperty = JobDefinitionProperties.RoleIdProperty;

		// Token: 0x0400014E RID: 334
		internal static readonly BackgroundJobBackendPropertyDefinition SingleInstancePerMachineProperty = JobDefinitionProperties.SingleInstancePerMachineProperty;

		// Token: 0x0400014F RID: 335
		internal static readonly BackgroundJobBackendPropertyDefinition SchedulingStrategyProperty = JobDefinitionProperties.SchedulingStrategyProperty;

		// Token: 0x04000150 RID: 336
		internal static readonly BackgroundJobBackendPropertyDefinition TimeoutProperty = JobDefinitionProperties.TimeoutProperty;

		// Token: 0x04000151 RID: 337
		internal static readonly BackgroundJobBackendPropertyDefinition MaxLocalRetryCountProperty = JobDefinitionProperties.MaxLocalRetryCountProperty;

		// Token: 0x04000152 RID: 338
		internal static readonly BackgroundJobBackendPropertyDefinition MaxFailoverCountProperty = JobDefinitionProperties.MaxFailoverCountProperty;
	}
}
