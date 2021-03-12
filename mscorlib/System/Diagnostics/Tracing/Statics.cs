using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000453 RID: 1107
	internal static class Statics
	{
		// Token: 0x060035D6 RID: 13782 RVA: 0x000CF170 File Offset: 0x000CD370
		public static byte[] MetadataForString(string name, int prefixSize, int suffixSize, int additionalSize)
		{
			Statics.CheckName(name);
			int num = Encoding.UTF8.GetByteCount(name) + 3 + prefixSize + suffixSize;
			byte[] array = new byte[num];
			ushort num2 = checked((ushort)(num + additionalSize));
			array[0] = (byte)num2;
			array[1] = (byte)(num2 >> 8);
			Encoding.UTF8.GetBytes(name, 0, name.Length, array, 2 + prefixSize);
			return array;
		}

		// Token: 0x060035D7 RID: 13783 RVA: 0x000CF1C8 File Offset: 0x000CD3C8
		public static void EncodeTags(int tags, ref int pos, byte[] metadata)
		{
			int num = tags & 268435455;
			bool flag;
			do
			{
				byte b = (byte)(num >> 21 & 127);
				flag = ((num & 2097151) != 0);
				b |= (flag ? 128 : 0);
				num <<= 7;
				if (metadata != null)
				{
					metadata[pos] = b;
				}
				pos++;
			}
			while (flag);
		}

		// Token: 0x060035D8 RID: 13784 RVA: 0x000CF216 File Offset: 0x000CD416
		public static byte Combine(int settingValue, byte defaultValue)
		{
			if ((int)((byte)settingValue) != settingValue)
			{
				return defaultValue;
			}
			return (byte)settingValue;
		}

		// Token: 0x060035D9 RID: 13785 RVA: 0x000CF221 File Offset: 0x000CD421
		public static byte Combine(int settingValue1, int settingValue2, byte defaultValue)
		{
			if ((int)((byte)settingValue1) == settingValue1)
			{
				return (byte)settingValue1;
			}
			if ((int)((byte)settingValue2) != settingValue2)
			{
				return defaultValue;
			}
			return (byte)settingValue2;
		}

		// Token: 0x060035DA RID: 13786 RVA: 0x000CF234 File Offset: 0x000CD434
		public static int Combine(int settingValue1, int settingValue2)
		{
			if ((int)((byte)settingValue1) != settingValue1)
			{
				return settingValue2;
			}
			return settingValue1;
		}

		// Token: 0x060035DB RID: 13787 RVA: 0x000CF23E File Offset: 0x000CD43E
		public static void CheckName(string name)
		{
			if (name != null && 0 <= name.IndexOf('\0'))
			{
				throw new ArgumentOutOfRangeException("name");
			}
		}

		// Token: 0x060035DC RID: 13788 RVA: 0x000CF258 File Offset: 0x000CD458
		public static bool ShouldOverrideFieldName(string fieldName)
		{
			return fieldName.Length <= 2 && fieldName[0] == '_';
		}

		// Token: 0x060035DD RID: 13789 RVA: 0x000CF270 File Offset: 0x000CD470
		public static TraceLoggingDataType MakeDataType(TraceLoggingDataType baseType, EventFieldFormat format)
		{
			return (baseType & (TraceLoggingDataType)31) | (TraceLoggingDataType)(format << 8);
		}

		// Token: 0x060035DE RID: 13790 RVA: 0x000CF27A File Offset: 0x000CD47A
		public static TraceLoggingDataType Format8(EventFieldFormat format, TraceLoggingDataType native)
		{
			switch (format)
			{
			case EventFieldFormat.Default:
				return native;
			case EventFieldFormat.String:
				return TraceLoggingDataType.Char8;
			case EventFieldFormat.Boolean:
				return TraceLoggingDataType.Boolean8;
			case EventFieldFormat.Hexadecimal:
				return TraceLoggingDataType.HexInt8;
			}
			return Statics.MakeDataType(native, format);
		}

		// Token: 0x060035DF RID: 13791 RVA: 0x000CF2B3 File Offset: 0x000CD4B3
		public static TraceLoggingDataType Format16(EventFieldFormat format, TraceLoggingDataType native)
		{
			switch (format)
			{
			case EventFieldFormat.Default:
				return native;
			case EventFieldFormat.String:
				return TraceLoggingDataType.Char16;
			case EventFieldFormat.Hexadecimal:
				return TraceLoggingDataType.HexInt16;
			}
			return Statics.MakeDataType(native, format);
		}

		// Token: 0x060035E0 RID: 13792 RVA: 0x000CF2E6 File Offset: 0x000CD4E6
		public static TraceLoggingDataType Format32(EventFieldFormat format, TraceLoggingDataType native)
		{
			switch (format)
			{
			case EventFieldFormat.Default:
				return native;
			case (EventFieldFormat)1:
			case EventFieldFormat.String:
				break;
			case EventFieldFormat.Boolean:
				return TraceLoggingDataType.Boolean32;
			case EventFieldFormat.Hexadecimal:
				return TraceLoggingDataType.HexInt32;
			default:
				if (format == EventFieldFormat.HResult)
				{
					return TraceLoggingDataType.HResult;
				}
				break;
			}
			return Statics.MakeDataType(native, format);
		}

		// Token: 0x060035E1 RID: 13793 RVA: 0x000CF31E File Offset: 0x000CD51E
		public static TraceLoggingDataType Format64(EventFieldFormat format, TraceLoggingDataType native)
		{
			if (format == EventFieldFormat.Default)
			{
				return native;
			}
			if (format != EventFieldFormat.Hexadecimal)
			{
				return Statics.MakeDataType(native, format);
			}
			return TraceLoggingDataType.HexInt64;
		}

		// Token: 0x060035E2 RID: 13794 RVA: 0x000CF335 File Offset: 0x000CD535
		public static TraceLoggingDataType FormatPtr(EventFieldFormat format, TraceLoggingDataType native)
		{
			if (format == EventFieldFormat.Default)
			{
				return native;
			}
			if (format != EventFieldFormat.Hexadecimal)
			{
				return Statics.MakeDataType(native, format);
			}
			return Statics.HexIntPtrType;
		}

		// Token: 0x060035E3 RID: 13795 RVA: 0x000CF34F File Offset: 0x000CD54F
		public static object CreateInstance(Type type, params object[] parameters)
		{
			return Activator.CreateInstance(type, parameters);
		}

		// Token: 0x060035E4 RID: 13796 RVA: 0x000CF358 File Offset: 0x000CD558
		public static bool IsValueType(Type type)
		{
			return type.IsValueType;
		}

		// Token: 0x060035E5 RID: 13797 RVA: 0x000CF370 File Offset: 0x000CD570
		public static bool IsEnum(Type type)
		{
			return type.IsEnum;
		}

		// Token: 0x060035E6 RID: 13798 RVA: 0x000CF388 File Offset: 0x000CD588
		public static IEnumerable<PropertyInfo> GetProperties(Type type)
		{
			return type.GetProperties();
		}

		// Token: 0x060035E7 RID: 13799 RVA: 0x000CF3A0 File Offset: 0x000CD5A0
		public static MethodInfo GetGetMethod(PropertyInfo propInfo)
		{
			return propInfo.GetGetMethod();
		}

		// Token: 0x060035E8 RID: 13800 RVA: 0x000CF3B8 File Offset: 0x000CD5B8
		public static MethodInfo GetDeclaredStaticMethod(Type declaringType, string name)
		{
			return declaringType.GetMethod(name, BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.NonPublic);
		}

		// Token: 0x060035E9 RID: 13801 RVA: 0x000CF3D0 File Offset: 0x000CD5D0
		public static bool HasCustomAttribute(PropertyInfo propInfo, Type attributeType)
		{
			object[] customAttributes = propInfo.GetCustomAttributes(attributeType, false);
			return customAttributes.Length != 0;
		}

		// Token: 0x060035EA RID: 13802 RVA: 0x000CF3F0 File Offset: 0x000CD5F0
		public static AttributeType GetCustomAttribute<AttributeType>(PropertyInfo propInfo) where AttributeType : Attribute
		{
			AttributeType result = default(AttributeType);
			object[] customAttributes = propInfo.GetCustomAttributes(typeof(AttributeType), false);
			if (customAttributes.Length != 0)
			{
				result = (AttributeType)((object)customAttributes[0]);
			}
			return result;
		}

		// Token: 0x060035EB RID: 13803 RVA: 0x000CF428 File Offset: 0x000CD628
		public static AttributeType GetCustomAttribute<AttributeType>(Type type) where AttributeType : Attribute
		{
			AttributeType result = default(AttributeType);
			object[] customAttributes = type.GetCustomAttributes(typeof(AttributeType), false);
			if (customAttributes.Length != 0)
			{
				result = (AttributeType)((object)customAttributes[0]);
			}
			return result;
		}

		// Token: 0x060035EC RID: 13804 RVA: 0x000CF45D File Offset: 0x000CD65D
		public static Type[] GetGenericArguments(Type type)
		{
			return type.GetGenericArguments();
		}

		// Token: 0x060035ED RID: 13805 RVA: 0x000CF468 File Offset: 0x000CD668
		public static Type FindEnumerableElementType(Type type)
		{
			Type type2 = null;
			if (Statics.IsGenericMatch(type, typeof(IEnumerable<>)))
			{
				type2 = Statics.GetGenericArguments(type)[0];
			}
			else
			{
				Type[] array = type.FindInterfaces(new TypeFilter(Statics.IsGenericMatch), typeof(IEnumerable<>));
				foreach (Type type3 in array)
				{
					if (type2 != null)
					{
						type2 = null;
						break;
					}
					type2 = Statics.GetGenericArguments(type3)[0];
				}
			}
			return type2;
		}

		// Token: 0x060035EE RID: 13806 RVA: 0x000CF4E0 File Offset: 0x000CD6E0
		public static bool IsGenericMatch(Type type, object openType)
		{
			bool isGenericType = type.IsGenericType;
			return isGenericType && type.GetGenericTypeDefinition() == (Type)openType;
		}

		// Token: 0x060035EF RID: 13807 RVA: 0x000CF50C File Offset: 0x000CD70C
		public static Delegate CreateDelegate(Type delegateType, MethodInfo methodInfo)
		{
			return Delegate.CreateDelegate(delegateType, methodInfo);
		}

		// Token: 0x060035F0 RID: 13808 RVA: 0x000CF524 File Offset: 0x000CD724
		public static TraceLoggingTypeInfo GetTypeInfoInstance(Type dataType, List<Type> recursionCheck)
		{
			TraceLoggingTypeInfo result;
			if (dataType == typeof(int))
			{
				result = TraceLoggingTypeInfo<int>.Instance;
			}
			else if (dataType == typeof(long))
			{
				result = TraceLoggingTypeInfo<long>.Instance;
			}
			else if (dataType == typeof(string))
			{
				result = TraceLoggingTypeInfo<string>.Instance;
			}
			else
			{
				MethodInfo declaredStaticMethod = Statics.GetDeclaredStaticMethod(typeof(TraceLoggingTypeInfo<>).MakeGenericType(new Type[]
				{
					dataType
				}), "GetInstance");
				object obj = declaredStaticMethod.Invoke(null, new object[]
				{
					recursionCheck
				});
				result = (TraceLoggingTypeInfo)obj;
			}
			return result;
		}

		// Token: 0x060035F1 RID: 13809 RVA: 0x000CF5C0 File Offset: 0x000CD7C0
		public static TraceLoggingTypeInfo<DataType> CreateDefaultTypeInfo<DataType>(List<Type> recursionCheck)
		{
			Type typeFromHandle = typeof(DataType);
			if (recursionCheck.Contains(typeFromHandle))
			{
				throw new NotSupportedException(Environment.GetResourceString("EventSource_RecursiveTypeDefinition"));
			}
			recursionCheck.Add(typeFromHandle);
			EventDataAttribute customAttribute = Statics.GetCustomAttribute<EventDataAttribute>(typeFromHandle);
			TraceLoggingTypeInfo traceLoggingTypeInfo;
			if (customAttribute != null || Statics.GetCustomAttribute<CompilerGeneratedAttribute>(typeFromHandle) != null)
			{
				TypeAnalysis typeAnalysis = new TypeAnalysis(typeFromHandle, customAttribute, recursionCheck);
				traceLoggingTypeInfo = new InvokeTypeInfo<DataType>(typeAnalysis);
			}
			else if (typeFromHandle.IsArray)
			{
				Type elementType = typeFromHandle.GetElementType();
				if (elementType == typeof(bool))
				{
					traceLoggingTypeInfo = new BooleanArrayTypeInfo();
				}
				else if (elementType == typeof(byte))
				{
					traceLoggingTypeInfo = new ByteArrayTypeInfo();
				}
				else if (elementType == typeof(sbyte))
				{
					traceLoggingTypeInfo = new SByteArrayTypeInfo();
				}
				else if (elementType == typeof(short))
				{
					traceLoggingTypeInfo = new Int16ArrayTypeInfo();
				}
				else if (elementType == typeof(ushort))
				{
					traceLoggingTypeInfo = new UInt16ArrayTypeInfo();
				}
				else if (elementType == typeof(int))
				{
					traceLoggingTypeInfo = new Int32ArrayTypeInfo();
				}
				else if (elementType == typeof(uint))
				{
					traceLoggingTypeInfo = new UInt32ArrayTypeInfo();
				}
				else if (elementType == typeof(long))
				{
					traceLoggingTypeInfo = new Int64ArrayTypeInfo();
				}
				else if (elementType == typeof(ulong))
				{
					traceLoggingTypeInfo = new UInt64ArrayTypeInfo();
				}
				else if (elementType == typeof(char))
				{
					traceLoggingTypeInfo = new CharArrayTypeInfo();
				}
				else if (elementType == typeof(double))
				{
					traceLoggingTypeInfo = new DoubleArrayTypeInfo();
				}
				else if (elementType == typeof(float))
				{
					traceLoggingTypeInfo = new SingleArrayTypeInfo();
				}
				else if (elementType == typeof(IntPtr))
				{
					traceLoggingTypeInfo = new IntPtrArrayTypeInfo();
				}
				else if (elementType == typeof(UIntPtr))
				{
					traceLoggingTypeInfo = new UIntPtrArrayTypeInfo();
				}
				else if (elementType == typeof(Guid))
				{
					traceLoggingTypeInfo = new GuidArrayTypeInfo();
				}
				else
				{
					traceLoggingTypeInfo = (TraceLoggingTypeInfo<DataType>)Statics.CreateInstance(typeof(ArrayTypeInfo<>).MakeGenericType(new Type[]
					{
						elementType
					}), new object[]
					{
						Statics.GetTypeInfoInstance(elementType, recursionCheck)
					});
				}
			}
			else if (Statics.IsEnum(typeFromHandle))
			{
				Type underlyingType = Enum.GetUnderlyingType(typeFromHandle);
				if (underlyingType == typeof(int))
				{
					traceLoggingTypeInfo = new EnumInt32TypeInfo<DataType>();
				}
				else if (underlyingType == typeof(uint))
				{
					traceLoggingTypeInfo = new EnumUInt32TypeInfo<DataType>();
				}
				else if (underlyingType == typeof(byte))
				{
					traceLoggingTypeInfo = new EnumByteTypeInfo<DataType>();
				}
				else if (underlyingType == typeof(sbyte))
				{
					traceLoggingTypeInfo = new EnumSByteTypeInfo<DataType>();
				}
				else if (underlyingType == typeof(short))
				{
					traceLoggingTypeInfo = new EnumInt16TypeInfo<DataType>();
				}
				else if (underlyingType == typeof(ushort))
				{
					traceLoggingTypeInfo = new EnumUInt16TypeInfo<DataType>();
				}
				else if (underlyingType == typeof(long))
				{
					traceLoggingTypeInfo = new EnumInt64TypeInfo<DataType>();
				}
				else
				{
					if (!(underlyingType == typeof(ulong)))
					{
						throw new NotSupportedException(Environment.GetResourceString("EventSource_NotSupportedEnumType", new object[]
						{
							typeFromHandle.Name,
							underlyingType.Name
						}));
					}
					traceLoggingTypeInfo = new EnumUInt64TypeInfo<DataType>();
				}
			}
			else if (typeFromHandle == typeof(string))
			{
				traceLoggingTypeInfo = new StringTypeInfo();
			}
			else if (typeFromHandle == typeof(bool))
			{
				traceLoggingTypeInfo = new BooleanTypeInfo();
			}
			else if (typeFromHandle == typeof(byte))
			{
				traceLoggingTypeInfo = new ByteTypeInfo();
			}
			else if (typeFromHandle == typeof(sbyte))
			{
				traceLoggingTypeInfo = new SByteTypeInfo();
			}
			else if (typeFromHandle == typeof(short))
			{
				traceLoggingTypeInfo = new Int16TypeInfo();
			}
			else if (typeFromHandle == typeof(ushort))
			{
				traceLoggingTypeInfo = new UInt16TypeInfo();
			}
			else if (typeFromHandle == typeof(int))
			{
				traceLoggingTypeInfo = new Int32TypeInfo();
			}
			else if (typeFromHandle == typeof(uint))
			{
				traceLoggingTypeInfo = new UInt32TypeInfo();
			}
			else if (typeFromHandle == typeof(long))
			{
				traceLoggingTypeInfo = new Int64TypeInfo();
			}
			else if (typeFromHandle == typeof(ulong))
			{
				traceLoggingTypeInfo = new UInt64TypeInfo();
			}
			else if (typeFromHandle == typeof(char))
			{
				traceLoggingTypeInfo = new CharTypeInfo();
			}
			else if (typeFromHandle == typeof(double))
			{
				traceLoggingTypeInfo = new DoubleTypeInfo();
			}
			else if (typeFromHandle == typeof(float))
			{
				traceLoggingTypeInfo = new SingleTypeInfo();
			}
			else if (typeFromHandle == typeof(DateTime))
			{
				traceLoggingTypeInfo = new DateTimeTypeInfo();
			}
			else if (typeFromHandle == typeof(decimal))
			{
				traceLoggingTypeInfo = new DecimalTypeInfo();
			}
			else if (typeFromHandle == typeof(IntPtr))
			{
				traceLoggingTypeInfo = new IntPtrTypeInfo();
			}
			else if (typeFromHandle == typeof(UIntPtr))
			{
				traceLoggingTypeInfo = new UIntPtrTypeInfo();
			}
			else if (typeFromHandle == typeof(Guid))
			{
				traceLoggingTypeInfo = new GuidTypeInfo();
			}
			else if (typeFromHandle == typeof(TimeSpan))
			{
				traceLoggingTypeInfo = new TimeSpanTypeInfo();
			}
			else if (typeFromHandle == typeof(DateTimeOffset))
			{
				traceLoggingTypeInfo = new DateTimeOffsetTypeInfo();
			}
			else if (typeFromHandle == typeof(EmptyStruct))
			{
				traceLoggingTypeInfo = new NullTypeInfo<EmptyStruct>();
			}
			else if (Statics.IsGenericMatch(typeFromHandle, typeof(KeyValuePair<, >)))
			{
				Type[] genericArguments = Statics.GetGenericArguments(typeFromHandle);
				traceLoggingTypeInfo = (TraceLoggingTypeInfo<DataType>)Statics.CreateInstance(typeof(KeyValuePairTypeInfo<, >).MakeGenericType(new Type[]
				{
					genericArguments[0],
					genericArguments[1]
				}), new object[]
				{
					recursionCheck
				});
			}
			else if (Statics.IsGenericMatch(typeFromHandle, typeof(Nullable<>)))
			{
				Type[] genericArguments2 = Statics.GetGenericArguments(typeFromHandle);
				traceLoggingTypeInfo = (TraceLoggingTypeInfo<DataType>)Statics.CreateInstance(typeof(NullableTypeInfo<>).MakeGenericType(new Type[]
				{
					genericArguments2[0]
				}), new object[]
				{
					recursionCheck
				});
			}
			else
			{
				Type type = Statics.FindEnumerableElementType(typeFromHandle);
				if (!(type != null))
				{
					throw new ArgumentException(Environment.GetResourceString("EventSource_NonCompliantTypeError", new object[]
					{
						typeFromHandle.Name
					}));
				}
				traceLoggingTypeInfo = (TraceLoggingTypeInfo<DataType>)Statics.CreateInstance(typeof(EnumerableTypeInfo<, >).MakeGenericType(new Type[]
				{
					typeFromHandle,
					type
				}), new object[]
				{
					Statics.GetTypeInfoInstance(type, recursionCheck)
				});
			}
			return (TraceLoggingTypeInfo<DataType>)traceLoggingTypeInfo;
		}

		// Token: 0x040017A4 RID: 6052
		public const byte DefaultLevel = 5;

		// Token: 0x040017A5 RID: 6053
		public const byte TraceLoggingChannel = 11;

		// Token: 0x040017A6 RID: 6054
		public const byte InTypeMask = 31;

		// Token: 0x040017A7 RID: 6055
		public const byte InTypeFixedCountFlag = 32;

		// Token: 0x040017A8 RID: 6056
		public const byte InTypeVariableCountFlag = 64;

		// Token: 0x040017A9 RID: 6057
		public const byte InTypeCustomCountFlag = 96;

		// Token: 0x040017AA RID: 6058
		public const byte InTypeCountMask = 96;

		// Token: 0x040017AB RID: 6059
		public const byte InTypeChainFlag = 128;

		// Token: 0x040017AC RID: 6060
		public const byte OutTypeMask = 127;

		// Token: 0x040017AD RID: 6061
		public const byte OutTypeChainFlag = 128;

		// Token: 0x040017AE RID: 6062
		public const EventTags EventTagsMask = (EventTags)268435455;

		// Token: 0x040017AF RID: 6063
		public static readonly TraceLoggingDataType IntPtrType = (IntPtr.Size == 8) ? TraceLoggingDataType.Int64 : TraceLoggingDataType.Int32;

		// Token: 0x040017B0 RID: 6064
		public static readonly TraceLoggingDataType UIntPtrType = (IntPtr.Size == 8) ? TraceLoggingDataType.UInt64 : TraceLoggingDataType.UInt32;

		// Token: 0x040017B1 RID: 6065
		public static readonly TraceLoggingDataType HexIntPtrType = (IntPtr.Size == 8) ? TraceLoggingDataType.HexInt64 : TraceLoggingDataType.HexInt32;
	}
}
