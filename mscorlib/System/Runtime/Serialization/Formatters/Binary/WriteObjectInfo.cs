using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Remoting;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000774 RID: 1908
	internal sealed class WriteObjectInfo
	{
		// Token: 0x060053A0 RID: 21408 RVA: 0x001284F7 File Offset: 0x001266F7
		internal WriteObjectInfo()
		{
		}

		// Token: 0x060053A1 RID: 21409 RVA: 0x001284FF File Offset: 0x001266FF
		internal void ObjectEnd()
		{
			WriteObjectInfo.PutObjectInfo(this.serObjectInfoInit, this);
		}

		// Token: 0x060053A2 RID: 21410 RVA: 0x00128510 File Offset: 0x00126710
		private void InternalInit()
		{
			this.obj = null;
			this.objectType = null;
			this.isSi = false;
			this.isNamed = false;
			this.isTyped = false;
			this.isArray = false;
			this.si = null;
			this.cache = null;
			this.memberData = null;
			this.objectId = 0L;
			this.assemId = 0L;
			this.binderTypeName = null;
			this.binderAssemblyString = null;
		}

		// Token: 0x060053A3 RID: 21411 RVA: 0x0012857C File Offset: 0x0012677C
		[SecurityCritical]
		internal static WriteObjectInfo Serialize(object obj, ISurrogateSelector surrogateSelector, StreamingContext context, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, ObjectWriter objectWriter, SerializationBinder binder)
		{
			WriteObjectInfo objectInfo = WriteObjectInfo.GetObjectInfo(serObjectInfoInit);
			objectInfo.InitSerialize(obj, surrogateSelector, context, serObjectInfoInit, converter, objectWriter, binder);
			return objectInfo;
		}

		// Token: 0x060053A4 RID: 21412 RVA: 0x001285A4 File Offset: 0x001267A4
		[SecurityCritical]
		internal void InitSerialize(object obj, ISurrogateSelector surrogateSelector, StreamingContext context, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, ObjectWriter objectWriter, SerializationBinder binder)
		{
			this.context = context;
			this.obj = obj;
			this.serObjectInfoInit = serObjectInfoInit;
			if (RemotingServices.IsTransparentProxy(obj))
			{
				this.objectType = Converter.typeofMarshalByRefObject;
			}
			else
			{
				this.objectType = obj.GetType();
			}
			if (this.objectType.IsArray)
			{
				this.isArray = true;
				this.InitNoMembers();
				return;
			}
			this.InvokeSerializationBinder(binder);
			objectWriter.ObjectManager.RegisterObject(obj);
			ISurrogateSelector surrogateSelector2;
			if (surrogateSelector != null && (this.serializationSurrogate = surrogateSelector.GetSurrogate(this.objectType, context, out surrogateSelector2)) != null)
			{
				this.si = new SerializationInfo(this.objectType, converter);
				if (!this.objectType.IsPrimitive)
				{
					this.serializationSurrogate.GetObjectData(obj, this.si, context);
				}
				this.InitSiWrite();
				return;
			}
			if (!(obj is ISerializable))
			{
				this.InitMemberInfo();
				WriteObjectInfo.CheckTypeForwardedFrom(this.cache, this.objectType, this.binderAssemblyString);
				return;
			}
			if (!this.objectType.IsSerializable)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_NonSerType", new object[]
				{
					this.objectType.FullName,
					this.objectType.Assembly.FullName
				}));
			}
			this.si = new SerializationInfo(this.objectType, converter, !FormatterServices.UnsafeTypeForwardersIsEnabled());
			((ISerializable)obj).GetObjectData(this.si, context);
			this.InitSiWrite();
			WriteObjectInfo.CheckTypeForwardedFrom(this.cache, this.objectType, this.binderAssemblyString);
		}

		// Token: 0x060053A5 RID: 21413 RVA: 0x00128728 File Offset: 0x00126928
		[Conditional("SER_LOGGING")]
		private void DumpMemberInfo()
		{
			for (int i = 0; i < this.cache.memberInfos.Length; i++)
			{
			}
		}

		// Token: 0x060053A6 RID: 21414 RVA: 0x00128750 File Offset: 0x00126950
		[SecurityCritical]
		internal static WriteObjectInfo Serialize(Type objectType, ISurrogateSelector surrogateSelector, StreamingContext context, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, SerializationBinder binder)
		{
			WriteObjectInfo objectInfo = WriteObjectInfo.GetObjectInfo(serObjectInfoInit);
			objectInfo.InitSerialize(objectType, surrogateSelector, context, serObjectInfoInit, converter, binder);
			return objectInfo;
		}

		// Token: 0x060053A7 RID: 21415 RVA: 0x00128774 File Offset: 0x00126974
		[SecurityCritical]
		internal void InitSerialize(Type objectType, ISurrogateSelector surrogateSelector, StreamingContext context, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, SerializationBinder binder)
		{
			this.objectType = objectType;
			this.context = context;
			this.serObjectInfoInit = serObjectInfoInit;
			if (objectType.IsArray)
			{
				this.InitNoMembers();
				return;
			}
			this.InvokeSerializationBinder(binder);
			ISurrogateSelector surrogateSelector2 = null;
			if (surrogateSelector != null)
			{
				this.serializationSurrogate = surrogateSelector.GetSurrogate(objectType, context, out surrogateSelector2);
			}
			if (this.serializationSurrogate != null)
			{
				this.si = new SerializationInfo(objectType, converter);
				this.cache = new SerObjectInfoCache(objectType);
				this.isSi = true;
			}
			else if (objectType != Converter.typeofObject && Converter.typeofISerializable.IsAssignableFrom(objectType))
			{
				this.si = new SerializationInfo(objectType, converter, !FormatterServices.UnsafeTypeForwardersIsEnabled());
				this.cache = new SerObjectInfoCache(objectType);
				WriteObjectInfo.CheckTypeForwardedFrom(this.cache, objectType, this.binderAssemblyString);
				this.isSi = true;
			}
			if (!this.isSi)
			{
				this.InitMemberInfo();
				WriteObjectInfo.CheckTypeForwardedFrom(this.cache, objectType, this.binderAssemblyString);
			}
		}

		// Token: 0x060053A8 RID: 21416 RVA: 0x00128860 File Offset: 0x00126A60
		private void InitSiWrite()
		{
			this.isSi = true;
			SerializationInfoEnumerator enumerator = this.si.GetEnumerator();
			int memberCount = this.si.MemberCount;
			int num = memberCount;
			TypeInformation typeInformation = null;
			string fullTypeName = this.si.FullTypeName;
			string assemblyName = this.si.AssemblyName;
			bool hasTypeForwardedFrom = false;
			if (!this.si.IsFullTypeNameSetExplicit)
			{
				typeInformation = BinaryFormatter.GetTypeInformation(this.si.ObjectType);
				fullTypeName = typeInformation.FullTypeName;
				hasTypeForwardedFrom = typeInformation.HasTypeForwardedFrom;
			}
			if (!this.si.IsAssemblyNameSetExplicit)
			{
				if (typeInformation == null)
				{
					typeInformation = BinaryFormatter.GetTypeInformation(this.si.ObjectType);
				}
				assemblyName = typeInformation.AssemblyString;
				hasTypeForwardedFrom = typeInformation.HasTypeForwardedFrom;
			}
			this.cache = new SerObjectInfoCache(fullTypeName, assemblyName, hasTypeForwardedFrom);
			this.cache.memberNames = new string[num];
			this.cache.memberTypes = new Type[num];
			this.memberData = new object[num];
			enumerator = this.si.GetEnumerator();
			int num2 = 0;
			while (enumerator.MoveNext())
			{
				this.cache.memberNames[num2] = enumerator.Name;
				this.cache.memberTypes[num2] = enumerator.ObjectType;
				this.memberData[num2] = enumerator.Value;
				num2++;
			}
			this.isNamed = true;
			this.isTyped = false;
		}

		// Token: 0x060053A9 RID: 21417 RVA: 0x001289B4 File Offset: 0x00126BB4
		private static void CheckTypeForwardedFrom(SerObjectInfoCache cache, Type objectType, string binderAssemblyString)
		{
			if (cache.hasTypeForwardedFrom && binderAssemblyString == null && !FormatterServices.UnsafeTypeForwardersIsEnabled())
			{
				Assembly assembly = objectType.Assembly;
				if (!SerializationInfo.IsAssemblyNameAssignmentSafe(assembly.FullName, cache.assemblyString) && !assembly.IsFullyTrusted)
				{
					throw new SecurityException(Environment.GetResourceString("Serialization_RequireFullTrust", new object[]
					{
						objectType
					}));
				}
			}
		}

		// Token: 0x060053AA RID: 21418 RVA: 0x00128A10 File Offset: 0x00126C10
		private void InitNoMembers()
		{
			this.cache = (SerObjectInfoCache)this.serObjectInfoInit.seenBeforeTable[this.objectType];
			if (this.cache == null)
			{
				this.cache = new SerObjectInfoCache(this.objectType);
				this.serObjectInfoInit.seenBeforeTable.Add(this.objectType, this.cache);
			}
		}

		// Token: 0x060053AB RID: 21419 RVA: 0x00128A74 File Offset: 0x00126C74
		[SecurityCritical]
		private void InitMemberInfo()
		{
			this.cache = (SerObjectInfoCache)this.serObjectInfoInit.seenBeforeTable[this.objectType];
			if (this.cache == null)
			{
				this.cache = new SerObjectInfoCache(this.objectType);
				this.cache.memberInfos = FormatterServices.GetSerializableMembers(this.objectType, this.context);
				int num = this.cache.memberInfos.Length;
				this.cache.memberNames = new string[num];
				this.cache.memberTypes = new Type[num];
				for (int i = 0; i < num; i++)
				{
					this.cache.memberNames[i] = this.cache.memberInfos[i].Name;
					this.cache.memberTypes[i] = this.GetMemberType(this.cache.memberInfos[i]);
				}
				this.serObjectInfoInit.seenBeforeTable.Add(this.objectType, this.cache);
			}
			if (this.obj != null)
			{
				this.memberData = FormatterServices.GetObjectData(this.obj, this.cache.memberInfos);
			}
			this.isTyped = true;
			this.isNamed = true;
		}

		// Token: 0x060053AC RID: 21420 RVA: 0x00128BA3 File Offset: 0x00126DA3
		internal string GetTypeFullName()
		{
			return this.binderTypeName ?? this.cache.fullTypeName;
		}

		// Token: 0x060053AD RID: 21421 RVA: 0x00128BBA File Offset: 0x00126DBA
		internal string GetAssemblyString()
		{
			return this.binderAssemblyString ?? this.cache.assemblyString;
		}

		// Token: 0x060053AE RID: 21422 RVA: 0x00128BD1 File Offset: 0x00126DD1
		private void InvokeSerializationBinder(SerializationBinder binder)
		{
			if (binder != null)
			{
				binder.BindToName(this.objectType, out this.binderAssemblyString, out this.binderTypeName);
			}
		}

		// Token: 0x060053AF RID: 21423 RVA: 0x00128BF0 File Offset: 0x00126DF0
		internal Type GetMemberType(MemberInfo objMember)
		{
			Type result;
			if (objMember is FieldInfo)
			{
				result = ((FieldInfo)objMember).FieldType;
			}
			else
			{
				if (!(objMember is PropertyInfo))
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_SerMemberInfo", new object[]
					{
						objMember.GetType()
					}));
				}
				result = ((PropertyInfo)objMember).PropertyType;
			}
			return result;
		}

		// Token: 0x060053B0 RID: 21424 RVA: 0x00128C4C File Offset: 0x00126E4C
		internal void GetMemberInfo(out string[] outMemberNames, out Type[] outMemberTypes, out object[] outMemberData)
		{
			outMemberNames = this.cache.memberNames;
			outMemberTypes = this.cache.memberTypes;
			outMemberData = this.memberData;
			if (this.isSi && !this.isNamed)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_ISerializableMemberInfo"));
			}
		}

		// Token: 0x060053B1 RID: 21425 RVA: 0x00128C9C File Offset: 0x00126E9C
		private static WriteObjectInfo GetObjectInfo(SerObjectInfoInit serObjectInfoInit)
		{
			WriteObjectInfo writeObjectInfo;
			if (!serObjectInfoInit.oiPool.IsEmpty())
			{
				writeObjectInfo = (WriteObjectInfo)serObjectInfoInit.oiPool.Pop();
				writeObjectInfo.InternalInit();
			}
			else
			{
				writeObjectInfo = new WriteObjectInfo();
				WriteObjectInfo writeObjectInfo2 = writeObjectInfo;
				int objectInfoIdCount = serObjectInfoInit.objectInfoIdCount;
				serObjectInfoInit.objectInfoIdCount = objectInfoIdCount + 1;
				writeObjectInfo2.objectInfoId = objectInfoIdCount;
			}
			return writeObjectInfo;
		}

		// Token: 0x060053B2 RID: 21426 RVA: 0x00128CEF File Offset: 0x00126EEF
		private static void PutObjectInfo(SerObjectInfoInit serObjectInfoInit, WriteObjectInfo objectInfo)
		{
			serObjectInfoInit.oiPool.Push(objectInfo);
		}

		// Token: 0x04002626 RID: 9766
		internal int objectInfoId;

		// Token: 0x04002627 RID: 9767
		internal object obj;

		// Token: 0x04002628 RID: 9768
		internal Type objectType;

		// Token: 0x04002629 RID: 9769
		internal bool isSi;

		// Token: 0x0400262A RID: 9770
		internal bool isNamed;

		// Token: 0x0400262B RID: 9771
		internal bool isTyped;

		// Token: 0x0400262C RID: 9772
		internal bool isArray;

		// Token: 0x0400262D RID: 9773
		internal SerializationInfo si;

		// Token: 0x0400262E RID: 9774
		internal SerObjectInfoCache cache;

		// Token: 0x0400262F RID: 9775
		internal object[] memberData;

		// Token: 0x04002630 RID: 9776
		internal ISerializationSurrogate serializationSurrogate;

		// Token: 0x04002631 RID: 9777
		internal StreamingContext context;

		// Token: 0x04002632 RID: 9778
		internal SerObjectInfoInit serObjectInfoInit;

		// Token: 0x04002633 RID: 9779
		internal long objectId;

		// Token: 0x04002634 RID: 9780
		internal long assemId;

		// Token: 0x04002635 RID: 9781
		private string binderTypeName;

		// Token: 0x04002636 RID: 9782
		private string binderAssemblyString;
	}
}
