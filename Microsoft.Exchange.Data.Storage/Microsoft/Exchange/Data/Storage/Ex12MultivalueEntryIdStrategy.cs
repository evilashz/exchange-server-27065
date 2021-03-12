using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000047 RID: 71
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class Ex12MultivalueEntryIdStrategy : LocationEntryIdStrategy
	{
		// Token: 0x06000592 RID: 1426 RVA: 0x0002D822 File Offset: 0x0002BA22
		internal Ex12MultivalueEntryIdStrategy(StorePropertyDefinition property, LocationEntryIdStrategy.GetLocationPropertyBagDelegate getLocationPropertyBag, int index) : base(property, getLocationPropertyBag)
		{
			this.index = index;
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0002D834 File Offset: 0x0002BA34
		internal override byte[] GetEntryId(DefaultFolderContext context)
		{
			byte[][] array = this.GetLocationPropertyBag(context).TryGetProperty(this.Property) as byte[][];
			if (array == null || array.Length <= this.index)
			{
				return null;
			}
			byte[] array2 = array[this.index];
			if (!IdConverter.IsFolderId(array2))
			{
				return null;
			}
			return array2;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0002D884 File Offset: 0x0002BA84
		internal override void SetEntryId(DefaultFolderContext context, byte[] entryId)
		{
			byte[][] array = this.GetLocationPropertyBag(context).TryGetProperty(this.Property) as byte[][];
			byte[][] array2 = Ex12MultivalueEntryIdStrategy.CreateMultiValuedPropertyValue(array, entryId, this.index, 6);
			if (array == null || array.Length <= 5)
			{
				uint value = ComputeCRC.Compute(0U, entryId);
				array2[5] = BitConverter.GetBytes(value);
			}
			base.SetEntryValueInternal(context, array2);
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0002D8E0 File Offset: 0x0002BAE0
		protected static byte[][] CreateMultiValuedPropertyValue(byte[][] entryIds, byte[] entryId, int index, int length)
		{
			byte[][] array;
			if (entryIds == null)
			{
				array = new byte[length][];
			}
			else if (entryIds.Length < length)
			{
				array = new byte[length][];
				for (int i = 0; i < entryIds.Length; i++)
				{
					array[i] = entryIds[i];
				}
			}
			else
			{
				array = entryIds;
			}
			array[index] = entryId;
			for (int j = 0; j < array.Length; j++)
			{
				if (array[j] == null)
				{
					array[j] = Array<byte>.Empty;
				}
			}
			return array;
		}

		// Token: 0x0400018A RID: 394
		protected int index;
	}
}
