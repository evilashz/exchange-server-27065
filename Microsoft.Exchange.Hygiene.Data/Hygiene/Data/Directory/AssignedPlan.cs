using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Hygiene.Data.Sync;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000CA RID: 202
	[Serializable]
	internal class AssignedPlan : ADObject
	{
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x000154B6 File Offset: 0x000136B6
		public override ObjectId Identity
		{
			get
			{
				return new ADObjectId(this.SubscribedPlanId);
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x000154C3 File Offset: 0x000136C3
		// (set) Token: 0x0600069E RID: 1694 RVA: 0x000154D5 File Offset: 0x000136D5
		internal ADObjectId TenantId
		{
			get
			{
				return this[ADObjectSchema.OrganizationalUnitRoot] as ADObjectId;
			}
			set
			{
				this[ADObjectSchema.OrganizationalUnitRoot] = value;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x000154E3 File Offset: 0x000136E3
		// (set) Token: 0x060006A0 RID: 1696 RVA: 0x000154F5 File Offset: 0x000136F5
		internal ADObjectId ObjectId
		{
			get
			{
				return this[CommonSyncProperties.ObjectIdProp] as ADObjectId;
			}
			set
			{
				this[CommonSyncProperties.ObjectIdProp] = value;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x00015503 File Offset: 0x00013703
		// (set) Token: 0x060006A2 RID: 1698 RVA: 0x00015515 File Offset: 0x00013715
		internal Guid SubscribedPlanId
		{
			get
			{
				return (Guid)this[AssignedPlanSchema.SubscribedPlanIdProp];
			}
			set
			{
				this[AssignedPlanSchema.SubscribedPlanIdProp] = value;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x00015528 File Offset: 0x00013728
		// (set) Token: 0x060006A4 RID: 1700 RVA: 0x0001553A File Offset: 0x0001373A
		internal string Capability
		{
			get
			{
				return this[AssignedPlanSchema.CapabilityProp] as string;
			}
			set
			{
				this[AssignedPlanSchema.CapabilityProp] = value;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x00015548 File Offset: 0x00013748
		// (set) Token: 0x060006A6 RID: 1702 RVA: 0x0001555A File Offset: 0x0001375A
		internal AssignedCapabilityStatus CapabilityStatus
		{
			get
			{
				return (AssignedCapabilityStatus)this[AssignedPlanSchema.CapabilityStatusProp];
			}
			set
			{
				this[AssignedPlanSchema.CapabilityStatusProp] = value;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0001556D File Offset: 0x0001376D
		// (set) Token: 0x060006A8 RID: 1704 RVA: 0x0001557F File Offset: 0x0001377F
		internal DateTime AssignedTimeStamp
		{
			get
			{
				return (DateTime)this[AssignedPlanSchema.AssignedTimeStampProp];
			}
			set
			{
				this[AssignedPlanSchema.AssignedTimeStampProp] = value;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x00015592 File Offset: 0x00013792
		// (set) Token: 0x060006AA RID: 1706 RVA: 0x000155A4 File Offset: 0x000137A4
		internal string InitialState
		{
			get
			{
				return this[AssignedPlanSchema.InitialStateProp] as string;
			}
			set
			{
				this[AssignedPlanSchema.InitialStateProp] = value;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x000155B2 File Offset: 0x000137B2
		// (set) Token: 0x060006AC RID: 1708 RVA: 0x000155C4 File Offset: 0x000137C4
		internal DirectoryObjectClass ObjectType
		{
			get
			{
				return (DirectoryObjectClass)this[CommonSyncProperties.ObjectTypeProp];
			}
			set
			{
				this[CommonSyncProperties.ObjectTypeProp] = value;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x000155D7 File Offset: 0x000137D7
		// (set) Token: 0x060006AE RID: 1710 RVA: 0x000155E9 File Offset: 0x000137E9
		internal string ServiceInstance
		{
			get
			{
				return this[CommonSyncProperties.ServiceInstanceProp] as string;
			}
			set
			{
				this[CommonSyncProperties.ServiceInstanceProp] = value;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x000155F7 File Offset: 0x000137F7
		internal override ADObjectSchema Schema
		{
			get
			{
				return AssignedPlan.schema;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x000155FE File Offset: 0x000137FE
		internal override string MostDerivedObjectClass
		{
			get
			{
				return AssignedPlan.mostDerivedClass;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x00015605 File Offset: 0x00013805
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x04000420 RID: 1056
		private static readonly AssignedPlanSchema schema = ObjectSchema.GetInstance<AssignedPlanSchema>();

		// Token: 0x04000421 RID: 1057
		private static string mostDerivedClass = "AssignedPlan";
	}
}
