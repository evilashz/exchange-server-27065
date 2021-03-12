using System;

namespace Microsoft.Exchange.Entities.DataModel.PropertyBags
{
	// Token: 0x0200009B RID: 155
	public interface IPropertyAccessor<in TContainer, TValue>
	{
		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060003F3 RID: 1011
		bool Readonly { get; }

		// Token: 0x060003F4 RID: 1012
		bool TryGetValue(TContainer container, out TValue value);

		// Token: 0x060003F5 RID: 1013
		void Set(TContainer container, TValue value);
	}
}
