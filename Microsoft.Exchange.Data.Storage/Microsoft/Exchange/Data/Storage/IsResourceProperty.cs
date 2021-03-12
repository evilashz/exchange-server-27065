using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C98 RID: 3224
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal sealed class IsResourceProperty : SmartPropertyDefinition
	{
		// Token: 0x060070A8 RID: 28840 RVA: 0x001F2E14 File Offset: 0x001F1014
		public IsResourceProperty() : base("IsResource", typeof(bool), PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.DisplayTypeExInternal, PropertyDependencyType.NeedForRead)
		})
		{
		}

		// Token: 0x060070A9 RID: 28841 RVA: 0x001F2E52 File Offset: 0x001F1052
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			return this.GetIsResourceFromDisplayTypeEx(propertyBag) ?? new PropertyError(this, PropertyErrorCode.NotFound);
		}

		// Token: 0x060070AA RID: 28842 RVA: 0x001F2E6C File Offset: 0x001F106C
		private bool? GetIsResourceFromDisplayTypeEx(PropertyBag.BasicPropertyStore propertyBag)
		{
			RecipientDisplayType? recipientDisplayType = new RecipientDisplayType?(propertyBag.GetValueOrDefault<RecipientDisplayType>(InternalSchema.DisplayTypeExInternal));
			if (recipientDisplayType == null)
			{
				return null;
			}
			return new bool?(DisplayTypeExProperty.IsResource(recipientDisplayType.Value));
		}
	}
}
