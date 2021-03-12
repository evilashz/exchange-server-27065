using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000A9A RID: 2714
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class CalculatedDefaultValuePropertyRule<TDepend, TSet> : BinPropertyRule<TDepend, TSet>
	{
		// Token: 0x0600634E RID: 25422 RVA: 0x001A2D75 File Offset: 0x001A0F75
		public CalculatedDefaultValuePropertyRule(string name, Action<ILocationIdentifierSetter> onSetWriteEnforceLocationIdentifier, NativeStorePropertyDefinition propertyToDepend, NativeStorePropertyDefinition propertyToSet, CalculatedDefaultValuePropertyRule<TDepend, TSet>.CalculateDefaultValueDelegate calculateDefaultValueDelegate) : base(name, onSetWriteEnforceLocationIdentifier, propertyToDepend, propertyToSet)
		{
			if (calculateDefaultValueDelegate == null)
			{
				throw new ArgumentNullException("calculateDefaultValueDelegate");
			}
			this.calculateDelegate = calculateDefaultValueDelegate;
		}

		// Token: 0x0600634F RID: 25423 RVA: 0x001A2D99 File Offset: 0x001A0F99
		protected override bool CalculateValue(TDepend propertyToDependValue, TSet propertyToSetValue, out TSet calculatedResult)
		{
			return this.calculateDelegate(propertyToDependValue, out calculatedResult);
		}

		// Token: 0x06006350 RID: 25424 RVA: 0x001A2DA8 File Offset: 0x001A0FA8
		protected override bool ShouldEnforce(bool isPropertyDirty, object propertyValue)
		{
			return PropertyError.IsPropertyNotFound(propertyValue) || propertyValue == null;
		}

		// Token: 0x0400382A RID: 14378
		private CalculatedDefaultValuePropertyRule<TDepend, TSet>.CalculateDefaultValueDelegate calculateDelegate;

		// Token: 0x02000A9B RID: 2715
		// (Invoke) Token: 0x06006352 RID: 25426
		internal delegate bool CalculateDefaultValueDelegate(TDepend propertyToDependValue, out TSet calculatedResult);
	}
}
