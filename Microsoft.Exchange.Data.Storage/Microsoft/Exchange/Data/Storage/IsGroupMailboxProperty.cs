using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C99 RID: 3225
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal sealed class IsGroupMailboxProperty : SmartPropertyDefinition
	{
		// Token: 0x060070AB RID: 28843 RVA: 0x001F2EB0 File Offset: 0x001F10B0
		public IsGroupMailboxProperty() : base("IsGroupMailbox", typeof(bool), PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.DisplayTypeExInternal, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.DisplayType, PropertyDependencyType.NeedForRead)
		})
		{
		}

		// Token: 0x060070AC RID: 28844 RVA: 0x001F2EFC File Offset: 0x001F10FC
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			return this.GetIsGroupMailboxFromDisplayTypeEx(propertyBag) ?? new PropertyError(this, PropertyErrorCode.NotFound);
		}

		// Token: 0x060070AD RID: 28845 RVA: 0x001F2F18 File Offset: 0x001F1118
		private bool? GetIsGroupMailboxFromDisplayTypeEx(PropertyBag.BasicPropertyStore propertyBag)
		{
			RecipientDisplayType? valueAsNullable = propertyBag.GetValueAsNullable<RecipientDisplayType>(InternalSchema.DisplayTypeExInternal);
			if (valueAsNullable == null)
			{
				return null;
			}
			return new bool?(DisplayTypeExProperty.IsGroupMailbox(valueAsNullable.Value));
		}
	}
}
