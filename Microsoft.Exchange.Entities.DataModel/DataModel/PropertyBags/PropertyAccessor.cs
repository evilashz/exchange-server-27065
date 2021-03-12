using System;

namespace Microsoft.Exchange.Entities.DataModel.PropertyBags
{
	// Token: 0x0200009C RID: 156
	public abstract class PropertyAccessor<TContainer, TValue> : IPropertyAccessor<TContainer, TValue>
	{
		// Token: 0x060003F6 RID: 1014 RVA: 0x000074EC File Offset: 0x000056EC
		protected PropertyAccessor(bool readOnly)
		{
			this.Readonly = readOnly;
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x000074FB File Offset: 0x000056FB
		// (set) Token: 0x060003F8 RID: 1016 RVA: 0x00007503 File Offset: 0x00005703
		public bool Readonly { get; private set; }

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000750C File Offset: 0x0000570C
		public bool TryGetValue(TContainer container, out TValue value)
		{
			return this.PerformTryGetValue(container, out value);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00007516 File Offset: 0x00005716
		public void Set(TContainer container, TValue value)
		{
			this.PerformSet(container, value);
		}

		// Token: 0x060003FB RID: 1019
		protected abstract bool PerformTryGetValue(TContainer container, out TValue value);

		// Token: 0x060003FC RID: 1020
		protected abstract void PerformSet(TContainer container, TValue value);
	}
}
