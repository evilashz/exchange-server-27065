using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler
{
	// Token: 0x02000043 RID: 67
	internal abstract class IdConverter
	{
		// Token: 0x060002B7 RID: 695 RVA: 0x00017F92 File Offset: 0x00016192
		protected IdConverter(PropertyTag clientProperty, PropertyDefinition serverProperty)
		{
			this.ClientProperty = clientProperty;
			this.ServerProperty = serverProperty;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00017FA8 File Offset: 0x000161A8
		internal bool GetClientId(StoreSession session, ICorePropertyBag propertyBag, PropertyTag property, out PropertyValue clientValue)
		{
			clientValue = default(PropertyValue);
			if (property != this.ClientProperty)
			{
				return false;
			}
			StoreId storeId = propertyBag.TryGetProperty(this.ServerProperty) as StoreId;
			if (storeId == null)
			{
				clientValue = PropertyValue.Error(this.ClientProperty.PropertyId, (ErrorCode)2147746063U);
			}
			else
			{
				clientValue = new PropertyValue(this.ClientProperty, this.CreateClientId(session, storeId));
			}
			return true;
		}

		// Token: 0x060002B9 RID: 697
		protected abstract long CreateClientId(StoreSession session, StoreId id);

		// Token: 0x04000104 RID: 260
		protected readonly PropertyTag ClientProperty;

		// Token: 0x04000105 RID: 261
		protected readonly PropertyDefinition ServerProperty;
	}
}
