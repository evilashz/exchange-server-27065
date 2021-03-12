using System;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000040 RID: 64
	internal interface IPropertyBag
	{
		// Token: 0x06000199 RID: 409
		bool TryGet(ContextProperty property, out object value);

		// Token: 0x0600019A RID: 410
		void Set(ContextProperty property, object value);
	}
}
