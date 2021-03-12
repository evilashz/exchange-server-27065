using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000773 RID: 1907
	internal sealed class Converter
	{
		// Token: 0x0600538D RID: 21389 RVA: 0x00127AEE File Offset: 0x00125CEE
		private Converter()
		{
		}

		// Token: 0x0600538E RID: 21390 RVA: 0x00127AF8 File Offset: 0x00125CF8
		internal static InternalPrimitiveTypeE ToCode(Type type)
		{
			InternalPrimitiveTypeE result;
			if (type != null && !type.IsPrimitive)
			{
				if (type == Converter.typeofDateTime)
				{
					result = InternalPrimitiveTypeE.DateTime;
				}
				else if (type == Converter.typeofTimeSpan)
				{
					result = InternalPrimitiveTypeE.TimeSpan;
				}
				else if (type == Converter.typeofDecimal)
				{
					result = InternalPrimitiveTypeE.Decimal;
				}
				else
				{
					result = InternalPrimitiveTypeE.Invalid;
				}
			}
			else
			{
				result = Converter.ToPrimitiveTypeEnum(Type.GetTypeCode(type));
			}
			return result;
		}

		// Token: 0x0600538F RID: 21391 RVA: 0x00127B48 File Offset: 0x00125D48
		internal static bool IsWriteAsByteArray(InternalPrimitiveTypeE code)
		{
			bool result = false;
			switch (code)
			{
			case InternalPrimitiveTypeE.Boolean:
			case InternalPrimitiveTypeE.Byte:
			case InternalPrimitiveTypeE.Char:
			case InternalPrimitiveTypeE.Double:
			case InternalPrimitiveTypeE.Int16:
			case InternalPrimitiveTypeE.Int32:
			case InternalPrimitiveTypeE.Int64:
			case InternalPrimitiveTypeE.SByte:
			case InternalPrimitiveTypeE.Single:
			case InternalPrimitiveTypeE.UInt16:
			case InternalPrimitiveTypeE.UInt32:
			case InternalPrimitiveTypeE.UInt64:
				result = true;
				break;
			}
			return result;
		}

		// Token: 0x06005390 RID: 21392 RVA: 0x00127BA4 File Offset: 0x00125DA4
		internal static int TypeLength(InternalPrimitiveTypeE code)
		{
			int result = 0;
			switch (code)
			{
			case InternalPrimitiveTypeE.Boolean:
				result = 1;
				break;
			case InternalPrimitiveTypeE.Byte:
				result = 1;
				break;
			case InternalPrimitiveTypeE.Char:
				result = 2;
				break;
			case InternalPrimitiveTypeE.Double:
				result = 8;
				break;
			case InternalPrimitiveTypeE.Int16:
				result = 2;
				break;
			case InternalPrimitiveTypeE.Int32:
				result = 4;
				break;
			case InternalPrimitiveTypeE.Int64:
				result = 8;
				break;
			case InternalPrimitiveTypeE.SByte:
				result = 1;
				break;
			case InternalPrimitiveTypeE.Single:
				result = 4;
				break;
			case InternalPrimitiveTypeE.UInt16:
				result = 2;
				break;
			case InternalPrimitiveTypeE.UInt32:
				result = 4;
				break;
			case InternalPrimitiveTypeE.UInt64:
				result = 8;
				break;
			}
			return result;
		}

		// Token: 0x06005391 RID: 21393 RVA: 0x00127C2C File Offset: 0x00125E2C
		internal static InternalNameSpaceE GetNameSpaceEnum(InternalPrimitiveTypeE code, Type type, WriteObjectInfo objectInfo, out string typeName)
		{
			InternalNameSpaceE internalNameSpaceE = InternalNameSpaceE.None;
			typeName = null;
			if (code != InternalPrimitiveTypeE.Invalid)
			{
				switch (code)
				{
				case InternalPrimitiveTypeE.Boolean:
				case InternalPrimitiveTypeE.Byte:
				case InternalPrimitiveTypeE.Char:
				case InternalPrimitiveTypeE.Double:
				case InternalPrimitiveTypeE.Int16:
				case InternalPrimitiveTypeE.Int32:
				case InternalPrimitiveTypeE.Int64:
				case InternalPrimitiveTypeE.SByte:
				case InternalPrimitiveTypeE.Single:
				case InternalPrimitiveTypeE.TimeSpan:
				case InternalPrimitiveTypeE.DateTime:
				case InternalPrimitiveTypeE.UInt16:
				case InternalPrimitiveTypeE.UInt32:
				case InternalPrimitiveTypeE.UInt64:
					internalNameSpaceE = InternalNameSpaceE.XdrPrimitive;
					typeName = "System." + Converter.ToComType(code);
					break;
				case InternalPrimitiveTypeE.Decimal:
					internalNameSpaceE = InternalNameSpaceE.UrtSystem;
					typeName = "System." + Converter.ToComType(code);
					break;
				}
			}
			if (internalNameSpaceE == InternalNameSpaceE.None && type != null)
			{
				if (type == Converter.typeofString)
				{
					internalNameSpaceE = InternalNameSpaceE.XdrString;
				}
				else if (objectInfo == null)
				{
					typeName = type.FullName;
					if (type.Assembly == Converter.urtAssembly)
					{
						internalNameSpaceE = InternalNameSpaceE.UrtSystem;
					}
					else
					{
						internalNameSpaceE = InternalNameSpaceE.UrtUser;
					}
				}
				else
				{
					typeName = objectInfo.GetTypeFullName();
					if (objectInfo.GetAssemblyString().Equals(Converter.urtAssemblyString))
					{
						internalNameSpaceE = InternalNameSpaceE.UrtSystem;
					}
					else
					{
						internalNameSpaceE = InternalNameSpaceE.UrtUser;
					}
				}
			}
			return internalNameSpaceE;
		}

		// Token: 0x06005392 RID: 21394 RVA: 0x00127D0D File Offset: 0x00125F0D
		internal static Type ToArrayType(InternalPrimitiveTypeE code)
		{
			if (Converter.arrayTypeA == null)
			{
				Converter.InitArrayTypeA();
			}
			return Converter.arrayTypeA[(int)code];
		}

		// Token: 0x06005393 RID: 21395 RVA: 0x00127D28 File Offset: 0x00125F28
		private static void InitTypeA()
		{
			Type[] array = new Type[Converter.primitiveTypeEnumLength];
			array[0] = null;
			array[1] = Converter.typeofBoolean;
			array[2] = Converter.typeofByte;
			array[3] = Converter.typeofChar;
			array[5] = Converter.typeofDecimal;
			array[6] = Converter.typeofDouble;
			array[7] = Converter.typeofInt16;
			array[8] = Converter.typeofInt32;
			array[9] = Converter.typeofInt64;
			array[10] = Converter.typeofSByte;
			array[11] = Converter.typeofSingle;
			array[12] = Converter.typeofTimeSpan;
			array[13] = Converter.typeofDateTime;
			array[14] = Converter.typeofUInt16;
			array[15] = Converter.typeofUInt32;
			array[16] = Converter.typeofUInt64;
			Converter.typeA = array;
		}

		// Token: 0x06005394 RID: 21396 RVA: 0x00127DCC File Offset: 0x00125FCC
		private static void InitArrayTypeA()
		{
			Type[] array = new Type[Converter.primitiveTypeEnumLength];
			array[0] = null;
			array[1] = Converter.typeofBooleanArray;
			array[2] = Converter.typeofByteArray;
			array[3] = Converter.typeofCharArray;
			array[5] = Converter.typeofDecimalArray;
			array[6] = Converter.typeofDoubleArray;
			array[7] = Converter.typeofInt16Array;
			array[8] = Converter.typeofInt32Array;
			array[9] = Converter.typeofInt64Array;
			array[10] = Converter.typeofSByteArray;
			array[11] = Converter.typeofSingleArray;
			array[12] = Converter.typeofTimeSpanArray;
			array[13] = Converter.typeofDateTimeArray;
			array[14] = Converter.typeofUInt16Array;
			array[15] = Converter.typeofUInt32Array;
			array[16] = Converter.typeofUInt64Array;
			Converter.arrayTypeA = array;
		}

		// Token: 0x06005395 RID: 21397 RVA: 0x00127E70 File Offset: 0x00126070
		internal static Type ToType(InternalPrimitiveTypeE code)
		{
			if (Converter.typeA == null)
			{
				Converter.InitTypeA();
			}
			return Converter.typeA[(int)code];
		}

		// Token: 0x06005396 RID: 21398 RVA: 0x00127E8C File Offset: 0x0012608C
		internal static Array CreatePrimitiveArray(InternalPrimitiveTypeE code, int length)
		{
			Array result = null;
			switch (code)
			{
			case InternalPrimitiveTypeE.Boolean:
				result = new bool[length];
				break;
			case InternalPrimitiveTypeE.Byte:
				result = new byte[length];
				break;
			case InternalPrimitiveTypeE.Char:
				result = new char[length];
				break;
			case InternalPrimitiveTypeE.Decimal:
				result = new decimal[length];
				break;
			case InternalPrimitiveTypeE.Double:
				result = new double[length];
				break;
			case InternalPrimitiveTypeE.Int16:
				result = new short[length];
				break;
			case InternalPrimitiveTypeE.Int32:
				result = new int[length];
				break;
			case InternalPrimitiveTypeE.Int64:
				result = new long[length];
				break;
			case InternalPrimitiveTypeE.SByte:
				result = new sbyte[length];
				break;
			case InternalPrimitiveTypeE.Single:
				result = new float[length];
				break;
			case InternalPrimitiveTypeE.TimeSpan:
				result = new TimeSpan[length];
				break;
			case InternalPrimitiveTypeE.DateTime:
				result = new DateTime[length];
				break;
			case InternalPrimitiveTypeE.UInt16:
				result = new ushort[length];
				break;
			case InternalPrimitiveTypeE.UInt32:
				result = new uint[length];
				break;
			case InternalPrimitiveTypeE.UInt64:
				result = new ulong[length];
				break;
			}
			return result;
		}

		// Token: 0x06005397 RID: 21399 RVA: 0x00127F70 File Offset: 0x00126170
		internal static bool IsPrimitiveArray(Type type, out object typeInformation)
		{
			typeInformation = null;
			bool result = true;
			if (type == Converter.typeofBooleanArray)
			{
				typeInformation = InternalPrimitiveTypeE.Boolean;
			}
			else if (type == Converter.typeofByteArray)
			{
				typeInformation = InternalPrimitiveTypeE.Byte;
			}
			else if (type == Converter.typeofCharArray)
			{
				typeInformation = InternalPrimitiveTypeE.Char;
			}
			else if (type == Converter.typeofDoubleArray)
			{
				typeInformation = InternalPrimitiveTypeE.Double;
			}
			else if (type == Converter.typeofInt16Array)
			{
				typeInformation = InternalPrimitiveTypeE.Int16;
			}
			else if (type == Converter.typeofInt32Array)
			{
				typeInformation = InternalPrimitiveTypeE.Int32;
			}
			else if (type == Converter.typeofInt64Array)
			{
				typeInformation = InternalPrimitiveTypeE.Int64;
			}
			else if (type == Converter.typeofSByteArray)
			{
				typeInformation = InternalPrimitiveTypeE.SByte;
			}
			else if (type == Converter.typeofSingleArray)
			{
				typeInformation = InternalPrimitiveTypeE.Single;
			}
			else if (type == Converter.typeofUInt16Array)
			{
				typeInformation = InternalPrimitiveTypeE.UInt16;
			}
			else if (type == Converter.typeofUInt32Array)
			{
				typeInformation = InternalPrimitiveTypeE.UInt32;
			}
			else if (type == Converter.typeofUInt64Array)
			{
				typeInformation = InternalPrimitiveTypeE.UInt64;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06005398 RID: 21400 RVA: 0x00128074 File Offset: 0x00126274
		private static void InitValueA()
		{
			string[] array = new string[Converter.primitiveTypeEnumLength];
			array[0] = null;
			array[1] = "Boolean";
			array[2] = "Byte";
			array[3] = "Char";
			array[5] = "Decimal";
			array[6] = "Double";
			array[7] = "Int16";
			array[8] = "Int32";
			array[9] = "Int64";
			array[10] = "SByte";
			array[11] = "Single";
			array[12] = "TimeSpan";
			array[13] = "DateTime";
			array[14] = "UInt16";
			array[15] = "UInt32";
			array[16] = "UInt64";
			Converter.valueA = array;
		}

		// Token: 0x06005399 RID: 21401 RVA: 0x00128118 File Offset: 0x00126318
		internal static string ToComType(InternalPrimitiveTypeE code)
		{
			if (Converter.valueA == null)
			{
				Converter.InitValueA();
			}
			return Converter.valueA[(int)code];
		}

		// Token: 0x0600539A RID: 21402 RVA: 0x00128134 File Offset: 0x00126334
		private static void InitTypeCodeA()
		{
			TypeCode[] array = new TypeCode[Converter.primitiveTypeEnumLength];
			array[0] = TypeCode.Object;
			array[1] = TypeCode.Boolean;
			array[2] = TypeCode.Byte;
			array[3] = TypeCode.Char;
			array[5] = TypeCode.Decimal;
			array[6] = TypeCode.Double;
			array[7] = TypeCode.Int16;
			array[8] = TypeCode.Int32;
			array[9] = TypeCode.Int64;
			array[10] = TypeCode.SByte;
			array[11] = TypeCode.Single;
			array[12] = TypeCode.Object;
			array[13] = TypeCode.DateTime;
			array[14] = TypeCode.UInt16;
			array[15] = TypeCode.UInt32;
			array[16] = TypeCode.UInt64;
			Converter.typeCodeA = array;
		}

		// Token: 0x0600539B RID: 21403 RVA: 0x001281A4 File Offset: 0x001263A4
		internal static TypeCode ToTypeCode(InternalPrimitiveTypeE code)
		{
			if (Converter.typeCodeA == null)
			{
				Converter.InitTypeCodeA();
			}
			return Converter.typeCodeA[(int)code];
		}

		// Token: 0x0600539C RID: 21404 RVA: 0x001281C0 File Offset: 0x001263C0
		private static void InitCodeA()
		{
			Converter.codeA = new InternalPrimitiveTypeE[]
			{
				InternalPrimitiveTypeE.Invalid,
				InternalPrimitiveTypeE.Invalid,
				InternalPrimitiveTypeE.Invalid,
				InternalPrimitiveTypeE.Boolean,
				InternalPrimitiveTypeE.Char,
				InternalPrimitiveTypeE.SByte,
				InternalPrimitiveTypeE.Byte,
				InternalPrimitiveTypeE.Int16,
				InternalPrimitiveTypeE.UInt16,
				InternalPrimitiveTypeE.Int32,
				InternalPrimitiveTypeE.UInt32,
				InternalPrimitiveTypeE.Int64,
				InternalPrimitiveTypeE.UInt64,
				InternalPrimitiveTypeE.Single,
				InternalPrimitiveTypeE.Double,
				InternalPrimitiveTypeE.Decimal,
				InternalPrimitiveTypeE.DateTime,
				InternalPrimitiveTypeE.Invalid,
				InternalPrimitiveTypeE.Invalid
			};
		}

		// Token: 0x0600539D RID: 21405 RVA: 0x0012823A File Offset: 0x0012643A
		internal static InternalPrimitiveTypeE ToPrimitiveTypeEnum(TypeCode typeCode)
		{
			if (Converter.codeA == null)
			{
				Converter.InitCodeA();
			}
			return Converter.codeA[(int)typeCode];
		}

		// Token: 0x0600539E RID: 21406 RVA: 0x00128254 File Offset: 0x00126454
		internal static object FromString(string value, InternalPrimitiveTypeE code)
		{
			object result;
			if (code != InternalPrimitiveTypeE.Invalid)
			{
				result = Convert.ChangeType(value, Converter.ToTypeCode(code), CultureInfo.InvariantCulture);
			}
			else
			{
				result = value;
			}
			return result;
		}

		// Token: 0x040025F7 RID: 9719
		private static int primitiveTypeEnumLength = 17;

		// Token: 0x040025F8 RID: 9720
		private static volatile Type[] typeA;

		// Token: 0x040025F9 RID: 9721
		private static volatile Type[] arrayTypeA;

		// Token: 0x040025FA RID: 9722
		private static volatile string[] valueA;

		// Token: 0x040025FB RID: 9723
		private static volatile TypeCode[] typeCodeA;

		// Token: 0x040025FC RID: 9724
		private static volatile InternalPrimitiveTypeE[] codeA;

		// Token: 0x040025FD RID: 9725
		internal static Type typeofISerializable = typeof(ISerializable);

		// Token: 0x040025FE RID: 9726
		internal static Type typeofString = typeof(string);

		// Token: 0x040025FF RID: 9727
		internal static Type typeofConverter = typeof(Converter);

		// Token: 0x04002600 RID: 9728
		internal static Type typeofBoolean = typeof(bool);

		// Token: 0x04002601 RID: 9729
		internal static Type typeofByte = typeof(byte);

		// Token: 0x04002602 RID: 9730
		internal static Type typeofChar = typeof(char);

		// Token: 0x04002603 RID: 9731
		internal static Type typeofDecimal = typeof(decimal);

		// Token: 0x04002604 RID: 9732
		internal static Type typeofDouble = typeof(double);

		// Token: 0x04002605 RID: 9733
		internal static Type typeofInt16 = typeof(short);

		// Token: 0x04002606 RID: 9734
		internal static Type typeofInt32 = typeof(int);

		// Token: 0x04002607 RID: 9735
		internal static Type typeofInt64 = typeof(long);

		// Token: 0x04002608 RID: 9736
		internal static Type typeofSByte = typeof(sbyte);

		// Token: 0x04002609 RID: 9737
		internal static Type typeofSingle = typeof(float);

		// Token: 0x0400260A RID: 9738
		internal static Type typeofTimeSpan = typeof(TimeSpan);

		// Token: 0x0400260B RID: 9739
		internal static Type typeofDateTime = typeof(DateTime);

		// Token: 0x0400260C RID: 9740
		internal static Type typeofUInt16 = typeof(ushort);

		// Token: 0x0400260D RID: 9741
		internal static Type typeofUInt32 = typeof(uint);

		// Token: 0x0400260E RID: 9742
		internal static Type typeofUInt64 = typeof(ulong);

		// Token: 0x0400260F RID: 9743
		internal static Type typeofObject = typeof(object);

		// Token: 0x04002610 RID: 9744
		internal static Type typeofSystemVoid = typeof(void);

		// Token: 0x04002611 RID: 9745
		internal static Assembly urtAssembly = Assembly.GetAssembly(Converter.typeofString);

		// Token: 0x04002612 RID: 9746
		internal static string urtAssemblyString = Converter.urtAssembly.FullName;

		// Token: 0x04002613 RID: 9747
		internal static Type typeofTypeArray = typeof(Type[]);

		// Token: 0x04002614 RID: 9748
		internal static Type typeofObjectArray = typeof(object[]);

		// Token: 0x04002615 RID: 9749
		internal static Type typeofStringArray = typeof(string[]);

		// Token: 0x04002616 RID: 9750
		internal static Type typeofBooleanArray = typeof(bool[]);

		// Token: 0x04002617 RID: 9751
		internal static Type typeofByteArray = typeof(byte[]);

		// Token: 0x04002618 RID: 9752
		internal static Type typeofCharArray = typeof(char[]);

		// Token: 0x04002619 RID: 9753
		internal static Type typeofDecimalArray = typeof(decimal[]);

		// Token: 0x0400261A RID: 9754
		internal static Type typeofDoubleArray = typeof(double[]);

		// Token: 0x0400261B RID: 9755
		internal static Type typeofInt16Array = typeof(short[]);

		// Token: 0x0400261C RID: 9756
		internal static Type typeofInt32Array = typeof(int[]);

		// Token: 0x0400261D RID: 9757
		internal static Type typeofInt64Array = typeof(long[]);

		// Token: 0x0400261E RID: 9758
		internal static Type typeofSByteArray = typeof(sbyte[]);

		// Token: 0x0400261F RID: 9759
		internal static Type typeofSingleArray = typeof(float[]);

		// Token: 0x04002620 RID: 9760
		internal static Type typeofTimeSpanArray = typeof(TimeSpan[]);

		// Token: 0x04002621 RID: 9761
		internal static Type typeofDateTimeArray = typeof(DateTime[]);

		// Token: 0x04002622 RID: 9762
		internal static Type typeofUInt16Array = typeof(ushort[]);

		// Token: 0x04002623 RID: 9763
		internal static Type typeofUInt32Array = typeof(uint[]);

		// Token: 0x04002624 RID: 9764
		internal static Type typeofUInt64Array = typeof(ulong[]);

		// Token: 0x04002625 RID: 9765
		internal static Type typeofMarshalByRefObject = typeof(MarshalByRefObject);
	}
}
