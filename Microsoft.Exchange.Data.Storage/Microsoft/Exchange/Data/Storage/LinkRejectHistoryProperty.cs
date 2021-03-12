using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C7C RID: 3196
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal sealed class LinkRejectHistoryProperty : SmartPropertyDefinition
	{
		// Token: 0x06007029 RID: 28713 RVA: 0x001F0AD8 File Offset: 0x001EECD8
		internal LinkRejectHistoryProperty() : base("LinkRejectHistory", typeof(PersonId[]), PropertyFlags.None, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.LinkRejectHistoryRaw, PropertyDependencyType.AllRead)
		})
		{
		}

		// Token: 0x0600702A RID: 28714 RVA: 0x001F0B18 File Offset: 0x001EED18
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			object value = propertyBag.GetValue(InternalSchema.LinkRejectHistoryRaw);
			byte[][] array = value as byte[][];
			if (array != null)
			{
				try
				{
					PersonId[] array2 = new PersonId[array.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array2[i] = PersonId.Create(array[i]);
					}
					return array2;
				}
				catch (ArgumentException)
				{
				}
				return new PropertyError(this, PropertyErrorCode.CorruptedData);
			}
			if (value is PropertyError)
			{
				return value;
			}
			return new PropertyError(this, PropertyErrorCode.CorruptedData);
		}

		// Token: 0x0600702B RID: 28715 RVA: 0x001F0B94 File Offset: 0x001EED94
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			PersonId[] array = value as PersonId[];
			if (array == null)
			{
				throw new ArgumentException("value");
			}
			byte[][] array2 = new byte[array.Length][];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = array[i].GetBytes();
			}
			propertyBag.SetValue(InternalSchema.LinkRejectHistoryRaw, array2);
		}

		// Token: 0x0600702C RID: 28716 RVA: 0x001F0BE5 File Offset: 0x001EEDE5
		protected override void InternalDeleteValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			propertyBag.Delete(InternalSchema.LinkRejectHistoryRaw);
		}
	}
}
