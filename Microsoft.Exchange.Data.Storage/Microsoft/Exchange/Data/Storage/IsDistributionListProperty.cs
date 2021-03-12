using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C95 RID: 3221
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal sealed class IsDistributionListProperty : SmartPropertyDefinition
	{
		// Token: 0x0600709D RID: 28829 RVA: 0x001F2B8C File Offset: 0x001F0D8C
		public IsDistributionListProperty() : base("IsDistributionList", typeof(bool), PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.DisplayTypeExInternal, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.DisplayType, PropertyDependencyType.NeedForRead)
		})
		{
		}

		// Token: 0x0600709E RID: 28830 RVA: 0x001F2BD8 File Offset: 0x001F0DD8
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			object result;
			if ((result = this.GetIsDLFromDisplayTypeEx(propertyBag)) == null)
			{
				result = (this.GetIsDLFromDisplayType(propertyBag) ?? new PropertyError(this, PropertyErrorCode.NotFound));
			}
			return result;
		}

		// Token: 0x0600709F RID: 28831 RVA: 0x001F2C04 File Offset: 0x001F0E04
		private bool? GetIsDLFromDisplayTypeEx(PropertyBag.BasicPropertyStore propertyBag)
		{
			RecipientDisplayType? valueAsNullable = propertyBag.GetValueAsNullable<RecipientDisplayType>(InternalSchema.DisplayTypeExInternal);
			if (valueAsNullable == null)
			{
				return null;
			}
			return new bool?(DisplayTypeExProperty.IsDL(valueAsNullable.Value));
		}

		// Token: 0x060070A0 RID: 28832 RVA: 0x001F2C44 File Offset: 0x001F0E44
		private bool? GetIsDLFromDisplayType(PropertyBag.BasicPropertyStore propertyBag)
		{
			LegacyRecipientDisplayType? valueAsNullable = propertyBag.GetValueAsNullable<LegacyRecipientDisplayType>(InternalSchema.DisplayType);
			if (valueAsNullable == null)
			{
				return null;
			}
			return new bool?(DisplayTypeExProperty.IsDL(valueAsNullable.Value));
		}
	}
}
