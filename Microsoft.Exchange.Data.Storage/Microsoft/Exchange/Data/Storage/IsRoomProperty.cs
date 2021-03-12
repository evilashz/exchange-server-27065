using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C97 RID: 3223
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal sealed class IsRoomProperty : SmartPropertyDefinition
	{
		// Token: 0x060070A5 RID: 28837 RVA: 0x001F2D7C File Offset: 0x001F0F7C
		public IsRoomProperty() : base("IsRoom", typeof(bool), PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.DisplayTypeExInternal, PropertyDependencyType.NeedForRead)
		})
		{
		}

		// Token: 0x060070A6 RID: 28838 RVA: 0x001F2DBA File Offset: 0x001F0FBA
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			return this.GetIsRoomFromDisplayTypeEx(propertyBag) ?? new PropertyError(this, PropertyErrorCode.NotFound);
		}

		// Token: 0x060070A7 RID: 28839 RVA: 0x001F2DD4 File Offset: 0x001F0FD4
		private bool? GetIsRoomFromDisplayTypeEx(PropertyBag.BasicPropertyStore propertyBag)
		{
			RecipientDisplayType? valueAsNullable = propertyBag.GetValueAsNullable<RecipientDisplayType>(InternalSchema.DisplayTypeExInternal);
			if (valueAsNullable == null)
			{
				return null;
			}
			return new bool?(DisplayTypeExProperty.IsRoom(valueAsNullable.Value));
		}
	}
}
