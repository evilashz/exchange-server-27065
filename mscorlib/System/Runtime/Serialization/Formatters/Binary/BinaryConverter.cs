using System;
using System.Reflection;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000750 RID: 1872
	internal static class BinaryConverter
	{
		// Token: 0x06005295 RID: 21141 RVA: 0x00121EF4 File Offset: 0x001200F4
		internal static BinaryTypeEnum GetBinaryTypeInfo(Type type, WriteObjectInfo objectInfo, string typeName, ObjectWriter objectWriter, out object typeInformation, out int assemId)
		{
			assemId = 0;
			typeInformation = null;
			BinaryTypeEnum result;
			if (type == Converter.typeofString)
			{
				result = BinaryTypeEnum.String;
			}
			else if ((objectInfo == null || (objectInfo != null && !objectInfo.isSi)) && type == Converter.typeofObject)
			{
				result = BinaryTypeEnum.Object;
			}
			else if (type == Converter.typeofStringArray)
			{
				result = BinaryTypeEnum.StringArray;
			}
			else if (type == Converter.typeofObjectArray)
			{
				result = BinaryTypeEnum.ObjectArray;
			}
			else if (Converter.IsPrimitiveArray(type, out typeInformation))
			{
				result = BinaryTypeEnum.PrimitiveArray;
			}
			else
			{
				InternalPrimitiveTypeE internalPrimitiveTypeE = objectWriter.ToCode(type);
				if (internalPrimitiveTypeE == InternalPrimitiveTypeE.Invalid)
				{
					string text;
					if (objectInfo == null)
					{
						text = type.Assembly.FullName;
						typeInformation = type.FullName;
					}
					else
					{
						text = objectInfo.GetAssemblyString();
						typeInformation = objectInfo.GetTypeFullName();
					}
					if (text.Equals(Converter.urtAssemblyString))
					{
						result = BinaryTypeEnum.ObjectUrt;
						assemId = 0;
					}
					else
					{
						result = BinaryTypeEnum.ObjectUser;
						assemId = (int)objectInfo.assemId;
						if (assemId == 0)
						{
							throw new SerializationException(Environment.GetResourceString("Serialization_AssemblyId", new object[]
							{
								typeInformation
							}));
						}
					}
				}
				else
				{
					result = BinaryTypeEnum.Primitive;
					typeInformation = internalPrimitiveTypeE;
				}
			}
			return result;
		}

		// Token: 0x06005296 RID: 21142 RVA: 0x00121FEC File Offset: 0x001201EC
		internal static BinaryTypeEnum GetParserBinaryTypeInfo(Type type, out object typeInformation)
		{
			typeInformation = null;
			BinaryTypeEnum result;
			if (type == Converter.typeofString)
			{
				result = BinaryTypeEnum.String;
			}
			else if (type == Converter.typeofObject)
			{
				result = BinaryTypeEnum.Object;
			}
			else if (type == Converter.typeofObjectArray)
			{
				result = BinaryTypeEnum.ObjectArray;
			}
			else if (type == Converter.typeofStringArray)
			{
				result = BinaryTypeEnum.StringArray;
			}
			else if (Converter.IsPrimitiveArray(type, out typeInformation))
			{
				result = BinaryTypeEnum.PrimitiveArray;
			}
			else
			{
				InternalPrimitiveTypeE internalPrimitiveTypeE = Converter.ToCode(type);
				if (internalPrimitiveTypeE == InternalPrimitiveTypeE.Invalid)
				{
					if (Assembly.GetAssembly(type) == Converter.urtAssembly)
					{
						result = BinaryTypeEnum.ObjectUrt;
					}
					else
					{
						result = BinaryTypeEnum.ObjectUser;
					}
					typeInformation = type.FullName;
				}
				else
				{
					result = BinaryTypeEnum.Primitive;
					typeInformation = internalPrimitiveTypeE;
				}
			}
			return result;
		}

		// Token: 0x06005297 RID: 21143 RVA: 0x00122070 File Offset: 0x00120270
		internal static void WriteTypeInfo(BinaryTypeEnum binaryTypeEnum, object typeInformation, int assemId, __BinaryWriter sout)
		{
			switch (binaryTypeEnum)
			{
			case BinaryTypeEnum.Primitive:
			case BinaryTypeEnum.PrimitiveArray:
				sout.WriteByte((byte)((InternalPrimitiveTypeE)typeInformation));
				return;
			case BinaryTypeEnum.String:
			case BinaryTypeEnum.Object:
			case BinaryTypeEnum.ObjectArray:
			case BinaryTypeEnum.StringArray:
				return;
			case BinaryTypeEnum.ObjectUrt:
				sout.WriteString(typeInformation.ToString());
				return;
			case BinaryTypeEnum.ObjectUser:
				sout.WriteString(typeInformation.ToString());
				sout.WriteInt32(assemId);
				return;
			default:
				throw new SerializationException(Environment.GetResourceString("Serialization_TypeWrite", new object[]
				{
					binaryTypeEnum.ToString()
				}));
			}
		}

		// Token: 0x06005298 RID: 21144 RVA: 0x001220F8 File Offset: 0x001202F8
		internal static object ReadTypeInfo(BinaryTypeEnum binaryTypeEnum, __BinaryParser input, out int assemId)
		{
			object result = null;
			int num = 0;
			switch (binaryTypeEnum)
			{
			case BinaryTypeEnum.Primitive:
			case BinaryTypeEnum.PrimitiveArray:
				result = (InternalPrimitiveTypeE)input.ReadByte();
				break;
			case BinaryTypeEnum.String:
			case BinaryTypeEnum.Object:
			case BinaryTypeEnum.ObjectArray:
			case BinaryTypeEnum.StringArray:
				break;
			case BinaryTypeEnum.ObjectUrt:
				result = input.ReadString();
				break;
			case BinaryTypeEnum.ObjectUser:
				result = input.ReadString();
				num = input.ReadInt32();
				break;
			default:
				throw new SerializationException(Environment.GetResourceString("Serialization_TypeRead", new object[]
				{
					binaryTypeEnum.ToString()
				}));
			}
			assemId = num;
			return result;
		}

		// Token: 0x06005299 RID: 21145 RVA: 0x00122180 File Offset: 0x00120380
		[SecurityCritical]
		internal static void TypeFromInfo(BinaryTypeEnum binaryTypeEnum, object typeInformation, ObjectReader objectReader, BinaryAssemblyInfo assemblyInfo, out InternalPrimitiveTypeE primitiveTypeEnum, out string typeString, out Type type, out bool isVariant)
		{
			isVariant = false;
			primitiveTypeEnum = InternalPrimitiveTypeE.Invalid;
			typeString = null;
			type = null;
			switch (binaryTypeEnum)
			{
			case BinaryTypeEnum.Primitive:
				primitiveTypeEnum = (InternalPrimitiveTypeE)typeInformation;
				typeString = Converter.ToComType(primitiveTypeEnum);
				type = Converter.ToType(primitiveTypeEnum);
				return;
			case BinaryTypeEnum.String:
				type = Converter.typeofString;
				return;
			case BinaryTypeEnum.Object:
				type = Converter.typeofObject;
				isVariant = true;
				return;
			case BinaryTypeEnum.ObjectUrt:
			case BinaryTypeEnum.ObjectUser:
				if (typeInformation != null)
				{
					typeString = typeInformation.ToString();
					type = objectReader.GetType(assemblyInfo, typeString);
					if (type == Converter.typeofObject)
					{
						isVariant = true;
						return;
					}
				}
				return;
			case BinaryTypeEnum.ObjectArray:
				type = Converter.typeofObjectArray;
				return;
			case BinaryTypeEnum.StringArray:
				type = Converter.typeofStringArray;
				return;
			case BinaryTypeEnum.PrimitiveArray:
				primitiveTypeEnum = (InternalPrimitiveTypeE)typeInformation;
				type = Converter.ToArrayType(primitiveTypeEnum);
				return;
			default:
				throw new SerializationException(Environment.GetResourceString("Serialization_TypeRead", new object[]
				{
					binaryTypeEnum.ToString()
				}));
			}
		}
	}
}
