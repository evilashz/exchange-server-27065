using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Kes
{
	// Token: 0x020001FF RID: 511
	internal class SpamExclusionData : ConfigurablePropertyBag
	{
		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x0600155C RID: 5468 RVA: 0x00042D34 File Offset: 0x00040F34
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.SpamExclusionDataID.ToString());
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x0600155D RID: 5469 RVA: 0x00042D5A File Offset: 0x00040F5A
		// (set) Token: 0x0600155E RID: 5470 RVA: 0x00042D6C File Offset: 0x00040F6C
		internal Guid SpamExclusionDataID
		{
			get
			{
				return (Guid)this[SpamExclusionData.SpamExclusionDataIDProperty];
			}
			set
			{
				this[SpamExclusionData.SpamExclusionDataIDProperty] = value;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x0600155F RID: 5471 RVA: 0x00042D7F File Offset: 0x00040F7F
		// (set) Token: 0x06001560 RID: 5472 RVA: 0x00042D91 File Offset: 0x00040F91
		internal SpamExclusionDataID DataID
		{
			get
			{
				return (SpamExclusionDataID)this[SpamExclusionData.DataIDProperty];
			}
			set
			{
				this[SpamExclusionData.DataIDProperty] = (byte)value;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001561 RID: 5473 RVA: 0x00042DA4 File Offset: 0x00040FA4
		// (set) Token: 0x06001562 RID: 5474 RVA: 0x00042DB6 File Offset: 0x00040FB6
		internal byte DataTypeID
		{
			get
			{
				return (byte)this[SpamExclusionData.DataTypeIDProperty];
			}
			set
			{
				this[SpamExclusionData.DataTypeIDProperty] = value;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001563 RID: 5475 RVA: 0x00042DC9 File Offset: 0x00040FC9
		// (set) Token: 0x06001564 RID: 5476 RVA: 0x00042DDB File Offset: 0x00040FDB
		internal string ExclusionDataTag
		{
			get
			{
				return (string)this[SpamExclusionData.ExclusionDataTagProperty];
			}
			set
			{
				this[SpamExclusionData.ExclusionDataTagProperty] = value;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001565 RID: 5477 RVA: 0x00042DE9 File Offset: 0x00040FE9
		// (set) Token: 0x06001566 RID: 5478 RVA: 0x00042DFB File Offset: 0x00040FFB
		internal string ExclusionData
		{
			get
			{
				return (string)this[SpamExclusionData.ExclusionDataProperty];
			}
			set
			{
				this[SpamExclusionData.ExclusionDataProperty] = value;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001567 RID: 5479 RVA: 0x00042E09 File Offset: 0x00041009
		// (set) Token: 0x06001568 RID: 5480 RVA: 0x00042E1B File Offset: 0x0004101B
		internal bool IsPersistent
		{
			get
			{
				return (bool)this[SpamExclusionData.IsPersistentProperty];
			}
			set
			{
				this[SpamExclusionData.IsPersistentProperty] = value;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001569 RID: 5481 RVA: 0x00042E2E File Offset: 0x0004102E
		// (set) Token: 0x0600156A RID: 5482 RVA: 0x00042E40 File Offset: 0x00041040
		internal bool IsListed
		{
			get
			{
				return (bool)this[SpamExclusionData.IsListedProperty];
			}
			set
			{
				this[SpamExclusionData.IsListedProperty] = value;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x0600156B RID: 5483 RVA: 0x00042E53 File Offset: 0x00041053
		// (set) Token: 0x0600156C RID: 5484 RVA: 0x00042E65 File Offset: 0x00041065
		internal string CreatedBy
		{
			get
			{
				return (string)this[SpamExclusionData.CreatedByProperty];
			}
			set
			{
				this[SpamExclusionData.CreatedByProperty] = value;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x0600156D RID: 5485 RVA: 0x00042E73 File Offset: 0x00041073
		// (set) Token: 0x0600156E RID: 5486 RVA: 0x00042E85 File Offset: 0x00041085
		internal DateTime? ExpirationDate
		{
			get
			{
				return (DateTime?)this[SpamExclusionData.ExpirationDateProperty];
			}
			set
			{
				this[SpamExclusionData.ExpirationDateProperty] = value;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x0600156F RID: 5487 RVA: 0x00042E98 File Offset: 0x00041098
		// (set) Token: 0x06001570 RID: 5488 RVA: 0x00042EAA File Offset: 0x000410AA
		internal string Comment
		{
			get
			{
				return (string)this[SpamExclusionData.CommentProperty];
			}
			set
			{
				this[SpamExclusionData.CommentProperty] = value;
			}
		}

		// Token: 0x04000AC0 RID: 2752
		public static readonly HygienePropertyDefinition SpamExclusionDataIDProperty = new HygienePropertyDefinition("id_SpamExclusionDataId", typeof(Guid));

		// Token: 0x04000AC1 RID: 2753
		public static readonly HygienePropertyDefinition DataIDProperty = new HygienePropertyDefinition("ti_DataId", typeof(byte), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000AC2 RID: 2754
		public static readonly HygienePropertyDefinition DataTypeIDProperty = new HygienePropertyDefinition("ti_DataTypeId", typeof(byte), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000AC3 RID: 2755
		public static readonly HygienePropertyDefinition ExclusionDataTagProperty = new HygienePropertyDefinition("nvc_ExclusionDataTag", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000AC4 RID: 2756
		public static readonly HygienePropertyDefinition ExclusionDataProperty = new HygienePropertyDefinition("nvc_ExclusionData", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000AC5 RID: 2757
		public static readonly HygienePropertyDefinition IsPersistentProperty = new HygienePropertyDefinition("f_IsPersistent", typeof(bool));

		// Token: 0x04000AC6 RID: 2758
		public static readonly HygienePropertyDefinition IsListedProperty = new HygienePropertyDefinition("f_IsListed", typeof(bool));

		// Token: 0x04000AC7 RID: 2759
		public static readonly HygienePropertyDefinition CreatedByProperty = new HygienePropertyDefinition("nvc_CreatedBy", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000AC8 RID: 2760
		public static readonly HygienePropertyDefinition ExpirationDateProperty = new HygienePropertyDefinition("dt_ExpirationDate", typeof(DateTime?));

		// Token: 0x04000AC9 RID: 2761
		public static readonly HygienePropertyDefinition CommentProperty = new HygienePropertyDefinition("nvc_Comment", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
