using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.Entities.TypeConversion.Converters;

namespace Microsoft.Exchange.Entities.TypeConversion.PropertyAccessors
{
	// Token: 0x02000071 RID: 113
	internal sealed class StorageBodyPropertyAccessor<TItem> : StoragePropertyAccessor<TItem, ItemBody> where TItem : IItem
	{
		// Token: 0x0600026B RID: 619 RVA: 0x00008583 File Offset: 0x00006783
		public StorageBodyPropertyAccessor() : base(false, PropertyChangeMetadata.PropertyGroup.Body, null)
		{
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00008594 File Offset: 0x00006794
		protected override bool PerformTryGetValue(TItem container, out ItemBody value)
		{
			char[] buffer = new char[32768];
			value = container.GetEntityBody(buffer);
			return true;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x000085BB File Offset: 0x000067BB
		protected override void PerformSet(TItem container, ItemBody value)
		{
			value.SetOnStorageItem(container, !container.IsNew);
		}

		// Token: 0x040000E9 RID: 233
		private const int RpcMaxBufferSize = 32768;
	}
}
