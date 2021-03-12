using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.Kes
{
	// Token: 0x0200020D RID: 525
	internal class SyncWatermark : ConfigurablePropertyBag
	{
		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x060015E7 RID: 5607 RVA: 0x00044644 File Offset: 0x00042844
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.Id.ToString());
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x060015E8 RID: 5608 RVA: 0x0004466A File Offset: 0x0004286A
		// (set) Token: 0x060015E9 RID: 5609 RVA: 0x0004467C File Offset: 0x0004287C
		public Guid Id
		{
			get
			{
				return (Guid)this[SyncWatermark.IdProperty];
			}
			set
			{
				this[SyncWatermark.IdProperty] = value;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x060015EA RID: 5610 RVA: 0x0004468F File Offset: 0x0004288F
		// (set) Token: 0x060015EB RID: 5611 RVA: 0x000446A1 File Offset: 0x000428A1
		public string SyncContext
		{
			get
			{
				return (string)this[SyncWatermark.SyncContextProperty];
			}
			set
			{
				this[SyncWatermark.SyncContextProperty] = value;
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x060015EC RID: 5612 RVA: 0x000446AF File Offset: 0x000428AF
		// (set) Token: 0x060015ED RID: 5613 RVA: 0x000446C1 File Offset: 0x000428C1
		public string Data
		{
			get
			{
				return (string)this[SyncWatermark.DataProperty];
			}
			set
			{
				this[SyncWatermark.DataProperty] = value;
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x060015EE RID: 5614 RVA: 0x000446CF File Offset: 0x000428CF
		// (set) Token: 0x060015EF RID: 5615 RVA: 0x000446E1 File Offset: 0x000428E1
		public Guid? Owner
		{
			get
			{
				return (Guid?)this[SyncWatermark.OwnerProperty];
			}
			set
			{
				this[SyncWatermark.OwnerProperty] = value;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x060015F0 RID: 5616 RVA: 0x000446F4 File Offset: 0x000428F4
		public DateTime? ChangedDateTime
		{
			get
			{
				object obj;
				if (base.TryGetValue(SyncWatermark.ChangedDateTimeProperty, out obj))
				{
					return (DateTime?)obj;
				}
				return null;
			}
		}

		// Token: 0x04000B03 RID: 2819
		public static readonly HygienePropertyDefinition IdProperty = new HygienePropertyDefinition("id_Identity", typeof(Guid));

		// Token: 0x04000B04 RID: 2820
		public static readonly HygienePropertyDefinition SyncContextProperty = new HygienePropertyDefinition("nvc_SyncContext", typeof(string));

		// Token: 0x04000B05 RID: 2821
		public static readonly HygienePropertyDefinition DataProperty = new HygienePropertyDefinition("nvc_Data", typeof(string));

		// Token: 0x04000B06 RID: 2822
		public static readonly HygienePropertyDefinition OwnerProperty = new HygienePropertyDefinition("id_Owner", typeof(Guid?));

		// Token: 0x04000B07 RID: 2823
		public static readonly HygienePropertyDefinition ChangedDateTimeProperty = new HygienePropertyDefinition("dt_ChangedDateTime", typeof(DateTime?));
	}
}
