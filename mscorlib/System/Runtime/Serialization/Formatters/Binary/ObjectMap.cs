using System;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000766 RID: 1894
	internal sealed class ObjectMap
	{
		// Token: 0x0600530E RID: 21262 RVA: 0x00123D2C File Offset: 0x00121F2C
		[SecurityCritical]
		internal ObjectMap(string objectName, Type objectType, string[] memberNames, ObjectReader objectReader, int objectId, BinaryAssemblyInfo assemblyInfo)
		{
			this.objectName = objectName;
			this.objectType = objectType;
			this.memberNames = memberNames;
			this.objectReader = objectReader;
			this.objectId = objectId;
			this.assemblyInfo = assemblyInfo;
			this.objectInfo = objectReader.CreateReadObjectInfo(objectType);
			this.memberTypes = this.objectInfo.GetMemberTypes(memberNames, objectType);
			this.binaryTypeEnumA = new BinaryTypeEnum[this.memberTypes.Length];
			this.typeInformationA = new object[this.memberTypes.Length];
			for (int i = 0; i < this.memberTypes.Length; i++)
			{
				object obj = null;
				BinaryTypeEnum parserBinaryTypeInfo = BinaryConverter.GetParserBinaryTypeInfo(this.memberTypes[i], out obj);
				this.binaryTypeEnumA[i] = parserBinaryTypeInfo;
				this.typeInformationA[i] = obj;
			}
		}

		// Token: 0x0600530F RID: 21263 RVA: 0x00123DF4 File Offset: 0x00121FF4
		[SecurityCritical]
		internal ObjectMap(string objectName, string[] memberNames, BinaryTypeEnum[] binaryTypeEnumA, object[] typeInformationA, int[] memberAssemIds, ObjectReader objectReader, int objectId, BinaryAssemblyInfo assemblyInfo, SizedArray assemIdToAssemblyTable)
		{
			this.objectName = objectName;
			this.memberNames = memberNames;
			this.binaryTypeEnumA = binaryTypeEnumA;
			this.typeInformationA = typeInformationA;
			this.objectReader = objectReader;
			this.objectId = objectId;
			this.assemblyInfo = assemblyInfo;
			if (assemblyInfo == null)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_Assembly", new object[]
				{
					objectName
				}));
			}
			this.objectType = objectReader.GetType(assemblyInfo, objectName);
			this.memberTypes = new Type[memberNames.Length];
			for (int i = 0; i < memberNames.Length; i++)
			{
				InternalPrimitiveTypeE internalPrimitiveTypeE;
				string text;
				Type type;
				bool flag;
				BinaryConverter.TypeFromInfo(binaryTypeEnumA[i], typeInformationA[i], objectReader, (BinaryAssemblyInfo)assemIdToAssemblyTable[memberAssemIds[i]], out internalPrimitiveTypeE, out text, out type, out flag);
				this.memberTypes[i] = type;
			}
			this.objectInfo = objectReader.CreateReadObjectInfo(this.objectType, memberNames, null);
			if (!this.objectInfo.isSi)
			{
				this.objectInfo.GetMemberTypes(memberNames, this.objectInfo.objectType);
			}
		}

		// Token: 0x06005310 RID: 21264 RVA: 0x00123EF8 File Offset: 0x001220F8
		internal ReadObjectInfo CreateObjectInfo(ref SerializationInfo si, ref object[] memberData)
		{
			if (this.isInitObjectInfo)
			{
				this.isInitObjectInfo = false;
				this.objectInfo.InitDataStore(ref si, ref memberData);
				return this.objectInfo;
			}
			this.objectInfo.PrepareForReuse();
			this.objectInfo.InitDataStore(ref si, ref memberData);
			return this.objectInfo;
		}

		// Token: 0x06005311 RID: 21265 RVA: 0x00123F46 File Offset: 0x00122146
		[SecurityCritical]
		internal static ObjectMap Create(string name, Type objectType, string[] memberNames, ObjectReader objectReader, int objectId, BinaryAssemblyInfo assemblyInfo)
		{
			return new ObjectMap(name, objectType, memberNames, objectReader, objectId, assemblyInfo);
		}

		// Token: 0x06005312 RID: 21266 RVA: 0x00123F58 File Offset: 0x00122158
		[SecurityCritical]
		internal static ObjectMap Create(string name, string[] memberNames, BinaryTypeEnum[] binaryTypeEnumA, object[] typeInformationA, int[] memberAssemIds, ObjectReader objectReader, int objectId, BinaryAssemblyInfo assemblyInfo, SizedArray assemIdToAssemblyTable)
		{
			return new ObjectMap(name, memberNames, binaryTypeEnumA, typeInformationA, memberAssemIds, objectReader, objectId, assemblyInfo, assemIdToAssemblyTable);
		}

		// Token: 0x04002548 RID: 9544
		internal string objectName;

		// Token: 0x04002549 RID: 9545
		internal Type objectType;

		// Token: 0x0400254A RID: 9546
		internal BinaryTypeEnum[] binaryTypeEnumA;

		// Token: 0x0400254B RID: 9547
		internal object[] typeInformationA;

		// Token: 0x0400254C RID: 9548
		internal Type[] memberTypes;

		// Token: 0x0400254D RID: 9549
		internal string[] memberNames;

		// Token: 0x0400254E RID: 9550
		internal ReadObjectInfo objectInfo;

		// Token: 0x0400254F RID: 9551
		internal bool isInitObjectInfo = true;

		// Token: 0x04002550 RID: 9552
		internal ObjectReader objectReader;

		// Token: 0x04002551 RID: 9553
		internal int objectId;

		// Token: 0x04002552 RID: 9554
		internal BinaryAssemblyInfo assemblyInfo;
	}
}
