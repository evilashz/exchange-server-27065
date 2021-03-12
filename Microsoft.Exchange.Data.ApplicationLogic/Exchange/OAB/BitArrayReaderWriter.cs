using System;
using System.Collections;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.OAB
{
	// Token: 0x02000143 RID: 323
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class BitArrayReaderWriter
	{
		// Token: 0x06000D3F RID: 3391 RVA: 0x00037D30 File Offset: 0x00035F30
		public static void WriteTo(BinaryWriter writer, BitArray bitArray)
		{
			byte[] array = new byte[(bitArray.Length + 7) / 8];
			for (int i = 0; i < bitArray.Length; i++)
			{
				if (bitArray.Get(i))
				{
					byte[] array2 = array;
					int num = i / 8;
					array2[num] |= BitArrayReaderWriter.BitArrayBitValue(i);
				}
			}
			writer.Write(array);
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x00037D8C File Offset: 0x00035F8C
		public static BitArray ReadFrom(BinaryReader reader, int count, string elementName)
		{
			BitArray bitArray = new BitArray(count);
			byte[] array = reader.ReadBytes((bitArray.Length + 7) / 8, elementName);
			for (int i = 0; i < count; i++)
			{
				bitArray.Set(i, (array[i / 8] & BitArrayReaderWriter.BitArrayBitValue(i)) != 0);
			}
			return bitArray;
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x00037DD8 File Offset: 0x00035FD8
		private static byte BitArrayBitValue(int index)
		{
			return (byte)(128 >> index % 8);
		}
	}
}
