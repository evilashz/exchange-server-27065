using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Sync
{
	// Token: 0x0200022D RID: 557
	internal class UnsyncedObject : UnpublishedObject
	{
		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x00045C44 File Offset: 0x00043E44
		// (set) Token: 0x06001699 RID: 5785 RVA: 0x00045C56 File Offset: 0x00043E56
		public SyncType SyncType
		{
			get
			{
				return (SyncType)this[UnsyncedObjectSchema.SyncTypeProp];
			}
			set
			{
				this[UnsyncedObjectSchema.SyncTypeProp] = value;
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x0600169A RID: 5786 RVA: 0x00045C69 File Offset: 0x00043E69
		// (set) Token: 0x0600169B RID: 5787 RVA: 0x00045C7B File Offset: 0x00043E7B
		public Guid BatchId
		{
			get
			{
				return (Guid)this[UnsyncedObjectSchema.BatchIdProp];
			}
			set
			{
				this[UnsyncedObjectSchema.BatchIdProp] = value;
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x0600169C RID: 5788 RVA: 0x00045C8E File Offset: 0x00043E8E
		internal override ADObjectSchema Schema
		{
			get
			{
				return UnsyncedObject.schema;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x0600169D RID: 5789 RVA: 0x00045C95 File Offset: 0x00043E95
		internal override string MostDerivedObjectClass
		{
			get
			{
				return UnsyncedObject.mostDerivedClass;
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x0600169E RID: 5790 RVA: 0x00045C9C File Offset: 0x00043E9C
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x04000B67 RID: 2919
		private static readonly UnsyncedObjectSchema schema = ObjectSchema.GetInstance<UnsyncedObjectSchema>();

		// Token: 0x04000B68 RID: 2920
		private static string mostDerivedClass = "UnsyncedObject";
	}
}
