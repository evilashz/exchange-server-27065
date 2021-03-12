using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000272 RID: 626
	internal class EntitySyncWatermark : MailboxSyncWatermark
	{
		// Token: 0x06001751 RID: 5969 RVA: 0x0008AD68 File Offset: 0x00088F68
		public EntitySyncWatermark()
		{
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x0008AD70 File Offset: 0x00088F70
		protected EntitySyncWatermark(int changeNumber) : base(changeNumber)
		{
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x0008AD79 File Offset: 0x00088F79
		public new static EntitySyncWatermark Create()
		{
			return new EntitySyncWatermark();
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x0008AD80 File Offset: 0x00088F80
		public new static EntitySyncWatermark CreateForSingleItem()
		{
			return new EntitySyncWatermark();
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x0008AD87 File Offset: 0x00088F87
		public new static EntitySyncWatermark CreateWithChangeNumber(int changeNumber)
		{
			return new EntitySyncWatermark(changeNumber);
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x0008AD8F File Offset: 0x00088F8F
		public override ICustomSerializable BuildObject()
		{
			return new EntitySyncWatermark();
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x0008AD98 File Offset: 0x00088F98
		public override object Clone()
		{
			EntitySyncWatermark entitySyncWatermark = EntitySyncWatermark.CreateWithChangeNumber(base.ChangeNumber);
			if (base.IcsState != null)
			{
				entitySyncWatermark.IcsState = (byte[])base.IcsState.Clone();
			}
			return entitySyncWatermark;
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x0008ADD0 File Offset: 0x00088FD0
		public override object CustomClone()
		{
			return EntitySyncWatermark.CreateWithChangeNumber(base.ChangeNumber);
		}
	}
}
