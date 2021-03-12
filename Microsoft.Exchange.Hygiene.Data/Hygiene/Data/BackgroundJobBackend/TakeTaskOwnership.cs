using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.BackgroundJobBackend
{
	// Token: 0x02000043 RID: 67
	internal sealed class TakeTaskOwnership : BackgroundJobBackendBase
	{
		// Token: 0x0600026E RID: 622 RVA: 0x00008820 File Offset: 0x00006A20
		public TakeTaskOwnership(Guid backgroundJobId, Guid taskId, Guid ownerId, int ownerFitnessScore, DateTime startTime)
		{
			this[TakeTaskOwnership.BackgroundJobIdProperty] = backgroundJobId;
			this[TakeTaskOwnership.TaskIdProperty] = taskId;
			this[TakeTaskOwnership.BJMOwnerIdProperty] = ownerId;
			this[TakeTaskOwnership.OwnerFitnessScoreProperty] = ownerFitnessScore;
			this[TakeTaskOwnership.StartTimeProperty] = startTime;
			this[TakeTaskOwnership.OwnershipTakenProperty] = false;
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000889B File Offset: 0x00006A9B
		public Guid BackgroundJobId
		{
			get
			{
				return (Guid)this[TakeTaskOwnership.BackgroundJobIdProperty];
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000270 RID: 624 RVA: 0x000088AD File Offset: 0x00006AAD
		public Guid TaskId
		{
			get
			{
				return (Guid)this[TakeTaskOwnership.TaskIdProperty];
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000271 RID: 625 RVA: 0x000088BF File Offset: 0x00006ABF
		public Guid BJMOwnerId
		{
			get
			{
				return (Guid)this[TakeTaskOwnership.BJMOwnerIdProperty];
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000272 RID: 626 RVA: 0x000088D1 File Offset: 0x00006AD1
		public int OwnerFitnessScore
		{
			get
			{
				return (int)this[TakeTaskOwnership.OwnerFitnessScoreProperty];
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000273 RID: 627 RVA: 0x000088E3 File Offset: 0x00006AE3
		public DateTime StartTime
		{
			get
			{
				return (DateTime)this[TakeTaskOwnership.StartTimeProperty];
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000274 RID: 628 RVA: 0x000088F5 File Offset: 0x00006AF5
		// (set) Token: 0x06000275 RID: 629 RVA: 0x00008907 File Offset: 0x00006B07
		public bool OwnershipTaken
		{
			get
			{
				return (bool)this[TakeTaskOwnership.OwnershipTakenProperty];
			}
			set
			{
				this[TakeTaskOwnership.OwnershipTakenProperty] = value;
			}
		}

		// Token: 0x040001A1 RID: 417
		internal static readonly BackgroundJobBackendPropertyDefinition BackgroundJobIdProperty = ScheduleItemProperties.BackgroundJobIdProperty;

		// Token: 0x040001A2 RID: 418
		internal static readonly BackgroundJobBackendPropertyDefinition TaskIdProperty = TaskItemProperties.TaskIdProperty;

		// Token: 0x040001A3 RID: 419
		internal static readonly BackgroundJobBackendPropertyDefinition BJMOwnerIdProperty = TaskItemProperties.BJMOwnerIdProperty;

		// Token: 0x040001A4 RID: 420
		internal static readonly BackgroundJobBackendPropertyDefinition OwnerFitnessScoreProperty = TaskItemProperties.OwnerFitnessScoreProperty;

		// Token: 0x040001A5 RID: 421
		internal static readonly BackgroundJobBackendPropertyDefinition StartTimeProperty = TaskItemProperties.StartTimeProperty;

		// Token: 0x040001A6 RID: 422
		internal static readonly BackgroundJobBackendPropertyDefinition OwnershipTakenProperty = new BackgroundJobBackendPropertyDefinition("OwnershipTaken", typeof(bool), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, false);
	}
}
