using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000A9C RID: 2716
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class CalculatedValueUpdatePropertyRule<TDepend, TSet> : BinPropertyRule<TDepend, TSet>
	{
		// Token: 0x06006355 RID: 25429 RVA: 0x001A2DB8 File Offset: 0x001A0FB8
		public CalculatedValueUpdatePropertyRule(string name, Action<ILocationIdentifierSetter> onSetWriteEnforceLocationIdentifier, NativeStorePropertyDefinition propertyToDepend, NativeStorePropertyDefinition propertyToSet, CalculatedValueUpdatePropertyRule<TDepend, TSet>.CalculateNewValueDelegate calculateNewValueDelegate) : base(name, onSetWriteEnforceLocationIdentifier, propertyToDepend, propertyToSet)
		{
			if (calculateNewValueDelegate == null)
			{
				throw new ArgumentNullException("calculateNewValueDelegate");
			}
			this.calculateDelegate = calculateNewValueDelegate;
		}

		// Token: 0x06006356 RID: 25430 RVA: 0x001A2DDC File Offset: 0x001A0FDC
		protected override bool CalculateValue(TDepend propertyToDependValue, TSet propertyToSetValue, out TSet calculatedResult)
		{
			return this.calculateDelegate(propertyToDependValue, propertyToSetValue, out calculatedResult);
		}

		// Token: 0x06006357 RID: 25431 RVA: 0x001A2DEC File Offset: 0x001A0FEC
		protected override bool ShouldEnforce(bool isPropertyDirty, object propertyValue)
		{
			return isPropertyDirty;
		}

		// Token: 0x0400382B RID: 14379
		private CalculatedValueUpdatePropertyRule<TDepend, TSet>.CalculateNewValueDelegate calculateDelegate;

		// Token: 0x02000A9D RID: 2717
		// (Invoke) Token: 0x06006359 RID: 25433
		internal delegate bool CalculateNewValueDelegate(TDepend propertyToDependValue, TSet propertyToSetValue, out TSet calculatedResult);
	}
}
