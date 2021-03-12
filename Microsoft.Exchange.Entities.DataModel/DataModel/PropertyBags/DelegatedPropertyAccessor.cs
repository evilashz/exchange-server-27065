using System;

namespace Microsoft.Exchange.Entities.DataModel.PropertyBags
{
	// Token: 0x0200009D RID: 157
	public class DelegatedPropertyAccessor<TContainer, TValue> : PropertyAccessor<TContainer, TValue>
	{
		// Token: 0x060003FD RID: 1021 RVA: 0x00007520 File Offset: 0x00005720
		public DelegatedPropertyAccessor(DelegatedPropertyAccessor<TContainer, TValue>.TryGetValueFunc getterDelegate, Action<TContainer, TValue> setterDelegate = null) : base(setterDelegate == null)
		{
			this.getterDelegate = getterDelegate;
			this.setterDelegate = setterDelegate;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000753A File Offset: 0x0000573A
		protected override bool PerformTryGetValue(TContainer container, out TValue value)
		{
			return this.getterDelegate(container, out value);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00007549 File Offset: 0x00005749
		protected override void PerformSet(TContainer container, TValue value)
		{
			this.setterDelegate(container, value);
		}

		// Token: 0x040001F4 RID: 500
		private readonly DelegatedPropertyAccessor<TContainer, TValue>.TryGetValueFunc getterDelegate;

		// Token: 0x040001F5 RID: 501
		private readonly Action<TContainer, TValue> setterDelegate;

		// Token: 0x0200009E RID: 158
		// (Invoke) Token: 0x06000401 RID: 1025
		public delegate bool TryGetValueFunc(TContainer container, out TValue value);
	}
}
