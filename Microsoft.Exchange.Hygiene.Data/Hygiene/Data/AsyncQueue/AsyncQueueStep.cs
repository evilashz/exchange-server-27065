using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Hygiene.Data.AsyncQueue
{
	// Token: 0x0200002A RID: 42
	internal class AsyncQueueStep : ConfigurablePropertyBag
	{
		// Token: 0x06000163 RID: 355 RVA: 0x00005AB6 File Offset: 0x00003CB6
		public AsyncQueueStep(string stepName, short stepOrdinal) : this(stepName, stepOrdinal, AsyncQueueStatus.NotStarted)
		{
			AsyncQueueStep.ValidateOrdinal(stepOrdinal);
			AsyncQueueStep.ValidateName(stepName);
			this.StepName = stepName;
			this.StepOrdinal = stepOrdinal;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00005ADC File Offset: 0x00003CDC
		public AsyncQueueStep(string stepName, short stepOrdinal, AsyncQueueStatus stepStatus) : this()
		{
			AsyncQueueStep.ValidateOrdinal(stepOrdinal);
			AsyncQueueStep.ValidateName(stepName);
			this.StepName = stepName;
			this.StepOrdinal = stepOrdinal;
			this.StepStatus = stepStatus;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00005B05 File Offset: 0x00003D05
		public AsyncQueueStep()
		{
			this.RequestStepId = CombGuidGenerator.NewGuid();
			this.MaxExecutionTime = 3600;
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00005B24 File Offset: 0x00003D24
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.RequestStepId.ToString());
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00005B4A File Offset: 0x00003D4A
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00005B5C File Offset: 0x00003D5C
		public Guid RequestStepId
		{
			get
			{
				return (Guid)this[AsyncQueueStepSchema.RequestStepIdProperty];
			}
			private set
			{
				this[AsyncQueueStepSchema.RequestStepIdProperty] = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00005B6F File Offset: 0x00003D6F
		public AsyncQueuePriority Priority
		{
			get
			{
				return (AsyncQueuePriority)this[AsyncQueueStepSchema.PriorityProperty];
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00005B81 File Offset: 0x00003D81
		// (set) Token: 0x0600016B RID: 363 RVA: 0x00005B93 File Offset: 0x00003D93
		public Guid RequestId
		{
			get
			{
				return (Guid)this[AsyncQueueStepSchema.RequestIdProperty];
			}
			set
			{
				this[AsyncQueueStepSchema.RequestIdProperty] = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00005BA6 File Offset: 0x00003DA6
		public Guid? DependantRequestId
		{
			get
			{
				return (Guid?)this[AsyncQueueStepSchema.DependantRequestIdProperty];
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00005BB8 File Offset: 0x00003DB8
		public string RequestCookie
		{
			get
			{
				return (string)this[AsyncQueueStepSchema.RequestCookieProperty];
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00005BCA File Offset: 0x00003DCA
		// (set) Token: 0x0600016F RID: 367 RVA: 0x00005BDC File Offset: 0x00003DDC
		public AsyncQueueFlags StepFlags
		{
			get
			{
				return (AsyncQueueFlags)this[AsyncQueueStepSchema.FlagsProperty];
			}
			set
			{
				this[AsyncQueueStepSchema.FlagsProperty] = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00005BEF File Offset: 0x00003DEF
		// (set) Token: 0x06000171 RID: 369 RVA: 0x00005C01 File Offset: 0x00003E01
		public string OwnerId
		{
			get
			{
				return (string)this[AsyncQueueStepSchema.OwnerIdProperty];
			}
			set
			{
				this[AsyncQueueStepSchema.OwnerIdProperty] = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00005C0F File Offset: 0x00003E0F
		// (set) Token: 0x06000173 RID: 371 RVA: 0x00005C21 File Offset: 0x00003E21
		public string FriendlyName
		{
			get
			{
				return (string)this[AsyncQueueStepSchema.FriendlyNameProperty];
			}
			set
			{
				this[AsyncQueueStepSchema.FriendlyNameProperty] = value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00005C2F File Offset: 0x00003E2F
		// (set) Token: 0x06000175 RID: 373 RVA: 0x00005C41 File Offset: 0x00003E41
		public Guid OrganizationalUnitRoot
		{
			get
			{
				return (Guid)this[AsyncQueueStepSchema.OrganizationalUnitRootProperty];
			}
			set
			{
				this[AsyncQueueStepSchema.OrganizationalUnitRootProperty] = value;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00005C54 File Offset: 0x00003E54
		// (set) Token: 0x06000177 RID: 375 RVA: 0x00005C66 File Offset: 0x00003E66
		public short StepNumber
		{
			get
			{
				return (short)this[AsyncQueueStepSchema.StepNumberProperty];
			}
			set
			{
				this[AsyncQueueStepSchema.StepNumberProperty] = value;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00005C79 File Offset: 0x00003E79
		// (set) Token: 0x06000179 RID: 377 RVA: 0x00005C8B File Offset: 0x00003E8B
		public string StepName
		{
			get
			{
				return (string)this[AsyncQueueStepSchema.StepNameProperty];
			}
			set
			{
				AsyncQueueStep.ValidateName(value);
				this[AsyncQueueStepSchema.StepNameProperty] = value;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00005C9F File Offset: 0x00003E9F
		// (set) Token: 0x0600017B RID: 379 RVA: 0x00005CB1 File Offset: 0x00003EB1
		public short StepOrdinal
		{
			get
			{
				return (short)this[AsyncQueueStepSchema.StepOrdinalProperty];
			}
			set
			{
				AsyncQueueStep.ValidateOrdinal(value);
				this[AsyncQueueStepSchema.StepOrdinalProperty] = value;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00005CCA File Offset: 0x00003ECA
		// (set) Token: 0x0600017D RID: 381 RVA: 0x00005CDC File Offset: 0x00003EDC
		public AsyncQueueStatus StepStatus
		{
			get
			{
				return (AsyncQueueStatus)this[AsyncQueueStepSchema.StepStatusProperty];
			}
			set
			{
				this[AsyncQueueStepSchema.StepStatusProperty] = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00005CEF File Offset: 0x00003EEF
		// (set) Token: 0x0600017F RID: 383 RVA: 0x00005D01 File Offset: 0x00003F01
		public int FetchCount
		{
			get
			{
				return (int)this[AsyncQueueStepSchema.FetchCountProperty];
			}
			set
			{
				this[AsyncQueueStepSchema.FetchCountProperty] = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00005D14 File Offset: 0x00003F14
		// (set) Token: 0x06000181 RID: 385 RVA: 0x00005D26 File Offset: 0x00003F26
		public int ErrorCount
		{
			get
			{
				return (int)this[AsyncQueueStepSchema.ErrorCountProperty];
			}
			set
			{
				this[AsyncQueueStepSchema.ErrorCountProperty] = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00005D39 File Offset: 0x00003F39
		// (set) Token: 0x06000183 RID: 387 RVA: 0x00005D4B File Offset: 0x00003F4B
		public string ProcessInstanceName
		{
			get
			{
				return (string)this[AsyncQueueStepSchema.ProcessInstanceNameProperty];
			}
			set
			{
				this[AsyncQueueStepSchema.ProcessInstanceNameProperty] = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00005D59 File Offset: 0x00003F59
		// (set) Token: 0x06000185 RID: 389 RVA: 0x00005D6B File Offset: 0x00003F6B
		public int MaxExecutionTime
		{
			get
			{
				return (int)this[AsyncQueueStepSchema.MaxExecutionTimeProperty];
			}
			set
			{
				this[AsyncQueueStepSchema.MaxExecutionTimeProperty] = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00005D7E File Offset: 0x00003F7E
		// (set) Token: 0x06000187 RID: 391 RVA: 0x00005D90 File Offset: 0x00003F90
		public string Cookie
		{
			get
			{
				return (string)this[AsyncQueueStepSchema.CookieProperty];
			}
			set
			{
				this[AsyncQueueStepSchema.CookieProperty] = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00005D9E File Offset: 0x00003F9E
		public short ExecutionState
		{
			get
			{
				return (short)this[AsyncQueueStepSchema.ExecutionStateProperty];
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00005DB0 File Offset: 0x00003FB0
		public DateTime? NextFetchDatetime
		{
			get
			{
				return (DateTime?)this[AsyncQueueStepSchema.NextFetchDatetimeProperty];
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00005DC2 File Offset: 0x00003FC2
		public override Type GetSchemaType()
		{
			return typeof(AsyncQueueStepSchema);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00005DCE File Offset: 0x00003FCE
		private static void ValidateName(string stepName)
		{
			if (string.IsNullOrWhiteSpace(stepName))
			{
				throw new ArgumentNullException("stepName");
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00005DE3 File Offset: 0x00003FE3
		private static void ValidateOrdinal(short stepOrdinal)
		{
			if (stepOrdinal <= 0)
			{
				throw new ArgumentOutOfRangeException("Step Ordinal should start from 1.");
			}
		}

		// Token: 0x040000C0 RID: 192
		private const int DefaultMaxExecutionTime = 3600;
	}
}
