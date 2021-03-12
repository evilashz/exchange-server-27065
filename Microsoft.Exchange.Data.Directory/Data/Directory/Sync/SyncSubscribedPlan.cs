using System;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000837 RID: 2103
	internal class SyncSubscribedPlan : SyncObject
	{
		// Token: 0x0600682F RID: 26671 RVA: 0x0016F1DF File Offset: 0x0016D3DF
		public SyncSubscribedPlan(SyncDirection syncDirection) : base(syncDirection)
		{
		}

		// Token: 0x170024DE RID: 9438
		// (get) Token: 0x06006830 RID: 26672 RVA: 0x0016F1E8 File Offset: 0x0016D3E8
		public override SyncObjectSchema Schema
		{
			get
			{
				return SyncSubscribedPlan.schema;
			}
		}

		// Token: 0x170024DF RID: 9439
		// (get) Token: 0x06006831 RID: 26673 RVA: 0x0016F1EF File Offset: 0x0016D3EF
		internal override DirectoryObjectClass ObjectClass
		{
			get
			{
				return DirectoryObjectClass.SubscribedPlan;
			}
		}

		// Token: 0x06006832 RID: 26674 RVA: 0x0016F1F2 File Offset: 0x0016D3F2
		protected override DirectoryObject CreateDirectoryObject()
		{
			return new SubscribedPlan();
		}

		// Token: 0x170024E0 RID: 9440
		// (get) Token: 0x06006833 RID: 26675 RVA: 0x0016F1F9 File Offset: 0x0016D3F9
		// (set) Token: 0x06006834 RID: 26676 RVA: 0x0016F20B File Offset: 0x0016D40B
		internal SyncProperty<string> AccountId
		{
			get
			{
				return (SyncProperty<string>)base[SyncSubscribedPlanSchema.AccountId];
			}
			set
			{
				base[SyncSubscribedPlanSchema.AccountId] = value;
			}
		}

		// Token: 0x170024E1 RID: 9441
		// (get) Token: 0x06006835 RID: 26677 RVA: 0x0016F219 File Offset: 0x0016D419
		// (set) Token: 0x06006836 RID: 26678 RVA: 0x0016F22B File Offset: 0x0016D42B
		internal SyncProperty<string> Capability
		{
			get
			{
				return (SyncProperty<string>)base[SyncSubscribedPlanSchema.Capability];
			}
			set
			{
				base[SyncSubscribedPlanSchema.Capability] = value;
			}
		}

		// Token: 0x170024E2 RID: 9442
		// (get) Token: 0x06006837 RID: 26679 RVA: 0x0016F239 File Offset: 0x0016D439
		// (set) Token: 0x06006838 RID: 26680 RVA: 0x0016F24B File Offset: 0x0016D44B
		internal SyncProperty<string> ServiceType
		{
			get
			{
				return (SyncProperty<string>)base[SyncSubscribedPlanSchema.ServiceType];
			}
			set
			{
				base[SyncSubscribedPlanSchema.ServiceType] = value;
			}
		}

		// Token: 0x170024E3 RID: 9443
		// (get) Token: 0x06006839 RID: 26681 RVA: 0x0016F259 File Offset: 0x0016D459
		// (set) Token: 0x0600683A RID: 26682 RVA: 0x0016F26B File Offset: 0x0016D46B
		internal SyncProperty<string> MaximumOverageUnitsDetail
		{
			get
			{
				return (SyncProperty<string>)base[SyncSubscribedPlanSchema.MaximumOverageUnitsDetail];
			}
			set
			{
				base[SyncSubscribedPlanSchema.MaximumOverageUnitsDetail] = value;
			}
		}

		// Token: 0x170024E4 RID: 9444
		// (get) Token: 0x0600683B RID: 26683 RVA: 0x0016F279 File Offset: 0x0016D479
		// (set) Token: 0x0600683C RID: 26684 RVA: 0x0016F28B File Offset: 0x0016D48B
		internal SyncProperty<string> PrepaidUnitsDetail
		{
			get
			{
				return (SyncProperty<string>)base[SyncSubscribedPlanSchema.PrepaidUnitsDetail];
			}
			set
			{
				base[SyncSubscribedPlanSchema.PrepaidUnitsDetail] = value;
			}
		}

		// Token: 0x170024E5 RID: 9445
		// (get) Token: 0x0600683D RID: 26685 RVA: 0x0016F299 File Offset: 0x0016D499
		// (set) Token: 0x0600683E RID: 26686 RVA: 0x0016F2AB File Offset: 0x0016D4AB
		internal SyncProperty<string> TotalTrialUnitsDetail
		{
			get
			{
				return (SyncProperty<string>)base[SyncSubscribedPlanSchema.TotalTrialUnitsDetail];
			}
			set
			{
				base[SyncSubscribedPlanSchema.TotalTrialUnitsDetail] = value;
			}
		}

		// Token: 0x04004496 RID: 17558
		private static readonly SyncSubscribedPlanSchema schema = ObjectSchema.GetInstance<SyncSubscribedPlanSchema>();
	}
}
