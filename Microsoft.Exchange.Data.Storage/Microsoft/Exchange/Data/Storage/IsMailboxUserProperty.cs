using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C96 RID: 3222
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal sealed class IsMailboxUserProperty : SmartPropertyDefinition
	{
		// Token: 0x060070A1 RID: 28833 RVA: 0x001F2C84 File Offset: 0x001F0E84
		public IsMailboxUserProperty() : base("IsMailboxUser", typeof(bool), PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.DisplayTypeExInternal, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.DisplayType, PropertyDependencyType.NeedForRead)
		})
		{
		}

		// Token: 0x060070A2 RID: 28834 RVA: 0x001F2CD0 File Offset: 0x001F0ED0
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			object result;
			if ((result = this.GetIsMailboxUserFromDisplayTypeEx(propertyBag)) == null)
			{
				result = (this.GetIsMailboxUserFromDisplayType(propertyBag) ?? new PropertyError(this, PropertyErrorCode.NotFound));
			}
			return result;
		}

		// Token: 0x060070A3 RID: 28835 RVA: 0x001F2CFC File Offset: 0x001F0EFC
		private bool? GetIsMailboxUserFromDisplayTypeEx(PropertyBag.BasicPropertyStore propertyBag)
		{
			RecipientDisplayType? valueAsNullable = propertyBag.GetValueAsNullable<RecipientDisplayType>(InternalSchema.DisplayTypeExInternal);
			if (valueAsNullable == null)
			{
				return null;
			}
			return new bool?(DisplayTypeExProperty.IsMailboxUser(valueAsNullable.Value));
		}

		// Token: 0x060070A4 RID: 28836 RVA: 0x001F2D3C File Offset: 0x001F0F3C
		private bool? GetIsMailboxUserFromDisplayType(PropertyBag.BasicPropertyStore propertyBag)
		{
			LegacyRecipientDisplayType? valueAsNullable = propertyBag.GetValueAsNullable<LegacyRecipientDisplayType>(InternalSchema.DisplayType);
			if (valueAsNullable == null)
			{
				return null;
			}
			return new bool?(DisplayTypeExProperty.IsMailboxUser(valueAsNullable.Value));
		}
	}
}
