using System;
using System.Globalization;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000770 RID: 1904
	internal sealed class PrimitiveArray
	{
		// Token: 0x06005340 RID: 21312 RVA: 0x00124A4C File Offset: 0x00122C4C
		internal PrimitiveArray(InternalPrimitiveTypeE code, Array array)
		{
			this.Init(code, array);
		}

		// Token: 0x06005341 RID: 21313 RVA: 0x00124A5C File Offset: 0x00122C5C
		internal void Init(InternalPrimitiveTypeE code, Array array)
		{
			this.code = code;
			switch (code)
			{
			case InternalPrimitiveTypeE.Boolean:
				this.booleanA = (bool[])array;
				return;
			case InternalPrimitiveTypeE.Byte:
			case InternalPrimitiveTypeE.Currency:
			case InternalPrimitiveTypeE.Decimal:
			case InternalPrimitiveTypeE.TimeSpan:
			case InternalPrimitiveTypeE.DateTime:
				break;
			case InternalPrimitiveTypeE.Char:
				this.charA = (char[])array;
				return;
			case InternalPrimitiveTypeE.Double:
				this.doubleA = (double[])array;
				return;
			case InternalPrimitiveTypeE.Int16:
				this.int16A = (short[])array;
				return;
			case InternalPrimitiveTypeE.Int32:
				this.int32A = (int[])array;
				return;
			case InternalPrimitiveTypeE.Int64:
				this.int64A = (long[])array;
				return;
			case InternalPrimitiveTypeE.SByte:
				this.sbyteA = (sbyte[])array;
				return;
			case InternalPrimitiveTypeE.Single:
				this.singleA = (float[])array;
				return;
			case InternalPrimitiveTypeE.UInt16:
				this.uint16A = (ushort[])array;
				return;
			case InternalPrimitiveTypeE.UInt32:
				this.uint32A = (uint[])array;
				return;
			case InternalPrimitiveTypeE.UInt64:
				this.uint64A = (ulong[])array;
				break;
			default:
				return;
			}
		}

		// Token: 0x06005342 RID: 21314 RVA: 0x00124B48 File Offset: 0x00122D48
		internal void SetValue(string value, int index)
		{
			switch (this.code)
			{
			case InternalPrimitiveTypeE.Boolean:
				this.booleanA[index] = bool.Parse(value);
				return;
			case InternalPrimitiveTypeE.Byte:
			case InternalPrimitiveTypeE.Currency:
			case InternalPrimitiveTypeE.Decimal:
			case InternalPrimitiveTypeE.TimeSpan:
			case InternalPrimitiveTypeE.DateTime:
				break;
			case InternalPrimitiveTypeE.Char:
				if (value[0] == '_' && value.Equals("_0x00_"))
				{
					this.charA[index] = '\0';
					return;
				}
				this.charA[index] = char.Parse(value);
				return;
			case InternalPrimitiveTypeE.Double:
				this.doubleA[index] = double.Parse(value, CultureInfo.InvariantCulture);
				return;
			case InternalPrimitiveTypeE.Int16:
				this.int16A[index] = short.Parse(value, CultureInfo.InvariantCulture);
				return;
			case InternalPrimitiveTypeE.Int32:
				this.int32A[index] = int.Parse(value, CultureInfo.InvariantCulture);
				return;
			case InternalPrimitiveTypeE.Int64:
				this.int64A[index] = long.Parse(value, CultureInfo.InvariantCulture);
				return;
			case InternalPrimitiveTypeE.SByte:
				this.sbyteA[index] = sbyte.Parse(value, CultureInfo.InvariantCulture);
				return;
			case InternalPrimitiveTypeE.Single:
				this.singleA[index] = float.Parse(value, CultureInfo.InvariantCulture);
				return;
			case InternalPrimitiveTypeE.UInt16:
				this.uint16A[index] = ushort.Parse(value, CultureInfo.InvariantCulture);
				return;
			case InternalPrimitiveTypeE.UInt32:
				this.uint32A[index] = uint.Parse(value, CultureInfo.InvariantCulture);
				return;
			case InternalPrimitiveTypeE.UInt64:
				this.uint64A[index] = ulong.Parse(value, CultureInfo.InvariantCulture);
				break;
			default:
				return;
			}
		}

		// Token: 0x040025B6 RID: 9654
		private InternalPrimitiveTypeE code;

		// Token: 0x040025B7 RID: 9655
		private bool[] booleanA;

		// Token: 0x040025B8 RID: 9656
		private char[] charA;

		// Token: 0x040025B9 RID: 9657
		private double[] doubleA;

		// Token: 0x040025BA RID: 9658
		private short[] int16A;

		// Token: 0x040025BB RID: 9659
		private int[] int32A;

		// Token: 0x040025BC RID: 9660
		private long[] int64A;

		// Token: 0x040025BD RID: 9661
		private sbyte[] sbyteA;

		// Token: 0x040025BE RID: 9662
		private float[] singleA;

		// Token: 0x040025BF RID: 9663
		private ushort[] uint16A;

		// Token: 0x040025C0 RID: 9664
		private uint[] uint32A;

		// Token: 0x040025C1 RID: 9665
		private ulong[] uint64A;
	}
}
