using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Security;
using System.Text;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200074E RID: 1870
	internal sealed class __BinaryWriter
	{
		// Token: 0x06005262 RID: 21090 RVA: 0x00121364 File Offset: 0x0011F564
		internal __BinaryWriter(Stream sout, ObjectWriter objectWriter, FormatterTypeStyle formatterTypeStyle)
		{
			this.sout = sout;
			this.formatterTypeStyle = formatterTypeStyle;
			this.objectWriter = objectWriter;
			this.m_nestedObjectCount = 0;
			this.dataWriter = new BinaryWriter(sout, Encoding.UTF8);
		}

		// Token: 0x06005263 RID: 21091 RVA: 0x001213A4 File Offset: 0x0011F5A4
		internal void WriteBegin()
		{
		}

		// Token: 0x06005264 RID: 21092 RVA: 0x001213A6 File Offset: 0x0011F5A6
		internal void WriteEnd()
		{
			this.dataWriter.Flush();
		}

		// Token: 0x06005265 RID: 21093 RVA: 0x001213B3 File Offset: 0x0011F5B3
		internal void WriteBoolean(bool value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x06005266 RID: 21094 RVA: 0x001213C1 File Offset: 0x0011F5C1
		internal void WriteByte(byte value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x06005267 RID: 21095 RVA: 0x001213CF File Offset: 0x0011F5CF
		private void WriteBytes(byte[] value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x06005268 RID: 21096 RVA: 0x001213DD File Offset: 0x0011F5DD
		private void WriteBytes(byte[] byteA, int offset, int size)
		{
			this.dataWriter.Write(byteA, offset, size);
		}

		// Token: 0x06005269 RID: 21097 RVA: 0x001213ED File Offset: 0x0011F5ED
		internal void WriteChar(char value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x0600526A RID: 21098 RVA: 0x001213FB File Offset: 0x0011F5FB
		internal void WriteChars(char[] value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x0600526B RID: 21099 RVA: 0x00121409 File Offset: 0x0011F609
		internal void WriteDecimal(decimal value)
		{
			this.WriteString(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x0600526C RID: 21100 RVA: 0x0012141D File Offset: 0x0011F61D
		internal void WriteSingle(float value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x0600526D RID: 21101 RVA: 0x0012142B File Offset: 0x0011F62B
		internal void WriteDouble(double value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x0600526E RID: 21102 RVA: 0x00121439 File Offset: 0x0011F639
		internal void WriteInt16(short value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x0600526F RID: 21103 RVA: 0x00121447 File Offset: 0x0011F647
		internal void WriteInt32(int value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x06005270 RID: 21104 RVA: 0x00121455 File Offset: 0x0011F655
		internal void WriteInt64(long value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x06005271 RID: 21105 RVA: 0x00121463 File Offset: 0x0011F663
		internal void WriteSByte(sbyte value)
		{
			this.WriteByte((byte)value);
		}

		// Token: 0x06005272 RID: 21106 RVA: 0x0012146D File Offset: 0x0011F66D
		internal void WriteString(string value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x06005273 RID: 21107 RVA: 0x0012147B File Offset: 0x0011F67B
		internal void WriteTimeSpan(TimeSpan value)
		{
			this.WriteInt64(value.Ticks);
		}

		// Token: 0x06005274 RID: 21108 RVA: 0x0012148A File Offset: 0x0011F68A
		internal void WriteDateTime(DateTime value)
		{
			this.WriteInt64(value.ToBinaryRaw());
		}

		// Token: 0x06005275 RID: 21109 RVA: 0x00121499 File Offset: 0x0011F699
		internal void WriteUInt16(ushort value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x06005276 RID: 21110 RVA: 0x001214A7 File Offset: 0x0011F6A7
		internal void WriteUInt32(uint value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x06005277 RID: 21111 RVA: 0x001214B5 File Offset: 0x0011F6B5
		internal void WriteUInt64(ulong value)
		{
			this.dataWriter.Write(value);
		}

		// Token: 0x06005278 RID: 21112 RVA: 0x001214C3 File Offset: 0x0011F6C3
		internal void WriteObjectEnd(NameInfo memberNameInfo, NameInfo typeNameInfo)
		{
		}

		// Token: 0x06005279 RID: 21113 RVA: 0x001214C8 File Offset: 0x0011F6C8
		internal void WriteSerializationHeaderEnd()
		{
			MessageEnd messageEnd = new MessageEnd();
			messageEnd.Dump(this.sout);
			messageEnd.Write(this);
		}

		// Token: 0x0600527A RID: 21114 RVA: 0x001214F0 File Offset: 0x0011F6F0
		internal void WriteSerializationHeader(int topId, int headerId, int minorVersion, int majorVersion)
		{
			SerializationHeaderRecord serializationHeaderRecord = new SerializationHeaderRecord(BinaryHeaderEnum.SerializedStreamHeader, topId, headerId, minorVersion, majorVersion);
			serializationHeaderRecord.Dump();
			serializationHeaderRecord.Write(this);
		}

		// Token: 0x0600527B RID: 21115 RVA: 0x00121516 File Offset: 0x0011F716
		internal void WriteMethodCall()
		{
			if (this.binaryMethodCall == null)
			{
				this.binaryMethodCall = new BinaryMethodCall();
			}
			this.binaryMethodCall.Dump();
			this.binaryMethodCall.Write(this);
		}

		// Token: 0x0600527C RID: 21116 RVA: 0x00121544 File Offset: 0x0011F744
		internal object[] WriteCallArray(string uri, string methodName, string typeName, Type[] instArgs, object[] args, object methodSignature, object callContext, object[] properties)
		{
			if (this.binaryMethodCall == null)
			{
				this.binaryMethodCall = new BinaryMethodCall();
			}
			return this.binaryMethodCall.WriteArray(uri, methodName, typeName, instArgs, args, methodSignature, callContext, properties);
		}

		// Token: 0x0600527D RID: 21117 RVA: 0x0012157C File Offset: 0x0011F77C
		internal void WriteMethodReturn()
		{
			if (this.binaryMethodReturn == null)
			{
				this.binaryMethodReturn = new BinaryMethodReturn();
			}
			this.binaryMethodReturn.Dump();
			this.binaryMethodReturn.Write(this);
		}

		// Token: 0x0600527E RID: 21118 RVA: 0x001215A8 File Offset: 0x0011F7A8
		internal object[] WriteReturnArray(object returnValue, object[] args, Exception exception, object callContext, object[] properties)
		{
			if (this.binaryMethodReturn == null)
			{
				this.binaryMethodReturn = new BinaryMethodReturn();
			}
			return this.binaryMethodReturn.WriteArray(returnValue, args, exception, callContext, properties);
		}

		// Token: 0x0600527F RID: 21119 RVA: 0x001215D0 File Offset: 0x0011F7D0
		internal void WriteObject(NameInfo nameInfo, NameInfo typeNameInfo, int numMembers, string[] memberNames, Type[] memberTypes, WriteObjectInfo[] memberObjectInfos)
		{
			this.InternalWriteItemNull();
			int num = (int)nameInfo.NIobjectId;
			string niname;
			if (num < 0)
			{
				niname = typeNameInfo.NIname;
			}
			else
			{
				niname = nameInfo.NIname;
			}
			if (this.objectMapTable == null)
			{
				this.objectMapTable = new Hashtable();
			}
			ObjectMapInfo objectMapInfo = (ObjectMapInfo)this.objectMapTable[niname];
			if (objectMapInfo != null && objectMapInfo.isCompatible(numMembers, memberNames, memberTypes))
			{
				if (this.binaryObject == null)
				{
					this.binaryObject = new BinaryObject();
				}
				this.binaryObject.Set(num, objectMapInfo.objectId);
				this.binaryObject.Write(this);
				return;
			}
			if (!typeNameInfo.NItransmitTypeOnObject)
			{
				if (this.binaryObjectWithMap == null)
				{
					this.binaryObjectWithMap = new BinaryObjectWithMap();
				}
				int num2 = (int)typeNameInfo.NIassemId;
				this.binaryObjectWithMap.Set(num, niname, numMembers, memberNames, num2);
				this.binaryObjectWithMap.Dump();
				this.binaryObjectWithMap.Write(this);
				if (objectMapInfo == null)
				{
					this.objectMapTable.Add(niname, new ObjectMapInfo(num, numMembers, memberNames, memberTypes));
					return;
				}
			}
			else
			{
				BinaryTypeEnum[] array = new BinaryTypeEnum[numMembers];
				object[] array2 = new object[numMembers];
				int[] array3 = new int[numMembers];
				int num2;
				for (int i = 0; i < numMembers; i++)
				{
					object obj = null;
					array[i] = BinaryConverter.GetBinaryTypeInfo(memberTypes[i], memberObjectInfos[i], null, this.objectWriter, out obj, out num2);
					array2[i] = obj;
					array3[i] = num2;
				}
				if (this.binaryObjectWithMapTyped == null)
				{
					this.binaryObjectWithMapTyped = new BinaryObjectWithMapTyped();
				}
				num2 = (int)typeNameInfo.NIassemId;
				this.binaryObjectWithMapTyped.Set(num, niname, numMembers, memberNames, array, array2, array3, num2);
				this.binaryObjectWithMapTyped.Write(this);
				if (objectMapInfo == null)
				{
					this.objectMapTable.Add(niname, new ObjectMapInfo(num, numMembers, memberNames, memberTypes));
				}
			}
		}

		// Token: 0x06005280 RID: 21120 RVA: 0x00121784 File Offset: 0x0011F984
		internal void WriteObjectString(int objectId, string value)
		{
			this.InternalWriteItemNull();
			if (this.binaryObjectString == null)
			{
				this.binaryObjectString = new BinaryObjectString();
			}
			this.binaryObjectString.Set(objectId, value);
			this.binaryObjectString.Write(this);
		}

		// Token: 0x06005281 RID: 21121 RVA: 0x001217B8 File Offset: 0x0011F9B8
		[SecurityCritical]
		internal void WriteSingleArray(NameInfo memberNameInfo, NameInfo arrayNameInfo, WriteObjectInfo objectInfo, NameInfo arrayElemTypeNameInfo, int length, int lowerBound, Array array)
		{
			this.InternalWriteItemNull();
			int[] lengthA = new int[]
			{
				length
			};
			int[] lowerBoundA = null;
			object typeInformation = null;
			BinaryArrayTypeEnum binaryArrayTypeEnum;
			if (lowerBound == 0)
			{
				binaryArrayTypeEnum = BinaryArrayTypeEnum.Single;
			}
			else
			{
				binaryArrayTypeEnum = BinaryArrayTypeEnum.SingleOffset;
				lowerBoundA = new int[]
				{
					lowerBound
				};
			}
			int assemId;
			BinaryTypeEnum binaryTypeInfo = BinaryConverter.GetBinaryTypeInfo(arrayElemTypeNameInfo.NItype, objectInfo, arrayElemTypeNameInfo.NIname, this.objectWriter, out typeInformation, out assemId);
			if (this.binaryArray == null)
			{
				this.binaryArray = new BinaryArray();
			}
			this.binaryArray.Set((int)arrayNameInfo.NIobjectId, 1, lengthA, lowerBoundA, binaryTypeInfo, typeInformation, binaryArrayTypeEnum, assemId);
			long niobjectId = arrayNameInfo.NIobjectId;
			this.binaryArray.Write(this);
			if (Converter.IsWriteAsByteArray(arrayElemTypeNameInfo.NIprimitiveTypeEnum) && lowerBound == 0)
			{
				if (arrayElemTypeNameInfo.NIprimitiveTypeEnum == InternalPrimitiveTypeE.Byte)
				{
					this.WriteBytes((byte[])array);
					return;
				}
				if (arrayElemTypeNameInfo.NIprimitiveTypeEnum == InternalPrimitiveTypeE.Char)
				{
					this.WriteChars((char[])array);
					return;
				}
				this.WriteArrayAsBytes(array, Converter.TypeLength(arrayElemTypeNameInfo.NIprimitiveTypeEnum));
			}
		}

		// Token: 0x06005282 RID: 21122 RVA: 0x001218AC File Offset: 0x0011FAAC
		[SecurityCritical]
		private void WriteArrayAsBytes(Array array, int typeLength)
		{
			this.InternalWriteItemNull();
			int num = array.Length * typeLength;
			int i = 0;
			if (this.byteBuffer == null)
			{
				this.byteBuffer = new byte[this.chunkSize];
			}
			while (i < array.Length)
			{
				int num2 = Math.Min(this.chunkSize / typeLength, array.Length - i);
				int num3 = num2 * typeLength;
				Buffer.InternalBlockCopy(array, i * typeLength, this.byteBuffer, 0, num3);
				this.WriteBytes(this.byteBuffer, 0, num3);
				i += num2;
			}
		}

		// Token: 0x06005283 RID: 21123 RVA: 0x0012192C File Offset: 0x0011FB2C
		internal void WriteJaggedArray(NameInfo memberNameInfo, NameInfo arrayNameInfo, WriteObjectInfo objectInfo, NameInfo arrayElemTypeNameInfo, int length, int lowerBound)
		{
			this.InternalWriteItemNull();
			int[] lengthA = new int[]
			{
				length
			};
			int[] lowerBoundA = null;
			object typeInformation = null;
			int assemId = 0;
			BinaryArrayTypeEnum binaryArrayTypeEnum;
			if (lowerBound == 0)
			{
				binaryArrayTypeEnum = BinaryArrayTypeEnum.Jagged;
			}
			else
			{
				binaryArrayTypeEnum = BinaryArrayTypeEnum.JaggedOffset;
				lowerBoundA = new int[]
				{
					lowerBound
				};
			}
			BinaryTypeEnum binaryTypeInfo = BinaryConverter.GetBinaryTypeInfo(arrayElemTypeNameInfo.NItype, objectInfo, arrayElemTypeNameInfo.NIname, this.objectWriter, out typeInformation, out assemId);
			if (this.binaryArray == null)
			{
				this.binaryArray = new BinaryArray();
			}
			this.binaryArray.Set((int)arrayNameInfo.NIobjectId, 1, lengthA, lowerBoundA, binaryTypeInfo, typeInformation, binaryArrayTypeEnum, assemId);
			long niobjectId = arrayNameInfo.NIobjectId;
			this.binaryArray.Write(this);
		}

		// Token: 0x06005284 RID: 21124 RVA: 0x001219CC File Offset: 0x0011FBCC
		internal void WriteRectangleArray(NameInfo memberNameInfo, NameInfo arrayNameInfo, WriteObjectInfo objectInfo, NameInfo arrayElemTypeNameInfo, int rank, int[] lengthA, int[] lowerBoundA)
		{
			this.InternalWriteItemNull();
			BinaryArrayTypeEnum binaryArrayTypeEnum = BinaryArrayTypeEnum.Rectangular;
			object typeInformation = null;
			int assemId = 0;
			BinaryTypeEnum binaryTypeInfo = BinaryConverter.GetBinaryTypeInfo(arrayElemTypeNameInfo.NItype, objectInfo, arrayElemTypeNameInfo.NIname, this.objectWriter, out typeInformation, out assemId);
			if (this.binaryArray == null)
			{
				this.binaryArray = new BinaryArray();
			}
			for (int i = 0; i < rank; i++)
			{
				if (lowerBoundA[i] != 0)
				{
					binaryArrayTypeEnum = BinaryArrayTypeEnum.RectangularOffset;
					break;
				}
			}
			this.binaryArray.Set((int)arrayNameInfo.NIobjectId, rank, lengthA, lowerBoundA, binaryTypeInfo, typeInformation, binaryArrayTypeEnum, assemId);
			long niobjectId = arrayNameInfo.NIobjectId;
			this.binaryArray.Write(this);
		}

		// Token: 0x06005285 RID: 21125 RVA: 0x00121A65 File Offset: 0x0011FC65
		[SecurityCritical]
		internal void WriteObjectByteArray(NameInfo memberNameInfo, NameInfo arrayNameInfo, WriteObjectInfo objectInfo, NameInfo arrayElemTypeNameInfo, int length, int lowerBound, byte[] byteA)
		{
			this.InternalWriteItemNull();
			this.WriteSingleArray(memberNameInfo, arrayNameInfo, objectInfo, arrayElemTypeNameInfo, length, lowerBound, byteA);
		}

		// Token: 0x06005286 RID: 21126 RVA: 0x00121A80 File Offset: 0x0011FC80
		internal void WriteMember(NameInfo memberNameInfo, NameInfo typeNameInfo, object value)
		{
			this.InternalWriteItemNull();
			InternalPrimitiveTypeE niprimitiveTypeEnum = typeNameInfo.NIprimitiveTypeEnum;
			if (memberNameInfo.NItransmitTypeOnMember)
			{
				if (this.memberPrimitiveTyped == null)
				{
					this.memberPrimitiveTyped = new MemberPrimitiveTyped();
				}
				this.memberPrimitiveTyped.Set(niprimitiveTypeEnum, value);
				bool niisArrayItem = memberNameInfo.NIisArrayItem;
				this.memberPrimitiveTyped.Dump();
				this.memberPrimitiveTyped.Write(this);
				return;
			}
			if (this.memberPrimitiveUnTyped == null)
			{
				this.memberPrimitiveUnTyped = new MemberPrimitiveUnTyped();
			}
			this.memberPrimitiveUnTyped.Set(niprimitiveTypeEnum, value);
			bool niisArrayItem2 = memberNameInfo.NIisArrayItem;
			this.memberPrimitiveUnTyped.Dump();
			this.memberPrimitiveUnTyped.Write(this);
		}

		// Token: 0x06005287 RID: 21127 RVA: 0x00121B20 File Offset: 0x0011FD20
		internal void WriteNullMember(NameInfo memberNameInfo, NameInfo typeNameInfo)
		{
			this.InternalWriteItemNull();
			if (this.objectNull == null)
			{
				this.objectNull = new ObjectNull();
			}
			if (!memberNameInfo.NIisArrayItem)
			{
				this.objectNull.SetNullCount(1);
				this.objectNull.Dump();
				this.objectNull.Write(this);
				this.nullCount = 0;
			}
		}

		// Token: 0x06005288 RID: 21128 RVA: 0x00121B78 File Offset: 0x0011FD78
		internal void WriteMemberObjectRef(NameInfo memberNameInfo, int idRef)
		{
			this.InternalWriteItemNull();
			if (this.memberReference == null)
			{
				this.memberReference = new MemberReference();
			}
			this.memberReference.Set(idRef);
			bool niisArrayItem = memberNameInfo.NIisArrayItem;
			this.memberReference.Dump();
			this.memberReference.Write(this);
		}

		// Token: 0x06005289 RID: 21129 RVA: 0x00121BC8 File Offset: 0x0011FDC8
		internal void WriteMemberNested(NameInfo memberNameInfo)
		{
			this.InternalWriteItemNull();
			bool niisArrayItem = memberNameInfo.NIisArrayItem;
		}

		// Token: 0x0600528A RID: 21130 RVA: 0x00121BD7 File Offset: 0x0011FDD7
		internal void WriteMemberString(NameInfo memberNameInfo, NameInfo typeNameInfo, string value)
		{
			this.InternalWriteItemNull();
			bool niisArrayItem = memberNameInfo.NIisArrayItem;
			this.WriteObjectString((int)typeNameInfo.NIobjectId, value);
		}

		// Token: 0x0600528B RID: 21131 RVA: 0x00121BF4 File Offset: 0x0011FDF4
		internal void WriteItem(NameInfo itemNameInfo, NameInfo typeNameInfo, object value)
		{
			this.InternalWriteItemNull();
			this.WriteMember(itemNameInfo, typeNameInfo, value);
		}

		// Token: 0x0600528C RID: 21132 RVA: 0x00121C05 File Offset: 0x0011FE05
		internal void WriteNullItem(NameInfo itemNameInfo, NameInfo typeNameInfo)
		{
			this.nullCount++;
			this.InternalWriteItemNull();
		}

		// Token: 0x0600528D RID: 21133 RVA: 0x00121C1B File Offset: 0x0011FE1B
		internal void WriteDelayedNullItem()
		{
			this.nullCount++;
		}

		// Token: 0x0600528E RID: 21134 RVA: 0x00121C2B File Offset: 0x0011FE2B
		internal void WriteItemEnd()
		{
			this.InternalWriteItemNull();
		}

		// Token: 0x0600528F RID: 21135 RVA: 0x00121C34 File Offset: 0x0011FE34
		private void InternalWriteItemNull()
		{
			if (this.nullCount > 0)
			{
				if (this.objectNull == null)
				{
					this.objectNull = new ObjectNull();
				}
				this.objectNull.SetNullCount(this.nullCount);
				this.objectNull.Dump();
				this.objectNull.Write(this);
				this.nullCount = 0;
			}
		}

		// Token: 0x06005290 RID: 21136 RVA: 0x00121C8C File Offset: 0x0011FE8C
		internal void WriteItemObjectRef(NameInfo nameInfo, int idRef)
		{
			this.InternalWriteItemNull();
			this.WriteMemberObjectRef(nameInfo, idRef);
		}

		// Token: 0x06005291 RID: 21137 RVA: 0x00121C9C File Offset: 0x0011FE9C
		internal void WriteAssembly(Type type, string assemblyString, int assemId, bool isNew)
		{
			this.InternalWriteItemNull();
			if (assemblyString == null)
			{
				assemblyString = string.Empty;
			}
			if (isNew)
			{
				if (this.binaryAssembly == null)
				{
					this.binaryAssembly = new BinaryAssembly();
				}
				this.binaryAssembly.Set(assemId, assemblyString);
				this.binaryAssembly.Dump();
				this.binaryAssembly.Write(this);
			}
		}

		// Token: 0x06005292 RID: 21138 RVA: 0x00121CF4 File Offset: 0x0011FEF4
		internal void WriteValue(InternalPrimitiveTypeE code, object value)
		{
			switch (code)
			{
			case InternalPrimitiveTypeE.Boolean:
				this.WriteBoolean(Convert.ToBoolean(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.Byte:
				this.WriteByte(Convert.ToByte(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.Char:
				this.WriteChar(Convert.ToChar(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.Decimal:
				this.WriteDecimal(Convert.ToDecimal(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.Double:
				this.WriteDouble(Convert.ToDouble(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.Int16:
				this.WriteInt16(Convert.ToInt16(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.Int32:
				this.WriteInt32(Convert.ToInt32(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.Int64:
				this.WriteInt64(Convert.ToInt64(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.SByte:
				this.WriteSByte(Convert.ToSByte(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.Single:
				this.WriteSingle(Convert.ToSingle(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.TimeSpan:
				this.WriteTimeSpan((TimeSpan)value);
				return;
			case InternalPrimitiveTypeE.DateTime:
				this.WriteDateTime((DateTime)value);
				return;
			case InternalPrimitiveTypeE.UInt16:
				this.WriteUInt16(Convert.ToUInt16(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.UInt32:
				this.WriteUInt32(Convert.ToUInt32(value, CultureInfo.InvariantCulture));
				return;
			case InternalPrimitiveTypeE.UInt64:
				this.WriteUInt64(Convert.ToUInt64(value, CultureInfo.InvariantCulture));
				return;
			}
			throw new SerializationException(Environment.GetResourceString("Serialization_TypeCode", new object[]
			{
				code.ToString()
			}));
		}

		// Token: 0x040024E2 RID: 9442
		internal Stream sout;

		// Token: 0x040024E3 RID: 9443
		internal FormatterTypeStyle formatterTypeStyle;

		// Token: 0x040024E4 RID: 9444
		internal Hashtable objectMapTable;

		// Token: 0x040024E5 RID: 9445
		internal ObjectWriter objectWriter;

		// Token: 0x040024E6 RID: 9446
		internal BinaryWriter dataWriter;

		// Token: 0x040024E7 RID: 9447
		internal int m_nestedObjectCount;

		// Token: 0x040024E8 RID: 9448
		private int nullCount;

		// Token: 0x040024E9 RID: 9449
		internal BinaryMethodCall binaryMethodCall;

		// Token: 0x040024EA RID: 9450
		internal BinaryMethodReturn binaryMethodReturn;

		// Token: 0x040024EB RID: 9451
		internal BinaryObject binaryObject;

		// Token: 0x040024EC RID: 9452
		internal BinaryObjectWithMap binaryObjectWithMap;

		// Token: 0x040024ED RID: 9453
		internal BinaryObjectWithMapTyped binaryObjectWithMapTyped;

		// Token: 0x040024EE RID: 9454
		internal BinaryObjectString binaryObjectString;

		// Token: 0x040024EF RID: 9455
		internal BinaryCrossAppDomainString binaryCrossAppDomainString;

		// Token: 0x040024F0 RID: 9456
		internal BinaryArray binaryArray;

		// Token: 0x040024F1 RID: 9457
		private byte[] byteBuffer;

		// Token: 0x040024F2 RID: 9458
		private int chunkSize = 4096;

		// Token: 0x040024F3 RID: 9459
		internal MemberPrimitiveUnTyped memberPrimitiveUnTyped;

		// Token: 0x040024F4 RID: 9460
		internal MemberPrimitiveTyped memberPrimitiveTyped;

		// Token: 0x040024F5 RID: 9461
		internal ObjectNull objectNull;

		// Token: 0x040024F6 RID: 9462
		internal MemberReference memberReference;

		// Token: 0x040024F7 RID: 9463
		internal BinaryAssembly binaryAssembly;

		// Token: 0x040024F8 RID: 9464
		internal BinaryCrossAppDomainAssembly crossAppDomainAssembly;
	}
}
